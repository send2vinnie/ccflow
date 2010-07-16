using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.En.Base;

namespace BP.Sys
{ 
	/// <summary>
	/// attr
	/// </summary>
	public class SysUIEnsRefFuncAttr :DictAttr
	{ 	 
		/// <summary>
		/// 相关联的路径
		/// </summary>
		public const string Url="Url"; 
		/// <summary>
		/// 打开方式
		/// </summary>
		public const string Target="Target"; 
		/// <summary>
		/// 宽度
		/// </summary>
		public const string Width="Width"; 
		/// <summary>
		/// 高度
		/// </summary>
		public const string Height="Height"; 

		/// <summary>
		/// 是否为Dtl
		/// </summary>
		public const string IsForDtl="IsForDtl"; 

		/// <summary>
		/// icon
		/// </summary>
		public const string Icon="Icon";
		/// <summary>
		/// 提示信息
		/// </summary>
		public const string ToolTip="ToolTip";

	}
	/// <summary>
	/// 关联功能
	/// </summary>
	public class SysUIEnsRefFunc : Dict
	{
		#region  基本属性		
		/// <summary>
		/// 相关联的路径
		/// </summary>
		public string Url
		{
			get
			{
				return  this.GetValStringByKey(SysUIEnsRefFuncAttr.Url);
				
			}
			set
			{
				SetValByKey(SysUIEnsRefFuncAttr.Url,value);
			}
		}
		public string Icon
		{
			get
			{
				return this.GetValStringByKey(SysUIEnsRefFuncAttr.Icon);
			}
			set
			{
				SetValByKey(SysUIEnsRefFuncAttr.Icon,value);
			}
		}
		public string ToolTip
		{
			get
			{
				return this.GetValStringByKey(SysUIEnsRefFuncAttr.ToolTip);
			}
			set
			{
				SetValByKey(SysUIEnsRefFuncAttr.ToolTip,value);
			}
		}
		/// <summary>
		/// 打开方式
		/// </summary>
		public string Target
		{
			get
			{
				return this.GetValStringByKey(SysUIEnsRefFuncAttr.Target);
			}
			set
			{
				SetValByKey(SysUIEnsRefFuncAttr.Target,value);
			}
		}
		/// <summary>
		/// 宽度
		/// </summary>
		public int Width
		{
			get
			{
				return this.GetValIntByKey(SysUIEnsRefFuncAttr.Width);
			}
			set
			{
				SetValByKey(SysUIEnsRefFuncAttr.Width,value);
			}
		}
		/// <summary>
		/// 高度
		/// </summary>
		public int Height
		{
			get
			{
				return this.GetValIntByKey(SysUIEnsRefFuncAttr.Height);
			}
			set
			{
				SetValByKey(SysUIEnsRefFuncAttr.Height,value);
			}
		}
		/// <summary>
		/// IsForDtl
		/// </summary>
		public bool IsForDtl
		{
			get
			{
				return this.GetValBooleanByKey(SysUIEnsRefFuncAttr.IsForDtl);
			}
			set
			{
				SetValByKey(SysUIEnsRefFuncAttr.IsForDtl,value);
			}
		}
		#endregion 
		 
		#region 构造函数
		/// <summary>
		/// 相关功能
		/// </summary>
		public SysUIEnsRefFunc(){}	
		/// <summary>
		/// 相关功能
		/// </summary>
		/// <param name="_oid">oid</param>
		public SysUIEnsRefFunc(int _oid ): base(_oid) {}
		
		/// <summary>
		/// Map
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) return this._enMap;
				Map map = new Map("Sys_UIEnsRefFunc");
				map.DepositaryOfEntity=Depositary.Application;
				map.EnType=EnType.Sys;
				map.EnDesc="相关功能";
				map.AddTBIntPKOID();
				map.AddTBString(SysUIEnsRefFuncAttr.No,null,"类名称",true,false,1,100,20);
				map.AddTBString(SysUIEnsRefFuncAttr.Name,null,"显示名称",true,false,1,100,20);
				map.AddTBString(SysUIEnsRefFuncAttr.Url,null,"连接",true,false,1,200,20);
				map.AddTBString(SysUIEnsRefFuncAttr.Icon,"/images/Default.gif","图标",true,false,1,100,20);
				map.AddTBString(SysUIEnsRefFuncAttr.ToolTip,null,"提示信息",true,false,1,100,20);
				map.AddTBString(SysUIEnsRefFuncAttr.Target,"WinOpen","打开方式",true,false,1,100,20);
				map.AddTBInt(SysUIEnsRefFuncAttr.Height,0,"高度",true,false);
				map.AddTBInt(SysUIEnsRefFuncAttr.Width,0,"宽度",true,false);
				map.AddTBInt(SysUIEnsRefFuncAttr.IsForDtl,0,"IsForDtl",true,false);

				this._enMap=map;
				return this._enMap;
			}
		}
		

		#endregion 

	}
	/// <summary>
	/// 相关功能集合
	/// </summary>
	public class SysUIEnsRefFuncs: Dicts
	{		 
		/// <summary>
		/// 相关功能集合
		/// </summary>
		public SysUIEnsRefFuncs(){} 
		/// <summary>
		/// 相关功能集合，根据编号，查找。
		/// </summary>
		/// <param name="No"></param>
		public SysUIEnsRefFuncs(string No)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere("No",No);			
			qo.DoQuery();
		 
		}
		/// <summary>
		/// 实体
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new SysUIEnsRefFunc();
			}
		}
	
	
	}	
}
 