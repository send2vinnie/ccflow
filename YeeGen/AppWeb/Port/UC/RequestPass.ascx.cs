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
    public partial class RequestPass : BP.Web.UC.UCBase3
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            this.AddFieldSet("获取易根网用户密码 - <a href='RegUser.aspx'>注册</a>-<a href=Login.aspx>登陆</a>");

            this.AddTable();
            TextBox tb = new TextBox();
            tb.ID = "TB_No";
            this.AddTR();
            this.AddTD("请输入您的易根网帐号");
            this.AddTD(tb);
            this.AddTREnd();

            this.AddTR();
            this.Add("<TD colspan=2>");
            Button btn = new Button();
            btn.Text = "请将密码发送我的邮箱里";
            btn.ID = "Btn_Submit";
            this.Add(btn);
            btn.Click += new EventHandler(btn_Click);
            this.AddTDEnd();
            this.AddTREnd();

            this.AddTRSum();
            this.AddTD("colspan=2","如果您注册时间没有正确的填写邮件地址，您将无法获得密码。");
            this.AddTREnd();


            this.AddTR();
            this.Add("<TD colspan=3>");
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
                Member m = new Member();
                m.No = this.GetTextBoxByID("TB_No").Text;
                if (m.IsExits == false)
                    throw new Exception("@用户编号错误。");

                this.Clear();
                this.AddMsgGreen("提示", "信息已经发送，请查收您的邮件。");
            }
            catch (Exception ex)
            {
                this.GetLabelByID("Lab_Msg").Text = "注册期间出现如下错误：" + ex.Message;
            }
        }
    }
}