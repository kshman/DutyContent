using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text;

namespace DutyContent
{
	internal static class WebApi
	{
		internal static string Request(string urlfmt, params object[] args)
		{
			var url = string.Format(urlfmt, args);

			try
			{
				ServicePointManager.Expect100Continue = true;
				ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
				ServicePointManager.DefaultConnectionLimit = 9999;

				var request = (HttpWebRequest)WebRequest.Create(url);
				request.UserAgent = "DFA";
				request.Timeout = 10000;
				request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
				using (var response = (HttpWebResponse)request.GetResponse())
				{
					var encoding = Encoding.GetEncoding(response.CharacterSet);

					using (var responseStream = response.GetResponseStream())
					using (var reader = new StreamReader(responseStream, encoding))
						return reader.ReadToEnd();
				}
			}
			catch (Exception ex)
			{
				MesgLog.Ex(ex, 30);
				MesgLog.L("URL: {0}", url);
			}

			return null;
		}
	}
}
