

using System;
using System.Data;
using BP.DA;
using BP.En.Base;
using BP.WF;
using BP.Tax; 
using BP.Port; 
using BP.En;


namespace BP.DA
{
	/// <summary>
	/// 转发记录
	/// </summary>
    public class GenerOIDAttr
    {
        #region 基本属性
        /// <summary>
        /// 工作ID
        /// </summary>
        public const string OID = "OID";
        /// <summary>
        /// 节点
        /// </summary>
        public const string NodeId = "NodeId";
        /// <summary>
        /// 工作人员
        /// </summary>
        public const string Worker = "Worker";
        /// <summary>
        /// 退回原因
        /// </summary>
        public const string Sort = "Sort";
        public const string FK_Emp = "FK_Emp";
        public const string Emps = "Emps";
        /// <summary>
        /// 是否是收回
        /// </summary>
        public const string IsTakeBack = "IsTakeBack";
        #endregion
    }
	/// <summary>
	/// 转发记录
	/// </summary>
	public class GenerOID : Entity
	{
        public int Gener(string sort)
        {
            int val = DBAccess.RunSQLReturnVal("SELECT OID FROM "
        }
		#region 基本属性
        public int OID
        {
            get
            {
                return this.GetValIntByKey(GenerOIDAttr.OID);
            }
            set
            {
                SetValByKey(GenerOIDAttr.OID, value);
            }
        }
        public string Sort
        {
            get
            {
                return this.GetValStringByKey(GenerOIDAttr.Sort);
            }
            set
            {
                SetValByKey(GenerOIDAttr.Sort, value);
            }
        }
		#endregion 

		#region 构造函数
		/// <summary>
		/// 转发记录
		/// </summary>
		public GenerOID(){}
		/// <summary>
		/// 重写基类方法
		/// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("Sys_GenerOID");
                map.EnDesc = "转发记录";
                map.EnType = EnType.App;

                map.AddTBIntPK(GenerOIDAttr.OID, 0, "OID", true, true);
                map.AddTBStringPK(GenerOIDAttr.Sort, "", "类型", true, true, 1, 20, 10);
                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion	 
	}
	/// <summary>
	/// 转发记录s 
	/// </summary>
	public class GenerOIDs : Entities
	{	 
		#region 构造
		/// <summary>
		/// 转发记录s
		/// </summary>
		public GenerOIDs()
		{

		}
		/// <summary>
		/// 得到它的 Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new GenerOID();
			}
		}
		#endregion
	}
	
}
