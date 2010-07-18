using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// 试卷答案attr
	/// </summary>
	public class PaperExamAttr:EntityOIDAttr
	{
		/// <summary>
		/// 试卷
		/// </summary>
		public const string FK_Paper="FK_Paper";
		/// <summary>
		/// 考
		/// </summary>
		public const string FK_Emp="FK_Emp";
		/// <summary>
		/// 成绩等级
		/// </summary>
		public const string FK_Level="FK_Level";
		/// <summary>
		/// 考试状态
		/// </summary>
		public const string ExamState="ExamState";
		/// <summary>
		/// dept
		/// </summary>
		public const string FK_Dept="FK_Dept";
		/// <summary>
		/// from datatime
		/// </summary>
		public const string FromDateTime="FromDateTime";
		/// <summary>
		/// to datetime
		/// </summary>
		public const string ToDateTime="ToDateTime";
		/// <summary>
		/// 考试时间分钟
		/// </summary>
		public const string MM="MM";
		/// <summary>
		/// 单项选择题
		/// </summary>
		public const string CentOfChoseOne="CentOfChoseOne";
		/// <summary>
		/// 多项选择题
		/// </summary>
		public const string CentOfChoseM="CentOfChoseM";
		/// <summary>
		/// 填空题
		/// </summary>
		public const string CentOfFillBlank="CentOfFillBlank";
		/// <summary>
		/// 判断题
		/// </summary>
		public const string CentOfJudgeTheme="CentOfJudgeTheme";
		/// <summary>
		/// 问答题
		/// </summary>
		public const string CentOfEssayQuestion="CentOfEssayQuestion";
		/// <summary>
		/// CentOfRC
		/// </summary>
		public const string CentOfRC="CentOfRC";
		/// <summary>
		/// 合计
		/// </summary>
		public const string CentOfSum="CentOfSum";
		/// <summary>
		/// 标准分
		/// </summary>
		public const string CentOfStd="CentOfStd";
		/// <summary>
		/// 是否是考试
		/// </summary>
		public const string IsExam="IsExam";
	}
	/// <summary>
	/// 考试状态
	/// </summary>
	public enum PaperExamState
	{
		/// <summary>
		/// 初始化
		/// </summary>
		Init,
		/// <summary>
		/// 考试中
		/// </summary>
		Examing,
		/// <summary>
		/// 阅卷中
		/// </summary>
		Reading,
		/// <summary>
		/// 阅卷完毕
		/// </summary>
		ReadOver,
	}
	/// <summary>
	/// 试卷答案
	/// </summary>
	public class PaperExam :EntityOID
	{
		#region 属性
		/// <summary>
		/// ExamStateText
		/// </summary>
		public string ExamStateText
		{
			get
			{
				//if (this.
				return this.GetValRefTextByKey( PaperExamAttr.ExamState);
			}
		}
		/// <summary>
		/// 考试状态
		/// </summary>
		public PaperExamState ExamState
		{
			get
			{
				return (PaperExamState)this.GetValIntByKey( PaperExamAttr.ExamState);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.ExamState,(int)value);
			}
		}
		/// <summary>
		/// 是否是考试
		/// </summary>
		public bool IsExam
		{
			get
			{
				return this.GetValBooleanByKey( PaperExamAttr.IsExam);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.IsExam,value);
			}
		}
		/// <summary>
		/// 部门
		/// </summary>
		public string FK_Dept
		{
			get
			{
				return this.GetValStringByKey( PaperExamAttr.FK_Dept);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.FK_Dept,value);
			}
		}
		/// <summary>
		/// paper.
		/// </summary>
		public PaperFix HisPaper
		{
			get
			{
				return new PaperFix(this.FK_Paper);
			}
		}
		#endregion

		/// <summary>
		/// 自动阅卷:
		/// </summary>
		public void AutoReadPaper()
		{
			PaperFix pp = new PaperFix(this.FK_Paper) ;
			// 选择题.
			PaperExamOfChoses  pes = new PaperExamOfChoses(this.FK_Paper,this.FK_Emp);
			ChoseOnes chs =this.HisChoseOnes;
			int countOne=0;
			int countM=0;
			foreach(PaperExamOfChose pe in pes)
			{
				if (pe.Val.Length==0)
					continue;
				/* */
				ChoseItems cts = new ChoseItems();
				cts.RetrieveRightItems(pe.FK_Chose); //查询出来正确的项目。
				if (cts.Val==pe.Val)
				{
					if (pe.Val.Length==1)
						countOne++; // 单项
					else
						countM++; // 多项。
				}
			}

			this.CentOfChoseOne= countOne*pp.CentOfChoseOne; // 单项选择题得分。
			this.CentOfChoseM= countM*pp.CentOfChoseM; // duo项选择题得分。

			// 判断题
			PaperExamOfJudgeThemes  pej = new PaperExamOfJudgeThemes(this.FK_Paper,this.FK_Emp);
			int count=0;
			foreach(PaperExamOfJudgeTheme pe in pej)
			{
				
				JudgeTheme jt =new JudgeTheme(pe.FK_JudgeTheme);
				if (pe.Val==jt.IsOkOfInt)
				{
					count++;
				}
			}
			this.CentOfJudgeTheme= count*pp.CentOfJudgeTheme; // duo项选择题得分。


			// 填空题
			PaperExamOfFillBlanks  peb = new PaperExamOfFillBlanks(this.FK_Paper, this.FK_Emp);
			count=0;
			foreach(PaperExamOfFillBlank pe in peb)
			{
				FillBlank fb= new FillBlank(pe.FK_FillBlank);
				if (pe.Val.Trim()==fb.HisKeys[pe.IDX].Trim() )
				{
					/*  */
					count++;
				}
			}
			this.CentOfFillBlank= count*pp.CentOfFillBlank; // blank项选择题得分。

			PaperExamOfEssayQuestions  pee = new PaperExamOfEssayQuestions(this.FK_Paper, this.FK_Emp);
			if (pee.Count==0)
			{
				/*如果没有简答题*/
				this.ExamState=PaperExamState.ReadOver;
			}
			else
			{
				if (this.ExamState==PaperExamState.Examing || this.ExamState==PaperExamState.Init )
					this.ExamState=PaperExamState.Reading;
			}

			this.DoResetLevel( pp.CentOfSum );
		}
		/// <summary>
		/// 执行重新设置级别。
		/// </summary>
		public void DoResetLevel(int centOfSum)
		{
			
			PaperFix pf= new PaperFix(this.FK_Paper);

			// 开始计算标准分。
			this.CentOfStd=centOfSum / pf.CentOfSum*100 ;
			//int sume pf.CentOfSum

			if (this.CentOfStd > 90)
			{
				this.FK_Level="4";
				this.Update();
				return;
			}
			if (this.CentOfStd >80)
			{
				this.FK_Level="3";
				this.Update();
				return;
			}

			if (this.CentOfStd >60)
			{
				this.FK_Level="2";
				this.Update();
				return;
			}
			this.FK_Level="1";
			this.Update();
		}

		#region his attrs
		/// <summary>
		/// 他的单项选择题
		/// </summary>
		public ChoseOnes HisChoseOnes
		{
			get
			{
				ChoseOnes chs = new ChoseOnes();
				QueryObject qo = new QueryObject(chs);
				//qo.AddWhereInSQL( ChoseOneAttr.No, "select fk_chose from GTS_PaperExamVSChoseOne where fk_PaperExam='"+this.No+"'");
				qo.DoQuery();
				return chs;
			}
		}
		/// <summary>
		/// 他的单项选择题
		/// </summary>
		public ChoseMs HisChoseMs
		{
			get
			{
				ChoseMs chs = new ChoseMs();
				QueryObject qo = new QueryObject(chs);
				//qo.AddWhereInSQL( ChoseOneAttr.FK_Chose, "select fk_chose from GTS_PaperExamVSChoseM where fk_PaperExam='"+this.No+"'");
				qo.DoQuery();
				return chs;
			}
		}
		#endregion


		#region attrs
		 
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(PaperExamAttr.FK_Emp);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.FK_Emp,value);
			}
		}
		/// <summary>
		/// FK_EmpText
		/// </summary>
		public string FK_EmpText
		{
			get
			{
				return this.GetValRefTextByKey(PaperExamAttr.FK_Emp);
			}
		}
		/// <summary>
		/// FK_Paper
		/// </summary>
		public string FK_Paper
		{
			get
			{
				return this.GetValStringByKey(PaperExamAttr.FK_Paper);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.FK_Paper,value);
			}
		}
		/// <summary>
		/// 成绩等级
		/// </summary>
		public string FK_Level
		{
			get
			{
				return this.GetValStringByKey(PaperExamAttr.FK_Level);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.FK_Level,value);
			}
		}
		public string FK_PaperText
		{
			get
			{
				return this.GetValRefTextByKey(PaperExamAttr.FK_Paper);
			}
		}
		/// <summary>
		/// 从时间
		/// </summary>
		public string FromDateTime
		{
			get
			{
				return this.GetValStringByKey(PaperExamAttr.FromDateTime);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.FromDateTime,value);
			}
		}
		/// <summary>
		/// 到时间
		/// </summary>
		public string ToDateTime
		{
			get
			{
				return this.GetValStringByKey(PaperExamAttr.ToDateTime);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.ToDateTime,value);
			}
		}
		/// <summary>
		/// 单选
		/// </summary>
		public int CentOfChoseOne
		{
			get
			{
				return this.GetValIntByKey(PaperExamAttr.CentOfChoseOne);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.CentOfChoseOne,value);
			}
		}
		/// <summary>
		/// 多选
		/// </summary>
		public int CentOfChoseM
		{
			get
			{
				return this.GetValIntByKey(PaperExamAttr.CentOfChoseM);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.CentOfChoseM,value);
			}
		}
		/// <summary>
		/// 判断
		/// </summary>
		public int CentOfJudgeTheme
		{
			get
			{
				return this.GetValIntByKey(PaperExamAttr.CentOfJudgeTheme);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.CentOfJudgeTheme,value);
			}
		}
		/// <summary>
		/// 问答
		/// </summary>
		public int CentOfEssayQuestion
		{
			get
			{
				return this.GetValIntByKey(PaperExamAttr.CentOfEssayQuestion);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.CentOfEssayQuestion,value);
			}
		}
		public int CentOfRC
		{
			get
			{
				return this.GetValIntByKey(PaperExamAttr.CentOfRC);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.CentOfRC,value);
			}
		}
		/// <summary>
		/// 填空
		/// </summary>
		public int CentOfFillBlank
		{
			get
			{
				return this.GetValIntByKey(PaperExamAttr.CentOfFillBlank);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.CentOfFillBlank,value);
			}
		}
		/// <summary>
		/// 合计
		/// </summary>
		public int CentOfSum
		{
			get
			{
				return this.GetValIntByKey(PaperExamAttr.CentOfSum);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.CentOfSum,value);
			}
		}
		/// <summary>
		/// 标准分
		/// </summary>
		public int CentOfStd
		{
			get
			{
				return this.GetValIntByKey(PaperExamAttr.CentOfStd);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.CentOfStd,value);
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
				uc.IsView=true;
				return uc;
			}
		}

		/// <summary>
		/// 是否超出了时间
		/// </summary>
		public bool IsOutTime
		{
			get
			{
				return false;
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
				
				Map map = new Map("GTS_PaperExam");
				map.EnDesc="考卷";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				map.AddTBIntPKOID();
				map.AddDDLSysEnum(PaperExamAttr.ExamState,0,"状态",true,false);
				map.AddDDLEntities(PaperExamAttr.FK_Paper,null,"试卷",new PaperFixs(),false);
				map.AddDDLEntities(PaperExamAttr.FK_Emp,Web.WebUser.No,"学生",new EmpExts(),false);
				map.AddDDLEntities(PaperExamAttr.FK_Dept,Web.WebUser.FK_Dept,"部门",new Depts(),false);
			
				map.AddTBInt(PaperExamAttr.CentOfChoseOne,0,"单选题分",true,true);
				map.AddTBInt(PaperExamAttr.CentOfChoseM,0,"多选题分",true,true);
				map.AddTBInt(PaperExamAttr.CentOfFillBlank,0,"填空题分",true,true);
				map.AddTBInt(PaperExamAttr.CentOfJudgeTheme,0,"判断题分",true,true);
				map.AddTBInt(PaperExamAttr.CentOfEssayQuestion,0,"问答题分",true,true);
				map.AddTBInt(PaperExamAttr.CentOfRC,0,"阅读理解题分",true,true);
				map.AddTBInt(PaperExamAttr.CentOfSum,0,"总分",true,true);
				map.AddTBInt(PaperExamAttr.CentOfStd,0,"标准分",true,true);
				map.AddDDLEntities(PaperExamAttr.FK_Level,"1","成绩等级",new Levels(),false);
				map.AddTBDateTime(PaperExamAttr.FromDateTime,"考时从",true,true);
				map.AddTBDateTime(PaperExamAttr.ToDateTime,"到",true,true);

				map.AddBoolean(PaperExamAttr.IsExam,false,"是否是考试",true,false);

				map.AddSearchAttr(PaperExamAttr.FK_Paper);
				map.AddSearchAttr(PaperExamAttr.FK_Dept);
				map.AddSearchAttr(PaperExamAttr.ExamState);
				map.AddSearchAttr(PaperExamAttr.FK_Level);



				/*
				map.AttrsOfOneVSM.Add( new PaperExamVSChoseOnes(), new ChoseOnes(),PaperExamVSChoseOneAttr.FK_PaperExam,PaperExamVSChoseOneAttr.FK_Chose, ChoseOneAttr.Name,ChoseOneAttr.No,"单选题");
				map.AttrsOfOneVSM.Add( new PaperExamVSChoseMs(), new ChoseMs(),PaperExamVSChoseMAttr.FK_PaperExam,PaperExamVSChoseMAttr.FK_Chose, ChoseMAttr.Name,ChoseMAttr.No,"多选题");
				map.AttrsOfOneVSM.Add( new PaperExamVSFillBlanks(), new FillBlanks(),PaperExamVSFillBlankAttr.FK_PaperExam,PaperExamVSFillBlankAttr.FK_FillBlank, FillBlankAttr.Name,FillBlankAttr.No,"填空题");
				map.AttrsOfOneVSM.Add( new PaperExamVSJudgeThemes(), new JudgeThemes(),PaperExamVSJudgeThemeAttr.FK_PaperExam,PaperExamVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"判断题");
				map.AttrsOfOneVSM.Add( new PaperExamVSEssayQuestions(), new EssayQuestions(),PaperExamVSEssayQuestionAttr.FK_PaperExam,PaperExamVSEssayQuestionAttr.FK_EssayQuestion, EssayQuestionAttr.Name,EssayQuestionAttr.No,"问答题");
			    map.AddDtl(new PaperExamVSEssayQuestions(),PaperExamVSEssayQuestionAttr.FK_PaperExam);
				*/

				//map.AttrsOfOneVSM.Add( new PaperExamVSChoseOnes(), new choseonhemes(),PaperExamVSJudgeThemeAttr.FK_PaperExam,PaperExamVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"判断题设计");
				//map.AttrsOfOneVSM.Add( new PaperExamVSChoseMs(), new JudgeThemes(),PaperExamVSJudgeThemeAttr.FK_PaperExam,PaperExamVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"判断题设计");
  
				this._enMap=map;
				return this._enMap;
			}
		}
		protected override bool beforeInsert()
		{

			if (this.Search(this.FK_Paper,this.FK_Emp)>=1)
				throw new Exception("已经有这个学生"+this.FK_Emp+"的试卷.");

		 
			Emp emp = new Emp(this.FK_Emp);
			this.FK_Dept = emp.FK_Dept;

			return base.beforeInsert ();
		}

		protected override bool beforeUpdateInsertAction()
		{
			 
			
			//this.FK_Dept =
			//this.MM =DataType.GetSpanMinute(this.FromDateTime,this.ToDateTime);
			this.CentOfSum=this.CentOfFillBlank+this.CentOfEssayQuestion+this.CentOfRC+this.CentOfChoseOne+this.CentOfChoseM+this.CentOfJudgeTheme;
			return base.beforeUpdateInsertAction ();
		}

		#endregion 

		#region 构造方法
		/// <summary>
		/// 试卷答案
		/// </summary> 
		public PaperExam()
		{
		}
		public PaperExam(int _oid) : base(_oid){}	
		public PaperExam(string paper, string fk_emp)
		{
			this.FK_Emp = fk_emp;
			this.FK_Paper=paper;
			if (this.Retrieve("FK_Emp",fk_emp, "FK_Paper", paper)==0)
				throw new Exception("错误：没有生成考生[“"+fk_emp+"”]考试信息。");
		}
		#endregion 

		public int Search(string paper, string fk_emp)
		{
			this.FK_Emp = fk_emp;
			this.FK_Paper=paper;
			QueryObject qo = new QueryObject(this);			
			qo.AddWhere(PaperExamAttr.FK_Emp,fk_emp);
			qo.addAnd();
			qo.AddWhere(PaperExamAttr.FK_Paper,paper);
			return qo.DoQuery();
		}

		#region 逻辑处理
		/// <summary>
		/// 合计得分。
		/// </summary>
		public void SumIt()
		{
		}
		#endregion

	}
	/// <summary>
	///  试卷答案
	/// </summary>
	public class PaperExams :EntitiesOID
	{
		/// <summary>
		/// 
		/// </summary>
		public void AutoReadPaper()
		{
			foreach(PaperExam pe in this)
			{
				pe.AutoReadPaper();
			}
		}
		/// <summary>
		/// PaperExams
		/// </summary>
		public PaperExams(){}
		public PaperExams(string fk_paper)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere( PaperExamAttr.FK_Paper, fk_paper);
			qo.DoQuery();
		}
		public int Search(string fk_emp)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere( PaperExamAttr.FK_Emp, fk_emp);
			return qo.DoQuery();
		}



		 
		/// <summary>
		/// PaperExam
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperExam();
			}
		}
	}
}
