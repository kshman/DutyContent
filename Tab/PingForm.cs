using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net.NetworkInformation;
using System.Net;

namespace DutyContent.Tab
{
	public partial class PingForm : Form, Interface.ISuppLocale, Interface.ISuppActPlugin
	{
		private static PingForm _self;
		public static PingForm Self => _self;

		//
		private System.Timers.Timer _timer;
		private long _last_ping;

		private Color _color = Color.Transparent;

		private Libre.PingGrapher _grpr;
		private List<int> _kepts = new List<int> { 0, 0 };

		//
		private const int PingInterval = 5000;
		private const int PingAmount = 5;
		private const int PingTimeout = PingInterval / (PingAmount + 1);

		//
		public PingForm()
		{
			_self = this;

			InitializeComponent();

			_grpr = new Libre.PingGrapher(pbxPingGraph);
		}

		public void PluginInitialize()
		{
			chkUsePing.Checked = DcConfig.Duty.UsePing;
			btnPingColor1.BackColor = DcConfig.Duty.PingColors[0];
			btnPingColor2.BackColor = DcConfig.Duty.PingColors[1];
			btnPingColor3.BackColor = DcConfig.Duty.PingColors[2];
			btnPingColor4.BackColor = DcConfig.Duty.PingColors[3];
			chkPingGraph.Checked = DcConfig.Duty.PingGraph;
			cboPingGraphType.SelectedIndex = DcConfig.Duty.PingGraphType;

			//
			try
			{
				var svl = File.ReadAllLines(Path.Combine(DcConfig.DataPath, "ServerList.txt"));
				int ssv = -1;

				for (var i = 0; i < svl.Length; i++)
				{
					cboPingDefAddr.Items.Add(svl[i]);

					if (svl[i].StartsWith(DcConfig.Duty.PingDefAddr))
						ssv = i;
				}

				if (string.IsNullOrEmpty(DcConfig.Duty.PingDefAddr))
					ssv = -1;

				cboPingDefAddr.SelectedIndex = ssv > 0 ? ssv : 0;
			}
			catch
			{
				cboPingDefAddr.Items.Clear();
				cboPingDefAddr.Items.Add(Locale.Text(27));
				cboPingDefAddr.SelectedIndex = 0;
			}

			//
			_timer = new System.Timers.Timer() { Interval = PingInterval };
			_timer.Elapsed += (sender, e) => PingOnce();

			if (DcConfig.Duty.UsePing)
			{
				PingOnce(false);
				_timer.Start();
			}

		}

		public void PluginDeinitialize()
		{
			_timer?.Stop();
		}

		public void RefreshLocale()
		{

		}

		public void UpdateUiLocale()
		{
			chkUsePing.Text = Locale.Text(401);
			lblPingColors.Text = Locale.Text(402);
			lblPingStat1.Text = Locale.Text(403);
			lblPingStat2.Text = Locale.Text(404);
			lblPingStat3.Text = Locale.Text(405);
			lblPingStat4.Text = Locale.Text(406);
			chkPingGraph.Text = Locale.Text(407);
			lblPingDefAddr.Text = Locale.Text(408);
			lblPingAddress.Text = Locale.Text(409);
			lblPingGraphType.Text = Locale.Text(410);
		}

		private void SaveConfig(int interval = 5000)
		{
			DcControl.Self.RefreshSaveConfig(interval);
		}

		private void ChkUsePing_CheckedChanged(object sender, EventArgs e)
		{
			if (!DcConfig.PluginEnable)
				return;

			DcConfig.Duty.UsePing = chkUsePing.Checked;

			SaveConfig();

			if (chkUsePing.Checked)
			{
				PingOnce();
				_timer.Start();
			}
			else
			{
				_timer.Stop();
				Overlay.DutyOvForm.Self?.ResetStat();
			}
		}

		private void PingColorWorker(int index, Button button)
		{
			Color color = (Color)WorkerAct.Invoker(new WorkerAct.ObjectReturnerDelegate(() =>
			{
				var dg = new ColorDialog()
				{
					AnyColor = true,
					Color = DcConfig.Duty.PingColors[index],
				};

				return dg.ShowDialog() == DialogResult.OK ? dg.Color : DcConfig.Duty.PingColors[index];
			}));

			if (DcConfig.Duty.PingColors[index] != color)
			{
				button.BackColor = color;
				DcConfig.Duty.PingColors[index] = color;
				SaveConfig();
			}
		}

		private void BtnPingColor1_Click(object sender, EventArgs e)
		{
			PingColorWorker(0, btnPingColor1);
		}

		private void BtnPingColor2_Click(object sender, EventArgs e)
		{
			PingColorWorker(1, btnPingColor2);
		}

		private void BtnPingColor3_Click(object sender, EventArgs e)
		{
			PingColorWorker(2, btnPingColor3);
		}

		private void BtnPingColor4_Click(object sender, EventArgs e)
		{
			PingColorWorker(3, btnPingColor4);
		}

		private void ChkPingGraph_CheckedChanged(object sender, EventArgs e)
		{
			if (!DcConfig.PluginEnable)
				return;

			DcConfig.Duty.PingGraph = chkPingGraph.Checked;

			SaveConfig();
		}

		private void CboPingDefAddr_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!DcConfig.PluginEnable)
				return;

			var val = cboPingDefAddr.SelectedItem as string;

			if (!string.IsNullOrEmpty(val))
			{
				var ss = val.Split(' ');
				if (ss.Length > 0)
				{
					DcConfig.Duty.PingDefAddr = ss[0].Trim();

					SaveConfig();

					return;
				}
			}

			DcConfig.Duty.PingDefAddr = string.Empty;

			SaveConfig();
		}

		private void CboPingGraphType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!DcConfig.PluginEnable)
				return;

			if (cboPingGraphType.SelectedIndex>=0)
			{
				DcConfig.Duty.PingGraphType = cboPingGraphType.SelectedIndex;
				SaveConfig();
			}
		}

		private void LstPingAddress_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (lstPingAddress.SelectedIndices.Count != 1)
				return;

			try
			{
				var s = lstPingAddress.SelectedItem as string;
				Clipboard.SetText(s);
			}
			catch (Exception ex)
			{
				Logger.Ex(ex, 35);
			}
		}

		//
		private void PingOnce(bool check_plugin_enable = true)
		{
			if (!DcConfig.Duty.UsePing)
				return;

			if (check_plugin_enable && !DcConfig.PluginEnable)
				return;

			var conns = DcConfig.Connections.CopyConnection();
			long rtt = 0;
			double loss = 0;

			List<string> addrs = new List<string>();

			if (conns.Length > 0)
			{
				foreach (var row in conns)
				{
					addrs.Add(row.RemoteAddress.ToString());

					var (r, l) = CalcPing(row.RemoteAddress, PingTimeout, PingAmount);

					if (rtt < r)
						rtt = r;

					if (loss < l)
						loss = l;
				}
			}
			else
			{
				if (string.IsNullOrEmpty(DcConfig.Duty.PingDefAddr))
				{
					Overlay.DutyOvForm.Self?.ResetStat();
					return;
				}

				addrs.Add(DcConfig.Duty.PingDefAddr);

				var defip = ThirdParty.Converter.ToIPAddressFromIPV4(DcConfig.Duty.PingDefAddr);

				if (defip == IPAddress.None || defip == IPAddress.IPv6None)
				{
					Overlay.DutyOvForm.Self?.ResetStat();
					return;
				}

				var (r, l) = CalcPing(defip, PingTimeout, PingAmount);

				if (rtt < r)
					rtt = r;

				if (loss < l)
					loss = l;
			}

			//MesgLog.L("Ping: {0}, {1}%", rtt, loss);

			Color color;
			if (loss > 0.0 || rtt > 150)
				color = DcConfig.Duty.PingColors[3];
			else if (rtt > 100)
				color = DcConfig.Duty.PingColors[2];
			else if (rtt > 50)
				color = DcConfig.Duty.PingColors[1];
			else
				color = DcConfig.Duty.PingColors[0];

			if (_last_ping != rtt || loss > 0.0 || _color != color)
			{
				_last_ping = rtt;
				_color = color;

				Overlay.DutyOvForm.Self?.SetStatPing(color, rtt, loss);
			}

			//
			if (DcConfig.Duty.PingGraph)
			{
				_kepts.Add((int)rtt);
				if (_kepts.Count > 120)
					_kepts.RemoveAt(0);

				_grpr.Enter();
				_grpr.DrawValues(_kepts, (Libre.PingGrapher.DrawType)DcConfig.Duty.PingGraphType);
				WorkerAct.Invoker(() => _grpr.Leave());
			}

			// 
			if (addrs.Count == 0)
			{
				if (lstPingAddress.Items.Count != 0)
					lstPingAddress.Items.Clear();
			}
			else
			{
				bool have_to_update_addrs = false;

				if (lstPingAddress.Items.Count == 0)
					have_to_update_addrs = true;
				else
				{
					var i = lstPingAddress.Items[0] as string;
					have_to_update_addrs = !addrs.Contains(i);
				}

				if (have_to_update_addrs)
				{
					WorkerAct.Invoker(() =>
					{
						lstPingAddress.BeginUpdate();
						lstPingAddress.Items.Clear();
						foreach (var i in addrs)
							lstPingAddress.Items.Add(i);
						lstPingAddress.EndUpdate();
					});
				}
			}
		}

		// http://forum.codecall.net/topic/37643-c-packet-lossping-program/

		private static readonly PingOptions _ping_options = new PingOptions { DontFragment = true };
		private static readonly byte[] _ping_buffers = Encoding.ASCII.GetBytes("01234567890123456789012345678901");

		//
		private (long Rtt, double Loss) CalcPing(IPAddress host, int timeout, int amount)
		{
			var ps = new Ping();

			int failed = 0;
			long rtt = 0;

			for (var i = 0; i < amount; i++)
			{
				try
				{
					PingReply pr = ps.Send(host, timeout, _ping_buffers, _ping_options);

					if (pr.Status != IPStatus.Success)
					{
						failed++;

						if (DcConfig.DebugEnable)
						{
							Logger.WriteCategory(Color.Red, "Ping", "failed. status: {0} / duration: {1} / at: {2}/{3}", pr.Status, timeout, i + 1, amount);
						}
					}

					if (rtt < pr.RoundtripTime)
						rtt = pr.RoundtripTime;
				}
				catch
				{
					failed++;
				}

				System.Threading.Thread.Sleep(1);
			}

			double loss = failed == 0 ? 0 : (double)failed / amount * 100.0;

			return (rtt, loss);
		}
	}
}
