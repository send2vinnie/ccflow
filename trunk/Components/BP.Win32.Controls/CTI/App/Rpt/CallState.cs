using System;
using System.Collections;
using BP.DA;
using BP.En.Base;


namespace BP.CTI.App
{
	/// <summary>
	/// 银行
	/// </summary>
	public class CallState :SimpleNoNameFix
	{
		#region 实现基本的方法
		/// <summary>
		/// PhysicsTable
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "CTI_CallState";
			}
		}
		/// <summary>
		/// Desc
		/// </summary>
		public override string  Desc
		{
			get
			{
				return "呼出状态";
			}
		}
		#endregion 

		#region 构造方法
		 
		public CallState(){}
		 
		public CallState(string _No ): base(_No){}
		#endregion 
	}
	 
	/// <summary>
	/// 银行s
	/// </summary>
	public class CallStates :SimpleNoNameFixs
	{
		#region 构造
		/// <summary>
		/// 银行s
		/// </summary>
		public CallStates(){}
		/// <summary>
		/// 银行
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CallState();
			}
		}
		#endregion
	}
}
