using System;
using System.Drawing;

namespace DutyContent.ThirdParty
{
	static class Converter
	{
		public static bool TryLong(string s, out long ret)
		{
			ret = 0;

			for (int i = 0; i < s.Length; i++)
			{
				var n = s[i] - '0';
				if (n < 0 || n > 9)
					return false;

				ret = ret * 10 + n;
			}

			return true;
		}

		public static long ToLong(string s, long failret = 0)
		{
			long ret = 0;

			for (int i = 0; i < s.Length; i++)
			{
				var n = s[i] - '0';
				if (n < 0 || n > 9)
					return failret;

				ret = ret * 10 + n;
			}

			return ret;
		}

		public static bool TryInt(string s, out int ret)
		{
			ret = 0;

			for (int i = 0; i < s.Length; i++)
			{
				var n = s[i] - '0';
				if (n < 0 || n > 9)
					return false;

				ret = ret * 10 + n;
			}

			return true;
		}

		public static int ToInt(string s, int failret = 0)
		{
			int ret = 0;

			for (int i = 0; i < s.Length; i++)
			{
				var n = s[i] - '0';
				if (n < 0 || n > 9)
					return failret;

				ret = ret * 10 + n;
			}

			return ret;
		}

		public static bool TryUshort(string s, out ushort ret)
		{
			ret = 0;

			for (int i = 0; i < s.Length; i++)
			{
				var n = s[i] - '0';
				if (n < 0 || n > 9)
					return false;

				ret = (ushort)(ret * 10 + n);
			}

			return true;
		}

		public static ushort ToUshort(string s, ushort failret = 0)
		{
			int ret = 0;

			for (int i = 0; i < s.Length; i++)
			{
				var n = s[i] - '0';
				if (n < 0 || n > 9)
					return failret;

				ret = ret * 10 + n;
			}

			return (ushort)ret;
		}

		public static bool ToBool(string s)
		{
			return s.ToUpper().Equals("TRUE");
		}

		public static float ToFloat(string s, float failret = 0.0f)
		{
			return float.TryParse(s, out float v) ? v : failret;
		}

		public static Color ToColorArgb(string s, Color failret)
		{
			try
			{
				var i = Convert.ToInt32(s, 16);
				var r = Color.FromArgb(i);
				return r;
			}
			catch
			{
				return failret;
			}
		}

		public static Color ToColorArgb(string s)
		{
			return ToColorArgb(s, Color.Transparent);
		}
	}
}
