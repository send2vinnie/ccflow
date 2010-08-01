using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;
using BP.Port;

namespace BP.TA
{
 	/// <summary>
	/// 回复节点状态
	/// </summary>
	public enum ReWorkState
	{
		/// <summary>
		/// 无
		/// </summary>
		None,
		/// <summary>
		/// 执行中(等待父节点签收)
		/// </summary>
		Sending,
		/// <summary>
		/// 未认可(这个时间执行人可以签收下来, 节点状态设置为 None,
		/// 重新等待执行人去做。这时间的状态为  UnRatify -> None -> Sending )
		/// </summary>
		UnRatify,
		/// <summary>
		/// 认可（工作已经结束）
		/// </summary>
		Ratify
	}
	/// <summary>
	/// 回复节点属性
	/// </summary>
	public class ReWorkAttr:WorkDtlBaseAttr
	{
		/// <summary>
		/// ReWorkState
		/// </summary>
		public const string ReWorkState="ReWorkState";
		/// <summary>
		/// 回复消息
		/// </summary>
		public const string Title="Title";
		/// <summary>
		/// 回复消息
		/// </summary>
		public const string Doc="Doc";
		/// <summary>
		/// 接受人动作时间(读取&表态)
		/// </summary>
		public const string SenderActionDateTime="SenderActionDateTime"; 
		/// <summary>
		/// 接受人意见
		/// </summary>
		public const string SenderNote="SenderNote";
	}
	/// <summary>
	/// 回复节点
	/// </summary> 
	public class ReWork : WorkDtlBase
	{
		#region 基本属性
		/// <summary>
		/// Title
		/// </summary>
		public string Title
		{
			get
			{
				return  this.GetValStringByKey(ReWorkAttr.Title);
			}
			set
			{
				SetValByKey(ReWorkAttr.Title,value);
			}
		}
		public string Doc
		{
			get
			{
				return this.GetValStringByKey(ReWorkAttr.Doc);
			}
			set
			{
				SetValByKey(ReWorkAttr.Doc,value);
			}
		}
		public string DocHtml
		{
			get
			{
				return this.GetValHtmlStringByKey(ReWorkAttr.Doc);
			}
		}
		/// <summary>
		/// 回复工作节点状态
		/// </summary>
		public ReWorkState ReWorkState
		{
			get
			{
				return (ReWorkState)this.GetValIntByKey(ReWorkAttr.ReWorkState);
			}
			set
			{
				this.SetValByKey(ReWorkAttr.ReWorkState, (int)value);
			}
		}
		/// <summary>
		/// 接受人Note
		/// </summary>
		public string SenderNote 
		{
			get
			{
				return this.GetValStringByKey(ReWorkAttr.SenderNote);
			}
			set
			{
				SetValByKey(ReWorkAttr.SenderNote,value);
			}
		}
		/// <summary>
		/// 接受人动作时间
		/// </summary>
		public string SenderActionDateTime 
		{
			get
			{
				return this.GetValStringByKey(ReWorkAttr.SenderActionDateTime);
			}
			set
			{
				SetValByKey(ReWorkAttr.SenderActionDateTime,value);
			}
		}
		/// <summary>
		/// 时间
		/// </summary>
		public DateTime SenderActionDateTime_S
		{
			get
			{
				return this.GetValDateTime(ReWorkAttr.SenderActionDateTime);
			}
		}
		#endregion
 
		#region 构造函数
		/// <summary>
		/// 回复节点
		/// </summary>
		public ReWork()
		{
		}
		/// <summary>
		/// 回复节点
		/// </summary>
		/// <param name="_No">No</param>
		public ReWork(int oid):base(oid)
		{
		}
		/// <summary>
		/// Map
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;

				Map map = new Map("TA_ReWork");
				map.EnDesc="回复节点";
				map.AddTBIntPKOID();
				map.AddTBInt(ReWorkAttr.ParentID,0,"父节点ID",true,true);
                map.AddDDLSysEnum(ReWorkAttr.ReWorkState, (int)ReWorkState.None, "状态", true, false, "ReWorkState", "@0=无@1=执行中@2=未认可@3=认可");

				map.AddDDLEntities(ReWorkAttr.Executer,null,"回复人", new Emps(),false );				 
				map.AddTBDateTime(ReWorkAttr.DTOfActive,"活动时间(回复&撤消回复)",false,false );

				map.AddTBString(WorkAttr.Title,null,"任务标题",true,false,0,500,15);
				map.AddTBString(WorkAttr.Doc,null,"任务内容",true,false,0,4000,15);

				map.AddDDLEntities(ReWorkAttr.Sender,null,"按受人", new Emps(),false );
				map.AddTBString(ReWorkAttr.SenderNote,null,"接受人备注",true,false,0,500,15);
				map.AddTBDateTime(ReWorkAttr.SenderActionDateTime,"活动时间(读取&认可动作)",false,false );
				map.AddTBInt(ReWorkAttr.AdjunctNum,0,"附件个数",true,true);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

	}
	/// <summary>
	/// 回复节点s
	/// </summary> 
	public class ReWorks: Entities
	{
		/// <summary>
		/// 获取entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new ReWork();
			}
		}
		/// <summary>
		/// ReWorks
		/// </summary>
		public ReWorks()
		{
		}


		/// <summary>
		///  
		/// </summary>
		/// <param name="emp"></param>
		/// <param name="ny"></param>		
		public ReWorks(string userNo,string ny)
		{
			QueryObject qo = new QueryObject(this);
			qo.addLeftBracket();
			qo.AddWhere(ReWorkAttr.Executer,userNo);
			qo.addOr();
			qo.AddWhere(ReWorkAttr.Sender,userNo);
			qo.addRightBracket();
			qo.addAnd();
			qo.AddWhere(ReWorkAttr.DTOfActive, " LIKE ", ny+"%");
			qo.DoQuery();
		}

		/// <summary>
		/// 需要我审核回复的工作。
		/// </summary>
		/// <returns></returns>
		public int Reing()
		{
			QueryObject qo = new QueryObject(this);

			qo.AddWhere(ReWorkAttr.Sender ,WebUser.No);
			qo.addAnd();
			qo.AddWhere(ReWorkAttr.ReWorkState,(int)ReWorkState.Sending);

			qo.addOrderBy( ReWorkAttr.DTOfActive );
			return qo.DoQuery();
		}
	}
}
 