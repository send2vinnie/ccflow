using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Web;

namespace BP.WF
{
	/// <summary>
	/// 流程属性
	/// </summary>
    public class WinFlowAttr : EntityNoNameAttr
    {
        /// <summary>
        /// 流程类别
        /// </summary>
        public const string FK_WinFlowSort = "FK_WinFlowSort";
        /// <summary>
        /// 建立的日期。
        /// </summary>
        public const string CreateDate = "CreateDate";
        /// <summary>
        /// 建立人
        /// </summary>
        public const string Creater = "Creater";
    }
	/// <summary>
	/// 流程
	/// 记录了流程的信息．
	/// 流程的编号，名称，建立时间．
	/// </summary>
	public class WinFlow :EntityNoName
	{
		#region 基本属性
		/// <summary>
		/// 流程类别
		/// </summary>
		public string FK_WinFlowSort
		{
			get
			{
				return this.GetValStringByKey(WinFlowAttr.FK_WinFlowSort);
			}
			set
			{
				this.SetValByKey(WinFlowAttr.FK_WinFlowSort,value);
			}
		}
		/// <summary>
		/// 建立人
		/// </summary>
		public int Creater
		{
			get
			{
				return this.GetValIntByKey(WinFlowAttr.Creater);
			}
			set
			{
				this.SetValByKey(WinFlowAttr.Creater,value);
			}
		}
		/// <summary>
		/// 建立时间
		/// </summary>
		public string CreateDate
		{
			get
			{
				return this.GetValAppDateByKey(WinFlowAttr.CreateDate);
			}
			set
			{
				this.SetDateValByKey(WinFlowAttr.CreateDate,value);
			}
		}		
		#endregion

		#region 扩展属性
		/// <summary>
		/// 是不是一条自动运行的工作流程.
		/// </summary>
		public bool IsPCWinFlow
		{
			get
			{
				foreach(Node nd in this.HisNodes)
				{
					if (nd.IsPCNode)
						return true;
				}
				return false;
			}
		}
		/// <summary>
		/// 节点
		/// </summary>
		private Nodes  _HisNodes=null;
		/// <summary>
		/// 他的节点集合.
		/// </summary>
		public Nodes HisNodes
		{
			get
			{
				if (this._HisNodes==null)					 
					_HisNodes =new Nodes(this.No);				 
				return _HisNodes;
			}
		}
		/// <summary>
		/// 他的 Start 节点
		/// </summary>
		public Node HisStartNode
		{
			get
			{
				foreach(Node nd in this.HisNodes)
				{
					if (nd.IsStartNode)
						return nd;
				}
				throw new Exception("没有找到他的开始节点,工作流程定义错误.");
			}
		}
		/// <summary>
		/// 他的 end 节点
		/// 可能有很多的结束节点.
		/// </summary>
		public Nodes HisEndNodes
		{
			get
			{
				Nodes ens = new Nodes();
				foreach(Node nd in this.HisNodes)
				{
					if (nd.IsEndNode)
						ens.AddEntity(nd);
				}
				if (ens.Count==0)
					throw new Exception("没有找到他的End节点,工作流程定义错误.");
				else
					return ens;
			}
		} 
		 
	
		#endregion
		

		#region 构造方法
		/// <summary>
		/// 事务
		/// </summary>
		public WinFlow()
		{
			//this.No =this.GenerNewNo;
		}
		/// <summary>
		/// 事务
		/// </summary>
		/// <param name="_No">编号</param>
		public WinFlow(string _No ): base(_No){}
		/// <summary>
		/// 重写基类方法
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				 
				Map map = new Map("WF_Flow");			 
				map.EnDesc="流程信息";
				map.AddTBStringPK(WinFlowAttr.No,null,"流程序号",false,true,3,3,3);
				map.AddTBString(WinFlowAttr.Name,null,"流程名称",true,false,0,20,10);
			//	map.AddDDLEntitiesNoName(WinFlowAttr.FK_WinFlowSort,"001","流程类别",new WinFlowSorts(),true);			 
			//	map.AddDDLEntities(WinFlowAttr.Creater,WebUser.OID,DataType.AppInt,"建立人",new Emps(),"OID","Name",true);
			//	map.AddTBDate(WinFlowAttr.CreateDate , DateTime.Now.ToString("yyyy-MM-dd hh-mm"),"建立时间",true,true);

			//	map.AddSearchAttrsByKey(WinFlowAttr.FK_WinFlowSort);
			//	map.AddSearchAttrsByKey(WinFlowAttr.Creater);
				this._enMap=map;

               
			 

				return this._enMap;
			}
		}
		#endregion 

		#region  公共方法
		/// <summary>
		/// 执行保存前的检查
		/// </summary>
		/// <returns>定义的工作流程是不是合理</returns>
		public bool DoCheck()
		{
			// 1 判断是不是有开始节点?
			// 2 判断条件
			return true;
		}
		#endregion
	}

	/// <summary>
	/// 流程集合
	/// </summary>
	public class WinFlows : EntitiesNoName
	{
		#region 构造方法
		/// <summary>
		/// 事务集合
		/// </summary>
		public WinFlows(){}
		/// <summary>
		/// 根据事务类别生成他的 事务集合.
		/// </summary>
		/// <param name="FK_WinFlowSort"></param>
		public WinFlows(string FK_WinFlowSort)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WinFlowAttr.FK_WinFlowSort,FK_WinFlowSort);
			qo.DoQuery();
			return;
		}		
		#endregion

		#region 得到实体
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WinFlow();
			}
		}
		#endregion
	}	 
}

