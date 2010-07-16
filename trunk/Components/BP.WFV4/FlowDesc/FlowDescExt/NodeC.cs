using System;
using System.Data;
using BP.DA;
using BP.En;
using System.Collections;
using BP.Port;

namespace BP.WF.Ext
{

    /// <summary>
    /// 这里存放每个节点的信息.	 
    /// </summary>
    public class NodeC : Entity, IDTS
    {
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
        public override string PK
        {
            get
            {
                return "NodeID";
            }
        }
        #region 初试化全局的 Nod
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
        #endregion

        #region 构造函数
        /// <summary>
        /// 节点
        /// </summary>
        public NodeC() { }
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
                map.EnDesc = this.ToE("Node", "节点");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBIntPK(NodeAttr.NodeID, 0, this.ToE("NodeID", "节点ID"), true, true);
                map.AddTBString(NodeAttr.Name, null, this.ToE("Name", "名称"), true, false, 0, 100, 10);
                map.AddTBInt(NodeAttr.Step, (int)NodeWorkType.Work, this.ToE("FlowStep", "流程步骤"), true, false);
                map.AddBoolean(NodeAttr.IsTask, true, "允许分配工作否?", true, true);
                map.AddBoolean(NodeAttr.IsSelectEmp, false, "可否选择接受人?", true, true);
                map.AddBoolean(NodeAttr.IsCanReturn, false, "是否可以退回", true, true);
                map.AddBoolean(NodeAttr.IsCanCC, true, "是否可以抄送", true, true);
                map.AddDDLSysEnum(NodeAttr.SignType, 0, "审核模式", true, true, NodeAttr.SignType, "@0=单签@1=汇签");
                map.AddDDLSysEnum(NodeAttr.FLRole, 0, "分流规则-对分流有效", true, true, NodeAttr.FLRole, "@0=按接受人@1=按部门@2=按岗位");
                map.AddDDLSysEnum(NodeAttr.FJOpen, 0, "附件权限", true, true, NodeAttr.FJOpen, "@0=关闭附件@1=操作员@2=工作ID@3=流程ID");

                map.AddDDLSysEnum(NodeAttr.FormType, 0, "表单类型", true, true, NodeAttr.FormType, "@0=系统表单@1=自定义表单");
                map.AddTBString(NodeAttr.FormUrl, "http://", "自定义表单URL", true, false, 0, 500, 10);


                map.AddTBString(NodeAttr.DoWhat, null, "完成后处理SQL", true, false, 0, 500, 10);
                Attr attr = map.GetAttrByKey(NodeAttr.DoWhat);
                attr.UIIsLine = true;


                // 考核设置。
                map.AddTBInt(NodeAttr.WarningDays, 0, this.ToE(NodeAttr.WarningDays, "警告期限(0不警告)"), true, false); // "警告期限(0不警告)"
                map.AddTBInt(NodeAttr.DeductDays, 1, this.ToE(NodeAttr.DeductDays, "限期(天)"), true, false); //"限期(天)"
                map.AddTBFloat(NodeAttr.DeductCent, 2, this.ToE(NodeAttr.DeductCent, "扣分(每延期1天扣)"), true, false); //"扣分(每延期1天扣)"

                map.AddTBFloat(NodeAttr.MaxDeductCent, 0, this.ToE(NodeAttr.MaxDeductCent, "最高扣分"), true, false);   //"最高扣分"
                map.AddTBFloat(NodeAttr.SwinkCent, float.Parse("0.1"), this.ToE("SwinkCent", "工作得分"), true, false); //"工作得分"


                map.AttrsOfOneVSM.Add(new NodeEmps(), new Emps(), NodeEmpAttr.FK_Node, EmpDeptAttr.FK_Emp, DeptAttr.Name, DeptAttr.No, "接受人员");
                map.AttrsOfOneVSM.Add(new NodeDepts(), new Depts(), NodeDeptAttr.FK_Node, NodeDeptAttr.FK_Dept, DeptAttr.Name, DeptAttr.No, "接受部门");
                map.AttrsOfOneVSM.Add(new NodeStations(), new Stations(), NodeStationAttr.FK_Node, NodeStationAttr.FK_Station, DeptAttr.Name, DeptAttr.No, "岗位");

                RefMethod rm = new RefMethod();
                rm.Title = this.ToE("DesignSheet", "设计表单"); // "设计表单";
                rm.ClassMethodName = this.ToString() + ".DoMapData";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("BillBook", "单据&文书"); //"单据&文书";
                rm.ClassMethodName = this.ToString() + ".DoBook";
                rm.Icon = "/Images/Btn/Word.gif";
                // rm.Target = "_self";
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

                this._enMap = map;
                return this._enMap;
            }
        }
        public string DoShowSheets()
        {
            BP.WF.Node nd = new BP.WF.Node(this.NodeID);
            return nd.DoShowSheets();
        }
        public string DoCond()
        {
            BP.WF.Node nd = new BP.WF.Node(this.NodeID);
            return nd.DoCond();
        }
        public string DoCondFL()
        {
            BP.WF.Node nd = new BP.WF.Node(this.NodeID);
            return nd.DoCondFL();
        }
        public string DoMapData()
        {
            BP.WF.Node nd = new BP.WF.Node(this.NodeID);
            return nd.DoMapData();
        }
        public string DoAction()
        {
            BP.WF.Node nd = new BP.WF.Node(this.NodeID);
            return nd.DoAction();
        }
        public string DoBook()
        {
            BP.WF.Node nd = new BP.WF.Node(this.NodeID);
            return nd.DoBook();
        }
        public string DoFAppSet()
        {
            BP.WF.Node nd = new BP.WF.Node(this.NodeID);
            return nd.DoFAppSet();
        }
        #endregion
    }
    /// <summary>
    /// 节点集合
    /// </summary>
    public class NodeCs : EntitiesOID
    {
        #region 构造方法
        /// <summary>
        /// 节点集合
        /// </summary>
        public NodeCs()
        {
        }
        #endregion

        public override Entity GetNewEntity
        {
            get { return new NodeC(); }
        }
    }
}
