using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// 试卷阅读题设计
	/// </summary>
	public class WorkVSRCAttr  :WorkVSBaseAttr
	{
		#region 基本属性
		/// <summary>
		/// 阅读题
		/// </summary>
		public const  string FK_RC="FK_RC";
		#endregion	
	}
	/// <summary>
	/// 试卷阅读题设计 的摘要说明
	/// </summary>
	public class WorkVSRC :WorkVSBase
	{
		#region 基本属性
		/// <summary>
		///阅读题
		/// </summary>
		public string FK_RC
		{
			get
			{
				return this.GetValStringByKey(WorkVSRCAttr.FK_RC);
			}
			set
			{
				SetValByKey(WorkVSRCAttr.FK_RC,value);
			}
		}
		#endregion

		 

		#region 构造函数
		/// <summary>
		/// 阅读题分数设计
		/// </summary> 
		public WorkVSRC()
		{
		}
		public WorkVSRC(string Work,string Equestion)
		{
			this.FK_Work = Work;
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
				
				Map map = new Map("GTS_WorkVSRC");
				map.EnDesc="阅读题分数设计";	
				map.EnType=EnType.Dot2Dot;
		 
				map.AddDDLEntitiesPK(WorkVSRCAttr.FK_Work,"0001","试卷",new WorkFixDesigns(),false);
				map.AddDDLEntitiesPK(WorkVSRCAttr.FK_RC,null,"阅读题",new RCs(),false);
				//map.AddTBInt(WorkVSRCAttr.Cent,1,"分",true,false);
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
				WorkVSRCDtl en = new WorkVSRCDtl();
				en.Cent=0;

				QueryObject qo = new QueryObject(en);
				qo.AddWhere(WorkVSRCDtlAttr.FK_Work, this.FK_Work);
				qo.addAnd();
				qo.AddWhere(WorkVSRCDtlAttr.FK_RCDtl,dtl.OID);
				if (qo.DoQuery()==1)
				{
					continue;
				}
				else
				{
					en.FK_Work=this.FK_Work;
					en.FK_RCDtl= dtl.OID;
					en.FK_RC=this.FK_RC;
					en.Insert();
				}
			}

			base.afterInsert ();

		}
		protected override bool beforeDelete()
		{
			DBAccess.RunSQL("delete GTS_WorkVSRCDtl where fk_Work='"+this.FK_Work+"' and fk_rc='"+this.FK_RC+"'");
			return base.beforeDelete ();
		}
		#endregion 
	
	}
	/// <summary>
	/// 试卷阅读题设计 
	/// </summary>
	public class WorkVSRCs : WorkVSBases
	{
		#region 构造
		/// <summary>
		/// 试卷阅读题设计
		/// </summary>
		public WorkVSRCs(){}

		/// <summary>
		///  试卷阅读题设计
		/// </summary>
		/// <param name="fk_Work"></param>
		public WorkVSRCs(string fk_Work)
		{
			QueryObject qo =new QueryObject(this);
			qo.AddWhere( WorkVSRCAttr.FK_Work, fk_Work);
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
				return new WorkVSRC();
			}
		}	
		#endregion 

		#region 查询方法
		public RCs GetRCs(string fk_Work)
		{

			RCs rcs = new RCs();
			QueryObject qo =new QueryObject(rcs);
			qo.AddWhereInSQL( RCAttr.No , "SELECT FK_RC FROM GTS_WorkVSRCs  WHERE FK_Work='"+fk_Work+"'");
			qo.DoQuery();
			return rcs;
		}
		#endregion
	}
	
}
