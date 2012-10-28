using System;
using System.Web;
using System.Web.UI;

namespace BP.CY
{
    public class Page
    {
        /// <summary>
        /// 弹出信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="message"></param>
        public static void MessageBox(System.Web.UI.Page page, string message)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "", "alert('" + message + "');", true);
        }
    }
}
