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
using System.Text;
using Lizard.Common;
using LTP.Accounts.Bus;
namespace Lizard.OA.Web.OA_SMS
{
    public partial class Modify : Page
    {       

        		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					string SmsId= Request.Params["id"];
					ShowInfo(SmsId);
				}
			}
		}
			
	private void ShowInfo(string SmsId)
	{
		BP.CCOA.OA_SMS bll=new BP.CCOA.OA_SMS();
		Lizard.OA.Model.OA_SMS model=bll.GetModel(SmsId);
		this.lblSmsId.Text=model.SmsId;
		this.txtSenderNumber.Text=model.SenderNumber;
		this.txtReciveNumber.Text=model.ReciveNumber;
		this.txtSendContent.Text=model.SendContent;
		this.txtReciveConent.Text=model.ReciveConent;
		this.txtSendTime.Text=model.SendTime.ToString();
		this.txtReciveTime.Text=model.ReciveTime.ToString();

	}

		public void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtSenderNumber.Text.Trim().Length==0)
			{
				strErr+="发送号码不能为空！\\n";	
			}
			if(this.txtReciveNumber.Text.Trim().Length==0)
			{
				strErr+="接收号码不能为空！\\n";	
			}
			if(this.txtSendContent.Text.Trim().Length==0)
			{
				strErr+="发送内容不能为空！\\n";	
			}
			if(this.txtReciveConent.Text.Trim().Length==0)
			{
				strErr+="接收内容不能为空！\\n";	
			}
			if(!PageValidate.IsDateTime(txtSendTime.Text))
			{
				strErr+="发送时间格式错误！\\n";	
			}
			if(!PageValidate.IsDateTime(txtReciveTime.Text))
			{
				strErr+="接收时间格式错误！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			string SmsId=this.lblSmsId.Text;
			string SenderNumber=this.txtSenderNumber.Text;
			string ReciveNumber=this.txtReciveNumber.Text;
			string SendContent=this.txtSendContent.Text;
			string ReciveConent=this.txtReciveConent.Text;
			DateTime SendTime=DateTime.Parse(this.txtSendTime.Text);
			DateTime ReciveTime=DateTime.Parse(this.txtReciveTime.Text);


			Lizard.OA.Model.OA_SMS model=new Lizard.OA.Model.OA_SMS();
			model.SmsId=SmsId;
			model.SenderNumber=SenderNumber;
			model.ReciveNumber=ReciveNumber;
			model.SendContent=SendContent;
			model.ReciveConent=ReciveConent;
			model.SendTime=SendTime;
			model.ReciveTime=ReciveTime;

			BP.CCOA.OA_SMS bll=new BP.CCOA.OA_SMS();
			bll.Update(model);
			Lizard.Common.MessageBox.ShowAndRedirect(this,"保存成功！","list.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
