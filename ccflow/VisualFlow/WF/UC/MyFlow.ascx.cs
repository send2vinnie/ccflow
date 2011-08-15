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
using BP.Sys;
using BP.Port;
using BP.Web.Controls;
using BP.DA;
using BP.En;
using BP.Web;
public partial class WF_UC_MyFlow : BP.Web.UC.UCBase3
{
    #region 控件
    public string _PageSamll = null;
    public string PageSmall
    {
        get
        {
            if (_PageSamll == null)
            {
                if (this.PageID.ToLower().Contains("small"))
                    _PageSamll = "Small";
                else
                    _PageSamll = "";
            }
            return _PageSamll;
        }
    }
    /// <summary>
    /// 发送
    /// </summary>
    protected Btn Btn_Send
    {
        get
        {
            return this.ToolBar1.GetBtnByID(NamesOfBtn.Send);
        }
    }
    protected Btn Btn_Delete
    {
        get
        {
            return this.ToolBar1.GetBtnByID(NamesOfBtn.Delete);
        }
    }
    /// <summary>
    /// 保存
    /// </summary>
    protected Btn Btn_Save
    {
        get
        {
            Btn btn= this.ToolBar1.GetBtnByID(NamesOfBtn.Save);
            if (btn == null)
                btn = new Btn();
            return btn;
        }
    }
    protected Btn Btn_ReturnWork
    {
        get
        {
            Btn btn = this.ToolBar1.GetBtnByID("Btn_ReturnWork");
            if (btn == null)
                btn = new Btn();
            return btn;
        }
    }
    protected Btn Btn_Shift
    {
        get
        {
            return this.ToolBar1.GetBtnByID(BP.Web.Controls.NamesOfBtn.Shift);
        }
    }
    #endregion

    #region  运行变量
    /// <summary>
    /// 当前的流程编号
    /// </summary>
    public string FK_Flow
    {
        get
        {
            string s = this.Request.QueryString["FK_Flow"];
            if (string.IsNullOrEmpty(s))
                throw new Exception("@流程编号参数错误...");
            return s;
        }
    }
    public string FK_Node_From
    {
        get
        {
            return  this.Request.QueryString["FK_Node_From"];
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
            string fk_nodeReq = this.Request.QueryString["FK_Node"];
            if (string.IsNullOrEmpty(fk_nodeReq))
                fk_nodeReq = this.Request.QueryString["NodeID"];

            if (string.IsNullOrEmpty(fk_nodeReq) == false)
                return int.Parse(fk_nodeReq);

            if (ViewState["FK_Node"] == null)
            {
                ViewState["FK_Node"] = int.Parse(this.FK_Flow + "01");
                return int.Parse(this.FK_Flow + "01");
            }
            else
            {
                return (int)ViewState["FK_Node"];
            }
        }
        set
        {
            ViewState["FK_Node"] = value;
        }
    }
    public int FID
    {
        get
        {
            try
            {
                return int.Parse(this.Request.QueryString["FID"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    #endregion

    #region 动态控件
    /// <summary>
    /// 节点名称
    /// </summary>
    public Label Lab_NodeName
    {
        get
        {
            return this.ToolBar1.GetLabelByID("Lab_NodeName");
        }
    }
    #endregion

    #region Page load 事件
    public void DoDoType()
    {
        switch (this.DoType)
        {
            case "Runing":
                ShowRuning();
                return;
            case "History":
                ShowHistory();
                return;
            case "Warting":
                ShowWarting();
                return;
            default:
                break;
        }
    }
    public void ShowWarting()
    {
        this.ToolBar1.AddLab("s", this.ToE("PendingWork", "待办工作"));
        string sql = "SELECT * FROM WF_EmpWorks WHERE FK_Emp='" + BP.Web.WebUser.No + "'  AND FK_Flow='" + this.FK_Flow + "' ORDER BY WorkID ";
        DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
        if (dt.Rows.Count == 0)
            return;
        int i = 0;
        bool is1 = false;
        DateTime cdt = DateTime.Now;
        string color = "";
        this.Pub1.AddTable();
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("ID");
        this.Pub1.AddTDTitle(this.ToE("NodeName", "节点"));
        this.Pub1.AddTDTitle(this.ToE("Title", "标题"));
        this.Pub1.AddTDTitle(this.ToE("Starter", "发起"));
        this.Pub1.AddTDTitle(this.ToE("RDT", "发起日期"));
        this.Pub1.AddTDTitle(this.ToE("ADT", "接受日期"));
        this.Pub1.AddTDTitle(this.ToE("SDT", "期限"));
        this.Pub1.AddTREnd();

        int idx = 0;
        foreach (DataRow dr in dt.Rows)
        {
            string sdt = dr["SDT"] as string;
            DateTime mysdt = DataType.ParseSysDate2DateTime(sdt);
            if (cdt >= mysdt)
            {
                this.Pub1.AddTRRed(); // ("onmouseover='TROver(this)' onmouseout='TROut(this)' onclick=\"\" ");
            }
            else
            {
                is1 = this.Pub1.AddTR(is1); // ("onmouseover='TROver(this)' onmouseout='TROut(this)' onclick=\"\" ");
            }
            i++;
            this.Pub1.AddTD(i);
            this.Pub1.AddTD(dr["NodeName"].ToString());
            this.Pub1.AddTD("<a href=\"MyFlow" + this.PageSmall + ".aspx?FK_Flow=" + dr["FK_Flow"] + "&WorkID=" + dr["WorkID"] + "&FID=" + dr["FID"] + "\" >" + dr["Title"].ToString());
            this.Pub1.AddTD(dr["Starter"].ToString());
            this.Pub1.AddTD(dr["RDT"].ToString());
            this.Pub1.AddTD(dr["ADT"].ToString());
            this.Pub1.AddTD(dr["SDT"].ToString());
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEnd();
    }
    public void ShowRuning()
    {
        this.ToolBar1.AddLab("s", this.ToE("OnTheWayWork", "在途工作"));

        this.Pub1.AddTable("width='80%' align=left");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("nowarp=true", this.ToE("IDX", "序"));
        this.Pub1.AddTDTitle("nowarp=true", this.ToE("Name", "名称"));
        this.Pub1.AddTDTitle("nowarp=true", this.ToE("CurrNode", "当前节点"));
        this.Pub1.AddTDTitle("nowarp=true", this.ToE("StartDate", "发起日期"));
        this.Pub1.AddTDTitle("nowarp=true", this.ToE("Emp", "发起人"));
        this.Pub1.AddTDTitle("nowarp=true", this.ToE("Oper", "操作"));
        this.Pub1.AddTREnd();

        string sql = "  SELECT a.WorkID FROM WF_GenerWorkFlow A, WF_GenerWorkerlist B  WHERE A.WorkID=B.WorkID AND B.FK_Emp='" + BP.Web.WebUser.No + "' AND B.IsEnable=1 AND B.IsPass=1 AND b.FK_Flow='" + this.FK_Flow + "'";
        //this.Response.Write(sql);

        GenerWorkFlowExts gwfs = new GenerWorkFlowExts();
        gwfs.RetrieveInSQL(GenerWorkFlowAttr.WorkID, "(" + sql + ")");
        int i = 0;
        bool is1 = false;
        string FromPageType=Glo.FromPageType;
        foreach (GenerWorkFlowExt gwf in gwfs)
        {
            i++;
            is1 = this.Pub1.AddTR(is1);
            this.Pub1.AddTDIdx(i);
            this.Pub1.AddTDA("MyFlow" + this.PageSmall + ".aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow, gwf.Title);
            this.Pub1.AddTD(gwf.FK_NodeText);
            this.Pub1.AddTD(gwf.RDT);
            this.Pub1.AddTD(gwf.RecText);

            this.Pub1.AddTDBegin();
            this.Pub1.Add("<a href=\"javascript:Do('" + this.ToE("AYS", "您确认吗？") + "','MyFlowInfo" + this.PageSmall + ".aspx?DoType=UnSend&FID=" + gwf.FID + "&WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "');\" ><img src='../images/btn/delete.gif' border=0 />" + this.ToE("UnDo", "撤消") + "</a>");
            this.Pub1.Add("-<a href=\"javascript:WinOpen('./../WF/WFRpt.aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "&FID=0')\" ><img src='../images/btn/rpt.gif' border=0 />" + this.ToE("WorkRpt", "报告") + "</a>");
            this.Pub1.Add("-<a href=\"javascript:WinOpen('./../WF/Chart.aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "&FID=0')\" ><img src='./Img/Track.gif' border=0 />" + this.ToE("Track", "轨迹") + "</a>");
            this.Pub1.AddTDEnd();
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTRSum();
        this.Pub1.AddTD("colspan=6","&nbsp;");
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();
    }
    public void ShowHistory()
    {
        BP.WF.Node nd = new BP.WF.Node(this.FK_Node);
        this.ToolBar1.AddLab("s", this.ToE("HistoryWork", "历史工作") + ":" + nd.Name);
        Works wks = nd.HisWorks;
        QueryObject qo = new QueryObject(wks);
        qo.AddWhere(WorkAttr.Rec, WebUser.No);
        qo.addAnd();
        qo.AddWhereInSQL(WorkAttr.OID,
            "SELECT OID FROM  ND" + int.Parse(nd.FK_Flow) + "Rpt WHERE WFState=" + (int)WFState.Complete);
        //qo.AddWhere(WorkAttr.NodeState, 1);
        qo.DoQuery();
        this.Pub1.BindWorkDtl(nd, wks);
    }
    #endregion

    #region 变量
    public Flow currFlow = null;
    public Work currWK = null;
    public BP.WF.Node currND = null;
    #endregion


    #region Page load 事件
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, System.EventArgs e)
    {
       if (this.DoType != null)
        {
            DoDoType();
            return;
        }

       string appPath = this.Request.ApplicationPath;
       this.currFlow = new Flow(this.FK_Flow);
       this.currND = new BP.WF.Node(this.FK_Node);

        #region 判断是否有 workid
        if (this.WorkID == 0)
        {
            currWK = this.currFlow.NewWork();
        }
        else
        {
            currWK = this.currFlow.GenerWork(this.WorkID,this.currND);

            string msg = "";
            switch (currWK.NodeState)
            {
                case NodeState.Back:
                    /* 如果工作节点退回了*/
                    ReturnWork rw = new ReturnWork();
                    rw.WorkID = this.WorkID;
                    rw.NodeId = this.FK_Node;
                    if (rw.Retrieve(ReturnWorkAttr.NodeId, this.FK_Node, ReturnWorkAttr.WorkID, rw.WorkID) != 0)
                    {
                        this.FlowMsg.AlertMsg_Info("流程退回提示", rw.NoteHtml);
                        currWK.Update("NodeState", (int)NodeState.Init);
                    }
                    break;
                case NodeState.Forward:
                    /* 如果不是退回来的，就判断是否是转发过来的。 */
                    ForwardWork fw = new ForwardWork();
                    int i = fw.Retrieve(ForwardWorkAttr.WorkID, this.WorkID,
                        ForwardWorkAttr.NodeId, this.FK_Node);
                    if (i == 1)
                    {
                        if (fw.IsTakeBack == false)
                        {
                            msg += "@" + this.ToE("Transfer", "转发人") + "[" + fw.FK_Emp + "]。@" + this.ToE("Accepter", "接受人") + "：" + fw.Emps + "。@" + this.ToE("FWNote", "转发原因") + "： @" + fw.NoteHtml;
                            this.FlowMsg.AlertMsg_Info("转发提示:", msg);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion 判断是否有workid

        #region 判断权限
        if (this.IsPostBack == false)
        {
            if (currND.IsStartNode == false && WorkerLists.CheckUserPower(this.WorkID, WebUser.No) == false)
            {
                this.ToolBar1.Clear();
                this.ToolBar1.Add("&nbsp;");

                this.FlowMsg.Clear();
                this.FlowMsg.DivInfoBlockBegin(); //("<b>提示</b><hr>@当前的工作已经被处理，或者您没有执行此工作的权限。<br>@您可以执行如下操作。<ul><li><a href='Start.aspx'>发起新流程。</a></li><li><a href='Runing.aspx'>返回在途工作列表。</a></li></ul>");
                this.FlowMsg.AddB(this.ToE("Note", "提示"));
                this.FlowMsg.AddHR();

                this.FlowMsg.Add(this.ToE("FW1", "@当前的工作已经被处理，或者您没有执行此工作的权限。<br>@您可以执行如下操作。"));

                this.FlowMsg.AddUL();
                if (WebUser.IsWap)
                    this.FlowMsg.AddLi("<a href='Home.aspx'><img src='./Img/Home.gif' border=0/>" + this.ToE("Home", "返回主页") + "</a>");
                this.FlowMsg.AddLi("<a href='Start" + this.PageSmall + ".aspx'><img src='./Img/Start.gif' border=0/>" + this.ToE("StartWork", "发起流程") + "</a>");
                this.FlowMsg.AddLi("<a href='Runing" + this.PageSmall + ".aspx'><img src='./Img/Runing.gif' border=0/>" + this.ToE("OnTheWayWork", "在途工作") + "</a>");
                this.FlowMsg.AddULEnd();

                this.FlowMsg.DivInfoBlockEnd();
                return;
            }
        }
        this.LoadPop();
        #endregion 判断权限

        try
        {
            #region 增加按钮
            BtnLab btnLab = new BtnLab(currND.NodeID);
            if (btnLab.SendEnable)
            {
                this.ToolBar1.AddBtn(NamesOfBtn.Send, btnLab.SendLab);
                this.Btn_Send.UseSubmitBehavior = false;
                this.Btn_Send.OnClientClick = "this.disabled=true;"; //this.disabled='disabled'; return true;";
                this.Btn_Send.Click += new System.EventHandler(this.ToolBar1_ButtonClick);
            }

            if (btnLab.SaveEnable)
            {
                this.ToolBar1.AddBtn(NamesOfBtn.Save, btnLab.SaveLab);
                this.Btn_Save.UseSubmitBehavior = false;
                this.Btn_Save.OnClientClick = "this.disabled=true;"; //this.disabled='disabled'; return true;";
                this.Btn_Save.Click += new System.EventHandler(this.ToolBar1_ButtonClick);
            }

            if (btnLab.ReturnEnable && this.currND.IsStartNode == false)
            {
                this.ToolBar1.AddBtn("Btn_ReturnWork", btnLab.ReturnLab);
                this.Btn_ReturnWork.Click += new System.EventHandler(this.ToolBar1_ButtonClick);
            }

            if (btnLab.ShiftEnable)
            {
                this.ToolBar1.AddBtn("Btn_Shift", btnLab.ShiftLab);
                this.Btn_Shift.Click += new System.EventHandler(this.ToolBar1_ButtonClick);
            }

            if (btnLab.CCEnable)
                this.ToolBar1.Add("<input type=button value='" + btnLab.CCLab + "' enable=true onclick=\"WinOpen('" + appPath + "/WF/Msg/Write.aspx?WorkID=" + this.WorkID + "&FK_Node=" + this.FK_Node + "','ds'); \" />");

            if (btnLab.DeleteEnable)
            {
                this.ToolBar1.AddBtn("Btn_Delete", btnLab.DeleteLab);
                this.Btn_Delete.OnClientClick = "return confirm('" + this.ToE("AYS", "将要执行删除流程，您确认吗？") + "')";
                this.Btn_Delete.Click += new System.EventHandler(this.ToolBar1_ButtonClick);
            }

            if (btnLab.EndFlowEnable && this.currND.IsStartNode == false)
            {
                this.ToolBar1.AddBtn("Btn_EndFlow", btnLab.EndFlowLab);
                this.ToolBar1.GetBtnByID("Btn_EndFlow").OnClientClick = "return confirm('" + this.ToE("AYS", "将要执行终止流程，您确认吗？") + "')";
                this.ToolBar1.GetBtnByID("Btn_EndFlow").Click += new System.EventHandler(this.ToolBar1_ButtonClick);
            }


            if (btnLab.RptEnable)
                this.ToolBar1.Add("<input type=button value='" + btnLab.RptLab + "' enable=true onclick=\"WinOpen('" + appPath + "/WF/WFRpt.aspx?WorkID=" + this.WorkID + "&FK_Flow=" + this.FK_Flow + "&FID=" + this.FID + "','ds0'); \" />");

            if (btnLab.TrackEnable)
                this.ToolBar1.Add("<input type=button value='" + btnLab.TrackLab + "' enable=true onclick=\"WinOpen('" + appPath + "/WF/Chart.aspx?WorkID=" + this.WorkID + "&FK_Flow=" + this.FK_Flow + "&FID=" + this.FID + "','ds'); \" />");

            if (currND.HisFJOpen != FJOpen.None)
            {
                if (this.WorkID == 0)
                    this.ToolBar1.Add("<input type=button value='" + this.ToE("Adjunct", "附件") + "' onclick=\"javascript:alert('" + this.ToE("ForFJ", "请保存后上传附件") + "');\" enable=false  />");
                else
                    this.ToolBar1.Add("<input type=button value='" + this.ToE("Adjunct", "附件") + "' enable=true onclick=\"WinOpen('" + appPath + "/WF/FileManager.aspx?WorkID=" + this.WorkID + "&FK_Node=" + currND.NodeID + "&FK_Flow=" + this.FK_Flow + "&FJOpen=" + (int)currND.HisFJOpen + "&FID=" + this.FID + "','dds'); \" />");
            }

            if (btnLab.OptEnable)
                this.ToolBar1.Add("<input type=button value='" + btnLab.OptLab + "' onclick=\"WinOpen('" + appPath + "/WF/WorkOpt/Home.aspx?WorkID=" + this.WorkID + "&FK_Node=" + currND.NodeID + "&FK_Flow=" + this.FK_Flow + "&FID=" + this.FID + "','dds'); \"  />");

            #endregion

            this.BindWork(currND, currWK);
            this.Session["Ect"] = null;
            if (currND.HisDeliveryWay == DeliveryWay.BySelected && currND.IsEndNode == false)
            {
                this.ToolBar1.Add("<input type=button value='" + this.ToE("JSRen", "接受人") + "' enable=true onclick=\"WinOpen('" + appPath + "/WF/Accpter.aspx?WorkID=" + this.WorkID + "&FK_Node=" + currND.NodeID + "&FK_Flow=" + this.FK_Flow + "&FID=" + this.FID + "','dds'); \" />");
            }
        }
        catch (Exception ex)
        {
            #region 解决开始节点数据库字段变化修复数据库问题 。
            string rowUrl = this.Request.RawUrl;
            if (rowUrl.IndexOf("rowUrl") > 1)
            {
            }
            else
            {
                this.Response.Redirect(rowUrl + "&rowUrl=1", true);
                return;
            }
            #endregion

            this.FlowMsg.DivInfoBlock(ex.Message);
            string Ect = this.Session["Ect"] as string;
            if (Ect == null)
                Ect = "0";
            if (int.Parse(Ect) < 2)
            {
                this.Session["Ect"] = int.Parse(Ect) + 1;
                return;
            }
            return;
        }
    }
    #endregion

    #region 公共方法
    /// <summary>
    /// BindWork
    /// </summary>
    public void BindWork(BP.WF.Node nd, Work wk)
    {
        switch (nd.HisNodeWorkType)
        {
            case NodeWorkType.StartWorkFL:
            case NodeWorkType.WorkFHL:
            case NodeWorkType.WorkFL:
            case NodeWorkType.WorkHL:
                if (this.FID != 0)
                {
                    /* 这种情况是分流节点向退回到了分河流。*/
                    //this.Btn_Send.Enabled = false;
                    //this.Btn_Save.Enabled = false;
                    //this.Btn_ReturnWork.Enabled = false;
                    //this.Btn_Delete.Enabled = false;
                    //this.Btn_Send.Enabled = false;
                    //this.Btn_ReturnWork.Enabled = false;

                    this.UCEn1.AddFieldSet("分流节点退回信息");

                    ReturnWork rw = new ReturnWork();
                    rw.Retrieve(ReturnWorkAttr.WorkID, this.WorkID, ReturnWorkAttr.NodeId, nd.NodeID);
                    this.UCEn1.Add(rw.NoteHtml);
                    this.UCEn1.AddHR();
                    //this.UCEn1.addb
                    TextBox tb = new TextBox();
                    tb.ID = "TB_Doc";
                    tb.TextMode = TextBoxMode.MultiLine;
                    tb.Rows = 7;
                    tb.Columns = 50;
                    this.UCEn1.Add(tb);

                    this.UCEn1.AddBR();
                    Btn btn = new Btn();
                    btn.ID = "Btn_Reject";
                    btn.Text = "驳回工作";
                    btn.Click += new EventHandler(ToolBar1_ButtonClick);
                    this.UCEn1.Add(btn);

                    btn = new Btn();
                    btn.ID = "Btn_KillSubFlow";
                    btn.Text = "终止工作";
                    btn.Click += new EventHandler(ToolBar1_ButtonClick);
                    this.UCEn1.Add(btn);
                    this.UCEn1.AddFieldSetEnd(); // ("分流节点退回信息");

                    //this.ToolBar1.Controls.Clear();//.Clear();
                    //this.Response.Write("<script language='JavaScript'> DoSubFlowReturn('" + this.FID + "','" + wk.OID + "','" + nd.NodeID + "');</script>");
                    //this.Response.Write("<javascript ></javascript>");
                    return;
                }
                break;
            default:
                break;
        }
        if (nd.IsStartNode)
        {
            /*判断是否来与子流程.*/
            if (string.IsNullOrEmpty(this.Request.QueryString["FK_Node_From"]) == false)
            {
                /* 如果来自于主流程 */
                int fk_node_From = int.Parse(this.Request.QueryString["FK_Node_From"]);
                BP.WF.Node fk_node_From_nd = new BP.WF.Node(fk_node_From);
                Work fromWk = fk_node_From_nd.HisWork;
                fromWk.OID = this.FID;
                fromWk.RetrieveFromDBSources();
                wk.Copy(fromWk);
                wk.FID = this.FID;
            }
        }


        // 处理传递过来的参数。
        foreach (string k in this.Request.QueryString.AllKeys)
        {
            wk.SetValByKey(k, this.Request.QueryString[k]);
        }

        #region 设置默认值。
        MapAttrs mattrs = new MapAttrs("ND" + nd.NodeID);
        foreach (MapAttr attr in mattrs)
        {
            if (attr.UIIsEnable)
                continue;

            if (attr.DefValReal.Contains("@") == false)
                continue;

            wk.SetValByKey(attr.KeyOfEn, attr.DefVal);
        }
        #endregion 设置默认值。

        #region 判断是否合流节点。。
        if (nd.HisNodeWorkType == NodeWorkType.WorkHL || nd.HisNodeWorkType == NodeWorkType.WorkFHL)
        {
            WorkerLists wls = new WorkerLists();
            QueryObject qo = new QueryObject(wls);
            qo.AddWhere(WorkerListAttr.FID, wk.OID);
            qo.addAnd();
            qo.AddWhere(WorkerListAttr.FK_Node, nd.HisFromNodes[0].GetValByKey(NodeAttr.NodeID));
            qo.DoQuery();

            this.Pub2.AddFieldSet("分流信息");
            this.Pub2.AddTable("border=0"); // ("<table border=0 >");
            this.Pub2.AddTR();
            this.Pub2.AddTDTitle("账号");
            this.Pub2.AddTDTitle("名称");
            this.Pub2.AddTDTitle("部门");
            this.Pub2.AddTDTitle("状态");
            this.Pub2.AddTDTitle("应完成日期");
            this.Pub2.AddTDTitle("实际完成日期");
            this.Pub2.AddTDTitle("");
            this.Pub2.AddTREnd();

            bool isHaveRuing = false;
            bool is1 = false;
            foreach (WorkerList wl in wls)
            {
                is1 = this.Pub2.AddTR(is1);
                this.Pub2.AddTD(wl.FK_Emp);
                this.Pub2.AddTD(wl.FK_EmpText);
                this.Pub2.AddTD(wl.FK_DeptT);

                if (wl.IsPass)
                {
                    this.Pub2.AddTD("已完成");
                    this.Pub2.AddTD(wl.SDT);
                    this.Pub2.AddTD(wl.RDT);
                }
                else
                {
                    this.Pub2.AddTD("未完成-<a href=\"javascript:WinOpen('');\"><img src='./Img/sms.gif' border=0/>催办</a>");
                    this.Pub2.AddTD(wl.SDT);
                    this.Pub2.AddTD();
                }

                if (wl.IsPass == false)
                {
                    isHaveRuing = true;
                    this.Pub2.AddTD("<a href=\"javascript:DoDelSubFlow('" + wl.FK_Flow + "','" + wl.WorkID + "')\"><img src='./../Images/Btn/Delete.gif' border=0/>终止</a>");
                }
                else
                {
                    this.Pub2.AddTD("<a href=\"javascript:WinOpen('FHLFlow.aspx?WorkID=" + wl.WorkID + "&FID=" + wl.FID + "&FK_Flow=" + nd.FK_Flow + "&FK_Node=" + this.FK_Node + "')\">打开</a>");
                }
                this.Pub2.AddTREnd();
            }
            if (isHaveRuing)
            {
                if (nd.IsForceKill == false)
                    this.Btn_Send.Enabled = false;
            }
            this.Pub2.AddTableEnd();
            this.Pub2.AddFieldSetEnd(); //.AddFieldSet("分流信息");
        }
        #endregion 判断是否合流节点。

        switch (nd.HisFormType)
        {
            case FormType.FixForm:
                this.UCEn1.BindColumn4(wk, "ND" + nd.NodeID); //, false, false, null);
                if (wk.WorkEndInfo.Length > 2)
                {
                    this.UCEn1.Add(wk.WorkEndInfo);
                }
                return;
            case FormType.FreeForm:
            case FormType.DisableIt:
                Frms frms = nd.HisFrms;
                if (frms.Count == 0)
                {
                    this.UCEn1.Add("<div id=divFreeFrm >");
                    this.UCEn1.BindFreeFrm(wk, "ND" + nd.NodeID, false); //, false, false, null);
                    if (wk.WorkEndInfo.Length > 2)
                    {
                        this.UCEn1.Add(wk.WorkEndInfo);
                    }
                    this.UCEn1.Add("</div>");
                }
                else
                {
                    /* 涉及到多个表单的情况...*/
                    if (nd.HisFormType != FormType.DisableIt)
                    {
                        Frm myfrm = new Frm();
                        myfrm.No = "ND" + nd.NodeID;
                        myfrm.Name = wk.EnDesc;

                        FrmNode fnNode = new FrmNode();
                        fnNode.FK_Frm = myfrm.No;
                        fnNode.IsReadonly = false;
                        fnNode.IsPrint = false;
                        myfrm.HisFrmNode = fnNode;
                        frms.AddEntity(myfrm, 0);
                    }

                    Int64 fid = this.FID;
                    if (this.FID == 0)
                        fid = this.WorkID;

                    if (frms.Count == 1)
                    {
                        /* 如果禁用了节点表单，并且只有一个表单的情况。*/
                        Frm frm = (Frm)frms[0];
                        FrmNode fn = frm.HisFrmNode;
                        string src = "";
                        src = "Frm.aspx?FK_MapData=" + frm.No + "&FID=" + fid + "&IsReadonly=" + fn.IsReadonlyInt + "&IsPrint=" + fn.IsPrintInt + "&FK_Node=" + nd.NodeID + "&WorkID=" + this.WorkID;
                        this.UCEn1.Add("\t\n <DIV id='" + frm.No + "' style='width:" + frm.FrmW + "px; height:" + frm.FrmH + "px;text-align: left;' >");
                        this.UCEn1.Add("\t\n <iframe ID='F" + frm.No + "' src='" + src + "' frameborder=0  style='position:absolute;width:" + frm.FrmW + "px; height:" + frm.FrmH + "px;text-align: left;'  leftMargin='0'  topMargin='0' scrolling=no /></iframe>");
                        this.UCEn1.Add("\t\n </DIV>");
                    }
                    else
                    {
                        #region 载入相关文件.
                        this.Page.RegisterClientScriptBlock("sg",
           "<link href='./Style/Frm/Tab.css' rel='stylesheet' type='text/css' />");

                        this.Page.RegisterClientScriptBlock("s2g4",
                 "<script language='JavaScript' src='./Style/Frm/jquery.min.js' ></script>");

                        this.Page.RegisterClientScriptBlock("sdf24j",
                "<script language='JavaScript' src='./Style/Frm/jquery.idTabs.min.js' ></script>");

                        this.Page.RegisterClientScriptBlock("sdsdf24j",
                "<script language='JavaScript' src='./Style/Frm/TabClick.js' ></script>");
                        #endregion 载入相关文件.

                        this.UCEn1.Clear();
                        this.UCEn1.Add("<div  style='clear:both' ></div>");
                        this.UCEn1.Add("\t\n<div  id='usual2' class='usual' >");  //begain.

                        #region 输出标签.
                        this.UCEn1.Add("\t\n <ul  class='abc' style='background:red;border-color: #800000;border-width: 10px;' >");
                        foreach (Frm frm in frms)
                        {
                            FrmNode fn = frm.HisFrmNode;
                            string src = "";
                            src = "Frm.aspx?FK_MapData=" + frm.No + "&FID=" + fid + "&IsReadonly=" + fn.IsReadonlyInt + "&IsPrint=" + fn.IsPrintInt + "&FK_Node=" + nd.NodeID + "&WorkID=" + this.WorkID;
                            this.UCEn1.Add("\t\n<li><a href=\"#" + frm.No + "\" onclick=\"TabClick('" + frm.No + "','" + src + "');\" >" + frm.Name + "</a></li>");
                        }
                        this.UCEn1.Add("\t\n </ul>");
                        #endregion 输出标签.

                        #region 输出表单 iframe 内容.
                        foreach (Frm frm in frms)
                        {
                            FrmNode fn = frm.HisFrmNode;
                            this.UCEn1.Add("\t\n <DIV id='" + frm.No + "' style='width:" + frm.FrmW + "px; height:" + frm.FrmH + "px;text-align: left;' >");
                            string src = "loading.htm";
                            this.UCEn1.Add("\t\n <iframe ID='F" + frm.No + "' src='" + src + "' frameborder=0  style='position:absolute;width:" + frm.FrmW + "px; height:" + frm.FrmH + "px;text-align: left;'  leftMargin='0'  topMargin='0' scrolling=no /></iframe>");
                            this.UCEn1.Add("\t\n </DIV>");
                        }
                        #endregion 输出表单 iframe 内容.

                        this.UCEn1.Add("\t\n</div>"); // end  usual2

                        // 设置选择的默认值.
                        this.UCEn1.Add("\t\n<script type='text/javascript'>");
                        this.UCEn1.Add("\t\n  $(\"#usual2 ul\").idTabs(\"" + frms[0].No + "\");");
                        this.UCEn1.Add("\t\n</script>");
                    }
                }
                return;
            case FormType.SelfForm:
            case FormType.SDKForm:
                wk.Save();
                if (this.WorkID == 0)
                {
                    this.WorkID = wk.OID;
                }
                string url = nd.FormUrl;
                string urlExt = this.RequestParas;
                if (urlExt.Contains("WorkID") == false)
                    urlExt += "&WorkID=" + this.WorkID;

                if (urlExt.Contains("NodeID") == false)
                    urlExt += "&NodeID=" + nd.NodeID;

                if (urlExt.Contains("FK_Node") == false)
                    urlExt += "&FK_Node=" + nd.NodeID;

                if (urlExt.Contains("FID") == false)
                    urlExt += "&FID=" + wk.FID;

                urlExt += "&UserNo=" + WebUser.No + "&SID=" + WebUser.SID;

                if (url.Contains("?") == true)
                    url += "&" + urlExt;
                else
                    url += "?" + urlExt;

                if (nd.HisFormType == FormType.SDKForm)
                {
                    this.Response.Redirect(url, true);
                }
                else
                {
                    //  this.UCEn1.AddIframeExt(url, nd.FrmAttr);
                    this.UCEn1.AddIframeAutoSize(url, "D" + this.FK_Flow, "WorkPlace");
                    this.UCEn1.Add(wk.WorkEndInfo);
                }
                return;
            default:
                throw new Exception("@没有涉及到的扩充。");
        }
    }
    public void OutJSAuto(Entity en)
    {
        if (en.EnMap.IsHaveJS == false)
            return;

        Attrs attrs = en.EnMap.Attrs;
        string js = "";
        foreach (Attr attr in attrs)
        {
            if (attr.UIContralType != UIContralType.TB)
                continue;

            if (attr.IsNum == false)
                continue;

            string tbID = "TB_" + attr.Key;
            TB tb = this.UCEn1.GetTBByID(tbID);
            if (tb == null)
                continue;

            tb.Attributes["OnKeyPress"] = "javascript:C();";
            tb.Attributes["onkeyup"] = "javascript:C();";

            if (attr.MyDataType == DataType.AppInt)
                tb.Attributes["OnKeyDown"] = "javascript:return VirtyInt(this);";
            else
                tb.Attributes["OnKeyDown"] = "javascript:return VirtyNum(this);";

            //   tb.Attributes["OnKeyDown"] = "javascript:return VirtyNum(this);";

            if (attr.MyDataType == DataType.AppMoney)
                tb.Attributes["onblur"] = "this.value=VirtyMoney(this.value);";

            if (attr.AutoFullWay == AutoFullWay.Way1_JS)
            {
                js += attr.Key + "," + attr.AutoFullDoc + "~";
                tb.Enabled = true;
            }
        }

        string[] strs = js.Split('~');

        ArrayList al = new ArrayList();
        foreach (string str in strs)
        {
            if (str == null || str == "")
                continue;

            string key = str.Substring(0, str.IndexOf(','));
            string exp = str.Substring(str.IndexOf(',') + 1);

            string left = "\n  document.forms[0].UCEn1_TB_" + key + ".value = ";
            foreach (Attr attr in attrs)
            {
                exp = exp.Replace("@" + attr.Key, "  parseFloat( document.forms[0].UCEn1_TB_" + attr.Key + ".value.replace( ',' ,  '' ) ) ");
                exp = exp.Replace("@" + attr.Desc, " parseFloat( document.forms[0].UCEn1_TB_" + attr.Key + ".value.replace( ',' ,  '' ) ) ");
            }
            al.Add(left + exp);
        }

        string body = "";
        foreach (string s in al)
        {
            body += s;
        }

        this.Response.Write("<script language='JavaScript'> function  C(){ " + body + " }  \n </script>");
    }
    /// <summary>
    /// 显示 关联表单
    /// </summary>
    /// <param name="nd"></param>
    public void ShowSheets(BP.WF.Node nd, Work currWk)
    {
        if (nd.HisFJOpen != FJOpen.None)
        {
            string url = "FileManager.aspx?WorkID=" + this.WorkID + "&FID=" + currWk.FID
                + "&FJOpen=" + (int)nd.HisFJOpen + "&FK_Node=" + nd.NodeID;
            this.Pub1.Add("<iframe leftMargin='0' topMargin='0' src='" + url + "' width='100%' height='200px' class=iframe name=fm style='border-style:none;' id=fm > </iframe>");
        }

        //this.Pub1.AddIframe("FileManager.aspx?WorkID="+this.WorkID+"&FID=0&FJOpen=1&FK_Node="+nd.NodeID);

        if (this.Pub1.Controls.Count > 20)
            return;

        // 显示设置的步骤.
        string[] strs = nd.ShowSheets.Split('@');

        if (strs.Length >= 1)
            this.Pub1.AddHR();

        foreach (string str in strs)
        {
            if (str == null || str == "")
                continue;

            int FK_Node = int.Parse(str);
            BP.WF.Node mynd;
            try
            {
                mynd = new BP.WF.Node(FK_Node);
            }
            catch
            {
                nd.ShowSheets = nd.ShowSheets.Replace("@" + FK_Node, "");
                nd.Update();
                continue;
            }

            Work nwk = mynd.HisWork;
            nwk.OID = this.WorkID;


            if (nwk.RetrieveFromDBSources() == 0)
                continue;

            // this.Pub1.AddB("== " + mynd.Name + " ==<hr width=90%>");

            this.Pub1.AddFieldSet("历史步骤:" + mynd.Name);
            // this.Pub1.DivInfoBlockBegin();
            this.Pub1.ADDWork(nwk, new ReturnWorks(), new ForwardWorks(), nd.NodeID);

            this.Pub1.AddFieldSetEnd(); // (mynd.Name);

            //this.Pub1.DivInfoBlockEnd(); // (mynd.Name);
        }
    }
    #endregion

    #region Web 窗体设计器生成的代码
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
        //
        InitializeComponent();
        base.OnInit(e);
    }

    /// <summary>
    /// 设计器支持所需的方法 - 不要使用代码编辑器修改
    /// 此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {

    }
    #endregion

    #region toolbar 2
    private void ToolBar1_ButtonClick(object sender, System.EventArgs e)
    {
        try
        {
            Btn btn = (Btn)sender;
            switch (btn.ID)
            {
                case "Btn_Reject":
                case "Btn_KillSubFlow":
                    try
                    {
                        WorkFlow wkf = new WorkFlow(this.FK_Flow, this.WorkID);
                        if (btn.ID == "Btn_KillSubFlow")
                        {
                            this.ToMsg("删除流程信息:<hr>" + wkf.DoDeleteWorkFlowByReal(), "info");
                        }
                        else
                        {
                            string msg = wkf.DoReject(this.FID, this.FK_Node, this.UCEn1.GetTextBoxByID("TB_Doc").Text);
                            this.ToMsg(msg, "info");
                        }
                        return;
                    }
                    catch (Exception ex)
                    {
                        this.ToMsg(ex.Message, "info");
                        return;
                    }
                case "Btn_EndFlow": //结束流程。
                    WorkFlow mywf = null;
                    if (this.FID == 0)
                        mywf = new WorkFlow(this.FK_Flow, this.WorkID);
                    else
                        mywf = new WorkFlow(this.FK_Flow, this.FID);

                    this.ToMsg("结束流程提示:<hr>" + mywf.DoFlowOver(), "info");
                    break;
                case NamesOfBtn.Delete:
                case "Btn_Del":
                    WorkFlow wf = null;
                    if (this.FID == 0)
                        wf = new WorkFlow(this.FK_Flow, this.WorkID);
                    else
                        wf = new WorkFlow(this.FK_Flow, this.FID);

                    this.ToMsg("删除流程提示<hr>" + wf.DoDeleteWorkFlowByReal(), "info");
                    // this.ToMsgPage("操作提示<hr>您已经成功删除了该流程。");
                    break;
                case NamesOfBtn.Save:
                    this.Send(true);
                    if (string.IsNullOrEmpty(this.Request.QueryString["WorkID"]))
                    {
                        this.Response.Redirect(this.PageID + ".aspx?FID=" + this.FID + "&WorkID=" + this.WorkID + "&FK_Node=" + this.FK_Node + "&FK_Flow=" + this.FK_Flow + "&FK_Node_From=" + this.FK_Node_From, true);
                        return;
                    }
                    break;
                case "Btn_ReturnWork":
                    this.BtnReturnWork();
                    break;
                case BP.Web.Controls.NamesOfBtn.Shift:
                    this.DoShift();
                    break;
                case "Btn_WorkerList":
                    if (WorkID == 0)
                        throw new Exception("没有指定当前的工作,不能查看工作者列表.");
                    break;
                case "Btn_PrintWorkRpt":
                    if (WorkID == 0)
                        throw new Exception("没有指定当前的工作,不能打印工作报告.");
                    this.WinOpen("WFRpt.aspx?FK_Flow=" + this.FK_Flow + "&WorkID=" + WorkID, this.ToE("WorkRpt", "工作报告"), 800, 600);
                    //this.BtnEdit();
                    break;
                case NamesOfBtn.Send:
                    this.Send(false);
                   
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            this.FlowMsg.AlertMsg_Warning("信息提示", ex.Message);
        }
    }
    #region 按钮事件
    /// <summary>
    /// 保存工作
    /// </summary>
    /// <param name="isDraft">是不是做为草稿保存</param> 
    private void
        Send(bool isSave)
    {
        System.Web.HttpContext.Current.Session["RunDT"] = DateTime.Now;
        if (this.FK_Node == 0)
            throw new Exception(this.ToE("NotCurrNode", "没有找到当前的节点"));

       // work.OID = this.WorkID;
        // work.RetrieveFromDBSources();
        try
        {
            switch (currND.HisFormType)
            {
                case FormType.SelfForm:
                    break;
                case FormType.FixForm:
                case FormType.FreeForm:
                    currWK = (Work)this.UCEn1.Copy(this.currWK);
                    break;
                case FormType.DisableIt:
                    break;
                default:
                    throw new Exception("@未涉及到的情况。");
            }
        }
        catch (Exception ex)
        {
            this.Btn_Send.Enabled = true;
            throw new Exception("@在保存前执行逻辑检查错误。" + ex.Message);
        }

        try
        {
            currWK.BeforeSave(); //调用业务逻辑检查。
        }
        catch (Exception ex)
        {
            if (BP.SystemConfig.IsDebug)
                currWK.CheckPhysicsTable();

           // this.Btn_Send.Enabled = true;
            throw new Exception("@在保存前执行逻辑检查错误。@技术信息:" + ex.Message);
        }

        currWK.NodeState = NodeState.Init;
        currWK.Rec = WebUser.No;
        currWK.SetValByKey("FK_Dept", WebUser.FK_Dept);
        currWK.SetValByKey("FK_NY", BP.DA.DataType.CurrentYearMonth);
        try
        {
            if (currND.IsStartNode)
                currWK.FID = 0;

            currWK.Update(); /* 如果是保存 */
        }
        catch (Exception ex)
        {
            try
            {
                currWK.CheckPhysicsTable();
            }
            catch (Exception ex1)
            {
                throw new Exception("@保存错误:" + ex.Message + "@检查物理表错误：" + ex1.Message);
            }
            this.Btn_Send.Enabled = true;
            this.Pub1.AlertMsg_Warning("错误", ex.Message + "@有可能此错误被系统自动修复,请您从新保存一次.");
            return;
        }
        string msg = "";
        // 调用工作流程，处理节点信息采集后保存后的工作。
        if (isSave)
        {
            if (string.IsNullOrEmpty(this.Request.QueryString["WorkID"]))
            {
            //    this.Response.Redirect(this.PageID + ".aspx?FID=" + this.FID + "&WorkID=" + this.WorkID + "&FK_Node=" + this.FK_Node + "&FK_Flow=" + this.FK_Flow + "&FK_Node_From=" + this.FK_Node_From, true);
                return;
            }
            currWK.RetrieveFromDBSources();
            this.UCEn1.ResetEnVal(currWK);
            return;
        }
        try
        {
            currWK.BeforeSend(); // 发送前作逻辑检查。
        }
        catch (Exception ex)
        {
            if (BP.SystemConfig.IsDebug)
                currWK.CheckPhysicsTable();
            throw ex;
        }

        WorkNode firstwn = new WorkNode(this.currWK, this.currND);
        try
        {
            msg = firstwn.AfterNodeSave();
            this.Btn_Send.Enabled = false;
            this.ToMsg(msg, "info");
            return;
        }
        catch (Exception ex)
        {
            msg = ex.Message.Replace("'", "’");
            msg = ex.Message.Replace("<br>", "\r\n");
            this.Btn_Send.Enabled = true;
            this.Pub1.AlertMsg_Warning("错误", msg);
            return;
        }


        //bool isCanDoNextWork = true;
        ////能不能执行下一步工作
        //this.WorkID = firstwn.HisWork.OID;
        //this.FK_Node = firstwn.HisNode.NodeID;
        ////    this.BPTabStrip1.SelectedIndex = 0;
        //if (firstwn.IsComplete == false)
        //{
        //    this.Btn_Send.Enabled = false;
        //    /* 如果当前得节点任务还没有完成 */
        //    this.ToMsg(msg, "info");
        //    return;
        //}

        ////判断流程得任务是不是完成？如果流程得任务完成，就不向下执行。
        //if (firstwn.HisWorkFlow.IsComplete)
        //{
        //    /* 如果工作已经完成 */
        //    this.Btn_Send.Enabled = false;
        //    this.Btn_Save.Enabled = false;
        //    msg += "@" + this.ToE("FlowOver", "此流程已经完成");
        //    this.ToMsg(msg, "info");
        //    return;
        //}
        //else
        //{
        //    this.Btn_Send.Enabled = false;
        //    this.ToMsg(msg, "info");
        //    return;
        //}

        /*
        // 取出他得下一个节点信息。判断是不是能够执行他。
        WorkNode secondwn = firstwn.GetNextWorkNode();
        //WorkFlow wf = new WorkFlow(this.CurrentFlow,this.FK_Node);
        if ( secondwn.HisWorkFlow.IsCanDoCurrentWork(WebUser.No)==false)
        {
            msg+="@您没有处理下个工作的权限。";
            this.ResponseWriteBlueMsg(msg);
            return ;
        }
        else
        {		
            this.ResponseWriteBlueMsg(msg+"@下一步工作你有权限处理,点[下一步]或[当前]按钮处理下一步工作.");
            return;
        } 
        */
    }
    public void ToMsg(string msg, string type)
    {
        this.Session["info"] = msg;
        this.Application["info" + WebUser.No] = msg;

        Glo.SessionMsg = msg;
        this.Response.Redirect("MyFlowInfo" + Glo.FromPageType + ".aspx?FK_Flow=" + this.FK_Flow + "&FK_Type=" + type + "&FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID, false);

        //if (this.PageID.Contains("Single") == true)
        //    this.Response.Redirect("MyFlowInfoSmallSingle.aspx?FK_Flow=" + this.FK_Flow + "&FK_Type=" + type + "&FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID, false);
        //else if (this.PageID.Contains("Small"))
        //    this.Response.Redirect("MyFlowInfoSmall.aspx?FK_Flow=" + this.FK_Flow + "&FK_Type=" + type + "&FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID, false);
        //else
        //    this.Response.Redirect("MyFlowInfo.aspx?FK_Flow=" + this.FK_Flow + "&FK_Type=" + type + "&FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID, false);
    }
    public void BtnReturnWork()
    {
        BP.WF.Node nd = new BP.WF.Node(this.FK_Node);
        Work wk = nd.HisWork;
        wk.OID = this.WorkID;
        wk.Retrieve();
        try
        {
            string msg = nd.HisNDEvents.DoEventNode(EventListOfNode.ReturnBefore, wk);
            if (msg != null)
                throw new Exception(msg);

            string small = this.PageID;
            small = small.Replace("MyFlow", "");
            this.Response.Redirect("ReturnWork" + small + ".aspx?FK_Node=" + this.FK_Node + "&FID=" + wk.FID + "&WorkID=" + this.WorkID + "&FK_Flow=" + this.FK_Flow, true);
        }
        catch (Exception ex)
        {
            this.Alert(ex.Message);
        }
        return;
    }
    public void DoShift()
    {
        string url = "Forward" + Glo.FromPageType + ".aspx?FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID + "&FK_Flow=" + this.FK_Flow;
        this.Response.Redirect(url, true);
    }
    #endregion

    #endregion

    #region 处理工作.
    /// <summary>
    /// 新建一个工作
    /// </summary>
   
    #endregion
}
