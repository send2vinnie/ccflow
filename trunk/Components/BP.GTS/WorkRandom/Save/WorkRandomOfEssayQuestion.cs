using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// 问答题考试attr
	/// </summary>
	public class WorkRandomOfEssayQuestionAttr:WorkRandomThemeBaseAttr
	{
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
	public class WorkRandomOfEssayQuestion :WorkRandomThemeBase
	{
		#region attrs
		/// <summary>
		/// 得分
		/// </summary>
		public int Cent
		{
			get
			{
				return this.GetValIntByKey(WorkRandomOfEssayQuestionAttr.Cent);
			}
			set
			{
				this.SetValByKey(WorkRandomOfEssayQuestionAttr.Cent,value);
			}
		}
		/// <summary>
		/// FK_EssayQuestion
		/// </summary>
		public string FK_EssayQuestion
		{
			get
			{
				return this.GetValStringByKey(WorkRandomOfEssayQuestionAttr.FK_EssayQuestion);
			}
			set
			{
				this.SetValByKey(WorkRandomOfEssayQuestionAttr.FK_EssayQuestion,value);
			}
		}
		/// <summary>
		/// Val
		/// </summary>
		public string Val
		{
			get
			{
				return this.GetValStringByKey(WorkRandomOfEssayQuestionAttr.Val);
			}
			set
			{
				this.SetValByKey(WorkRandomOfEssayQuestionAttr.Val,value);
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
				
				Map map = new Map("GTS_WorkRandomOfEssayQuestion");
				map.EnDesc="问答题";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(WorkRandomOfEssayQuestionAttr.FK_Emp,Web.WebUser.No,"学生",true,false,0,50,20);
				map.AddTBStringPK(WorkRandomOfEssayQuestionAttr.FK_WorkRandom,null,"作业",true,true,0,50,20);
				map.AddTBStringPK(WorkRandomOfEssayQuestionAttr.FK_EssayQuestion,null,"问答题",true,true,0,50,20);

				map.AddTBString(WorkRandomOfEssayQuestionAttr.Val,null,"val",true,true,0,50,20);
				map.AddTBInt(WorkRandomOfEssayQuestionAttr.Cent,0,"得分",true,true);

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
		public WorkRandomOfEssayQuestion()
		{
		}

		 
		/// <summary>
		/// bulider
		/// </summary>
		/// <param name="paper"></param>
		/// <param name="empid"></param>
		public WorkRandomOfEssayQuestion(string work,string empid,string fk_EssayQuestion)
		{
			this.FK_EssayQuestion = fk_EssayQuestion;
			this.FK_Emp=empid;
			this.FK_WorkRandom=work;
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
	public class WorkRandomOfEssayQuestions :WorkRandomThemeBases
	{
		/// <summary>
		/// WorkRandomOfEssayQuestions
		/// </summary>
		public WorkRandomOfEssayQuestions(){}

		/// <summary>
		/// empid
		/// </summary>
		/// <param name="empid"></param>
		public WorkRandomOfEssayQuestions(string FK_WorkRandom,string empid)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkRandomOfEssayQuestionAttr.FK_Emp,empid);
			qo.addAnd();
			qo.AddWhere(WorkRandomOfEssayQuestionAttr.FK_WorkRandom, FK_WorkRandom);
			qo.DoQuery();
		}
		/// <summary>
		/// WorkRandomOfEssayQuestion
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkRandomOfEssayQuestion();
			}
		}
	}
}
