using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Sys
{
	/// <summary>
	/// 会计期间
	/// </summary>
	public class SysDTSSort :SimpleNoNameFix
	{
		#region 实现基本的方方法
		 
		/// <summary>
		/// 物理表
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Sys_DTSSort";
			}
		}
		/// <summary>
		/// 描述
		/// </summary>
		public override string  Desc
		{
			get
			{
				return "类别";
			}
		}
		#endregion 

		#region 构造方法
		 
		public SysDTSSort(){
		
		}
		 
		public SysDTSSort(string _No ): base(_No){}
		
		#endregion 
	}
	/// <summary>
	/// NDs
	/// </summary>
	public class SysDTSSorts :SimpleNoNameFixs
	{
		/// <summary>
		/// 会计期间集合
		/// </summary>
		public SysDTSSorts()
		{
		}
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new SysDTSSort();
			}
		}
	}
}
