using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.XML;


namespace BP.WF.XML
{
    public class BookTemplateAttr
    {
        /// <summary>
        /// 编号
        /// </summary>
        public const string No = "No";
        public const string Name = "Name";
        public const string URL = "URL";
        public const string NodeID = "NodeID";
    }
    public class BookTemplate : XmlEnNoName
    {
        #region 属性
        /// <summary>
        /// flow
        /// </summary>
        public string URL
        {
            get
            {
                return this.GetValStringByKey(BookTemplateAttr.URL);
            }
        }
        /// <summary>
        /// sql
        /// </summary>
        public int NodeID
        {
            get
            {
                return this.GetValIntByKey(BookTemplateAttr.NodeID);
            }
        }
        #endregion

        #region 构造
        public BookTemplate()
        {
        }
        /// <summary>
        /// 编号
        /// </summary>
        /// <param name="no"></param>
        public BookTemplate(string no)
        {
        }
        /// <summary>
        /// 获取一个实例
        /// </summary>
        public override XmlEns GetNewEntities
        {
            get
            {
                return new BookTemplates();
            }
        }
        #endregion
    }
	/// <summary>
	/// 
	/// </summary>
	public class BookTemplates:XmlEns
	{
		#region 构造
		/// <summary>
		/// 文书模板
		/// </summary>
		public BookTemplates(){}
		public BookTemplates(int node)
		{
            this.RetrieveBy(BookTemplateAttr.NodeID, node);
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
				return new BookTemplate();
			}
		}
		public override string File
		{
			get
			{
				return  SystemConfig.PathOfXML+"\\BookTemplate.xml";
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
				return null; //new BP.ZF1.AdminTools();
			}
		}
		#endregion
		 
	}
}
