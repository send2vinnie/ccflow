using System;

using BP.Tax;
using System.Collections;
using BP.DA;
using System.Data;
using BP.Sys;
using BP.En.Base;
using BP.Rpt;
using BP.Web;
using BP.WF;


namespace BP.WF
{
	/// <summary>
	/// 纳税人统计报表
	/// </summary>
	public class BookRpt : Rpt3D
	{
		/// <summary>
		/// 构造
		/// </summary>
		public BookRpt()
		{
			#region 报表的基本属性

			this.HisEns = new BP.WF.Books();
			this.Title="文书分析";
			this.DataProperty="文书";
			this.IsShowRate=true;  //是否显示百分比。
			this.IsShowSum=false;  //是否显示合计。

			//this.HisAnalyseObjs.AddAnalyseObj("个数量化分析","COUNT(*)");
			//this.HisAnalyseObjs.AddAnalyseObj("总体考核统计","SUM(Cent)", AnalyseDataType.AppMoney);
			//this.HisAnalyseObjs.AddAnalyseObj("时效扣分统计","SUM(CentOfCut)", AnalyseDataType.AppMoney);
			//this.HisAnalyseObjs.AddAnalyseObj("工作量得分统计","SUM(CentOfAdd)", AnalyseDataType.AppMoney);
			//this.HisAnalyseObjs.AddAnalyseObj("质量考核统计","SUM(CentOfQU)", AnalyseDataType.AppMoney);
			//量化得分统计
			//this.HisAnalyseObjs.AddAnalyseObj("总体考核统计","SUM(Cent)+SUM(CentOfCut)+SUM(CentOfQU)");
			// 设置 计算列。default  COUNT(*)
			//this.OperationColumn="AVG(ZCZJ)";
			#endregion

			#region 设置普同字段查询条件
			this.IsShowSearchKey=false;
			//this.HisAttrsOfSearch.AddFromTo("日期",BookAttr.RDT, DateTime.Now.AddMonths(-12).ToString("yyyy-MM-dd"), DA.DataType.CurrentData, 7 );
			#endregion

			#region 设置外键查询条件
			if (WebUser.IsSJUser)
				this.AddFKSearchAttrs(BookAttr.FK_XJ);
			else
				this.AddFKSearchAttrs(BookAttr.FK_Dept);

			this.AddFKSearchAttrs(BookAttr.BookState);

			#endregion

			#region 设置纬度属性
			//this.AddDAttrByKey(BookAttr.FK_Flow);
			this.AddDAttrByKey(BookAttr.FK_Dept);


//			if (WebUser.IsSJUser)
//				this.AddDAttrByKey(BookAttr.FK_XJ);
//			else
//				this.AddDAttrByKey(BookAttr.FK_Dept);

			//this.AddDAttrByKey(BookAttr.FK_Node);
			this.AddDAttrByKey(BookAttr.FK_NY);
			this.AddDAttrByKey(BookAttr.FK_NodeRefFunc);
			this.AddDAttrByKey(BookAttr.Recorder);
			#endregion

			#region 设置默认的纬度属性， 让用户进入就可以使用它.
			this.AttrOfD1=BookAttr.FK_NY;
			if (WebUser.IsSJUser)
			{
				this.AttrOfD2=BookAttr.FK_XJ;
				this.AttrOfD3= BookAttr.FK_XJ;
			}
			else
			{
				//this.AddFKSearchAttrs(BookAttr.FK_Emp);
				//this.AddFKSearchAttrs(BookAttr.FK_Emp);
				this.AttrOfD2=BookAttr.FK_Dept;
				this.AttrOfD3= BookAttr.FK_Dept;
			}
			#endregion

		} 
	}
}
