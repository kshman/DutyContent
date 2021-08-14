
namespace DutyContent.Tab
{
	partial class UpdateNotifyForm
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
			this.lblTitle = new System.Windows.Forms.Label();
			this.lblMesg = new System.Windows.Forms.Label();
			this.lblLink = new System.Windows.Forms.LinkLabel();
			this.txtBody = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panelBase = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.panelBase.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTitle.Location = new System.Drawing.Point(69, 0);
			this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(62, 31);
			this.lblTitle.TabIndex = 0;
			this.lblTitle.Text = "207";
			// 
			// lblMesg
			// 
			this.lblMesg.AutoSize = true;
			this.lblMesg.Location = new System.Drawing.Point(4, 69);
			this.lblMesg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblMesg.Name = "lblMesg";
			this.lblMesg.Size = new System.Drawing.Size(32, 18);
			this.lblMesg.TabIndex = 1;
			this.lblMesg.Text = "208";
			// 
			// lblLink
			// 
			this.lblLink.AutoSize = true;
			this.lblLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblLink.Location = new System.Drawing.Point(4, 120);
			this.lblLink.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblLink.Name = "lblLink";
			this.lblLink.Size = new System.Drawing.Size(32, 18);
			this.lblLink.TabIndex = 2;
			this.lblLink.TabStop = true;
			this.lblLink.Text = "209";
			this.lblLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LblLink_LinkClicked);
			// 
			// txtBody
			// 
			this.txtBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtBody.BackColor = System.Drawing.Color.OldLace;
			this.txtBody.Location = new System.Drawing.Point(24, 150);
			this.txtBody.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.txtBody.Multiline = true;
			this.txtBody.Name = "txtBody";
			this.txtBody.ReadOnly = true;
			this.txtBody.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtBody.Size = new System.Drawing.Size(584, 283);
			this.txtBody.TabIndex = 3;
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.AutoScroll = true;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.lblTitle);
			this.panel1.Controls.Add(this.txtBody);
			this.panel1.Controls.Add(this.lblMesg);
			this.panel1.Controls.Add(this.lblLink);
			this.panel1.Location = new System.Drawing.Point(4, 4);
			this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(796, 443);
			this.panel1.TabIndex = 4;
			// 
			// panelBase
			// 
			this.panelBase.AutoScroll = true;
			this.panelBase.Controls.Add(this.panel1);
			this.panelBase.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelBase.Location = new System.Drawing.Point(0, 0);
			this.panelBase.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.panelBase.Name = "panelBase";
			this.panelBase.Size = new System.Drawing.Size(804, 451);
			this.panelBase.TabIndex = 5;
			// 
			// UpdateNotifyForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(804, 451);
			this.Controls.Add(this.panelBase);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "UpdateNotifyForm";
			this.Text = "Update";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panelBase.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.Label lblMesg;
		private System.Windows.Forms.LinkLabel lblLink;
		private System.Windows.Forms.TextBox txtBody;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panelBase;
	}
}