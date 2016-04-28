namespace Shameless
{
	using System.Drawing;

	public class QrResult
	{
		public QrResult(string url, Bitmap qrCode)
		{
			this.Url = url;
			this.QrCode = qrCode;
		}

		public string Url { get; }

		public Bitmap QrCode { get; }
	}
}