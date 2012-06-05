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
namespace Lizard.OA.Web.EIP_LayoutDetail
{
    public partial class Modify : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    string DetailId = Request.Params["id"];
                    ShowInfo(DetailId);
                }
            }
        }

        private void ShowInfo(string DetailId)
        {
            //Lizard.GPM.BLL.EIP_LayoutDetail bll = new Lizard.GPM.BLL.EIP_LayoutDetail();
            //Lizard.GPM.Model.EIP_LayoutDetail model = bll.GetModel(DetailId);

            BP.CCOA.EIP_LayoutDetail model = new BP.CCOA.EIP_LayoutDetail(DetailId);
            //model = model.GetModel(DetailId);

            this.lblDetailId.Text = model.No.ToString();
            this.txtColumnNo.Text = model.ColumnNo.ToString();
            this.txtPanelId.Text = model.PanelId;
            this.txtPanelTitle.Text = model.PanelTitle;
            this.chkShowCollapseButton.Checked = model.ShowCollapseButton;
            this.chkIsShow.Checked = model.IsShow;
            this.txtWidth.Text = model.Width.ToString();
            this.txtHeight.Text = model.Height.ToString();
            this.txtUrl.Text = model.Url;
        }

        public void btnSave_Click(object sender, EventArgs e)
        {

            string strErr = "";
            if (!PageValidate.IsNumber(txtColumnNo.Text))
            {
                strErr += "ColumnNo格式错误！\\n";
            }
            if (this.txtPanelId.Text.Trim().Length == 0)
            {
                strErr += "PanelId不能为空！\\n";
            }
            if (this.txtPanelTitle.Text.Trim().Length == 0)
            {
                strErr += "PanelTitle不能为空！\\n";
            }
            if (!PageValidate.IsNumber(txtWidth.Text))
            {
                strErr += "Width格式错误！\\n";
            }
            if (!PageValidate.IsNumber(txtHeight.Text))
            {
                strErr += "Height格式错误！\\n";
            }

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            string DetailId = this.lblDetailId.Text;
            int ColumnNo = int.Parse(this.txtColumnNo.Text);
            string PanelId = this.txtPanelId.Text;
            string PanelTitle = this.txtPanelTitle.Text;
            bool ShowCollapseButton = this.chkShowCollapseButton.Checked;
            bool IsShwo = this.chkIsShow.Checked;
            int Width = int.Parse(this.txtWidth.Text);
            int Height = int.Parse(this.txtHeight.Text);
            string Url = this.txtUrl.Text;

            //Lizard.GPM.Model.EIP_LayoutDetail model = new Lizard.GPM.Model.EIP_LayoutDetail();
            BP.CCOA.EIP_LayoutDetail model = new BP.CCOA.EIP_LayoutDetail(DetailId);

            //model.No = DetailId;
            model.ColumnNo = ColumnNo;
            model.PanelId = PanelId;
            model.PanelTitle = PanelTitle;
            model.ShowCollapseButton = ShowCollapseButton;
            model.IsShow = IsShwo;
            model.Width = Width;
            model.Height = Height;
            model.Url = Url;

            //Lizard.GPM.BLL.EIP_LayoutDetail bll = new Lizard.GPM.BLL.EIP_LayoutDetail();
            model.Update();
            Lizard.Common.MessageBox.ShowAndRedirect(this, "保存成功！", "LayoutSetting.aspx");
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("LayoutSetting.aspx");
        }
    }
}
