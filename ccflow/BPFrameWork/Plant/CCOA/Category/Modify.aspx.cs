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
namespace Lizard.Web.OA_Category
{
    public partial class Modify : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    string No = Request.Params["id"];
                    ShowInfo(No);
                }
            }
        }

        private void ShowInfo(string No)
        {
            //Lizard.BLL.OA_Category bll = new Lizard.BLL.OA_Category();
            //Lizard.Model.OA_Category model = bll.GetModel(No);
            BP.CCOA.OA_Category model = new BP.CCOA.OA_Category(No);
            this.lblNo.Text = model.No;
            this.txtCategoryName.Text = model.CategoryName;
            this.txtType.Text = model.Type;
            this.txtDescription.Text = model.Description;
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            string strErr = "";
            if (this.txtCategoryName.Text.Trim().Length == 0)
            {
                strErr += "类别名称不能为空！\\n";
            }
            if (this.txtType.Text.Trim().Length == 0)
            {
                strErr += "类型：0-news不能为空！\\n";
            }
            if (this.txtDescription.Text.Trim().Length == 0)
            {
                strErr += "描述不能为空！\\n";
            }

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            string No = this.lblNo.Text;
            string CategoryName = this.txtCategoryName.Text;
            string Type = this.txtType.Text;
            string Description = this.txtDescription.Text;

            //Lizard.Model.OA_Category model = new Lizard.Model.OA_Category();
            BP.CCOA.OA_Category model = new BP.CCOA.OA_Category(No);
            model.CategoryName = CategoryName;
            model.Type = Type;
            model.Description = Description;

            model.Update();

            Lizard.Common.MessageBox.ShowAndRedirect(this, "保存成功！", "list.aspx");
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
