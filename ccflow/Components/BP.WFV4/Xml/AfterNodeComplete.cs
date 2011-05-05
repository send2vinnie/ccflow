using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.XML;


namespace BP.WF.XML
{
	public class AfterNodeCompleteAttr
	{
		/// <summary>
		/// 节点
		/// </summary>
		public const string FK_Node="FK_Node";
		/// <summary>
		/// 名称
		/// </summary>
		public const string SQL="SQL";
	}
	public class AfterNodeComplete:XmlEn
	{
		#region 属性
        /// <summary>
        /// flow
        /// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(AfterNodeCompleteAttr.FK_Node);
            }
        }
        /// <summary>
        /// sql
        /// </summary>
		public string SQL
		{
			get
			{
				return this.GetValStringByKey(AfterNodeCompleteAttr.SQL);
			}
		}
		#endregion

		#region 构造
		public AfterNodeComplete()
		{
		}
		/// <summary>
		/// 编号
		/// </summary>
		/// <param name="no"></param>
        public AfterNodeComplete(string no)
        {
        }
		/// <summary>
		/// 获取一个实例
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new AfterNodeCompletes();
			}
		}
		#endregion

		#region  公共方法
		#endregion
	}
	/// <summary>
	/// 
	/// </summary>
	public class AfterNodeCompletes:XmlEns
	{
		#region 构造
		/// <summary>
		/// 考核率的数据元素
		/// </summary>
		public AfterNodeCompletes(){}

        /// <summary>
        /// AfterNodeCompletes
        /// </summary>
        /// <param name="nodeID"></param>
        public AfterNodeCompletes(int nodeID)
        {
            this.RetrieveBy(AfterNodeCompleteAttr.FK_Node, nodeID);
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
				return new AfterNodeComplete();
			}
		}
        /// <summary>
        /// 文件
        /// </summary>
        public override string File
        {
            get
            {
                return SystemConfig.PathOfXML + "\\AfterNodeComplete.xml";
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
