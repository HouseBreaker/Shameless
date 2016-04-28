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
			this.urlLabel = new System.Windows.Forms.Label();
			this.titleIdLabel = new System.Windows.Forms.Label();
			this.nameLabel = new System.Windows.Forms.Label();
			this.sizeLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.qrCodeBox)).BeginInit();
			this.SuspendLayout();
			// 
			// qrCodeBox
			// 
			this.qrCodeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.qrCodeBox.Location = new System.Drawing.Point(30, 51);
			this.qrCodeBox.Name = "qrCodeBox";
			this.qrCodeBox.Size = new System.Drawing.Size(275, 279);
			this.qrCodeBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.qrCodeBox.TabIndex = 0;
			this.qrCodeBox.TabStop = false;
			// 
			// urlLabel
			// 
			this.urlLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.urlLabel.AutoSize = true;
			this.urlLabel.Location = new System.Drawing.Point(27, 333);
			this.urlLabel.MaximumSize = new System.Drawing.Size(285, 0);
			this.urlLabel.Name = "urlLabel";
			this.urlLabel.Size = new System.Drawing.Size(18, 13);
			this.urlLabel.TabIndex = 1;
			this.urlLabel.Text = "url";
			this.urlLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// titleIdLabel
			// 
			this.titleIdLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.titleIdLabel.AutoSize = true;
			this.titleIdLabel.Location = new System.Drawing.Point(27, 9);
			this.titleIdLabel.MaximumSize = new System.Drawing.Size(285, 0);
			this.titleIdLabel.Name = "titleIdLabel";
			this.titleIdLabel.Size = new System.Drawing.Size(44, 13);
			this.titleIdLabel.TabIndex = 2;
			this.titleIdLabel.Text = "TitleID: ";
			this.titleIdLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// nameLabel
			// 
			this.nameLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.nameLabel.AutoSize = true;
			this.nameLabel.Location = new System.Drawing.Point(27, 22);
			this.nameLabel.MaximumSize = new System.Drawing.Size(285, 0);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(44, 13);
			this.nameLabel.TabIndex = 2;
			this.nameLabel.Text = "Name:  ";
			this.nameLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// sizeLabel
			// 
			this.sizeLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.sizeLabel.AutoSize = true;
			this.sizeLabel.Location = new System.Drawing.Point(27, 35);
			this.sizeLabel.MaximumSize = new System.Drawing.Size(285, 0);
			this.sizeLabel.Name = "sizeLabel";
			this.sizeLabel.Size = new System.Drawing.Size(42, 13);
			this.sizeLabel.TabIndex = 2;
			this.sizeLabel.Text = "Size:    ";
			this.sizeLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// QrCodeResultForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(334, 368);
			this.Controls.Add(this.sizeLabel);
			this.Controls.Add(this.nameLabel);
			this.Controls.Add(this.titleIdLabel);
			this.Controls.Add(this.urlLabel);
			this.Controls.Add(this.qrCodeBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.Name = "QrCodeResultForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "QR Code";
			((System.ComponentModel.ISupportInitialize)(this.qrCodeBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox qrCodeBox;
		private System.Windows.Forms.Label urlLabel;
		private System.Windows.Forms.Label titleIdLabel;
		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.Label sizeLabel;
	}
}