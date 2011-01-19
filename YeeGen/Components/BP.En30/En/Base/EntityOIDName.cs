using System;
using System.Collections;
using BP.DA;

namespace BP.En
{
	/// <summary>
	/// EntityOIDNameAttr
	/// </summary>
	public class EntityOIDNameAttr:EntityOIDAttr
	{
		/// <summary>
		/// 名称
		/// </summary>
		public const string Name="Name";
	}
	/// <summary>
	/// 用于 OID Name 属性的实体继承。	
	/// </summary>
    abstract public class EntityOIDName : EntityOID
    {
        #region 构造
        /// <summary>
        /// 主键值
        /// </summary>
        public override string PK
        {
            get
            {
                return "OID";
            }
        }
        /// <summary>
        /// 构造
        /// </summary>
        protected EntityOIDName() { }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="oid">OID</param>
        protected EntityOIDName(int oid) : base(oid) { }
        #endregion

        #region 属性方法

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStringByKey(EntityOIDNameAttr.Name);
            }
            set
            {
                this.SetValByKey(EntityOIDNameAttr.Name, value);
            }
        }
        /// <summary>
        /// 按照名称查询。
        /// </summary>
        /// <returns>返回查询出来的个数</returns>
        public int RetrieveByName()
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere("Name", this.Name);
            return qo.DoQuery();
        }
        protected int LoadDir(string dir)
        {


            return 1;
        }
        #endregion
    }
	/// <summary>
	/// 用于OID Name 属性的实体继承
	/// </summary>
	abstract public class EntitiesOIDName : EntitiesOID
	{
		#region 构造
		/// <summary>
		/// 构造
		/// </summary>
		public EntitiesOIDName()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}		
		#endregion
	}
}
