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
namespace Lizard.OA.Web.OA_Message
{
    public partial class Add : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                       
        }

        		protected void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(!PageValidate.IsNumber(txtMessageId.Text))
			{
				strErr+="主键Id格式错误！\\n";	
			}
			if(this.txtMessageName.Text.Trim().Length==0)
			{
				strErr+="消息名称（标题）不能为空！\\n";	
			}
			if(!PageValidate.IsNumber(txtMeaageType.Text))
			{
				strErr+="消息类型格式错误！\\n";	
			}
			if(!PageValidate.IsNumber(txtAuthor.Text))
			{
				strErr+="发布人格式错误！\\n";	
			}
			if(!PageValidate.IsDateTime(txtCreateTime.Text))
			{
				strErr+="发布时间格式错误！\\n";	
			}
			if(!PageValidate.IsDateTime(txtUpDT.Text))
			{
				strErr+="最后更新时间格式错误！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			int MessageId=int.Parse(this.txtMessageId.Text);
			string MessageName=this.txtMessageName.Text;
			int MeaageType=int.Parse(this.txtMeaageType.Text);
			int Author=int.Parse(this.txtAuthor.Text);
			DateTime CreateTime=DateTime.Parse(this.txtCreateTime.Text);
			DateTime UpDT=DateTime.Parse(this.txtUpDT.Text);
			bool Status=this.chkStatus.Checked;

			Lizard.OA.Model.OA_Message model=new Lizard.OA.Model.OA_Message();
			model.MessageId=MessageId;
			model.MessageName=MessageName;
			model.MeaageType=MeaageType;
			model.Author=Author;
			model.CreateTime=CreateTime;
			model.UpDT=UpDT;
			model.Status=Status;

			BP.CCOA.OA_Message bll=new BP.CCOA.OA_Message();
			bll.Add(model);
			Lizard.Common.MessageBox.ShowAndRedirect(this,"保存成功！","add.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
