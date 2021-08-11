using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DutyContent
{
	internal class Updater
	{
		// https://raw.githubusercontent.com/kshman/DutyContent/main/Data/####-####.####
		// https://api.github.com/repos/kshman/DutyContent/releases/latest

		private const string UrlContent = "https://raw.githubusercontent.com/kshman/DutyContent";
		private const string UrlApiRepo = "https://api.github.com/repos/kshman/DutyContent";
		private const string PathData = "main/Data";

		private const string PfxDuty = "DcDuty";
		private const string PfxPacket = "DcPacket";

		internal static void CheckNewVersion()
		{
			Task.Factory.StartNew(() =>
			{
				try
				{
					var url = $"{UrlContent}/{PathData}/{PfxDuty}-{DcContent.Language}.json";
					var json = WebApi.Request(url);
					DcContent.Fill(json);
				}
				catch (Exception ex)
				{
					Logger.Ex(ex, 31);
				}
			});
		}

		public static string CheckNewPacket(string name)
		{
			try
			{
				var url = $"{UrlContent}/{PathData}/{PfxPacket}-{name}.config";
				var ret = WebApi.Request(url);

				return ret;
			}
			catch (Exception ex)
			{
				Logger.Ex(ex, 32);

				return null;
			}
		}

		public static int CheckPluginUpdate(out string body)
		{
			body = string.Empty;

			var url = $"{UrlApiRepo}/releases/latest";
			var req = WebApi.Request(url);

			if (!string.IsNullOrEmpty(req))
			{
				try
				{
					var js = JsonConvert.DeserializeObject<dynamic>(req);
					var tag = js.tag_name.ToObject<string>();
					body = js.body.ToObject<string>();

					return ThirdParty.Converter.ToInt(tag);
				}
				catch (Exception /*ex*/)
				{
				}
			}

			return 0;
		}
	}
}
