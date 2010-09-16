using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// 问答题考试attr
	/// </summary>
	public class PaperExamOfEssayQuestionAttr:EntityOIDAttr
	{
		/// <summary>
		/// 试卷
		/// </summary>
		public const string FK_Paper="FK_Paper";
		/// <summary>
		/// 学生
		/// </summary>
		public const string FK_Emp="FK_Emp";
		/// <summary>
		/// 问答题
		/// </summary>
		public const string FK_EssayQuestion="FK_EssayQuestion";
		/// <summary>
		/// 值
		/// </summary>
		public const string Val="Val";
		/// <summary>
		/// 得分
		/// </summary>
		public const string Cent="Cent";
	}
	/// <summary>
	/// 问答题考试
	/// </summary>
	public class PaperExamOfEssayQuestion :Entity
	{
		#region attrs
		/// <summary>
		/// 得分
		/// </summary>
		public int Cent
		{
			get
			{
				return this.GetValIntByKey(PaperExamOfEssayQuestionAttr.Cent);
			}
			set
			{
				this.SetValByKey(PaperExamOfEssayQuestionAttr.Cent,value);
			}
		}
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(PaperExamOfEssayQuestionAttr.FK_Emp);
			}
			set
			{
				this.SetValByKey(PaperExamOfEssayQuestionAttr.FK_Emp,value);
			}
		}
		/// <summary>
		/// paper
		/// </summary>
		public string FK_Paper
		{
			get
			{
				return this.GetValStringByKey(PaperExamOfEssayQuestionAttr.FK_Paper);
			}
			set
			{
				this.SetValByKey(PaperExamOfEssayQuestionAttr.FK_Paper,value);
			}
		}
		/// <summary>
		/// FK_EssayQuestion
		/// </summary>
		public string FK_EssayQuestion
		{
			get
			{
				return this.GetValStringByKey(PaperExamOfEssayQuestionAttr.FK_EssayQuestion);
			}
			set
			{
				this.SetValByKey(PaperExamOfEssayQuestionAttr.FK_EssayQuestion,value);
			}
		}
		/// <summary>
		/// Val
		/// </summary>
		public string Val
		{
			get
			{
				return this.GetValStringByKey(PaperExamOfEssayQuestionAttr.Val);
			}
			set
			{
				this.SetValByKey(PaperExamOfEssayQuestionAttr.Val,value);
			}
		}
		#endregion

		#region 实现基本的方法
		/// <summary>
		/// uac
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
		/// 重写基类方法
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				
				Map map = new Map("GTS_PaperExamOfEssayQuestion");
				map.EnDesc="试卷问答题";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(PaperExamOfEssayQuestionAttr.FK_Emp,Web.WebUser.No,"学生",true,false,0,50,20);
				map.AddTBStringPK(PaperExamOfEssayQuestionAttr.FK_Paper,null,"考卷",true,true,0,50,20);
				map.AddTBStringPK(PaperExamOfEssayQuestionAttr.FK_EssayQuestion,null,"问答题",true,true,0,50,20);
				map.AddTBString(PaperExamOfEssayQuestionAttr.Val,null,"val",true,true,0,50,20);
				map.AddTBInt(PaperExamOfEssayQuestionAttr.Cent,0,"得分",true,true);

				this._enMap=map;
				return this._enMap;
			}
		}
		protected override bool beforeUpdateInsertAction()
		{
			//this.CentOfSum=this.CentOfFillBlank+this.CentOfEssayQuestion+this.CentOfEssayQuestionOne+this.CentOfEssayQuestionM+this.CentOfJudgeTheme;
			return base.beforeUpdateInsertAction ();
		}

		#endregion 

		#region 构造方法
		/// <summary>
		/// 问答题考试
		/// </summary> 
		public PaperExamOfEssayQuestion()
		{
		}

		 
		/// <summary>
		/// bulider
		/// </summary>
		/// <param name="paper"></param>
		/// <param name="empid"></param>
		public PaperExamOfEssayQuestion(string paper,string empid,string fk_EssayQuestion)
		{

			this.FK_EssayQuestion = fk_EssayQuestion;
			this.FK_Emp=empid;
			this.FK_Paper=paper;

			try
			{
				this.Retrieve();
			}
			catch
			{
				this.Insert();
			}
		}
		#endregion 

		#region 逻辑处理
		
		#endregion
	}
	/// <summary>
	///  问答题考试
	/// </summary>
	public class PaperExamOfEssayQuestions :EntitiesOID
	{
		/// <summary>
		/// PaperExamOfEssayQuestions
		/// </summary>
		public PaperExamOfEssayQuestions(){}

		/// <summary>
		/// empid
		/// </summary>
		/// <param name="empid"></param>
		public PaperExamOfEssayQuestions(string fk_paper,string empid)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(PaperExamOfEssayQuestionAttr.FK_Emp,empid);
			qo.addAnd();
			qo.AddWhere(PaperExamOfEssayQuestionAttr.FK_Paper, fk_paper);
			qo.DoQuery();
		}
		/// <summary>
		/// PaperExamOfEssayQuestion
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperExamOfEssayQuestion();
			}
		}
	}
}
