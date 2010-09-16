using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// 试卷阅读题设计
	/// </summary>
	public class PaperVSRCAttr  
	{
		#region 基本属性
		/// <summary>
		/// 试卷
		/// </summary>
		public const  string FK_Paper="FK_Paper";
		/// <summary>
		/// 阅读题
		/// </summary>
		public const  string FK_RC="FK_RC";
		/// <summary>
		/// 分数
		/// </summary>
		public const  string Cent="Cent";
		#endregion	
	}
	/// <summary>
	/// 试卷阅读题设计 的摘要说明
	/// </summary>
	public class PaperVSRC :Entity
	{
		#region 基本属性
		/// <summary>
		///阅读题
		/// </summary>
		public string FK_RC
		{
			get
			{
				return this.GetValStringByKey(PaperVSRCAttr.FK_RC);
			}
			set
			{
				SetValByKey(PaperVSRCAttr.FK_RC,value);
			}
		}
		/// <summary>
		///设卷
		/// </summary>
		public string FK_Paper
		{
			get
			{
				return this.GetValStringByKey(PaperVSRCAttr.FK_Paper);
			}
			set
			{
				SetValByKey(PaperVSRCAttr.FK_Paper,value);
			}
		}
		#endregion

		#region 扩展属性
//		/// <summary>
//		/// 它的明细
//		/// </summary>
//		public RCs HisRCs
//		{
//			get
//			{
//				return new RCs(
//			}
//		}
		#endregion		

		#region 构造函数
		/// <summary>
		/// HisUAC
		/// </summary>
		public override UAC HisUAC
		{
			get
			{
				UAC uc = new UAC();
				uc.OpenForSysAdmin();
				return uc;
			}
		}
		/// <summary>
		/// 阅读题分数设计
		/// </summary> 
		public PaperVSRC()
		{
		}
		public PaperVSRC(string paper,string Equestion)
		{
			this.FK_Paper = paper;
			this.FK_RC = Equestion;
			try
			{
				this.Retrieve();
			}
			catch
			{
				this.Insert();
			}
		}
		/// <summary>
		/// 重写基类方法
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				
				Map map = new Map("GTS_PaperVSRC");
				map.EnDesc="阅读题分数设计";	
				map.EnType=EnType.Dot2Dot;
		 
				map.AddDDLEntitiesPK(PaperVSRCAttr.FK_Paper,"0001","试卷",new Papers(),false);
				map.AddDDLEntitiesPK(PaperVSRCAttr.FK_RC,null,"阅读题",new RCs(),false);
				//map.AddTBInt(PaperVSRCAttr.Cent,1,"分",true,false);
				//map.AddSearchAttr(EmpDutyAttr.FK_Emp);
				//map.AddSearchAttr(EmpDutyAttr.FK_Duty);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion

		#region 重载基类方法

		protected override void afterInsert()
		{
			RC rc = new RC(this.FK_RC);
			RCDtls rcs = rc.HisRCDtls;
			foreach(RCDtl dtl in rcs)
			{
				PaperVSRCDtl en = new PaperVSRCDtl();
				en.Cent=0;

				QueryObject qo = new QueryObject(en);
				qo.AddWhere(PaperVSRCDtlAttr.FK_Paper, this.FK_Paper);
				qo.addAnd();
				qo.AddWhere(PaperVSRCDtlAttr.FK_RCDtl,dtl.OID);
				if (qo.DoQuery()==1)
				{
					continue;
				}
				else
				{
					en.FK_Paper=this.FK_Paper;
					en.FK_RCDtl= dtl.OID;
					en.FK_RC=this.FK_RC;
					en.Insert();
				}
				 
			}

			base.afterInsert ();

		}
		protected override bool beforeDelete()
		{
			DBAccess.RunSQL("delete GTS_PaperVSRCDtl where fk_paper='"+this.FK_Paper+"' and fk_rc='"+this.FK_RC+"'");
			return base.beforeDelete ();
		}
		#endregion 
	
	}
	/// <summary>
	/// 试卷阅读题设计 
	/// </summary>
	public class PaperVSRCs : Entities
	{
		#region 构造
		/// <summary>
		/// 试卷阅读题设计
		/// </summary>
		public PaperVSRCs(){}

		/// <summary>
		///  试卷阅读题设计
		/// </summary>
		/// <param name="fk_paper"></param>
		public PaperVSRCs(string fk_paper)
		{
			QueryObject qo =new QueryObject(this);
			qo.AddWhere( PaperVSRCAttr.FK_Paper, fk_paper);
			qo.DoQuery();
		}

		#endregion

		#region 方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperVSRC();
			}
		}	
		#endregion 

		#region 查询方法
		public RCs GetRCs(string fk_paper)
		{

			RCs rcs = new RCs();
			QueryObject qo =new QueryObject(rcs);
			qo.AddWhereInSQL( RCAttr.No , "SELECT FK_RC FROM GTS_PaperVSRCs  WHERE FK_Paper='"+fk_paper+"'");

			qo.DoQuery();
			
			return rcs;

		}
		 
		#endregion
	}
	
}
