namespace Shameless.ColumnComparers
{
	using System.Collections;
	using System.Windows.Forms;

	public class CompareAscending : IComparer
	{
		private int col;

		public CompareAscending()
		{
			this.col = 0;
		}

		public CompareAscending(int column)
		{
			this.col = column;
		}

		public int Compare(object x, object y)
		{
			return string.CompareOrdinal(((ListViewItem)x).SubItems[this.col].Text, ((ListViewItem)y).SubItems[this.col].Text);
		}
	}
}
