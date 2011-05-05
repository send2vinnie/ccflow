using System;
using System.Collections;
using BP.DA;

namespace BP.En
{
	/// <summary>
	/// EntityOIDNameMyFileAttr
	/// </summary>
	public class EntityOIDNameMyFileAttr:EntityOIDAttr
	{
		/// <summary>
		/// 名称
		/// </summary>
		public const string Name="Name";

        public const string MyFileName = "MyFileName";
        public const string MyFileSize = "MyFileSize";
        public const string MyFileH = "MyFileH";
        public const string MyFileW = "MyFileW";
        public const string MyFileExt = "MyFileExt";
	}
	/// <summary>
	/// 用于 OID Name 属性的实体继承。	
	/// </summary>
    abstract public class EntityOIDNameMyFile : EntityOID
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        protected EntityOIDNameMyFile() { }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="oid">OID</param>
        protected EntityOIDNameMyFile(int oid) : base(oid) { }
        #endregion

        #region 属性方法
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStringByKey(EntityOIDNameMyFileAttr.Name);
            }
            set
            {
                this.SetValByKey(EntityOIDNameMyFileAttr.Name, value);
            }
        }
        public string MyFileExt
        {
            get
            {
                return this.GetValStringByKey("MyFileExt");
            }
            set
            {
                this.SetValByKey("MyFileExt", value);
            }
        }
        public string MyFileName
        {
            get
            {
                return this.GetValStringByKey("MyFileName");
            }
            set
            {
                this.SetValByKey("MyFileName", value);
            }
        }
        public int MyFileSize
        {
            get
            {
                return this.GetValIntByKey("MyFileSize");
            }
            set
            {
                this.SetValByKey("MyFileSize", value);
            }
        }
        public int MyFileH
        {
            get
            {
                return this.GetValIntByKey("MyFileH");
            }
            set
            {
                this.SetValByKey("MyFileH", value);
            }
        }
        public int MyFileW
        {
            get
            {
                return this.GetValIntByKey("MyFileW");
            }
            set
            {
                this.SetValByKey("MyFileW", value);
            }
        }
        public bool IsImg
        {
            get
            {
                return DataType.IsImgExt(this.MyFileExt);
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
        #endregion
    }
	/// <summary>
	/// 用于OID Name 属性的实体继承
	/// </summary>
	abstract public class EntityOIDNameMyFiles : EntitiesOID
	{
		#region 构造
		/// <summary>
		/// 构造
		/// </summary>
		public EntityOIDNameMyFiles()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}		
		#endregion
	}
}
