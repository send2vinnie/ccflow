using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// 试卷答案attr
	/// </summary>
	public class ExamAttr:EntityOIDAttr
	{
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
	}
	 
	/// <summary>
	/// 试卷答案
	/// </summary>
	public class Exam :EntityNo
	{

	 
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
		/// 是否超出了时间
		/// </summary>
		public bool IsOutTime
		{
			get
			{
				return false;
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
				
				Map map = new Map("GTS_Exam");
				map.EnDesc="考卷";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;

				map.AddTBStringPK(PaperExamRandomAttr.No,null,"系统ID",false,true,0,50,20);
				map.AddDDLSysEnum(PaperExamRandomAttr.ExamState,0,"状态",true,true);
				map.AddDDLEntities(PaperExamRandomAttr.FK_Paper,null,"试卷",new PaperExts(),false);
				map.AddDDLEntities(PaperExamRandomAttr.FK_Emp,Web.WebUser.No,"学生",new EmpExts(),false);
				map.AddDDLEntities(PaperExamRandomAttr.FK_Dept,Web.WebUser.FK_Dept,"部门",new Depts(),false);

				map.AddTBInt(PaperExamRandomAttr.CentOfChoseOne,0,"单选题分",true,true);
				map.AddTBInt(PaperExamRandomAttr.CentOfChoseM,0,"多选题分",true,true);
				map.AddTBInt(PaperExamRandomAttr.CentOfFillBlank,0,"填空题分",true,true);
				map.AddTBInt(PaperExamRandomAttr.CentOfJudgeTheme,0,"判断题分",true,true);
				map.AddTBInt(PaperExamRandomAttr.CentOfEssayQuestion,0,"问答题分",true,true);
				map.AddTBInt(PaperExamRandomAttr.CentOfRC,0,"阅读理解题分",true,true);
				map.AddTBInt(PaperExamRandomAttr.CentOfSum,0,"总分",true,true);
				map.AddTBInt(PaperExamRandomAttr.CentOfSum,0,"总分",true,true);


				map.AddDDLEntities(PaperExamAttr.FK_Level,"0","等级",new Levels(),false);
				map.AddTBDateTime(PaperExamRandomAttr.FromDateTime,"考时从",true,true);
				map.AddTBDateTime(PaperExamRandomAttr.ToDateTime,"到",true,true);
				map.AddTBInt(PaperExamRandomAttr.MM,0,"用时(分钟)",true,false);

				//map.AttrsOfSearch.AddFromTo("日期从",PaperExamRandomAttr.FromDateTime,DateTime.Now.AddDays(-30).ToString(DataType.SysDataFormat) , DataType.CurrentData,8);
				map.AddSearchAttr(ExamAttr.FK_Paper);
				map.AddSearchAttr(ExamAttr.FK_Dept);
				map.AddSearchAttr(ExamAttr.ExamState);
				map.AddSearchAttr(PaperExamAttr.FK_Level);
				 
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		#region 构造方法
		/// <summary>
		/// 试卷答案
		/// </summary> 
		public Exam()
		{
		}
		#endregion 

		 

		 

	}
	/// <summary>
	///  试卷答案
	/// </summary>
	public class Exams :EntitiesOID
	{
		/// <summary>
		/// Exams
		/// </summary>
		public Exams(){}
		/// <summary>
		/// Exam
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Exam();
			}
		}
	}
}
