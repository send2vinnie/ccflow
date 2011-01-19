using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BP.YG;
using BP.En;
using BP.DA;
using BP.Web;

namespace BP.Web.YG.HiTax
{
	/// <summary>
	/// RegUser 的摘要说明。
	/// </summary>
	public partial class IRegUser :YGPage
	{
        public Button Btn_Submit
        {
            get
            {
                return this.UCPub1.GetButtonByID("Btn_Submit");
            }
        }
		protected void Page_Load(object sender, System.EventArgs e)
		{

            ////foreach (string s in this.Request.ServerVariables)
            ////{
            ////    this.Response.Write("<BR>" + s + " = ");
            ////    this.Response.Write("<B>" + this.Request.ServerVariables[s] + "</b>");
            ////}
            ////return;
        //    this.Request.ServerVariables[.


            //System.Net.IPHostEntry hostinf = Dns.GetHostByName(TxtDomain.Text);
            //TxtIp.Text = hostinf.AddressList[0].ToString();
            //IpLab.Text = IpLab.Text = "网络域名:" + TxtDomain.Text + "       对应的IP地址:" + TxtIp.Text; 

			this.BindRegUser();

			this.Btn_Submit.Click+=new EventHandler(Btn_Submit_Click);
		}
		public void BindRegUser()
		{
            string m = "<b><font color=red>*</font></b>";
			this.UCPub1.AddTableDef();
			this.UCPub1.AddTR();
            this.UCPub1.AddTDTitleDef("colspan=2", "如果您已经caishui800.cn用户，请点这里<U><a href='/Login.aspx?B=" + this.BureauNo + "' style='color:blue' >登陆系统</a></U>，<U><a href='/Home.aspx?B=" + this.BureauNo + "' style='color:blue' >返回主页</a></U>。");
			this.UCPub1.AddTREnd();

			this.UCPub1.AddTR();
			this.UCPub1.Add("<TD valign=top align=right><BR>用户名"+m+"：</TD>");
			TextBox tb = new TextBox();
			tb.ID="TB_No";
			tb.MaxLength=14;
            this.UCPub1.Add("<TD class=Note >");
			this.UCPub1.Add(tb);
			this.UCPub1.Add("<BR>他是您登陆本系统的帐号，也是您blog空间的帐号。<BR>不超过7个汉字或14个字节(数字，字母和下划线)</TD>");
			this.UCPub1.AddTREnd();

			this.UCPub1.AddTR();
			this.UCPub1.Add("<TD valign=top align=right>密码"+m+"：</TD>");
			tb = new TextBox();
			tb.ID="TB_Pass1";
			tb.MaxLength=15;
			tb.TextMode =TextBoxMode.Password;
			this.UCPub1.Add("<TD class=Note >");
			this.UCPub1.Add(tb);
			this.UCPub1.Add("<BR>请注意您的密码安全，2-15位。</TD>");
			this.UCPub1.AddTREnd();

			this.UCPub1.AddTR();
			this.UCPub1.Add("<TD align=right >重输密码"+m+"：</TD>");
			tb = new TextBox();
			tb.ID="TB_Pass2";
			tb.MaxLength=15;
			tb.TextMode =TextBoxMode.Password;

			this.UCPub1.Add("<TD>");
			this.UCPub1.Add(tb);
			this.UCPub1.Add("</TD>");
			this.UCPub1.AddTREnd();

			this.UCPub1.AddTR();
			this.UCPub1.Add("<TD valign=top align=right>邮件：</TD>");
			tb = new TextBox();
			tb.ID="TB_Mail";
			tb.MaxLength=100;
			tb.Width=300;
			this.UCPub1.Add("<TD class=Note>");
			this.UCPub1.Add(tb);
			this.UCPub1.Add("<BR>请真实填写，在您丢失密码时可以用它来找回，也可以接受来自税务局的通知与定阅网送税法。</TD>");
			this.UCPub1.AddTREnd();


            //this.UCPub1.AddTR();
            //this.UCPub1.Add("<TD valign=top align=right>来自：</TD>");
            //tb = new TextBox();
            //tb.ID = "TB_Addr";
            //tb.MaxLength = 100;
            //tb.Width = 300;
            //this.UCPub1.Add("<TD class=Note>");
            //this.UCPub1.Add(tb);
            //this.UCPub1.Add("<BR>比如：山东济南。填写好来源，方便系统给您提供地域化服务。</TD>");
            //this.UCPub1.AddTREnd();


			this.UCPub1.AddTR();
			this.UCPub1.Add("<TD valign=top align=right>称谓：</TD>");
			this.UCPub1.Add("<TD>");
			RadioButton rb = new RadioButton();
			rb.Text="先生";
			rb.ID="RB_1";
			rb.Checked=false;
			rb.GroupName="s";
			this.UCPub1.Add(rb);
			rb = new RadioButton();
			rb.Text="女士";
			rb.ID="RB_2";
			rb.Checked=true;
			rb.GroupName="s";
			this.UCPub1.Add(rb);
			this.UCPub1.Add("</TD>");
			this.UCPub1.AddTREnd();


			this.UCPub1.AddTR();
			this.UCPub1.Add("<TD></TD><TD>");
			Button btn = new Button();
			btn.Text="接受协议并创建用户";
			btn.ID="Btn_Submit";
            btn.CssClass = "Btn";

			this.UCPub1.Add(btn);
			this.UCPub1.Add("</TD>");
			this.UCPub1.AddTREnd();


			this.UCPub1.AddTR();
			this.UCPub1.Add("<TD></TD>");
			this.UCPub1.Add("<TD>");

			Label lab = new Label();
			lab.ID="Lab_Msg";
			lab.ForeColor=Color.Red;
			lab.Font.Bold=true;

			this.UCPub1.Add(lab);
			this.UCPub1.Add("</TD>");
			this.UCPub1.Add("<TD></TD>");
			this.UCPub1.AddTREnd();



            this.UCPub1.AddTR();
            this.UCPub1.Add("<TD valign=top align=right>许可协议：</TD>");
            tb = new TextBox();
            tb.ID = "TB_Leic";
            tb.MaxLength = 100;
            tb.Width = 500;
            tb.Rows = 10;
            tb.TextMode = TextBoxMode.MultiLine;
            tb.Text = "文明上网，信息发布真实有效。";

            this.UCPub1.Add("<TD class=Note>");
            this.UCPub1.Add(tb);
            this.UCPub1.AddTREnd();


			this.UCPub1.AddTableEnd();
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		private void Btn_Submit_Click(object sender, EventArgs e)
		{

            try
            {
                string no = this.UCPub1.GetTextBoxByID("TB_No").Text.Trim();
                if (no.Length <= 1)
                    throw new Exception("用户名长度必须等于2位数。");

                Customer c = new Customer();
                if (c.IsExit("No", no))
                    throw new Exception("很抱歉，用户名[" + no + "]，已经存在，请选择其它的用户名。");


                string mail = this.UCPub1.GetTextBoxByID("TB_Mail").Text.Trim();
                //if (mail.IndexOf("@") == -1
                //    || mail.Length == 0
                //    || mail.IndexOf(".") == -1 )
                //    throw new Exception("邮件错误。");

                string pass1 = this.UCPub1.GetTextBoxByID("TB_Pass1").Text.Trim();
                string pass2 = this.UCPub1.GetTextBoxByID("TB_Pass2").Text.Trim();

                if (pass1 != pass2)
                    throw new Exception("两次密码不一致。");

                if (pass1 == "" || pass1.Length == 0)
                    throw new Exception("密码不能为空。");

                c.No = no;
                c.Name = no;
                c.Email = mail;
                if (this.UCPub1.GetRadioButtonByID("RB_1").Checked)
                    c.SEX = 1;
                else
                    c.SEX = 2;

                c.ADT = DataType.CurrentDataTime;
                c.RDT = DataType.CurrentDataTime;
              //  c.Addr = this.UCPub1.GetTextBoxByID("TB_Addr").Text.Trim();
                c.Pass = pass1;
           //     c.FK_Bureau = Global.BureauNo;

                c.FK_Area = "37";

                c.RegFrom = "37";


                //string ip = this.Request.ServerVariables["REMOTE_ADDR"];
                //try
                //{
                //    //BP.CN.IP pp = new BP.CN.IP(ip);
                //    //c.FK_Area = pp.FK_Area;
                //}
                //catch (Exception ex)
                //{
                //    string fk_area = BP.CN.Area.GenerAreaNoByName(c.Addr, null);
                //    if (fk_area == null)
                //        throw new Exception("@系统无法判断您的来源，无法给您提供地区化服务，请填写好详细的地址。" + ex.Message);
                //    c.FK_Area = fk_area;
                //}

                c.Insert();

                Global.Signin(c,true);
                Global.Trade(CBuessType.CZ_Reg, "Reg", "注册用户赠送");
                string comefrom = this.Request.QueryString["WhereGo"];
                if (comefrom == null || comefrom.Contains("RegUser") || comefrom.Contains("Login.asp"))
                    comefrom = "Home.aspx";

                string url = this.Request.RawUrl;
                if (comefrom.IndexOf("?") != -1)
                    comefrom = comefrom + "&from=23&" + url.Substring(url.IndexOf("?"));
                else
                    comefrom = comefrom + "?from=23&";

                if (comefrom.IndexOf("Msg.aspx") != -1)
                    comefrom = "Home.aspx";

                this.ToUrl(comefrom);
            }
            catch (Exception ex)
            {
                this.UCPub1.GetLabelByID("Lab_Msg").Text = "注册期间出现如下错误：" + ex.Message;
            }

		}
	}
}
