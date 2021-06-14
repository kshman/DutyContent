
namespace DutyContent.Tab
{
	partial class ConfigForm
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.rdoDataUpdateRemote = new System.Windows.Forms.RadioButton();
			this.rdoDataUpdateLocal = new System.Windows.Forms.RadioButton();
			this.lblDataUpdate = new System.Windows.Forms.Label();
			this.lblCurrentLang = new System.Windows.Forms.Label();
			this.cboDispLang = new System.Windows.Forms.ComboBox();
			this.lblDispLang = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.rdoDataUpdateRemote);
			this.panel1.Controls.Add(this.rdoDataUpdateLocal);
			this.panel1.Controls.Add(this.lblDataUpdate);
			this.panel1.Controls.Add(this.lblCurrentLang);
			this.panel1.Controls.Add(this.cboDispLang);
			this.panel1.Controls.Add(this.lblDispLang);
			this.panel1.Location = new System.Drawing.Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(776, 158);
			this.panel1.TabIndex = 0;
			// 
			// rdoDataUpdateRemote
			// 
			this.rdoDataUpdateRemote.AutoSize = true;
			this.rdoDataUpdateRemote.Checked = true;
			this.rdoDataUpdateRemote.Location = new System.Drawing.Point(251, 80);
			this.rdoDataUpdateRemote.Name = "rdoDataUpdateRemote";
			this.rdoDataUpdateRemote.Size = new System.Drawing.Size(43, 17);
			this.rdoDataUpdateRemote.TabIndex = 7;
			this.rdoDataUpdateRemote.TabStop = true;
			this.rdoDataUpdateRemote.Text = "205";
			this.rdoDataUpdateRemote.UseVisualStyleBackColor = true;
			this.rdoDataUpdateRemote.CheckedChanged += new System.EventHandler(this.RdoDataUpdateRemote_CheckedChanged);
			// 
			// rdoDataUpdateLocal
			// 
			this.rdoDataUpdateLocal.AutoSize = true;
			this.rdoDataUpdateLocal.Location = new System.Drawing.Point(251, 57);
			this.rdoDataUpdateLocal.Name = "rdoDataUpdateLocal";
			this.rdoDataUpdateLocal.Size = new System.Drawing.Size(43, 17);
			this.rdoDataUpdateLocal.TabIndex = 6;
			this.rdoDataUpdateLocal.Text = "204";
			this.rdoDataUpdateLocal.UseVisualStyleBackColor = true;
			this.rdoDataUpdateLocal.CheckedChanged += new System.EventHandler(this.RdoDataUpdateLocal_CheckedChanged);
			// 
			// lblDataUpdate
			// 
			this.lblDataUpdate.AutoSize = true;
			this.lblDataUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDataUpdate.Location = new System.Drawing.Point(12, 54);
			this.lblDataUpdate.Name = "lblDataUpdate";
			this.lblDataUpdate.Size = new System.Drawing.Size(39, 20);
			this.lblDataUpdate.TabIndex = 5;
			this.lblDataUpdate.Text = "201";
			// 
			// lblCurrentLang
			// 
			this.lblCurrentLang.Location = new System.Drawing.Point(318, 37);
			this.lblCurrentLang.Name = "lblCurrentLang";
			this.lblCurrentLang.Size = new System.Drawing.Size(150, 15);
			this.lblCurrentLang.TabIndex = 4;
			this.lblCurrentLang.Text = "--";
			this.lblCurrentLang.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// cboDispLang
			// 
			this.cboDispLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDispLang.FormattingEnabled = true;
			this.cboDispLang.Location = new System.Drawing.Point(251, 13);
			this.cboDispLang.Name = "cboDispLang";
			this.cboDispLang.Size = new System.Drawing.Size(217, 21);
			this.cboDispLang.TabIndex = 1;
			this.cboDispLang.SelectedIndexChanged += new System.EventHandler(this.CboDispLang_SelectedIndexChanged);
			// 
			// lblDispLang
			// 
			this.lblDispLang.AutoSize = true;
			this.lblDispLang.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDispLang.Location = new System.Drawing.Point(12, 11);
			this.lblDispLang.Name = "lblDispLang";
			this.lblDispLang.Size = new System.Drawing.Size(39, 20);
			this.lblDispLang.TabIndex = 0;
			this.lblDispLang.Text = "201";
			// 
			// ConfigForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.panel1);
			this.Name = "ConfigForm";
			this.Text = "Config";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lblDispLang;
		private System.Windows.Forms.ComboBox cboDispLang;
		private System.Windows.Forms.Label lblCurrentLang;
		private System.Windows.Forms.RadioButton rdoDataUpdateRemote;
		private System.Windows.Forms.RadioButton rdoDataUpdateLocal;
		private System.Windows.Forms.Label lblDataUpdate;
	}
}