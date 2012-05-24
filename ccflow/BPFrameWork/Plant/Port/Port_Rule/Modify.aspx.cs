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
namespace BP.EIP.Web.Port_Rule
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
		BP.EIP.BLL.Port_Rule bll=new BP.EIP.BLL.Port_Rule();
		BP.EIP.Model.Port_Rule model=bll.GetModel(No);
		this.lblNo.Text=model.No;
		this.txtPermission.Text=model.Permission;
		this.txtRulePolicy.Text=model.RulePolicy;
		this.txtRuleGroup.Text=model.RuleGroup;
		this.txtDescription.Text=model.Description;
		this.txtPerfix.Text=model.Perfix;
		this.txtRuleType.Text=model.RuleType;
		this.txtFK_Domain.Text=model.FK_Domain;

	}

		public void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtPermission.Text.Trim().Length==0)
			{
				strErr+="需要控制的权限名称不能为空！\\n";	
			}
			if(this.txtRulePolicy.Text.Trim().Length==0)
			{
				strErr+="RulePolicy不能为空！\\n";	
			}
			if(this.txtRuleGroup.Text.Trim().Length==0)
			{
				strErr+="规则的分组不能为空！\\n";	
			}
			if(this.txtDescription.Text.Trim().Length==0)
			{
				strErr+="Description不能为空！\\n";	
			}
			if(this.txtPerfix.Text.Trim().Length==0)
			{
				strErr+="Perfix不能为空！\\n";	
			}
			if(this.txtRuleType.Text.Trim().Length==0)
			{
				strErr+="RuleType不能为空！\\n";	
			}
			if(this.txtFK_Domain.Text.Trim().Length==0)
			{
				strErr+="所属管理域不能为空！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			string No=this.lblNo.Text;
			string Permission=this.txtPermission.Text;
			string RulePolicy=this.txtRulePolicy.Text;
			string RuleGroup=this.txtRuleGroup.Text;
			string Description=this.txtDescription.Text;
			string Perfix=this.txtPerfix.Text;
			string RuleType=this.txtRuleType.Text;
			string FK_Domain=this.txtFK_Domain.Text;


			BP.EIP.Model.Port_Rule model=new BP.EIP.Model.Port_Rule();
			model.No=No;
			model.Permission=Permission;
			model.RulePolicy=RulePolicy;
			model.RuleGroup=RuleGroup;
			model.Description=Description;
			model.Perfix=Perfix;
			model.RuleType=RuleType;
			model.FK_Domain=FK_Domain;

			BP.EIP.BLL.Port_Rule bll=new BP.EIP.BLL.Port_Rule();
			bll.Update(model);
			Lizard.Common.MessageBox.ShowAndRedirect(this,"保存成功！","list.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
