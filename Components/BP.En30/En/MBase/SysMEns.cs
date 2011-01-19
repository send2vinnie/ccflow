using System;
using System.Collections;
using BP.DA;
using BP.En;
//using BP.ZHZS.Base;
using BP;
namespace BP.Sys
{
	/// <summary>
	/// sss
	/// </summary>
	public class SysIEnAttr : EntityEnsNameAttr
	{
		/// <summary>
		/// 名称
		/// </summary>
		public const string Name="Name";
		/// <summary>
		/// 实体名称
		/// </summary>
		public const string EnEnsName="EnEnsName";
		/// <summary>
		/// 实体类型
		/// </summary> 
		public const string EnType="EnType";	
	}
	 
	/// <summary>
	/// SysIEns
	/// </summary>
	public class SysIEn: EntityEnsName 
	{
		#region 基本属性
		public Entity En
		{
			get
			{
			  return ClassFactory.GetEn(this.EnEnsName);
			}
		}
		public Entities Ens
		{
			get
			{
				return ClassFactory.GetEns(this.EnsEnsName );
			}
		}
		/// <summary>
		/// 实体名称
		/// </summary>
		public string EnEnsName
		{
			get
			{
				return this.GetValStringByKey(SysIEnAttr.EnEnsName ) ; 
			}
			set
			{
				this.SetValByKey(SysIEnAttr.EnEnsName,value) ; 
			}
		}
		/// <summary>
		/// 名称
		/// </summary>
		public string Name
		{
			get
			{
				return this.GetValStringByKey(SysIEnAttr.Name ) ; 
			}
			set
			{
				this.SetValByKey(SysIEnAttr.Name,value) ; 
			}
		}
		/// <summary>
		/// 实体类型 0 , 应用, 1, 管理员维护, 2, 预制实体.
		/// </summary>
		public int HisEnType
		{
			get
			{
				return this.GetValIntByKey(SysIEnAttr.EnType) ; 
			}
			set
			{
				this.SetValByKey(SysIEnAttr.EnType,value) ; 
			}
		}
		#endregion

		#region 构造方法
		public SysIEn(){}
		/// <summary>
		/// EnsEnsName
		/// </summary>
		/// <param name="EnsEnsName">EnsEnsName</param>
		public SysIEn(string EnsEnsName )
		{
			this.EnsEnsName= EnsEnsName;
			if (this.IsExits==false)
			{
				Entities ens =ClassFactory.GetEns(this.EnsEnsName) ;
				Entity en = ens.GetNewEntity;
				this.Name = en.EnDesc;
				this.EnEnsName = en.ToString();
				this.Insert();
			}
			else
			{
				this.Retrieve();
			}

		}
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				Map map = new Map("Sys_Ens");
				map.EnDesc="实体信息";
				map.EnType=EnType.Sys;

				map.DepositaryOfEntity=Depositary.Application;
				map.DepositaryOfMap=Depositary.Application;


				map.AddTBStringPK(SysIEnAttr.EnsEnsName ,"EnsName",null,"实体类",true,true,1,200,4);
				map.AddTBString(SysIEnAttr.EnEnsName,"EnName",null,"实体名称",true,false,1,200,50);
				map.AddTBString(SysIEnAttr.Name,null,"实体名称",true,false,0,200,50);
				map.AddDDLSysEnum(SysIEnAttr.EnType,0,"实体类型",true,false);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		#region 查询方法
		public void RetrieveByEnEnsName(string EnEnsName)
		{
			QueryObject qo  = new QueryObject(this);
			qo.AddWhere(SysIEnAttr.EnEnsName,EnEnsName) ; 
			if (qo.DoQuery()==0)
				throw new Exception("@请刷新记录.");
		}
		#endregion


	}
	
	/// <summary>
	/// 实体集合
	/// </summary>
	public class SysIEns : EntitiesEnsName
	{		 
		public SysIEns(){}
		/// <summary>
		/// 得到它的 Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new SysIEn();
			}
		}
		
	}
}
