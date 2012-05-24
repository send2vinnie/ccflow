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
namespace BP.EIP.Web.Port_MenuOperate
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
			if(this.txtFK_Menu.Text.Trim().Length==0)
			{
				strErr+="FK_Menu不能为空！\\n";	
			}
			if(this.txtFK_Operate.Text.Trim().Length==0)
			{
				strErr+="FK_Operate不能为空！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			string No=this.txtNo.Text;
			string FK_Menu=this.txtFK_Menu.Text;
			string FK_Operate=this.txtFK_Operate.Text;

			BP.EIP.Model.Port_MenuOperate model=new BP.EIP.Model.Port_MenuOperate();
			model.No=No;
			model.FK_Menu=FK_Menu;
			model.FK_Operate=FK_Operate;

			BP.EIP.BLL.Port_MenuOperate bll=new BP.EIP.BLL.Port_MenuOperate();
			bll.Add(model);
			Lizard.Common.MessageBox.ShowAndRedirect(this,"保存成功！","add.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
