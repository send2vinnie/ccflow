using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Pub
{
	/// <summary>
	/// 会计期间
	/// </summary>
	public class AP :SimpleNoNameFix
	{
		#region 实现基本的方方法		 
		/// <summary>
		/// 物理表
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Pub_AP";
			}
		}
		/// <summary>
		/// 描述
		/// </summary>
		public override string  Desc
		{
			get
			{
                return this.ToE("QJ", "会计期间");  //"会计期间";
			}
		}
		#endregion 

		#region 构造方法
		 
		public AP(){}
		 
		public AP(string _No ): base(_No){}
		
		#endregion 
	}
	/// <summary>
	/// APs
	/// </summary>
	public class APs :SimpleNoNameFixs
	{
		/// <summary>
		/// 会计期间集合
		/// </summary>
		public APs()
		{
		}
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new AP();
			}
		}
	}
}
