using Advanced_Combat_Tracker;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DutyContent
{
	public partial class DcControl : UserControl, IActPluginV1
	{
		public static string PluginName => "DutyContent";

		private static DcControl _self;
		public static DcControl Self => _self;

		//
		private ActPluginData _ffxiv_plugin_data;

		private bool _is_form_loaded;
		private bool _is_plugin_initializing;
		private TabPage _act_tab;
		private Label _act_label;

		private Timer _update_and_check;
		private ThirdParty.NativeMethods.ProcessHandle _game_process;
		private bool _game_exist;
		private bool _game_active;
		private string _game_zone;

		//
		public DcControl()
		{
			_self = this;

			//
			RegisterActAssemblies();

			InitializeComponent();

			foreach (var f in Application.OpenForms)
			{
				if (f == ActGlobals.oFormActMain)
				{
					_is_form_loaded = true;
					break;
				}
			}

			//
			Tab.DutyForm dutyform = new Tab.DutyForm();
			tabPageDuty.Controls.Add(dutyform.Controls[0]);

			Tab.ConfigForm configform = new Tab.ConfigForm();
			tabPageConfig.Controls.Add(configform.Controls[0]);
		}

		//
		public void RegisterActAssemblies()
		{
			var pin = ActGlobals.oFormActMain.ActPlugins.FirstOrDefault(x => x.pluginFile.Name.Equals("DutyContent.dll"));
			DcConfig.PluginPath = pin?.pluginFile.DirectoryName;

			DcConfig.DataPath = Path.Combine(DcConfig.PluginPath, "Data");
			DcConfig.ConfigPath = Path.Combine(DcConfig.PluginPath, "DutyContent.config");
			DcConfig.PacketPath = Path.Combine(DcConfig.PluginPath, "DutyPacket.config");
		}

		//
		public void InitPlugin(TabPage tab, Label label)
		{
			_act_tab = tab;
			_act_label = label;

			if (_is_form_loaded)
				ActPluginInitialize();
			else
				ActGlobals.oFormActMain.Shown += OFormActMain_Shown;

			var actinfo = System.Reflection.Assembly.GetAssembly(typeof(ActGlobals));
			MesgLog.I(5, actinfo.GetName().Version, actinfo.Location);

			if (_ffxiv_plugin_data == null)
			{
				_ffxiv_plugin_data = ActGlobals.oFormActMain.ActPlugins.Where(x =>
						x.pluginFile.Name.ToUpper().Contains("FFXIV_ACT_PLUGIN") &&
						x.lblPluginStatus.Text.ToUpper().Contains("FFXIV PLUGIN STARTED."))
					.Select(x => x)
					.FirstOrDefault();
			}

			if (_ffxiv_plugin_data == null)
				MesgLog.E(2);   // FFXIV plugin is missing!
			else
			{
				var ids = ((FFXIV_ACT_Plugin.FFXIV_ACT_Plugin)_ffxiv_plugin_data.pluginObj).DataSubscription;
				ids.NetworkReceived -= FFXIVPlugin_NetworkReceived;
				ids.NetworkReceived += FFXIVPlugin_NetworkReceived;
				ids.ZoneChanged -= FFXIVPlugin_ZoneChanged;
				ids.ZoneChanged += FFXIVPlugin_ZoneChanged;

				MesgLog.I(6, System.Diagnostics.FileVersionInfo.GetVersionInfo(_ffxiv_plugin_data.pluginFile.FullName).FileVersion, _ffxiv_plugin_data.pluginFile.FullName);
			}

			_update_and_check = new Timer()
			{
				Interval = 300,
			};
			_update_and_check.Tick += (sender, e) =>
			  {
				  UpdateAndCheckProc();
				  _update_and_check.Interval = _game_exist ? _game_active ? 50 : 300 : 500;
			  };
			_update_and_check.Start();
		}

		//
		public void DeInitPlugin()
		{
			_update_and_check.Stop();

			DcConfig.PluginEnable = false;

			Tab.DutyForm.Self?.PluginDeinitialize();
			Tab.ConfigForm.Self?.PluginDeinitialize();
			DcConfig.SaveConfig();

			MesgLog.SetTextBox(null);

			_act_tab = null;

			if (_act_label != null)
			{
				_act_label.Text = "Closed";
				_act_label = null;
			}
		}

		private void OFormActMain_Shown(object sender, EventArgs e)
		{
			_is_form_loaded = true;
			ActPluginInitialize();
		}

		//
		private void ActPluginInitialize()
		{
			if (_is_plugin_initializing)
				return;

			_is_plugin_initializing = true;
			_act_label.Text = "Starting...";

			//
			MesgLog.SetTextBox(txtMesg);
			MesgLog.Initialize(Properties.Resources.DefaultMessage);

			DcConfig.Load();
			DcConfig.ReadLanguage(true);
			DcContent.ReadContent();

			UpdateUiLocale();

			MesgLog.C(Color.Aquamarine, 4);

			//
			Dock = DockStyle.Fill;

			_act_label.Text = MesgLog.Text(1);  // Duty ready
			_act_tab.Text = MesgLog.Text(0);    // FFXIV dc
			_act_tab.Controls.Add(this);

			//
			Tab.ConfigForm.Self?.PluginInitialize();
			Tab.DutyForm.Self?.PluginInitialize();

			//
			DcConfig.PluginEnable = true;

			_is_plugin_initializing = false;
		}

		//
		private void TabMain_DrawItem(object sender, DrawItemEventArgs e)
		{
			var g = e.Graphics;
			TabPage p = tabMain.TabPages[e.Index];
			Rectangle r = tabMain.GetTabRect(e.Index);
			StringFormat s = new StringFormat()
			{
				Alignment = StringAlignment.Near,
				LineAlignment = StringAlignment.Center,
			};

			Brush b, h;
			Font f;

			if (tabMain.SelectedIndex == e.Index)
			{
				f = new Font(tabMain.Font.FontFamily, 12.0f, FontStyle.Bold, GraphicsUnit.Pixel);
#if false
				b = new SolidBrush(Color.Black);
				h = SystemBrushes.Window;
#else
				b = new SolidBrush(Color.White);
				h = SystemBrushes.Highlight;
#endif
			}
			//else if (p.col)
			else
			{
				f = new Font(tabMain.Font.FontFamily, 12.0f, FontStyle.Regular, GraphicsUnit.Pixel);
				b = new SolidBrush(Color.DarkSlateGray);
				h = SystemBrushes.Control;
			}

			g.FillRectangle(h, r);
			g.DrawString(p.Text, f, b, r, new StringFormat(s));
		}

		//
		private void UpdateAndCheckProc()
		{
			if (_game_process == null || _game_process.Process.HasExited)
			{
				_game_exist = false;
				_game_active = false;

				// will be update game status next time
				var p = (from x in Process.GetProcessesByName("ffxiv_dx11") where !x.HasExited && x.MainModule != null && x.MainModule.ModuleName == "ffxiv_dx11.exe" select x).FirstOrDefault<Process>();

				if (p != null && p.HasExited)
					p = null;

				if (((_game_process == null) != (p == null)) ||
					(_game_process != null && p != null && _game_process.Process.Id != p.Id))
				{
					_game_process = p != null ? new ThirdParty.NativeMethods.ProcessHandle(p) : null;
				}

				if (_game_process != null)
				{
					//
					DcConfig.Connections.GetConnections(_game_process.Process);
					//MesgLog.L("count: {0}", DcConfig.Connections.Conns.Count);
				}
			}
			else
			{
				_game_exist = true;

				var fgw = ThirdParty.NativeMethods.GetForegroundWindow();
				ThirdParty.NativeMethods.GetWindowThreadProcessId(fgw, out int id);
				_game_active = _game_process.Process.Id == id;
			}

			var zone = ActGlobals.oFormActMain.CurrentZone;
			if (_game_zone == null || !zone.Equals(_game_zone))
			{
#if false
				if (_game_zone != null)
					MesgLog.I(1008, _game_zone);

				_game_zone = zone;
				MesgLog.I(1007, zone);
#else
				_game_zone = zone;
#endif
			}
		}

		//
		private void FFXIVPlugin_NetworkReceived(string connection, long epoch, byte[] message)
		{
			if (message.Length < 32)
				return;

			Tab.DutyForm.Self?.PacketHandler(connection, message);
		}

		//
		private void FFXIVPlugin_ZoneChanged(uint zone_id, string zone_name)
		{
			Tab.DutyForm.Self?.ZoneChanged(zone_id, zone_name);
		}

		//
		public void UpdateUiLocale()
		{
			tabPageDuty.Text = MesgLog.Text(300);
			Tab.DutyForm.Self?.UpdateUiLocale();

			tabPageConfig.Text= MesgLog.Text(200);
			Tab.ConfigForm.Self?.UpdateUiLocale();
		}
	}
}
