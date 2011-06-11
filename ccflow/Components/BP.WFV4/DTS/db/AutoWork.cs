using System;
using System.Data;
using System.Collections;
using BP.En;
using BP.En.Base;
using BP.DA;
using BP.DTS;
using BP.Pub;
using BP.Tax;
using BP.Web;
using BP.WF;


namespace BP.WF
{
	/// <summary>
	/// 自动工作调度
	/// </summary>
	public class AutoWork:DataIOEn
	{
		public static bool IsDoToday
		{
			get
			{
				string val= System.Web.HttpContext.Current.Application["Today"] as string ;
				if ( val== System.DateTime.Now.ToString("yyyyMMdd") )
				{
					return true;
				}
				else
				{
					System.Web.HttpContext.Current.Application["Today"]=System.DateTime.Now.ToString("yyyyMMdd");
					return false;
				}
			}
		}
		/// <summary>
		/// 自动工作调度
		/// </summary>
		public AutoWork()
		{
			this.HisDoType=DoType.Especial;
			this.Title="自动工作调度";
			this.HisRunTimeType=RunTimeType.Day;
			this.HisUserType = Web.UserType.AppAdmin;
			this.DefaultEveryMonth="00";
			this.DefaultEveryDay="00";
			this.DefaultEveryHH="10";
			this.DefaultEveryMin="01";
		}
		/// <summary>
		/// 自动工作调度
		/// </summary>
		public override void Do()
		{
			if (AutoWork.IsDoToday )
				return ;

			//Emp myemp = new Emp(WebUser.No);
			Log.DebugWriteInfo("调度工作自动开始**************");

			//调度有关中间节点的数据(比如非正常户认定的最后一个节点)
			ArrayList als = DA.ClassFactory.GetObjects("BP.WF.PCWorks");
			foreach(PCWorks wks in als)
				wks.DoInitData();

			//调度有关第一个节点的数据（比如催缴流程的第一个节点）
			als = DA.ClassFactory.GetObjects("BP.WF.PCStartWorks");
			//催缴流程：
			foreach(PCStartWorks wks in als)
				wks.AutoGenerWorkFlow();

			als = DA.ClassFactory.GetObjects("BP.WF.PCTaxpayerStartWorks");
			foreach(PCStartWorks wks in als)
				wks.AutoGenerWorkFlow();

			Log.DebugWriteInfo("调度工作自动结束**************");
			//TaxUser.SignInOfWFQH(myemp,false,true);
		}
	}
		 
}
