using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.SSO
{
	/// <summary>
	/// 信息块部门
	/// </summary>
	public class BarDeptAttr  
	{
		#region 基本属性
		/// <summary>
		/// 信息块
		/// </summary>
		public const  string FK_Bar="FK_Bar";
		/// <summary>
		/// 部门
		/// </summary>
		public const  string FK_Dept="FK_Dept";		 
		#endregion	
	}
	/// <summary>
    /// 信息块部门 的摘要说明。
	/// </summary>
    public class BarDept : Entity
    {
        #region 访问权限控制
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }
        #endregion 访问权限控制

        #region 基本属性
        /// <summary>
        /// 信息块
        /// </summary>
        public string FK_Bar
        {
            get
            {
                return this.GetValStringByKey(BarDeptAttr.FK_Bar);
            }
            set
            {
                SetValByKey(BarDeptAttr.FK_Bar, value);
            }
        }
        public string FK_DeptT
        {
            get
            {
                return this.GetValRefTextByKey(BarDeptAttr.FK_Dept);
            }
        }
        /// <summary>
        ///部门
        /// </summary>
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(BarDeptAttr.FK_Dept);
            }
            set
            {
                SetValByKey(BarDeptAttr.FK_Dept, value);
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 信息块部门
        /// </summary> 
        public BarDept() { }
        /// <summary>
        /// 信息块部门
        /// </summary>
        /// <param name="_Temoid"></param>
        /// <param name="wsNo"></param>
        public BarDept(string fk_bar, string FK_Dept)
        {
            this.FK_Bar = fk_bar;
            this.FK_Dept = FK_Dept;
            if (this.Retrieve() == 0)
                this.Insert();
        }
        /// <summary>
        /// 重写基类方法
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("SSO_BarDept");
                map.EnDesc = "信息块部门";
                map.EnType = EnType.Dot2Dot;

                map.AddTBStringPK(BarDeptAttr.FK_Bar, null, "信息块", false, false, 1, 15, 1);
                map.AddDDLEntitiesPK(BarDeptAttr.FK_Dept, null, "部门", new Depts(), true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
    /// 信息块部门 
	/// </summary>
	public class BarDepts : Entities
	{
		#region 构造
		/// <summary>
        /// 信息块部门
		/// </summary>
		public BarDepts()
		{
		}
		/// <summary>
        /// 信息块部门s
		/// </summary>
		public BarDepts(string DeptNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(BarDeptAttr.FK_Dept, DeptNo);
			qo.DoQuery();
		}		 
		#endregion

		#region 方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new BarDept();
			}
		}	
		#endregion 
	}
}
