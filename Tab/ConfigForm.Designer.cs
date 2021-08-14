
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
			this.panel3 = new System.Windows.Forms.Panel();
			this.rdoDebugDisable = new System.Windows.Forms.RadioButton();
			this.rdoDebugEnable = new System.Windows.Forms.RadioButton();
			this.lblUseDebug = new System.Windows.Forms.Label();
			this.btnLogFont = new System.Windows.Forms.Button();
			this.lblLogFont = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.lblStatusBarNeedRestart = new System.Windows.Forms.Label();
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
			this.pnlBase.SuspendLayout();
			this.panel3.SuspendLayout();
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
			this.pnlBase.Controls.Add(this.lblStatusBarNeedRestart);
			this.pnlBase.Controls.Add(this.panel3);
			this.pnlBase.Controls.Add(this.lblUseDebug);
			this.pnlBase.Controls.Add(this.btnLogFont);
			this.pnlBase.Controls.Add(this.lblLogFont);
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
			this.pnlBase.Location = new System.Drawing.Point(0, 0);
			this.pnlBase.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pnlBase.Name = "pnlBase";
			this.pnlBase.Size = new System.Drawing.Size(804, 394);
			this.pnlBase.TabIndex = 0;
			// 
			// panel3
			// 
			this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel3.Controls.Add(this.rdoDebugDisable);
			this.panel3.Controls.Add(this.rdoDebugEnable);
			this.panel3.Location = new System.Drawing.Point(160, 352);
			this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(627, 30);
			this.panel3.TabIndex = 19;
			// 
			// rdoDebugDisable
			// 
			this.rdoDebugDisable.AutoSize = true;
			this.rdoDebugDisable.Checked = true;
			this.rdoDebugDisable.Location = new System.Drawing.Point(158, 4);
			this.rdoDebugDisable.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.rdoDebugDisable.Name = "rdoDebugDisable";
			this.rdoDebugDisable.Size = new System.Drawing.Size(50, 22);
			this.rdoDebugDisable.TabIndex = 1;
			this.rdoDebugDisable.TabStop = true;
			this.rdoDebugDisable.Text = "214";
			this.rdoDebugDisable.UseVisualStyleBackColor = true;
			this.rdoDebugDisable.CheckedChanged += new System.EventHandler(this.rdoDebugDisable_CheckedChanged);
			// 
			// rdoDebugEnable
			// 
			this.rdoDebugEnable.AutoSize = true;
			this.rdoDebugEnable.Location = new System.Drawing.Point(4, 4);
			this.rdoDebugEnable.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.rdoDebugEnable.Name = "rdoDebugEnable";
			this.rdoDebugEnable.Size = new System.Drawing.Size(50, 22);
			this.rdoDebugEnable.TabIndex = 0;
			this.rdoDebugEnable.Text = "213";
			this.rdoDebugEnable.UseVisualStyleBackColor = true;
			this.rdoDebugEnable.CheckedChanged += new System.EventHandler(this.rdoDebugEnable_CheckedChanged);
			// 
			// lblUseDebug
			// 
			this.lblUseDebug.AutoSize = true;
			this.lblUseDebug.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblUseDebug.Location = new System.Drawing.Point(18, 357);
			this.lblUseDebug.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblUseDebug.Name = "lblUseDebug";
			this.lblUseDebug.Size = new System.Drawing.Size(96, 20);
			this.lblUseDebug.TabIndex = 18;
			this.lblUseDebug.Text = "Use debug";
			// 
			// btnLogFont
			// 
			this.btnLogFont.Location = new System.Drawing.Point(160, 223);
			this.btnLogFont.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnLogFont.Name = "btnLogFont";
			this.btnLogFont.Size = new System.Drawing.Size(377, 53);
			this.btnLogFont.TabIndex = 17;
			this.btnLogFont.Text = "button1";
			this.btnLogFont.UseVisualStyleBackColor = true;
			this.btnLogFont.Click += new System.EventHandler(this.BtnLogFont_Click);
			// 
			// lblLogFont
			// 
			this.lblLogFont.AutoSize = true;
			this.lblLogFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblLogFont.Location = new System.Drawing.Point(18, 239);
			this.lblLogFont.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblLogFont.Name = "lblLogFont";
			this.lblLogFont.Size = new System.Drawing.Size(39, 20);
			this.lblLogFont.TabIndex = 16;
			this.lblLogFont.Text = "216";
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.Controls.Add(this.rdoStatusBarEnable);
			this.panel2.Controls.Add(this.rdoStatusBarDisable);
			this.panel2.Location = new System.Drawing.Point(160, 284);
			this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(627, 50);
			this.panel2.TabIndex = 15;
			// 
			// lblStatusBarNeedRestart
			// 
			this.lblStatusBarNeedRestart.AutoSize = true;
			this.lblStatusBarNeedRestart.Location = new System.Drawing.Point(164, 314);
			this.lblStatusBarNeedRestart.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblStatusBarNeedRestart.Name = "lblStatusBarNeedRestart";
			this.lblStatusBarNeedRestart.Size = new System.Drawing.Size(32, 18);
			this.lblStatusBarNeedRestart.TabIndex = 14;
			this.lblStatusBarNeedRestart.Text = "215";
			// 
			// rdoStatusBarEnable
			// 
			this.rdoStatusBarEnable.AutoSize = true;
			this.rdoStatusBarEnable.Location = new System.Drawing.Point(3, 4);
			this.rdoStatusBarEnable.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.rdoStatusBarEnable.Name = "rdoStatusBarEnable";
			this.rdoStatusBarEnable.Size = new System.Drawing.Size(50, 22);
			this.rdoStatusBarEnable.TabIndex = 12;
			this.rdoStatusBarEnable.Text = "213";
			this.rdoStatusBarEnable.UseVisualStyleBackColor = true;
			this.rdoStatusBarEnable.CheckedChanged += new System.EventHandler(this.RdoStatusBarEnable_CheckedChanged);
			// 
			// rdoStatusBarDisable
			// 
			this.rdoStatusBarDisable.AutoSize = true;
			this.rdoStatusBarDisable.Checked = true;
			this.rdoStatusBarDisable.Location = new System.Drawing.Point(158, 4);
			this.rdoStatusBarDisable.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.rdoStatusBarDisable.Name = "rdoStatusBarDisable";
			this.rdoStatusBarDisable.Size = new System.Drawing.Size(50, 22);
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
			this.panel1.Location = new System.Drawing.Point(160, 89);
			this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(627, 65);
			this.panel1.TabIndex = 14;
			// 
			// rdoDataUpdateLocal
			// 
			this.rdoDataUpdateLocal.AutoSize = true;
			this.rdoDataUpdateLocal.Location = new System.Drawing.Point(4, 4);
			this.rdoDataUpdateLocal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.rdoDataUpdateLocal.Name = "rdoDataUpdateLocal";
			this.rdoDataUpdateLocal.Size = new System.Drawing.Size(50, 22);
			this.rdoDataUpdateLocal.TabIndex = 6;
			this.rdoDataUpdateLocal.Text = "204";
			this.rdoDataUpdateLocal.UseVisualStyleBackColor = true;
			this.rdoDataUpdateLocal.CheckedChanged += new System.EventHandler(this.RdoDataUpdateLocal_CheckedChanged);
			// 
			// rdoDataUpdateRemote
			// 
			this.rdoDataUpdateRemote.AutoSize = true;
			this.rdoDataUpdateRemote.Checked = true;
			this.rdoDataUpdateRemote.Location = new System.Drawing.Point(4, 36);
			this.rdoDataUpdateRemote.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.rdoDataUpdateRemote.Name = "rdoDataUpdateRemote";
			this.rdoDataUpdateRemote.Size = new System.Drawing.Size(50, 22);
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
			this.lblUseStatusBar.Location = new System.Drawing.Point(18, 289);
			this.lblUseStatusBar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblUseStatusBar.Name = "lblUseStatusBar";
			this.lblUseStatusBar.Size = new System.Drawing.Size(39, 20);
			this.lblUseStatusBar.TabIndex = 11;
			this.lblUseStatusBar.Text = "212";
			// 
			// lblTag
			// 
			this.lblTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTag.Location = new System.Drawing.Point(160, 8);
			this.lblTag.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblTag.Name = "lblTag";
			this.lblTag.Size = new System.Drawing.Size(636, 18);
			this.lblTag.TabIndex = 10;
			this.lblTag.Text = "@@@";
			this.lblTag.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// btnUiFont
			// 
			this.btnUiFont.Location = new System.Drawing.Point(160, 162);
			this.btnUiFont.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnUiFont.Name = "btnUiFont";
			this.btnUiFont.Size = new System.Drawing.Size(377, 53);
			this.btnUiFont.TabIndex = 9;
			this.btnUiFont.Text = "button1";
			this.btnUiFont.UseVisualStyleBackColor = true;
			this.btnUiFont.Click += new System.EventHandler(this.BtnUiFont_Click);
			// 
			// lblUiFont
			// 
			this.lblUiFont.AutoSize = true;
			this.lblUiFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblUiFont.Location = new System.Drawing.Point(18, 178);
			this.lblUiFont.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblUiFont.Name = "lblUiFont";
			this.lblUiFont.Size = new System.Drawing.Size(39, 20);
			this.lblUiFont.TabIndex = 8;
			this.lblUiFont.Text = "210";
			// 
			// lblDataUpdate
			// 
			this.lblDataUpdate.AutoSize = true;
			this.lblDataUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDataUpdate.Location = new System.Drawing.Point(18, 93);
			this.lblDataUpdate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDataUpdate.Name = "lblDataUpdate";
			this.lblDataUpdate.Size = new System.Drawing.Size(39, 20);
			this.lblDataUpdate.TabIndex = 5;
			this.lblDataUpdate.Text = "203";
			// 
			// lblCurrentLang
			// 
			this.lblCurrentLang.Location = new System.Drawing.Point(354, 60);
			this.lblCurrentLang.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblCurrentLang.Name = "lblCurrentLang";
			this.lblCurrentLang.Size = new System.Drawing.Size(225, 21);
			this.lblCurrentLang.TabIndex = 4;
			this.lblCurrentLang.Text = "--";
			// 
			// cboDispLang
			// 
			this.cboDispLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDispLang.FormattingEnabled = true;
			this.cboDispLang.Location = new System.Drawing.Point(160, 55);
			this.cboDispLang.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cboDispLang.Name = "cboDispLang";
			this.cboDispLang.Size = new System.Drawing.Size(186, 26);
			this.cboDispLang.TabIndex = 1;
			this.cboDispLang.SelectedIndexChanged += new System.EventHandler(this.CboDispLang_SelectedIndexChanged);
			// 
			// lblDispLang
			// 
			this.lblDispLang.AutoSize = true;
			this.lblDispLang.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDispLang.Location = new System.Drawing.Point(18, 57);
			this.lblDispLang.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
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
			this.panelBase.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.panelBase.Name = "panelBase";
			this.panelBase.Size = new System.Drawing.Size(804, 623);
			this.panelBase.TabIndex = 1;
			// 
			// ConfigForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(804, 623);
			this.Controls.Add(this.panelBase);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.MinimumSize = new System.Drawing.Size(640, 0);
			this.Name = "ConfigForm";
			this.Text = "Config";
			this.pnlBase.ResumeLayout(false);
			this.pnlBase.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
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
		private System.Windows.Forms.Button btnLogFont;
		private System.Windows.Forms.Label lblLogFont;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.RadioButton rdoDebugDisable;
		private System.Windows.Forms.RadioButton rdoDebugEnable;
		private System.Windows.Forms.Label lblUseDebug;
	}
}