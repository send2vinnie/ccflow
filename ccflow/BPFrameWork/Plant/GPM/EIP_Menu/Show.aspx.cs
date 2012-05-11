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
namespace Lizard.GPM.Web.EIP_Menu
{
    public partial class Show : BP.Web.WebPage
    {
        public string strid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    strid = Request.Params["id"];
                    string MenuId = strid;
                    ShowInfo(MenuId);
                }
            }
        }

        private void ShowInfo(string MenuId)
        {
            Lizard.GPM.BLL.EIP_Menu bll = new Lizard.GPM.BLL.EIP_Menu();
            Lizard.GPM.Model.EIP_Menu model = bll.GetModel(MenuId);
            this.lblMenuId.Text = model.MenuId;
            this.lblMenuNo.Text = model.MenuNo;
            this.lblMenuName.Text = model.MenuName;
            this.lblTitle.Text = model.Title;
            this.lblImg.Text = model.Img;
            this.lblUrl.Text = model.Url;
            this.lblPath.Text = model.Path;
            this.lblPid.Text = model.Pid;
            this.lblStatus.Text = model.Status ? "是" : "否";

        }


    }
}
