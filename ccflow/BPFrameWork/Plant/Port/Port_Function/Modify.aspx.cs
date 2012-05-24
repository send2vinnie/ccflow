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
namespace BP.EIP.Web.Port_Function
{
    public partial class Modify : Page
    {       

        		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					string No= Request.Params["id"];
					ShowInfo(No);
				}
			}
		}
			
	private void ShowInfo(string No)
	{
		BP.EIP.BLL.Port_Function bll=new BP.EIP.BLL.Port_Function();
		BP.EIP.Model.Port_Function model=bll.GetModel(No);
		this.lblNo.Text=model.No;
		this.txtFunctionName.Text=model.FunctionName;
		this.txtFunctionDesc.Text=model.FunctionDesc;
		this.txtClassName.Text=model.ClassName;
		this.txtFK_Domain.Text=model.FK_Domain;

	}

		public void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtFunctionName.Text.Trim().Length==0)
			{
				strErr+="功能名称不能为空！\\n";	
			}
			if(this.txtFunctionDesc.Text.Trim().Length==0)
			{
				strErr+="功能描述不能为空！\\n";	
			}
			if(this.txtClassName.Text.Trim().Length==0)
			{
				strErr+="类名不能为空！\\n";	
			}
			if(this.txtFK_Domain.Text.Trim().Length==0)
			{
				strErr+="FK_Domain不能为空！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			string No=this.lblNo.Text;
			string FunctionName=this.txtFunctionName.Text;
			string FunctionDesc=this.txtFunctionDesc.Text;
			string ClassName=this.txtClassName.Text;
			string FK_Domain=this.txtFK_Domain.Text;


			BP.EIP.Model.Port_Function model=new BP.EIP.Model.Port_Function();
			model.No=No;
			model.FunctionName=FunctionName;
			model.FunctionDesc=FunctionDesc;
			model.ClassName=ClassName;
			model.FK_Domain=FK_Domain;

			BP.EIP.BLL.Port_Function bll=new BP.EIP.BLL.Port_Function();
			bll.Update(model);
			Lizard.Common.MessageBox.ShowAndRedirect(this,"保存成功！","list.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
