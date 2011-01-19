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
using Tax666.Common;
using Tax666.BusinessFacade;
using Tax666.DataEntity;
using Tax666.WebControls;

namespace Tax666.AppWeb.Manager.AticleManage
{
    public partial class BulletinEdit : PageBase
    {
        private int bulletinID;
        private RecordOption OptionType;
        private string publishName = "系统管理员";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnOK.Attributes.Add("onclick", "return checkInput();");

            if (Request.Params["bulletinid"] != null)
            {
                //修改状态；
                OptionType = RecordOption.ModifyMode;
                bulletinID = Int32.Parse(Request.Params["bulletinid"]);
                if (!Page.IsPostBack)
                {
                    InfoBulletinData bulletinData = (new InfoBulletinFacade()).GetInfoBulletin(" where BulletinID=" + bulletinID + " and  IsAudit=2", 0, 0, true);
                    DataRow row = bulletinData.Tables[InfoBulletinData.InfoBulletin_Table].Rows[0];

                    //填充界面中控件的内容；
                    this.txtTitle.Value = row[InfoBulletinData.BulletinTitle_Field].ToString();
                    this.txtStartTime.Value = WebUtility.formatDate((DateTime)row[InfoBulletinData.StartTime_Field]);
                    this.txtEndTime.Value = WebUtility.formatDate((DateTime)row[InfoBulletinData.EndTime_Field]);
                    this.Editor1.Text = (String)row[InfoBulletinData.BulletinDesc_Field];

                    if (Convert.ToBoolean(row[InfoBulletinData.IsAvailable_Field]))
                        this.radPublish.Checked = true;
                    else
                        this.radNoPublish.Checked = true;

                    if (Int32.Parse(row[InfoBulletinData.StartTime_Field].ToString()) == 1)
                        this.radScoreMe.Checked = true;
                    else
                        this.radScoreAll.Checked = true;
                }
            }
            else
            {
                //添加状态；
                OptionType = RecordOption.AddMode;

                if (!Page.IsPostBack)
                {
                    txtStartTime.Value = WebUtility.formatDate(DateTime.Now);
                    txtEndTime.Value = WebUtility.formatDate(DateTime.Now.AddDays(60));
                }
            }

        }
        /// <summary>
        /// 添加公告记录/修改公告记录。
        /// </summary>
        /// <returns></returns>
        private int SaveBulletin()
        {
            bool retVal = false;
            int m_Reason = -1;
            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.AddDays(60);
          //  publishName = AgentUserInfo.Tables[UserAgentData.UserAgent_Table].Rows[0][UserAgentData.AgentName_Field].ToString();

            if (this.txtStartTime.Value.Trim() != "" || this.txtStartTime.Value.Trim() != string.Empty)
                startTime = Convert.ToDateTime(this.txtStartTime.Value);
            if (this.txtEndTime.Value.Trim() != "" || this.txtEndTime.Value.Trim() != string.Empty)
                endTime = Convert.ToDateTime(this.txtEndTime.Value);

            if (startTime >= endTime)
                endTime = startTime.AddDays(60);

            int audit = 1;
            int publishScore = 1;
            if (this.radScoreMe.Checked)
            {
                if (this.radPublish.Checked)
                    audit = 1;
                else
                    audit = 0;

                //publishScore = 1;
            }
            else
            {
                audit = 0;
               // publishScore = 2;
            }

            //根据OptionType的值，将新信息保存或更新到数据库中；
            switch (this.OptionType)
            {
                case RecordOption.AddMode:
                    //添加记录；
                    retVal = (new InfoBulletinFacade()).InsertInfoBulletin(
                        this.txtTitle.Value.Trim(),
                        this.Editor1.Text,
                        1,
                        startTime,
                        endTime,
                        audit,
                        out m_Reason);
                    break;

                case RecordOption.ModifyMode:
                    //更新记录；
                    InfoBulletinData bulletinData = new InfoBulletinData();
                    DataRow row = bulletinData.Tables[InfoBulletinData.InfoBulletin_Table].NewRow();

                    row[InfoBulletinData.BulletinTitle_Field] = this.txtTitle.Value.Trim();
                    row[InfoBulletinData.BulletinDesc_Field] = this.Editor1.Text.Trim();
                    //row[BulletinData.AgentID_Field] = ParentAgentID;
                    row[InfoBulletinData.StartTime_Field] = startTime;
                    row[InfoBulletinData.EndTime_Field] = endTime;
                    //row[InfoBulletinData.PublishScore_Field] = publishScore;
                    row[InfoBulletinData.IsAudit_Field] = audit;

                    //将该行添加到数据集DataSet中；
                    bulletinData.Tables[InfoBulletinData.InfoBulletin_Table].Rows.Add(row);
                    bulletinData.AcceptChanges();
                    row[InfoBulletinData.BulletinID_Field] = bulletinID;

                    retVal = (new InfoBulletinFacade()).UpdateInfoBulletin(bulletinData);

                    m_Reason =Convert.ToInt32(row[InfoBulletinData.Reason_Field].ToString());

                    break;
            }

            return m_Reason;
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (SaveBulletin() == 1)
                Response.Redirect(UrlBase + "Manager/AticleManage/BulletinManage.aspx");
            else
                EventMessage.MessageBox(1, "添加公告信息失败", "添加公告信息失败！可能原因：①已存在相同应用名称；②网络或数据库繁忙。请稍后重试！", Tax666.WebControls.Icon_Type.Error, PublicCommon.GetHomeBaseUrl("BulletinManage.aspx"));
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(UrlBase + "Manager/AticleManage/BulletinManage.aspx");
        }

    }
}

