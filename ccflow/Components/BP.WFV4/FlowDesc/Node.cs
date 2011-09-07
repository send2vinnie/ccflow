using System;
using System.Data;
using BP.DA;
using BP.Sys;
using BP.En;
using System.Collections;
using BP.Port;

namespace BP.WF
{
    /// <summary>
    /// 投递方式
    /// </summary>
    public enum DeliveryWay
    {
        /// <summary>
        /// 按岗位
        /// </summary>
        ByStation,
        /// <summary>
        /// 按部门
        /// </summary>
        ByDept,
        /// <summary>
        /// 按指定的人员
        /// </summary>
        BySpcEmp,
        /// <summary>
        /// 按SQL
        /// </summary>
        BySQL,
        /// <summary>
        /// 由发送人选择
        /// </summary>
        BySelected,
        /// <summary>
        /// 按人员
        /// </summary>
        ByEmp
    }
    /// <summary>
    /// 节点工作退回规则
    /// </summary>
    public enum ReturnRole
    {
        /// <summary>
        /// 不能退回
        /// </summary>
        CanNotReturn,
        /// <summary>
        /// 只能退回上一个节点
        /// </summary>
        ReturnPreviousNode,
        /// <summary>
        /// 可退回以前任意节点(默认)
        /// </summary>
        ReturnPreviousAnyNodes,
        /// <summary>
        /// 可退回指定的节点
        /// </summary>
        ReturnSpecifiedNodes
    }
    /// <summary>
    /// 附件开放类型
    /// </summary>
    public enum FJOpen
    {
        /// <summary>
        /// 不开放
        /// </summary>
        None,
        /// <summary>
        /// 对操作员开放
        /// </summary>
        ForEmp,
        /// <summary>
        /// 对工作ID开放
        /// </summary>
        ForWorkID,
        /// <summary>
        /// 对流程ID开放
        /// </summary>
        ForFID
    }
    /// <summary>
    /// 分流规则
    /// </summary>
    public enum FLRole
    {
        /// <summary>
        /// 按照接受人
        /// </summary>
        ByEmp,
        /// <summary>
        /// 按照部门
        /// </summary>
        ByDept,
        /// <summary>
        /// 按照岗位
        /// </summary>
        ByStation
    }
    /// <summary>
    /// 运行模式
    /// </summary>
    public enum RunModel
    {
        /// <summary>
        /// 普通
        /// </summary>
        Ordinary = 0,
        /// <summary>
        /// 合流
        /// </summary>
        HL = 1,
        /// <summary>
        /// 分流
        /// </summary>
        FL = 2,
        /// <summary>
        /// 分合流
        /// </summary>
        FHL
    }
    /// <summary>
    /// 节点签字类型
    /// </summary>
    public enum SignType
    {
        /// <summary>
        /// 单签
        /// </summary>
        OneSign,
        /// <summary>
        /// 会签
        /// </summary>
        Countersign
    }
    /// <summary>
    /// 节点状态
    /// </summary>
    public enum NodeState
    {
        /// <summary>
        /// 初始化
        /// </summary>
        Init,
        /// <summary>
        /// 已经完成
        /// </summary>
        Complete,
        /// <summary>
        /// 扣分状态
        /// </summary>
        CutCent,
        /// <summary>
        /// 强制终止
        /// </summary>
        Stop,
        /// <summary>
        /// 删除
        /// </summary>
        Delete,
        /// <summary>
        /// 退回
        /// </summary>
        Back,
        /// <summary>
        /// 转发
        /// </summary>
        Forward
    }
    /// <summary>
    /// 节点工作类型
    /// 节点工作类型( 0, 审核节点, 1 信息采集节点,  2, 开始节点)
    /// </summary>
    public enum NodeWorkType
    {
        Work = 0,
        /// <summary>
        /// 开始节点
        /// </summary>
        StartWork = 1,
        /// <summary>
        /// 开始节点分流
        /// </summary>
        StartWorkFL = 2,
        /// <summary>
        /// 合流节点
        /// </summary>
        WorkHL = 3,
        /// <summary>
        /// 分流节点
        /// </summary>
        WorkFL = 4,
        /// <summary>
        /// 分合流
        /// </summary>
        WorkFHL = 5
    }
    /// <summary>
    /// 流程节点类型
    /// </summary>
    public enum FNType
    {
        /// <summary>
        /// 平面节点
        /// </summary>
        Plane = 0,
        /// <summary>
        /// 分合流
        /// </summary>
        River = 1,
        /// <summary>
        /// 支流
        /// </summary>
        Branch = 2
    }
    /// <summary>
    /// 
    /// </summary>
    public enum NodePosType
    {
        Start,
        Mid,
        End
    }
    /// <summary>
    /// 节点数据采集类型
    /// </summary>
    public enum FormType
    {
        /// <summary>
        /// system form.
        /// </summary>
        FixForm = 0,
        /// <summary>
        /// system form.
        /// </summary>
        FreeForm = 1,
        /// <summary>
        /// self form.
        /// </summary>
        SelfForm = 2,
        /// <summary>
        /// SDKForm
        /// </summary>
        SDKForm = 3,
        /// <summary>
        /// 禁用(对多表单流程有效)
        /// </summary>
        DisableIt = 9
    }
    /// <summary>
    /// 节点属性
    /// </summary>
    public class NodeAttr
    {
        #region 新属性
        /// <summary>
        /// 是否可以隐性退回
        /// </summary>
        public const string IsCanHidReturn = "IsCanHidReturn";
        /// <summary>
        /// 通过率
        /// </summary>
        public const string PassRate = "PassRate";
        /// <summary>
        /// 是否可以设置流程完成
        /// </summary>
        public const string IsCanOver = "IsCanOver";
        /// <summary>
        /// 是否是保密步骤
        /// </summary>
        public const string IsSecret = "IsSecret";
        public const string IsCCNode = "IsCCNode";
        public const string IsCCFlow = "IsCCFlow";
        public const string HisStas = "HisStas";
        public const string HisToNDs = "HisToNDs";
        public const string HisBillIDs = "HisBillIDs";
        public const string NodePosType = "NodePosType";
        public const string HisDeptStrs = "HisDeptStrs";
        public const string HisEmps = "HisEmps";
        public const string GroupStaNDs = "GroupStaNDs";
        public const string FJOpen = "FJOpen";
        public const string IsCanReturn = "IsCanReturn";
        public const string IsCanCC = "IsCanCC";
        public const string IsHandOver = "IsHandOver";
        public const string IsCanDelFlow = "IsCanDelFlow";

        public const string FormType = "FormType";
        public const string FormUrl = "FormUrl";
        /// <summary>
        /// 是否可以查看工作报告
        /// </summary>
        public const string IsCanRpt = "IsCanRpt";
        /// <summary>
        /// 发送之前的信息提示
        /// </summary>
        public const string BeforeSendAlert = "BeforeSendAlert";
        /// <summary>
        /// 是否可以强制删除线程
        /// </summary>
        public const string IsForceKill = "IsForceKill";

        /// <summary>
        /// 接受人sql
        /// </summary>
        public const string RecipientSQL = "RecipientSQL";

        /// <summary>
        /// 退回规则
        /// </summary>
        public const string ReturnRole = "ReturnRole";
        #endregion

        #region 基本属性
        /// <summary>
        /// OID
        /// </summary>
        public const string NodeID = "NodeID";
        /// <summary>
        /// 节点的流程
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// FK_FlowSort
        /// </summary>
        public const string FK_FlowSort = "FK_FlowSort";
        /// <summary>
        /// FK_FlowSortT
        /// </summary>
        public const string FK_FlowSortT = "FK_FlowSortT";
        /// <summary>
        /// 流程名
        /// </summary>
        public const string FlowName = "FlowName";
        /// <summary>
        /// 是否分配工作
        /// </summary>
        public const string IsTask = "IsTask";
        /// <summary>
        /// 节点工作类型
        /// </summary>
        public const string NodeWorkType = "NodeWorkType";
        /// <summary>
        /// 节点的描述
        /// </summary>
        public const string Name = "Name";
        /// <summary>
        /// x
        /// </summary>
        public const string X = "X";
        /// <summary>
        /// y
        /// </summary>
        public const string Y = "Y";
        /// <summary>
        /// WarningDays(警告天数)
        /// </summary>
        public const string WarningDays_del = "WarningDays";
        /// <summary>
        /// DeductDays(扣分天数)
        /// </summary>
        public const string DeductDays = "DeductDays";
        /// <summary>
        /// 警告天
        /// </summary>
        public const string WarningDays = "WarningDays";
        /// <summary>
        /// 扣分
        /// </summary>
        public const string DeductCent = "DeductCent";
        /// <summary>
        /// 最高扣分
        /// </summary>
        public const string MaxDeductCent = "MaxDeductCent";
        /// <summary>
        /// 辛苦加分
        /// </summary>
        public const string SwinkCent = "SwinkCent";
        /// <summary>
        /// 最大的辛苦加分
        /// </summary>
        public const string MaxSwinkCent = "MaxSwinkCent";
        /// <summary>
        /// 流程步骤
        /// </summary>
        public const string Step = "Step";
        /// <summary>
        /// 工作内容
        /// </summary>
        public const string Doc = "Doc";
        /// <summary>
        ///  物理表名
        /// </summary>
        public const string PTable = "PTable";
        /// <summary>
        /// 签字类型
        /// </summary>
        public const string SignType = "SignType";
        /// <summary>
        /// 
        /// </summary>
        public const string DoWhat = "DoWhat";
        /// <summary>
        /// 显示的表单
        /// </summary>
        public const string ShowSheets = "ShowSheets";
        /// <summary>
        /// 运行模式
        /// </summary>
        public const string RunModel = "RunModel";
        /// <summary>
        /// 分流规则
        /// </summary>
        public const string FLRole = "FLRole";
        /// <summary>
        /// 是否是干流
        /// </summary>
        public const string FNType = "FNType";
        /// <summary>
        /// IsSubFlow
        /// </summary>
        public const string HisSubFlows = "HisSubFlows";
        /// <summary>
        /// 超时处理内容
        /// </summary>
        public const string DoOutTime = "DoOutTime";
        public const string OutTimeDeal = "OutTimeDeal";
        /// <summary>
        /// 属性
        /// </summary>
        public const string FrmAttr = "FrmAttr";
        /// <summary>
        /// 个性化发送信息
        /// </summary>
        public const string MsgSend = "MsgSend";
        /// <summary>
        /// 投递规则
        /// </summary>
        public const string DeliveryWay = "DeliveryWay";
        #endregion
    }
    /// <summary>
    /// 这里存放每个节点的信息.	 
    /// </summary>
    public class Node : Entity, IDTS
    {
        #region 初试化全局的 Nod
        private NDEvents _HisNDEvents = null;
        public NDEvents HisNDEvents
        {
            get
            {
                if (_HisNDEvents == null)
                    _HisNDEvents = new NDEvents(this.NodeID);
                return _HisNDEvents;
            }
        }
        public override string PK
        {
            get
            {
                return "NodeID";
            }
        }
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                if (BP.Web.WebUser.No == "admin")
                    uac.IsUpdate = true;
                return uac;
            }
        }
        /// <summary>
        /// 初试化全局的
        /// </summary>
        /// <returns></returns>
        public NodePosType GetHisNodePosType()
        {
            string nodeid = this.NodeID.ToString();
            if (nodeid.Substring(nodeid.Length - 2) == "01")
                return NodePosType.Start;

            if (this.HisFromNodes.Count == 0)
                return NodePosType.Mid;

            if (this.HisToNodes.Count == 0)
                return NodePosType.End;

            return NodePosType.Mid;
        }
        /// <summary>
        /// 检查流程
        /// </summary>
        /// <param name="fl"></param>
        /// <returns></returns>
        public static string CheckFlow(Flow fl)
        {
            Nodes nds = new Nodes();
            nds.Retrieve(NodeAttr.FK_Flow, fl.No);

            if (nds.Count == 0)
                return "流程[" + fl.No + fl.Name + "]中没有节点数据，您需要注册一下这个流程。";

            // 更新是否是有完成条件的节点。
            DA.DBAccess.RunSQL("UPDATE WF_Node SET IsCCNode=0,IsCCFlow=0  WHERE FK_Flow='" + fl.No + "'");

            //DA.DBAccess.RunSQL("UPDATE WF_Node SET IsCCNode=1 WHERE NodeID IN (SELECT NodeID FROM WF_NodeCompleteCondition)");
            //   DA.DBAccess.RunSQL("UPDATE WF_Node SET IsCCFlow=1 WHERE NodeID IN (SELECT NodeID FROM WF_FlowCompleteCondition) ");

            DA.DBAccess.RunSQL("DELETE FROM WF_Direction WHERE Node=0 OR ToNode=0");
            DA.DBAccess.RunSQL("DELETE FROM WF_Direction WHERE Node  NOT IN (SELECT NODEID FROM WF_NODE )");
            DA.DBAccess.RunSQL("DELETE FROM WF_Direction WHERE ToNode  NOT IN (SELECT NODEID FROM WF_NODE) ");

            //更新流程类别
            // DA.DBAccess.RunSQL("UPDATE WF_Node SET FK_FlowSort=(SELECT FK_FlowSort FROM WF_Flow WHERE WF_Flow.No=WF_Node.FK_Flow) ");
            //   DA.DBAccess.RunSQL("UPDATE WF_Node SET FK_FlowSortT=(SELECT Name FROM WF_FlowSort WHERE No=WF_NODE.FK_FlowSort)");


            // 单据信息，岗位，节点信息。
            foreach (Node nd in nds)
            {
                DA.DBAccess.RunSQL("UPDATE WF_Node SET FK_FlowSort='" + fl.FK_FlowSort + "',FK_FlowSortT='" + fl.FK_FlowSortText + "'");

                BP.Sys.MapData md = new BP.Sys.MapData();
                md.No = "ND" + nd.NodeID;
                if (md.IsExits == false)
                {
                    nd.CreateMap();
                }


                // 工作岗位。
                NodeStations stas = new NodeStations(nd.NodeID);
                string strs = "";
                foreach (NodeStation sta in stas)
                    strs += "@" + sta.FK_Station;

                nd.HisStas = strs;

                // 工作部门。
                NodeDepts ndpts = new NodeDepts(nd.NodeID);
                strs = "";
                foreach (NodeDept ndp in ndpts)
                    strs += "@" + ndp.FK_Dept;

                nd.HisDeptStrs = strs;

                // 可执行人员。
                NodeEmps ndemps = new NodeEmps(nd.NodeID);
                strs = "";
                foreach (NodeEmp ndp in ndemps)
                    strs += "@" + ndp.FK_Emp;
                nd.HisEmps = strs;

                // 子流程。
                NodeFlows ndflows = new NodeFlows(nd.NodeID);
                strs = "";
                foreach (NodeFlow ndp in ndflows)
                    strs += "@" + ndp.FK_Flow;
                nd.HisSubFlows = strs;


                // 节点
                strs = "";
                Directions dirs = new Directions(nd.NodeID);
                foreach (Direction dir in dirs)
                    strs += "@" + dir.ToNode;

                nd.HisToNDs = strs;

                // 单据
                strs = "";
                BillTemplates temps = new BillTemplates(nd);
                foreach (BillTemplate temp in temps)
                    strs += "@" + temp.No;
                nd.HisBillIDs = strs;

                // 检查节点的位置属性。
                nd.HisNodePosType = nd.GetHisNodePosType();
                nd.DirectUpdate();
            }

            // 处理抄送人员对象。
            string sql = "select FK_Station from WF_FlowStation WHERE fk_flow='" + fl.No + "'";
            DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            string mystas = "";
            foreach (DataRow dr in dt.Rows)
            {
                mystas += dr[0].ToString() + ",";
            }
            fl.CCStas = mystas;


            // 处理岗位分组.
            sql = "SELECT HisStas, COUNT(*) as NUM FROM WF_Node WHERE FK_Flow='" + fl.No + "' GROUP BY HisStas";
            dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                string stas = dr[0].ToString();
                string nodes = "";
                foreach (Node nd in nds)
                {
                    if (nd.HisStas == stas)
                        nodes += "@" + nd.NodeID;
                }

                foreach (Node nd in nds)
                {
                    if (nodes.Contains("@" + nd.NodeID.ToString()) == false)
                        continue;

                    nd.GroupStaNDs = nodes;
                    nd.DirectUpdate();
                }
            }


            /* 判断流程的类型 */

            sql = "SELECT Name FROM WF_Node WHERE (NodeWorkType=" + (int)NodeWorkType.StartWorkFL + " OR NodeWorkType=" + (int)NodeWorkType.WorkFHL + " OR NodeWorkType=" + (int)NodeWorkType.WorkFL + " OR NodeWorkType=" + (int)NodeWorkType.WorkHL + ") AND (FK_Flow='" + fl.No + "')";
            dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
                fl.HisFlowType = FlowType.Panel;
            else
                fl.HisFlowType = FlowType.FHL;

            //fl.EnsName = fl.HisStartNode.EnsName;
            //fl.EnName = fl.HisStartNode.EnName;
            //fl.StartNodeID = fl.HisStartNode.NodeID;
            fl.DirectUpdate();
            return null;
        }
        protected override bool beforeUpdate()
        {
            if (this.IsStartNode)
            {
                this.SetValByKey(BtnAttr.ReturnRole, (int)ReturnRole.CanNotReturn);
                this.SetValByKey(BtnAttr.ShiftEnable, 0);
                this.SetValByKey(BtnAttr.CCEnable, 0);
                this.SetValByKey(BtnAttr.EndFlowEnable, 0);
                this.SetValByKey(BtnAttr.RptEnable, 0);
                this.SetValByKey(BtnAttr.EndFlowEnable, 0);
                this.SetValByKey(BtnAttr.EndFlowEnable, 0);
            }

            #region 更新流程判断条件的标记。
            DBAccess.RunSQL("UPDATE WF_Node SET IsCCNode=0,IsCCFlow=0  WHERE FK_Flow='" + this.FK_Flow + "'");
            DBAccess.RunSQL("UPDATE WF_Node SET IsCCNode=1 WHERE NodeID IN (SELECT NodeID FROM WF_Cond WHERE CondType=0) AND FK_Flow='" + this.FK_Flow + "'");
            DBAccess.RunSQL("UPDATE WF_Node SET IsCCFlow=1 WHERE NodeID IN (SELECT NodeID FROM WF_Cond WHERE CondType=1) AND FK_Flow='" + this.FK_Flow + "'");
            #endregion

            Flow fl = new Flow();
            fl.No = this.FK_Flow;
            fl.RetrieveFromDBSources();

            Node.CheckFlow(fl);
            this.FlowName = fl.Name;

            DBAccess.RunSQL("UPDATE Sys_MapData SET Name='" + this.Name + "' WHERE No='ND" + this.NodeID + "'");

            switch (this.HisRunModel)
            {
                case RunModel.Ordinary:
                    if (this.IsStartNode)
                        this.HisNodeWorkType = NodeWorkType.StartWork;
                    else
                        this.HisNodeWorkType = NodeWorkType.Work;
                    break;
                case RunModel.FL:
                    if (this.IsStartNode)
                        this.HisNodeWorkType = NodeWorkType.StartWorkFL;
                    else
                        this.HisNodeWorkType = NodeWorkType.WorkFL;
                    break;
                case RunModel.HL:
                    if (this.IsStartNode)
                        throw new Exception("@您不能设置开始节点为合流节点。");
                    else
                        this.HisNodeWorkType = NodeWorkType.WorkHL;
                    break;
                case RunModel.FHL:
                    if (this.IsStartNode)
                        throw new Exception("@您不能设置开始节点为分合流节点。");
                    else
                        this.HisNodeWorkType = NodeWorkType.WorkFHL;
                    break;
                default:
                    throw new Exception("eeeee");
                    break;
            }


            #region  判断流程节点类型。
            while (true)
            {
                if (this.IsFLHL)
                {
                    /*如果是分合流，那么它一定是干流。*/
                    this.HisFNType = FNType.River;
                    break;
                }

                if (fl.HisFlowType == FlowType.Panel)
                {
                    /*说明它没有分流程合流之说，只有平面节点。*/
                    this.HisFNType = FNType.Plane;
                    break;
                }


                if (this.HisNodeWorkType == NodeWorkType.StartWork)
                {
                    /* 如果是开始节点，判断其它节点中是否有分流合流节点，如果有说明它是一个 */
                    this.HisFNType = FNType.Branch;
                    break;
                }


                // 查看能够转向到他的节点。
                Nodes fNDs = this.HisFromNodes;
                if (fNDs.Count == 0)
                {
                    /*说明它是一个单个节点，就不处理它的情况。*/
                    break;
                }

                bool isOk = false;
                foreach (Node nd in fNDs)
                {
                    if (nd.IsFL)
                    {
                        /*它的上一个节点是分流*/
                        this.HisFNType = FNType.Branch;
                        isOk = true;
                        break;
                    }

                    if (nd.HisNodeWorkType == NodeWorkType.WorkHL)
                    {
                        /*它的上一个节点是合流*/
                        this.HisFNType = FNType.River;
                        isOk = true;
                        break;
                    }

                    if (nd.HisFNType == FNType.Plane)
                        break;

                    this.HisFNType = nd.HisFNType;
                    isOk = false;
                    break;
                }

                ////如果判断了，就放出它来。
                //if (isOk)
                //    break;
                //else
                //    throw new Exception("@没有判断到它是分流还是合流节点。");
                break;
            }
            #endregion  判断流程节点类型。


            return base.beforeUpdate();
        }
        /// <summary>
        /// 注册流程
        /// </summary>
        /// <param name="fl"></param>
        /// <returns></returns>
        public static string RegFlow(Flow fl)
        {

            #region 取出来流程节点
            string msg = "";
            // 取出来流程节点.
            Nodes alNodes = new Nodes();
            ArrayList alWorks = BP.DA.ClassFactory.GetObjects("BP.WF.Works");
            #endregion


            #region 更新流程判断条件的标记。
            DA.DBAccess.RunSQL("UPDATE WF_Node SET IsCCNode=0,IsCCFlow=0  WHERE FK_Flow='"+fl.No+"'");
            DA.DBAccess.RunSQL("UPDATE WF_Node SET IsCCNode=1 WHERE NodeID IN (SELECT NodeID FROM WF_Cond WHERE CondType=0) AND FK_Flow='"+fl.No+"'");
            DA.DBAccess.RunSQL("UPDATE WF_Node SET IsCCFlow=1 WHERE NodeID IN (SELECT NodeID FROM WF_Cond WHERE CondType=1) AND FK_Flow='" + fl.No + "'");
            #endregion


            #region 删除方向节点
           // DBAccess.RunSQL("DELETE FROM WF_FLOWCOMPLETECONDITION WHERE nodeid not in ( select nodeid from wf_node)");
            //   DBAccess.RunSQL("DELETE FROM WF_NODECOMPLETECONDITION WHERE enclassName not in (select EnsName from wf_node)");
            //  DBAccess.RunSQL("DELETE FROM WF_FLOWCOMPLETECONDITION WHERE enclassName not in (select EnsName from wf_node)");
            DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist WHERE FK_Node not in( select NodeId from wf_node)");


            //更新标准审核节点和数量审核节点的表名到WF_Node里去
            // DBAccess.RunSQL("update wf_node set PTable='WF_GECheckStand' WHERE ensname='BP.WF.GECheckStands'");
            // DBAccess.RunSQL("update wf_node set PTable='WF_NumCheck' WHERE ensname='BP.WF.NumChecks'");
            #endregion


            #region 调度节点的基本信息
            // 检查开始节点的完整性
            //string sql = "SELECT COUNT(*) FROM (SELECT NODEID, FK_FLOW, COUNT(FK_FLOW) AS NUM  FROM WF_NODE  WHERE WF_NODE.IsStartNode=1 GROUP BY NODEID ,FK_FLOW) WHERE NUM >1";
            //if (DBAccess.RunSQLReturnValInt(sql) > 0)
            //    msg += "流程的开始节点不唯一。" + sql;

            //sql = "UPDATE WF_FLOW SET (StartNODEID ,EnsName,EnName )=( SELECT NODEID, EnsName,EnName  FROM WF_NODE WHERE WF_NODE.IsStartNode=1 AND WF_NODE.FK_FLOW=WF_FLOW.NO )";
            //DBAccess.RunSQL(sql);
            #endregion

            return msg;
        }
        #endregion

        #region 条件
        private Conds _HisNodeCompleteConditions = null;
        /// <summary>
        /// 节点完成任务的条件
        /// 条件与条件之间是or 的关系, 就是说,如果任何一个条件满足,这个工作人员在这个节点上的任务就完成了.
        /// </summary>
        public Conds HisNodeCompleteConditions
        {
            get
            {
                if (this._HisNodeCompleteConditions == null)
                {
                    _HisNodeCompleteConditions = new Conds(CondType.Node, this.NodeID, this.HisWork.OID);
                }
                return _HisNodeCompleteConditions;
            }
        }
        private Conds _HisFlowCompleteConditions = null;
        /// <summary>
        /// 他的完成任务的条件,此节点是完成任务的条件集合
        /// 条件与条件之间是or 的关系, 就是说,如果任何一个条件满足,这个任务就完成了.
        /// </summary>
        public Conds HisFlowCompleteConditions
        {
            get
            {
                if (this._HisFlowCompleteConditions == null)
                    _HisFlowCompleteConditions = new Conds(CondType.Flow, this.NodeID, this.HisWork.OID);
                return _HisFlowCompleteConditions;
            }
        }
        #endregion

        #region 基本属性
        /// <summary>
        /// 内部编号
        /// </summary>
        public string No
        {
            get
            {
                return this.NodeID.ToString().Substring(this.NodeID.ToString().Length - 2);
            }
        }
        /// <summary>
        /// 流程节点类型
        /// </summary>
        public FNType HisFNType
        {
            get
            {
                return (FNType)this.GetValIntByKey(NodeAttr.FNType);
            }
            set
            {
                this.SetValByKey(NodeAttr.FNType, (int)value);
            }
        }

        public FormType HisFormType
        {
            get
            {
                return (FormType)this.GetValIntByKey(NodeAttr.FormType);
            }
            set
            {
                this.SetValByKey(NodeAttr.FormType, (int)value);
            }
        }

        /// <summary>
        /// OID
        /// </summary>
        public int NodeID
        {
            get
            {
                return this.GetValIntByKey(NodeAttr.NodeID);
            }
            set
            {
                this.SetValByKey(NodeAttr.NodeID, value);
            }
        }
        /// <summary>
        /// FormUrl 
        /// </summary>
        public string FormUrl
        {
            get
            {
                return this.GetValStrByKey(NodeAttr.FormUrl);
            }
            set
            {
                this.SetValByKey(NodeAttr.FormUrl, value);
            }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStrByKey(EntityOIDNameAttr.Name);
            }
            set
            {
                this.SetValByKey(EntityOIDNameAttr.Name, value);
            }
        }
        /// <summary>
        /// 需要天数（限期）
        /// </summary>
        public int DeductDays
        {
            get
            {
                int i = this.GetValIntByKey(NodeAttr.DeductDays);
                if (i == 0)
                    return 1;
                return i;
            }
            set
            {
                this.SetValByKey(NodeAttr.DeductDays, value);
            }
        }
        /// <summary>
        /// 最高扣分
        /// </summary>
        public float MaxDeductCent
        {
            get
            {
                return this.GetValFloatByKey(NodeAttr.MaxDeductCent);
            }
            set
            {
                this.SetValByKey(NodeAttr.MaxDeductCent, value);
            }
        }
        /// <summary>
        /// 最高辛苦加分
        /// </summary>
        public float SwinkCent
        {
            get
            {
                return this.GetValFloatByKey(NodeAttr.SwinkCent);
            }
            set
            {
                this.SetValByKey(NodeAttr.SwinkCent, value);
            }
        }
        /// <summary>
        /// 流程步骤
        /// </summary>
        public int Step
        {
            get
            {
                return this.GetValIntByKey(NodeAttr.Step);
            }
            set
            {
                this.SetValByKey(NodeAttr.Step, value);
            }
        }

        /// <summary>
        /// 最终期限( 需要天数（限期）+警告天数)
        /// </summary>
        public int NeedCompleteDays
        {
            get
            {
                return this.DeductDays;
            }
        }
        /// <summary>
        /// 扣分率（分/天）
        /// </summary>
        public float DeductCent
        {
            get
            {
                return this.GetValFloatByKey(NodeAttr.DeductCent);
            }
            set
            {
                this.SetValByKey(NodeAttr.DeductCent, value);
            }
        }
        /// <summary>
        /// 是否是开始节点
        /// </summary>
        public bool IsStartNode
        {
            get
            {
                if (this.No == "01")
                    return true;

                return false;

                //if (this.HisNodePosType == NodePosType.Start)
                //    return true;
                //else
                //    return false;
            }
        }
        /// <summary>
        /// x
        /// </summary>
        public int X
        {
            get
            {
                return this.GetValIntByKey(NodeAttr.X);
            }
            set
            {
                this.SetValByKey(NodeAttr.X, value);
            }
        }
        public int WarningDays
        {
            get
            {
                if (this.GetValIntByKey(NodeAttr.WarningDays) == 0)
                    return this.DeductDays;
                else
                    return this.DeductDays - this.GetValIntByKey(NodeAttr.WarningDays);
            }
        }
        /// <summary>
        /// y
        /// </summary>
        public int Y
        {
            get
            {
                return this.GetValIntByKey(NodeAttr.Y);
            }
            set
            {
                this.SetValByKey(NodeAttr.Y, value);
            }
        }
        /// <summary>
        /// 位置
        /// </summary>
        public NodePosType NodePosType
        {
            get
            {
                return (NodePosType)this.GetValIntByKey(NodeAttr.NodePosType);
            }
            set
            {
                this.SetValByKey(NodeAttr.NodePosType, (int)value);
            }
        }
        /// <summary>
        /// 运行模式
        /// </summary>
        public RunModel HisRunModel
        {
            get
            {
                return (RunModel)this.GetValIntByKey(NodeAttr.RunModel);
            }
        }
        /// <summary>
        /// 节点的事务编号
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStrByKey(NodeAttr.FK_Flow);
            }
            set
            {
                SetValByKey(NodeAttr.FK_Flow, value);
            }
        }
        /// <summary>
        /// 获取它的上一步的分流点
        /// </summary>
        private Node _GetHisPriFLNode(Nodes nds)
        {
            foreach (Node mynd in nds)
            {
                if (mynd.IsHL)
                    return mynd;
                else
                    return _GetHisPriFLNode(mynd.HisFromNodes);
            }
            return null;
        }
        /// <summary>
        /// 它的上一步分流节点
        /// </summary>
        public Node HisPriFLNode
        {
            get
            {
                return _GetHisPriFLNode(this.HisFromNodes);
            }
        }
        public string DoWhat
        {
            get
            {
                return this.GetValStrByKey(NodeAttr.DoWhat);
            }
            set
            {
                SetValByKey(NodeAttr.DoWhat, value);
            }
        }
        public string MsgSend
        {
            get
            {
                return this.GetValStrByKey(NodeAttr.MsgSend);
            }
            set
            {
                SetValByKey(NodeAttr.MsgSend, value);
            }
        }
        public string FlowName
        {
            get
            {
                return this.GetValStrByKey(NodeAttr.FlowName);
            }
            set
            {
                SetValByKey(NodeAttr.FlowName, value);
            }
        }
        //public string EnsName
        //{
        //    get
        //    {
        //        return this.GetValStrByKey(NodeAttr.EnsName);
        //    }
        //    set
        //    {
        //        SetValByKey(NodeAttr.EnsName, value);
        //    }
        //}
        //public string EnName
        //{
        //    get
        //    {
        //        string ms = this.GetValStrByKey(NodeAttr.EnName);
        //        if (ms == null || ms.Trim() == "")
        //        {
        //            ms = this.HisWorks.GetNewEntity.ToString();
        //            this.SetValByKey(NodeAttr.EnName, ms);
        //            this.Update(NodeAttr.EnName, ms);
        //        }
        //        return ms;
        //    }
        //    set
        //    {
        //        SetValByKey(NodeAttr.EnName, value);
        //    }
        //}
        public string PTable
        {
            get
            {
                
                return "ND" + this.NodeID;
            }
            set
            {
                SetValByKey(NodeAttr.PTable, value);
            }
        }
        /// <summary>
        /// 要显示在后面的表单
        /// </summary>
        public string ShowSheets
        {
            get
            {
                string s = this.GetValStrByKey(NodeAttr.ShowSheets);
                if (s == "")
                    return "@";
                return s;
            }
            set
            {
                SetValByKey(NodeAttr.ShowSheets, value);
            }
        }
        /// <summary>
        /// Doc
        /// </summary> 
        public string Doc
        {
            get
            {
                return this.GetValStrByKey(NodeAttr.Doc);
            }
            set
            {
                SetValByKey(NodeAttr.Doc, value);
            }
        }
        public string GroupStaNDs
        {
            get
            {
                return this.GetValStrByKey(NodeAttr.GroupStaNDs);
            }
            set
            {
                this.SetValByKey(NodeAttr.GroupStaNDs, value);
            }
        }

        public string HisToNDs
        {
            get
            {
                return this.GetValStrByKey(NodeAttr.HisToNDs);
            }
            set
            {
                this.SetValByKey(NodeAttr.HisToNDs, value);
            }
        }
        /// <summary>
        /// 签字类型
        /// </summary>
        public SignType HisSignType
        {
            get
            {
                return (SignType)this.GetValIntByKey(NodeAttr.SignType);
            }
            set
            {
                this.SetValByKey(NodeAttr.SignType, (int)value);
            }
        }
        public string HisDeptStrs
        {
            get
            {
                return this.GetValStrByKey(NodeAttr.HisDeptStrs);
            }
            set
            {
                this.SetValByKey(NodeAttr.HisDeptStrs, value);
            }
        }
        public string HisStas
        {
            get
            {
                return this.GetValStrByKey(NodeAttr.HisStas);
            }
            set
            {
                this.SetValByKey(NodeAttr.HisStas, value);
            }
        }
        public string HisEmps
        {
            get
            {
                return this.GetValStrByKey(NodeAttr.HisEmps);
            }
            set
            {
                this.SetValByKey(NodeAttr.HisEmps, value);
            }
        }
        public string HisBillIDs
        {
            get
            {
                string str = this.GetValStrByKey(NodeAttr.HisBillIDs);
                if (this.IsStartNode)
                    if (str.Contains("@SLHZ") == false)
                        str += "@SLHZ";
                return str;
            }
            set
            {
                this.SetValByKey(NodeAttr.HisBillIDs, value);
            }
        }
        #endregion

        #region 扩展属性
        private BillTemplates _BillTemplates = null;
        /// <summary>
        /// HisNodeRefFuncs
        /// </summary>
        public BillTemplates HisBillTemplates
        {
            get
            {
                if (_BillTemplates == null)
                {
                    _BillTemplates = new BillTemplates();
                    _BillTemplates.Retrieve(BillTemplateAttr.NodeID, this.NodeID);
                    //  _BillTemplates.AddEntities(this.HisBillIDs);
                }
                return _BillTemplates;
            }
        }
        private Flow _Flow = null;
        /// <summary>
        /// 此节点所在的事务.
        /// </summary>
        public Flow HisFlow
        {
            get
            {
                if (this._Flow == null)
                    _Flow = new Flow(this.FK_Flow);
                return _Flow;
            }
        }
        /// <summary>
        /// 是不是多岗位工作节点.
        /// </summary>
        public bool IsMultiStations
        {
            get
            {
                if (this.HisStations.Count > 1)
                    return true;
                return false;
            }
        }
        private Stations _HisStations = null;
        /// <summary>
        /// 此节点所在的工作岗位
        /// </summary>
        public Stations HisStations
        {
            get
            {
                if (this._HisStations == null)
                {
                    _HisStations = new Stations();
                    _HisStations.AddEntities(this.HisStas);
                }
                return _HisStations;
            }
        }
        /// <summary>
        /// 他的工作描述
        /// </summary>
        public string HisWorksDesc
        {
            get
            {
                return this.Name;
            }
        }
        private Frms _HisFrms = null;
        /// <summary>
        /// HisFrms
        /// </summary>
        public Frms HisFrms
        {
            get
            {
                if (this._HisFrms == null)
                {
                    _HisFrms = new Frms();
                    foreach (FrmNode fn in this.HisFrmNodes)
                    {
                        _HisFrms.AddEntity(fn.HisFrm);
                    }
                }
                return _HisFrms;
            }
        }

        private FrmNodes _HisFrmNodes = null;
        /// <summary>
        /// HisFrms
        /// </summary>
        public FrmNodes HisFrmNodes
        {
            get
            {
                if (this._HisFrmNodes == null)
                {
                    _HisFrmNodes = new FrmNodes(this.NodeID);
                }
                return _HisFrmNodes;
            }
        }

        private Depts _HisDepts = null;
        /// <summary>
        /// 此节点所在的工作岗位
        /// </summary>
        public Depts HisDepts
        {
            get
            {
                if (this._HisDepts == null)
                {
                    _HisDepts = new Depts();
                    _HisDepts.AddEntities(this.HisDeptStrs);
                }
                return _HisDepts;
            }
        }
        /// <summary>
        /// HisStationsStr
        /// </summary>
        public string HisStationsStr
        {
            get
            {
                string str = "";
                foreach (Station st in this.HisStations)
                {
                    str += st.Name + "，";
                }
                return str;
            }
        }

        #region 转换的工作
        private Work _Work = null;
        /// <summary>
        /// 得到他的一个工作实体
        /// </summary>
        public Work HisWork
        {
            get
            {
                if (_Work == null)
                {
                    if (this.IsStartNode)
                    {
                        _Work = new BP.WF.GEStartWork(this.NodeID);
                        return _Work;
                    }

                    _Work = new BP.WF.GEWork(this.NodeID);
                    return _Work;
                }
                return _Work;
            }
        }
        private Works _HisWorks = null;
        /// <summary>
        /// 他的工作s
        /// </summary>
        public Works HisWorks
        {
            get
            {
                if (_HisWorks == null)
                    _HisWorks = (Works)this.HisWork.GetNewEntities;
                return _HisWorks;
            }
        }
        #endregion

        
        #endregion

        #region 公共方法
        /// <summary>
        /// 得到一个工作data实体
        /// </summary>
        /// <param name="workId">工作ID</param>
        /// <returns>如果没有就返回null</returns>
        public Work GetWork(Int64 workId)
        {
            Work wk = this.HisWork;
            wk.SetValByKey("OID", workId);
            

            if (wk.RetrieveFromDBSources() == 0)
                return null;
            else
                return wk;
            return wk;
        }
        #endregion

        #region 节点的工作类型
        /// <summary>
        /// 投递规则
        /// </summary>
        public DeliveryWay HisDeliveryWay
        {
            get
            {
                return (DeliveryWay)this.GetValIntByKey(NodeAttr.DeliveryWay);
            }
            set
            {
                this.SetValByKey(NodeAttr.DeliveryWay, (int)value);
            }
        }
        /// <summary>
        /// 附件开放类型
        /// </summary>
        public FJOpen HisFJOpen
        {
            get
            {
                return (FJOpen)this.GetValIntByKey(NodeAttr.FJOpen);
            }
            set
            {
                this.SetValByKey(NodeAttr.FJOpen, (int)value);
            }
        }
        /// <summary>
        /// 分流规则
        /// </summary>
        public FLRole HisFLRole
        {
            get
            {
                return (FLRole)this.GetValIntByKey(NodeAttr.FLRole);
            }
            set
            {
                this.SetValByKey(NodeAttr.FLRole, (int)value);
            }
        }
        /// <summary>
        /// Int type
        /// </summary>
        public NodeWorkType HisNodeWorkType
        {
            get
            {
                return (NodeWorkType)this.GetValIntByKey(NodeAttr.NodeWorkType);
            }
            set
            {
                this.SetValByKey(NodeAttr.NodeWorkType, (int)value);
            }
        }
        public string HisNodeWorkTypeT
        {
            get
            {
                return this.HisNodeWorkType.ToString();

                //Sys.SysEnum se = new Sys.SysEnum(NodeAttr.NodeWorkType, this.GetValIntByKey(NodeAttr.NodeWorkType));
                //return se.Lab;
            }
        }
        #endregion

        #region 推算属性 (对于节点位置的判断)
        private NodeEmps _HisNodeEmps = null;
        public NodeEmps HisNodeEmps
        {
            get
            {
                if (_HisNodeEmps == null || _HisNodeEmps.Count == 0)
                {
                    _HisNodeEmps = new NodeEmps(this.NodeID);
                }
                return _HisNodeEmps;
            }
        }
        /// <summary>
        /// 类型
        /// </summary>
        public NodePosType HisNodePosType
        {
            get
            {
                if (SystemConfig.IsDebug)
                {
                    this.SetValByKey(NodeAttr.NodePosType, (int)this.GetHisNodePosType());
                    return this.GetHisNodePosType();
                }

                //if (this.HisNodeWorkType == NodeWorkType.StartWork || this.HisNodeWorkType == NodeWorkType.)
                //    return NodePosType.Start;

                return (NodePosType)this.GetValIntByKey(NodeAttr.NodePosType);
            }
            set
            {
                if (value == NodePosType.Start)
                    if (this.No != "01")
                        value = NodePosType.Mid;

                this.SetValByKey(NodeAttr.NodePosType, (int)value);
            }
        }
        /// <summary>
        /// 是不是结束节点
        /// </summary>
        public bool IsEndNode
        {
            get
            {
                if (this.HisNodePosType == NodePosType.End)
                    return true;
                else
                    return false;
            }
        }
        public bool IsCanDelFlow
        {
            get
            {
                return this.GetValBooleanByKey(NodeAttr.IsCanDelFlow);
            }
        }
        public bool IsForceKill
        {
            get
            {
                return this.GetValBooleanByKey(NodeAttr.IsForceKill);
            }
        }
        /// <summary>
        /// 是否保密步骤
        /// </summary>
        public bool IsSecret
        {
            get
            {
                return this.GetValBooleanByKey(NodeAttr.IsSecret);
            }
            set
            {
                this.SetValByKey(NodeAttr.IsSecret, value);
            }
        }
        public decimal PassRate
        {
            get
            {
                return this.GetValDecimalByKey(NodeAttr.PassRate);
            }
            set
            {
                this.SetValByKey(NodeAttr.PassRate, value);
            }
        }
        public bool IsCanOver
        {
            get
            {
                return this.GetValBooleanByKey(NodeAttr.IsCanOver);
            }
            set
            {
                this.SetValByKey(NodeAttr.IsCanOver, value);
            }
        }
        public bool IsCanRpt
        {
            get
            {
                return this.GetValBooleanByKey(NodeAttr.IsCanRpt);
            }
            set
            {
                this.SetValByKey(NodeAttr.IsCanRpt, value);
            }
        }
        /// <summary>
        /// 是否可以移交
        /// </summary>
        public bool IsHandOver
        {
            get
            {
                if (this.IsStartNode)
                    return false;

                return this.GetValBooleanByKey(NodeAttr.IsHandOver);
            }
            set
            {
                this.SetValByKey(NodeAttr.IsHandOver, value);
            }
        }
        /// <summary>
        /// 是否可以抄送
        /// </summary>
        public bool IsCanCC
        {
            get
            {
                return this.GetValBooleanByKey(NodeAttr.IsCanCC);
            }
            set
            {
                this.SetValByKey(NodeAttr.IsCanCC, value);
            }
        }
        public bool IsCanHidReturn_del
        {
            get
            {
                return this.GetValBooleanByKey(NodeAttr.IsCanHidReturn);
            }
            set
            {
                this.SetValByKey(NodeAttr.IsCanHidReturn, value);
            }
        }
        public bool IsCanReturn
        {
            get
            {
                if (this.HisReturnRole == ReturnRole.CanNotReturn)
                    return false;
                return true;
            }
        }
        /// <summary>
        /// 退回规则
        /// </summary>
        public ReturnRole HisReturnRole
        {
            get
            {
                return (ReturnRole)this.GetValIntByKey(NodeAttr.ReturnRole);
            }
            set
            {
                this.SetValByKey(NodeAttr.ReturnRole, (int)value);
            }
        }
        /// <summary>
        /// 是不是中间节点
        /// </summary>
        public bool IsMiddleNode
        {
            get
            {
                if (this.HisNodePosType == NodePosType.Mid)
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// 是否有节点完成条件。
        /// </summary>
        public bool IsCCNode
        {
            get
            {
                return false;
                // return this.GetValBooleanByKey(NodeAttr.IsCCNode);
            }
            set
            {
                this.SetValByKey(NodeAttr.IsCCNode, value);
            }
        }
        public string HisSubFlows
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.HisSubFlows);
            }
            set
            {
                this.SetValByKey(NodeAttr.HisSubFlows, value);
            }
        }
        public string FrmAttr
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.FrmAttr);
            }
            set
            {
                this.SetValByKey(NodeAttr.FrmAttr, value);
            }
        }
        /// <summary>
        /// 就是否有子流程
        /// </summary>
        public bool IsHaveSubFlow
        {
            get
            {
                if (this.HisSubFlows.Length > 2)
                    return true;
                else
                    return false;
            }
        }
        public bool IsSetDept
        {
            get
            {
                if (this.HisDeptStrs.Length > 3)
                    return true;
                else
                    return false;
            }
        }
        public bool IsHL
        {
            get
            {
                switch (this.HisNodeWorkType)
                {
                    case NodeWorkType.WorkFL:
                    case NodeWorkType.WorkFHL:
                    case NodeWorkType.StartWorkFL:
                        return true;
                    default:
                        return false;
                }
            }
        }
        /// <summary>
        /// 是否是分流
        /// </summary>
        public bool IsFL
        {
            get
            {
                switch (this.HisNodeWorkType)
                {
                    case NodeWorkType.WorkFL:
                    case NodeWorkType.WorkFHL:
                    case NodeWorkType.StartWorkFL:
                        return true;
                    default:
                        return false;
                }
            }
        }
        /// <summary>
        /// 是否分流合流
        /// </summary>
        public bool IsFLHL
        {
            get
            {
                switch (this.HisNodeWorkType)
                {
                    case NodeWorkType.WorkHL:
                    case NodeWorkType.WorkFL:
                    case NodeWorkType.WorkFHL:
                    case NodeWorkType.StartWorkFL:
                        return true;
                    default:
                        return false;
                }
            }
        }
        /// <summary>
        /// 是否有流程完成条件
        /// </summary>
        public bool IsCCFlow
        {
            get
            {
                //return false;
                return this.GetValBooleanByKey(NodeAttr.IsCCFlow);
            }
            set
            {
                this.SetValByKey(NodeAttr.IsCCFlow, value);
            }
        }
        /// <summary>
        /// 接受人sql
        /// </summary>
        public string RecipientSQL
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.RecipientSQL);
            }
            set
            {
                this.SetValByKey(NodeAttr.RecipientSQL, value);
            }
        }
        /// <summary>
        /// 是不是PC工作节点
        /// </summary>
        public bool IsPCNode
        {
            get
            {
                return false;
                //switch (this.HisNodeWorkType)
                //{
                //    case NodeWorkType.PCWork:
                //    case NodeWorkType.PCStartWork:
                //        return true;
                //    default:
                //        return false;
                //}
            }
        }
        /// <summary>
        /// 工作性质
        /// </summary>
        public string NodeWorkTypeText
        {
            get
            {
                return this.HisNodeWorkType.ToString();
                //return this.GetValRefTextByKey(NodeAttr.NodeWorkType);
            }
        }
        #endregion

        #region 节点的方向 (from nodes AND to nodes , 注意判断节点的生命周期的问题)
        /// <summary>
        /// 他的将要可以转向的节点方向集合.
        /// 没有生命周期的概念,全部的节点..
        /// </summary>
        private Nodes _HisToNodes = null;
        /// <summary>
        /// 他的将要转向的方向集合
        /// 如果他没有到转向方向,他就是结束节点.
        /// 没有生命周期的概念,全部的节点.
        /// </summary>
        public Nodes HisToNodes
        {
            get
            {
                if (this._HisToNodes == null)
                {
                    _HisToNodes = new Nodes();
                    _HisToNodes.AddEntities(this.HisToNDs);
                }
                return this._HisToNodes;
            }
        }
        private Nodes _HisFromNodes = null;
        /// <summary>
        /// 他的将要来自的方向集合
        /// 如果他没有到来的方向,他就是开始节点.
        /// </summary>
        public Nodes HisFromNodes
        {
            get
            {
                if (this._HisFromNodes == null || this._HisFromNodes.Count == 0)
                {
                    Directions ens = new Directions();
                    this._HisFromNodes = ens.GetHisFromNodes(this.NodeID);
                }
                return _HisFromNodes;
            }
        }
        #endregion

        #region 公共方法 (用户执行动作之后,所要做的工作)
        /// <summary>
        /// 用户执行动作之后,所要做的工作		 
        /// </summary>
        /// <returns>返回消息,运行的消息</returns>
        public string AfterDoTask()
        {
            return "";
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 节点
        /// </summary>
        public Node() { }
        /// <summary>
        /// 节点
        /// </summary>
        /// <param name="_oid">节点ID</param>	
        public Node(int _oid)
        {
            this.NodeID = _oid;
            if (SystemConfig.IsDebug)
            {
                if (this.RetrieveFromDBSources() <= 0)
                    throw new Exception("Node Retrieve 错误没有ID=" + _oid);
            }
            else
            {
                if (this.Retrieve() <= 0)
                    throw new Exception("Node Retrieve 错误没有ID=" + _oid);
            }
        }
        public Node(string ndName)
        {
            ndName = ndName.Replace("ND", "");
            this.NodeID = int.Parse(ndName);

            if (SystemConfig.IsDebug)
            {
                if (this.RetrieveFromDBSources() <= 0)
                    throw new Exception("Node Retrieve 错误没有ID=" + ndName);
            }
            else
            {
                if (this.Retrieve() <= 0)
                    throw new Exception("Node Retrieve 错误没有ID=" + ndName);
            }
        }
        public string EnName
        {
            get
            {
                return "ND" + this.NodeID;
            }
        }
        public string EnsName
        {
            get
            {
                return "ND" + this.NodeID + "s";
            }
        }
        /// <summary>
        /// 重写基类方法
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_Node");
                map.EnDesc = this.ToE("Node", "节点"); // "节点";

                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBIntPK(NodeAttr.NodeID, 0, this.ToE("NodeID", "节点ID"), true, true);
                map.AddTBString(NodeAttr.Name, null, this.ToE("Name", "名称"), true, false, 0, 100, 10);
                map.AddTBInt(NodeAttr.Step, (int)NodeWorkType.Work, this.ToE("FlowStep", "流程步骤"), true, false);

                map.AddTBInt(NodeAttr.NodeWorkType, 0, "节点类型", false, false);

                //  map.AddDDLSysEnum(NodeAttr.NodeWorkType, 0, "节点类型", true, true, NodeAttr.NodeWorkType,
                //  "@0=开始节点@1=开始分流节点@2=标准审核节点@3=数量审核节点@4=会签@5=合流节点@6=分流节点@7=分合流节点@8=普通节点");
                // map.AddTBInt(NodeAttr.NodeWorkType, 0, "节点类型", false, false);

                map.AddTBString(NodeAttr.FK_Flow, null, "FK_Flow", false, false, 0, 100, 10);

                map.AddTBString(NodeAttr.FlowName, null, null, false, true, 0, 100, 10);
                map.AddTBString(NodeAttr.FK_FlowSort, null, "FK_FlowSort", false, true, 0, 100, 10);
                map.AddTBString(NodeAttr.FK_FlowSortT, null, "FK_FlowSortT", false, true, 0, 100, 10);
                map.AddTBString(NodeAttr.FrmAttr, null, "FrmAttr", false, true, 0, 500, 10);


                //map.AddTBString(NodeAttr.EnsName, null, "工作s", false, false, 0, 100, 10);
                //map.AddTBString(NodeAttr.EnName, null, "工作", false, false, 0, 100, 10);

                map.AddTBInt(NodeAttr.WarningDays, 0, this.ToE(NodeAttr.WarningDays, "警告期限(0不警告)"), false, false); // "警告期限(0不警告)"
                map.AddTBInt(NodeAttr.DeductDays, 1, this.ToE(NodeAttr.DeductDays, "限期(天)"), false, false); //"限期(天)"
                map.AddTBFloat(NodeAttr.DeductCent, 2, this.ToE(NodeAttr.DeductCent, "扣分(每延期1天扣)"), false, false); //"扣分(每延期1天扣)"
                map.AddTBFloat(NodeAttr.MaxDeductCent, 10, this.ToE(NodeAttr.MaxDeductCent, "最高扣分"), false, false); //"最高扣分"
                map.AddTBFloat(NodeAttr.SwinkCent, float.Parse("0.1"), this.ToE("SwinkCent", "工作得分"), false, false); //"工作得分"


                //map.AddDDLSysEnum(NodeAttr.DoWhere, 0, "在那里处理", true, true);
                //map.AddDDLSysEnum(NodeAttr.RunType, 0, "执行类型", true, true);
                //map.AddTBStringDoc(NodeAttr.DoWhat, null, "工作完成后处理什么", true, false);
                //map.AddTBStringDoc(NodeAttr.DoWhatMsg, null, "提示执行信息", true, false);


                map.AddTBString(NodeAttr.RecipientSQL, null, "接受人SQL", true, false, 0, 500, 10);
                map.AddTBString(NodeAttr.DoWhat, null, "完成后处理SQL", true, false, 0, 500, 10);
                map.AddTBString(NodeAttr.Doc, null, BP.Sys.Language.GetValByUserLang("Desc", "描述"), true, false, 0, 100, 10);
                map.AddBoolean(NodeAttr.IsTask, true, "允许分配工作否?", true, true);

                map.AddTBInt(NodeAttr.ReturnRole, 2, "退回规则", true, true);
                map.AddTBInt(NodeAttr.DeliveryWay, 0, "投递规则", true, true);


                //map.AddBoolean(NodeAttr.IsCanReturn, true, "是否可以退回", true, true);
                //map.AddBoolean(NodeAttr.IsCanHidReturn, false, "是否可以隐性退回", true, true);

                map.AddBoolean(NodeAttr.IsCanCC, true, "是否可以抄送", true, true);
                map.AddBoolean(NodeAttr.IsCanRpt, true, "是否可以查看工作报告?", true, true, true);
                map.AddBoolean(NodeAttr.IsCanOver, false, "是否可以终止流程", true, true);
                map.AddBoolean(NodeAttr.IsSecret, false, "是否是保密步骤", true, true);
                map.AddBoolean(NodeAttr.IsCanDelFlow, false, "是否可以删除流程", true, true);
                map.AddBoolean(NodeAttr.IsForceKill, false, "是否可以强制删除了流程(对河流点有效)", true, true);


                map.AddBoolean(NodeAttr.IsHandOver, false, "是否可以移交", true, true);
                map.AddTBInt(NodeAttr.PassRate, 100, "通过率", true, true);


                map.AddDDLSysEnum(NodeAttr.SignType, 0, "审核模式(对审核节点有效)", true, true, 
                    NodeAttr.SignType, "@0=单签@1=汇签");

                map.AddDDLSysEnum(NodeAttr.RunModel, 0, "运行模式(对普通节点有效)", true, true,
                    NodeAttr.RunModel, "@0=普通@1=合流@2=分流@3=分合流");


                //map.AddDDLSysEnum(NodeAttr.FLRole, 0, "分流规则", true, true, 
                //    NodeAttr.FLRole, "@0=按接受人@1=按部门@2=按岗位");

                map.AddTBInt(NodeAttr.FLRole, 0, "分流规则", true, true);
                map.AddTBInt(NodeAttr.FJOpen, 0, "附件权限", true, true);

                //map.AddDDLSysEnum(NodeAttr.FJOpen, 0, "附件权限", true, true, NodeAttr.FJOpen,
                //    "@0=关闭附件@1=操作员@2=工作ID@3=流程ID");

                // 流程的节点分为干流支流. FNType  @0=平面节点@1=干流@2=支流.
                map.AddTBInt(NodeAttr.FNType, (int)FNType.Plane, "流程节点类型", false, false);

                //默认为自由表单.
                map.AddDDLSysEnum(NodeAttr.FormType, 1, this.ToE("FormType", "表单类型"), true, true);

                map.AddTBString(NodeAttr.FormUrl, "http://", "表单URL", true, false, 0, 500, 10);

                map.AddTBString(NodeAttr.RecipientSQL, null, "接受人SQL", true, false, 0, 500, 10, true);

                map.AddTBString(NodeAttr.MsgSend, null, "发送后提示信息", true, false, 0, 2000, 10, true);
                map.AddTBInt(NodeAttr.NodePosType, 0, "位置", false, false);


                map.AddTBInt(NodeAttr.IsCCNode, 0, "是否有节点完成条件", false, false);
                map.AddTBInt(NodeAttr.IsCCFlow, 0, "是否有流程完成条件", false, false);

                map.AddTBString(NodeAttr.HisStas, null, "岗位", false, false, 0, 4000, 10);
                map.AddTBString(NodeAttr.HisDeptStrs, null, "部门", false, false, 0, 4000, 10);

                map.AddTBString(NodeAttr.HisToNDs, null, "转到的节点", false, false, 0, 100, 10);
                map.AddTBString(NodeAttr.HisBillIDs, null, "单据IDs", false, false, 0, 200, 10);
                map.AddTBString(NodeAttr.HisEmps, null, "HisEmps", false, false, 0, 600, 10);
                map.AddTBString(NodeAttr.HisSubFlows, null, "HisSubFlows", false, false, 0, 200, 10);



                map.AddTBString(NodeAttr.PTable, null, "物理表", false, false, 0, 100, 10);

                map.AddTBString(NodeAttr.ShowSheets, null, "显示的表单", false, false, 0, 100, 10);
                map.AddTBString(NodeAttr.GroupStaNDs, null, "岗位分组节点", false, false, 0, 200, 10);
                map.AddTBInt(NodeAttr.X, 0, "X坐标", false, false);
                map.AddTBInt(NodeAttr.Y, 0, "Y坐标", false, false);


                //map.AddTBDate(FlowAttr.LifeCycleFrom, BP.DA.DataType.CurrentData, "生命周期从", true, false);
                //map.AddTBDate(FlowAttr.LifeCycleTo, DateTime.Now.AddMonths(3).ToString("yyyy-MM-dd"), "到", true, false);

                map.AttrsOfOneVSM.Add(new NodeEmps(), new Emps(), NodeEmpAttr.FK_Node, EmpDeptAttr.FK_Emp, DeptAttr.Name, DeptAttr.No, "接受人员");
                map.AttrsOfOneVSM.Add(new NodeDepts(), new Depts(), NodeDeptAttr.FK_Node, NodeDeptAttr.FK_Dept, DeptAttr.Name, DeptAttr.No, "接受部门");
                map.AttrsOfOneVSM.Add(new NodeStations(), new Stations(), NodeStationAttr.FK_Node, NodeStationAttr.FK_Station, DeptAttr.Name, DeptAttr.No, "岗位");


                RefMethod rm = new RefMethod();
                rm.Title = this.ToE("DesignSheet", "设计表单"); // "设计表单";
                rm.ClassMethodName = this.ToString() + ".DoMapData";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("BillBill", "单据&单据"); //"单据&单据";
                rm.ClassMethodName = this.ToString() + ".DoBill";
                rm.Icon = "/Images/FileType/doc.gif";


                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("DoFAppSet", "调用外部程序接口"); // "调用外部程序接口";
                rm.ClassMethodName = this.ToString() + ".DoFAppSet";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("DoAction", "调用事件接口"); // "调用事件接口";
                rm.ClassMethodName = this.ToString() + ".DoAction";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = "表单显示"; // this.ToE("DoAction", "调用事件接口"); // "调用事件接口";
                rm.ClassMethodName = this.ToString() + ".DoShowSheets";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("DoCond", "节点完成条件"); // "节点完成条件";
                rm.ClassMethodName = this.ToString() + ".DoCond";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("DoListen", "消息收听"); // "节点完成条件";
                rm.ClassMethodName = this.ToString() + ".DoListen";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("DoFeatureSet", "特性集"); // "调用事件接口";
                rm.ClassMethodName = this.ToString() + ".DoFeatureSet";
                map.AddRefMethod(rm);

                //rm = new RefMethod();
                //rm.Title = this.ToE("DoCondFL", "分流完成规则"); // "分流完成规则";
                //rm.ClassMethodName = this.ToString() + ".DoCondFL";
                //map.AddRefMethod(rm);

                this._enMap = map;
                return this._enMap;
            }
        }
        public string DoListen()
        {
            PubClass.WinOpen("./../WF/Admin/Listen.aspx?CondType=0&FK_Flow=" + this.FK_Flow + "&FK_Node=" + this.NodeID + "&FK_Attr=&DirType=&ToNodeID=", "单据", "cdn",
                600, 600, 100, 150);
            return null;
        }
        public string DoFeatureSet()
        {
            PubClass.WinOpen("./../WF/Admin/FeatureSetUI.aspx?CondType=0&FK_Flow=" + this.FK_Flow + "&FK_Node=" + this.NodeID + "&FK_Attr=&DirType=&ToNodeID=", "单据", "cdn",
                800, 600, 20,150);
            return null;
        }
        public string DoShowSheets()
        {
            PubClass.WinOpen("./../WF/Admin/ShowSheets.aspx?CondType=0&FK_Flow=" + this.FK_Flow + "&FK_Node=" + this.NodeID + "&FK_Attr=&DirType=&ToNodeID=", "单据", "cdn", 400, 400, 200, 300);
            return null;
        }
        public string DoCond()
        {
            PubClass.WinOpen("./../WF/Admin/Cond.aspx?CondType=" + (int)CondType.Flow + "&FK_Flow=" + this.FK_Flow + "&FK_MainNode=" + this.NodeID + "&FK_Node=" + this.NodeID + "&FK_Attr=&DirType=&ToNodeID=", "单据", "cdn", 400, 400, 200, 300);
            return null;
        }
        //public string DoCondFL()
        //{
        //    //if (this.IsCheckNode)
        //    //    return "审核节点不能设置 分流完成规则 。";

        //    PubClass.WinOpen("./../WF/Admin/Cond.aspx?CondType=" + (int)CondType.FLRole + "&FK_Flow=" + this.FK_Flow + "&FK_MainNode=" + this.NodeID + "&FK_Node=" + this.NodeID + "&FK_Attr=&DirType=", "分流完成规则", "cdn", 400, 400, 200, 300);
        //    return null;
        //}
        public string DoMapData()
        {
            switch (this.HisFormType)
            {
                case FormType.FreeForm:
                    PubClass.WinOpen("./../WF/MapDef/FreeFrm/Frm.aspx?FK_MapData=ND" + this.NodeID + "&FK_Flow="+this.FK_Flow, "设计表单", "sheet", 1024, 768, 0, 0);
                    break;
                default:
                case FormType.FixForm:
                    PubClass.WinOpen("./../WF/MapDef/MapDef.aspx?PK=ND" + this.NodeID, "设计表单", "sheet", 800, 500, 210, 300);
                    break;
            }
            return null;
        }
        public string DoAction()
        {
            PubClass.WinOpen("./../WF/Admin/Action.aspx?NodeID=" + this.NodeID + "&FK_Flow=" + this.FK_Flow, "单据", "Bill", 800, 500, 200, 300);
            return null;
        }
        public string DoBill()
        {
            PubClass.WinOpen("./../WF/Admin/Bill.aspx?NodeID=" + this.NodeID + "&FK_Flow=" + this.FK_Flow, "单据", "Bill", 800, 500, 200, 300);
            return null;
        }
        public string DoFAppSet()
        {
            PubClass.WinOpen("./../WF/Admin/FAppSet.aspx?NodeID=" + this.NodeID + "&FK_Flow=" + this.FK_Flow, "单据", "sd", 800, 500, 200, 200);
            //  PubClass.WinOpen("./../WF/Admin/FAppSet.aspx?NodeID=" + this.NodeID, 400, 500);
            return null;
        }
        #endregion

        protected override bool beforeDelete()
        {
            // 删除它的节点。
            BP.Sys.MapData md = new BP.Sys.MapData();
            md.No = "ND" + this.NodeID;
            md.Delete();

            // 删除分组.
            BP.Sys.GroupFields gfs = new BP.Sys.GroupFields();
            gfs.Retrieve(BP.Sys.GroupFieldAttr.EnName, md.No);
            gfs.Delete();

            //删除它的明细。
            BP.Sys.MapDtls dtls = new BP.Sys.MapDtls(md.No);
            dtls.Delete();

            //删除框架
            BP.Sys.MapFrames frams = new BP.Sys.MapFrames(md.No);
            frams.Delete();

            // 删除多选
            BP.Sys.MapM2Ms m2ms = new BP.Sys.MapM2Ms(md.No);
            m2ms.Delete();

            // 删除扩展
            BP.Sys.MapExts exts = new BP.Sys.MapExts(md.No);
            exts.Delete();

            //删除节点与岗位的对应.
            BP.DA.DBAccess.RunSQL("DELETE WF_NodeStation WHERE FK_Node=" + this.NodeID);
            BP.DA.DBAccess.RunSQL("DELETE WF_NodeEmp WHERE FK_Node=" + this.NodeID);
            BP.DA.DBAccess.RunSQL("DELETE WF_NodeDept WHERE FK_Node=" + this.NodeID);
            BP.DA.DBAccess.RunSQL("DELETE WF_NodeFlow WHERE FK_Node=" + this.NodeID);

            return base.beforeDelete();
        }
        public void RepariMap()
        {
            
            BP.Sys.MapData md = new BP.Sys.MapData();
            md.No = "ND" + this.NodeID;
            if (md.RetrieveFromDBSources() == 1)
            {
            }
            else
            {
                md.Name = this.Name;
                md.Insert();
            }

            Map map = md.GenerHisMap();
            if (md.No == "ND101")
            {
                int Y = 0;
            }
            if (map.Attrs.Contains(WorkAttr.FID) == false)
            {
                BP.Sys.MapAttr attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.En.EditType.Readonly;
                attr.KeyOfEn = "FID";
                attr.Name = "FID";
                attr.MyDataType = BP.DA.DataType.AppInt;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.DefVal = "0";
                attr.Insert();
            }
        }
        /// <summary>
        /// 文书流程
        /// </summary>
        /// <param name="md"></param>
        private void AddDocAttr(BP.Sys.MapData md)
        {
            /*如果是单据流程？ */
            BP.Sys.MapAttr attr = new BP.Sys.MapAttr();

            //attr = new BP.Sys.MapAttr();
            //attr.FK_MapData = md.No;
            //attr.HisEditType = BP.En.EditType.UnDel;
            //attr.KeyOfEn = "Title";
            //attr.Name = "标题";
            //attr.MyDataType = BP.DA.DataType.AppString;
            //attr.UIContralType = UIContralType.TB;
            //attr.LGType = FieldTypeS.Normal;
            //attr.UIVisible = true;
            //attr.UIIsEnable = true;
            //attr.MinLen = 0;
            //attr.MaxLen = 300;
            //attr.IDX = 1;
            //attr.UIIsLine = true;
            //attr.IDX = -100;
            //attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.KeyOfEn = "KeyWord";
            attr.Name = "主题词";
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.UIIsLine = true;
            attr.MinLen = 0;
            attr.MaxLen = 300;
            attr.IDX = -99;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.Insert();


            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.KeyOfEn = "FZ";
            attr.Name = "附注";
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.MinLen = 0;
            attr.MaxLen = 300;
            attr.UIIsLine = true;
            attr.IDX = 1;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.IDX = -98;
            attr.Insert();


            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.KeyOfEn = "DW_SW";
            attr.Name = "收文单位";
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.MinLen = 0;
            attr.MaxLen = 300;
            attr.UIIsLine = true;
            attr.IDX = 1;
            attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.KeyOfEn = "DW_FW";
            attr.Name = "发文单位";
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.MinLen = 0;
            attr.MaxLen = 300;
            attr.IDX = 1;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.UIIsLine = true;
            attr.Insert();


            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.KeyOfEn = "DW_BS";
            attr.Name = "主报(送)单位";
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.MinLen = 0;
            attr.MaxLen = 300;
            attr.IDX = 1;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.UIIsLine = true;
            attr.Insert();


            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.KeyOfEn = "DW_CS";
            attr.Name = "抄报(送)单位";
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.MinLen = 0;
            attr.MaxLen = 300;
            attr.IDX = 1;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.UIIsLine = true;
            attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.KeyOfEn = "NumPrint";
            attr.Name = "印制份数";
            attr.MyDataType = BP.DA.DataType.AppInt;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.MinLen = 0;
            attr.MaxLen = 10;
            attr.IDX = 1;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.UIIsLine = false;
            attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.KeyOfEn = "JMCD";
            attr.Name = "机密程度";
            attr.MyDataType = BP.DA.DataType.AppInt;
            attr.UIContralType = UIContralType.DDL;
            attr.LGType = FieldTypeS.Enum;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.MinLen = 0;
            attr.MaxLen = 300;
            attr.IDX = 1;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.UIIsLine = false;
            attr.UIBindKey = "JMCD";
            attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.KeyOfEn = "PRI";
            attr.Name = "紧急程度";
            attr.MyDataType = BP.DA.DataType.AppInt;
            attr.UIContralType = UIContralType.DDL;
            attr.LGType = FieldTypeS.Enum;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.MinLen = 0;
            attr.MaxLen = 300;
            attr.IDX = 1;
            attr.UIIsLine = false;
            attr.UIBindKey = "PRI";
            attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.KeyOfEn = "GWWH";
            attr.Name = "公文文号";
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.MinLen = 0;
            attr.MaxLen = 300;
            attr.IDX = 1;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.UIIsLine = false;
            attr.Insert();
        }
        /// <summary>
        /// 修复map
        /// </summary>
        public void RepareMap()
        {
            BP.Sys.MapData md = new BP.Sys.MapData();
            md.No = "ND" + this.NodeID;
            if (md.RetrieveFromDBSources() == 0)
            {
                this.CreateMap();
                return;
            }

            BP.Sys.MapAttr attr = new BP.Sys.MapAttr();
            if (attr.IsExit(MapAttrAttr.KeyOfEn, "OID", MapAttrAttr.FK_MapData, md.No) == false)
            {
                attr.FK_MapData = md.No;
                attr.KeyOfEn = "OID";
                attr.Name = "WorkID";
                attr.MyDataType = BP.DA.DataType.AppInt;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.DefVal = "0";
                attr.HisEditType = BP.En.EditType.Readonly;
                attr.Insert();
            }

            if (attr.IsExit(MapAttrAttr.KeyOfEn, "FID", MapAttrAttr.FK_MapData, md.No) == false)
            {
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.KeyOfEn = "FID";
                attr.Name = "FID";
                attr.MyDataType = BP.DA.DataType.AppInt;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.HisEditType = BP.En.EditType.UnDel;
                attr.DefVal = "0";
                attr.Insert();
            }

            if (attr.IsExit(MapAttrAttr.KeyOfEn, WorkAttr.RDT, MapAttrAttr.FK_MapData, md.No) == false)
            {
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.En.EditType.UnDel;
                attr.KeyOfEn = WorkAttr.RDT;
                attr.Name = BP.Sys.Language.GetValByUserLang("AcceptTime", "接受时间");  //"接受时间";
                attr.MyDataType = BP.DA.DataType.AppDateTime;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.Tag = "1";
                attr.Insert();
            }

            if (attr.IsExit(MapAttrAttr.KeyOfEn, WorkAttr.CDT, MapAttrAttr.FK_MapData, md.No) == false)
            {
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.En.EditType.UnDel;
                attr.KeyOfEn = WorkAttr.CDT;
                if (this.IsStartNode)
                    attr.Name = BP.Sys.Language.GetValByUserLang("StartTime", "发起时间"); //"发起时间";
                else
                    attr.Name = BP.Sys.Language.GetValByUserLang("CompleteTime", "完成时间"); //"完成时间";

                attr.MyDataType = BP.DA.DataType.AppDateTime;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.DefVal = "@RDT";
                attr.Tag = "1";
                attr.Insert();
            }


            if (attr.IsExit(MapAttrAttr.KeyOfEn, WorkAttr.Rec, MapAttrAttr.FK_MapData, md.No) == false)
            {
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.En.EditType.UnDel;
                attr.KeyOfEn = WorkAttr.Rec;
                if (this.IsStartNode == false)
                    attr.Name = BP.Sys.Language.GetValByUserLang("Rec", "记录人"); // "记录人";
                else
                    attr.Name = BP.Sys.Language.GetValByUserLang("Sponsor", "发起人"); //"发起人";

                attr.MyDataType = BP.DA.DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.MaxLen = 20;
                attr.MinLen = 0;
                attr.DefVal = "@WebUser.No";
                attr.Insert();
            }


            if (attr.IsExit(MapAttrAttr.KeyOfEn, WorkAttr.Emps, MapAttrAttr.FK_MapData, md.No) == false)
            {
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.En.EditType.UnDel;
                attr.KeyOfEn = WorkAttr.Emps;
                attr.Name = WorkAttr.Emps;
                attr.MyDataType = BP.DA.DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.MaxLen = 400;
                attr.MinLen = 0;
                attr.Insert();
            }

            if (attr.IsExit(MapAttrAttr.KeyOfEn, WorkAttr.NodeState, MapAttrAttr.FK_MapData, md.No) == false)
            {
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.KeyOfEn = WorkAttr.NodeState;
                attr.Name = BP.Sys.Language.GetValByUserLang("NodeState", "节点状态"); //"节点状态";
                attr.MyDataType = BP.DA.DataType.AppInt;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.DefVal = "0";
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.HisEditType = BP.En.EditType.UnDel;
                attr.Insert();
            }

            if (attr.IsExit(MapAttrAttr.KeyOfEn, StartWorkAttr.FK_Dept, MapAttrAttr.FK_MapData, md.No) == false)
            {
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.En.EditType.UnDel;
                attr.KeyOfEn = StartWorkAttr.FK_Dept;
                attr.Name = BP.Sys.Language.GetValByUserLang("OperDept", "操作员部门"); //"操作员部门";
                attr.MyDataType = BP.DA.DataType.AppString;
                attr.UIContralType = UIContralType.DDL;
                attr.LGType = FieldTypeS.FK;
                attr.UIBindKey = "BP.Port.Depts";
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.MinLen = 0;
                attr.MaxLen = 20;
                attr.Insert();
            }

            if (this.NodePosType == NodePosType.Start)
            {
                //开始节点信息 
                if (attr.IsExit(MapAttrAttr.KeyOfEn, StartWorkAttr.WFState, MapAttrAttr.FK_MapData, md.No) == false)
                {
                    attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = md.No;
                    attr.HisEditType = BP.En.EditType.Readonly;
                    attr.KeyOfEn = StartWorkAttr.WFState;
                    attr.DefVal = "0";
                    attr.Name = BP.Sys.Language.GetValByUserLang("FlowState", "流程状态"); //"流程状态";
                    attr.MyDataType = BP.DA.DataType.AppInt;
                    attr.LGType = FieldTypeS.Normal;
                    attr.UIBindKey = attr.KeyOfEn;
                    attr.UIVisible = false;
                    attr.UIIsEnable = false;
                    attr.Insert();
                }

                if (attr.IsExit(MapAttrAttr.KeyOfEn, StartWorkAttr.WFLog, MapAttrAttr.FK_MapData, md.No) == false)
                {
                    attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = md.No;
                    attr.HisEditType = BP.En.EditType.Readonly;
                    attr.KeyOfEn = StartWorkAttr.WFLog;
                    attr.Name = BP.Sys.Language.GetValByUserLang("Log", "日志"); //"日志";
                    attr.MyDataType = BP.DA.DataType.AppString;
                    attr.UIContralType = UIContralType.TB;
                    attr.LGType = FieldTypeS.Normal;
                    attr.UIVisible = false;
                    attr.UIIsEnable = true;
                    attr.MinLen = 0;
                    attr.MaxLen = 3000;
                    attr.Insert();
                }


                if (attr.IsExit(MapAttrAttr.KeyOfEn, StartWorkAttr.BillNo, MapAttrAttr.FK_MapData, md.No) == false)
                {
                    attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = md.No;
                    attr.HisEditType = BP.En.EditType.UnDel;
                    attr.KeyOfEn = StartWorkAttr.BillNo;
                    attr.Name = BP.Sys.Language.GetValByUserLang("BillNo", "单据文号"); //"单据文号";
                    attr.MyDataType = BP.DA.DataType.AppString;
                    attr.UIContralType = UIContralType.TB;
                    attr.LGType = FieldTypeS.Normal;
                    attr.UIVisible = false;
                    attr.UIIsEnable = false;
                    attr.MinLen = 0;
                    attr.MaxLen = 15;
                    attr.Insert();
                }

                if (attr.IsExit(MapAttrAttr.KeyOfEn, StartWorkAttr.Title, MapAttrAttr.FK_MapData, md.No) == false)
                {
                    attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = md.No;
                    attr.HisEditType = BP.En.EditType.UnDel;
                    attr.KeyOfEn = StartWorkAttr.Title;
                    attr.Name = BP.Sys.Language.GetValByUserLang("Title", "标题"); // "流程标题";
                    attr.MyDataType = BP.DA.DataType.AppString;
                    attr.UIContralType = UIContralType.TB;
                    attr.LGType = FieldTypeS.Normal;
                    attr.UIVisible = true;
                    attr.UIIsEnable = true;
                    attr.UIIsLine = true;
                    attr.UIWidth = 251;

                    attr.MinLen = 0;
                    attr.MaxLen = 200;
                    attr.IDX = -100;
                    attr.X = (float)171.2;
                    attr.Y = (float)68.4;
                    attr.Insert();
                }

                if (attr.IsExit(MapAttrAttr.KeyOfEn, "faqiren", MapAttrAttr.FK_MapData, md.No) == false)
                {
                    attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = md.No;
                    attr.HisEditType = BP.En.EditType.Edit;
                    attr.KeyOfEn = "faqiren";
                    attr.Name = BP.Sys.Language.GetValByUserLang("faqiren", "发起人"); // "发起人";
                    attr.MyDataType = BP.DA.DataType.AppString;
                    attr.UIContralType = UIContralType.TB;
                    attr.LGType = FieldTypeS.Normal;
                    attr.UIVisible = true;
                    attr.UIIsEnable = false;
                    attr.UIIsLine = false;
                    attr.MinLen = 0;
                    attr.MaxLen = 200;
                    attr.IDX = -100;
                    attr.DefVal = "@WebUser.No";
                    attr.X = (float)159.2;
                    attr.Y = (float)102.8;
                    attr.Insert();
                }

                if (attr.IsExit(MapAttrAttr.KeyOfEn, "faqishijian", MapAttrAttr.FK_MapData, md.No) == false)
                {
                    attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = md.No;
                    attr.HisEditType = BP.En.EditType.Edit;
                    attr.KeyOfEn = "faqishijian";
                    attr.Name = BP.Sys.Language.GetValByUserLang("faqishijian", "发起时间"); //"发起时间";
                    attr.MyDataType = BP.DA.DataType.AppDateTime;
                    attr.UIContralType = UIContralType.TB;
                    attr.LGType = FieldTypeS.Normal;
                    attr.UIVisible = true;
                    attr.UIIsEnable = false;
                    attr.DefVal = "@RDT";
                    attr.Tag = "1";
                    attr.X = (float)324;
                    attr.Y = (float)102.8;
                    attr.Insert();
                }


                if (attr.IsExit(MapAttrAttr.KeyOfEn, "FK_NY", MapAttrAttr.FK_MapData, md.No) == false)
                {
                    attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = md.No;
                    attr.HisEditType = BP.En.EditType.UnDel;
                    attr.KeyOfEn = "FK_NY";
                    attr.Name = BP.Sys.Language.GetValByUserLang("YearMonth", "年月"); //"年月";
                    attr.MyDataType = BP.DA.DataType.AppString;
                    attr.UIContralType = UIContralType.TB;
                    attr.UIVisible = false;
                    attr.UIIsEnable = false;
                    attr.LGType = FieldTypeS.Normal;
                    //attr.UIBindKey = "BP.Pub.NYs";
                    attr.UIVisible = false;
                    attr.UIIsEnable = false;
                    attr.MinLen = 0;
                    attr.MaxLen = 7;
                    attr.Insert();
                }


                if (attr.IsExit(MapAttrAttr.KeyOfEn, "MyNum", MapAttrAttr.FK_MapData, md.No) == false)
                {
                    attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = md.No;
                    attr.HisEditType = BP.En.EditType.UnDel;
                    attr.KeyOfEn = "MyNum";
                    attr.Name = BP.Sys.Language.GetValByUserLang("Num", "个数"); // "个数";
                    attr.DefVal = "1";
                    attr.MyDataType = BP.DA.DataType.AppInt;
                    attr.UIContralType = UIContralType.TB;
                    attr.UIVisible = false;
                    attr.UIIsEnable = false;
                    attr.LGType = FieldTypeS.Normal;
                    attr.UIVisible = false;
                    attr.UIIsEnable = false;
                    attr.Insert();
                }
            }
        }
        /// <summary>
        /// 建立map
        /// </summary>
        public void CreateMap()
        {
            BP.Sys.MapData md = new BP.Sys.MapData();
            md.No = "ND" + this.NodeID;

            string sqls = "DELETE Sys_MapData WHERE No='" + md.No + "'";
            sqls += "@DELETE Sys_MapAttr WHERE FK_MapData='" + md.No + "'";
            sqls += "@DELETE Sys_MapFrame WHERE FK_MapData='" + md.No + "'";
            sqls += "@DELETE Sys_MapExt WHERE FK_MapData='" + md.No + "'";
            sqls += "@DELETE Sys_MapDtl WHERE FK_MapData='" + md.No + "'";
            BP.DA.DBAccess.RunSQLs(sqls);
            
            md.Name = this.Name;
            md.Insert();

            BP.Sys.MapAttr attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.KeyOfEn = "OID";
            attr.Name = "WorkID";
            attr.MyDataType = BP.DA.DataType.AppInt;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = false;
            attr.UIIsEnable = false;
            attr.DefVal = "0";
            attr.HisEditType = BP.En.EditType.Readonly;
            attr.Insert();

            if (this.HisFlow.HisFlowSheetType == FlowSheetType.DocFlow)
            {
                this.AddDocAttr(md);
            }

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.KeyOfEn = "FID";
            attr.Name = "FID";
            attr.MyDataType = BP.DA.DataType.AppInt;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = false;
            attr.UIIsEnable = false;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.DefVal = "0";
            attr.Insert();
          

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.KeyOfEn = WorkAttr.RDT;
            attr.Name = BP.Sys.Language.GetValByUserLang("AcceptTime", "接受时间");  //"接受时间";
            attr.MyDataType = BP.DA.DataType.AppDateTime;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
            attr.UIIsEnable = false;
            attr.Tag = "1";
            attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.KeyOfEn = WorkAttr.CDT;
            if (this.IsStartNode)
                attr.Name = BP.Sys.Language.GetValByUserLang("StartTime", "发起时间"); //"发起时间";
            else
                attr.Name = BP.Sys.Language.GetValByUserLang("CompleteTime", "完成时间"); //"完成时间";

            attr.MyDataType = BP.DA.DataType.AppDateTime;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = false;
            attr.UIIsEnable = false;
            attr.DefVal = "@RDT";
            attr.Tag = "1";
            attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.KeyOfEn = WorkAttr.Rec;
            if (this.IsStartNode == false)
                attr.Name = BP.Sys.Language.GetValByUserLang("Rec", "记录人"); // "记录人";
            else
                attr.Name = BP.Sys.Language.GetValByUserLang("Sponsor", "发起人"); //"发起人";

            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = false;
            attr.UIIsEnable = false;
            attr.MaxLen = 20;
            attr.MinLen = 0;
            attr.DefVal = "@WebUser.No";
            attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.KeyOfEn = WorkAttr.Emps;
            attr.Name = "Emps";
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = false;
            attr.UIIsEnable = false;
            attr.MaxLen = 400;
            attr.MinLen = 0;
            attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.KeyOfEn = WorkAttr.NodeState;
            attr.Name = BP.Sys.Language.GetValByUserLang("NodeState", "节点状态"); //"节点状态";
            attr.MyDataType = BP.DA.DataType.AppInt;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.DefVal = "0";
            attr.UIVisible = false;
            attr.UIIsEnable = false;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.Insert();

            attr = new BP.Sys.MapAttr();
            attr.FK_MapData = md.No;
            attr.HisEditType = BP.En.EditType.UnDel;
            attr.KeyOfEn = StartWorkAttr.FK_Dept;
            attr.Name = BP.Sys.Language.GetValByUserLang("OperDept", "操作员部门"); //"操作员部门";
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.DDL;
            attr.LGType = FieldTypeS.FK;
            attr.UIBindKey = "BP.Port.Depts";
            attr.UIVisible = false;
            attr.UIIsEnable = false;
            attr.MinLen = 0;
            attr.MaxLen = 20;
            attr.Insert();

            if (this.NodePosType == NodePosType.Start)
            {
                //开始节点信息 
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.En.EditType.Readonly;
                attr.KeyOfEn = StartWorkAttr.WFState;
                attr.DefVal = "0";
                attr.Name = BP.Sys.Language.GetValByUserLang("FlowState", "流程状态"); //"流程状态";
                attr.MyDataType = BP.DA.DataType.AppInt;
                attr.LGType = FieldTypeS.Normal;
                attr.UIBindKey = attr.KeyOfEn;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.Insert();

                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.En.EditType.Readonly;
                attr.KeyOfEn = StartWorkAttr.WFLog;
                attr.Name = BP.Sys.Language.GetValByUserLang("Log", "日志"); //"日志";
                attr.MyDataType = BP.DA.DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = true;
                attr.MinLen = 0;
                attr.MaxLen = 3000;
                attr.Insert();

                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.En.EditType.UnDel;
                attr.KeyOfEn = "BillNo";
                attr.Name = BP.Sys.Language.GetValByUserLang("BillNo", "单据文号"); //"单据文号";
                attr.MyDataType = BP.DA.DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.MinLen = 0;
                attr.MaxLen = 15;
                attr.Insert();

                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.En.EditType.UnDel;
                attr.KeyOfEn = "Title";
                attr.Name = BP.Sys.Language.GetValByUserLang("Title", "标题"); // "流程标题";
                attr.MyDataType = BP.DA.DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = true;
                attr.UIIsLine = true;
                attr.UIWidth = 251;

                attr.MinLen = 0;
                attr.MaxLen = 200;
                attr.IDX = -100;
                attr.X = (float)171.2;
                attr.Y = (float)68.4;
                attr.Insert();

                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.En.EditType.Edit;
                attr.KeyOfEn = "faqiren";
                attr.Name = BP.Sys.Language.GetValByUserLang("faqiren", "发起人"); // "发起人";
                attr.MyDataType = BP.DA.DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.UIIsLine = false;
                attr.MinLen = 0;
                attr.MaxLen = 200;
                attr.IDX = -100;
                attr.DefVal = "@WebUser.No";
                attr.X = (float)159.2;
                attr.Y = (float)102.8;
                attr.Insert();

                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.En.EditType.Edit;
                attr.KeyOfEn = "faqishijian";
                attr.Name = BP.Sys.Language.GetValByUserLang("faqishijian", "发起时间"); //"发起时间";
                attr.MyDataType = BP.DA.DataType.AppDateTime;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.DefVal = "@RDT";
                attr.Tag = "1";
                attr.X = (float)324;
                attr.Y = (float)102.8;
                attr.Insert();


                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.En.EditType.UnDel;
                attr.KeyOfEn = "FK_NY";
                attr.Name = BP.Sys.Language.GetValByUserLang("YearMonth", "年月"); //"年月";
                attr.MyDataType = BP.DA.DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.LGType = FieldTypeS.Normal;
                //attr.UIBindKey = "BP.Pub.NYs";
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.MinLen = 0;
                attr.MaxLen = 7;
                attr.Insert();

                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.En.EditType.UnDel;
                attr.KeyOfEn = "MyNum";
                attr.Name = BP.Sys.Language.GetValByUserLang("Num", "个数"); // "个数";
                attr.DefVal = "1";
                attr.MyDataType = BP.DA.DataType.AppInt;
                attr.UIContralType = UIContralType.TB;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.Insert();

                //attr = new BP.Sys.MapAttr();
                //attr.FK_MapData = md.No;
                //attr.HisEditType = BP.En.EditType.UnDel;
                //attr.KeyOfEn = "FK_Dept";
                //attr.Name = BP.Sys.Language.GetValByUserLang("SponsorDept", "发起人部门"); //"发起人部门";
                //attr.MyDataType = BP.DA.DataType.AppString;
                //attr.UIContralType = UIContralType.TB;
                //attr.UIVisible = false;
                //attr.UIIsEnable = false;
                //attr.LGType = FieldTypeS.Normal;
                //attr.UIVisible = false;
                //attr.UIIsEnable = false;
                //attr.MinLen = 0;
                //attr.MaxLen = 7;
                //attr.DefVal = "@WebUser.FK_Dept";
                //attr.Insert();
            }

            if (this.NodePosType != NodePosType.Start)
            {
                //attr = new BP.Sys.MapAttr();
                //attr.FK_MapData = md.No;
                //attr.HisEditType = BP.En.EditType.UnDel;
                //attr.KeyOfEn = GECheckStandAttr.Sender;
                //attr.Name = "发送人"; // BP.Sys.Language.GetValByUserLang("CheckState", "审核状态"); // "审核状态";
                //attr.MyDataType = BP.DA.DataType.AppString;
                //attr.UIContralType = UIContralType.TB;
                //attr.LGType = FieldTypeS.Normal;
                //attr.UIVisible = false;
                //attr.UIIsEnable = false;
                //attr.UIIsLine = false;
                //attr.MinLen = 0;
                //attr.MaxLen = 20;
                //attr.IDX = -100;
                //attr.Insert();
            }


            //if (this.IsCheckNode)
            //{
            //    attr = new BP.Sys.MapAttr();
            //    attr.FK_MapData = md.No;
            //    attr.HisEditType = BP.En.EditType.UnDel;
            //    attr.KeyOfEn = GECheckStandAttr.Note; // "CheckState";
            //    attr.Name = "审核意见"; // BP.Sys.Language.GetValByUserLang("CheckState", "审核状态"); // "审核状态";
            //    attr.MyDataType = BP.DA.DataType.AppString;
            //    attr.UIContralType = UIContralType.TB;
            //    attr.LGType = FieldTypeS.Normal;
            //    attr.UIVisible = true;
            //    attr.UIIsEnable = false;
            //    attr.UIIsLine = true;
            //    attr.MinLen = 0;
            //    attr.MaxLen = 4000;
            //    attr.IDX = -100;
            //    attr.Insert();

            //    attr = new BP.Sys.MapAttr();
            //    attr.FK_MapData = md.No;
            //    attr.HisEditType = BP.En.EditType.UnDel;
            //    attr.KeyOfEn = GECheckStandAttr.RefMsg; // "CheckState";
            //    attr.Name = "辅助信息"; //BP.Sys.Language.GetValByUserLang("CheckState", "审核状态"); // "审核状态";
            //    attr.MyDataType = BP.DA.DataType.AppString;
            //    attr.UIContralType = UIContralType.TB;
            //    attr.LGType = FieldTypeS.Normal;
            //    attr.UIVisible = true;
            //    attr.UIIsEnable = false;
            //    attr.UIIsLine = true;
            //    attr.MinLen = 0;
            //    attr.MaxLen = 4000;
            //    attr.IDX = -100;
            //    attr.Insert();

            //    attr = new BP.Sys.MapAttr();
            //    attr.FK_MapData = md.No;
            //    attr.HisEditType = BP.En.EditType.UnDel;
            //    attr.KeyOfEn = GECheckStandAttr.CheckState;
            //    attr.Name = "审核状态"; // BP.Sys.Language.GetValByUserLang("CheckState", "审核状态"); // "审核状态";
            //    attr.MyDataType = BP.DA.DataType.AppString;
            //    attr.UIContralType = UIContralType.TB;
            //    attr.LGType = FieldTypeS.Normal;
            //    attr.UIVisible = true;
            //    attr.UIIsEnable = false;
            //    attr.UIIsLine = false;
            //    attr.MinLen = 0;
            //    attr.MaxLen = 20;
            //    attr.IDX = -100;
            //    attr.Insert();
            //}
        }
    }
    /// <summary>
    /// 节点集合
    /// </summary>
    public class Nodes : EntitiesOID
    {
        #region 方法
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Node();
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 节点集合
        /// </summary>
        public Nodes()
        {
        }
        /// <summary>
        /// 节点集合.
        /// </summary>
        /// <param name="FlowNo"></param>
        public Nodes(string fk_flow)
        {
            //   Nodes nds = new Nodes();
            this.Retrieve(NodeAttr.FK_Flow, fk_flow, NodeAttr.Step);
            //this.AddEntities(NodesCash.GetNodes(fk_flow));
            return;
        }
        #endregion

        #region 查询方法
        /// <summary>
        /// RetrieveAll
        /// </summary>
        /// <returns></returns>
        public override int RetrieveAll()
        {
            Nodes nds = Cash.GetObj(this.ToString(), Depositary.Application) as Nodes;
            if (nds == null)
            {
                nds = new Nodes();
                QueryObject qo = new QueryObject(nds);
                qo.AddWhereInSQL(NodeAttr.NodeID, " SELECT Node FROM WF_Direction ");
                qo.addOr();
                qo.AddWhereInSQL(NodeAttr.NodeID, " SELECT ToNode FROM WF_Direction ");
                qo.DoQuery();

                Cash.AddObj(this.ToString(), Depositary.Application, nds);
                Cash.AddObj(this.GetNewEntity.ToString(), Depositary.Application, nds);
            }

            this.Clear();
            this.AddEntities(nds);
            return this.Count;
        }
        /// <summary>
        /// 开始节点
        /// </summary>
        public void RetrieveStartNode()
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeAttr.NodePosType, (int)NodePosType.Start);
            qo.addAnd();
            qo.AddWhereInSQL(NodeAttr.NodeID, "SELECT FK_Node FROM WF_NodeStation WHERE FK_STATION IN (SELECT FK_STATION FROM Port_EmpSTATION WHERE FK_EMP='" + Web.WebUser.No + "')");

            qo.addOrderBy(NodeAttr.FK_Flow);
            qo.DoQuery();
        }
        #endregion
    }
}
