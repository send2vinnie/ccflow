using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BP.DA;
using BP.Edu;
using BP.Edu.TH;
using BP.Port;
using BP.En;
using BP.Web.Controls;
using System.IO;
using BP.Edu.Res;

public partial class FAQ_Ask : BP.Web.WebPage
{
    private string zhangjie;


    public string Zhangjie
    {
        get
        {
            return zhangjie;
        }
        set
        {
            zhangjie = value;
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(BP.Edu.EduUser.No) || BP.Edu.EduUser.No.Contains("admin"))
            Response.Write("<script> window.parent.location.href='../Port/SignIn.aspx?Jurl="+Request.RawUrl+"';</script>");
        if (this.HiddenID.Value != null && this.HiddenID.Value != "")
        {
            zhangjie = this.HiddenID.Value;

        }
        this.Pub1.Add("<div class=\"showBox\">");

        this.Pub1.Add("<table class=\"tab_01\" width=\"100%\">");
        
        this.Pub1.Add("<tr>");
        this.Pub1.Add("<th>标题</th>");
        TextBox tb1 = new TextBox();
        tb1.ID = "TB_Title";
        tb1.Columns = 50;
        this.Pub1.Add("<td>");
        this.Pub1.Add(tb1);
        this.Pub1.Add("</td>");
        this.Pub1.Add("</tr>");

        this.Pub1.Add("<tr>");
        this.Pub1.Add("<th>描述</th>");
        TextBox tb2 = new TextBox();
        tb2.ID = "TB_Descs";
        tb2.Rows = 8;
        tb2.Columns = 50;
        tb2.TextMode = TextBoxMode.MultiLine;
        this.Pub1.Add("<td>");
        this.Pub1.Add(tb2);
        this.Pub1.Add("</td>");
        this.Pub1.Add("</tr>");

        this.Pub1.Add("<tr>");
        this.Pub1.Add("<th>资源类型</th>");
        this.Pub1.Add("<td>");
        //资源类型
        ResBTypes rbs = new ResBTypes();
        rbs.RetrieveAll();
        DDL ddltype = new DDL();
        ddltype.ID = "DDL_Type";
        ddltype.Width = 200;
        foreach (ResBType rb in rbs)
        {
            ddltype.Items.Add(new ListItem(rb.Name, rb.No));
        }
        this.Pub1.Add(ddltype);
        this.Pub1.Add("</td>");
        this.Pub1.Add("</tr>");

        this.Pub1.Add("<tr>");
        this.Pub1.Add("<th>悬赏积分</th>");
        this.Pub1.Add("<td>");
        //积分
        DDL ddlcent = new DDL();
        ddlcent.ID = "DDL_Cent";
        ddlcent.Width = 200;
        for (int i = 0; i <= 15; i++)
        {
            ddlcent.Items.Add(new ListItem(i.ToString() + "分", i.ToString()));
        }
        this.Pub1.Add(ddlcent);
        this.Pub1.Add("</td>");
        this.Pub1.Add("<td style=\"background:none\" >&nbsp;");
        this.Pub1.Add("</td>");
        this.Pub1.Add("</tr>");
        this.Pub1.Add("</table>");
        this.Pub1.Add("</div>");

        this.Pub1.Add("<div class=\"btnEnder\">");
        Button btn = new Button();
        btn.ID = "Btn_UpDate";
        btn.Text = "确认提交";
        btn.Click += new EventHandler(btn_Click);
        btn.CssClass = "btn_o_01";
        this.Pub1.Add(btn);
        this.Pub1.Add("</div>");

    }
    /// <summary>
    /// 提交请求
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void btn_Click(object sender, EventArgs e)
    {
        if (Pub1.GetTextBoxByID("TB_Title").Text != null && Pub1.GetTextBoxByID("TB_Title").Text != "")
        {
            if (Pub1.GetTextBoxByID("TB_Title").Text.Length > 30)
            {
                this.Alert("标题过长，限定在30字符内");
                return;
            }
        }
        else
        {
            this.Alert("标题为空");
            return;
        }
        if (Pub1.GetTextBoxByID("TB_Descs").Text != null && Pub1.GetTextBoxByID("TB_Descs").Text != "")
        {
            if (Pub1.GetTextBoxByID("TB_Descs").Text.Length < 10)
            {
                this.Alert("描述内容过少，应不少于10字符");
                return;
            }
            if (Pub1.GetTextBoxByID("TB_Descs").Text.Length > 499)
            {
                this.Alert("描述内容多，应不多于500字符");
                return;
            }
        }
        else
        {
            this.Alert("描述为空");
            return;
        }
        if(this.HiddenID.Value.Length<=0)
        {
            this.Alert("章节信息获取错误,请检查……");
            return;
        }
        string[] jies = zhangjie.Split('_');

        Question en = new Question();
        en.Title = this.Pub1.GetTextBoxByID("TB_Title").Text;
        en.Descs = this.Pub1.GetTextBoxByID("TB_Descs").Text;
        en.Cent = Convert.ToInt32(this.Pub1.GetDDLByID("DDL_Cent").SelectedValue);
        en.RDT = DataType.CurrentDataTime;
        en.FK_Dept = BP.Edu.EduUser.FK_Dept;
        en.FK_KM = jies[0];
        en.FK_NJ = jies[1];
        en.FK_Ver = jies[2];
        en.FK_ZJ = zhangjie;
        en.FK_Work = EduUser.CurrWorkStr;
        en.FK_BType = this.Pub1.GetDDLByID("DDL_Type").SelectedValue;
        en.Insert();
        //次数累加
        try
        {
            PerEmp.FAQCounts();
        }
        catch (Exception ex)
        {
            //ex.Message;

        }
        string url = "../FAQ/DefaultFAQ.aspx?RefOID=" + en.OID;
        Glo.Trade("FAQ_Ask", "", en.OID.ToString(), en.Title, url);//更新日志
        Actives.Trade("FAQ_Ask", en.Title, en.OID.ToString(), url);//个人中心使用
        //this.Response.Redirect("ShowMsg.aspx?DoType=Ask&RefOID=" + en.OID, true);
        this.WinClose();
    }
}
