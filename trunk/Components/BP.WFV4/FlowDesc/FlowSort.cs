using System;
using System.Collections;
using BP.DA;
using BP.En;
//using BP.ZHZS.Base;

namespace BP.WF
{
	/// <summary>
    ///  �������
	/// </summary>
	public class FlowSort :EntityNoName
	{
		#region ���췽��
		/// <summary>
		/// �������
		/// </summary>
		public FlowSort()
        {
        }
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="_No"></param>
		public FlowSort(string _No ): base(_No){}
		#endregion 

		/// <summary>
		/// �������Map
		/// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_FlowSort");
                map.EnDesc =  this.ToE("FlowSort", "�������") ;
                map.CodeStruct = "2";

                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;

                map.IsAllowRepeatNo = false;

                map.AddTBStringPK(SimpleNoNameAttr.No, null, "���", true, true, 2, 2, 2);
                map.AddTBString(SimpleNoNameAttr.Name, null, "����", true, false, 2, 50, 50);
                map.AddTBInt("IDX", 0, "IDX", false, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        protected override bool beforeDelete()
        {
            if (this.No == "00")
                throw new Exception("�����������ɾ����");
            return base.beforeDelete();
        }
	}
	/// <summary>
    /// �������
	/// </summary>
	public class FlowSorts :SimpleNoNames
	{
		/// <summary>
		/// �������s
		/// </summary>
		public FlowSorts(){}
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new FlowSort();
			}
			 
		}
		/// <summary>
		/// �������s
		/// </summary>
		/// <param name="no">ss</param>
		/// <param name="name">anme</param>
		public void AddByNoName(string no , string name)
		{
			FlowSort en = new FlowSort();
			en.No = no;
			en.Name = name;
			this.AddEntity(en);
		}
        public override int RetrieveAll()
        {
            int i= base.RetrieveAll();
            if (i == 0)
            {
                FlowSort fs = new FlowSort();
                fs.Name = this.ToE("DocCl","������");
                fs.No = "01";
                fs.Insert(); 

                fs = new FlowSort();
                fs.Name = this.ToE("BCL","�칫��");
                fs.No = "02";
                fs.Insert();
                  i = base.RetrieveAll();
            }

            return i;
        }
	}
}
