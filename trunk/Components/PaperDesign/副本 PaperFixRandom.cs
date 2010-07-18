using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// 试卷
	/// </summary>
	public class PaperFixRandomAttr:EntityNoNameAttr
	{
		/// <summary>
		/// 试卷状态
		/// </summary>
		public const string PaperState="PaperState";
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
	}
	/// <summary>
	/// 试卷
	/// </summary>
	public class PaperFixRandom :EntityNoName
	{
		#region his attrs

		/// <summary>
		/// 考试集合
		/// </summary>
		public PaperExams HisPaperExams
		{
			get
			{
				PaperExams chs = new PaperExams(this.No);
				return chs;
			}
		}
		/// <summary>
		/// 他的单项选择题
		/// </summary>
		public ChoseOnes HisChoseOnes
		{
			get
			{
				ChoseOnes chs = new ChoseOnes();
				QueryObject qo = new QueryObject(chs);
				qo.AddWhereInSQL( ChoseOneAttr.No, "select fk_chose from GTS_PaperVSChoseOne where FK_Paper='"+this.No+"'");
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
				qo.AddWhereInSQL( ChoseOneAttr.No, "select fk_chose from GTS_PaperVSChoseM where FK_Paper='"+this.No+"'");
				qo.DoQuery();
				return chs;
			}
		}
		/// <summary>
		/// 判断题
		/// </summary>
		public JudgeThemes HisJudgeThemes
		{
			get
			{
				JudgeThemes chs = new JudgeThemes();
				QueryObject qo = new QueryObject(chs);
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_JudgeTheme from GTS_PaperVSJudgeTheme where FK_Paper='"+this.No+"'");
				qo.DoQuery();
				return chs;
			}
		}
		/// <summary>
		/// 简答体
		/// </summary>
		public EssayQuestions HisEssayQuestions
		{
			get
			{
				EssayQuestions chs = new EssayQuestions();
				QueryObject qo = new QueryObject(chs);
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_EssayQuestion from GTS_PaperVSEssayQuestion where FK_Paper='"+this.No+"'");
				qo.DoQuery();
				return chs;
			}
		}
		/// <summary>
		/// 阅读理解题目
		/// </summary>
		public RCs HisRCs
		{
			get
			{
				RCs chs = new RCs();
				QueryObject qo = new QueryObject(chs);
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_RC from GTS_PaperVSRC where FK_Paper='"+this.No+"'");
				qo.DoQuery();
				return chs;
			}
		}
		/// <summary>
		/// 填空题目
		/// </summary>
		public FillBlanks HisFillBlanks
		{
			get
			{
				FillBlanks chs = new FillBlanks();
				QueryObject qo = new QueryObject(chs);
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_FillBlank from GTS_PaperVSFillBlank where FK_Paper='"+this.No+"'");
				qo.DoQuery();
				return chs;
			}
		}
		#endregion

		#region attrs 
		public string HisPaperRandomNo
		{
			get
			{
				return this.GetValStringByKey(PaperAttr.HisPaperRandomNo);
			}
			set
			{
				this.SetValByKey(PaperAttr.HisPaperRandomNo,value);
			}
		}
		public string HisExamEmp
		{
			get
			{
				return this.GetValStringByKey(PaperAttr.HisExamEmp);
			}
			set
			{
				this.SetValByKey(PaperAttr.HisExamEmp,value);
			}
		}
		/// <summary>
		/// 试卷状态
		/// </summary>
		public PaperState PaperState
		{
			get
			{
				return (PaperState)this.GetValIntByKey(PaperFixRandomAttr.PaperState);
			}
			set
			{
				this.SetValByKey(PaperFixRandomAttr.PaperState,(int)value);
			}
		}
		public string PaperStateText
		{
			get
			{
				return this.GetValRefTextByKey(PaperFixRandomAttr.PaperState);
			}
		}
		/// <summary>
		/// 单选
		/// </summary>
		public int CentOfChoseOne
		{
			get
			{
				return this.GetValIntByKey(PaperFixRandomAttr.CentOfChoseOne);
			}
			set
			{
				this.SetValByKey(PaperFixRandomAttr.CentOfChoseOne,value);
			}
		}
		/// <summary>
		/// 多选
		/// </summary>
		public int CentOfChoseM
		{
			get
			{
				return this.GetValIntByKey(PaperFixRandomAttr.CentOfChoseM);
			}
			set
			{
				this.SetValByKey(PaperFixRandomAttr.CentOfChoseM,value);
			}
		}
		/// <summary>
		/// 判断
		/// </summary>
		public int CentOfJudgeTheme
		{
			get
			{
				return this.GetValIntByKey(PaperFixRandomAttr.CentOfJudgeTheme);
			}
			set
			{
				this.SetValByKey(PaperFixRandomAttr.CentOfJudgeTheme,value);
			}
		}
		/// <summary>
		/// 问答
		/// </summary>
		public int CentOfEssayQuestion
		{
			get
			{
				return this.GetValIntByKey(PaperFixRandomAttr.CentOfEssayQuestion);
			}
			set
			{
				this.SetValByKey(PaperFixRandomAttr.CentOfEssayQuestion,value);
			}
		}
		/// <summary>
		/// 阅读
		/// </summary>
		public int CentOfRC
		{
			get
			{
				return this.GetValIntByKey(PaperFixRandomAttr.CentOfRC);
			}
			set
			{
				this.SetValByKey(PaperFixRandomAttr.CentOfRC,value);
			}
		}
		/// <summary>
		/// 填空
		/// </summary>
		public int CentOfFillBlank
		{
			get
			{
				return this.GetValIntByKey(PaperFixRandomAttr.CentOfFillBlank);
			}
			set
			{
				this.SetValByKey(PaperFixRandomAttr.CentOfFillBlank,value);
			}
		}
		/// <summary>
		/// 合计
		/// </summary>
		public int CentOfSum
		{
			get
			{
				return this.GetValIntByKey(PaperFixRandomAttr.CentOfSum);
			}
			set
			{
				this.SetValByKey(PaperFixRandomAttr.CentOfSum,value);
			}
		}
		/// <summary>
		/// 时长
		/// </summary>
		public int MM
		{
			get
			{
				return this.GetValIntByKey(PaperFixRandomAttr.MM);
			}
			set
			{
				this.SetValByKey(PaperFixRandomAttr.MM,value);
			}
		}
		#endregion

		#region attrs ext
		/// <summary>
		/// 每个单项选择题得分。
		/// </summary>
		public int SumCentOfChoseOne
		{
			get
			{
				try
				{
					return this.CentOfChoseOne*this.HisChoseOnes.Count;
				}
				catch
				{
					return 0 ;
				}
			}
		}
		/// <summary>
		/// duo项选择题得分。
		/// </summary>
		public int SumCentOfChoseM
		{
			get
			{
				try
				{
					return this.CentOfChoseM * this.HisChoseMs.Count;
				}
				catch
				{
					return 0;
				}
			}
		}
		/// <summary>
		/// 判断题的分。
		/// </summary>
		public int SumCentOfJudgeTheme
		{
			get
			{
				try
				{
					return this.CentOfJudgeTheme * this.HisJudgeThemes.Count;
				}
				catch
				{
					return 0;
				}
			}
		}
		/// <summary>
		/// 填空题
		/// </summary>
		public int SumCentOfFillBlank
		{
			get
			{
				try
				{
					int blankNum=DBAccess.RunSQLReturnValInt("select count(BlankNum) from GTS_FillBlank where No in  ( SELECT FK_FillBlank from GTS_PaperVSFillBlank where FK_Paper='"+this.No+"' )");
					return this.CentOfFillBlank *blankNum;
				}
				catch
				{
					return 0 ;
				}
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
				
				Map map = new Map("GTS_PaperFixRandom");
				map.EnDesc="随机试卷";
				map.CodeStruct="4";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(PaperFixRandomAttr.No,null,"编号",true,true,0,50,20);
				map.AddTBString(PaperFixRandomAttr.Name,null,"试卷名称",true,false,0,50,20);
				map.AddDDLSysEnum(PaperFixRandomAttr.PaperState,0,"试卷状态",true,true);				 
				map.AddTBInt(PaperFixRandomAttr.MM,90,"考试时间(分钟)",true,false);

				map.AddTBInt(PaperFixRandomAttr.CentOfChoseOne,0,"单选题每题分",true,false);
				map.AddTBInt(PaperFixRandomAttr.CentOfChoseM,0,"多选题每题分",true,false);
				map.AddTBInt(PaperFixRandomAttr.CentOfFillBlank,0,"填空题每题分",true,false);

				map.AddTBInt(PaperFixRandomAttr.CentOfJudgeTheme,0,"判断题每题分",true,false);
				map.AddTBInt(PaperFixRandomAttr.CentOfEssayQuestion,0,"问答题每题分",true,true);

				map.AddTBInt(PaperFixRandomAttr.CentOfRC,0,"阅读题总分",true,true);
				map.AddTBInt(PaperFixRandomAttr.CentOfSum,0,"总分",true,true);

				map.AddTBString(PaperAttr.HisExamEmp,null,"HisExamEmp",false,true,0,50,20);
				map.AddTBString(PaperAttr.HisPaperRandomNo,null,"HisExamEmp",false,false,0,50,20);

				map.AttrsOfOneVSM.Add( new PaperVSChoseOnes(), new ChoseOnes(),PaperVSChoseOneAttr.FK_Paper,PaperVSChoseOneAttr.FK_Chose, ChoseOneAttr.Name,ChoseOneAttr.No,"单选题");
				map.AttrsOfOneVSM.Add( new PaperVSChoseMs(), new ChoseMs(),PaperVSChoseMAttr.FK_Paper,PaperVSChoseMAttr.FK_Chose, ChoseMAttr.Name,ChoseMAttr.No,"多选题");
				map.AttrsOfOneVSM.Add( new PaperVSFillBlanks(), new FillBlanks(),PaperVSFillBlankAttr.FK_Paper,PaperVSFillBlankAttr.FK_FillBlank, FillBlankAttr.Name,FillBlankAttr.No,"填空题");
				map.AttrsOfOneVSM.Add( new PaperVSJudgeThemes(), new JudgeThemes(),PaperVSJudgeThemeAttr.FK_Paper,PaperVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"判断题");
				map.AttrsOfOneVSM.Add( new PaperVSEssayQuestions(), new EssayQuestions(),PaperVSEssayQuestionAttr.FK_Paper,PaperVSEssayQuestionAttr.FK_EssayQuestion, EssayQuestionAttr.Name,EssayQuestionAttr.No,"问答题");
				map.AttrsOfOneVSM.Add( new PaperVSRCs(), new RCs(),PaperVSRCAttr.FK_Paper,PaperVSRCAttr.FK_RC, RCAttr.Name,RCAttr.No,"阅读题");
				map.AttrsOfOneVSM.Add( new PaperVSEmps(), new EmpExts(),PaperVSEmpAttr.FK_Paper,PaperVSEmpAttr.FK_Emp, RCAttr.Name,RCAttr.No,"考试的学生");

				//map.AddDtl(new PaperFixRandomExams(),PaperFixRandomExamAttr.FK_Paper);
				map.AddDtl(new PaperVSEssayQuestions(),PaperVSEssayQuestionAttr.FK_Paper);
				map.AddDtl(new PaperVSRCDtls(),PaperVSRCAttr.FK_Paper);
				map.AddSearchAttr(PaperFixRandomAttr.PaperState );

				//map.AttrsOfOneVSM.Add( new PaperVSChoseOnes(), new choseonhemes(),PaperVSJudgeThemeAttr.FK_Paper,PaperVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"判断题设计");
				//map.AttrsOfOneVSM.Add( new PaperVSChoseMs(), new JudgeThemes(),PaperVSJudgeThemeAttr.FK_Paper,PaperVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"判断题设计");
				this._enMap=map;
				return this._enMap;
			}
		}
		protected override void afterDelete()
		{
			DBAccess.RunSQL("DELETE GTS_PaperVSChoseOne WHERE FK_Paper='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_PaperVSChoseM WHERE FK_Paper='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_PaperVSJudgeTheme WHERE FK_Paper='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_PaperVSFillBlank WHERE FK_Paper='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_PaperVSEssayQuestion WHERE FK_Paper='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_PaperVSRC WHERE FK_Paper='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_PaperVSRCDtl WHERE FK_Paper='"+this.No+"'");
		
			DBAccess.RunSQL("DELETE GTS_PaperExam WHERE FK_Paper='"+this.No+"'");
			base.afterDelete ();
		}

		protected override bool beforeUpdateInsertAction()
		{
			this.CentOfEssayQuestion = DBAccess.RunSQLReturnValInt("select isnull( sum(cent), 0) from GTS_PaperVSEssayQuestion where FK_Paper='"+this.No+"'") ;
			this.CentOfRC = DBAccess.RunSQLReturnValInt("select isnull( sum(cent), 0) from GTS_PaperVSRCDtl where FK_Paper='"+this.No+"'") ;
			this.CentOfSum=this.SumCentOfFillBlank+this.CentOfEssayQuestion+this.SumCentOfChoseOne+this.SumCentOfChoseM+this.SumCentOfJudgeTheme+this.CentOfRC;
			return base.beforeUpdateInsertAction();
		}
		#endregion 

		#region 构造方法
		/// <summary>
		/// 试卷
		/// </summary> 
		public PaperFixRandom()
		{
		}
		/// <summary>
		/// 试卷
		/// </summary>
		/// <param name="_No">征收机关编号</param> 
		public PaperFixRandom(string _No ):base(_No)
		{

		}
		#endregion 

		#region 逻辑处理
		#endregion

	}
	/// <summary>
	///  试卷
	/// </summary>
	public class PaperFixRandoms :EntitiesNoName
	{
		/// <summary>
		/// PaperFixRandoms
		/// </summary>
		public PaperFixRandoms(){}

		public PaperFixRandoms(string fk_randomNo, string fk_emp)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(PaperAttr.HisExamEmp, fk_emp);
			qo.addAnd();
			qo.AddWhere(PaperAttr.HisPaperRandomNo, fk_randomNo);
			qo.DoQuery();
		}
		/// <summary>
		/// PaperFixRandom
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperFixRandom();
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="state"></param>
		/// <returns></returns>
		public int RetrievePaperFixRandom(PaperState state )
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(PaperFixRandomAttr.PaperState, (int)state);
			return qo.DoQuery();
		}
		 
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fk_emp"></param>
		/// <returns></returns> 
		public int RetrievePaperFixRandom(string fk_emp)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhereInSQL(PaperFixRandomAttr.No,  "SELECT FK_Paper FROM GTS_PaperVSEmp WHERE FK_Emp='"+fk_emp+"'");
			return qo.DoQuery();
		}
	 
	}

	 
}
