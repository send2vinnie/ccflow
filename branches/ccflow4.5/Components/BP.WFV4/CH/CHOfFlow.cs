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
	/// ����
	/// </summary>
	public class CHOfFlowAttr:BP.WF.GERptAttr
	{
		#region ��������
		/// <summary>
		/// ����
		/// </summary>
		public const  string FK_Dept="FK_Dept";
		/// <summary>
		/// ����״̬
		/// </summary>
		public const  string WFState="WFState";
		/// <summary>
		/// ����
		/// </summary>
		public const  string Title="Title";
		/// <summary>
		/// ����
		/// </summary>
		public const  string FK_Flow="FK_Flow";
		/// <summary>
		/// ����
		/// </summary>
		public const  string FK_NY="FK_NY";
		/// <summary>
		/// ����
		/// </summary>
		public const  string FK_AP_del="FK_AP";
		/// <summary>
		/// ����ID
		/// </summary>
		public const  string WorkID="WorkID";
        public const string FID = "FID";
		/// <summary>
		/// ���˵Ľڵ�
		/// </summary>
		public const  string NodeState="NodeState";
		/// <summary>
		/// ִ��Ա
		/// </summary>
		public const  string FK_Emp="FK_Emp";
		/// <summary>
		/// ���˵Ĺ�����λ
		/// </summary>
		public const  string FK_Station="FK_Station";		 
		/// <summary>
		/// �����ķ���ʱ��
		/// </summary>
		public const  string RDT="RDT";		 	 
		/// <summary>
		/// �������
		/// </summary>
		public const  string CDT="CDT";		 
		/// <summary>
		/// SpanDays
		/// </summary>
		public const  string SpanDays="SpanDays";
		/// <summary>
		/// Note
		/// </summary>
		public const  string Note="Note";
        /// <summary>
        /// ���ڴ�
        /// </summary>
        public const string DateLitFrom = "DateLitFrom";
        /// <summary>
        /// ���ڵ�
        /// </summary>
        public const string DateLitTo = "DateLitTo";
		#endregion
	}
	/// <summary>
	/// ����
	/// </summary>
    public class CHOfFlow : Entity
    {
        #region ��������
        /// <summary>
        /// �ڵ�ID
        /// </summary>
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(CHOfFlowAttr.WorkID);
            }
            set
            {
                this.SetValByKey(CHOfFlowAttr.WorkID, value);
            }
        }
        public Int64 FID
        {
            get
            {
                return this.GetValInt64ByKey(CHOfFlowAttr.FID);
            }
            set
            {
                this.SetValByKey(CHOfFlowAttr.FID, value);
            }
        }
        public int SpanDays
        {
            get
            {
                return this.GetValIntByKey(CHOfFlowAttr.SpanDays);
            }
            set
            {
                this.SetValByKey(CHOfFlowAttr.SpanDays, value);
            }
        }
        /// <summary>
        /// ������Ա
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(CHOfFlowAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(CHOfFlowAttr.FK_Emp, value);
            }
        }
        public string FK_NY
        {
            get
            {
                return this.GetValStringByKey(CHOfFlowAttr.FK_NY);
            }
            set
            {
                this.SetValByKey(CHOfFlowAttr.FK_NY, value);
            }
        }
        /// <summary>
        /// ������Ա
        /// </summary>
        public int WFState
        {
            get
            {
                return this.GetValIntByKey(CHOfFlowAttr.WFState);
            }
            set
            {
                this.SetValByKey(CHOfFlowAttr.WFState, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Title
        {
            get
            {
                return this.GetValStringByKey(CHOfFlowAttr.Title);
            }
            set
            {
                this.SetValByKey(CHOfFlowAttr.Title, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(CHOfFlowAttr.FK_Dept);
            }
            set
            {
                this.SetValByKey(CHOfFlowAttr.FK_Dept, value);
            }
        }
        public string DateLitFrom
        {
            get
            {
                return this.GetValStringByKey(CHOfFlowAttr.DateLitFrom);
            }
            set
            {
                this.SetValByKey(CHOfFlowAttr.DateLitFrom, value);
            }
        }
        public string DateLitTo
        {
            get
            {
                return this.GetValStringByKey(CHOfFlowAttr.DateLitTo);
            }
            set
            {
                this.SetValByKey(CHOfFlowAttr.DateLitTo, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(CHOfFlowAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(CHOfFlowAttr.FK_Flow, value);
            }
        }
        public string FK_FlowText
        {
            get
            {
                return this.GetValRefTextByKey(CHOfFlowAttr.FK_Flow);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string FK_Dept_D
        {
            get
            {
                return this.GetValStringByKey(CHOfFlowAttr.FK_Dept);
            }
            set
            {
                this.SetValByKey(CHOfFlowAttr.FK_Dept, value);
            }
        }

        /// <summary>
        /// ���ʱ��
        /// </summary>
        public string CDT
        {
            get
            {
                return this.GetValStringByKey(CHOfFlowAttr.CDT);
            }
            set
            {
                this.SetValByKey(CHOfFlowAttr.CDT, value);
            }
        }
        /// <summary>
        /// ��¼ʱ��
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(CHOfFlowAttr.RDT);
            }
            set
            {
                this.SetValByKey(CHOfFlowAttr.RDT, value);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// ����
        /// </summary>
        public CHOfFlow()
        {
        }
        public CHOfFlow(Int64 workid)
        {
            this.WorkID = workid;
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
                Map map = new Map("WF_CHOfFlow");
                map.EnDesc = this.ToE("FlowSearch", "��������"); //"���̲�ѯ";
                map.EnType = EnType.App;

                map.AddTBIntPK(CHOfFlowAttr.WorkID, 0, "����ID", true, true);
                map.AddTBInt(CHOfFlowAttr.FID, 0, "FID", true, true);
                map.AddDDLEntities(CHOfFlowAttr.FK_Flow, null, "����", new Flows(), false);
                map.AddDDLSysEnum(CHOfFlowAttr.WFState, 0, "����״̬", true, true);
                map.AddTBString(CHOfFlowAttr.Title, null, "����", true, true, 0, 400, 10);

                map.AddDDLEntities(CHOfFlowAttr.FlowStarter, null, "������", new BP.Port.Emps(), false);
                map.AddDDLEntities(CHOfFlowAttr.FK_Dept, null, "�����˲���", new Depts(), false);
                map.AddTBDateTime(CHOfFlowAttr.FlowStartRDT, "����ʱ��", true, true);

                map.AddTBString(CHOfFlowAttr.FlowEmps, null, "������", true, true, 0, 400, 10);
                map.AddDDLEntities(CHOfFlowAttr.FlowEnder, null, "������", new BP.Port.Emps(), false);
                map.AddDDLEntities(CHOfFlowAttr.FlowEnderRDT, null, "���ʱ��", new BP.Port.Emps(), false);

                map.AddTBInt(CHOfFlowAttr.FlowDaySpan, 0, "��������", true, true);

                map.AddDDLEntities(CHOfFlowAttr.FK_NY, DataType.CurrentYearMonth, "��������", new BP.Pub.NYs(), false);
                map.AddTBIntMyNum();
                map.AddSearchAttr(CHOfFlowAttr.WFState);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        public string DoSelfTest()
        {
            string info = "";
            CHOfFlows ens = new CHOfFlows();
            QueryObject qo = new QueryObject(ens);
            qo.AddWhere(CHOfFlowAttr.FK_Dept, " like ", BP.Web.WebUser.FK_Dept + "%");
            qo.addAnd();
            qo.AddWhere(CHOfFlowAttr.WFState, (int)BP.WF.WFState.Runing);
            qo.DoQuery();
            foreach (CHOfFlow en in ens)
            {
                WorkFlow wf = new WorkFlow(this.FK_Flow, this.WorkID, this.FID);
                info += "<hr><b>�ԣ�" + this.Title + "�������Ϣ���£�</b><BR>" + wf.DoSelfTest();
            }
            return info;
        }
    }
	/// <summary>
	/// ����s BP.Port.FK.CHOfFlows
	/// </summary>
	public class CHOfFlows:Entities
	{
		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CHOfFlow();
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public CHOfFlows(){}
		#endregion
	}
	
}
