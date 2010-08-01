using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;
using BP.Port;

namespace BP.TA
{
 	/// <summary>
	/// 退回节点状态
	/// </summary>
	public enum ReturnWorkState
	{
		/// <summary>
		/// 无
		/// </summary>
		None,
		/// <summary>
		/// 发送中
		/// </summary>
		Sending,
		/// <summary>
		/// 未认可
		/// </summary>
		UnRatify,
		/// <summary>
		/// 认可
		/// </summary>
		Ratify
	}
	/// <summary>
	/// 退回节点属性
	/// </summary>
	public class ReturnWorkAttr:WorkDtlBaseAttr
	{
		/// <summary>
		/// 退回原因
		/// </summary>
		public const string ReturnReason="ReturnReason";
		/// <summary>
		/// 接受人意见
		/// </summary>
		public const string SenderNote="SenderNote";
		 
		/// <summary>
		/// 退回节点状态
		/// </summary>
		public const string ReturnWorkState="ReturnWorkState";
	}
	/// <summary>
	/// 退回节点
	/// </summary> 
	public class ReturnWork : WorkDtlBase
	{
		#region 扩展 属性
	 
		#endregion

		#region 基本属性
		/// <summary>
		/// 退回原因
		/// </summary>
		public string ReturnReason 
		{
			get
			{
				string str= this.GetValHtmlStringByKey(ReturnWorkAttr.ReturnReason);
				if (str=="")
					str="您好：\n\n   由于以下原因，我需要将此工作退回给您。\n\n\n 致! \n\n    "+WebUser.Name+"\n    "+DataType.CurrentDataTimeCN;
				return DataType.Html2Text(str);
			}
			set
			{
				SetValByKey(ReturnWorkAttr.ReturnReason,value);
			}
		}
		public string ReturnReasonHtml
		{
			get
			{
				string str= this.GetValHtmlStringByKey(ReturnWorkAttr.ReturnReason);
				if (str=="")
					str="您好：\n\n   由于以下原因，我需要将此工作退回给您。\n\n\n 致! \n\n    "+WebUser.Name+"\n    "+DataType.CurrentDataTimeCN;
				return str;
				//return DataType.Html2Text(str);
			}
		}
		 
		/// <summary>
		/// 接受人Note
		/// </summary>
		public string SenderNote 
		{
			get
			{
				return this.GetValStringByKey(ReturnWorkAttr.SenderNote);
			}
			set
			{
				SetValByKey(ReturnWorkAttr.SenderNote,value);
			}
		}
		public ReturnWorkState ReturnWorkState
		{
			get
			{
				return (ReturnWorkState)this.GetValIntByKey(ReturnWorkAttr.ReturnWorkState);
			}
			set
			{
				this.SetValByKey(ReturnWorkAttr.ReturnWorkState,(int)value);
			}
		}
		public string ReturnWorkStateText
		{
			get
			{
				return this.GetValRefTextByKey(ReturnWorkAttr.ReturnWorkState);
			}
		}
		#endregion
 
		#region 构造函数
		/// <summary>
		/// 退回节点
		/// </summary>
		public ReturnWork()
		{
		  
		}
		/// <summary>
		/// 退回节点
		/// </summary>
		/// <param name="_No">No</param>
		public ReturnWork(int oid):base(oid)
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

				Map map = new Map("TA_ReturnWork");
				map.EnDesc="退回节点";
				map.AddTBIntPKOID();
				map.AddTBInt(ReturnWorkAttr.ParentID,0,"父节点ID",true,true);
                map.AddDDLSysEnum(ReturnWorkAttr.ReturnWorkState, (int)ReturnWorkState.None, "状态", true, false, "ReturnWorkState", "@0=无@1=发送中@2=未认可@3=认可");
				map.AddDDLEntities(ReturnWorkAttr.Executer,null,"执行人", new Emps(),false );
				map.AddTBString(ReturnWorkAttr.ReturnReason,null,"退回原因",true,false,0,500,15);	
				map.AddTBDateTime(ReturnWorkAttr.DTOfActive,"活动时间(退回&撤消退回)",false,false );
				map.AddDDLEntities(ReturnWorkAttr.Sender,null,"发送人", new Emps(),false );
				map.AddTBString(ReturnWorkAttr.SenderNote,null,"发送人备注",true,false,0,500,15);	
				map.AddTBInt(ReturnWorkAttr.AdjunctNum,0,"附件个数",true,true);

				
 
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// 退回节点s
	/// </summary> 
	public class ReturnWorks: Entities
	{
		public int Returning()
		{
			QueryObject qo = new QueryObject(this);

			qo.AddWhere(ReturnWorkAttr.Sender ,WebUser.No);
			qo.addAnd();
			qo.AddWhere(ReturnWorkAttr.ReturnWorkState,(int)ReturnWorkState.Sending);

			qo.addOrderByDesc( ReturnWorkAttr.DTOfActive );
			return qo.DoQuery();
		}
		/// <summary>
		/// 获取entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new ReturnWork();
			}
		}
		/// <summary>
		/// ReturnWorks
		/// </summary>
		public ReturnWorks()
		{

		}
		public ReturnWorks(string userNo,string ny)
		{
			QueryObject qo = new QueryObject(this);
			qo.addLeftBracket();
			qo.AddWhere(ReturnWorkAttr.Executer,userNo);
			qo.addOr();
			qo.AddWhere(ReturnWorkAttr.Sender,userNo);
			qo.addRightBracket();
			qo.addAnd();
			qo.AddWhere(ReturnWorkAttr.DTOfActive, " LIKE ", ny+"%");
			qo.DoQuery();
		}
		
	}
}
 