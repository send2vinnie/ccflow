using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	/// <summary>
	/// 阅读题设计
	/// </summary>
	public class PaperVSRCDtlAttr  
	{
		#region 基本属性
		/// <summary>
		/// 试卷
		/// </summary>
		public const  string FK_Paper="FK_Paper";
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
	public class PaperVSRCDtl :Entity
	{
		#region 基本属性
		/// <summary>
		///阅读题
		/// </summary>
		public int FK_RCDtl
		{
			get
			{
				return this.GetValIntByKey(PaperVSRCDtlAttr.FK_RCDtl);
			}
			set
			{
				SetValByKey(PaperVSRCDtlAttr.FK_RCDtl,value);
			}
		}		  
		public string FK_RCDtlText
		{
			get
			{
				return this.GetValRefTextByKey(PaperVSRCDtlAttr.FK_RCDtl);
			}
			 
		}
		/// <summary>
		///设卷
		/// </summary>
		public string FK_Paper
		{
			get
			{
				return this.GetValStringByKey(PaperVSRCDtlAttr.FK_Paper);
			}
			set
			{
				SetValByKey(PaperVSRCDtlAttr.FK_Paper,value);
			}
		}
		/// <summary>
		/// FK_RC 
		/// </summary>
		public string FK_RC
		{
			get
			{
				return this.GetValStringByKey(PaperVSRCDtlAttr.FK_RC);
			}
			set
			{
				SetValByKey(PaperVSRCDtlAttr.FK_RC,value);
			}
		}
		/// <summary>
		/// 分数
		/// </summary>
		public int Cent
		{
			get
			{
				return this.GetValIntByKey(PaperVSRCDtlAttr.Cent);
			}
			set
			{
				SetValByKey(PaperVSRCDtlAttr.Cent,value);
			}
		}
		/// <summary>
		/// HisUAC
		/// </summary>
		public override UAC HisUAC
		{
			get
			{
				UAC uc = new UAC();
				uc.OpenForSysAdmin();
                uc.IsDelete = false;
                uc.IsInsert = false;

				return uc;
			}
		}
		#endregion

		#region 扩展属性
		 
		#endregion		

		#region 构造函数
		/// <summary>
		/// 阅读题分数设计
		/// </summary> 
		public PaperVSRCDtl()
		{
		}
		 
		public PaperVSRCDtl(string fk_paper, int  fk_rcdtl)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(PaperVSRCDtlAttr.FK_Paper,fk_paper);
			qo.addAnd();
			qo.AddWhere(PaperVSRCDtlAttr.FK_RCDtl,fk_rcdtl);
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
				
				Map map = new Map("GTS_PaperVSRCDtl");
				map.EnDesc="阅读题分数设计";	
				map.EnType=EnType.Dot2Dot;
			 

				map.AddDDLEntitiesPK(PaperVSRCDtlAttr.FK_Paper,"0001","试卷",new Papers(),false);
				map.AddDDLEntitiesPK(PaperVSRCDtlAttr.FK_RCDtl,1,DataType.AppInt,"阅读问答题",new RCDtls(), RCDtlAttr.OID, RCDtlAttr.Name,false);
				
				map.AddTBInt(PaperVSRCDtlAttr.Cent,5,"分",true,false);
				map.AddDDLEntities(PaperVSRCDtlAttr.FK_RC,"0001","阅读题",new RCs(),false);
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
	public class PaperVSRCDtls : Entities
	{
		#region 构造
		/// <summary>
		/// 阅读题设计
		/// </summary>
		public PaperVSRCDtls(){}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fk_paper">试卷编号</param>
		/// <param name="fk_rc">阅读题目</param>
		public PaperVSRCDtls(string fk_paper,string fk_rc)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(PaperVSRCDtlAttr.FK_Paper,fk_paper);
			qo.addAnd();
			qo.AddWhere(PaperVSRCDtlAttr.FK_RC,fk_rc);
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
				return new PaperVSRCDtl();
			}
		}	
		#endregion 

		#region 查询方法
		 
		#endregion
	}
	
}
