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
using Tax666.DataEntity;
using Tax666.BusinessFacade;

namespace Tax666.AppWeb.Manager.AticleManage
{
    public partial class HomeLinkManage : PageBase
    {
        private int m_TypeID;
        private int m_Valid;
        private int m_Audit;

        protected int m_RecordCount;
        protected int m_PageCount;
        protected int m_CurrentPageIndex;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnValid.Attributes.Add("onclick", "return OptionRec('CheckAll','VALID','btnDel');");
            this.btnDelete.Attributes.Add("onclick", "return OptionRec('CheckAll','DELETE','btnDelete');");
            this.btnAudit.Attributes.Add("onclick", "return OptionRec('CheckAll','AUDIT','btnAudit');");

            //页面按钮执行权限判断(只有平台拥有者才具有的权限)；
            //m_AgentID = int.Parse(AgentUserInfo.Tables[UserAgentData.UserAgent_Table].Rows[0][UserAgentData.AgentID_Field].ToString());
            //if (m_AgentID == ParentAgentID)
            //{
                this.btnValid.Enabled = true;
                this.btnDelete.Enabled = true;
                this.btnAudit.Enabled = true;
            //}
            //else
            //{
            //    this.btnValid.Enabled = false;
            //    this.btnDelete.Enabled = false;
            //    this.btnAudit.Enabled = false;
            //}

            if (Request.Params["typeid"] == null)
                m_TypeID = 0;
            else
                m_TypeID = int.Parse(Request.Params["typeid"].ToString());

            if (Request.Params["valid"] == null)
                m_Valid = 2;
            else
                m_Valid = int.Parse(Request.Params["valid"].ToString());

            if (Request.Params["audit"] == null)
                m_Audit = 2;
            else
                m_Audit = int.Parse(Request.Params["audit"].ToString());

            if (!Page.IsPostBack)
            {
                //保持下拉列表中的值；
                this.BindLinkType();
                foreach (ListItem item in this.dplType.Items)
                {
                    if (item.Value == m_TypeID.ToString())
                        item.Selected = true;
                }

                foreach (ListItem item in this.dplAudit.Items)
                {
                    if (item.Value == m_Audit.ToString())
                        item.Selected = true;
                }

                foreach (ListItem item in this.dplvalid.Items)
                {
                    if (item.Value == m_Valid.ToString())
                        item.Selected = true;
                }

                //获取分页的数据；
                HomeLinkData RecCountSet = (new HomeLinkFacade()).GetHomeLinkAll( m_TypeID, m_Audit, m_Valid, 0, 0, true);
                this.pagesTrade.RecordCount = Int32.Parse(RecCountSet.Tables[HomeLinkData.HomeLink_Table].Rows[0][HomeLinkData.TOTALCOUNT_FIELD].ToString());
                this.BindDataGridData();
            }
        }
        private void BindLinkType()
        {
            HomeLinkTypeData typeData = (new HomeLinkTypeFacade()).GetHomeLinkTypeByListType(2, 0, 2);
            //添加新行；
            DataRow row = typeData.Tables[HomeLinkTypeData.HomeLinkType_Table].NewRow();
            row[HomeLinkTypeData.LinkTypeID_Field] = 0;
            row[HomeLinkTypeData.TypeName_Field] = "所有类型";
            typeData.Tables[HomeLinkTypeData.HomeLinkType_Table].Rows.Add(row);

            this.dplType.DataSource = typeData;
            this.dplType.DataTextField = HomeLinkTypeData.TypeName_Field;
            this.dplType.DataValueField = HomeLinkTypeData.LinkTypeID_Field;
            this.dplType.DataBind();
        }

        private void BindDataGridData()
        {
            HomeLinkData logData;
            logData = (new HomeLinkFacade()).GetHomeLinkAll( m_TypeID, m_Audit, m_Valid, pagesTrade.PageSize, pagesTrade.CurrentPageIndex, false);
            DataTable table = logData.Tables[HomeLinkData.HomeLink_Table];
            DataColumnCollection columns = table.Columns;
            columns.Add("LogoPic", typeof(System.String));

            if (table.Rows.Count == 0)
            {
                this.tabNoRec.Visible = true;

                this.btnValid.Enabled = false;
                this.btnDelete.Enabled = false;
                this.btnAudit.Enabled = false;
            }
            else
            {
                this.tabNoRec.Visible = false;
                //if (m_AgentID == ParentAgentID)
                //{
                    this.btnValid.Enabled = true;
                    this.btnDelete.Enabled = true;
                    this.btnAudit.Enabled = true;
                //}
                //else
                //{
                //    this.btnValid.Enabled = false;
                //    this.btnDelete.Enabled = false;
                //    this.btnAudit.Enabled = false;
                //}
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i][HomeLinkData.LogoPath_Field].ToString() == "" || table.Rows[i][HomeLinkData.LogoPath_Field].ToString() == string.Empty)
                {
                    table.Rows[i]["LogoPic"] = "无Logo图片";
                }
                else
                {
                    string logoPicStr = "<img src='";
                    logoPicStr += UrlBase;
                    logoPicStr += table.Rows[i][HomeLinkData.LogoPath_Field].ToString();
                    logoPicStr += "' border='0' align='absmiddle'>";

                    table.Rows[i]["LogoPic"] = logoPicStr;
                }
            }

            this.listRecord.DataSource = table;
            this.listRecord.DataKeyField = HomeLinkData.LinkID_Field;
            this.listRecord.DataBind();

            //动态设置用户自定义文本内容
            m_RecordCount = pagesTrade.RecordCount;
            m_PageCount = pagesTrade.PageCount;
            m_CurrentPageIndex = pagesTrade.CurrentPageIndex;
        }

        protected void pagesTrade_PageChanged(object sender, EventArgs e)
        {
            this.BindDataGridData();
        }

        protected void btnValid_Click(object sender, EventArgs e)
        {
            //有效和无效设置操作；
            foreach (DataGridItem thisItem in listRecord.Items)
            {
                if (((CheckBox)thisItem.Cells[0].Controls[1]).Checked)
                {
                    int chkedID = Int32.Parse(listRecord.DataKeys[thisItem.ItemIndex].ToString());
                    (new HomeLinkFacade()).SetHomeLinkInfo("VALID", chkedID);
                }
            }
            this.BindDataGridData();
        }

        protected void btnAudit_Click(object sender, EventArgs e)
        {
            foreach (DataGridItem thisItem in listRecord.Items)
            {
                if (((CheckBox)thisItem.Cells[0].Controls[1]).Checked)
                {
                    int chkedID = Int32.Parse(listRecord.DataKeys[thisItem.ItemIndex].ToString());
                    (new HomeLinkFacade()).SetHomeLinkInfo("AUDIT", chkedID);
                }
            }
            this.BindDataGridData();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //有效和无效设置操作；
            foreach (DataGridItem thisItem in listRecord.Items)
            {
                if (((CheckBox)thisItem.Cells[0].Controls[1]).Checked)
                {
                    int chkedID = Int32.Parse(listRecord.DataKeys[thisItem.ItemIndex].ToString());
                    (new HomeLinkFacade()).DelHomeLink(chkedID);
                }
            }
            this.BindDataGridData();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect(UrlBase + "Manager/AticleManage/HomeLinkEdit.aspx");
        }

        protected void dplType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string typeid = this.dplType.SelectedItem.Value;
            string valid = this.dplvalid.SelectedItem.Value;
            string audit = this.dplAudit.SelectedItem.Value;

            Response.Redirect(UrlBase + "Manager/AticleManage/HomeLinkManage.aspx?typeid=" + typeid + "&valid=" + valid + "&audit=" + audit);
        }

        protected void listRecord_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ImageButton upImageButton = (ImageButton)e.Item.FindControl("UpImageButton");
                ImageButton downImageButton = (ImageButton)e.Item.FindControl("DownImageButton");
                //if (m_AgentID != ParentAgentID)
                //{
                    upImageButton.Attributes.Add("onclick", "alert('对不起，您不是管理员,无操作权限！');return false;");
                    downImageButton.Attributes.Add("onclick", "alert('对不起，您不是管理员,无操作权限！');return false;");
               // }
            }
        }

        protected void listRecord_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            int linkID = int.Parse(((Label)e.Item.FindControl("LinkIDLabel")).Text);
            bool retVal = false;

            switch (e.CommandName)
            {
                case "OrderUp":
                    {
                        retVal = (new HomeLinkFacade()).SetHomeLinkInfo("UP", linkID);
                        break;
                    }
                case "OrderDown":
                    {
                        retVal = (new HomeLinkFacade()).SetHomeLinkInfo("DOWN", linkID);
                        break;
                    }
            }

            if (retVal)
            {
                Uri MyUrl = Request.Url;
                Response.Redirect(MyUrl.PathAndQuery);
            }
        }

    }
}