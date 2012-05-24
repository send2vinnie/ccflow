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
namespace BP.EIP.Web.Port_EmpDomain
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
				strErr+="No不能为空！\\n";	
			}
			if(this.txtFK_Emp.Text.Trim().Length==0)
			{
				strErr+="FK_Emp不能为空！\\n";	
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
			string No=this.txtNo.Text;
			string FK_Emp=this.txtFK_Emp.Text;
			string FK_Domain=this.txtFK_Domain.Text;

			BP.EIP.Model.Port_EmpDomain model=new BP.EIP.Model.Port_EmpDomain();
			model.No=No;
			model.FK_Emp=FK_Emp;
			model.FK_Domain=FK_Domain;

			BP.EIP.BLL.Port_EmpDomain bll=new BP.EIP.BLL.Port_EmpDomain();
			bll.Add(model);
			Lizard.Common.MessageBox.ShowAndRedirect(this,"保存成功！","add.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
