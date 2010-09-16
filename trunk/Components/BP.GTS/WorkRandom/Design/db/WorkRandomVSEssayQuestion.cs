using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.En.Base;

namespace BP.GTS
{
	
	/// <summary>
	/// 问答题设计
	/// </summary>
	public class WorkRandomVSEssayQuestionAttr  :WorkVSBaseAttr
	{
		#region 基本属性
		/// <summary>
		/// 问答题
		/// </summary>
		public const  string FK_EssayQuestion="FK_EssayQuestion";
		#endregion	
	}
	/// <summary>
	/// 问答题设计 的摘要说明。
	/// </summary>
	public class WorkRandomVSEssayQuestion :WorkVSBase
	{
		#region 基本属性
		/// <summary>
		///问答题
		/// </summary>
		public string FK_EssayQuestion
		{
			get
			{
				return this.GetValStringByKey(WorkRandomVSEssayQuestionAttr.FK_EssayQuestion);
			}
			set
			{
				SetValByKey(WorkRandomVSEssayQuestionAttr.FK_EssayQuestion,value);
			}
		}		  
		#endregion

		#region 扩展属性
		 
		#endregion		

		#region 构造函数
		/// <summary>
		/// 问答题分数设计
		/// </summary> 
		public WorkRandomVSEssayQuestion()
		{
		}
		public WorkRandomVSEssayQuestion(string Work,string Equestion)
		{
			this.FK_Work = Work;
			this.FK_EssayQuestion = Equestion;
			
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
				
				Map map = new Map("GTS_WorkRandomVSEssayQuestion");
				map.EnDesc="问答题分数设计";	
				map.EnType=EnType.Dot2Dot;
		 
				map.AddDDLEntitiesPK(WorkRandomVSEssayQuestionAttr.FK_Work,"0001","试卷",new WorkFixDesigns(),false);
				map.AddDDLEntitiesPK(WorkRandomVSEssayQuestionAttr.FK_EssayQuestion,null,"问答题",new EssayQuestions(),false);
				map.AddTBInt(WorkRandomVSEssayQuestionAttr.Cent,1,"分",true,false);

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
	/// 问答题设计 
	/// </summary>
	public class WorkRandomVSEssayQuestions : WorkVSBases
	{
		#region 构造
		/// <summary>
		/// 问答题设计
		/// </summary>
		public WorkRandomVSEssayQuestions(){}
		#endregion

		#region 方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkRandomVSEssayQuestion();
			}
		}	
		#endregion 

		#region 查询方法
		 
		#endregion
	}
	
}
