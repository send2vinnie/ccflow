
using System;
using System.Data;
using BP.DA;
using BP.En.Base;
using BP.WF;
using BP.En;


namespace BP.WF
{
	 
	/// <summary> 
	/// 节点的相关功能属性
	/// </summary>
	public class NodeRefFuncAttr :EntityOIDNameAttr
	{  
		#region 基本属性
		/// <summary>
		/// 节点ID
		/// </summary>
		public const  string NodeId="NodeId";	
		/// <summary>
		/// URL
		/// </summary>
		public const  string URL="URL";
		/// <summary>
		/// 相关功能类型
		/// </summary>
		public const  string RefFuncType="RefFuncType";
		/// <summary>
		/// Width
		/// </summary>
		public const  string Width="Width";
		/// <summary>
		/// Height
		/// </summary>
		public const  string Height="Height";
		/// <summary>
		/// 默认得图标
		/// </summary>
		public const  string DefaultIcon="DefaultIcon";
		/// <summary>
		/// 默认得盘旋图标
		/// </summary>
		public const  string DefaultHover="DefaultHover";
		/// <summary>
		/// ToolTip
		/// </summary>
		public const  string ToolTip="ToolTip";
		/// <summary>
		/// 送达期限
		/// </summary>
		public const  string TimeLimit="TimeLimit";
		public const  string FilePrix="FilePrix";
		#endregion
	}
	/// <summary>
	/// 相关功能
	/// </summary>
	public class NodeRefFunc : EntityOIDName
	{
		#region 基本属性
	    /// <summary>
	    /// 相关功能类型
		/// </summary>
		public int RefFuncType
		{
			get
			{  
				return this.GetValIntByKey(NodeRefFuncAttr.RefFuncType);
			}
			set
			{
				this.SetValByKey(NodeRefFuncAttr.RefFuncType,value);
			}
		}
		/// <summary>
		/// NodeId
		/// </summary>
		public int NodeId
		{
			get
			{
				return this.GetValIntByKey(NodeRefFuncAttr.NodeId);
			}
			set
			{
				this.SetValByKey(NodeRefFuncAttr.NodeId,value);
			}
		}
		/// <summary>
		/// url
		/// </summary>
		public string URL
		{
			get
			{
				return this.GetValStringByKey(NodeRefFuncAttr.URL);
			}
			set
			{
				this.SetValByKey(NodeRefFuncAttr.URL,value);
			}
		}
		public string FilePrix
		{
			get
			{
				return this.GetValStringByKey(NodeRefFuncAttr.FilePrix).Trim();
			}
			set
			{
				this.SetValByKey(NodeRefFuncAttr.FilePrix,value);
			}
		}
		/// <summary>
		/// Height
		/// </summary>
		public int Height
		{
			get
			{
				return this.GetValIntByKey(NodeRefFuncAttr.Height);
			}
			set
			{
				this.SetValByKey(NodeRefFuncAttr.Height,value);
			}
		}
		/// <summary>
		/// Width
		/// </summary>
		public int Width
		{
			get
			{
				return this.GetValIntByKey(NodeRefFuncAttr.Width);
			}
			set
			{
				this.SetValByKey(NodeRefFuncAttr.Width,value);
			}
		}
		/// <summary>
		/// 默认盘旋图标
		/// </summary>
		public string DefaultIcon
		{
			get
			{
				if ( this.GetValStringByKey(NodeRefFuncAttr.DefaultIcon)=="")
					return "/images/AppIcon/NodeRefIcon/Default.gif";
				else
					return this.GetValStringByKey(NodeRefFuncAttr.DefaultIcon); 
			}
			set
			{
				this.SetValByKey(NodeRefFuncAttr.DefaultIcon,value);
			}
		}
		/// <summary>
		/// 默认得盘旋图标
		/// </summary>
		public string DefaultHover
		{
			get
			{
				if ( this.GetValStringByKey(NodeRefFuncAttr.DefaultHover)=="")
					return "/images/AppIcon/NodeRefIcon/DefaultHover.gif";
				else
					return this.GetValStringByKey(NodeRefFuncAttr.DefaultHover);

				 
			}
			set
			{
				this.SetValByKey(NodeRefFuncAttr.DefaultHover,value);
			}
		}
		/// <summary>
		/// 提示信息
		/// </summary>
		public string ToolTip
		{
			get
			{
				if ( this.GetValStringByKey(NodeRefFuncAttr.ToolTip)=="")
					return this.Name;

				return this.GetValStringByKey(NodeRefFuncAttr.ToolTip) ; 
			}
			set
			{
				this.SetValByKey(NodeRefFuncAttr.ToolTip,value);
			}
		}
		/// <summary>
		/// 送达期限
		/// </summary>
		public int TimeLimit
		{
			get
			{
				return this.GetValIntByKey(NodeRefFuncAttr.TimeLimit);
			}
		}
		#endregion

		#region 构造函数
		/// <summary>
		/// ss
		/// </summary>
		public NodeRefFunc(){}
		/// <summary>
		/// BookOID
		/// </summary>
		/// <param name="_oid"></param>
		public NodeRefFunc(int _oid) : base(_oid)
		{
		}

        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForAppAdmin();
                return uac;
            }
        }
		/// <summary>
		/// 重写基类方法
		/// </summary>
		public override Map EnMap
		{
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("WF_NodeRefFunc");
                map.EnDesc = "节点的相关功能";
                map.EnType = EnType.Admin;

                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBIntPKOID();
                map.AddTBString(NodeRefFuncAttr.NodeId, "0", "节点", true, false, 0, 200, 20);
                map.AddTBString(NodeRefFuncAttr.Name, "", "文书名称", true, false, 0, 200, 20);
                map.AddTBString(NodeRefFuncAttr.URL, "", "文件路径", true, true, 0, 200, 20);
                map.AddTBInt(NodeRefFuncAttr.TimeLimit, 3, "文书送达期限", true, false);
                map.AddTBString(NodeRefFuncAttr.FilePrix, null, "文号", true, false, 0, 50, 10);

                /*
                map.AddDDLSysEnum(NodeRefFuncAttr.RefFuncType,0,"类型",true,true,"RefFuncType");
                map.AddTBInt(NodeRefFuncAttr.Height,150,"高度",false,false);
                map.AddTBInt(NodeRefFuncAttr.Width,300,"宽度",false,false);
                map.AddTBString(NodeRefFuncAttr.DefaultIcon,"/images/AppIcon/NodeRefIcon/Default.gif","图标",false,false,0,200,20);
                map.AddTBString(NodeRefFuncAttr.DefaultHover,"/images/AppIcon/NodeRefIcon/DefaultHover.gif","盘旋图标",false,false,0,200,20);
                map.AddTBString(NodeRefFuncAttr.ToolTip,"相关功能","提示信息",false,false,0,300,20);
                */
                this._enMap = map;
                return this._enMap;
            }
		}
		#endregion 

		#region 重载基类方法
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected override bool beforeUpdateInsertAction()
		{            	 
			return  base.beforeUpdateInsertAction();			 
		}
		/// <summary>
		/// beforeInsert
		/// </summary>
		/// <returns></returns>
		protected override bool beforeInsert()
		{
			//this.NoticeNo=EnDA.GenerOID().ToString();			
			return base.beforeInsert();			
		}
		/// <summary>
		/// 这里要做的工作是,第1,从征收管理数软件中找.罚款信息.
		/// 是不是一致.
		/// 1, 纳税人, 罚款金额. 票据号码. 是不是有这一条记录.
		/// 如果没有就抛出异常.
		/// </summary>
		/// <returns></returns>
		protected override bool beforeUpdate()
		{
			//取出罚款num.
			NumCheck nc = new NumCheck(this.OID);
			//	nc.Num ;
			return base.beforeUpdate(); 
		}
		/// <summary>
		/// beforeDelete
		/// </summary>
		/// <returns></returns>
		protected override bool beforeDelete()
		{
			return base.beforeDelete(); 
		}
		#endregion 	
	}
	/// <summary>
	/// 节点的相关功能
	/// </summary>
	public class NodeRefFuncs: EntitiesOID
	{
		#region 构造
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new NodeRefFunc();
			}
		}
		/// <summary>
		/// 相关功能
		/// </summary>
		public NodeRefFuncs(){}

        public NodeRefFuncs(Node nd)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeRefFuncAttr.NodeId, nd.NodeID);
            if (nd.IsStartNode)
            {
                qo.addOr();
                qo.AddWhere(NodeRefFuncAttr.NodeId, 0);
            }
            qo.DoQuery();
        }
		/// <summary>
		/// 相关功能
		/// </summary>
		/// <param name="flowNo">flowNo</param>
        public NodeRefFuncs(string flowNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhereInSQL(NodeRefFuncAttr.NodeId, "SELECT NodeID from wf_node where fk_flow='" + flowNo + "' ");
            qo.DoQuery();
        }
		#endregion
	}
	
}
