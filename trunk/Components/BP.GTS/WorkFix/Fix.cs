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
	/// 固
	/// </summary>
	public class FixAttr:EntityNoNameAttr
	{
		/// <summary>
		/// 试卷状态
		/// </summary>
		public const string State="State";
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
	/// 固定作业
	/// </summary>
	public class Fix :EntityNoName
	{
		#region attrs
		/// <summary>
		/// 单选
		/// </summary>
		public int CentOfChoseOne
		{
			get
			{
				return this.GetValIntByKey(FixAttr.CentOfChoseOne);
			}
			set
			{
				this.SetValByKey(FixAttr.CentOfChoseOne,value);
			}
		}
		/// <summary>
		/// 多选
		/// </summary>
		public int CentOfChoseM
		{
			get
			{
				return this.GetValIntByKey(FixAttr.CentOfChoseM);
			}
			set
			{
				this.SetValByKey(FixAttr.CentOfChoseM,value);
			}
		}
		#endregion 

		#region his attrs
		/// <summary>
		/// 考试集合
		/// </summary>
		public Exams HisExams
		{
			get
			{
				Exams chs = new Exams(this.No);
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
				qo.AddWhereInSQL( ChoseOneAttr.No, "select fk_chose from GTS_VSChoseOne where FK_='"+this.No+"'");
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
				qo.AddWhereInSQL( ChoseOneAttr.No, "select fk_chose from GTS_VSChoseM where FK_='"+this.No+"'");
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
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_JudgeTheme from GTS_VSJudgeTheme where FK_='"+this.No+"'");
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
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_EssayQuestion from GTS_VSEssayQuestion where FK_='"+this.No+"'");
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
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_RC from GTS_VSRC where FK_='"+this.No+"'");
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
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_FillBlank from GTS_VSFillBlank where FK_='"+this.No+"'");
				qo.DoQuery();
				return chs;
			}
		}
		#endregion

		#region attrs 

		public string ValidTimeFrom
		{
			get
			{
				return this.GetValStringByKey(RandomAttr.ValidTimeFrom);
			}
			set
			{
				this.SetValByKey(RandomAttr.ValidTimeFrom,value);
			}
		}
		public string ValidTimeTo
		{
			get
			{
				return this.GetValStringByKey(RandomAttr.ValidTimeTo);
			}
			set
			{
				this.SetValByKey(RandomAttr.ValidTimeTo,value);
			}
		}
		/// <summary>
		/// 是否超过了时间
		/// </summary>
		public bool IsPastTime
		{
			get
			{

				DateTime dtto=DataType.ParseSysDateTime2DateTime(this.ValidTimeTo);
				DateTime dt=DateTime.Now;
				if ( dtto>= dt )
					return true;
				else
					return false;
			}
		}
		/// <summary>
		/// 是否在考试与作业的时间范围内
		/// </summary>
		public bool IsValid
		{
			get
			{
				DateTime dtfrom=DataType.ParseSysDateTime2DateTime(this.ValidTimeFrom);
				DateTime dtto=DataType.ParseSysDateTime2DateTime(this.ValidTimeTo);
				DateTime dt=DateTime.Now;

				if ( dtfrom <= dt )
				{
					if ( dtto>= dt )
						return true;
					else
						return false;
				}
				else
					return false;
			}
		}
		/// <summary>
		/// 判断
		/// </summary>
		public int CentOfJudgeTheme
		{
			get
			{
				return this.GetValIntByKey(FixAttr.CentOfJudgeTheme);
			}
			set
			{
				this.SetValByKey(FixAttr.CentOfJudgeTheme,value);
			}
		}
		/// <summary>
		/// 问答
		/// </summary>
		public int CentOfEssayQuestion
		{
			get
			{
				return this.GetValIntByKey(FixAttr.CentOfEssayQuestion);
			}
			set
			{
				this.SetValByKey(FixAttr.CentOfEssayQuestion,value);
			}
		}
		/// <summary>
		/// 阅读
		/// </summary>
		public int CentOfRC
		{
			get
			{
				return this.GetValIntByKey(FixAttr.CentOfRC);
			}
			set
			{
				this.SetValByKey(FixAttr.CentOfRC,value);
			}
		}
		/// <summary>
		/// 填空
		/// </summary>
		public int CentOfFillBlank
		{
			get
			{
				return this.GetValIntByKey(FixAttr.CentOfFillBlank);
			}
			set
			{
				this.SetValByKey(FixAttr.CentOfFillBlank,value);
			}
		}
		/// <summary>
		/// 合计
		/// </summary>
		public int CentOfSum
		{
			get
			{
				return this.GetValIntByKey(FixAttr.CentOfSum);
			}
			set
			{
				this.SetValByKey(FixAttr.CentOfSum,value);
			}
		}
		/// <summary>
		/// 时长
		/// </summary>
		public int MM
		{
			get
			{
				return this.GetValIntByKey(FixAttr.MM);
			}
			set
			{
				this.SetValByKey(FixAttr.MM,value);
			}
		}
		#endregion

		#region attrs ext
		public bool IsExam
		{
			get
			{
				if (this.MM==0)
					return false;
				else
					return true;
			}
		}
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
					int blankNum=DBAccess.RunSQLReturnValInt("select count(BlankNum) from GTS_FillBlank where No in  ( SELECT FK_FillBlank from GTS_VSFillBlank where FK_='"+this.No+"' )");
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
				
				Map map = new Map("GTS_Fix");
				map.EnDesc="固定题设计";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;
				map.AddTBStringPK(FixAttr.No,null,"编号",true,true,0,50,20);
				map.AddTBString(FixAttr.Name,"新建1","名称",true,false,0,50,20);

				DateTime dt = DateTime.Now;
				dt=dt.AddDays(1);
				map.AddTBDateTime(RandomAttr.ValidTimeFrom,dt.ToString("yyyy-MM-dd")+" 09:00","有效时间从",true,false);
				map.AddTBDateTime(RandomAttr.ValidTimeTo,dt.ToString("yyyy-MM-dd")+" 10:00","到",true,false);

				map.AddTBInt(FixAttr.MM,90,"考试时间(分钟)",true,false);

				map.AddTBInt(FixAttr.CentOfChoseOne,1,"单选题每题分",true,false);
				map.AddTBInt(FixAttr.CentOfChoseM,1,"多选题每题分",true,false);
				map.AddTBInt(FixAttr.CentOfFillBlank,1,"填空题每题分",true,false);

				map.AddTBInt(FixAttr.CentOfJudgeTheme,1,"判断题每题分",true,false);
				map.AddTBInt(FixAttr.CentOfEssayQuestion,1,"问答题分",true,true);

				map.AddTBInt(FixAttr.CentOfRC,20,"阅读题分",true,true);
				map.AddTBInt(FixAttr.CentOfSum,0,"总分",true,true);

				map.AttrsOfOneVSM.Add( new VSChoseOnes(), new ChoseOnes(),VSChoseOneAttr.FK_,VSChoseOneAttr.FK_Chose, ChoseOneAttr.Name,ChoseOneAttr.No,"单选题");
				map.AttrsOfOneVSM.Add( new VSChoseMs(), new ChoseMs(),VSChoseMAttr.FK_,VSChoseMAttr.FK_Chose, ChoseMAttr.Name,ChoseMAttr.No,"多选题");
				map.AttrsOfOneVSM.Add( new VSFillBlanks(), new FillBlanks(),VSFillBlankAttr.FK_,VSFillBlankAttr.FK_FillBlank, FillBlankAttr.Name,FillBlankAttr.No,"填空题");
				map.AttrsOfOneVSM.Add( new VSJudgeThemes(), new JudgeThemes(),VSJudgeThemeAttr.FK_,VSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"判断题");
				map.AttrsOfOneVSM.Add( new VSEssayQuestions(), new EssayQuestions(),VSEssayQuestionAttr.FK_,VSEssayQuestionAttr.FK_EssayQuestion, EssayQuestionAttr.Name,EssayQuestionAttr.No,"问答题");
				map.AttrsOfOneVSM.Add( new VSRCs(), new RCs(),VSRCAttr.FK_,VSRCAttr.FK_RC, RCAttr.Name,RCAttr.No,"阅读题");

				map.AttrsOfOneVSM.Add( new VSEmps(), new EmpExts(),VSEmpAttr.FK_,VSEmpAttr.FK_Emp, RCAttr.Name,RCAttr.No,"考试的学生");
				map.AddDtl(new VSEssayQuestions(),VSEssayQuestionAttr.FK_);
				map.AddDtl(new VSRCDtls(),VSRCAttr.FK_);

				this._enMap=map;
				return this._enMap;
			}
		}
		protected override void afterDelete()
		{
			DBAccess.RunSQL("DELETE GTS_VSChoseOne WHERE FK_='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_VSChoseM WHERE FK_='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_VSJudgeTheme WHERE FK_='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_VSFillBlank WHERE FK_='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_VSEssayQuestion WHERE FK_='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_VSRC WHERE FK_='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_VSRCDtl WHERE FK_='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_Exam WHERE FK_='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_VSEmp WHERE FK_='"+this.No+"'");
			base.afterDelete ();
		}

		protected override bool beforeUpdateInsertAction()
		{

			// 判断是否有考生做题。
			string sql="SELECT COUNT(*) FROM GTS_Exam WHERE ExamState >0 and FK_='"+this.No+"'";
			int i=DBAccess.RunSQLReturnValInt(sql);
			if (i>=1)
				throw new Exception("试卷["+this.Name+"], 不能被您再设计，因为它已经有“"+i+"”个考生开始做题。");

			// 设置分数。
			this.CentOfEssayQuestion = DBAccess.RunSQLReturnValInt("select isnull( sum(cent), 0) from GTS_VSEssayQuestion where FK_='"+this.No+"'") ;
			this.CentOfRC = DBAccess.RunSQLReturnValInt("select isnull( sum(cent), 0) from GTS_VSRCDtl where FK_='"+this.No+"'") ;
			this.CentOfSum=this.SumCentOfFillBlank+this.CentOfEssayQuestion+this.SumCentOfChoseOne+this.SumCentOfChoseM+this.SumCentOfJudgeTheme+this.CentOfRC;

			// 初始化考生 , 在考试时间设置考生范围, 给每个考生初始化他的试卷。
			sql="DELETE GTS_Exam WHERE FK_Emp NOT IN (SELECT FK_Emp FROM GTS_VSEmp WHERE FK_='"+this.No+"') AND FK_='"+this.No+"'";
			DBAccess.RunSQL(sql); // 删除没有。

			sql="SELECT FK_Emp FROM GTS_VSEmp WHERE FK_='"+this.No+"' AND FK_Emp ";
			sql+="NOT IN (SELECT FK_Emp FROM GTS_Exam WHERE FK_='"+this.No+"')";
			DataTable dt = DBAccess.RunSQLReturnTable(sql);
			foreach(DataRow dr in dt.Rows)
			{
				Exam pe = new Exam();
				pe.FK_Emp=dr[0].ToString();
				pe.FK_=this.No;
				pe.IsExam=this.IsExam;
				pe.Insert();
			}
			return base.beforeUpdateInsertAction();
		}
		#endregion 

		#region 构造方法
		/// <summary>
		/// 试卷
		/// </summary> 
		public Fix()
		{
		}
		/// <summary>
		/// 试卷
		/// </summary>
		/// <param name="_No">征收机关编号</param> 
		public Fix(string _No ):base(_No)
		{

		}
		#endregion 

		#region 逻辑处理
		#endregion

	}
	/// <summary>
	///  固定作业
	/// </summary>
	public class Fixs :EntitiesNoName
	{
		/// <summary>
		/// 固定作业s
		/// </summary>
		public Fixs(){}

		/// <summary>
		/// 固定作业s
		/// </summary>
		/// <param name="fk_emp">操作员</param>
		public Fixs(string fk_emp)
		{
			QueryObject qo= new QueryObject(this);
			qo.AddWhereInSQL(FixAttr.No, "SELECT FK_ FROM GTS_VSEmp WHERE FK_Emp='"+Web.WebUser.No+"'" );
			qo.DoQuery();
		}

		/// <summary>
		/// 固定作业
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Fix();
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fk_emp"></param>
		/// <returns></returns> 
		public int RetrieveFix(string fk_emp)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhereInSQL(FixAttr.No,  "SELECT FK_ FROM GTS_VSEmp WHERE FK_Emp='"+fk_emp+"'");
			return qo.DoQuery();
		}
	 
	}

	 
}
