using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.XML;


namespace BP.WF.XML
{
	public class AfterDelFlowAttr
	{
		/// <summary>
		/// 编号
		/// </summary>
		public const string FK_Flow="FK_Flow";
		/// <summary>
		/// 名称
		/// </summary>
		public const string SQL="SQL";
	}
	public class AfterDelFlow:XmlEn
	{
		#region 属性
        /// <summary>
        /// flow
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(AfterDelFlowAttr.FK_Flow);
            }
        }
        /// <summary>
        /// sql
        /// </summary>
		public string SQL
		{
			get
			{
				return this.GetValStringByKey(AfterDelFlowAttr.SQL);
			}
		}
		#endregion

		#region 构造
		public AfterDelFlow()
		{
		}
		/// <summary>
		/// 编号
		/// </summary>
		/// <param name="no"></param>
        public AfterDelFlow(string no)
        {
        }
		/// <summary>
		/// 获取一个实例
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new AfterDelFlows();
			}
		}
		#endregion

		#region  公共方法
		#endregion
	}
	/// <summary>
	/// 
	/// </summary>
	public class AfterDelFlows:XmlEns
	{
		#region 构造
		/// <summary>
		/// 考核率的数据元素
		/// </summary>
		public AfterDelFlows(){}

		public AfterDelFlows(string flow)
		{
			this.RetrieveBy(AfterDelFlowAttr.FK_Flow, flow);
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
				return new AfterDelFlow();
			}
		}
		public override string File
		{
			get
			{
				return  SystemConfig.PathOfXML+"\\AfterDelFlow.xml";
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
