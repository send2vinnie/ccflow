using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// 作业attr
	/// </summary>
	public class WorkRDAttr:EntityNoNameAttr
	{
		/// <summary>
		/// 到
		/// </summary>
		public const string ValidTimeTo="ValidTimeTo";
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
	/// 作业
	/// </summary>
	public class WorkRD :EntityNoName
	{

		#region his attrs
		/// <summary>
		/// 考试集合
		/// </summary>
		public WorkRandoms HisWorkRandoms
		{
			get
			{
				WorkRandoms chs = new WorkRandoms(this.No);
				return chs;
			}
		}
		
		#endregion

		#region attrs
		public bool IsValid
		{
			get
			{
				DateTime dtto=DataType.ParseSysDateTime2DateTime(this.ValidTimeTo+" 10:10");
				DateTime dt=DateTime.Now;
				if ( dtto >= dt )
					return true;
				else
					return false;
			}
		}
		/// <summary>
		/// 单选
		/// </summary>
		public int CentOfPerChoseOne
		{
			get
			{
				return this.GetValIntByKey(WorkRDAttr.CentOfPerChoseOne);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.CentOfPerChoseOne,value);
			}
		}
		/// <summary>
		/// 多选
		/// </summary>
		public int CentOfPerChoseM
		{
			get
			{
				return this.GetValIntByKey(WorkRDAttr.CentOfPerChoseM);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.CentOfPerChoseM,value);
			}
		}
		/// <summary>
		/// 判断
		/// </summary>
		public int CentOfPerJudgeTheme
		{
			get
			{
				return this.GetValIntByKey(WorkRDAttr.CentOfPerJudgeTheme);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.CentOfPerJudgeTheme,value);
			}
		}
		/// <summary>
		/// 问答
		/// </summary>
		public int CentOfPerEssayQuestion
		{
			get
			{
				return this.GetValIntByKey(WorkRDAttr.CentOfPerEssayQuestion);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.CentOfPerEssayQuestion,value);
			}
		}
		/// <summary>
		/// 阅读
		/// </summary>
		public int CentOfPerRC
		{
			get
			{
				return this.GetValIntByKey(WorkRDAttr.CentOfPerRC);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.CentOfPerRC,value);
			}
		}
		/// <summary>
		/// 填空
		/// </summary>
		public int CentOfPerFillBlank
		{
			get
			{
				return this.GetValIntByKey(WorkRDAttr.CentOfPerFillBlank);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.CentOfPerFillBlank,value);
			}
		}
		/// <summary>
		/// 合计
		/// </summary>
		public int CentOfSum
		{
			get
			{
				return this.GetValIntByKey(WorkRDAttr.CentOfSum);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.CentOfSum,value);
			}
		}
		/// <summary>
		/// 时长
		/// </summary>
		public int MM
		{
			get
			{
				return this.GetValIntByKey(WorkRDAttr.MM);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.MM,value);
			}
		}
		#endregion

		#region Num of 
		/// <summary>
		/// 单选
		/// </summary>
		public int NumOfChoseOne
		{
			get
			{
				return this.GetValIntByKey(WorkRDAttr.NumOfChoseOne);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.NumOfChoseOne,value);
			}
		}
		/// <summary>
		/// 多选
		/// </summary>
		public int NumOfChoseM
		{
			get
			{
				return this.GetValIntByKey(WorkRDAttr.NumOfChoseM);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.NumOfChoseM,value);
			}
		}
		/// <summary>
		/// 判断
		/// </summary>
		public int NumOfJudgeTheme
		{
			get
			{
				return this.GetValIntByKey(WorkRDAttr.NumOfJudgeTheme);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.NumOfJudgeTheme,value);
			}
		}
		/// <summary>
		/// 问答
		/// </summary>
		public int NumOfEssayQuestion
		{
			get
			{
				return this.GetValIntByKey(WorkRDAttr.NumOfEssayQuestion);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.NumOfEssayQuestion,value);
			}
		}
		/// <summary>
		/// 阅读
		/// </summary>
		public int NumOfRC
		{
			get
			{
				return this.GetValIntByKey(WorkRDAttr.NumOfRC);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.NumOfRC,value);
			}
		}
		/// <summary>
		/// 填空
		/// </summary>
		public int NumOfFillBlank
		{
			get
			{
				return this.GetValIntByKey(WorkRDAttr.NumOfFillBlank);
			}
			set
			{
				this.SetValByKey(WorkRDAttr.NumOfFillBlank,value);
			}
		}
		#endregion

		#region attrs ext
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
				
				Map map = new Map("GTS_WorkRD");
				map.EnDesc="随机作业设计";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(WorkRDAttr.No,null,"编号",true,true,0,50,20);
				map.AddTBString(WorkRDAttr.Name,"新建随机作业1","名称",true,false,0,50,20);

				map.AddTBInt(WorkRDAttr.NumOfChoseOne,30,"单选题个数",true,false);
				map.AddTBInt(WorkRDAttr.CentOfPerChoseOne,1,"每单选题分",true,false);

				map.AddTBInt(WorkRDAttr.NumOfChoseM,10,"多选题个数",true,false);
				map.AddTBInt(WorkRDAttr.CentOfPerChoseM,2,"每多选题分",true,false);

				map.AddTBInt(WorkRDAttr.NumOfFillBlank,10,"填空题个数",true,false);
				map.AddTBInt(WorkRDAttr.CentOfPerFillBlank,1,"每填空题分",true,false);

				map.AddTBInt(WorkRDAttr.NumOfJudgeTheme,10,"判断题个数",true,false);
				map.AddTBInt(WorkRDAttr.CentOfPerJudgeTheme,1,"每判断题分",true,false);

				map.AddTBInt(WorkRDAttr.NumOfEssayQuestion,5,"问答题个数",true,false);
				map.AddTBInt(WorkRDAttr.CentOfPerEssayQuestion,4,"每问答题分",true,false);

				map.AddTBInt(WorkRDAttr.NumOfRC,1,"阅读题个数",true,false);
				map.AddTBInt(WorkRDAttr.CentOfPerRC,10,"阅读题总分",true,false);

				map.AddTBInt(WorkRDAttr.CentOfSum,100,"总分",true,true);

				DateTime dt = DateTime.Now;
				dt=dt.AddDays(7);
				map.AddTBDateTime(WorkRDAttr.ValidTimeTo,dt.ToString("yyyy-MM-dd")+" 10:00","收作业时间",true,false);


				map.AttrsOfOneVSM.Add( new WorkRandomVSEmps(), new Emps(),WorkRandomVSEmpAttr.FK_Work,WorkRandomVSEmpAttr.FK_Emp, RCAttr.Name,RCAttr.No,"被布置的学生");
				this._enMap=map;
				return this._enMap;
			}
		}
		protected override bool beforeUpdateInsertAction()
		{
			// 判断是此考试已经由开始的考生。
			string sql="SELECT COUNT(*) FROM GTS_WorkVSChoseOne WHERE FK_Work IN ( SELECT No FROM GTS_WorkRandom WHERE FK_WorkRD='"+this.No+"')" ;

			int i=DBAccess.RunSQLReturnValInt(sql);
			if (i>=1)
				throw new Exception("作业["+this.Name+"], 不能被您再设计，因为它已经有一个或多学生开始做题。");

			if (this.CentOfPerChoseOne==0 && this.NumOfChoseOne > 0 )
				throw new Exception("每 单选题 分数不能为0。");

			if (this.CentOfPerChoseM==0 && this.NumOfChoseM > 0 )
				throw new Exception("每 多选题 分数不能为0。");

			if (this.CentOfPerFillBlank==0 && this.NumOfFillBlank > 0 )
				throw new Exception("每 填空题 分数不能为0。");

			if (this.CentOfPerJudgeTheme==0 && this.NumOfJudgeTheme > 0 )
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

			/* 1, 初始化考生, 在考试时间设置考生范围, 给每个考生初始化他的作业格式，但是不给他设置题目。
			 * 等待用户进入考试后再给他题目。
			 * 2, 增加考试任务。
			 * */
//			sql="DELETE GTS_WorkRandom WHERE FK_Emp NOT IN (SELECT FK_Emp FROM GTS_WorkRandomVSEmp WHERE FK_Work='"+this.No+"') AND FK_WorkRandom='"+this.No+"'";
//			DBAccess.RunSQL(sql); // 删除没有。
//
//			sql="SELECT FK_Emp FROM GTS_WorkRandomVSEmp WHERE FK_WorkRandom='"+this.No+"' AND FK_Emp ";
//			sql+="NOT IN (SELECT FK_Emp FROM GTS_WorkRandom WHERE FK_WorkRD='"+this.No+"')";

			sql="SELECT FK_Emp FROM GTS_WorkRandomVSEmp WHERE FK_Work='"+this.No+"'";
			DataTable dt = DBAccess.RunSQLReturnTable(sql);

			// 删除所有
			DBAccess.RunSQL("DELETE GTS_WorkRandom WHERE FK_WorkRD='"+this.No+"' ");
			foreach(DataRow dr in dt.Rows)
			{
				WorkRandom pe = new WorkRandom();
				pe.FK_Emp=dr[0].ToString();
				pe.FK_WorkRD=this.No;
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
		public WorkRD()
		{
		}
		/// <summary>
		/// 试卷
		/// </summary>
		/// <param name="_No">征收机关编号</param> 
		public WorkRD(string _No ):base(_No)
		{
		}
		#endregion 

		

	}
	/// <summary>
	///  作业
	/// </summary>
	public class WorkRDs :EntitiesNoName
	{
		/// <summary>
		/// WorkRDs
		/// </summary>
		public WorkRDs(){}
		/// <summary>
		/// WorkRD
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkRD();
			}
		}
		 
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fk_emp"></param>
		/// <returns></returns>
		public int RetrieveWorkRD(string fk_emp)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhereInSQL(PaperFixAttr.No,  "SELECT FK_Work FROM GTS_WorkRandomVSEmp WHERE FK_Emp='"+fk_emp+"'");
			return qo.DoQuery();
		}

		 
	}
}
