using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;
using BP.Web;


namespace BP.GTS
{
	/// <summary>
	/// 试卷答案attr
	/// </summary>
	public class PaperExamRandomAttr:EntityOIDAttr
	{
		public const string No="No";
		/// <summary>
		/// 试卷
		/// </summary>
		public const string FK_Paper="FK_Paper";
		/// <summary>
		/// 考
		/// </summary>
		public const string FK_Emp="FK_Emp";
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
		public const string RightRate="RightRate";

	}
	/// <summary>
	/// 试卷答案
	/// </summary>
	public class PaperExamRandom :EntityNo
	{
		/// <summary>
		/// 自动阅卷
		/// </summary>
		public void AutoReadPaper()
		{
			PaperRandomDesign pr = new PaperRandomDesign(this.FK_Paper);
			// 选择题.
			PaperExamOfChoses  pes = new PaperExamOfChoses(this.No,this.FK_Emp);
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

			this.CentOfChoseOne= countOne*pr.CentOfPerChoseOne; // 单项选择题得分。
			this.CentOfChoseM= countM*pr.CentOfPerChoseM; // duo项选择题得分。

			// 判断题
			PaperExamOfJudgeThemes  pej = new PaperExamOfJudgeThemes(this.No,this.FK_Emp);
			int count=0;
			foreach(PaperExamOfJudgeTheme pe in pej)
			{
				JudgeTheme jt =new JudgeTheme(pe.FK_JudgeTheme);
				if (pe.Val==jt.IsOkOfInt)
				{
					count++;
				}
			}
			this.CentOfJudgeTheme= count*pr.CentOfPerJudgeTheme; // duo项选择题得分。

			// 填空题
			PaperExamOfFillBlanks  peb = new PaperExamOfFillBlanks(this.No,this.FK_Emp);
			count=0;
            foreach (PaperExamOfFillBlank pe in peb)
            {
                FillBlank fb = new FillBlank(pe.FK_FillBlank);
                if (pe.Val.Trim().Replace(" ", "") == fb.HisKeys[pe.IDX].Trim().Replace(" ", ""))
                {
                    count++;
                }
            }
			this.CentOfFillBlank= count*pr.CentOfPerFillBlank; // blank项选择题得分。

			PaperExamOfEssayQuestions  pee = new PaperExamOfEssayQuestions(this.No, this.FK_Emp);
            if (pee.Count == 0)
            {
                /*如果没有简答题 */
                this.ExamState = PaperExamState.ReadOver;
            }
            else
            {
                bool IsEndExam = true;
                foreach (PaperExamOfEssayQuestion q in pee)
                {
                    EssayQuestion eq = new EssayQuestion(q.FK_EssayQuestion);
                    if (eq.IsTextInput)
                    {
                        PaperVSEssayQuestion peq = new PaperVSEssayQuestion(q.FK_Paper, q.FK_EssayQuestion);
                        q.Cent = eq.CheckIt(q.Val, peq.Cent);
                        q.Update();
                    }
                    else
                    {
                        IsEndExam = false;
                    }
                }

                if (IsEndExam == false)
                {
                    if (this.ExamState == PaperExamState.Examing || this.ExamState == PaperExamState.Init)
                        this.ExamState = PaperExamState.Reading;
                }
                else
                {
                    this.ExamState = PaperExamState.ReadOver;
                }
            }



			//this.ToDateTime=DataType.CurrentDataTime; //到时间。
			//设置成绩等级。
			//this.Update();
			this.DoResetLevel( pr.CentOfSum );
		}
		/// <summary>
		/// 标准分
		/// </summary>
		public decimal RightRate
		{
			get
			{
				return this.GetValDecimalByKey(PaperExamAttr.RightRate);
			}
			set
			{
				this.SetValByKey(PaperExamAttr.RightRate,value);
			}
		}
		public void DoResetLevel(int centOfSum)
		{
			// 开始计算标准分。
			this.RightRate=	this.CentOfSum/centOfSum * decimal.Parse( "100") ;
			if (this.RightRate > 90)
			{
				this.FK_Level="4";
				this.Update();
				return;
			}
			if (this.RightRate >80)
			{
				this.FK_Level="3";
				this.Update();
				return;
			}

			if (this.RightRate >60)
			{
				this.FK_Level="2";
				this.Update();
				return;
			}
			this.FK_Level="1";
			this.Update();
		}

		/// <summary>
		/// 清除随即试卷。
		/// </summary>
		public void ThemeOfClear()
		{
			// 删除试题。
			DBAccess.RunSQL("DELETE GTS_PaperVSChoseOne WHERE FK_Paper='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_PaperVSChoseM WHERE FK_Paper='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_PaperVSJudgeTheme WHERE FK_Paper='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_PaperVSFillBlank WHERE FK_Paper='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_PaperVSEssayQuestion WHERE FK_Paper='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_PaperVSRC WHERE FK_Paper='"+this.No+"'");
			DBAccess.RunSQL("DELETE GTS_PaperVSRCDtl WHERE FK_Paper='"+this.No+"'");
			//DBAccess.RunSQL("DELETE GTS_PaperExam WHERE FK_Paper='"+this.No+"'");
			//DBAccess.RunSQL("DELETE GTS_PaperVSEmp WHERE FK_Paper='"+this.No+"'");
		}
		/// <summary>
		/// 组成随即试卷。
		/// </summary>
		public void ThemeOfInit()
		{
			this.ThemeOfClear();
		    BP.GTS.PaperRandomDesign prd  = new PaperRandomDesign( this.FK_Paper);

			#region 生成ChonseOne 选择题目
			ChoseOnes chs = new ChoseOnes();
			chs.RetrieveAllOrderByRandom(prd.NumOfChoseOne );
			foreach(ChoseOne ch in chs)
			{
				PaperVSChoseOne one = new PaperVSChoseOne();
				//one.Cent=prd.CentOfPerChoseOne;
				one.FK_Chose=ch.No;
				one.FK_Paper=this.No;
				one.Insert();
			}
			#endregion

			#region 生成 ChonseM 选择题目
			ChoseMs chsM = new ChoseMs();
			chsM.RetrieveAllOrderByRandom(prd.NumOfChoseM);
			foreach(ChoseM ch in chsM)
			{
				PaperVSChoseM theme = new PaperVSChoseM();
				theme.Cent=prd.CentOfPerChoseM;
				theme.FK_Chose=ch.No;
				theme.FK_Paper=this.No;
				theme.Insert();
			}
			#endregion


			#region 生成 fillbla 选择题目
			FillBlanks fbs = new FillBlanks();
			fbs.RetrieveAllOrderByRandom(prd.NumOfFillBlank);
			foreach(FillBlank ch in fbs)
			{
				PaperVSFillBlank theme = new PaperVSFillBlank();
				theme.Cent=prd.CentOfPerFillBlank;
				theme.FK_FillBlank=ch.No;
				theme.FK_Paper=this.No;
				theme.Insert();
			}
			#endregion


			#region 生成 JudgeThemes  题目
			JudgeThemes jts = new JudgeThemes();
			jts.RetrieveAllOrderByRandom(prd.NumOfJudgeTheme);
			foreach(JudgeTheme ch in jts)
			{
				PaperVSJudgeTheme theme = new PaperVSJudgeTheme();
				theme.Cent=prd.CentOfPerJudgeTheme;
				theme.FK_JudgeTheme=ch.No;
				theme.FK_Paper=this.No;
				theme.Insert();
			}
			#endregion


			#region 生成 EssayQuestions 题目
			EssayQuestions eqs = new EssayQuestions();
			eqs.RetrieveAllOrderByRandom(prd.NumOfEssayQuestion);
			foreach(EssayQuestion ch in eqs)
			{ 
				PaperVSEssayQuestion theme = new PaperVSEssayQuestion();
				theme.Cent=prd.CentOfPerEssayQuestion;
				theme.FK_EssayQuestion=ch.No;
				theme.FK_Paper=this.No;
				theme.Insert();
			}
			#endregion


			#region 生成 rcs 题目
			RCs rcs = new RCs();
			rcs.RetrieveAllOrderByRandom(prd.NumOfRC);
			// 随机找到一些阅读题。
			foreach(RC ch in rcs)
			{
				PaperVSRC theme = new PaperVSRC();
				theme.FK_RC=ch.No;
				theme.FK_Paper=this.No;
				theme.Insert();
			}

			if (rcs.Count > 0)
			{
				/* 如果能够生成问答题目。*/
				RCDtls rcdtl = new RCDtls();
				int numES = 0;
				foreach(RC myrc in rcs)
				{
					DBAccess.RunSQL(" DELETE GTS_PaperVSRCDtl  WHERE FK_Paper='"+this.No+"' and FK_RC='"+myrc.No+"' ");
					RCDtls dtls = myrc.HisRCDtls;
					numES+=dtls.Count;
					rcdtl.AddEntities(dtls);
				}
				decimal d=decimal.Parse( prd.CentOfPerRC.ToString() )  / decimal.Parse( numES.ToString() ) ;
				int perCent = Convert.ToInt32(  d  ) ;
				// 生成 小问答题。
				foreach(RCDtl dtl in rcdtl)
				{
					PaperVSRCDtl pdtl = new PaperVSRCDtl();
					pdtl.Cent=perCent; // 设置分数。
					pdtl.FK_Paper=this.No;
					pdtl.FK_RC=dtl.FK_RC;
					pdtl.FK_RCDtl=dtl.OID;
					pdtl.Insert();
				}
			}
			#endregion
		}

		#region 属性
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
		/// <summary>
		/// ExamStateText
		/// </summary>
		public string ExamStateText
		{
			get
			{
				return this.GetValRefTextByKey( PaperExamRandomAttr.ExamState);
			}
		}
		/// <summary>
		/// 考试状态
		/// </summary>
		public PaperExamState ExamState
		{
			get
			{
				return (PaperExamState)this.GetValIntByKey( PaperExamRandomAttr.ExamState);
			}
			set
			{
				this.SetValByKey(PaperExamRandomAttr.ExamState,(int)value);
			}
		}
		public string FK_Dept
		{
			get
			{
				return this.GetValStringByKey( PaperExamRandomAttr.FK_Dept);
			}
			set
			{
				this.SetValByKey(PaperExamRandomAttr.FK_Dept,value);
			}
		}
		 
		#endregion

		/// <summary>
		/// 自动阅卷:
		/// </summary>
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
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(PaperExamRandomAttr.FK_Emp);
			}
			set
			{
				this.SetValByKey(PaperExamRandomAttr.FK_Emp,value);
			}
		}
		/// <summary>
		/// FK_EmpText
		/// </summary>
		public string FK_EmpText
		{
			get
			{
				return this.GetValRefTextByKey(PaperExamRandomAttr.FK_Emp);
			}
		}
		/// <summary>
		/// FK_Paper
		/// </summary>
		public string FK_Paper
		{
			get
			{
				return this.GetValStringByKey(PaperExamRandomAttr.FK_Paper);
			}
			set
			{
				this.SetValByKey(PaperExamRandomAttr.FK_Paper,value);
			}
		}
		public string FK_PaperText
		{
			get
			{
				return this.GetValRefTextByKey(PaperExamRandomAttr.FK_Paper);
			}
		}
		/// <summary>
		/// 从时间
		/// </summary>
		public string FromDateTime
		{
			get
			{
				return this.GetValStringByKey(PaperExamRandomAttr.FromDateTime);
			}
			set
			{
				this.SetValByKey(PaperExamRandomAttr.FromDateTime,value);
			}
		}
		/// <summary>
		/// 到时间
		/// </summary>
		public string ToDateTime
		{
			get
			{
				return this.GetValStringByKey(PaperExamRandomAttr.ToDateTime);
			}
			set
			{
				this.SetValByKey(PaperExamRandomAttr.ToDateTime,value);
			}
		}
		/// <summary>
		/// 单选
		/// </summary>
        public decimal CentOfChoseOne
		{
			get
			{
				return this.GetValDecimalByKey(PaperExamRandomAttr.CentOfChoseOne);
			}
			set
			{
				this.SetValByKey(PaperExamRandomAttr.CentOfChoseOne,value);
			}
		}
		/// <summary>
		/// 多选
		/// </summary>
        public decimal CentOfChoseM
		{
			get
			{
                return this.GetValDecimalByKey(PaperExamRandomAttr.CentOfChoseM);
			}
			set
			{
				this.SetValByKey(PaperExamRandomAttr.CentOfChoseM,value);
			}
		}
		/// <summary>
		/// 判断
		/// </summary>
		public decimal CentOfJudgeTheme
		{
			get
			{
                return this.GetValDecimalByKey(PaperExamRandomAttr.CentOfJudgeTheme);
			}
			set
			{
				this.SetValByKey(PaperExamRandomAttr.CentOfJudgeTheme,value);
			}
		}
		/// <summary>
		/// 问答
		/// </summary>
		public decimal CentOfEssayQuestion
		{
			get
			{
				return this.GetValDecimalByKey(PaperExamRandomAttr.CentOfEssayQuestion);
			}
			set
			{
				this.SetValByKey(PaperExamRandomAttr.CentOfEssayQuestion,value);
			}
		}
		public decimal CentOfRC
		{
			get
			{
				return this.GetValDecimalByKey(PaperExamRandomAttr.CentOfRC);
			}
			set
			{
				this.SetValByKey(PaperExamRandomAttr.CentOfRC,value);
			}
		}
		/// <summary>
		/// 填空
		/// </summary>
		public decimal CentOfFillBlank
		{
			get
			{
				return this.GetValDecimalByKey(PaperExamRandomAttr.CentOfFillBlank);
			}
			set
			{
				this.SetValByKey(PaperExamRandomAttr.CentOfFillBlank,value);
			}
		}
		/// <summary>
		/// 合计
		/// </summary>
		public decimal CentOfSum
		{
			get
			{
				return this.GetValDecimalByKey(PaperExamRandomAttr.CentOfSum);
			}
			set
			{
				this.SetValByKey(PaperExamRandomAttr.CentOfSum,value);
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
				uc.Readonly();
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
				
				Map map = new Map("GTS_PaperExamRandom");
				map.EnDesc="考卷";
				map.CodeStruct="7";
				map.IsAllowRepeatNo=false;
				map.EnType= EnType.Admin;
				map.AddTBStringPK(PaperExamRandomAttr.No,null,"系统ID",false,true,1,50,20);
				//map.AddTBStringPK(PaperAttr.Name,null,"系统编号",true,true,0,50,20);
				map.AddDDLSysEnum(PaperExamRandomAttr.ExamState,0,"状态",true,false);
				map.AddDDLEntities(PaperExamRandomAttr.FK_Paper,null,"随机试卷",new BP.GTS.PaperRandomDesigns(),false);
				map.AddDDLEntities(PaperExamRandomAttr.FK_Emp,Web.WebUser.No,"学生",new Emps(),false);
                map.AddDDLEntities(PaperExamRandomAttr.FK_Dept, null, "部门", new BP.Port.Depts(), false);

			
				map.AddTBDecimal(PaperExamRandomAttr.CentOfSum,0,"总分",true,true);
                map.AddTBDecimal(PaperExamRandomAttr.RightRate, 0, "标准分", true, true);

                map.AddTBDecimal(PaperExamRandomAttr.CentOfChoseOne, 0, "单选题", true, true);
                map.AddTBDecimal(PaperExamRandomAttr.CentOfChoseM, 0, "多选题", true, true);
                map.AddTBDecimal(PaperExamRandomAttr.CentOfFillBlank, 0, "填空题", true, true);
                map.AddTBDecimal(PaperExamRandomAttr.CentOfJudgeTheme, 0, "判断题", true, true);
                map.AddTBDecimal(PaperExamRandomAttr.CentOfEssayQuestion, 0, "问答题", true, true);
                map.AddTBDecimal(PaperExamRandomAttr.CentOfRC, 0, "阅读理解题", true, true);

				map.AddDDLEntities(PaperExamAttr.FK_Level,"1","成绩等级",new Levels(),false);
			
				map.AddTBDateTime(PaperExamRandomAttr.FromDateTime,"考时从",true,true);
				map.AddTBDateTime(PaperExamRandomAttr.ToDateTime,"到",true,true);

				/*
				map.AttrsOfOneVSM.Add( new PaperVSChoseOnes(), new ChoseOnes(),PaperVSChoseOneAttr.FK_Paper,PaperVSChoseOneAttr.FK_Chose, ChoseOneAttr.Name,ChoseOneAttr.No,"单选题");
				map.AttrsOfOneVSM.Add( new PaperVSChoseMs(), new ChoseMs(),PaperVSChoseMAttr.FK_Paper,PaperVSChoseMAttr.FK_Chose, ChoseMAttr.Name,ChoseMAttr.No,"多选题");
				map.AttrsOfOneVSM.Add( new PaperVSFillBlanks(), new FillBlanks(),PaperVSFillBlankAttr.FK_Paper,PaperVSFillBlankAttr.FK_FillBlank, FillBlankAttr.Name,FillBlankAttr.No,"填空题");
				map.AttrsOfOneVSM.Add( new PaperVSJudgeThemes(), new JudgeThemes(),PaperVSJudgeThemeAttr.FK_Paper,PaperVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"判断题");
				map.AttrsOfOneVSM.Add( new PaperVSEssayQuestions(), new EssayQuestions(),PaperVSEssayQuestionAttr.FK_Paper,PaperVSEssayQuestionAttr.FK_EssayQuestion, EssayQuestionAttr.Name,EssayQuestionAttr.No,"问答题");
				map.AttrsOfOneVSM.Add( new PaperVSRCs(), new RCs(),PaperVSRCAttr.FK_Paper,PaperVSRCAttr.FK_RC, RCAttr.Name,RCAttr.No,"阅读题");

				// 只有一个学生。
				//map.AttrsOfOneVSM.Add( new PaperVSEmps(), new Emps(),PaperVSEmpAttr.FK_Paper,PaperVSEmpAttr.FK_Emp, RCAttr.Name,RCAttr.No,"考试的学生");
				map.AddDtl(new PaperVSEssayQuestions(),PaperVSEssayQuestionAttr.FK_Paper);
				map.AddDtl(new PaperVSRCDtls(),PaperVSRCAttr.FK_Paper);
				*/

				map.AddSearchAttr(PaperExamRandomAttr.FK_Paper);
				//map.AddSearchAttr(PaperExamRandomAttr.FK_Dept);
				map.AddSearchAttr(PaperExamRandomAttr.ExamState);
				map.AddSearchAttr(PaperExamAttr.FK_Level);

				 
				this._enMap=map;
				return this._enMap;
			}
		}
		 
		protected override bool beforeUpdateInsertAction()
		{
			this.CentOfSum=this.CentOfFillBlank+this.CentOfEssayQuestion+this.CentOfRC+this.CentOfChoseOne+this.CentOfChoseM+this.CentOfJudgeTheme;
			return base.beforeUpdateInsertAction();
		}
		protected override bool beforeInsert()
		{
			this.No=this.FK_Emp+"@"+this.FK_Paper;
			Emp emp = new Emp(this.FK_Emp);
			this.FK_Dept = emp.FK_Dept;

			return base.beforeInsert ();
		}

		#endregion 

		#region 构造方法
		/// <summary>
		/// 试卷答案
		/// </summary> 
		public PaperExamRandom()
		{
		}
		public PaperExamRandom(string no):base(no)
		{
		 
		}
		public PaperExamRandom(string paper, string fk_emp)
		{
			this.FK_Emp = fk_emp;
			this.FK_Paper=paper;
            if (this.Retrieve("FK_Emp", fk_emp, "FK_Paper", paper) == 0)
            {
                this.Insert();
               // throw new Exception("错误：没有生成考生[“" + fk_emp + "”" + paper + "]考试信息。");
            }

		}
		#endregion 

		public int Search(string paper, string fk_emp)
		{
			this.FK_Emp = fk_emp;
			this.FK_Paper=paper;
			QueryObject qo = new QueryObject(this);			
			qo.AddWhere(PaperExamRandomAttr.FK_Emp,fk_emp);
			qo.addAnd();
			qo.AddWhere(PaperExamRandomAttr.FK_Paper,paper);
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
	public class PaperExamRandoms :EntitiesOID
	{
		 
		/// <summary>
		/// PaperExamRandoms
		/// </summary>
		public PaperExamRandoms(){}
		public PaperExamRandoms(string fk_paper)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere( PaperExamRandomAttr.FK_Paper, fk_paper);
			qo.DoQuery();
		}
		public int Search(string fk_emp)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere( PaperExamRandomAttr.FK_Emp, fk_emp);
			return qo.DoQuery();
		}



		 
		/// <summary>
		/// PaperExamRandom
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperExamRandom();
			}
		}
	}
}
