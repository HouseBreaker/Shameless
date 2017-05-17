namespace Shameless.Tickets
{
	using System;
	using System.IO;
	using System.Linq;

	using Shameless.Properties;
	using Shameless.Utils;

	public static class TicketGenerator
	{
		public static void GenerateTicket(Nintendo3DSTitle title, string fileName, string outputDir)
		{
			var ticket = Convert.FromBase64String(Resources.TikTemplate);
			var titleId = BitConversion.HexToBytes(title.TitleId);
			var titleKey = BitConversion.HexToBytes(title.EncKey);

			const int titleKeyOffset = 0x1BF;
			for (var offset = titleKeyOffset; offset < titleKeyOffset + 0x10; offset++)
			{
				ticket[offset] = titleKey[offset - titleKeyOffset];
			}

			const int titleIdOffset = 0x1DC;
			for (int offset = titleIdOffset; offset < titleIdOffset + 0x8; offset++)
			{
				ticket[offset] = titleId[offset - titleIdOffset];
			}

			if (!Directory.Exists(outputDir))
			{
				Directory.CreateDirectory(outputDir);
			}

			fileName = SanitizeFileName(fileName);

			File.WriteAllBytes($"{outputDir}\\{fileName}", ticket);
		}

		public static byte[] GenerateTicket(Nintendo3DSTitle title)
		{
			var ticket = Convert.FromBase64String(Resources.TikTemplate);
			var titleId = BitConversion.HexToBytes(title.TitleId);
			var titleKey = BitConversion.HexToBytes(title.EncKey);

			const int titleKeyOffset = 0x1BF;
			for (var offset = titleKeyOffset; offset < titleKeyOffset + 0x10; offset++)
			{
				ticket[offset] = titleKey[offset - titleKeyOffset];
			}

			const int titleIdOffset = 0x1DC;
			for (int offset = titleIdOffset; offset < titleIdOffset + 0x8; offset++)
			{
				ticket[offset] = titleId[offset - titleIdOffset];
			}

			return ticket;
		}

		public static string SanitizeFileName(string title)
		{
			var sanitizedName = title;
			var invalidFileNameChars = Path.GetInvalidFileNameChars().ToList();
			invalidFileNameChars.AddRange(new[] { '™', '®', '®' }); // it's annoying...

			foreach (var invalidFileNameChar in invalidFileNameChars)
			{
				if (sanitizedName.Contains(invalidFileNameChar))
				{
					sanitizedName = sanitizedName.Replace(invalidFileNameChar.ToString(), string.Empty);
				}
			}

			sanitizedName = sanitizedName.Trim();
			return sanitizedName;
		}
	}
}
