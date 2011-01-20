using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BP.YG;

namespace Tax666.AppWeb
{
    public partial class TouGao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Pub1.AddTable();
            this.Pub1.AddTR();
            this.Pub1.AddTDTitle("项目");
            this.Pub1.AddTDTitle("输入");
            this.Pub1.AddTDTitle("说明");
            this.Pub1.AddTREnd();

            this.Pub1.AddTR();
            this.Pub1.AddTD("稿件名称");
            TextBox tb = new TextBox();
            tb.ID = "TB_Name";
            this.Pub1.AddTD(tb);
            this.Pub1.AddTD("输入名称");
            this.Pub1.AddTREnd();



            this.Pub1.AddTR();
            this.Pub1.AddTD("作者");
            tb = new TextBox();
            tb.ID = "TB_Author";
            this.Pub1.AddTD(tb);
            this.Pub1.AddTD("作者ssss");
            this.Pub1.AddTREnd();



            this.Pub1.AddTR();
            this.Pub1.AddTD();
            Button btn = new Button();
            btn.ID = "Btn_Save";
            btn.Text = "投稿";
            btn.Click += new EventHandler(btn_Click);
            this.Pub1.AddTD(btn);
            this.Pub1.AddTD();
            this.Pub1.AddTREnd();
            this.Pub1.AddTableEnd();
        }

        void btn_Click(object sender, EventArgs e)
        {
            BP.YG.TouGao en = new BP.YG.TouGao();
            en.CheckPhysicsTable();

          //  en = this.Pub1.Copy(en) as BP.YG.TouGao;

            en.Name = this.Pub1.GetTextBoxByID("TB_Name").Text;
            en.Author = this.Pub1.GetTextBoxByID("TB_Author").Text;
            en.Insert();

            this.Response.Write(" run ok.....");
        }
    }
}
