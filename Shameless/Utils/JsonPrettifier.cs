using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Shameless.Utils
{
	public static class JsonPrettifier
	{
		public static string FormatJson(string str)
		{
			var formatted = JObject.Parse(str).ToString(Formatting.Indented);
			return formatted;
		}
	}
}
