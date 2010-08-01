using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.TA
{
	 
	/// <summary>
	/// 任务类别类别
	/// </summary>
	public class TaskGroup :SimpleNoNameFix
	{
		#region 实现基本的方方法
		/// <summary>
		/// Pub_TaskGroup
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "TA_TaskGroup";
			}
		}
		/// <summary>
		/// Desc
		/// </summary>
		public override string  Desc
		{
			get
			{
				return "任务类别";
			}
		}
		#endregion

		#region 构造方法
		/// <summary>
		/// 任务类别类别
		/// </summary>
		public TaskGroup(){}	
		/// <summary>
		/// 任务类别类别
		/// </summary>
		/// <param name="_No">No</param>
		public TaskGroup(string _No ): base(_No){}
		#endregion 
	}
	/// <summary>
	/// TaskGroups
	/// </summary>
	public class TaskGroups : SimpleNoNameFixs
	{
		/// <summary>
		/// 任务类别类别		
		/// </summary>
		public TaskGroups(){}
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new TaskGroup();
			}
		}
	}
}
