namespace Shameless.ColumnComparers
{
	using System.Collections;
	using System.Windows.Forms;

	public class CompareDescending : IComparer
	{
		private int col;

		public CompareDescending()
		{
			this.col = 0;
		}

		public CompareDescending(int column)
		{
			this.col = column;
		}

		public int Compare(object x, object y)
		{
			return string.CompareOrdinal(((ListViewItem)y).SubItems[this.col].Text, ((ListViewItem)x).SubItems[this.col].Text);
		}
	}
}
