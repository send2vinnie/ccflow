using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// 问答题设计
	/// </summary>
	public class PaperVSEssayQuestionAttr  
	{
		#region 基本属性
		/// <summary>
		/// 试卷
		/// </summary>
		public const  string FK_Paper="FK_Paper";
		/// <summary>
		/// 问答题
		/// </summary>
		public const  string FK_EssayQuestion="FK_EssayQuestion";
		/// <summary>
		/// 分数
		/// </summary>
		public const  string Cent="Cent";
		#endregion	
	}
	/// <summary>
	/// 问答题设计 的摘要说明。
	/// </summary>
	public class PaperVSEssayQuestion :Entity
	{
		#region 基本属性
		/// <summary>
		/// HisUAC
		/// </summary>
		public override UAC HisUAC
		{
			get
			{
				UAC uc = new UAC();
				uc.OpenForSysAdmin();
               // uc.IsInsert = false;
              //  uc.IsDelete = false;
				return uc;
			}
		}

		/// <summary>
		///问答题
		/// </summary>
		public string FK_EssayQuestion
		{
			get
			{
				return this.GetValStringByKey(PaperVSEssayQuestionAttr.FK_EssayQuestion);
			}
			set
			{
				SetValByKey(PaperVSEssayQuestionAttr.FK_EssayQuestion,value);
			}
		}		  
		/// <summary>
		///设卷
		/// </summary>
		public string FK_Paper
		{
			get
			{
				return this.GetValStringByKey(PaperVSEssayQuestionAttr.FK_Paper);
			}
			set
			{
				SetValByKey(PaperVSEssayQuestionAttr.FK_Paper,value);
			}
		}
		/// <summary>
		/// 分数
		/// </summary>
		public decimal Cent
		{
			get
			{
				return this.GetValDecimalByKey(PaperVSEssayQuestionAttr.Cent);
			}
			set
			{
				SetValByKey(PaperVSEssayQuestionAttr.Cent,value);
			}
		}
		#endregion

		#region 扩展属性
		 
		#endregion		

		#region 构造函数
		/// <summary>
		/// 问答题分数设计
		/// </summary> 
		public PaperVSEssayQuestion()
		{
		}
		public PaperVSEssayQuestion(string paper,string Equestion)
		{
			this.FK_Paper = paper;
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
				
				Map map = new Map("GTS_PaperVSEssayQuestion");
				map.EnDesc="问答题分数设计";	
				map.EnType=EnType.Dot2Dot;
		 
				map.AddDDLEntitiesPK(PaperVSEssayQuestionAttr.FK_Paper,"0001","试卷",new Papers(),false);
				map.AddDDLEntitiesPK(PaperVSEssayQuestionAttr.FK_EssayQuestion,null,"问答题",new EssayQuestions(),false);
				map.AddTBDecimal(PaperVSEssayQuestionAttr.Cent,5,"分",true,false);

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
	public class PaperVSEssayQuestions : Entities
	{
		#region 构造
		/// <summary>
		/// 问答题设计
		/// </summary>
		public PaperVSEssayQuestions(){}
		#endregion

		#region 方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperVSEssayQuestion();
			}
		}	
		#endregion 

		#region 查询方法
		 
		#endregion
	}
	
}
