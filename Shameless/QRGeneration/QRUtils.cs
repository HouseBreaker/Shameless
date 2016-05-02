namespace Shameless.QRGeneration
{
	using System.Drawing;
	using System.Net;

	using ZXing;
	using ZXing.Common;

	public static class QrUtils
	{
		public static QrResult MakeTicketIntoQrCode(string path)
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
	}
}
