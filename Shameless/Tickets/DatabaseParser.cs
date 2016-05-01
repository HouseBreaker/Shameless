namespace Shameless.Tickets
{
	using System;
	using System.IO;
	using System.Linq;
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
			var entries = from title in titles
						  let titleId = value(title["titleID"])
						  let type = Nintendo3DSTitle.GetTitleType(value(titleId))
						  where !type.Contains("System")
						  let encKey = value(title["encTitleKey"])
						  let name = value(title["name"]).Trim()
						  where !string.IsNullOrWhiteSpace(encKey) && !string.IsNullOrWhiteSpace(name)
						  let region = value(title["region"])
						  let serial = value(title["serial"])
						  select new Nintendo3DSTitle(titleId, encKey, name, type, region, serial);

			return entries.ToArray();
		}
	}
}