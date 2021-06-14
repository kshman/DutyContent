using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DutyContent
{
    internal class Updater
    {
        internal static void CheckNewVersion()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    var json = WebApi.Request($"https://raw.githubusercontent.com/kshman/DutyContent/main/Data/DcDuty-{DcContent.Language}.json");
                    DcContent.Fill(json);
                }
                catch (Exception ex)
                {
                    MesgLog.Ex(ex, 31);
                }
            });
        }

        public static string CheckNewPacket(string name)
		{
            try
			{
                var ret = WebApi.Request($"https://raw.githubusercontent.com/kshman/DutyContent/main/Data/DcPacket-{name}.config");

                return ret;
            }
            catch (Exception ex)
			{
                MesgLog.Ex(ex, 32);

                return null;
			}
		}
    }
}
