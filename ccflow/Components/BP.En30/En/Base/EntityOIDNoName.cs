using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.En
{
	/// <summary>
    /// 用于分级次的维护
	/// </summary>
    public class EntityOIDNoNameAttr : EntityOIDAttr
    {
        /// <summary>
        /// 编号
        /// </summary>
        public const string No = "No";
        /// <summary>
        /// 名称
        /// </summary>
        public const string Name = "Name";
    }
	/// <summary>
    /// 用于分级次的维护
	/// </summary>
    abstract public class EntityOIDNoName : EntityOIDNo
    {
        #region 基本属性
        /// <summary>
        /// 实体编号
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStringByKey(EntityOIDNoNameAttr.Name);
            }
            set
            {
                this.SetValByKey(EntityOIDNoNameAttr.Name, value);
            }
        }
        #endregion

        #region 构造
        public EntityOIDNoName()
        { }
        protected EntityOIDNoName(string _No)
        {
            this.No = _No;
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(EntityOIDNoAttr.No, this.No);
            if (qo.DoQuery() == 0)
            {
                throw new Exception("@没有编号[" + this.No + "][" + this.EnDesc + "]这个实体");
            }
        }
        protected EntityOIDNoName(int _OID) : base(_OID) { }

        public int RetrieveByNo()
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(EntityOIDNoAttr.No, this.No);
            return qo.DoQuery();
        }
        #endregion

        #region  重写基类的方法。
        #endregion
    }
    /// <summary>
    /// EntitiesOIDNoName 用于分级次的维护
    /// </summary>
    abstract public class EntitiesOIDNoName : EntitiesOID
    {
        /// <summary>
        /// 用于分级次的维护
        /// </summary>
        public EntitiesOIDNoName()
        {
        }
    }
}
