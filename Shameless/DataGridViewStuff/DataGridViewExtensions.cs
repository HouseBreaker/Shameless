namespace Shameless.DataGridViewStuff
{
	using System.Reflection;
	using System.Windows.Forms;

	/// <summary>
	/// http://bitmatic.com/c/fixing-a-slow-scrolling-datagridview
	/// </summary>
	public static class DatGridViewExtensions
	{
		public static void DoubleBuffered(this DataGridView dgv, bool setting)
		{
			var dgvType = dgv.GetType();
			var pi = dgvType.GetProperty("DoubleBuffered",
				BindingFlags.Instance | BindingFlags.NonPublic);
			pi.SetValue(dgv, setting, null);
		}
	}
}
