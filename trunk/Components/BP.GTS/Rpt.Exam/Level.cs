using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// 等级
	/// </summary>
    public class Level : SimpleNoNameFix
    {

        #region 实现基本的方方法
        /// <summary>
        /// 物理表
        /// </summary>
        public override string PhysicsTable
        {
            get
            {
                return "GTS_Level";
            }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public override string Desc
        {
            get
            {
                return "成绩等级";
            }
        }
        #endregion

        #region 构造方法
        public Level()
        {
        }
        public Level(string _No)
            : base(_No)
        {

        }
        #endregion
    }
	/// <summary>
	/// 成绩等级
	/// </summary>
	public class Levels :SimpleNoNameFixs
	{
		/// <summary>
		/// 等级集合
		/// </summary>
		public Levels()
		{

		}
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Level();
			}
		}
	}
}
