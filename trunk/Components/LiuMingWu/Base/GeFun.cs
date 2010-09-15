using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace BP.GE
{
    public static class GeFun
    {
        public static string getIp()
        {
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                return System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(new char[] { ',' })[0];
            else
                return System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }

        public static void ShowMessage(Page page, string strKey, string strMsg)
        {
            if (!page.ClientScript.IsStartupScriptRegistered(strKey))
            {
                string strScript = "alert('" + strMsg + "')";
                page.ClientScript.RegisterStartupScript(page.GetType(), strKey, strScript, true);
            }
        }
    }
}
