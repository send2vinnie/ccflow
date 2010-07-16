using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port; 
using BP.Port; 
using BP.En;


namespace BP.WF.Port
{
	/// <summary>
	/// 操作员
	/// </summary>
    public class WFEmpAttr
    {
        #region 基本属性
        /// <summary>
        /// No
        /// </summary>
        public const string No = "No";
        /// <summary>
        /// 申请人
        /// </summary>
        public const string Name = "Name";
        public const string LoginData = "LoginData";
        public const string Tel = "Tel";
        /// <summary>
        /// 授权人
        /// </summary>
        public const string Author = "Author";
        /// <summary>
        /// 授权日期
        /// </summary>
        public const string AuthorDate = "AuthorDate";
        /// <summary>
        /// 是否处于授权状态
        /// </summary>
        public const string AuthorIsOK = "AuthorIsOK";
        public const string Email = "Email";

        #endregion
    }
	/// <summary>
	/// 操作员
	/// </summary>
	public class WFEmp : EntityNoName
	{		
		#region 基本属性
        
        public string Tel
        {
            get
            {
                return this.GetValStringByKey(WFEmpAttr.Tel);
            }
            set
            {
                SetValByKey(WFEmpAttr.Tel, value);
            }
        }
        public string Email
        {
            get
            {
                return this.GetValStringByKey(WFEmpAttr.Email);
            }
            set
            {
                SetValByKey(WFEmpAttr.Email, value);
            }
        }
        public string Author
        {
            get
            {
                return this.GetValStringByKey(WFEmpAttr.Author);
            }
            set
            {
                SetValByKey(WFEmpAttr.Author, value);
            }
        }
        public string AuthorDate
        {
            get
            {
                return this.GetValStringByKey(WFEmpAttr.AuthorDate);
            }
            set
            {
                SetValByKey(WFEmpAttr.AuthorDate, value);
            }
        }
        public bool AuthorIsOK
        {
            get
            {
                return this.GetValBooleanByKey(WFEmpAttr.AuthorIsOK);
            }
            set
            {
                SetValByKey(WFEmpAttr.AuthorIsOK, value);
            }
        }
          
		#endregion 

		#region 构造函数
		/// <summary>
		/// 操作员
		/// </summary>
		public WFEmp(){}
        /// <summary>
        /// 操作员
        /// </summary>
        /// <param name="no"></param>
        public WFEmp(string no) 
        {
            this.No = no;
            try
            {
                if (this.RetrieveFromDBSources() == 0)
                {
                    Emp emp = new Emp(no);
                    this.Copy(emp);
                    this.Insert();
                }
            }
            catch
            {
                this.CheckPhysicsTable();
            }
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

                Map map = new Map("WF_Emp");
                map.EnDesc = "操作员";
                map.EnType = EnType.App;
                map.AddTBStringPK(WFEmpAttr.No, null, "No", true, true, 1, 50, 20);
                map.AddTBString(WFEmpAttr.Name, null, "Name", true, true, 0, 50, 20);
                map.AddTBString(WFEmpAttr.Tel, null, "Tel", true, true, 0, 50, 20);
                map.AddTBString(WFEmpAttr.Email, null, "Email", true, true, 0, 50, 20);

                map.AddTBString(WFEmpAttr.Author, null, "授权人", true, true, 0, 50, 20);
                map.AddTBString(WFEmpAttr.AuthorDate, null, "授权日期", true, true, 0, 50, 20);
                map.AddTBInt(WFEmpAttr.AuthorIsOK, 0, "是否授权成功", true, true);

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion	 

        #region 方法
        #endregion
    }
	/// <summary>
	/// 操作员s 
	/// </summary>
	public class WFEmps : EntitiesNoName
	{	 
		#region 构造
		/// <summary>
		/// 操作员s
		/// </summary>
		public WFEmps()
		{
		}
		/// <summary>
		/// 得到它的 Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WFEmp();
			}
		}
		#endregion
	}
	
}
