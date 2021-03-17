using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DutyContent
{
	class DcConfig
	{
		public static bool PluginEnable { get; set; }
		public static string PluginPath { get; set; }
		public static string DataPath { get; set; }
		public static string PacketPath { get; set; }
		public static string ConfigPath { get; set; }

		//
		public static PacketConfig Packet = new PacketConfig();
		public static DutyConfig Duty = new DutyConfig();

		//
		public static ConnectionList Connections = new ConnectionList();

		//
		public static string Language { get; set; } = "";

		//
		public static void SaveConfig(string filename = null)
		{
			if (filename == null)
				filename = ConfigPath;

			using (var sw = new StreamWriter(filename, false, Encoding.UTF8))
			{
				sw.WriteLine("# DutyContent configuration: {0}", DateTime.Now.ToString());
				sw.WriteLine();

				sw.WriteLine("# config");
				sw.WriteLine("Language={0}", Language);
				sw.WriteLine();

				Duty.InternalSaveStream(sw);
			}
		}

		//
		public static void LoadConfig(string filename = null)
		{
			if (filename == null)
				filename = ConfigPath;

			if (!File.Exists(filename))
				SaveConfig(filename);

			var db = new ThirdParty.LineDb(filename, Encoding.UTF8, false);

			Language = db["Language"];

			Duty.InternalReadFromDb(db);
		}

		//
		public static void Load()
		{
			Packet.Load();
			LoadConfig();
		}

		//
		public static void ReadLanguage(bool is_in_init = false)
		{
			if (string.IsNullOrWhiteSpace(DcConfig.Language))
			{
				if (!is_in_init)
					MesgLog.Initialize(Properties.Resources.DefaultMessage);
			}
			else
			{
				string filename = Path.Combine(DcConfig.DataPath, $"DcLang-{DcConfig.Language}.txt");

				if (File.Exists(filename))
					MesgLog.LoadFile(filename);
				else
					MesgLog.Initialize(Properties.Resources.DefaultMessage);
			}
		}

		//
		public class PacketConfig
		{
			// Packet
			public string Version { get; set; } = "5.45 HotFix";
			public ushort OpFate { get; set; } = 0x3D5;
			public ushort OpDuty { get; set; } = 0x307;
			public ushort OpMatch { get; set; } = 0x26E;
			public ushort OpInstance { get; set; } = 0x10C;
			public ushort OpSouthernBozja { get; set; } = 0x1F5;

			// 
			public void Save(string filename = null)
			{
				if (filename == null)
					filename = PacketPath;

				using (var sw = new StreamWriter(filename, false, Encoding.UTF8))
				{
					sw.WriteLine("# DutyPacket configuration: {0}", DateTime.Now.ToString());
					sw.WriteLine();

					sw.WriteLine("# packet");
					sw.WriteLine("Version={0}", Version);
					sw.WriteLine("OpFate={0}", OpFate);
					sw.WriteLine("OpDuty={0}", OpDuty);
					sw.WriteLine("OpMatch={0}", OpMatch);
					sw.WriteLine("OpInstance={0}", OpInstance);
					sw.WriteLine("OpSouthernBozja={0}", OpSouthernBozja);
					sw.WriteLine();
				}
			}

			//
			public void Load(string filename = null)
			{
				if (filename == null)
					filename = PacketPath;

				if (!File.Exists(filename))
					Save(filename);

				var db = new ThirdParty.LineDb(filename, Encoding.UTF8, false);

				Version = db["Version"];
				OpFate = ThirdParty.Converter.ToUshort(db["OpFate"], OpFate);
				OpDuty = ThirdParty.Converter.ToUshort(db["OpDuty"], OpDuty);
				OpMatch = ThirdParty.Converter.ToUshort(db["OpMatch"], OpMatch);
				OpInstance = ThirdParty.Converter.ToUshort(db["OpInstance"], OpInstance);
				OpSouthernBozja = ThirdParty.Converter.ToUshort(db["OpSouthernBozja"], OpSouthernBozja);
			}
		}

		//
		public class DutyConfig
		{
			public string Language { get; set; } = "English";
			public int ActiveFate { get; set; } = 0;
			public string LogFontFamily { get; set; } = "Microsoft Sans Serif";
			public float LogFontSize { get; set; } = 12.0f;
			public bool EnableOverlay { get; set; }
			public bool EnableSound { get; set; }
			public string SoundInstanceFile { get; set; }
			public int SoundInstanceVolume { get; set; } = 100;
			public string SoundFateFile { get; set; }
			public int SoundFateVolume { get; set; } = 100;
			public bool UseNotifyLine { get; set; }
			public string NotifyLineToken { get; set; }
			public bool UseNotifyTelegram { get; set; }
			public string NotifyTelegramId { get; set; }
			public string NotifyTelegramToken { get; set; }
			public Point OverlayLocation { get; set; } = new Point(0, 0);

			//
			public bool EnableNotify => UseNotifyLine || UseNotifyTelegram;

			//
			public FateSelection[] Fates = new FateSelection[4]
			{
				new FateSelection(0),
				new FateSelection(1),
				new FateSelection(2),
				new FateSelection(3),
			};

			//
			internal void InternalSaveStream(StreamWriter sw)
			{
				sw.WriteLine("# duty");
				sw.WriteLine("DutyLanguage={0}", Language);
				sw.WriteLine("DutyActiveFate={0}", ActiveFate);
				sw.WriteLine("DutyFate0={0}", Fates[0].Line);
				sw.WriteLine("DutyFate1={0}", Fates[1].Line);
				sw.WriteLine("DutyFate2={0}", Fates[2].Line);
				sw.WriteLine("DutyFate3={0}", Fates[3].Line);
				sw.WriteLine("DutyLogFontFamily={0}", LogFontFamily);
				sw.WriteLine("DutyLogFontSize={0}", LogFontSize);
				sw.WriteLine("DutyEnableOverlay={0}", EnableOverlay);
				sw.WriteLine("DutyEnableSound={0}", EnableSound);
				sw.WriteLine("DutySoundInstanceFile={0}", SoundInstanceFile);
				sw.WriteLine("DutySoundInstanceVolume={0}", SoundInstanceVolume);
				sw.WriteLine("DutySoundFateFile={0}", SoundFateFile);
				sw.WriteLine("DutySoundFateVolume={0}", SoundFateVolume);
				sw.WriteLine("DutyUseNotifyLine={0}", UseNotifyLine);
				sw.WriteLine("DutyNotifyLineToken={0}", NotifyLineToken);
				sw.WriteLine("DutyUseNotifyTelegram={0}", UseNotifyTelegram);
				sw.WriteLine("DutyNotifyTelegramId={0}", NotifyTelegramId);
				sw.WriteLine("DutyNotifyTelegramToken={0}", NotifyTelegramToken);
				sw.WriteLine("DutyOverlayLocationX={0}", OverlayLocation.X);
				sw.WriteLine("DutyOverlayLocationY={0}", OverlayLocation.Y);
				sw.WriteLine();
			}

			//
			internal void InternalReadFromDb(ThirdParty.LineDb db)
			{
				Language = db["DutyLanguage"];
				ActiveFate = ThirdParty.Converter.ToInt(db["DutyActiveFate"]);
				Fates[0].Line = db["DutyFate0"];
				Fates[1].Line = db["DutyFate1"];
				Fates[2].Line = db["DutyFate2"];
				Fates[3].Line = db["DutyFate3"];
				LogFontFamily = db["DutyLogFontFamily"];
				LogFontSize = ThirdParty.Converter.ToFloat(db["DutyLogFontSize"], LogFontSize);
				EnableOverlay = ThirdParty.Converter.ToBool(db["DutyEnableOverlay"]);
				EnableSound = ThirdParty.Converter.ToBool(db["DutyEnableSound"]);
				SoundInstanceFile = db["DutySoundInstanceFile"];
				SoundFateFile = db["DutySoundFateFile"];
				SoundInstanceVolume = ThirdParty.Converter.ToInt(db["DutySoundInstanceVolume"], 100);
				SoundFateVolume = ThirdParty.Converter.ToInt(db["DutySoundFateVolume"], 100);
				UseNotifyLine = ThirdParty.Converter.ToBool(db["DutyUseNotifyLine"]);
				NotifyLineToken = db["DutyNotifyLineToken"];
				UseNotifyTelegram = ThirdParty.Converter.ToBool(db["DutyUseNotifyTelegram"]);
				NotifyTelegramId = db["DutyNotifyTelegramId"];
				NotifyTelegramToken = db["DutyNotifyTelegramToken"];
				OverlayLocation = new Point(ThirdParty.Converter.ToInt(db["DutyOverlayLocationX"]), ThirdParty.Converter.ToInt(db["DutyOverlayLocationY"]));
			}
		}

		//
		public class FateSelection
		{
			public HashSet<int> Selected { get; } = new HashSet<int>();
			public string Line { get; set; }
			public int Index { get; set; }

			public FateSelection(int index)
			{
				Index = index;
			}

			public void MakeSelects(bool clear = false)
			{
				if (clear)
					Selected.Clear();

				var ss = Line.Split('|');
				foreach (var s in ss)
				{
					if (!string.IsNullOrWhiteSpace(s) && ThirdParty.Converter.TryInt(s, out int i))
						Selected.Add(i);
				}
			}

			public void MakeLine()
			{
				Line = string.Join("|", Selected);
			}
		}

		//
		public class ConnectionList
		{
			public List<ThirdParty.NativeMethods.TcpRow> Conns = new List<ThirdParty.NativeMethods.TcpRow>();

			public void GetConnections(Process process)
			{
				Conns.Clear();

				var size = 0;
				var ret = ThirdParty.NativeMethods.GetExtendedTcpTable(IntPtr.Zero, ref size, true, AddressFamily.InterNetwork, 4);
				var buff = Marshal.AllocHGlobal(size);

				try
				{
					ret = ThirdParty.NativeMethods.GetExtendedTcpTable(buff, ref size, true, AddressFamily.InterNetwork, 4);
					if (ret == 0)
					{
						var tbl = Marshal.PtrToStructure<ThirdParty.NativeMethods.TcpTable>(buff);
						var ptr = (IntPtr)((long)buff + Marshal.SizeOf(tbl.entries));

						for (var i = 0; i < tbl.entries; i++)
						{
							var row = Marshal.PtrToStructure<ThirdParty.NativeMethods.TcpRow>(ptr);

							if (!IPAddress.IsLoopback(row.RemoteAddress) &&
								process.Id == row.owningPid)
								Conns.Add(row);

							ptr = (IntPtr)((long)ptr + Marshal.SizeOf(row));
						}
					}
				}
				finally
				{
					if (buff != null)
						Marshal.FreeHGlobal(buff);
				}
			}
		}
	}
}
