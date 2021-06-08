using DutyContent.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
	}
}
