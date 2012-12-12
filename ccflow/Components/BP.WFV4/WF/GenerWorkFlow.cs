using System;
using System.Data;
using BP.DA;
using BP.WF;
using BP.Port ; 
using BP.En;

namespace BP.WF
{
	/// <summary>
	/// �����Ĺ���
	/// </summary>
    public class GenerWorkFlowAttr
    {
        #region ��������
        /// <summary>
        /// ��˰���
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// ������
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// ����״̬
        /// </summary>
        public const string WFState = "WFState";
        /// <summary>
        /// ����
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// ��¼��
        /// </summary>
        public const string Rec = "Rec";
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// ���ʱ��
        /// </summary>
        public const string CDT = "CDT";
        /// <summary>
        /// �÷�
        /// </summary>
        public const string Cent = "Cent";
        /// <summary>
        /// note
        /// </summary>
        public const string FlowNote = "FlowNote";
        /// <summary>
        /// ��ǰ�������Ľڵ�.
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// ��ǰ������λ
        /// </summary>
        public const string FK_Station = "FK_Station";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// ����ID
        /// </summary>
        public const string FID = "FID";
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public const string IsEnable = "IsEnable";
        /// <summary>
        /// ��������
        /// </summary>
        public const string FlowName = "FlowName";
        /// <summary>
        /// ����������
        /// </summary>
        public const string RecName = "RecName";
        /// <summary>
        /// �ڵ�����
        /// </summary>
        public const string NodeName = "NodeName";
        /// <summary>
        /// ��������
        /// </summary>
        public const string DeptName = "DeptName";
        /// <summary>
        /// �������
        /// </summary>
        public const string FK_FlowSort = "FK_FlowSort";
        /// <summary>
        /// ���ȼ�
        /// </summary>
        public const string PRI = "PRI";
        /// <summary>
        /// ����Ӧ���ʱ��
        /// </summary>
        public const string SDTOfFlow = "SDTOfFlow";
        /// <summary>
        /// �ڵ�Ӧ���ʱ��
        /// </summary>
        public const string SDTOfNode = "SDTOfNode";
        #endregion
    }
	/// <summary>
	/// �����Ĺ���
	/// </summary>
	public class GenerWorkFlow : Entity
	{	
		#region ��������
        public override string PK
        {
            get
            {
                return GenerWorkFlowAttr.WorkID;
            }
        }
		/// <summary>
		/// �������̱��
		/// </summary>
		public string  FK_Flow
		{
			get
			{
				return this.GetValStrByKey(GenerWorkFlowAttr.FK_Flow);
			}
			set
			{
				SetValByKey(GenerWorkFlowAttr.FK_Flow,value);
			}
		}
        /// <summary>
        /// ��������
        /// </summary>
        public string FlowName
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.FlowName);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.FlowName, value);
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
        /// �����
        /// </summary>
        public string FK_FlowSort
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.FK_FlowSort);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.FK_FlowSort, value);
            }
        }
		public string  FK_Dept
		{
			get
			{
				return this.GetValStrByKey(GenerWorkFlowAttr.FK_Dept);
			}
			set
			{
				SetValByKey(GenerWorkFlowAttr.FK_Dept,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string  Title
		{
			get
			{
				return this.GetValStrByKey(GenerWorkFlowAttr.Title);
			}
			set
			{
				SetValByKey(GenerWorkFlowAttr.Title,value);
			}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public string  RDT
		{
			get
			{
				return this.GetValStrByKey(GenerWorkFlowAttr.RDT);
			}
			set
			{
				SetValByKey(GenerWorkFlowAttr.RDT,value);
			}
		}
        /// <summary>
        /// �ڵ�Ӧ���ʱ��
        /// </summary>
        public string SDTOfNode
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.SDTOfNode);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.SDTOfNode, value);
            }
        }
        /// <summary>
        /// ����Ӧ���ʱ��
        /// </summary>
        public string SDTOfFlow
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.SDTOfFlow);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.SDTOfFlow, value);
            }
        }
		/// <summary>
		/// ����ID
		/// </summary>
        public Int64 WorkID
		{
			get
			{
                return this.GetValInt64ByKey(GenerWorkFlowAttr.WorkID);
			}
			set
			{
				SetValByKey(GenerWorkFlowAttr.WorkID,value);
			}
		}
        public Int64 FID
        {
            get
            {
                return this.GetValInt64ByKey(GenerWorkFlowAttr.FID);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.FID, value);
            }
        }
        public string Rec
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.Rec);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.Rec, value);
            }
        }
        public string RecName
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.RecName);
            }
            set
            {
                this.SetValByKey(GenerWorkFlowAttr.RecName, value);
            }
        }
        public string DeptName
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.DeptName);
            }
            set
            {
                this.SetValByKey(GenerWorkFlowAttr.DeptName, value);
            }
        }
        public string NodeName
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.NodeName);
            }
            set
            {
                this.SetValByKey(GenerWorkFlowAttr.NodeName, value);
            }
        }
		/// <summary>
		/// ��ǰ�������Ľڵ�
		/// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(GenerWorkFlowAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(GenerWorkFlowAttr.FK_Node, value);
            }
        }
        /// <summary>
		/// ��������״̬( 0, δ���,1 ���, 2 ǿ����ֹ 3, ɾ��״̬,) 
		/// </summary>
        public WFState WFState
        {
            get
            {
                return (WFState)this.GetValIntByKey(GenerWorkFlowAttr.WFState);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.WFState, (int)value);
            }
        }
       
        public string WFStateText
        {
            get
            {
                BP.WF.WFState ws = (WFState)this.WFState;
                switch(ws)
                {
                    case WF.WFState.Complete:
                        return "�����";
                    case WF.WFState.Runing:
                        return "������";
                    case WF.WFState.HungUp:
                        return "����";
                    default:
                        return "δ�ж�";
                }
            }
        }
        /// <summary>
        /// �����Ƿ����ã�
        /// </summary>
        public bool IsEnable_del
        {
            get
            {
                return this.GetValBooleanByKey(GenerWorkFlowAttr.IsEnable);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.IsEnable, value);
            }
        }
		#endregion

		#region ���캯��
		/// <summary>
		/// �����Ĺ�������
		/// </summary>
		public GenerWorkFlow()
		{
		}
        public GenerWorkFlow(Int64 workId)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(GenerWorkFlowAttr.WorkID, workId);
            if (qo.DoQuery() == 0)
                throw new Exception("���� GenerWorkFlow [" + workId + "]�����ڣ��������Ѿ���ɡ�");
        }
        /// <summary>
        /// ִ���޸�
        /// </summary>
        public void DoRepair()
        { 
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

                Map map = new Map("WF_GenerWorkFlow");
                map.EnDesc = "����ע���";

                map.AddTBIntPK(GenerWorkFlowAttr.WorkID, 0, "WorkID", true, true);
                map.AddTBInt(GenerWorkFlowAttr.FID, 0, "����ID", true, true);

                map.AddTBString(GenerWorkFlowAttr.FK_FlowSort, null, "�������", true, false, 0, 500, 10);
                map.AddTBString(GenerWorkFlowAttr.FK_Flow, null, "����", true, false, 0, 500, 10);
                map.AddTBString(GenerWorkFlowAttr.FlowName, null, "��������", true, false, 0, 500, 10);

                map.AddTBString(GenerWorkFlowAttr.Title, null, "����", true, false, 0, 500, 10);
                map.AddTBInt(GenerWorkFlowAttr.WFState, 0, "����״̬", true, false);

                map.AddTBString(GenerWorkFlowAttr.Rec, null, "������", true, false, 0, 500, 10);
                map.AddTBString(GenerWorkFlowAttr.RecName, null, "����������", true, false, 0, 500, 10);

                map.AddTBDateTime(GenerWorkFlowAttr.RDT, "��¼����", true, true);
                map.AddTBInt(GenerWorkFlowAttr.FK_Node, 0, "�ڵ�", true, false);
                map.AddTBString(GenerWorkFlowAttr.NodeName, null, "�ڵ�����", true, false, 0, 500, 10);

                map.AddTBString(GenerWorkFlowAttr.FK_Dept, null, "����", true, false, 0, 500, 10);
                map.AddTBString(GenerWorkFlowAttr.DeptName, null, "��������", true, false, 0, 500, 10);
                map.AddTBInt(GenerWorkFlowAttr.PRI, 1, "���ȼ�", true, true);

                map.AddTBDateTime(GenerWorkFlowAttr.SDTOfNode, "�ڵ�Ӧ���ʱ��", true, true);
                map.AddTBDateTime(GenerWorkFlowAttr.SDTOfFlow, "����Ӧ���ʱ��", true, true);

                RefMethod rm = new RefMethod();
                rm.Title = this.ToE("WorkRpt", "��������");  // "��������";
                rm.ClassMethodName = this.ToString() + ".DoRpt";
                rm.Icon = "../Images/FileType/doc.gif";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("FlowSelfTest", "�����Լ�"); // "�����Լ�";
                rm.ClassMethodName = this.ToString() + ".DoSelfTestInfo";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = "�����Լ첢�޸�";
                rm.ClassMethodName = this.ToString() + ".DoRepare";
                rm.Warning = "��ȷ��Ҫִ�д˹����� \t\n 1)����Ƕ����̣�����ͣ���ڵ�һ���ڵ��ϣ�ϵͳΪִ��ɾ������\t\n 2)����Ƿǵص�һ���ڵ㣬ϵͳ�᷵�ص��ϴη����λ�á�";
                map.AddRefMethod(rm);

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 

		#region ���ػ��෽��
		/// <summary>
		/// ɾ����,��Ҫ�ѹ������б�ҲҪɾ��.
		/// </summary>
        protected override void afterDelete()
        {
            // . clear bad worker .  
            DBAccess.RunSQLReturnTable("DELETE FROM WF_GenerWorkerlist WHERE WorkID in  ( select WorkID from WF_GenerWorkerlist WHERE WorkID not in (select WorkID from WF_GenerWorkFlow) )");

            WorkFlow wf = new WorkFlow(new Flow(this.FK_Flow), this.WorkID,this.FID);
            wf.DoDeleteWorkFlowByReal(); /* ɾ������Ĺ�����*/

            base.afterDelete();
        }
		#endregion 

		#region ִ�����
        public string DoRpt()
        {
            PubClass.WinOpen("WFRpt.aspx?WorkID=" + this.WorkID + "&FID="+this.FID+"&FK_Flow="+this.FK_Flow);
            return null;
        }
		/// <summary>
		/// ִ���޸�
		/// </summary>
		/// <returns></returns>
        public string DoRepare()
        {
            if (this.DoSelfTestInfo() == "û�з����쳣��")
                return "û�з����쳣��";

            string sql = "SELECT FK_Node FROM WF_GenerWorkerlist WHERE WORKID='" + this.WorkID + "' ORDER BY FK_Node desc";
            DataTable dt = DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
            {
                /*����ǿ�ʼ�����ڵ㣬��ɾ������*/
                WorkFlow wf = new WorkFlow(new Flow(this.FK_Flow), this.WorkID, this.FID );
                wf.DoDeleteWorkFlowByReal();
                return "����������Ϊ������ʧ�ܱ�ϵͳɾ����";
            }

            int FK_Node = int.Parse(dt.Rows[0][0].ToString());

            Node nd = new Node(FK_Node);
            if (nd.IsStartNode)
            {
                /*����ǿ�ʼ�����ڵ㣬��ɾ������*/
                WorkFlow wf = new WorkFlow(new Flow(this.FK_Flow), this.WorkID, this.FID);
                wf.DoDeleteWorkFlowByReal();
                return "����������Ϊ������ʧ�ܱ�ϵͳɾ����";
            }

            this.FK_Node = nd.NodeID;
            this.NodeName = nd.Name;
            this.Update();

            string str = "";
            GenerWorkerLists wls = new GenerWorkerLists();
            wls.Retrieve(GenerWorkerListAttr.FK_Node, FK_Node, GenerWorkerListAttr.WorkID, this.WorkID);
            foreach (GenerWorkerList wl in wls)
            {
                str += wl.FK_Emp + wl.FK_EmpText + ",";
            }

            return "����������Ϊ[" + nd.Name + "]��������ʧ�ܱ��ع�����ǰλ�ã���ת��[" + str + "]�����޸��ɹ���";
        }
		public string DoSelfTestInfo()
		{
            GenerWorkerLists wls = new GenerWorkerLists(this.WorkID, this.FK_Flow);

			#region  �鿴һ�µ�ǰ�Ľڵ��Ƿ�ʼ�����ڵ㡣
			Node nd = new Node(this.FK_Node);
			if (nd.IsStartNode)
			{
				/* �ж��Ƿ����˻صĽڵ� */
				Work wk = nd.HisWork;
				wk.OID = this.WorkID;
				wk.Retrieve();

				if (wk.NodeState!=NodeState.Back)
				{
					return "��ǰ�Ĺ����ڵ� �����ڿ�ʼ�����ڵ��� �������˻����� ��Ӧ�ó��ֵ����������ɾ����ǰ�Ĺ����ڵ㡣 ";
				}
			}
			#endregion


			#region  �鿴һ���Ƿ��е�ǰ�Ĺ����ڵ���Ϣ��
			bool isHave=false;
            foreach (GenerWorkerList wl in wls)
			{
				if (wl.FK_Node==this.FK_Node)
					isHave=true;
			}

			if (isHave==false)
			{
				/*  */
				return "�Ѿ������ڵ�ǰ�Ĺ����ڵ���Ϣ����ɴ����̵�ԭ�������û�в����ϵͳ�쳣������ɾ�������̻��߽���ϵͳ�Զ��޸�����";
			}
			#endregion

			return "û�з����쳣��";
		}
		#endregion
	}
	/// <summary>
	/// �����Ĺ���s
	/// </summary>
	public class GenerWorkFlows : Entities
	{
		/// <summary>
		/// ���ݹ�������,������ԱID ��ѯ��������ǰ�������Ĺ���.
		/// </summary>
		/// <param name="flowNo">���̱��</param>
		/// <param name="empId">������ԱID</param>
		/// <returns></returns>
		public static DataTable QuByFlowAndEmp(string flowNo, int empId)
		{
			string sql="SELECT a.WorkID FROM WF_GenerWorkFlow a, WF_GenerWorkerlist b WHERE a.WorkID=b.WorkID   AND b.FK_Node=a.FK_Node  AND b.FK_Emp='"+empId.ToString()+"' AND a.FK_Flow='"+flowNo+"'";
			return DBAccess.RunSQLReturnTable(sql);
		}

		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{			 
				return new GenerWorkFlow();
			}
		}
		/// <summary>
		/// �����������̼���
		/// </summary>
		public GenerWorkFlows(){}
		#endregion
	}
	
}
