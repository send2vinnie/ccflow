using System;
using System.Collections;
using System.Configuration;
using System.Data;
using BP;
using System.Web;
using System.Web.Security;
using System.Web.UI; 
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
public partial class Face_MasterPage : BP.Web.MasterPage
{
    private string _pageID = null;
    public string PageID
    {
        get
        {
            if (_pageID == null)
            {
                string url = System.Web.HttpContext.Current.Request.RawUrl;
                int i = url.LastIndexOf("/") + 1;
                int i2 = url.IndexOf(".aspx") - 5;
                try
                {
                    url = url.Substring(i);
                    _pageID = url.Substring(0, url.IndexOf(".aspx"));
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + url + " i=" + i + " i2=" + i2);
                }
            }
            return _pageID;
        }
    }
    /// <summary>
    /// DoType
    /// </summary>
    public string DoType
    {
        get
        {
            return "";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.RegisterClientScriptBlock("s",
            "<link href='" + this.Request.ApplicationPath + "/DataUser/Style/Table" + BP.Web.WebUser.Style + ".css' rel='stylesheet' type='text/css' />");


        if (this.Request.RawUrl.ToLower().Contains("login.aspx") == false)
        {
            if (BP.Web.WebUser.No == null)
            {
                this.Response.Redirect("Login.aspx", true);
                return;
            }
        }

        Response.AddHeader("P3P", "CP=CAO PSA OUR");
        Response.AddHeader("Cache-Control", "no-store");
        Response.AddHeader("Expires", "0");
        Response.AddHeader("Pragma", "no-cache");

        BP.WF.XML.ToolBars ens = new BP.WF.XML.ToolBars();
        ens.RetrieveAll();

        string sql = "SELECT COUNT(*) AS Num FROM WF_EmpWorks WHERE FK_Emp='" + BP.Web.WebUser.No + "'";
        int num = BP.DA.DBAccess.RunSQLReturnValInt(sql);
        string msg = this.ToE("PendingWork", "待办");
        if (num != 0)
        {
            msg = "<div id='blink'>" + this.ToE("PendingWork", "待办") + "-" + num + "</div>";
            string script = "";
            script += "<script language=javascript>";
            script += "function changeColor(){";
            script += " var color='#f00|#0f0|#00f|#880|#808|#088|yellow|green|blue|gray';";
            script += " color=color.split('|'); ";
            script += " document.getElementById('blink').style.color=color[parseInt(Math.random() * color.length)] ";
            script += " }";
            script += " setInterval('changeColor()',200);";
            script += "</script> ";
            this.Page.ClientScript.RegisterClientScriptBlock(typeof(string), "s", script);
        }
        #region 菜单输出区域


        string dotype = this.PageID;
        if (BP.WF.Glo.IsShowTitle)
        {
            this.Pub1.Add("<DIV class='wp' >");
            this.Pub1.Add("<div  id='Top' class='top' /><h2><img src='" + this.Request.ApplicationPath + "/DataUser/ICON/" + SystemConfig.CompanyID + "/LogBiger.png'  border=0 /></h2></div>");
        }

        this.Page.RegisterClientScriptBlock("d",
           "<link href='" + this.Request.ApplicationPath + "/DataUser/Style/Skin/T" + BP.Web.WebUser.Style + "/Style.css' rel='stylesheet' type='text/css' />");

        this.Pub1.Add("<DIV ID=nv class='nv' >");
        this.Pub1.Add("<UL>");
        foreach (BP.WF.XML.ToolBar en in ens)
        {
            if (en.No == dotype)
            {
                if (en.No == "EmpWorks")
                    this.Pub1.Add("<li class=current ><a href=\"" + en.Url + "\" target='_self' title='" + en.Title + "' ><span>" + msg + "</span></a></li>");
                else
                    this.Pub1.Add("<li class=current ><a href=\"" + en.Url + "\" target='_self' title='" + en.Title + "' ><span>" + en.Name + "</span></a></li>");
            }
            else
            {
                if (en.No == "EmpWorks")
                    this.Pub1.Add("<li><a href=\"" + en.Url + "\" target='_self' title='" + en.Title + "' ><span>" + msg + "</span></a></li>");
                else
                    this.Pub1.Add("<li><a href=\"" + en.Url + "\" target='_self' title='" + en.Title + "' ><span>" + en.Name + "</span></a></li>");
            }
        }
        this.Pub1.Add("</UL> <div style='float:right;margin-right:30px;display:inline-block;line-height:35px;color:white' >您好:"+BP.Web.WebUser.No+","+BP.Web.WebUser.Name+"</div>");
        this.Pub1.Add("</DIV>");

        if (BP.WF.Glo.IsShowTitle)
        {
            this.Pub1.Add("</DIV>");
        }
        #endregion 菜单输出区域


        return;
         
    }
}
