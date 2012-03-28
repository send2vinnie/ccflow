using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;
using BP.XML;


namespace BP.Port.Xml
{
	/// <summary>
	/// 属性
	/// </summary>
    public class ModelAttr 
    {
        /// <summary>
        /// Url
        /// </summary>
        public const string Url = "Url";
        /// <summary>
        /// 是否可用
        /// </summary>
        public const string IsEnable = "IsEnable";
        /// <summary>
        /// Img
        /// </summary>
        public const string Img = "Img";

    }
	/// <summary>
	/// Model 的摘要说明，属性的配置。
	/// </summary>
	public class Model:XmlEnNoName
	{
		#region 属性
        public string Url
		{
			get
			{
				return this.GetValStringByKey(ModelAttr.Url);
			}
		}
        public string Img
        {
            get
            {
                return this.GetValStringByKey(ModelAttr.Img);
            }
        }
        public bool IsEnable
        {
            get
            {
                string s = this.GetValStringByKey(ModelAttr.IsEnable);
                if (s == null || s == "" || s=="0")
                    return false;

                return true;
            }
        }
		#endregion

		#region 构造
		public Model()
		{
		}
		/// <summary>
		/// 获取一个实例
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new Models();
			}
		}
		#endregion
	}
	/// <summary>
	/// 属性集合
	/// </summary>
	public class Models:XmlEns
	{
		#region 构造
		/// <summary>
		/// 考核过错行为的数据元素
		/// </summary>
		public Models()
		{
		}
		#endregion

		#region 重写基类属性或方法。
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new Model();
			}
		}
		public override string File
		{
			get
			{
                return SystemConfig.PathOfWebApp + "\\Port\\Model.xml";
			}
		}
		/// <summary>
		/// 物理表名
		/// </summary>
		public override string TableName
		{
			get
			{
				return "Item";
			}
		}
		public override Entities RefEns
		{
			get
			{
				return null;
			}
		}
		#endregion
	}
}
