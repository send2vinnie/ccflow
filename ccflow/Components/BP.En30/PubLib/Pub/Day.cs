using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Pub
{
	/// <summary>
	/// 日期
	/// </summary>
	public class Day :SimpleNoNameFix
	{
		#region 实现基本的方方法
		 
		/// <summary>
		/// 物理表
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Pub_Day";
			}
		}
		/// <summary>
		/// 描述
		/// </summary>
		public override string  Desc
		{
			get
			{
                return this.ToE("Day", "日"); // "日";
			}
		}
		#endregion 

		#region 构造方法
		 
		public Day(){}
		 
		public Day(string _No ): base(_No){}
        public override Entities GetNewEntities
        {
            get { return new Days(); }
        }
		#endregion 
	}
	/// <summary>
	/// NDs
	/// </summary>
	public class Days :SimpleNoNameFixs
	{
		/// <summary>
		/// 日期集合
		/// </summary>
		public Days()
		{
		}
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Day();
			}
		}
	}
}
