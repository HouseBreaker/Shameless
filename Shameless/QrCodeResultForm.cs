namespace Shameless
{
	using System;
	using System.Drawing;
	using System.Windows.Forms;

	public partial class QrCodeResultForm : Form
	{
		public QrCodeResultForm(string[][] qrContents, Image qrCode)
		{
			this.InitializeComponent();

			var showSize = false;
			long totalSize = 0;

			foreach (var qrContent in qrContents)
			{
				var titleId = qrContent[0];
				var name = qrContent[1];

				this.titleInfoRichTextbox.AppendText(titleId + ": " + name);
				
				var size = qrContent[2];
				if (size != string.Empty)
				{
					showSize = true;
					var sizeAsLong = long.Parse(size);
					totalSize += sizeAsLong;

					this.titleInfoRichTextbox.AppendText(Environment.NewLine + "Size: " + HumanReadableFileSize(sizeAsLong) + Environment.NewLine);
				}

				this.titleInfoRichTextbox.AppendText(Environment.NewLine);

				var url = qrContent[3];
				this.urlRichTextbox.AppendText(url);
				this.urlRichTextbox.AppendText(Environment.NewLine);
			}

			if (showSize && qrContents.Length > 1)
			{
				this.titleInfoRichTextbox.AppendText("Total size: " + HumanReadableFileSize(totalSize));
			}
			
			this.qrCodeBox.Image = qrCode;
		}

		private static string HumanReadableFileSize(long size)
		{
			string[] sizes = { "B", "KB", "MB", "GB" };

			var order = 0;

			double actualSize = size;

			while (actualSize >= 1024 && order + 1 < sizes.Length)
			{
				order++;
				actualSize /= 1024;
			}

			return $"{actualSize:0.##} {sizes[order]}";
		}

		private void QrCodeResultForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				this.Close();
			}
		}
	}
}
