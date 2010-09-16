using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// 试卷attr
	/// </summary>
	public class PaperRandomDesignAttr:EntityNoNameAttr
	{
		/// <summary>
		/// 有效时间从
		/// </summary>
		public const string ValidTimeFrom="ValidTimeFrom";
		/// <summary>
		/// 到
		/// </summary>
		public const string ValidTimeTo="ValidTimeTo";
		/// <summary>
		/// 试卷状态
		/// </summary>
		public const string PaperState="PaperState";
		/// <summary>
		/// 考试时间分钟
		/// </summary>
		public const string MM="MM";

		#region  题目个数
		/// <summary>
		/// 单项选择题
		/// </summary>
		public const string NumOfChoseOne="NumOfChoseOne";
		/// <summary>
		/// 多项选择题
		/// </summary>
		public const string NumOfChoseM="NumOfChoseM";
		/// <summary>
		/// 填空题
		/// </summary>
		public const string NumOfFillBlank="NumOfFillBlank";
		/// <summary>
		/// 判断题
		/// </summary>
		public const string NumOfJudgeTheme="NumOfJudgeTheme";
		/// <summary>
		/// 问答题
		/// </summary>
		public const string NumOfEssayQuestion="NumOfEssayQuestion";
		/// <summary>
		/// NumOfRC
		/// </summary>
		public const string NumOfRC="NumOfRC";
		#endregion

		#region cent
		/// <summary>
		/// 单项选择题
		/// </summary>
		public const string CentOfPerChoseOne="CentOfPerChoseOne";
		/// <summary>
		/// 多项选择题
		/// </summary>
		public const string CentOfPerChoseM="CentOfPerChoseM";
		/// <summary>
		/// 填空题
		/// </summary>
		public const string CentOfPerFillBlank="CentOfPerFillBlank";
		/// <summary>
		/// 判断题
		/// </summary>
		public const string CentOfPerJudgeTheme="CentOfPerJudgeTheme";
		/// <summary>
		/// 问答题
		/// </summary>
		public const string CentOfPerEssayQuestion="CentOfPerEssayQuestion";
		/// <summary>
		/// CentOfPerRC
		/// </summary>
		public const string CentOfPerRC="CentOfPerRC";
		/// <summary>
		/// 合计
		/// </summary>
		public const string CentOfSum="CentOfSum";
		#endregion
	}
	/// <summary>
	/// 试卷
	/// </summary>
	public class PaperRandomDesign :EntityNoName
	{

		#region his attrs
		/// <summary>
		/// 考试集合
		/// </summary>
		public PaperExams HisPaperRandomDesignExams
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
				qo.AddWhereInSQL( ChoseOneAttr.No, "select fk_chose from GTS_PaperVSChoseOne where fk_Paper='"+this.No+"'");
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
				qo.AddWhereInSQL( ChoseOneAttr.No, "select fk_chose from GTS_PaperVSChoseM where fk_Paper='"+this.No+"'");
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
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_JudgeTheme from GTS_PaperVSJudgeTheme where fk_Paper='"+this.No+"'");
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
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_EssayQuestion from GTS_PaperVSEssayQuestion where fk_Paper='"+this.No+"'");
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
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_RC from GTS_PaperVSRC where fk_Paper='"+this.No+"'");
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
				qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_FillBlank from GTS_PaperVSFillBlank where fk_Paper='"+this.No+"'");
				qo.DoQuery();
				return chs;
			}
		}
		#endregion

		#region attrs
		  
				 

		/// <summary>
		/// 单选
		/// </summary>
		public int CentOfPerChoseOne
		{
			get
			{
				return this.GetValIntByKey(PaperRandomDesignAttr.CentOfPerChoseOne);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.CentOfPerChoseOne,value);
			}
		}
		/// <summary>
		/// 多选
		/// </summary>
		public int CentOfPerChoseM
		{
			get
			{
				return this.GetValIntByKey(PaperRandomDesignAttr.CentOfPerChoseM);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.CentOfPerChoseM,value);
			}
		}
		/// <summary>
		/// 判断
		/// </summary>
		public int CentOfPerJudgeTheme
		{
			get
			{
				return this.GetValIntByKey(PaperRandomDesignAttr.CentOfPerJudgeTheme);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.CentOfPerJudgeTheme,value);
			}
		}
		/// <summary>
		/// 问答
		/// </summary>
		public int CentOfPerEssayQuestion
		{
			get
			{
				return this.GetValIntByKey(PaperRandomDesignAttr.CentOfPerEssayQuestion);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.CentOfPerEssayQuestion,value);
			}
		}
		/// <summary>
		/// 阅读
		/// </summary>
		public int CentOfPerRC
		{
			get
			{
				return this.GetValIntByKey(PaperRandomDesignAttr.CentOfPerRC);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.CentOfPerRC,value);
			}
		}
		/// <summary>
		/// 填空
		/// </summary>
		public int CentOfPerFillBlank
		{
			get
			{
				return this.GetValIntByKey(PaperRandomDesignAttr.CentOfPerFillBlank);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.CentOfPerFillBlank,value);
			}
		}
		/// <summary>
		/// 合计
		/// </summary>
		public int CentOfSum
		{
			get
			{
				return this.GetValIntByKey(PaperRandomDesignAttr.CentOfSum);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.CentOfSum,value);
			}
		}
		/// <summary>
		/// 时长
		/// </summary>
		public int MM
		{
			get
			{
				return this.GetValIntByKey(PaperRandomDesignAttr.MM);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.MM,value);
			}
		}
		#endregion

		#region Numof 
		/// <summary>
		/// 单选
		/// </summary>
		public int NumOfChoseOne
		{
			get
			{
				return this.GetValIntByKey(PaperRandomDesignAttr.NumOfChoseOne);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.NumOfChoseOne,value);
			}
		}
		/// <summary>
		/// 多选
		/// </summary>
		public int NumOfChoseM
		{
			get
			{
				return this.GetValIntByKey(PaperRandomDesignAttr.NumOfChoseM);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.NumOfChoseM,value);
			}
		}
		/// <summary>
		/// 判断
		/// </summary>
		public int NumOfJudgeTheme
		{
			get
			{
				return this.GetValIntByKey(PaperRandomDesignAttr.NumOfJudgeTheme);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.NumOfJudgeTheme,value);
			}
		}
		/// <summary>
		/// 问答
		/// </summary>
		public int NumOfEssayQuestion
		{
			get
			{
				return this.GetValIntByKey(PaperRandomDesignAttr.NumOfEssayQuestion);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.NumOfEssayQuestion,value);
			}
		}
		/// <summary>
		/// 阅读
		/// </summary>
		public int NumOfRC
		{
			get
			{
				return this.GetValIntByKey(PaperRandomDesignAttr.NumOfRC);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.NumOfRC,value);
			}
		}
		/// <summary>
		/// 填空
		/// </summary>
		public int NumOfFillBlank
		{
			get
			{
				return this.GetValIntByKey(PaperRandomDesignAttr.NumOfFillBlank);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.NumOfFillBlank,value);
			}
		}
		#endregion

		#region attrs ext
		/// <summary>
		/// 每个单项选择题得分。
		/// </summary>
		public int PerCentOfPerChoseOne
		{
			get
			{
				return this.CentOfPerChoseOne/this.HisChoseOnes.Count;
			}
		}
		/// <summary>
		/// duo项选择题得分。
		/// </summary>
		public int PerCentOfPerChoseM
		{
			get
			{
				return this.CentOfPerChoseM/this.HisChoseMs.Count;
			}
		}
		/// <summary>
		/// 判断题的分。
		/// </summary>
		public int PerCentOfPerJudgeTheme
		{
			get
			{
				return this.CentOfPerJudgeTheme/this.HisJudgeThemes.Count;
			}
		}
	 
		/// <summary>
		/// 填空题
		/// </summary>
		public int PerCentOfPerFillBlank
		{
			get
			{

				int blankNum=DBAccess.RunSQLReturnValInt("select count(BlankNum) from GTS_FillBlank where No in  ( SELECT FK_FillBlank from GTS_PaperVSFillBlank where fk_Paper='"+this.No+"' )");
				return this.CentOfPerFillBlank/blankNum;
			}
		}
		public string ValidTimeFrom
		{
			get
			{
				return this.GetValStringByKey(PaperRandomDesignAttr.ValidTimeFrom);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.ValidTimeFrom,value);
			}
		}
		public string ValidTimeTo
		{
			get
			{
				return this.GetValStringByKey(PaperRandomDesignAttr.ValidTimeTo);
			}
			set
			{
				this.SetValByKey(PaperRandomDesignAttr.ValidTimeTo,value);
			}
		}
		public bool IsValid
		{
			get
			{
				DateTime dtfrom=DataType.ParseSysDateTime2DateTime(this.ValidTimeFrom);
				DateTime dtto=DataType.ParseSysDateTime2DateTime(this.ValidTimeTo);
				DateTime dt=DateTime.Now;

				if ( dtfrom <= dt || dt>= dtto )
					return true;
				else
					return false;
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
				
				Map map = new Map("GTS_PaperRandomDesign");
				map.EnDesc="随机试卷设计";
				map.CodeStruct="4";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(PaperRandomDesignAttr.No,null,"编号",true,true,0,50,20);
				map.AddTBString(PaperRandomDesignAttr.Name,"新建随机试卷1","名称",true,false,0,50,20);

				DateTime dt = DateTime.Now;
				dt=dt.AddDays(1);

				map.AddTBDateTime(PaperRandomDesignAttr.ValidTimeFrom,dt.ToString("yyyy-MM-dd")+" 09:00","有效时间从",true,false);
				map.AddTBDateTime(PaperRandomDesignAttr.ValidTimeTo,dt.ToString("yyyy-MM-dd")+" 10:00","到",true,false);


				map.AddTBInt(PaperRandomDesignAttr.NumOfChoseOne,30,"单选题个数",true,false);
				map.AddTBInt(PaperRandomDesignAttr.CentOfPerChoseOne,1,"每单选题分",true,false);

				map.AddTBInt(PaperRandomDesignAttr.NumOfChoseM,10,"多选题个数",true,false);
				map.AddTBInt(PaperRandomDesignAttr.CentOfPerChoseM,2,"每多选题分",true,false);

				map.AddTBInt(PaperRandomDesignAttr.NumOfFillBlank,10,"填空题个数",true,false);
				map.AddTBInt(PaperRandomDesignAttr.CentOfPerFillBlank,1,"每填空题分",true,false);
				map.AddTBInt(PaperRandomDesignAttr.NumOfJudgeTheme,10,"判断题个数",true,false);
				map.AddTBInt(PaperRandomDesignAttr.CentOfPerJudgeTheme,1,"每判断题分",true,false);

				map.AddTBInt(PaperRandomDesignAttr.NumOfEssayQuestion,5,"问答题个数",true,false);
				map.AddTBInt(PaperRandomDesignAttr.CentOfPerEssayQuestion,4,"每问答题分",true,false);

				map.AddTBInt(PaperRandomDesignAttr.NumOfRC,1,"阅读题个数",true,false);
				map.AddTBInt(PaperRandomDesignAttr.CentOfPerRC,10,"阅读题总分",true,false);
				map.AddTBInt(PaperRandomDesignAttr.CentOfSum,100,"总分",true,true);
				map.AddTBInt(PaperRandomDesignAttr.MM,90,"考试时间(分钟)",true,false);

				map.AttrsOfOneVSM.Add( new PaperVSEmps(), new Emps(),PaperVSEmpAttr.FK_Paper,PaperVSEmpAttr.FK_Emp, RCAttr.Name,RCAttr.No,"考试的学生");
				this._enMap=map;
				return this._enMap;
			}
		}
		protected override bool beforeUpdateInsertAction()
		{
			// 判断是此考试已经由开始的考生。
			string sql="SELECT COUNT(*) FROM GTS_PaperExamRandom WHERE ExamState >0 and FK_Paper='"+this.No+"' " ; 
			int i=DBAccess.RunSQLReturnValInt(sql);
			if (i>=1)
				throw new Exception("试卷["+this.Name+"], 不能被您再设计，因为它已经有“"+i+"”个考生开始做题。");

			if (this.CentOfPerChoseOne==0 && this.NumOfChoseOne > 0 )
				throw new Exception("每 单选题 分数不能为0。");

			if (this.CentOfPerChoseM==0 && this.NumOfChoseM > 0 )
				throw new Exception("每 多选题 分数不能为0。");

			if (this.CentOfPerFillBlank==0 && this.NumOfFillBlank > 0 )
				throw new Exception("每 填空题 分数不能为0。");

			if (this.CentOfPerJudgeTheme==0 && this.PerCentOfPerJudgeTheme > 0 )
				throw new Exception("每 判断题 分数不能为0。");

			//			if (this.CentOfPerEssayQuestion==0 && this.PerCentOfPerEssayQuestion > 0 )
			//				throw new Exception("每 问答题 分数不能为0。");

			if (this.CentOfPerRC==0 && this.NumOfRC > 0 )
				throw new Exception("每阅读题分数不能为0。");

			if (this.NumOfRC==0)
				this.CentOfPerRC=0;
			 
			if (this.NumOfEssayQuestion==0)
				this.CentOfPerEssayQuestion=0;


			int centsum=0;
			centsum+=this.CentOfPerChoseOne*this.NumOfChoseOne;
			centsum+=this.CentOfPerChoseM*this.NumOfChoseM;
			centsum+=this.CentOfPerFillBlank*this.NumOfFillBlank;
			centsum+=this.CentOfPerJudgeTheme*this.NumOfJudgeTheme;
			centsum+=this.CentOfPerEssayQuestion*this.NumOfEssayQuestion;
			centsum+=this.CentOfPerRC;

			sql="SELECT COUNT(*) FROM V_GTS_ChoseOne";
			if ( DBAccess.RunSQLReturnValInt(sql) <this.NumOfChoseOne)
				throw new Exception("单选题 数目太大");

			sql="SELECT COUNT(*) FROM V_GTS_ChoseM";
			if ( DBAccess.RunSQLReturnValInt(sql) <this.NumOfChoseM)
				throw new Exception("多选题 数目太大");

			sql="SELECT COUNT(*) FROM GTS_EssayQuestion";
			if ( DBAccess.RunSQLReturnValInt(sql) <this.NumOfEssayQuestion)
				throw new Exception("简答题 数目太大");


			sql="SELECT COUNT(*) FROM GTS_FillBlank";
			if ( DBAccess.RunSQLReturnValInt(sql) <this.NumOfFillBlank)
				throw new Exception("填空题 数目太大");

			sql="SELECT COUNT(*) FROM GTS_RC";
			if ( DBAccess.RunSQLReturnValInt(sql) <this.NumOfRC)
				throw new Exception("阅读理解 "+NumOfRC+" 数目太大");

			sql="SELECT COUNT(*) FROM GTS_JudgeTheme";
			if ( DBAccess.RunSQLReturnValInt(sql) <this.NumOfJudgeTheme)
				throw new Exception("判断题 "+NumOfJudgeTheme+"数目太大");

			if (this.CentOfPerRC ==0 )
				this.NumOfRC=0;

			if (this.NumOfRC==0)
				this.CentOfPerRC=0;

			/* 1, 初始化考生, 在考试时间设置考生范围, 给每个考生初始化他的试卷格式，但是不给他设置题目。
			 * 等待用户进入考试后再给他题目。
			 * 2, 增加考试任务。
			 * */
			sql="DELETE GTS_PaperExam WHERE FK_Emp NOT IN (SELECT FK_Emp FROM GTS_PaperVSEmp WHERE FK_Paper='"+this.No+"') AND FK_Paper='"+this.No+"'";
			DBAccess.RunSQL(sql); // 删除没有。

			sql="SELECT FK_Emp FROM GTS_PaperVSEmp WHERE FK_Paper='"+this.No+"' AND FK_Emp ";
			sql+="NOT IN (SELECT FK_Emp FROM GTS_PaperExam WHERE FK_Paper='"+this.No+"')";
			DataTable dt = DBAccess.RunSQLReturnTable(sql);

			// 删除所有
			DBAccess.RunSQL("DELETE GTS_PaperExamRandom WHERE FK_Paper='"+this.No+"' ");
			foreach(DataRow dr in dt.Rows)
			{
				PaperExamRandom pe = new PaperExamRandom();
				pe.No=dr[0].ToString()+""+this.No;
				pe.FK_Emp=dr[0].ToString();
				pe.FK_Paper=this.No;
				pe.Insert(); // 增加考试任务。
			}
			// 总分
			this.CentOfSum=this.CentOfPerChoseOne*this.NumOfChoseOne+this.CentOfPerChoseM*this.NumOfChoseM+this.CentOfPerEssayQuestion*this.NumOfEssayQuestion+this.CentOfPerFillBlank*this.NumOfFillBlank+this.CentOfPerJudgeTheme*this.NumOfJudgeTheme+this.CentOfPerRC;
			return base.beforeUpdateInsertAction();
		}
		#endregion 

		#region 构造方法
		/// <summary>
		/// 试卷
		/// </summary> 
		public PaperRandomDesign()
		{
		}
		/// <summary>
		/// 试卷
		/// </summary>
		/// <param name="_No">征收机关编号</param> 
		public PaperRandomDesign(string _No ):base(_No)
		{
		}
		#endregion 

		#region 逻辑处理
		#endregion

	}
	/// <summary>
	///  试卷
	/// </summary>
	public class PaperRandomDesigns :EntitiesNoName
	{
		/// <summary>
		/// PaperRandomDesigns
		/// </summary>
		public PaperRandomDesigns(){}
		/// <summary>
		/// PaperRandomDesign
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperRandomDesign();
			}
		}
		 
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fk_emp"></param>
		/// <returns></returns>
		public int RetrievePaperRandomDesign(string fk_emp)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhereInSQL(PaperFixAttr.No,  "SELECT FK_Paper FROM GTS_PaperVSEmp WHERE FK_Emp='"+fk_emp+"'");
			return qo.DoQuery();
		}

		 
	}
}
