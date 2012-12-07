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
    public string workid;
    public Int64 workIDint;
    public string fk_Flow;
    public string fk_Node;
    public string doType;

    protected void Page_Load(object sender, EventArgs e)
    {
          workid = this.Request.QueryString["WorkID"];
          fk_Flow = this.Request.QueryString["FK_Flow"];
          fk_Node = this.Request.QueryString["FK_Node"];
          doType = this.Request.QueryString["DoType"];
          workIDint = Int64.Parse(workid);

        #region 功能执行
        try
        {
            switch (this.Request.QueryString["DoType"])
            {
                case "Del":
                    WorkFlow wf = new WorkFlow(fk_Flow, workIDint);
                    wf.DoDeleteWorkFlowByReal();
                    this.WinCloseWithMsg("流程已经被删除.");
                    break;
                case "HungUp":
                    WorkFlow wf1 = new WorkFlow(fk_Flow, workIDint);
                     //wf1.DoHungUp(HungUpWa;
                    this.WinCloseWithMsg("流程已经被挂起.");
                    break;
                case "UnHungUp":
                    WorkFlow wf2 = new WorkFlow(fk_Flow, workIDint);
                  //  wf2.DoUnHungUp();
                    this.WinCloseWithMsg("流程已经被解除挂起.");
                    break;
                case "ComeBack":
                    WorkFlow wf3 = new WorkFlow(fk_Flow, workIDint);
                    wf3.DoComeBackWrokFlow("无");
                    this.WinCloseWithMsg("流程已经被回复启用.");
                    break;
                case "Takeback": /*取回审批.*/
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

        int wfState = BP.DA.DBAccess.RunSQLReturnValInt("SELECT WFState FROM WF_GenerWorkFlow WHERE WorkID=" + workid, 1);
        //string sta = "";
        //if (wfState == "1")
        //    sta = "当前工作已经完成";
        //if (wfState == "0")
        //    sta = "当前工作在运行中";
        WFState wfstateEnum = (WFState)wfState;
        this.Pub2.AddH3("您可执行的操作<hr>" );

//  this.Pub2.AddH3("可执行的操作" + wfState + " sta=" + wfstateEnum);

        switch (wfstateEnum)
        {
            case WFState.Runing: /* 运行时*/
                GenerWorkFlow gwf = new GenerWorkFlow(workIDint);
                this.Pub2.AddFieldSet("删除流程");
                if (WebUser.No == "admin")
                {
                    this.Pub2.Add("功能执行:<a href=\"javascript:DoFunc('Del','" + workid + "','" + fk_Flow + "','" + fk_Node + "')\" >点击执行删除流程</a>。");
                    this.Pub2.AddBR("说明：如果执行流程将会被彻底的删除。");
                }
                else
                {
                    this.Pub2.Add("您没有此权限.");
                }
                this.Pub2.AddFieldSetEnd();


                this.Pub2.AddFieldSet("挂起");
                if (BP.WF.Dev2Interface.Flow_CheckIsCanDoCurrentWork(workIDint, WebUser.No) == true)
                {
                    this.Pub2.Add("功能执行:<a href=\"javascript:DoFunc('HungUp','" + workid + "','" + fk_Flow + "','" + fk_Node + "','')\" >点击执行挂起流程</a>。");
                    this.Pub2.AddBR("说明：对该流程执行挂起，挂起后可以解除挂起，挂起的时间不计算考核。");
                }
                else
                {
                    this.Pub2.Add("您没有此权限.");
                }
                this.Pub2.AddFieldSetEnd();

                /* 判断是否有取回审批的权限。*/
                this.Pub2.AddFieldSet("取回审批");
                string sql = "SELECT NodeID FROM WF_Node WHERE CheckNodes LIKE '%" + gwf.FK_Node + "%'";
                int myNode = DBAccess.RunSQLReturnValInt(sql, 0);
                if (myNode != 0)
                {
                    GetTask gt = new GetTask(myNode);
                    if (gt.Can_I_Do_It() == true)
                    {
                        this.Pub2.Add("功能执行:<a href=\"javascript:Takeback('" + workid + "','" + fk_Flow + "','" + gwf.FK_Node + "','" + myNode + "')\" >点击执行取回审批流程</a>。");
                        this.Pub2.AddBR("说明：如果被成功取回，ccflow就会把停留在别人工作节点上的工作发送到您的待办列表里。");
                    }
                }
                else
                {
                    this.Pub2.Add("您没有此权限.");
                }
                this.Pub2.AddFieldSetEnd();

                /*催办*/
                this.Pub2.AddFieldSet("工作催办");


                this.Pub2.Add("您没有此权限.");

                this.Pub2.AddFieldSetEnd();

                /*撤销发送*/
                this.Pub2.AddFieldSet("撤销发送");
                this.Pub2.Add("您没有此权限.");
                this.Pub2.AddFieldSetEnd();
                break;
            case WFState.Complete: // 完成.
                if (WebUser.No == "admin")
                {
                    this.Pub2.AddFieldSet("恢复启用流程数据到结束节点");
                    this.Pub2.Add("功能执行:<a href=\"javascript:DoFunc('ComeBack','" + workid + "','" + fk_Flow + "','" + fk_Node + "')\" >点击执行恢复流程</a>。");
                    this.Pub2.AddBR("说明：如果被成功恢复，ccflow就会把待办工作发送给最后一个结束流程的工作人员。");
                    this.Pub2.AddFieldSetEnd();
                }
                break;
            case WFState.HungUp: // 挂起.
                this.AddUnHungUp();
                break;
            default:
                break;
        }
    }


    public void AddUnHungUp()
    {
        this.Pub2.AddFieldSet("取消挂起");
        if (BP.WF.Dev2Interface.Flow_CheckIsCanDoCurrentWork(workIDint, WebUser.No) == true)
        {
            this.Pub2.Add("功能执行:<a href=\"javascript:DoFunc('UnHungUp','" + workid + "','" + fk_Flow + "','" + fk_Node + "')\" >点击执行取消挂起流程</a>。");
            this.Pub2.AddBR("说明：解除流程挂起的状态。");
        }
        else
        {
            this.Pub2.AddBR("您没有此权限，或者当前不是挂起的状态。");
        }
        this.Pub2.AddFieldSetEnd();
    }

    public void AddHungUp()
    {
        this.Pub2.AddFieldSet("取消挂起");
        if (BP.WF.Dev2Interface.Flow_CheckIsCanDoCurrentWork(workIDint, WebUser.No) == true)
        {
            this.Pub2.Add("功能执行:<a href=\"javascript:DoFunc('UnHungUp','" + workid + "','" + fk_Flow + "','" + fk_Node + "')\" >点击执行取消挂起流程</a>。");
            this.Pub2.AddBR("说明：解除流程挂起的状态。");
        }
        else
        {
            this.Pub2.AddBR("您没有此权限，或者当前不是挂起的状态。");
        }
        this.Pub2.AddFieldSetEnd();
    }

    public void AddShift()
    {
        this.Pub2.AddFieldSet("取消挂起");
        if (BP.WF.Dev2Interface.Flow_CheckIsCanDoCurrentWork(workIDint, WebUser.No) == true)
        {
            this.Pub2.Add("功能执行:<a href=\"javascript:DoFunc('UnHungUp','" + workid + "','" + fk_Flow + "','" + fk_Node + "')\" >点击执行取消挂起流程</a>。");
            this.Pub2.AddBR("说明：解除流程挂起的状态。");
        }
        else
        {
            this.Pub2.AddBR("您没有此权限，或者当前不是挂起的状态。");
        }
        this.Pub2.AddFieldSetEnd();
    }
}