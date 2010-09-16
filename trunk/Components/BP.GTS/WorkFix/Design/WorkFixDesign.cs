using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// 固定作业
	/// </summary>
	public class WorkFixDesignAttr:EntityNoNameAttr
	{
		/// <summary>
		/// 试卷状态
		/// </summary>
		public const string WorkState="WorkState";
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
		/// 到
		/// </summary>
		public const string ValidTimeTo="ValidTimeTo";

	}
	/// <summary>
	/// 固定作业
	/// </summary>
	public class WorkFixDesign :EntityNoName
	{
		#region attrs
		/// <summary>
		/// 单选
		/// </summary>
		public int CentOfChoseOne
		{
			get
			{
				return this.GetValIntByKey(WorkFixDesignAttr.CentOfChoseOne);
			}
			set
			{
				this.SetValByKey(WorkFixDesignAttr.CentOfChoseOne,value);
			}
		}
		/// <summary>
		/// 多选
		/// </summary>
		public int CentOfChoseM
		{
			get
			{
				return this.GetValIntByKey(WorkFixDesignAttr.CentOfChoseM);
			}
			set
			{
				this.SetValByKey(WorkFixDesignAttr.CentOfChoseM,value);
			}
		}
		#endregion 

		#region his attrs
		/// <summary>
		/// 他的单项选择题
		/// </summary>
		public ChoseOnes HisChoseOnes
		{
			get
			{
				//ChoseOnes ens = new ChoseOnes();
				if (BP.DA.Cash.IsExits("ChoseOnes"+this.No,Depositary.Session) ==false )
				{
					ChoseOnes chs = new ChoseOnes();
					QueryObject qo = new QueryObject(chs);
					qo.AddWhereInSQL( ChoseOneAttr.No, "SELECT FK_Chose FROM GTS_WorkVSChoseOne WHERE FK_Work='"+this.No+"'");
					qo.addOrderByRandom(); 
					qo.DoQuery();
					BP.DA.Cash.AddObj("ChoseOnes"+this.No, Depositary.Session,chs );
					return chs;
				}
				return (ChoseOnes)BP.DA.Cash.GetObjFormSession("ChoseOnes"+this.No);
				//				if (ens.Count==0)
				//				{
				//
				//				}
			}
		}
		/// <summary>
		/// 他的单项选择题
		/// </summary>
		public ChoseMs HisChoseMs
		{
			get
			{
				if (BP.DA.Cash.IsExits("HisChoseMs"+this.No,Depositary.Session) ==false )
				{
					ChoseMs chs = new ChoseMs();
					QueryObject qo = new QueryObject(chs);
					qo.AddWhereInSQL( ChoseOneAttr.No, "select fk_chose from GTS_WorkVSChoseM where FK_Work='"+this.No+"'");
					qo.addOrderByRandom(); 
					qo.DoQuery();
					BP.DA.Cash.AddObj("HisChoseMs"+this.No, Depositary.Session,chs );
					return chs;
				}
				return (ChoseMs)BP.DA.Cash.GetObjFormSession("HisChoseMs"+this.No);
			}
		}
		/// <summary>
		/// 判断题
		/// </summary>
		public JudgeThemes HisJudgeThemes
		{
			get
			{
				if (BP.DA.Cash.IsExits("JudgeThemes"+this.No,Depositary.Session) ==false )
				{
					JudgeThemes chs = new JudgeThemes();
					QueryObject qo = new QueryObject(chs);
					qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_JudgeTheme from GTS_WorkVSJudgeTheme where FK_Work='"+this.No+"'");
					qo.addOrderByRandom(); 
					qo.DoQuery();
					BP.DA.Cash.AddObj("JudgeThemes"+this.No, Depositary.Session,chs );
					return chs;
				}
				return (JudgeThemes)BP.DA.Cash.GetObjFormSession("JudgeThemes"+this.No);
			}
		}
		/// <summary>
		/// 简答体
		/// </summary>
		public EssayQuestions HisEssayQuestions
		{
			get
			{
				if (BP.DA.Cash.IsExits("EssayQuestions"+this.No,Depositary.Session) ==false )
				{
					EssayQuestions chs = new EssayQuestions();
					QueryObject qo = new QueryObject(chs);
					qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_EssayQuestion from GTS_WorkVSEssayQuestion where FK_Work='"+this.No+"'");
					qo.addOrderByRandom(); 
					qo.DoQuery();
					BP.DA.Cash.AddObj("EssayQuestions"+this.No, Depositary.Session,chs );
					return chs;
				}
				return (EssayQuestions)BP.DA.Cash.GetObjFormSession("EssayQuestions"+this.No);

			}
		}
		/// <summary>
		/// 阅读理解题目
		/// </summary>
		public RCs HisRCs
		{
			get
			{
				if (BP.DA.Cash.IsExits("RCs"+this.No,Depositary.Session) ==false )
				{
					RCs chs = new RCs();
					QueryObject qo = new QueryObject(chs);
					qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_RC from GTS_WorkVSRC where FK_Work='"+this.No+"'");
					qo.addOrderByRandom(); 
					qo.DoQuery();
					BP.DA.Cash.AddObj("RCs"+this.No, Depositary.Session,chs );
					return chs;
				}
				return (RCs)BP.DA.Cash.GetObjFormSession("RCs"+this.No);

			}
		}
		/// <summary>
		/// 填空题目
		/// </summary>
		public FillBlanks HisFillBlanks
		{
			get
			{
				if (BP.DA.Cash.IsExits("FillBlanks"+this.No,Depositary.Session) ==false )
				{
					FillBlanks chs = new FillBlanks();
					QueryObject qo = new QueryObject(chs);
					qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_FillBlank from GTS_WorkVSFillBlank where FK_Work='"+this.No+"'");
					qo.addOrderByRandom(); 
					qo.DoQuery();
					BP.DA.Cash.AddObj("FillBlanks"+this.No, Depositary.Session,chs );
					return chs;
				}
				return (FillBlanks)BP.DA.Cash.GetObjFormSession("FillBlanks"+this.No);
			}
		}
		#endregion

		#region attrs 
		public string ValidTimeTo
		{
			get
			{
				return this.GetValStringByKey(WorkFixDesignAttr.ValidTimeTo);
			}
			set
			{
				this.SetValByKey(WorkFixDesignAttr.ValidTimeTo,value);
			}
		}
		/// <summary>
		/// 是否超过了时间
		/// </summary>
		public bool IsPastTime_del
		{
			get
			{
				DateTime dtto=DataType.ParseSysDateTime2DateTime(this.ValidTimeTo);
				DateTime dt=DateTime.Now;
				if ( dtto>= dt)
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
				DateTime dtto=DataType.ParseSysDateTime2DateTime(this.ValidTimeTo+" 10:10");
				DateTime dt=DateTime.Now;
				if ( dtto>= dt )
					return true;
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
				return this.GetValIntByKey(WorkFixDesignAttr.CentOfJudgeTheme);
			}
			set
			{
				this.SetValByKey(WorkFixDesignAttr.CentOfJudgeTheme,value);
			}
		}
		/// <summary>
		/// 问答
		/// </summary>
		public int CentOfEssayQuestion
		{
			get
			{
				return this.GetValIntByKey(WorkFixDesignAttr.CentOfEssayQuestion);
			}
			set
			{
				this.SetValByKey(WorkFixDesignAttr.CentOfEssayQuestion,value);
			}
		}
		/// <summary>
		/// 阅读
		/// </summary>
		public int CentOfRC
		{
			get
			{
				return this.GetValIntByKey(WorkFixDesignAttr.CentOfRC);
			}
			set
			{
				this.SetValByKey(WorkFixDesignAttr.CentOfRC,value);
			}
		}
		/// <summary>
		/// 填空
		/// </summary>
		public int CentOfFillBlank
		{
			get
			{
				return this.GetValIntByKey(WorkFixDesignAttr.CentOfFillBlank);
			}
			set
			{
				this.SetValByKey(WorkFixDesignAttr.CentOfFillBlank,value);
			}
		}
		/// <summary>
		/// 合计
		/// </summary>
		public int CentOfSum
		{
			get
			{
				return this.GetValIntByKey(WorkFixDesignAttr.CentOfSum);
			}
			set
			{
				this.SetValByKey(WorkFixDesignAttr.CentOfSum,value);
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
				string sql="SELECT COUNT(*) FROM GTS_WorkVSChoseOne WHERE FK_Work='"+this.No+"'";
				int i=0;
				i=DBAccess.RunSQLReturnValInt(sql);
				return i*this.CentOfChoseOne;
			}
		}
		/// <summary>
		/// duo项选择题得分。
		/// </summary>
		public int SumCentOfChoseM
		{
			get
			{
				string sql="SELECT COUNT(*) FROM GTS_WorkVSChoseM WHERE FK_Work='"+this.No+"'";
				int i=0;
				i=DBAccess.RunSQLReturnValInt(sql);
				return i*this.CentOfChoseM;
			}
		}
		/// <summary>
		/// 判断题的分。
		/// </summary>
		public int SumCentOfJudgeTheme
		{
			get
			{
				string sql="SELECT COUNT(*) FROM GTS_WorkVSJudgeTheme WHERE FK_Work='"+this.No+"'";
				int i=0;
				i=DBAccess.RunSQLReturnValInt(sql);
				return i*this.CentOfJudgeTheme;
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
					int blankNum=DBAccess.RunSQLReturnValInt("SELECT SUM(BlankNum) FROM GTS_FillBlank WHERE No in  ( SELECT FK_FillBlank from GTS_WorkVSFillBlank where FK_Work='"+this.No+"' )");
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
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("GTS_WorkFixDesign");
                map.EnDesc = "作业布置";
                map.CodeStruct = "4";
                map.EnType = EnType.Admin;
                map.AddTBStringPK(WorkFixDesignAttr.No, null, "编号", true, true, 0, 50, 20);
                map.AddTBString(WorkFixDesignAttr.Name, "新建作业1", "作业名称", true, false, 0, 50, 20);

                DateTime dt = DateTime.Now;
                dt = dt.AddDays(7);
                //map.AddTBDate(WorkFixDesignAttr.ValidTimeFrom,DateTime.Now.ToString("yyyy-MM-dd"),"有效日期从",true,false);
                map.AddTBDate(WorkFixDesignAttr.ValidTimeTo, dt.ToString("yyyy-MM-dd"), "提交作业日期", true, false);

                map.AddTBDecimal(WorkFixDesignAttr.CentOfChoseOne, 1, "单选题每题分", true, false);
                map.AddTBDecimal(WorkFixDesignAttr.CentOfChoseM, 1, "多选题每题分", true, false);
                map.AddTBDecimal(WorkFixDesignAttr.CentOfFillBlank, 1, "填空题每空分", true, false);

                map.AddTBDecimal(WorkFixDesignAttr.CentOfJudgeTheme, 1, "判断题每题分", true, false);
                map.AddTBDecimal(WorkFixDesignAttr.CentOfEssayQuestion, 1, "问答题分", true, true);

                map.AddTBDecimal(WorkFixDesignAttr.CentOfRC, 20, "阅读题分", true, true);
                map.AddTBDecimal(WorkFixDesignAttr.CentOfSum, 0, "总分", true, true);

                map.AttrsOfOneVSM.Add(new WorkVSChoseOnes(), new ChoseOnes(), WorkVSChoseOneAttr.FK_Work, WorkVSChoseOneAttr.FK_Chose, ChoseOneAttr.Name, ChoseOneAttr.No, "单选题");
                map.AttrsOfOneVSM.Add(new WorkVSChoseMs(), new ChoseMs(), WorkVSChoseMAttr.FK_Work, WorkVSChoseMAttr.FK_Chose, ChoseMAttr.Name, ChoseMAttr.No, "多选题");
                map.AttrsOfOneVSM.Add(new WorkVSFillBlanks(), new FillBlanks(), WorkVSFillBlankAttr.FK_Work, WorkVSFillBlankAttr.FK_FillBlank, FillBlankAttr.Name, FillBlankAttr.No, "填空题");
                map.AttrsOfOneVSM.Add(new WorkVSJudgeThemes(), new JudgeThemes(), WorkVSJudgeThemeAttr.FK_Work, WorkVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name, JudgeThemeAttr.No, "判断题");
                map.AttrsOfOneVSM.Add(new WorkVSEssayQuestions(), new EssayQuestions(), WorkVSEssayQuestionAttr.FK_Work, WorkVSEssayQuestionAttr.FK_EssayQuestion, EssayQuestionAttr.Name, EssayQuestionAttr.No, "问答题");
                map.AttrsOfOneVSM.Add(new WorkVSRCs(), new RCs(), WorkVSRCAttr.FK_Work, WorkVSRCAttr.FK_RC, RCAttr.Name, RCAttr.No, "阅读题");

                map.AttrsOfOneVSM.Add(new WorkVSEmps(), new Emps(), WorkVSEmpAttr.FK_Work, WorkVSEmpAttr.FK_Emp, RCAttr.Name, RCAttr.No, "要布置的学生");
                map.AddDtl(new WorkVSEssayQuestions(), WorkVSEssayQuestionAttr.FK_Work);
                map.AddDtl(new WorkVSRCDtls(), WorkVSRCAttr.FK_Work);

                this._enMap = map;
                return this._enMap;
            }
		}
		protected override void afterDelete()
		{
			DBAccess.RunSQL("DELETE GTS_WorkVSChoseOne WHERE FK_Work='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_WorkVSChoseM WHERE FK_Work='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_WorkVSJudgeTheme WHERE FK_Work='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_WorkVSFillBlank WHERE FK_Work='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_WorkVSEssayQuestion WHERE FK_Work='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_WorkVSRC WHERE FK_Work='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_WorkVSRCDtl WHERE FK_Work='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_WorkFix WHERE FK_Work='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_WorkVSEmp WHERE FK_Work='"+this.No+"'");
			base.afterDelete ();
		}

		protected override bool beforeUpdateInsertAction()
		{

			// 判断是否有考生做题。
			//			string sql="SELECT COUNT(*) FROM GTS_WorkFix WHERE  FK_Work='"+this.No+"'";
			//			int i=DBAccess.RunSQLReturnValInt(sql);
			//			if (i>=1)
			//				throw new Exception("作业["+this.Name+"], 不能被您再设计，因为它已经有“"+i+"”个学生开始做题。");
			//
			//			if (this.IsPastTime)
			//				throw new Exception("作业["+this.Name+"], 不能被您再设计，因为它已经超过时间。");

			string sql="";
			// 设置分数。
			this.CentOfEssayQuestion = DBAccess.RunSQLReturnValInt("select isnull( sum(cent), 0) from GTS_WorkVSEssayQuestion where FK_Work='"+this.No+"'") ;
			this.CentOfRC = DBAccess.RunSQLReturnValInt("select isnull( sum(cent), 0) from GTS_WorkVSRCDtl where FK_Work='"+this.No+"'") ;
			this.CentOfSum=this.SumCentOfFillBlank+this.CentOfEssayQuestion+this.SumCentOfChoseOne+this.SumCentOfChoseM+this.SumCentOfJudgeTheme+this.CentOfRC;

			// 初始化考生 , 给每个考生初始化他的作业。
			sql="DELETE GTS_WorkFix WHERE FK_Emp NOT IN (SELECT FK_Emp FROM GTS_WorkVSEmp WHERE FK_Work='"+this.No+"') AND FK_Work='"+this.No+"'";
			DBAccess.RunSQL(sql);

			sql="SELECT FK_Emp FROM GTS_WorkVSEmp WHERE FK_Work='"+this.No+"' AND FK_Emp";
			sql+=" NOT IN (SELECT FK_Emp FROM GTS_WorkFix WHERE FK_Work='"+this.No+"')";
			DataTable dt = DBAccess.RunSQLReturnTable(sql);
			foreach(DataRow dr in dt.Rows)
			{
				WorkFix pe = new WorkFix();
				pe.No=dr[0].ToString()+"@"+this.No;
				pe.FK_Emp=dr[0].ToString();
				pe.FK_Work=this.No;
				if ( pe.IsExit(WorkFixAttr.FK_Emp, pe.FK_Emp, WorkFixAttr.FK_Work, this.No))
					continue;
				else
					pe.Insert();
			}
			return base.beforeUpdateInsertAction();
		}
		#endregion 

		#region 构造方法
		/// <summary>
		/// 试卷
		/// </summary> 
		public WorkFixDesign()
		{
		}
		/// <summary>
		/// 试卷
		/// </summary>
		/// <param name="_No">征收机关编号</param> 
		public WorkFixDesign(string _No ):base(_No)
		{

		}
		#endregion 

		#region 逻辑处理
		#endregion
	}
	/// <summary>
	///  固定作业
	/// </summary>
	public class WorkFixDesigns :EntitiesNoName
	{
		/// <summary>
		/// 固定作业s
		/// </summary>
		public WorkFixDesigns(){}

		/// <summary>
		/// 固定作业s
		/// </summary>
		/// <param name="fk_emp">操作员</param>
		public WorkFixDesigns(string fk_emp)
		{
			QueryObject qo= new QueryObject(this);
			qo.AddWhereInSQL(WorkFixDesignAttr.No, "SELECT FK_Work FROM GTS_WorkVSEmp WHERE FK_Emp='"+Web.WebUser.No+"'" );
			qo.DoQuery();
		}

		/// <summary>
		/// 固定作业
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkFixDesign();
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fk_emp"></param>
		/// <returns></returns> 
		public int RetrieveWorkFixDesign(string fk_emp)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhereInSQL(WorkFixDesignAttr.No,  "SELECT FK_Work FROM GTS_WorkVSEmp WHERE FK_Emp='"+fk_emp+"'");
			return qo.DoQuery();
		}
	 
	}

	 
}
