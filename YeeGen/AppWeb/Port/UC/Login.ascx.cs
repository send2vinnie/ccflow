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
    public partial class Login : BP.Web.UC.UCBase3
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            TextBox tb = new TextBox();
            tb.ID = "TB_No";


            this.AddFieldSet("易根网用户登陆    <a href=RegUser.aspx >注册</a> - <a href='RequestPass.aspx'>找回密码</a>");

            this.AddTable();
            this.AddTR();
            this.AddTD("用户名");
            this.AddTD(tb);
            this.AddTREnd();


            tb = new TextBox();
            tb.ID = "TB_Pass";
            this.AddTR();
            this.AddTD("密码");
            this.AddTD(tb);
            this.AddTREnd();


            this.AddTR();
            this.Add("<TD colspan=2>");
            Button btn = new Button();
            btn.Text = "登陆易根";
            btn.ID = "Btn_Submit";
            this.Add(btn);
            btn.Click += new EventHandler(btn_Click);
            this.AddTDEnd();
            this.AddTREnd();

            this.AddTableEnd();
            this.AddFieldSetEnd();
        }

        void btn_Click(object sender, EventArgs e)
        {
            try
            {
                Member m = new Member();
                m.No = this.GetTextBoxByID("TB_No").Text;
                if (m.IsExits == false
                    || m.Pass.Equals(this.GetTextBoxByID("TB_Pass").Text))
                    throw new Exception("错误的用户名与密码");

                Glo.Signin(m, true);

                this.Response.Redirect("Default.aspx", true);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
            