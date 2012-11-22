using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.DA;
using BP.Web;
using BP.Port;
using BP.WF;

public partial class WF_WorkOpt_OneWork_OP : BP.Web.WebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string workid = this.Request.QueryString["WorkID"];
        string FK_Flow = this.Request.QueryString["FK_Flow"];
        string FK_Node = this.Request.QueryString["FK_Node"];
        string doType = this.Request.QueryString["DoType"];

        #region 功能执行
        try
        {
            switch (this.Request.QueryString["DoType"])
            {
                case "Del":
                    WorkFlow wf = new WorkFlow(FK_Flow, Int64.Parse(workid));
                    wf.DoDeleteWorkFlowByReal();
                    this.WinCloseWithMsg("流程已经被删除.");
                    break;
                case "Hung":
                    WorkFlow wf1 = new WorkFlow(FK_Flow, Int64.Parse(workid));
                    wf1.DoHung();
                    this.WinCloseWithMsg("流程已经被挂起.");
                    break;
                case "UnHung":
                    WorkFlow wf2 = new WorkFlow(FK_Flow, Int64.Parse(workid));
                    wf2.DoUnHung();
                    this.WinCloseWithMsg("流程已经被解除挂起.");
                    break;
                case "ComeBack":
                    WorkFlow wf3 = new WorkFlow(FK_Flow, Int64.Parse(workid));
                    wf3.DoComeBackWrokFlow("无");
                    this.WinCloseWithMsg("流程已经被回复启用.");
                    break;
                default:
                    break;
            }
        }
        catch(Exception ex)
        {
            this.Alert("执行功能:" + doType+",出现错误:"+ex.Message);
        }
        #endregion


        string wfState = BP.DA.DBAccess.RunSQLReturnStringIsNull("SELECT WorkID FROM WF_GenerWorkFlow WHERE WorkID=" + workid, "1");
        string sta = "";
        if (wfState == "1")
            sta = "当前工作已经完成";
        if (wfState == "0")
            sta = "当前工作在运行中";
        this.Pub2.AddH3(sta);

        switch (wfState)
        {
            case "0":
                this.Pub2.AddFieldSet("删除流程");
                this.Pub2.Add("功能执行:<a href=\"javascript:DoFunc('Del','" + workid + "','" + FK_Flow + "','" + FK_Node + "')\" >点击执行删除流程</a>。");
                this.Pub2.AddBR("说明：如果执行流程将会被彻底的删除。");
                this.Pub2.AddFieldSetEnd();

                this.Pub2.AddFieldSet("挂起");
                this.Pub2.Add("功能执行:<a href=\"javascript:DoFunc('Hung','" + workid + "','" + FK_Flow + "','" + FK_Node + "')\" >点击执行取消挂起流程</a>。");
                this.Pub2.AddBR("说明：解除流程挂起的状态。");
                this.Pub2.AddFieldSetEnd();

                this.Pub2.AddFieldSet("取回审批");
                this.Pub2.Add("功能执行:<a href=\"javascript:DoFunc('Takeback','" + workid + "','" + FK_Flow + "','" + FK_Node + "')\" >点击执行取回审批流程</a>。");
                this.Pub2.AddBR("说明：如果被成功取回，ccflow就会把停留在别人工作节点上的工作发送到您的待办列表里。");
                this.Pub2.AddFieldSetEnd();
                break;
            case "1":
                this.Pub2.AddFieldSet("恢复启用流程数据到结束节点");
                this.Pub2.Add("功能执行:<a href=\"javascript:DoFunc('ComeBack','" + workid + "','" + FK_Flow + "','" + FK_Node + "')\" >点击执行恢复流程</a>。");
                this.Pub2.AddBR("说明：如果被成功恢复，ccflow就会把待办工作发送给最后一个结束流程的工作人员。");
                this.Pub2.AddFieldSetEnd();
                break;
            case "2":
                this.Pub2.AddFieldSet("取消挂起");
                this.Pub2.Add("功能执行:<a href=\"javascript:DoFunc('UnHung','" + workid + "','" + FK_Flow + "','" + FK_Node + "')\" >点击执行取消挂起流程</a>。");
                this.Pub2.AddBR("说明：解除流程挂起的状态。");
                this.Pub2.AddFieldSetEnd();
                break;
            default:
                break;
        }
    }
}