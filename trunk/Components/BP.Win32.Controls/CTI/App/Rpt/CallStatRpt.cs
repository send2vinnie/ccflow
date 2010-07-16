
using System;

using BP.Tax;
using System.Collections;
using BP.DA;
using System.Data;
using BP.Sys;
using BP.En.Base;
using BP.Rpt;
using BP.Web;
 


namespace BP.CTI.App
{
	/// <summary>
	/// 纳税人统计报表
	/// </summary>
	public class CallStatRpt : Rpt3D
	{
		/// <summary>
		/// 构造
		/// </summary>
		public CallStatRpt()
		{
			#region 报表的基本属性
			this.HisEns = new BP.CTI.App.CallStats();
			this.Title="呼叫统计分析";
			this.DataProperty="个数";
			this.IsShowRate=true; //是否显示百分比。
			this.IsShowSum=true;  //是否显示合计。
			// 设置 计算列。default  COUNT(*)
			//this.OperationColumn="AVG(ZCZJ)";
			#endregion

			#region 设置普同字段查询条件
			this.IsShowSearchKey=false;
			#endregion

			#region 设置外键查询条件
			//this.AddFKSearchAttrs(CallStatAttr.FK_Dept);
			this.AddFKSearchAttrs(CallStatAttr.CallDate);
			this.AddFKSearchAttrs(CallStatAttr.FK_TelType);
			#endregion

			#region 设置纬度属性
			this.AddDAttrByKey(CallStatAttr.CallDate);
			this.AddDAttrByKey(CallStatAttr.FK_TelType);
			#endregion

			#region 设置默认的纬度属性， 让用户进入就可以使用它.
			this.AttrOfD1=CallStatAttr.CallDate;
			this.AttrOfD2=CallStatAttr.FK_TelType;
			#endregion
		} 
		 
	}
}
