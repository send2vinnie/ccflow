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
using BP.WF;
using BP.Port;
using BP.Sys;
using BP.Web.Controls;
using BP.DA;
using BP.En;
using BP.Web;

public partial class GovDoc_MyFlow : BP.Web.WebPage
{
    #region 按钮与变量
    /// <summary>
    /// 发送
    /// </summary>
    public BP.Web.Controls.Btn Btn_Send
    {
        get
        {
            return this.PubBar.GetBtnByID("Btn_Send");
        }
    }
    /// <summary>
    /// 保存
    /// </summary>
    public BP.Web.Controls.Btn Btn_Save
    {
        get
        {
            return this.PubBar.GetBtnByID("Btn_Save");
        }
    }
    /// <summary>
    /// 退签
    /// </summary>
    public BP.Web.Controls.Btn Btn_Return
    {
        get
        {
            return this.PubBar.GetBtnByID("Btn_Return");
        }
    }
    /// <summary>
    /// 转发
    /// </summary>
    public BP.Web.Controls.Btn Btn_FW
    {
        get
        {
            return this.PubBar.GetBtnByID("Btn_FW");
        }
    }
    /// <summary>
    /// 撤消
    /// </summary>
    public BP.Web.Controls.Btn Btn_UnDo
    {
        get
        {
            return this.PubBar.GetBtnByID("Btn_UnDo");
        }
    }
    #endregion

    /// <summary>
    /// 是否关闭
    /// </summary>
    public bool IsClose
    {
        get
        {
            return true;
            if (this.Request.QueryString["IsClose"] == null)
                return false;
            else
                return true;
        }
    }

    /// <summary>
    /// 当前的流程编号
    /// </summary>
    public string FK_Flow
    {
        get
        {
            return this.Request.QueryString["FK_Flow"];
        }
    }
    public string DoType
    {
        get
        {
            return this.Request.QueryString["DoType"];
        }
    }
    /// <summary>
    /// 当前的工作ID
    /// </summary>
    public Int64 WorkID
    {
        get
        {
            if (ViewState["WorkID"] == null)
            {
                if (this.Request.QueryString["WorkID"] == null)
                    return 0;
                else
                    return Int64.Parse(this.Request.QueryString["WorkID"]);
            }
            else
                return Int64.Parse(ViewState["WorkID"].ToString());
        }
        set
        {
            ViewState["WorkID"] = value;
        }
    }
    /// <summary>
    /// 当前的 NodeID ,在开始时间,nodeID,是地一个,流程的开始节点ID.
    /// </summary>
    public int FK_Node
    {
        get
        {
            string s = this.Request.QueryString["FK_Node"];
            if (s == null)
            {
                return int.Parse(this.FK_Flow + "01");
            }
            return int.Parse(s);
        }
    }
    /// <summary>
    /// 当前的节点
    /// </summary>
    public BP.WF.Node CurrentNode
    {
        get
        {
            return new BP.WF.Node(this.FK_Node);
        }
    }
    private Flow _CurrentFlow = null;

    /// <summary>
    /// 取当前选择的流程
    /// </summary>
    public Flow CurrentFlow
    {
        get
        {
            if (_CurrentFlow == null)
                _CurrentFlow = new Flow(this.FK_Flow);
            return _CurrentFlow;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.AddHeader("P3P", "CP=CAO PSA OUR");

        //BP.WF.XML.FlowBars bars = new BP.WF.XML.FlowBars();
        //bars.RetrieveAll();
        //foreach (BP.WF.XML.FlowBar bar in bars)
        //{
        //this.PubBar.Add("<a href='" + bar.No + "&FK_Flow=" + this.FK_Flow + "&WorkID=" + this.WorkID + "&FK_Node="+this.FK_Node+"' >" + bar.Name + "</a>&nbsp;&nbsp;");

        BP.WF.Node currNd = this.CurrentNode;
        this.PubBar.Add("流程:<b>" + currNd.FlowName + "</b>节点：<b>" + currNd.Name + "</b>");
        this.PubBar.AddHR();


        BP.Web.Controls.Btn btn = new BP.Web.Controls.Btn();
        btn.ID = "Btn_Send";
        btn.Text = " 发送 ";
        btn.Click += new EventHandler(Btn_Click);
        this.PubBar.Add(btn);

        btn = new BP.Web.Controls.Btn();
        btn.ID = "Btn_Save";
        btn.Text = " 保存 ";
        btn.Click += new EventHandler(Btn_Click);
        this.PubBar.Add(btn);

        btn = new BP.Web.Controls.Btn();
        btn.ID = "Btn_Return";
        btn.Text = "退回";
        btn.Attributes["onclick"] = "windows.href='ReturnWork.aspx?FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID + "'";
        btn.Click += new EventHandler(Btn_Click);
        btn.Visible = false;
        this.PubBar.Add(btn);

        btn = new BP.Web.Controls.Btn();
        btn.ID = "Btn_FW";
        btn.Text = "转发";
        btn.Attributes["onclick"] = "windows.href='Forward.aspx?FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID + "'";
        btn.Click += new EventHandler(Btn_Click);
        btn.Visible = false;
        this.PubBar.Add(btn);

        btn = new BP.Web.Controls.Btn();
        btn.ID = "Btn_UnDo";
        btn.Text = "撤消";
        btn.Visible = false;
        btn.Click += new EventHandler(Btn_Click);
        this.PubBar.Add(btn);
       // this.PubBar.Add("<hr>");
        switch (this.DoType)
        {
            case "FlowImg":
                this.PubBar.Add("<img src='../Data/FlowDesc/" + this.FK_Flow + ".gif' border=0  width='100%'/>");
                return;
                break;
            case "Sheet":
            default:
                this.BindSheet();
                break;
        }
    }
    private void
           Send(bool isSave)
    {
        System.Web.HttpContext.Current.Session["RunDT"] = DateTime.Now;
        if (this.FK_Node == 0)
            throw new Exception(this.ToE("NotCurrNode", "没有找到当前的节点"));

        BP.WF.Node currNd = this.CurrentNode;
        Work work = currNd.HisWork;
        work.OID = this.WorkID;
        try
        {
            work = (Work)this.UCEn1.Copy(work);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        try
        {
            work.BeforeSave(); //调用业务逻辑检查。
        }
        catch (Exception ex)
        {
            if (BP.SystemConfig.IsDebug)
                work.CheckPhysicsTable();
            throw ex;
        }

        work.NodeState = NodeState.Init;
        work.Rec = WebUser.No;
        work.SetValByKey("FK_Dept", WebUser.FK_Dept);
        work.SetValByKey("FK_NY", BP.DA.DataType.CurrentYearMonth);

        try
        {
            if (currNd.IsStartNode)
                work.FID = 0;

            if (work.OID == 0)
                work.Insert();
            else
                work.Update(); /* 如果是保存 */
        }
        catch (Exception ex)
        {
            work.CheckPhysicsTable();
            throw ex;
        }


        this.WorkID = work.OID;

        string msg = "";
        // 调用工作流程，处理节点信息采集后保存后的工作。
        if (isSave)
        {
            this.WorkID = work.OID;
            work.RetrieveFromDBSources();
            //work.SetValByKey(CheckWorkAttr.NodeID, this.FK_Node);
            this.UCEn1.Bind(work, "ND" + this.FK_Node, false, false, "FK_Taxpayer");
            this.UCEn1.Add(work.WorkEndInfo);
            return;
        }

        //if (currNd.IsCheckNode)
        //{
        //    if (work.GetValIntByKey(CheckWorkAttr.CheckState) == 2)
        //    {
        //        this.ResponseWriteBlueMsg(this.ToE("FlowHup", "流程挂起"));
        //        return;
        //    }
        //}

        try
        {
            work.BeforeSend(); // 发送前作逻辑检查。
        }
        catch (Exception ex)
        {
            if (BP.SystemConfig.IsDebug  )
                work.CheckPhysicsTable();
            throw ex;
        }

        WorkNode firstwn = new WorkNode(work, currNd);
        try
        {
            msg = firstwn.AfterNodeSave();
            this.Session["Msg"] = msg;
            this.Response.Redirect("DoClient.aspx?DoType=Send", false);
            return;
        }
        catch (Exception ex)
        {
            this.Session["Msg"] = "流程发送失败-<a href='javascript:history.back(-1)' >返回</a><hr>" + ex.Message;
            this.Response.Redirect("DoClient.aspx?DoType=Msg", false);
            return;
        }
    }

    private void Btn_Click(object sender, System.EventArgs e)
    {
        try
        {
            BP.Web.Controls.Btn btn = (BP.Web.Controls.Btn)sender;
            switch (btn.ID)
            {
                case "Btn_Save":
                    this.Send(true);
                    break;
                case BP.Web.Controls.NamesOfBtn.Forward:
                    break;
                case "Btn_WorkerList":
                    if (WorkID == 0)
                        throw new Exception("没有指定当前的工作,不能查看工作者列表.");
                   // this.BtnWorkerList();
                    break;
                case "Btn_PrintWorkRpt":
                    if (WorkID == 0)
                        throw new Exception("没有指定当前的工作,不能打印工作报告.");
                    this.WinOpen("WFRpt.aspx?FK_Flow=" + this.FK_Flow + "&WorkID=" + WorkID, this.ToE("WorkRpt", "工作报告"), 800, 600);
                    //this.BtnEdit();
                    break;
                case NamesOfBtn.Send:
                    this.Send(false);
                    if (this.IsClose)
                    {
                        WinClose();
                        return;
                    }
                    break;
                default:
                   // this.DealNodeRefFunc(btn.ID);
                    break;
            }
        }
        catch (Exception ex)
        {
            this.ResponseWriteRedMsg(ex);
        }
    }


    /// <summary>
    /// 绑定
    /// </summary>
    public void BindSheet()
    {
        BP.WF.Node currND = null;
        if (this.WorkID == 0)
        {
            currND = this.CurrentNode;
            this.New(true, currND);
            this.Btn_Return.Enabled = false;
            this.Btn_FW.Enabled = false;
        }
        else
        {
            if (WorkerLists.CheckUserPower(this.WorkID, WebUser.No) == false && this.CurrentNode.IsStartNode == false)
            {
                this.ToMsgPage("@当前的工作已经被处理，或者您没有执行此工作的权限。");
                return;
            }
            this.BindWork(this.CurrentNode);
        }
    }
    /// <summary>
    /// BindWork
    /// </summary>
    public void BindWork(BP.WF.Node nd)
    {
        if (nd.IsStartNode)
        {
            this.Btn_UnDo.Enabled = false;
            this.Btn_Return.Enabled = false;
            this.Btn_FW.Enabled = false;
        }



        Work wk = nd.HisWork;
        switch (wk.NodeState)
        {
            case NodeState.Complete:
                this.Btn_Send.Enabled = false;
                this.Btn_Save.Enabled = false;
                break;
            default:
                this.Btn_Send.Enabled = true;
                this.Btn_Save.Enabled = true;
                break;
        }

      //  this.Response.Write(wk.NodeState);

        wk.OID = this.WorkID;
        //if (nd.IsCheckNode)
        //{
        //    wk.NodeID = this.FK_Node;
        //}

        wk.Rec = WebUser.No;
        if (wk.OID == 0)
        {
            this.UCEn1.Bind(wk, "ND" + nd.NodeID, false, false, "FK_Taxpayer");
            return;
        }
        else
        {
            wk.Retrieve();
        }

        if (wk.NodeState == NodeState.Back)
        {
            ReturnWork rw = new ReturnWork();
            this.ResponseWriteBlueMsg(rw.NoteHtml);
            //  return;
        }


        this.UCEn1.Bind(wk, "ND" + nd.NodeID, false, false);
        // OutJSAuto(wk);

        this.UCEn1.Add(wk.WorkEndInfo);

        if (nd.ShowSheets.Length < 3)
            return;

        //this.ShowSheets(nd);
    }

    /// <summary>
    /// 新建一个工作
    /// </summary>
    private void New(bool isPostBack, BP.WF.Node nd)
    {
        Flow fl = new Flow(this.FK_Flow);
        this.Btn_Send.Enabled = true;
        this.Btn_Save.Enabled = true;
        this.Btn_UnDo.Enabled = false;
        this.WorkID = 0;

        StartWork wk = (StartWork)nd.HisWork;

        int num = wk.Retrieve(StartWorkAttr.NodeState, 0, StartWorkAttr.Rec, WebUser.No);
        if (num > 1)
        {
            DBAccess.RunSQL("DELETE " + wk.EnMap.PhysicsTable + " WHERE Rec='" + WebUser.No + "' AND NodeState=0 AND OID!=" + wk.OID);
        }

        if (num != 0)
            this.WorkID = wk.OID;

        wk.Title = "";
        wk.Rec = WebUser.No;
        wk.SetValByKey(WorkAttr.RDT, BP.DA.DataType.CurrentDataTime);
        wk.SetValByKey(WorkAttr.CDT, BP.DA.DataType.CurrentDataTime);
        wk.WFState = 0;
        wk.NodeState = 0;
        wk.FK_XJ = WebUser.FK_Dept;
        wk.FK_Dept = WebUser.FK_Dept;

        wk.SetValByKey("FK_DeptText", WebUser.FK_DeptName);
        wk.SetValByKey("FK_XJText", WebUser.FK_DeptName);

        Dept Dept = new Dept(WebUser.FK_Dept);
        wk.SetValByKey("FK_XJText", Dept.Name);
        wk.FID = 0;

        wk.SetValByKey("RecText", WebUser.Name);
        this.UCEn1.Bind(wk,"ND" + nd.NodeID, false, false, "FK_Taxpayer");
        this.UCEn1.Add(wk.WorkEndInfo);
    }
}
