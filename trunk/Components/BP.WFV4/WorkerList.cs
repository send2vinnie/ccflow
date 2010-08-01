
using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port;
using BP.Port;
using BP.En;


namespace BP.WF
{
    /// <summary>
    /// 工作人员集合
    /// </summary>
    public class WorkerListAttr
    {
        #region 基本属性
        /// <summary>
        /// 工作节点
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// 处罚单据编号
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// 流程
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// 征管软件是不是罚款
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        /// <summary>
        /// 使用的岗位
        /// </summary>
        public const string UseStation_del = "UseStation";
        /// <summary>
        /// 使用的部门
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// 应该完成时间
        /// </summary>
        public const string SDT = "SDT";
        /// <summary>
        /// 警告日期
        /// </summary>
        public const string DTOfWarning = "DTOfWarning";
        public const string RDT = "RDT";
        /// <summary>
        /// 
        /// </summary>
        public const string IsEnable = "IsEnable";
        /// <summary>
        /// WarningDays
        /// </summary>
        public const string WarningDays = "WarningDays";
        /// <summary>
        /// 是否自动分配
        /// </summary>
        //public const  string IsAutoGener="IsAutoGener";
        /// <summary>
        /// 产生时间
        /// </summary>
        //public const  string GenerDateTime="GenerDateTime";
        /// <summary>
        /// IsPass
        /// </summary>
        public const string IsPass = "IsPass";
        /// <summary>
        /// 流程ID
        /// </summary>
        public const string FID = "FID";
        #endregion
    }
    /// <summary>
    /// 工作者列表
    /// </summary>
    public class WorkerList : Entity
    {
        #region 基本属性
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnable
        {
            get
            {
                return this.GetValBooleanByKey(WorkerListAttr.IsEnable);
            }
            set
            {
                this.SetValByKey(WorkerListAttr.IsEnable, value);
            }
        }
        /// <summary>
        /// 是否通过(对审核的会签节点有效)
        /// </summary>
        public bool IsPass
        {
            get
            {
                return this.GetValBooleanByKey(WorkerListAttr.IsPass);
            }
            set
            {
                this.SetValByKey(WorkerListAttr.IsPass, value);
            }
        }
        /// <summary>
        /// WorkID
        /// </summary>
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(WorkerListAttr.WorkID);
            }
            set
            {
                this.SetValByKey(WorkerListAttr.WorkID, value);
            }
        }
        /// <summary>
        /// Node
        /// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(WorkerListAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(WorkerListAttr.FK_Node, value);
            }
        }
        public string FK_NodeT
        {
            get
            {
                return this.GetValRefTextByKey(WorkerListAttr.FK_Node);
            }
        }
        /// <summary>
        /// 流程ID
        /// </summary>
        public Int64 FID
        {
            get
            {
                return this.GetValInt64ByKey(WorkerListAttr.FID);
            }
            set
            {
                this.SetValByKey(WorkerListAttr.FID, value);
            }
        }
        /// <summary>
        /// 警告天
        /// </summary>
        public int WarningDays
        {
            get
            {
                return this.GetValIntByKey(WorkerListAttr.WarningDays);
            }
            set
            {
                this.SetValByKey(WorkerListAttr.WarningDays, value);
            }
        }
        /// <summary>
        /// 工作人员
        /// </summary>
        public Emp HisEmp
        {
            get
            {
                return new Emp(this.FK_Emp);
            }
        }

        public string RDT
        {
            get
            {
                return this.GetValStringByKey(WorkerListAttr.RDT);
            }
            set
            {
                this.SetValByKey(WorkerListAttr.RDT, value);
            }
        }


        /// <summary>
        /// 应该完成日期
        /// </summary>
        public string SDT
        {
            get
            {
                return this.GetValStringByKey(WorkerListAttr.SDT);
            }
            set
            {
                this.SetValByKey(WorkerListAttr.SDT, value);
            }
        }
        public string DTOfWarning
        {
            get
            {
                return this.GetValStringByKey(WorkerListAttr.DTOfWarning);
            }
            set
            {
                this.SetValByKey(WorkerListAttr.DTOfWarning, value);
            }
        }
        /// <summary>
        /// 工作人员
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(WorkerListAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(WorkerListAttr.FK_Emp, value);
            }
        }

        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(WorkerListAttr.FK_Dept);
            }
            set
            {
                this.SetValByKey(WorkerListAttr.FK_Dept, value);
            }
        }

        public string FK_EmpText
        {
            get
            {
                return this.GetValRefTextByKey(WorkerListAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(WorkerListAttr.FK_Emp + "Text", value);
            }
        }
        /// <summary>
        /// FK_Flow
        /// </summary>		 
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(WorkerListAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(WorkerListAttr.FK_Flow, value);
            }
        }
        /// <summary>
        /// 节点
        /// </summary>
        public Node HisNode
        {
            get
            {
                return new Node(this.FK_Node);
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// WorkerList
        /// </summary>
        public WorkerList()
        {

        }
        public WorkerList(int workid, int fk_node, string fk_emp)
        {
            if (this.WorkID == 0)
                return;

            this.WorkID = workid;
            this.FK_Node = fk_node;
            this.FK_Emp = fk_emp;
            this.Retrieve();
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

                Map map = new Map("WF_GenerWorkerList");
                map.EnDesc = "工作者";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBIntPK(WorkerListAttr.WorkID, 0, "工作ID", true, true);
                map.AddDDLEntitiesPK(WorkerListAttr.FK_Emp, null, "工作者", new Emps(), false);

                map.AddDDLEntitiesPK(WorkerListAttr.FK_Node, null, "节点", new NodeExts(), false);

              //map.AddTBIntPK(WorkerListAttr.FK_Node, 0, "节点", true, false);

                map.AddTBInt(WorkerListAttr.FID, 0, "流程ID", true, false);
                map.AddTBString(WorkerListAttr.FK_Flow, null, "流程", true, false, 0, 100, 100);
                map.AddTBString(WorkerListAttr.FK_Dept, null, "使用部门", true, false, 0, 100, 100);

                map.AddTBDate(WorkerListAttr.SDT, "应完成日期", false, false);
                map.AddTBDate(WorkerListAttr.DTOfWarning, "警告日期", false, false);

                map.AddTBInt(WorkerListAttr.WarningDays, 0, "预警天", true, false);
                map.AddTBDate(WorkerListAttr.RDT, "RDT", false, false);
                map.AddBoolean(WorkerListAttr.IsEnable, true, "是否可用", true, true);

                //对会签节点有效
                map.AddTBInt(WorkerListAttr.IsPass, 0, "是否通过(对会签节点有效)", false, false);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

    }
    /// <summary>
    /// 工作人员集合
    /// </summary>
    public class WorkerLists : Entities
    {
        #region 方法
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new WorkerList();
            }
        }
        /// <summary>
        /// WorkerList
        /// </summary>
        public WorkerLists() { }
        public WorkerLists(Int64 workId, int nodeId)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(WorkerListAttr.WorkID, workId);
            qo.addAnd();
            qo.AddWhere(WorkerListAttr.FK_Node, nodeId);
            qo.DoQuery();
            return;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="workId"></param>
        /// <param name="nodeId"></param>
        /// <param name="isWithEmpExts">是否要包含记忆中的人员</param>
        public WorkerLists(int workId, int nodeId, bool isWithEmpExts)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(WorkerListAttr.WorkID, workId);
            qo.addAnd();
            qo.AddWhere(WorkerListAttr.FK_Node, nodeId);
            int i = qo.DoQuery();

            if (isWithEmpExts == false)
                return;

            if (i == 0)
                throw new Exception("@系统错误，工作人员丢失请与管理员联系。");

            RememberMe rm = new RememberMe();
            rm.FK_Emp = Web.WebUser.No;
            rm.FK_Node = nodeId;

            if (rm.RetrieveFromDBSources() == 0)
                return;


            WorkerList wl = (WorkerList)this[0];
            string[] emps = rm.Emps.Split('@');
            foreach (string emp in emps)
            {
                if (emp==null || emp=="")
                    continue;

                if (this.GetCountByKey(WorkerListAttr.FK_Emp, emp) >= 1)
                    continue;

                WorkerList mywl = new WorkerList();
                mywl.Copy(wl);
                mywl.IsEnable = false;
                mywl.FK_Emp = emp;
                try
                {
                    mywl.Insert();
                }
                catch
                {
                    mywl.Update();
                    continue;
                }
                this.AddEntity(mywl);
            }
            return;
        }
        /// <summary>
        /// 工作者
        /// </summary>
        /// <param name="workId">工作者ID</param>
        /// <param name="flowNo">流程编号</param>
        public WorkerLists(Int64 workId, string flowNo)
        {
            if (workId == 0)
                return;

            Flow fl = new Flow(flowNo);

            QueryObject qo = new QueryObject(this);
            qo.AddWhere(WorkerListAttr.WorkID, workId);
            qo.addAnd();
            qo.AddWhere(WorkerListAttr.FK_Flow, flowNo);
            qo.DoQuery();

        }

        /// <summary>
        /// 检查用户的权限
        /// </summary>
        /// <param name="workId">工作ID</param>
        /// <param name="flowNo">流程编号</param>
        /// <param name="empId">工作人员信息</param>
        /// <returns></returns>
        public static bool CheckUserPower(Int64 workId, string empId)
        {

            string sql = "SELECT count( a.Workid) FROM dbo.WF_GenerWorkFlow a , dbo.WF_GenerWorkerList b WHERE a.workid=b.workid and a.fk_node=b.fk_node and b.fk_emp='" + empId + "' and b.IsEnable=1 and a.workid=" + workId;
            if (DBAccess.RunSQLReturnValInt(sql) == 0)
                return false;
            return true;

            //// 找到当前的工作节点。
            //GenerWorkFlow gwf = new GenerWorkFlow(workId, flowNo);
            //WorkerLists wls = new WorkerLists();
            //QueryObject qo = new QueryObject(wls);
            //qo.AddWhere(WorkerListAttr.WorkID, workId);
            //qo.addAnd();
            //qo.AddWhere(WorkerListAttr.FK_Emp, empId);
            //qo.addAnd();
            //qo.AddWhere(WorkerListAttr.FK_Node, gwf.FK_Node);
            //if (qo.DoQuery() == 0)
            //    return false;
            //return true;
        }
        #endregion
    }
}
