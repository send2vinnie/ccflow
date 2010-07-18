using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// 试卷attr
	/// </summary>
	public class PaperFixRandomAttr
	{
		/// <summary>
		/// OID
		/// </summary>
		public const string OID="OID";
		/// <summary>
		/// FK_PaperFixRandom
		/// </summary>
		public const string FK_PaperFixRandom="FK_PaperFixRandom";
		/// <summary>
		/// fk_emp
		/// </summary>
		public const string FK_Emp="FK_Emp";
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
	public class PaperFixRandom :EntityOID
	{
 
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
		#endregion 

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
				map.EnDesc="固定试卷";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				Map map = new Map("GTS_PaperFix");
				map.EnDesc="固定试卷";
				map.CodeStruct="7";
				map.EnType= EnType.Admin;
				map.AddTBStringPK(PaperFixAttr.No,null,"编号",true,true,0,50,20);
				map.AddTBString(PaperFixAttr.Name,"新建试卷1","试卷名称",true,false,0,50,20);
				map.AddTBInt(PaperFixAttr.MM,90,"考试时间(分钟)",true,false);

				map.AddDDLEntitiesNoName(PaperFixRandomAttr.FK_PaperRandom,null,"随机试卷",true);
				map.AddDDLEntitiesNoName(PaperFixRandomAttr.FK_Emp,Web.WebUser.No,"操作员",true);

				map.AddTBInt(PaperFixAttr.CentOfChoseOne,1,"单选题每题分",true,false);
				map.AddTBInt(PaperFixAttr.CentOfChoseM,1,"多选题每题分",true,false);
				map.AddTBInt(PaperFixAttr.CentOfFillBlank,1,"填空题每题分",true,false);

				map.AddTBInt(PaperFixAttr.CentOfJudgeTheme,1,"判断题每题分",true,false);
				map.AddTBInt(PaperFixAttr.CentOfEssayQuestion,1,"问答题分(自动计算)",true,true);

				map.AddTBInt(PaperFixAttr.CentOfRC,20,"阅读题分(自动计算)",true,true);
				map.AddTBInt(PaperFixAttr.CentOfSum,0,"总分(自动计算)",true,true);

				map.AttrsOfOneVSM.Add( new PaperVSChoseOnes(), new ChoseOnes(),PaperVSChoseOneAttr.FK_Paper,PaperVSChoseOneAttr.FK_Chose, ChoseOneAttr.Name,ChoseOneAttr.No,"单选题");
				map.AttrsOfOneVSM.Add( new PaperVSChoseMs(), new ChoseMs(),PaperVSChoseMAttr.FK_Paper,PaperVSChoseMAttr.FK_Chose, ChoseMAttr.Name,ChoseMAttr.No,"多选题");
				map.AttrsOfOneVSM.Add( new PaperVSFillBlanks(), new FillBlanks(),PaperVSFillBlankAttr.FK_Paper,PaperVSFillBlankAttr.FK_FillBlank, FillBlankAttr.Name,FillBlankAttr.No,"填空题");
				map.AttrsOfOneVSM.Add( new PaperVSJudgeThemes(), new JudgeThemes(),PaperVSJudgeThemeAttr.FK_Paper,PaperVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"判断题");
				map.AttrsOfOneVSM.Add( new PaperVSEssayQuestions(), new EssayQuestions(),PaperVSEssayQuestionAttr.FK_Paper,PaperVSEssayQuestionAttr.FK_EssayQuestion, EssayQuestionAttr.Name,EssayQuestionAttr.No,"问答题");
				map.AttrsOfOneVSM.Add( new PaperVSRCs(), new RCs(),PaperVSRCAttr.FK_Paper,PaperVSRCAttr.FK_RC, RCAttr.Name,RCAttr.No,"阅读题");

				map.AttrsOfOneVSM.Add( new PaperVSEmps(), new EmpExts(),PaperVSEmpAttr.FK_Paper,PaperVSEmpAttr.FK_Emp, RCAttr.Name,RCAttr.No,"考试的学生");
				map.AddDtl(new PaperVSEssayQuestions(),PaperVSEssayQuestionAttr.FK_Paper);
				map.AddDtl(new PaperVSRCDtls(),PaperVSRCAttr.FK_Paper);
 
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
			DBAccess.RunSQL("DELETE GTS_PaperVSEmp WHERE FK_Paper='"+this.No+"'");
			base.afterDelete ();
		}

		protected override bool beforeUpdateInsertAction()
		{
			this.CentOfEssayQuestion = DBAccess.RunSQLReturnValInt("select isnull( sum(cent), 0) from GTS_PaperVSEssayQuestion where FK_Paper='"+this.No+"'") ;
			this.CentOfRC = DBAccess.RunSQLReturnValInt("select isnull( sum(cent), 0) from GTS_PaperVSRCDtl where FK_Paper='"+this.No+"'") ;
			this.CentOfSum=this.SumCentOfFillBlank+this.CentOfEssayQuestion+this.SumCentOfChoseOne+this.SumCentOfChoseM+this.SumCentOfJudgeTheme+this.CentOfRC;

			// 初始化考生 , 在考试时间设置考生范围, 给每个考生初始化他的试卷。
			string sql="";
			sql="DELETE GTS_PaperExam WHERE FK_Emp NOT IN (SELECT FK_Emp FROM GTS_PaperVSEmp WHERE FK_Paper='"+this.No+"') AND FK_Paper='"+this.No+"'";
			DBAccess.RunSQL(sql); // 删除没有。

			sql="SELECT FK_Emp FROM GTS_PaperVSEmp WHERE FK_Paper='"+this.No+"' AND FK_Emp ";
			sql+="NOT IN (SELECT FK_Emp FROM GTS_PaperExam WHERE FK_Paper='"+this.No+"')";
			DataTable dt = DBAccess.RunSQLReturnTable(sql);
			foreach(DataRow dr in dt.Rows)
			{
				PaperExam pe = new PaperExam() ; 
				pe.FK_Emp=dr[0].ToString();
				pe.FK_Paper=this.No;
				pe.Insert();
			}
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
	public class PaperFixRandoms :EntitiesOID
	{
 
		/// <summary>
		/// PaperFixRandoms
		/// </summary>
		public PaperFixRandoms(){}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fk_emp">操作员</param>
		public PaperFixRandoms(string fk_emp)
		{
			QueryObject qo= new QueryObject(this);
			qo.AddWhereInSQL(PaperFixRandomAttr.No, "SELECT FK_Paper FROM GTS_PaperVSEmp WHERE FK_Emp='"+Web.WebUser.No+"'" );
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
