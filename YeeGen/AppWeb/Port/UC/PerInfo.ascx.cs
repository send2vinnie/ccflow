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
    public partial class PerInfo : BP.Web.UC.UCBase3
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            this.AddFieldSet("编辑个人信息");

            this.AddTable();


            this.AddTR();
            this.AddTD("用户名");
            this.AddTD(Glo.MemberNo);
            this.AddTD("登陆帐号");
            this.AddTREnd();


            TextBox tb = new TextBox();
            tb = new TextBox();
            tb.ID = "TB_Name";
            this.AddTR();
            this.AddTD("真实姓名");
            this.AddTD(tb);
            this.AddTD("不超过7个汉字，或14个字节(数字，字母和下划线)");
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
            tb.ID = "TB_Email";
            this.AddTR();
            this.AddTD("Email");
            this.AddTD(tb);
            this.AddTD("请输入有效的邮件地址，当密码遗失时凭此领取");
            this.AddTREnd();



            tb = new TextBox();
            tb.ID = "TB_QQMSN";
            this.AddTR();
            this.AddTD("QQ/MSN");
            this.AddTD(tb);
            this.AddTD("让您可以交到更多的易根朋友。");
            this.AddTREnd();


            this.AddTR();
            this.Add("<TD colspan=3>");
            Button btn = new Button();
            btn.Text = " 保 存 ";
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
                c.No = Glo.MemberNo;
                c.RetrieveFromDBSources();

                c.No = no;
                c.Name = this.GetTextBoxByID("TB_Name").Text.Trim();
                c.Email = this.GetTextBoxByID("TB_Email").Text.Trim();
                c.QQ = this.GetTextBoxByID("TB_QQMSN").Text.Trim();
                if (this.GetRadioButtonByID("RB_1").Checked)
                    c.SEX = 1;
                else
                    c.SEX = 2;

                c.Update();
                this.Alert("保存成功。");
            }
            catch (Exception ex)
            {
                this.GetLabelByID("Lab_Msg").Text = "注册期间出现如下错误：" + ex.Message;
            }
        }
    }

}