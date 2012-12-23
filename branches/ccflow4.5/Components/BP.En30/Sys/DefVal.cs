using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Sys
{
	 /// <summary>
	 /// ����
	 /// </summary>
	public class DefValAttr
	{
		/// <summary>
		/// ����Key
		/// </summary>
		public const string AttrKey="AttrKey";
        /// <summary>
        /// ����
        /// </summary>
        public const string AttrDesc = "AttrDesc";
		/// <summary>
		/// ������ԱID
		/// </summary>
		public const string FK_Emp="FK_Emp";
		/// <summary>
		/// Ĭ��ֵ
		/// </summary>
		public const string Val="Val";
		/// <summary>
		/// EnsName
		/// </summary>
		public const string EnsName="EnsName";
        /// <summary>
        /// ����
        /// </summary>
        public const string EnsDesc = "EnsDesc";
	}
	/// <summary>
	/// Ĭ��ֵ
	/// </summary>
	public class DefVal: EntityOID
	{
		#region ��������
        /// <summary>
        /// ����
        /// </summary>
		public string EnsName
		{
			get
			{
				return this.GetValStringByKey(DefValAttr.EnsName ) ; 
			}
			set
			{
				this.SetValByKey(DefValAttr.EnsName,value) ; 
			}
		}
        /// <summary>
        /// ����
        /// </summary>
        public string EnsDesc
        {
            get
            {
                return this.GetValStringByKey(DefValAttr.EnsDesc);
            }
            set
            {
                this.SetValByKey(DefValAttr.EnsDesc, value);
            }
        }
		/// <summary>
		/// Ĭ��ֵ
		/// </summary>
		public string Val
		{
			get
			{
				return this.GetValStringByKey(DefValAttr.Val ) ; 
			}
			set
			{
				this.SetValByKey(DefValAttr.Val,value) ; 
			}
		}
		/// <summary>
		/// ����ԱID
		/// </summary>
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(DefValAttr.FK_Emp ) ; 
			}
			set
			{
				this.SetValByKey(DefValAttr.FK_Emp,value) ; 
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string AttrKey
		{
			get
			{
				return this.GetValStringByKey(DefValAttr.AttrKey ) ; 
			}
			set
			{
				this.SetValByKey(DefValAttr.AttrKey,value) ; 
			}
		}
        /// <summary>
        /// ��������
        /// </summary>
        public string AttrDesc
        {
            get
            {
                return this.GetValStringByKey(DefValAttr.AttrDesc);
            }
            set
            {
                this.SetValByKey(DefValAttr.AttrDesc, value);
            }
        }
		#endregion

		#region ���췽��
       
		/// <summary>
		/// Ĭ��ֵ
		/// </summary>
		public DefVal()
		{
		}
		/// <summary>
		/// map
		/// </summary>
		public override Map EnMap
		{
            get
            {
                if (this._enMap != null) return this._enMap;
                Map map = new Map("Sys_DefVal");
                map.EnType = EnType.Sys;
                map.EnDesc = "Ĭ��ֵ";
                map.DepositaryOfEntity = Depositary.None;

                map.AddTBIntPKOID();
                map.AddTBString(DefValAttr.EnsName, null, "������", false, true, 0, 100, 10);
                map.AddTBString(DefValAttr.EnsDesc, null, "������", false, true, 0, 100, 10);

                map.AddTBString(DefValAttr.AttrKey, null, "����", false, true, 0, 100, 10);
                map.AddTBString(DefValAttr.AttrDesc, null, "��������", false, true, 0, 100, 10);

                map.AddTBString(DefValAttr.FK_Emp, null, "��Ա", false, true, 0, 100, 10);
                map.AddTBString(DefValAttr.Val, null, "ֵ", true, false, 0, 1000, 10);
                this._enMap = map;
                return this._enMap;
            }
		}
		#endregion 
	}
	/// <summary>
	/// Ĭ��ֵs
	/// </summary>
	public class DefVals : EntitiesOID
	{
		/// <summary>
		/// ��ѯ.
		/// </summary>
		/// <param name="EnsName"></param>
		/// <param name="key"></param>
		/// <param name="FK_Emp"></param>
        public void Retrieve(string EnsName, string key, int FK_Emp)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(DefValAttr.AttrKey, key);
            qo.addAnd();
            qo.AddWhere(DefValAttr.EnsName, EnsName);
            qo.addAnd();
            qo.AddWhere(DefValAttr.FK_Emp, FK_Emp);
            qo.DoQuery();
        }
		/// <summary>
		/// ��ѯ
		/// </summary>
		/// <param name="EnsName"></param>
		/// <param name="key"></param>
        public void Retrieve(string EnsName, string key)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(DefValAttr.AttrKey, key);
            qo.addAnd();
            qo.AddWhere(DefValAttr.EnsName, EnsName);
            qo.DoQuery();
        }
		/// <summary>
		/// Ĭ��ֵs
		/// </summary>
		public DefVals()
		{
		}
		/// <summary>
		/// �õ����� Entity
		/// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new DefVal();
            }
        }
	}
}
