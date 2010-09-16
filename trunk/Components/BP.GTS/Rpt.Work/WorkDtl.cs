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
	public class WorkDtlAttr:EntityNoAttr
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
	public class WorkDtl :EntityNo
	{
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

				Map map = new Map("V_GTS_WorkDtl");
				map.EnDesc="作业";
				map.CodeStruct="5";
				map.EnType= EnType.View;

				map.AddTBStringPK(WorkDtlAttr.No,null,"编号",true,false,1,30,20);
				map.AddDDLEntities(WorkDtlAttr.FK_Work,null,"作业",new WorkExts(),false);
				map.AddDDLEntities(WorkDtlAttr.FK_Emp,Web.WebUser.No,"学生",new Emps(),false);
                map.AddDDLEntities(WorkDtlAttr.FK_Dept, null, "部门", new BP.Port.Depts(), false);

				map.AddTBInt(WorkDtlAttr.CentOfChoseOne,0,"单选题",true,true);
				map.AddTBInt(WorkDtlAttr.CentOfChoseM,0,"多选题",true,true);
				map.AddTBInt(WorkDtlAttr.CentOfFillBlank,0,"填空题",true,true);
				map.AddTBInt(WorkDtlAttr.CentOfJudgeTheme,0,"判断题",true,true);
				map.AddTBInt(WorkDtlAttr.CentOfEssayQuestion,0,"问答题",true,true);
				map.AddTBInt(WorkDtlAttr.CentOfRC,0,"阅读理解题",true,true);
				map.AddTBInt(WorkDtlAttr.CentOfSum,0,"总分",true,true);
				map.AddTBDecimal(WorkDtlAttr.RightRate,0,"正确率%",true,true);
				map.AddDDLEntities(WorkDtlAttr.FK_Level,"1","成绩等级",new Levels(),false);

				map.AddSearchAttr(WorkDtlAttr.FK_Work);
				map.AddSearchAttr(WorkDtlAttr.FK_Dept);
				map.AddSearchAttr(WorkDtlAttr.FK_Level);
			 
				this._enMap=map;
				return this._enMap;
			}
		}
	}
	/// <summary>
	///  试卷答案
	/// </summary>
	public class WorkDtls :EntitiesNo
	{
		/// <summary>
		/// WorkDtls
		/// </summary>
		public WorkDtls(){}
		/// <summary>
		/// WorkDtl
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkDtl();
			}
		}
	}
}
