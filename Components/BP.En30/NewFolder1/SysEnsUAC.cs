using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
//using BP.ZHZS.Base;

namespace BP.Sys
{
	/// <summary>
	/// 访问控制
	/// </summary>
	public class SysEnUACAttr  //EntityClassNameAttr
	{	
		/// <summary>
		/// 工作人员ID
		/// </summary>
		public const string FK_Emp="FK_Emp";
		/// <summary>
		/// 实体类名称
		/// </summary>
		public const string FK_Ens="FK_Ens";
		/// <summary>
		///是否新增
		/// </summary>
		public const string IsInsert="IsInsert"; 
		/// <summary>
		/// 是否删除
		/// </summary>
		public const string IsDelete="IsDelete";
		/// <summary>
		/// 是否更新
		/// </summary>
		public const string IsUpdate="IsUpdate";
		/// <summary>
		/// 是否视图
		/// </summary>
		public const string IsView="IsView";
		//public const string IsPrint="IsPrint";
		/// <summary>
		/// 是否附件
		/// </summary>
		public const string IsAdjunct="IsAdjunct";
		//public const string IsExport="IsExport";
		//public const string IsHelp="IsHelp";

	}
	/// <summary>
	/// 访问控制
	/// </summary> 
	public class SysEnsUAC:Entity //EntityClassName 
	{
		#region 基本属性
		/// <summary>
		/// 工作人员ID
		/// </summary>
		public  string  FK_Emp
		{
			get
			{
				return this.GetValStringByKey(SysEnUACAttr.FK_Emp);
			}
			set
			{
				this.SetValByKey(SysEnUACAttr.FK_Emp,value);
			}
		} 
		/// <summary>
		/// 类名称
		/// </summary>
		public  string  FK_Ens
		{
			get
			{
				return this.GetValStringByKey(SysEnUACAttr.FK_Ens);
			}
			set
			{
				this.SetValByKey(SysEnUACAttr.FK_Ens,value);
			}
		}
		/// <summary>
		/// 是否新增
		/// </summary>
		public  bool  IsInsert
		{
			get
			{
				return this.GetValBooleanByKey(SysEnUACAttr.IsInsert);
			}
			set
			{
				this.SetValByKey(SysEnUACAttr.IsInsert,value);
			}
		}
		/// <summary>
		/// 是否删除
		/// </summary>
		public  bool  IsDelete
		{
			get
			{
				return this.GetValBooleanByKey(SysEnUACAttr.IsDelete);
			}
			set
			{
				this.SetValByKey(SysEnUACAttr.IsDelete,value);
			}
		}
		
		/// <summary>
		/// 是否更新
		/// </summary>
		public  bool  IsUpdate
		{
			get
			{
				return this.GetValBooleanByKey(SysEnUACAttr.IsUpdate);
			}
			set
			{
				this.SetValByKey(SysEnUACAttr.IsUpdate,value);
			}
		}
		/// <summary>
		/// 是否查看
		/// </summary>
		public  bool  IsView
		{
			get
			{
				return this.GetValBooleanByKey(SysEnUACAttr.IsView);
			}
			set
			{
				this.SetValByKey(SysEnUACAttr.IsView,value);
			}
		} 
		/// <summary>
		/// 是否附件
		/// </summary>
		public  bool  IsAdjunct
		{
			get
			{
				return this.GetValBooleanByKey(SysEnUACAttr.IsAdjunct);
			}
			set
			{
				this.SetValByKey(SysEnUACAttr.IsAdjunct,value);
			}
		} 
		#endregion 

		#region 检查方法
//		/// <summary>
//		/// CheckByEmpID
//		/// </summary>
//		/// <param name="empId"></param>
//		public  static void   CheckByEmpID(int empId)
//		{
//			SysEns ens  =  new SysEns();
//			ens.RetrieveAll();
//			foreach(SysEn en in ens)
//			{
//				SysEnsUAC ua = new SysEnsUAC(en.EnsClassName,empId);
//			}
//		}
		/// <summary>
		/// CheckAll
		/// </summary>
		public  static void  CheckAll()
		{
//			Emps  ens  =  new Emps();
//			ens.RetrieveAll();
//			foreach(Emp en in ens)
//			{
//				SysEnsUAC.CheckByEmpID(en.OID) ; 				
//			}
		}
		#endregion 

		#region 构造方法		 
		/// <summary>
		/// 访问控制
		/// </summary>
		public SysEnsUAC()
		{
		}		 
		/// <summary>
		/// 访问控制
		/// </summary>
		/// <param name="classesName">集合类名称</param>
		/// <param name="empid">工作人员ID</param>
		public SysEnsUAC(Entities ens, string empid)
		{
			Entity myen = ens.GetNewEntity;
			if (myen.EnMap.EnType==EnType.View || myen.EnMap.EnType==EnType.ThirdPart )
			{
				/*  视图  ThirdPart */
			}

			if (empid=="admin")
			{				
				return ;
			}

			if (SystemConfig.SysNo=="TP"   )
			{
//				/* 如果是 TP 项目，或则是信息管理员 */
			 
				return;
			}
			if (  myen.EnMap.EnType == EnType.Dtl )
			{
				 
				return ;

			}
			
		
			if ( (myen.EnMap.EnType == EnType.PowerAble
				|| myen.EnMap.EnType != EnType.Dtl)==false )
			{
				/* 如果不是纳入权限管理的实体， 一律不让更新修改，察看。 */
				return ;
			}

			/* 其他的是纳入权限管理的。*/
			string classesName = ens.ToString();


			//SysEnPowerAble en = new SysEnPowerAble( classesName );
			this.FK_Ens=classesName;
			this.EmpID=empid;
			if (this.IsExits==false)
			{
				 
				this.Insert();
			}
			else
			{
				this.Retrieve();
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
				Map map = new Map("Sys_EnsUAC");
				map.EnDesc="权限信息";
				map.EnType=EnType.Admin; //实体类型，admin 系统管理员表，PowerAble 权限管理表,也是用户表,你要想把它加入权限管理里面请在这里设置。。
				
			 
				map.EnType=EnType.Admin; //实体类型。


				map.AddDDLEntitiesPK(SysEnUACAttr.EmpID,SysEnUACAttr.EmpID, 0 , DataType.AppInt ,"操作员",new Emps(),"OID","Name",false);
				map.AddDDLEntitiesPK(SysEnUACAttr.FK_Ens,null, DataType.AppString,"操作对象",new SysEnPowerAbles(),SysEnAttr.EnsClassName,SysEnAttr.Name,false);
				map.AddBoolean(SysEnUACAttr.IsView,true,"查看",true,true);
				map.AddBoolean(SysEnUACAttr.IsInsert,false,"新增",true,true);
				map.AddBoolean(SysEnUACAttr.IsUpdate,false,"更新",true,true);
				map.AddBoolean(SysEnUACAttr.IsDelete,false,"删除",true,true);
				map.AddBoolean(SysEnUACAttr.IsAdjunct,false,"附件",true,true);

				//map.AddBoolean(SysEnUACAttr.IsExport,false,"数据导出",true,true);
				//map.AddBoolean(SysEnUACAttr.IsPrint,false,"打印",true,true);
				//map.AddBoolean(SysEnUACAttr.IsHelp,false,"是否是帮助页面",true,true);

				map.AddSearchAttr(SysEnUACAttr.EmpID);
				map.AddSearchAttr(SysEnUACAttr.FK_Ens);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// 访问控制
	/// </summary> 
	public class SysEnsUACs : Entities//EntitiesNoName
	{
		#region 刷新
		/// <summary>
		/// 刷新
		/// </summary>
		public static void RefreshUAC()
		{			
		}
		#endregion		 
		
		#region 构造函数
		/// <summary>
		/// 关于实体访问的构造
		/// </summary>
		public SysEnsUACs(){}
		/// <summary>
		/// New entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new SysEnsUAC();
			}
		}
		#endregion
	
	}
}
