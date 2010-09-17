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

public partial class Face_MasterPage : System.Web.UI.MasterPage
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
    public string DoType
    {
        get
        {
            return "";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.AddHeader("P3P", "CP=CAO PSA OUR");

        BP.WF.XML.ToolBars ens = new BP.WF.XML.ToolBars();
        ens.RetrieveAll();

       
        
//  <LI><A href="http://app.javaeye.com/profile">个人资料</A> </LI>
//  <LI class=activetab><A href="http://app.javaeye.com/password">修改密码</A> </LI>
//  <LI><A href="http://app.javaeye.com/email">注册邮箱</A> </LI></UL>
//</DIV>

        string sql = "SELECT COUNT(*) AS Num FROM WF_EmpWorks WHERE FK_Emp='" + BP.Web.WebUser.No + "'";
        int num = BP.DA.DBAccess.RunSQLReturnValInt(sql);
        string msg = "待办";
        if (num != 0)
        {
            msg = "<div id='blink'>待办-" + num + "</div>";
            string script = "";
            script += "<script language=javascript>";
            script += "function changeColor(){";
            script += " var color='#f00|#0f0|#00f|#880|#808|#088|yellow|green|blue|gray';";
            script += " color=color.split('|'); ";
            script += " document.getElementById('blink').style.color=color[parseInt(Math.random() * color.length)] ";
            script += " }";
            script += " setInterval('changeColor()',200);";
            script += "</script> ";
            this.Page.RegisterClientScriptBlock("s", script);
        }
       

        string dotype = this.PageID;
        this.Pub1.Add("<Img src='./Style/Title.gif' align=center onerror=\"src='./Style/TitleCCFlow.gif'\" >");

        this.Pub1.Add("<DIV align=center><UL id=main_nav align=center>");
        this.Pub1.Add("<LI>Hi:"+BP.Web.WebUser.No+ BP.Web.WebUser.Name+"</LI>");
         

        foreach (BP.WF.XML.ToolBar en in ens)
        {
            if (en.No == dotype)
            {
                if (en.No == "EmpWorks")
                    this.Pub1.Add("<LI class=activetab><a href='" + en.Url + "' target='_self' title='" + en.Title + "' >" + msg + "</a></LI>");
                else
                    this.Pub1.Add("<LI class=activetab><a href='" + en.Url + "' target='_self' title='" + en.Title + "' >" + en.Name + "</a></LI>");

                // this.Pub1.Add("<TD nowrap=true><img src='" + en.Img + "' border='0' class=ImgIcon ><b>" + en.Name + "</b></TD>"); <img src='" + en.Img + "' border='0' height='9px' width='9px' >
            }
            else
            {
                // this.Pub1.Add("<LI><A href=http://app.javaeye.com/profile">个人资料</A> </LI>
                if (en.No == "EmpWorks")
                    this.Pub1.Add("<LI ><a href='" + en.Url + "' target='_self' title='" + en.Title + "' >" + msg + "</a></LI>");
                else
                    this.Pub1.Add("<LI ><a href='" + en.Url + "' target='_self' title='" + en.Title + "' >" + en.Name + "</a></LI>");
            }
        }
        this.Pub1.Add("</UL><DIV>");

        //this.Pub1.Add("<TD width='20%' ></TD>");
        //this.Pub1.Add("</TR>");
        //this.Pub1.Add("</Table></div><hr width='80%'>");
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
