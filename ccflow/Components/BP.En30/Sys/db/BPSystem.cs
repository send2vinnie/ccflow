using System;
using BP.En;
using BP.DA;

namespace BP.Sys
{
	
	public class BPSystemAttr : EntityNoNameAttr
	{	 
		/// <summary>
		/// 标记
		/// </summary>
		public const string  IsOk="IsOk";
		/// <summary>
		/// 连接
		/// </summary>
		public const string  URL="URL";
		/// <summary>
		/// 版本
		/// </summary>
		public const string  Ver="Ver";
		/// <summary>
		/// 备注
		/// </summary>
		public const string  Note="Note";
		/// <summary>
		/// 发布日期
		/// </summary>
		public const string  IssueDate="IssueDate";
	}
	public class BPSystem : EntityNoName
	{
		/// <summary>
		/// 是否使用
		/// </summary>
		public bool IsOk
		{
			get
			{
				return  this.GetValBooleanByKey(BPSystemAttr.IsOk);
			}
		}
		public string URL
		{
			get
			{
				return  this.GetValStringByKey(BPSystemAttr.URL);
			}
		}	
		public string IssueDate
		{
			get
			{
				return  this.GetValStringByKey(BPSystemAttr.IssueDate);
			}
		}
		public string Ver
		{
			get
			{
				return  this.GetValStringByKey(BPSystemAttr.Ver);
			}
		}
		public string Note
		{
			get
			{
				return  this.GetValStringByKey(BPSystemAttr.Note);
			}
		}	
		/// <summary>
		/// 
		/// </summary>
		public BPSystem()
		{
		}
		public BPSystem(string no) :base(no){}
		/// <summary>
		/// 重写基类的方法
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) return this._enMap;
				Map map = new Map("Sys_Tem");
				map.EnDesc="系统";
				map.AddTBStringPK(BPSystemAttr.No,null,"编号",true,false,1,10,10);
				map.AddTBString(BPSystemAttr.Name,null,"系统名称",true,false,1,50,10);
				map.AddTBString(BPSystemAttr.URL,null,"连接",true,false,1,100,20);
				map.AddTBInt(BPSystemAttr.IsOk,0,"是否使用",true,false);
				map.AddTBString(BPSystemAttr.Ver,"1.0","版本",true,false,1,10,10);
				map.AddTBDate(BPSystemAttr.IssueDate,"发布日期",true,false);
				//map.AddTBString(BPSystemAttr.Note,"备注","备注",true,false,0,500,20);
				this._enMap=map;
				return this._enMap;
			}
		}
	}
	public class BPSystems : EntitiesNoName
	{
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new BPSystem();
			}
		}		
	}
	 
	
	
}
