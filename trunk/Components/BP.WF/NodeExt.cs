
using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port; 
using BP.Port; 
using BP.En;

 
namespace BP.WF
{
	/// <summary>
	/// �ڵ�
	/// </summary>
    public class NodeExtAttr : EntityNoNameAttr
    {
        /// <summary>
        /// ��Ա
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        /// <summary>
        /// �ڵ�
        /// </summary>
        public const string PerCentFK = "PerCentFK";
        public const string CentOfTo = "CentOfTo";
        /// <summary>
        /// ʡ��
        /// </summary>
        public const string FK_XZCL = "FK_XZCL";
    }
	/// <summary>
	/// �ڵ�
	/// </summary>
    public class NodeExt : EntityNoName
    {
        #region ��������

        #endregion

        #region ���캯��
        /// <summary>
        /// �ڵ�
        /// </summary>
        public NodeExt() { }
        /// <summary>
        /// strubg
        /// </summary>
        public NodeExt(string no)
        {
            this.No = no;
            this.Retrieve();
        }
        public NodeExt(int no)
        {
            this.No = no.ToString();
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

                Map map = new Map("WF_NodeExt");
                map.EnDesc = "�ڵ�";
                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBStringPK(SimpleNoNameFixAttr.No, null, "���", true, false, 4, 4, 100);
                map.AddTBString(SimpleNoNameFixAttr.Name, null, "����", true, false, 2, 60, 500);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// �ڵ�s
	/// </summary>
	public class NodeExts : EntitiesNoName
	{	
		#region ���췽��
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{			 
				return new NodeExt();
			}
		}
		/// <summary>
		/// �ڵ�s 
		/// </summary>
		public NodeExts(){}
		#endregion
	}
	
}
