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
using BP.Port;
using BP.YG;
using BP.DA;

namespace BP.YG.WebUI.Port.UC
{
    public partial class ReleaseDoc : BP.Web.UC.UCBase3
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.AddFieldSet("发布日志");

            this.AddTable();
            this.AddTR();
            this.AddTD("标题");
            TextBox tb = new TextBox();
            tb.ID = "TB_Name";
            this.AddTD(tb);
            this.AddTREnd();

            this.AddTR();
            this.AddTDBegin("colspan=2");
            tb = new TextBox();
            tb.ID = "TB_Doc";
            tb.TextMode = TextBoxMode.MultiLine;
            tb.Rows = 20;
            tb.Columns = 40;
            this.Add(tb);
            this.AddTDEnd();
            this.AddTREnd();

            this.AddTR();
            this.AddTD("关键字");
             tb = new TextBox();
            tb.ID = "TB_KeyWords";
            this.AddTD(tb);
            this.AddTREnd();

            this.AddTR();
            this.AddTD("");
            Button btn = new Button();
            btn.ID = "Btn_Save";
            btn.Text = "保存";
            btn.Click += new EventHandler(btn_Click);
            this.AddTD(btn);
            this.AddTREnd();
            this.AddTableEnd();

            this.AddFieldSetEnd();
        }

        void btn_Click(object sender, EventArgs e)
        {
            Doc en = new Doc();
            en.FK_Member = Glo.MemberNo;
            en.Name = this.GetTextBoxByID("TB_Name").Text;
            en.KeyWords = this.GetTextBoxByID("TB_KeyWords").Text;
            en.DocHtml = this.GetTextBoxByID("TB_Doc").Text;
            en.Insert();
            this.Alert("发布成功.");
        }
    }
}