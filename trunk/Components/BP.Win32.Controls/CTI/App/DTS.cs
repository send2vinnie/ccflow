using System;
using System.Data;
using BP.DTS;
using BP.DA;


namespace BP.CTI
{
	public class Bank:DataIOEn2
	{
		public Bank()
		{
			this.HisDoType=DoType.UnName;
			this.Title="银行存款余额不足";
			this.HisRunTimeType=RunTimeType.UnName;
			this.FromDBUrl=DBUrlType.AppCenterDSN;
			this.ToDBUrl=DBUrlType.AppCenterDSN;
		}
		public override void Do()
		{
			TelDTS dts = new TelDTS();
			BP.SystemConfig.AppSettings["Tax_WSB"]=BP.SystemConfig.AppSettings["Tax_YEBZ"];
			dts.Do();
		}
	}
	/// <summary>
	/// 临沂用友未未申报纳税人
	/// </summary>
	public class TelDTSOfLinYi:DataIOEn
	{
		public TelDTSOfLinYi()
		{
			this.HisDoType=DoType.UnName;
			this.Title="(临沂地税)电话崔交";
			this.HisRunTimeType=RunTimeType.UnName;
			this.FromDBUrl=DBUrlType.AppCenterDSN;
			this.ToDBUrl=DBUrlType.AppCenterDSN;
		}
		public override void Do()
		{
			TelDTS dts = new TelDTS();
			BP.SystemConfig.AppSettings["Tax_WSB"]=BP.SystemConfig.AppSettings["Tax_YEBZ"];
			dts.Do();
		}
	}


	/// <summary>
	/// 流程统计
	/// </summary>
	public class TelDTS:DataIOEn
	{
		/// <summary>
		/// 流程统计
		/// </summary>
		public TelDTS()
		{
			this.HisDoType=DoType.UnName;
			if (SystemConfig.SysNo !="CTISRVOfLinYiTax" )
			{
				this.Title="欠费数据";
			}
			else
			{
				this.Title="(临沂地税)未申报数据";
			}
			this.HisRunTimeType=RunTimeType.UnName;
			this.FromDBUrl=DBUrlType.AppCenterDSN;
			this.ToDBUrl=DBUrlType.AppCenterDSN;
		}
		public override void Do()
		{
			#region 增加测试记录.在更新之前.
			if (Sys.SysConfigs.GetValByKeyBoolen("CallTestTelEveryDay",true))
			{
				BP.CTI.App.TelTests tests1 = new BP.CTI.App.TelTests();
				tests1.InitCallList();
			}
			#endregion

			// 得到当前的调度，在调度系统中的编号。
			string dtsNo=this.GetNoInDTS();
			string  CurrScheduleId=DataType.CurrentMonth+ "_" + dtsNo  ; // 取出当前  月份+"_"+调度编号  
			string  ConfigScheduleId=Sys.SysConfigs.GetValByKey("CurrentScheduleID", DataType.CurrentMonth+"_0" );

			if ( CurrScheduleId != ConfigScheduleId)
			{
				/* 如果两个ID 不相等就说明，第一次开始使用此调度。 备份数据 */
				string  bakTable="CTI_CallList_"+ConfigScheduleId ;
				DBAccess.RunSQLDropTable(bakTable); 
				DBAccess.RunSQL("SELECT * into "+bakTable+" FROM CTI_CallList"); //bak it.
				DBAccess.RunSQL("DELETE CTI_CallList"); //.清除当前的呼出.
				Sys.SysConfigs.SetValByKey("CurrentScheduleID", CurrScheduleId); //设置当前的为
			}
			else
			{
				/* 说明上一次已经执行过此 dts.不做任何处理。 */
			}

			  
			try
			{
				Log.DefaultLogWriteLineInfo("****** Start OF "+SystemConfig.CustomerNo+" Tel DTS 结束执行取数据");
				switch (SystemConfig.CustomerNo)
				{
					case CustomerNoList.LYTax:
					case CustomerNoList.YSDS:
					case CustomerNoList.DS371301:
					case CustomerNoList.DS371306:
					case CustomerNoList.DS371307:
					case CustomerNoList.DS371311:
					case CustomerNoList.DS371312:
					case CustomerNoList.DS371321:
					case CustomerNoList.DS371323:
					case CustomerNoList.DS371324:
					case CustomerNoList.DS371325:
					case CustomerNoList.DS371326:
					case CustomerNoList.DS371327:
					case CustomerNoList.DS371328:
					case CustomerNoList.DS371329:
						WSB ws= new WSB();
						ws.Do(); // 形成为申报数据.						
						//DBAccess.RunSP("ProCTI_WillCall");      //此存储过程，把申报表里面的信息导入到呼出列表里面。．
						DBAccess.RunSP("ProCTI_Tax_NotDeclare");      //此存储过程，把申报表里面的信息导入到呼出列表里面。．
						break;
					case CustomerNoList.YSGS:
						DBAccess.RunSQL("DELETE CTI_WillCall"); // 删除申报表。
						DBAccess.RunSQL( BP.SystemConfig.AppSettings["Tax_WSB"] ); //取出来要更新的数据。
						break;
					case CustomerNoList.YSNet: //网通公司
						this.DoOfYSNet();
						break;
					default:
						throw new Exception("CustomerNoList errory");
				}
				Log.DefaultLogWriteLineInfo("****** END OF "+SystemConfig.CustomerNo+" Tel DTS 结束执行取数据");

			}
			catch (Exception ex)
			{
				Log.DefaultLogWriteLineError("******  "+SystemConfig.CustomerNo+" Tel DTS 调度失败"+ex.Message );
				throw new Exception(ex.Message);

				/* 如果调度失败如何处理? */
				int  val=Sys.SysConfigs.GetValByKeyInt("WhenDTSError",3);	
				if (val==3)
				{
					/* 保持原来的状态不处理. */
					return;
				}
				else
				{
					/* 设置卡的工作状态. */
					Sys.SysConfigs.SetValByKey("CardWorkState",val);
				}
			}

			#region 增加测试记录.在更新之后
			if (Sys.SysConfigs.GetValByKeyBoolen("CallTestTelEveryDay",true))
			{
				/* 如果允许呼出测试数据. */
				BP.CTI.App.TelTests tests = new BP.CTI.App.TelTests();
				tests.InitCallList();
			}
			#endregion
		}
		/// <summary>
		/// 流程统计
		/// </summary>
		public void DoOfYSNet()
		{
			
			Log.DefaultLogWriteLineInfo("****** START OF TelDTS 开始执行取数据");
			// 判断是否调度过来了数据.
			int i =DBAccess.RunSQLReturnValInt("select count(*) from ywxttbl");
			if (i==0)
			{
				/*说明调度包没有得到数据*/
				Log.DefaultLogWriteLineError("调度包没有得到数据,系统执行了暂停...");
				Card.DoPause();
				return;
			}

			#region  判断数据是否变化．
			/* 如果两个数值不相等．*/
			DBAccess.RunSP("ProCTIData"); // 运新此存储过程．
			#endregion

			Log.DefaultLogWriteLineInfo("****** END OF TelDTS 结束执行取数据");
		}
	 
		public  void DoOfTax()
		{
			Log.DefaultLogWriteLineInfo("****** START OF Tel DTS 开始执行取数据");

			WSB wsb = new WSB();
			wsb.Do();

			// 生成cti_temp 临时表 	
			DBAccess.RunSQLDropTable("CTI_Temp");
			// 在这里面有可能有两个企业留下一个电话号码的。
			DBAccess.RunSQL("SELECT DISTINCT Tel  INTO CTI_Temp from Tax_WSB where NY='"+DataType.CurrentYearMonth+"'");
			 
			/* 如果两个数值不相等．*/
			try
			{
				DBAccess.RunSP("ProCTIData_Tax"); // 运新此存储过程．
			}
			catch(Exception ex)
			{
				Log.DefaultLogWriteLineError("run ProCTIData_Tax"+ex.Message);
			}
			Log.DefaultLogWriteLineInfo("****** END OF Tel DTS 结束执行取数据");
		}

		/// <summary>
		/// 产生未申报纳税人
		/// </summary>
		public class WSB :DataIOEn
		{
			public WSB()
			{
				this.HisDoType=DoType.UnName;
				this.Title="产生未申报纳税人";
				this.HisRunTimeType= RunTimeType.Month;
				this.FromDBUrl=DBUrlType.DBAccessOfOracle9i;
				this.ToDBUrl=DBUrlType.AppCenterDSN;
				this.Note="需要每月的执行一次，执行的结果可以供分局人员查询。";
			}
			/// <summary>
			/// 执行
			/// </summary>
			public override void Do()
			{
				//this.Directly( "SELECT  QYBM AS NO ,  JKJJ   from DSBM.DJSW  where city_num='"+SystemConfigOfTax.FK_ZSJG+"' ","DS_TaxpayerJJLX");  // 生成企业临时表。

				//return;

				string sql="";
			 
				DBAccess.RunSQL("DELETE  CTI_WillCall"); // 删除原来的申报数据.
				//DBAccess.RunSQL("DELETE  CTI_WillCall"); // 删除原来的申报数据.

				int thisYear=DateTime.Now.Year;
				string thisDay=DateTime.Now.ToString("yyyyMMdd");

				#region 查询个体户
				switch(DateTime.Now.Month)
				{
					case 1:
						sql="SELECT qybm No, TRIM(QYMC) Name, TRIM(QYDH) Tel   FROM DSBM.DJSW WHERE   FZCH<>'02' and  qybm in ( select qybm from dsbm.shd where ( B_YEAR='"+thisYear+"'  AND city_num = '"+SystemConfigOfTax.FK_ZSJG+"' )  union select qybm from ds"+SystemConfigOfTax.FK_ZSJG+".ynszjd ) ";
						break;
					case 4:
					case 10:
						sql="SELECT qybm No, TRIM(QYMC) Name, TRIM(QYDH) Tel   FROM DSBM.DJSW WHERE   FZCH<>'02' and  qybm in ( select qybm from dsbm.shd where ( B_YEAR='"+thisYear+"'  AND city_num = '"+SystemConfigOfTax.FK_ZSJG+"' )  and ( SBFS  LIKE  '%A%' OR SBFS  LIKE  '%C%' ) union select qybm from ds"+SystemConfigOfTax.FK_ZSJG+".ynszjd WHERE JNQX='01' OR JNQX='02' ) ";
						break;
					case 7:
						sql="SELECT qybm No, TRIM(QYMC) Name, TRIM(QYDH) Tel   FROM DSBM.DJSW WHERE   FZCH<>'02' and  qybm in ( select qybm from dsbm.shd where ( B_YEAR='"+thisYear+"'  AND city_num = '"+SystemConfigOfTax.FK_ZSJG+"' )  and ( SBFS  Not like '%E%' ) union select qybm from ds"+SystemConfigOfTax.FK_ZSJG+".ynszjd WHERE JNQX<>'03' ) ";
						break;			 
					default:
						sql="SELECT qybm No, TRIM(QYMC) Name, TRIM(QYDH) Tel   FROM DSBM.DJSW WHERE   FZCH<>'02' and  qybm in (select qybm from dsbm.shd where B_YEAR='"+thisYear+"' AND SBFS like '%A%' AND city_num = '"+SystemConfigOfTax.FK_ZSJG+"' union select qybm from ds"+SystemConfigOfTax.FK_ZSJG+".ynszjd where JNQX='01' ) ";
						break;
				}
				this.FromDBUrl=DBUrlType.DBAccessOfOracle9i;
				this.Directly(sql,"Tax_TempAll_GT");  // 生成个体临时表。

				//DBAccess.RunSQL("update Tax_TempAll set NY='"+DataType.CurrentYearMonth+"'" ); // 删除当前的.

				sql="SELECT QYBM   FROM DSBM.DJSWTY  WHERE city_num ='"+SystemConfigOfTax.FK_ZSJG+"' AND  TO_CHAR(TY_BEGIN,'YYYYMMDD')<='"+DateTime.Now.ToString("yyyyMM")+"01'   AND  TO_CHAR(  TY_END, 'YYYYMMDD' )  >=  '"+DateTime.Now.ToString("yyyyMM")+"20'  ";
				this.Directly(sql,"Tax_Temp1");

				sql="SELECT REGIS_NUM   No FROM DS000000.SZZ88 WHERE city_num = '"+SystemConfigOfTax.FK_ZSJG+"' AND TO_CHAR(INQU_DATE,'YYYYMM')='"+DateTime.Now.ToString("yyyyMM")+"' AND WASTE_FLAG<>'1'";
				this.Directly(sql,"Tax_Temp2");

				DBAccess.RunSQL("DELETE  Tax_TempAll_GT WHERE No in (SELECT * from Tax_Temp1 ) ");
				DBAccess.RunSQL("DELETE  Tax_TempAll_GT WHERE No in (SELECT * from Tax_Temp2 ) ");

				// 删除当前月份的数据。
				//DBAccess.RunSQL("DELETE Tax_WSB WHERE NY='"+DataType.CurrentYearMonth+"' ");
				// 把产生的记录插入里面。
				DBAccess.RunSQL("INSERT INTO CTI_WillCall  SELECT No,  '"+DataType.CurrentYearMonth+"' as NY ,Name, Tel , 1 as FK_TelType, 1000 AS JE FROM Tax_TempAll_GT");

				//DBAccess.RunSQL("INSERT INTO Tax_WSB (No, Name, Tel, TaxpayerType ) SELECT No, NY,Name, Tel, 1 as TaxpayerType  FROM Tax_TempAll");
				#endregion

				#region  企业
				// 开始查询企业的
				switch(DateTime.Now.Month)
				{
					case 1: // 按年，按月份。
						sql="SELECT qybm No, TRIM(QYMC) Name, TRIM(QYDH) Tel FROM DSBM.DJSW WHERE city_num like '"+SystemConfigOfTax.FK_ZSJG+"' AND   FZCH<>'02' AND QYBM IN (SELECT QYBM FROM DSBM.YNSZJD ) ";
						break;
					case 4:
					case 10:
						sql="SELECT qybm No, TRIM(QYMC) Name, TRIM(QYDH) Tel FROM DSBM.DJSW WHERE city_num like '"+SystemConfigOfTax.FK_ZSJG+"' AND  FZCH<>'02' AND QYBM IN (SELECT QYBM FROM DSBM.YNSZJD WHERE JNQX='01' or JNQX='02') ";
						break;
					case 7:
						sql="SELECT qybm No, TRIM(QYMC) Name, TRIM(QYDH) Tel FROM DSBM.DJSW WHERE city_num = '"+SystemConfigOfTax.FK_ZSJG+"' AND  FZCH<>'02' AND QYBM IN (SELECT QYBM FROM DSBM.YNSZJD WHERE JNQX='01' OR JNQX='04') ";
						break;			 
					default:
						sql="SELECT qybm as No, TRIM(QYMC) as Name, TRIM(QYDH) as Tel FROM DSBM.DJSW WHERE city_num='"+SystemConfigOfTax.FK_ZSJG+"' AND  FZCH<>'02' AND QYBM IN (SELECT QYBM FROM DSBM.YNSZJD WHERE JNQX='01') ";
						break;
				}

				this.Directly(sql,"Tax_TempAll_QY");  // 生成企业临时表。
 
				DBAccess.RunSQL("DELETE  Tax_TempAll_QY WHERE No in (select * from Tax_Temp1 ) ");
				DBAccess.RunSQL("DELETE  Tax_TempAll_QY WHERE No in (select * from Tax_Temp2 ) ");
				DBAccess.RunSQL("DELETE  Tax_TempAll_QY WHERE No in (select No from CTI_WillCall  ) ");

				// 把产生的记录插入里面。
				DBAccess.RunSQL("INSERT INTO CTI_WillCall  SELECT No, '"+DataType.CurrentYearMonth+"' as NY,Name, Tel , 2 as FK_TelType, 1000 AS JE FROM Tax_TempAll_QY");
				#endregion

				this.Directly( "SELECT  QYBM AS NO ,  JKJJ   from DSBM.DJSW  where city_num='"+SystemConfigOfTax.FK_ZSJG+"' ","DS_TaxpayerJJLX");  // 生成企业临时表。


				


			}
		}

	}

}

	 
 
