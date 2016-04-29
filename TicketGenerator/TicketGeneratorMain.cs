// ReSharper disable LocalizableElement
namespace TicketGenerator
{
	using System;
	using System.IO;
	using System.Linq;
	using System.Reflection;
	using System.Text;

	public class TicketGeneratorMain
	{
		public static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;
			PrintProgramVersion();
#if DEBUG
			// File.Delete(Files.DbPath);
			File.Delete(Files.CsvPath);
			if (Directory.Exists(Files.TicketsDir))
			{
				Directory.Delete(Files.TicketsDir, true);
			}

#endif

			if (args.Contains("-h"))
			{
				Console.WriteLine();
				Environment.Exit(0);
			}

			Nintendo3DSTitle[] entries;

			if (!File.Exists(Files.CsvPath))
			{
				Console.WriteLine("CSV data missing. Parsing from database...");

				if (!File.Exists(Files.DbPath))
				{
					Console.WriteLine("Downloading database...");
					DatabaseParser.DownloadDatabase(Files.DbPath);
				}

				entries = DatabaseParser.ParseFromDatabase(Files.DbPath);

				if (!args.Contains("-keepdb"))
				{
					Console.WriteLine("Deleting html database.");
					File.Delete(Files.DbPath);
				}
				else
				{
					Console.WriteLine("Keeping html database on disk.");
				}

				Console.Write("Writing title data to " + Files.CsvPath);
				DatabaseParser.WriteCsvData(entries, Files.CsvPath);
				Console.WriteLine(" Done!");
			}
			else
			{
				Console.WriteLine("Parsing from CSV data...");
				entries = DatabaseParser.ParseFromCsv(Files.CsvPath);
			}

			Console.WriteLine(entries.Length + " tickets are about to be generated. Continue?");
			Console.ReadKey();
			GenerateTickets(entries, Files.TicketsDir);

			Console.WriteLine("Done! Press any key to exit...");
			Console.ReadKey();
		}

		private static void PrintProgramVersion()
		{
			var assemblyName = Assembly.GetExecutingAssembly().GetName();
			var majorVersion = assemblyName.Version.Major;
			var minorVersion = assemblyName.Version.Minor;

			Console.WriteLine($"{assemblyName.Name} v{majorVersion}.{minorVersion}");
		}

		private static void GenerateTickets(Nintendo3DSTitle[] entries, string outputDir)
		{
			Directory.CreateDirectory(outputDir);

			var types = entries.Select(a => a.Type).Distinct().ToList();

			var total = entries.Length;
			var processed = 0;

			foreach (var type in types)
			{
				var ticketOutputPath = "tickets" + "\\" + type;
				Directory.CreateDirectory(ticketOutputPath);

				foreach (var title in entries.Where(a => a.Type == type))
				{
					TicketGenerator.GenerateTicket(title, $"{title.Name} ({title.Serial}).tik", $"{ticketOutputPath}\\{title.Region}");
					Console.Write("\r{0}/{1} processed", ++processed, total);
				}
			}

			Console.WriteLine();
		}
	}
}