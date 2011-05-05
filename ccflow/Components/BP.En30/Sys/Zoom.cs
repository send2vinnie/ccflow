using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Sys
{
	/// <summary>
	/// 会计期间
	/// </summary>
	public class Zoom :SimpleNoNameFix
	{
		#region 实现基本的方方法
		/// <summary>
		/// 物理表
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Sys_Zoom";
			}
		}
		/// <summary>
		/// 描述
		/// </summary>
        public override string Desc
        {
            get
            {
                return "图片缩放";
            }
        }
		#endregion 

		#region 构造方法
		public Zoom(){}
		public Zoom(string _No ): base(_No){}
		#endregion 
	}
	/// <summary>
	/// NDs
	/// </summary>
	public class Zooms :SimpleNoNameFixs
	{
		/// <summary>
		/// 会计期间集合
		/// </summary>
		public Zooms()
		{
		}
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Zoom();
			}
		}
	}
}
