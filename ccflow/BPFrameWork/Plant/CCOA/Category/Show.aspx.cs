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
namespace Lizard.Web.OA_Category
{
    public partial class Show : BasePage
    {
        public string strid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    strid = Request.Params["id"];
                    string No = strid;
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
            this.lblCategoryName.Text = model.CategoryName;
            this.lblType.Text = model.Type;
            this.lblDescription.Text = model.Description;
        }
    }
}
