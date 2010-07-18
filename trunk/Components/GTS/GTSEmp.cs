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
    public class GTSEmpAttr
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
    public class GTSEmp : EntityNoName
    {
        #region 基本属性
        /// <summary>
        ///阅读题
        /// </summary>
        public string Addr
        {
            get
            {
                return this.GetValStringByKey(GTSEmpAttr.Addr);
            }
            set
            {
                SetValByKey(GTSEmpAttr.Addr, value);
            }
        }
        /// <summary>
        ///设卷
        /// </summary>
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(GTSEmpAttr.FK_Dept);
            }
            set
            {
                SetValByKey(GTSEmpAttr.FK_Dept, value);
            }
        }
        public string Pass
        {
            get
            {
                return this.GetValStringByKey(GTSEmpAttr.Pass);
            }
            set
            {
                SetValByKey(GTSEmpAttr.Pass, value);
            }
        }
        #endregion

        #region 构造函数
        public GTSEmp()
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

                Map map = new Map("Port_Emp");
                map.EnDesc = "考生";
                map.EnType = EnType.Admin;

                #region 字段
                /*关于字段属性的增加 */
                map.AddTBStringPK(EmpAttr.No, null, null, true, false, 2, 20, 100);
                map.AddTBString(EmpAttr.Name, null, null, true, false, 1, 100, 100);
                map.AddTBString(EmpAttr.Pass, "pub", null, true, false, 0, 20, 10);

                map.AddTBInt("MyFileNum", 0, "MyFileNum ", true, false);
                 

                map.AddDDLEntities(EmpAttr.FK_Dept, null, null, new Port.Depts(), true);
                map.AddMyFile();
                #endregion 字段增加

                map.AddSearchAttr(EmpAttr.FK_Dept);

                RefMethod rm = new RefMethod();
                rm.Title = "图片";
                rm.ClassMethodName = this.ToString() + ".DoOpen";
                map.AddRefMethod(rm);



                this._enMap = map;
                return this._enMap;
            }
        }
        protected override bool beforeDelete()
        {
            if (this.No == "admin")
                throw new Exception("@您不能删除admin.");
            return base.beforeDelete();
        }
        protected override bool beforeUpdateInsertAction()
        {
            if (this.No == "admin")
                this.FK_Dept = "00";
            return base.beforeUpdateInsertAction();
        }
        public string DoOpen()
        {
            BP.PubClass.Open("../Comm/Item3.aspx?EnName=BP.GTS.GTSEmp&No="+this.No);
            return null;
        }
        #endregion
    }
	/// <summary>
	/// 人员 
	/// </summary>
	public class GTSEmps : Entities
	{
		#region 构造
		/// <summary>
		/// 人员
		/// </summary>
		public GTSEmps(){}
		#endregion

		#region 方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new GTSEmp();
			}
		}	
		#endregion 

		#region 查询方法
		 
		#endregion
	}
	
}
