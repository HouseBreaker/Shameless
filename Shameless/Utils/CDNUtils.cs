using System;
using System.Linq;

namespace Shameless.Utils
{
	using System.Net;

	public static class CDNUtils
	{
		public static long GetTitleSize(string titleId)
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
				var contentOffset = 0xB04 + 0x30 * i;
				var contentId = BitConversion.BytesToHex(tmd.Skip(contentOffset).Take(4));

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
	}
}
