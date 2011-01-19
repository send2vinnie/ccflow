using System;
using System.Collections;
using BP.DA;

namespace BP.En
{
	/// <summary>
	/// NoEntity 的摘要说明。
	/// </summary>
    abstract public class EntityMyPKInt : Entity
    {
        #region 基本属性
        /// <summary>
        /// 主要键
        /// </summary>
        public override string PK
        {
            get
            {
                return "MyPK";
            }
        }
        /// <summary>
        /// 集合类名称
        /// </summary>
        public string MyPK
        {
            get
            {
                return this.GetValStringByKey(EntityMyPKAttr.MyPK);
            }
            set
            {
                this.SetValByKey(EntityMyPKAttr.MyPK, value);
            }
        }
        public int NoInt
        {
            get
            {
                return this.GetValIntByKey(EntityMyPKAttr.NoInt);
            }
            set
            {
                this.SetValByKey(EntityMyPKAttr.NoInt, value);
            }
         }
        #endregion

        #region 构造
        public EntityMyPKInt()
        {
        }
        /// <summary>
        /// class Name 
        /// </summary>
        /// <param name="_MyPK">_MyPK</param>
        protected EntityMyPKInt(string _MyPK)
        {
            this.MyPK = _MyPK;
            this.Retrieve();
        }
        #endregion
    }
	/// <summary>
	/// 类名实体集合
	/// </summary>
    abstract public class EntitiesMyPKInt : Entities
    {
        /// <summary>
        /// 实体集合
        /// </summary>
        public EntitiesMyPKInt()
        {
        }
    }
}
