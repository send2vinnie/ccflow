using System;
using System.Data;
using BP.DA ; 
using System.Collections;
using BP.En.Base;
using BP.WF;
using BP.Port ; 
using BP.En;
using BP.DTS;

namespace BP.WF.DTS
{
	/// <summary>
	/// 流程时效考核
	/// </summary>
	public class InitCHOfNode : DataIOEn
	{
		/// <summary>
		/// 流程时效考核
		/// </summary>
		public InitCHOfNode()
		{
			this.HisDoType=DoType.UnName;
			this.Title="节点时效考核(汇总节点工作信息，对预期的工作追加责任人，并计算考核分数.)";
			this.HisRunTimeType=RunTimeType.UnName;
			this.FromDBUrl=DBUrlType.AppCenterDSN;
			this.ToDBUrl=DBUrlType.AppCenterDSN;
		}
		/// <summary>
		/// 流程时效考核
		/// </summary>
        public override void Do()
        {
            WFDTS.InitCHOfNode();

            WFDTS.InitOne2More();
        }
	}
	/// <summary>
	/// 流程调度
	/// </summary>
	public class WFDTS
	{
        /// <summary>
        /// 为了处理，工作预期过错责任的问题。
        /// </summary>
        /// <returns></returns>
        public static string InitOne2More()
        {
            string sql = "";

            #region /* 查询出还没做完但是逾期的情况 */
            sql = "SELECT WorkID, FK_Node,EMPS, COUNT(*) AS NUM FROM WF_CHOFNODE WHERE NodeState=0 AND SUBSTR(SDT,0,11) < '" + DataType.CurrentData + "' GROUP BY WORKID,FK_NODE,EMPS";
            DataTable dt_no = DBAccess.RunSQLReturnTable(sql);
            foreach (DataRow dr_no in dt_no.Rows)
            {
                int num = int.Parse(dr_no["Num"].ToString());
                if (num > 1)
                {
                    /*说明这条数据已加工过了。*/
                    continue;
                }
                string myemps = dr_no["Emps"].ToString();
                string WorkID = dr_no["WorkID"].ToString();
                int fk_node = int.Parse(dr_no["FK_Node"].ToString());

                // 找到其中的一个Entity.
                CHOfNode cn = new CHOfNode();
                int i = cn.Retrieve(CHOfNodeAttr.WorkId, WorkID, CHOfNodeAttr.FK_Node, fk_node);
                if (i == 0)
                    throw new Exception("不可能出现的情况。");

                Node nd = new Node(fk_node);

                string[] emps = myemps.Split(',');
                foreach (string emp in emps)
                {
                    if (emp == "" || emp == null)
                        continue;

                    if (emp == cn.FK_Emp)
                    {
                        /*如果是已经存在人记录。*/
                        continue;
                    }

                    CHOfNode mycn = new CHOfNode();
                    mycn.Copy(cn);
                    mycn.NodeState = 0;
                    mycn.FK_Emp = emp;
                    mycn.CDT = "无";
                    mycn.CentOfAdd = 0;
                    mycn.CentOfCut = nd.DeductCent;
                    mycn.Cent = 0;
                    mycn.Save();
                }
                /*执行扣分*/
                DBAccess.RunSQL("UPDATE WF_CHofNode SET CentOfCut=" + nd.DeductCent + " WHERE FK_Node=" + nd.NodeID + " and workid='" + WorkID + "'");
            }
            #endregion

            #region /* 查询出已经做完但是逾期的情况 */
            sql = "SELECT WorkID, FK_Node,EMPS, COUNT(*) AS NUM FROM WF_CHOFNODE WHERE NodeState=1 AND SUBSTR(SDT,0,11) < '" + DataType.CurrentData + "' GROUP BY WORKID,FK_NODE,EMPS";
            DataTable dt_yes = DBAccess.RunSQLReturnTable(sql);
            foreach (DataRow dr_yes in dt_yes.Rows)
            {
                int num = int.Parse(dr_yes["Num"].ToString());
                if (num > 1)
                {
                    /*说明这条数据已加工过了。*/
                    continue;
                }
                string myemps = dr_yes["Emps"].ToString();
                string WorkID = dr_yes["WorkID"].ToString();
                int fk_node = int.Parse(dr_yes["FK_Node"].ToString());

                // 找到其中的一个Entity.
                CHOfNode cn = new CHOfNode();
                int i = cn.Retrieve(CHOfNodeAttr.WorkId, WorkID, CHOfNodeAttr.FK_Node, fk_node);
                if (i == 0)
                    throw new Exception("不可能出现的情况。");

                Node nd = new Node(fk_node);

                string[] emps = myemps.Split(',');
                foreach (string emp in emps)
                {
                    if (emp == "" || emp == null)
                        continue;

                    if (emp == cn.FK_Emp)
                    {
                        /*如果是已经存在人记录。*/
                        continue;
                    }

                    CHOfNode mycn = new CHOfNode();
                    mycn.Copy(cn);
                    mycn.FK_Emp = emp;
                    //如果在逾期的情况下已经做完了此工作，那么程序跳出
                    int a = mycn.Retrieve(CHOfNodeAttr.WorkId, WorkID, CHOfNodeAttr.FK_Node, fk_node, CHOfNodeAttr.FK_Emp, mycn.FK_Emp);
                    if (a == 1)
                        continue;
                    mycn.CDT = "无";
                    mycn.NodeState = 0;
                    mycn.CentOfAdd = 0;
                    mycn.CentOfCut = nd.DeductCent;
                    mycn.Cent = 0;
                    mycn.Save();
                }
            }
            #endregion
        }
		/// <summary>
		/// 流程时效考核(次举是为了处理流程效率低下的问题。巴在流程运行过程中的数据通过此调度运行到这里。)
		/// </summary>
		/// <returns></returns>
        public static string InitCHOfNode()
        {
            Log.DefaultLogWriteLine(LogType.Info, Web.WebUser.Name + "开始调度考核信息:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            string infoMsg = "", errMsg = "";

            //DBAccess.RunSQL("DELETE WF_CHOfStation");  //clear data.
            Nodes nds = new Nodes();
            nds.RetrieveAll();
            // 建立一个事例．
            //CHOfNode ch = new CHOfNode();

            string fromdatetime = DateTime.Now.Year + "-01-01";
            fromdatetime = "2004-01-01 00:00";
            //string fromdatetime=DateTime.Now.Year+"-01-01 00:00";
            //string fromdatetime=DateTime.Now.Year+"-01-01 00:00";
            string insertSql = "";
            foreach (Node nd in nds)
            {
                if (nd.IsPCNode)  /* 如果是计算机节点.*/
                    continue;

                if (nd.IsCheckNode)
                    continue;

                insertSql = "INSERT INTO WF_CHOfNode ( FK_Node, WorkID, NodeState,  Recorder, Emps,RDT, CDT  )"
                    + " "
                    + "  SELECT " + nd.NodeID + " as FK_Node, OID as WorkID, NodeState, Recorder,Emps,RDT,CDT "
                    + "  FROM " + nd.HisWork.EnMap.PhysicsTable
                    + "  WHERE  OID NOT IN ( SELECT WorkID FROM WF_CHOfNode WHERE FK_Node=" + nd.NodeID + " )";
                try
                {
                    DBAccess.RunSQL(insertSql);
                }
                catch (Exception ex)
                {
                    Log.DefaultLogWriteLineInfo(insertSql + " " + ex.Message);
                }
            }
            // 加入审核信息。
            //Log.DefaultLogWriteLine(LogType.Info, Web.WebUser.Name + "调度考核信息End"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")) ;
            DBAccess.RunSP("WF_UpdateCHOfNode");
            return infoMsg;
        }
		/// <summary>
		/// 流程统计分析
		/// </summary>
		/// <param name="fromDatetime"></param>
		/// <returns></returns>
		public static string InitFlows(string fromDatetime)
		{
			Log.DefaultLogWriteLine(LogType.Info,Web.WebUser.Name+" ################# Start 执行统计 #####################"); 
			//删除部门错误的流程
			//DBAccess.RunSQL("DELETE WF_BadWF WHERE BadFlag='FlowDeptBad'");
			fromDatetime="2004-01-01 00:00";

			Flows fls= new Flows();
			fls.RetrieveAll();

			CHOfFlow   fs = new CHOfFlow();
			foreach(Flow fl in fls)
			{
				Node nd =fl.HisStartNode;
				try
				{
					string sql="INSERT INTO WF_CHOfFlow SELECT OID WorkID, "+fl.No+" as FK_Flow, WFState, ltrim(rtrim(Title)) as Title,ltrim(rtrim(WFLog)) as WFLog, Recorder as FK_Emp,"
						+" RDT, CDT, 0 as SpanDays,'' FK_Dept,"
						+"'' as FK_ZSJG,'' AS FK_NY,'' as FK_AP,'' AS FK_ND, '' AS FK_YF, Recorder ,'' as FK_XJ, '' as FK_Station   "
						+" FROM "+nd.HisWork.EnMap.PhysicsTable+" WHERE RDT>='"+fromDatetime+"' AND OID NOT IN ( SELECT WorkID FROM WF_CHOfFlow  )";
					DBAccess.RunSQL(sql);
				}
				catch(Exception ex)
				{
					throw new Exception(fl.Name+ "   "+nd.Name+""+ex.Message );
				}
			}
			DBAccess.RunSP("WF_UpdateCHOfFlow");
			Log.DefaultLogWriteLine(LogType.Info,Web.WebUser.Name+" End 执行统计调度"); 
			return "";
		}
		  

		#region 流程	 
		/// <summary>
		/// 两个方面：
		/// 1，如果原始表里删除了，产生工作者列表里要删除。工作者集合里要删除。		 
		/// 2，如果原始表里开始节点里有没有完成的工作，
		/// 但是在产生的流程里没有他们。就把原始记录删除。
		/// </summary>
		public static void ClearInvalidWF()
		{
			Log.DefaultLogWriteLine(LogType.Info, Web.WebUser.Name+"==============开始调度清理坏节点信息。");
			

			Flows fls = new Flows();
			fls.RetrieveAll();
			string sql="";
			string startNodeTable="";	
			int i=0;

			#region 产生原因可能是运行时间不能确定的错误.这些不能确定的错误就是,非法数据库操作造成的成的.

			// 如果操作员被删除了， 删除它当前的工作。


			// DBAccess.RunSQL("DELETE WF_GenerWorkFlow WHERE Recorder=1 OR Recorder NOT IN (SELECT EmpID FROM Pub_Emp) "); 此操作不能执行。
			// 可能出现的情况是，如果当前的工作有3个人可以做，并且是他发起的，当前人员有被删除掉了，那么其他的两个人也不能处理这个工作了。

			DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE FK_Emp='admin'"); // 系统管理员不能发起流程。
			DBAccess.RunSQL("DELETE WF_GenerWorkFlow WHERE Recorder='admin'"); // 系统管理员不能发起流程。

			DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE TO_CHAR( FK_EMP )  LIKE '%8888'"); // 系统管理员不能发起流程。
			DBAccess.RunSQL("DELETE WF_GenerWorkFlow   WHERE TO_CHAR( RECORDER )  LIKE '%8888'"); // 系统管理员不能发起流程。
     


			//删除已经删除用户发起的流程.
			DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE FK_Emp NOT IN (SELECT EmpID FROM Pub_Emp) "); 
			

			sql="DELETE WF_GenerWorkFlow WHERE   WorkId NOT IN (SELECT WorkId FROM WF_GenerWorkerList)";
			i=DBAccess.RunSQL(sql);
			Log.DefaultLogWriteLine(LogType.Info, "工作集合里面存在的非法workId共删除"+i+"个sql="+sql);

			sql="DELETE WF_GenerWorkerList WHERE   WorkId NOT IN (SELECT WorkId FROM WF_GenerWorkFlow)";
			i=DBAccess.RunSQL(sql);
			Log.DefaultLogWriteLine(LogType.Info, "工作者列表里面存在的非法workId共删除"+i+"个sql="+sql);
			#endregion


			#region 产生的原因是,可能是流程变化,造成当前流程的错误。.
			//sql="DELETE WF_GenerWorkerList WHERE   FK_Node NOT IN (SELECT NodeId FROM WF_Node)";
			//i=DBAccess.RunSQL(sql);
			//Log.DefaultLogWriteLine(LogType.Info, "产生可能造成的节点(工作流程)共删除:"+i+"个sql="+sql);

			//sql="DELETE WF_GenerWorkFlow WHERE   FK_CurrentNode NOT IN (SELECT NodeId FROM WF_Node)";
			//i=DBAccess.RunSQL(sql);
			//Log.DefaultLogWriteLine(LogType.Info, "产生可能造成的节点(工作者列表):共删除"+i+"个sql="+sql);


			//sql="DELETE WF_NumCheck WHERE   NodeId NOT IN (SELECT NodeId FROM WF_Node)";
			//i=DBAccess.RunSQL(sql);
			//Log.DefaultLogWriteLine(LogType.Info, "清除坏节点:共删除"+i+"个sql="+sql);

			//sql="DELETE WF_StandardCheck WHERE   NodeId NOT IN (SELECT NodeId FROM WF_Node)";
			//i=DBAccess.RunSQL(sql);
			//Log.DefaultLogWriteLine(LogType.Info, "清除坏节点:共删除"+i+"个sql="+sql);
			#endregion end 
			 
			foreach(Flow fl in fls)
			{
				Log.DefaultLogWriteLine(LogType.Info, " ---: 要操作的流程名称:"+fl.Name);

				/* each flow. */
				Nodes nds = fl.HisNodes;
				startNodeTable = fl.HisStartNode.HisWork.EnMap.PhysicsTable;

				#region 关于开始节点表与 产生工作流程表之间的关系。
				// 如果原始表里删除了，产生工作流程列表里要删除  (暂时不能使用,因为有两个开始工作,都同用一个物理表.) 
				
				sql="DELETE   WF_GenerWorkFlow WHERE  FK_Flow='"+fl.No+"' AND WorkId NOT IN (SELECT OID FROM "+startNodeTable+")";
				i = DBAccess.RunSQL(sql);
				Log.DefaultLogWriteLine(LogType.Info, "坏产生流程:"+i+ "个,sql="+sql);
				 

				// 如果开始工作节点表里面，没有完成的节点，但是在产生的工作流程里面不存在。就要删除他们。
				sql="DELETE "+startNodeTable+" WHERE WFState!=1 AND OID NOT IN (SELECT WorkID FROM WF_GenerWorkFlow WHERE FK_Flow='"+fl.No+"')";
				i=DBAccess.RunSQL(sql);
				Log.DefaultLogWriteLine(LogType.Info, "坏工作开始节点["+fl.Name+"]:"+i+ "个,sql="+sql);
				#endregion 关于开始节点表与 产生工作流程表之间的关系。

				// 删除每一个表里面的与开始工作节点ID对应不上的记录。
				foreach(Node nd in nds)
				{
					if (nd.IsStartNode)
						continue;
					if (nd.IsCheckNode)
					{
						/* 如果是审核节点*/
						sql="DELETE "+nd.HisWork.EnMap.PhysicsTable+" WHERE NodeId="+nd.NodeID+" and OID  NOT IN ( SELECT OID from "+startNodeTable+" )";
						i =DBAccess.RunSQL(sql);
						Log.DefaultLogWriteLine(LogType.Info, "坏普通节点:"+i+ "个名称["+nd.Name+"],sql="+sql);
						continue;
					}
					
					// 去掉,因为有可能多个节点同用一个表.
					sql="DELETE "+nd.HisWork.EnMap.PhysicsTable+" WHERE  OID  NOT IN ( SELECT OID from "+startNodeTable+" )";
					i =DBAccess.RunSQL(sql);
					Log.DefaultLogWriteLine(LogType.Info, "坏普通节点:"+i+ "个名称["+nd.Name+"],sql="+sql);

				}
				// 工作者列表与产生流程表之间的关系。
				//sql="DELETE WF_GenerWorkerList where WorkId in  ( select WorkId from WF_GenerWorkerList where WorkId not in (select WorkId from WF_GenerWorkFlow) )";
				//  i = DBAccess.RunSQL(sql) ;
				//Log.DefaultLogWriteLine(LogType.Info, "坏普通节点:"+i+ "个 ,sql="+sql);
			}

			Log.DefaultLogWriteLine(LogType.Info, Web.WebUser.Name+"end==============调度清理坏节点信息。");
		}
		#endregion	

		/// <summary>
		/// 获取外部属性
		/// </summary>
		/// <returns></returns>
		public static void DTSPCWork()
		{
			 
			ArrayList als = DA.ClassFactory.GetObjects("BP.WF.PCWorks");
			foreach(PCWorks wks in als)
			{
				wks.DoInitData();
			}
		}
		/// <summary>
		/// TransferAutoGenerWorkFlow
		/// </summary>
		/// <param name="flowNo">TransferAutoGenerWorkFlow</param>
		/// <returns></returns>
		public static string TransferAutoGenerWorkFlow(string flowNo)
		{
			Flow fl = new Flow(flowNo);
			return TransferAutoGenerWorkFlow(fl);
		}
		/// <summary>
		/// TransferAutoGenerWorkFlow
		/// </summary>
		/// <param name="fl"></param>
		/// <returns></returns>
		public static string TransferAutoGenerWorkFlow(Flow fl)
		{				 
			//if (fl.IsAutoWorkFlow==false)
			//	throw new Exception("@此工作流程不是一个自动产生工作流程。");
			string str="";
			PCTaxpayerStartWorks ptws=(PCTaxpayerStartWorks)fl.HisStartNode.HisWorks;
			try
			{
				str+= ptws.AutoGenerWorkFlow();
			}
			catch(Exception ex)
			{
				Log.DefaultLogWriteLine(LogType.Error,"@流程["+fl.Name+"]在生成工作流程出现错误。"+ex.Message);
				//throw new Exception("@流程["+fl.Name+"]在生成工作流程出现错误。"+ex.Message) ; 
			}
			return str;
		}
		/// <summary>
		/// 调用自动产生工作流程
		/// </summary>
		public static string TransferAutoGenerWorkFlowAll()
		{
			string str="";
			Flows fls = new Flows();
			fls.RetrieveIsAutoWorkFlow();
			foreach(Flow fl in fls)
			{
				str+=TransferAutoGenerWorkFlow(fl);
			}
			return str;
		}

		/// <summary>
		/// StandardCheckss
		/// </summary>
		public WFDTS()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
	}
}
