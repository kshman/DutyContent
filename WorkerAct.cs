using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advanced_Combat_Tracker;

namespace DutyContent
{
	static class WorkerAct
	{
		public delegate object ObjectReturnerDelegate();

		private static object _lock_effect_sound = new object();
		private static long _last_effect_sound;

		// main invoker
		public static object Invoker(ObjectReturnerDelegate dgt)
		{
			return ActGlobals.oFormActMain.Invoke(dgt);
		}

		// 
		public static void Invoker(Action dgt)
		{
			ActGlobals.oFormActMain.Invoke(dgt);
		}

		//
		public static void Tts(string msg)
		{
			ActGlobals.oFormActMain.TTS(msg);
		}

		//
		public static void PlaySound(string filename, int volume_percent = 100)
		{
			ActGlobals.oFormActMain.PlaySoundWinApi(filename, volume_percent);
		}

		//
		public static void PlayEffectSound(string filename, int volume_percent = 100)
		{
			if (string.IsNullOrWhiteSpace(filename) || !File.Exists(filename))
				return;

			lock (_lock_effect_sound)
			{
				long now = DateTime.Now.Ticks;
				long delta = now - _last_effect_sound;
				TimeSpan span = new TimeSpan(delta);

				if (span.TotalSeconds > 2)
				{
					_last_effect_sound = now;
					PlaySound(filename, volume_percent);
				}
			}
		}
	}
}
