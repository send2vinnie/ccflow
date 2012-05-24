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
			if(this.txtOperateName.Text.Trim().Length==0)
			{
				strErr+="OperateName不能为空！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			string No=this.txtNo.Text;
			string OperateName=this.txtOperateName.Text;

			BP.EIP.Model.Port_Operate model=new BP.EIP.Model.Port_Operate();
			model.No=No;
			model.OperateName=OperateName;

			BP.EIP.BLL.Port_Operate bll=new BP.EIP.BLL.Port_Operate();
			bll.Add(model);
			Lizard.Common.MessageBox.ShowAndRedirect(this,"保存成功！","add.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
