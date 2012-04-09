using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP;
namespace BP.Sys
{
	/// <summary>
	/// 用户注册表
	/// </summary>
    public class UserRegeditAttr
    {
        /// <summary>
        /// 是否显示图片
        /// </summary>
        public const string IsPic = "IsPic";
        /// <summary>
        /// 名称
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        /// <summary>
        /// 实体名称
        /// </summary>
        public const string CfgKey = "CfgKey";
        /// <summary>
        /// 属性
        /// </summary> 
        public const string Vals = "Vals";
        /// <summary>
        /// 查询
        /// </summary>
        public const string SearchKey = "SearchKey";
        public const string MyPK = "MyPK";

        /// <summary>
        /// OrderBy
        /// </summary>
        public const string OrderBy = "OrderBy";
        /// <summary>
        /// OrderWay
        /// </summary>
        public const string OrderWay = "OrderWay";

        public const string GenerSQL = "GenerSQL";
        public const string Paras = "Paras";
        public const string NumKey = "NumKey";
        /// <summary>
        /// 查询中
        /// </summary>
        public const string MVals = "MVals";
    }
	/// <summary>
	/// 用户注册表
	/// </summary>
	public class UserRegedit: EntityMyPK
	{
		#region 用户注册表信息键值列表
		#endregion

		#region 基本属性
        public bool IsPic
        {
            get
            {
                return this.GetValBooleanByKey(UserRegeditAttr.IsPic);
            }
            set
            {
                this.SetValByKey(UserRegeditAttr.IsPic, value);
            }
        }
        public string NumKey
        {
            get
            {
                return this.GetValStringByKey(UserRegeditAttr.NumKey);
            }
            set
            {
                this.SetValByKey(UserRegeditAttr.NumKey, value);
            }
        }
        public string Paras
        {
            get
            {
                return this.GetValStringByKey(UserRegeditAttr.Paras);
            }
            set
            {
                this.SetValByKey(UserRegeditAttr.Paras, value);
            }
        }
        public string GenerSQL
        {
            get
            {
                string GenerSQL = this.GetValStringByKey(UserRegeditAttr.GenerSQL);
                GenerSQL = GenerSQL.Replace("~", "'");
                return GenerSQL;
            }
            set
            {
                this.SetValByKey(UserRegeditAttr.GenerSQL, value);
            }
        }
        public string OrderWay
        {
            get
            {
                return this.GetValStringByKey(UserRegeditAttr.OrderWay);
            }
            set
            {
                this.SetValByKey(UserRegeditAttr.OrderWay, value);
            }
        }
        public string OrderBy
        {
            get
            {
                return this.GetValStringByKey(UserRegeditAttr.OrderBy);
            }
            set
            {
                this.SetValByKey(UserRegeditAttr.OrderBy, value);
            }
        }
		/// <summary>
		/// FK_Emp
		/// </summary>
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(UserRegeditAttr.FK_Emp) ; 
			}
			set
			{
				this.SetValByKey(UserRegeditAttr.FK_Emp,value) ; 
			}		
		}
		/// <summary>
		/// CfgKey
		/// </summary>
		public string CfgKey
		{
			get
			{
				return this.GetValStringByKey(UserRegeditAttr.CfgKey ) ; 
			}
			set
			{
				this.SetValByKey(UserRegeditAttr.CfgKey,value) ; 
			}
		}
        public string SearchKey
        {
            get
            {
                return this.GetValStringByKey(UserRegeditAttr.SearchKey);
            }
            set
            {
                this.SetValByKey(UserRegeditAttr.SearchKey, value);
            }
        }
		/// <summary>
		/// Vals
		/// </summary>
		public string Vals
		{
			get
			{
				return this.GetValStringByKey(UserRegeditAttr.Vals ) ; 
			}
			set
			{
				this.SetValByKey(UserRegeditAttr.Vals,value) ; 
			}
		}
        public string MVals
        {
            get
            {
                return this.GetValStringByKey(UserRegeditAttr.MVals);
            }
            set
            {
                this.SetValByKey(UserRegeditAttr.MVals, value);
            }
        }
        public string MyPK
        {
            get
            {
                return this.GetValStringByKey(UserRegeditAttr.MyPK);
            }
            set
            {
                this.SetValByKey(UserRegeditAttr.MyPK, value);
            }
        }
		#endregion

		#region 构造方法
		/// <summary>
		/// 用户注册表
		/// </summary>
		public UserRegedit()
		{
		}
        public UserRegedit(string mypk)
        {
           // this.MyPK = mypk;
           //int i=  this.RetrieveFromDBSources();
           //if (i == 0)
           //{
           //    this.CfgKey = cfgkey;
           //    this.FK_Emp = fk_emp;
           //}
        }
		/// <summary>
		/// 用户注册表
		/// </summary>
		/// <param name="fk_emp">人员</param>
		/// <param name="cfgkey">配置</param>
		public UserRegedit(string fk_emp, string cfgkey)
		{
            this.MyPK = fk_emp + cfgkey;
            this.CfgKey = cfgkey;
            this.FK_Emp = fk_emp;
            int i = this.RetrieveFromDBSources();
            if (i == 0)
            {
                this.CfgKey = cfgkey;
                this.FK_Emp = fk_emp;
                this.DirectInsert();
               // this.DirectInsert();
            }
		}
		/// <summary>
		/// EnMap
		/// </summary>
		public override Map EnMap
		{
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Sys_UserRegedit");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.EnDesc = "用户注册表";
                map.EnType = EnType.Sys;

                map.AddMyPK();

                map.AddTBString(UserRegeditAttr.FK_Emp, null, "用户", false, false, 1, 30, 20);
                map.AddTBString(UserRegeditAttr.CfgKey, null, "键", true, false, 1, 200, 20);
                map.AddTBString(UserRegeditAttr.Vals, null, "值", true, false, 0, 2000, 20);
                map.AddTBString(UserRegeditAttr.GenerSQL, null, "GenerSQL", true, false, 0, 2000, 20);
                map.AddTBString(UserRegeditAttr.Paras, null, "Paras", true, false, 0, 2000, 20);
                map.AddTBString(UserRegeditAttr.NumKey, null, "分析的Key", true, false, 0, 300, 20);
                map.AddTBString(UserRegeditAttr.OrderBy, null, "OrderBy", true, false, 0, 300, 20);
                map.AddTBString(UserRegeditAttr.OrderWay, null, "OrderWay", true, false, 0, 300, 20);
                map.AddTBString(UserRegeditAttr.SearchKey, null, "SearchKey", true, false, 0, 300, 20);
                map.AddTBString(UserRegeditAttr.MVals, null, "MVals", true, false, 0, 300, 20);
                map.AddBoolean(UserRegeditAttr.IsPic, false, "是否图片", true, false);

                this._enMap = map;
                return this._enMap;
            }
		}
		#endregion 
        public override Entities GetNewEntities
        {
            get { return new UserRegedits(); }
        }
        protected override bool beforeUpdateInsertAction()
        {
            this.MyPK = this.FK_Emp + this.CfgKey;
            return base.beforeUpdateInsertAction();
        }
	}
	/// <summary>
	/// 用户注册表s
	/// </summary>
	public class UserRegedits : EntitiesOID
	{		
		#region 构造
		public UserRegedits()
		{
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="emp"></param>
		public UserRegedits(string emp)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(UserRegeditAttr.FK_Emp, emp);
			qo.DoQuery();
		}
		/// <summary>
		/// 得到它的 Entity
		/// </summary>
		public override Entity GetNewEntity 
		{
			get
			{
				return new UserRegedit();
			}
		}
		#endregion
	}
}
