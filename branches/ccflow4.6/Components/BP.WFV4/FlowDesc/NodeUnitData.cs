using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.En;
using System.Collections;
using BP.Port;

namespace BP.WF
{
    /// <summary>
    /// �ڵ�����
    /// </summary>
    public class NodeUnitDataAttr:BP.En.EntityMyPKAttr
    {
        #region ��������
        /// <summary>
        /// �ڵ�
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// FID
        /// </summary>
        public const string FID = "FID";
        /// <summary>
        /// WorkID
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// ��¼����
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// �������
        /// </summary>
        public const string CDT = "CDT";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_NY = "FK_NY";
        /// <summary>
        /// ��¼��
        /// </summary>
        public const string Rec = "Rec";
        /// <summary>
        /// ��¼��Ա
        /// </summary>
        public const string Emps = "Emps";
        /// <summary>
        /// �ڵ�״̬
        /// </summary>
        public const string NodeState = "NodeState";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// ����
        /// </summary>
        public const string MyNum = "MyNum";
        #endregion
    }
    /// <summary>
    /// ������ÿ���ڵ����ݻ��ܵ���Ϣ.	 
    /// </summary>
    public class NodeUnitData : EntityMyPK
    {
        #region ��������
        /// <summary>
        /// FID
        /// </summary>
        public int FID
        {
            get
            {
                return this.GetValIntByKey(NodeUnitDataAttr.FID);
            }
            set
            {
                this.SetValByKey(NodeUnitDataAttr.FID, value);
            }
        }
        /// <summary>
        /// WorkID
        /// </summary>
        public int WorkID
        {
            get
            {
                return this.GetValIntByKey(NodeUnitDataAttr.WorkID);
            }
            set
            {
                this.SetValByKey(NodeUnitDataAttr.WorkID, value);
            }
        }
        /// <summary>
        /// FK_Node
        /// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(NodeUnitDataAttr.FK_Node);
            }
            set
            {
                SetValByKey(NodeUnitDataAttr.FK_Node, value);
            }
        }
        public string FK_NodeT
        {
            get
            {
                return this.GetValRefTextByKey(NodeUnitDataAttr.FK_Node);
            }
        }
        #endregion

        #region ���캯��
        public string FlowNo = null;
        /// <summary>
        /// �ڵ����ݻ���
        /// </summary>
        public NodeUnitData() 
        {

        }
        public NodeUnitData(string fk_flow)
        {
            this.FlowNo = fk_flow;
        }
        /// <summary>
        /// �ڵ����ݻ���
        /// </summary>
        /// <param name="_oid">�ڵ����ݻ���ID</param>	
        public NodeUnitData(string fk_flow,string mypk)
        {
            this.FlowNo = fk_flow;
            this.MyPK = mypk;
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
                {
                    this._enMap.PhysicsTable = "V" + this.FlowNo;
                    return this._enMap;
                }

                Map map = new Map("ViewFlow");
                map.EnDesc = "�ڵ����ݻ���";
                map.EnType = En.EnType.View;
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddMyPK();
                map.AddTBInt(NodeUnitDataAttr.WorkID, 0, "WorkID", true, true);
                map.AddDDLEntities(NodeUnitDataAttr.FK_Node, null, "�ڵ�", new NodeExts(), false);
                map.AddDDLSysEnum(NodeUnitDataAttr.NodeState, 0, "�ڵ�״̬", false, false);
                map.AddDDLEntities(NodeUnitDataAttr.Rec, null, "����Ա", new WF.Port.WFEmps(), false);


                map.AddTBDateTime(NodeUnitDataAttr.RDT, null, "��¼����", false, true);
                map.AddTBDateTime(NodeUnitDataAttr.CDT, null, "�������", false, true);

                map.AddDDLEntities(NodeUnitDataAttr.FK_NY, null, "����", new BP.Pub.NYs(), false);

                map.AddTBString(NodeUnitDataAttr.Emps, null, "����Աs", true, false, 0, 400, 10);

                map.AddTBMyNum();
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// �ڵ����ݻ��ܼ���
    /// </summary>
    public class NodeUnitDatas : Entities
    {
        #region ����
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new NodeUnitData();
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// �ڵ����ݻ��ܼ���
        /// </summary>
        public NodeUnitDatas()
        {
        }
        /// <summary>
        /// �ڵ����ݻ��ܼ���.
        /// </summary>
        /// <param name="FlowNo"></param>
        public NodeUnitDatas(string FK_Node)
        {
            this.Retrieve(NodeUnitDataAttr.FK_Node, FK_Node);
        }
        #endregion
    }
}
