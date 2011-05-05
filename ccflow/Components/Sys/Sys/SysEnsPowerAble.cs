using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP;
namespace BP.Sys
{
	/// <summary>
	/// abc_afs
	/// </summary>
	public class SysEnPowerAbleAttr : EntityEnsNameAttr
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
	/// SysEnPowerAbles
	/// </summary>
	public class SysEnPowerAble: EntityEnsName 
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
				return this.GetValStringByKey(SysEnPowerAbleAttr.EnEnsName ) ; 
			}
			set
			{
				this.SetValByKey(SysEnPowerAbleAttr.EnEnsName,value) ; 
			}
		}
		/// <summary>
		/// 名称
		/// </summary>
		public string Name
		{
			get
			{
				return this.GetValStringByKey(SysEnPowerAbleAttr.Name ) ; 
			}
			set
			{
				this.SetValByKey(SysEnPowerAbleAttr.Name,value) ; 
			}
		}
		/// <summary>
		/// 实体类型 0 , 应用, 1, 管理员维护, 2, 预制实体.
		/// </summary>
		public int HisEnType
		{
			get
			{
				return this.GetValIntByKey(SysEnPowerAbleAttr.EnType) ; 
			}
			set
			{
				this.SetValByKey(SysEnPowerAbleAttr.EnType,value) ; 
			}
		}
		#endregion

		#region 构造方法
		/// <summary>
		/// 系统实体
		/// </summary>
		public SysEnPowerAble()
		{
		}		
		/// <summary>
		/// 系统实体
		/// </summary>
		/// <param name="EnsEnsName">类名称</param>
		public SysEnPowerAble(string EnsEnsName )
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
				Map map = new Map("Sys_EnsPowerAble");
				map.DepositaryOfEntity=Depositary.None;
				map.EnDesc="实体信息";
				map.EnType=EnType.Sys;
				map.AddTBStringPK(SysEnPowerAbleAttr.EnsEnsName ,"EnsName",null,"实体类",true,true,0,90,10);
				map.AddTBString(SysEnPowerAbleAttr.EnEnsName,"EnName",null,"实体名称",true,false,0,50,20);
				map.AddTBString(SysEnPowerAbleAttr.Name,null,"实体名称",true,false,0,50,50);
				map.AddDDLSysEnum(SysEnPowerAbleAttr.EnType,0,"实体类型",true,false,"EnType");
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		#region 查询方法
		
		
		#endregion


	}
	
	/// <summary>
	/// 实体集合
	/// </summary>
	public class SysEnPowerAbles : EntitiesEnsName
	{		 
		public SysEnPowerAbles(){}
		/// <summary>
		/// 得到它的 Entity
		/// </summary>
		public override Entity GetNewEntity 
		{
			get
			{
				return new SysEnPowerAble();
			}
		}
		
	}
}
