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

public partial class FAQ_KMGuide : System.Web.UI.Page
{
    public string Type
    {
        get
        {
            string s = this.Request.QueryString["Type"];
            //if (s == null)
            //    s = "";
            return s;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //this.Pub1.Add("您的位置：资源发布导航=><a href='KMGuide.aspx'>选择科目</a>=>选择册别");

        if (this.Type == null)
        {
            this.Pub1.Add("<div id=\"open_wrapper\">");
            this.Pub1.Add("<h2 class=\"ttlm_share_01\"><img style=\"margin-right:100px;\" src=\"../Style/Img/openwindow/ttsl_navi.gif\" alt=\"资源发布导航\" /><img src=\"../Style/Img/openwindow/img_step_01_on.gif\" alt=\"选择科目\" /><img src=\"../Style/Img/openwindow/img_step_02.gif\" alt=\"选择册别\" /><img src=\"../Style/Img/openwindow/img_step_03.gif\" alt=\"选择章节\" /></h2>");
            // this.Pub1.Add("您的位置：资源发布导航=><b>选择科目</b>=>选择册别=>选择章节");

            BindKMGuide();
            this.Pub1.Add("</div>");
        }
        else
        {
            this.Pub1.Add("<div id=\"open_wrapper\">");
            this.Pub1.Add("<h2 class=\"ttlm_share_01\"><img style=\"margin-right:100px;\" src=\"../Style/Img/openwindow/ttsl_navi.gif\" alt=\"资源发布导航\" /><a href=\"KMGuide.aspx\"><img src=\"../Style/Img/openwindow/img_step_01.gif\" alt=\"选择科目\" /></a><img src=\"../Style/Img/openwindow/img_step_02_on.gif\" alt=\"选择册别\" /><img src=\"../Style/Img/openwindow/img_step_03.gif\" alt=\"选择章节\" /></h2>");
            //this.Pub1.Add("您的位置：资源发布导航=><a href='KMGuide.aspx'>选择科目</a>=><b>选择册别</b>=>选择章节");
            BindNJCB();
            this.Pub1.Add("</div>");
        }
    }

    private void BindNJCB()
    {
        KM km = new KM(this.Type);
        KMNJs njs = km.HisKMNJs;
        //Vers vers = new Vers();
        //vers.RetrieveAll();

        KMVers vers = new KMVers();
        vers.Retrieve(KMVerAttr.FK_KM, this.Type);
        this.Pub1.Add("<div id=\"open_wrapper\">");
        foreach (KMVer ver in vers)
        {

            this.Pub1.Add("<h3 class=\"ttls_jie\">" + ver.FK_VerT + " - " + km.Name + "</h3>");
            //this.Pub1.AddHR();

            this.Pub1.Add("<table class=\"tab_03\" width=\"100%\">");
            int i = 0;
            foreach (KMNJ nj in njs)
            {
                KMCBs cbs = km.HisKMCBs;

                if (i == 0)
                    this.Pub1.Add("<tr>");
                foreach (KMCB cb in cbs)
                {
                    this.Pub1.Add("<td>");
                    this.Pub1.Add("<a href='ZJGuide.aspx?s=" + this.Type + "_" + nj.FK_NJ + "_" + ver.FK_Ver + "_" + cb.FK_CB + "'> " + nj.FK_NJText + "." + cb.FK_CBT + "</a>");
                    this.Pub1.Add("</td>");
                    i++;
                    if (i == 4)
                    {
                        this.Pub1.Add("</tr>");
                        i = 0;
                    }
                }


            }

            switch (i)
            {
                case 1:
                    this.Pub1.Add("<td>");
                    this.Pub1.Add("<td>");
                    this.Pub1.Add("<td>");
                    this.Pub1.Add("</tr>");
                    break;
                case 2:
                    this.Pub1.Add("<td>");
                    this.Pub1.Add("<td>");
                    this.Pub1.Add("</tr>");
                    break;
                case 3:
                    this.Pub1.Add("<td>");
                    this.Pub1.Add("</tr>");
                    break;
                default:
                    break;
            }
            this.Pub1.Add("</table>");
        }
        this.Pub1.Add("<div class=\"btnEnder\"><a class=\"btn_link\" href=\"KMGuide.aspx\">上一步</a></div>");
        this.Pub1.Add("</div>");
        //this.Pub1.AddTable("Style='width:100%'");
        //this.Pub1.AddTR();
        //this.Pub1.AddTD("align=center", "<a href='KMGuide.aspx'>上一步</a>");
    }

    private void BindKMGuide()
    {
        SysEnums ses = new SysEnums("XD");
        KMs kms = new KMs();
        kms.RetrieveAllFromDBSource();

        foreach (SysEnum se in ses)
        {
            this.Pub1.Add("<h3 class=\"ttls_jie\">" + se.Lab + "</h3>");
            //this.Pub1.AddHR();

            int idx = 0;
            this.Pub1.Add("<table class=\"tab_03\" width=\"100%\">");
            foreach (KM km in kms)
            {

                if (km.XD != se.IntKey)
                    continue;

                if (idx == 0)
                    this.Pub1.Add("<tr>");


                this.Pub1.Add("<td><a href='KMGuide.aspx?Type=" + km.No + "'>" + km.Name + "</a></td>");

                idx++;
                if (idx == 4)
                {
                    this.Pub1.Add("</tr>");
                    idx = 0;
                }
            }


            switch (idx)
            {
                case 1:
                    this.Pub1.Add("<td>");
                    this.Pub1.Add("<td>");
                    this.Pub1.Add("<td>");
                    this.Pub1.Add("</tr>");
                    break;
                case 2:
                    this.Pub1.Add("<td>");
                    this.Pub1.Add("<td>");
                    this.Pub1.Add("</tr>");
                    break;
                case 3:
                    this.Pub1.Add("<td>");
                    this.Pub1.Add("</tr>");
                    break;
                default:
                    break;
            }

            this.Pub1.Add("</table>");

        }



    }
}
