
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
			this.panel2 = new System.Windows.Forms.Panel();
			this.rdoStatusBarEnable = new System.Windows.Forms.RadioButton();
			this.rdoStatusBarDisable = new System.Windows.Forms.RadioButton();
			this.panel1 = new System.Windows.Forms.Panel();
			this.rdoDataUpdateLocal = new System.Windows.Forms.RadioButton();
			this.rdoDataUpdateRemote = new System.Windows.Forms.RadioButton();
			this.lblUseStatusBar = new System.Windows.Forms.Label();
			this.lblTag = new System.Windows.Forms.Label();
			this.btnUiFont = new System.Windows.Forms.Button();
			this.lblUiFont = new System.Windows.Forms.Label();
			this.lblDataUpdate = new System.Windows.Forms.Label();
			this.lblCurrentLang = new System.Windows.Forms.Label();
			this.cboDispLang = new System.Windows.Forms.ComboBox();
			this.lblDispLang = new System.Windows.Forms.Label();
			this.panelBase = new System.Windows.Forms.Panel();
			this.lblStatusBarNeedRestart = new System.Windows.Forms.Label();
			this.pnlBase.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panelBase.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlBase
			// 
			this.pnlBase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlBase.AutoScroll = true;
			this.pnlBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlBase.Controls.Add(this.panel2);
			this.pnlBase.Controls.Add(this.panel1);
			this.pnlBase.Controls.Add(this.lblUseStatusBar);
			this.pnlBase.Controls.Add(this.lblTag);
			this.pnlBase.Controls.Add(this.btnUiFont);
			this.pnlBase.Controls.Add(this.lblUiFont);
			this.pnlBase.Controls.Add(this.lblDataUpdate);
			this.pnlBase.Controls.Add(this.lblCurrentLang);
			this.pnlBase.Controls.Add(this.cboDispLang);
			this.pnlBase.Controls.Add(this.lblDispLang);
			this.pnlBase.Location = new System.Drawing.Point(12, 12);
			this.pnlBase.Name = "pnlBase";
			this.pnlBase.Size = new System.Drawing.Size(776, 286);
			this.pnlBase.TabIndex = 0;
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.Controls.Add(this.lblStatusBarNeedRestart);
			this.panel2.Controls.Add(this.rdoStatusBarEnable);
			this.panel2.Controls.Add(this.rdoStatusBarDisable);
			this.panel2.Location = new System.Drawing.Point(251, 220);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(520, 47);
			this.panel2.TabIndex = 15;
			// 
			// rdoStatusBarEnable
			// 
			this.rdoStatusBarEnable.AutoSize = true;
			this.rdoStatusBarEnable.Location = new System.Drawing.Point(2, 3);
			this.rdoStatusBarEnable.Name = "rdoStatusBarEnable";
			this.rdoStatusBarEnable.Size = new System.Drawing.Size(43, 17);
			this.rdoStatusBarEnable.TabIndex = 12;
			this.rdoStatusBarEnable.Text = "213";
			this.rdoStatusBarEnable.UseVisualStyleBackColor = true;
			this.rdoStatusBarEnable.CheckedChanged += new System.EventHandler(this.RdoStatusBarEnable_CheckedChanged);
			// 
			// rdoStatusBarDisable
			// 
			this.rdoStatusBarDisable.AutoSize = true;
			this.rdoStatusBarDisable.Checked = true;
			this.rdoStatusBarDisable.Location = new System.Drawing.Point(105, 3);
			this.rdoStatusBarDisable.Name = "rdoStatusBarDisable";
			this.rdoStatusBarDisable.Size = new System.Drawing.Size(43, 17);
			this.rdoStatusBarDisable.TabIndex = 13;
			this.rdoStatusBarDisable.TabStop = true;
			this.rdoStatusBarDisable.Text = "214";
			this.rdoStatusBarDisable.UseVisualStyleBackColor = true;
			this.rdoStatusBarDisable.CheckedChanged += new System.EventHandler(this.RdoStatusBarDisable_CheckedChanged);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.rdoDataUpdateLocal);
			this.panel1.Controls.Add(this.rdoDataUpdateRemote);
			this.panel1.Location = new System.Drawing.Point(251, 83);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(520, 67);
			this.panel1.TabIndex = 14;
			// 
			// rdoDataUpdateLocal
			// 
			this.rdoDataUpdateLocal.AutoSize = true;
			this.rdoDataUpdateLocal.Location = new System.Drawing.Point(3, 3);
			this.rdoDataUpdateLocal.Name = "rdoDataUpdateLocal";
			this.rdoDataUpdateLocal.Size = new System.Drawing.Size(43, 17);
			this.rdoDataUpdateLocal.TabIndex = 6;
			this.rdoDataUpdateLocal.Text = "204";
			this.rdoDataUpdateLocal.UseVisualStyleBackColor = true;
			this.rdoDataUpdateLocal.CheckedChanged += new System.EventHandler(this.RdoDataUpdateLocal_CheckedChanged);
			// 
			// rdoDataUpdateRemote
			// 
			this.rdoDataUpdateRemote.AutoSize = true;
			this.rdoDataUpdateRemote.Checked = true;
			this.rdoDataUpdateRemote.Location = new System.Drawing.Point(3, 26);
			this.rdoDataUpdateRemote.Name = "rdoDataUpdateRemote";
			this.rdoDataUpdateRemote.Size = new System.Drawing.Size(43, 17);
			this.rdoDataUpdateRemote.TabIndex = 7;
			this.rdoDataUpdateRemote.TabStop = true;
			this.rdoDataUpdateRemote.Text = "205";
			this.rdoDataUpdateRemote.UseVisualStyleBackColor = true;
			this.rdoDataUpdateRemote.CheckedChanged += new System.EventHandler(this.RdoDataUpdateRemote_CheckedChanged);
			// 
			// lblUseStatusBar
			// 
			this.lblUseStatusBar.AutoSize = true;
			this.lblUseStatusBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblUseStatusBar.Location = new System.Drawing.Point(12, 220);
			this.lblUseStatusBar.Name = "lblUseStatusBar";
			this.lblUseStatusBar.Size = new System.Drawing.Size(39, 20);
			this.lblUseStatusBar.TabIndex = 11;
			this.lblUseStatusBar.Text = "212";
			// 
			// lblTag
			// 
			this.lblTag.AutoSize = true;
			this.lblTag.Location = new System.Drawing.Point(27, 4);
			this.lblTag.Name = "lblTag";
			this.lblTag.Size = new System.Drawing.Size(35, 13);
			this.lblTag.TabIndex = 10;
			this.lblTag.Text = "label1";
			// 
			// btnUiFont
			// 
			this.btnUiFont.Location = new System.Drawing.Point(251, 165);
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
			this.lblUiFont.Location = new System.Drawing.Point(12, 165);
			this.lblUiFont.Name = "lblUiFont";
			this.lblUiFont.Size = new System.Drawing.Size(39, 20);
			this.lblUiFont.TabIndex = 8;
			this.lblUiFont.Text = "210";
			// 
			// lblDataUpdate
			// 
			this.lblDataUpdate.AutoSize = true;
			this.lblDataUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDataUpdate.Location = new System.Drawing.Point(12, 84);
			this.lblDataUpdate.Name = "lblDataUpdate";
			this.lblDataUpdate.Size = new System.Drawing.Size(39, 20);
			this.lblDataUpdate.TabIndex = 5;
			this.lblDataUpdate.Text = "203";
			// 
			// lblCurrentLang
			// 
			this.lblCurrentLang.Location = new System.Drawing.Point(318, 67);
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
			this.cboDispLang.Location = new System.Drawing.Point(251, 43);
			this.cboDispLang.Name = "cboDispLang";
			this.cboDispLang.Size = new System.Drawing.Size(217, 21);
			this.cboDispLang.TabIndex = 1;
			this.cboDispLang.SelectedIndexChanged += new System.EventHandler(this.CboDispLang_SelectedIndexChanged);
			// 
			// lblDispLang
			// 
			this.lblDispLang.AutoSize = true;
			this.lblDispLang.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDispLang.Location = new System.Drawing.Point(12, 41);
			this.lblDispLang.Name = "lblDispLang";
			this.lblDispLang.Size = new System.Drawing.Size(39, 20);
			this.lblDispLang.TabIndex = 0;
			this.lblDispLang.Text = "201";
			// 
			// panelBase
			// 
			this.panelBase.AutoScroll = true;
			this.panelBase.Controls.Add(this.pnlBase);
			this.panelBase.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelBase.Location = new System.Drawing.Point(0, 0);
			this.panelBase.Name = "panelBase";
			this.panelBase.Size = new System.Drawing.Size(800, 450);
			this.panelBase.TabIndex = 1;
			// 
			// lblStatusBarNeedRestart
			// 
			this.lblStatusBarNeedRestart.AutoSize = true;
			this.lblStatusBarNeedRestart.Location = new System.Drawing.Point(238, 5);
			this.lblStatusBarNeedRestart.Name = "lblStatusBarNeedRestart";
			this.lblStatusBarNeedRestart.Size = new System.Drawing.Size(25, 13);
			this.lblStatusBarNeedRestart.TabIndex = 14;
			this.lblStatusBarNeedRestart.Text = "215";
			// 
			// ConfigForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.panelBase);
			this.Name = "ConfigForm";
			this.Text = "Config";
			this.pnlBase.ResumeLayout(false);
			this.pnlBase.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panelBase.ResumeLayout(false);
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
		private System.Windows.Forms.Label lblTag;
		private System.Windows.Forms.Panel panelBase;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.RadioButton rdoStatusBarEnable;
		private System.Windows.Forms.RadioButton rdoStatusBarDisable;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lblUseStatusBar;
		private System.Windows.Forms.Label lblStatusBarNeedRestart;
	}
}