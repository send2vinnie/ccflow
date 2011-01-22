using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
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
    public partial class RegUser : BP.Web.UC.UCBase3
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            this.AddFieldSet("易根网用户注册 - <a href='Login.aspx'>已经注册用直接登陆</a>");

            this.AddTable();

            TextBox tb = new TextBox();
            tb.ID = "TB_No";

            this.AddTR();
            this.AddTD("用户名");
            this.AddTD(tb);
            this.AddTD("不超过7个汉字，或14个字节(数字，字母和下划线)");
            this.AddTREnd();

            tb = new TextBox();
            tb.ID = "TB_Name";
            this.AddTR();
            this.AddTD("真实姓名");
            this.AddTD(tb);
            this.AddTD("不超过7个汉字，或14个字节(数字，字母和下划线)");
            this.AddTREnd();


            tb = new TextBox();
            tb.ID = "TB_Pass1";
            tb.TextMode = TextBoxMode.Password;
            this.AddTR();
            this.AddTD("密码");
            this.AddTD(tb);
            this.AddTD("密码长度6～14位，字母区分大小写。");
            this.AddTREnd();

            tb = new TextBox();
            tb.ID = "TB_Pass2";
            tb.TextMode = TextBoxMode.Password;
            this.AddTR();
            this.AddTD("确认密码");
            this.AddTD(tb);
            this.AddTD();
            this.AddTREnd();

            this.AddTR();
            this.Add("<TD valign=top align=right>称谓：</TD>");
            this.Add("<TD>");
            RadioButton rb = new RadioButton();
            rb.Text = "先生";
            rb.ID = "RB_1";
            rb.Checked = false;
            rb.GroupName = "s";
            this.Add(rb);
            rb = new RadioButton();
            rb.Text = "女士";
            rb.ID = "RB_2";
            rb.Checked = true;
            rb.GroupName = "s";
            this.Add(rb);
            this.Add("</TD>");
            this.AddTREnd();

            tb = new TextBox();
            tb.ID = "TB_QQMSN";
            this.AddTR();
            this.AddTD("QQ/MSN");
            this.AddTD(tb);
            this.AddTD("让您可以交到更多的易根朋友。");
            this.AddTREnd();


            tb = new TextBox();
            tb.ID = "TB_Email";
            this.AddTR();
            this.AddTD("Email");
            this.AddTD(tb);
            this.AddTD("请输入有效的邮件地址，当密码遗失时凭此领取");
            this.AddTREnd();

            this.AddTR();
            this.Add("<TD colspan=3>");
            Button btn = new Button();
            btn.Text = "接受协议并创建用户";
            btn.ID = "Btn_Submit";
            this.Add(btn);
            btn.Click += new EventHandler(btn_Click);
            this.AddTDEnd();
            this.AddTREnd();


            this.AddTR();
            this.Add("<TD colspan=3>");
            Label lab = new Label();
            lab.ID = "Lab_Msg";
            lab.ForeColor = Color.Red;
            lab.Font.Bold = true;

            this.Add(lab);
            this.Add("</TD>");
            this.AddTREnd();
            this.AddTableEnd();

            this.AddFieldSetEnd();// ("易根网用户注册");
        }

        void btn_Click(object sender, EventArgs e)
        {
            try
            {
                string no = this.GetTextBoxByID("TB_No").Text.Trim();
                if (no.Length <= 1)
                    throw new Exception("用户名长度必须等于2位数。");

                Member c = new Member();
                if (c.IsExit("No", no))
                    throw new Exception("很抱歉，用户名[" + no + "]，已经存在，请选择其它的用户名。");


                string mail = this.GetTextBoxByID("TB_Email").Text.Trim();
                string pass1 = this.GetTextBoxByID("TB_Pass1").Text.Trim();
                string pass2 = this.GetTextBoxByID("TB_Pass2").Text.Trim();

                if (pass1 != pass2)
                    throw new Exception("两次密码不一致。");

                if (pass1 == "" || pass1.Length == 0)
                    throw new Exception("密码不能为空。");

                c.No = no;
                c.Name = this.GetTextBoxByID("TB_Name").Text.Trim();
                c.QQ = this.GetTextBoxByID("TB_QQMSN").Text.Trim();

                c.Email = mail;
                if (this.GetRadioButtonByID("RB_1").Checked)
                    c.SEX = 1;
                else
                    c.SEX = 2;

                c.ADT = DataType.CurrentDataTime;
                c.RDT = DataType.CurrentDataTime;
                c.Pass = pass1;

                c.Insert();

                Glo.Signin(c, true);
                Glo.Trade(CBuessType.CZ_Reg, "Reg", "注册用户赠送");
                string comefrom = this.Request.QueryString["WhereGo"];
                if (comefrom == null || comefrom.Contains("RegUser") || comefrom.Contains("Login.asp"))
                    comefrom = "./../Home.aspx";

                string url = this.Request.RawUrl;
                if (comefrom.IndexOf("?") != -1)
                    comefrom = comefrom + "&from=23&" + url.Substring(url.IndexOf("?"));
                else
                    comefrom = comefrom + "?from=23&";

                if (comefrom.IndexOf("Msg.aspx") != -1)
                    comefrom = "./../Home.aspx";

                this.Response.Redirect(comefrom);
                //                this.ToUrl(comefrom);
            }
            catch (Exception ex)
            {
                this.GetLabelByID("Lab_Msg").Text = "注册期间出现如下错误：" + ex.Message;
            }
        }
    }
}