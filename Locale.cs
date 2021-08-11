using System.Text;

namespace DutyContent
{
	static class Locale
	{
		private static ThirdParty.LineDb _lines;

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
	}
}
