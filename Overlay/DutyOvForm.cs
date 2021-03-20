using System;
using System.Drawing;
using System.Windows.Forms;

namespace DutyContent.Overlay
{
	public partial class DutyOvForm : Form
	{
		private const int BlinkTime = 300;
		private const int BlinkCount = 20;

		private static readonly Color ColorFate = Color.DarkOrange;
		private static readonly Color ColorMatch = Color.Red;
		private static readonly Color ColorNone = Color.Black;

		//
		private Timer _timer;
		private int _blink;
		private Color _accent;

		//
		public DutyOvForm()
		{
			InitializeComponent();

			Location = DcConfig.Duty.OverlayLocation;

			_timer = new Timer { Interval = BlinkTime };
			_timer.Tick += (sender, e) =>
			{
				if (++_blink > BlinkCount)
				{
					StopBlink();
				}
				else
				{
					BackColor = (BackColor == ColorNone) ? _accent : ColorNone;
				}
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
			if (DcConfig.Duty.OverlayClickThru)
				return;

			if (e.Button == MouseButtons.Left)
			{
				ThirdParty.NativeMethods.ReleaseCapture();
				ThirdParty.NativeMethods.SendMessage(Handle, 0xA1/*WM_NCLBUTTONDOWN*/, new IntPtr(0x2/*HT_CAPTION*/), IntPtr.Zero);
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (DcConfig.Duty.OverlayClickThru)
				return;

			base.OnMouseDown(e);
			DoMoveDown(e);
		}

		protected override void OnLocationChanged(EventArgs e)
		{
			base.OnLocationChanged(e);
			DcConfig.Duty.OverlayLocation = Location;
		}

		private void lblText_MouseDown(object sender, MouseEventArgs e)
		{
			DoMoveDown(e);
		}

		public void SetText(string text)
		{
			lblText.Text = text;
		}

		public void StartBlink()
		{
			_blink = 0;
			_timer.Enabled = false;
			_timer.Start();
		}

		public void StopBlink()
		{
			_timer.Stop();

			BackColor = ColorNone;

			_accent = ColorNone;
			lblText.Text = string.Empty;
		}

		public void PlayNone()
		{
			Invoke((MethodInvoker)(() =>
			{
				_accent = ColorNone;
				lblText.Text = string.Empty;
				StopBlink();
			}));
		}

		public void PlayFate(DcContent.Fate f)
		{
			Invoke((MethodInvoker)(() =>
			{
				_accent = ColorFate;
				lblText.Text = f.Name;
				StartBlink();
			}));
		}

		public void PlayQueue(string name)
		{
			Invoke((MethodInvoker)(() =>
			{
				_accent = ColorNone;
				lblText.Text = name;
			}));
		}

		public void PlayMatch(string name, bool blink = true)   // PlayEnter
		{
			Invoke((MethodInvoker)(() =>
			{
				_accent = ColorMatch;
				lblText.Text = name;
				if (blink)
					StartBlink();
			}));
		}

		public void ResetStat()
		{
			lblStat.BackColor = Color.Transparent;
			lblStat.Text = string.Empty;
		}

		public void SetStatPing(Color color, long rtt, double loss)
		{
			Invoke((MethodInvoker)(() =>
			{
				if (rtt > 999)
					rtt = 999;

				lblStat.Text = string.Format("{0}{1}{2:0.#}%", rtt, Environment.NewLine, loss);
				lblStat.BackColor = color;
				//lblStat.ForeColor = Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B);
			}));
		}
	}
}
