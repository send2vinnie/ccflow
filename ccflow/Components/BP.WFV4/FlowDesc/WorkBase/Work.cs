using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;
using BP.Sys;
using BP.Port;
using System.Security.Cryptography;
using System.Text;

namespace BP.WF
{
    /// <summary>
    /// 工作类型
    /// </summary>
    public enum WorkType
    {
        /// <summary>
        /// 普通的
        /// </summary>
        Ordinary,
        /// <summary>
        /// 自动的
        /// </summary>
        Auto
    }
    /// <summary>
    /// 工作属性
    /// </summary>
    public class WorkAttr
    {
        #region 基本属性
        /// <summary>
        /// 工作ID
        /// </summary>
        public const string OID = "OID";
        /// <summary>
        /// 完成任务时间
        /// </summary>
        public const string CDT = "CDT";
        /// <summary>
        /// 记录时间
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// 记录人
        /// </summary>
        public const string Rec = "Rec";
        /// <summary>
        /// 节点工作状态(0 是初试化, 1 已经完成, 2,已经读取)
        /// </summary>
        public const string NodeState = "NodeState";
        /// <summary>
        /// 备注
        /// </summary>
        public const string Note = "Note";
        /// <summary>
        /// Emps
        /// </summary>
        public const string Emps = "Emps";
        /// <summary>
        /// 流程ID
        /// </summary>
        public const string FID = "FID";
        /// <summary>
        /// Sender
        /// </summary>
        public const string Sender = "Sender";
        /// <summary>
        /// MyNum
        /// </summary>
        public const string MyNum = "MyNum";
        /// <summary>
        /// MD5
        /// </summary>
        public const string MD5 = "MD5";
        #endregion
    }
    /// <summary>
    /// WorkBase 的摘要说明。
    /// 工作流程采集信息的基类
    /// </summary>
    abstract public class Work : Entity
    {
        /// <summary>
        /// 检查MD5值是否通过
        /// </summary>
        /// <returns>true/false</returns>
        public bool IsPassCheckMD5()
        {
            string md51 = this.GetValStringByKey(WorkAttr.MD5);
            string md52 = Glo.GenerMD5(this);
            if (md51 != md52)
                return false;
            return true;
        }

        #region 基本属性(必须的属性)
        public override string PK
        {
            get
            {
                return "OID";
            }
        }
        /// <summary>
        /// classID
        /// </summary>
        public override string ClassID
        {
            get
            {
                return "ND"+this.HisNode.NodeID;
            }
        }
        /// <summary>
        /// 流程ID
        /// </summary>
        public virtual Int64 FID
        {
            get
            {
                return this.GetValInt64ByKey(WorkAttr.FID);
            }
            set
            {
                this.SetValByKey(WorkAttr.FID, value);
            }
        }
        /// <summary>
        /// workid,如果是空的就返回 0 . 
        /// </summary>
        public virtual Int64 OID
        {
            get
            {
                return this.GetValInt64ByKey(WorkAttr.OID);
            }
            set
            {
                this.SetValByKey(WorkAttr.OID, value);
            }
        }
        /// <summary>
        /// 完成时间
        /// </summary>
        public string CDT
        {
            get
            {
                string str = this.GetValStringByKey(WorkAttr.CDT);
                if (str.Length < 5)
                    this.SetValByKey(WorkAttr.CDT, DataType.CurrentDataTime);

                return this.GetValStringByKey(WorkAttr.CDT);
            }
        }
        public string Emps
        {
            get
            {
                return this.GetValStringByKey(WorkAttr.Emps);
            }
            set
            {
                this.SetValByKey(WorkAttr.Emps, value);
            }
        }
        public override int RetrieveFromDBSources()
        {
            try
            {
                return base.RetrieveFromDBSources();
            }
            catch (Exception ex)
            {
                this.CheckPhysicsTable();
                throw ex;
            }
        }
        public int RetrieveFID()
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhereIn(WorkAttr.OID, "(" + this.FID + "," + this.OID + ")");
            // qo.addOr();
            //qo.AddWhere(WorkAttr.OID, this.OID);
            int i = qo.DoQuery();

            if (i == 0)
            {
                if (SystemConfig.IsDebug == false)
                {
                    this.CheckPhysicsTable();
                    throw new Exception("@节点[" + this.EnDesc + "]数据丢失：WorkID=" + this.OID + " FID=" + this.FID + " sql=" + qo.SQL);
                }
            }
            return i;
        }
        public override int Retrieve()
        {
            try
            {
                return base.Retrieve();
            }
            catch (Exception ex)
            {
                this.CheckPhysicsTable();
                throw ex;
            }
        }
        /// <summary>
        /// 记录时间
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(WorkAttr.RDT);
            }
        }
        public string RDT_Date
        {
            get
            {
                try
                {
                    return DataType.ParseSysDate2DateTime(this.RDT).ToString(DataType.SysDataFormat);
                }
                catch
                {
                    return DataType.CurrentData;
                }
            }
        }
        public DateTime RDT_DateTime
        {
            get
            {
                try
                {
                    return DataType.ParseSysDate2DateTime(this.RDT_Date);
                }
                catch
                {
                    return DateTime.Now;
                }
            }
        }
        public string Record_FK_NY
        {
            get
            {
                return this.RDT.Substring(0, 7);
            }
        }
        /// <summary>
        /// 记录人
        /// </summary>
        public string Rec
        {
            get
            {
                string str = this.GetValStringByKey(WorkAttr.Rec);
                if (str == "")
                    this.SetValByKey(WorkAttr.Rec, Web.WebUser.No);

                return this.GetValStringByKey(WorkAttr.Rec);
            }
            set
            {
                this.SetValByKey(WorkAttr.Rec, value);
            }
        }
        /// <summary>
        /// 工作人员
        /// </summary>
        public Emp RecOfEmp
        {
            get
            {
                return new Emp(this.Rec);
            }
        }
        /// <summary>
        /// 记录人名称
        /// </summary>
        public string RecText
        {
            get
            {
                try
                {
                    return this.HisRec.Name;
                }
                catch
                {
                    return this.Rec;
                }
            }
            set
            {
                this.SetValByKey("RecText", value);
            }
        }
        /// <summary>
        /// 拖延天数  
        /// </summary>
        public float DelayDays
        {
            get
            {
                if (this.SpanDays == 0)
                    return 0;
                float days = this.SpanDays - this.HisNode.DeductDays;
                if (days < 0)
                    return 0;
                return days;
            }
        }

        /// <summary>
        /// 得到他的节点状态
        /// </summary>
        /// <returns></returns>
        public int GetNodeState(bool isUpdate)
        {
            if (this.NodeState == NodeState.Complete)
                return 1;
            int days = this.SpanDays;
            if (days > this.HisNode.DeductDays)
            {
                this.NodeState = NodeState.CutCent;
                if (isUpdate)
                    this.DirectUpdate();
                return 3;
            }
            else if (days > this.HisNode.DeductDays)
            {
                this.NodeState = NodeState.CutCent;
                if (isUpdate)
                    this.DirectUpdate();
                return 2;
            }
            else
            {
                return 0;
            }
        }

        private Node _HisNode = null;
        /// <summary>
        /// 工作的节点.
        /// </summary>
        public Node HisNode
        {
            get
            {
                if (this._HisNode == null)
                {
                    this._HisNode = new Node(this.NodeID);
                }
                return _HisNode;
            }
            set
            {
                _HisNode = value;
            }
        }
        /// <summary>
        /// 明细表.
        /// </summary>
        public MapDtls HisMapDtls
        {
            get
            {
                return this.HisNode.MapData.MapDtls;
            }
        }
      
        /// <summary>
        /// 明细表.
        /// </summary>
        public FrmAttachments HisFrmAttachments
        {
            get
            {
                return this.HisNode.MapData.FrmAttachments;
            }
        }
        /// <summary>
        /// 他的工作流程
        /// </summary>
        public WorkFlow HisWorkFlow_del
        {
            get
            {
                return new WorkFlow(this.HisNode.HisFlow, this.OID, this.FID);
            }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public NodeState NodeStateOfEnum
        {
            get
            {
                return (NodeState)this.NodeState;
            }
            set
            {
                this.SetValByKey(WorkAttr.NodeState, (int)value);
            }
        }
        /// <summary>
        /// 节点状态
        /// </summary>
        public NodeState NodeState
        {
            get
            {
                return (NodeState)this.GetValIntByKey(WorkAttr.NodeState);
            }
            set
            {
                this.SetValByKey(WorkAttr.NodeState, (int)value);
            }
        }
        /// <summary>
        /// 节点状态NodeStateText
        /// </summary>
        public string NodeStateText
        {
            get
            {
                try
                {
                    return this.GetValRefTextByKey(WorkAttr.NodeState);
                }
                catch
                {
                    Sys.SysEnum se = new BP.Sys.SysEnum("NodeState", (int)this.NodeState);
                    return se.Lab;

                }
            }

        }

        /// <summary>
        /// 备注(在审核活动中为 审核人意见)
        /// </summary>
        public string Note
        {
            get
            {
                return this.GetValStringByKey(WorkAttr.Note);
            }
            set
            {
                this.SetValByKey(WorkAttr.Note, value);
            }
        }
        #endregion

        #region 扩展属性
        /// <summary>
        /// 跨度天数
        /// </summary>
        public int SpanDays
        {
            get
            {
                if (this.CDT == this.RDT)
                    return 0;
                return DataType.SpanDays(this.RDT, this.CDT);
            }
        }
        /// <summary>
        /// 得到从工作完成到现在的日期
        /// </summary>
        /// <returns></returns>
        public int GetCDTSpanDays(string todata)
        {
            return DataType.SpanDays(this.CDT, todata);
        }
        /// <summary>
        /// 他的记录人
        /// </summary>
        public Emp HisRec
        {
            get
            {
              //  return new Emp(this.Rec);
                Emp emp = this.GetValByKey("HisRec"+this.Rec) as Emp;
                if (emp == null)
                {
                    emp = new Emp(this.Rec);
                    this.SetValByKey("HisRec" + this.Rec, emp);
                }
                return emp;
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 工作流程
        /// </summary>
        protected Work()
        {
            //this.RDT=DateTime.Now.ToString(DataType.SysDataTimeFormat);
            //this.CDT=this.RDT;
            //this.NodeState=NodeState.Init;
            //this.Rec=Web.WebUser.No;			
        }
        /// <summary>
        /// 工作流程
        /// </summary>
        /// <param name="oid">WFOID</param>		 
        protected Work(Int64 oid)
        {
            this.SetValByKey(EntityOIDAttr.OID, oid);
            this.Retrieve();
        }
        #endregion

        #region Node.xml 要配置的信息.
        /// <summary>
        /// 产生本工作中所有的外键参数
        /// 附加一些必要的属性.
        /// </summary>
        /// <returns></returns>
        private string GenerParas_del()
        {
            string paras = "*WorkID" + this.OID + "*UserNo=" + this.Rec + "*NodeState=" + (int)this.NodeState;
            foreach (Attr attr in this.EnMap.Attrs)
            {
                if (attr.MyFieldType == FieldType.Normal)
                    continue;

                if (attr.MyFieldType == FieldType.RefText)
                    continue;

                if (attr.MyFieldType == FieldType.NormalVirtual)
                    continue;

                if (attr.Key == WorkAttr.Rec
                    || attr.Key == "OID"
                    || attr.Key == "NodeState"
                    )
                    continue;

                paras += "*" + attr.Key + "=" + this.GetValStringByKey(attr.Key);
            }
            return paras;
        }
        public virtual string WorkEndInfo
        {
            get
            {
                string tp = "";
                //FAppSets sets = new FAppSets(this.NodeID);
                //foreach (FAppSet set in sets)
                //{
                //    if (set.DoWhat.Contains("?"))
                //        tp += "[<a href=\"javascript:WinOpen('" + set.DoWhat + "&WorkID=" + this.OID + "' ,'sd');\" ><img src='../images/Btn/Do.gif' border=0/>" + set.Name + "</a>]";
                //    else
                //        tp += "[<a href=\"javascript:WinOpen('" + set.DoWhat + "?WorkID=" + this.OID + "' ,'sd');\" ><img src='../images/Btn/Do.gif' border=0/>" + set.Name + "</a>]";
                //}

                if (this.HisNode.IsHaveSubFlow)
                {
                    NodeFlows flows = new NodeFlows(this.HisNode.NodeID);
                    foreach (NodeFlow fl in flows)
                    {
                        tp += "[<a href='CallSubFlow.aspx?FID=" + this.OID + "&FK_Flow=" + fl.FK_Flow + "&FK_FlowFrom=" + this.HisNode.FK_Flow + "' ><img src='../images/Btn/Do.gif' border=0/>" + fl.FK_FlowT + "</a>]";
                    }
                }
                if (tp.Length > 0)
                    return "<div align=left>" + tp + "</div>";
                return tp;
            }
        }
        /// <summary>
        /// 产生要执行的url.
        /// </summary>
        public string GenerNextUrl()
        {
            string appName = System.Web.HttpContext.Current.Request.ApplicationPath;
            string ip = SystemConfig.AppSettings["CIP"];
            if (ip == null || ip == "")
                throw new Exception("@您没有设置CIP");
            return "http://" + ip + "/" + appName + "/WF/Port.aspx?UserNo=" + Web.WebUser.No + "&DoWhat=DoNode&WorkID=" + this.OID + "&FK_Node=" + this.HisNode.NodeID + "&Key=MyKey";
        }
        #endregion

        #region 需要子类写的方法
        /// <summary>
        /// 节点任务完成之后要做的工作.
        /// 可能要做的是
        /// 1, 启动其他的流程.
        /// 2, ...
        /// </summary>
        /// <returns>消息</returns>
        public virtual string AfterWorkNodeComplete()
        {
            if (Web.WebUser.IsAuthorize)
            {
                //AgentLog al = new AgentLog(this.OID,this.HisNode.FK_Flow,this.HisNode.OID,this.Rec,this.RecOfEmp.AuthorizedAgent);
                //this.HisWorkFlow.WritLog("["+this.RecOfEmp.Name+"]授权给["+this.RecOfEmp.AuthorizedAgentOfEmp.Name+"]处理["+this.HisNode.Name+"]工作.");
            }
            return "";
        }
        public void DoAutoFull(Attr attr)
        {
            if (this.OID == 0)
                return;

            if (attr.AutoFullDoc == null || attr.AutoFullDoc.Length == 0)
                return;

            string objval = null;

            // 这个代码需要提纯到基类中去。
            switch (attr.AutoFullWay)
            {
                case BP.En.AutoFullWay.Way0:
                    return;
                case BP.En.AutoFullWay.Way1_JS:
                    break;
                case BP.En.AutoFullWay.Way2_SQL:
                    string sql = attr.AutoFullDoc;
                    Attrs attrs1 = this.EnMap.Attrs;
                    foreach (Attr a1 in attrs1)
                    {
                        if (a1.IsNum)
                            sql = sql.Replace("@" + a1.Key, this.GetValStringByKey(a1.Key));
                        else
                            sql = sql.Replace("@" + a1.Key, "'" + this.GetValStringByKey(a1.Key) + "'");
                    }

                    objval = DBAccess.RunSQLReturnString(sql);
                    break;
                case BP.En.AutoFullWay.Way3_FK:
                    try
                    {
                        string sqlfk = "SELECT @Field FROM @Table WHERE No=@AttrKey";
                        string[] strsFK = attr.AutoFullDoc.Split('@');
                        foreach (string str in strsFK)
                        {
                            if (str == null || str.Length == 0)
                                continue;

                            string[] ss = str.Split('=');
                            if (ss[0] == "AttrKey")
                                sqlfk = sqlfk.Replace('@' + ss[0], "'" + this.GetValStringByKey(ss[1]) + "'");
                            else
                                sqlfk = sqlfk.Replace('@' + ss[0], ss[1]);
                        }
                        sqlfk = sqlfk.Replace("''", "'");

                        objval = DBAccess.RunSQLReturnString(sqlfk);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("@在处理自动完成：外键[" + attr.Key + ";" + attr.Desc + "],时出现错误。异常信息：" + ex.Message);
                    }
                    break;
                case BP.En.AutoFullWay.Way4_Dtl:
                    string mysql = "SELECT @Way(@Field) FROM @Table WHERE RefPK='"+ this.OID+"'";
                    string[] strs = attr.AutoFullDoc.Split('@');
                    foreach (string str in strs)
                    {
                        if (str == null || str.Length == 0)
                            continue;

                        string[] ss = str.Split('=');
                        mysql = mysql.Replace('@' + ss[0], ss[1]);
                    }
                    objval = DBAccess.RunSQLReturnString(mysql);
                    break;
                default:
                    throw new Exception("未涉及到的类型。");
            }
            if (objval == null)
                return;

            if (attr.IsNum)
            {
                try
                {
                    decimal d = decimal.Parse(objval);
                    this.SetValByKey(attr.Key, objval);
                }
                catch
                {
                }
            }
            else
            {
                this.SetValByKey(attr.Key, objval);
            }
            return;
        }
        public virtual string BeforeSave()
        {
            this.AutoFull(); /*处理自动计算。*/
           
            // 执行保存前的事件。
            this.HisNode.MapData.FrmEvents.DoEventNode(EventListOfNode.SaveBefore, this);
            return "";
        }
        #endregion

        #region  重写基类的方法。
        /// <summary>
        /// 按照指定的OID Insert.
        /// </summary>
        public void InsertAsOID(Int64 oid)
        {
            this.SetValByKey("OID", oid);
            //EnDA.Insert(this);
            this.RunSQL(SqlBuilder.Insert(this));
        }
        /// <summary>
        /// 按照指定的OID 保存
        /// </summary>
        /// <param name="oid"></param>
        public void SaveAsOID(Int64 oid)
        {
            this.SetValByKey("OID", oid);
            if (this.RetrieveNotSetValues().Rows.Count == 0)
                this.InsertAsOID(oid);
            this.Update();
        }
        /// <summary>
        /// 保存实体信息
        /// </summary>
        public new int Save()
        {
            if ( this.OID==0 || this.IsExits == false)
            {
                this.OID = DBAccess.GenerOID();
                this.InsertAsOID( this.OID);
                return 0;
            }
            else
            {
                this.Update();
                return 1;
            }
        }
        public void CopyCellsData(string fromRefVal)
        {
            if (this.OID == 0)
                throw new Exception("@OID Not set val  you can not copy it.");


            //Sys.DataCells dsc = new BP.Sys.DataCells(fromRefVal);
            //dsc.Delete(Sys.DataCellAttr.RefVal, "ND" + this.HisNode.NodeID + "_" + this.OID);

            //foreach (Sys.DataCell ds in dsc)
            //{
            //    Sys.DataCell en = new BP.Sys.DataCell();
            //    en.Copy(ds);
            //    en.RefVal = "ND" + this.HisNode.NodeID + "_" + this.OID;
            //    en.OID = 0;
            //    en.Insert();
            //}
        }

        public void CopyCellsData_bak(string fromRefVal)
        {
            if (this.OID == 0)
                throw new Exception("@OID Not set val  you can not copy it.");


            //Sys.DataCells dsc = new BP.Sys.DataCells(fromRefVal);
            //dsc.Delete(Sys.DataCellAttr.RefVal, "ND" + this.HisNode.NodeID + "_" + this.OID);

            //foreach (Sys.DataCell ds in dsc)
            //{
            //    Sys.DataCell en = new BP.Sys.DataCell();
            //    en.Copy(ds);
            //    en.RefVal = "ND" + this.HisNode.NodeID + "_" + this.OID;
            //    en.OID = 0;
            //    en.Insert();
            //}
        }
        public override void Copy(Entity fromEn)
        {
            if (fromEn == null)
                return;

            //if (this.EnMap.Attrs.Count > fromEn.EnMap.Attrs.Count)
            //{

            Attrs attrs = fromEn.EnMap.Attrs;
            foreach (Attr attr in attrs)
            {
                if (attr.Key == WorkAttr.CDT
                    || attr.Key == WorkAttr.NodeState
                    || attr.Key == WorkAttr.RDT
                    || attr.Key == WorkAttr.Rec
                    || attr.Key == "No"
                    || attr.Key == "Name"
                    || attr.Key == WorkAttr.Note
                    || attr.Key == WorkAttr.Sender)
                    continue;
                this.SetValByKey(attr.Key, fromEn.GetValByKey(attr.Key));
            }

            //}
            //else
            //{
            //    Attrs attrs = fromEn.EnMap.Attrs;
            //}
        }
        /// <summary>
        /// 删除主表数据也要删除它的明细数据
        /// </summary>
        protected override void afterDelete()
        {
            MapDtls dtls = this.HisNode.MapData.MapDtls; 
            foreach (MapDtl dtl in dtls)
                DBAccess.RunSQL("DELETE " + dtl.PTable + " WHERE RefPK=" + this.OID);

            base.afterDelete();
        }
        /// <summary>
        /// 结束前插入
        /// </summary>
        /// <returns></returns>
        protected override bool beforeInsert()
        {
            this.SetValByKey(WorkAttr.RDT, DataType.CurrentDataTime);

            //this.InitBiillNo();
            if (this.OID == 0)
                this.OID = DBAccess.GenerOID();

            return base.beforeInsert();
        }
        /// <summary>
        /// 防止在一次查询
        /// </summary>
        protected virtual void afterInsertUpdateAction()
        {
            return;
        }
        #endregion

        #region  公共方法
        /// <summary>
        /// 直接的保存前要做的工作
        /// </summary>
        protected virtual void BeforeDirectSave()
        {
        }
        /// <summary>
        /// 直接的保存
        /// </summary>
        public new void DirectSave()
        {
            this.BeforeDirectSave();
            this.beforeUpdateInsertAction();
            if (this.DirectUpdate() == 0)
            {
                this.SetValByKey(WorkAttr.RDT, DateTime.Now.ToString("yyyy-MM-dd"));
                this.DirectInsert();
            }
        }
        protected int _nodeID = 0;
        public int NodeID
        {
            get
            {
                if (_nodeID == 0)
                    throw new Exception("您没有给_Node给值。");
                return this._nodeID;
            }
            set
            {
                if (this._nodeID != value)
                {
                    this._nodeID = value;
                    this._enMap = null;
                }
                this._nodeID = value;
            }
        }
        //public override string ToString()
        //{
        //    return "ND" + this.NodeID;
        //}
        #endregion
    }
    /// <summary>
    /// 工作流程采集信息的基类 集合
    /// </summary>
    abstract public class Works : EntitiesMyPK
    {
        #region 构造方法
        /// <summary>
        /// 信息采集基类
        /// </summary>
        public Works()
        {
        }
        #endregion

        #region 查询方法
        /// <summary>
        /// 查询工作(不适合审核节点查询)
        /// </summary>
        /// <param name="empId">工作人员</param>
        /// <param name="nodeStat">节点状态</param>
        /// <param name="fromdate">记录日期从</param>
        /// <param name="todate">记录日期到</param>
        /// <returns></returns>
        public int Retrieve(string key, string empId, string fromdate, string todate)
        {
            QueryObject qo = new QueryObject(this);
                qo.AddWhere(WorkAttr.Rec, empId);

            qo.addAnd();
            qo.AddWhere(WorkAttr.RDT, ">=", fromdate);
            qo.addAnd();
            qo.AddWhere(WorkAttr.RDT, "<=", todate);

            if (key.Trim().Length == 0)
                return qo.DoQuery();
            else
            {
                if (key.IndexOf("%") == -1)
                    key = "%" + key + "%";
                Entity en = this.GetNewEntity;
                qo.addAnd();
                qo.addLeftBracket();
                qo.AddWhere(en.PK, " LIKE ", key);
                foreach (Attr attr in en.EnMap.Attrs)
                {
                    if (attr.MyFieldType == FieldType.RefText)
                        continue;
                    if (attr.UIContralType == UIContralType.DDL || attr.UIContralType == UIContralType.CheckBok)
                        continue;
                    qo.addOr();
                    qo.AddWhere(attr.Key, " LIKE ", key);
                }
                qo.addRightBracket();
                return qo.DoQuery();
            }
        }
        public int Retrieve(string fromDataTime, string toDataTime)
        {
            QueryObject qo = new QueryObject(this);
            qo.Top = 90000;
            qo.AddWhere(WorkAttr.RDT, " >=", fromDataTime);
            qo.addAnd();
            qo.AddWhere(WorkAttr.RDT, " <= ", toDataTime);
            return qo.DoQuery();
        }
        #endregion
    }
}
