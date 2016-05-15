namespace Shameless.QRGeneration
{
	using System.Collections.Generic;
	using System.Drawing;
	using System.Linq;
	using System.Net;

	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

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

		public static string Shorten(string url)
		{
			string shortUrl;

			using (var client = new WebClient())
			{
				var response = client.DownloadString($"http://hec.su/api?url={url}");
				var json = (JObject)JsonConvert.DeserializeObject(response);

				shortUrl = json["short"].Value<string>();
			}

			return shortUrl;
		}
	}
}
