namespace TicketGenerator
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.IO;
	using System.Linq;
	using System.Net;
	using System.Text;
	using System.Text.RegularExpressions;
	using System.Threading;
	using System.Web;

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

				tokens[2] = FixType(tokens[2]);

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
			var html = File.ReadAllLines(databasePath);

			html =
				html.SkipWhile(a => a.Trim() != @"<th>Serial</th>")
					.Skip(2)
					.TakeWhile(a => a.Trim() != @"</table>")
					.Select(a => a.Trim())
					.ToArray();

			var entries = new List<Nintendo3DSTitle>();

			var startIndex = 0;
			do
			{
				startIndex++;

				var entry = html.Skip(startIndex).TakeWhile(a => a != "</tr>").Select(HttpUtility.HtmlDecode).ToArray();

				const string HexRegex16 = "[0-9ABCDEFabcdef]{16}";
				const string HexRegex32 = "[0-9ABCDEFabcdef]{32}";
				const string TagContentRegex = @"(?<=<td>).*(?=<\/td>)";

				var titleId = Regex.Match(entry[0], HexRegex16).Value.ToUpper();

				if (!Nintendo3DSTitle.GetTitleType(titleId).Contains("System"))
				{
					var encKey = Regex.Match(entry[3], HexRegex32).Value.ToUpper();

					if (!string.IsNullOrWhiteSpace(encKey))
					{
						var type = Regex.Match(entry[5], TagContentRegex).Value;
						type = FixType(type);

						if (!string.IsNullOrWhiteSpace(type))
						{
							string name;

							// if there's a new line in the name
							var dualLineName = entry.Length == 10;

							if (dualLineName)
							{
								name = entry[6] + " " + entry[7];
							}
							else
							{
								name = entry[6];
							}

							name = Regex.Match(name, TagContentRegex).Value.Trim();

							if (!string.IsNullOrWhiteSpace(name))
							{
								var regionIndex = dualLineName ? 8 : 7;
								var region = Regex.Match(entry[regionIndex], TagContentRegex).Value;

								if (!string.IsNullOrWhiteSpace(region))
								{
									var serial = Regex.Match(entry[regionIndex + 1], TagContentRegex).Value;

									if (!string.IsNullOrWhiteSpace(serial))
									{
										var result = new Nintendo3DSTitle(titleId, encKey, name, type, region, serial);
										entries.Add(result);
									}
								}
							}
						}
					}
				}

				startIndex = Array.IndexOf(html, "<tr>", startIndex);
			}
			while (startIndex != -1);

			return entries.ToArray();
		}

		public static string FixType(string type)
		{
			switch (type)
			{
				case "eShop/Application":
					return "eShop";
				case "Unknown":
					return "DSIWare";
				default:
					return type;
			}
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
