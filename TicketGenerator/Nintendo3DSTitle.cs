namespace TicketGenerator
{
	using System;
	using System.Collections;
	using System.Collections.Generic;

	// ReSharper disable once InconsistentNaming
	public class Nintendo3DSTitle : IEnumerable<string>
	{
		public Nintendo3DSTitle(string titleId, string encKey, string name, string type, string region, string serial)
		{
			this.TitleId = titleId;
			this.EncKey = encKey;
			this.Name = name;
			this.Type = type;
			this.Region = region;
			this.Serial = serial;
		}

		public string TitleId { get; set; }

		public string EncKey { get; set; }

		public string Name { get; set; }

		public string Type { get; set; }

		public string Region { get; set; }

		public string Serial { get; set; }

		public static string GetTitleType(string titleId)
		{
			var titleTypes = new Dictionary<string, string>()
								{
									{ "00040000", "3DS Game" },
									{ "00040010", "System Application" },
									{ "0004001B", "System Data Archive" },
									{ "000400DB", "System Data Archive" },
									{ "0004009B", "System Data Archive" },
									{ "00040030", "System Applet" },
									{ "00040130", "System Module" },
									{ "00040138", "System Firmware" },
									{ "00040001", "Download Play Title" },
									{ "00048000", "TWL Application" },
									{ "00048005", "TWL System Application" },
									{ "0004800F", "TWL System Data Archive" },
									{ "00040002", "Game Demo" },
									{ "0004008C", "Addon DLC" },
								};

			var choppedTitleId = titleId.Substring(0, 8).ToUpper();
			return titleTypes.ContainsKey(choppedTitleId) ? titleTypes[choppedTitleId] : "Unknown type";
		}

		public IEnumerator<string> GetEnumerator()
		{
			var correctOrder = new List<string> { this.TitleId, this.EncKey, this.Type, this.Name, this.Region, this.Serial };
			return correctOrder.GetEnumerator();
		}

		public override bool Equals(object obj)
		{
			var other = obj as Nintendo3DSTitle;
			return this.TitleId.Equals(other.TitleId, StringComparison.OrdinalIgnoreCase);
		}

		public bool Equals(Nintendo3DSTitle other)
		{
			return string.Equals(this.TitleId, other.TitleId, StringComparison.OrdinalIgnoreCase);
		}

		public override int GetHashCode()
		{
			return this.TitleId != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(this.TitleId) : 0;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}