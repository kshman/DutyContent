using Advanced_Combat_Tracker;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
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

		private System.Timers.Timer _save_timer;
		private System.Timers.Timer _update_timer;
		private ThirdParty.NativeMethods.ProcessHandle _game_process;
		private long _game_connection_tick = DateTime.Now.Ticks;
		private bool _game_exist;
		private bool _game_active;
		private string _game_zone;
		private IPAddress _game_ipaddr = IPAddress.None;

		//
		private const int IntervalGameActive = 50;
		private const int IntervalGameExist = 300;
		private const int IntervalGameNone = 500;

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

			Tab.PingForm pingform = new Tab.PingForm();
			tabPagePing.Controls.Add(pingform.Controls[0]);

			Tab.ConfigForm configform = new Tab.ConfigForm();
			tabPageConfig.Controls.Add(configform.Controls[0]);

			Tab.LogForm logform = new Tab.LogForm();
			tabPageLog.Controls.Add(logform.Controls[0]);
		}

		//
		public void RegisterActAssemblies()
		{
			var pin = ActGlobals.oFormActMain.ActPlugins.FirstOrDefault(x => x.pluginFile.Name.Equals("DutyContent.dll"));
			DcConfig.PluginPath = pin?.pluginFile.DirectoryName;

			DcConfig.DataPath = Path.Combine(DcConfig.PluginPath, "Data");
			DcConfig.PluginConfigPath = Path.Combine(DcConfig.PluginPath, "DutyContent.config");

			var actdata = ActGlobals.oFormActMain.AppDataFolder.FullName;
			DcConfig.ActConfigPath = Path.Combine(actdata, "Config", "DutyContent.config");
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
			Logger.I(5, actinfo.GetName().Version);

			if (_ffxiv_plugin_data == null)
			{
				_ffxiv_plugin_data = ActGlobals.oFormActMain.ActPlugins.Where(x =>
						x.pluginFile.Name.ToUpper().StartsWith("FFXIV_ACT_PLUGIN") &&
						(x.lblPluginStatus.Text.ToUpper().StartsWith("FFXIV PLUGIN STARTED.") ||
						x.lblPluginStatus.Text.ToUpper().StartsWith("FFXIV_ACT_PLUGIN STARTED.")))
					.Select(x => x)
					.FirstOrDefault();
			}

			if (_ffxiv_plugin_data == null)
				Logger.E(2);   // FFXIV plugin is missing!
			else
			{
				var ids = ((FFXIV_ACT_Plugin.FFXIV_ACT_Plugin)_ffxiv_plugin_data.pluginObj).DataSubscription;
				ids.NetworkReceived -= FFXIVPlugin_NetworkReceived;
				ids.NetworkReceived += FFXIVPlugin_NetworkReceived;
				ids.ZoneChanged -= FFXIVPlugin_ZoneChanged;
				ids.ZoneChanged += FFXIVPlugin_ZoneChanged;

				Logger.I(6, System.Diagnostics.FileVersionInfo.GetVersionInfo(_ffxiv_plugin_data.pluginFile.FullName).FileVersion);
			}

			// begin region check - from cacbot "VersionChecker.cs"
			try
			{
				var mach = System.Reflection.Assembly.Load("Machina.FFXIV");
				var opcode_manager_type = mach.GetType("Machina.FFXIV.Headers.Opcodes.OpcodeManager");
				var opcode_manager = opcode_manager_type.GetProperty("Instance").GetValue(null);
				var machina_region = opcode_manager_type.GetProperty("GameRegion").GetValue(opcode_manager).ToString();
				switch (machina_region)
				{
					//case "Chinese": // no chinese support now
					case "Korean":
						DcConfig.GameRegion = 1;
						break;
					default:
						DcConfig.GameRegion = 0;
						break;
				}

				Logger.I(45, machina_region, DcConfig.GameRegion);
			}
			catch (Exception ex)
			{
				Logger.Ex(ex, 44);
				DcConfig.GameRegion = 0;
			}
			// end region check

			_save_timer = new System.Timers.Timer() { Interval = 5000 };
			_save_timer.Elapsed += (sender, e) =>
			  {
				  DcConfig.SaveConfig();
				  _save_timer.Enabled = false;
			  };

			_update_timer = new System.Timers.Timer() { Interval = IntervalGameExist };
			_update_timer.Elapsed += (sender, e) =>
			  {
				  UpdateAndCheckProc();
				  _update_timer.Interval = _game_exist ? _game_active ?
					IntervalGameActive : IntervalGameExist : IntervalGameNone;
			  };
			_update_timer.Start();
		}

		//
		public void DeInitPlugin()
		{
			_update_timer.Stop();
			_save_timer.Stop();

			DcConfig.PluginEnable = false;

			Tab.UpdateNotifyForm.Self?.PluginDeinitialize();
			Tab.PingForm.Self?.PluginDeinitialize();
			Tab.DutyForm.Self?.PluginDeinitialize();
			Tab.ConfigForm.Self?.PluginDeinitialize();
			Tab.LogForm.Self?.PluginDeinitialize();
			DcConfig.SaveConfig();

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
			Locale.Initialize(Properties.Resources.DefaultMessage);

			Logger.I(4, DcConfig.PluginVersion.ToString());

			DcConfig.LoadConfig();
			ShowStatusBarAsConfig(true);

			DcConfig.ReadLanguage(true);
			DcContent.ReadContent();
			DcConfig.ReadPacket();

			UpdateUiLocale();

			lblStatusLeft.Text = Locale.Text(99, DcConfig.PluginVersion);  // once here

			//
			Dock = DockStyle.Fill;
			_act_tab.Controls.Add(this);

			//
			Tab.LogForm.Self?.PluginInitialize();
			Tab.ConfigForm.Self?.PluginInitialize();
			Tab.DutyForm.Self?.PluginInitialize();
			Tab.PingForm.Self?.PluginInitialize();

			tabMain.SelectedTab = tabPageDuty;

			// 
			if (DcConfig.DataRemoteUpdate)
			{
				var tag = Updater.CheckPluginUpdate(out string body);
				if (tag > DcConfig.PluginTag)
				{
					Tab.UpdateNotifyForm frm = new Tab.UpdateNotifyForm(tag, body);
					frm.PluginInitialize();
					frm.UpdateUiLocale();

					TabPage tp = new TabPage(Locale.Text(206));
					try
					{
						// why? sometimes trouble
						tp.Controls.Add(frm.Controls[0]);
					}
					catch (Exception ex)
					{
						Logger.Ex(ex);
					}

					tabMain.TabPages.Add(tp);

					if (DcConfig.LastUpdatedPlugin < tag)
					{
						tabMain.SelectedTab = tp;

						DcConfig.LastUpdatedPlugin = tag;
						DcConfig.SaveConfig();
					}

					Logger.C(Color.Aquamarine, 207, DcConfig.PluginTag, tag);
				}
			}

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
				f = new Font(tabMain.Font.FontFamily, 14.0f, FontStyle.Bold, GraphicsUnit.Pixel);
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
				f = new Font(tabMain.Font.FontFamily, 14.0f, FontStyle.Regular, GraphicsUnit.Pixel);
				b = new SolidBrush(Color.DarkSlateGray);
				h = SystemBrushes.Control;
			}

			g.FillRectangle(h, r);
			g.DrawString(p.Text, f, b, r, new StringFormat(s));
		}

		//
		public void RefreshSaveConfig(int interval = 5000)
		{
			_save_timer.Enabled = false;
			_save_timer.Interval = interval;
			_save_timer.Start();
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
			}
			else
			{
				_game_exist = true;

				//
				var fgw = ThirdParty.NativeMethods.GetForegroundWindow();
				ThirdParty.NativeMethods.GetWindowThreadProcessId(fgw, out int id);
				_game_active = _game_process.Process.Id == id;

				//
				var now = DateTime.Now.Ticks;
				var delta = now - _game_connection_tick;
				var span = new TimeSpan(delta);

				if (span.TotalSeconds > 2)
				{
					_game_connection_tick = now;
					DcConfig.Connections.BuildConnections(_game_process.Process, out var retaddr);

					if (!_game_ipaddr.Equals(retaddr))
					{
						if (!retaddr.Equals(IPAddress.None))
							Logger.I(42, retaddr);
						else
							Logger.I(42, Locale.Text(43));

						_game_ipaddr = retaddr;
						Tab.DutyForm.Self?.ResetContentItems();
					}
				}
			}

			var zone = ActGlobals.oFormActMain.CurrentZone;
			if (_game_zone == null || !zone.Equals(_game_zone))
				_game_zone = zone;
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

			lblStatusLeft.Text = Locale.Text(34, zone_name, zone_id);
		}

		//
		public void UpdateUiLocale()
		{
			ThirdParty.FontUtilities.SimpleChangeFont(this, DcConfig.UiFontFamily, true);

			_act_label.Text = Locale.Text(1);  // Duty ready
			_act_tab.Text = Locale.Text(0);    // FFXIV dc

			tabPageDuty.Text = Locale.Text(300);
			Tab.DutyForm.Self?.UpdateUiLocale();

			tabPagePing.Text = Locale.Text(400);
			Tab.PingForm.Self?.UpdateUiLocale();

			tabPageConfig.Text = Locale.Text(200);
			Tab.ConfigForm.Self?.UpdateUiLocale();

			tabPageLog.Text = Locale.Text(500);
			Tab.LogForm.Self?.UpdateUiLocale();

			Tab.UpdateNotifyForm.Self?.UpdateUiLocale();
		}

		//
		public void ShowStatusBarAsConfig(bool force = false)
		{
			if (DcConfig.StatusBar)
			{
				if (!lblStatusLeft.Visible || force)
				{
					tabMain.Dock = DockStyle.None;
					lblStatusLeft.Visible = true;
				}
			}
			else
			{
				if (lblStatusLeft.Visible || force)
				{
					lblStatusLeft.Visible = false;
					tabMain.Dock = DockStyle.Fill;
				}
			}
		}
	}
}
