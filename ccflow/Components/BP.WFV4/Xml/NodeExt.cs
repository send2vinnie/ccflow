using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.XML;


namespace BP.WF.XML
{
	public class NodeExtAttr
	{
		/// <summary>
		/// 节点
		/// </summary>
		public const string NodeEnName="NodeEnName";
		/// <summary>
		/// 是否启动发送按钮
		/// </summary>
		public const string EnableSendBtn="EnableSendBtn";
		/// <summary>
		/// 在选择时间
		/// </summary>
		public const string OnSelectedMsg="OnSelectedMsg";
		/// <summary>
		/// 在新建时间
		/// </summary>
		public const string OnNew="OnNew";
		/// <summary>
		/// 在保存
		/// </summary>
		public const string OnSaveMsg="OnSaveMsg";
		/// <summary>
		/// 报表
		/// </summary>
		public const string RptMsg="RptMsg";
		/// <summary>
		/// 最底部信息
		/// </summary>
		public const string WorkEndInfo="WorkEndInfo";
        public const string EnableReturnBtn = "EnableReturnBtn";
	}
	public class NodeExt:XmlEn
	{
		#region 属性
		/// <summary>
		/// 节点
		/// </summary>
		public string NodeEnName
		{
			get
			{
				return  this.GetValStringByKey(NodeExtAttr.NodeEnName) ;
			}
		}
        public string EnableReturnBtnStr
        {
            get
            {
                string msg = this.GetValStringByKey(NodeExtAttr.EnableReturnBtn);
                return msg;
            }
        }
        public string EnableSendBtnStr
        {
            get
            {
                string msg = this.GetValStringByKey(NodeExtAttr.EnableSendBtn);
                return msg;
            }
        }
		/// <summary>
		/// 是否启动发送按钮
		/// </summary>
		public bool EnableSendBtn
		{
            get
            {
                string msg = this.GetValStringByKey(NodeExtAttr.EnableSendBtn);
                if (msg == "0")
                    return false;
                else
                    return true;
            }
		}
		/// <summary>
		/// 在选择时间
		/// </summary>
		public string OnSelectedMsg
		{
			get
			{
				return  this.GetValStringByKey(NodeExtAttr.OnSelectedMsg) ;
			}
		}
		/// <summary>
		/// 在新建时间
		/// </summary>
		public string OnNew
		{
			get
			{
				return  this.GetValStringByKey(NodeExtAttr.OnNew) ;
			}
		}
		/// <summary>
		/// 在保存
		/// </summary>
		public string OnSaveMsg
		{
			get
			{
				return this.GetValStringByKey(NodeExtAttr.OnSaveMsg);
			}
		}
		/// <summary>
		/// 报表
		/// </summary>
        public string RptMsg
        {
            get
            {
                return this.GetValStringByKey(NodeExtAttr.RptMsg);
            }
        }
		/// <summary>
		/// 最底部信息
		/// </summary>
		public string WorkEndInfo
		{
			get
			{
				return this.GetValStringByKey(NodeExtAttr.WorkEndInfo);
			}
		}
		#endregion

		#region 构造
		/// <summary>
		/// 节点扩展信息
		/// </summary>
		public NodeExt()
		{
		}
		/// <summary>
		/// 节点扩展信息
		/// </summary>
		/// <param name="enName">如果是审核点，它可以是节点ID。</param></param>
		public NodeExt(string enName)
		{
			this.RetrieveByPK(NodeExtAttr.NodeEnName,enName);
		}
		/// <summary>
		/// 获取一个实例
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new NodeExts();
			}
		}
		#endregion

		#region  公共方法
		 
		#endregion
	}
	/// <summary>
	/// 
	/// </summary>
	public class NodeExts:XmlEns
	{
		#region 构造
		/// <summary>
		/// 考核率的数据元素
		/// </summary>
		public NodeExts(){}
		#endregion

		#region 重写基类属性或方法。
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new NodeExt();
			}
		}
		public override string File
		{
			get
			{
				return  SystemConfig.PathOfXML+"\\Node\\";
			}
		}
		/// <summary>
		/// 物理表名
		/// </summary>
		public override string TableName
		{
			get
			{
				return "Node";
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
