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
			lblCurrentLang.Text = Locale.Text("LANG");

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

			//
			rdoDataUpdateLocal.Checked = !DcConfig.DataRemoteUpdate;
			rdoDataUpdateRemote.Checked = DcConfig.DataRemoteUpdate;

			//
			rdoStatusBarEnable.Checked = DcConfig.StatusBar;
			rdoStatusBarDisable.Checked = !DcConfig.StatusBar;

			//
			rdoDebugEnable.Checked = DcConfig.DebugEnable;
			rdoDebugDisable.Checked = !DcConfig.DebugEnable;
		}

		public void RefreshLocale()
		{

		}

		public void UpdateUiLocale()
		{
			lblDispLang.Text = Locale.Text(201);

			lblDataUpdate.Text = Locale.Text(203);
			rdoDataUpdateLocal.Text = Locale.Text(204);
			rdoDataUpdateRemote.Text = Locale.Text(205);

			lblUiFont.Text = Locale.Text(210);
			btnUiFont.Text = DcConfig.UiFontFamily;

			lblLogFont.Text = Locale.Text(216);
			btnLogFont.Text = $"{DcConfig.Duty.LogFontFamily}, {DcConfig.Duty.LogFontSize}";

			lblTag.Text = Locale.Text(211, DcConfig.PluginTag, DcConfig.PluginVersion);

			lblUseStatusBar.Text = Locale.Text(212);
			rdoStatusBarEnable.Text = Locale.Text(213);
			rdoStatusBarDisable.Text = Locale.Text(214);
			lblStatusBarNeedRestart.Text = Locale.Text(215);


			rdoDebugEnable.Text = Locale.Text(213);
			rdoDebugDisable.Text = Locale.Text(214);
		}

		public static List<string> MakeConfigLangList()
		{
			List<string> lst = new List<string>();

			lst.Add($"<{Locale.Text(26)}>");

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

			lblCurrentLang.Text = Locale.Text("LANG");
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
				DcConfig.SaveConfig();
			}
		}

		private void BtnLogFont_Click(object sender, EventArgs e)
		{
			Font ret = (Font)WorkerAct.Invoker(new WorkerAct.ObjectReturnerDelegate(() =>
			{
				FontDialog dg = new FontDialog
				{
					Font = Tab.LogForm.Self?.LogFont,
					FontMustExist = true,
					AllowVerticalFonts = false
				};

				return (dg.ShowDialog() == DialogResult.OK) ? dg.Font : null;
			}));

			if (ret != null)
			{
				if (LogForm.Self != null)
					LogForm.Self.LogFont = ret;

				DcConfig.Duty.LogFontFamily = ret.Name;
				DcConfig.Duty.LogFontSize = ret.Size;
				DcConfig.SaveConfig();

				btnLogFont.Font = ret;
				btnLogFont.Text = $"{DcConfig.Duty.LogFontFamily}, {DcConfig.Duty.LogFontSize}";
			}
		}

		private void InternalStatusBar(bool value)
		{
			if (!DcConfig.PluginEnable)
				return;

			if (value && DcConfig.StatusBar)
				return;
			if (!value && !DcConfig.StatusBar)
				return;

			DcConfig.StatusBar = value;
			DcConfig.SaveConfig();

			DcControl.Self?.ShowStatusBarAsConfig();
		}

		private void RdoStatusBarEnable_CheckedChanged(object sender, EventArgs e)
		{
			InternalStatusBar(true);
		}

		private void RdoStatusBarDisable_CheckedChanged(object sender, EventArgs e)
		{
			InternalStatusBar(false);
		}

		private void InternalDebug(bool value)
		{
			if (!DcConfig.PluginEnable)
				return;

			if (value && DcConfig.DebugEnable)
				return;
			if (!value && !DcConfig.DebugEnable)
				return;

			DcConfig.DebugEnable = value;
			DcConfig.SaveConfig();
		}

		private void rdoDebugEnable_CheckedChanged(object sender, EventArgs e)
		{
			InternalDebug(true);
		}

		private void rdoDebugDisable_CheckedChanged(object sender, EventArgs e)
		{
			InternalDebug(false);
		}
	}
}
