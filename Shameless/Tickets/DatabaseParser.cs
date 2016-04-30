namespace Shameless.Tickets
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.IO;
	using System.Linq;
	using System.Net;
	using System.Text;
	using System.Threading;
	using System.Web;

	using HtmlAgilityPack;

	public static class DatabaseParser
	{
		public static void WriteCsvData(IEnumerable<Nintendo3DSTitle> entries, string outputPath)
		{
			var csvResult = new StringBuilder();
			foreach (var entry in entries)
			{
				if (entry.Name.Contains(','))
				{
					entry.Name = '\"' + entry.Name + "\"";
				}

				csvResult.AppendLine(string.Join(",", entry));
			}

			File.WriteAllText(outputPath, csvResult.ToString(), Encoding.UTF8);
		}

		public static void DownloadDatabase(string outputPath)
		{
			using (var client = new WebClient())
			{
				client.DownloadProgressChanged += OnDownloadProgressChanged;
				client.DownloadFileCompleted += OnDownloadComplete;
				client.DownloadFileAsync(new Uri("http://3ds.nfshost.com/"), outputPath);

				while (client.IsBusy)
				{
					Thread.Sleep(200);
				}
			}
		}

		public static Nintendo3DSTitle[] ParseFromCsv(string csvPath)
		{
			var entries = new List<Nintendo3DSTitle>();

			var csv = File.ReadAllLines(csvPath);

			foreach (var line in csv)
			{
				var tokens = line.Split(new[] { ',', '\"' }, StringSplitOptions.RemoveEmptyEntries);

				// tokens[2] = FixType(tokens[2]);
				if (tokens.Length != 6)
				{
					// a terrible mess lies within this "if" statement
					var validTokens = tokens.Take(3).ToList();
					validTokens.AddRange(tokens.Reverse().Take(2));

					var nameParts = tokens.Except(validTokens);
					var name = string.Join(string.Empty, nameParts);

					var newTokens = new List<string>();
					newTokens.AddRange(tokens.Take(3));
					newTokens.Add(name);
					newTokens.AddRange(tokens.Skip(tokens.Length - 2).Take(2));

					tokens = newTokens.ToArray();
				}

				entries.Add(new Nintendo3DSTitle(tokens[0], tokens[1], tokens[3], tokens[2], tokens[4], tokens[5]));
			}

			return entries.ToArray();
		}

		public static Nintendo3DSTitle[] ParseFromDatabase(string databasePath)
		{
			var doc = new HtmlDocument();
			doc.Load(databasePath, Encoding.UTF8);

			Func<HtmlNode, string> titleData = node => HttpUtility.HtmlDecode(node.InnerText.Trim().Replace("\n", " "));

			var descendants = doc.DocumentNode.Descendants("table").First().Descendants("tr").Skip(1);

			descendants = descendants.Where(a => a.Descendants("td").All(b => !string.IsNullOrWhiteSpace(b.InnerText)));

			var entries = new List<Nintendo3DSTitle>();

			foreach (var descendant in descendants)
			{
				var children = descendant.Descendants("td").ToArray();

				var titleId = titleData(children[0]);
				var encKey = titleData(children[2]);
				var type = Nintendo3DSTitle.GetTitleType(titleId); // titleData(children[3]);
				var name = titleData(children[4]);
				var region = titleData(children[5]);
				var serial = titleData(children[6]);

				entries.Add(new Nintendo3DSTitle(titleId.ToUpper(), encKey.ToUpper(), name, type, region, serial));
			}

			return entries.ToArray();
		}

		public static void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			Console.Write($"\rDownloaded {e.BytesReceived} bytes. ");
		}

		public static void OnDownloadComplete(object sender, AsyncCompletedEventArgs e)
		{
			Console.WriteLine("Downloaded!");

			// clean up
			var client = (WebClient)sender;
			client.DownloadProgressChanged -= OnDownloadProgressChanged;
			client.DownloadFileCompleted -= OnDownloadComplete;

			if (e.Error != null)
			{
				throw e.Error;
			}
		}
	}
}