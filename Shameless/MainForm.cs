// ReSharper disable InconsistentNaming
namespace Shameless
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.IO;
	using System.Linq;
	using System.Net;
	using System.Reflection;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	using Shameless.DataGridViewStuff;
	using Shameless.QRGeneration;
	using Shameless.Resources;
	using Shameless.Tickets;
	using Shameless.TitleFiltering;

	using Utils;

	public partial class MainForm : Form
	{
		private ListSortDirection sortOrder = ListSortDirection.Ascending;

		private int lastSortedColumn = 2;

		private bool searchBoxInitialized;

		private bool isSearching;

		private SortableBindingList<Nintendo3DSTitle> titles;

		private Nintendo3DSTitle[] allTitles;

		private TitleFilter titleFilter = new TitleFilter();

		private string lastSearchTerm = string.Empty;

		public MainForm()
		{
			this.InitializeComponent();
		}

		private async void MainForm_Shown(object sender, EventArgs e)
		{
			this.SetVersion();

			this.currentTitleStatusLabel.Text = string.Empty;

			if (!File.Exists(Files.DbPath))
			{
				var result = MessageBox.Show(
					Properties.Resources.Disclaimer, 
					"boo", 
					MessageBoxButtons.OKCancel, 
					MessageBoxIcon.Warning);

				if (result == DialogResult.Cancel)
				{
					Environment.Exit(0);
				}

				this.statusProgressbar.Style = ProgressBarStyle.Marquee;
				this.UpdateAction("Downloading database...");
				await Task.Run(() => DatabaseParser.DownloadDatabase(Files.DbPath));

				this.UpdateAction($"Prettifying JSON in \"{Files.DbPath}\"...");
				File.WriteAllText(Files.DbPath, JsonPrettifier.FormatJson(File.ReadAllText(Files.DbPath)));
			}

			this.UpdateAction($"Reading data from \"{Files.DbPath}\"...");
			this.allTitles = DatabaseParser.ParseFromDatabase(Files.DbPath);

			if (File.Exists(Files.FilterPath))
			{
				this.titleFilter = TitleFilterStorage.ParseFilterSettings(Files.FilterPath);
			}

			this.titles = new SortableBindingList<Nintendo3DSTitle>(TitleFilter.FilterTitles(this.allTitles, this.titleFilter));

			this.titlesDataGrid.DoubleBuffered(true);
			this.titlesDataGrid.DataSource = this.titles;
			this.SortDataGrid();

			this.UpdateAction(string.Empty);
			this.currentTitleStatusLabel.Text = string.Empty;
			this.titlesCountLabel.Text = this.titles.Count + " titles";
			this.statusProgressbar.Style = ProgressBarStyle.Blocks;

			this.generateAllTicketsButton.Enabled = true;
			this.filterButton.Enabled = true;
			this.generateQrCodeButton.Enabled = true;
		}

		private void SetVersion()
		{
			var assemblyName = Assembly.GetExecutingAssembly().GetName();
			var majorVersion = assemblyName.Version.Major;
			var minorVersion = assemblyName.Version.Minor;

			this.Text = $"{assemblyName.Name} v{majorVersion}.{minorVersion}";
		}

		private void UpdateAction(string message)
		{
			this.currentActionLabel.Text = message;
		}

		private void searchBox_TextChanged(object sender, EventArgs e)
		{
			this.delayTimer.Stop();
			this.delayTimer.Start();
		}

		private void delayTimer_Tick(object sender, EventArgs e)
		{
			this.delayTimer.Stop();

			if (this.isSearching)
			{
				return;
			}

			this.isSearching = true;

			if (!string.IsNullOrWhiteSpace(this.searchBox.Text))
			{
				this.UpdateAction("Searching...");
				this.statusProgressbar.Style = ProgressBarStyle.Marquee;
			}

			this.SearchAsYouType();

			this.isSearching = false;

			this.statusProgressbar.Style = ProgressBarStyle.Blocks;

			if (this.titlesDataGrid.RowCount != this.titles.Count)
			{
				this.UpdateAction($"Found {this.titlesDataGrid.RowCount} items.");
			}
			else
			{
				this.UpdateAction(string.Empty);
			}
		}

		private void SearchAsYouType()
		{
			if (this.searchBox.Text == this.lastSearchTerm)
			{
				return;
			}

			if (string.IsNullOrWhiteSpace(this.searchBox.Text) || this.searchBox.SelectedText == this.searchBox.Text)
			{
				return;
			}

			Func<string, string, bool> containsSubstring = (x, y) => x.Contains(y, StringComparison.OrdinalIgnoreCase);

			var foundTitles =
				TitleFilter.FilterTitles(this.allTitles, this.titleFilter)
					.Where(
						a =>
						containsSubstring(a.Name.RemoveDiacritics(), this.searchBox.Text)
						|| containsSubstring(a.TitleId, this.searchBox.Text) || containsSubstring(a.Serial, this.searchBox.Text))
					.ToArray();

			if (foundTitles.Any())
			{
				this.titlesDataGrid.DataSource = new SortableBindingList<Nintendo3DSTitle>(foundTitles);
				this.SortDataGrid();
			}
			else
			{
				this.UpdateAction("No titles found.");
			}

			this.lastSearchTerm = this.searchBox.Text;
		}

		private void SortDataGrid()
		{
			this.titlesDataGrid.Sort(this.titlesDataGrid.Columns[this.lastSortedColumn], this.sortOrder);
		}

		private void searchBox_Enter(object sender, EventArgs e)
		{
			if (!this.searchBoxInitialized)
			{
				this.searchBox.Text = string.Empty;
				this.searchBox.ForeColor = DefaultForeColor;

				this.searchBoxInitialized = true;
			}
		}

		private async void generateQrCodeButton_Click(object sender, EventArgs e)
		{
			var selectedCells = this.titlesDataGrid.SelectedCells;

			var selectedRows =
				(from DataGridViewCell item in selectedCells select this.titlesDataGrid.Rows[item.RowIndex]).Distinct().ToArray();

			const byte maxTitles = 15;
			if (selectedRows.Length > maxTitles)
			{
				MessageBox.Show(
					$"If you make a QR code which has more than {maxTitles} titles, your 3DS will not read it. The camera on the thing isn't that good. Please select less titles.", 
					$"{maxTitles} title limit", 
					MessageBoxButtons.OK, 
					MessageBoxIcon.Error);
				return;
			}

			this.statusProgressbar.Style = ProgressBarStyle.Marquee;

			// var mboxResult =
			// 	MessageBox.Show(
			// 		$"Generate one QR code for all {selectedRows.Length} titles? If you select no, individual QR codes will be generated.",
			// 		"Multiple titles selected",
			// 		MessageBoxButtons.YesNo);

			// if (mboxResult == DialogResult.Yes)
			// {
			var qrContents = new List<string[]>();

			for (var index = 0; index < selectedRows.Length; index++)
			{
				var row = selectedRows[index];
				var cells = row.Cells;

				var titleId = cells[0].Value.ToString();
				var name = cells[2].Value.ToString();

				var url = "http://3ds.nfshost.com/ticket/" + titleId.ToLower();
				var shortUrl = await Task.Run(() => QrUtils.Shorten(url));

				this.UpdateAction($"Shortening url ({index + 1}/{selectedRows.Length})");

				long size = 0;

				if (this.showSizeCheckbox.Checked)
				{
					this.UpdateAction($"Getting title size ({index + 1}/{selectedRows.Length})");
					size = await Task.Run(() => CDNUtils.GetTitleSize(titleId));
				}

				var sizeString = size > 0 ? size.ToString() : string.Empty;

				qrContents.Add(new[] { titleId, name, sizeString, shortUrl });
			}

			var urls = qrContents.Select(a => a[3]).ToArray();

			var result = QrUtils.MakeUrlIntoQrCode(string.Join("\n", urls));

			var resultForm = new QrCodeResultForm(qrContents.ToArray(), result.QrCode);

			resultForm.Show(this);

			// }
			this.UpdateAction(string.Empty);
			this.statusProgressbar.Style = ProgressBarStyle.Blocks;
		}

		private async void generateAllTicketsButton_Click(object sender, EventArgs e)
		{
			var result =
				MessageBox.Show(
					$"Warning: Only tickets with the current filter applied will be generated.\r\n\r\n{this.titles.Count} tickets are about to be generated. Continue?", 
					"Ticket generation", 
					MessageBoxButtons.YesNo);

			if (result == DialogResult.Yes)
			{
				var dialog = new FolderBrowserDialog
								{
									SelectedPath = Environment.CurrentDirectory, 
									Description = "Folder where tickets will be outputted."
								};

				var dialogResult = dialog.ShowDialog(this);

				if (dialogResult == DialogResult.OK)
				{
					await Task.Run(() => this.GenerateTickets(this.titles.ToArray(), dialog.SelectedPath));
					MessageBox.Show(
						$"Successfully generated tickets at\r\n{dialog.SelectedPath}\\tickets", 
						"Success", 
						MessageBoxButtons.OK, 
						MessageBoxIcon.Information);
				}
			}
		}

		private void GenerateTickets(Nintendo3DSTitle[] entries, string outputDir)
		{
			this.statusProgressbar.Style = ProgressBarStyle.Blocks;

			Directory.CreateDirectory(outputDir);

			var types = entries.Select(a => a.Type).Distinct().ToList();

			var total = entries.Length;
			var processed = 0;

			foreach (var type in types)
			{
				var ticketOutputPath = "tickets" + "\\" + type;
				Directory.CreateDirectory(ticketOutputPath);

				var filtered = entries.Where(a => a.Type == type).ToArray();

				foreach (var title in filtered)
				{
					TicketGenerator.GenerateTicket(title, $"{title.Name} ({title.Serial}).tik", $"{ticketOutputPath}\\{title.Region}");

					processed++;

					var percentageDone = (int)Math.Ceiling((double)processed / total * 100);

					if (percentageDone != this.statusProgressbar.Value)
					{
						this.progressUpdater.ReportProgress(percentageDone, new[] { processed, total });
					}
				}
			}
		}

		private void progressUpdater_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			this.statusProgressbar.Value = e.ProgressPercentage;
			this.UpdateAction($"Generating tickets: {e.ProgressPercentage}%");
		}

		private void filterButton_Click(object sender, EventArgs e)
		{
			var dialog = new FilterDialog(this.titleFilter.Clone());
			var result = dialog.ShowDialog();

			if (result == DialogResult.OK)
			{
				if (!dialog.TitleFilter.Equals(this.titleFilter))
				{
					this.titleFilter = dialog.TitleFilter.Clone();
					TitleFilterStorage.WriteFilterSettings(this.titleFilter, Files.FilterPath);

					this.FilterTitlesAndUpdate();
				}
			}
		}

		private void FilterTitlesAndUpdate()
		{
			this.UpdateAction("Filtering...");
			this.statusProgressbar.Style = ProgressBarStyle.Marquee;

			this.titlesDataGrid.DataSource =
				this.titles = new SortableBindingList<Nintendo3DSTitle>(TitleFilter.FilterTitles(this.allTitles, this.titleFilter));

			this.UpdateAction(string.Empty);
			this.statusProgressbar.Style = ProgressBarStyle.Blocks;
			this.titlesCountLabel.Text = $"{this.titles.Count} titles";
		}

		private void titlesDataGrid_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (this.lastSortedColumn == e.ColumnIndex)
			{
				this.sortOrder = this.sortOrder ^= ListSortDirection.Descending;
			}
			else
			{
				this.sortOrder = ListSortDirection.Ascending;
			}

			this.lastSortedColumn = e.ColumnIndex;
			this.SortDataGrid();
		}

		private async void checkUpdatesButton_Click(object sender, EventArgs e)
		{
			var result = MessageBox.Show("Check for updates?", "Update", MessageBoxButtons.YesNo);

			if (result == DialogResult.No)
			{
				return;
			}

			this.statusProgressbar.Style = ProgressBarStyle.Marquee;

			const string tempPath = Files.DbPath + "_temp";

			this.UpdateAction("Downloading new database.");
			using (var client = new WebClient())
			{
				await Task.Run(() => client.DownloadFile("http://3ds.nfshost.com/json_enc", tempPath));
			}

			this.UpdateAction("Done!");

			this.UpdateAction("Parsing...");

			var downloadedTitles = DatabaseParser.ParseFromDatabase(tempPath);

			var newTitles = downloadedTitles.Except(this.allTitles).ToList();
			var newTitleCount = newTitles.Count;

			this.statusProgressbar.Style = ProgressBarStyle.Blocks;

			if (newTitleCount == 0)
			{
				File.Delete(tempPath);
				this.UpdateAction(string.Empty);
				MessageBox.Show("Database is up to date.", "Good news!");
				return;
			}

			if (newTitles.Except(this.allTitles).Count() > 10)
			{
				newTitles = newTitles.Take(10).ToList();
			}

			var more = newTitleCount != newTitles.Count ? $" \r\n and {newTitleCount - 10} more" : string.Empty;

			var parseResult = MessageBox.Show(
				$"New titles:\r\n {string.Join("\r\n", newTitles)}{more}. Update?", 
				"New titles found", 
				MessageBoxButtons.YesNo);

			if (parseResult == DialogResult.Yes)
			{
				var titles = JsonPrettifier.FormatJson(File.ReadAllText(tempPath));
				File.WriteAllText(Files.DbPath, titles);
				File.Delete(tempPath);

				this.allTitles = downloadedTitles;
				this.FilterTitlesAndUpdate();

				this.UpdateAction("Database updated successfully!");
			}
		}
	}
}