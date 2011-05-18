using System;
using System.Collections;
using System.Configuration;
using System.Data;
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
                int i2 = url.IndexOf(".aspx") - 6;
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
            "<link href='" + this.Request.ApplicationPath + "/Comm/Style/Table" + BP.Web.WebUser.Style + ".css' rel='stylesheet' type='text/css' />");

        //this.Page.RegisterClientScriptBlock("a",
        //   "<link href='" + this.Request.ApplicationPath + "/WF/Style/Menu" + BP.Web.WebUser.Style + ".css' rel='stylesheet' type='text/css' />");

         //this.Page.RegisterClientScriptBlock("d",
         //"<link href='" + this.Request.ApplicationPath + "/Comm/Style/t1/style.css' rel='stylesheet' type='text/css' />");
        
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
            this.Pub1.Add("<div  id='Top' /></div>");
            // this.Pub1.Add("<div  style='float:right' />&nbsp;" + BP.SystemConfig.SysName + "&nbsp;</div>");
            this.Page.RegisterClientScriptBlock("d",
            "<link href='" + this.Request.ApplicationPath + "/WF/Style/Skin/t" + BP.Web.WebUser.Style + "/style.css' rel='stylesheet' type='text/css' />");
            //    this.Pub1.Add("<Img src='./../DataUser/Title.gif' align=center onerror=\"src='./Style/TitleCCFlow.gif'\" >");
        }

       // this.Pub1.Add("<div style='float:left'>Hi:" + BP.Web.WebUser.No + BP.Web.WebUser.Name + "</div>");
        //this.Pub1.Add("<DIV ID=MainDiv>");
        //this.Pub1.Add("<UL id=MainUL>");

        this.Pub1.Add("<DIV ID=nv>");
    //    this.Pub1.Add("<a href='Tools.aspx' id='qmenu'  >我的中心</a>");

        this.Pub1.Add("<UL>");
        foreach (BP.WF.XML.ToolBar en in ens)
        {
            if (en.No == dotype)
            {
                if (en.No == "EmpWorks")
                    this.Pub1.Add("<li class=S ><a href='" + en.Url + "' target='_self' title='" + en.Title + "' ><span>" + msg + "</span></a></li>");
                else
                    this.Pub1.Add("<li class=S ><a href='" + en.Url + "' target='_self' title='" + en.Title + "' ><span>" + en.Name + "</span></a></li>");
            }
            else
            {
                if (en.No == "EmpWorks")
                    this.Pub1.Add("<li><a href='" + en.Url + "' target='_self' title='" + en.Title + "' ><span>" + msg + "</span></a></li>");
                else
                    this.Pub1.Add("<li><a href='" + en.Url + "' target='_self' title='" + en.Title + "' ><span>" + en.Name + "</span></a></li>");
            }
        }
       this.Pub1.AddLi("<a href=#  onmouseover='' >您好:" + BP.Web.WebUser.Name+"</a>" );
    //   this.Pub1.Add("<span style='float:right'>您好:" + BP.Web.WebUser.Name + "</span>");
        this.Pub1.Add("</UL>");
     //   this.Pub1.Add("<span style='float:right'>您好:" + BP.Web.WebUser.Name + "</span>");
       // this.Pub1.Add("<div style='float:right'>");

        
        this.Pub1.Add("</DIV>");
        #endregion 菜单输出区域


        #region 菜单输出区域 bak
        //string dotype = this.PageID;
        //if (BP.WF.Glo.IsShowTitle)
        //    this.Pub1.Add("<Img src='./../DataUser/Title.gif' align=center onerror=\"src='./Style/TitleCCFlow.gif'\" >");

        //this.Pub1.Add("<DIV align=center><UL id=main_nav align=center>");
        //this.Pub1.Add("<LI>Hi:" + BP.Web.WebUser.No + BP.Web.WebUser.Name + "</LI>");
        //foreach (BP.WF.XML.ToolBar en in ens)
        //{
        //    if (en.No == dotype)
        //    {
        //        if (en.No == "EmpWorks")
        //            this.Pub1.Add("<LI class=activetab><a href='" + en.Url + "' target='_self' title='" + en.Title + "' >" + msg + "</a></LI>");
        //        else
        //            this.Pub1.Add("<LI class=activetab><a href='" + en.Url + "' target='_self' title='" + en.Title + "' >" + en.Name + "</a></LI>");
        //    }
        //    else
        //    {
        //        if (en.No == "EmpWorks")
        //            this.Pub1.Add("<LI ><a href='" + en.Url + "' target='_self' title='" + en.Title + "' >" + msg + "</a></LI>");
        //        else
        //            this.Pub1.Add("<LI ><a href='" + en.Url + "' target='_self' title='" + en.Title + "' >" + en.Name + "</a></LI>");
        //    }
        //}
        //this.Pub1.Add("</UL>");
        //this.Pub1.Add("</DIV>");
        #endregion 菜单输出区域 bak

        return;

        //this.Pub1.Add("<div align=center ><Table width='500px' class=TTable ><TR>");
        //this.Pub1.Add("<TD width='10%' align=right nowarp=true ><a href='Tools.aspx' >Hi:" + BP.Web.WebUser.No + "</a></TD>");
        //this.Pub1.Add("<TD width='50%' align=right ></TD>");

        //string dotype = "";
        //foreach (BP.WF.XML.ToolBar en in ens)
        //{
        //    if (en.No == this.DoType)
        //    {
        //        this.Pub1.Add("<TD nowrap=true><img src='" + en.Img + "' border='0' ><b>" + en.Name + "</b></TD>");
        //    }
        //    else
        //    {
        //        this.Pub1.Add("<TD nowrap=true><a href='" + en.Url + "' target='_self' title='" + en.Title + "' ><img src='" + en.Img + "' border='0' >" + en.Name + "</a></TD>");
        //    }
        //}
        //this.Pub1.Add("<TD width='20%' ></TD>");
        //this.Pub1.Add("</TR>");
        //this.Pub1.Add("</Table></div><hr width='80%'>");
    }
}
