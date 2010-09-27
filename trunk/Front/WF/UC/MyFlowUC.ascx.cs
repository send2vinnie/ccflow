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
using BP.Port;
using BP.Web.Controls;
using BP.DA;
using BP.En;
using BP.Web;
public partial class WF_UC_MyFlowUC : BP.Web.UC.UCBase3
{

    #region 控件
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
    /// <summary>
    /// 保存
    /// </summary>
    protected Btn Btn_Save
    {
        get
        {
            return this.ToolBar1.GetBtnByID(NamesOfBtn.Save);
        }
    }
    protected Btn Btn_ReturnWork
    {
        get
        {
            return this.ToolBar1.GetBtnByID("Btn_ReturnWork");
        }
    }
    protected Btn Btn_Forward
    {
        get
        {
            return this.ToolBar1.GetBtnByID(BP.Web.Controls.NamesOfBtn.Forward);
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
            if (s == null)
                s = "012";
            return s;
        }
    }
    /// <summary>
    /// 当前的工作流
    /// </summary>
    public WorkFlow CurrentWorkFlow_del
    {
        get
        {
            return null;
        }
    }
    /// <summary>
    /// 当前的工作节点
    /// </summary>
    public WorkNode CurrentWorkNode
    {
        get
        {
            BP.WF.Node nd = new BP.WF.Node(this.FK_Node);
            return new WorkNode(nd.HisWork, nd);
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
            if (ViewState["FK_Node"] == null)
            {
                this.FK_Node = this.CurrentFlow.HisStartNode.NodeID;
            }
            return (int)ViewState["FK_Node"];
        }
        set
        {
            ViewState["FK_Node"] = value;
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
    /// <summary>
    /// 取当前选择的流程
    /// </summary>
    public Flow CurrentFlow
    {
        get
        {
            return new Flow(this.FK_Flow);
        }
    }
    /// <summary>
    /// 当前的开始的Works
    /// </summary>
    public StartWorks CurrentFlowStartWorks
    {
        get
        {
            return (StartWorks)this.CurrentFlow.HisStartNode.HisWorks;
        }
    }
    /// <summary>
    /// 当前的开始的Works
    /// </summary>
    public StartWork CurrentFlowStartWork
    {
        get
        {
            try
            {
                return (StartWork)this.CurrentFlowStartWorks.GetNewEntity;
            }
            catch (Exception ex)
            {
                throw new Exception(this.CurrentNode.EnsName + " 不能想开始工作节点转换。" + ex.Message);
            }
        }
    }
    /// <summary>
    /// 当前的工作
    /// </summary>
    public Work CurrentWork_del
    {
        get
        {
            return (Work)this.CurrentNode.HisWorks.GetNewEntity;
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
        this.ToolBar1.AddLab("s", "待办工作");
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
        this.Pub1.AddTDTitle(this.ToE("Starter", "发起人"));
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

            this.Pub1.AddTD("<a href=\"MyFlow.aspx?FK_Flow=" + dr["FK_Flow"] + "&WorkID=" + dr["WorkID"] + "\" >" + dr["Title"].ToString());
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
        this.ToolBar1.AddLab("s", "在途工作");

        this.Pub1.AddTable("width='80%' align=left");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("nowarp=true", this.ToE("IDX", "序"));
        this.Pub1.AddTDTitle("nowarp=true", this.ToE("Name", "名称"));
        this.Pub1.AddTDTitle("nowarp=true", this.ToE("CurrNode", "当前节点"));
        this.Pub1.AddTDTitle("nowarp=true", this.ToE("StartDate", "发起日期"));
        this.Pub1.AddTDTitle("nowarp=true", this.ToE("StartDate", "发起人"));
        this.Pub1.AddTDTitle("nowarp=true", this.ToE("Oper", "操作"));
        this.Pub1.AddTDTitle("nowarp=true", this.ToE("Rpt", "报告"));
        this.Pub1.AddTREnd();

        string sql = "  SELECT a.WorkID FROM WF_GenerWorkFlow A, WF_GenerWorkerlist B  WHERE A.WorkID=B.WorkID   AND B.FK_Emp='" + BP.Web.WebUser.No + "' AND B.IsEnable=1 AND B.IsCurr=0 AND b.FK_Flow='"+this.FK_Flow+"'";
        //this.Response.Write(sql);

        GenerWorkFlowExts gwfs = new GenerWorkFlowExts();
        gwfs.RetrieveInSQL(GenerWorkFlowAttr.WorkID, "(" + sql + ")");
        int i = 0;
        bool is1 = false;
        foreach (GenerWorkFlowExt gwf in gwfs)
        {
            i++;
            is1 = this.Pub1.AddTR(is1);
            this.Pub1.AddTDIdx(i);
            this.Pub1.AddTDA("MyFlow.aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow, gwf.Title);
            this.Pub1.AddTD(gwf.FK_NodeText);
            this.Pub1.AddTD(gwf.RDT);
            this.Pub1.AddTD(gwf.RecText);
            this.Pub1.AddTD("<a href=\"javascript:Do('" + this.ToE("AYS", "您确认吗？") + "','MyFlowInfo.aspx?DoType=UnSend&WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "');\" ><img src='../images/btn/delete.gif' border=0 />" + this.ToE("UnDo", "撤消") + "</a>");
            this.Pub1.AddTD("<a href=\"javascript:WinOpen('./../WF/WFRpt.aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "&FID=0')\" ><img src='../images/btn/rpt.gif' border=0 />" + this.ToE("WorkRpt", "报告") + "</a>");
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEnd();
    }
    public void ShowHistory()
    {
        BP.WF.Node nd = new BP.WF.Node(this.FK_Node);
        this.ToolBar1.AddLab("s", "历史工作:" + nd.Name);
        Works wks = nd.HisWorks;
        QueryObject qo = new QueryObject(wks);
        qo.AddWhere(WorkAttr.Rec, WebUser.No);
        qo.addAnd();
        qo.AddWhere(WorkAttr.NodeState, 1);
        qo.DoQuery();
        this.Pub1.BindWorkDtl(nd, wks);

        //this.UCEn1.AddTable();
        //this.UCEn1.AddTR();
        //this.UCEn1.AddTREnd();
        //this.UCEn1.AddTableEnd();
    }
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


        BP.WF.Node currND;
        BP.WF.Work currWK;


        if (this.IsPostBack == false)
        {
            if (this.WorkID != 0 && WorkerLists.CheckUserPower(this.WorkID, WebUser.No) == false)
            {
                this.ToolBar1.Clear();
                this.ToolBar1.Add("&nbsp;");

                this.FlowMsg.Clear();
                this.FlowMsg.DivInfoBlockBegin(); //("<b>提示</b><hr>@当前的工作已经被处理，或者您没有执行此工作的权限。<br>@您可以执行如下操作。<ul><li><a href='Start.aspx'>发起新流程。</a></li><li><a href='Runing.aspx'>返回在途工作列表。</a></li></ul>");
                this.FlowMsg.AddB("提示");
                this.FlowMsg.AddHR();
                this.FlowMsg.Add("@当前的工作已经被处理，或者您没有执行此工作的权限。<br>@您可以执行如下操作。");

                this.FlowMsg.AddUL();
                this.FlowMsg.AddLi("<a href='Start.aspx'>发起新流程。</a>");
                this.FlowMsg.AddLi("<a href='Runing.aspx'>返回在途工作列表。</a>");
                this.FlowMsg.AddULEnd();

                // this.UCEn1.Add("@当前的工作已经被处理，或者您没有执行此工作的权限。<br>@您可以执行如下操作。<ul><li><a href='Start.aspx'>发起新流程。</a></li><li><a href='Runing.aspx'>返回在途工作列表。</a></li></ul>");
                this.FlowMsg.DivInfoBlockEnd();
                return;
            }
        }


        try
        {
            #region 增加按钮
            this.ToolBar1.AddBtn(NamesOfBtn.Send, "发送(G)");
            this.ToolBar1.AddBtn(NamesOfBtn.Save, "保存(S)");
            this.ToolBar1.AddSpt("ss");
            //this.ToolBar1.AddBtn(NamesOfBtn.Save, "保存(S)");
            //this.ToolBar1.AddBtn("Btn_ReturnWork", this.ToE("Return", "退回(R)"));
            this.ToolBar1.AddBtn("Btn_ReturnWork", "退回(R)");
            this.ToolBar1.AddBtn(BP.Web.Controls.NamesOfBtn.Forward, "转发(F)");

            if (this.WorkID > 0)
                this.ToolBar1.Add("<input type=button value='抄送' enable=true onclick=\"WinOpen('./Msg/Write.aspx?WorkID=" + this.WorkID + "&FK_Node=1601','ds'); \" class=Btn/>");
            else
                this.ToolBar1.Add("<input type=button value='抄送' enable=false onclick=\"WinOpen('./Msg/Write.aspx?WorkID=" + this.WorkID + "&FK_Node=1601','ds'); \" class=Btn/>");

            this.ToolBar1.AddSpt("ss");
            this.ToolBar1.AddBtn(NamesOfBtn.Previous, "上一条(P)");
            this.ToolBar1.AddBtn(NamesOfBtn.Next, "下一条(N)");

            if (this.WorkID == 0)
            {
                this.ToolBar1.GetBtnByID(NamesOfBtn.Previous).Enabled = false;
                this.ToolBar1.GetBtnByID(NamesOfBtn.Next).Enabled = false;
            }
            else
            {
                NextPreviouRec rec = new NextPreviouRec("WF_EmpWorks", "WorkID", this.WorkID, " FK_Emp='" + WebUser.No + "' AND FK_Flow='" + this.FK_Flow + "' ");
                if (rec.NextID == null)
                    this.ToolBar1.GetBtnByID(NamesOfBtn.Next).Enabled = false;
                else
                    this.ToolBar1.GetBtnByID(NamesOfBtn.Next).Attributes["onclick"] = " window.location.href='MyFlow.aspx?WorkID=" + rec.NextID + "&FK_Flow=" + this.FK_Flow + "';return false;";

                if (rec.PreviouID == null)
                    this.ToolBar1.GetBtnByID(NamesOfBtn.Previous).Enabled = false;
                else
                    this.ToolBar1.GetBtnByID(NamesOfBtn.Previous).Attributes["onclick"] = " window.location.href='MyFlow.aspx?WorkID=" + rec.PreviouID + "&FK_Flow=" + this.FK_Flow + "';return false;";
            }

            //  this.ToolBar1.AddBtn("Btn_PrintWorkRpt", "报告");
            //  this.ToolBar1.AddBtn(NamesOfBtn.Option, "选项");
            //  this.ToolBar1.AddBtn("Btn_CurrentWork", "当前");

            this.ToolBar1.AddSpt("Next");
            #endregion

            currND = this.CurrentNode;
            if (this.WorkID == 0)
            {
                currWK = this.New(true, currND);
                this.Btn_ReturnWork.Enabled = false;
                this.Btn_Forward.Enabled = false;
            }
            else
            {
                currWK = this.GenerCurrWork(this.WorkID);
            }

            currND = this.CurrentNode;
            this.BindWork(currND, currWK);


            if (currND.HisFormType == FormType.SelfForm)
                this.Btn_Save.Enabled = false;

            if (this.ToolBar1.IsExit(NamesOfBtn.Send))
            {
                // this.Btn_Send.Attributes["onclick"] = "return SaveDtl();";
                this.Btn_Send.Click += new System.EventHandler(this.ToolBar1_ButtonClick);
            }

            if ( this.ToolBar1.IsExit(NamesOfBtn.Save))
            {
                //   this.Btn_Save.Attributes["onclick"] = "return SaveDtl();";
                // this.Btn_Save.Attributes["onclick"] = "javascript:return SaveDtl();";
                this.Btn_Save.Click += new System.EventHandler(this.ToolBar1_ButtonClick);
            }

            if (this.ToolBar1.IsExit(NamesOfBtn.Forward))
                this.Btn_Forward.Click += new System.EventHandler(this.ToolBar1_ButtonClick);

            if (this.ToolBar1.IsExit("Btn_ReturnWork"))
                this.Btn_ReturnWork.Click += new System.EventHandler(this.ToolBar1_ButtonClick);

            if (currND == null)
                currND = this.CurrentNode;

            if (currND.IsStartNode || currND.IsFLHL)
            {
                /* 是开始节点并且是分流合流节点。*/
                this.Btn_ReturnWork.Enabled = false;
                this.Btn_Forward.Enabled = false;
            }
            this.Session["Ect"] = null;
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
        //  this.UCSys1.Add(this.ToE("Flow", "流程") + "：" + currND.FlowName + " &nbsp;&nbsp;" + this.ToE("Node", "节点") + "：" + currND.Name);
    }
    #endregion

    #region 公共方法
    public string StepStr
    {
        get
        {
            return this.ViewState["StepStr"] as string;
        }
        set
        {
            this.ViewState["StepStr"] = value;
        }
    }
    /// <summary>
    /// BindWork
    /// </summary>
    public void BindWork(BP.WF.Node nd, Work wk)
    {
        wk.Rec = WebUser.No;
        wk.SetValByKey("FK_Dept", WebUser.FK_Dept);
        wk.SetValByKey("FK_DeptText", WebUser.FK_DeptName);
        wk.SetValByKey("FK_NY", BP.DA.DataType.CurrentYearMonth);

        #region 设置默认值。
        MapAttrs mattrs = new MapAttrs("ND" + nd.NodeID);
        foreach (MapAttr attr in mattrs)
        {
            if (attr.DefValReal.Contains("@") == false || attr.Tag == "1")
                continue;

            wk.SetValByKey(attr.KeyOfEn, attr.DefVal);
        }
        #endregion 设置默认值。


        if (wk.NodeState == NodeState.Back)
        {
            ReturnWork rw = new ReturnWork();
            this.FlowMsg.AddMsgInfo("", rw.NoteHtml);
        }

        #region 判断是否合流节点。。
        if (nd.HisNodeWorkType == NodeWorkType.WorkHL || nd.HisNodeWorkType == NodeWorkType.WorkFHL)
        {
             
            WorkerLists wls = new WorkerLists();
            QueryObject qo = new QueryObject(wls);
            //qo.AddWhereInSQL(WorkerListAttr.WorkID, "select workid");
            //qo.addAnd();
            qo.AddWhere(WorkerListAttr.FK_Node, nd.HisFromNodes[0].GetValByKey(NodeAttr.NodeID) );
            qo.DoQuery();


            this.FlowMsg.AddTable();
            this.FlowMsg.AddCaptionLeft("分流信息");

            this.FlowMsg.AddTR();
            this.FlowMsg.AddTDTitle("工作人员账号");
            this.FlowMsg.AddTDTitle("工作人员名称");
            this.FlowMsg.AddTDTitle("部门");
            this.FlowMsg.AddTDTitle("状态");
            this.FlowMsg.AddTDTitle("应完成日期");
            this.FlowMsg.AddTDTitle("实际完成日期");
            this.FlowMsg.AddTDTitle("");
            this.FlowMsg.AddTREnd();

            foreach (WorkerList wl in wls)
            {
                this.FlowMsg.AddTR();
                this.FlowMsg.AddTD(wl.FK_Emp);
                this.FlowMsg.AddTD(wl.FK_EmpText);

                this.FlowMsg.AddTD(wl.FK_DeptT);
                if (wl.IsPass && wl.FID == wk.FID)
                    this.FlowMsg.AddTD("已完成");
                else
                    this.FlowMsg.AddTD("未完成-<a href=\"javascript:WinOpen('');\"><img src='./Img/sms.gif' border=0/>催办</a>");

                this.FlowMsg.AddTD(wl.SDT);
                this.FlowMsg.AddTD(wl.RDT);

                if (wl.IsPass == false)
                    this.FlowMsg.AddTD("<a href=\"javascript:DoUnSend('" + wl.WorkID + "')\">撤销工作</a>");
                else
                    this.FlowMsg.AddTD();

                this.FlowMsg.AddTREnd();
            }
            this.FlowMsg.AddTableEnd();
        }
        #endregion 判断是否合流节点。


        switch (nd.HisFormType)
        {
            case FormType.SysForm:
                this.UCEn1.BindColumn4(wk, "ND" + nd.NodeID); //, false, false, null);
                if (wk.WorkEndInfo.Length > 2)
                {
                    this.UCEn1.Add(wk.WorkEndInfo);
                }
                OutJSAuto(wk);
                return;
            case FormType.SelfForm:
                wk.Save();
                this.UCEn1.AddIframeWithOnload(nd.FormUrl + "?WorkID=" + wk.OID);
                return;
            default:
                throw new Exception("@没有涉及到的扩充。");
                break;
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
            tb.Attributes["OnKeyDown"] = "javascript:return VirtyNum(this);";

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
    public void ShowSheets(BP.WF.Node nd,Work currWk)
    {

        if (nd.HisFJOpen!= FJOpen.None )
        {
            string url = "FileManager.aspx?WorkID=" + this.WorkID + "&FID=" + currWk.FID
                + "&FJOpen="+(int)nd.HisFJOpen+"&FK_Node=" + nd.NodeID;
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

    #region ToolBar1

    private void CheckStartWorkPower()
    {
        if (WorkFlow.IsCanDoWorkCheckByEmpStation(this.CurrentFlow.HisStartNode.NodeID, WebUser.No) == false)
        {
            this.Btn_Send.Enabled = false;
            this.Btn_Save.Enabled = false;
            //this.Btn_New.Enabled=false;				  
        }
        else
        {
            this.Btn_Send.Enabled = true;
            this.Btn_Save.Enabled = true;
            //this.Btn_New.Enabled=true;
        }
    }
    /// <summary>
    /// 查找。
    /// </summary>
    private void Search()
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
                case NamesOfBtn.Save:
                    this.Send(true);
                   // this.Response.Redirect(this.Request.RawUrl, true);
                    //BP.WF.Node nd = new BP.WF.Node(this.FK_Node);
                    //Work work = nd.HisWork;
                    //work.OID = this.WorkID;
                    //if (work.EnMap.Attrs.Contains(BP.WF.GECheckStandAttr.NodeID))
                    //    work.SetValByKey(GECheckStandAttr.NodeID, this.FK_Node);
                    //work.Retrieve();
                    /* 说明有脚本存在，就要判断是否是需要关闭窗口。 */
                    //if (this.IsClose)
                    //{
                    //    this.WinClose();
                    //    return;
                    //}
                    break;
                case "Btn_ReturnWork":
                    this.BtnReturnWork();
                    break;
                case BP.Web.Controls.NamesOfBtn.Forward:
                    this.DoForward();
                    break;
                case "Btn_WorkerList":
                    if (WorkID == 0)
                        throw new Exception("没有指定当前的工作,不能查看工作者列表.");
                    this.BtnWorkerList();
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
                    this.DealNodeRefFunc(btn.ID);
                    break;
            }
        }
        catch (Exception ex)
        {
            this.FlowMsg.AddMsgOfWarning("信息提示", ex.Message);
            //this.ResponseWriteRedMsg(ex.Message);
        }
    }
    /// <summary>
    /// 处理相关功能
    /// </summary>
    public void DealNodeRefFunc(string btnId)
    {
        //int oid= int.Parse(btnId.Substring("Btn_NodeRef".Length) );
        //   BillTemplate en = new BillTemplate(oid);
        //if (en.RefFuncType==1)
        //{
        //	if (this.WorkID==0)
        //			throw new Exception("@没有找到当前的工作ID.系统错误。");
        ////		this.WinOpen("NodeRefFunc.aspx?NodeId="+this.FK_Node.ToString()+"&FlowNo="+this.FK_Flow+"&NodeRefFuncOID="+en.OID.ToString()+"&WorkFlowID="+this.WorkID.ToString(),en.Name,"rpt",en.Width,en.Height,60,60,true,true);
        //	}
        //	else
        //		this.WinOpen(this.Request.ApplicationPath+en.URL+"?WorkFlowID="+this.WorkID.ToString(),en.Name,en.Width,en.Height);
    }

    #region 按钮事件
    /// <summary>
    /// 上一个工作
    /// </summary>
    public void BtnPreviousWork()
    {
        //			this.Btn_NextWork.Enabled=true;		
        //			if (this.DG_Works.SelectedIndex < 0 )
        //				throw new Exception("@没有选择当前的工作.");
        //
        //			WorkNode wn = new WorkNode(this.CurrentWorkEn,this.CurrentNode);
        //			if (wn.HisNode.IsStatrNode)
        //				this.Btn_PreviousWork.Enabled=false;
        //			WorkNode toWn= wn.GetPreviousWorkNode();
        //			if (toWn.HisWork.NodeState!=BP.WF.NodeState.Complete)
        //			{
        //				/* 如果是当前的工作 */
        //				BtnCurrentWork();
        //				return;
        //			}
        //
        //			this.Btn_Send.Enabled=toWn.IsCanOpenCurrentWorkNode(WebUser.No);
        //			this.Btn_Save.Enabled=this.Btn_Send.Enabled;
        //
        //			if (toWn.HisNode.IsStatrNode)
        //				this.Btn_PreviousWork.Enabled=false;
        //			else
        //				this.Btn_PreviousWork.Enabled=true;
        //
        //			
        //			this.FK_Node =toWn.HisNode.OID;	
        //		 
        //			this.UCEn1.Bind(toWn.HisWork,false,true);
        //
        //			this.ResetNodeName(toWn.HisNode);
    }
    //		/// <summary>
    //		/// 转到当前工作
    //		/// </summary>
    //		public void BtnCurrentWork()
    //		{
    //			//this.Btn_NextWork.Enabled=false;
    //			//this.Btn_PreviousWork.Enabled=true;
    //			//if (this.DG_Works.SelectedIndex < 0)
    //			//throw new Exception("@没有选择当前的工作.");
    //			LoadWorkID( this.WorkID  );
    //			this.BPTabStrip1.SelectedIndex=0;
    //		}
    /// <summary>
    /// 下一个工作
    /// </summary>
    public void NextWork()
    {
        //			this.Btn_PreviousWork.Enabled=true;
        //
        //			if (this.DG_Works.SelectedIndex < 0)
        //				throw new Exception("@没有选择当前的工作.");
        //
        //			Work work = this.CurrentWorkEn;
        //			if (work.NodeState!=BP.WF.NodeState.Complete)
        //			{
        //				this.Btn_NextWork.Enabled=false;
        //
        //				//throw new Exception("@工作["+work.EnDesc+"]是正在进行的工作.");
        //				/* 如果当前的工作状态不是完成状态, 就是工作进行状态. */
        //				//BtnCurrentWork();
        //				return;
        //			}
        //			 
        //			WorkNode wn =  new WorkNode(work,this.CurrentNode);
        //			WorkNode toWn= wn.GetNextWorkNode();
        //			//			if (toWn.HisWork.NodeState!=0)
        //			//			{
        //			//				/* 如果是当前的工作 */
        //			//				BtnCurrentWork();
        //			//				return;
        //			//			}
        //
        //			this.Btn_Send.Enabled=toWn.IsCanOpenCurrentWorkNode(WebUser.No) ; 
        //			this.Btn_Save.Enabled=this.Btn_Send.Enabled;
        //			this.FK_Node =toWn.HisNode.OID;
        //			
        //
        //			this.UCEn1.Bind(toWn.HisWork,false,true);
        //			//this.DG_WorkArea.DataBind(toWn.HisWork,true);
        //			this.ResetNodeName(toWn.HisNode);

    }
    /// <summary>
    /// 生成回执单据
    /// </summary>
    private string GenerHZ(Work wk, BP.WF.Node nd)
    {
        return null;

        #region  适合机关
        //if (nd.IsStartNode == false)
        //    return null;

        //Flow fl = new Flow(this.FK_Flow);
        //if (fl.DateLit == 0)
        //    return null;

        //if (wk.EnMap.Attrs.Contains("FK_Taxpayer") == false)
        //    return null;

        //string msg1 = "<img src='../../" + SystemConfig.AppName + "/Images/Btn/Word.gif' /><a href=\"javascript:WinOpen('GenerHuiZhi.aspx?FK_Flow=" + this.FK_Flow + "&WorkID=" + wk.OID + "','Hz') \"  >受理回执</a>";
        //return msg1;


        //BillTemplate func = new BillTemplate("SLHZ"); // 回执
        //string year = DateTime.Now.Year.ToString();
        //string file = year + "_" + WebUser.FK_Dept + "_" + func.No + "_" + wk.OID + ".doc";
        //string msg = "<img src='../../" + SystemConfig.AppName + "/Images/Btn/Word.gif' /><a href=\"javascript:Run('C:\\\\ds2002\\\\OpenBill.EXE','" + file + "','0');\"  >" + func.Name + "</a>";


        //BP.Rpt.RTF.RTFEngine rtf = new BP.Rpt.RTF.RTFEngine();
        //CHOfFlow cf = new CHOfFlow();
        //cf.FK_Flow = this.FK_Flow;
        //cf.SetValByKey("FK_FlowText", fl.Name);
        //cf.FK_Flow = this.FK_Flow;
        //Dept Dept = new Dept(WebUser.FK_Dept);

        //cf.SetValByKey("FK_DeptText", Dept.Name);
        //cf.Copy(wk);

        //cf.DateLitFrom = DateTime.Now.AddDays(fl.DateLit).ToString(DataType.SysDataFormat);
        //cf.DateLitTo = DateTime.Now.AddDays(fl.DateLit + 10).ToString(DataType.SysDataFormat);
        ////cf.Update();
        //rtf.AddEn(cf);

        //string path = BP.SystemConfig.AppSettings["FtpPath"].ToString() + year + "\\" + WebUser.FK_Dept + "\\" + func.No + "\\";
        //rtf.MakeDoc(func.Url + ".rtf",
        //    path, file, false);

        //return msg; // +"<a href='Do.aspx?ActionType=DeleteFlow&WorkID=" + wk.OID + "&FK_Flow=" + this.FK_Flow + "'  /><img src='../../Images/Btn/Delete.gif' border=0/>删除</a>";

        #endregion
    }
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

        BP.WF.Node currNd = this.CurrentNode;
        Work work = currNd.HisWork;
        work.OID = this.WorkID;
        work.RetrieveFromDBSources();
        try
        {
            switch (currNd.HisFormType)
            {
                case FormType.SelfForm:
                    break;
                case FormType.SysForm:
                    // work = this.UCEn1.GetEnData(work) as Work;
                    work = (Work)this.UCEn1.Copy(work);
                    break;
                default:
                    throw new Exception("@未涉及到的情况。");
            }
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
            {
                work.FID = 0;
            }

            // 处理传递过来的参数。
            foreach (string k in this.Request.QueryString.AllKeys)
            {
                work.SetValByKey(k, this.Request.QueryString[k]);
            }

            if (work.OID == 0)
                work.Insert();
            else
                work.Update(); /* 如果是保存 */

            //save data cells.
            if (currNd.HisFormType == FormType.SysForm)
            {
                //MapData md = new MapData("ND" + currNd.NodeID);
                //work.Update();
            }
        }
        catch (Exception ex)
        {
            try
            {
                work.CheckPhysicsTable();
            }
            catch (Exception ex1)
            {
                throw new Exception("@保存错误:" + ex.Message + "@检查物理表错误：" + ex1.Message);
            }
            this.Pub1.AddMsgOfWarning("错误", ex.Message + "@有可能此错误被系统自动修复,请您从新保存一次.");
            //throw new Exception(ex+);
        }
        this.WorkID = work.OID;
        string msg = "";
        // 调用工作流程，处理节点信息采集后保存后的工作。
        if (isSave)
        {
            this.WorkID = work.OID;
            work.RetrieveFromDBSources();
            this.UCEn1.ResetEnVal(work);
            return;
        }

        try
        {
            work.BeforeSend(); // 发送前作逻辑检查。
        }
        catch (Exception ex)
        {
            if (BP.SystemConfig.IsDebug)
                work.CheckPhysicsTable();
            throw ex;
        }


        WorkNode firstwn = new WorkNode(work, currNd);
        try
        {
            msg = firstwn.AfterNodeSave();
            this.ToMsg(msg, "info");
        }
        catch (Exception ex)
        {
            this.ToMsg(ex.Message, "warning");
            return;
        }

        //if (currNd.IsPCNode)
        //{
        //    this.UCEn1.BindColumn4(work, "ND" + currNd.No );
        //    this.UCEn1.Add(work.WorkEndInfo);
        //}

        bool isCanDoNextWork = true;
        //能不能执行下一步工作
        this.WorkID = firstwn.HisWork.OID;
        this.FK_Node = firstwn.HisNode.NodeID;
        //    this.BPTabStrip1.SelectedIndex = 0;
        if (firstwn.IsComplete == false)
        {
            /* 如果当前得节点任务还没有完成 */
            this.ToMsg(msg, "info");
            return;
        }

        //判断流程得任务是不是完成？如果流程得任务完成，就不向下执行。
        if (firstwn.HisWorkFlow.IsComplete)
        {
            /* 如果工作已经完成 */
            this.Btn_Send.Enabled = false;
            this.Btn_Save.Enabled = false;
            msg += "@" + this.ToE("FlowOver", "此流程已经完成(删除它)");
            this.ToMsg(msg, "info");
        }
        else
        {
            this.ToMsg(msg, "info");
        }

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
        this.Response.Redirect("MyFlowInfo.aspx?FK_Flow=" + this.FK_Flow + "&FK_Type=" + type + "&FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID , false);
    }
    /// <summary>
    /// 新建一个工作
    /// </summary>
    private Work New(bool isPostBack, BP.WF.Node nd)
    {
        Flow fl = new Flow(this.FK_Flow);
        this.FK_Node = fl.StartNodeID;
        this.Btn_Send.Enabled = true;
        this.Btn_Save.Enabled = true;

        // this.BPTabStrip1.SelectedIndex = 0;
        this.FK_Node = nd.NodeID;

        StartWork wk = (StartWork)nd.HisWork;
        int num = wk.Retrieve(StartWorkAttr.NodeState, 0, StartWorkAttr.Rec, WebUser.No);
        if (num == 0)
        {
            wk.Rec = WebUser.No;
            wk.SetValByKey(WorkAttr.RDT, BP.DA.DataType.CurrentDataTime);
            wk.SetValByKey(WorkAttr.CDT, BP.DA.DataType.CurrentDataTime);
            wk.WFState = 0;
            wk.NodeState = 0;
            wk.Insert();
        }

        wk.Rec = WebUser.No;
        wk.SetValByKey(WorkAttr.RDT, BP.DA.DataType.CurrentDataTime);
        wk.SetValByKey(WorkAttr.CDT, BP.DA.DataType.CurrentDataTime);
        wk.WFState = 0;
        wk.NodeState = 0;
        wk.FK_Dept = WebUser.FK_Dept;
        wk.SetValByKey("FK_DeptText", WebUser.FK_DeptName);
        Dept Dept = new Dept(WebUser.FK_Dept);
        wk.FID = 0;
        wk.SetValByKey("RecText", WebUser.Name);
        this.WorkID = wk.OID;
        return wk;

        //switch (nd.HisFormType)
        //{
        //    case FormType.SysForm:
        //        this.UCEn1.BindColumn4(wk, "ND" + nd.NodeID);
        //        this.UCEn1.Add(wk.WorkEndInfo);
        //        this.OutJSAuto(wk);
        //        return;
        //    case FormType.SelfForm:
        //        this.Pub1.Clear();
        //        this.UCEn1.Clear();
        //        this.Pub1.AddIframeWithOnload(nd.FormUrl + "?WorkID=" + wk.OID);
        //        break;
        //}
    }
    /// <summary>
    /// show worker list.
    /// </summary>
    private void BtnWorkerList()
    {
        throw new Exception("不处理.");
        //this.WinOpen(this.Request.ApplicationPath+"/Comm/UIEnDtl.aspx?Key=WorkID&WorkID="+this.WorkID.ToString()+"&EnsName=BP.WF.WorkerLists","工作者列表",800,600);
    }
    public void BtnOption()
    {
        if (this.FK_Node == 0 && this.WorkID <= 0)
        {
            this.Alert("@请选择要操作的工作。");
            return;
        }
        this.WinOpenShowModalDialog("Option.aspx?WorkID=" + this.WorkID + "&FK_Flow=" + this.FK_Flow, "选项", "WF" + WorkID, 500, 300, 200, 200);
        //this.WinOpenShowModalDialog("OpWorkFlow.aspx?WorkID="+this.WorkID+"&FK_Flow="+this.FK_Flow,"选项","WF"+WorkID,600,400,150,160) ; 
        //this.WinOpen("Option.aspx?NodeId="+this.FK_Node+"&WorkID="+this.WorkID+"&FlowNo="+this.CurrentFlow.No,"选项","Task",700,500,200,200);
    }

    public void BtnReturnWork()
    {
        this.Response.Redirect("ReturnWork.aspx?FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID+"&FK_Flow="+this.FK_Flow, true);
        return;
    }

    public void DoForward()
    {
        string url = "Forward.aspx?NodeId=" + this.FK_Node + "&WorkID=" + this.WorkID + "&FK_Flow=" + this.CurrentFlow.No;
        this.Response.Redirect(url, true);
    }
    #endregion

    #endregion

    #region DG_Works
    private Work GenerCurrWork(Int64 workid)
    {
        Work wk = null;
        BP.WF.Node node = null;
         
            Flow fl = new Flow(this.FK_Flow);
            GenerWorkFlow gwf = new GenerWorkFlow();
            if (gwf.Retrieve(GenerWorkFlowAttr.WorkID, workid) == 0)
            {
                node = this.CurrentNode;
                wk = node.HisWork;
                wk.OID = workid;
                wk.RetrieveFromDBSources();
            }
            else
            {
                node = new BP.WF.Node(gwf.FK_Node);
                wk = node.HisWork;
                wk.OID = this.WorkID;
                wk.RetrieveFromDBSources();
            }

        // 在工作区域内设置此流程的当前要处理的工作.(就是要走的节点.)
        // 如果流程完成任务,或者没有找到当前的工作节点。
        WorkNode wn = new WorkNode(wk, node);
        this.FK_Node = node.NodeID;
        try
        {
            string msg = "";
            switch (wn.HisWork.NodeState)
            {
                case NodeState.Back:
                    /* 如果工作节点退回了*/
                    ReturnWork rw = new ReturnWork();
                    rw.WorkID = wn.HisWork.OID;
                    rw.NodeId = wn.HisNode.NodeID;
                    try
                    {
                        rw.Retrieve();
                        //如果是第一个节点，那么把删除流程链接载入消息框页面
                        if (wn.HisNode.IsStartNode)
                            msg = rw.NoteHtml + "<HR><a href='../../WF/MyFlowInfo.aspx?DoType=DeleteFlow&WorkID=" + wn.HisWork.OID + "&FK_Flow=" + this.FK_Flow + "' /><img src='../../Images/Btn/Delete.gif' border=0/>" + this.ToE("SetOverFlow", "结束流程") + "</a>&nbsp;&nbsp;" + msg;
                        else
                            msg = rw.NoteHtml + "<HR>" + msg;
                    }
                    catch
                    {
                    }

                    this.FlowMsg.DivInfoBlock("流程退回提示", msg);

                    this.FK_Node = wn.HisNode.NodeID;
                    this.WorkID = wn.HisWork.OID;
                    break;
                default:
                    /* 如果不是退回来的，就判断是否是转发过来的。 */
                    ForwardWork fw = new ForwardWork();
                    int i = fw.Retrieve(ForwardWorkAttr.WorkID, this.WorkID,
                        ForwardWorkAttr.NodeId, node.NodeID);
                    if (i == 1)
                    {
                        if (fw.IsTakeBack == false)
                        {
                            msg += "@" + this.ToE("Transfer", "转发人") + "[" + fw.FK_Emp + "]。@" + this.ToE("Accepter", "接受人") + "：" + fw.Emps + "。@" + this.ToE("FWNote", "转发原因") + "： @" + fw.NoteHtml;
                            this.FlowMsg.DivInfoBlock("转发提示:", msg);
                        }
                    }
                    break;
            }

            //是否启动  EnableReturnBtn 
            this.FK_Node = wn.HisNode.NodeID;
            // 设置当前的人员把记录人。
            wn.HisWork.Rec = WebUser.No;
            wn.HisWork.RecText = WebUser.Name;
            this.Btn_Save.Enabled = true;
            this.Btn_ReturnWork.Enabled = true;
            if (wn.HisNode.IsEndNode)
                this.Btn_Send.Text = "完成(G)";
            return wn.HisWork;
        }
        catch (Exception ex)
        {
            this.FlowMsg.AddMsgOfWarning("提示:", this.ToE("WhenSeleWorkErr", "处理选择工作出现错误") + ex.Message);
            return null;
        }
        return wn.HisWork;
    }
    #endregion
}
