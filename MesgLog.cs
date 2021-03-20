using System;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DutyContent
{
	class MesgLog
	{
		private static ThirdParty.LineDb _lines;
		private static RichTextBox _logs;

		private static readonly Regex ExceptionPattern = new Regex(@"\{(.+?)\}");

		public static string Language { get; private set; }

		public static void Initialize(string text)
		{
			_lines = new ThirdParty.LineDb(text, true);
		}

		public static bool LoadFile(string filename, Encoding enc)
		{
			if (_lines == null)
				return false;
			else
			{
				_lines.LoadFile(filename, enc, true);
				return true;
			}
		}

		public static bool LoadFile(string filename)
		{
			return LoadFile(filename, Encoding.UTF8);
		}

		public static void SetTextBox(RichTextBox textbox)
		{
			_logs = textbox;
		}

		public static string Text(string text)
		{
			return _lines == null || !_lines.Try(text, out string v) ? $"<{text}>" : v;
		}

		public static string Text(string text, params object[] prms)
		{
			return _lines == null || !_lines.Try(text, out string v) ? $"<{text}>" : string.Format(v, prms);
		}

		public static string Text(int key)
		{
			return _lines == null || !_lines.Try(key, out string v) ? $"<{key}>" : v;
		}

		public static string Text(int key, params object[] prms)
		{
			return _lines == null || !_lines.Try(key, out string v) ? $"<{key}>" : string.Format(v, prms);
		}

		public static void Write(Color color, string format, params object[] prms)
		{
			if (_logs == null || _logs.IsDisposed || format == null)
				return;

			var text = string.Format(format, prms);
			var dt = DateTime.Now.ToString("HH:mm:ss");
			var ms = $"[{dt}] {text}{Environment.NewLine}";

			WorkerAct.Invoker(() =>
			{
				_logs.SelectionColor = color;
				_logs.SelectionStart = _logs.TextLength;
				_logs.SelectionLength = 0;
				_logs.AppendText(ms);

				_logs.SelectionColor = _logs.ForeColor;
				ThirdParty.NativeMethods.ScrollToBottom(_logs);
			});
		}

		public static void Write(string format, params object[] prms)
		{
			Write(Color.White, format, prms);
		}

		public static void L(string format, params object[] prms)
		{
			Write(Color.DarkBlue, format, prms);
		}

		// color
		public static void C(Color color, int key, params object[] prms)
		{
			Write(color, Text(key, prms));
		}

		// info / black
		public static void I(int key, params object[] prms)
		{
			Write(Color.Black, Text(key, prms));
		}

		// error / red
		public static void E(int key, params object[] prms)
		{
			Write(Color.Red, Text(key, prms));
		}

		// gray
		public static void Y(int key, params object[] prms)
		{
			Write(Color.Gray, Text(key, prms));
		}

		// exception
		public static void Ex(Exception ex, int key, params object[] prms)
		{
			string text = Text(key, prms);
			string msg = ExceptionPattern.Replace(ex.Message, "{{$1}}");
			Write(Color.Red, $"{text}: {msg}");
		}
	}
}
