namespace Shameless
{
	using System;
	using System.Drawing;
	using System.Windows.Forms;

	using Shameless.Tickets;

	public partial class QrCodeResultForm : Form
	{
		public QrCodeResultForm(string[][] qrContents, Image qrCode)
		{
			this.InitializeComponent();
			long totalSize = 0;

			foreach (var qrContent in qrContents)
			{
				var titleId = qrContent[0];
				var name = qrContent[1];

				this.titleInfoRichTextbox.AppendText(titleId + ": " + name);

				var size = qrContent[2];

				var sizeAsLong = long.Parse(size);
				totalSize += sizeAsLong;

				this.titleInfoRichTextbox.AppendText(Environment.NewLine + "Size: " + DatabaseParser.HumanReadableFileSize(sizeAsLong) + Environment.NewLine);


				this.titleInfoRichTextbox.AppendText(Environment.NewLine);

				var url = qrContent[3];
				this.urlRichTextbox.AppendText(url);
				this.urlRichTextbox.AppendText(Environment.NewLine);
			}

			if (qrContents.Length > 1)
			{
				this.titleInfoRichTextbox.AppendText("Total size: " + DatabaseParser.HumanReadableFileSize(totalSize));
			}

			this.qrCodeBox.Image = qrCode;
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
