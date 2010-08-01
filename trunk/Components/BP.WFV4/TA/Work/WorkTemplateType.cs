using System;
using System.Collections;
using BP.DA;
using BP.En;


namespace BP.TA
{
	/// <summary>
	/// 类型
	/// </summary>
    public class WorkTemplateType : SimpleNoNameFix
    {
        #region 实现基本的方法
        /// <summary>
        /// PhysicsTable
        /// </summary>
        public override string PhysicsTable
        {
            get
            {
                return "TA_WorkTemplateType";
            }
        }
        /// <summary>
        /// Desc
        /// </summary>
        public override string Desc
        {
            get
            {
                return "类型";
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 类型
        /// </summary>
        public WorkTemplateType() { }

        /// <summary>
        /// 类型
        /// </summary>
        /// <param name="_No">编号</param>
        public WorkTemplateType(string _No) : base(_No) { }
        #endregion
    }
	 
	/// <summary>
	/// 类型s
	/// </summary>
	public class WorkTemplateTypes :SimpleNoNameFixs
	{
		#region 构造
		/// <summary>
		/// 类型s
		/// </summary>
		public WorkTemplateTypes(){}
		/// <summary>
		/// 类型
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkTemplateType();
			}
		}
		#endregion
	}
}
