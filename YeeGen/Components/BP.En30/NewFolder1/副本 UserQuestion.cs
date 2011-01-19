using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
//using BP.ZHZS.Base;

namespace BP.Sys
{
	/// <summary>
	/// 用户问题
	/// </summary>
	public class UserQuestionAttr  //EntityClassNameAttr
	{
		/// <summary>
		/// OID
		/// </summary>
		public const string OID="OID";
		/// <summary>
		/// 发送人
		/// </summary>
		public const string Sender="Sender";
		/// <summary>
		/// 报告人
		/// </summary>
		public const string Reporter="Reporter";
		/// <summary>
		/// 发送时间
		/// </summary>
		public const string SendDateTime="SendDateTime";
		/// <summary>
		/// 标题
		/// </summary>
		public const string Title="Title";
		/// <summary>
		/// 内容
		/// </summary>
		public const string Docs="Docs";
		/// <summary>
		/// 
		/// </summary>
		public const string ReDocs="ReDocs";

	}
	/// <summary>
	/// 用户问题
	/// </summary> 
	public class UserQuestion:EntityOID 
	{
		#region 基本属性		 
		/// <summary>
		/// 发送人
		/// </summary>
		public  string  Sender
		{
			get
			{
				return this.GetValStringByKey(UserQuestionAttr.Sender);
			}
			set
			{
				this.SetValByKey(UserQuestionAttr.Sender,value);
			}
		}
		/// <summary>
		/// 发送日期时间
		/// </summary>
		public  string  SendDateTime
		{
			get
			{
				return this.GetValStringByKey(UserQuestionAttr.SendDateTime);
			}
			set
			{
				this.SetValByKey(UserQuestionAttr.SendDateTime,value);
			}
		}
		/// <summary>
		/// 标题
		/// </summary>
		public  string  Title
		{
			get
			{
				return this.GetValStringByKey(UserQuestionAttr.Title);
			}
			set
			{
				this.SetValByKey(UserQuestionAttr.Title,value);
			}
		}
		/// <summary>
		/// 内容
		/// </summary>
		public  string  Docs
		{
			get
			{
				return this.GetValStringByKey(UserQuestionAttr.Docs);
			}
			set
			{
				this.SetValByKey(UserQuestionAttr.Docs,value);
			}
		}		 
		#endregion 

		#region 构造方法
		/// <summary>
		/// 用户问题
		/// </summary>
		public UserQuestion(){}
		/// <summary>
		/// 用户问题
		/// </summary>
		/// <param name="oid"></param>
		public UserQuestion(int oid ) : base(oid)
		{
		}
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				if (BP.Web.TaxUser.No=="admin" || BP.Web.TaxUser.No.ToString().IndexOf("8888") > -1 )
				{
					uac.IsView=true;
					uac.IsDelete=true;
					uac.IsInsert=true;
					uac.IsUpdate=true;
					uac.IsAdjunct=true;
				}
				return uac;
			}
		}
		public  Map EnMap_del
		{
			get
			{
				if (this._enMap!=null)
					return this._enMap;
				Map map = new Map("Sys_UserQuestion");
				map.EnDesc="用户问题";
				map.EnType=EnType.Admin;
				map.AddTBIntPKOID();

				map.AddDDLEntitiesPK(UserQuestionAttr.Reporter,null,"发送人ID",new Emps(),false);
				map.AddTBString(UserQuestionAttr.Sender,null,"发送人",true,false,0,500,10);
				map.AddTBString(UserQuestionAttr.Title,null,"标题",true,false,0,300,20);
				map.AddTBDateTime(UserQuestionAttr.SendDateTime,"发送时间",true,false);

				map.AddTBStringDoc(UserQuestionAttr.Docs,null,"问题内容",true,false);
				map.AddTBStringDoc(UserQuestionAttr.ReDocs,null,"回答",true,false);

				map.AddSearchAttr(UserQuestionAttr.Reporter);
				this._enMap=map;
				return this._enMap;
			}
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
				Map map = new Map("Sys_UserQuestion");
				map.EnDesc="用户问题";
				map.EnType=EnType.Admin;
				map.AddTBIntPKOID();


				map.AddDDLEntitiesPK(UserQuestionAttr.Reporter,null,"发送人ID",new Emps(),false);
				map.AddTBString(UserQuestionAttr.Sender,null,"发送人",true,false,0,500,10);
				map.AddTBString(UserQuestionAttr.Title,null,"标题",true,false,0,300,20);
				map.AddTBDateTime(UserQuestionAttr.SendDateTime,"发送时间",true,false);

				map.AddTBStringDoc(UserQuestionAttr.Docs,null,"问题内容",true,false);
				map.AddTBStringDoc(UserQuestionAttr.ReDocs,null,"回答",true,false);

				map.AddSearchAttr(UserQuestionAttr.Reporter);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// 用户问题
	/// </summary> 
	public class UserQuestions : EntitiesOID  //EntitiesNoName
	{
		#region 刷新
		/// <summary>
		/// 刷新
		/// </summary>
		public static void RefreshUAC()
		{			
		}
		#endregion		 
		
		#region 构造函数
		/// <summary>
		/// 关于实体访问的构造
		/// </summary>
		public UserQuestions(){}
		/// <summary>
		/// New entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new UserQuestion();
			}
		}
		#endregion
	
	}
}
