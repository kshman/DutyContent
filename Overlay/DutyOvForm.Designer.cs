
namespace DutyContent.Overlay
{
	partial class DutyOvForm
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
			this.lblText = new System.Windows.Forms.Label();
			this.lblStat = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblText
			// 
			this.lblText.AutoEllipsis = true;
			this.lblText.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblText.ForeColor = System.Drawing.Color.White;
			this.lblText.Location = new System.Drawing.Point(41, 0);
			this.lblText.Name = "lblText";
			this.lblText.Size = new System.Drawing.Size(319, 32);
			this.lblText.TabIndex = 0;
			this.lblText.Text = "Duty Content";
			this.lblText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblText_MouseDown);
			// 
			// lblStat
			// 
			this.lblStat.AutoEllipsis = true;
			this.lblStat.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStat.ForeColor = System.Drawing.Color.White;
			this.lblStat.Location = new System.Drawing.Point(0, 0);
			this.lblStat.Name = "lblStat";
			this.lblStat.Size = new System.Drawing.Size(40, 32);
			this.lblStat.TabIndex = 1;
			this.lblStat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// DutyOvForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(360, 32);
			this.ControlBox = false;
			this.Controls.Add(this.lblStat);
			this.Controls.Add(this.lblText);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "DutyOvForm";
			this.Opacity = 0.5D;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "DutyOvForm";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.DutyOvForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblText;
		private System.Windows.Forms.Label lblStat;
	}
}