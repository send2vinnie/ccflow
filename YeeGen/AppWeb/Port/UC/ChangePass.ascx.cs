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
using System.Drawing;
using BP.Port;
using BP.YG;
using BP.DA;

namespace BP.YG.WebUI.Port.UC
{
    public partial class ChangePass : BP.Web.UC.UCBase3
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            this.AddFieldSet("修改密码");

            this.AddTable();
            TextBox tb = new TextBox();
            tb.ID = "TB_P1";
            tb.TextMode = TextBoxMode.Password;
            this.AddTR();
            this.AddTD("老密码");
            this.AddTD(tb);
            this.AddTREnd();

            tb = new TextBox();
            tb.ID = "TB_P1";
            tb.TextMode = TextBoxMode.Password;
            this.AddTR();
            this.AddTD("新密码");
            this.AddTD(tb);
            this.AddTREnd();

            tb = new TextBox();
            tb.ID = "TB_P2";
            tb.TextMode = TextBoxMode.Password;
            this.AddTR();
            this.AddTD("重输一次");
            this.AddTD(tb);
            this.AddTREnd();


            this.AddTR();
            this.Add("<TD colspan=2>");
            Button btn = new Button();
            btn.Text = "执行修改";
            btn.ID = "Btn_Submit";
            this.Add(btn);
            btn.Click += new EventHandler(btn_Click);
            this.AddTDEnd();
            this.AddTREnd();

            this.AddTR();
            this.Add("<TD colspan=2>");
            Label lab = new Label();
            lab.ID = "Lab_Msg";
            lab.ForeColor = Color.Red;
            lab.Font.Bold = true;
            this.Add(lab);
            this.AddTDEnd();
            this.AddTREnd();
            this.AddTableEnd();
            this.AddFieldSetEnd();
        }

        void btn_Click(object sender, EventArgs e)
        {

            try
            {
                string pass1 = this.GetTextBoxByID("TB_P1").Text;
                string pass2 = this.GetTextBoxByID("TB_P2").Text;
                string pass3 = this.GetTextBoxByID("TB_P3").Text;
                if (pass2 != pass3)
                    throw new Exception("两次密码不一致。");


              
                Member m = new Member();
                m.No = Glo.MemberNo;
                m.RetrieveFromDBSources();

                if (m.Pass.Equals(pass1) == false)
                    throw new Exception("老密码错误");



                this.Clear();
                this.AddMsgGreen("提示", "密码修改成功。");
            }
            catch (Exception ex)
            {
                this.GetLabelByID("Lab_Msg").Text = "注册期间出现如下错误：" + ex.Message;
            }
        }
    }
}