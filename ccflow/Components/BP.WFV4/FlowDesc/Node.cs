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
    /// �ڵ����ת����
    /// </summary>
    public enum TurnToDeal
    {
        /// <summary>
        /// ��ϵͳĬ�ϵ���ʾ
        /// </summary>
        CCFlowMsg,
        /// <summary>
        /// ָ����Ϣ
        /// </summary>
        SpecMsg,
        /// <summary>
        /// ָ��Url
        /// </summary>
        SpecUrl,
        /// <summary>
        /// ������ת��
        /// </summary>
        TurnToByCond
    }
    /// <summary>
    /// Ͷ�ݷ�ʽ
    /// </summary>
    public enum DeliveryWay
    {
        /// <summary>
        /// ����λ
        /// </summary>
        ByStation = 0,
        /// <summary>
        /// ������
        /// </summary>
        ByDept = 1,
        /// <summary>
        /// ��SQL
        /// </summary>
        BySQL = 2,
        /// <summary>
        /// �����ڵ����õ�ָ����Ա
        /// </summary>
        BySpcEmp = 3,
        /// <summary>
        /// ����һ��������ѡ��
        /// </summary>
        BySelected = 4,
        /// <summary>
        /// ����ѡ����Ա
        /// </summary>
        ByEmp = 5,
        /// <summary>
        /// ����һ��������Ա
        /// </summary>
        ByPreviousOper = 6,
        /// <summary>
        /// ����һ��������Ա���Զ���ת
        /// </summary>
        ByPreviousOperSkip = 7,
        /// <summary>
        /// ��ָ���ڵ�ĸ�λ����
        /// </summary>
        BySpecNodeStation = 8,
        /// <summary>
        /// ����λ�벿�Ž�������
        /// </summary>
        ByDeptAndStation = 9,
        /// <summary>
        /// ����λ����(�Բ��ż���Ϊγ��)
        /// </summary>
        ByStationAndEmpDept
    }
    /// <summary>
    /// �ڵ㹤���˻ع���
    /// </summary>
    public enum JumpWay
    {
        /// <summary>
        /// ������ת
        /// </summary>
        CanNotJump,
        /// <summary>
        /// �����ת
        /// </summary>
        Next,
        /// <summary>
        /// ��ǰ��ת
        /// </summary>
        Previous,
        /// <summary>
        /// �κνڵ�
        /// </summary>
        AnyNode,
        /// <summary>
        /// �����
        /// </summary>
        JumpSpecifiedNodes
    }
    /// <summary>
    /// �ڵ㹤���˻ع���
    /// </summary>
    public enum ReturnRole
    {
        /// <summary>
        /// �����˻�
        /// </summary>
        CanNotReturn,
        /// <summary>
        /// ֻ���˻���һ���ڵ�
        /// </summary>
        ReturnPreviousNode,
        /// <summary>
        /// ���˻���ǰ����ڵ�(Ĭ��)
        /// </summary>
        ReturnAnyNodes,
        /// <summary>
        /// ���˻�ָ���Ľڵ�
        /// </summary>
        ReturnSpecifiedNodes
    }
    /// <summary>
    /// ������������
    /// </summary>
    public enum FJOpen
    {
        /// <summary>
        /// ������
        /// </summary>
        None,
        /// <summary>
        /// �Բ���Ա����
        /// </summary>
        ForEmp,
        /// <summary>
        /// �Թ���ID����
        /// </summary>
        ForWorkID,
        /// <summary>
        /// ������ID����
        /// </summary>
        ForFID
    }
    /// <summary>
    /// ��������
    /// </summary>
    public enum FLRole
    {
        /// <summary>
        /// ���ս�����
        /// </summary>
        ByEmp,
        /// <summary>
        /// ���ղ���
        /// </summary>
        ByDept,
        /// <summary>
        /// ���ո�λ
        /// </summary>
        ByStation
    }
    /// <summary>
    /// ����ģʽ
    /// </summary>
    public enum RunModel
    {
        /// <summary>
        /// ��ͨ
        /// </summary>
        Ordinary = 0,
        /// <summary>
        /// ����
        /// </summary>
        HL = 1,
        /// <summary>
        /// ����
        /// </summary>
        FL = 2,
        /// <summary>
        /// �ֺ���
        /// </summary>
        FHL = 3,
        /// <summary>
        /// ���߳�
        /// </summary>
        SubThread = 4
    }
    /// <summary>
    /// �ڵ�ǩ������
    /// </summary>
    public enum SignType
    {
        /// <summary>
        /// ��ǩ
        /// </summary>
        OneSign,
        /// <summary>
        /// ��ǩ
        /// </summary>
        Countersign
    }
    /// <summary>
    /// �ڵ�״̬
    /// </summary>
    public enum NodeState
    {
        /// <summary>
        /// ��ʼ��
        /// </summary>
        Init,
        /// <summary>
        /// �Ѿ����
        /// </summary>
        Complete,
        /// <summary>
        /// �۷�״̬
        /// </summary>
        CutCent,
        /// <summary>
        /// ǿ����ֹ
        /// </summary>
        Stop,
        /// <summary>
        /// ɾ��
        /// </summary>
        Delete,
        /// <summary>
        /// �˻�
        /// </summary>
        Back,
        /// <summary>
        /// ת��
        /// </summary>
        Forward
    }
    /// <summary>
    /// �ڵ㹤������
    /// �ڵ㹤������( 0, ��˽ڵ�, 1 ��Ϣ�ɼ��ڵ�,  2, ��ʼ�ڵ�)
    /// </summary>
    public enum NodeWorkType
    {
        Work = 0,
        /// <summary>
        /// ��ʼ�ڵ�
        /// </summary>
        StartWork = 1,
        /// <summary>
        /// ��ʼ�ڵ����
        /// </summary>
        StartWorkFL = 2,
        /// <summary>
        /// �����ڵ�
        /// </summary>
        WorkHL = 3,
        /// <summary>
        /// �����ڵ�
        /// </summary>
        WorkFL = 4,
        /// <summary>
        /// �ֺ���
        /// </summary>
        WorkFHL = 5,
        /// <summary>
        /// ������
        /// </summary>
        SubThreadWork=6
    }
    /// <summary>
    /// ���̽ڵ�����
    /// </summary>
    public enum FNType
    {
        /// <summary>
        /// ƽ��ڵ�
        /// </summary>
        Plane = 0,
        /// <summary>
        /// �ֺ���
        /// </summary>
        River = 1,
        /// <summary>
        /// ֧��
        /// </summary>
        Branch = 2
    }
    /// <summary>
    /// ˭ִ����
    /// </summary>
    public enum CCRole
    {
        /// <summary>
        /// ���ܳ���
        /// </summary>
        UnCC,
        /// <summary>
        /// �ֹ�����
        /// </summary>
        HandCC,
        /// <summary>
        /// �Զ�����
        /// </summary>
        AutoCC,
        /// <summary>
        /// �ֹ����Զ�����
        /// </summary>
        HandAndAuto
    }
    /// <summary>
    /// ˭ִ����
    /// </summary>
    public enum WhoDoIt
    {
        /// <summary>
        /// ����Ա
        /// </summary>
        Operator,
        /// <summary>
        /// ����
        /// </summary>
        MachtionOnly,
        /// <summary>
        /// ���
        /// </summary>
        Mux
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
    /// �ڵ����ݲɼ�����
    /// </summary>
    public enum FormType
    {
        /// <summary>
        /// ɵ�ϱ�.
        /// </summary>
        FixForm = 0,
        /// <summary>
        /// ���ɱ�.
        /// </summary>
        FreeForm = 1,
        /// <summary>
        /// �Զ����.
        /// </summary>
        SelfForm = 2,
        /// <summary>
        /// SDKForm
        /// </summary>
        SDKForm = 3,
        /// <summary>
        /// ����(�Զ��������Ч)
        /// </summary>
        DisableIt = 9
    }
    /// <summary>
    /// �ڵ�����
    /// </summary>
    public class NodeAttr
    {
        #region ������
        /// <summary>
        /// �Ƿ���������˻�
        /// </summary>
        public const string IsCanHidReturn = "IsCanHidReturn";
        /// <summary>
        /// ͨ����
        /// </summary>
        public const string PassRate = "PassRate";
        /// <summary>
        /// �Ƿ���������������
        /// </summary>
        public const string IsCanOver = "IsCanOver";
        /// <summary>
        /// �Ƿ��Ǳ��ܲ���
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
        public const string IsHandOver = "IsHandOver";
        public const string IsCanDelFlow = "IsCanDelFlow";
        /// <summary>
        /// �Ƿ����ԭ·����
        /// </summary>
        public const string IsBackTracking = "IsBackTracking";
        /// <summary>
        /// �Ƿ�����Ͷ��·���Զ����书��?
        /// </summary>
        public const string IsRM = "IsRM";

        public const string FormType = "FormType";
        public const string FormUrl = "FormUrl";
        /// <summary>
        /// �Ƿ���Բ鿴��������
        /// </summary>
        public const string IsCanRpt = "IsCanRpt";
        /// <summary>
        /// ����֮ǰ����Ϣ��ʾ
        /// </summary>
        public const string BeforeSendAlert = "BeforeSendAlert";
        /// <summary>
        /// �Ƿ����ǿ��ɾ���߳�
        /// </summary>
        public const string IsForceKill = "IsForceKill";
        /// <summary>
        /// ������sql
        /// </summary>
        public const string RecipientSQL = "RecipientSQL";
        /// <summary>
        /// �˻ع���
        /// </summary>
        public const string ReturnRole = "ReturnRole";
        /// <summary>
        /// ת����
        /// </summary>
        public const string TurnToDeal = "TurnToDeal";
        /// <summary>
        /// ���˷�ʽ
        /// </summary>
        public const string CHWay = "CHWay";
        /// <summary>
        /// ������
        /// </summary>
        public const string Workload = "Workload";
        #endregion

        #region ��������
        /// <summary>
        /// OID
        /// </summary>
        public const string NodeID = "NodeID";
        /// <summary>
        /// �ڵ������
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
        /// ������
        /// </summary>
        public const string FlowName = "FlowName";
        /// <summary>
        /// �Ƿ���乤��
        /// </summary>
        public const string IsTask = "IsTask";
        /// <summary>
        /// �ڵ㹤������
        /// </summary>
        public const string NodeWorkType = "NodeWorkType";
        /// <summary>
        /// �ڵ������
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
        /// WarningDays(��������)
        /// </summary>
        public const string WarningDays_del = "WarningDays";
        /// <summary>
        /// DeductDays(�۷�����)
        /// </summary>
        public const string DeductDays = "DeductDays";
        /// <summary>
        /// ������
        /// </summary>
        public const string WarningDays = "WarningDays";
        /// <summary>
        /// �۷�
        /// </summary>
        public const string DeductCent = "DeductCent";
        /// <summary>
        /// ��߿۷�
        /// </summary>
        public const string MaxDeductCent = "MaxDeductCent";
        /// <summary>
        /// ����ӷ�
        /// </summary>
        public const string SwinkCent = "SwinkCent";
        /// <summary>
        /// ��������ӷ�
        /// </summary>
        public const string MaxSwinkCent = "MaxSwinkCent";
        /// <summary>
        /// ���̲���
        /// </summary>
        public const string Step = "Step";
        /// <summary>
        /// ��������
        /// </summary>
        public const string Doc = "Doc";
        /// <summary>
        ///  �������
        /// </summary>
        public const string PTable = "PTable";
        /// <summary>
        /// ǩ������
        /// </summary>
        public const string SignType = "SignType";
        /// <summary>
        /// ��ʾ�ı�
        /// </summary>
        public const string ShowSheets = "ShowSheets";
        /// <summary>
        /// ����ģʽ
        /// </summary>
        public const string RunModel = "RunModel";
        /// <summary>
        /// ˭ִ������
        /// </summary>
        public const string WhoExeIt = "WhoExeIt";
        /// <summary>
        /// ��������
        /// </summary>
        public const string FLRole = "FLRole";
        /// <summary>
        /// �Ƿ��Ǹ���
        /// </summary>
        public const string FNType = "FNType";
        /// <summary>
        /// IsSubFlow
        /// </summary>
        public const string HisSubFlows = "HisSubFlows";
        /// <summary>
        /// ��ʱ��������
        /// </summary>
        public const string DoOutTime = "DoOutTime";
        public const string OutTimeDeal = "OutTimeDeal";
        /// <summary>
        /// ����
        /// </summary>
        public const string FrmAttr = "FrmAttr";
        /// <summary>
        /// ���Ի�������Ϣ
        /// </summary>
        public const string TurnToDealDoc = "TurnToDealDoc";
        /// <summary>
        /// ���ʹ���
        /// </summary>
        public const string DeliveryWay = "DeliveryWay";
        /// <summary>
        /// �����ֶ�
        /// </summary>
        public const string FocusField = "FocusField";
        /// <summary>
        /// ��ת����
        /// </summary>
        public const string JumpWay = "JumpWay";
        /// <summary>
        /// ����ת�Ľڵ�
        /// </summary>
        public const string JumpSQL = "JumpSQL";
        /// <summary>
        /// ���͹���
        /// </summary>
        public const string CCRole = "CCRole";
        #endregion
    }
    /// <summary>
    /// ������ÿ���ڵ����Ϣ.	 
    /// </summary>
    public class Node : Entity
    {
        #region ���Ի�ȫ�ֵ� Nod
        private FrmEvents _HisNDEvents = null;
        public FrmEvents HisNDEvents
        {
            get
            {
                if (_HisNDEvents == null)
                    _HisNDEvents = new FrmEvents( "ND"+this.NodeID);
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
        /// ���Ի�ȫ�ֵ�
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
        /// �������
        /// </summary>
        /// <param name="fl"></param>
        /// <returns></returns>
        public static string CheckFlow(Flow fl)
        {
            Nodes nds = new Nodes();
            nds.Retrieve(NodeAttr.FK_Flow, fl.No);

            if (nds.Count == 0)
                return "����[" + fl.No + fl.Name + "]��û�нڵ����ݣ�����Ҫע��һ��������̡�";

            // �����Ƿ�������������Ľڵ㡣
            DA.DBAccess.RunSQL("UPDATE WF_Node SET IsCCNode=0,IsCCFlow=0  WHERE FK_Flow='" + fl.No + "'");

            //DA.DBAccess.RunSQL("UPDATE WF_Node SET IsCCNode=1 WHERE NodeID IN (SELECT NodeID FROM WF_NodeCompleteCondition)");
            //   DA.DBAccess.RunSQL("UPDATE WF_Node SET IsCCFlow=1 WHERE NodeID IN (SELECT NodeID FROM WF_FlowCompleteCondition) ");

            DA.DBAccess.RunSQL("DELETE FROM WF_Direction WHERE Node=0 OR ToNode=0");
            DA.DBAccess.RunSQL("DELETE FROM WF_Direction WHERE Node  NOT IN (SELECT NODEID FROM WF_NODE )");
            DA.DBAccess.RunSQL("DELETE FROM WF_Direction WHERE ToNode  NOT IN (SELECT NODEID FROM WF_NODE) ");

            //�����������
            // DA.DBAccess.RunSQL("UPDATE WF_Node SET FK_FlowSort=(SELECT FK_FlowSort FROM WF_Flow WHERE WF_Flow.No=WF_Node.FK_Flow) ");
            //   DA.DBAccess.RunSQL("UPDATE WF_Node SET FK_FlowSortT=(SELECT Name FROM WF_FlowSort WHERE No=WF_NODE.FK_FlowSort)");


            // ������Ϣ����λ���ڵ���Ϣ��
            foreach (Node nd in nds)
            {
                DA.DBAccess.RunSQL("UPDATE WF_Node SET FK_FlowSort='" + fl.FK_FlowSort + "',FK_FlowSortT='" + fl.FK_FlowSortText + "'");

                BP.Sys.MapData md = new BP.Sys.MapData();
                md.No = "ND" + nd.NodeID;
                if (md.IsExits == false)
                {
                    nd.CreateMap();
                }


                // ������λ��
                NodeStations stas = new NodeStations(nd.NodeID);
                string strs = "";
                foreach (NodeStation sta in stas)
                    strs += "@" + sta.FK_Station;

                nd.HisStas = strs;

                // �������š�
                NodeDepts ndpts = new NodeDepts(nd.NodeID);
                strs = "";
                foreach (NodeDept ndp in ndpts)
                    strs += "@" + ndp.FK_Dept;

                nd.HisDeptStrs = strs;

                // ��ִ����Ա��
                NodeEmps ndemps = new NodeEmps(nd.NodeID);
                strs = "";
                foreach (NodeEmp ndp in ndemps)
                    strs += "@" + ndp.FK_Emp;
                nd.HisEmps = strs;

                // �����̡�
                NodeFlows ndflows = new NodeFlows(nd.NodeID);
                strs = "";
                foreach (NodeFlow ndp in ndflows)
                    strs += "@" + ndp.FK_Flow;
                nd.HisSubFlows = strs;


                // �ڵ�
                strs = "";
                Directions dirs = new Directions(nd.NodeID);
                foreach (Direction dir in dirs)
                    strs += "@" + dir.ToNode;

                nd.HisToNDs = strs;

                // ����
                strs = "";
                BillTemplates temps = new BillTemplates(nd);
                foreach (BillTemplate temp in temps)
                    strs += "@" + temp.No;
                nd.HisBillIDs = strs;

                // ���ڵ��λ�����ԡ�
                nd.HisNodePosType = nd.GetHisNodePosType();
                nd.DirectUpdate();
            }

            // ��������Ա����
            string sql = "select FK_Station from WF_FlowStation WHERE fk_flow='" + fl.No + "'";
            DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            string mystas = "";
            foreach (DataRow dr in dt.Rows)
            {
                mystas += dr[0].ToString() + ",";
            }
         //   fl.CCStas = mystas;


            // �����λ����.
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


            /* �ж����̵����� */
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
                //  this.SetValByKey(BtnAttr.CCRole, 0);
                this.SetValByKey(BtnAttr.EndFlowEnable, 0);
                this.SetValByKey(BtnAttr.RptEnable, 0);
                this.SetValByKey(BtnAttr.EndFlowEnable, 0);
                this.SetValByKey(BtnAttr.EndFlowEnable, 0);
            }

            #region �Է��ʹ�����м��
            switch (this.HisDeliveryWay)
            {
                case DeliveryWay.ByStation:
                    //if (nd.HisStations.Count == 0)
                    //    rpt += "<font color=red>�������˸ýڵ�ķ��ʹ����ǰ���λ��������û��Ϊ�ڵ�󶨸�λ��</font>";
                    break;
                case DeliveryWay.ByDept:
                    //if (nd.HisDepts.Count == 0)
                    //    rpt += "<font color=red>�������˸ýڵ�ķ��ʹ����ǰ����ţ�������û��Ϊ�ڵ�󶨲��š�</font>";
                    break;
                case DeliveryWay.ByEmp:
                    //if (nd.HisNodeEmps.Count == 0)
                    //    rpt += "<font color=red>�������˸ýڵ�ķ��ʹ����ǰ���Ա��������û��Ϊ�ڵ����Ա��</font>";
                    break;
                case DeliveryWay.BySQL:
                    //if (nd.IsStartNode)
                    //{
                    //    rpt += "<font color=red>��ʼ�ڵ㲻֧�ְ�SQL ���÷��ʹ���</font>";
                    //    break;
                    //}

                    //if (nd.RecipientSQL.Trim().Length == 0)
                    //    rpt += "<font color=red>�������˸ýڵ�ķ��ʹ����ǰ�SQL��ѯ��������û���ڽڵ����������ò�ѯsql����sql��Ҫ���ǲ�ѯ�������No,Name�����У�sql���ʽ��֧��@+�ֶα�������ϸ�ο������ֲ� ��</font>";
                    //else
                    //{
                    //    try
                    //    {
                    //        DataTable testDB = DBAccess.RunSQLReturnTable(nd.RecipientSQL);
                    //        if (testDB.Columns.Contains("No") == false || testDB.Columns.Contains("Name") == false)
                    //        {
                    //            rpt += "<font color=red>�������˸ýڵ�ķ��ʹ����ǰ�SQL��ѯ�����õ�sql�����Ϲ��򣬴�sql��Ҫ���ǲ�ѯ�������No,Name�����У�sql���ʽ��֧��@+�ֶα�������ϸ�ο������ֲ� ��</font>";
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        rpt += "<font color=red>�������˸ýڵ�ķ��ʹ����ǰ�SQL��ѯ,ִ�д�������." + ex.Message + "</font>";
                    //    }
                    //}
                    break;
                case DeliveryWay.BySpcEmp:
                    //if (nd.IsStartNode)
                    //{
                    //    rpt += "<font color=red>��ʼ�ڵ㲻������ָ������Ա���ʹ���</font>";
                    //    break;
                    //}
                    //if (attrs.Contains(BP.Sys.MapAttrAttr.KeyOfEn, "FK_Emp") == false)
                    //{
                    //    /*���ڵ��ֶ��Ƿ���FK_Emp�ֶ�*/
                    //    rpt += "<font color=red>�������˸ýڵ�ķ��ʹ����ǰ�ָ���ڵ����Ա��������û���ڽڵ��������FK_Emp�ֶΣ���ϸ�ο������ֲ� ��</font>";
                    //}
                    break;
                case DeliveryWay.BySelected: /* ����һ��������Աѡ�� */
                    //if (nd.IsStartNode)
                    //{
                    //    rpt += "<font color=red>��ʼ�ڵ㲻������ָ����ѡ����Ա���ʹ���</font>";
                    //    break;
                    //}

                    //if (attrs.Contains(BP.Sys.MapAttrAttr.KeyOfEn, "FK_Emp") == false)
                    //{
                    //    /*���ڵ��ֶ��Ƿ���FK_Emp�ֶ�*/
                    //    rpt += "<font color=red>�������˸ýڵ�ķ��ʹ����ǰ�ָ���ڵ����Ա��������û���ڽڵ��������FK_Emp�ֶΣ���ϸ�ο������ֲ� ��</font>";
                    //}
                    break;
                case DeliveryWay.ByPreviousOper: /* ����һ��������Աѡ�� */
                case DeliveryWay.ByPreviousOperSkip: /* ����һ��������Աѡ�� */
                    //if (nd.IsStartNode)
                    //{
                    //    rpt += "<font color=red>�ڵ���ʹ������ô���:��ʼ�ڵ㡣</font>";
                    //    break;
                    //}
                    break;
                default:
                    break;
            }
            #endregion


            #region ���������ж������ı�ǡ�
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
                        throw new Exception("@���������ÿ�ʼ�ڵ�Ϊ�����ڵ㡣");
                    else
                        this.HisNodeWorkType = NodeWorkType.WorkHL;
                    break;
                case RunModel.FHL:
                    if (this.IsStartNode)
                        throw new Exception("@���������ÿ�ʼ�ڵ�Ϊ�ֺ����ڵ㡣");
                    else
                        this.HisNodeWorkType = NodeWorkType.WorkFHL;
                    break;
                case RunModel.SubThread:
                    this.HisNodeWorkType = NodeWorkType.SubThreadWork;
                    break;
                default:
                    throw new Exception("eeeee");
                    break;
            }

            #region  �ж����̽ڵ����͡�
            while (true)
            {
                if (this.IsFLHL)
                {
                    /*����Ƿֺ�������ô��һ���Ǹ�����*/
                    this.HisFNType = FNType.River;
                    break;
                }

                if (fl.HisFlowType == FlowType.Panel)
                {
                    /*˵����û�з����̺���֮˵��ֻ��ƽ��ڵ㡣*/
                    this.HisFNType = FNType.Plane;
                    break;
                }


                if (this.HisNodeWorkType == NodeWorkType.StartWork)
                {
                    /* ����ǿ�ʼ�ڵ㣬�ж������ڵ����Ƿ��з��������ڵ㣬�����˵������һ�� */
                    this.HisFNType = FNType.Branch;
                    break;
                }

                // �鿴�ܹ�ת�����Ľڵ㡣
                Nodes fNDs = this.HisFromNodes;
                if (fNDs.Count == 0)
                {
                    /*˵������һ�������ڵ㣬�Ͳ��������������*/
                    break;
                }

                bool isOk = false;
                foreach (Node nd in fNDs)
                {
                    if (nd.IsFL)
                    {
                        /*������һ���ڵ��Ƿ���*/
                        this.HisFNType = FNType.Branch;
                        isOk = true;
                        break;
                    }

                    if (nd.HisNodeWorkType == NodeWorkType.WorkHL)
                    {
                        /*������һ���ڵ��Ǻ���*/
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

                ////����ж��ˣ��ͷų�������
                //if (isOk)
                //    break;
                //else
                //    throw new Exception("@û���жϵ����Ƿ������Ǻ����ڵ㡣");
                break;
            }
            #endregion  �ж����̽ڵ����͡�


            return base.beforeUpdate();
        }
        /// <summary>
        /// ע������
        /// </summary>
        /// <param name="fl"></param>
        /// <returns></returns>
        public static string RegFlow(Flow fl)
        {

            #region ȡ�������̽ڵ�
            string msg = "";
            // ȡ�������̽ڵ�.
            Nodes alNodes = new Nodes();
            ArrayList alWorks = BP.DA.ClassFactory.GetObjects("BP.WF.Works");
            #endregion


            #region ���������ж������ı�ǡ�
            DA.DBAccess.RunSQL("UPDATE WF_Node SET IsCCNode=0,IsCCFlow=0  WHERE FK_Flow='"+fl.No+"'");
            DA.DBAccess.RunSQL("UPDATE WF_Node SET IsCCNode=1 WHERE NodeID IN (SELECT NodeID FROM WF_Cond WHERE CondType=0) AND FK_Flow='"+fl.No+"'");
            DA.DBAccess.RunSQL("UPDATE WF_Node SET IsCCFlow=1 WHERE NodeID IN (SELECT NodeID FROM WF_Cond WHERE CondType=1) AND FK_Flow='" + fl.No + "'");
            #endregion


            #region ɾ������ڵ�
           // DBAccess.RunSQL("DELETE FROM WF_FLOWCOMPLETECONDITION WHERE nodeid not in ( select nodeid from wf_node)");
            //   DBAccess.RunSQL("DELETE FROM WF_NODECOMPLETECONDITION WHERE enclassName not in (select EnsName from wf_node)");
            //  DBAccess.RunSQL("DELETE FROM WF_FLOWCOMPLETECONDITION WHERE enclassName not in (select EnsName from wf_node)");
            DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist WHERE FK_Node not in( select NodeId from wf_node)");


            //���±�׼��˽ڵ��������˽ڵ�ı�����WF_Node��ȥ
            // DBAccess.RunSQL("update wf_node set PTable='WF_GECheckStand' WHERE ensname='BP.WF.GECheckStands'");
            // DBAccess.RunSQL("update wf_node set PTable='WF_NumCheck' WHERE ensname='BP.WF.NumChecks'");
            #endregion


            #region ���Ƚڵ�Ļ�����Ϣ
            // ��鿪ʼ�ڵ��������
            //string sql = "SELECT COUNT(*) FROM (SELECT NODEID, FK_FLOW, COUNT(FK_FLOW) AS NUM  FROM WF_NODE  WHERE WF_NODE.IsStartNode=1 GROUP BY NODEID ,FK_FLOW) WHERE NUM >1";
            //if (DBAccess.RunSQLReturnValInt(sql) > 0)
            //    msg += "���̵Ŀ�ʼ�ڵ㲻Ψһ��" + sql;

            //sql = "UPDATE WF_FLOW SET (StartNODEID ,EnsName,EnName )=( SELECT NODEID, EnsName,EnName  FROM WF_NODE WHERE WF_NODE.IsStartNode=1 AND WF_NODE.FK_FLOW=WF_FLOW.NO )";
            //DBAccess.RunSQL(sql);
            #endregion

            return msg;
        }
        #endregion

        #region ����
        private Conds _HisNodeCompleteConditions = null;
        /// <summary>
        /// �ڵ�������������
        /// ����������֮����or �Ĺ�ϵ, ����˵,����κ�һ����������,���������Ա������ڵ��ϵ�����������.
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
        /// ����������������,�˽ڵ�������������������
        /// ����������֮����or �Ĺ�ϵ, ����˵,����κ�һ����������,�������������.
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

        #region ��������
        /// <summary>
        /// �ڲ����
        /// </summary>
        public string No
        {
            get
            {
                return this.NodeID.ToString().Substring(this.NodeID.ToString().Length - 2);
            }
        }
        /// <summary>
        /// ���̽ڵ�����
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
        /// ����
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
        /// ��Ҫ���������ڣ�
        /// </summary>
        public float DeductDays
        {
            get
            {
                float i= this.GetValFloatByKey(NodeAttr.DeductDays);
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
        /// ��߿۷�
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
        /// �������ӷ�
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
        /// ���̲���
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
        /// ��������( ��Ҫ���������ڣ�+��������)
        /// </summary>
        public float NeedCompleteDays
        {
            get
            {
                return this.DeductDays;
            }
        }
        /// <summary>
        /// �۷��ʣ���/�죩
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
        /// �Ƿ��ǿ�ʼ�ڵ�
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
        public float WarningDays
        {
            get
            {
                if (this.GetValFloatByKey(NodeAttr.WarningDays) == 0)
                    return this.DeductDays;
                else
                    return this.DeductDays - this.GetValFloatByKey(NodeAttr.WarningDays);
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
        /// λ��
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
        /// ����ģʽ
        /// </summary>
        public RunModel HisRunModel
        {
            get
            {
                return (RunModel)this.GetValIntByKey(NodeAttr.RunModel);
            }
        }
        /// <summary>
        /// �����ֶ�
        /// </summary>
        public string FocusField
        {
            get
            {
                return this.GetValStrByKey(NodeAttr.FocusField);
            }
        }
        /// <summary>
        /// �ڵ��������
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
        /// ��ȡ������һ���ķ�����
        /// </summary>
        private Node _GetHisPriFLNode(Nodes nds)
        {
            foreach (Node mynd in nds)
            {
                if (mynd.IsFL)
                    return mynd;
                else
                    return _GetHisPriFLNode(mynd.HisFromNodes);
            }
            return null;
        }
        /// <summary>
        /// ������һ�������ڵ�
        /// </summary>
        public Node HisPriFLNode
        {
            get
            {
                return _GetHisPriFLNode(this.HisFromNodes);
            }
        }
        public string TurnToDealDoc
        {
            get
            {
                return this.GetValStrByKey(NodeAttr.TurnToDealDoc);
            }
            set
            {
                SetValByKey(NodeAttr.TurnToDealDoc, value);
            }
        }
        /// <summary>
        /// ����ת�Ľڵ�
        /// </summary>
        public string JumpSQL
        {
            get
            {
                return this.GetValStrByKey(NodeAttr.JumpSQL);
            }
            set
            {
                SetValByKey(NodeAttr.JumpSQL, value);
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
        /// Ҫ��ʾ�ں���ı�
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
        /// ǩ������
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

        #region ��չ����
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
        /// �˽ڵ����ڵ�����.
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
        /// �ǲ��Ƕ��λ�����ڵ�.
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
        /// �˽ڵ����ڵĹ�����λ
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
        /// ���Ĺ�������
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
        /// �˽ڵ����ڵĹ�����λ
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
                    str += st.Name + "��";
                }
                return str;
            }
        }

        #region ת���Ĺ���
        private Work _Work = null;
        /// <summary>
        /// �õ�����һ������ʵ��
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
                    _Work.HisNode = this;
                    _Work.NodeID = this.NodeID;
                    return _Work;
                }
                return _Work;
            }
        }
        private Works _HisWorks = null;
        /// <summary>
        /// ���Ĺ���s
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

        #region ��������
        /// <summary>
        /// �õ�һ������dataʵ��
        /// </summary>
        /// <param name="workId">����ID</param>
        /// <returns>���û�оͷ���null</returns>
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

        #region �ڵ�Ĺ�������
        /// <summary>
        /// ת����
        /// </summary>
        public TurnToDeal HisTurnToDeal
        {
            get
            {
                return (TurnToDeal)this.GetValIntByKey(NodeAttr.TurnToDeal);
            }
            set
            {
                this.SetValByKey(NodeAttr.TurnToDeal, (int)value);
            }
        }
        /// <summary>
        /// ���ʹ���
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
        /// ���͹���
        /// </summary>
        public CCRole HisCCRole
        {
            get
            {
                return (CCRole)this.GetValIntByKey(NodeAttr.CCRole);
            }
            set
            {
                this.SetValByKey(NodeAttr.CCRole, (int)value);
            }
        }
        /// <summary>
        /// ������������
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
        /// ��������
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
#warning 2012-01-24�޶�,û���Զ�����������ԡ�
                switch (this.HisRunModel)
                {
                    case RunModel.Ordinary:
                        if (this.IsStartNode)
                            return NodeWorkType.StartWork;
                        else
                            return NodeWorkType.Work;
                    case RunModel.FL:
                        if (this.IsStartNode)
                            return NodeWorkType.StartWorkFL;
                        else
                            return NodeWorkType.WorkFL;
                    case RunModel.HL:
                            return NodeWorkType.WorkHL;
                    case RunModel.FHL:
                        return NodeWorkType.WorkFHL;
                    case RunModel.SubThread:
                        return NodeWorkType.SubThreadWork;
                    default:
                        throw new Exception("@û���ж�����NodeWorkType.");
                }
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

        #region �������� (���ڽڵ�λ�õ��ж�)
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
        /// ����
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
        /// �ǲ��ǽ����ڵ�
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
        /// <summary>
        /// �Ƿ�������˻غ�ԭ·���أ�
        /// </summary>
        public bool IsBackTracking
        {
            get
            {
                return this.GetValBooleanByKey(NodeAttr.IsBackTracking);
            }
        }
        /// <summary>
        /// �Ƿ������Զ����书��
        /// </summary>
        public bool IsRememberMe
        {
            get
            {
                return this.GetValBooleanByKey(NodeAttr.IsRM);
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
        /// �Ƿ��ܲ���
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
        /// �Ƿ�����ƽ�
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
        /// �˻ع���
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
        /// �ǲ����м�ڵ�
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
        /// �Ƿ��нڵ����������
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
        /// ���Ƿ���������
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
        public bool IsHL
        {
            get
            {
                switch (this.HisNodeWorkType)
                {
                    case NodeWorkType.WorkHL:
                    case NodeWorkType.WorkFHL:
                        return true;
                    default:
                        return false;
                }
            }
        }
        /// <summary>
        /// �Ƿ��Ƿ���
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
        /// �Ƿ��������
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
        /// �Ƿ��������������
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
        /// ������sql
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
        /// �ǲ���PC�����ڵ�
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
        /// ��������
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

        #region �ڵ�ķ��� (from nodes AND to nodes , ע���жϽڵ���������ڵ�����)
        /// <summary>
        /// ���Ľ�Ҫ����ת��Ľڵ㷽�򼯺�.
        /// û���������ڵĸ���,ȫ���Ľڵ�..
        /// </summary>
        private Nodes _HisToNodes = null;
        /// <summary>
        /// ���Ľ�Ҫת��ķ��򼯺�
        /// �����û�е�ת����,�����ǽ����ڵ�.
        /// û���������ڵĸ���,ȫ���Ľڵ�.
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
        /// ���Ľ�Ҫ���Եķ��򼯺�
        /// �����û�е����ķ���,�����ǿ�ʼ�ڵ�.
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

        #region �������� (�û�ִ�ж���֮��,��Ҫ���Ĺ���)
        /// <summary>
        /// �û�ִ�ж���֮��,��Ҫ���Ĺ���		 
        /// </summary>
        /// <returns>������Ϣ,���е���Ϣ</returns>
        public string AfterDoTask()
        {
            return "";
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// �ڵ�
        /// </summary>
        public Node() { }
        /// <summary>
        /// �ڵ�
        /// </summary>
        /// <param name="_oid">�ڵ�ID</param>	
        public Node(int _oid)
        {
            this.NodeID = _oid;
            if (SystemConfig.IsDebug)
            {
                if (this.RetrieveFromDBSources() <= 0)
                    throw new Exception("Node Retrieve ����û��ID=" + _oid);
            }
            else
            {
                if (this.Retrieve() <= 0)
                    throw new Exception("Node Retrieve ����û��ID=" + _oid);
            }
        }
        public Node(string ndName)
        {
            ndName = ndName.Replace("ND", "");
            this.NodeID = int.Parse(ndName);

            if (SystemConfig.IsDebug)
            {
                if (this.RetrieveFromDBSources() <= 0)
                    throw new Exception("Node Retrieve ����û��ID=" + ndName);
            }
            else
            {
                if (this.Retrieve() <= 0)
                    throw new Exception("Node Retrieve ����û��ID=" + ndName);
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
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_Node");
                map.EnDesc = this.ToE("Node", "�ڵ�"); // "�ڵ�";

                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBIntPK(NodeAttr.NodeID, 0, this.ToE("NodeID", "�ڵ�ID"), true, true);
                map.AddTBString(NodeAttr.Name, null, this.ToE("Name", "����"), true, false, 0, 100, 10);
                map.AddTBInt(NodeAttr.Step, (int)NodeWorkType.Work, this.ToE("FlowStep", "���̲���"), true, false);

                map.AddTBInt(NodeAttr.NodeWorkType, 0, "�ڵ�����", false, false);

                //  map.AddDDLSysEnum(NodeAttr.NodeWorkType, 0, "�ڵ�����", true, true, NodeAttr.NodeWorkType,
                //  "@0=��ʼ�ڵ�@1=��ʼ�����ڵ�@2=��׼��˽ڵ�@3=������˽ڵ�@4=��ǩ@5=�����ڵ�@6=�����ڵ�@7=�ֺ����ڵ�@8=��ͨ�ڵ�");
                // map.AddTBInt(NodeAttr.NodeWorkType, 0, "�ڵ�����", false, false);

                map.AddTBString(NodeAttr.FK_Flow, null, "FK_Flow", false, false, 0, 100, 10);

                map.AddTBString(NodeAttr.FlowName, null, "������", false, true, 0, 100, 10);
                map.AddTBString(NodeAttr.FK_FlowSort, null, "FK_FlowSort", false, true, 0, 100, 10);
                map.AddTBString(NodeAttr.FK_FlowSortT, null, "FK_FlowSortT", false, true, 0, 100, 10);
                map.AddTBString(NodeAttr.FrmAttr, null, "FrmAttr", false, true, 0, 500, 10);

                //map.AddTBString(NodeAttr.EnsName, null, "����s", false, false, 0, 100, 10);
                //map.AddTBString(NodeAttr.EnName, null, "����", false, false, 0, 100, 10);

                map.AddTBFloat(NodeAttr.WarningDays, 0, this.ToE(NodeAttr.WarningDays, "��������(0������)"), false, false); // "��������(0������)"
                map.AddTBFloat(NodeAttr.DeductDays, 1, this.ToE(NodeAttr.DeductDays, "����(��)"), false, false); //"����(��)"
                map.AddTBFloat(NodeAttr.DeductCent, 2, this.ToE(NodeAttr.DeductCent, "�۷�(ÿ����1���)"), false, false); //"�۷�(ÿ����1���)"
                map.AddTBFloat(NodeAttr.MaxDeductCent, 10, this.ToE(NodeAttr.MaxDeductCent, "��߿۷�"), false, false); //"��߿۷�"
                map.AddTBFloat(NodeAttr.SwinkCent, float.Parse("0.1"), this.ToE("SwinkCent", "�����÷�"), false, false); //"�����÷�"


                //map.AddDDLSysEnum(NodeAttr.DoWhere, 0, "�����ﴦ��", true, true);
                //map.AddDDLSysEnum(NodeAttr.RunType, 0, "ִ������", true, true);
                //map.AddTBStringDoc(NodeAttr.DoWhat, null, "������ɺ���ʲô", true, false);
                //map.AddTBStringDoc(NodeAttr.DoWhatMsg, null, "��ʾִ����Ϣ", true, false);


                map.AddTBString(NodeAttr.RecipientSQL, null, "���ʹ�������", true, false, 0, 500, 10);

                map.AddTBString(NodeAttr.Doc, null, BP.Sys.Language.GetValByUserLang("Desc", "����"), true, false, 0, 100, 10);
                map.AddBoolean(NodeAttr.IsTask, true, "������乤����?", true, true);

                map.AddTBInt(NodeAttr.ReturnRole, 2, "�˻ع���", true, true);
                map.AddTBInt(NodeAttr.DeliveryWay, 0, "���ʹ���", true, true);
                map.AddTBInt(NodeAttr.CCRole, 0, "���͹���", true, true);


                //map.AddBoolean(NodeAttr.IsCanReturn, true, "�Ƿ�����˻�", true, true);
                //map.AddBoolean(NodeAttr.IsCanHidReturn, false, "�Ƿ���������˻�", true, true);
                //map.AddBoolean(NodeAttr.CCRole, true, "�Ƿ���Գ���", true, true);

                map.AddBoolean(NodeAttr.IsCanRpt, true, "�Ƿ���Բ鿴��������?", true, true, true);
                map.AddBoolean(NodeAttr.IsCanOver, false, "�Ƿ������ֹ����", true, true);
                map.AddBoolean(NodeAttr.IsSecret, false, "�Ƿ��Ǳ��ܲ���", true, true);
                map.AddBoolean(NodeAttr.IsCanDelFlow, false, "�Ƿ����ɾ������", true, true);
                map.AddBoolean(NodeAttr.IsForceKill, false, "�Ƿ����ǿ��ɾ��������(�Ժ�������Ч)", true, true);
                map.AddBoolean(NodeAttr.IsBackTracking, false, "�Ƿ�������˻غ�ԭ·����(ֻ�������˻ع��ܲ���Ч)", true, true, true);
                map.AddBoolean(NodeAttr.IsRM, true, "�Ƿ�����Ͷ��·���Զ����书��?", true, true, true);

                map.AddBoolean(NodeAttr.IsHandOver, false, "�Ƿ�����ƽ�", true, true);
                map.AddTBInt(NodeAttr.PassRate, 100, "ͨ����", true, true);

                map.AddDDLSysEnum(NodeAttr.SignType, 0, "���ģʽ(����˽ڵ���Ч)", true, true, 
                    NodeAttr.SignType, "@0=��ǩ@1=��ǩ");

                map.AddDDLSysEnum(NodeAttr.RunModel, 0, "����ģʽ(����ͨ�ڵ���Ч)", true, true,
                    NodeAttr.RunModel, "@0=��ͨ@1=����@2=����@3=�ֺ���@4=���߳�");

                //map.AddDDLSysEnum(NodeAttr.FLRole, 0, "��������", true, true, 
                //    NodeAttr.FLRole, "@0=��������@1=������@2=����λ");

                map.AddTBInt(NodeAttr.FLRole, 0, "��������", true, true);
                map.AddTBInt(NodeAttr.FJOpen, 0, "����Ȩ��", true, true);

                //map.AddDDLSysEnum(NodeAttr.FJOpen, 0, "����Ȩ��", true, true, NodeAttr.FJOpen,
                //    "@0=�رո���@1=����Ա@2=����ID@3=����ID");

                // ���̵Ľڵ��Ϊ����֧��. FNType  @0=ƽ��ڵ�@1=����@2=֧��.
                map.AddTBInt(NodeAttr.FNType, (int)FNType.Plane, "���̽ڵ�����", false, false);

                //Ĭ��Ϊ���ɱ�.
                map.AddDDLSysEnum(NodeAttr.FormType, 1, this.ToE("FormType", "������"), true, true);

                map.AddTBString(NodeAttr.FormUrl, "http://", "��URL", true, false, 0, 500, 10);

                map.AddTBString(NodeAttr.RecipientSQL, null, "������SQL", true, false, 0, 500, 10, true);

                map.AddTBInt(NodeAttr.TurnToDeal, 0, "ת����", false, false);
                map.AddTBString(NodeAttr.TurnToDealDoc, null, "���ͺ���ʾ��Ϣ", true, false, 0, 2000, 10, true);
                map.AddTBInt(NodeAttr.NodePosType, 0, "λ��", false, false);

                map.AddTBInt(NodeAttr.IsCCNode, 0, "�Ƿ��нڵ��������", false, false);
                map.AddTBInt(NodeAttr.IsCCFlow, 0, "�Ƿ��������������", false, false);

                map.AddTBString(NodeAttr.HisStas, null, "��λ", false, false, 0, 4000, 10);
                map.AddTBString(NodeAttr.HisDeptStrs, null, "����", false, false, 0, 4000, 10);

                map.AddTBString(NodeAttr.HisToNDs, null, "ת���Ľڵ�", false, false, 0, 300, 10);
                map.AddTBString(NodeAttr.HisBillIDs, null, "����IDs", false, false, 0, 200, 10);
                map.AddTBString(NodeAttr.HisEmps, null, "HisEmps", false, false, 0, 4000, 10);
                map.AddTBString(NodeAttr.HisSubFlows, null, "HisSubFlows", false, false, 0, 200, 10);

                map.AddTBString(NodeAttr.PTable, null, "�����", false, false, 0, 100, 10);

                map.AddTBString(NodeAttr.ShowSheets, null, "��ʾ�ı�", false, false, 0, 100, 10);
                map.AddTBString(NodeAttr.GroupStaNDs, null, "��λ����ڵ�", false, false, 0, 200, 10);
                map.AddTBInt(NodeAttr.X, 0, "X����", false, false);
                map.AddTBInt(NodeAttr.Y, 0, "Y����", false, false);

                map.AddTBString(NodeAttr.FocusField, null, "�����ֶ�", false, false, 0, 100, 10);
                map.AddTBString(NodeAttr.JumpSQL, null, "����ת�Ľڵ�", true, false, 0, 2000, 10, true);


                //map.AddTBDate(FlowAttr.LifeCycleFrom, BP.DA.DataType.CurrentData, "�������ڴ�", true, false);
                //map.AddTBDate(FlowAttr.LifeCycleTo, DateTime.Now.AddMonths(3).ToString("yyyy-MM-dd"), "��", true, false);

                map.AttrsOfOneVSM.Add(new NodeEmps(), new Emps(), NodeEmpAttr.FK_Node, EmpDeptAttr.FK_Emp, DeptAttr.Name, DeptAttr.No, "������Ա");
                map.AttrsOfOneVSM.Add(new NodeDepts(), new Depts(), NodeDeptAttr.FK_Node, NodeDeptAttr.FK_Dept, DeptAttr.Name, DeptAttr.No, "���ܲ���");
                map.AttrsOfOneVSM.Add(new NodeStations(), new Stations(), NodeStationAttr.FK_Node, NodeStationAttr.FK_Station, DeptAttr.Name, DeptAttr.No, "��λ");


                RefMethod rm = new RefMethod();
                rm.Title = this.ToE("DesignSheet", "��Ʊ�"); // "��Ʊ�";
                rm.ClassMethodName = this.ToString() + ".DoMapData";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("BillBill", "����&����"); //"����&����";
                rm.ClassMethodName = this.ToString() + ".DoBill";
                rm.Icon = "/Images/FileType/doc.gif";


                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("DoFAppSet", "�����ⲿ����ӿ�"); // "�����ⲿ����ӿ�";
                rm.ClassMethodName = this.ToString() + ".DoFAppSet";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("DoAction", "�����¼��ӿ�"); // "�����¼��ӿ�";
                rm.ClassMethodName = this.ToString() + ".DoAction";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = "����ʾ"; // this.ToE("DoAction", "�����¼��ӿ�"); // "�����¼��ӿ�";
                rm.ClassMethodName = this.ToString() + ".DoShowSheets";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("DoCond", "�ڵ��������"); // "�ڵ��������";
                rm.ClassMethodName = this.ToString() + ".DoCond";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("DoListen", "��Ϣ����"); // "�ڵ��������";
                rm.ClassMethodName = this.ToString() + ".DoListen";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("DoFeatureSet", "���Լ�"); // "�����¼��ӿ�";
                rm.ClassMethodName = this.ToString() + ".DoFeatureSet";
                map.AddRefMethod(rm);

                //rm = new RefMethod();
                //rm.Title = this.ToE("DoCondFL", "������ɹ���"); // "������ɹ���";
                //rm.ClassMethodName = this.ToString() + ".DoCondFL";
                //map.AddRefMethod(rm);

                this._enMap = map;
                return this._enMap;
            }
        }
        /// <summary>
        /// ���ܴ���ǰ�Ľڵ���
        /// </summary>
        /// <returns></returns>
        public bool CanIdoIt()
        {


            return false;
        }

        public string DoListen()
        {
            PubClass.WinOpen("./../WF/Admin/Listen.aspx?CondType=0&FK_Flow=" + this.FK_Flow + "&FK_Node=" + this.NodeID + "&FK_Attr=&DirType=&ToNodeID=", "����", "cdn",
                600, 600, 100, 150);
            return null;
        }
        public string DoFeatureSet()
        {
            PubClass.WinOpen("./../WF/Admin/FeatureSetUI.aspx?CondType=0&FK_Flow=" + this.FK_Flow + "&FK_Node=" + this.NodeID + "&FK_Attr=&DirType=&ToNodeID=", "����", "cdn",
                800, 600, 20,150);
            return null;
        }
        public string DoShowSheets()
        {
            PubClass.WinOpen("./../WF/Admin/ShowSheets.aspx?CondType=0&FK_Flow=" + this.FK_Flow + "&FK_Node=" + this.NodeID + "&FK_Attr=&DirType=&ToNodeID=", "����", "cdn", 400, 400, 200, 300);
            return null;
        }
        public string DoCond()
        {
            PubClass.WinOpen("./../WF/Admin/Cond.aspx?CondType=" + (int)CondType.Flow + "&FK_Flow=" + this.FK_Flow + "&FK_MainNode=" + this.NodeID + "&FK_Node=" + this.NodeID + "&FK_Attr=&DirType=&ToNodeID=", "����", "cdn", 400, 400, 200, 300);
            return null;
        }
        
        public string DoMapData()
        {
            switch (this.HisFormType)
            {
                case FormType.FreeForm:
                    PubClass.WinOpen("./../WF/MapDef/CCForm/Frm.aspx?FK_MapData=ND" + this.NodeID + "&FK_Flow=" + this.FK_Flow, "��Ʊ�", "sheet", 1024, 768, 0, 0);
                    break;
                default:
                case FormType.FixForm:
                    PubClass.WinOpen("./../WF/MapDef/MapDef.aspx?PK=ND" + this.NodeID, "��Ʊ�", "sheet", 800, 500, 210, 300);
                    break;
            }
            return null;
        }
        public string DoAction()
        {
            PubClass.WinOpen("./../WF/Admin/Action.aspx?NodeID=" + this.NodeID + "&FK_Flow=" + this.FK_Flow, "����", "Bill", 800, 500, 200, 300);
            return null;
        }
        /// <summary>
        /// ת��
        /// </summary>
        /// <returns></returns>
        public string DoTurn()
        {
            PubClass.WinOpen("./../WF/Admin/TurnTo.aspx?FK_Node=" + this.NodeID, "�ڵ����ת����", "FrmTurn", 800, 500, 200, 300);
            return null;
        }
       
        public string DoBill()
        {
            PubClass.WinOpen("./../WF/Admin/Bill.aspx?NodeID=" + this.NodeID + "&FK_Flow=" + this.FK_Flow, "����", "Bill", 800, 500, 200, 300);
            return null;
        }
        public string DoFAppSet()
        {
            PubClass.WinOpen("./../WF/Admin/FAppSet.aspx?NodeID=" + this.NodeID + "&FK_Flow=" + this.FK_Flow, "����", "sd", 800, 500, 200, 200);
            //  PubClass.WinOpen("./../WF/Admin/FAppSet.aspx?NodeID=" + this.NodeID, 400, 500);
            return null;
        }
        #endregion

        protected override bool beforeDelete()
        {
            // ɾ�����Ľڵ㡣
            BP.Sys.MapData md = new BP.Sys.MapData();
            md.No = "ND" + this.NodeID;
            md.Delete();

            // ɾ������.
            BP.Sys.GroupFields gfs = new BP.Sys.GroupFields();
            gfs.Delete(BP.Sys.GroupFieldAttr.EnName, md.No);

            //ɾ��������ϸ��
            BP.Sys.MapDtls dtls = new BP.Sys.MapDtls(md.No);
            dtls.Delete();

            //ɾ�����
            BP.Sys.MapFrames frams = new BP.Sys.MapFrames(md.No);
            frams.Delete();

            // ɾ����ѡ
            BP.Sys.MapM2Ms m2ms = new BP.Sys.MapM2Ms(md.No);
            m2ms.Delete();

            // ɾ����չ
            BP.Sys.MapExts exts = new BP.Sys.MapExts(md.No);
            exts.Delete();

            //ɾ���ڵ����λ�Ķ�Ӧ.
            BP.DA.DBAccess.RunSQL("DELETE WF_NodeStation WHERE FK_Node=" + this.NodeID);
            BP.DA.DBAccess.RunSQL("DELETE WF_NodeEmp WHERE FK_Node=" + this.NodeID);
            BP.DA.DBAccess.RunSQL("DELETE WF_NodeDept WHERE FK_Node=" + this.NodeID);
            BP.DA.DBAccess.RunSQL("DELETE WF_NodeFlow WHERE FK_Node=" + this.NodeID);
            BP.DA.DBAccess.RunSQL("DELETE WF_FrmNode WHERE FK_Node=" + this.NodeID);

            return base.beforeDelete();
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="md"></param>
        private void AddDocAttr(BP.Sys.MapData md)
        {
            /*����ǵ������̣� */
            BP.Sys.MapAttr attr = new BP.Sys.MapAttr();

            //attr = new BP.Sys.MapAttr();
            //attr.FK_MapData = md.No;
            //attr.HisEditType = BP.En.EditType.UnDel;
            //attr.KeyOfEn = "Title";
            //attr.Name = "����";
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
            attr.Name = "�����";
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
            attr.Name = "��ע";
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
            attr.Name = "���ĵ�λ";
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
            attr.Name = "���ĵ�λ";
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
            attr.Name = "����(��)��λ";
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
            attr.Name = "����(��)��λ";
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
            attr.Name = "ӡ�Ʒ���";
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
            attr.Name = "���̶ܳ�";
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
            attr.Name = "�����̶�";
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
            attr.Name = "�����ĺ�";
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
        /// �޸�map
        /// </summary>
        public string RepareMap()
        {
            BP.Sys.MapData md = new BP.Sys.MapData();
            md.No = "ND" + this.NodeID;
            if (md.RetrieveFromDBSources() == 0)
            {
                this.CreateMap();
                return "";
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
                attr.Name = BP.Sys.Language.GetValByUserLang("AcceptTime", "����ʱ��");  //"����ʱ��";
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
                    attr.Name = BP.Sys.Language.GetValByUserLang("StartTime", "����ʱ��"); //"����ʱ��";
                else
                    attr.Name = BP.Sys.Language.GetValByUserLang("CompleteTime", "���ʱ��"); //"���ʱ��";

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
                    attr.Name = BP.Sys.Language.GetValByUserLang("Rec", "��¼��"); // "��¼��";
                else
                    attr.Name = BP.Sys.Language.GetValByUserLang("Sponsor", "������"); //"������";

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
                attr.Name = BP.Sys.Language.GetValByUserLang("NodeState", "�ڵ�״̬"); //"�ڵ�״̬";
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
                attr.Name = BP.Sys.Language.GetValByUserLang("OperDept", "����Ա����"); //"����Ա����";
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

            if (this.HisFlow.IsMD5
                && attr.IsExit(MapAttrAttr.KeyOfEn, WorkAttr.MD5, MapAttrAttr.FK_MapData, md.No) == false)
            {
                /* �����MD5��������. */
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.En.EditType.UnDel;
                attr.KeyOfEn = StartWorkAttr.MD5;
                attr.UIBindKey = attr.KeyOfEn;
                attr.Name = "MD5";
                attr.MyDataType = BP.DA.DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIIsEnable = false;
                attr.UIIsLine = false;
                attr.UIVisible = false;
                attr.MinLen = 0;
                attr.MaxLen = 40;
                attr.IDX = -100;
                attr.Insert();
            }

            if (this.NodePosType == NodePosType.Start)
            {
                //��ʼ�ڵ���Ϣ 
                if (attr.IsExit(MapAttrAttr.KeyOfEn, StartWorkAttr.WFState, MapAttrAttr.FK_MapData, md.No) == false)
                {
                    attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = md.No;
                    attr.HisEditType = BP.En.EditType.Readonly;
                    attr.KeyOfEn = StartWorkAttr.WFState;
                    attr.DefVal = "0";
                    attr.Name = BP.Sys.Language.GetValByUserLang("FlowState", "����״̬"); //"����״̬";
                    attr.MyDataType = BP.DA.DataType.AppInt;
                    attr.LGType = FieldTypeS.Normal;
                    attr.UIBindKey = attr.KeyOfEn;
                    attr.UIVisible = false;
                    attr.UIIsEnable = false;
                    attr.Insert();
                }

                if (Glo.IsEnablePRI && this.IsStartNode
                    && attr.IsExit(MapAttrAttr.KeyOfEn, StartWorkAttr.PRI, MapAttrAttr.FK_MapData, md.No) == false)
                {
                    /* ��������ȼ� */
                    attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = md.No;
                    attr.HisEditType = BP.En.EditType.UnDel;
                    attr.KeyOfEn = StartWorkAttr.PRI;
                    attr.UIBindKey = attr.KeyOfEn;
                    attr.Name = "���ȼ�";
                    attr.MyDataType = BP.DA.DataType.AppInt;
                    attr.UIContralType = UIContralType.DDL;
                    attr.LGType = FieldTypeS.Enum;
                    attr.UIIsEnable = true;
                    attr.UIIsLine = false;
                    attr.MinLen = 0;
                    attr.MaxLen = 200;
                    attr.IDX = -100;
                    attr.DefVal = "0";
                    attr.X = (float)174.76;
                    attr.Y = (float)56.19;
                    attr.Insert();
                }

                
                if (attr.IsExit(MapAttrAttr.KeyOfEn, StartWorkAttr.Title, MapAttrAttr.FK_MapData, md.No) == false)
                {
                    attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = md.No;
                    attr.HisEditType = BP.En.EditType.UnDel;
                    attr.KeyOfEn = StartWorkAttr.Title;
                    attr.Name = BP.Sys.Language.GetValByUserLang("Title", "����"); // "���̱���";
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

                //if (attr.IsExit(MapAttrAttr.KeyOfEn, "faqiren", MapAttrAttr.FK_MapData, md.No) == false)
                //{
                //    attr = new BP.Sys.MapAttr();
                //    attr.FK_MapData = md.No;
                //    attr.HisEditType = BP.En.EditType.Edit;
                //    attr.KeyOfEn = "faqiren";
                //    attr.Name = BP.Sys.Language.GetValByUserLang("faqiren", "������"); // "������";
                //    attr.MyDataType = BP.DA.DataType.AppString;
                //    attr.UIContralType = UIContralType.TB;
                //    attr.LGType = FieldTypeS.Normal;
                //    attr.UIVisible = true;
                //    attr.UIIsEnable = false;
                //    attr.UIIsLine = false;
                //    attr.MinLen = 0;
                //    attr.MaxLen = 200;
                //    attr.IDX = -100;
                //    attr.DefVal = "@WebUser.No";
                //    attr.X = (float)159.2;
                //    attr.Y = (float)102.8;
                //    attr.Insert();
                //}

                //if (attr.IsExit(MapAttrAttr.KeyOfEn, "faqishijian", MapAttrAttr.FK_MapData, md.No) == false)
                //{
                //    attr = new BP.Sys.MapAttr();
                //    attr.FK_MapData = md.No;
                //    attr.HisEditType = BP.En.EditType.Edit;
                //    attr.KeyOfEn = "faqishijian";
                //    attr.Name = BP.Sys.Language.GetValByUserLang("faqishijian", "����ʱ��"); //"����ʱ��";
                //    attr.MyDataType = BP.DA.DataType.AppDateTime;
                //    attr.UIContralType = UIContralType.TB;
                //    attr.LGType = FieldTypeS.Normal;
                //    attr.UIVisible = true;
                //    attr.UIIsEnable = false;
                //    attr.DefVal = "@RDT";
                //    attr.Tag = "1";
                //    attr.X = (float)324;
                //    attr.Y = (float)102.8;
                //    attr.Insert();
                //}


                if (attr.IsExit(MapAttrAttr.KeyOfEn, "FK_NY", MapAttrAttr.FK_MapData, md.No) == false)
                {
                    attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = md.No;
                    attr.HisEditType = BP.En.EditType.UnDel;
                    attr.KeyOfEn = "FK_NY";
                    attr.Name = BP.Sys.Language.GetValByUserLang("YearMonth", "����"); //"����";
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
                    attr.Name = BP.Sys.Language.GetValByUserLang("Num", "����"); // "����";
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
            string msg = "";
            if (this.FocusField != "")
            {
                if (attr.IsExit(MapAttrAttr.KeyOfEn, this.FocusField, MapAttrAttr.FK_MapData, md.No) == false)
                {
                    msg += "@�����ֶ� " + this.FocusField + " ���Ƿ�ɾ����.";
                    //this.FocusField = "";
                    //this.DirectUpdate();
                }
            }
            return msg;
        }
        /// <summary>
        /// ����map
        /// </summary>
        public void CreateMap()
        {
            BP.Sys.MapData md = new BP.Sys.MapData();
            md.No = "ND" + this.NodeID;
            md.Delete();
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
            attr.Name = BP.Sys.Language.GetValByUserLang("AcceptTime", "����ʱ��");  //"����ʱ��";
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
                attr.Name = BP.Sys.Language.GetValByUserLang("StartTime", "����ʱ��"); //"����ʱ��";
            else
                attr.Name = BP.Sys.Language.GetValByUserLang("CompleteTime", "���ʱ��"); //"���ʱ��";

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
                attr.Name = BP.Sys.Language.GetValByUserLang("Rec", "��¼��"); // "��¼��";
            else
                attr.Name = BP.Sys.Language.GetValByUserLang("Sponsor", "������"); //"������";

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
            attr.Name = BP.Sys.Language.GetValByUserLang("NodeState", "�ڵ�״̬"); //"�ڵ�״̬";
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
            attr.Name = BP.Sys.Language.GetValByUserLang("OperDept", "����Ա����"); //"����Ա����";
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
         //   attr.UIBindKey = "BP.Port.Depts";
            attr.UIVisible = false;
            attr.UIIsEnable = false;
            attr.MinLen = 0;
            attr.MaxLen = 20;
            attr.Insert();


            if (this.NodePosType == NodePosType.Start)
            {
                //��ʼ�ڵ���Ϣ.
                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.En.EditType.Readonly;
                attr.KeyOfEn = StartWorkAttr.WFState;
                attr.DefVal = "0";
                attr.Name = BP.Sys.Language.GetValByUserLang("FlowState", "����״̬"); //"����״̬";
                attr.MyDataType = BP.DA.DataType.AppInt;
                attr.LGType = FieldTypeS.Normal;
                attr.UIBindKey = attr.KeyOfEn;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.Insert();
               

                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.En.EditType.Edit;
             //   attr.edit
                attr.KeyOfEn = "Title";
                attr.Name = BP.Sys.Language.GetValByUserLang("Title", "����"); // "���̱���";
                attr.MyDataType = BP.DA.DataType.AppString;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = false;
                attr.UIIsEnable = false;
                attr.UIIsLine = true;
                attr.UIWidth = 251;

                attr.MinLen = 0;
                attr.MaxLen = 200;
                attr.IDX = -100;
                attr.X = (float)174.83;
                attr.Y = (float)54.4;
                attr.Insert();

                if (Glo.IsEnablePRI)
                {
                    /* ��������ȼ� */
                    attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = md.No;
                    attr.HisEditType = BP.En.EditType.UnDel;
                    attr.KeyOfEn = "PRI";
                    attr.UIBindKey = attr.KeyOfEn;
                    attr.Name = "���ȼ�";
                    attr.MyDataType = BP.DA.DataType.AppInt;
                    attr.UIContralType = UIContralType.DDL;
                    attr.LGType = FieldTypeS.Enum;
                    attr.UIIsEnable = true;
                    attr.UIIsLine = false;
                    attr.MinLen = 0;
                    attr.MaxLen = 200;
                    attr.IDX = -100;
                    attr.DefVal = "0";
                    attr.X = (float)174.76;
                    attr.Y = (float)56.19;
                    attr.Insert();
                }

                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.En.EditType.Edit;
                attr.KeyOfEn = "FaQiRen";
                attr.Name = BP.Sys.Language.GetValByUserLang("faqiren", "������"); // "������";
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
                attr.X = (float)175.56;
                attr.Y = (float)92.43;
                attr.Insert();

                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.En.EditType.Edit;
                attr.KeyOfEn = "FaQiShiJian";
                attr.Name = BP.Sys.Language.GetValByUserLang("faqishijian", "����ʱ��"); //"����ʱ��";
                attr.MyDataType = BP.DA.DataType.AppDateTime;
                attr.UIContralType = UIContralType.TB;
                attr.LGType = FieldTypeS.Normal;
                attr.UIVisible = true;
                attr.UIIsEnable = false;
                attr.DefVal = "@RDT";
                attr.Tag = "1";
                attr.X = (float)384;
                attr.Y = (float)92.43;
                attr.Insert();


                attr = new BP.Sys.MapAttr();
                attr.FK_MapData = md.No;
                attr.HisEditType = BP.En.EditType.UnDel;
                attr.KeyOfEn = "FK_NY";
                attr.Name = BP.Sys.Language.GetValByUserLang("YearMonth", "����"); //"����";
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
                attr.Name = BP.Sys.Language.GetValByUserLang("Num", "����"); // "����";
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
                //attr.Name = BP.Sys.Language.GetValByUserLang("SponsorDept", "�����˲���"); //"�����˲���";
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
                //attr.Name = "������"; // BP.Sys.Language.GetValByUserLang("CheckState", "���״̬"); // "���״̬";
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
            //    attr.Name = "������"; // BP.Sys.Language.GetValByUserLang("CheckState", "���״̬"); // "���״̬";
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
            //    attr.Name = "������Ϣ"; //BP.Sys.Language.GetValByUserLang("CheckState", "���״̬"); // "���״̬";
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
            //    attr.Name = "���״̬"; // BP.Sys.Language.GetValByUserLang("CheckState", "���״̬"); // "���״̬";
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
    /// �ڵ㼯��
    /// </summary>
    public class Nodes : EntitiesOID
    {
        #region ����
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Node();
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// �ڵ㼯��
        /// </summary>
        public Nodes()
        {
        }
        /// <summary>
        /// �ڵ㼯��.
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

        #region ��ѯ����
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
        /// ��ʼ�ڵ�
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
