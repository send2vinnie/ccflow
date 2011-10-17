
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BP.WF;
using BP.Port;
using BP.Sys;
using BP.Web.Controls;
using BP.DA;
using BP.En;

namespace BP.Web.WF
{
    /// <summary>
    /// Flow 的摘要说明。
    /// </summary>
    public partial class GovDoc_MyFlow : WebPage
    {
        #region 控件
        protected BP.Web.Controls.ToolbarBtn Btn_Send
        {
            get
            {
                return this.BPToolBar2.GetBtnByKey(NamesOfBtn.Send);
            }
        }
        /// <summary>
        /// 工作报告
        /// </summary>
        protected BP.Web.Controls.ToolbarBtn Btn_PrintWorkRpt_De
        {
            get
            {
                return this.BPToolBar2.GetBtnByKey("Btn_PrintWorkRpt");
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        protected BP.Web.Controls.ToolbarBtn Btn_Save
        {
            get
            {
                return this.BPToolBar2.GetBtnByKey(NamesOfBtn.Save);
            }
        }
        protected BP.Web.Controls.ToolbarBtn Btn_ReturnWork
        {
            get
            {
                return this.BPToolBar2.GetBtnByKey("Btn_ReturnWork");
            }
        }
        protected BP.Web.Controls.ToolbarBtn Btn_Forward
        {
            get
            {
                return this.BPToolBar2.GetBtnByKey(BP.Web.Controls.NamesOfBtn.Forward);
            }
        }
        protected BP.Web.Controls.ToolbarBtn Btn_New_del
        {
            get
            {
                return this.BPToolBar2.GetBtnByKey("Btn_New");
            }
        }
        protected BP.Web.Controls.ToolbarBtn Btn_CurrentWork_del
        {
            get
            {
                return this.BPToolBar2.GetBtnByKey("Btn_CurrentWork");
            }
        }
        protected BP.Web.Controls.ToolbarBtn Btn_PreviousWork_del
        {
            get
            {
                return this.BPToolBar2.GetBtnByKey("Btn_PreviousWork");
            }
        }
        protected BP.Web.Controls.ToolbarBtn Btn_NextWork_del
        {
            get
            {
                return this.BPToolBar2.GetBtnByKey("Btn_NextWork");
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
                return this.Request.QueryString["FK_Flow"];
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
        //		private Work  _CurrentFlowStartWork=null;
        //
        //		private Work _CurrentWork=null;
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
        public ToolbarLab Lab_NodeName
        {
            get
            {
                return this.BPToolBar2.GetLabByKey("Lab_NodeName");
            }
        }
        #endregion

        #region Page load 事件
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
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, System.EventArgs e)
        {
            BP.WF.Node currND;
            try
            {
                if (this.IsPostBack == false)
                {
                    #region 增加按钮

                    this.BPToolBar2.AddBtn(NamesOfBtn.Send);
                    this.BPToolBar2.AddBtn(NamesOfBtn.Save);
                    this.BPToolBar2.AddSpt("ss");
                    this.BPToolBar2.AddBtn("Btn_ReturnWork", this.ToE("Return", "退回"));
                    this.BPToolBar2.AddBtn(BP.Web.Controls.NamesOfBtn.Forward);

                    //  this.BPToolBar2.AddBtn("Btn_PrintWorkRpt", "报告");
                    //  this.BPToolBar2.AddBtn(NamesOfBtn.Option, "选项");
                    //  this.BPToolBar2.AddBtn("Btn_CurrentWork", "当前");

                    this.BPToolBar2.AddSpt("Next");
                    #endregion


                    if (this.WorkID == 0)
                    {
                        currND = this.CurrentNode;
                        this.New(true, currND);
                        this.Btn_ReturnWork.Enabled = false;
                        this.Btn_Forward.Enabled = false;
                    }
                    else
                    {
                        if (WorkerLists.CheckUserPower(this.WorkID, WebUser.No) == false)
                        {
                            this.ToMsgPage("@当前的工作已经被处理，或者您没有执行此工作的权限。");
                            return;
                        }

                        currND = WhenDG_WorksSelectedIndexChanged(this.WorkID);
                    }
                }
                else
                {
                    currND = new BP.WF.Node(this.FK_Node);
                    this.BindWork(currND);
                }

                this.BPToolBar2.ButtonClick += new System.EventHandler(this.BPToolBar2_ButtonClick);

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
                this.Response.Write(ex.Message);
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


            this.UCSys1.Add( this.ToE("Flow", "流程") + "：" + currND.FlowName + " &nbsp;&nbsp;" + this.ToE("Node", "节点") + "：" + currND.Name);
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
        public void BindWork(BP.WF.Node nd)
        {
            Work wk = nd.HisWork;
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


            this.UCEn1.Bind(wk, "ND"+nd.NodeID, false, false);
            
            //OutJSAuto(wk);

            this.UCEn1.Add(wk.WorkEndInfo);

            this.Btn_Send.Enabled = true;


            if (nd.ShowSheets.Length < 3)
                return;

            this.ShowSheets(nd);

        }
        public void OutJSAuto(Entity en)
        {
            return;

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
        public void ShowSheets(BP.WF.Node nd)
        {
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

                int fk_node = int.Parse(str);
                 BP.WF.Node mynd ;
                 try
                 {
                     mynd = new BP.WF.Node(fk_node);
                 }
                 catch
                 {
                     nd.ShowSheets = nd.ShowSheets.Replace("@" + fk_node, "");
                     nd.Update();
                     continue;
                 }

                Work nwk = mynd.HisWork;
                nwk.OID = this.WorkID;
                //if (nd.IsCheckNode)
                //    nwk.SetValByKey(CheckWorkAttr.NodeID, nd.NodeID);

                if (nwk.RetrieveFromDBSources() == 0)
                    continue;

                this.Pub1.AddB("== " + mynd.Name + " ==<hr width=70%>");

              // this.Pub1.ADDWork(nwk, new ReturnWorks(), new ForwardWorks(), nd.NodeID);
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

        #region toolbar1

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
        private void BPToolBar2_ButtonClick(object sender, System.EventArgs e)
        {
            try
            {
                ToolbarBtn btn = (ToolbarBtn)sender;
                switch (btn.ID)
                {
                    case NamesOfBtn.Save:
                        this.Send(true);

                        //BP.WF.Node nd = new BP.WF.Node(this.FK_Node);
                        //Work work = nd.HisWork;
                        //work.OID = this.WorkID;
                        //if (work.EnMap.Attrs.Contains(BP.WF.CheckWorkAttr.NodeID))
                        //    work.SetValByKey(CheckWorkAttr.NodeID, this.FK_Node);
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
                        if (this.IsClose)
                        {
                            WinClose();
                            return;
                        }
                        break;
                    default:
                        this.DealNodeRefFunc(btn.ID);
                        break;
                }
            }
            catch (Exception ex)
            {
                this.ResponseWriteRedMsg(ex);
            }
        }
        /// <summary>
        /// 处理相关功能
        /// </summary>
        public void DealNodeRefFunc(string btnId)
        {
            //int oid= int.Parse(btnId.Substring("Btn_NodeRef".Length) );
            //   BookTemplate en = new BookTemplate(oid);
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
        //			WhenDG_WorksSelectedIndexChanged( this.WorkID  );
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
        /// 生成回执文书
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


            //BookTemplate func = new BookTemplate("SLHZ"); // 回执
            //string year = DateTime.Now.Year.ToString();
            //string file = year + "_" + WebUser.FK_Dept + "_" + func.No + "_" + wk.OID + ".doc";
            //string msg = "<img src='../../" + SystemConfig.AppName + "/Images/Btn/Word.gif' /><a href=\"javascript:Run('C:\\\\ds2002\\\\OpenBook.EXE','" + file + "','0');\"  >" + func.Name + "</a>";


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

                this.UCEn1.Bind(work, "ND" + this.FK_Node , false, false, "FK_Taxpayer");
                string hzStr = this.GenerHZ(work, currNd);

                this.OutJSAuto(work);
                //work.DoCopy(); // 执行数据copy.
                this.UCEn1.Add(work.WorkEndInfo + hzStr);
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
                if (BP.SystemConfig.IsDebug )
                    work.CheckPhysicsTable();
                throw ex;
            }


            WorkNode firstwn = new WorkNode(work, currNd);
            msg = firstwn.AfterNodeSave();
            if (this.IsClose)
            {
                /* 如果需要关闭窗口。*/
                this.ResponseWriteBlueMsg(msg);
                return;
            }

            if (currNd.IsPCNode)
            {
                // work.Retrieve();
                this.UCEn1.Bind(work, "ND" + currNd.NodeID, false, false);
                this.UCEn1.Add(work.WorkEndInfo);
            }

            bool isCanDoNextWork = true;
            //能不能执行下一步工作
            this.WorkID = firstwn.HisWork.OID;
            this.FK_Node = firstwn.HisNode.NodeID;
            //    this.BPTabStrip1.SelectedIndex = 0;
            if (firstwn.IsComplete == false)
            {
                /* 如果当前得节点任务还没有完成 */
                this.ResponseWriteBlueMsg(msg);
                return;
            }

            //判断流程得任务是不是完成？如果流程得任务完成，就不向下执行。
            if (firstwn.HisWorkFlow.IsComplete)
            {
                /* 如果工作已经完成 */
                this.Btn_Send.Enabled = false;
                this.Btn_Save.Enabled = false;
                msg += "@" + this.ToE("FlowOver", "此流程已经完成");
                this.ResponseWriteBlueMsg(msg);
                this.WinClose();
            }
            else
            {
                this.ResponseWriteBlueMsg(msg);
                this.WinClose(); // 发送完毕后
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
        /// <summary>
        /// 新建一个工作
        /// </summary>
        private void New(bool isPostBack, BP.WF.Node nd)
        {
            Flow fl = new Flow(this.FK_Flow);
            this.FK_Node = fl.StartNodeID;
            this.Btn_Send.Enabled = true;
            this.Btn_Save.Enabled = true;

            // this.BPTabStrip1.SelectedIndex = 0;
            this.FK_Node = nd.NodeID;
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
            this.UCEn1.Bind(wk, "ND" + nd.NodeID,false, false, "FK_Taxpayer");
            this.UCEn1.Add(wk.WorkEndInfo);

            // 生成 自动生成 的js.
            this.OutJSAuto(wk);
            //this.Btn_PrintWorkRpt.Enabled = false;
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
            this.Response.Redirect("ReturnWork.aspx?FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID, true);
            return;
        }

        public void DoForward()
        {
            string url = "Forward.aspx?NodeId=" + this.FK_Node + "&WorkID=" + this.WorkID + "&FlowNo=" + this.CurrentFlow.No;
            this.Response.Redirect(url, true);
        }
        #endregion

        #endregion

        #region DG_Works
        private BP.WF.Node WhenDG_WorksSelectedIndexChanged(Int64 workid)
        {
            Flow fl = new Flow(this.FK_Flow);
            GenerWorkFlow gwf = new GenerWorkFlow(workid);
            BP.WF.Node node = new BP.WF.Node(gwf.FK_Node);
            Work wk = node.HisWork;
            wk.OID = this.WorkID;
            //if (node.IsCheckNode)
            //{
            //    wk.SetValByKey(CheckWorkAttr.NodeID, node.NodeID);
            //}
            wk.Retrieve();

            // 在工作区域内设置此流程的当前要处理的工作.(就是要走的节点.)
            // 如果流程完成任务,或者没有找到当前的工作节点。
            WorkNode wn = new WorkNode(wk, node);
            this.FK_Node = node.NodeID;
            try
            {
                string msg = "";
                bool isClose = true;
                if (wn.HisWork.NodeState != NodeState.Back)
                {
                    /* 如果不是退回来的，就判断是否是转发过来的。 */
                    ForwardWork fw = new ForwardWork();
                    int i = fw.Retrieve(ForwardWorkAttr.WorkID, this.WorkID,
                        ForwardWorkAttr.NodeId, node.NodeID);

                    if (i == 1)
                    {
                        if (fw.IsTakeBack == false)
                        {
                            msg += "@" + this.ToE("Transfer", "转发人") + "[" + fw.FK_Emp + "]。@" + this.ToE("Accepter", "接受人") + "：" + fw.Emps + "。@" + this.ToE("FWNote", "转发原因") + "： @" + fw.NoteHtml;
                            isClose = false;
                            this.Alert(msg);
                        }
                    }
                }

                if (wn.HisWork.NodeState == NodeState.Back)
                {
                    /* 如果工作节点退回了*/
                    ReturnWork rw = new ReturnWork();
                    rw.WorkID = wn.HisWork.OID;
                    rw.NodeId = wn.HisNode.NodeID;
                    try
                    {
                        rw.Retrieve();
                        //如果是第一个节点，那么把删除流程链接载入消息框页面
                        if (wn.HisNode.IsStartNode)
                            this.ResponseWriteBlueMsg(rw.NoteHtml + "<HR><a href='../../WF/Do.aspx?ActionType=DeleteFlow&WorkID=" + wn.HisWork.OID + "&FK_Flow=" + this.FK_Flow + "' /><img src='../../Images/Btn/Delete.gif' border=0/>" + this.ToE("SetOverFlow", "结束流程") + "</a>&nbsp;&nbsp;" + msg);
                        else
                            this.ResponseWriteBlueMsg(rw.NoteHtml + "<HR>" + msg);
                    }
                    catch
                    {
                    }

                    if (wn.HisNode.IsStartNode)
                    {
                        if (msg.IndexOf("href=") != -1)
                            msg = "<HR><a href='../../WF/Do.aspx?ActionType=DeleteFlow&WorkID=" + wn.HisWork.OID + "&FK_Flow=" + this.FK_Flow + "' /><img src='../../Images/Btn/Delete.gif' border=0/>" + this.ToE("SetOverFlow", "结束流程") + "</a>&nbsp;&nbsp;" + msg;
                    }

                    this.FK_Node = wn.HisNode.NodeID;
                    this.WorkID = wn.HisWork.OID;
                    this.ResponseWriteBlueMsg(msg);
                }


                //是否启动  EnableReturnBtn 
                this.FK_Node = wn.HisNode.NodeID;
                // 设置当前的人员把记录人。
                wn.HisWork.Rec = WebUser.No;
                wn.HisWork.RecText = WebUser.Name;


                // this.HisWork = wn.HisWork;
                this.UCEn1.Bind(wn.HisWork, "ND" + this.FK_Node,  false, false);
                this.UCEn1.Add(wn.HisWork.WorkEndInfo + this.GenerHZ(wn.HisWork, wn.HisNode));

                this.Btn_Save.Enabled = true;
                if (wn.HisWork.NodeState == NodeState.Back)
                {
                    /* 如果工作节点退回了。 */
                    ReturnWork rw = new ReturnWork();
                    rw.WorkID = wn.HisWork.OID;
                    rw.NodeId = wn.HisNode.NodeID;
                    try
                    {
                        rw.Retrieve();
                        //如果是第一个节点，那么把删除流程链接载入消息框页面
                        if (wn.HisNode.Step == 1)
                        {
                            this.ResponseWriteBlueMsg(rw.NoteHtml + "<HR><a href='../../WF/Do.aspx?ActionType=DeleteFlow&WorkID=" + wn.HisWork.OID + "&FK_Flow=" + this.FK_Flow + "' />" + this.ToE("SetOverFlow", "结束流程") + "</a>" + msg);
                        }
                        else
                        {
                            this.ResponseWriteBlueMsg(rw.NoteHtml + "<HR>" + msg);
                        }
                        //this.ResponseWriteBlueMsg("<b><font color=red >当前工作被退回，退回原因如下：</font></b><br><br>"+rw.NoteHtml);
                    }
                    catch
                    {

                    }
                }
                if (wn.HisNode.Step == 2)
                {
                    //this.Btn_ReturnWork.Enabled=false;
                    //this.BPToolBar2.Items.Remove(this.Btn_ReturnWork);
                }
                //this.Btn_PrintWorkRpt.Enabled = true;
                this.Btn_ReturnWork.Enabled = true;
                //  this.BPTabStrip1.SelectedIndex = 0;

                this.ShowSheets(wn.HisNode);

                return wn.HisNode;
            }
            catch (Exception ex)
            {
                this.ResponseWriteRedMsg(this.ToE("WhenSeleWorkErr", "处理选择工作出现错误") + ex.Message);
                return null;
            }

            this.ShowSheets(wn.HisNode);

            return wn.HisNode;
        }

        #endregion
    }
}
