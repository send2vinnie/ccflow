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
namespace Lizard.OA.Web.OA_Meeting
{
    public partial class Modify : Page
    {       

        		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					string MeetingId= Request.Params["id"];
					ShowInfo(MeetingId);
				}
			}
		}
			
	private void ShowInfo(string MeetingId)
	{
		BP.CCOA.OA_Meeting bll=new BP.CCOA.OA_Meeting();
		Lizard.OA.Model.OA_Meeting model=bll.GetModel(MeetingId);
		this.lblMeetingId.Text=model.MeetingId;
		this.txtTopic.Text=model.Topic;
		this.txtPlanStartTime.Text=model.PlanStartTime.ToString();
		this.txtPlanEndTime.Text=model.PlanEndTime.ToString();
		this.txtPlanAddress.Text=model.PlanAddress;
		this.txtPlanMembers.Text=model.PlanMembers;
		this.txtRealStartTime.Text=model.RealStartTime.ToString();
		this.txtRealEndTime.Text=model.RealEndTime.ToString();
		this.txtRealAddress.Text=model.RealAddress;
		this.txtRealMembers.Text=model.RealMembers;
		this.txtRecorder.Text=model.Recorder;
		this.txtSummary.Text=model.Summary;
		this.txtUpUser.Text=model.UpUser.ToString();
		this.txtUpDT.Text=model.UpDT.ToString();
		this.chkStatus.Checked=model.Status;

	}

		public void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtTopic.Text.Trim().Length==0)
			{
				strErr+="议题不能为空！\\n";	
			}
			if(!PageValidate.IsDateTime(txtPlanStartTime.Text))
			{
				strErr+="计划开始时间格式错误！\\n";	
			}
			if(!PageValidate.IsDateTime(txtPlanEndTime.Text))
			{
				strErr+="计划结束时间格式错误！\\n";	
			}
			if(this.txtPlanAddress.Text.Trim().Length==0)
			{
				strErr+="计划召开地址不能为空！\\n";	
			}
			if(this.txtPlanMembers.Text.Trim().Length==0)
			{
				strErr+="计划参加人员不能为空！\\n";	
			}
			if(!PageValidate.IsDateTime(txtRealStartTime.Text))
			{
				strErr+="实际开始时间格式错误！\\n";	
			}
			if(!PageValidate.IsDateTime(txtRealEndTime.Text))
			{
				strErr+="实际结束时间格式错误！\\n";	
			}
			if(this.txtRealAddress.Text.Trim().Length==0)
			{
				strErr+="实际召开地址不能为空！\\n";	
			}
			if(this.txtRealMembers.Text.Trim().Length==0)
			{
				strErr+="实际参加人员不能为空！\\n";	
			}
			if(this.txtRecorder.Text.Trim().Length==0)
			{
				strErr+="记录人不能为空！\\n";	
			}
			if(this.txtSummary.Text.Trim().Length==0)
			{
				strErr+="会议纪要不能为空！\\n";	
			}
			if(!PageValidate.IsNumber(txtUpUser.Text))
			{
				strErr+="更新人格式错误！\\n";	
			}
			if(!PageValidate.IsDateTime(txtUpDT.Text))
			{
				strErr+="更新时间格式错误！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			string MeetingId=this.lblMeetingId.Text;
			string Topic=this.txtTopic.Text;
			DateTime PlanStartTime=DateTime.Parse(this.txtPlanStartTime.Text);
			DateTime PlanEndTime=DateTime.Parse(this.txtPlanEndTime.Text);
			string PlanAddress=this.txtPlanAddress.Text;
			string PlanMembers=this.txtPlanMembers.Text;
			DateTime RealStartTime=DateTime.Parse(this.txtRealStartTime.Text);
			DateTime RealEndTime=DateTime.Parse(this.txtRealEndTime.Text);
			string RealAddress=this.txtRealAddress.Text;
			string RealMembers=this.txtRealMembers.Text;
			string Recorder=this.txtRecorder.Text;
			string Summary=this.txtSummary.Text;
			int UpUser=int.Parse(this.txtUpUser.Text);
			DateTime UpDT=DateTime.Parse(this.txtUpDT.Text);
			bool Status=this.chkStatus.Checked;


			Lizard.OA.Model.OA_Meeting model=new Lizard.OA.Model.OA_Meeting();
			model.MeetingId=MeetingId;
			model.Topic=Topic;
			model.PlanStartTime=PlanStartTime;
			model.PlanEndTime=PlanEndTime;
			model.PlanAddress=PlanAddress;
			model.PlanMembers=PlanMembers;
			model.RealStartTime=RealStartTime;
			model.RealEndTime=RealEndTime;
			model.RealAddress=RealAddress;
			model.RealMembers=RealMembers;
			model.Recorder=Recorder;
			model.Summary=Summary;
			model.UpUser=UpUser;
			model.UpDT=UpDT;
			model.Status=Status;

			BP.CCOA.OA_Meeting bll=new BP.CCOA.OA_Meeting();
			bll.Update(model);
			Lizard.Common.MessageBox.ShowAndRedirect(this,"保存成功！","list.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
