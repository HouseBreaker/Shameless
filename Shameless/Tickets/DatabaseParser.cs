namespace Shameless.Tickets
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Net;

	using Newtonsoft.Json.Linq;

	public static class DatabaseParser
	{
		public static void DownloadDatabase(string outputPath)
		{
			using (var client = new WebClient())
			{
				client.DownloadFile("http://3ds.nfshost.com/json_enc", outputPath);
			}
		}

		public static void DownloadSizes(string sizesPath)
		{
			using (var client = new WebClient())
			{
				client.DownloadFile("http://housebreaker.net/sizes.json", sizesPath);
			}
		}

		public static Nintendo3DSTitle[] ParseFromDatabase(string databasePath, string sizesPath)
		{
			var database = File.ReadAllText(databasePath);
			var sizesDb = File.ReadAllText(sizesPath);

			Func<JToken, string> value = t => t.ToString();
			var titles = JArray.Parse(database);
			var sizes = JObject.Parse(sizesDb);

			var entries = new List<Nintendo3DSTitle>();
			foreach (var title in titles)
			{
				var titleId = value(title["titleID"]);
				var type = Nintendo3DSTitle.GetTitleType(value(titleId));
				var encKey = value(title["encTitleKey"]);
				if (string.IsNullOrWhiteSpace(encKey))
				{
					continue;
				}

				var name = value(title["name"]).Trim();

				if (string.IsNullOrWhiteSpace(name))
				{
					name = "Unknown";
				}

				var region = value(title["region"]);
				var serial = value(title["serial"]);
				long size = 0;

				if (sizes[titleId.ToUpper()] != null)
				{
					size = sizes[titleId.ToUpper()].Value<long>();
				}
				
				var generated = new Nintendo3DSTitle(titleId, encKey, name, type, region, serial, size);
				entries.Add(generated);
			}

			return entries.ToArray();
		}

		public static string HumanReadableFileSize(long size)
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
	}
}