using System;
using System.Collections;
using BP.DA;

namespace BP.En
{
    public class EntityAIDAttr
    {
        /// <summary>
        /// AID
        /// </summary>
        public static string AID = "AID";
    }
	/// <summary>
	/// 属性列表
	/// </summary>
    public class EntityAIDMyFileAttr : EntityAIDAttr
    {
        /// <summary>
        /// MyFileName
        /// </summary>
        public const string MyFileName = "MyFileName";
        /// <summary>
        /// MyFilePath
        /// </summary>
        public const string MyFilePath = "MyFilePath";
        /// <summary>
        /// MyFileExt
        /// </summary>
        public const string MyFileExt = "MyFileExt";
    }
	/// <summary>
	/// AID实体,只有一个实体这个实体只有一个主键属性。
	/// </summary>
	abstract public class EntityAID : Entity
	{		 
		#region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public override string PK
        {
            get
            {
                return "AID";
            }
        }
		/// <summary>
		/// AID, 如果是空的就返回 0 . 
		/// </summary>
        public int AID
        {
            get
            {
                return this.GetValIntByKey(EntityAIDAttr.AID);
            }
        }
		#endregion

		#region 构造函数
		/// <summary>
		/// 构造一个空实例
		/// </summary>
		protected EntityAID()
		{
		}
		/// <summary>
		/// 根据AID构造实体
		/// </summary>
		/// <param name="AID">AID</param>
        protected EntityAID(int AID)
        {
            this.SetValByKey(EntityAIDAttr.AID, AID);
            this.Retrieve();
        }
		#endregion
	 
		#region override 方法
        public override bool IsExits
        {
            get
            {
                if (this.AID == 0)
                    return false;

                // 生成数据库判断语句。
                string selectSQL = "SELECT " + this.PKField + " FROM " + this.EnMap.PhysicsTable + " WHERE AID=" + this.HisDBVarStr + "v";
                Paras ens = new Paras();
                ens.Add("v", this.AID);

                // 从数据库里面查询，判断有没有。
                switch (this.EnMap.EnDBUrl.DBUrlType)
                {
                    case DBUrlType.AppCenterDSN:
                        return DBAccess.IsExits(selectSQL, ens);
                    case DBUrlType.DBAccessOfMSSQL2000:
                        return DBAccessOfMSSQL2000.IsExits(selectSQL);
                    case DBUrlType.DBAccessOfOLE:
                        return DBAccessOfOLE.IsExits(selectSQL);
                    case DBUrlType.DBAccessOfOracle9i:
                        return DBAccessOfOracle9i.IsExits(selectSQL);
                    default:
                        throw new Exception("没有设计到。" + this.EnMap.EnDBUrl.DBType);
                }
            }
        }
		/// <summary>
		/// 删除之前的操作。
		/// </summary>
		/// <returns></returns>
		protected override bool beforeDelete() 
		{
			if (base.beforeDelete()==false)
				return false;			
			try 
			{				
				if (this.AID < 0 )
					throw new Exception("@实体["+this.EnDesc+"]没有被实例化，不能Delete().");
				return true;
			} 
			catch (Exception ex) 
			{
				throw new Exception("@["+this.EnDesc+"].beforeDelete err:"+ex.Message);
			}
		}
		/// <summary>
		/// beforeInsert 之前的操作。
		/// </summary>
		/// <returns></returns>
        protected override bool beforeInsert()
        {
            if (this.AID > 0)
                throw new Exception("@[" + this.EnDesc + "], 实体已经被实例化 AID=[" + this.AID + "]，不能Insert.");

            return base.beforeInsert();
        }
		/// <summary>
		/// beforeUpdate
		/// </summary>
		/// <returns></returns>
		protected override bool beforeUpdate()
		{
			if (base.beforeUpdate()==false)
				return false;

			/*
			if (this.AID <= 0 )
				throw new Exception("@实体["+this.EnDesc+"]没有被实例化，不能Update().");
				*/
			return true;
		}
        protected virtual string SerialKey
        {
            get
            {
                return "AID";
            }
        }
       
		#endregion

		#region public 方法
		  
		#endregion
	}
    /// <summary>
    /// EntitiesAID
    /// </summary>
    abstract public class EntitiesAID : Entities
    {
        public EntitiesAID()
        {
        }
    }
}
