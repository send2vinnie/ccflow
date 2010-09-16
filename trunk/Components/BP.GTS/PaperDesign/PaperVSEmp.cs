using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.GTS
{
	
	/// <summary>
	/// vaemp
	/// </summary>
	public class PaperVSEmpAttr  
	{
		#region 基本属性
		/// <summary>
		/// 试卷
		/// </summary>
		public const  string FK_Paper="FK_Paper";
		/// <summary>
		/// 阅读题
		/// </summary>
		public const  string FK_Emp="FK_Emp";
		/// <summary>
		/// UseState
		/// </summary>
		public const  string UseState="UseState";

	 
		#endregion	
	}
	/// <summary>
	/// emp 的摘要说明
	/// </summary>
	public class PaperVSEmp :Entity
	{
		#region 基本属性
		/// <summary>
		///阅读题
		/// </summary>
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(PaperVSEmpAttr.FK_Emp);
			}
			set
			{
				SetValByKey(PaperVSEmpAttr.FK_Emp,value);
			}
		}
		/// <summary>
		///设卷
		/// </summary>
		public string FK_Paper
		{
			get
			{
				return this.GetValStringByKey(PaperVSEmpAttr.FK_Paper);
			}
			set
			{
				SetValByKey(PaperVSEmpAttr.FK_Paper,value);
			}
		}
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
		public PaperVSEmp()
		{
		}
		public PaperVSEmp(string paper,string Equestion)
		{
			this.FK_Paper = paper;
			this.FK_Emp = Equestion;
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
				
				Map map = new Map("GTS_PaperVSEmp");
				map.EnDesc="考试学生";	
				map.EnType=EnType.Dot2Dot;

				map.AddDDLEntitiesPK(PaperVSEmpAttr.FK_Paper,"0001","试卷",new Papers(),false);
				map.AddDDLEntitiesPK(PaperVSEmpAttr.FK_Emp,null,"学生",new Emps(),false);
				//map.AddTBInt(PaperVSEmpAttr.UseState,0,"状态",true,true);
				/* 0 ,没有考试过。 1 已经考试过了。 */
				/* UseState*/
				//map.AddSearchAttr(EmpDutyAttr.FK_Emp);
				//map.AddSearchAttr(EmpDutyAttr.FK_Duty);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion
	}
	/// <summary>
	/// emp 
	/// </summary>
	public class PaperVSEmps : Entities
	{
		#region 构造
		/// <summary>
		/// emp
		/// </summary>
		public PaperVSEmps(){}
		
		/// <summary>
		/// 
		/// </summary>
		public PaperVSEmps(string fk_emp, int UseState)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(PaperVSEmpAttr.FK_Emp, fk_emp);
			qo.addAnd();
			qo.AddWhere(PaperVSEmpAttr.UseState, UseState);

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
				return new PaperVSEmp();
			}
		}	
		#endregion 

		#region 查询方法
		 
		public WorkRDs GetWorkRDs(string fk_emp)
		{
			WorkRDs pps = new WorkRDs();
			QueryObject qo = new QueryObject(pps);
			qo.AddWhereInSQL(PaperFixAttr.No, "SELECT FK_Paper FROM GTS_PaperVSEmp WHERE Len(FK_Paper)=4 AND FK_Emp='"+fk_emp+"'") ;
			qo.DoQuery();
			return pps;
		}
		 
		#endregion
	}
	
}
