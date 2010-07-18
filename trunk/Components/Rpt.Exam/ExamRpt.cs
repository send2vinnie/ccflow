using System;

using BP.Tax;
using System.Collections;
using BP.DA;
using System.Data;
using BP.Sys;
using BP.En;
using BP.Rpt;
using BP.Web;
using BP.GTS;
 


namespace BP.GTS
{
	/// <summary>
	/// 纳税人统计报表
	/// </summary>
	public class ExamRpt : Rpt3D
	{
		/// <summary>
		/// 构造
		/// </summary>
		public ExamRpt()
		{
			#region 报表的基本属性
			this.HisEns = new PaperExamExts();
			this.Title="试卷分析";
			this.DataProperty="分数";
			this.IsShowRate=true; //是否显示百分比。
			this.IsShowSum=true;  //是否显示合计。

			//this.HisAnalyseObjs.AddAnalyseObj("个数量化分析","COUNT(*)");

			this.HisAnalyseObjs.AddAnalyseObj("总分","SUM("+PaperExamAttr.CentOfSum+")", AnalyseDataType.AppMoney);
			this.HisAnalyseObjs.AddAnalyseObj("平均分","AVG("+PaperExamAttr.CentOfSum+")", AnalyseDataType.AppMoney);

			this.HisAnalyseObjs.AddAnalyseObj("单选题总分","SUM("+PaperExamAttr.CentOfChoseOne+")", AnalyseDataType.AppMoney);
			this.HisAnalyseObjs.AddAnalyseObj("单选题平均分","AVG("+PaperExamAttr.CentOfChoseOne+")", AnalyseDataType.AppMoney);
			
			
			this.HisAnalyseObjs.AddAnalyseObj("多选题总分","SUM("+PaperExamAttr.CentOfChoseM+")", AnalyseDataType.AppMoney);
			this.HisAnalyseObjs.AddAnalyseObj("多选题平均分","AVG("+PaperExamAttr.CentOfChoseM+")", AnalyseDataType.AppMoney);

			
			this.HisAnalyseObjs.AddAnalyseObj("填空题总分","SUM("+PaperExamAttr.CentOfFillBlank+")", AnalyseDataType.AppMoney);
			this.HisAnalyseObjs.AddAnalyseObj("填空题平均分","AVG("+PaperExamAttr.CentOfFillBlank+")", AnalyseDataType.AppMoney);

			this.HisAnalyseObjs.AddAnalyseObj("判断题总分","SUM("+PaperExamAttr.CentOfJudgeTheme+")", AnalyseDataType.AppMoney);
			this.HisAnalyseObjs.AddAnalyseObj("判断题平均分","AVG("+PaperExamAttr.CentOfJudgeTheme+")", AnalyseDataType.AppMoney);


			this.HisAnalyseObjs.AddAnalyseObj("问答题总分","SUM("+PaperExamAttr.CentOfEssayQuestion+")", AnalyseDataType.AppMoney);
			this.HisAnalyseObjs.AddAnalyseObj("问答题平均分","AVG("+PaperExamAttr.CentOfEssayQuestion+")", AnalyseDataType.AppMoney);
			#endregion

			#region 设置普同字段查询条件
			this.IsShowSearchKey=false;
			//	this.HisAttrsOfSearch.AddFromTo("记录日期",PaperExamAttr.FromDateTime, DateTime.Now.Year+"-01-01", DA.DataType.CurrentData, 7 );
			#endregion

			#region 设置外键查询条件
			this.AddFKSearchAttrs(PaperExamAttr.FK_Dept);
			this.AddFKSearchAttrs(PaperExamAttr.FK_Paper);
			this.AddFKSearchAttrs(PaperExamAttr.FK_Level);
			#endregion

			#region 设置纬度属性
			this.AddDAttrByKey(PaperExamAttr.FK_Dept);
			this.AddDAttrByKey(PaperExamAttr.FK_Emp);
			this.AddDAttrByKey(PaperExamAttr.FK_Paper);
			this.AddDAttrByKey(PaperExamAttr.FK_Level);
			#endregion

			#region 设置默认的纬度属性， 让用户进入就可以使用它.
			this.AttrOfD1=PaperExamAttr.FK_Paper;
			this.AttrOfD2=PaperExamAttr.FK_Emp;
			this.AttrOfD3=PaperExamAttr.FK_Emp;
			#endregion
		} 
	}
}
