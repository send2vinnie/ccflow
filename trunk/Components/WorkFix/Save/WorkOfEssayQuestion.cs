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
	public class WorkOfEssayQuestionAttr:WorkThemeBaseAttr
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
	public class WorkOfEssayQuestion :WorkThemeBase
	{
		#region attrs
		/// <summary>
		/// 得分
		/// </summary>
		public int Cent
		{
			get
			{
				return this.GetValIntByKey(WorkOfEssayQuestionAttr.Cent);
			}
			set
			{
				this.SetValByKey(WorkOfEssayQuestionAttr.Cent,value);
			}
		}
		/// <summary>
		/// FK_EssayQuestion
		/// </summary>
		public string FK_EssayQuestion
		{
			get
			{
				return this.GetValStringByKey(WorkOfEssayQuestionAttr.FK_EssayQuestion);
			}
			set
			{
				this.SetValByKey(WorkOfEssayQuestionAttr.FK_EssayQuestion,value);
			}
		}
		/// <summary>
		/// Val
		/// </summary>
		public string Val
		{
			get
			{
				return this.GetValStringByKey(WorkOfEssayQuestionAttr.Val);
			}
			set
			{
				this.SetValByKey(WorkOfEssayQuestionAttr.Val,value);
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
				
				Map map = new Map("GTS_WorkOfEssayQuestion");
				map.EnDesc="问答题";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(WorkOfEssayQuestionAttr.FK_Emp,Web.WebUser.No,"学生",true,false,0,50,20);
				map.AddTBStringPK(WorkOfEssayQuestionAttr.FK_Work,null,"作业",true,true,0,50,20);
				map.AddTBStringPK(WorkOfEssayQuestionAttr.FK_EssayQuestion,null,"问答题",true,true,0,50,20);
				map.AddTBString(WorkOfEssayQuestionAttr.Val,null,"val",true,true,0,50,20);
				map.AddTBInt(WorkOfEssayQuestionAttr.Cent,0,"得分",true,true);

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
		public WorkOfEssayQuestion()
		{
		}

		 
		/// <summary>
		/// bulider
		/// </summary>
		/// <param name="paper"></param>
		/// <param name="empid"></param>
		public WorkOfEssayQuestion(string work,string empid,string fk_EssayQuestion)
		{
			this.FK_EssayQuestion = fk_EssayQuestion;
			this.FK_Emp=empid;
			this.FK_Work=work;
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
	public class WorkOfEssayQuestions :WorkThemeBases
	{
		/// <summary>
		/// WorkOfEssayQuestions
		/// </summary>
		public WorkOfEssayQuestions(){}

		/// <summary>
		/// empid
		/// </summary>
		/// <param name="empid"></param>
		public WorkOfEssayQuestions(string fk_work,string empid)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkOfEssayQuestionAttr.FK_Emp,empid);
			qo.addAnd();
			qo.AddWhere(WorkOfEssayQuestionAttr.FK_Work, fk_work);
			qo.DoQuery();
		}
		/// <summary>
		/// WorkOfEssayQuestion
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkOfEssayQuestion();
			}
		}
	}
}
