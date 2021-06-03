
namespace DutyContent
{
	partial class DcControl
	{
		/// <summary> 
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region 구성 요소 디자이너에서 생성한 코드

		/// <summary> 
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
		/// </summary>
		private void InitializeComponent()
		{
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tabMain = new System.Windows.Forms.TabControl();
			this.tabPageDuty = new System.Windows.Forms.TabPage();
			this.tabPageConfig = new System.Windows.Forms.TabPage();
			this.txtMesg = new System.Windows.Forms.RichTextBox();
			this.btnClearMesg = new System.Windows.Forms.Button();
			this.tabPagePing = new System.Windows.Forms.TabPage();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tabMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tabMain);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.txtMesg);
			this.splitContainer1.Panel2.Controls.Add(this.btnClearMesg);
			this.splitContainer1.Size = new System.Drawing.Size(792, 567);
			this.splitContainer1.SplitterDistance = 497;
			this.splitContainer1.TabIndex = 0;
			// 
			// tabMain
			// 
			this.tabMain.Alignment = System.Windows.Forms.TabAlignment.Left;
			this.tabMain.Controls.Add(this.tabPageDuty);
			this.tabMain.Controls.Add(this.tabPagePing);
			this.tabMain.Controls.Add(this.tabPageConfig);
			this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabMain.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.tabMain.ItemSize = new System.Drawing.Size(30, 100);
			this.tabMain.Location = new System.Drawing.Point(0, 0);
			this.tabMain.Multiline = true;
			this.tabMain.Name = "tabMain";
			this.tabMain.SelectedIndex = 0;
			this.tabMain.Size = new System.Drawing.Size(792, 497);
			this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tabMain.TabIndex = 0;
			this.tabMain.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.TabMain_DrawItem);
			// 
			// tabPageDuty
			// 
			this.tabPageDuty.BackColor = System.Drawing.Color.Transparent;
			this.tabPageDuty.Location = new System.Drawing.Point(104, 4);
			this.tabPageDuty.Name = "tabPageDuty";
			this.tabPageDuty.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageDuty.Size = new System.Drawing.Size(684, 489);
			this.tabPageDuty.TabIndex = 0;
			this.tabPageDuty.Text = "Duty";
			// 
			// tabPageConfig
			// 
			this.tabPageConfig.Location = new System.Drawing.Point(104, 4);
			this.tabPageConfig.Name = "tabPageConfig";
			this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageConfig.Size = new System.Drawing.Size(684, 489);
			this.tabPageConfig.TabIndex = 1;
			this.tabPageConfig.Text = "Config";
			this.tabPageConfig.UseVisualStyleBackColor = true;
			// 
			// txtMesg
			// 
			this.txtMesg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMesg.Location = new System.Drawing.Point(104, 3);
			this.txtMesg.Name = "txtMesg";
			this.txtMesg.ReadOnly = true;
			this.txtMesg.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.txtMesg.Size = new System.Drawing.Size(685, 60);
			this.txtMesg.TabIndex = 1;
			this.txtMesg.Text = "";
			// 
			// btnClearMesg
			// 
			this.btnClearMesg.Location = new System.Drawing.Point(3, 3);
			this.btnClearMesg.Name = "btnClearMesg";
			this.btnClearMesg.Size = new System.Drawing.Size(95, 33);
			this.btnClearMesg.TabIndex = 0;
			this.btnClearMesg.Text = "Clear Mesg";
			this.btnClearMesg.UseVisualStyleBackColor = true;
			// 
			// tabPagePing
			// 
			this.tabPagePing.Location = new System.Drawing.Point(104, 4);
			this.tabPagePing.Name = "tabPagePing";
			this.tabPagePing.Size = new System.Drawing.Size(684, 489);
			this.tabPagePing.TabIndex = 2;
			this.tabPagePing.Text = "Ping";
			this.tabPagePing.UseVisualStyleBackColor = true;
			// 
			// DcControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "DcControl";
			this.Size = new System.Drawing.Size(792, 567);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.tabMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Button btnClearMesg;
		private System.Windows.Forms.TabControl tabMain;
		private System.Windows.Forms.TabPage tabPageDuty;
		private System.Windows.Forms.TabPage tabPageConfig;
		private System.Windows.Forms.RichTextBox txtMesg;
		private System.Windows.Forms.TabPage tabPagePing;
	}
}
