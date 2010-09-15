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
using System.IO;
using System.Collections.Generic;
using BP.DA;
using BP.Edu;
using BP.Edu.TH;
using BP.Port;
using BP.En;
using BP.Web;
using BP.Web.Controls;

public partial class FAQ_Edit : WebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Pub1.Add("<table class='table_01'>");

        this.Pub1.AddTR();
        this.Pub1.Add("<th>当前章节</th>");
        Question q = new Question(this.RefOID);
        ZhangJie zhang = new ZhangJie(q.FK_ZJ);
        ZhangJie zj2 = new ZhangJie(q.FK_ZJ.Substring(0, q.FK_ZJ.Length - 2));
        Work w = new Work(q.FK_ZJ.Substring(0, q.FK_ZJ.LastIndexOf("_")));
        this.Pub1.AddTD(w.Name + "/" + zj2.Name + "/" + zhang.Name);
        this.Pub1.AddTREnd();

        this.Pub1.Add("<tr>");
        this.Pub1.Add("<th>标题</th>");

        TextBox tb1 = new TextBox();
        tb1.ID="TB_Title";
        tb1.Columns = 80;
        this.Pub1.Add("<td>");

        this.Pub1.Add(tb1);
        this.Pub1.Add("</td>");
        this.Pub1.Add("</tr>");
        this.Pub1.Add("<tr>");
        this.Pub1.Add("<th>描述</th>");
        TextBox tb2 = new TextBox();
        tb2.ID = "TB_Descs";
        tb2.Rows = 8;
        tb2.Columns = 61;  
        tb2.TextMode = TextBoxMode.MultiLine;
        this.Pub1.AddTD(tb2);
        this.Pub1.AddTREnd();



        this.Pub1.AddTR();
        this.Pub1.Add("<th>资源类型</th>");
        this.Pub1.AddTDBegin();
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
        this.Pub1.AddTDEnd();
        this.Pub1.AddTREnd();




        this.Pub1.Add("<tr>");
        this.Pub1.Add("<th>悬赏积分</th>");
        this.Pub1.Add("<td>");

        //积分
        DDL ddlcent = new DDL();
        ddlcent.ID = "DDL_Cent";
        ddlcent.Width = 200;
        for (int i = 0; i <= 15;i++ )
        {
            ddlcent.Items.Add(new ListItem(i.ToString() + "分", i.ToString()));
        }
        this.Pub1.Add(ddlcent);

        this.Pub1.AddTREnd();


        this.Pub1.AddTR();
        Button btn = new Button();
        btn.ID = "Btn_UpDate";
        btn.Text = "确认修改";
        btn.Click += new EventHandler(btn_Click);
        this.Pub1.AddTD();
        this.Pub1.Add("<td>");
        this.Pub1.Add(btn);
        this.Pub1.Add("</td>");

        this.Pub1.AddTREnd();
        this.Pub1.Add("</table>");
        //绑定数据
        GetData();
    }
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

        Question en = new Question(this.RefOID);
        en.Title = Pub1.GetTextBoxByID("TB_Title").Text;
        en.Descs = Pub1.GetTextBoxByID("TB_Descs").Text;
        en.Cent = Convert.ToInt32(Pub1.GetDDLByID("DDL_Cent").SelectedValue);
        en.FK_BType = Pub1.GetDDLByID("DDL_Type").SelectedValue;
        en.Update();
        this.Alert("修改成功");
        this.WinClose();
    }
    /// <summary>
    /// 绑定数据
    /// </summary>
    public void GetData()
    {
        Question q = new Question(this.RefOID);
        this.Pub1.GetTextBoxByID("TB_Title").Text= q.Title;
        this.Pub1.GetTextBoxByID("TB_Descs").Text = q.Descs;
        this.Pub1.GetDDLByID("DDL_Cent").SelectedValue = q.Cent.ToString();
        this.Pub1.GetDDLByID("DDL_Type").SelectedValue = q.FK_BType.ToString();
    }
}
