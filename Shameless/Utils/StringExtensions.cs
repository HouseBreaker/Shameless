namespace Shameless.Utils
{
	using System;
	using System.Globalization;
	using System.Linq;
	using System.Text;

	public static class StringExtensions
	{
		/// <summary>
		/// /http://stackoverflow.com/questions/359827/ignoring-accented-letters-in-string-comparison
		/// </summary>
		public static string RemoveDiacritics(this string text)
		{
			var formD = text.Normalize(NormalizationForm.FormD);
			var sb = new StringBuilder();

			foreach (var ch in formD)
			{
				var uc = CharUnicodeInfo.GetUnicodeCategory(ch);
				if (uc != UnicodeCategory.NonSpacingMark)
				{
					sb.Append(ch);
				}
			}

			return sb.ToString().Normalize(NormalizationForm.FormC);
		}

		public static bool Contains(this string source, string toCheck, StringComparison comp)
		{
			return source != null && toCheck != null && source.IndexOf(toCheck, comp) >= 0;
		}

		public static string RemoveTrademarks(this string source)
		{
			var trademarks = new[] { '™', '®', '®', '\n' };

			var sb = new StringBuilder();

			foreach (var ch in source)
			{
				if (!trademarks.Contains(ch))
				{
					sb.Append(ch);
				}
			}

			return sb.ToString();
		}
	}
}
