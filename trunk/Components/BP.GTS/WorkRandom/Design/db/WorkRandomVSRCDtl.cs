using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.En.Base;

namespace BP.GTS
{
	
	/// <summary>
	/// 阅读题设计
	/// </summary>
	public class WorkRandomVSRCDtlAttr  
	{
		#region 基本属性
		/// <summary>
		/// 试卷
		/// </summary>
		public const  string FK_Work="FK_Work";
		/// <summary>
		/// FK_RC
		/// </summary>
		public const  string FK_RC="FK_RC";
		/// <summary>
		/// 阅读题
		/// </summary>
		public const  string FK_RCDtl="FK_RCDtl";
		/// <summary>
		/// 分数
		/// </summary>
		public const  string Cent="Cent";
		#endregion	
	}
	/// <summary>
	/// 阅读题设计 的摘要说明。
	/// </summary>
	public class WorkRandomVSRCDtl :Entity
	{
		
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.OpenForAppAdmin();
				return uac;
			}
		}

		#region 基本属性
		/// <summary>
		///阅读题
		/// </summary>
		public int FK_RCDtl
		{
			get
			{
				return this.GetValIntByKey(WorkRandomVSRCDtlAttr.FK_RCDtl);
			}
			set
			{
				SetValByKey(WorkRandomVSRCDtlAttr.FK_RCDtl,value);
			}
		}		  
		public RCDtl HisRCDtl
		{
			get
			{
				return new RCDtl(this.FK_RCDtl);
			}
		}
		public string FK_RCDtlText
		{
			get
			{
				return this.GetValRefTextByKey(WorkRandomVSRCDtlAttr.FK_RCDtl);
			}
			 
		}
		/// <summary>
		///设卷
		/// </summary>
		public string FK_Work
		{
			get
			{
				return this.GetValStringByKey(WorkRandomVSRCDtlAttr.FK_Work);
			}
			set
			{
				SetValByKey(WorkRandomVSRCDtlAttr.FK_Work,value);
			}
		}
		/// <summary>
		/// FK_RC 
		/// </summary>
		public string FK_RC
		{
			get
			{
				return this.GetValStringByKey(WorkRandomVSRCDtlAttr.FK_RC);
			}
			set
			{
				SetValByKey(WorkRandomVSRCDtlAttr.FK_RC,value);
			}
		}
		/// <summary>
		/// 分数
		/// </summary>
		public int Cent
		{
			get
			{
				return this.GetValIntByKey(WorkRandomVSRCDtlAttr.Cent);
			}
			set
			{
				SetValByKey(WorkRandomVSRCDtlAttr.Cent,value);
			}
		}
		#endregion

		#region 扩展属性
		 
		#endregion		

		#region 构造函数
		/// <summary>
		/// 阅读题分数设计
		/// </summary> 
		public WorkRandomVSRCDtl()
		{
		}
		 
		public WorkRandomVSRCDtl(string fk_Work, int  fk_rcdtl)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkRandomVSRCDtlAttr.FK_Work,fk_Work);
			qo.addAnd();
			qo.AddWhere(WorkRandomVSRCDtlAttr.FK_RCDtl,fk_rcdtl);
			qo.DoQuery();
			 
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
				
				Map map = new Map("GTS_WorkRandomVSRCDtl");
				map.EnDesc="阅读题分数设计";	
				map.EnType=EnType.Dot2Dot;
			 

				map.AddDDLEntitiesPK(WorkRandomVSRCDtlAttr.FK_Work,"0001","试卷",new WorkFixDesigns(),false);
				map.AddDDLEntitiesPK(WorkRandomVSRCDtlAttr.FK_RCDtl,1,DataType.AppInt,"阅读问答题",new RCDtls(), RCDtlAttr.OID, RCDtlAttr.Name,false);
				
				map.AddTBInt(WorkRandomVSRCDtlAttr.Cent,1,"分",true,false);
				map.AddDDLEntities(WorkRandomVSRCDtlAttr.FK_RC,"0001","阅读题",new RCs(),false);

				//map.AddSearchAttr(EmpDutyAttr.FK_Emp);
				//map.AddSearchAttr(EmpDutyAttr.FK_Duty);

				this._enMap=map;
				return this._enMap;
			}
		}

		 
		#endregion

		#region 重载基类方法

		#endregion 
	
	}
	/// <summary>
	/// 阅读题设计 
	/// </summary>
	public class WorkRandomVSRCDtls : Entities
	{
		#region 构造
		/// <summary>
		/// 阅读题设计
		/// </summary>
		public WorkRandomVSRCDtls(){}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fk_Work">试卷编号</param>
		/// <param name="fk_rc">阅读题目</param>
		public WorkRandomVSRCDtls(string fk_Work,string fk_rc)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkRandomVSRCDtlAttr.FK_Work,fk_Work);
			qo.addAnd();
			qo.AddWhere(WorkRandomVSRCDtlAttr.FK_RC,fk_rc);
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
				return new WorkRandomVSRCDtl();
			}
		}	
		#endregion 

		#region 查询方法
		 
		#endregion
	}
	
}
