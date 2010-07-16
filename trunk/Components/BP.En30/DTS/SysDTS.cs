using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading;
using System.Collections;
using BP.En;
using BP.DA;

namespace BP.DTS
{
	
	/// <summary>
	/// 调度包明细属性
	/// </summary>
	public class DTSAttr :EntityNoNameAttr
	{
		/// <summary>
		/// 编号
		/// </summary>
		public const string FK_PackNo="FK_PackNo";
		/// <summary>
		/// 运行类型
		/// </summary>
		public const string RunType="RunType";
		/// <summary>
		/// 运行文本(sql,方法)
		/// </summary>
		public const string RunText="RunText";
		/// <summary>
		/// 运行频率
		/// </summary>
		public const string RunTimeType="RunTimeType";
		/// <summary>
		/// 每月
		/// </summary>
		public const string EveryMonth="EveryMonth";
		/// <summary>
		/// 每季度
		/// </summary>
		public const string EveryQuarter="EveryQuarter";
		/// <summary>
		/// 每日
		/// </summary>
		public const string EveryDay="EveryDay";
		/// <summary>
		/// 每时
		/// </summary>
		public const string EveryHour="EveryHour";
		/// <summary>
		/// 每分
		/// </summary>
		public const string EveryMinute="EveryMinute";
		/// <summary>
		/// 备注
		/// </summary>
		public const string Note="Note";
		/// <summary>
		/// 执行日志
		/// </summary>
		public const string RunLog="RunLog";
		public const string FK_Sort="FK_Sort";

	}
	/// <summary>
	/// 调度包明细
	/// </summary>
	public class SysDTS : EntityNoName 
	{

		#region 基本属性
		/// <summary>
		/// 运行类型
		/// </summary>
		public RunType RunTypeOfEnum
		{
			get
			{
				return (RunType)this.GetValIntByKey(DTSAttr.RunType);
			}
			set
			{
				this.SetValByKey(DTSAttr.RunType,(int)value);
			}
		}
		/// <summary>
		/// 频率
		/// </summary>
		public  RunTimeType  RunTimeType_del
		{
			get
			{
				return (RunTimeType)this.GetValIntByKey(DTSAttr.RunTimeType);
			}
			set
			{
				this.SetValByKey(DTSAttr.RunTimeType,(int)value);
			}	
		}
		/// <summary>
		/// 频率
		/// </summary>
		public  BP.DTS.RunTimeType  RunTimeTypeOfEnum_del
		{
			get
			{
				return (BP.DTS.RunTimeType)this.GetValIntByKey(DTSAttr.RunTimeType);
			}
			set
			{
				this.SetValByKey(DTSAttr.RunTimeType,(int)value);
			}	
		}
		/// <summary>
		/// 运行时间类型
		/// </summary>
		public string RunTimeTypeText
		{
			get
			{
				return this.GetValRefTextByKey(DTSAttr.RunTimeType);
			}
		}
		/// <summary>
		/// 所做工作
		/// </summary>
		public  string  RunText
		{
			get
			{
				return this.GetValStringByKey(DTSAttr.RunText);
			}
			set
			{
				this.SetValByKey(DTSAttr.RunText,value);
			}	
		}
		/// <summary>
		/// 备注
		/// </summary>
		public  string  Note
		{
			get
			{
				return this.GetValStringByKey(DTSAttr.Note);
			}
			set
			{
				this.SetValByKey(DTSAttr.Note,value);
			}	
		}
		public  string  FK_Sort
		{
			get
			{
				return this.GetValStringByKey(DTSAttr.FK_Sort);
			}
			set
			{
				this.SetValByKey(DTSAttr.FK_Sort,value);
			}	
		}
		/// <summary>
		/// log
		/// </summary>
		public  string  RunLog
		{
			get
			{
				return this.GetValStringByKey(DTSAttr.RunLog);
			}
			set
			{
				this.SetValByKey(DTSAttr.RunLog,value);
			}	
		}		
		/// <summary>
		/// EveryMonth
		/// </summary>
		public  string  EveryMonth
		{
			get
			{
				return this.GetValStringByKey(DTSAttr.EveryMonth);
			}
			set
			{
				this.SetValByKey(DTSAttr.EveryMonth,value);
			}	
		}
		/// <summary>
		/// EveryDay
		/// </summary>
		public  string  EveryDay
		{
			get
			{
				return this.GetValStringByKey(DTSAttr.EveryDay);
			}
			set
			{
				this.SetValByKey(DTSAttr.EveryDay,value);
			}	
		}
		/// <summary>
		/// EveryHour
		/// </summary>
		public  string  EveryHour
		{
			get
			{
				return this.GetValStringByKey(DTSAttr.EveryHour);
			}
			set
			{
				this.SetValByKey(DTSAttr.EveryHour,value);
			}	
		}	
		/// <summary>
		/// EveryMinute
		/// </summary>
		public  string  EveryMinute
		{
			get
			{
				return this.GetValStringByKey(DTSAttr.EveryMinute);
			}
			set
			{
				this.SetValByKey(DTSAttr.EveryMinute,value);
			}	
		}	
		#endregion 

		#region 构造方法
		/// <summary>
		/// 一个任务
		/// </summary>
		public SysDTS()
		{
		}
		/// <summary>
		/// 任务
		/// </summary>
		/// <param name="_No">编号</param>
		public SysDTS( string _No ) :base(_No)
		{
		}
		/// <summary>
		/// Map
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) return this._enMap;
				Map map = new Map("Sys_DTS");
				map.EnDesc="调度明细";
				map.EnType=EnType.Sys; //实体类型，admin 系统管理员表，PowerAble 权限管理表,也是用户表,你要想把它加入权限管理里面请在这里设置。。
				map.CodeStruct ="2";
				map.IsAllowRepeatNo=false;
				//map.IsAllowRepeatName=false;

				map.AddTBStringPK(DTSAttr.No,null,"任务编号",true,true,3,3,3);
				map.AddTBString(DTSAttr.Name,null,"名称",true,false,1,2000,20);
				map.AddDDLEntities(DTSAttr.FK_Sort,"0","类别",new Sys.SysDTSSorts(),false);
				map.AddTBStringDoc(DTSAttr.Note,null,"备注",true,false);
				map.AddTBString(DTSAttr.EveryMonth,"00","月",true,false,2,200,4);
				map.AddTBString(DTSAttr.EveryDay,"00","日",true,false,2,200,4);
				map.AddTBString(DTSAttr.EveryHour,"00","时",true,false,2,200,4);
				map.AddTBString(DTSAttr.EveryMinute,"00","分",true,false,2,200,4);
				map.AddTBString(DTSAttr.RunText,null,"所做工作",true,false,1,3000,20);
				map.AddTBStringDoc(DTSAttr.RunLog,null,"日志",true,false);
				map.AddDDLSysEnum(DTSAttr.RunType,0,"任务类型",true,false);

				this._enMap=map;
				return this._enMap; 
			}
		}
		protected override bool beforeUpdateInsertAction()
		{
			return base.beforeUpdateInsertAction ();
		}
		#endregion 

		#region 执行任务
		/// <summary>
		/// 当前时间能否运行.
		/// </summary>
		public bool IsCanRun
		{
			get
			{
				string MM=DateTime.Now.ToString("MM"); 
				string dd=DateTime.Now.ToString("dd");
				string HH=DateTime.Now.ToString("HH");
				string min=DateTime.Now.ToString("mm");

				if (this.EveryMonth!="00")				 
					if (this.EveryMonth.IndexOf(MM)==-1)
						return false;

				if (this.EveryDay!="00")				 
					if (this.EveryDay.IndexOf(dd)==-1)
						return false;

				if (this.EveryHour!="00")				 
					if (this.EveryHour.IndexOf(HH)==-1)
						return false;

				if (this.EveryMinute!="00")				 
					if (this.EveryMinute.IndexOf(min)==-1)
						return false;

				return true;
			}
		}
		public Thread thisThread = null;
		/// <summary>
		/// 执行任务(Execute(),通过线程状态)
		/// </summary>
		public void Run()
		{
			if (this.IsCanRun==false)
				return ;

			if( thisThread==null)
			{
				ThreadStart ts =new ThreadStart( this.Execute )  ; 
				thisThread = new Thread(ts);		 
				thisThread.Start();
			}
			else
			{
				if( thisThread.ThreadState == System.Threading.ThreadState.Running )
				{
					BP.DA.Log.DefaultLogWriteLine(BP.DA.LogType.Info,  "@在上一个线程["+this.No+"]没有执行完就开始做下次调度.系统处理的是返回。" ) ;
					return ;  // 如果正在运行， 就返回。					 
				}
				else
				{
					thisThread.Abort();
					thisThread = new Thread(new ThreadStart( this.Execute ));
					thisThread.Start();
				}
			}
		}		 
		/// <summary>
		/// 执行任务(0  中间层,1  SQL文本,2  SQL存储过程)
		/// </summary>
		public void Execute()
		{
			string log="\n开始执行时间:"+DataType.CurrentDataTimess;
			string task = "TaskNo["+this.No+"],TaskName["+this.Name+"],RunType["+this.RunTypeOfEnum.ToString()+"], RunText["+this.RunText+"].";
			//BP.DA.Log.DefaultLogWriteLine(BP.DA.LogType.Info,  task ) ;
			try
			{
				switch( this.RunTypeOfEnum )
				{
					case RunType.Method : // 中间层方法 
						this.ExecClassMethod();
						break;
					case RunType.SQL: // SQL文本
						this.ExecSqlText();
						break;
					case RunType.SP: // SQL存储过程
						this.ExecSqlStoredPro();
						break;
					case RunType.DataIO: // SQL存储过程
						DataIOEn ioen =(DataIOEn)BP.DA.ClassFactory.GetDataIOEn(this.RunText);
						ioen.Do();
						break;
					default :
						task = "任务类型不正确！\r\n" + task;
						BP.DA.Log.DefaultLogWriteLine(BP.DA.LogType.Warning,  task ) ;
						throw new Exception(task);
				}
				//task = "执行完毕！\r\n*****TaskInfo:" + task;
				BP.DA.Log.DefaultLogWriteLineInfo( task );
			}
			catch(Exception ex)
			{
				string err = "发生错误！\r\n*****TaskInfo:" + task + "\r\n*****错误消息："+ex.Message ;
				BP.DA.Log.DefaultLogWriteLineInfo(  err ) ;
				throw new Exception("@执行调度:"+this.Name+"出现如下错误:"+ex.Message);
			}
			log= log+ "结束执行时间"+DataType.CurrentDataTimess;

			//this.RunLog=log+this.RunLog.Substring(0,450);
			this.Update("RunLog", log );
		}
		#endregion 执行任务

		#region 任务执行方法
		/// <summary>
		/// 任务执行方法(中间层方法)
		/// </summary>
		private void ExecClassMethod()
		{
			/*
			switch(this.No)
			{
				case "001":	  // 开业登记					 
//					BP.DA.Log.DefaultLogWriteLine(BP.DA.LogType.Info,  "开始执行"+this.Name ) ;
//					BP.WF.WFDTS.TransferAutoGenerWorkBreed( new BP.WF.Breed("1000"));
//					BP.DA.Log.DefaultLogWriteLine(BP.DA.LogType.Info,  "结束执行"+this.Name ) ;
					break;
				case "002": // 获取外部属性。
//					BP.DA.Log.DefaultLogWriteLine(BP.DA.LogType.Info,  "开始执行"+this.Name ) ;
//					BP.WF.WFDTS.DTSPCWork();
//					BP.DA.Log.DefaultLogWriteLine(BP.DA.LogType.Info,  "结束执行"+this.Name ) ;
					break;
				case "003": // 考核
					BP.DA.Log.DefaultLogWriteLine(BP.DA.LogType.Info,  "开始执行"+this.Name ) ;
					//BP.WF.WFDTS.InitCHOfStation();
					BP.DA.Log.DefaultLogWriteLine(BP.DA.LogType.Info,  "结束执行"+this.Name ) ;
					break;
				case "004": // 统计
					BP.DA.Log.DefaultLogWriteLine(BP.DA.LogType.Info,  "开始执行"+this.Name ) ;
					//BP.WF.WFDTS.InitBreedStat() ;
					BP.DA.Log.DefaultLogWriteLine(BP.DA.LogType.Info,  "结束执行"+this.Name ) ;
					break;
				default:
					throw new Exception("@没有处理的 dts method .");
			}
*/

			 
			string str =this.RunText.Trim(' ',';','.');
			int pos = str.LastIndexOf(".");
			string clas = str.Substring(0,pos);
			string meth = str.Substring(pos,str.Length-pos).Trim('.',' ','(',')');
			object obj =  DA.ClassFactory.GetEn( clas );
			if(obj==null)
				throw new Exception("创建对象["+clas+"]实例失败！");
			Type tp = obj.GetType();
			MethodInfo mp = tp.GetMethod( meth );
			if(mp==null)
				throw new Exception("@对象实例["+tp.FullName+"]中没有找到方法["+meth+"]！");
			mp.Invoke( obj ,null ); //调用由此 MethodInfo 实例反射的方法或构造函数。

		}
		/// <summary>
		/// 任务执行方法(SQL文本)
		/// </summary>
		private void ExecSqlText()
		{
			DA.DBAccess.RunSQL(this.RunText) ;			 
		}
		
		/// <summary>
		/// 任务执行方法(SQL存储过程)
		/// </summary>
		private void ExecSqlStoredPro()
		{
			DA.DBAccess.RunSP(this.RunText);
			return ;

			/*
			SqlConnection conn2 = new SqlConnection((DA.DBAccess.GetAppCenterDBConn as SqlConnection).ConnectionString);
			try
			{
				conn2.Open();
				string sql = this.RunText.Trim();
				DA.DBAccess.RunSQL( sql ,conn2 ,CommandType.StoredProcedure );
			}
			catch(Exception ex)
			{
				conn2.Close();
				conn2.Dispose();
				throw new Exception(ex.Message);
			}
			finally
			{
				conn2.Close();
				conn2.Dispose();
			}
			*/
		}
		#endregion 任务执行方法

		#region 查询
		/// <summary>
		/// 按照执行的内容查询．
		/// </summary>
		/// <param name="runText"></param>
		/// <returns></returns>
		public int RetrieveByRunText(string runText)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(DTSAttr.RunText,runText);
			return qo.DoQuery();
		}
		#endregion
	}
	/// <summary>
	/// 调度包明细集构造函数
	/// </summary> 
	public class SysDTSs : EntitiesNoName
	{
		/// <summary>
		/// 这个方法是把系统中编写的 DataIOEn 实体．没有加入到调度中的加入进去．
		/// 包调度中不存在的删除．
		/// </summary>
		public static void InitDataIOEns()
		{
			//baseEnsName
			ArrayList al =BP.DA.ClassFactory.GetObjects("BP.DTS.DataIOEn");
			//int i=1;
			// 初始化系统实体。
            foreach (DataIOEn en in al)
            {
                SysDTS dts = new SysDTS();
                int ii = (int)en.HisRunTimeType;
                if (dts.RetrieveByRunText(en.ToString()) == 1)
                    continue;
                dts.Name = en.Title;
                dts.RunTypeOfEnum = RunType.DataIO;
                //dts.RunTimeType=en.HisRunTimeType;
                dts.RunText = en.ToString();
                //dts.RunTimeTypeOfEnum=en.HisRunTimeType;

                dts.Note = en.Note + "@日期" + BP.DA.DataType.CurrentDataTimess;
                dts.EveryDay = en.DefaultEveryDay;
                dts.EveryHour = en.DefaultEveryHH;
                dts.EveryMinute = en.DefaultEveryMin;
                dts.EveryMonth = en.DefaultEveryMonth;
                dts.FK_Sort = en.FK_Sort;
                dts.No = dts.GenerNewNo;
                dts.Save();
            }
		}

		#region  tick

		
	  
		/// <summary>
		/// 获取当前任务（执行SQL,绑定调度包任务明细）
		/// </summary>
		public static SysDTSs GetNowTasks_del()
		{
			SysDTSs dts =new SysDTSs();
			try
			{
				DateTime dt = DateTime.Now;
				string month = dt.ToString("MM");
				string dd = dt.ToString("dd");
				string HH = dt.ToString("HH"); // 24 小时制。
				string mm = dt.ToString("mm");
				/*WF_AttemperPack调度包,WF_DTS调度包明细*/
				string sql="SELECT No FROM Sys_DTS WHERE (RunTimeType=3 and EveryMonth  LIKE '%@month%' and EveryDay  LIKE '%@dd%' and EveryHour LIKE '%@hh%' and EveryMinute='%@mm%') OR (RunTimeType=2 and EveryDay  LIKE '%@dd%' and EveryHour LIKE '%@HH%' and EveryMinute='%@mm%') OR (RunTimeType=1 and EveryHour LIKE '%@HH%' and EveryMinute LIKE'%@mm%') OR ( RunTimeType=0 and EveryMinute like '%@mm%')";
				sql=sql.Replace("@dd",dd);
				sql=sql.Replace("@mm",mm);
				sql=sql.Replace("@month",month);
				sql=sql.Replace("@HH",HH);

				DA.Log.DebugWriteInfo(" dts run sql="+sql);
				DataTable table = DA.DBAccess.RunSQLReturnTable(sql);
				foreach(DataRow dr in table.Rows)
				{
					SysDTS en = new SysDTS();
					en.No = dr["No"].ToString();
					dts.AddEntity(en);
				}
				return dts;
			}
			catch(Exception ex)
			{
				string error="GetNowTasks()  "+ ex.Message;
				DA.Log.DefaultLogWriteLine(DA.LogType.Error, error);
				throw new Exception(error);			
			}
		}
		/// <summary>
		/// 执行任务
		/// </summary>
		public string Run()
		{
			string strs="";
			foreach(SysDTS en in this)
			{
				try
				{
					en.Run();
				}
				catch(Exception ex)
				{
					strs+="@在批量运行DTS "+en.Name+"出现了异常."+ex.Message;
					BP.DA.Log.DefaultLogWriteLine(BP.DA.LogType.Error,strs);
				}
			}
			return strs;
		}
		#endregion

		/// <summary>
		/// 调度包明细集构造函数
		/// </summary>
		public SysDTSs()
		{
		}		
		/// <summary>
		/// 调度包明细集构造函数
		/// </summary>
		public new SysDTS this[int index]
		{
			get
			{
				return base[index] as SysDTS;
			}
		}
		/// <summary>
		/// 调度包明细集构造函数
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new SysDTS();
			}
		}
	}
}

