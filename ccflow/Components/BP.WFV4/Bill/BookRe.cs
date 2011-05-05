using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;
using BP.Web;

namespace BP.WF
{
	 
	/// <summary>
	/// 文书属性
	/// </summary>
	public class BookReAttr:BookAttr
	{
		#region 属性
		/// <summary>
		/// 工作ID
		/// </summary>
		public const string WorkID="WorkID";
		/// <summary>
		/// 节点
		/// </summary>
		public const string FK_Node="FK_Node";
		/// <summary>
		/// 相关功能
		/// </summary>
		public const string FK_NodeRefFunc="FK_NodeRefFunc";
		/// <summary>
		/// 送达否
		/// </summary>
		public const string BookState="BookState";
		/// <summary>
		/// 发送时间
		/// </summary>
		public const string SendDateTime="SendDateTime";
		/// <summary>
		/// 退回时间
		/// </summary>
		public const string ReturnDateTime="ReturnDateTime";
		/// <summary>
		/// 纳税人编号
		/// </summary>
		public const string FK_Taxpayer="FK_Taxpayer";
		/// <summary>
		/// 名称
		/// </summary>
		public const string TaxpayerName="TaxpayerName";
		/// <summary>
		/// BookNo
		/// </summary>
		public const string BookNo="BookNo";
		/// <summary>
		/// 管理员
		/// </summary>
		public const string BookAdmin="BookAdmin";
		/// <summary>
		/// 记录时间．
		/// </summary>
		public const string RecordDateTime="RecordDateTime";
		/// <summary>
		/// 记录人．
		/// </summary>
		public const string Recorder="Recorder";
		/// <summary>
		/// 征收机关
		/// </summary>
		public const string FK_XJ="FK_XJ";
		/// <summary>
		/// 征收机关
		/// </summary>
		public const string FK_ZSJG="FK_ZSJG";
		#endregion
	}
	/// <summary>
	/// 文书
	/// </summary> 
	public class BookRe :Book
	{
		#region 基本属性

		#endregion

		#region 构造方法
		/// <summary>
		/// HisUAC
		/// </summary>
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				 
				uac.IsDelete=false;
				uac.IsInsert=false;
				uac.IsUpdate=true;
				uac.IsView=true;
				return uac;
			}
		}
		/// <summary>
		/// 条目
		/// </summary>
		public Book(){}
		/// <summary>
		/// mid
		/// </summary>
		/// <param name="mid">mid</param>
		public Book(int mid):base(mid){}
		#endregion 

		#region Map
		/// <summary>
		/// EnMap
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				Map map = new Map( "WF_Book" );
				map.DepositaryOfMap=Depositary.None;
				map.EnDesc="文书";
				map.AddTBIntPKOID();
				//map.AddTBInt(BookAttr.WorkID,0,"工作ID",false,true);
				//map.AddDDLEntities(BookAttr.FK_Node, 0 , DataType.AppInt,"节点名称",new Nodes(),NodeAttr.OID,NodeAttr.Name, false );
				map.AddDDLEntities(BookAttr.FK_NodeRefFunc,0, DataType.AppInt,"文书名称",new NodeRefFuncs(),NodeRefFuncAttr.OID,NodeRefFuncAttr.Name,false);
				map.AddTBString(BookAttr.BookNo,null,"编号",true,true,0,100,5);
				map.AddDDLSysEnum(BookAttr.BookState,0,"文书状态",true,true);
				map.AddTBDateTime(BookAttr.SendDateTime,"送达时间",true,true);
				//map.AddTBDateTime(BookAttr.ReturnDateTime,"归还时间",true,true);

				map.AddTBString(BookAttr.FK_Taxpayer,null,"纳税人编号",true,true,0,100,5);
				map.AddTBString(BookAttr.TaxpayerName,null,"纳税人名称",true,true,0,100,5);

				map.AddTBDate(BookAttr.RecordDateTime,"文书打印时间",true,true);
				//map.AddDDLEntitiesNoName(BookAttr.Recorder,Web.WebUser.No,"打印人",new EmpExts(),false);

				map.AddDDLEntitiesNoName(BookAttr.FK_ZSJG,TaxUser.FK_ZSJG,"征收机关",new BP.Tax.ZSJGs(),false);
				map.AddDDLEntitiesNoName(BookAttr.FK_XJ,TaxUser.FK_ZSJGOfXJ,"县局",new BP.Tax.XJs(),false);

				map.AddSearchAttr(BookAttr.FK_ZSJG);
				map.AddSearchAttr(BookAttr.Recorder);
				map.AddSearchAttr(BookAttr.BookState);
				map.AttrsOfSearch.AddFromTo("日期",BookAttr.RecordDateTime, DateTime.Now.AddMonths(-1).ToString(DataType.SysDataTimeFormat), DA.DataType.CurrentDataTime,6);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		 
		 
	}
	/// <summary>
	/// 条目
	/// </summary>
	public class BookRes :EntitiesOID
	{
		#region 构造方法属性
		/// <summary>
		/// Books
		/// </summary>
		public BookRes(){}
		#endregion 

		#region 属性
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new BookRe();
			}
		}
		#endregion
	}
}
