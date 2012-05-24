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
namespace BP.EIP.Web.Port_FunctionOperate
{
    public partial class Add : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                       
        }

        		protected void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtNo.Text.Trim().Length==0)
			{
				strErr+="主键不能为空！\\n";	
			}
			if(this.txtFK_Function.Text.Trim().Length==0)
			{
				strErr+="所属功能ID不能为空！\\n";	
			}
			if(this.txtOperateName.Text.Trim().Length==0)
			{
				strErr+="操作名称不能为空！\\n";	
			}
			if(this.txtOperateDesc.Text.Trim().Length==0)
			{
				strErr+="功能描述不能为空！\\n";	
			}
			if(this.txtControl_Name.Text.Trim().Length==0)
			{
				strErr+="控件名称不能为空！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			string No=this.txtNo.Text;
			string FK_Function=this.txtFK_Function.Text;
			string OperateName=this.txtOperateName.Text;
			string OperateDesc=this.txtOperateDesc.Text;
			string Control_Name=this.txtControl_Name.Text;

			BP.EIP.Model.Port_FunctionOperate model=new BP.EIP.Model.Port_FunctionOperate();
			model.No=No;
			model.FK_Function=FK_Function;
			model.OperateName=OperateName;
			model.OperateDesc=OperateDesc;
			model.Control_Name=Control_Name;

			BP.EIP.BLL.Port_FunctionOperate bll=new BP.EIP.BLL.Port_FunctionOperate();
			bll.Add(model);
			Lizard.Common.MessageBox.ShowAndRedirect(this,"保存成功！","add.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
