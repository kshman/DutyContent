using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DutyContent
{
	public class DcContent
	{
		// roulette
		public class Roulette
		{
			public string Name { get; set; }

			public static explicit operator Roulette(string name)
			{
				return new Roulette { Name = name };
			}
		}

		// instance
		public class Instance
		{
			public string Name { get; set; }

			public static explicit operator Instance(string name)
			{
				return new Instance { Name = name };
			}
		}

		// area
		public class Area
		{
			public string Name { get; set; }

			public IReadOnlyDictionary<int, Fate> Fates { get; set; }
		}

		// fate
		public class Fate
		{
			public Area Area { get; set; }

			public string Name { get; set; }

			public static explicit operator Fate(string name)
			{
				return new Fate { Name = name };
			}
		}

		// group
		public class Group
		{
			public decimal Version { get; set; }
			public string Language { get; set; }
			public string DisplayLanguage { get; set; }
			public Dictionary<int, Roulette> Roulettes { get; set; }
			public Dictionary<int, Instance> Instances { get; set; }
			public Dictionary<int, Area> Areas { get; set; }
		}

		//
		public static decimal Version { get; private set; } = 0;
		public static string Language { get; private set; }
		public static string DisplayLanguage { get; private set; }
		public static IReadOnlyDictionary<int, Roulette> Roulettes { get; private set; } = new Dictionary<int, Roulette>();
		public static IReadOnlyDictionary<int, Instance> Instances { get; private set; } = new Dictionary<int, Instance>();
		public static IReadOnlyDictionary<int, Area> Areas { get; private set; } = new Dictionary<int, Area>();
		public static IReadOnlyDictionary<int, Fate> Fates { get; private set; } = new Dictionary<int, Fate>();
		public static Dictionary<int, int> Missions { get; private set; } = new Dictionary<int, int>();

		//
		public static string GetInformation()
		{
			return Locale.Text(20,
					Language,
					Version,
					Areas.Count,
					Roulettes.Count,
					Instances.Count,
					Fates.Count);
		}

		// 
		public static bool Initialize(string json)
		{
			if (string.IsNullOrWhiteSpace(json))
				return false;

			bool ret;

			try
			{
				ret = Fill(json);
			}
			catch
			{
				ret = false;
			}

			return ret;
		}


		// parse json 
		public static bool Fill(string json)
		{
			Group data = JsonConvert.DeserializeObject<Group>(json);

			Dictionary<int, Fate> fates = new Dictionary<int, Fate>();

			var version = data.Version;
			var language = data.Language;

			if (version > Version || language != Language)
			{
				foreach (var area in data.Areas)
				{
					foreach (var fate in area.Value.Fates)
					{
						try
						{
							fate.Value.Area = area.Value;
							fates.Add(fate.Key, fate.Value);
						}
						catch (NullReferenceException /*nex*/)
						{
							Logger.E(7, fate.Key);
							return false;
						}
						catch (Exception ex)
						{
							Logger.Ex(ex, 8);
							return false;
						}
					}
				}

				Version = data.Version;
				Language = data.Language;
				DisplayLanguage = data.DisplayLanguage;
				Roulettes = data.Roulettes;
				Instances = data.Instances;
				Areas = data.Areas;
				Fates = fates;
			}

			return true;
		}

		//
		public static Roulette TryRoulette(int code)
		{
			return Roulettes.TryGetValue(code, out Roulette roulette) ? roulette : null;
		}

		//
		public static Instance TryInstance(int code)
		{
			return Instances.TryGetValue(code, out Instance instance) ? instance : null;
		}

		//
		public static Area TryArea(int code)
		{
			return Areas.TryGetValue(code, out Area area) ? area : null;
		}

		//
		public static Fate TryFate(int code)
		{
			return Fates.ContainsKey(code) ? Fates[code] : null;
		}

		//
		public static Roulette GetRoulette(int code)
		{
			return Roulettes.TryGetValue(code, out Roulette roulette) ? roulette :
				new Roulette { Name = Locale.Text(9, code) };
		}

		//
		public static Instance GetInstance(int code)
		{
			return Instances.TryGetValue(code, out Instance instance) ? instance :
				new Instance { Name = Locale.Text(10, code) };
		}

		//
		public static Area GetArea(int code)
		{
			return Areas.TryGetValue(code, out Area area) ? area :
				new Area { Name = Locale.Text(11, code) };
		}

		//
		public static Fate GetFate(int code)
		{
			return Fates.ContainsKey(code) ? Fates[code] :
				new Fate { Name = Locale.Text(12, code) };
		}

		//
		public static bool ReadContent(string language = null)
		{
			if (language == null)
				language = DcConfig.Duty.Language;
			else if (!DcConfig.Duty.Language.Equals(language))
				DcConfig.Duty.Language = language;

			string filename = DcConfig.BuildDutyFileName(language);

			if (!File.Exists(filename))
			{
				filename = DcConfig.BuildDutyFileName(language = "English");

				if (!File.Exists(filename))
				{
					Logger.E(14, filename);
					return false;
				}

				if (!DcConfig.Duty.Language.Equals(language))
					DcConfig.Duty.Language = language;
			}

			string json = File.ReadAllText(filename, Encoding.UTF8);

			if (Initialize(json))
			{
				Logger.Write(GetInformation());
				return true;
			}
			else
			{
				Logger.E(13);
				return false;
			}
		}

		public static string CeStatusToString(int s)
		{
			// 10[1] status 0=end, 1=wait, 2=??, 3=progress
			switch (s)
			{
				case 0: return Locale.Text(10017);
				case 1: return Locale.Text(10018);
				case 2: return Locale.Text(10019);
				case 3: return Locale.Text(10020);
				default: return Locale.Text(10021);
			}
		}

		// save the queen type
		public enum SaveTheQueenType
		{
			No,
			Bozja,
			Delubrum,
			Zadnor,
		}

		//
		public static int SaveTheQueenTypeToCeBase(SaveTheQueenType stq)
		{
			return
				stq == SaveTheQueenType.Bozja ? 30000 :
				stq == SaveTheQueenType.Zadnor ? 30100 :
				30100;	// fail safe
		}
	}
}
