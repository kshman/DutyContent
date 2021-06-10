using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace DutyContent.Tab
{
	public partial class DutyForm : Form, Interface.ISuppLocale, Interface.IPacketHandler, Interface.ISuppActPlugin
	{
		private static DutyForm _self;
		public static DutyForm Self => _self;

		//
		private bool _is_lock_fate;
		private bool _is_packet_finder;
		private DcContent.SaveTheQueenType _stq_type = DcContent.SaveTheQueenType.No;
		private DcConfig.PacketConfig _new_packet;

		private Overlay.DutyOvForm _overlay;

		public DutyForm()
		{
			_self = this;

			InitializeComponent();

			_overlay = new Overlay.DutyOvForm();
		}

		private void DutyTabForm_FormClosing(object sender, FormClosingEventArgs e)
		{

		}

		public void PluginInitialize()
		{
			//
			lblCurrentDataSet.Text = DcContent.DisplayLanguage;

			//
			var lang = MakeDutyLangList();

			foreach (var i in lang)
			{
				var n = cboDataset.Items.Add(i);
				if (i.Equals(DcConfig.Duty.Language))
					cboDataset.SelectedIndex = n;
			}

			//
			var font = new Font(DcConfig.Duty.LogFontFamily, DcConfig.Duty.LogFontSize, FontStyle.Regular, GraphicsUnit.Point);
			txtContentLog.Font = font;
			btnLogFont.Font = font;
			btnLogFont.Text = $"{DcConfig.Duty.LogFontFamily}, {DcConfig.Duty.LogFontSize}";

			//
			chkEnableOverlay.Checked = DcConfig.Duty.EnableOverlay;

			progbOverlayTransparent.Enabled = DcConfig.Duty.EnableOverlay;
			btnOverlayDimming.Enabled = DcConfig.Duty.EnableOverlay;
			chkOverlayClickThru.Checked = DcConfig.Duty.OverlayClickThru;

			//
			_overlay.SetText(MesgLog.Text(99, System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()));
			_overlay.Location = DcConfig.Duty.OverlayLocation;

			if (DcConfig.Duty.EnableOverlay)
				_overlay.Show();
			else
				_overlay.Hide();
			if (DcConfig.Duty.OverlayClickThru)
				EnableOverlayClickThru();

			//
			chkEnableSound.Checked = DcConfig.Duty.EnableSound;

			txtSoundInstance.Text = DcConfig.Duty.SoundInstanceFile;
			txtSoundInstance.Enabled = DcConfig.Duty.EnableSound;
			btnSoundFindInstance.Enabled = DcConfig.Duty.EnableSound;
			btnSoundPlayInstance.Enabled = DcConfig.Duty.EnableSound;

			txtSoundFate.Text = DcConfig.Duty.SoundFateFile;
			txtSoundFate.Enabled = DcConfig.Duty.EnableSound;
			btnSoundFindFate.Enabled = DcConfig.Duty.EnableSound;
			btnSoundPlayFate.Enabled = DcConfig.Duty.EnableSound;

			//
			chkUseNotifyLine.Checked = DcConfig.Duty.UseNotifyLine;
			txtLineToken.Text = DcConfig.Duty.NotifyLineToken;
			//txtLineToken.Enabled = !DcConfig.Duty.UseNotifyLine;

			chkUseNotifyTelegram.Checked = DcConfig.Duty.UseNotifyTelegram;
			txtTelegramId.Text = DcConfig.Duty.NotifyTelegramId;
			txtTelegramToken.Text = DcConfig.Duty.NotifyTelegramToken;
			//txtLineToken.Enabled = !DcConfig.Duty.UseNotifyTelegram;
			//txtLineToken.Enabled = !DcConfig.Duty.UseNotifyTelegram;

			btnTestNotify.Enabled = DcConfig.Duty.EnableNotify;

			//
			switch (DcConfig.Duty.ActiveFate)
			{
				case 0: rdoFatePreset1.Checked = true; break;
				case 1: rdoFatePreset2.Checked = true; break;
				case 2: rdoFatePreset3.Checked = true; break;
				case 3: rdoFatePreset4.Checked = true; break;
			}
			UpdateFates();
		}

		public void PluginDeinitialize()
		{
			_overlay.Hide();
			_overlay = null;
		}

		private void SaveConfig(int interval = 5000)
		{
			DcControl.Self.RefreshSaveConfig(interval);
		}

		public static List<string> MakeDutyLangList()
		{
			List<string> lst = new List<string>();

			DirectoryInfo di = new DirectoryInfo(DcConfig.DataPath);

			foreach (var fi in di.GetFiles("DcDuty-*.json"))
			{
				var s = fi.Name.Substring(7, fi.Name.Length - 7 - 5);
				lst.Add(s);
			}

			return lst;
		}

		public void RefreshLocale()
		{

		}

		public void UpdateUiLocale()
		{
			tabPageContent.Text = MesgLog.Text(301);
			tabPageSetting.Text = MesgLog.Text(302);
			tabPagePacket.Text = MesgLog.Text(303);

			lblDataSet.Text = MesgLog.Text(304);
			lblLogFont.Text = MesgLog.Text(305);

			chkEnableOverlay.Text = MesgLog.Text(306);
			lblOverlayTransparent.Text = MesgLog.Text(307);
			chkOverlayClickThru.Text = MesgLog.Text(104);

			chkEnableSound.Text = MesgLog.Text(308);
			lblSoundInstance.Text = MesgLog.Text(309);
			lblSoundFate.Text = MesgLog.Text(310);

			chkUseNotifyLine.Text = MesgLog.Text(311);
			lblLineToken.Text = MesgLog.Text(312);

			chkUseNotifyTelegram.Text = MesgLog.Text(313);
			lblTelegramId.Text = MesgLog.Text(314);
			lblTelegramToken.Text = MesgLog.Text(315);

			lblPacketFinder.Text = MesgLog.Text(316);
			lblPacketDesc.Text = MesgLog.Text(317);
			lblPacketBozja.Text = MesgLog.Text(318);

			lstPacketInfo.Columns[0].Text = MesgLog.Text(319);
			lstPacketInfo.Columns[1].Text = MesgLog.Text(320);
			lstPacketInfo.Columns[2].Text = MesgLog.Text(321);
			lstPacketInfo.Columns[3].Text = MesgLog.Text(322);

			lstBozjaInfo.Columns[0].Text = MesgLog.Text(323);
			lstBozjaInfo.Columns[1].Text = MesgLog.Text(324);
			lstBozjaInfo.Columns[2].Text = MesgLog.Text(325);
			lstBozjaInfo.Columns[3].Text = MesgLog.Text(326);

			btnPacketStart.Text = MesgLog.Text(10007);
			btnPacketApply.Text = MesgLog.Text(10009);
		}

		public void PacketHandler(string pid, byte[] message)
		{
			if (_is_packet_finder)
				PacketFinderHandler(message);

			var opcode = BitConverter.ToUInt16(message, 18);

			if (opcode != DcConfig.Packet.OpFate &&
				opcode != DcConfig.Packet.OpDuty &&
				opcode != DcConfig.Packet.OpMatch &&
				opcode != DcConfig.Packet.OpInstance &&
				opcode != DcConfig.Packet.OpSouthernBozja)
				return;

			var data = message.Skip(32).ToArray();

			// FATE
			if (opcode == DcConfig.Packet.OpFate)
			{
				// 53=begin, 54=end, 62=progress
				if (data[0] == 53)
				{
					var fcode = BitConverter.ToUInt16(data, 4);

					if (fcode > 100)
					{
						var fate = DcContent.GetFate(fcode);
						if (_stq_type != DcContent.SaveTheQueenType.No)
							LogSkirmish(10001, fate.Name);
						else
							LogFate(10001, fate.Name);

						if (DcConfig.Duty.Fates[DcConfig.Duty.ActiveFate].Selected.Contains(fcode))
						{
							MesgLog.L("{0} - {1}", DcConfig.Duty.ActiveFate, fcode);
							PlayEffectSoundFate();
							NotifyFate(fate);
							_overlay.PlayFate(fate);
						}
					}
				}
				else if (chkShowDebug.Checked && data[0] == 62 && data[8] > 0)  // more than 0%
				{
					var fcode = BitConverter.ToUInt16(data, 4);

					if (fcode > 100)
					{
						var fate = DcContent.TryFate(fcode);
						if (fate == null)
							LogDebug("unknown fate {0}% \"{1}\"", data[8], fcode);
					}
				}
			}

			// Duty
			else if (opcode == DcConfig.Packet.OpDuty)
			{
				var rcode = data[8];

				if (rcode != 0)
				{
					var roulette = DcContent.GetRoulette(rcode);
					LogRoulette(10002, roulette.Name);
					_overlay.PlayQueue(roulette.Name);
				}
				else
				{
					var insts = new List<int>();
					for (var i = 0; i < 5; i++)
					{
						var icode = BitConverter.ToUInt16(data, 12 + (i * 4));
						if (icode == 0)
							break;
					}

					if (insts.Any())
					{
						LogInstance(10002, string.Join("/", insts.ToArray()));
						_overlay.PlayQueue(MesgLog.Text(10006, $"#{insts.Count}"));
					}
				}

				DcContent.Missions.Clear();
			}

			// match
			else if (opcode == DcConfig.Packet.OpMatch)
			{
				var rcode = BitConverter.ToUInt16(data, 2);
				var icode = BitConverter.ToUInt16(data, 20);
				string name;

				if (icode == 0 && rcode != 0)
				{
					var roulette = DcContent.GetRoulette(rcode);
					LogRoulette(10003, roulette.Name);
					name = roulette.Name;
				}
				else if (icode != 0)
				{
					var instance = DcContent.GetInstance(icode);
					LogInstance(10003, instance.Name);
					name = instance.Name;
				}
				else
				{
					// ???
					name = MesgLog.Text(10003, icode);
				}

				PlayEffecSoundInstance();
				NotifyMatch(name);
				_overlay.PlayMatch(name);
			}

			// instance
			else if (opcode == DcConfig.Packet.OpInstance && DcConfig.Packet.OpInstance != 0)
			{
				// 0[2] instance number
				// 2[2] ?
				// 4[1] 0=enter, 4=enter, 5=leave

				if (data[4] == 0)
				{
					var icode = BitConverter.ToUInt16(data, 0);
					var instance = DcContent.GetInstance(icode);
					LogInstance(10004, instance.Name);
					_overlay.PlayMatch(MesgLog.Text(10004, instance.Name));

					DcContent.Missions.Clear();
				}
				else if (data[4] != 4)
				{
					_overlay.PlayNone();
				}
			}
			// southen bozja front critical engagement
			else if (opcode == DcConfig.Packet.OpSouthernBozja)
			{
				//  0[4] timestamp
				//  4[2] mmss
				//  6[2] ?
				//  8[1] code
				//  9[1] ?
				// 10[1] status 0=end, 1=register, 2=entry, 3=progress
				// 12[1] progress percentage

				var stq =
					_stq_type == DcContent.SaveTheQueenType.Bozja ? 30000 :
					_stq_type == DcContent.SaveTheQueenType.Zadnor ? 30100 :
					30100;  // temporary

				var ce = stq + data[8];
				var stat = data[10];

				if (stat == 0 /* || data[10] == 3 */)
				{
					if (DcContent.Missions.ContainsKey(ce))
						DcContent.Missions.Remove(ce);
				}
				else if (stat == 1 || stat == 2)
				{
					if (!DcContent.Missions.ContainsKey(ce))
					{
						DcContent.Missions.Add(ce, 0);

						var fate = DcContent.GetFate(ce);
						LogCe(10001, fate.Name);

						if (DcConfig.Duty.Fates[DcConfig.Duty.ActiveFate].Selected.Contains(ce))
						{
							PlayEffectSoundFate();
							NotifyFate(fate);
							_overlay.PlayFate(fate);
						}
					}
				}
				else if (stat == 3)
				{
					if (DcContent.Missions.ContainsKey(ce))
					{
						DcContent.Missions.Add(ce, 0);

						var fate = DcContent.GetFate(ce);
						LogCe(10001, fate.Name);
					}
				}
			}
		}

		//
		public void ZoneChanged(uint zone_id, string zone_name)
		{
			//_overlay.PlayNone();

			_stq_type =
				(zone_id == 920) ? DcContent.SaveTheQueenType.Bozja :
				(zone_id == 921) ? DcContent.SaveTheQueenType.Zadnor :
				DcContent.SaveTheQueenType.No;

			if (chkShowDebug.Checked)
				LogDebug("Zone: {0} \"{1}\"", zone_id, zone_name);
		}

		//
		private void WriteLog(Color color, string category, string format, params object[] prms)
		{
			if (txtContentLog == null || txtContentLog.IsDisposed || format == null)
				return;

			var fmt = string.Format(format, prms);
			var dt = DateTime.Now.ToString("HH:mm:ss");
			var ms = $"[{dt}/{category}] {fmt}{Environment.NewLine}";

			WorkerAct.Invoker(() =>
			{
				txtContentLog.SelectionColor = color;
				txtContentLog.SelectionStart = txtContentLog.TextLength;
				txtContentLog.SelectionLength = 0;
				txtContentLog.AppendText(ms);

				txtContentLog.SelectionColor = txtContentLog.ForeColor;
				ThirdParty.NativeMethods.ScrollToBottom(txtContentLog);
			});
		}

		private void WriteLog(Color color, int catkey, int fmtkey, params object[] prms)
		{
			string catergory = MesgLog.Text(catkey);
			string format = MesgLog.Text(fmtkey);
			WriteLog(color, catergory, format, prms);
		}

		//
		private void LogDebug(string msg, params object[] prms)
		{
			WriteLog(Color.Red, "Debug", msg, prms);
		}

		//
		private void LogRoulette(int key, params object[] prms)
		{
			WriteLog(Color.Black, 21, key, prms);
		}

		//
		private void LogInstance(int key, params object[] prms)
		{
			WriteLog(Color.Black, 22, key, prms);
		}

		//
		private void LogFate(int key, params object[] prms)
		{
			WriteLog(Color.Black, 23, key, prms);
		}

		//
		private void LogSkirmish(int key, params object[] prms)
		{
			WriteLog(Color.Black, 24, key, prms);
		}

		//
		private void LogCe(int key, params object[] prms)
		{
			WriteLog(Color.Black, 25, key, prms);
		}

		//
		public void UpdateFates()
		{
			var fs = DcConfig.Duty.Fates[DcConfig.Duty.ActiveFate];

			treeFates.Nodes.Clear();
			fs.MakeSelects(true);

			_is_lock_fate = true;

			foreach (var a in DcContent.Areas)
			{
				var n = treeFates.Nodes.Add(a.Value.Name);
				n.Tag = a.Key + 100000;

				if (fs.Selected.Contains((int)n.Tag))
				{
					n.Checked = true;
					n.Expand();
				}

				foreach (var f in a.Value.Fates)
				{
					var name = f.Value.Name;
					var node = n.Nodes.Add(name);
					node.Tag = f.Key;

					if (fs.Selected.Contains((int)node.Tag))
					{
						node.Checked = true;

						if (!n.IsExpanded)
							n.Expand();
					}
				}
			}

			MakeFatesSelection();

			_is_lock_fate = false;
		}

		//
		private void MakeFatesSelection(bool makeline = false)
		{
			var fs = DcConfig.Duty.Fates[DcConfig.Duty.ActiveFate];

			fs.Selected.Clear();
			FateSelectionMakingLoop(treeFates.Nodes);

			if (makeline)
				fs.MakeLine();
		}

		//
		private void FateSelectionMakingLoop(TreeNodeCollection nodes)
		{
			var fs = DcConfig.Duty.Fates[DcConfig.Duty.ActiveFate];

			foreach (TreeNode n in nodes)
			{
				if (n.Checked)
					fs.Selected.Add((int)n.Tag);
				FateSelectionMakingLoop(n.Nodes);
			}
		}

		private void CboDataset_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!DcConfig.PluginEnable)
				return;

			var l = (string)cboDataset.SelectedItem;

			if (!string.IsNullOrWhiteSpace(l) && !l.Equals(DcConfig.Duty.Language) && DcContent.ReadContent(l))
			{

				lblCurrentDataSet.Text = DcContent.DisplayLanguage;

				SaveConfig();

				Updater.CheckNewVersion();

				UpdateFates();
			}
		}

		private void BtnLogFont_Click(object sender, EventArgs e)
		{
			Font ret = (Font)WorkerAct.Invoker(new WorkerAct.ObjectReturnerDelegate(() =>
			{
				FontDialog dg = new FontDialog
				{
					Font = txtContentLog.Font,
					FontMustExist = true,
					AllowVerticalFonts = false
				};

				return (dg.ShowDialog() == DialogResult.OK) ? dg.Font : null;
			}));

			if (ret != null)
			{
				txtContentLog.Font = ret;

				DcConfig.Duty.LogFontFamily = ret.Name;
				DcConfig.Duty.LogFontSize = ret.Size;

				SaveConfig();

				btnLogFont.Font = ret;
				btnLogFont.Text = $"{DcConfig.Duty.LogFontFamily}, {DcConfig.Duty.LogFontSize}";
			}
		}

		private void TreeFates_AfterCheck(object sender, TreeViewEventArgs e)
		{
			if (!DcConfig.PluginEnable)
				return;

			if (_is_lock_fate)
				return;

			_is_lock_fate = true;

			if (((int)e.Node.Tag) > 100000)
			{
				foreach (TreeNode n in e.Node.Nodes)
					n.Checked = e.Node.Checked;
			}
			else
			{
				if (!e.Node.Checked)
					e.Node.Parent.Checked = false;
				else
				{
					var f = true;
					foreach (TreeNode n in e.Node.Parent.Nodes)
						f &= n.Checked;
					e.Node.Parent.Checked = f;
				}
			}

			MakeFatesSelection(true);
			SaveConfig();

			_is_lock_fate = false;
		}

		//
		private void ChangeFatePreset(int index)
		{
			if (!DcConfig.PluginEnable)
				return;

			if (index >= 0 && index < 4)
			{
				DcConfig.Duty.ActiveFate = index;
				UpdateFates();

				SaveConfig();
			}
		}

		private void RdoFatePreset1_CheckedChanged(object sender, EventArgs e)
		{
			ChangeFatePreset(0);
		}

		private void RdoFatePreset2_CheckedChanged(object sender, EventArgs e)
		{
			ChangeFatePreset(1);
		}

		private void RdoFatePreset3_CheckedChanged(object sender, EventArgs e)
		{
			ChangeFatePreset(2);
		}

		private void RdoFatePreset4_CheckedChanged(object sender, EventArgs e)
		{
			ChangeFatePreset(3);
		}

		private void ChkEnableOverlay_CheckedChanged(object sender, EventArgs e)
		{
			if (!DcConfig.PluginEnable)
				return;

			progbOverlayTransparent.Enabled = chkEnableOverlay.Checked;
			btnOverlayDimming.Enabled = chkEnableOverlay.Checked;

			if (chkEnableOverlay.Checked)
				_overlay.Show();
			else
				_overlay.Hide();

			DcConfig.Duty.EnableOverlay = chkEnableOverlay.Checked;

			SaveConfig();
		}

		private void ProgbOverlayTransparent_Click(object sender, EventArgs e)
		{

		}

		private void BtnOverlayDimming_Click(object sender, EventArgs e)
		{
			_overlay.StartBlink();
		}

		private void ChkOverlayClickThru_CheckedChanged(object sender, EventArgs e)
		{
			if (!DcConfig.PluginEnable)
				return;

			DcConfig.Duty.OverlayClickThru = chkOverlayClickThru.Checked;
			if (DcConfig.Duty.OverlayClickThru)
				EnableOverlayClickThru();
			else
				DisableOverlayClickThru();

			SaveConfig();
		}

		private void ChkEnableSound_CheckedChanged(object sender, EventArgs e)
		{
			if (!DcConfig.PluginEnable)
				return;

			txtSoundInstance.Enabled = chkEnableSound.Checked;
			btnSoundFindInstance.Enabled = chkEnableSound.Checked;
			btnSoundPlayInstance.Enabled = chkEnableSound.Checked;
			txtSoundFate.Enabled = chkEnableSound.Checked;
			btnSoundFindFate.Enabled = chkEnableSound.Checked;
			btnSoundPlayFate.Enabled = chkEnableSound.Checked;

			DcConfig.Duty.EnableSound = chkEnableSound.Checked;

			SaveConfig();
		}

		//
		private void PlayEffectSoundFate()
		{
			if (DcConfig.Duty.EnableSound)
				WorkerAct.PlayEffectSound(DcConfig.Duty.SoundFateFile, DcConfig.Duty.SoundFateVolume);
		}

		//
		private void PlayEffecSoundInstance()
		{
			if (DcConfig.Duty.EnableSound)
				WorkerAct.PlayEffectSound(DcConfig.Duty.SoundInstanceFile, DcConfig.Duty.SoundInstanceVolume);
		}

		private void BtnSoundPlayInstance_Click(object sender, EventArgs e)
		{
			if (!DcConfig.PluginEnable)
				return;

			PlayEffecSoundInstance();
		}

		private void BtnSoundPlayFate_Click(object sender, EventArgs e)
		{
			if (!DcConfig.PluginEnable)
				return;

			PlayEffectSoundFate();
		}

		//
		private string SoundFileOpenDialog()
		{
			string filename = (string)WorkerAct.Invoker(new WorkerAct.ObjectReturnerDelegate(() =>
			{
				var dg = new OpenFileDialog
				{
					Title = MesgLog.Text(101),
					DefaultExt = "wav",
					Filter = MesgLog.Text(102)
				};

				return (dg.ShowDialog() == DialogResult.OK) ? dg.FileName : null;
			}));

			return filename;
		}

		private void BtnSoundFindInstance_Click(object sender, EventArgs e)
		{
			if (!DcConfig.PluginEnable)
				return;

			string filename = SoundFileOpenDialog();

			DcConfig.Duty.SoundInstanceFile = string.IsNullOrEmpty(filename) ? string.Empty : filename;
			txtSoundInstance.Text = DcConfig.Duty.SoundInstanceFile;

			SaveConfig();
		}

		private void BtnSoundFindFate_Click(object sender, EventArgs e)
		{
			if (!DcConfig.PluginEnable)
				return;

			string filename = SoundFileOpenDialog();

			DcConfig.Duty.SoundFateFile = string.IsNullOrEmpty(filename) ? string.Empty : filename;
			txtSoundFate.Text = DcConfig.Duty.SoundFateFile;

			SaveConfig();
		}

		private async void BtnTestNotify_Click(object sender, EventArgs e)
		{
			if (!DcConfig.PluginEnable)
				return;

			string s = MesgLog.Text(103);

			if (DcConfig.Duty.UseNotifyLine)
				await NotifyUsingLine(s);

			if (DcConfig.Duty.UseNotifyTelegram)
				NotifyUsingTelegram(s);
		}

		//
		private void SendNotify(string s)
		{
			if (DcConfig.Duty.UseNotifyLine)
				NotifyUsingLine(s).Wait();

			if (DcConfig.Duty.UseNotifyTelegram)
				NotifyUsingTelegram(s);
		}

		//
		private void NotifyFate(DcContent.Fate f)
		{
			if (!DcConfig.Duty.EnableNotify)
				return;

			string s = MesgLog.Text(10005, f.Name);
			SendNotify(s);
		}

		//
		private void NotifyMatch(string name)
		{
			if (!DcConfig.Duty.EnableNotify)
				return;

			string s = MesgLog.Text(10003, name);
			SendNotify(s);
		}

		private void ChkUseNotifyLine_CheckedChanged(object sender, EventArgs e)
		{
			if (!DcConfig.PluginEnable)
				return;

			DcConfig.Duty.UseNotifyLine = chkUseNotifyLine.Checked;
			txtLineToken.Enabled = chkUseNotifyLine.Checked;

			btnTestNotify.Enabled = DcConfig.Duty.EnableNotify;

			SaveConfig();
		}

		private void LblLineNotifyBotLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (!DcConfig.PluginEnable)
				return;

			Process.Start(lblLineNotifyBotLink.Text);
		}

		private void TxtLineToken_KeyDown(object sender, KeyEventArgs e)
		{
			if (!DcConfig.PluginEnable)
				return;

			if (e.KeyCode == Keys.Enter)
			{
				DcConfig.Duty.NotifyLineToken = txtLineToken.Text;
				SaveConfig();
			}
		}

		//
		internal async Task NotifyUsingLine(string mesg)
		{
			if (txtLineToken.TextLength == 0)
				return;

			if (!txtLineToken.Text.Equals(DcConfig.Duty.NotifyLineToken))
			{
				DcConfig.Duty.NotifyLineToken = txtLineToken.Text;
				SaveConfig();
			}

			var hc = new HttpClient();
			hc.DefaultRequestHeaders.Add("Authorization", $"Bearer {DcConfig.Duty.NotifyLineToken}");

			var param = new Dictionary<string, string>
			{
				{ "message", mesg }
			};

			await hc.PostAsync("https://notify-api.line.me/api/notify",
				new FormUrlEncodedContent(param)).ConfigureAwait(false);
		}

		//
		private void ChkUseNotifyTelegram_CheckedChanged(object sender, EventArgs e)
		{
			if (!DcConfig.PluginEnable)
				return;

			DcConfig.Duty.UseNotifyTelegram = chkUseNotifyTelegram.Checked;
			txtTelegramId.Enabled = chkUseNotifyTelegram.Checked;
			txtTelegramToken.Enabled = chkUseNotifyTelegram.Checked;

			btnTestNotify.Enabled = DcConfig.Duty.EnableNotify;

			SaveConfig();
		}

		private void TxtTelegramId_KeyDown(object sender, KeyEventArgs e)
		{
			if (!DcConfig.PluginEnable)
				return;

			if (e.KeyCode == Keys.Enter)
			{
				DcConfig.Duty.NotifyTelegramId = txtTelegramId.Text;
				SaveConfig();
			}
		}

		private void TxtTelegramToken_KeyDown(object sender, KeyEventArgs e)
		{
			if (!DcConfig.PluginEnable)
				return;

			if (e.KeyCode == Keys.Enter)
			{
				DcConfig.Duty.NotifyTelegramToken = txtTelegramToken.Text;
				SaveConfig();
			}
		}

		//
		private static string EncodeJsonChars(string text)
		{
			return text.Replace("\b", "\\\b")
			.Replace("\f", "\\\f")
			.Replace("\n", "\\\n")
			.Replace("\r", "\\\r")
			.Replace("\t", "\\\t")
			.Replace("\"", "\\\"")
			.Replace("\\", "\\\\");
		}

		//
		private bool NotifyUsingTelegram(string mesg)
		{
			if (txtTelegramId.TextLength == 0 || txtTelegramToken.TextLength == 0)
				return false;

			if (!txtTelegramId.Text.Equals(DcConfig.Duty.NotifyTelegramId))
			{
				DcConfig.Duty.NotifyTelegramId = txtTelegramId.Text;
				SaveConfig();
			}

			if (!txtTelegramToken.Text.Equals(DcConfig.Duty.NotifyTelegramToken))
			{
				DcConfig.Duty.NotifyTelegramToken = txtTelegramToken.Text;
				SaveConfig();
			}

			// https://codingman.tistory.com/41

			var json = string.Format("{{\"chat_id\":\"{0}\", \"text\":\"{1}\"}}",
				DcConfig.Duty.NotifyTelegramId, EncodeJsonChars(mesg));
			var jbin = Encoding.UTF8.GetBytes(json);

			var url = string.Format("https://api.telegram.org/bot{0}/sendMessage", DcConfig.Duty.NotifyTelegramToken);
			HttpWebRequest req = WebRequest.Create(new Uri(url)) as HttpWebRequest;
			req.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
			req.Timeout = 30 * 1000;
			req.Method = "POST";
			req.ContentType = "application/json";

			using (var st = req.GetRequestStream())
			{
				st.Write(jbin, 0, jbin.Length);
				st.Flush();
			}

			HttpWebResponse res = null;
			try
			{
				res = req.GetResponse() as HttpWebResponse;
				if (res.StatusCode == HttpStatusCode.OK)
				{
					string ss = null;
					using (var st = res.GetResponseStream())
					{
						using (var rdr = new StreamReader(st, Encoding.UTF8))
							ss = rdr.ReadToEnd();
					}

					if (0 < ss.IndexOf("\"ok\":true"))
						return true;
					else
						return false;
				}
				else
				{
					// http status is not ok
					return false;
				}
			}
			catch
			{
				// ???
				return false;
			}
			finally
			{
				if (res != null)
					res.Close();
			}
		}

		//
		private void PacketFinderResetUi(bool is_enable)
		{
			if (!is_enable)
			{
				btnPacketStart.Text = MesgLog.Text(10007);
				btnPacketStart.BackColor = Color.Transparent;
			}
			else
			{
				btnPacketStart.Text = MesgLog.Text(10008);
				btnPacketStart.BackColor = Color.Salmon;
			}

			btnPacketApply.Visible = is_enable;
			btnPacketApply.Enabled = is_enable;
			lstPacketInfo.Enabled = is_enable;
			txtPacketInfo.Enabled = is_enable;
			txtPacketDescription.Enabled = is_enable;
			lstBozjaInfo.Enabled = is_enable;
		}

		//
		private void PacketFindClearUi(DcConfig.PacketConfig newpk)
		{
			//
			txtPacketDescription.Text = DcConfig.Packet.Version;
			lstBozjaInfo.Items.Clear();

			// FATE
			lstPacketInfo.Items[0].SubItems[1].Text = DcConfig.Packet.OpFate.ToString();
			lstPacketInfo.Items[0].SubItems[2].Text = "";
			lstPacketInfo.Items[0].SubItems[3].Text = newpk.OpFate.ToString();

			// Duty
			lstPacketInfo.Items[1].SubItems[1].Text = DcConfig.Packet.OpDuty.ToString();
			lstPacketInfo.Items[1].SubItems[2].Text = "";
			lstPacketInfo.Items[1].SubItems[3].Text = newpk.OpDuty.ToString();

			// Match
			lstPacketInfo.Items[2].SubItems[1].Text = DcConfig.Packet.OpMatch.ToString();
			lstPacketInfo.Items[2].SubItems[2].Text = "";
			lstPacketInfo.Items[2].SubItems[3].Text = newpk.OpMatch.ToString();

			// Instance
			lstPacketInfo.Items[3].SubItems[1].Text = DcConfig.Packet.OpInstance.ToString();
			lstPacketInfo.Items[3].SubItems[2].Text = "";
			lstPacketInfo.Items[3].SubItems[3].Text = newpk.OpInstance.ToString();

			// Bozja
			lstPacketInfo.Items[4].SubItems[1].Text = DcConfig.Packet.OpSouthernBozja.ToString();
			lstPacketInfo.Items[4].SubItems[2].Text = "";
			lstPacketInfo.Items[4].SubItems[3].Text = newpk.OpSouthernBozja.ToString();
		}

		private void BtnPacketStart_Click(object sender, EventArgs e)
		{
			if (!_is_packet_finder)
			{
				_new_packet = new DcConfig.PacketConfig()
				{
					Version = DcConfig.Packet.Version,
					OpFate = 0,
					OpDuty = 0,
					OpMatch = 0,
					OpInstance = 0,
					OpSouthernBozja = 0,
				};

				PacketFindClearUi(_new_packet);
			}

			_is_packet_finder = !_is_packet_finder;
			PacketFinderResetUi(_is_packet_finder);
		}

		private void BtnPacketApply_Click(object sender, EventArgs e)
		{
			var ret = (DialogResult)WorkerAct.Invoker(new WorkerAct.ObjectReturnerDelegate(() =>
				  {
					  var r = MessageBox.Show(MesgLog.Text(10022), MesgLog.Text(0), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					  return r;
				  }));

			if (ret == DialogResult.Yes)
			{
				_new_packet.Version = txtPacketDescription.Text;

				DcConfig.Packet.Version = _new_packet.Version;
				DcConfig.Packet.OpFate = _new_packet.OpFate;
				DcConfig.Packet.OpDuty = _new_packet.OpDuty;
				DcConfig.Packet.OpMatch = _new_packet.OpMatch;
				DcConfig.Packet.OpInstance = _new_packet.OpInstance;
				DcConfig.Packet.OpSouthernBozja = _new_packet.OpSouthernBozja;
				DcConfig.Packet.Save();

				_is_packet_finder = false;
				PacketFinderResetUi(false);
			}
		}

		private void LstPacketInfo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstPacketInfo.SelectedIndices.Count != 1)
				return;

			int m;

			switch (lstPacketInfo.SelectedIndices[0])
			{
				case 0: m = 10010; break;
				case 1: m = 10011; break;
				case 2: m = 10011; break;
				case 3: m = 10011; break;
				case 4: m = 10014; break;
				default: m = 10015; break;
			}

			txtPacketInfo.Text = MesgLog.Text(m);
		}

		private void LstBozjaInfo_MouseDoubleClick(object sender, MouseEventArgs e)
		{

		}

		private void LstBozjaInfo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstBozjaInfo.SelectedItems.Count != 1)
				return;

			ushort opcode = (ushort)lstBozjaInfo.SelectedItems[0].Tag;

			_new_packet.OpSouthernBozja = opcode;

			lstPacketInfo.Items[4].SubItems[2].Text = MesgLog.Text(10023);
			lstPacketInfo.Items[4].SubItems[3].Text = _new_packet.OpSouthernBozja.ToString();
		}

		private static readonly short[] _packet_target_fates =
		{
			// middle la noscea
			553, 649, 687, 688, 693, 717,
			220, 221, 222, 223, 225, 226, 227, 229, 231, 233, 235, 237, 238, 239, 240,
			1387,

			// southern bozja front
			1597, 1598, 1599,
			1600, 1601, 1602, 1603, 1604, 1605, 1606, 1607, 1608, 1609,
			1610, 1611, 1612, 1613, 1614, 1615, 1616, 1617, 1618, 1619,
			1620, 1621, 1622, 1623, 1624, 1625, 1626, 1627, 1628,

			// zadnor
			1717, 1718, 1719, 1720, 1721, 1722, 1723, 1724,
			1725, 1726, 1727, 1728, 1729, 1730, 1731, 1732,
			1733, 1734, 1735, 1736, 1737, 1738, 1739, 1740, 1741, 1742,
		};

		//
		private void PacketFinderHandler(byte[] message)
		{
			var opcode = BitConverter.ToUInt16(message, 18);
			var data = message.Skip(32).ToArray();

			// fate
			if (_new_packet.OpFate == 0 && data.Length > 4 && data[0] == 0x3E)
			{
				var cc = BitConverter.ToInt16(data, 4);
				if (_packet_target_fates.Contains(cc) && _new_packet.OpFate != opcode)
				{
					_new_packet.OpFate = opcode;

					WorkerAct.Invoker(() =>
					{
						lstPacketInfo.Items[0].SubItems[2].Text = MesgLog.Text(10016);
						lstPacketInfo.Items[0].SubItems[3].Text = _new_packet.OpFate.ToString();
					});

					return;
				}
			}

			// duty
			if (_new_packet.OpDuty == 0 && data.Length > 12)
			{
				var rcode = data[8];
				if (rcode == 0)
				{
					// The Steps of Fath (83)
					short m = BitConverter.ToInt16(data, 12);
					if (m == 83 && _new_packet.OpDuty != opcode)
					{
						_new_packet.OpDuty = opcode;

						WorkerAct.Invoker(() =>
						{
							lstPacketInfo.Items[1].SubItems[2].Text = MesgLog.Text(10016);
							lstPacketInfo.Items[1].SubItems[3].Text = _new_packet.OpDuty.ToString();
						});

						return;
					}
				}
			}

			// match
			if (_new_packet.OpMatch == 0 && data.Length > 20)
			{
				var rcode = data[2];
				if (rcode == 0)
				{
					// The Steps of Fath (83)
					short m = BitConverter.ToInt16(data, 20);
					if (m == 83 && _new_packet.OpMatch != opcode)
					{
						_new_packet.OpMatch = opcode;

						WorkerAct.Invoker(() =>
						{
							lstPacketInfo.Items[2].SubItems[2].Text = MesgLog.Text(10016);
							lstPacketInfo.Items[2].SubItems[3].Text = _new_packet.OpMatch.ToString();
						});

						return;
					}
				}
			}

			// instance
			if (_new_packet.OpInstance == 0 && data.Length >= 16)
			{
				// The Steps of Fath (83)
				short m = BitConverter.ToInt16(data, 0);
				short u = BitConverter.ToInt16(data, 2);
				if (m == 83 && u == 0 && _new_packet.OpInstance != opcode)
				{
					_new_packet.OpInstance = opcode;

					WorkerAct.Invoker(() =>
					{
						lstPacketInfo.Items[3].SubItems[2].Text = MesgLog.Text(10016);
						lstPacketInfo.Items[3].SubItems[3].Text = _new_packet.OpInstance.ToString();
					});

					return;
				}
			}

			// critical engagement
			if (data.Length >= 12 && _stq_type != DcContent.SaveTheQueenType.No)
			{
				//  0[4] timestamp
				//  4[2] mmss
				//  6[2] ?
				//  8[1] code
				//  9[1] members
				// 10[1] status 0=end, 1=register, 2=entry, 3=progress
				// 12[1] progress percentage

				var code = data[8];

				if (code < 0 || code > 15)
				{
					// not ce
					return;
				}

				var stat = data[10];
				var prg = data[12];
				var mem = data[9];
				var ok = false;

				/*
				if (stat == 0)
				{
					// end. other conditions unknown
					ok = true;
				}
				else*/
				if (stat == 1)
				{
					// register. progress must be 0
					if (mem >= 0 && mem <= 48 && prg == 0)
						ok = true;
				}
				else if (stat == 2)
				{
					// entry. progress must be 0
					if (mem >= 0 && mem <= 48 && prg == 0)
						ok = true;
				}
				else if (stat == 3)
				{
					// progress. progress must be in 1~99
					if (mem > 0 && mem <= 48 && prg >= 1 && prg < 100)
						ok = true;
				}

				if (ok)
				{
					var stq =
						_stq_type == DcContent.SaveTheQueenType.Bozja ? 30000 :
						_stq_type == DcContent.SaveTheQueenType.Zadnor ? 30100 :
						30100;  // temporary
					var ce = DcContent.GetFate(code + stq);

					var li = new ListViewItem(new string[]
					{
						ce.Name,
						DcContent.CeStatusToString(stat),
						mem.ToString(),
						prg.ToString()
					})
					{
						Tag = opcode
					};

					WorkerAct.Invoker(() =>
					{
						lstBozjaInfo.Items.Add(li);
						lstBozjaInfo.EnsureVisible(lstBozjaInfo.Items.Count - 1);
					});
				}
			}
		}

		void EnableOverlayClickThru()
		{
			long initialStyle = (long)ThirdParty.NativeMethods.GetWindowLong(_overlay.Handle, -20);
			if (DcConfig.Duty.OverlayClickThru)
			{
				ThirdParty.NativeMethods.SetWindowLong(_overlay.Handle, -20, (IntPtr)(initialStyle | 0x80000 | 0x20));
			}
			else
			{
				ThirdParty.NativeMethods.SetWindowLong(_overlay.Handle, -20, (IntPtr)(0x00000 | 0x80000));
			}
		}

		void DisableOverlayClickThru()
		{
			long initialStyle = (long)ThirdParty.NativeMethods.GetWindowLong(_overlay.Handle, -20);
			if (DcConfig.Duty.OverlayClickThru)
			{
				ThirdParty.NativeMethods.SetWindowLong(_overlay.Handle, -20, (IntPtr)(initialStyle | 0x80000 | 0x20));
			}
			else
			{
				ThirdParty.NativeMethods.SetWindowLong(_overlay.Handle, -20, (IntPtr)(0x00000 | 0x80000));
			}
		}
	}
}
