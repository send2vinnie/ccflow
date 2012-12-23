using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.En;
using BP.Port;

namespace BP.WF
{
	/// <summary>
	/// ѡ�����������
	/// </summary>
    public class SelectAccperAttr
    {
        public const string WorkID = "WorkID";
        /// <summary>
        /// �ڵ�
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// ����Ա
        /// </summary>
        public const string FK_Emp = "FK_Emp";
    }
	/// <summary>
	/// ѡ�������
	/// �ڵ�ĵ���Ա�����������.	 
	/// ��¼�˴�һ���ڵ㵽�����Ķ���ڵ�.
	/// Ҳ��¼�˵�����ڵ�������Ľڵ�.
	/// </summary>
    public class SelectAccper : EntityMyPK
    {
        #region ��������
        /// <summary>
        ///����ID
        /// </summary>
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(SelectAccperAttr.WorkID);
            }
            set
            {
                this.SetValByKey(SelectAccperAttr.WorkID, value);
            }
        }
        /// <summary>
        ///�ڵ�
        /// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(SelectAccperAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(SelectAccperAttr.FK_Node, value);
            }
        }
        /// <summary>
        /// ����Ա
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(SelectAccperAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(SelectAccperAttr.FK_Emp, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ѡ�������
        /// </summary>
        public SelectAccper()
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

                Map map = new Map("WF_SelectAccper");
                map.EnDesc = "ѡ���������Ϣ";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.AddMyPK();

                map.AddTBInt(SelectAccperAttr.FK_Node, 0, "FK_Node", true, false);
                map.AddTBInt(SelectAccperAttr.WorkID, 0, "WorkID", true, false);
                map.AddTBString(SelectAccperAttr.FK_Emp, null, "FK_Emp", true, false, 0, 20, 10);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

    }
	/// <summary>
	/// ѡ�������
	/// </summary>
    public class SelectAccpers : EntitiesMyPK
    {
        /// <summary>
        /// ���ĵ���Ա
        /// </summary>
        public Emps HisEmps
        {
            get
            {
                Emps ens = new Emps();
                foreach (SelectAccper ns in this)
                {
                    ens.AddEntity(new Emp(ns.FK_Emp));
                }
                return ens;
            }
        }
        /// <summary>
        /// ���Ĺ����ڵ�
        /// </summary>
        public Nodes HisNodes
        {
            get
            {
                Nodes ens = new Nodes();
                foreach (SelectAccper ns in this)
                {
                    ens.AddEntity(new Node(ns.FK_Node));
                }
                return ens;
            }
        }
        /// <summary>
        /// ѡ�������
        /// </summary>
        public SelectAccpers() { }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new SelectAccper();
            }
        }
    }
}
