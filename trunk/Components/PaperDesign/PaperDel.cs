using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// 试卷attr
	/// </summary>
	public class PaperAttr:EntityNoNameAttr
	{
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
		/// 是否检查范围
		/// </summary>
		public const string IsCheckScope="IsCheckScope";
	}
	/// <summary>
	/// 试卷类型
	/// </summary>
	public enum PaperType
	{
		/// <summary>
		/// 标准试卷
		/// </summary>
		Standard,
		/// <summary>
		/// 随机学生试卷
		/// </summary>
		Random
	}
	/// <summary>
	/// 试卷状态
	/// </summary>
	public enum PaperState
	{
		/// <summary>
		/// 保秘
		/// </summary>
		Secrecy,
		/// <summary>
		/// 考试
		/// </summary>
		Examing,
		/// <summary>
		/// 作废
		/// </summary>
		Cancellation,
	}
	/// <summary>
	/// 试卷
	/// </summary>
	public class Paper :EntityNoName
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
				qo.AddWhereInSQL( ChoseOneAttr.No, "select fk_chose from GTS_PaperVSChoseOne where fk_paper='"+this.No+"'");
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
				qo.AddWhereInSQL( ChoseOneAttr.No, "select fk_chose from GTS_PaperVSChoseM where fk_paper='"+this.No+"'");
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
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_JudgeTheme from GTS_PaperVSJudgeTheme where fk_paper='"+this.No+"'");
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
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_EssayQuestion from GTS_PaperVSEssayQuestion where fk_paper='"+this.No+"'");
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
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_RC from GTS_PaperVSRC where fk_paper='"+this.No+"'");
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
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_FillBlank from GTS_PaperVSFillBlank where fk_paper='"+this.No+"'");
				qo.DoQuery();
				return chs;
			}
		}
		#endregion

		#region attrs
		/// <summary>
		/// 试卷类型
		/// </summary>
		public PaperType PaperType
		{
			get
			{
				return (PaperType)this.GetValIntByKey(PaperAttr.PaperType);
			}
			set
			{
				this.SetValByKey(PaperAttr.PaperType,(int)value);
			}
		}
		/// <summary>
		/// 试卷状态
		/// </summary>
		public PaperState PaperState
		{
			get
			{
				return (PaperState)this.GetValIntByKey(PaperAttr.PaperState);
			}
			set
			{
				this.SetValByKey(PaperAttr.PaperState,(int)value);
			}
		}
		public string PaperStateText
		{
			get
			{
				return this.GetValRefTextByKey(PaperAttr.PaperState);
			}
		}
		/// <summary>
		/// 单选
		/// </summary>
		public int CentOfChoseOne
		{
			get
			{
				return this.GetValIntByKey(PaperAttr.CentOfChoseOne);
			}
			set
			{
				this.SetValByKey(PaperAttr.CentOfChoseOne,value);
			}
		}
		/// <summary>
		/// 多选
		/// </summary>
		public int CentOfChoseM
		{
			get
			{
				return this.GetValIntByKey(PaperAttr.CentOfChoseM);
			}
			set
			{
				this.SetValByKey(PaperAttr.CentOfChoseM,value);
			}
		}
		/// <summary>
		/// 随机试卷编号。
		/// </summary>
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
		/// 判断
		/// </summary>
		public int CentOfJudgeTheme
		{
			get
			{
				return this.GetValIntByKey(PaperAttr.CentOfJudgeTheme);
			}
			set
			{
				this.SetValByKey(PaperAttr.CentOfJudgeTheme,value);
			}
		}
		/// <summary>
		/// 问答
		/// </summary>
		public int CentOfEssayQuestion
		{
			get
			{
				return this.GetValIntByKey(PaperAttr.CentOfEssayQuestion);
			}
			set
			{
				this.SetValByKey(PaperAttr.CentOfEssayQuestion,value);
			}
		}
		/// <summary>
		/// 阅读
		/// </summary>
		public int CentOfRC
		{
			get
			{
				return this.GetValIntByKey(PaperAttr.CentOfRC);
			}
			set
			{
				this.SetValByKey(PaperAttr.CentOfRC,value);
			}
		}
		/// <summary>
		/// 填空
		/// </summary>
		public int CentOfFillBlank
		{
			get
			{
				return this.GetValIntByKey(PaperAttr.CentOfFillBlank);
			}
			set
			{
				this.SetValByKey(PaperAttr.CentOfFillBlank,value);
			}
		}
		/// <summary>
		/// 合计
		/// </summary>
		public int CentOfSum
		{
			get
			{
				return this.GetValIntByKey(PaperAttr.CentOfSum);
			}
			set
			{
				this.SetValByKey(PaperAttr.CentOfSum,value);
			}
		}
		/// <summary>
		/// 时长
		/// </summary>
		public int MM
		{
			get
			{
				return this.GetValIntByKey(PaperAttr.MM);
			}
			set
			{
				this.SetValByKey(PaperAttr.MM,value);
			}
		}
		#endregion

		#region attrs ext
		/// <summary>
		/// 每个单项选择题得分。
		/// </summary>
		public int PerCentOfChoseOne
		{
			get
			{
				try
				{
					return this.CentOfChoseOne/this.HisChoseOnes.Count;
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
		public int PerCentOfChoseM
		{
			get
			{
				try
				{
					return this.CentOfChoseM/this.HisChoseMs.Count;
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
		public int PerCentOfJudgeTheme
		{
			get
			{
				try
				{
					return this.CentOfJudgeTheme/this.HisJudgeThemes.Count;
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
		public int PerCentOfFillBlank
		{
			get
			{
				try
				{
					int blankNum=DBAccess.RunSQLReturnValInt("select count(BlankNum) from GTS_FillBlank where No in  ( SELECT FK_FillBlank from GTS_PaperVSFillBlank where fk_paper='"+this.No+"' )");
					return this.CentOfFillBlank/blankNum;
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
				
				Map map = new Map("V_GTS_Paper");
				map.EnDesc="固定试卷";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;
				map.AddTBStringPK(PaperAttr.No,null,"编号",true,true,0,50,20);
				map.AddTBString(PaperAttr.Name,null,"试卷名称",true,false,0,50,20);
				map.AddDDLSysEnum(PaperAttr.PaperState,0,"试卷状态",true,true);
				map.AddDDLSysEnum(PaperAttr.PaperType,0,"试卷类型",true,true);
				map.AddTBInt(PaperAttr.MM,90,"考试时间(分钟)",true,false);

				map.AddTBInt(PaperAttr.CentOfChoseOne,0,"单选题分",true,false);
				map.AddTBInt(PaperAttr.CentOfChoseM,0,"多选题分",true,false);
				map.AddTBInt(PaperAttr.CentOfFillBlank,0,"填空题分",true,false);
				map.AddTBInt(PaperAttr.CentOfJudgeTheme,0,"判断题分",true,false);
				map.AddTBInt(PaperAttr.CentOfEssayQuestion,0,"问答题分",true,true);
				map.AddTBInt(PaperAttr.CentOfRC,0,"阅读题分",true,true);
				map.AddTBInt(PaperAttr.CentOfSum,0,"总分",true,true);

				map.AddTBString(PaperAttr.HisExamEmp,"a","HisExamEmp",false,true,0,50,20);
				map.AddTBString(PaperAttr.HisPaperRandomNo,"a","HisExamEmp",false,false,0,50,20);



 


				map.AttrsOfOneVSM.Add( new PaperVSChoseOnes(), new ChoseOnes(),PaperVSChoseOneAttr.FK_Paper,PaperVSChoseOneAttr.FK_Chose, ChoseOneAttr.Name,ChoseOneAttr.No,"单选题");
				map.AttrsOfOneVSM.Add( new PaperVSChoseMs(), new ChoseMs(),PaperVSChoseMAttr.FK_Paper,PaperVSChoseMAttr.FK_Chose, ChoseMAttr.Name,ChoseMAttr.No,"多选题");
				map.AttrsOfOneVSM.Add( new PaperVSFillBlanks(), new FillBlanks(),PaperVSFillBlankAttr.FK_Paper,PaperVSFillBlankAttr.FK_FillBlank, FillBlankAttr.Name,FillBlankAttr.No,"填空题");
				map.AttrsOfOneVSM.Add( new PaperVSJudgeThemes(), new JudgeThemes(),PaperVSJudgeThemeAttr.FK_Paper,PaperVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"判断题");
				map.AttrsOfOneVSM.Add( new PaperVSEssayQuestions(), new EssayQuestions(),PaperVSEssayQuestionAttr.FK_Paper,PaperVSEssayQuestionAttr.FK_EssayQuestion, EssayQuestionAttr.Name,EssayQuestionAttr.No,"问答题");
				map.AttrsOfOneVSM.Add( new PaperVSRCs(), new RCs(),PaperVSRCAttr.FK_Paper,PaperVSRCAttr.FK_RC, RCAttr.Name,RCAttr.No,"阅读题");
				map.AttrsOfOneVSM.Add( new PaperVSEmps(), new EmpExts(),PaperVSEmpAttr.FK_Paper,PaperVSEmpAttr.FK_Emp, RCAttr.Name,RCAttr.No,"考试的学生");

				//map.AddDtl(new PaperExams(),PaperExamAttr.FK_Paper);
				map.AddDtl(new PaperVSEssayQuestions(),PaperVSEssayQuestionAttr.FK_Paper);
				map.AddDtl(new PaperVSRCDtls(),PaperVSRCAttr.FK_Paper);
				map.AddSearchAttr(PaperAttr.PaperState );
				map.AddSearchAttr(PaperAttr.PaperType );

				//map.AttrsOfOneVSM.Add( new PaperVSChoseOnes(), new choseonhemes(),PaperVSJudgeThemeAttr.FK_Paper,PaperVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"判断题设计");
				//map.AttrsOfOneVSM.Add( new PaperVSChoseMs(), new JudgeThemes(),PaperVSJudgeThemeAttr.FK_Paper,PaperVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"判断题设计");
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		#region 构造方法
		/// <summary>
		/// 试卷
		/// </summary> 
		public Paper()
		{
		}
		/// <summary>
		/// 试卷
		/// </summary>
		/// <param name="_No">征收机关编号</param> 
		public Paper(string _No ):base(_No)
		{
		}
		#endregion 

		#region 逻辑处理
		#endregion

	}
	/// <summary>
	///  试卷
	/// </summary>
	public class Papers :EntitiesNoName
	{
		/// <summary>
		/// Papers
		/// </summary>
		public Papers(){}
		/// <summary>
		/// Paper
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Paper();
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="state"></param>
		/// <returns></returns>
		public int RetrievePaper(PaperState state )
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(PaperAttr.PaperState, (int)state);
			return qo.DoQuery();
		}
		/// <summary>
		/// 查询出来当前我的固定paper.
		/// </summary>
		/// <param name="fk_emp"></param>
		/// <returns></returns>
		public int RetrievePaper(string fk_emp, PaperState state )
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhereInSQL(PaperAttr.No,  "SELECT FK_Paper FROM GTS_PaperVSEmp WHERE FK_Paper NOT IN (SELECT FK_Paper FROM GTS_PaperExam) AND len(FK_Paper)=5 and FK_Emp='"+fk_emp+"'");
			qo.addAnd();
			qo.AddWhere(PaperAttr.PaperState, (int)state);
			return qo.DoQuery();
		}
		public int RetrieveHisRandom (string fk_emp, string fk_random  )
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(PaperAttr.HisExamEmp,fk_emp );
			qo.addAnd();
			qo.AddWhere(PaperAttr.HisPaperRandomNo, fk_random );
			return qo.DoQuery();
		}
	}

	 
}
