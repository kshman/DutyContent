using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DutyContent.ThirdParty
{
	class LineDb
	{
		private Dictionary<string, string> _dbstr = new Dictionary<string, string>();
		private Dictionary<int, string> _dbint = new Dictionary<int, string>();

		public static string InvalidKey = "<invalid>";

		public LineDb(string ctx, bool useintdb)
		{
			LoadString(ctx, useintdb, false);
		}

		public LineDb(string filename, Encoding enc, bool useintdb)
		{
			LoadFile(filename, enc, useintdb, false);
		}

		public void LoadString(string ctx, bool useintdb, bool reset = false)
		{
			if (reset)
			{
				_dbstr.Clear();
				_dbint.Clear();
			}

			ParseLines(ctx, useintdb);
		}

		public void LoadFile(string filename, Encoding enc, bool useintdb, bool reset = false)
		{
			try
			{
				var ctx = File.ReadAllText(filename, enc);
				LoadString(ctx, useintdb, reset);
			}
			catch { }
		}

		public bool SaveFile(string filename, Encoding enc, string header = null)
		{
			if (string.IsNullOrEmpty(filename))
				return false;

			using (var sw = new StreamWriter(filename, false, enc))
			{
				if (!string.IsNullOrEmpty(header))
				{
					sw.WriteLine(header);
					sw.WriteLine();
				}

				foreach (var l in _dbstr)
				{
					if (l.Key.IndexOf('=') < 0)
						sw.WriteLine($"{l.Key}={l.Value}");
					else
						sw.WriteLine($"\"{l.Key}\"={l.Value}");
				}

				foreach (var l in _dbint)
					sw.WriteLine($"{l.Key}={l.Value}");
			}

			return true;
		}

		private void ParseLines(string ctx, bool useintdb)
		{
			_dbstr.Clear();

			var ss = ctx.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

			foreach (var v in ss)
			{
				string name, value, l = v.TrimStart();

				if (l[0] == '#' || l.StartsWith("//"))
					continue;

				if (l[0] == '"')
				{
					var qt = l.IndexOf('"', 1);
					if (qt < 0)
					{
						// no end quote?
						continue;
					}

					value = l.Substring(qt + 1).TrimStart();

					if (value.Length == 0 || value[0] != '=')
					{
						// no value
						continue;
					}

					name = l.Substring(1, qt - 1).Trim();
					value = value.Substring(1).Trim();
				}
				else
				{
					var div = l.IndexOf('=');
					if (div <= 0)
						continue;

					name = l.Substring(0, div).Trim();
					value = l.Substring(div + 1).Trim();
				}

				if (!useintdb)
				{
					if (_dbstr.TryGetValue(name, out _))
						_dbstr.Remove(name);
					_dbstr.Add(name, value);
				}
				else
				{
					if (!Converter.TryInt(name, out int nkey))
					{
						if (_dbstr.TryGetValue(name, out _))
							_dbstr.Remove(name);
						_dbstr.Add(name, value);
					}
					else
					{
						if (_dbint.TryGetValue(nkey, out _))
							_dbint.Remove(nkey);
						_dbint.Add(nkey, value);
					}
				}
			}
		}

		public string Get(string name)
		{
			if (!_dbstr.TryGetValue(name, out string value))
				return name;
			return value;
		}

		public string Get(int key)
		{
			if (!_dbint.TryGetValue(key, out string value))
				return InvalidKey;
			return value;
		}

		public bool Try(string name, out string value)
		{
			return _dbstr.TryGetValue(name, out value);
		}

		public bool Try(int key, out string value)
		{
			return _dbint.TryGetValue(key, out value);
		}

		public bool Try(string name, out int value)
		{
			if (!_dbstr.TryGetValue(name, out string v))
			{
				value = 0;
				return false;
			}
			else
			{
				if (!ThirdParty.Converter.TryInt(v, out value))
					return false;
				return true;
			}
		}

		public bool Try(string name, out ushort value)
		{
			if (!_dbstr.TryGetValue(name, out string v))
			{
				value = 0;
				return false;
			}
			else
			{
				if (!ThirdParty.Converter.TryUshort(v, out value))
					return false;
				return true;
			}
		}

		public IEnumerator<KeyValuePair<string, string>> GetStringDb()
		{
			return (IEnumerator<KeyValuePair<string, string>>)_dbstr;
		}

		public IEnumerator<KeyValuePair<int, string>> GetIntDb()
		{
			return (IEnumerator<KeyValuePair<int, string>>)_dbint;
		}

		public int Count { get { return _dbstr.Count + _dbint.Count; } }

		public string this[string index]
		{
			get
			{
				return Try(index, out string v) ? v : "";
			}
		}

		public string this[int key]
		{
			get
			{
				return Try(key, out string v) ? v : "";
			}
		}
	}
}
