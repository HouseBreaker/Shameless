namespace Shameless
{
	using System.Drawing;
	using System.Windows.Forms;

	public partial class QrCodeResultForm : Form
	{
		public QrCodeResultForm(string titleId, string name, long size, string url, Image qrCode)
		{
			this.InitializeComponent();

			this.titleIdLabel.Text = this.titleIdLabel.Text + titleId;
			this.nameLabel.Text = this.nameLabel.Text + name;
			this.sizeLabel.Text = size == 0 ? string.Empty : this.sizeLabel.Text + HumanReadableFileSize(size);
			this.urlLabel.Text = url;
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
