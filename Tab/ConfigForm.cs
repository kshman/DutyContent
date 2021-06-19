using DutyContent.ThirdParty;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DutyContent.Tab
{
	public partial class ConfigForm : Form, Interface.ISuppLocale, Interface.ISuppActPlugin
	{
		private static ConfigForm _self;
		public static ConfigForm Self => _self;

		public ConfigForm()
		{
			_self = this;

			InitializeComponent();
		}

		public void PluginDeinitialize()
		{

		}

		public void PluginInitialize()
		{
			//
			lblCurrentLang.Text = MesgLog.Text("LANG");

			//
			var lang = MakeConfigLangList();

			foreach (var i in lang)
			{
				var n = cboDispLang.Items.Add(i);
				if (i.Equals(DcConfig.Language))
					cboDispLang.SelectedIndex = n;
			}

			if (cboDispLang.SelectedIndex < 0)
				cboDispLang.SelectedIndex = 0;

			Task.Factory.StartNew(() =>
			{
				while (true)
				{
					Updater.CheckNewVersion();
					Thread.Sleep(30 * 60 * 1000);
				}
			});
		}

		public void RefreshLocale()
		{

		}

		public void UpdateUiLocale()
		{
			lblDispLang.Text = MesgLog.Text(201);

			lblDataUpdate.Text = MesgLog.Text(203);
			rdoDataUpdateLocal.Text = MesgLog.Text(204);
			rdoDataUpdateRemote.Text = MesgLog.Text(205);

			lblUiFont.Text = MesgLog.Text(210);
			btnUiFont.Text = DcConfig.UiFontFamily;
		}

		public static List<string> MakeConfigLangList()
		{
			List<string> lst = new List<string>();

			lst.Add($"<{MesgLog.Text(26)}>");

			DirectoryInfo di = new DirectoryInfo(DcConfig.DataPath);

			foreach (var fi in di.GetFiles("DcLang-*.txt"))
			{
				var s = fi.Name.Substring(7, fi.Name.Length - 7 - 4);
				lst.Add(s);
			}

			return lst;
		}

		private void CboDispLang_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!DcConfig.PluginEnable)
				return;

			if (cboDispLang.SelectedIndex == 0)
			{
				// default
				DcConfig.Language = string.Empty;
			}
			else
			{
				var l = (string)cboDispLang.SelectedItem;

				if (string.IsNullOrWhiteSpace(l) || l.Equals(DcConfig.Language))
					return;

				DcConfig.Language = l;
			}

			//
			DcConfig.ReadLanguage();
			DcControl.Self?.UpdateUiLocale();
			DcConfig.SaveConfig();

			lblCurrentLang.Text = MesgLog.Text("LANG");
		}

		private void InternalDataUpdate(bool value)
		{
			if (!DcConfig.PluginEnable)
				return;

			if (value && DcConfig.DataRemoteUpdate)
				return;
			if (!value && !DcConfig.DataRemoteUpdate)
				return;

			DcConfig.DataRemoteUpdate = value;
			DcConfig.SaveConfig();
		}

		private void RdoDataUpdateLocal_CheckedChanged(object sender, EventArgs e)
		{
			InternalDataUpdate(false);
		}

		private void RdoDataUpdateRemote_CheckedChanged(object sender, EventArgs e)
		{
			InternalDataUpdate(true);
		}

		private void BtnUiFont_Click(object sender, EventArgs e)
		{
			Font ret = (Font)WorkerAct.Invoker(new WorkerAct.ObjectReturnerDelegate(() =>
			  {
				  FontDialog dg = new FontDialog
				  {
					  Font = btnUiFont.Font,
					  FontMustExist = true,
					  AllowVerticalFonts = false,
					  AllowVectorFonts = false,
					  ShowColor = false,
					  ShowEffects = false,
					  MaxSize = 12,
					  MinSize = 12,
				  };

				  return dg.ShowDialog() == DialogResult.OK ? dg.Font : null;
			  }));

			if (ret != null)
			{
				DcConfig.UiFontFamily = ret.Name;
				DcControl.Self.UpdateUiLocale();

				// locale change first. because when error occured in ui working, prevent saving invalid value
				DcConfig.SaveConfig();
			}
		}
	}
}
