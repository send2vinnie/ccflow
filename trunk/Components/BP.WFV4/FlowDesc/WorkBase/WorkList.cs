using System;
using System.Collections;
using BP.DA;
using BP.En;
//using BP.ZHZS.Base;

namespace BP.WF
{	 
	/// <summary>
	/// 工作信息列表
	/// </summary>
	public class WorkListAttr:EntityEnsNameAttr
	{
		/// <summary>
		/// 工作描述
		/// </summary>
		public const string WorkDesc="WorkDesc";
	}
	/// <summary>
	/// 工作列表
	/// </summary>
	public class WorkList :EntityEnsName
	{
		#region 实现基本的方方法	
		public string WorkDesc
		{
			get
			{
				return this.GetValStringByKey(WorkListAttr.WorkDesc);
			}
			set
			{
				this.SetValByKey(WorkListAttr.WorkDesc,value);
			}
		}

		#endregion 

		#region 构造方法
		/// <summary>
		/// 工作列表
		/// </summary> 
		public WorkList(){}		 
		/// <summary>
		/// 工作列表
		/// </summary>
		/// <param name="_No"></param>
		public WorkList(string _No ): base(_No){}
		/// <summary>
		/// EnMap
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) return this._enMap;
				Map map = new Map("WF_WorkList");
				map.EnDesc="工作信息列表";
				map.AddTBStringPK(WorkListAttr.EnsEnsName,null,"工作类名称",true,true,1,100,4);
				map.AddTBString(WorkListAttr.WorkDesc,null,"描述",true,false,0,50,50);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	 /// <summary>
	 /// 工作列表s
	 /// </summary>
	public class WorkLists :En.EntitiesEnsName
	{
		/// <summary>
		/// 工作列表s
		/// </summary>
		public WorkLists(){}
		/// <summary>
		/// 工作列表s
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkList();
			}
		}
	}
}
