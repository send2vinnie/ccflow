using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Tax666.AppWeb.Manager.AticleManage
{
    public partial class ArticleTypeManage : PageBase
    {
        private int m_TypeID = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["type"] == null)
                m_TypeID = 1;
            else
                m_TypeID = Int32.Parse(Request.Params["type"].ToString());

            if (!Page.IsPostBack)
            {
                if (m_TypeID == 1)
                    TabOptionWebControls1.SelectIndex = 1;
                else
                    TabOptionWebControls1.SelectIndex = 0;
            }
        }
    }
}
