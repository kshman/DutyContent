
namespace DutyContent.Tab
{
	partial class PingForm
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
			this.panel6 = new System.Windows.Forms.Panel();
			this.cboPingGraphType = new System.Windows.Forms.ComboBox();
			this.lblPingGraphType = new System.Windows.Forms.Label();
			this.lstPingAddress = new System.Windows.Forms.ListBox();
			this.lblPingAddress = new System.Windows.Forms.Label();
			this.cboPingDefAddr = new System.Windows.Forms.ComboBox();
			this.lblPingDefAddr = new System.Windows.Forms.Label();
			this.pbxPingGraph = new System.Windows.Forms.PictureBox();
			this.chkPingGraph = new System.Windows.Forms.CheckBox();
			this.tlpnPingColors = new System.Windows.Forms.TableLayoutPanel();
			this.lblPingStat1 = new System.Windows.Forms.Label();
			this.btnPingColor4 = new System.Windows.Forms.Button();
			this.btnPingColor2 = new System.Windows.Forms.Button();
			this.btnPingColor3 = new System.Windows.Forms.Button();
			this.lblPingStat2 = new System.Windows.Forms.Label();
			this.lblPingStat4 = new System.Windows.Forms.Label();
			this.btnPingColor1 = new System.Windows.Forms.Button();
			this.lblPingStat3 = new System.Windows.Forms.Label();
			this.lblPingColors = new System.Windows.Forms.Label();
			this.chkUsePing = new System.Windows.Forms.CheckBox();
			this.panelDock = new System.Windows.Forms.Panel();
			this.panel6.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbxPingGraph)).BeginInit();
			this.tlpnPingColors.SuspendLayout();
			this.panelDock.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel6
			// 
			this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel6.AutoScroll = true;
			this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel6.Controls.Add(this.cboPingGraphType);
			this.panel6.Controls.Add(this.lblPingGraphType);
			this.panel6.Controls.Add(this.lstPingAddress);
			this.panel6.Controls.Add(this.lblPingAddress);
			this.panel6.Controls.Add(this.cboPingDefAddr);
			this.panel6.Controls.Add(this.lblPingDefAddr);
			this.panel6.Controls.Add(this.pbxPingGraph);
			this.panel6.Controls.Add(this.chkPingGraph);
			this.panel6.Controls.Add(this.tlpnPingColors);
			this.panel6.Controls.Add(this.lblPingColors);
			this.panel6.Controls.Add(this.chkUsePing);
			this.panel6.Location = new System.Drawing.Point(0, 0);
			this.panel6.Margin = new System.Windows.Forms.Padding(4);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(804, 377);
			this.panel6.TabIndex = 2;
			// 
			// cboPingGraphType
			// 
			this.cboPingGraphType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboPingGraphType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPingGraphType.FormattingEnabled = true;
			this.cboPingGraphType.Items.AddRange(new object[] {
            "Linear",
            "Curved"});
			this.cboPingGraphType.Location = new System.Drawing.Point(529, 140);
			this.cboPingGraphType.Margin = new System.Windows.Forms.Padding(4);
			this.cboPingGraphType.Name = "cboPingGraphType";
			this.cboPingGraphType.Size = new System.Drawing.Size(267, 26);
			this.cboPingGraphType.TabIndex = 19;
			this.cboPingGraphType.SelectedIndexChanged += new System.EventHandler(this.CboPingGraphType_SelectedIndexChanged);
			// 
			// lblPingGraphType
			// 
			this.lblPingGraphType.AutoSize = true;
			this.lblPingGraphType.Location = new System.Drawing.Point(513, 118);
			this.lblPingGraphType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblPingGraphType.Name = "lblPingGraphType";
			this.lblPingGraphType.Size = new System.Drawing.Size(32, 18);
			this.lblPingGraphType.TabIndex = 18;
			this.lblPingGraphType.Text = "410";
			// 
			// lstPingAddress
			// 
			this.lstPingAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstPingAddress.FormattingEnabled = true;
			this.lstPingAddress.ItemHeight = 18;
			this.lstPingAddress.Location = new System.Drawing.Point(160, 327);
			this.lstPingAddress.Margin = new System.Windows.Forms.Padding(4);
			this.lstPingAddress.Name = "lstPingAddress";
			this.lstPingAddress.ScrollAlwaysVisible = true;
			this.lstPingAddress.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lstPingAddress.Size = new System.Drawing.Size(636, 40);
			this.lstPingAddress.TabIndex = 17;
			this.lstPingAddress.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LstPingAddress_MouseDoubleClick);
			// 
			// lblPingAddress
			// 
			this.lblPingAddress.AutoSize = true;
			this.lblPingAddress.Location = new System.Drawing.Point(34, 327);
			this.lblPingAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblPingAddress.Name = "lblPingAddress";
			this.lblPingAddress.Size = new System.Drawing.Size(32, 18);
			this.lblPingAddress.TabIndex = 16;
			this.lblPingAddress.Text = "409";
			// 
			// cboPingDefAddr
			// 
			this.cboPingDefAddr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboPingDefAddr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPingDefAddr.FormattingEnabled = true;
			this.cboPingDefAddr.Location = new System.Drawing.Point(529, 78);
			this.cboPingDefAddr.Margin = new System.Windows.Forms.Padding(4);
			this.cboPingDefAddr.Name = "cboPingDefAddr";
			this.cboPingDefAddr.Size = new System.Drawing.Size(267, 26);
			this.cboPingDefAddr.TabIndex = 15;
			this.cboPingDefAddr.SelectedIndexChanged += new System.EventHandler(this.CboPingDefAddr_SelectedIndexChanged);
			// 
			// lblPingDefAddr
			// 
			this.lblPingDefAddr.AutoSize = true;
			this.lblPingDefAddr.Location = new System.Drawing.Point(513, 58);
			this.lblPingDefAddr.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblPingDefAddr.Name = "lblPingDefAddr";
			this.lblPingDefAddr.Size = new System.Drawing.Size(32, 18);
			this.lblPingDefAddr.TabIndex = 14;
			this.lblPingDefAddr.Text = "408";
			// 
			// pbxPingGraph
			// 
			this.pbxPingGraph.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pbxPingGraph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pbxPingGraph.Location = new System.Drawing.Point(160, 174);
			this.pbxPingGraph.Margin = new System.Windows.Forms.Padding(4);
			this.pbxPingGraph.Name = "pbxPingGraph";
			this.pbxPingGraph.Size = new System.Drawing.Size(637, 145);
			this.pbxPingGraph.TabIndex = 13;
			this.pbxPingGraph.TabStop = false;
			// 
			// chkPingGraph
			// 
			this.chkPingGraph.AutoSize = true;
			this.chkPingGraph.Location = new System.Drawing.Point(37, 174);
			this.chkPingGraph.Margin = new System.Windows.Forms.Padding(4);
			this.chkPingGraph.Name = "chkPingGraph";
			this.chkPingGraph.Size = new System.Drawing.Size(51, 22);
			this.chkPingGraph.TabIndex = 12;
			this.chkPingGraph.Text = "407";
			this.chkPingGraph.UseVisualStyleBackColor = true;
			this.chkPingGraph.CheckedChanged += new System.EventHandler(this.ChkPingGraph_CheckedChanged);
			// 
			// tlpnPingColors
			// 
			this.tlpnPingColors.ColumnCount = 4;
			this.tlpnPingColors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tlpnPingColors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tlpnPingColors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tlpnPingColors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tlpnPingColors.Controls.Add(this.lblPingStat1, 0, 0);
			this.tlpnPingColors.Controls.Add(this.btnPingColor4, 3, 1);
			this.tlpnPingColors.Controls.Add(this.btnPingColor2, 1, 1);
			this.tlpnPingColors.Controls.Add(this.btnPingColor3, 2, 1);
			this.tlpnPingColors.Controls.Add(this.lblPingStat2, 1, 0);
			this.tlpnPingColors.Controls.Add(this.lblPingStat4, 3, 0);
			this.tlpnPingColors.Controls.Add(this.btnPingColor1, 0, 1);
			this.tlpnPingColors.Controls.Add(this.lblPingStat3, 2, 0);
			this.tlpnPingColors.Location = new System.Drawing.Point(160, 58);
			this.tlpnPingColors.Margin = new System.Windows.Forms.Padding(4);
			this.tlpnPingColors.Name = "tlpnPingColors";
			this.tlpnPingColors.Padding = new System.Windows.Forms.Padding(4);
			this.tlpnPingColors.RowCount = 2;
			this.tlpnPingColors.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.53846F));
			this.tlpnPingColors.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.46154F));
			this.tlpnPingColors.Size = new System.Drawing.Size(325, 60);
			this.tlpnPingColors.TabIndex = 11;
			// 
			// lblPingStat1
			// 
			this.lblPingStat1.AutoSize = true;
			this.lblPingStat1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblPingStat1.Location = new System.Drawing.Point(8, 4);
			this.lblPingStat1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblPingStat1.Name = "lblPingStat1";
			this.lblPingStat1.Size = new System.Drawing.Size(71, 18);
			this.lblPingStat1.TabIndex = 6;
			this.lblPingStat1.Text = "403";
			this.lblPingStat1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnPingColor4
			// 
			this.btnPingColor4.BackColor = System.Drawing.Color.Plum;
			this.btnPingColor4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnPingColor4.Location = new System.Drawing.Point(245, 26);
			this.btnPingColor4.Margin = new System.Windows.Forms.Padding(4);
			this.btnPingColor4.Name = "btnPingColor4";
			this.btnPingColor4.Size = new System.Drawing.Size(72, 26);
			this.btnPingColor4.TabIndex = 8;
			this.btnPingColor4.UseVisualStyleBackColor = false;
			this.btnPingColor4.Click += new System.EventHandler(this.BtnPingColor4_Click);
			// 
			// btnPingColor2
			// 
			this.btnPingColor2.BackColor = System.Drawing.Color.Aqua;
			this.btnPingColor2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnPingColor2.Location = new System.Drawing.Point(87, 26);
			this.btnPingColor2.Margin = new System.Windows.Forms.Padding(4);
			this.btnPingColor2.Name = "btnPingColor2";
			this.btnPingColor2.Size = new System.Drawing.Size(71, 26);
			this.btnPingColor2.TabIndex = 10;
			this.btnPingColor2.UseVisualStyleBackColor = false;
			this.btnPingColor2.Click += new System.EventHandler(this.BtnPingColor2_Click);
			// 
			// btnPingColor3
			// 
			this.btnPingColor3.BackColor = System.Drawing.Color.LawnGreen;
			this.btnPingColor3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnPingColor3.Location = new System.Drawing.Point(166, 26);
			this.btnPingColor3.Margin = new System.Windows.Forms.Padding(4);
			this.btnPingColor3.Name = "btnPingColor3";
			this.btnPingColor3.Size = new System.Drawing.Size(71, 26);
			this.btnPingColor3.TabIndex = 9;
			this.btnPingColor3.UseVisualStyleBackColor = false;
			this.btnPingColor3.Click += new System.EventHandler(this.BtnPingColor3_Click);
			// 
			// lblPingStat2
			// 
			this.lblPingStat2.AutoSize = true;
			this.lblPingStat2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblPingStat2.Location = new System.Drawing.Point(87, 4);
			this.lblPingStat2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblPingStat2.Name = "lblPingStat2";
			this.lblPingStat2.Size = new System.Drawing.Size(71, 18);
			this.lblPingStat2.TabIndex = 5;
			this.lblPingStat2.Text = "404";
			this.lblPingStat2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblPingStat4
			// 
			this.lblPingStat4.AutoSize = true;
			this.lblPingStat4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblPingStat4.Location = new System.Drawing.Point(245, 4);
			this.lblPingStat4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblPingStat4.Name = "lblPingStat4";
			this.lblPingStat4.Size = new System.Drawing.Size(72, 18);
			this.lblPingStat4.TabIndex = 3;
			this.lblPingStat4.Text = "406";
			this.lblPingStat4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnPingColor1
			// 
			this.btnPingColor1.BackColor = System.Drawing.Color.RoyalBlue;
			this.btnPingColor1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnPingColor1.Location = new System.Drawing.Point(8, 26);
			this.btnPingColor1.Margin = new System.Windows.Forms.Padding(4);
			this.btnPingColor1.Name = "btnPingColor1";
			this.btnPingColor1.Size = new System.Drawing.Size(71, 26);
			this.btnPingColor1.TabIndex = 7;
			this.btnPingColor1.UseVisualStyleBackColor = false;
			this.btnPingColor1.Click += new System.EventHandler(this.BtnPingColor1_Click);
			// 
			// lblPingStat3
			// 
			this.lblPingStat3.AutoSize = true;
			this.lblPingStat3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblPingStat3.Location = new System.Drawing.Point(166, 4);
			this.lblPingStat3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblPingStat3.Name = "lblPingStat3";
			this.lblPingStat3.Size = new System.Drawing.Size(71, 18);
			this.lblPingStat3.TabIndex = 4;
			this.lblPingStat3.Text = "405";
			this.lblPingStat3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblPingColors
			// 
			this.lblPingColors.AutoSize = true;
			this.lblPingColors.Location = new System.Drawing.Point(34, 58);
			this.lblPingColors.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblPingColors.Name = "lblPingColors";
			this.lblPingColors.Size = new System.Drawing.Size(32, 18);
			this.lblPingColors.TabIndex = 2;
			this.lblPingColors.Text = "402";
			// 
			// chkUsePing
			// 
			this.chkUsePing.AutoSize = true;
			this.chkUsePing.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkUsePing.Location = new System.Drawing.Point(4, 4);
			this.chkUsePing.Margin = new System.Windows.Forms.Padding(4);
			this.chkUsePing.Name = "chkUsePing";
			this.chkUsePing.Size = new System.Drawing.Size(62, 28);
			this.chkUsePing.TabIndex = 0;
			this.chkUsePing.Text = "401";
			this.chkUsePing.UseVisualStyleBackColor = true;
			this.chkUsePing.CheckedChanged += new System.EventHandler(this.ChkUsePing_CheckedChanged);
			// 
			// panelDock
			// 
			this.panelDock.AutoScroll = true;
			this.panelDock.Controls.Add(this.panel6);
			this.panelDock.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelDock.Location = new System.Drawing.Point(0, 0);
			this.panelDock.Margin = new System.Windows.Forms.Padding(4);
			this.panelDock.Name = "panelDock";
			this.panelDock.Size = new System.Drawing.Size(804, 381);
			this.panelDock.TabIndex = 3;
			// 
			// PingForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(804, 381);
			this.Controls.Add(this.panelDock);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MinimumSize = new System.Drawing.Size(800, 39);
			this.Name = "PingForm";
			this.Text = "Ping";
			this.panel6.ResumeLayout(false);
			this.panel6.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbxPingGraph)).EndInit();
			this.tlpnPingColors.ResumeLayout(false);
			this.tlpnPingColors.PerformLayout();
			this.panelDock.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.ComboBox cboPingDefAddr;
		private System.Windows.Forms.Label lblPingDefAddr;
		private System.Windows.Forms.PictureBox pbxPingGraph;
		private System.Windows.Forms.CheckBox chkPingGraph;
		private System.Windows.Forms.TableLayoutPanel tlpnPingColors;
		private System.Windows.Forms.Label lblPingStat1;
		private System.Windows.Forms.Button btnPingColor4;
		private System.Windows.Forms.Button btnPingColor2;
		private System.Windows.Forms.Button btnPingColor3;
		private System.Windows.Forms.Label lblPingStat2;
		private System.Windows.Forms.Label lblPingStat4;
		private System.Windows.Forms.Button btnPingColor1;
		private System.Windows.Forms.Label lblPingStat3;
		private System.Windows.Forms.Label lblPingColors;
		private System.Windows.Forms.CheckBox chkUsePing;
		private System.Windows.Forms.Panel panelDock;
		private System.Windows.Forms.Label lblPingAddress;
		private System.Windows.Forms.ListBox lstPingAddress;
		private System.Windows.Forms.ComboBox cboPingGraphType;
		private System.Windows.Forms.Label lblPingGraphType;
	}
}