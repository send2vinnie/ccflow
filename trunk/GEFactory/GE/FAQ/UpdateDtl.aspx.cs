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
using System.IO;

public partial class FAQ_UpdateDtl : BP.Web.WebPage
{
    string mypk = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        QDtl qd = new QDtl();
        qd.Retrieve(QDtlAttr.MyPK,RefNo);
        Question q=new Question();
        QueryObject qo=new QueryObject(q);
        qo.AddWhere(QuestionAttr.OID,this.RefOID);
        int num=qo.DoQuery();
        if (num==0)
        {
            this.Alert("此问题已删除……");
            return;
        }
        this.Pub1.Add("<table class='table_01'>");
        this.Pub1.Add("<tr>");
        this.Pub1.Add("<th>内容</th>");
        TextBox tb = new TextBox();
        tb.ID = "TB_Doc";
        tb.Rows = 10;
        tb.Columns = 65;
        tb.TextMode = TextBoxMode.MultiLine;
        tb.Text = qd.Doc;
        this.Pub1.AddTD(tb);
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();

        Button btn = new Button();
        btn.ID = "Btn1";
        btn.Text = "提交内容";
        btn.Click+=new EventHandler(btn_Click);
        this.Pub1.AddTD("colspan=2  align=center",btn);
        this.Pub1.AddTREnd();

        this.Pub1.AddTableEnd();
    }
    void btn_Click(object sender, EventArgs e)
    {
        QDtl qd = new QDtl();
        int num = qd.Retrieve(QDtlAttr.MyPK, RefNo);
        if (num <= 0)
        {
            this.Alert("数据被删除……请重试");
            return;
        }
        string doc = this.Pub1.GetTextBoxByID("TB_Doc").Text.ToString();
        if (doc.Length > 500)
        {
            this.Alert("请限制在500个字符内！");
            return;
        }

        qd.Doc = this.Pub1.GetTextBoxByID("TB_Doc").Text.ToString();
        qd.Update();
        this.Alert("更新成功");
        this.WinClose();
    }
}
