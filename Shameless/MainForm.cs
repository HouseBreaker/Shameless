// ReSharper disable InconsistentNaming
namespace Shameless
{
	using System;
	using System.Drawing;
	using System.Globalization;
	using System.IO;
	using System.Linq;
	using System.Net;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	using Shameless.ColumnComparers;

	using TicketGenerator;

	using ZXing;
	using ZXing.Common;

	using Files = Shameless.Resources.Files;

	public partial class MainForm : Form
	{
		private bool sortColumnsAscending;

		private int lastSortedColumn;

		private bool searchBoxInitialized;

		public MainForm()
		{
			this.InitializeComponent();

			// ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
		}

		private async void MainForm_Shown(object sender, EventArgs e)
		{
			ServicePointManager.ServerCertificateValidationCallback = (sender2, certificate, chain, sslPolicyErrors) => true;

			this.currentTitleStatusLabel.Text = string.Empty;

			if (!File.Exists(Files.CsvPath))
			{
				var result = MessageBox.Show(Properties.Resources.Disclaimer, "boo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

				if (result == DialogResult.Cancel)
				{
					Environment.Exit(0);
				}

				this.statusProgressbar.Style = ProgressBarStyle.Marquee;
				this.UpdateAction("Downloading database...");
				await Task.Run(() => DatabaseParser.DownloadDatabase(Files.DbPath));

				this.UpdateAction("Parsing Database...");
				var titles = await Task.Run(() => DatabaseParser.ParseFromDatabase(Files.DbPath));

				File.Delete("db.html");

				this.UpdateAction($"Writing data to \"{Files.CsvPath}\"...");
				await Task.Run(() => DatabaseParser.WriteCsvData(titles, Files.CsvPath));
			}

			this.UpdateAction($"Reading data from \"{Files.CsvPath}\"...");
			this.titlesListView.BeginUpdate();
			this.ReadDataCsvIntoListView();
			this.titlesListView.EndUpdate();

			this.UpdateAction(string.Empty);
			this.currentTitleStatusLabel.Text = string.Empty;
			this.titlesCountLabel.Text = this.titlesListView.Items.Count + " titles";
			this.statusProgressbar.Style = ProgressBarStyle.Blocks;
		}

		private void UpdateAction(string message)
		{
			this.currentActionLabel.Text = message;
		}

		private void ReadDataCsvIntoListView()
		{
			var entries = DatabaseParser.ParseFromCsv(Files.CsvPath);

			foreach (var title in entries)
			{
				string[] row = { title.TitleId, title.EncKey, title.Name, title.Region, title.Type, title.Serial };
				var listViewItem = new ListViewItem(row);

				this.allTitlesListView.Items.Add(listViewItem);
				this.titlesListView.Items.Add((ListViewItem)listViewItem.Clone());
			}

			this.titlesListView.ListViewItemSorter = new CompareAscending(2);
			this.titlesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
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
			var foundItems = new ListView.ListViewItemCollection(this.titlesListView);

			if (string.IsNullOrEmpty(this.searchBox.Text))
			{
				return;
			}

			this.titlesListView.BeginUpdate();

			this.titlesListView.Items.Clear();

			foreach (ListViewItem item in this.allTitlesListView.Items)
			{
				var subItems = item.SubItems;

				for (int i = 0; i < subItems.Count; i++)
				{
					var isNotTitleKey = i != 1;
					if (isNotTitleKey)
					{
						var subItem = RemoveDiacritics(subItems[i].Text);
						if (subItem.IndexOf(this.searchBox.Text, 0, StringComparison.CurrentCultureIgnoreCase) != -1)
						{
							foundItems.Add((ListViewItem)item.Clone());
							break;
						}
					}
				}
			}

			this.titlesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			this.titlesListView.EndUpdate();
		}

		/// <summary>
		/// /http://stackoverflow.com/questions/359827/ignoring-accented-letters-in-string-comparison
		/// </summary>
		private static string RemoveDiacritics(string text)
		{
			string formD = text.Normalize(NormalizationForm.FormD);
			StringBuilder sb = new StringBuilder();

			foreach (char ch in formD)
			{
				UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(ch);
				if (uc != UnicodeCategory.NonSpacingMark)
				{
					sb.Append(ch);
				}
			}

			return sb.ToString().Normalize(NormalizationForm.FormC);
		}

		private void searchBox_Click(object sender, EventArgs e)
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
					size = await Task.Run(() => GetTitleSize(titleId));
					this.statusProgressbar.Style = ProgressBarStyle.Blocks;
				}

				var resultForm = new QrCodeResultForm(titleId, name, size, result.Url, result.QrCode);
				resultForm.Show(this);

				this.currentActionLabel.Text = string.Empty;
			}

			this.currentTitleStatusLabel.Text = string.Empty;
		}

		private static long GetTitleSize(string titleId)
		{
			// translated from FunKeyCIA
			var cdnUrl = "http://ccs.cdn.c.shop.nintendowifi.net/ccs/download/" + titleId.ToUpper();

			byte[] tmd;

			using (var client = new WebClient())
			{
				tmd = client.DownloadData(cdnUrl + "/tmd");
			}

			const int TikOffset = 0x140;

			var contentCount = Convert.ToInt32(BitConversion.BytesToHex(tmd.Skip(TikOffset + 0x9E).Take(2)), 16);

			long size = 0;

			for (int i = 0; i < contentCount; i++)
			{
				var cOffs = 0xB04 + 0x30 * i;
				var contentId = BitConversion.BytesToHex(tmd.Skip(cOffs).Take(4));

				try
				{
					var req = WebRequest.Create(cdnUrl + "/" + contentId);

					using (var resp = req.GetResponse())
					{
						long currentSize;
						if (long.TryParse(resp.Headers.Get("Content-Length"), out currentSize))
						{
							size += currentSize;
						}
					}
				}
				catch (WebException)
				{
				}
			}

			return size;
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

		private void delayTimer_Tick(object sender, EventArgs e)
		{
			this.delayTimer.Stop();
			this.SearchAsYouType();
		}
	}
}