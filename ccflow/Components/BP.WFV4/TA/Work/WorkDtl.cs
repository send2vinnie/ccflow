using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.En;
using BP.Web;
using BP.Port;
 

namespace BP.TA
{
	/// <summary>
	/// 工作状态
	/// </summary>
	public enum WDS
	{
		/// <summary>
		/// 未完成
		/// </summary>
		UnComplete,
		/// <summary>
		/// 审核中
		/// </summary>
		Checking,
		/// <summary>
		/// 处理完成
		/// </summary>
		Over
	}
	/// <summary>
	/// 工作明晰状态.
	/// </summary>
	public enum WorkDtlState
	{
		/// <summary>
		/// 未阅读 0 
		/// </summary>
		UnRead,
		/// <summary>
		/// 已阅读 1
		/// </summary>
		Read,
		/// <summary>
		/// 执行回复工作中 2
		/// </summary>
		DoReing,
		/// <summary>
		/// 执行退回工作中 3
		/// </summary>
		DoReturning,
		/// <summary>
		/// 以回复方式结束 4
		/// </summary>
		OverByRe,
		/// <summary>
		/// 以退回的方式结束 5
		/// </summary>
		OverByReturn,
		/// <summary>
		/// 读取的方式结束 6 
		/// </summary>
		OverByRead
	} 
	/// <summary>
	/// 工作明细属性
	/// </summary>
	public class WorkDtlAttr:WorkDtlBaseAttr
	{
		/// <summary>
		/// 工作状态明细
		/// </summary>
		public const string WorkDtlState="WorkDtlState";
		/// <summary>
		/// 工作状态
		/// </summary>
		public const string WDS="WDS";
		/// <summary>
		/// 发送日期
		/// </summary>
		public const string DTOfSend="DTOfSend";
		/// <summary>
		/// 时间跨度
		/// </summary>
		public const string SpanDays="SpanDays";
		/// <summary>
		/// 考核成绩
		/// </summary>
		public const string Cent="Cent";
	}
	/// <summary>
	/// 工作明细
	/// </summary> 
	public class WorkDtl :WorkDtlBase
	{
		#region 扩展属性
		public int TimeOutDays
		{
			get
			{
				Work wk = this.HisWork ; 
				return DataType.GetSpanDays(this.DTOfActive,wk.DTOfEnd);
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public int SpanDays
		{
			get
			{
				return DataType.GetSpanDays(this.DTOfSend, this.DTOfActive);
			}
		}
		/// <summary>
		/// 它的回复节点
		/// </summary>
		public new ReWork HisReWork
		{
            get
            {
                ReWork nd = new ReWork();
                nd.OID = this.OID;
                nd.ParentID = this.ParentID;
                if (nd.RetrieveFromDBSources() == 0)
                {
                    nd.InsertAsOID(this.OID);
                }
                else
                {
                    return nd;
                }

                Work wk = this.HisWork;
                nd.Title = "答复:" + wk.Title;
                nd.Doc = "您好" + wk.SenderText + ": \n\n  您的《" + wk.Title + "》 现处理如下: \n  1、\n  2、     \n\n " + WebUser.Name + "\n" + DataType.CurrentDataTimeCN;
                nd.Executer = this.Executer;
                nd.DTOfActive = DataType.CurrentDataTime;
                nd.Sender = wk.Sender;
                nd.Update();
                return nd;
            }
		}
		/// <summary>
		/// 它的退回节点
		/// </summary>
		public new ReturnWork HisReturnWork
		{
			get
			{
				ReturnWork nd = new ReturnWork();
				nd.OID=this.OID;
				if (nd.RetrieveFromDBSources()==0 )
				{
					nd.ParentID=this.ParentID;
					nd.Executer=this.Executer;
					nd.Sender=this.Sender;
					nd.DTOfActive=DataType.CurrentDataTime;
					//nd.MyReturnWorkState=ReturnWorkState.None; 
					//	nd.Update();
					nd.InsertAsOID(this.OID);
					return nd;
				}
				else
				{
					return nd;
				}
			}
		}
		/// <summary>
		/// 它的父节点
		/// </summary>
		public new Work HisWork
		{
			get
			{
				Work wk  =new Work(this.ParentID);
				return wk;
			}
		}
		#endregion 扩展属性

		#region 运算属性
		public bool IsOver
		{
			get
			{
				if (this.WorkDtlState==BP.TA.WorkDtlState.OverByRe 
					|| this.WorkDtlState==BP.TA.WorkDtlState.OverByRead
					|| this.WorkDtlState==BP.TA.WorkDtlState.OverByReturn)
					return true;
				else
					return false;
			}
		}
		#endregion

		#region 基本属性
		
		 
		/// <summary>
		/// 考核成绩
		/// </summary>
		public int Cent
		{
			get
			{
				return this.GetValIntByKey(WorkDtlAttr.Cent);
			}
			set
			{
				SetValByKey(WorkDtlAttr.Cent,value);
			}
		}
		public string CentText
		{
			get
			{
				if (this.HisWork.MyCheckWay==CheckWay.UnSet)
					return "NULL";
				else
					return this.Cent.ToString();
			}
		}
		 
		/// <summary>
		/// 状态
		/// </summary>
		public WorkDtlState WorkDtlState
		{
			get
			{
				return (BP.TA.WorkDtlState)this.GetValIntByKey(WorkDtlAttr.WorkDtlState); 
			}
			set
			{
				SetValByKey(WorkDtlAttr.WorkDtlState,(int)value);
			}
		}
		/// <summary>
		/// 工作状态
		/// </summary>
		public WDS WDS
		{
			get
			{
				return (BP.TA.WDS)this.GetValIntByKey(WorkDtlAttr.WDS); 
			}
			set
			{
				SetValByKey(WorkDtlAttr.WDS,(int)value);
			}
		}

		/// <summary>
		/// 状态
		/// </summary>
		public string WorkDtlStateText
		{
			get
			{
				return this.GetValRefTextByKey(WorkDtlAttr.WorkDtlState);
			}
		}
		/// <summary>
		/// 工作状态
		/// </summary>
		public string WDSText
		{
			get
			{
				return this.GetValRefTextByKey(WorkDtlAttr.WDS);
			}
		}
		public string WorkDtlStateImg
		{
			get
			{
                return "<img src='./Images/" + this.WorkDtlState.ToString() + ".gif' border=0 />";
			}
		}
		
		/// <summary>
		/// 发送时间
		/// </summary>
		public string DTOfSend
		{
			get
			{
				return this.GetValAppDateByKey(WorkDtlAttr.DTOfSend); 
			}
			set
			{
				SetValByKey(WorkDtlAttr.DTOfSend,value);
			}
		}
		public DateTime DTOfSend_S
		{
			get
			{
				return DataType.ParseSysDateTime2DateTime(this.DTOfSend);
			}
		}
		#endregion
 
		#region 构造函数
		/// <summary>
		/// 工作明晰
		/// </summary>
		public WorkDtl()
		{
		}
		/// <summary>
		/// 工作明晰
		/// </summary>
		/// <param name="_No">No</param>
		public WorkDtl(int oid):base(oid)
		{
		}
		/// <summary>
		/// Map
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;

				Map map = new Map("TA_WorkDtl");
				map.EnDesc="工作明晰";
                map.Icon = "../TA/Images/WorkDtl_s.gif";
				map.AddTBIntPKOID();
				map.AddTBInt(WorkDtlAttr.ParentID, 0, "父节点ID",true,false);
                map.AddDDLSysEnum(WorkDtlAttr.WorkDtlState, 0, "工作状态明细", false, false, "WorkDtlState", "@0=未阅读@1=已阅读@2=执行回复工作中@3=执行退回工作中@4=以回复方式结束@5=以退回的方式结束@6=读取的方式结束");
                map.AddDDLSysEnum(WorkDtlAttr.WDS, 0, "工作状态", false, false,  "WDS","@0=未完成@1=审核中@2=处理完成");

				//map.AddDDLEntities(WorkDtlAttr.FK_Work,0,DataType.AppInt,"工作", new Works(), WorkAttr.OID, WorkAttr.Title,false);
				map.AddDDLEntities(WorkDtlAttr.Executer,WebUser.No,"执行人",new Emps(),false);
				map.AddDDLEntities(WorkDtlAttr.Sender,WebUser.No,"发送行人",new Emps(),false);

				map.AddTBDateTime(WorkDtlAttr.DTOfActive,"活动时间",true,true);
				map.AddTBDateTime(WorkDtlAttr.DTOfSend,"发送时间",true,true);

				map.AddTBInt(WorkDtlAttr.SpanDays,0,"时间跨度",true,false);
				map.AddTBInt(WorkDtlAttr.Cent,0,"考核成绩",true,false);
				map.AddTBInt(WorkDtlAttr.AdjunctNum,0,"附件个数",true,true);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		#region 回复
		/// <summary>
		/// 执行不认可退回
		/// </summary>
		public void DoUnRatifyReWork()
		{
			ReWork rn =this.HisReWork;
            rn.ReWorkState = ReWorkState.UnRatify;
            rn.Update();
           
			this.WorkDtlState = WorkDtlState.UnRead;
			this.Update();
			//this.GenerCheckCent(); // 产生考核分
		}
		/// <summary>
		/// 执行认可回复
		/// </summary>
		public void DoRatifyReWork()
		{
			ReWork rn =this.HisReWork;
			rn.Update( ReWorkAttr.ReWorkState,(int)ReWorkState.Ratify );
			this.WorkDtlState = WorkDtlState.OverByRe;
			this.Update();
			this.GenerCheckCent(); // 产生考核分
            this.HisWork.DoSettleAccounts(); // 结帐。
		}
		/// <summary>
		/// 撤消回复
		/// </summary>
		public void DoTakeBackRe()
		{
			ReWork rn =this.HisReWork;
			rn.Update( ReWorkAttr.ReWorkState,(int)ReWorkState.None);
			//this.Update( WorkAttr.WorkState,(int)WorkState.Read ); // 设置为已经退回状态。
			this.WorkDtlState = WorkDtlState.Read;
			this.Update();
			this.GenerCheckCent(); //产生考核
		}
		#endregion

		#region 退回
		/// <summary>
		/// 执行不认可退回
		/// </summary>
		public void DoUnRatifyReturnWork()
		{
			ReturnWork rn =this.HisReturnWork;
			rn.Update( ReturnWorkAttr.ReturnWorkState,(int)ReturnWorkState.UnRatify, ReturnWorkAttr.DTOfActive,DataType.CurrentDataTime );
			
			this.Update(WorkDtlAttr.WorkDtlState, BP.TA.WorkDtlState.UnRead);
		}
		 
		/// <summary>
		/// 执行认可退回(工作完成。)
		/// </summary>
		public void DoRatifyReturnWork()
		{
			ReturnWork rn =this.HisReturnWork;
			rn.Update( ReturnWorkAttr.ReturnWorkState,(int)ReturnWorkState.Ratify,ReturnWorkAttr.DTOfActive,DataType.CurrentDataTime );
			this.Update( WorkDtlAttr.WorkDtlState,(int)WorkDtlState.OverByReturn ); // 设置为已经退回状态。
			
			this.GenerCheckCent(); //设置考核问题。
			this.HisWork.DoSettleAccounts(); // 结帐。
		}
		#endregion

		#region  公共方法
		/// <summary>
		/// 执行签收
		/// </summary>
		public void DoRead()
		{
			if (this.WorkDtlState==BP.TA.WorkDtlState.UnRead)
			{

				if (this.HisWork.IsRe)
				{
					this.Update( WorkDtlAttr.WorkDtlState,(int)WorkDtlState.Read);
				}
				else
				{
					this.Update( WorkDtlAttr.WorkDtlState,(int)WorkDtlState.OverByRead );
				}
			}


		
		}
		/// <summary>
		/// 执行收回任务
		/// </summary>
        public void DoTakeBack()
        {
            Work wk = this.HisWork;
            switch (this.WorkDtlState)
            {
                case WorkDtlState.UnRead:
                    this.Delete();
                    break;
                case WorkDtlState.Read:
                    this.Delete();

                    ReWork re = new ReWork();
                    re.Delete(ReWorkAttr.ParentID, this.ParentID);

                    ReturnWork ret = new ReturnWork();
                    ret.Delete(ReturnWorkAttr.ParentID, this.ParentID);
                    break;
                default:
                    throw new Exception("工作已经被执行下去了，您不能在收回它。");
            }

            WorkDtls dtls = new WorkDtls();
            dtls.Retrieve(WorkDtlAttr.ParentID, this.ParentID);

            if (dtls.Count == 0)
                wk.Delete();
        }
		/// <summary>
		/// 执行退回
		/// </summary>
		/// <param name="reason">退回原因</param>
		public ReturnWork DoReturn(string reason)
		{
			ReturnWork nd = this.HisReturnWork;

			nd.OID=this.OID;
			nd.ReturnReason = reason;
			nd.DTOfActive=DataType.CurrentDataTime;
			nd.ReturnWorkState = ReturnWorkState.Sending;
			nd.AdjunctNum=nd.HisSysFileManagers.Count;
			nd.Update();
			this.Update(WorkDtlAttr.WorkDtlState,(int)WorkDtlState.DoReturning );  // 设置当前的状态为退回。
			return nd;
		}
		/// <summary>
		/// 执行撤消退回
		/// </summary>
		public void DoReturnOfRecall()
		{
			/* 执行撤消退回 */
			ReturnWork rn = this.HisReturnWork;			 
			rn.Update(ReturnWorkAttr.ReturnWorkState,(int)BP.TA.ReturnWorkState.None  );
			this.Update(WorkDtlAttr.WorkDtlState,(int)BP.TA.WorkDtlState.DoReturning ); 
			//设置工作节点为已经阅读
		}
		/// <summary>
		/// 执行回复
		/// </summary>
		public void DoRe(string title,string Doc)
		{
			ReWork rn = this.HisReWork;
			rn.Title=title;
			rn.Doc=Doc;
			rn.DTOfActive=DataType.CurrentDataTime;
			rn.ReWorkState=BP.TA.ReWorkState.Sending;
			rn.Update();


			this.WorkDtlState = BP.TA.WorkDtlState.DoReing;
			this.AdjunctNum = rn.HisSysFileManagers.Count;
			this.Update();  
		}
		#endregion

		#region 关于考核
		public void GenerCheckCent()
		{
			Work wk=this.HisWork;

			// 如果没有设置考核就未设置。
			if (wk.MyCheckWay==CheckWay.UnSet)
				return;

			this.Cent=0;
			// 生成扣分。
			switch(this.WorkDtlState)
			{
				case BP.TA.WorkDtlState.OverByRe: //以回复的形式结束了次工作。
					if (wk.MyCheckWay==CheckWay.ByDays)
					{
						int cent=-this.TimeOutDays*wk.CentOfPerDay;
						if (cent >0)
						{
							if (cent>wk.CentOfMax)
								cent=wk.CentOfMax;
						}
						this.Cent=cent; /* 如果是按照天来考核。*/
					}
					else if (wk.MyCheckWay==CheckWay.ByOnce )
					{
						if (this.TimeOutDays >=1 )
						{
							/*如果延期就扣分*/
							this.Cent=-wk.CentOfOnce;
						}
						else
						{
							this.Cent = wk.CentOfOnce ;
						}
					}
					break;
				case BP.TA.WorkDtlState.OverByReturn: //以退回的形式结束了次工作。
					this.Cent=0; // 没有处理这件工作，它的得分就为0。 
					break;
				default:
					throw new Exception("工作还没有结束，不能进行节点考核。");
			}
			this.Update();

			wk.DoResetWorkState(); // 执行状态设置。			
			
		}
		#endregion

		#region 重写方法
		protected override bool beforeUpdateInsertAction()
		{
			this.DTOfActive = DataType.CurrentDataTime;

			switch(this.WorkDtlState)
			{
				case BP.TA.WorkDtlState.Read:
				case BP.TA.WorkDtlState.UnRead:
					this.WDS=BP.TA.WDS.UnComplete;
					break;
				case BP.TA.WorkDtlState.DoReing:
				case BP.TA.WorkDtlState.DoReturning:
					this.WDS=BP.TA.WDS.Checking;
					break;
				case BP.TA.WorkDtlState.OverByRe:
				case BP.TA.WorkDtlState.OverByRead:
				case BP.TA.WorkDtlState.OverByReturn:
					this.WDS=BP.TA.WDS.Over;
					break;
				default:
					throw new Exception("no sumch casde.");
			}
			return base.beforeUpdateInsertAction ();
		}

		#endregion

		
	}
	/// <summary>
	/// 工作明细s
	/// </summary> 
	public class WorkDtls: Entities
	{
		/// <summary>
		/// 获取entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkDtl();
			}
		}
		public override int RetrieveAll()
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkDtlAttr.WorkDtlState, WebUser.No);
			qo.addOrderBy(WorkDtlAttr.Executer);
			return qo.DoQuery();
		}
		/// <summary>
		/// 计算个数
		/// </summary>
		/// <param name="key"></param>
		/// <param name="val"></param>
		/// <returns></returns>
		public int StatCOUNT(string key,string val)
		{
			Entity en = this.GetNewEntity;
			string sql="SELECT COUNT(*) FROM "+en.EnMap.PhysicsTable+" WHERE "+en.EnMap.GetFieldByKey(key)+" ='"+val+"'";
			return int.Parse( en.RunSQLReturnTable(sql).Rows[0][0].ToString()) ;
		}
		public int StatCOUNT(string key1,string val1, string key2,string val2 )
		{
			Entity en = this.GetNewEntity;
			string sql="SELECT COUNT(*) FROM "+en.EnMap.PhysicsTable+" WHERE "+en.EnMap.GetFieldByKey(key1)+" ='"+val1+"' AND "+en.EnMap.GetFieldByKey(key2)+" ='"+val2+"'";
			return int.Parse( en.RunSQLReturnTable(sql).Rows[0][0].ToString()) ;
		}
		/// <summary>
		/// WorkDtls
		/// </summary>
		public WorkDtls()
		{
		}
		/// <summary>
		/// WorkDtls
		/// </summary>
		public WorkDtls(WorkDtlState wds)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkDtlAttr.WorkDtlState, (int)wds);
			qo.DoQuery();
		}
		public WorkDtls(string empNo, string ny)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkDtlAttr.Executer, empNo);
			qo.addAnd();
			qo.AddWhere(WorkDtlAttr.DTOfSend, " LIKE ", ny+"%" );
			qo.DoQuery();
		}
		/// <summary>
		/// 我正在处理的工作
		/// </summary>
		/// <returns></returns>
        public int QueryPending()
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(WorkDtlAttr.Executer, WebUser.No);
            qo.addAnd();
            qo.AddWhere(WorkDtlAttr.WorkDtlState, "<=", 1);
            qo.addOrderByDesc(WorkDtlAttr.WorkDtlState);
            return qo.DoQuery();
        }

        public int QueryHistory()
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(WorkDtlAttr.Executer, WebUser.No);
            qo.addAnd();
            qo.AddWhere(WorkDtlAttr.WorkDtlState, ">=", 4);
            qo.addOrderByDesc(WorkDtlAttr.WorkDtlState);
            return qo.DoQuery();
        }

        public int QueryRuning()
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(WorkDtlAttr.Executer, WebUser.No);
            qo.addAnd();
            qo.AddWhereIn(WorkDtlAttr.WorkDtlState, " ('2','3')");
            qo.addOrderByDesc(WorkDtlAttr.WorkDtlState);
            return qo.DoQuery();
        }
	}
}
 