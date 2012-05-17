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
namespace Lizard.OA.Web.OA_Meeting
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
                    string MeetingId = strid;
                    ShowInfo(MeetingId);
                }
            }
        }

        private void ShowInfo(string MeetingId)
        {
            BP.CCOA.OA_Meeting model = new BP.CCOA.OA_Meeting(MeetingId);
            this.lblMeetingId.Text = model.No;
            this.lblTopic.Text = model.Topic;
            this.lblPlanStartTime.Text = model.PlanStartTime.ToString();
            this.lblPlanEndTime.Text = model.PlanEndTime.ToString();
            this.lblPlanAddress.Text = model.PlanAddress;
            this.lblPlanMembers.Text = model.PlanMembers;
            this.lblRealStartTime.Text = model.RealStartTime.ToString();
            this.lblRealEndTime.Text = model.RealEndTime.ToString();
            this.lblRealAddress.Text = model.RealAddress;
            this.lblRealMembers.Text = model.RealMembers;
            this.lblRecorder.Text = model.Recorder;
            this.lblSummary.Text = model.Summary;
            this.lblUpUser.Text = model.UpUser.ToString();
            this.lblUpDT.Text = model.UpDT.ToString();
            this.lblStatus.Text = model.Status ? "是" : "否";

        }


    }
}
