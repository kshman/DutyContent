using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace DutyContent.Overlay
{
	public partial class DutyOvForm : Form
	{
		private static DutyOvForm _self;
		public static DutyOvForm Self => _self;

		private const int BlinkElapse = 300;
		private const int BlinkTotalCount = 20;

		private static readonly Color ColorFate = Color.DarkOrange;
		private static readonly Color ColorMatch = Color.Red;
		private static readonly Color ColorNone = Color.Black;
		private static readonly Color ColorHide = Color.Transparent;

		//
		private Timer _blink_timer;
		private int _blink_count;
		private Color _accent;

		private Timer _hide_timer;

		//
		public DutyOvForm()
		{
			_self = this;

			InitializeComponent();

			Location = DcConfig.Duty.OverlayLocation;

			_blink_timer = new Timer { Interval = BlinkElapse };
			_blink_timer.Tick += (sender, e) =>
			{
				if (++_blink_count > BlinkTotalCount)
				{
					StopBlink();
				}
				else
				{
					//BackColor = (BackColor == ColorNone) ? _accent : ColorNone;
					BackColor = (BackColor == _accent) ? ColorNone : _accent;
				}
			};

			_hide_timer = new Timer { Interval = DcConfig.Duty.OverlayAutoElapse };
			_hide_timer.Tick += (sender, e) =>
			{
				Visible = false;
			};
		}

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= 0x80/*WS_EX_TOOLWINDOW*/ | 0x80000/*WS_EX_LAYERED*/;
				return cp;
			}
		}

		private void DutyOvForm_Load(object sender, EventArgs e)
		{

		}

		private void DoMoveDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				ThirdParty.NativeMethods.ReleaseCapture();
				ThirdParty.NativeMethods.SendMessage(Handle, 0xA1/*WM_NCLBUTTONDOWN*/, new IntPtr(0x2/*HT_CAPTION*/), IntPtr.Zero);
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			DoMoveDown(e);
		}

		protected override void OnLocationChanged(EventArgs e)
		{
			base.OnLocationChanged(e);
			DcConfig.Duty.OverlayLocation = Location;
		}

		private void LblText_MouseDown(object sender, MouseEventArgs e)
		{
			DoMoveDown(e);
		}

		public void SetClickThruStatus(bool is_click_thru)
		{
			long style = (long)ThirdParty.NativeMethods.GetWindowLong(Handle, -20);
			long value = is_click_thru ? (style | 0x80000 | 0x20) : (0x80000 | 0x0);
			ThirdParty.NativeMethods.SetWindowLong(Handle, -20, (IntPtr)value);
		}

		public void SetText(string text, bool hidenow = false)
		{
			lblText.Text = text;

			if (DcConfig.Duty.OverlayAutoHide)
			{
				if (hidenow)
					Visible = false;
				else
				{
					Visible = true;
					_hide_timer.Enabled = false;
					_hide_timer.Interval = DcConfig.Duty.OverlayAutoElapse;
					_hide_timer.Start();
				}
			}
		}

		public void ResetAutoHide()
		{
			if (DcConfig.Duty.OverlayAutoHide)
			{
				Visible = true;
				_hide_timer.Enabled = false;
				_hide_timer.Interval = DcConfig.Duty.OverlayAutoElapse;
				_hide_timer.Start();
			}
		}

		public void StartBlink()
		{
			_blink_count = 0;
			_blink_timer.Enabled = false;
			_blink_timer.Start();
		}

		public void StopBlink()
		{
			_blink_timer.Stop();

			BackColor = ColorNone;

			_accent = ColorNone;

			if (!DcConfig.Duty.OverlayAutoHide)
				lblText.Text = string.Empty;
		}

		public void PlayNone()
		{
			Invoke((MethodInvoker)(() =>
			{
				_accent = ColorNone;
				SetText(string.Empty, true);
				StopBlink();
			}));
		}

		public void PlayFate(DcContent.Fate f)
		{
			Invoke((MethodInvoker)(() =>
			{
				_accent = ColorFate;
				SetText(f.Name);
				StartBlink();
			}));
		}

		public void PlayQueue(string name)
		{
			Invoke((MethodInvoker)(() =>
			{
				_accent = ColorNone;
				SetText(name);
			}));
		}

		public void PlayMatch(string name, bool blink = true)   // PlayEnter
		{
			Invoke((MethodInvoker)(() =>
			{
				_accent = ColorMatch;
				SetText(name);
				if (blink)
					StartBlink();
			}));
		}

		public void ResetStat()
		{
			lblStat.BackColor = Color.Transparent;
			SetText(string.Empty, true);
		}

		public void SetStatPing(Color color, long rtt, double loss)
		{
			Invoke((MethodInvoker)(() =>
			{
				if (rtt > 999)
					rtt = 999;

				lblStat.Text = (Math.Abs(loss) < 0.0001)
					? string.Format("{0}", rtt)
					: string.Format("{0}{1}{2:0.#}%", rtt, Environment.NewLine, loss);
				lblStat.BackColor = color;
			}));
		}
	}
}
