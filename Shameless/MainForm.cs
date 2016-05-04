// ReSharper disable InconsistentNaming
namespace Shameless
{
	using System;
	using System.ComponentModel;
	using System.IO;
	using System.Linq;
	using System.Net;
	using System.Reflection;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	using Shameless.ColumnComparers;
	using Shameless.QRGeneration;
	using Shameless.Resources;
	using Shameless.Tickets;
	using Shameless.TitleFiltering;

	using Utils;

	public partial class MainForm : Form
	{
		private bool sortColumnsAscending;

		private int lastSortedColumn;

		private bool searchBoxInitialized;

		private bool isSearching;

		private Nintendo3DSTitle[] titles;

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
			
			ServicePointManager.ServerCertificateValidationCallback = (sender2, certificate, chain, sslPolicyErrors) => true;

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

			await this.FilterTitlesAndUpdate();

			// this.UpdateAction("Sorting...");
			// this.statusProgressbar.Style = ProgressBarStyle.Marquee;
			// this.titlesListView.ListViewItemSorter = new CompareAscending(2);
			// this.statusProgressbar.Style = ProgressBarStyle.Blocks;
			this.UpdateAction(string.Empty);
			this.currentTitleStatusLabel.Text = string.Empty;
			this.titlesCountLabel.Text = this.titles.Length + " titles";
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

		private void UpdateTitlesList(Nintendo3DSTitle[] titles)
		{
#if DEBUG
			var st = new Stopwatch();
			st.Start();
#endif
			this.titlesListView.BeginUpdate();

			this.titlesListView.Sorting = SortOrder.None;

			this.titlesListView.Items.Clear();

			foreach (var title in titles)
			{
				this.titlesListView.Items.Add(title.ToListViewItem());
			}

			this.titlesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

			this.titlesListView.ListViewItemSorter = new CompareAscending(2);
			this.titlesListView.AutoSize = true;
			this.ResizeNameColumn();

			this.titlesListView.EndUpdate();
#if DEBUG
			st.Stop();

			MessageBox.Show($"Time to update titles list: {st.ElapsedMilliseconds}ms");
#endif
		}

		private void ResizeNameColumn()
		{
			if (this.titlesListView.Items.Count > 0)
			{
				var size = 10
							* (int)Math.Ceiling(this.titlesListView.Items.Cast<ListViewItem>().Average(item => item.SubItems[2].Text.Length));
				this.titlesListView.Columns[2].Width = size;
			}
			else
			{
				foreach (ColumnHeader column in this.titlesListView.Columns)
				{
					column.Width = 50;
				}

				// Encrypted title key column
				this.titlesListView.Columns[1].Width = 110;
			}
		}

		private void titlesListView_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if (e.Column != this.lastSortedColumn)
			{
				this.titlesListView.ListViewItemSorter = new CompareAscending(e.Column);
				this.lastSortedColumn = e.Column;
			}
			else
			{
				if (this.sortColumnsAscending)
				{
					this.titlesListView.ListViewItemSorter = new CompareAscending(e.Column);
					this.sortColumnsAscending = false;
				}
				else
				{
					this.titlesListView.ListViewItemSorter = new CompareDescending(e.Column);
					this.sortColumnsAscending = true;
				}
			}
		}

		private void searchBox_TextChanged(object sender, EventArgs e)
		{
			this.delayTimer.Stop();
			this.delayTimer.Start();
		}

		private async void delayTimer_Tick(object sender, EventArgs e)
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

			await Task.Run(() => this.SearchAsYouType());

			this.isSearching = false;

			this.statusProgressbar.Style = ProgressBarStyle.Blocks;

			if (this.titlesListView.Items.Count != this.titles.Length)
			{
				this.UpdateAction($"Found {this.titlesListView.Items.Count} items.");
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
				if (this.titlesListView.Items.Count != this.titles.Length)
				{
					this.UpdateTitlesList(this.titles);
					this.lastSearchTerm = this.searchBox.Text;
				}

				return;
			}

			Func<string, string, bool> containsSubstring = (x, y) => x.Contains(y, StringComparison.OrdinalIgnoreCase);

			var foundTitles =
				this.titles.Where(
					a =>
					containsSubstring(a.Name.RemoveDiacritics(), this.searchBox.Text)
					|| containsSubstring(a.TitleId, this.searchBox.Text) || containsSubstring(a.Serial, this.searchBox.Text)).ToArray();

			if (foundTitles.Any())
			{
				this.UpdateTitlesList(foundTitles);
			}
			else
			{
				MessageBox.Show("No titles found.");
			}

			this.lastSearchTerm = this.searchBox.Text;
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
			foreach (ListViewItem item in this.titlesListView.SelectedItems)
			{
				var subItems = item.SubItems;

				var titleId = subItems[0].Text;
				var titleKey = subItems[1].Text;
				var name = subItems[2].Text;
				var serial = subItems[5].Text;

				this.currentTitleStatusLabel.Text = name;

				var title = new Nintendo3DSTitle(titleId, titleKey, name, string.Empty, string.Empty, serial);
				var ticketFileName = $"{title.Name} ({title.Serial}).tik";
				TicketGenerator.GenerateTicket(title, ticketFileName, Environment.CurrentDirectory);

				this.UpdateAction("Generating ticket...");
				this.statusProgressbar.Style = ProgressBarStyle.Marquee;
				var result = await Task.Run(() => QrUtils.MakeTicketIntoQrCode(TicketGenerator.SanitizeFileName(ticketFileName)));
				this.statusProgressbar.Style = ProgressBarStyle.Blocks;

				File.Delete(TicketGenerator.SanitizeFileName(ticketFileName));

				long size = 0;

				if (this.showSizeCheckbox.Checked)
				{
					this.UpdateAction("Getting title size...");
					this.statusProgressbar.Style = ProgressBarStyle.Marquee;
					size = await Task.Run(() => CDNUtils.GetTitleSize(titleId));
					this.statusProgressbar.Style = ProgressBarStyle.Blocks;
				}

				var resultForm = new QrCodeResultForm(titleId, name, size, result.Url, result.QrCode);
				resultForm.Show(this);

				this.currentActionLabel.Text = string.Empty;
			}

			this.currentTitleStatusLabel.Text = string.Empty;
		}

		

		private async void generateAllTicketsButton_Click(object sender, EventArgs e)
		{
			var result = MessageBox.Show(
				$"Warning: Only tickets with the current filter applied will be generated.\r\n\r\n{this.titles.Length} tickets are about to be generated. Continue?", 
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
					await Task.Run(() => this.GenerateTickets(this.titles, dialog.SelectedPath));
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

		private async void filterButton_Click(object sender, EventArgs e)
		{
			var dialog = new FilterDialog(this.titleFilter.Clone());
			var result = dialog.ShowDialog();

			if (result == DialogResult.OK)
			{
				if (!dialog.TitleFilter.Equals(this.titleFilter))
				{
					this.titleFilter = dialog.TitleFilter.Clone();
					TitleFilterStorage.WriteFilterSettings(this.titleFilter, Files.FilterPath);

					await this.FilterTitlesAndUpdate();
				}
			}
		}

		private async Task FilterTitlesAndUpdate()
		{
			this.UpdateAction("Filtering...");
			this.statusProgressbar.Style = ProgressBarStyle.Marquee;

			await Task.Run(() => this.titles = TitleFilter.FilterTitles(this.allTitles, this.titleFilter));

			this.UpdateAction("Updating title list...");
			await Task.Run(() => this.UpdateTitlesList(this.titles));

			this.UpdateAction(string.Empty);
			this.statusProgressbar.Style = ProgressBarStyle.Blocks;
			this.titlesCountLabel.Text = $"{this.titlesListView.Items.Count} titles";
		}
	}
}