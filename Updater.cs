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
                    var json = WebApi.Request($"https://raw.githubusercontent.com/Jaehyuk-Lee/DutyContent/test/Data/DcDuty-{DcContent.Language}.json");
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