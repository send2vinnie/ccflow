using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.En;
using BP.Port;
using BP.Web;

namespace BP.WF
{
	/// <summary>
	/// 文书送达状态
	/// </summary>
	public enum BookState
	{
		/// <summary>
		/// 未送达
		/// </summary>
		UnSend,
		/// <summary>
		/// 逾期未送达
		/// </summary>
		UnSendTimeout,
		/// <summary>
		/// 已送达
		/// </summary>
		Send,
		/// <summary>
		/// 未找到人
		/// </summary>
		Notfind,
		/// <summary>
		///已经归档
		/// </summary>
		Pigeonhole
	}
	/// <summary>
	/// 文书属性
	/// </summary>
	public class BookAttr
	{
		#region 属性
        public const string MyPK = "MyPK";
		/// <summary>
		/// 工作ID
		/// </summary>
		public const string WorkID="WorkID";
		/// <summary>
		/// 节点
		/// </summary>
		public const string FK_Node="FK_Node";
		/// <summary>
		/// 相关功能
		/// </summary>
		public const string FK_NodeRefFunc="FK_NodeRefFunc";
        public const string BookName = "BookName";

		/// <summary>
		/// 送达否
		/// </summary>
		public const string BookState="BookState";
		/// <summary>
		/// 退回时间
		/// </summary>
		public const string ReturnDateTime="ReturnDateTime";
		/// <summary>
		/// BookNo
		/// </summary>
		public const string BookNo="BookNo";
		/// <summary>
		/// 文号
		/// </summary>
		public const string FilePrix="FilePrix";
		public const string FileName="FileName";
		/// <summary>
		/// 归档日期
		/// </summary>
		public const string PigeDate="PigeDate";
		/// <summary>
		/// 案卷编号
		/// </summary>
		public const string AJNo="AJNo";
		/// <summary>
		/// 管理员
		/// </summary>
		public const string BookAdmin="BookAdmin";
		/// <summary>
		/// 记录时间．
		/// </summary>
		public const string RDT="RDT";
		/// <summary>
		/// 应送达时间
		/// </summary>
		public const string ShouldSendDT="ShouldSendDT";
		/// <summary>
		/// 记录人．
		/// </summary>
		public const string Rec="Rec";
		/// <summary>
		/// 部门
		/// </summary>
		public const string FK_Dept="FK_Dept";
		///<summary>
		///送达人
		///</summary>
		public const string Sender="Sender";
		/// <summary>
		/// 受送达人
		/// </summary>
		public const string Accepter="Accepter";
		/// <summary>
		/// 送达地点
		/// </summary>
		public const string AccepterAddr="AccepterAddr";
		/// <summary>
		/// 收件日期
		/// </summary>
		public const string AccepterDateTime="AccepterDateTime";

		/// <summary>
		/// 代收人代收理由
		/// </summary>
		public const string AccepterNote="AccepterNote";
		/// <summary>
		/// 受送达人拒收理由和日期
		/// </summary>
		public const string AccepterDisNote="AccepterDisNote";
		/// <summary>
		/// 见证人签名或盖章
		/// </summary>
		public const string JZR="JZR";
		/// <summary>
		/// 年月
		/// </summary>
		public const string FK_NY="FK_NY";
		/// <summary>
		/// 保管期限
		/// </summary>
		public const string BGQX="BGQX";
        /// <summary>
        /// 要替换的信息
        /// </summary>
        public const string ReplaceVal = "ReplaceVal";
        public const string FID = "FID";

        
		#endregion
	}
	/// <summary>
	/// 文书
	/// </summary> 
    public class Book : Entity
    {
        #region 统计信息的属性
        /// <summary>
        /// 未送达
        /// </summary>
        public static int NumOfUnSend
        {
            get
            {
                if (int.Parse(DateTime.Now.ToString("hh")) < 9)
                {
                    string sq1l = "UPDATE WF_Book SET BookState=" + (int)BookState.UnSendTimeout + "  WHERE ShouldSendDT > '" + DataType.CurrentData + "' AND  BookState=" + (int)BookState.UnSend;
                    DBAccess.RunSQL(sq1l);
                }
                string sql = "SELECT COUNT(*)  FROM WF_Book WHERE  Rec='" + WebUser.No + "' AND BookState=" + (int)BookState.UnSend;
                return DBAccess.RunSQLReturnValInt(sql);
            }
        }
        /// <summary>
        /// 逾期未送到
        /// </summary>
        public static int NumOfUnSendTimeout
        {
            get
            {
                string sql = "SELECT COUNT(*)  FROM WF_Book WHERE  Rec='" + WebUser.No + "' AND  BookState=" + (int)BookState.UnSendTimeout;
                return DBAccess.RunSQLReturnValInt(sql);
            }
        }
        /// <summary>
        /// 已经送达
        /// </summary>
        public static int NumOfSend
        {
            get
            {
                string sql = "SELECT COUNT(*)  FROM WF_Book WHERE  Rec='" + WebUser.No + "' AND BookState=" + (int)BookState.Send;
                return DBAccess.RunSQLReturnValInt(sql);
            }
        }
        /// <summary>
        /// 没有发现人
        /// </summary>
        public static int NumOfNotfind
        {
            get
            {
                string sql = "SELECT COUNT(*)  FROM WF_Book WHERE  Rec='" + WebUser.No + "' AND BookState=" + (int)BookState.Notfind;
                return DBAccess.RunSQLReturnValInt(sql);
            }
        }
        /// <summary>
        /// 已经归档
        /// </summary>
        public static int NumOfPigeonhole
        {
            get
            {
                string sql = "SELECT COUNT(*)  FROM WF_Book WHERE  Rec='" + WebUser.No + "' AND BookState=" + (int)BookState.Pigeonhole;
                return DBAccess.RunSQLReturnValInt(sql);
            }
        }
        #endregion

        #region 基本属性
        /// <summary>
        ///   文书送达状态。
        /// </summary>
        public BookState BookState
        {
            get
            {
                return (BookState)GetValIntByKey(BookAttr.BookState);
            }
            set
            {
                SetValByKey(BookAttr.BookState, (int)value);
            }
        }
        public string FilePrix
        {
            get
            {
                return this.GetValStringByKey(BookAttr.FilePrix);
            }
            set
            {
                this.SetValByKey(BookAttr.FilePrix, value);
            }
        }
        public string FileName
        {
            get
            {
                return this.GetValStringByKey(BookAttr.FileName);
            }
            set
            {
                this.SetValByKey(BookAttr.FileName, value);
            }
        }

        public string PigeDate
        {
            get
            {
                return this.GetValStringByKey(BookAttr.PigeDate);
            }
            set
            {
                this.SetValByKey(BookAttr.PigeDate, value);
            }
        }
        public string AJNo
        {
            get
            {
                return this.GetValStringByKey(BookAttr.AJNo);
            }
            set
            {
                this.SetValByKey(BookAttr.AJNo, value);
            }
        }
      
        /// <summary>
        ///  文书编号
        /// </summary>
        public string BookNo
        {
            get
            {
                return this.GetValStringByKey(BookAttr.BookNo);
            }
            set
            {
                this.SetValByKey(BookAttr.BookNo, value);
            }
        }
        public string FK_NodeRefFuncText
        {
            get
            {
                return this.GetValRefTextByKey(BookAttr.FK_NodeRefFunc);
            }
        }
        public string FK_NodeRefFunc
        {
            get
            {
                return this.GetValStrByKey(BookAttr.FK_NodeRefFunc);
            }
            set
            {
                this.SetValByKey(BookAttr.FK_NodeRefFunc, value);
            }
        }
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(BookAttr.WorkID);
            }
            set
            {
                this.SetValByKey(BookAttr.WorkID, value);
            }
        }
        public Int64 FID
        {
            get
            {
                return this.GetValInt64ByKey(BookAttr.FID);
            }
            set
            {
                this.SetValByKey(BookAttr.FID, value);
            }
        }
        /// <summary>
        /// Node
        /// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(BookAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(BookAttr.FK_Node, value);
            }
        }
        /// <summary>
        /// 送达时间
        /// </summary>
        public string AccepterDateTime
        {
            get
            {
                return this.GetValStringByKey(BookAttr.AccepterDateTime);
            }
            set
            {
                this.SetValByKey(BookAttr.AccepterDateTime, value);
            }
        }
        public string ShouldSendDT
        {
            get
            {
                return this.GetValStringByKey(BookAttr.ShouldSendDT);
            }
            set
            {
                this.SetValByKey(BookAttr.ShouldSendDT, value);
            }
        }
        /// <summary>
        /// 归还时间
        /// </summary>
        public string ReturnDateTime
        {
            get
            {
                return this.GetValStringByKey(BookAttr.ReturnDateTime);
            }
            set
            {
                this.SetValByKey(BookAttr.ReturnDateTime, value);
            }
        }
        /// <summary>
        /// 文书打印时间
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(BookAttr.RDT);
            }
            set
            {
                this.SetValByKey(BookAttr.RDT, value);
            }
        }
        /// <summary>
        /// 打印人
        /// </summary>
        public string Rec
        {
            get
            {
                return this.GetValStringByKey(BookAttr.Rec);
            }
            set
            {
                this.SetValByKey(BookAttr.Rec, value);
            }
        }
        /// <summary>
        /// 部门
        /// </summary>
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(BookAttr.FK_Dept);
            }
            set
            {
                this.SetValByKey(BookAttr.FK_Dept, value);
            }
        }
        /// <summary>
        /// 送达人
        /// </summary>
        public string Sender
        {
            get
            {
                return this.GetValStringByKey(BookAttr.Sender);
            }
            set
            {
                this.SetValByKey(BookAttr.Sender, value);
            }
        }
        public string Accepter
        {
            get
            {
                return this.GetValStringByKey(BookAttr.Accepter);
            }
            set
            {
                this.SetValByKey(BookAttr.Accepter, value);
            }
        }
        public string AccepterAddr
        {
            get
            {
                return this.GetValStringByKey(BookAttr.AccepterAddr);
            }
            set
            {
                this.SetValByKey(BookAttr.AccepterAddr, value);
            }
        }

        public string AccepterDisNote
        {
            get
            {
                return this.GetValStringByKey(BookAttr.AccepterDisNote);
            }
            set
            {
                this.SetValByKey(BookAttr.AccepterDisNote, value);
            }
        }
        public string AccepterNote
        {
            get
            {
                return this.GetValStringByKey(BookAttr.AccepterNote);
            }
            set
            {
                this.SetValByKey(BookAttr.AccepterNote, value);
            }
        }
        public string JZR
        {
            get
            {
                return this.GetValStringByKey(BookAttr.JZR);
            }
            set
            {
                this.SetValByKey(BookAttr.JZR, value);
            }
        }
        public string BookName
        {
            get
            {
                return this.GetValStringByKey(BookAttr.BookName);
            }
            set
            {
                this.SetValByKey(BookAttr.BookName, value);
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// HisUAC
        /// </summary>
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.IsDelete = false;
                uac.IsInsert = false;
                uac.IsUpdate = false;
                uac.IsView = true;
                return uac;
            }
        }
        /// <summary>
        /// 条目
        /// </summary>
        public Book() { }
        public Book(string pk) 
        {
            this.FileName = pk;
            this.Retrieve();
        }

         
        #endregion

        #region Map
        /// <summary>
        /// EnMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("WF_Book");
                map.DepositaryOfMap = Depositary.None;
                map.EnDesc = "文书";

                map.AddTBStringPK(BookAttr.FileName, null, "文书文件名称", false, false, 1, 100, 5);
                map.AddTBInt(BookAttr.WorkID, 0, "工作ID", false, true);
                map.AddTBInt(BookAttr.FID, 0, "FID", false, true);

                map.AddDDLEntities(BookAttr.FK_Node, 0, DataType.AppInt, "工作名称", new Nodes(), NodeAttr.NodeID, NodeAttr.Name, false);
                map.AddTBString(BookAttr.FilePrix, null, "文号", true, true, 0, 100, 5);
                map.AddDDLEntities(BookAttr.FK_NodeRefFunc, "", "文书", new BookTemplates(), false);
                map.AddTBString(BookAttr.BookName, null, "文书名称", true, true, 0, 100, 5);
                map.AddTBDate(BookAttr.RDT, "文书打印时间", true, true);
  
                map.AddDDLEntities(BookAttr.Rec, Web.WebUser.No, "打印人", new Emps(), false);
                map.AddTBString(BookAttr.PigeDate, null, "归档日期", true, true, 0, 100, 5);

                map.AddTBString(BookAttr.AJNo, null, "案卷编号", true, true, 0, 100, 5);

                map.AddTBString(BookAttr.BookNo, null, "编号", true, true, 0, 100, 5);
                map.AddTBString(BookAttr.BGQX, "10年", "保管期限", true, true, 0, 100, 5);

                map.AddDDLSysEnum(BookAttr.BookState, 1, "文书状态", false, true);
                map.AddTBDate(BookAttr.ShouldSendDT, "应送达时间", true, true);

                //送达人
                map.AddTBString(BookAttr.Sender, null, "送达人", false, true, 0, 100, 5);
                 

                //送达地点
                map.AddTBString(BookAttr.Accepter, null, "受送达人", false, true, 0, 100, 5);
                map.AddTBString(BookAttr.AccepterAddr, null, "送达地点", false, true, 0, 100, 5);
                map.AddTBString(BookAttr.AccepterDateTime, null, "收件日期", false, true, 0, 100, 5);
                map.AddTBString(BookAttr.AccepterNote, null, "代收人代收理由", false, true, 0, 100, 5);
                map.AddTBString(BookAttr.AccepterDisNote, null, "受送达人拒收理由和日期", false, true, 0, 100, 5);
                map.AddTBString(BookAttr.JZR, null, "见证人签名或盖章", false, true, 0, 100, 5);

                map.AddDDLEntities(BookAttr.FK_Dept, null, "部门", new BP.Port.Depts(), false);
                map.AddDDLEntities(BookAttr.FK_NY, DataType.CurrentYearMonth, "隶属年月", new BP.Pub.NYs(), false);
                map.AddTBIntMyNum();

                //设置条件查询
                //map.AddSearchAttr(BookAttr.FK_NY);
                map.AddSearchAttr(BookAttr.FK_Dept);
                map.AddSearchAttr(BookAttr.BookState);
                map.AddSearchAttr(BookAttr.FK_NY);
                map.AddSearchAttr(BookAttr.Rec);
                map.AddSearchAttr(BookAttr.FK_NodeRefFunc);

               // map.AttrsOfSearch.AddFromTo("打印日期", BookAttr.RDT, DateTime.Now.AddDays(-15).ToString(DataType.SysDataFormat),
                 //  DataType.CurrentData, 8);

                //map.AddSearchAttr(BookAttr.FK_NodeRefFunc);
                //map.AddSearchAttr(BookAttr.BookState);
                //map.AttrsOfSearch.AddFromTo("日期",BookAttr.RDT, DateTime.Now.AddMonths(-1).ToString(DataType.SysDataTimeFormat), DA.DataType.CurrentDataTime,6);

                RefMethod rm = new RefMethod();
                rm.Title = "标记文书送达";
                rm.ClassMethodName = this.ToString() + ".DoMarketSend()";
                rm.Icon = "/Images/Btn/do.gif";
                rm.ToolTip = "标记文书送达。";
                rm.Warning = "您是否要标记这个文书送达吗？";
                rm.Width = 0;
                rm.Height = 0;
                rm.Target = null;
                map.AddRefMethod(rm);


                rm = new RefMethod();
                rm.Title = "标记文书未找到人";
                rm.ClassMethodName = this.ToString() + ".DoMarketNotfind()";
                rm.Icon = "/Images/Btn/AlertBell.gif";
            //    rm.ToolTip = "标记文书送达。";
                rm.Warning = "您是否要标记这个文书未找到人吗？";
                rm.Width = 0;
                rm.Height = 0;
                rm.Target = null;
                map.AddRefMethod(rm);

                    
                rm = new RefMethod();
                rm.Title = "查看文书";
                rm.ClassMethodName = this.ToString() + ".DoPrint()";
                rm.Icon = "/Images/Btn/Word.gif";
                rm.ToolTip = "查看文书。";
                rm.Width = 0;
                rm.Height = 0;
                rm.Target = null;
                map.AddRefMethod(rm);


                //rm = new RefMethod();
                //rm.Title = "一户式资料";
                //rm.ClassMethodName = this.ToString() + ".DoYHS()";
                //rm.Icon = "/Images/Btn/Authorize.gif";
                //rm.ToolTip = "一户式资料。";
                //rm.Width = 0;
                //rm.Height = 0;
                //rm.Target = null;
                //map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("WorkRpt", "工作报告"); // "工作报告";
                rm.ClassMethodName = this.ToString() + ".DoWorkRpt()";
                rm.Icon = "/Images/Btn/Authorize.gif";
                rm.ToolTip = "工作报告。";
                rm.Width = 0;
                rm.Height = 0;
                rm.Target = null;
                map.AddRefMethod(rm);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        #region 对文书功能处理
        public string DoMarketSend()
        {
            this.BookState = BookState.Send;
            this.Update();
            return "已经标记文书送达。";
        }
        public string DoMarketNotfind()
        {
            this.BookState = BookState.Notfind;
            this.Update();
            return "已经标记文书未找到人。";
        }
        #endregion


        #region 打印文书
        public string DoWorkRpt()
        {
            Node nd = new Node(this.FK_Node);
            PubClass.WinOpen("../WF/WFRpt.aspx?WorkID=" + this.WorkID + "&FID="+this.FID+"&FK_Flow=" + nd.FK_Flow + "&FlowNo=" + nd.FK_Flow + "&NodeId=" + this.FK_Node);
            return null;
        }
        public string DoYHS()
        {
            //string url = "/"+SystemConfig.AppName+"/Comm/UIEn.aspx?EnsName=BP.Port.TaxpayerDtls&PK=" + this.FK_Taxpayer;
            //PubClass.WinOpen(url);
            return null;
        }
        /// <summary>
        /// 执行打印
        /// </summary>
        /// <returns></returns>
        public string DoPrint()
        {
            //string script = "<a href=\"javascript:WinOpem(+ "&1" + "', '0' );\"  ><img src='../../Images/Btn/WORD.gif' border=0 /> 打开</a>";
            string path = this.FileName;

            path = path.Replace("_"+this.WorkID+".doc", "");
            path = path.Replace("_", "/");



            string url = "/"+System.Web.HttpContext.Current.Request.ApplicationPath+"/FlowFile/" + path +"/"+ this.FileName;
            PubClass.WinOpen(url);
            return null;

            // string script = "Run2('C:\\\\ds2002\\\\OpenBook.exe', '" + this.FileName + "', '1', '0');";
            // string script = "Run('C:\\\\ds2002\\\\OpenBook.exe', '" + this.FileName + "', '0' );";
            // PubClass.ResponseWriteScript(script);
            // return null;
            // PubClass.WinOpen("../WF/NodeRefFunc.aspx?NodeRefFuncOID=" + this.FK_NodeRefFunc + "&WorkFlowID=" + this.WorkID + "&FlowNo=" + nd.FK_Flow + "&NodeId=" + this.FK_Node);
            //  return null;
        }
        #endregion
    }
	/// <summary>
	/// 条目
	/// </summary>
	public class Books :Entities
	{
		#region 构造方法属性
		/// <summary>
		/// Books
		/// </summary>
		public Books(){}
		#endregion 

		#region 属性
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Book();
			}
		}
		#endregion
	}
}
