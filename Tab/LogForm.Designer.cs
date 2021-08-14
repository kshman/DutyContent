
namespace DutyContent.Tab
{
	partial class LogForm
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
			this.pnlBase = new System.Windows.Forms.Panel();
			this.txtLogText = new System.Windows.Forms.RichTextBox();
			this.chkLogScroll = new System.Windows.Forms.CheckBox();
			this.btnLogClear = new System.Windows.Forms.Button();
			this.btnLogCopy = new System.Windows.Forms.Button();
			this.pnlBase.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlBase
			// 
			this.pnlBase.Controls.Add(this.txtLogText);
			this.pnlBase.Controls.Add(this.chkLogScroll);
			this.pnlBase.Controls.Add(this.btnLogClear);
			this.pnlBase.Controls.Add(this.btnLogCopy);
			this.pnlBase.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlBase.Location = new System.Drawing.Point(0, 0);
			this.pnlBase.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pnlBase.Name = "pnlBase";
			this.pnlBase.Size = new System.Drawing.Size(804, 451);
			this.pnlBase.TabIndex = 0;
			// 
			// txtLogText
			// 
			this.txtLogText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtLogText.BackColor = System.Drawing.Color.AliceBlue;
			this.txtLogText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtLogText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtLogText.Location = new System.Drawing.Point(4, 4);
			this.txtLogText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.txtLogText.Name = "txtLogText";
			this.txtLogText.ReadOnly = true;
			this.txtLogText.Size = new System.Drawing.Size(796, 407);
			this.txtLogText.TabIndex = 3;
			this.txtLogText.Text = "";
			// 
			// chkLogScroll
			// 
			this.chkLogScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.chkLogScroll.AutoSize = true;
			this.chkLogScroll.Location = new System.Drawing.Point(740, 421);
			this.chkLogScroll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.chkLogScroll.Name = "chkLogScroll";
			this.chkLogScroll.Size = new System.Drawing.Size(51, 22);
			this.chkLogScroll.TabIndex = 2;
			this.chkLogScroll.Text = "503";
			this.chkLogScroll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkLogScroll.UseVisualStyleBackColor = true;
			// 
			// btnLogClear
			// 
			this.btnLogClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnLogClear.Location = new System.Drawing.Point(191, 415);
			this.btnLogClear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnLogClear.Name = "btnLogClear";
			this.btnLogClear.Size = new System.Drawing.Size(166, 32);
			this.btnLogClear.TabIndex = 1;
			this.btnLogClear.Text = "502";
			this.btnLogClear.UseVisualStyleBackColor = true;
			this.btnLogClear.Click += new System.EventHandler(this.BtnLogClear_Click);
			// 
			// btnLogCopy
			// 
			this.btnLogCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnLogCopy.Location = new System.Drawing.Point(4, 415);
			this.btnLogCopy.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnLogCopy.Name = "btnLogCopy";
			this.btnLogCopy.Size = new System.Drawing.Size(166, 32);
			this.btnLogCopy.TabIndex = 0;
			this.btnLogCopy.Text = "501";
			this.btnLogCopy.UseVisualStyleBackColor = true;
			this.btnLogCopy.Click += new System.EventHandler(this.BtnLogCopy_Click);
			// 
			// LogForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(804, 451);
			this.Controls.Add(this.pnlBase);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.MinimumSize = new System.Drawing.Size(640, 200);
			this.Name = "LogForm";
			this.Text = "Log";
			this.pnlBase.ResumeLayout(false);
			this.pnlBase.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlBase;
		private System.Windows.Forms.CheckBox chkLogScroll;
		private System.Windows.Forms.Button btnLogClear;
		private System.Windows.Forms.Button btnLogCopy;
		private System.Windows.Forms.RichTextBox txtLogText;
	}
}