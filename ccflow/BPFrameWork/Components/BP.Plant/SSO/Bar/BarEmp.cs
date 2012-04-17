using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.SSO
{
	/// <summary>
	/// 信息块人员
	/// </summary>
	public class BarEmpAttr  
	{
		#region 基本属性
		/// <summary>
		/// 信息块
		/// </summary>
		public const  string FK_Bar="FK_Bar";
		/// <summary>
		/// 工作人员
		/// </summary>
		public const  string FK_Emp="FK_Emp";		 
		#endregion	
	}
	/// <summary>
    /// 信息块人员 的摘要说明。
	/// </summary>
    public class BarEmp : Entity
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
                return this.GetValStringByKey(BarEmpAttr.FK_Bar);
            }
            set
            {
                SetValByKey(BarEmpAttr.FK_Bar, value);
            }
        }
        public string FK_EmpT
        {
            get
            {
                return this.GetValRefTextByKey(BarEmpAttr.FK_Emp);
            }
        }
        /// <summary>
        ///工作人员
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(BarEmpAttr.FK_Emp);
            }
            set
            {
                SetValByKey(BarEmpAttr.FK_Emp, value);
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 信息块人员
        /// </summary> 
        public BarEmp() { }
        /// <summary>
        /// 信息块人员
        /// </summary>
        /// <param name="_Temoid"></param>
        /// <param name="wsNo"></param>
        public BarEmp(string fk_bar, string FK_Emp)
        {
            this.FK_Bar = fk_bar;
            this.FK_Emp = FK_Emp;
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

                Map map = new Map("SSO_BarEmp");
                map.EnDesc = "信息块人员";
                map.EnType = EnType.Dot2Dot;

                map.AddTBStringPK(BarEmpAttr.FK_Bar, null, "信息块", false, false, 1, 15, 1);
                map.AddDDLEntitiesPK(BarEmpAttr.FK_Emp, null, "工作人员", new Emps(), true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
    /// 信息块人员 
	/// </summary>
	public class BarEmps : Entities
	{
		#region 构造
		/// <summary>
        /// 信息块人员
		/// </summary>
		public BarEmps()
		{
		}
		/// <summary>
        /// 信息块人员s
		/// </summary>
		public BarEmps(string EmpNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(BarEmpAttr.FK_Emp, EmpNo);
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
				return new BarEmp();
			}
		}	
		#endregion 
	}
}
