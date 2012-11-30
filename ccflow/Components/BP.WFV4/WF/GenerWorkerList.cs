
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
    /// ������Ա����
    /// </summary>
    public class WorkerListAttr
    {
        #region ��������
        /// <summary>
        /// �����ڵ�
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// �������ݱ��
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// ��������ǲ��Ƿ���
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        /// <summary>
        /// ʹ�õĸ�λ
        /// </summary>
        public const string UseStation_del = "UseStation";
        /// <summary>
        /// ʹ�õĲ���
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// Ӧ�����ʱ��
        /// </summary>
        public const string SDT = "SDT";
        /// <summary>
        /// ��������
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
        /// �Ƿ��Զ�����
        /// </summary>
        //public const  string IsAutoGener="IsAutoGener";
        /// <summary>
        /// ����ʱ��
        /// </summary>
        //public const  string GenerDateTime="GenerDateTime";
        /// <summary>
        /// IsPass
        /// </summary>
        public const string IsPass = "IsPass";
        /// <summary>
        /// ����ID
        /// </summary>
        public const string FID = "FID";
        /// <summary>
        /// ��Ա����
        /// </summary>
        public const string FK_EmpText = "FK_EmpText";
        /// <summary>
        /// �ڵ�����
        /// </summary>
        public const string FK_NodeText = "FK_NodeText";
        /// <summary>
        /// ������
        /// </summary>
        public const string Sender = "Sender";
        /// <summary>
        /// ˭ִ����?
        /// </summary>
        public const string WhoExeIt = "WhoExeIt";
        /// <summary>
        /// ���ȼ�
        /// </summary>
        public const string PRI = "PRI";
        /// <summary>
        /// �Ƿ��ȡ��
        /// </summary>
        public const string IsRead = "IsRead";
        #endregion
    }
    /// <summary>
    /// �������б�
    /// </summary>
    public class WorkerList : Entity
    {
        #region ��������
        /// <summary>
        /// ˭��ִ����
        /// </summary>
        public int WhoExeIt
        {
            get
            {
                return this.GetValIntByKey(WorkerListAttr.WhoExeIt);
            }
            set
            {
                SetValByKey(WorkerListAttr.WhoExeIt, value);
            }
        }
        /// <summary>
        /// ���ȼ�
        /// </summary>
        public int PRI
        {
            get
            {
                return this.GetValIntByKey(GenerWorkFlowAttr.PRI);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.PRI, value);
            }
        }
        /// <summary>
        /// WorkID
        /// </summary>
        public override string PK
        {
            get
            {
                return "WorkID,FK_Emp,FK_Node";
            }
        }
        /// <summary>
        /// �Ƿ����
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
        /// �Ƿ�ͨ��(����˵Ļ�ǩ�ڵ���Ч)
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
        /// <summary>
        /// ������
        /// </summary>
        public string Sender
        {
            get
            {
                return this.GetValStrByKey(WorkerListAttr.Sender);
            }
            set
            {
                this.SetValByKey(WorkerListAttr.Sender, value);
            }
        }
        /// <summary>
        /// �ڵ�����
        /// </summary>
        public string FK_NodeText
        {
            get
            {
                return this.GetValStrByKey(WorkerListAttr.FK_NodeText);
            }
            set
            {
                this.SetValByKey(WorkerListAttr.FK_NodeText, value);
            }
        }
        
        /// <summary>
        /// ����ID
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
        /// ������
        /// </summary>
        public float WarningDays
        {
            get
            {
                return this.GetValFloatByKey(WorkerListAttr.WarningDays);
            }
            set
            {
                this.SetValByKey(WorkerListAttr.WarningDays, value);
            }
        }
        /// <summary>
        /// ������Ա
        /// </summary>
        public Emp HisEmp
        {
            get
            {
                return new Emp(this.FK_Emp);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
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
        /// Ӧ���������
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
        /// ��Ա
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
        /// <summary>
        /// ��Ա����
        /// </summary>
        public string FK_EmpText
        {
            get
            {
                return this.GetValStrByKey(WorkerListAttr.FK_EmpText);
            }
            set
            {
                this.SetValByKey(WorkerListAttr.FK_EmpText, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
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
        public string FK_DeptT
        {
            get
            {
                BP.Port.Dept d = new BP.Port.Dept(this.FK_Dept);
                return d.Name;
                //return this.GetValStringByKey(WorkerListAttr.FK_Dept);
            }
        }
        /// <summary>
        /// ���̱��
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
        #endregion

        #region ���캯��
         
        /// <summary>
        /// ������
        /// </summary>
        public WorkerList()
        {
        }
        public WorkerList(Int64 workid, int FK_Node, string fk_emp)
        {
            if (this.WorkID == 0)
                return;

            this.WorkID = workid;
            this.FK_Node = FK_Node;
            this.FK_Emp = fk_emp;
            this.Retrieve();
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

                Map map = new Map("WF_GenerWorkerlist");
                map.EnDesc = "������";
                 

                map.AddTBIntPK(WorkerListAttr.WorkID, 0, "����ID", true, true);
                map.AddTBStringPK(WorkerListAttr.FK_Emp, null, "��Ա", true, false, 0, 50, 100);
                map.AddTBIntPK(WorkerListAttr.FK_Node, 0, "�ڵ�ID", true, false);

                map.AddTBString(WorkerListAttr.FK_EmpText, null, "��Ա����", true, false, 0, 100, 100);

                map.AddTBString(WorkerListAttr.FK_NodeText, null, "�ڵ�����", true, false, 0, 100, 100);

                map.AddTBInt(WorkerListAttr.FID, 0, "����ID", true, false);
                map.AddTBString(WorkerListAttr.FK_Flow, null, "����", true, false, 0, 100, 100);
                map.AddTBString(WorkerListAttr.FK_Dept, null, "ʹ�ò���", true, false, 0, 100, 100);

                map.AddTBDateTime(WorkerListAttr.SDT, "Ӧ�������", false, false);
                map.AddTBDateTime(WorkerListAttr.DTOfWarning, "��������", false, false);

                map.AddTBFloat(WorkerListAttr.WarningDays, 0, "Ԥ����", true, false);
                map.AddTBDateTime(WorkerListAttr.RDT, "RDT", false, false);

                map.AddBoolean(WorkerListAttr.IsEnable, true, "�Ƿ����", true, true);

                //  add for lijian 2012-11-30
                map.AddTBInt(WorkerListAttr.IsRead, 0, "�Ƿ��ȡ", true, true);


                //�Ի�ǩ�ڵ���Ч
                map.AddTBInt(WorkerListAttr.IsPass, 0, "�Ƿ�ͨ��(�Ժ����ڵ���Ч)", false, false);

                // ˭ִ������
                map.AddTBInt(WorkerListAttr.WhoExeIt, 0, "˭ִ����", false, false);

                //������. 2011-11-12 Ϊ����û����ӡ�
                map.AddTBString(WorkerListAttr.Sender, null, "������", true, false, 0, 100, 100);

                //���ȼ���2012-06-15 Ϊ�ൺ�û����ӡ�
                map.AddTBInt(GenerWorkFlowAttr.PRI, 1, "���ȼ�", true, true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
        protected override bool beforeInsert()
        {
            if (this.FID != 0)
            {
                if (this.FID == this.WorkID)
                    this.FID = 0;
            }
            this.Sender = BP.Web.WebUser.No + "," + BP.Web.WebUser.Name;
            return base.beforeInsert();
        }
    }
    /// <summary>
    /// ������Ա����
    /// </summary>
    public class WorkerLists : Entities
    {
        #region ����
        /// <summary>
        /// �õ����� Entity 
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
        public WorkerLists(Int64 workId, int nodeId,string fk_emp)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(WorkerListAttr.WorkID, workId);
            qo.addAnd();
            qo.AddWhere(WorkerListAttr.FK_Node, nodeId);
            qo.addAnd();
            qo.AddWhere(WorkerListAttr.FK_Emp, fk_emp);
            qo.DoQuery();
            return;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="workId"></param>
        /// <param name="nodeId"></param>
        /// <param name="isWithEmpExts">�Ƿ�Ҫ���������е���Ա</param>
        public WorkerLists(Int64 workId, int nodeId, bool isWithEmpExts)
        {
            QueryObject qo = new QueryObject(this);
            qo.addLeftBracket();
            qo.AddWhere(WorkerListAttr.WorkID, workId);
            qo.addOr();
            qo.AddWhere(WorkerListAttr.FID, workId);
            qo.addRightBracket();
            qo.addAnd();
            qo.AddWhere(WorkerListAttr.FK_Node, nodeId);
            int i = qo.DoQuery();

            if (isWithEmpExts == false)
                return;

            if (i == 0)
                throw new Exception("@ϵͳ���󣬹�����Ա��ʧ�������Ա��ϵ��NodeID=" + nodeId + " WorkID=" + workId);

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
                WF.Port.WFEmp myEmp = new Port.WFEmp(emp);
                mywl.FK_EmpText = myEmp.Name;
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
        /// ������
        /// </summary>
        /// <param name="workId">������ID</param>
        /// <param name="flowNo">���̱��</param>
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
        #endregion
    }
}
