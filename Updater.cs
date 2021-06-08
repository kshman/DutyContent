using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
                    var lang = DcConfig.Language;
                    if (lang.Contains("Korean")) lang = "Korean";
                    var json = WebApi.Request($"https://raw.githubusercontent.com/kshman/DutyContent/main/Data/DcDuty-{lang}.json");
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