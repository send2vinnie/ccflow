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
	public class PaperExamExtAttr:EntityNoAttr
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
		/// <summary>
		/// 是否是考试
		/// </summary>
		public const string IsExam="IsExam";
	}
 
	/// <summary>
	/// 试卷答案
	/// </summary>
	public class PaperExamExt :EntityNo
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
		/// 重写基类方法
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				
				Map map = new Map("V_GTS_PaperExam");
				map.EnDesc="考卷";
				map.CodeStruct="5";
				map.EnType= EnType.View;
				map.AddTBStringPK(PaperExamAttr.No,null,"编号",false,false,1,30,10);
				map.AddDDLSysEnum(PaperExamAttr.ExamState,0,"状态",true,false);
				map.AddDDLEntities(PaperExamAttr.FK_Paper,null,"试卷",new PaperExts(),false);
				map.AddDDLEntities(PaperExamAttr.FK_Emp,Web.WebUser.No,"学生",new Emps(),false);
                map.AddDDLEntities(PaperExamAttr.FK_Dept, null, "部门", new BP.Port.Depts(), false);

				map.AddTBDecimal(PaperExamAttr.CentOfChoseOne,0,"单选题",true,true);
                map.AddTBDecimal(PaperExamAttr.CentOfChoseM, 0, "多选题", true, true);
                map.AddTBDecimal(PaperExamAttr.CentOfFillBlank, 0, "填空题", true, true);
                map.AddTBDecimal(PaperExamAttr.CentOfJudgeTheme, 0, "判断题", true, true);
                map.AddTBDecimal(PaperExamAttr.CentOfEssayQuestion, 0, "问答题", true, true);
                map.AddTBDecimal(PaperExamAttr.CentOfRC, 0, "阅读理解题", true, true);
                map.AddTBDecimal(PaperExamAttr.CentOfSum, 0, "总分", true, true);
				map.AddTBDecimal(PaperExamAttr.RightRate,0,"正确率%",true,true);
				map.AddDDLEntities(PaperExamAttr.FK_Level,"1","成绩等级",new Levels(),false);

				map.AddSearchAttr(PaperExamAttr.FK_Paper);
				map.AddSearchAttr(PaperExamAttr.FK_Dept);
				map.AddSearchAttr(PaperExamAttr.ExamState);
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
		public PaperExamExt()
		{
		}
		#endregion 

	}
	/// <summary>
	///  试卷答案
	/// </summary>
	public class PaperExamExts :EntitiesNo
	{
		/// <summary>
		/// PaperExamExts
		/// </summary>
		public PaperExamExts(){}
		/// <summary>
		/// PaperExamExt
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperExamExt();
			}
		}
	}
}
