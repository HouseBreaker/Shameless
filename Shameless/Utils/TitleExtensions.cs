namespace Shameless.Utils
{
	using System.Windows.Forms;

	using Shameless.Tickets;

	public static class TitleExtensions
	{
		public static ListViewItem ToListViewItem(this Nintendo3DSTitle title)
		{
			string[] row = { title.TitleId, title.EncKey, title.Name, title.Region, title.Type, title.Serial };

			var item = new ListViewItem(row) { Tag = title.TitleId };
			return item;
		}
	}
}
