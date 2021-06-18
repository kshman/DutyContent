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
		public static Version PluginVersion => System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
		public static bool PluginEnable { get; set; }
		public static string PluginPath { get; set; }
		public static string DataPath { get; set; }
		public static string ConfigPath { get; set; }

		//
		public static PacketConfig Packet = new PacketConfig();
		public static DutyConfig Duty = new DutyConfig();

		//
		public static ConnectionList Connections = new ConnectionList();

		//
		public static string Language { get; set; } = "";
		public static bool DataRemoteUpdate { get; set; } = true;   // true = use remote update

		public static string BuildDataFileName(string header, string context, string ext)
		{
			return Path.Combine(DataPath, $"{header}-{context}.{ext}");
		}

		//
		public static string BuildDutyFileName(string language)
		{
			return BuildDataFileName("DcDuty", language, "json");
		}

		//
		public static string BuildLangFileName(string language)
		{
			return BuildDataFileName("DcLang", language, "txt");
		}

		//
		public static string BuildPacketFileName(string set)
		{
			return BuildDataFileName("DcPacket", set, "config");
		}

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
				sw.WriteLine("DataRemoteUpdate={0}", DataRemoteUpdate);
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
			DataRemoteUpdate = ThirdParty.Converter.ToBool(db["DataRemoteUpdate"], DataRemoteUpdate);

			Duty.InternalReadFromDb(db);
		}

		//
		public static void ReadLanguage(bool is_in_init = false)
		{
			if (string.IsNullOrWhiteSpace(Language))
			{
				if (!is_in_init)
					MesgLog.Initialize(Properties.Resources.DefaultMessage);
			}
			else
			{
				string filename = BuildLangFileName(Language);

				if (File.Exists(filename))
					MesgLog.LoadFile(filename);
				else
					MesgLog.Initialize(Properties.Resources.DefaultMessage);
			}
		}

		public static bool ReadPacket(string set = null)
		{
			if (set == null)
				set = Duty.PacketSet;
			else if (!Duty.PacketSet.Equals(set))
				Duty.PacketSet = set;

			var filename = BuildPacketFileName(set);

			if (!File.Exists(filename))
			{
				filename = BuildPacketFileName(set = PacketConfig.DefaultSetNameGlobal);

				if (!File.Exists(filename))
				{
					filename = BuildPacketFileName(set = PacketConfig.DefaultSetNameCustom);

					if (!File.Exists(filename))
					{
						MesgLog.E(27, " ");
						return false;
					}
				}

				if (!Duty.PacketSet.Equals(set))
					Duty.PacketSet = set;
			}

			// load. if file not exist, create new one with default value
			Packet.Load(filename);

			MesgLog.I(29, Packet.Version, Packet.Description, filename);

			return true;
		}

		//
		public class PacketConfig
		{
			// Packet
			public long Version { get; set; } = 2005551;
			public string Description { get; set; } = "5.55 HotFix";
			public ushort OpFate { get; set; } = 858;
			public ushort OpDuty { get; set; } = 271;
			public ushort OpMatch { get; set; } = 220;
			public ushort OpInstance { get; set; } = 923;
			public ushort OpSouthernBozja { get; set; } = 584;

			// packet version structure
			// 0 - Service area (1:Custom, 2:Global, 3:Korea)
			// 1 - Reserved. Must be 0
			// 2 - Expansion version
			// 3
			// 4 - Update version
			// 5
			// 6 - HotFix or packet version

			public readonly static string DefaultSetNameCustom = "Custom";
			public readonly static string DefaultSetNameGlobal = "Global";

			public PacketConfig()
			{
				// nothing to do
			}

			public PacketConfig(DateTime dt, PacketConfig right = null)
			{
				// for custom
				Version = ThirdParty.Converter.ToLong($"1{dt:yyMMdd}");
				Description = $"Custom ({dt:d})";

				if (right != null)
				{
					OpFate = right.OpFate;
					OpDuty = right.OpDuty;
					OpMatch = right.OpMatch;
					OpInstance = right.OpInstance;
					OpSouthernBozja = right.OpSouthernBozja;
				}
				else
				{
					OpFate = 0;
					OpDuty = 0;
					OpMatch = 0;
					OpInstance = 0;
					OpSouthernBozja = 0;
				}
			}

			// 
			public bool Save(string filename)
			{
				if (filename == null)
					return false;

				using (var sw = new StreamWriter(filename, false, Encoding.UTF8))
				{
					sw.WriteLine("# DutyPacket configuration: {0}", DateTime.Now.ToString());
					sw.WriteLine();

					sw.WriteLine("# packet");
					sw.WriteLine("Version={0}", Version);
					sw.WriteLine("Description={0}", Description);
					sw.WriteLine("OpFate={0}", OpFate);
					sw.WriteLine("OpDuty={0}", OpDuty);
					sw.WriteLine("OpMatch={0}", OpMatch);
					sw.WriteLine("OpInstance={0}", OpInstance);
					sw.WriteLine("OpSouthernBozja={0}", OpSouthernBozja);
					sw.WriteLine();
				}

				return true;
			}

			private void InternalParseString(ThirdParty.LineDb db)
			{
				Version = ThirdParty.Converter.ToLong(db["Version"]);
				Description = db["Description"];
				OpFate = ThirdParty.Converter.ToUshort(db["OpFate"], OpFate);
				OpDuty = ThirdParty.Converter.ToUshort(db["OpDuty"], OpDuty);
				OpMatch = ThirdParty.Converter.ToUshort(db["OpMatch"], OpMatch);
				OpInstance = ThirdParty.Converter.ToUshort(db["OpInstance"], OpInstance);
				OpSouthernBozja = ThirdParty.Converter.ToUshort(db["OpSouthernBozja"], OpSouthernBozja);
			}

			//
			public void Load(string filename = null)
			{
				if (!File.Exists(filename))
					Save(filename);

				var db = new ThirdParty.LineDb(filename, Encoding.UTF8, false);
				InternalParseString(db);
			}

			//
			public static PacketConfig ParseString(string ctx)
			{
				var pk = new PacketConfig();
				var db = new ThirdParty.LineDb(ctx, false);
				pk.InternalParseString(db);
				return pk;
			}
		}

		//
		public class DutyConfig
		{
			public string Language { get; set; } = "English";
			public int ActiveFate { get; set; } = 0;
			public string PacketSet { get; set; } = "Global";
			public string LogFontFamily { get; set; } = "Microsoft Sans Serif";
			public float LogFontSize { get; set; } = 12.0f;

			public bool EnableOverlay { get; set; }
			public Point OverlayLocation { get; set; } = new Point(0, 0);
			public bool OverlayClickThru { get; set; }

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

			public bool UsePing { get; set; }
			public Color[] PingColors { get; set; } = new Color[4]
			{
				Color.FromArgb(0xFF, 0x00, 0x00, 0x40),
				Color.FromArgb(0xFF, 0x40, 0x00, 0x80),
				Color.FromArgb(0xFF, 0x80, 0x40, 0x00),
				Color.FromArgb(0xFF, 0xDD, 0xA0, 0xDD),
			};
			public bool PingGraph { get; set; }
			public string PingDefAddr { get; set; }

			//
			public bool EnableNotify => UseNotifyLine || UseNotifyTelegram;

			//
			public FateSelection[] Fates { get; set; } = new FateSelection[4]
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
				sw.WriteLine("DutypPacketSet={0}", PacketSet);

				sw.WriteLine("DutyLogFontFamily={0}", LogFontFamily);
				sw.WriteLine("DutyLogFontSize={0}", LogFontSize);

				sw.WriteLine("DutyEnableOverlay={0}", EnableOverlay);
				sw.WriteLine("DutyOverlayLocationX={0}", OverlayLocation.X);
				sw.WriteLine("DutyOverlayLocationY={0}", OverlayLocation.Y);
				sw.WriteLine("DutyOverlayClickThru={0}", OverlayClickThru);

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

				sw.WriteLine("DutyUsePing={0}", UsePing);
				sw.WriteLine("DutyPingColor0={0:X}", PingColors[0].ToArgb());
				sw.WriteLine("DutyPingColor1={0:X}", PingColors[1].ToArgb());
				sw.WriteLine("DutyPingColor2={0:X}", PingColors[2].ToArgb());
				sw.WriteLine("DutyPingColor3={0:X}", PingColors[3].ToArgb());
				sw.WriteLine("DutyPingGraph={0}", PingGraph);
				sw.WriteLine("DutyPingDefAddr={0}", PingDefAddr);
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
				PacketSet = db.Get("DutypPacketSet", PacketSet);

				LogFontFamily = db.Get("DutyLogFontFamily", LogFontFamily);
				LogFontSize = ThirdParty.Converter.ToFloat(db["DutyLogFontSize"], LogFontSize);

				EnableOverlay = ThirdParty.Converter.ToBool(db["DutyEnableOverlay"]);
				OverlayLocation = new Point(
					ThirdParty.Converter.ToInt(db["DutyOverlayLocationX"]),
					ThirdParty.Converter.ToInt(db["DutyOverlayLocationY"]));
				OverlayClickThru = ThirdParty.Converter.ToBool(db["DutyOverlayClickThru"]);

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

				UsePing = ThirdParty.Converter.ToBool(db["DutyUsePing"]);
				PingColors[0] = ThirdParty.Converter.ToColorArgb(db["DutyPingColor0"], PingColors[0]);
				PingColors[1] = ThirdParty.Converter.ToColorArgb(db["DutyPingColor1"], PingColors[1]);
				PingColors[2] = ThirdParty.Converter.ToColorArgb(db["DutyPingColor2"], PingColors[2]);
				PingColors[3] = ThirdParty.Converter.ToColorArgb(db["DutyPingColor3"], PingColors[3]);
				PingGraph = ThirdParty.Converter.ToBool(db["DutyPingGraph"]);
				PingDefAddr = db.Get("DutyPingDefAddr", string.Empty);
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

			public ThirdParty.NativeMethods.TcpRow[] CopyConnection()
			{
				ThirdParty.NativeMethods.TcpRow[] ret;

				lock (Conns)
					ret = Conns.ToArray();

				return ret;
			}

			public void GetConnections(Process process)
			{
				var size = 0;
				ThirdParty.NativeMethods.GetExtendedTcpTable(IntPtr.Zero, ref size, true, AddressFamily.InterNetwork, 4);

				var buff = Marshal.AllocHGlobal(size);

				try
				{
					var ret = ThirdParty.NativeMethods.GetExtendedTcpTable(buff, ref size, true, AddressFamily.InterNetwork, 4);
					if (ret == 0)
					{
						var tbl = Marshal.PtrToStructure<ThirdParty.NativeMethods.TcpTable>(buff);
						var ptr = (IntPtr)((long)buff + Marshal.SizeOf(tbl.entries));

						var rows = new ThirdParty.NativeMethods.TcpRow[tbl.entries];
						var rcnt = 0;

						for (var i = 0; i < tbl.entries; i++)
						{
							var row = Marshal.PtrToStructure<ThirdParty.NativeMethods.TcpRow>(ptr);

							if (!IPAddress.IsLoopback(row.RemoteAddress) &&
								process.Id == row.owningPid)
								rows[rcnt++] = row;

							ptr = (IntPtr)((long)ptr + Marshal.SizeOf(row));
						}

						lock (Conns)
						{
							Conns.Clear();
							for (var i = 0; i < rcnt; i++)
								Conns.Add(rows[i]);
						}
					}
					else
					{
						lock (Conns)
							Conns.Clear();
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
