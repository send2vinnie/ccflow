using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.YG
{
	public class CBuessAttr:EntityNoNameAttr
	{
		/// <summary>
		/// 创建人
		/// </summary>
		public const string Cent="Cent";
		/// <summary>
		/// 管理员
		/// </summary>
		public const string FK_Member="FK_Member";
		public const string FK_CBuess="FK_CBuess";
		public const string RefObj="RefObj";
		public const string Note="Note";
		public const string RDT="RDT";

	}
	/// <summary>
	/// 积分
	/// </summary>
	public class CBuess :Entity
	{
		#region cent .
		public int Cent
		{
			get
			{
				return this.GetValIntByKey(CBuessAttr.Cent);
			}
			set
			{
				this.SetValByKey(CBuessAttr.Cent,value);
			}
		}
		public string RefObj
		{
			get
			{
				return this.GetValStringByKey(CBuessAttr.RefObj);
			}
			set
			{
				this.SetValByKey(CBuessAttr.RefObj,value);
			}
		}
		public string FK_CBuess
		{
			get
			{
				return this.GetValStringByKey(CBuessAttr.FK_CBuess);
			}
			set
			{
				this.SetValByKey(CBuessAttr.FK_CBuess,value);
			}
		}
		public string FK_Member
		{
			get
			{
				return this.GetValStringByKey(CBuessAttr.FK_Member);
			}
			set
			{
				this.SetValByKey(CBuessAttr.FK_Member,value);
			}
		}
		public string Note
		{
			get
			{
				return this.GetValStringByKey(CBuessAttr.Note);
			}
			set
			{
				this.SetValByKey(CBuessAttr.Note,value);
			}
		}
		public string RDT
		{
			get
			{
				return this.GetValStringByKey(CBuessAttr.RDT);
			}
			set
			{
				this.SetValByKey(CBuessAttr.RDT,value);
			}
		}
		 
		#endregion

		#region 实现基本的方法
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.OpenForSysAdmin();
				return uac;
			}
		}
		/// <summary>
		/// FLinkMap
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				Map map = new Map();

				#region 基本属性 
				map.EnDBUrl =new DBUrl(DBUrlType.AppCenterDSN) ; 
				map.PhysicsTable="YG_CBuess";
				map.AdjunctType = AdjunctType.AllType ;  
				map.DepositaryOfMap=Depositary.Application; 
				map.DepositaryOfEntity=Depositary.None;
				map.EnDesc="积分";
				map.EnType=EnType.App;

				map.AddTBStringPK(CBuessAttr.FK_Member,null,"FK_Member",true,false,1,100,100);
				map.AddTBStringPK(CBuessAttr.FK_CBuess,null,"FK_CBuess",true,false,1,100,100);
				map.AddTBStringPK(CBuessAttr.RefObj,null,"RefObj",true,false,1,100,100);

				map.AddTBInt(CBuessAttr.Cent,0,"Cent",true,false);
				map.AddTBString(CBuessAttr.Note,null,"note",true,false,0,500,10);
				map.AddTBDate(CBuessAttr.RDT,null,"RDT",true,false);

				#endregion

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		#region 构造方法
		/// <summary>
		/// 积分
		/// </summary>
		public CBuess(){}
		#endregion 
	}
	/// <summary>
	/// 积分
	/// </summary>
	public class CBuesss :Entities
	{
		#region 构造
		/// <summary>
		/// 积分s
		/// </summary>
		public CBuesss(){}
		/// <summary>
		/// 积分
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CBuess();
			}
		}
		#endregion
	}
}
