using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DutyContent.Interface;

namespace DutyContent.Tab
{
	public partial class UpdateNotifyForm : Form, Interface.ISuppLocale, Interface.ISuppActPlugin
	{
		private static UpdateNotifyForm _self;
		public static UpdateNotifyForm Self => _self;

		private int _newtag;

		public UpdateNotifyForm(int newtag, string body)
		{
			_self = this;

			InitializeComponent();

			_newtag = newtag;
			txtBody.Text = body;
		}

		public void PluginInitialize()
		{

		}

		public void PluginDeinitialize()
		{

		}

		public void RefreshLocale()
		{

		}

		public void UpdateUiLocale()
		{
			lblTitle.Text = MesgLog.Text(207, DcConfig.PluginTag, _newtag);
			lblMesg.Text = MesgLog.Text(208);
			lblLink.Text = MesgLog.Text(209);
		}

		private void LblLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(lblLink.Text);
		}
	}
}
