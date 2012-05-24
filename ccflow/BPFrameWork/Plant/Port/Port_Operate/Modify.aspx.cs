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
namespace BP.EIP.Web.Port_Operate
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
		BP.EIP.BLL.Port_Operate bll=new BP.EIP.BLL.Port_Operate();
		BP.EIP.Model.Port_Operate model=bll.GetModel(No);
		this.lblNo.Text=model.No;
		this.txtOperateName.Text=model.OperateName;

	}

		public void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtOperateName.Text.Trim().Length==0)
			{
				strErr+="OperateName不能为空！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			string No=this.lblNo.Text;
			string OperateName=this.txtOperateName.Text;


			BP.EIP.Model.Port_Operate model=new BP.EIP.Model.Port_Operate();
			model.No=No;
			model.OperateName=OperateName;

			BP.EIP.BLL.Port_Operate bll=new BP.EIP.BLL.Port_Operate();
			bll.Update(model);
			Lizard.Common.MessageBox.ShowAndRedirect(this,"保存成功！","list.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
