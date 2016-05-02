namespace Shameless
{
	partial class FilterDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.applyButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.regions_ALL_Checkbox = new System.Windows.Forms.CheckBox();
			this.regionsGroupBox = new System.Windows.Forms.GroupBox();
			this.regions_JPN_Checkbox = new System.Windows.Forms.CheckBox();
			this.regions_EUR_Checkbox = new System.Windows.Forms.CheckBox();
			this.regions_USA_Checkbox = new System.Windows.Forms.CheckBox();
			this.typesGroupBox = new System.Windows.Forms.GroupBox();
			this.types_DSIWare_Checkbox = new System.Windows.Forms.CheckBox();
			this.types_Update_Checkbox = new System.Windows.Forms.CheckBox();
			this.types_DLC_Checkbox = new System.Windows.Forms.CheckBox();
			this.types_Demo_Checkbox = new System.Windows.Forms.CheckBox();
			this.types_System_Checkbox = new System.Windows.Forms.CheckBox();
			this.types_eShop_Checkbox = new System.Windows.Forms.CheckBox();
			this.other_UnknownNames_Checkbox = new System.Windows.Forms.CheckBox();
			this.otherGroupBox = new System.Windows.Forms.GroupBox();
			this.regionsGroupBox.SuspendLayout();
			this.typesGroupBox.SuspendLayout();
			this.otherGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// applyButton
			// 
			this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.applyButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.applyButton.Location = new System.Drawing.Point(12, 205);
			this.applyButton.Name = "applyButton";
			this.applyButton.Size = new System.Drawing.Size(76, 23);
			this.applyButton.TabIndex = 5;
			this.applyButton.Text = "Apply";
			this.applyButton.UseVisualStyleBackColor = true;
			this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(94, 205);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(76, 23);
			this.cancelButton.TabIndex = 5;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// regions_ALL_Checkbox
			// 
			this.regions_ALL_Checkbox.AutoSize = true;
			this.regions_ALL_Checkbox.Location = new System.Drawing.Point(6, 19);
			this.regions_ALL_Checkbox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.regions_ALL_Checkbox.Name = "regions_ALL_Checkbox";
			this.regions_ALL_Checkbox.Size = new System.Drawing.Size(45, 17);
			this.regions_ALL_Checkbox.TabIndex = 1;
			this.regions_ALL_Checkbox.Text = "ALL";
			this.regions_ALL_Checkbox.UseVisualStyleBackColor = true;
			// 
			// regionsGroupBox
			// 
			this.regionsGroupBox.Controls.Add(this.regions_JPN_Checkbox);
			this.regionsGroupBox.Controls.Add(this.regions_EUR_Checkbox);
			this.regionsGroupBox.Controls.Add(this.regions_USA_Checkbox);
			this.regionsGroupBox.Controls.Add(this.regions_ALL_Checkbox);
			this.regionsGroupBox.Location = new System.Drawing.Point(12, 12);
			this.regionsGroupBox.Name = "regionsGroupBox";
			this.regionsGroupBox.Size = new System.Drawing.Size(76, 129);
			this.regionsGroupBox.TabIndex = 0;
			this.regionsGroupBox.TabStop = false;
			this.regionsGroupBox.Text = "Regions:";
			// 
			// regions_JPN_Checkbox
			// 
			this.regions_JPN_Checkbox.AutoSize = true;
			this.regions_JPN_Checkbox.Location = new System.Drawing.Point(6, 70);
			this.regions_JPN_Checkbox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.regions_JPN_Checkbox.Name = "regions_JPN_Checkbox";
			this.regions_JPN_Checkbox.Size = new System.Drawing.Size(46, 17);
			this.regions_JPN_Checkbox.TabIndex = 4;
			this.regions_JPN_Checkbox.Text = "JPN";
			this.regions_JPN_Checkbox.UseVisualStyleBackColor = true;
			// 
			// regions_EUR_Checkbox
			// 
			this.regions_EUR_Checkbox.AutoSize = true;
			this.regions_EUR_Checkbox.Location = new System.Drawing.Point(6, 53);
			this.regions_EUR_Checkbox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.regions_EUR_Checkbox.Name = "regions_EUR_Checkbox";
			this.regions_EUR_Checkbox.Size = new System.Drawing.Size(49, 17);
			this.regions_EUR_Checkbox.TabIndex = 3;
			this.regions_EUR_Checkbox.Text = "EUR";
			this.regions_EUR_Checkbox.UseVisualStyleBackColor = true;
			// 
			// regions_USA_Checkbox
			// 
			this.regions_USA_Checkbox.AutoSize = true;
			this.regions_USA_Checkbox.Location = new System.Drawing.Point(6, 36);
			this.regions_USA_Checkbox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.regions_USA_Checkbox.Name = "regions_USA_Checkbox";
			this.regions_USA_Checkbox.Size = new System.Drawing.Size(48, 17);
			this.regions_USA_Checkbox.TabIndex = 2;
			this.regions_USA_Checkbox.Text = "USA";
			this.regions_USA_Checkbox.UseVisualStyleBackColor = true;
			// 
			// typesGroupBox
			// 
			this.typesGroupBox.Controls.Add(this.types_DSIWare_Checkbox);
			this.typesGroupBox.Controls.Add(this.types_Update_Checkbox);
			this.typesGroupBox.Controls.Add(this.types_DLC_Checkbox);
			this.typesGroupBox.Controls.Add(this.types_Demo_Checkbox);
			this.typesGroupBox.Controls.Add(this.types_System_Checkbox);
			this.typesGroupBox.Controls.Add(this.types_eShop_Checkbox);
			this.typesGroupBox.Location = new System.Drawing.Point(94, 12);
			this.typesGroupBox.Name = "typesGroupBox";
			this.typesGroupBox.Size = new System.Drawing.Size(76, 129);
			this.typesGroupBox.TabIndex = 5;
			this.typesGroupBox.TabStop = false;
			this.typesGroupBox.Text = "Types:";
			// 
			// types_DSIWare_Checkbox
			// 
			this.types_DSIWare_Checkbox.AutoSize = true;
			this.types_DSIWare_Checkbox.Location = new System.Drawing.Point(6, 104);
			this.types_DSIWare_Checkbox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.types_DSIWare_Checkbox.Name = "types_DSIWare_Checkbox";
			this.types_DSIWare_Checkbox.Size = new System.Drawing.Size(70, 17);
			this.types_DSIWare_Checkbox.TabIndex = 11;
			this.types_DSIWare_Checkbox.Text = "DSIWare";
			this.types_DSIWare_Checkbox.UseVisualStyleBackColor = true;
			// 
			// types_Update_Checkbox
			// 
			this.types_Update_Checkbox.AutoSize = true;
			this.types_Update_Checkbox.Location = new System.Drawing.Point(6, 87);
			this.types_Update_Checkbox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.types_Update_Checkbox.Name = "types_Update_Checkbox";
			this.types_Update_Checkbox.Size = new System.Drawing.Size(61, 17);
			this.types_Update_Checkbox.TabIndex = 10;
			this.types_Update_Checkbox.Text = "Update";
			this.types_Update_Checkbox.UseVisualStyleBackColor = true;
			// 
			// types_DLC_Checkbox
			// 
			this.types_DLC_Checkbox.AutoSize = true;
			this.types_DLC_Checkbox.Location = new System.Drawing.Point(6, 70);
			this.types_DLC_Checkbox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.types_DLC_Checkbox.Name = "types_DLC_Checkbox";
			this.types_DLC_Checkbox.Size = new System.Drawing.Size(47, 17);
			this.types_DLC_Checkbox.TabIndex = 9;
			this.types_DLC_Checkbox.Text = "DLC";
			this.types_DLC_Checkbox.UseVisualStyleBackColor = true;
			// 
			// types_Demo_Checkbox
			// 
			this.types_Demo_Checkbox.AutoSize = true;
			this.types_Demo_Checkbox.Location = new System.Drawing.Point(6, 53);
			this.types_Demo_Checkbox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.types_Demo_Checkbox.Name = "types_Demo_Checkbox";
			this.types_Demo_Checkbox.Size = new System.Drawing.Size(54, 17);
			this.types_Demo_Checkbox.TabIndex = 8;
			this.types_Demo_Checkbox.Text = "Demo";
			this.types_Demo_Checkbox.UseVisualStyleBackColor = true;
			// 
			// types_System_Checkbox
			// 
			this.types_System_Checkbox.AutoSize = true;
			this.types_System_Checkbox.Location = new System.Drawing.Point(6, 36);
			this.types_System_Checkbox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.types_System_Checkbox.Name = "types_System_Checkbox";
			this.types_System_Checkbox.Size = new System.Drawing.Size(60, 17);
			this.types_System_Checkbox.TabIndex = 7;
			this.types_System_Checkbox.Text = "System";
			this.types_System_Checkbox.UseVisualStyleBackColor = true;
			// 
			// types_eShop_Checkbox
			// 
			this.types_eShop_Checkbox.AutoSize = true;
			this.types_eShop_Checkbox.Location = new System.Drawing.Point(6, 19);
			this.types_eShop_Checkbox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.types_eShop_Checkbox.Name = "types_eShop_Checkbox";
			this.types_eShop_Checkbox.Size = new System.Drawing.Size(57, 17);
			this.types_eShop_Checkbox.TabIndex = 6;
			this.types_eShop_Checkbox.Text = "eShop";
			this.types_eShop_Checkbox.UseVisualStyleBackColor = true;
			// 
			// other_UnknownNames_Checkbox
			// 
			this.other_UnknownNames_Checkbox.AutoSize = true;
			this.other_UnknownNames_Checkbox.Location = new System.Drawing.Point(6, 16);
			this.other_UnknownNames_Checkbox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.other_UnknownNames_Checkbox.Name = "other_UnknownNames_Checkbox";
			this.other_UnknownNames_Checkbox.Size = new System.Drawing.Size(154, 17);
			this.other_UnknownNames_Checkbox.TabIndex = 13;
			this.other_UnknownNames_Checkbox.Text = "Titles with unknown names";
			this.other_UnknownNames_Checkbox.UseVisualStyleBackColor = true;
			// 
			// otherGroupBox
			// 
			this.otherGroupBox.Controls.Add(this.other_UnknownNames_Checkbox);
			this.otherGroupBox.Location = new System.Drawing.Point(12, 147);
			this.otherGroupBox.Name = "otherGroupBox";
			this.otherGroupBox.Size = new System.Drawing.Size(158, 40);
			this.otherGroupBox.TabIndex = 12;
			this.otherGroupBox.TabStop = false;
			this.otherGroupBox.Text = "Other:";
			// 
			// FilterDialog
			// 
			this.AcceptButton = this.applyButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(187, 240);
			this.Controls.Add(this.typesGroupBox);
			this.Controls.Add(this.otherGroupBox);
			this.Controls.Add(this.regionsGroupBox);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.applyButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FilterDialog";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Title Filter";
			this.regionsGroupBox.ResumeLayout(false);
			this.regionsGroupBox.PerformLayout();
			this.typesGroupBox.ResumeLayout(false);
			this.typesGroupBox.PerformLayout();
			this.otherGroupBox.ResumeLayout(false);
			this.otherGroupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button applyButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.CheckBox regions_ALL_Checkbox;
		private System.Windows.Forms.GroupBox regionsGroupBox;
		private System.Windows.Forms.CheckBox regions_JPN_Checkbox;
		private System.Windows.Forms.CheckBox regions_EUR_Checkbox;
		private System.Windows.Forms.CheckBox regions_USA_Checkbox;
		private System.Windows.Forms.GroupBox typesGroupBox;
		private System.Windows.Forms.CheckBox types_eShop_Checkbox;
		private System.Windows.Forms.CheckBox types_DSIWare_Checkbox;
		private System.Windows.Forms.CheckBox types_Update_Checkbox;
		private System.Windows.Forms.CheckBox types_DLC_Checkbox;
		private System.Windows.Forms.CheckBox types_Demo_Checkbox;
		private System.Windows.Forms.CheckBox types_System_Checkbox;
		private System.Windows.Forms.CheckBox other_UnknownNames_Checkbox;
		private System.Windows.Forms.GroupBox otherGroupBox;
	}
}