using System;

using BP.Port;
using System.Collections;
using BP.DA;
using System.Data;
using BP.Sys;
using BP.En;
using BP.Rpt;
using BP.Web;
using BP.WF;


namespace BP.WF
{
	/// <summary>
	/// 纳税人统计报表
	/// </summary>
	public class CHOfNodeRpt : Rpt3D
	{
		/// <summary>
		/// 构造
		/// </summary>
        public CHOfNodeRpt()
        {
            #region 报表的基本属性

            this.HisEns = new BP.WF.CHOfNodes();
            this.Title = "工作分析";
            this.DataProperty = "工作个数";
            this.IsShowRate = true;  //是否显示百分比。
            this.IsShowSum = false;  //是否显示合计。

            //this.HisAnalyseObjs.AddAnalyseObj("个数量化分析","COUNT(*)");
            this.HisAnalyseObjs.AddAnalyseObj("总体考核统计", "SUM(Cent)", AnalyseDataType.AppMoney);
            this.HisAnalyseObjs.AddAnalyseObj("时效扣分统计", "SUM(CentOfCut)", AnalyseDataType.AppMoney);
            this.HisAnalyseObjs.AddAnalyseObj("工作量得分统计", "SUM(CentOfAdd)", AnalyseDataType.AppMoney);
            this.HisAnalyseObjs.AddAnalyseObj("质量考核统计", "SUM(CentOfQU)", AnalyseDataType.AppMoney);
            //量化得分统计
            //this.HisAnalyseObjs.AddAnalyseObj("总体考核统计","SUM(Cent)+SUM(CentOfCut)+SUM(CentOfQU)");
            // 设置 计算列。default  COUNT(*)
            //this.OperationColumn="AVG(ZCZJ)";
            #endregion

            #region 设置普同字段查询条件
            this.IsShowSearchKey = false;
            //this.HisAttrsOfSearch.AddFromTo("日期",CHOfNodeAttr.RDT, DateTime.Now.AddMonths(-12).ToString("yyyy-MM-dd"), DA.DataType.CurrentData, 7 );
            #endregion

            #region 设置外键查询条件
            this.AddFKSearchAttrs(CHOfNodeAttr.FK_Dept);
            this.AddFKSearchAttrs(CHOfNodeAttr.FK_NY);
            this.AddFKSearchAttrs(CHOfNodeAttr.FK_Emp);
            #endregion

            #region 设置纬度属性
            this.AddDAttrByKey(CHOfNodeAttr.FK_Flow);

            
                this.AddDAttrByKey(CHOfNodeAttr.FK_Dept);

            this.AddDAttrByKey(CHOfNodeAttr.FK_Station);
            this.AddDAttrByKey(CHOfNodeAttr.FK_NY);
            //this.AddDAttrByKey(CHOfNodeAttr.FK_AP);
            this.AddDAttrByKey(CHOfNodeAttr.FK_Dept);
            this.AddDAttrByKey(CHOfNodeAttr.FK_Emp);
            #endregion

            #region 设置默认的纬度属性， 让用户进入就可以使用它.
            this.AttrOfD1 = CHOfNodeAttr.FK_NY;
          
                this.AttrOfD2 = CHOfNodeAttr.FK_Dept;
                this.AttrOfD3 = CHOfNodeAttr.FK_Dept;
            #endregion

        } 
	}
}
