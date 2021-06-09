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
    }
}
