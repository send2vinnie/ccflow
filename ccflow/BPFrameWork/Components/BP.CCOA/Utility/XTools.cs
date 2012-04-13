using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using XControls;
using System.Web.UI.HtmlControls;
using BP.CCOA.Enum;

namespace BP.CCOA.Utility
{
    /// <summary>
    ///XPage 的摘要说明
    /// </summary>
    public class XTools : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            //if (Session["_loginaccountinfo"] == null)
            //{
            //    ClientAlertMsg("页面已过期，请重新登录！");
            //    Response.Redirect("~/Login.aspx");
            //}
            if (!IsPostBack)
            {

            }
        }

        public void ClientAlertMsg(string strMsg)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(),
                "ss",
                "<script>alert('" + strMsg + "')</script>");
        }

        public void CloseWindow()
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(),
               "ss",
               "<script>window.close()</script>");
        }

        public void SetControlsDisable(Control ctlMaster)
        {
            foreach (var ctl in ctlMaster.Controls)
            {
                if (ctl is IXInput)
                {
                    ((IXInput)ctl).SetDisable();
                }
                if (ctl is HtmlGenericControl)
                {
                    SetControlsDisable((Control)ctl);
                }
                if (ctl is HtmlTable)
                {
                    foreach (HtmlTableRow row in ((HtmlTable)ctl).Rows)
                    {
                        foreach (HtmlTableCell cell in row.Cells)
                        {
                            SetControlsDisable((Control)cell);
                        }
                    }
                }
            }
        }

        public XEnum.EditMode EditMode
        {
            get
            {
                if (Session["EditModle"] != null)
                {
                    return (XEnum.EditMode)Session["EditModle"];
                }
                else
                {
                    return XEnum.EditMode.Normal;
                }
            }
            set
            {
                Session["EditModle"] = value;
            }
        }

        public string EditNO
        {
            get { return Session["EditNO"] == null ? string.Empty : Session["EditNO"].ToString(); }
            set { Session["EditNO"] = value; }
        }
    }
}