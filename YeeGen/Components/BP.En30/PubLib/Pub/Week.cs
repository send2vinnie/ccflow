using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Pub
{
	/// <summary>
	/// 周
	/// </summary>
    public class Week : SimpleNoNameFix
    {
        #region 实现基本的方方法
        /// <summary>
        /// 物理表
        /// </summary>
        public override string PhysicsTable
        {
            get
            {
                return "Pub_Week";
            }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public override string Desc
        {
            get
            {
                return this.ToE("Week", "周"); // "日期";
            }
        }
        #endregion

        #region 构造方法
        public Week() { }
        public Week(string _No) : base(_No) { }
        #endregion
    }
	/// <summary>
    /// 周s
	/// </summary>
    public class Weeks : SimpleNoNameFixs
    {
        /// <summary>
        /// 周集合
        /// </summary>
        public Weeks()
        {
        }
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Week();
            }
        }
    }
}
