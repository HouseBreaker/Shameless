// ReSharper disable InconsistentNaming
namespace Shameless
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.IO;
	using System.Linq;
	using System.Net;
	using System.Reflection;
	using System.Threading.Tasks;
	using System.Windows.Forms;
	using Shameless.ColumnComparers;
	using Shameless.Tickets;
	using Utils;
	using ZXing;
	using ZXing.Common;

	public partial class MainForm : Form
	{
		private bool sortColumnsAscending;

		private int lastSortedColumn;

		private bool searchBoxInitialized;

		private Nintendo3DSTitle[] titles;

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
			}
			
			this.UpdateAction($"Reading data from \"{Files.DbPath}\"...");
			this.DeserializeJson();

			this.UpdateAction($"Prettifying JSON in \"{Files.DbPath}\"...");
			File.WriteAllText(Files.DbPath, JsonPrettifier.FormatJson(File.ReadAllText(Files.DbPath)));

			this.UpdateAction(string.Empty);
			this.currentTitleStatusLabel.Text = string.Empty;
			this.titlesCountLabel.Text = this.titles.Length + " titles";
			this.statusProgressbar.Style = ProgressBarStyle.Blocks;
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

		private void DeserializeJson()
		{
			this.titles = DatabaseParser.ParseFromDatabase(Files.DbPath);

			this.titlesListView.BeginUpdate();
			
			foreach (var title in this.titles)
			{
				title.Name = title.Name.RemoveTrademarks();
				this.titlesListView.Items.Add(title.ToListViewItem());
			}

			this.titlesListView.ListViewItemSorter = new CompareAscending(2);
			this.titlesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

			this.ResizeNameColumn();

			this.titlesListView.EndUpdate();
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
					var items = this.titles.Select(a => a.ToListViewItem()).ToArray();

					this.titlesListView.BeginUpdate();

					this.titlesListView.Items.Clear();
					this.titlesListView.Items.AddRange(items);

					this.titlesListView.EndUpdate();

					this.lastSearchTerm = this.searchBox.Text;
				}

				return;
			}

			Func<string, string, bool> containsSubstring =
				(x, y) => x.RemoveDiacritics().Contains(y, StringComparison.OrdinalIgnoreCase);

			var foundTitles =
				this.titles.Where(
					a =>
					containsSubstring(a.Name, this.searchBox.Text) || containsSubstring(a.TitleId, this.searchBox.Text)
					|| containsSubstring(a.Serial, this.searchBox.Text)).ToArray();

			if (foundTitles.Any())
			{
				this.titlesListView.BeginUpdate();

				this.titlesListView.Items.Clear();

				var titlesAsListViewItems = foundTitles.Select(a => a.ToListViewItem());

				this.titlesListView.Items.AddRange(titlesAsListViewItems.ToArray());

				this.titlesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
				this.ResizeNameColumn();

				this.titlesListView.EndUpdate();
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
				var result = await Task.Run(() => MakeTicketIntoQrCode(TicketGenerator.SanitizeFileName(ticketFileName)));
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

		private static QrResult MakeTicketIntoQrCode(string path)
		{
			var resultUrl = UploadToTempHost(path);

			var writer = new BarcodeWriter
							{
								Format = BarcodeFormat.QR_CODE, 
								Options = new EncodingOptions { Height = 275, Width = 275 }
							};

			var result = writer.Write(resultUrl);

			var qrResult = new QrResult(resultUrl, new Bitmap(result));
			return qrResult;
		}

		private static string UploadToTempHost(string path)
		{
			string response;
			using (var client = new WebClient())
			{
				var responseBytes = client.UploadFile("https://uguu.se/api.php?d=upload-tool", path);
				response = client.Encoding.GetString(responseBytes);
			}

			return response;
		}

		private async void delayTimer_Tick(object sender, EventArgs e)
		{
			this.delayTimer.Stop();

			if (!string.IsNullOrWhiteSpace(this.searchBox.Text))
			{
				this.UpdateAction("Searching...");
				this.statusProgressbar.Style = ProgressBarStyle.Marquee;
			}

			await Task.Run(() => this.SearchAsYouType());

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

		private async void generateAllTicketsButton_Click(object sender, EventArgs e)
		{
			var result = MessageBox.Show(
				$"{this.titles.Length} tickets are about to be generated. Continue?", 
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
	}
}