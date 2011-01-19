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
using Tax666.BusinessFacade;
using Tax666.DataEntity;

namespace Tax666.AppWeb.Manager.AticleManage
{
    public partial class BulletinManage : PageBase
    {
        private int m_Score = 0;
        private int m_Audit = 2;

        protected int m_RecordCount;
        protected int m_PageCount;
        protected int m_CurrentPageIndex;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnDelete.Attributes.Add("onclick", "return OptionRec('CheckAll','DELETE','btnDelete');");
            this.btnAudit.Attributes.Add("onclick", "return OptionRec('CheckAll','AUDIT','btnAudit');");

            //if (Request.Params["scoreid"] == null)
            //    m_Score = 0;
            //else
            //    m_Score = Int32.Parse(Request.Params["scoreid"].ToString());

            if (Request.Params["scoreid"] == null)
                m_Audit = 2;
            else
                m_Audit = Int32.Parse(Request.Params["audit"].ToString());

            if (!Page.IsPostBack)
            {
                ////保持下拉列表中的值；
                //foreach (ListItem item in this.dplScore.Items)
                //{
                //    if (item.Value == m_Score.ToString())
                //        item.Selected = true;
                //}

                //保持下拉列表中的值；
                foreach (ListItem item in this.dplAudit.Items)
                {
                    if (item.Value == m_Audit.ToString())
                        item.Selected = true;
                }

                //获取分页的数据；
                InfoBulletinData RecCountSet = (new InfoBulletinFacade()).GetInfoBulletin(" where IsAudit=" + m_Audit + "  and IsAudit=" + m_Audit + "", 0, 0, true);
                
                this.pagesTrade.RecordCount = Int32.Parse(RecCountSet.Tables[InfoBulletinData.InfoBulletin_Table].Rows[0][InfoBulletinData.TOTALCOUNT_FIELD].ToString());
                if (int.Parse(RecCountSet.Tables[InfoBulletinData.InfoBulletin_Table].Rows[0][InfoBulletinData.BulletinID_Field].ToString()) == 0) { }
                else
                this.BindBulletin();
            }
        }

        /// <summary>
        /// 绑定所有的公告记录列表；
        /// </summary>
        private void BindBulletin()
        {
            InfoBulletinData bulletinData;
            bulletinData = (new InfoBulletinFacade()).GetInfoBulletin(" where IsAudit=" + m_Audit +  "  and IsAudit=" + m_Audit + "", 0, 0, true);
            DataTable table = bulletinData.Tables[InfoBulletinData.InfoBulletin_Table];

            if (table.Rows.Count > 0)
            {
                this.tabNoRec.Visible = false;
                this.btnDelete.Enabled = true;
                this.btnAudit.Enabled = true;
            }
            else
            {
                this.tabNoRec.Visible = true;
                this.btnDelete.Enabled = false;
                this.btnAudit.Enabled = false;
            }

            this.listRecord.DataSource = table;
            this.listRecord.DataKeyField = InfoBulletinData.BulletinID_Field;
            this.listRecord.DataBind();

            //动态设置用户自定义文本内容			
            m_RecordCount = pagesTrade.RecordCount;
            m_PageCount = pagesTrade.PageCount;
            m_CurrentPageIndex = pagesTrade.CurrentPageIndex;
        }

        protected void listRecord_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            bool retVal = false;
            int bulletinID = int.Parse(((Label)e.Item.FindControl("BulletinIDLabel")).Text);

            switch (e.CommandName)
            {
                case "OrderUp":
                    retVal = (new InfoBulletinFacade()).SetInfoBulletin("DOWN", bulletinID);
                    break;
                case "OrderDown":
                    retVal = (new InfoBulletinFacade()).SetInfoBulletin("UP", bulletinID);
                    break;
            }

            if (retVal)
            {
                Uri MyUrl = Request.Url;
                Response.Redirect(MyUrl.PathAndQuery);
            }
        }

        protected void listRecord_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ImageButton upImageButton = (ImageButton)e.Item.FindControl("UpImageButton");
                ImageButton downImageButton = (ImageButton)e.Item.FindControl("DownImageButton");

                //操作的权限判断和提示；
            }
        }

        protected void pagesTrade_PageChanged(object sender, EventArgs e)
        {
            this.BindBulletin();
        }

        protected void btnAudit_Click(object sender, EventArgs e)
        {
            foreach (DataGridItem thisItem in listRecord.Items)
            {
                if (((CheckBox)thisItem.Cells[0].Controls[1]).Checked)
                {
                    int chkedID = Int32.Parse(listRecord.DataKeys[thisItem.ItemIndex].ToString());
                    (new InfoBulletinFacade()).SetInfoBulletin("VALID", chkedID);
                }
            }
            this.BindBulletin();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridItem thisItem in listRecord.Items)
            {
                if (((CheckBox)thisItem.Cells[0].Controls[1]).Checked)
                {
                    int chkedID = Int32.Parse(listRecord.DataKeys[thisItem.ItemIndex].ToString());
                    (new InfoBulletinFacade()).DelInfoBulletin(chkedID);
                }
            }
            this.BindBulletin();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect(UrlBase + "Manager/AticleManage/BulletinEdit.aspx");
        }

        protected void dplScore_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string publishScore = this.dplScore.SelectedItem.Value;
            string isAudit = this.dplAudit.SelectedValue;

            Response.Redirect(UrlBase + "Manager/AticleManage/BulletinManage.aspx?audit=" + isAudit);
        }

    }
}
