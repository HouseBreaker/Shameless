namespace Shameless.QRGeneration
{
	using System.Drawing;
	using ZXing;
	using ZXing.Common;

	public static class QrUtils
	{
		public static QrResult MakeUrlIntoQrCode(string url)
		{
			var writer = new BarcodeWriter
			{
				Format = BarcodeFormat.QR_CODE,
				Options = new EncodingOptions { Height = 325, Width = 325 }
			};

			var result = writer.Write(url);

			var qrResult = new QrResult(url, new Bitmap(result));
			return qrResult;
		}
	}
}
