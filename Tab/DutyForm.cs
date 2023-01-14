//#define TESTPK
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

namespace DutyContent.Tab
{
    public partial class DutyForm : Form, Interface.ISuppLocale, Interface.IPacketHandler, Interface.ISuppActPlugin
    {
        private static DutyForm _self;
        public static DutyForm Self => _self;

        //
        private bool _is_lock_fate;
        private ushort _last_fate = 0;
        private ushort _last_zone = 0;

        //
        private bool _is_packet_finder;
        private DcContent.SaveTheQueenType _stq_type = DcContent.SaveTheQueenType.No;
        private DcConfig.PacketConfig _new_packet;

        //
        private Dictionary<string, string> _packet_list = new Dictionary<string, string>();

        //
        private Overlay.DutyOvForm _overlay;

        //
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
            RefreshDatasetList();
            RefreshPacketList();

            //
            chkEnableOverlay.Checked = DcConfig.Duty.EnableOverlay;

            progbOverlayTransparent.Enabled = DcConfig.Duty.EnableOverlay;
            btnOverlayDimming.Enabled = DcConfig.Duty.EnableOverlay;
            chkOverlayClickThru.Checked = DcConfig.Duty.OverlayClickThru;
            chkOverlayAutoHide.Checked = DcConfig.Duty.OverlayAutoHide;

            //
            _overlay.SetText(Locale.Text(99, DcConfig.PluginVersion.ToString()));
            _overlay.Location = DcConfig.Duty.OverlayLocation;

            if (DcConfig.Duty.EnableOverlay)
                _overlay.Show();
            else
                _overlay.Hide();
            if (DcConfig.Duty.OverlayClickThru)
                _overlay.SetClickThruStatus(true);

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

            chkUseNotifyTelegram.Checked = DcConfig.Duty.UseNotifyTelegram;
            txtTelegramId.Text = DcConfig.Duty.NotifyTelegramId;
            txtTelegramToken.Text = DcConfig.Duty.NotifyTelegramToken;

            chkUseNotifyDiscowk.Checked = DcConfig.Duty.UseNotifyDiscordWebhook;
            txtDiscowkUrl.Text = DcConfig.Duty.NotifyDiscordWebhookUrl;
            chkDiscowkTts.Checked = DcConfig.Duty.NotifyDiscordWebhookTts;

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

            //
            if (cboPacketset.SelectedIndex >= 0)
            {
                var p = _packet_list.ElementAt(cboPacketset.SelectedIndex);
                RemotePacketUpdate(p.Key);
            }

            //
#if TESTPK
            Logger.Write(Color.Red, "[TEST PK MODE]");
#endif
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

        public void RefreshLocale()
        {

        }

        public void UpdateUiLocale()
        {
            tabPageContent.Text = Locale.Text(301);
            tabPageSetting.Text = Locale.Text(302);
            tabPagePacket.Text = Locale.Text(337);

            lblDataSet.Text = Locale.Text(304);
            lblPacketSet.Text = Locale.Text(336);
            btnResetContentList.Text = Locale.Text(347);

            chkEnableOverlay.Text = Locale.Text(306);
            lblOverlayTransparent.Text = Locale.Text(307);
            chkOverlayClickThru.Text = Locale.Text(104);
            chkOverlayAutoHide.Text = Locale.Text(105);

            chkEnableSound.Text = Locale.Text(308);
            lblSoundInstance.Text = Locale.Text(309);
            lblSoundFate.Text = Locale.Text(310);

            chkUseNotifyLine.Text = Locale.Text(311);
            lblLineToken.Text = Locale.Text(312);

            chkUseNotifyTelegram.Text = Locale.Text(313);
            lblTelegramId.Text = Locale.Text(314);
            lblTelegramToken.Text = Locale.Text(315);

            chkUseNotifyDiscowk.Text = Locale.Text(338);
            chkDiscowkTts.Text = Locale.Text(341);
            lblDiscowkUrl.Text = Locale.Text(339);

            btnTestNotify.Text = Locale.Text(340);

            lblPacketFinder.Text = Locale.Text(316);
            lblPacketDesc.Text = Locale.Text(317);
            lblPacketBozja.Text = Locale.Text(318);

            lstPacketInfo.Columns[0].Text = Locale.Text(319);
            lstPacketInfo.Columns[1].Text = Locale.Text(320);
            lstPacketInfo.Columns[2].Text = Locale.Text(321);
            lstPacketInfo.Columns[3].Text = Locale.Text(322);

            lstBozjaInfo.Columns[0].Text = Locale.Text(323);
            lstBozjaInfo.Columns[1].Text = Locale.Text(324);
            lstBozjaInfo.Columns[2].Text = Locale.Text(325);
            lstBozjaInfo.Columns[3].Text = Locale.Text(326);

            btnPacketStart.Text = Locale.Text(10007);
            btnPacketApply.Text = Locale.Text(10009);

            // content reset
            lstContents.InitializeContentList(
                Locale.Text(343),   // ID
                Locale.Text(344),   // Type
                Locale.Text(345),   // %
                Locale.Text(346));  // Name

            Image im_r = Properties.Resources.pix_rdrt_red;
            Image im_g = Properties.Resources.pix_rdrt_green;
            Image im_p = Properties.Resources.pix_rdrt_puple;
            Image im_b = Properties.Resources.pix_rdrt_bline;

            lstContents.ClearImages();
            lstContents.AddCategoryImage(im_b, Locale.Text(27), Brushes.Black); // 0, none
            lstContents.AddCategoryImage(im_g, Locale.Text(21));                // 1, roulette
            lstContents.AddCategoryImage(im_g, Locale.Text(22));                // 2, instance
            lstContents.AddCategoryImage(im_r, Locale.Text(23));                // 3, FATE
            lstContents.AddCategoryImage(im_r, Locale.Text(24));                // 4, skirmish
            lstContents.AddCategoryImage(im_p, Locale.Text(25));                // 5, CE
            lstContents.AddCategoryImage(im_p, Locale.Text(38));                // 6, Match
            lstContents.AddCategoryImage(im_g, Locale.Text(39));                // 7, Entry

            lstContents.AddContentItem(0, Locale.Text(27));
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
                opcode != DcConfig.Packet.OpZone &&
                opcode != DcConfig.Packet.OpCe)
                return;

            var data = message.Skip(32).ToArray();

            // FATE
            if (opcode == DcConfig.Packet.OpFate)
            {
                var fcode = BitConverter.ToUInt16(data, 4);

                if (fcode > 100)
                {
                    if (DcConfig.Duty.PacketForLocal)
                    {
                        // 53=begin, 54=end, 62=progress
                        if (data[0] == 53)
                        {
                            var fate = DcContent.GetFate(fcode);
                            TraceFate(fcode, true, fate, 0);

                            if (DcConfig.Duty.Fates[DcConfig.Duty.ActiveFate].Selected.Contains(fcode))
                            {
                                PlayEffectSoundFate();
                                NotifyFate(fate);
                                _overlay.PlayFate(fate);
                            }

                            _last_fate = fcode;
                        }
                        else if (data[0] == 62 && data[8] > 0)  // more than 0%
                        {
                            var fate = DcContent.TryFate(fcode);

                            if (fate != null)
                                TraceFate(fcode, false, fate, data[8]);
                            else
                            {
                                if (DcConfig.DebugEnable)
                                {
                                    WriteLog(Color.Magenta, 37, 12, fcode);
                                    _last_fate = fcode;
                                }
                            }
                        }
                        else if (data[0] == 54)
                        {
                            var fate = DcContent.TryFate(fcode);

                            if (fate != null)
                                TraceFate(fcode, false, fate);
                        }
                    }
                    else
                    {
                        // 52,49,66=begin / 55=probably npc contact point
                        // 60=progress
                        // 65=end
                        if (data[0] == 52)
                        {
                            var fate = DcContent.GetFate(fcode);
                            TraceFate(fcode, true, fate, 0);

                            if (DcConfig.Duty.Fates[DcConfig.Duty.ActiveFate].Selected.Contains(fcode))
                            {
                                PlayEffectSoundFate();
                                NotifyFate(fate);
                                _overlay.PlayFate(fate);
                            }

                            _last_fate = fcode;
                        }
                        else if (data[0] == 60 && data[8] > 0)  // more than 0%
                        {
                            var fate = DcContent.TryFate(fcode);

                            if (fate != null)
                                TraceFate(fcode, false, fate, data[8]);
                            else
                            {
                                if (DcConfig.DebugEnable)
                                {
                                    WriteLog(Color.Magenta, 37, 12, fcode);
                                    _last_fate = fcode;
                                }
                            }
                        }
                        else if (data[0] == 65)
                        {
                            var fate = DcContent.TryFate(fcode);

                            if (fate != null)
                                TraceFate(fcode, false, fate);
                        }
                    }
                }
            }

            // Duty
            else if (opcode == DcConfig.Packet.OpDuty)
            {
                if (DcConfig.Duty.PacketForLocal)
                {
                    // for ACTOZ/Korea
                    if (data[11] == 0)
                    {
                        var rcode = data[8];

                        if (rcode != 0)
                        {
                            var roulette = DcContent.GetRoulette(rcode);
                            TraceEntryRoulette(roulette);
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
                                TraceEntryInstance(insts);
                                _overlay.PlayQueue(Locale.Text(10006, $"#{insts.Count}"));
                            }
                        }

                        DcContent.Missions.Clear();
                    }
                }
                else
                {
                    if (data[19] == 0) // duty packet comes twice, index 19 is 0 and 1
                    {
                        // for global
                        var rcode = data[16];

                        if (rcode != 0)
                        {
                            var roulette = DcContent.GetRoulette(rcode);
                            TraceEntryRoulette(roulette);
                            _overlay.PlayQueue(roulette.Name);
                        }
                        else
                        {
                            var insts = new List<int>();
                            for (var i = 0; i < 5; i++)
                            {
                                var icode = BitConverter.ToUInt16(data, 20 + (i * 4));
                                if (icode == 0)
                                    break;
                            }

                            if (insts.Any())
                            {
                                TraceEntryInstance(insts);
                                _overlay.PlayQueue(Locale.Text(10006, $"#{insts.Count}"));
                            }
                        }

                        DcContent.Missions.Clear();
                    }
                }
            }

            // match
            else if (opcode == DcConfig.Packet.OpMatch)
            {
                string name = null;

                if (DcConfig.Duty.PacketForLocal)
                {
                    // For ACTOZ/Korea
                    var rcode = BitConverter.ToUInt16(data, 2);
                    var icode = BitConverter.ToUInt16(data, 20);

                    if (icode == 0 && rcode != 0)
                    {
                        var roulette = DcContent.GetRoulette(rcode);
                        TraceMatchRoulette(roulette);
                        name = roulette.Name;
                    }
                    else if (icode != 0)
                    {
                        var instance = DcContent.GetInstance(icode);
                        TraceMatchInstance(instance);
                        name = instance.Name;
                    }
                    else
                    {
                        // ???
                        name = Locale.Text(10003, icode);
                    }
                }
                else
                {
                    // For global
                    var rcode = BitConverter.ToUInt16(data, 2);
                    var icode = BitConverter.ToUInt16(data, 28);

                    if (icode == 0 && rcode != 0)
                    {
                        var roulette = DcContent.GetRoulette(rcode);
                        TraceMatchRoulette(roulette);
                        name = roulette.Name;
                    }
                    else if (icode != 0)
                    {
                        var instance = DcContent.GetInstance(icode);
                        TraceMatchInstance(instance);
                        name = instance.Name;
                    }
                    else
                    {
                        // ???
                        name = Locale.Text(10003, icode);
                    }
                }

                if (!string.IsNullOrEmpty(name))
                {
                    PlayEffecSoundInstance();
                    NotifyMatch(name);
                    _overlay.PlayMatch(name);
                }
            }

            // instance
            else if (DcConfig.Packet.OpInstance != 0 && opcode == DcConfig.Packet.OpInstance)
            {
                // 0[2] instance number
                // 2[2] ?
                // 4[1] 0=enter, 4=enter, 5=leave

                if (data[4] == 0)
                {
                    var icode = BitConverter.ToUInt16(data, 0);
                    var instance = DcContent.GetInstance(icode);
                    TraceEnterInstance(instance);
                    _overlay.PlayMatch(Locale.Text(10004, instance.Name));

                    DcContent.Missions.Clear();
                    ResetContentItems();    // frankly no meaning to here
                }
                else if (data[4] != 4)
                {
                    _overlay.PlayNone();
                }
            }

            // zone
            else if (DcConfig.Packet.OpZone != 0 && opcode == DcConfig.Packet.OpZone)
            {
                var zone = BitConverter.ToUInt16(data, 4);

                if (zone != _last_zone)
                {
                    _last_zone = zone;
                    ResetContentItems();
                }
            }

            // save the queen critical engagement
            else if (DcConfig.Packet.OpCe != 0 && opcode == DcConfig.Packet.OpCe)
            {
                //  0[4] timestamp
                //  4[2] mmss
                //  6[2] ?
                //  8[1] code
                //  9[1] members
                // 10[1] status 0=end, 1=register, 2=entry, 3=progress
                // 12[1] progress percentage

                if (_stq_type == DcContent.SaveTheQueenType.No)
                {
                    if (IsFateForSouthernBozja(_last_fate))
                        _stq_type = DcContent.SaveTheQueenType.Bozja;
                    else if (IsFateForZadnor(_last_fate))
                        _stq_type = DcContent.SaveTheQueenType.Zadnor;
                    else
                        _stq_type = DcContent.SaveTheQueenType.Zadnor;
                }

                var ce = data[8] + DcContent.SaveTheQueenTypeToCeBase(_stq_type);
                var stat = data[10];
                var fate = DcContent.GetFate(ce);

                if (stat == 0 /* || data[10] == 3 */)
                {
                    if (DcContent.Missions.ContainsKey(ce))
                        DcContent.Missions.Remove(ce);

                    TraceCe(ce, false, fate);
                }
                else if (stat == 1 || stat == 2)
                {
                    var withlog = false;

                    if (!DcContent.Missions.ContainsKey(ce))
                    {
                        withlog = true;
                        DcContent.Missions.Add(ce, 0);

                        if (DcConfig.Duty.Fates[DcConfig.Duty.ActiveFate].Selected.Contains(ce))
                        {
                            PlayEffectSoundFate();
                            NotifyFate(fate);
                            _overlay.PlayFate(fate);
                        }
                    }

                    TraceCe(ce, withlog, fate, stat == 1 ? "R" : "E");
                }
                else if (stat == 3)
                {
                    if (!DcContent.Missions.ContainsKey(ce))
                        DcContent.Missions.Add(ce, 0);

                    TraceCe(ce, false, fate, data[12].ToString());
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

#if false
			LogInstance(10025, $"{zone_name} ({zone_id})");

			if (DcConfig.DebugEnable)
				LogDebug("Zone: {0}", zone_id);
#endif

            // Probably receive FATE auto end command before changing zone
            // No end data found: logout, critical engagement -> have to reset
            if (DcConfig.Packet.OpZone == 0)
                ResetContentItems();
        }

        //
        private void WriteLog(Color color, int catkey, int fmtkey, params object[] prms)
        {
            string category = Locale.Text(catkey);
            string format = Locale.Text(fmtkey);
            Logger.WriteCategory(color, category, format, prms);
        }

        //
        private void TraceFate(ushort code, bool withlog, DcContent.Fate fate, int progress = -1)
        {
            // TODO: check area

            int key, subs;

            if (_stq_type != DcContent.SaveTheQueenType.No || IsSkirmishFate(code))
            {
                key = 24;
                subs = 4;
            }
            else
            {
                key = 23;
                subs = 3;
            }

            if (withlog)
                WriteLog(Color.Black, key, 10001, fate.Name);

            WorkerAct.Invoker(() => lstContents.TreatItemFate(code, subs, progress, fate.Name));
        }

        //
        private void TraceCe(int code, bool withlog, DcContent.Fate fate, string progress = null)
        {
            if (withlog)
                WriteLog(Color.Black, 25, 10001, fate.Name);

            WorkerAct.Invoker(() => lstContents.TreatItemCe(code, 5, progress, fate.Name));
        }

        //
        private void UpdateTraceInstance(int imageindex, int count, string insname)
        {
            WorkerAct.Invoker(() => lstContents.TreatItemInstance(imageindex, count, insname));
        }

        //
        private void TraceMatchInstance(DcContent.Instance instance)
        {
            WriteLog(Color.Black, 22, 10003, instance.Name);

            UpdateTraceInstance(6, 0, instance.Name);
        }

        //
        private void TraceEnterInstance(DcContent.Instance instance)
        {
            WriteLog(Color.Black, 22, 10004, instance.Name);

            UpdateTraceInstance(2, 0, instance.Name);
        }

        //
        private void TraceEntryInstance(List<int> instances)
        {
            var insnames = string.Join("/", instances.ToArray());

            WriteLog(Color.Black, 22, 10002, insnames);

            UpdateTraceInstance(7, instances.Count, insnames);
        }

        //
        private void TraceMatchRoulette(DcContent.Roulette roulette)
        {
            WriteLog(Color.Black, 22, 10003, roulette.Name);

            UpdateTraceInstance(6, 0, roulette.Name);
        }

        //
        private void TraceEntryRoulette(DcContent.Roulette roulette)
        {
            WriteLog(Color.Black, 22, 10002, roulette.Name);

            UpdateTraceInstance(1, 0, roulette.Name);
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

                if (DcConfig.DataRemoteUpdate)
                    Updater.CheckNewVersion();

                UpdateFates();
            }
        }

        private void CboPacketset_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!DcConfig.PluginEnable)
                return;

            if (cboPacketset.SelectedIndex >= _packet_list.Count)
                return;

            var p = _packet_list.ElementAt(cboPacketset.SelectedIndex);
            var n = p.Key;

            if (!string.IsNullOrWhiteSpace(n) && !n.Equals(DcConfig.Duty.PacketSet) && DcConfig.ReadPacket(n))
            {
                SaveConfig();

                if (!n.Equals(DcConfig.PacketConfig.DefaultSetNameCustom))
                {
                    // check update and save
                    RemotePacketUpdate(n);
                }
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
            _overlay.ResetAutoHide();
            _overlay.StartBlink();
        }

        private void ChkOverlayClickThru_CheckedChanged(object sender, EventArgs e)
        {
            if (!DcConfig.PluginEnable)
                return;

            DcConfig.Duty.OverlayClickThru = chkOverlayClickThru.Checked;
            _overlay.SetClickThruStatus(chkOverlayClickThru.Checked);

            SaveConfig();
        }

        private void ChkOverlayAutoHide_CheckedChanged(object sender, EventArgs e)
        {
            if (!DcConfig.PluginEnable)
                return;

            DcConfig.Duty.OverlayAutoHide = chkOverlayAutoHide.Checked;

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
                    Title = Locale.Text(101),
                    DefaultExt = "wav",
                    Filter = Locale.Text(102)
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

            string s = Locale.Text(103);

            if (DcConfig.Duty.UseNotifyLine)
                await NotifyUsingLine(s);

            if (DcConfig.Duty.UseNotifyTelegram)
                NotifyUsingTelegram(s);

            if (DcConfig.Duty.UseNotifyDiscordWebhook)
                await NotifyUsingDiscordWebhook(s);
        }

        //
        private void SendNotify(string s)
        {
            if (DcConfig.Duty.UseNotifyLine)
                NotifyUsingLine(s).Wait();

            if (DcConfig.Duty.UseNotifyTelegram)
                NotifyUsingTelegram(s);

            if (DcConfig.Duty.UseNotifyDiscordWebhook)
                NotifyUsingDiscordWebhook(s).Wait();
        }

        //
        private void NotifyFate(DcContent.Fate f)
        {
            if (!DcConfig.Duty.EnableNotify)
                return;

            string s = Locale.Text(10005, f.Name);
            SendNotify(s);
        }

        //
        private void NotifyMatch(string name)
        {
            if (!DcConfig.Duty.EnableNotify)
                return;

            string s = Locale.Text(10003, name);
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

            btnTestNotify.Enabled = true;
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

            btnTestNotify.Enabled = true;
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

            btnTestNotify.Enabled = true;
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

        private void ChkUseNotifyDiscowk_CheckedChanged(object sender, EventArgs e)
        {
            if (!DcConfig.PluginEnable)
                return;

            DcConfig.Duty.UseNotifyDiscordWebhook = chkUseNotifyDiscowk.Checked;
            txtDiscowkUrl.Enabled = chkUseNotifyDiscowk.Checked;

            btnTestNotify.Enabled = DcConfig.Duty.EnableNotify;

            SaveConfig();
        }

        private void TxtDiscowkUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (!DcConfig.PluginEnable)
                return;

            if (e.KeyCode == Keys.Enter)
            {
                if (!IsValidDiscwkUrl(txtDiscowkUrl.Text))
                {
                    Logger.E(342);
                    return;
                }

                DcConfig.Duty.NotifyDiscordWebhookUrl = txtDiscowkUrl.Text;
                SaveConfig();
            }

            btnTestNotify.Enabled = true;
        }

        private void ChkDiscowkTts_CheckedChanged(object sender, EventArgs e)
        {
            if (!DcConfig.PluginEnable)
                return;

            DcConfig.Duty.NotifyDiscordWebhookTts = chkDiscowkTts.Checked;

            btnTestNotify.Enabled = DcConfig.Duty.EnableNotify;

            SaveConfig();
        }

        private static bool IsValidDiscwkUrl(string url)
        {
            url = url.ToLower();
            return url.StartsWith("https://discord.com/api/webhooks/");
        }

        //
        internal async Task NotifyUsingDiscordWebhook(string mesg)
        {
            if (txtDiscowkUrl.TextLength == 0)
                return;

            if (!IsValidDiscwkUrl(txtDiscowkUrl.Text))
            {
                Logger.E(342);
                return;
            }

            if (!txtDiscowkUrl.Text.Equals(DcConfig.Duty.NotifyDiscordWebhookUrl))
            {
                DcConfig.Duty.NotifyDiscordWebhookUrl = txtDiscowkUrl.Text;
                SaveConfig();
            }

            var hc = new HttpClient();
            var param = new Dictionary<string, string>
            {
                { "content", mesg },
                { "tts", DcConfig.Duty.NotifyDiscordWebhookTts.ToString() },
            };

            await hc.PostAsync(DcConfig.Duty.NotifyDiscordWebhookUrl,
                new FormUrlEncodedContent(param)).ConfigureAwait(false);
        }

        //
        private void PacketFinderResetUi(bool is_enable)
        {
            if (!is_enable)
            {
                btnPacketStart.Text = Locale.Text(10007);
                btnPacketStart.BackColor = Color.Transparent;
            }
            else
            {
                btnPacketStart.Text = Locale.Text(10008);
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
            lblPacketVersion.Text = newpk.Version.ToString();
            txtPacketDescription.Text = newpk.Description;
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

            // Zone
            lstPacketInfo.Items[4].SubItems[1].Text = DcConfig.Packet.OpZone.ToString();
            lstPacketInfo.Items[4].SubItems[2].Text = "";
            lstPacketInfo.Items[4].SubItems[3].Text = newpk.OpZone.ToString();

            // Bozja
            lstPacketInfo.Items[5].SubItems[1].Text = DcConfig.Packet.OpCe.ToString();
            lstPacketInfo.Items[5].SubItems[2].Text = "";
            lstPacketInfo.Items[5].SubItems[3].Text = newpk.OpCe.ToString();
        }

        private void BtnPacketStart_Click(object sender, EventArgs e)
        {
            if (!_is_packet_finder)
            {
                _new_packet = new DcConfig.PacketConfig(DateTime.Now);
                PacketFindClearUi(_new_packet);
            }

            _is_packet_finder = !_is_packet_finder;
            PacketFinderResetUi(_is_packet_finder);
        }

        private void BtnPacketApply_Click(object sender, EventArgs e)
        {
            var ret = (DialogResult)WorkerAct.Invoker(new WorkerAct.ObjectReturnerDelegate(() =>
            {
                var r = MessageBox.Show(Locale.Text(10022), Locale.Text(0), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                return r;
            }));

            if (ret == DialogResult.Yes)
            {
                var newfilename = DcConfig.BuildPacketFileName(DcConfig.PacketConfig.DefaultSetNameCustom);

                _new_packet.Description = txtPacketDescription.Text;
                _new_packet.Save(newfilename);

                _is_packet_finder = false;
                PacketFinderResetUi(false);

                // select custom
                DcConfig.Duty.PacketSet = DcConfig.PacketConfig.DefaultSetNameCustom;
                RefreshPacketList();
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
                case 4: m = 10026; break;
                case 5: m = 10014; break;
                default: m = 10015; break;
            }

            txtPacketInfo.Text = Locale.Text(m);
        }

        private void LstPacketInfo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstPacketInfo.SelectedIndices.Count != 1)
                return;

            var v = int.MaxValue;

            switch (lstPacketInfo.SelectedIndices[0])
            {
                case 0:
                    v = _new_packet.OpFate = DcConfig.Packet.OpFate;
                    break;

                case 1:
                    v = _new_packet.OpDuty = DcConfig.Packet.OpDuty;
                    break;

                case 2:
                    v = _new_packet.OpMatch = DcConfig.Packet.OpMatch;
                    break;

                case 3:
                    v = _new_packet.OpInstance = DcConfig.Packet.OpInstance;
                    break;

                case 4:
                    v = _new_packet.OpZone = DcConfig.Packet.OpZone;
                    break;

                case 5:
                    v = _new_packet.OpCe = DcConfig.Packet.OpCe;
                    break;
            }

            if (v != int.MaxValue)
            {
                lstPacketInfo.SelectedItems[0].SubItems[2].Text = Locale.Text(10024);
                lstPacketInfo.SelectedItems[0].SubItems[3].Text = v.ToString();
            }
        }

        private void LstBozjaInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstBozjaInfo.SelectedItems.Count != 1)
                return;

            ushort opcode = (ushort)lstBozjaInfo.SelectedItems[0].Tag;

            _new_packet.OpCe = opcode;

            lstPacketInfo.Items[5].SubItems[2].Text = Locale.Text(10023);
            lstPacketInfo.Items[5].SubItems[3].Text = _new_packet.OpCe.ToString();
        }

        private void LstBozjaInfo_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        // middle la noscea
        private static readonly ushort[] _fates_middle_la_noscea =
        {
            553, 649, 687, 688, 693, 717,
            220, 221, 222, 223, 225, 226, 227, 229, 231, 233, 235, 237, 238, 239, 240,
            1387,
        };

        // southern bozja front
        private static readonly ushort[] _fates_southern_bojza =
        {
            1597, 1598, 1599,
            1600, 1601, 1602, 1603, 1604, 1605, 1606, 1607, 1608, 1609,
            1610, 1611, 1612, 1613, 1614, 1615, 1616, 1617, 1618, 1619,
            1620, 1621, 1622, 1623, 1624, 1625, 1626, 1627, 1628,
        };

        // zadnor
        private static readonly ushort[] _fates_zadnor =
        {
            1717, 1718, 1719, 1720, 1721, 1722, 1723, 1724,
            1725, 1726, 1727, 1728, 1729, 1730, 1731, 1732,
            1733, 1734, 1735, 1736, 1737, 1738, 1739, 1740, 1741, 1742,
        };

        //
        private bool IsFateInFindList(ushort code)
        {
            return
                _fates_middle_la_noscea.Contains(code) ||
                _fates_southern_bojza.Contains(code) ||
                _fates_zadnor.Contains(code);
        }

        //
        private bool IsSkirmishFate(ushort code)
        {
            return
                _fates_southern_bojza.Contains(code) ||
                _fates_zadnor.Contains(code);
        }

        //
        private bool IsFateForSouthernBozja(ushort code)
        {
            return _fates_southern_bojza.Contains(code);
        }

        //
        private bool IsFateForZadnor(ushort code)
        {
            return _fates_zadnor.Contains(code);
        }

#if TESTPK
        private static string DataToByteString(byte[] be)
        {
            const string HexAlphabet = "0123456789ABCDEF";
            StringBuilder sb = new StringBuilder();

            foreach (var b in be)
                sb.Append(HexAlphabet[(int)(b >> 4)]).Append(HexAlphabet[(int)(b & 0xF)]).Append(' ');

            return sb.ToString();
        }

        private static string DataToUshortString(byte[] data, int index)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = index; i < data.Length; i += 2)
            {
                sb.Append(BitConverter.ToInt16(data, i));
                sb.Append(' ');
            }

            return sb.ToString();
        }

        private static int IndexOfData(byte[] data, int index, ushort value)
        {
            int len = data.Length;

            for (int i = index; i < len; i++)
            {
                if (i + 1 >= len)
                    break;

                ushort n = BitConverter.ToUInt16(data, i);
                if (value == n)
                    return i;
            }

            return -1;
        }

        private static int IndexOfData(byte[] data, int index, ushort[] values)
        {
            int len = data.Length;

            for (int i = index; i < len; i++)
            {
                if (i + 1 >= len)
                    break;

                ushort n = BitConverter.ToUInt16(data, i);
                for (int u = 0; u < values.Length; u++)
                {
                    if (values[u] == n)
                        return i;
                }
            }

            return -1;
        }

        private static (int Pos, int Value) IndexOfValues(byte[] data, int index, params ushort[] values)
        {
            int len = data.Length;

            for (int i = index; i < len; i++)
            {
                if (i + 1 >= len)
                    break;

                ushort n = BitConverter.ToUInt16(data, i);
                for (int u = 0; u < values.Length; u++)
                {
                    if (values[u] == n)
                        return (i, n);
                }
            }

            return (-1, 0);
        }
#endif

        //
        private void PacketFinderHandler(byte[] message)
        {
            var opcode = BitConverter.ToUInt16(message, 18);
            var data = message.Skip(32).ToArray();

#if TESTPK
#if false
			// 파이날 스텝으로 오는거 전부 얻기
			var t = IndexOfData(data, 0, new ushort[] { 169, 134, 183, 223, 637 }); // final, P1, S1, Z1, Snake
			if (t > 0)
			{
				var s = DataToByteString(data);
				Logger.L("{0}({1},{2}) => {3}", opcode, data.Length, t, s);
			}
			else 
#endif
#if false
			// 매칭 관련
			if (opcode == 368)
			{
				var s = DataToByteString(data);
				Logger.L("{0}({1}) => {2}", opcode, data.Length, s);
			}
#endif
#if true
            // 페이트 
            (int t, int v) = IndexOfValues(data, 0,
                // middle la noscea
                553, 649, 687, 688, 693, 717, 220, 221, 222, 223, 225,
                226, 227, 229, 231, 233, 235, 237, 238, 239, 240, 1387,
                1597, 1598, 1599,
                // southern bozja
                1600, 1601, 1602, 1603, 1604, 1605, 1606, 1607, 1608, 1609,
                1610, 1611, 1612, 1613, 1614, 1615, 1616, 1617, 1618, 1619,
                1620, 1621, 1622, 1623, 1624, 1625, 1626, 1627, 1628);
            if (opcode == 623 && t > 0 && data[0] != 0x3C)
            {
                var s = DataToByteString(data);
                Logger.L("페이트: {0}({1}:{1:X})({2},{3}) => {4}", opcode, v, data.Length, t, s);
            }
#endif
#endif

            // fate & duty & packet
            if (DcConfig.Duty.PacketForLocal)
            {
                // for ACTOZ/Korean service

                // fate
                if (_new_packet.OpFate == 0 && data.Length > 4 && data[0] == 0x3E)
                {
                    var cc = BitConverter.ToUInt16(data, 4);
                    if (IsFateInFindList(cc) && _new_packet.OpFate != opcode)
                    {
                        _new_packet.OpFate = opcode;

                        WorkerAct.Invoker(() =>
                        {
                            lstPacketInfo.Items[0].SubItems[2].Text = Locale.Text(10016);
                            lstPacketInfo.Items[0].SubItems[3].Text = _new_packet.OpFate.ToString();
                        });

                        _last_fate = cc;

                        return;
                    }
                }

                // duty
                if (_new_packet.OpDuty == 0 && data.Length > 12)
                {
                    var rcode = data[8];
                    if (rcode == 0)
                    {
                        // The Final Steps of Faith (169)
                        short m = BitConverter.ToInt16(data, 12);
                        if (m == 169 && _new_packet.OpDuty != opcode)
                        {
                            _new_packet.OpDuty = opcode;

                            WorkerAct.Invoker(() =>
                            {
                                lstPacketInfo.Items[1].SubItems[2].Text = Locale.Text(10016);
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
                        // The Final Steps of Faith (169)
                        short m = BitConverter.ToInt16(data, 20);
                        if (m == 169 && _new_packet.OpMatch != opcode)
                        {
                            _new_packet.OpMatch = opcode;

                            WorkerAct.Invoker(() =>
                            {
                                lstPacketInfo.Items[2].SubItems[2].Text = Locale.Text(10016);
                                lstPacketInfo.Items[2].SubItems[3].Text = _new_packet.OpMatch.ToString();
                            });

                            return;
                        }
                    }
                }
            }
            else
            {
                // for common

                // fate
                if (_new_packet.OpFate == 0 && data.Length > 20 && data[0] == 0x3C) // real size is 32
                {
                    var cc = BitConverter.ToUInt16(data, 4);
                    if (IsFateInFindList(cc) && _new_packet.OpFate != opcode)
                    {
                        _new_packet.OpFate = opcode;

                        WorkerAct.Invoker(() =>
                        {
                            lstPacketInfo.Items[0].SubItems[2].Text = Locale.Text(10016);
                            lstPacketInfo.Items[0].SubItems[3].Text = _new_packet.OpFate.ToString();
                        });

                        _last_fate = cc;

                        return;
                    }
                }

                // duty
                if (_new_packet.OpDuty == 0 && data.Length > 20)    // real size is 40
                {
                    var rcode = data[16];
                    var scode = data[19];
                    if (rcode == 0 && scode == 0)
                    {
                        // The Final Steps of Faith (169)
                        short m = BitConverter.ToInt16(data, 20);
                        if (m == 169 && _new_packet.OpDuty != opcode)
                        {
                            _new_packet.OpDuty = opcode;

                            WorkerAct.Invoker(() =>
                            {
                                lstPacketInfo.Items[1].SubItems[2].Text = Locale.Text(10016);
                                lstPacketInfo.Items[1].SubItems[3].Text = _new_packet.OpDuty.ToString();
                            });

                            return;
                        }
                    }
                }

                // match
                if (_new_packet.OpMatch == 0 && data.Length > 20)   // real size is 40
                {
                    var rcode = data[2];
                    if (rcode == 0)
                    {
                        // The Final Steps of Faith (169)
                        short m = BitConverter.ToInt16(data, 28);
                        if (m == 169 && _new_packet.OpMatch != opcode)
                        {
                            _new_packet.OpMatch = opcode;

                            WorkerAct.Invoker(() =>
                            {
                                lstPacketInfo.Items[2].SubItems[2].Text = Locale.Text(10016);
                                lstPacketInfo.Items[2].SubItems[3].Text = _new_packet.OpMatch.ToString();
                            });

                            return;
                        }
                    }
                }
            }

            // instance
            if (_new_packet.OpInstance == 0 && data.Length >= 16)
            {
                // The Final Steps of Faith (169)
                short m = BitConverter.ToInt16(data, 0);
                short u = BitConverter.ToInt16(data, 2);
                if (m == 169 && u == 0 && _new_packet.OpInstance != opcode)
                {
                    _new_packet.OpInstance = opcode;

                    WorkerAct.Invoker(() =>
                    {
                        lstPacketInfo.Items[3].SubItems[2].Text = Locale.Text(10016);
                        lstPacketInfo.Items[3].SubItems[3].Text = _new_packet.OpInstance.ToString();
                    });

                    return;
                }
            }

            // zone
            if (_new_packet.OpZone == 0 && data.Length == 16)
            {
                // Middle La Noscea (134)
                var h = BitConverter.ToUInt32(data, 0);
                var z = BitConverter.ToUInt16(data, 4);
                if (h == 0 && z == 134)
                {
                    _new_packet.OpZone = opcode;

                    WorkerAct.Invoker(() =>
                    {
                        lstPacketInfo.Items[4].SubItems[2].Text = Locale.Text(10016);
                        lstPacketInfo.Items[4].SubItems[3].Text = _new_packet.OpZone.ToString();
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

                var ok = false;
                var mem = data[9];
                var stat = data[10];
                var prg = data[12];

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
                    if (_stq_type == DcContent.SaveTheQueenType.No)
                    {
                        if (IsFateForSouthernBozja(_last_fate))
                            _stq_type = DcContent.SaveTheQueenType.Bozja;
                        else if (IsFateForZadnor(_last_fate))
                            _stq_type = DcContent.SaveTheQueenType.Zadnor;
                        else
                            _stq_type = DcContent.SaveTheQueenType.Zadnor;
                    }

                    var ce = DcContent.GetFate(code + DcContent.SaveTheQueenTypeToCeBase(_stq_type));

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

        private void RemotePacketUpdate(string name)
        {
            // need to resign using thread -> blocked by network troubles
            if (!DcConfig.DataRemoteUpdate)
                return;

            var ns = Updater.CheckNewPacket(name);

            if (!string.IsNullOrWhiteSpace(ns))
            {
                var pk = DcConfig.PacketConfig.ParseString(ns);

                if (pk.Version > DcConfig.Packet.Version)
                {
                    DcConfig.Packet.Version = pk.Version;
                    DcConfig.Packet.Description = pk.Description;
                    DcConfig.Packet.OpFate = pk.OpFate;
                    DcConfig.Packet.OpDuty = pk.OpDuty;
                    DcConfig.Packet.OpMatch = pk.OpMatch;
                    DcConfig.Packet.OpInstance = pk.OpInstance;
                    DcConfig.Packet.OpZone = pk.OpZone;
                    DcConfig.Packet.OpCe = pk.OpCe;

                    var nfn = DcConfig.BuildPacketFileName(name);
                    pk.Save(nfn);

                    Logger.I(33, pk.Version, pk.Description);

                    //
                    _packet_list.Remove(name);
                    _packet_list.Add(name, pk.Description);

                    RefreshPacketList(false);
                }
            }
        }

        private void RefreshDatasetList()
        {
            // quick description read?

            cboDataset.Items.Clear();

            DirectoryInfo di = new DirectoryInfo(DcConfig.DataPath);

            foreach (var fi in di.GetFiles("DcDuty-*.json"))
            {
                var s = fi.Name.Substring(7, fi.Name.Length - 7 - 5);
                var n = cboDataset.Items.Add(s);

                if (s.Equals(DcConfig.Duty.Language))
                    cboDataset.SelectedIndex = n;
            }
        }

        private void RefreshPacketList(bool reload_file_info = true)
        {
            if (reload_file_info)
            {
                _packet_list.Clear();

                DirectoryInfo di = new DirectoryInfo(DcConfig.DataPath);

                foreach (var fi in di.GetFiles("DcPacket-*.config"))
                {
                    var name = fi.Name.Substring(9, fi.Name.Length - 9 - 7);

                    var db = new ThirdParty.LineDb(fi.FullName, Encoding.UTF8, false);
                    var desc = db.Get("Description", null);

                    if (desc == null)
                    {
                        // config file was for below version 9
                        _packet_list.Add(name, name);
                    }
                    else
                    {
                        _packet_list.Add(name, desc);
                    }
                }
            }

            cboPacketset.Items.Clear();

            foreach (var i in _packet_list)
            {
                var n = cboPacketset.Items.Add(i.Value);

                if (i.Key.Equals(DcConfig.Duty.PacketSet))
                    cboPacketset.SelectedIndex = n;
            }
        }

        private void BtnResetContentList_Click(object sender, EventArgs e)
        {
            lstContents.ResetContentItems();
        }

        public void ResetContentItems()
        {
            WorkerAct.Invoker(() => lstContents.ResetContentItems());
        }
    }
}
