using System;
using System.Drawing;
using System.Windows.Forms;

namespace DutyContent.Tab
{
	public partial class LogForm : Form, Interface.ISuppLocale, Interface.ISuppActPlugin
	{
		private static LogForm _self;
		public static LogForm Self => _self;

		public LogForm()
		{
			_self = this;

			InitializeComponent();
		}

		public void PluginDeinitialize()
		{

		}

		public void PluginInitialize()
		{
			chkLogScroll.Checked = true;
		}

		public void RefreshLocale()
		{

		}

		public void UpdateUiLocale()
		{
			btnLogCopy.Text = Locale.Text(501);
			btnLogClear.Text = Locale.Text(502);
			chkLogScroll.Text = Locale.Text(503);

			var logfont = new Font(DcConfig.Duty.LogFontFamily, DcConfig.Duty.LogFontSize, FontStyle.Regular);
			txtLogText.Font = logfont;
		}

		private void BtnLogCopy_Click(object sender, EventArgs e)
		{
			txtLogText.Copy();
		}

		private void BtnLogClear_Click(object sender, EventArgs e)
		{
			txtLogText.Clear();
		}

		private void InvokeLog(Color color, string mesg)
		{
			WorkerAct.Invoker(() =>
			{
				txtLogText.SelectionColor = color;
				txtLogText.SelectionStart = txtLogText.TextLength;
				txtLogText.SelectionLength = 0;
				txtLogText.AppendText(mesg);

				txtLogText.SelectionColor = txtLogText.ForeColor;

				if (chkLogScroll.Checked)
					ThirdParty.NativeMethods.ScrollToBottom(txtLogText);
			});
		}

		//
		public void WriteLog(Color color, string mesg, bool with_time = true)
		{
			if (txtLogText.IsDisposed || string.IsNullOrEmpty(mesg))
				return;

			string line;

			if (!with_time)
				line = mesg + Environment.NewLine;
			else
			{
				var dt = DateTime.Now.ToString("HH:mm:ss");
				line = $"[{dt}] {mesg}{Environment.NewLine}";
			}

			InvokeLog(color, line);
		}

		//
		public void WriteLogSection(Color color, string section, string mesg, bool with_time = true)
		{
			if (txtLogText.IsDisposed || string.IsNullOrEmpty(mesg))
				return;

			string line;

			if (!with_time)
				line = $"[{section}] {mesg}{Environment.NewLine}";
			else
			{
				var dt = DateTime.Now.ToString("HH:mm:ss");
				line = $"[{dt}/{section}] {mesg}{Environment.NewLine}";
			}

			InvokeLog(color, line);
		}

		//
		public Font LogFont
		{
			get { return txtLogText.Font; }
			set { txtLogText.Font = value; }
		}
	}
}
