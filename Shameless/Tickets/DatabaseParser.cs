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

		public static Nintendo3DSTitle[] ParseFromDatabase(string databasePath)
		{
			var database = File.ReadAllText(databasePath);
			Func<JToken, string> value = t => t.ToString();
			var titles = JArray.Parse(database);

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

				var generated = new Nintendo3DSTitle(titleId, encKey, name, type, region, serial);
				entries.Add(generated);
			}

			return entries.ToArray();
		}
	}
}