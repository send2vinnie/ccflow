using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

using Discuz.Toolkit;
using BP.CY;

/// <summary>
///DiscuzSessionHelper 的摘要说明
/// </summary>
public class DiscuzSessionHelper
{
    private static DiscuzSession ds;
    private static string api_key = string.Empty;
    private static string secret = string.Empty;
    private static string url = string.Empty;

    public DiscuzSessionHelper()
    {

    }

    public static DiscuzSession GetSession()
    {
        Dictionary<string, string> dicBBS = Config.ParseDic(ConfigurationManager.AppSettings["BBSConfig"]);

        url = dicBBS.ContainsKey("url") ? dicBBS["url"].Trim() : string.Empty;
        api_key = dicBBS.ContainsKey("api_key") ? dicBBS["api_key"].Trim() : string.Empty;
        secret = dicBBS.ContainsKey("secret") ? dicBBS["secret"].Trim() : string.Empty;

        object lockObj = new object();

        if (ds == null)
        {
            lock (lockObj)
            {
                ds = new DiscuzSession(api_key, secret, url);
            }
        }
        else
        {
            try
            {
                ds.session_info = ds.GetSessionFromToken(HttpContext.Current.Session["AuthToken"].ToString());
            }
            catch (Exception ex)
            {
                ds.session_info = null;
            }
        }

        return ds;
    }
}
