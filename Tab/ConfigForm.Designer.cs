
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
			this.pnlBase = new System.Windows.Forms.Panel();
			this.btnUiFont = new System.Windows.Forms.Button();
			this.lblUiFont = new System.Windows.Forms.Label();
			this.rdoDataUpdateRemote = new System.Windows.Forms.RadioButton();
			this.rdoDataUpdateLocal = new System.Windows.Forms.RadioButton();
			this.lblDataUpdate = new System.Windows.Forms.Label();
			this.lblCurrentLang = new System.Windows.Forms.Label();
			this.cboDispLang = new System.Windows.Forms.ComboBox();
			this.lblDispLang = new System.Windows.Forms.Label();
			this.pnlBase.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlBase
			// 
			this.pnlBase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlBase.Controls.Add(this.btnUiFont);
			this.pnlBase.Controls.Add(this.lblUiFont);
			this.pnlBase.Controls.Add(this.rdoDataUpdateRemote);
			this.pnlBase.Controls.Add(this.rdoDataUpdateLocal);
			this.pnlBase.Controls.Add(this.lblDataUpdate);
			this.pnlBase.Controls.Add(this.lblCurrentLang);
			this.pnlBase.Controls.Add(this.cboDispLang);
			this.pnlBase.Controls.Add(this.lblDispLang);
			this.pnlBase.Location = new System.Drawing.Point(12, 12);
			this.pnlBase.Name = "pnlBase";
			this.pnlBase.Size = new System.Drawing.Size(776, 186);
			this.pnlBase.TabIndex = 0;
			// 
			// btnUiFont
			// 
			this.btnUiFont.Location = new System.Drawing.Point(251, 135);
			this.btnUiFont.Name = "btnUiFont";
			this.btnUiFont.Size = new System.Drawing.Size(217, 38);
			this.btnUiFont.TabIndex = 9;
			this.btnUiFont.Text = "button1";
			this.btnUiFont.UseVisualStyleBackColor = true;
			this.btnUiFont.Click += new System.EventHandler(this.BtnUiFont_Click);
			// 
			// lblUiFont
			// 
			this.lblUiFont.AutoSize = true;
			this.lblUiFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblUiFont.Location = new System.Drawing.Point(12, 135);
			this.lblUiFont.Name = "lblUiFont";
			this.lblUiFont.Size = new System.Drawing.Size(39, 20);
			this.lblUiFont.TabIndex = 8;
			this.lblUiFont.Text = "210";
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
			this.lblDataUpdate.Text = "203";
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
			this.Controls.Add(this.pnlBase);
			this.Name = "ConfigForm";
			this.Text = "Config";
			this.pnlBase.ResumeLayout(false);
			this.pnlBase.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlBase;
		private System.Windows.Forms.Label lblDispLang;
		private System.Windows.Forms.ComboBox cboDispLang;
		private System.Windows.Forms.Label lblCurrentLang;
		private System.Windows.Forms.RadioButton rdoDataUpdateRemote;
		private System.Windows.Forms.RadioButton rdoDataUpdateLocal;
		private System.Windows.Forms.Label lblDataUpdate;
		private System.Windows.Forms.Button btnUiFont;
		private System.Windows.Forms.Label lblUiFont;
	}
}