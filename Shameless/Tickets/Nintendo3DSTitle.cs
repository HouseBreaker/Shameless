namespace Shameless.Tickets
{
	using System;
	using System.Collections.Generic;

	using Shameless.Utils;

	// ReSharper disable once InconsistentNaming
	public class Nintendo3DSTitle
	{
		public Nintendo3DSTitle(string titleId, string encKey, string name, string type, string region, string serial, long size)
		{
			// validation in the constructor and not in the properties. my uni would be so proud.
			this.TitleId = titleId.ToUpper();
			this.EncKey = encKey.ToUpper();
			this.Name = string.IsNullOrWhiteSpace(name) ? "Unknown" : name.RemoveTrademarks().Replace('\n', ' ');
			this.Type = type;
			this.Region = string.IsNullOrWhiteSpace(region) ? "Unknown" : region;
			this.Serial = serial;
			this.Size = size;
			this.HumanReadableSize = DatabaseParser.HumanReadableFileSize(size);
		}

		public string TitleId { get; }

		public string EncKey { get; }

		public string Name { get; }

		public string Type { get; }

		public string Region { get; }

		public string Serial { get; }

		public long Size { get; set; }

		public string HumanReadableSize { get; set; }

		public static string GetTitleType(string titleId)
		{
			var titleTypes = new Dictionary<string, string>
								{
									{ "00040000", "eShop" }, 
									{ "00040010", "System Application" }, 
									{ "0004001B", "System Data Archive" }, 
									{ "000400DB", "System Data Archive" }, 
									{ "0004009B", "System Data Archive" }, 
									{ "00040030", "System Applet" }, 
									{ "00040130", "System Module" }, 
									{ "00040138", "System Firmware" }, 
									{ "00040001", "Download Play Title" }, 
									{ "00048005", "DSIWare System Application" }, 
									{ "0004800F", "DSIWare System Data Archive" }, 
									{ "00048000", "DSIWare" }, 
									{ "00048004", "DSIWare" }, 
									{ "0004000E", "Update" }, 
									{ "00040002", "Demo" }, 
									{ "0004008C", "DLC" }, 
								};

			var choppedTitleId = titleId.Substring(0, 8).ToUpper();
			return titleTypes.ContainsKey(choppedTitleId) ? titleTypes[choppedTitleId] : "Unknown type";
		}

		public override bool Equals(object obj)
		{
			var other = obj as Nintendo3DSTitle;
			return this.TitleId.Equals(other.TitleId, StringComparison.OrdinalIgnoreCase);
		}

		public override int GetHashCode()
		{
			return this.TitleId != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(this.TitleId) : 0;
		}

		public override string ToString()
		{
			return this.TitleId + ": " + this.Name;
		}
	}
}