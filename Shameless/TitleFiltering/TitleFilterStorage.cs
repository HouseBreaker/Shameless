namespace Shameless.TitleFiltering
{
	using System.IO;

	using Newtonsoft.Json;

	using Utils;

	public static class TitleFilterStorage
	{
		public static TitleFilter ParseFilterSettings(string filterPath)
		{
			var json = File.ReadAllText(filterPath);
			var filter = JsonConvert.DeserializeObject<TitleFilter>(json);

			return filter;
		}

		public static void WriteFilterSettings(TitleFilter filter, string filterPath)
		{
			var output = JsonPrettifier.FormatJson(JsonConvert.SerializeObject(filter));
			File.WriteAllText(filterPath, output);
		}
	}
}
