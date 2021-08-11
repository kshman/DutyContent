using System;
using System.Drawing;
using System.Text.RegularExpressions;

namespace DutyContent
{
	static class Logger
	{
		private static readonly Regex ExceptionPattern = new Regex(@"\{(.+?)\}");

		public static void Write(Color color, string fmt, params object[] prms)
		{
			if (Tab.LogForm.Self != null)
				Tab.LogForm.Self.WriteLog(color, string.Format(fmt, prms));
		}

		public static void Write(string format, params object[] prms)
		{
			Write(Color.Black, format, prms);
		}

		public static void WriteCategory(Color color, string category, string fmt, params object[] prms)
		{
			if (Tab.LogForm.Self != null)
				Tab.LogForm.Self.WriteLogSection(color, category, string.Format(fmt, prms));
		}

		public static void WriteCategory(string category, string format, params object[] prms)
		{
			Write(Color.Black, category, format, prms);
		}

		public static void L(string format, params object[] prms)
		{
			Write(Color.DarkBlue, format, prms);
		}

		// color
		public static void C(Color color, int key, params object[] prms)
		{
			Write(color, Locale.Text(key, prms));
		}

		// info / black
		public static void I(int key, params object[] prms)
		{
			Write(Color.Black, Locale.Text(key, prms));
		}

		// error / red
		public static void E(int key, params object[] prms)
		{
			Write(Color.Red, Locale.Text(key, prms));
		}

		// gray
		public static void Y(int key, params object[] prms)
		{
			Write(Color.Gray, Locale.Text(key, prms));
		}

		// exception
		public static void Ex(Exception ex, int key, params object[] prms)
		{
			string text = Locale.Text(key, prms);
			string msg = ExceptionPattern.Replace(ex.Message, "{{$1}}");
			Write(Color.Red, $"{text}: {msg}");
		}

		// exception
		public static void Ex(Exception ex)
		{
			string msg = ExceptionPattern.Replace(ex.Message, "{{$1}}");
			Write(Color.Red, $"EX: {msg}");
		}
	}
}
