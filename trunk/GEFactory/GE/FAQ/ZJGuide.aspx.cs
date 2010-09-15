using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BP.DA;
using BP.Edu;
using BP.Edu.TH;
using BP.Port;
using BP.En;
using BP.Sys;

public partial class FAQ_ZJGuide : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string s = this.Request.QueryString["s"];
        string[] ss = s.Split('_');
        this.Pub1.Add("<div id=\"open_wrapper\">");
        this.Pub1.Add("<h2 class=\"ttlm_share_01\"><img style=\"margin-right:100px;\" src=\"../Style/Img/openwindow/ttsl_navi.gif\" alt=\"资源请求导航\" /><a href=\"KMGuide.aspx\"><img src=\"../Style/Img/openwindow/img_step_01.gif\" alt=\"选择科目\" /></a><a href='KMGuide.aspx?Type=" + ss[0] + "'><img src=\"../Style/Img/openwindow/img_step_02.gif\" alt=\"选择册别\" /></a><img src=\"../Style/Img/openwindow/img_step_03_on.gif\" alt=\"选择章节\" /></h2>");
        this.Pub1.Add("您的位置：资源请求导航=><a href='KMGuide.aspx'>选择科目</a>=><a href='KMGuide.aspx?Type=" + ss[0] + "'>选择册别</a>=><b>选择章节</b>");
        this.Pub1.Add("<table class=\"tab_04\"  width=\"100%\">");
        this.Pub1.Add("<tr>");
        //this.Pub1.Add("序");
        this.Pub1.Add("<th width=\"30%\">章</th>");
        this.Pub1.Add("<th width=\"70%\">节</th>");
        //this.Pub1.AddTDTitle("确定");

        this.Pub1.Add("</tr>");

        ZhangJies ens = new ZhangJies();

        ens.Retrieve(ZhangJieAttr.FK_Work, s, ZhangJieAttr.GradeNo);




        bool flag = false;
        ZhangJie zhang = new ZhangJie();
        int i = 0;
        foreach (ZhangJie en in ens)
        {
            if (en.GradeNo.Length == 2)
            {
                zhang = en;
                flag = true;
                continue;
            }

            this.Pub1.Add("<tr>");
            //this.Pub1.AddTRTX();
            //i++;
            //this.Pub1.Add("<td style='text-align:center'>" + i + "</td>");
            if (flag)
            {
                this.Pub1.Add("<td>" + zhang.Name + "</td>");
                flag = false;
            }
            else
                this.Pub1.AddTD("");
            Work wk = new Work(s);
            this.Pub1.Add("<td><a href=\"javascript:Test('" + en.MyPK + "','" + wk.Name + zhang.Name + en.Name + "') \">" + en.Name + "</a></td>");



            //this.Pub1.AddTD("align=center", "<a href=\"javascript:Test('" + en.MyPK +"','"+wk.Name+zhang.RealName+en.RealName+ "') \">确定</a>");
            //this.Pub1.AddTD("align=center", "<a href=\"javascript:DoRelM('" + en.MyPK + "') \">批量共享</a>");

            this.Pub1.AddTREnd();
        }
        this.Pub1.Add("</table>");
        this.Pub1.Add("<div class=\"btnEnder\"><a class=\"btn_link\" href='KMGuide.aspx?Type=" + ss[0] + "'>上一步</a></div>");
        this.Pub1.Add("</div>");
        //this.Pub1.AddTD("colspan=5 align=center", "<a href='KMGuide.aspx?Type=" + ss[0] + "'>上一步</a>");
        //this.Pub1.AddTREnd();
        //this.Pub1.AddTableEnd();
    }
}
