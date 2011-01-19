using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.En.Base;

namespace BP.En
{
	/// <summary>
	/// EmpExtAttr
	/// </summary>
	public class EmpExtAttr
	{ 
		/// <summary>
		/// 值
		/// </summary>
		public const string Val="Val";
		/// <summary>
		/// 字段
		/// </summary>
		public const string Field="Field";
		/// <summary>
		/// 用户
		/// </summary>
		public const string FK_Emp="FK_Emp";
	}
	/// <summary>
	/// sdfsad
	/// </summary>
	public class EmpExt : Entity
	{
		#region  属性
		/// <summary>
		///  值
		/// </summary>
		public string  Val
		{
			get
			{
				return this.GetValStringByKey(EmpExtAttr.Val);
			}
			set
			{
				SetValByKey(EmpExtAttr.Val,value);
			}
		}
		public string  Field
		{
			get
			{
				return this.GetValStringByKey(EmpExtAttr.Field);
			}
			set
			{
				SetValByKey(EmpExtAttr.Field,value);
			}
		}
		public string  FK_Emp
		{
			get
			{
				return this.GetValStringByKey(EmpExtAttr.FK_Emp);
			}
			set
			{
				SetValByKey(EmpExtAttr.FK_Emp,value);
			}
		}
		#endregion 
		 
		#region 构造函数
		/// <summary>
		/// 用户扩展信息
		/// </summary>
		public EmpExt()
		{
		}
		/// <summary>
		/// 用户扩展信息
		/// </summary>
		/// <param name="fk_emp">人员</param>
		/// <param name="field">字段</param>
        public EmpExt(string fk_emp, string field)
        {
            this.FK_Emp = fk_emp;
            this.Field = field;
            try
            {
                this.Retrieve();
            }
            catch
            {
                this.Insert();
            }
        }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fk_emp"></param>
		/// <param name="field"></param>
		/// <param name="IsNullVal"></param>
		public EmpExt(string fk_emp,string field, string IsNullVal)
		{ 
			this.FK_Emp=fk_emp;
			this.Field=field;
			try
			{
				this.Retrieve();
			}
			catch
			{
				this.Val=IsNullVal;
				this.Insert();
			}
		}
		/// <summary>
		/// Map
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				
				Map map = new Map("Pub_EmpExt");
				map.EnDesc="用户扩展信息";
				map.EnType = EnType.View;
				map.DepositaryOfMap=Depositary.Application;
				map.DepositaryOfEntity=Depositary.Application;
                map.AddMyPK();

				map.AddTBString(EmpExtAttr.FK_Emp,null,"用户",true,false,4,20,100);
				map.AddTBString(EmpExtAttr.Field,null,"字段",true,false,1,20,100);
				map.AddTBStringDoc(EmpExtAttr.Val,null,"名称",true,false);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// 用户扩展信息s
	/// </summary>
	public class EmpExts: Entities
	{
		/// <summary>
		/// 用户扩展信息
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new EmpExt();
			}
		}
		/// <summary>
		/// 用户扩展信息
		/// </summary>
		public EmpExts()
		{
		}
	}
}
 