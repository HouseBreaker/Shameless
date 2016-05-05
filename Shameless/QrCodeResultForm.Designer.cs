namespace Shameless
{
	partial class QrCodeResultForm
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
			this.qrCodeBox = new System.Windows.Forms.PictureBox();
			this.titleInfoRichTextbox = new System.Windows.Forms.RichTextBox();
			this.urlRichTextbox = new System.Windows.Forms.RichTextBox();
			this.urlsLabel = new System.Windows.Forms.Label();
			this.titlesLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.qrCodeBox)).BeginInit();
			this.SuspendLayout();
			// 
			// qrCodeBox
			// 
			this.qrCodeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.qrCodeBox.Location = new System.Drawing.Point(12, 85);
			this.qrCodeBox.Name = "qrCodeBox";
			this.qrCodeBox.Size = new System.Drawing.Size(325, 325);
			this.qrCodeBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.qrCodeBox.TabIndex = 0;
			this.qrCodeBox.TabStop = false;
			// 
			// titleInfoRichTextbox
			// 
			this.titleInfoRichTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.titleInfoRichTextbox.Location = new System.Drawing.Point(12, 25);
			this.titleInfoRichTextbox.Name = "titleInfoRichTextbox";
			this.titleInfoRichTextbox.ReadOnly = true;
			this.titleInfoRichTextbox.Size = new System.Drawing.Size(325, 54);
			this.titleInfoRichTextbox.TabIndex = 3;
			this.titleInfoRichTextbox.Text = "";
			// 
			// urlRichTextbox
			// 
			this.urlRichTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.urlRichTextbox.Location = new System.Drawing.Point(12, 448);
			this.urlRichTextbox.Name = "urlRichTextbox";
			this.urlRichTextbox.ReadOnly = true;
			this.urlRichTextbox.Size = new System.Drawing.Size(325, 65);
			this.urlRichTextbox.TabIndex = 4;
			this.urlRichTextbox.Text = "";
			// 
			// urlsLabel
			// 
			this.urlsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.urlsLabel.AutoSize = true;
			this.urlsLabel.Location = new System.Drawing.Point(9, 432);
			this.urlsLabel.Name = "urlsLabel";
			this.urlsLabel.Size = new System.Drawing.Size(37, 13);
			this.urlsLabel.TabIndex = 5;
			this.urlsLabel.Text = "URLs:";
			// 
			// titlesLabel
			// 
			this.titlesLabel.AutoSize = true;
			this.titlesLabel.Location = new System.Drawing.Point(9, 9);
			this.titlesLabel.Name = "titlesLabel";
			this.titlesLabel.Size = new System.Drawing.Size(35, 13);
			this.titlesLabel.TabIndex = 5;
			this.titlesLabel.Text = "Titles:";
			// 
			// QrCodeResultForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(349, 525);
			this.Controls.Add(this.titlesLabel);
			this.Controls.Add(this.urlsLabel);
			this.Controls.Add(this.urlRichTextbox);
			this.Controls.Add(this.titleInfoRichTextbox);
			this.Controls.Add(this.qrCodeBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.Name = "QrCodeResultForm";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "QR Code";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.QrCodeResultForm_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.qrCodeBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox qrCodeBox;
		private System.Windows.Forms.RichTextBox titleInfoRichTextbox;
		private System.Windows.Forms.RichTextBox urlRichTextbox;
		private System.Windows.Forms.Label urlsLabel;
		private System.Windows.Forms.Label titlesLabel;
	}
}