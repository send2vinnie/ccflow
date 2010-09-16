using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// 试卷答案attr
	/// </summary>
	public class WorkFixAttr:EntityNoAttr
	{
		/// <summary>
		/// 试卷
		/// </summary>
		public const string FK_Work="FK_Work";
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
        public const string FK_Dept = "FK_Dept";
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
	public class WorkFix :EntityNo
	{
		#region 属性
		public string GenerWorkResult()
		{
			string strs="<TABLE border=1 align=center >";
			strs+="<CAPTION>"+this.FK_EmpText+"</CAPTION>";
			strs+="<TR>";
			strs+="<TD >单项选择题</TD>";
			strs+="<TD>"+this.CentOfChoseOne+"</TD>";
			strs+="</TR>";

			strs+="<TR>";
			strs+="<TD>多项选择题</TD>";
			strs+="<TD>"+this.CentOfChoseM+"</TD>";
			strs+="</TR>";

			strs+="<TR>";
			strs+="<TD>填空题</TD>";
			strs+="<TD>"+this.CentOfFillBlank+"</TD>";
			strs+="</TR>";

			strs+="<TR>";
			strs+="<TD>判断题</TD>";
			strs+="<TD>"+this.CentOfJudgeTheme+"</TD>";
			strs+="</TR>";

			strs+="<TR>";
			strs+="<TD>简答题</TD>";
			strs+="<TD>"+this.CentOfEssayQuestion+"</TD>";
			strs+="</TR>";

			strs+="<TR>";
			strs+="<TD>阅读理解题</TD>";
			strs+="<TD>"+this.CentOfRC+"</TD>";
			strs+="</TR>";

			strs+="<TR>";
			strs+="<TD>总分</TD>";
			strs+="<TD>"+this.CentOfSum+"</TD>";
			strs+="</TR>";

			strs+="<TR>";
			strs+="<TD>正确率%</TD>";
			strs+="<TD>"+this.RightRate.ToString("0.00")+"</TD>";
			strs+="</TR>";

			strs+="<TR>";
			strs+="<TD>等级</TD>";
			strs+="<TD>"+this.FK_LevelText+"</TD>";
			strs+="</TR>";

			strs+="</Table>";
			return strs ;
		}
		/// <summary>
		/// 部门
		/// </summary>
		public string FK_Dept
		{
			get
			{
				return this.GetValStringByKey( WorkFixAttr.FK_Dept);
			}
			set
			{
				this.SetValByKey(WorkFixAttr.FK_Dept,value);
			}
		}
		/// <summary>
		/// Work.
		/// </summary>
		public WorkFixDesign HisWorkFixDesign
		{
			get
			{
				return new WorkFixDesign(this.FK_Work);
			}
		}
		#endregion

		/// <summary>
		/// 自动批改作业:
		/// </summary>
		public void AutoReadWork()
		{
			WorkFixDesign pp = new WorkFixDesign(this.FK_Work);
			// 选择题.
			WorkOfChoses  pes = new WorkOfChoses(this.FK_Work,this.FK_Emp);
			ChoseOnes chs =this.HisChoseOnes;
			int countOne=0;
			int countM=0;
			foreach(WorkOfChose pe in pes)
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

				pe.Answer=cts.Val;
				pe.Update();
				
			}

			this.CentOfChoseOne= countOne*pp.CentOfChoseOne; // 单项选择题得分
			this.CentOfChoseM= countM*pp.CentOfChoseM; // duo项选择题得分

			// 判断题
			WorkOfJudgeThemes  pej = new WorkOfJudgeThemes(this.FK_Work,this.FK_Emp);
			int count=0;
			foreach(WorkOfJudgeTheme pe in pej)
			{
				
				JudgeTheme jt =new JudgeTheme(pe.FK_JudgeTheme);
				if (pe.Val==jt.IsOkOfInt)
					count++;

				pe.Answer= jt.IsOkOfInt;
				pe.Update();
			}
			this.CentOfJudgeTheme= count*pp.CentOfJudgeTheme; // duo项选择题得分。


			// 填空题
			WorkOfFillBlanks  peb = new WorkOfFillBlanks(this.FK_Work, this.FK_Emp);
			count=0;
			foreach(WorkOfFillBlank pe in peb)
			{
				FillBlank fb= new FillBlank(pe.FK_FillBlank);
				if (pe.Val.Trim()==fb.HisKeys[pe.IDX].Trim() )
					count++;

				pe.Answer= fb.HisKeys[pe.IDX].Trim();
				pe.Update();
			}
			this.CentOfFillBlank= count*pp.CentOfFillBlank; // blank项选择题得分。

			WorkOfEssayQuestions  pee = new WorkOfEssayQuestions(this.FK_Work, this.FK_Emp);
			//			if (pee.Count==0)
			//			{
			//				/*如果没有简答题*/
			//				this.ExamState=WorkFixState.ReadOver;
			//			}
			//			else
			//			{
			//				if (this.ExamState==WorkFixState.Examing || this.ExamState==WorkFixState.Init )
			//					this.ExamState=WorkFixState.Reading;
			//			}

			this.DoResetLevel();
		}
		/// <summary>
		/// 执行重新设置级别。
		/// </summary>
		public void DoResetLevel()
		{
			this.CentOfSum=this.CentOfFillBlank+this.CentOfEssayQuestion+this.CentOfRC+this.CentOfChoseOne+this.CentOfChoseM+this.CentOfJudgeTheme;
			
			WorkFixDesign wfl= new WorkFixDesign(this.FK_Work);

			// 开始计算标准分。
			decimal sum1=decimal.Parse( this.CentOfSum.ToString()) ;
			decimal sum2=decimal.Parse( wfl.CentOfSum.ToString()) ;
			this.RightRate= sum1/sum2 *100;
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

		#region 它的属性
		/// <summary>
		/// 他的单项选择题
		/// </summary>
		public ChoseOnes HisChoseOnes
		{
			get
			{
				ChoseOnes chs = new ChoseOnes();
				QueryObject qo = new QueryObject(chs);
				//qo.AddWhereInSQL( ChoseOneAttr.No, "select fk_chose from GTS_WorkFixVSChoseOne where fk_WorkFix='"+this.No+"'");
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
				//qo.AddWhereInSQL( ChoseOneAttr.FK_Chose, "select fk_chose from GTS_WorkFixVSChoseM where fk_WorkFix='"+this.No+"'");
				qo.DoQuery();
				return chs;
			}
		}
		#endregion

		#region 属性
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(WorkFixAttr.FK_Emp);
			}
			set
			{
				this.SetValByKey(WorkFixAttr.FK_Emp,value);
			}
		}
		/// <summary>
		/// FK_EmpText
		/// </summary>
		public string FK_EmpText
		{
			get
			{
				return this.GetValRefTextByKey(WorkFixAttr.FK_Emp);
			}
		}
		/// <summary>
		/// FK_Work
		/// </summary>
		public string FK_Work
		{
			get
			{
				return this.GetValStringByKey(WorkFixAttr.FK_Work);
			}
			set
			{
				this.SetValByKey(WorkFixAttr.FK_Work,value);
			}
		}
		/// <summary>
		/// 成绩等级
		/// </summary>
		public string FK_Level
		{
			get
			{
				return this.GetValStringByKey(WorkFixAttr.FK_Level);
			}
			set
			{
				this.SetValByKey(WorkFixAttr.FK_Level,value);
			}
		}
		public string FK_LevelText
		{
			get
			{
				return this.GetValRefTextByKey(WorkFixAttr.FK_Level);
			}
		}
		public string FK_WorkText
		{
			get
			{
				return this.GetValRefTextByKey(WorkFixAttr.FK_Work);
			}
		}
		/// <summary>
		/// 从时间
		/// </summary>
		public string FromDateTime
		{
			get
			{
				return this.GetValStringByKey(WorkFixAttr.FromDateTime);
			}
			set
			{
				this.SetValByKey(WorkFixAttr.FromDateTime,value);
			}
		}
		/// <summary>
		/// 到时间
		/// </summary>
		public string ToDateTime_
		{
			get
			{
				return this.GetValStringByKey(WorkFixAttr.ToDateTime);
			}
			set
			{
				this.SetValByKey(WorkFixAttr.ToDateTime,value);
			}
		}
		/// <summary>
		/// 单选
		/// </summary>
		public int CentOfChoseOne
		{
			get
			{
				return this.GetValIntByKey(WorkFixAttr.CentOfChoseOne);
			}
			set
			{
				this.SetValByKey(WorkFixAttr.CentOfChoseOne,value);
			}
		}
		/// <summary>
		/// 多选
		/// </summary>
		public int CentOfChoseM
		{
			get
			{
				return this.GetValIntByKey(WorkFixAttr.CentOfChoseM);
			}
			set
			{
				this.SetValByKey(WorkFixAttr.CentOfChoseM,value);
			}
		}
		/// <summary>
		/// 判断
		/// </summary>
		public int CentOfJudgeTheme
		{
			get
			{
				return this.GetValIntByKey(WorkFixAttr.CentOfJudgeTheme);
			}
			set
			{
				this.SetValByKey(WorkFixAttr.CentOfJudgeTheme,value);
			}
		}
		/// <summary>
		/// 问答
		/// </summary>
		public decimal CentOfEssayQuestion
		{
			get
			{
				return this.GetValDecimalByKey(WorkFixAttr.CentOfEssayQuestion);
			}
			set
			{
				this.SetValByKey(WorkFixAttr.CentOfEssayQuestion,value);
			}
		}
		public int CentOfRC
		{
			get
			{
				return this.GetValIntByKey(WorkFixAttr.CentOfRC);
			}
			set
			{
				this.SetValByKey(WorkFixAttr.CentOfRC,value);
			}
		}
		/// <summary>
		/// 填空
		/// </summary>
		public int CentOfFillBlank
		{
			get
			{
				return this.GetValIntByKey(WorkFixAttr.CentOfFillBlank);
			}
			set
			{
				this.SetValByKey(WorkFixAttr.CentOfFillBlank,value);
			}
		}
		/// <summary>
		/// 合计
		/// </summary>
		public decimal CentOfSum
		{
			get
			{
				return this.GetValDecimalByKey(WorkFixAttr.CentOfSum);
			}
			set
			{
				this.SetValByKey(WorkFixAttr.CentOfSum,value);
			}
		}
		/// <summary>
		/// 标准分
		/// </summary>
		public decimal RightRate
		{
			get
			{
				return this.GetValDecimalByKey(WorkFixAttr.RightRate);
			}
			set
			{
				this.SetValByKey(WorkFixAttr.RightRate,value);
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
		/// 重写基类方法
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;

				Map map = new Map("GTS_WorkFix");
				map.EnDesc="作业";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(WorkFixAttr.No,null,"编号",true,false,1,30,20);
				map.AddDDLEntities(WorkFixAttr.FK_Work,null,"作业",new WorkFixDesigns(),false);
				map.AddDDLEntities(WorkFixAttr.FK_Emp,Web.WebUser.No,"学生",new Emps(),false);
                map.AddDDLEntities(WorkFixAttr.FK_Dept, null, "部门", new BP.Port.Depts(), false);

				map.AddTBInt(WorkFixAttr.CentOfChoseOne,0,"单选题",true,true);
				map.AddTBInt(WorkFixAttr.CentOfChoseM,0,"多选题",true,true);
				map.AddTBInt(WorkFixAttr.CentOfFillBlank,0,"填空题",true,true);
				map.AddTBInt(WorkFixAttr.CentOfJudgeTheme,0,"判断题",true,true);
				map.AddTBInt(WorkFixAttr.CentOfEssayQuestion,0,"问答题",true,true);
				map.AddTBInt(WorkFixAttr.CentOfRC,0,"阅读理解题",true,true);
				map.AddTBInt(WorkFixAttr.CentOfSum,0,"总分",true,true);
				map.AddTBDecimal(WorkFixAttr.RightRate,0,"正确率%",true,true);

               // map.AddTBInt(WorkFixAttr.FK_Level, 0, "成绩等级", false, false);

			 	map.AddDDLEntities(WorkFixAttr.FK_Level,"1","成绩等级",new Levels(),false);

				map.AddSearchAttr(WorkFixAttr.FK_Work);
				map.AddSearchAttr(WorkFixAttr.FK_Dept);
			//	map.AddSearchAttr(WorkFixAttr.FK_Level);
				/*
				map.AttrsOfOneVSM.Add( new WorkFixVSChoseOnes(), new ChoseOnes(),WorkFixVSChoseOneAttr.FK_WorkFix,WorkFixVSChoseOneAttr.FK_Chose, ChoseOneAttr.Name,ChoseOneAttr.No,"单选题");
				map.AttrsOfOneVSM.Add( new WorkFixVSChoseMs(), new ChoseMs(),WorkFixVSChoseMAttr.FK_WorkFix,WorkFixVSChoseMAttr.FK_Chose, ChoseMAttr.Name,ChoseMAttr.No,"多选题");
				map.AttrsOfOneVSM.Add( new WorkFixVSFillBlanks(), new FillBlanks(),WorkFixVSFillBlankAttr.FK_WorkFix,WorkFixVSFillBlankAttr.FK_FillBlank, FillBlankAttr.Name,FillBlankAttr.No,"填空题");
				map.AttrsOfOneVSM.Add( new WorkFixVSJudgeThemes(), new JudgeThemes(),WorkFixVSJudgeThemeAttr.FK_WorkFix,WorkFixVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"判断题");
				map.AttrsOfOneVSM.Add( new WorkFixVSEssayQuestions(), new EssayQuestions(),WorkFixVSEssayQuestionAttr.FK_WorkFix,WorkFixVSEssayQuestionAttr.FK_EssayQuestion, EssayQuestionAttr.Name,EssayQuestionAttr.No,"问答题");
				map.AddDtl(new WorkFixVSEssayQuestions(),WorkFixVSEssayQuestionAttr.FK_WorkFix);
				*/
				//map.AttrsOfOneVSM.Add( new WorkFixVSChoseOnes(), new choseonhemes(),WorkFixVSJudgeThemeAttr.FK_WorkFix,WorkFixVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"判断题设计");
				//map.AttrsOfOneVSM.Add( new WorkFixVSChoseMs(), new JudgeThemes(),WorkFixVSJudgeThemeAttr.FK_WorkFix,WorkFixVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"判断题设计");

			
				// 批改作业 
				RefMethod func1 = new RefMethod();
				func1.Title="批改作业";
				//func1.Warning="确定执行吗？";
				func1.ClassMethodName=this.ToString()+".DoCheckWork()";
				func1.Icon="/Images/Btn/PrintWorkRpt.gif";
				func1.ToolTip="批改作业。";
				func1.Width=0;
				func1.Height=0;
				func1.Target=null;
				map.AddRefMethod(func1);

				// 批改作业 
				RefMethod f2 = new RefMethod();
				f2.Title="批量批改作业";
				//func1.Warning="确定执行吗？";
				f2.ClassMethodName=this.ToString()+".DoBatchCheckWork()";
				f2.Icon="/Images/Btn/PrintWorkRpt.gif";
				f2.ToolTip="批改作业。";
				f2.Width=0;
				f2.Height=0;
				f2.Target=null;
				map.AddRefMethod(f2);
  
				this._enMap=map;
				return this._enMap;
			}
		}
		/// <summary>
		/// 执行批改作业
		/// </summary>
		/// <returns></returns>
		public string DoCheckWork()
		{
			PubClass.WinOpen("../App/Paper/ReadEssayQuestionWork.aspx?Flag=PaperExam&RefNo="+this.No.ToString()+"&IsFix=1&FK_Emp="+this.FK_Emp+"&FK_Work="+this.FK_Work);
			return null;
		}
		/// <summary>
		/// 批量批改作业
		/// </summary>
		/// <returns></returns>
		public string DoBatchCheckWork()
		{
			string msg="";
			WorkFixs wrd = new WorkFixs(this.FK_Work);
			foreach(WorkFix wr in wrd)
			{
				try
				{
					wr.AutoReadWork();
					msg+="@成功批改["+wr.FK_EmpText+"]的作业，成绩："+wr.CentOfSum+"，正确率："+wr.RightRate.ToString("0.00")+"。";
				}
				catch(Exception ex)
				{
					msg+="@批改["+wr.FK_EmpText+"]作业期间出现错误。"+ex.Message;
				}
			}
			return msg;
		}
		protected override bool beforeInsert()
		{

			if (this.Search(this.FK_Work,this.FK_Emp)>=1)
				throw new Exception("已经有这个学生"+this.FK_Emp+"的试卷.");

		 
			Emp emp = new Emp(this.FK_Emp);
			this.FK_Dept = emp.FK_Dept;

			return base.beforeInsert ();
		}

		protected override bool beforeUpdateInsertAction()
		{
			 
			this.CentOfSum=this.CentOfFillBlank+this.CentOfEssayQuestion+this.CentOfRC+this.CentOfChoseOne+this.CentOfChoseM+this.CentOfJudgeTheme;
			//this.FK_Dept =
			//this.MM =DataType.GetSpanMinute(this.FromDateTime,this.ToDateTime);
			return base.beforeUpdateInsertAction ();
		}

		#endregion 

		#region 构造方法
		/// <summary>
		/// 试卷答案
		/// </summary> 
		public WorkFix()
		{
		}
		public WorkFix(string no):base(no){}
		public WorkFix(string Work, string fk_emp)
		{
			this.FK_Emp = fk_emp;
			this.FK_Work=Work;
            if (this.Retrieve("FK_Emp", fk_emp, "FK_Work", Work) == 0)
            {
                this.Insert();
               // throw new Exception("错误：没有生成考生[“" + fk_emp + "”]考试信息。");
            }
		}
		#endregion 

		public int Search(string Work, string fk_emp)
		{
			this.FK_Emp = fk_emp;
			this.FK_Work=Work;
			QueryObject qo = new QueryObject(this);			
			qo.AddWhere(WorkFixAttr.FK_Emp,fk_emp);
			qo.addAnd();
			qo.AddWhere(WorkFixAttr.FK_Work,Work);
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
	public class WorkFixs :EntitiesNo
	{
		/// <summary>
		/// 
		/// </summary>
		public void AutoReadWork()
		{
			foreach(WorkFix pe in this)
			{
				pe.AutoReadWork();
			}
		}
		/// <summary>
		/// WorkFixs
		/// </summary>
		public WorkFixs(){}
		public WorkFixs(string fk_Work)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere( WorkFixAttr.FK_Work, fk_Work);
			qo.DoQuery();
		}
		public int Search(string fk_emp)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere( WorkFixAttr.FK_Emp, fk_emp);
			return qo.DoQuery();
		}
		/// <summary>
		/// WorkFix
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkFix();
			}
		}
	}
}
