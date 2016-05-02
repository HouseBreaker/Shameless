namespace Shameless
{
	using System;
	using System.Windows.Forms;

	using Shameless.TitleFiltering;

	public partial class FilterDialog : Form
	{
		public FilterDialog(TitleFilter titleFilter)
		{
			this.InitializeComponent();
			this.TitleFilter = titleFilter;
			this.InitializeCheckboxes(titleFilter);
		}

		public TitleFilter TitleFilter { get; }

		private void InitializeCheckboxes(TitleFilter titleFilter)
		{
			this.regions_ALL_Checkbox.Checked = titleFilter.RegionFilter["ALL"];
			this.regions_USA_Checkbox.Checked = titleFilter.RegionFilter["USA"];
			this.regions_EUR_Checkbox.Checked = titleFilter.RegionFilter["EUR"];
			this.regions_JPN_Checkbox.Checked = titleFilter.RegionFilter["JPN"];

			this.types_eShop_Checkbox.Checked = titleFilter.TypeFilter["eShop"];
			this.types_System_Checkbox.Checked = titleFilter.TypeFilter["System"];
			this.types_Demo_Checkbox.Checked = titleFilter.TypeFilter["Demo"];
			this.types_DLC_Checkbox.Checked = titleFilter.TypeFilter["DLC"];
			this.types_Update_Checkbox.Checked = titleFilter.TypeFilter["Update"];
			this.types_DSIWare_Checkbox.Checked = titleFilter.TypeFilter["DSIWare"];

			this.other_UnknownNames_Checkbox.Checked = titleFilter.OtherFilter["Unknown"];
		}

		private void applyButton_Click(object sender, EventArgs e)
		{
			this.UpdateFilter();
		}

		private void UpdateFilter()
		{
			this.TitleFilter.RegionFilter["ALL"] = this.regions_ALL_Checkbox.Checked;
			this.TitleFilter.RegionFilter["USA"] = this.regions_USA_Checkbox.Checked;
			this.TitleFilter.RegionFilter["EUR"] = this.regions_EUR_Checkbox.Checked;
			this.TitleFilter.RegionFilter["JPN"] = this.regions_JPN_Checkbox.Checked;

			this.TitleFilter.TypeFilter["eShop"] = this.types_eShop_Checkbox.Checked;
			this.TitleFilter.TypeFilter["System"] = this.types_System_Checkbox.Checked;
			this.TitleFilter.TypeFilter["Demo"] = this.types_Demo_Checkbox.Checked;
			this.TitleFilter.TypeFilter["DLC"] = this.types_DLC_Checkbox.Checked;
			this.TitleFilter.TypeFilter["Update"] = this.types_Update_Checkbox.Checked;
			this.TitleFilter.TypeFilter["DSIWare"] = this.types_DSIWare_Checkbox.Checked;

			this.TitleFilter.OtherFilter["Unknown"] = this.other_UnknownNames_Checkbox.Checked;
		}
	}
}