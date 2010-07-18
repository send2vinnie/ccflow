using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.GTS
{
	/// <summary>
	/// 固定试卷
	/// </summary>
	public class PaperFixAttr:EntityNoNameAttr
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
		/// <summary>
		/// ValidTimeFrom
		/// </summary>
		public const string ValidTimeFrom="ValidTimeFrom";
		/// <summary>
		/// ValidTimeTo
		/// </summary>
		public const string ValidTimeTo="ValidTimeTo";
        public const string TTypes = "TTypes";
	}
	  
	/// <summary>
	/// 固定试卷
	/// </summary>
	public class PaperFix :EntityNoName
	{
 
		#region attrs
		 
		/// <summary>
		/// 单选
		/// </summary>
        public decimal CentOfChoseOne
		{
			get
			{
				return this.GetValDecimalByKey(PaperFixAttr.CentOfChoseOne);
			}
			set
			{
				this.SetValByKey(PaperFixAttr.CentOfChoseOne,value);
			}
		}
		/// <summary>
		/// 多选
		/// </summary>
		public decimal CentOfChoseM
		{
			get
			{
                return this.GetValDecimalByKey(PaperFixAttr.CentOfChoseM);
			}
			set
			{
				this.SetValByKey(PaperFixAttr.CentOfChoseM,value);
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
        public ChoseOnes HisChoseOnes_SQL
        {
            get
            {
                ChoseOnes chs = new ChoseOnes();
                QueryObject qo = new QueryObject(chs);
                qo.AddWhereInSQL(ChoseOneAttr.No, "SELECT fk_chose from GTS_PaperVSChoseOne where FK_Paper='" + this.No + "'");
                qo.addOrderByRandom();
                qo.DoQuery();
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
				if (BP.DA.Cash.IsExits("ChoseOnes"+this.No,Depositary.Session) ==false )
				{
					ChoseOnes chs = new ChoseOnes();
					QueryObject qo = new QueryObject(chs);
					qo.AddWhereInSQL( ChoseOneAttr.No, "SELECT fk_chose from GTS_PaperVSChoseOne where FK_Paper='"+this.No+"'");
					qo.addOrderByRandom();
					qo.DoQuery();
					BP.DA.Cash.AddObj("ChoseOnes"+this.No, Depositary.Session,chs );
					return chs;
				}
				return (ChoseOnes)BP.DA.Cash.GetObjFormSession("ChoseOnes"+this.No);
			}
		}
        public ChoseMs HisChoseMs_SQL
        {
            get
            {
                ChoseMs chs = new ChoseMs();
                QueryObject qo = new QueryObject(chs);
                qo.AddWhereInSQL(ChoseOneAttr.No, "select fk_chose from GTS_PaperVSChoseM where FK_Paper='" + this.No + "'");
                qo.addOrderByRandom();
                qo.DoQuery();
                return chs;
            }
        }
        public JudgeThemes HisJudgeThemes_SQL
        {
            get
            {
                JudgeThemes chs = new JudgeThemes();
                QueryObject qo = new QueryObject(chs);
                qo.AddWhereInSQL(ChoseOneAttr.No, "select fk_JudgeTheme from GTS_PaperVSJudgeTheme where FK_Paper='" + this.No + "'");
                qo.addOrderByRandom();
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
				if (BP.DA.Cash.IsExits("HisChoseMs"+this.No,Depositary.Session) ==false )
				{
					ChoseMs chs = new ChoseMs();
					QueryObject qo = new QueryObject(chs);
					qo.AddWhereInSQL( ChoseOneAttr.No, "select fk_chose from GTS_PaperVSChoseM where FK_Paper='"+this.No+"'");
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
					qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_JudgeTheme from GTS_PaperVSJudgeTheme where FK_Paper='"+this.No+"'");
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
					qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_EssayQuestion from GTS_PaperVSEssayQuestion where FK_Paper='"+this.No+"'");
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
					qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_RC from GTS_PaperVSRC where FK_Paper='"+this.No+"'");
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
					qo.AddWhereInSQL( JudgeThemeAttr.No, "SELECT FK_FillBlank from GTS_PaperVSFillBlank where FK_Paper='"+this.No+"'");
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
		public string ValidTimeFrom
		{
			get
			{
				return this.GetValStringByKey(PaperFixAttr.ValidTimeFrom);
			}
			set
			{
				this.SetValByKey(PaperFixAttr.ValidTimeFrom,value);
			}
		}
		public string ValidTimeTo
		{
			get
			{
				return this.GetValStringByKey(WorkRDAttr.ValidTimeTo);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.ValidTimeTo,value);
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
        public string TTypes
        {
            get
            {
                return this.GetValStringByKey(PaperFixAttr.TTypes);
            }
            set
            {
                this.SetValByKey(PaperFixAttr.TTypes, value);
            }
        }
		/// <summary>
		/// 判断
		/// </summary>
		public decimal CentOfJudgeTheme
		{
			get
			{
                return this.GetValDecimalByKey(PaperFixAttr.CentOfJudgeTheme);
			}
			set
			{
				this.SetValByKey(PaperFixAttr.CentOfJudgeTheme,value);
			}
		}
		/// <summary>
		/// 问答
		/// </summary>
		public decimal CentOfEssayQuestion
		{
			get
			{
                return this.GetValDecimalByKey(PaperFixAttr.CentOfEssayQuestion);
			}
			set
			{
				this.SetValByKey(PaperFixAttr.CentOfEssayQuestion,value);
			}
		}
		/// <summary>
		/// 阅读
		/// </summary>
		public decimal CentOfRC
		{
			get
			{
                return this.GetValDecimalByKey(PaperFixAttr.CentOfRC);
			}
			set
			{
				this.SetValByKey(PaperFixAttr.CentOfRC,value);
			}
		}
		/// <summary>
		/// 填空
		/// </summary>
		public decimal CentOfFillBlank
		{
			get
			{
                return this.GetValDecimalByKey(PaperFixAttr.CentOfFillBlank);
			}
			set
			{
				this.SetValByKey(PaperFixAttr.CentOfFillBlank,value);
			}
		}
		/// <summary>
		/// 合计
		/// </summary>
		public decimal CentOfSum
		{
			get
			{
                return this.GetValDecimalByKey(PaperFixAttr.CentOfSum);
			}
			set
			{
				this.SetValByKey(PaperFixAttr.CentOfSum,value);
			}
		}
		/// <summary>
		/// 时长
		/// </summary>
		public int MM
		{
			get
			{
				return this.GetValIntByKey(PaperFixAttr.MM);
			}
			set
			{
				this.SetValByKey(PaperFixAttr.MM,value);
			}
		}
		#endregion

		#region attrs ext
		/// <summary>
		/// 每个单项选择题得分。
		/// </summary>
		public decimal SumCentOfChoseOne
		{
			get
			{
				string sql="SELECT COUNT(*) FROM GTS_PaperVSChoseOne WHERE FK_Paper='"+this.No+"'";
				int i=0;
				i=DBAccess.RunSQLReturnValInt(sql);
                return decimal.Parse(i.ToString()) * this.CentOfChoseOne;
			}
		}
		/// <summary>
		/// duo项选择题得分。
		/// </summary>
		public decimal SumCentOfChoseM
		{
			get
			{
				string sql="SELECT COUNT(*) FROM GTS_PaperVSChoseM WHERE FK_Paper='"+this.No+"'";
				int i=0;
				i=DBAccess.RunSQLReturnValInt(sql);
                return decimal.Parse(i.ToString()) * this.CentOfChoseM;
			}
		}
		/// <summary>
		/// 判断题的分。
		/// </summary>
		public decimal SumCentOfJudgeTheme
		{
			get
			{
				string sql="SELECT COUNT(*) FROM GTS_PaperVSJudgeTheme WHERE FK_Paper='"+this.No+"'";
				int i=0;
				i=DBAccess.RunSQLReturnValInt(sql);
				return decimal.Parse(i.ToString()) *this.CentOfJudgeTheme;
			}
		}
		/// <summary>
		/// 填空题
		/// </summary>
		public decimal SumCentOfFillBlank
		{
			get
			{
				int blankNum=DBAccess.RunSQLReturnValInt("SELECT ISNULL( SUM(BlankNum) , 0 )  FROM GTS_FillBlank WHERE No IN  ( SELECT FK_FillBlank from GTS_PaperVSFillBlank where FK_Paper='"+this.No+"' )");
				return this.CentOfFillBlank * decimal.Parse( blankNum.ToString() );
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
				
				Map map = new Map("GTS_PaperFix");
				map.EnDesc="固定试卷设计";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;
				map.AddTBStringPK(PaperFixAttr.No,null,"编号",true,true,0,50,20);
				map.AddTBString(PaperFixAttr.Name,"新建固定试卷1","名称",true,false,0,50,20);

				DateTime dt = DateTime.Now;
				dt=dt.AddDays(1);
				map.AddTBDateTime(PaperFixAttr.ValidTimeFrom,dt.ToString("yyyy-MM-dd")+" 09:00","有效时间从",true,false);
				map.AddTBDateTime(PaperFixAttr.ValidTimeTo,dt.ToString("yyyy-MM-dd")+" 10:00","到",true,false);

                map.AddTBDecimal(PaperFixAttr.MM, 90, "考试时间(分钟)", true, false);
                map.AddTBDecimal(PaperFixAttr.CentOfChoseOne, 1, "单选题每题分", true, false);
                map.AddTBDecimal(PaperFixAttr.CentOfChoseM, 1, "多选题每题分", true, false);
                map.AddTBDecimal(PaperFixAttr.CentOfFillBlank, 1, "填空题每空分", true, false);
                map.AddTBDecimal(PaperFixAttr.CentOfJudgeTheme, 1, "判断题每题分", true, false);
                map.AddTBDecimal(PaperFixAttr.CentOfEssayQuestion, 1, "问答题分", true, true);
                map.AddTBDecimal(PaperFixAttr.CentOfRC, 20, "阅读题分", true, true);
                map.AddTBDecimal(PaperFixAttr.CentOfSum, 0, "总分", true, true);


                map.AddTBString(PaperFixAttr.TTypes, null, "题目类型s", false, false, 0, 50, 20);

				map.AttrsOfOneVSM.Add( new PaperVSChoseOnes(), new ChoseOnes(),PaperVSChoseOneAttr.FK_Paper,PaperVSChoseOneAttr.FK_Chose, ChoseOneAttr.Name,ChoseOneAttr.No,"单选题");
				map.AttrsOfOneVSM.Add( new PaperVSChoseMs(), new ChoseMs(),PaperVSChoseMAttr.FK_Paper,PaperVSChoseMAttr.FK_Chose, ChoseMAttr.Name,ChoseMAttr.No,"多选题");
				map.AttrsOfOneVSM.Add( new PaperVSFillBlanks(), new FillBlanks(),PaperVSFillBlankAttr.FK_Paper,PaperVSFillBlankAttr.FK_FillBlank, FillBlankAttr.Name,FillBlankAttr.No,"填空题");
				map.AttrsOfOneVSM.Add( new PaperVSJudgeThemes(), new JudgeThemes(),PaperVSJudgeThemeAttr.FK_Paper,PaperVSJudgeThemeAttr.FK_JudgeTheme, JudgeThemeAttr.Name,JudgeThemeAttr.No,"判断题");
				map.AttrsOfOneVSM.Add( new PaperVSEssayQuestions(), new EssayQuestions(),PaperVSEssayQuestionAttr.FK_Paper,PaperVSEssayQuestionAttr.FK_EssayQuestion, EssayQuestionAttr.Name,EssayQuestionAttr.No,"问答题");
				map.AttrsOfOneVSM.Add( new PaperVSRCs(), new RCs(),PaperVSRCAttr.FK_Paper,PaperVSRCAttr.FK_RC, RCAttr.Name,RCAttr.No,"阅读题");

				map.AttrsOfOneVSM.Add( new PaperVSEmps(), new Emps(),PaperVSEmpAttr.FK_Paper,PaperVSEmpAttr.FK_Emp, RCAttr.Name,RCAttr.No,"考试的学生");
				map.AddDtl(new PaperVSEssayQuestions(),PaperVSEssayQuestionAttr.FK_Paper);
				map.AddDtl(new PaperVSRCDtls(),PaperVSRCAttr.FK_Paper);

                RefMethod rm = new RefMethod();
                rm.Title = "生成试卷";
                rm.ClassMethodName = this.ToString() + ".DoGenerIt";
                map.AddRefMethod(rm);


				this._enMap=map;
				return this._enMap;
			}
		}
        public string DoGenerIt()
        {
            PubClass.WinOpen("../App/Paper/GenerPaper.aspx?No="+this.No);
            return null;

            string doc = this.Name;
            doc += "<BR><b>"+this.Name+" </b><hr>";

            ChoseOnes ones = this.HisChoseOnes;
            doc += "<BR><b>一、</b>单项选择题 共" + ones.Count + "个 <br>";
            int i=0;
            foreach (ChoseOne on in ones)
            {
                i++;
                doc += i.ToString() + " 、" +on.Name;
                doc += "<br> &nbsp;&nbsp;&nbsp;&nbsp;";
                ChoseItems items = on.HisChoseItems;
                foreach (ChoseItem item in items)
                {
                    doc += item.Item + "、" + item.ItemDoc + "<br>";
                }
            }


            ChoseMs chms = this.HisChoseMs;
            doc += "<BR><b>二、</b>多选题   共"+chms.Count+"个<br>";
             i = 0;
            foreach (ChoseM on in chms)
            {
                i++;
                doc += i.ToString() + " 、" + on.Name;
                doc += "<br> &nbsp;&nbsp;&nbsp;&nbsp;";
                ChoseItems items = on.HisChoseItems;
                foreach (ChoseItem item in items)
                {
                    doc += item.Item + "、" + item.ItemDoc + "<br>";
                }
            }

           

            JudgeThemes jts = this.HisJudgeThemes;
            doc += "<BR><b>三、</b>判断题   共" + jts.Count + "个<br>";
              i = 0;
            foreach (JudgeTheme on in jts)
            {
                i++;
                doc += i.ToString() + " 、" + on.Name;
                doc += "<br> &nbsp;&nbsp;&nbsp;&nbsp;";
                doc += "<br> A 正确        B 错误";
            }

            return doc;
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

            // 判断是否有学生做题。
            string sql = "SELECT COUNT(*) FROM GTS_PaperExam WHERE FK_Paper='" + this.No + "' AND ExamState >0  ";
            int i = DBAccess.RunSQLReturnValInt(sql);
            if (i >= 1)
                throw new Exception("试卷[" + this.Name + "], 不能被您再设计，因为它已经有“" + i + "”个学生开始做题。");


            // 设置分数。
            this.CentOfEssayQuestion = DBAccess.RunSQLReturnValDecimal("select isnull( sum(cent), 0) from GTS_PaperVSEssayQuestion where FK_Paper='" + this.No + "'", 0, 2);
            this.CentOfRC = DBAccess.RunSQLReturnValDecimal("select isnull( sum(cent), 0) from GTS_PaperVSRCDtl where FK_Paper='" + this.No + "'", 0, 2);

            this.CentOfSum = this.SumCentOfFillBlank + this.SumCentOfChoseOne + this.SumCentOfChoseM + this.SumCentOfJudgeTheme + this.CentOfEssayQuestion + this.CentOfRC;

            // 初始化学生 , 在考试时间设置学生范围, 给每个学生初始化他的试卷。
            sql = "DELETE GTS_PaperExam WHERE FK_Emp NOT IN (SELECT FK_Emp FROM GTS_PaperVSEmp WHERE FK_Paper='" + this.No + "') AND FK_Paper='" + this.No + "'";
            DBAccess.RunSQL(sql); // 删除没有涉及到学生。

            sql = "SELECT FK_Emp FROM GTS_PaperVSEmp WHERE FK_Paper='" + this.No + "' AND FK_Emp ";
            sql += "NOT IN (SELECT FK_Emp FROM GTS_PaperExam WHERE FK_Paper='" + this.No + "')";
            DataTable dt = DBAccess.RunSQLReturnTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                PaperExam pe = new PaperExam();
                pe.No = dr[0].ToString() + this.No;
                pe.FK_Emp = dr[0].ToString();
                pe.FK_Paper = this.No;
                pe.Insert();
            }

            string ttypes = "";
            if (this.SumCentOfChoseM > 0)
                ttypes += "@" + BP.GTS.ThemeType.ChoseM;

            if (this.SumCentOfChoseOne > 0)
                ttypes += "@" + BP.GTS.ThemeType.ChoseOne;

            if (this.CentOfEssayQuestion > 0)
                ttypes += "@" + BP.GTS.ThemeType.EssayQuestion;

            if (this.SumCentOfFillBlank > 0)
                ttypes += "@" + BP.GTS.ThemeType.FillBlank;

            if (this.SumCentOfJudgeTheme > 0)
                ttypes += "@" + BP.GTS.ThemeType.JudgeTheme;

            if (this.CentOfRC > 0)
                ttypes += "@" + BP.GTS.ThemeType.RC;

            this.TTypes = ttypes;
            return base.beforeUpdateInsertAction();
        }
		#endregion 

		#region 构造方法
		/// <summary>
		/// 试卷
		/// </summary> 
		public PaperFix()
		{
		}
		/// <summary>
		/// 试卷
		/// </summary>
		/// <param name="_No">征收机关编号</param> 
		public PaperFix(string _No ):base(_No)
		{

		}
		#endregion 

		#region 逻辑处理
		#endregion

	}
	/// <summary>
	///  固定试卷
	/// </summary>
	public class PaperFixs :EntitiesNoName
	{
		/// <summary>
		/// 固定试卷s
		/// </summary>
		public PaperFixs(){}

		/// <summary>
		/// 固定试卷s
		/// </summary>
		/// <param name="fk_emp">操作员</param>
		public PaperFixs(string fk_emp)
		{
			QueryObject qo= new QueryObject(this);
			qo.AddWhereInSQL(PaperFixAttr.No, "SELECT FK_Paper FROM GTS_PaperVSEmp WHERE FK_Emp='"+Web.WebUser.No+"'" );
			qo.DoQuery();
		}

		/// <summary>
		/// 固定试卷
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperFix();
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fk_emp"></param>
		/// <returns></returns> 
		public int RetrievePaperFix(string fk_emp)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhereInSQL(PaperFixAttr.No,  "SELECT FK_Paper FROM GTS_PaperVSEmp WHERE FK_Emp='"+fk_emp+"'");
			return qo.DoQuery();
		}
	 
	}

	 
}
