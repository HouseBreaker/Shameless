namespace Shameless
{
	partial class MainForm
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
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.statusProgressbar = new System.Windows.Forms.ToolStripProgressBar();
			this.currentTitleStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.currentActionLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.titlesCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.searchBox = new System.Windows.Forms.TextBox();
			this.generateQrCodeButton = new System.Windows.Forms.Button();
			this.delayTimer = new System.Windows.Forms.Timer(this.components);
			this.generateAllTicketsButton = new System.Windows.Forms.Button();
			this.progressUpdater = new System.ComponentModel.BackgroundWorker();
			this.filterButton = new System.Windows.Forms.Button();
			this.titlesDataGrid = new System.Windows.Forms.DataGridView();
			this.checkUpdatesButton = new System.Windows.Forms.Button();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.humanReadableSizeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.nintendo3DSTitleBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.statusStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.titlesDataGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nintendo3DSTitleBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusProgressbar,
            this.currentTitleStatusLabel,
            this.currentActionLabel,
            this.titlesCountLabel});
			this.statusStrip.Location = new System.Drawing.Point(0, 429);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(824, 22);
			this.statusStrip.TabIndex = 1;
			this.statusStrip.Text = "Status";
			// 
			// statusProgressbar
			// 
			this.statusProgressbar.MarqueeAnimationSpeed = 30;
			this.statusProgressbar.Name = "statusProgressbar";
			this.statusProgressbar.Size = new System.Drawing.Size(100, 16);
			// 
			// currentTitleStatusLabel
			// 
			this.currentTitleStatusLabel.Name = "currentTitleStatusLabel";
			this.currentTitleStatusLabel.Size = new System.Drawing.Size(30, 17);
			this.currentTitleStatusLabel.Text = "Title";
			// 
			// currentActionLabel
			// 
			this.currentActionLabel.Name = "currentActionLabel";
			this.currentActionLabel.Size = new System.Drawing.Size(85, 17);
			this.currentActionLabel.Text = "Current Action";
			// 
			// titlesCountLabel
			// 
			this.titlesCountLabel.Name = "titlesCountLabel";
			this.titlesCountLabel.Size = new System.Drawing.Size(592, 17);
			this.titlesCountLabel.Spring = true;
			this.titlesCountLabel.Text = "0 titles";
			this.titlesCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// searchBox
			// 
			this.searchBox.ForeColor = System.Drawing.SystemColors.WindowFrame;
			this.searchBox.Location = new System.Drawing.Point(12, 15);
			this.searchBox.Name = "searchBox";
			this.searchBox.Size = new System.Drawing.Size(185, 20);
			this.searchBox.TabIndex = 3;
			this.searchBox.Text = "Search...";
			this.searchBox.Click += new System.EventHandler(this.searchBox_Enter);
			this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
			this.searchBox.Enter += new System.EventHandler(this.searchBox_Enter);
			// 
			// generateQrCodeButton
			// 
			this.generateQrCodeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.generateQrCodeButton.Enabled = false;
			this.generateQrCodeButton.Location = new System.Drawing.Point(668, 403);
			this.generateQrCodeButton.Name = "generateQrCodeButton";
			this.generateQrCodeButton.Size = new System.Drawing.Size(144, 23);
			this.generateQrCodeButton.TabIndex = 6;
			this.generateQrCodeButton.Text = "Generate QR Code for FBI";
			this.generateQrCodeButton.UseVisualStyleBackColor = true;
			this.generateQrCodeButton.Click += new System.EventHandler(this.generateQrCodeButton_Click);
			// 
			// delayTimer
			// 
			this.delayTimer.Tick += new System.EventHandler(this.delayTimer_Tick);
			// 
			// generateAllTicketsButton
			// 
			this.generateAllTicketsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.generateAllTicketsButton.Enabled = false;
			this.generateAllTicketsButton.Location = new System.Drawing.Point(12, 403);
			this.generateAllTicketsButton.Name = "generateAllTicketsButton";
			this.generateAllTicketsButton.Size = new System.Drawing.Size(130, 23);
			this.generateAllTicketsButton.TabIndex = 8;
			this.generateAllTicketsButton.Text = "Generate all tickets";
			this.generateAllTicketsButton.UseVisualStyleBackColor = true;
			this.generateAllTicketsButton.Click += new System.EventHandler(this.generateAllTicketsButton_Click);
			// 
			// progressUpdater
			// 
			this.progressUpdater.WorkerReportsProgress = true;
			this.progressUpdater.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.progressUpdater_ProgressChanged);
			// 
			// filterButton
			// 
			this.filterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.filterButton.Enabled = false;
			this.filterButton.Location = new System.Drawing.Point(148, 403);
			this.filterButton.Name = "filterButton";
			this.filterButton.Size = new System.Drawing.Size(49, 23);
			this.filterButton.TabIndex = 9;
			this.filterButton.Text = "Filter";
			this.filterButton.UseVisualStyleBackColor = true;
			this.filterButton.Click += new System.EventHandler(this.filterButton_Click);
			// 
			// titlesDataGrid
			// 
			this.titlesDataGrid.AllowUserToAddRows = false;
			this.titlesDataGrid.AllowUserToDeleteRows = false;
			this.titlesDataGrid.AllowUserToResizeRows = false;
			this.titlesDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.titlesDataGrid.AutoGenerateColumns = false;
			this.titlesDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.titlesDataGrid.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
			this.titlesDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.titlesDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.humanReadableSizeDataGridViewTextBoxColumn,
            this.dataGridViewTextBoxColumn7});
			this.titlesDataGrid.DataSource = this.nintendo3DSTitleBindingSource;
			this.titlesDataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.titlesDataGrid.Location = new System.Drawing.Point(12, 41);
			this.titlesDataGrid.Name = "titlesDataGrid";
			this.titlesDataGrid.ReadOnly = true;
			this.titlesDataGrid.RowHeadersVisible = false;
			this.titlesDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.titlesDataGrid.ShowEditingIcon = false;
			this.titlesDataGrid.Size = new System.Drawing.Size(800, 356);
			this.titlesDataGrid.TabIndex = 2;
			this.titlesDataGrid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.titlesDataGrid_RowHeaderMouseClick);
			this.titlesDataGrid.Scroll += new System.Windows.Forms.ScrollEventHandler(this.titlesDataGrid_Scroll);
			// 
			// checkUpdatesButton
			// 
			this.checkUpdatesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkUpdatesButton.Location = new System.Drawing.Point(702, 12);
			this.checkUpdatesButton.Name = "checkUpdatesButton";
			this.checkUpdatesButton.Size = new System.Drawing.Size(110, 23);
			this.checkUpdatesButton.TabIndex = 10;
			this.checkUpdatesButton.Text = "Update database";
			this.checkUpdatesButton.UseVisualStyleBackColor = true;
			this.checkUpdatesButton.Click += new System.EventHandler(this.checkUpdatesButton_Click);
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.dataGridViewTextBoxColumn1.DataPropertyName = "TitleId";
			this.dataGridViewTextBoxColumn1.HeaderText = "Title ID";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewTextBoxColumn1.Width = 66;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.dataGridViewTextBoxColumn2.DataPropertyName = "EncKey";
			this.dataGridViewTextBoxColumn2.HeaderText = "Encrypted Key";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewTextBoxColumn2.Width = 101;
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.DataPropertyName = "Name";
			this.dataGridViewTextBoxColumn3.HeaderText = "Name";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn4
			// 
			this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.dataGridViewTextBoxColumn4.DataPropertyName = "Type";
			this.dataGridViewTextBoxColumn4.HeaderText = "Type";
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewTextBoxColumn4.Width = 56;
			// 
			// dataGridViewTextBoxColumn5
			// 
			this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.dataGridViewTextBoxColumn5.DataPropertyName = "Region";
			this.dataGridViewTextBoxColumn5.HeaderText = "Region";
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.ReadOnly = true;
			this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewTextBoxColumn5.Width = 66;
			// 
			// dataGridViewTextBoxColumn6
			// 
			this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.dataGridViewTextBoxColumn6.DataPropertyName = "Serial";
			this.dataGridViewTextBoxColumn6.HeaderText = "Serial";
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.ReadOnly = true;
			this.dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewTextBoxColumn6.Width = 58;
			// 
			// humanReadableSizeDataGridViewTextBoxColumn
			// 
			this.humanReadableSizeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.humanReadableSizeDataGridViewTextBoxColumn.DataPropertyName = "HumanReadableSize";
			this.humanReadableSizeDataGridViewTextBoxColumn.HeaderText = "Size";
			this.humanReadableSizeDataGridViewTextBoxColumn.Name = "humanReadableSizeDataGridViewTextBoxColumn";
			this.humanReadableSizeDataGridViewTextBoxColumn.ReadOnly = true;
			this.humanReadableSizeDataGridViewTextBoxColumn.Width = 52;
			// 
			// dataGridViewTextBoxColumn7
			// 
			this.dataGridViewTextBoxColumn7.DataPropertyName = "Size";
			this.dataGridViewTextBoxColumn7.HeaderText = "SizeInBytes";
			this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			this.dataGridViewTextBoxColumn7.ReadOnly = true;
			this.dataGridViewTextBoxColumn7.Visible = false;
			// 
			// nintendo3DSTitleBindingSource
			// 
			this.nintendo3DSTitleBindingSource.DataSource = typeof(Shameless.Tickets.Nintendo3DSTitle);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(824, 451);
			this.Controls.Add(this.checkUpdatesButton);
			this.Controls.Add(this.titlesDataGrid);
			this.Controls.Add(this.filterButton);
			this.Controls.Add(this.generateAllTicketsButton);
			this.Controls.Add(this.generateQrCodeButton);
			this.Controls.Add(this.searchBox);
			this.Controls.Add(this.statusStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(840, 490);
			this.Name = "MainForm";
			this.Text = " Shameless";
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.titlesDataGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nintendo3DSTitleBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel currentActionLabel;
		private System.Windows.Forms.TextBox searchBox;
		private System.Windows.Forms.Button generateQrCodeButton;
		private System.Windows.Forms.Timer delayTimer;
		private System.Windows.Forms.ToolStripProgressBar statusProgressbar;
		private System.Windows.Forms.ToolStripStatusLabel currentTitleStatusLabel;
		private System.Windows.Forms.ToolStripStatusLabel titlesCountLabel;
		private System.Windows.Forms.Button generateAllTicketsButton;
		private System.ComponentModel.BackgroundWorker progressUpdater;
		private System.Windows.Forms.Button filterButton;
		private System.Windows.Forms.DataGridView titlesDataGrid;
		private System.Windows.Forms.DataGridViewTextBoxColumn titleIdDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn encKeyDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn regionDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn serialDataGridViewTextBoxColumn;
		private System.Windows.Forms.Button checkUpdatesButton;
		private System.Windows.Forms.DataGridViewTextBoxColumn sizeDataGridViewTextBoxColumn;
		private System.Windows.Forms.BindingSource nintendo3DSTitleBindingSource;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
		private System.Windows.Forms.DataGridViewTextBoxColumn humanReadableSizeDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
	}
}

