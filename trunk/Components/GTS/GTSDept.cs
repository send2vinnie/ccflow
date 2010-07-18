using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.GTS
{
	/// <summary>
	/// 人员
	/// </summary>
    public class GTSDeptAttr
    {
        #region 基本属性
        /// <summary>
        /// 学生
        /// </summary>
        public const string Addr = "Addr";
        /// <summary>
        /// 类型
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// 类别
        /// </summary>
        public const string Pass = "Pass";
        #endregion
    }
	/// <summary>
	/// 人员 的摘要说明
	/// </summary>
    public class GTSDept : EntityNoName
    {
        #region 基本属性
        /// <summary>
        ///阅读题
        /// </summary>
        public string Addr
        {
            get
            {
                return this.GetValStringByKey(GTSDeptAttr.Addr);
            }
            set
            {
                SetValByKey(GTSDeptAttr.Addr, value);
            }
        }
        /// <summary>
        ///设卷
        /// </summary>
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(GTSDeptAttr.FK_Dept);
            }
            set
            {
                SetValByKey(GTSDeptAttr.FK_Dept, value);
            }
        }
        public string Pass
        {
            get
            {
                return this.GetValStringByKey(GTSDeptAttr.Pass);
            }
            set
            {
                SetValByKey(GTSDeptAttr.Pass, value);
            }
        }
        #endregion

        #region 构造函数
        public GTSDept()
        {

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

                Map map = new Map("Port_Dept");
                map.EnDesc = "部门维护";
                map.EnType = EnType.Admin;

                #region 字段
                /*关于字段属性的增加 */
                map.AddTBStringPK(EmpAttr.No, null, null, true, false, 1, 20, 100);
                map.AddTBString(EmpAttr.Name, null, null, true, false, 2, 100, 100);
                #endregion 字段增加


                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion


        protected override bool beforeDelete()
        {
            if (this.No == "00")
                throw new Exception("您不能删除 00 部门。");

            return base.beforeDelete();
        }

        //protected override bool beforeInsert()
        //{
        //    if (this.No.Length == 2)
        //        throw new Exception("您需要输入 2 位长度数的部门编号。");
        //    return base.beforeInsert();
        //}
    }
	/// <summary>
    /// 部门维护 
	/// </summary>
	public class GTSDepts : Entities
	{
		#region 构造
		/// <summary>
		/// 人员
		/// </summary>
		public GTSDepts(){}
		#endregion

		#region 方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new GTSDept();
			}
		}	
		#endregion 

		#region 查询方法
		 
		#endregion
	}
	
}
