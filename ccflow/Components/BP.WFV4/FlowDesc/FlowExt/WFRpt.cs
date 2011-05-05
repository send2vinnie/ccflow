using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.En;
using System.Collections;
using BP.Port;
using BP.Sys;

namespace BP.WF
{
    /// <summary>
    /// 流程报表
    /// </summary>
    public class WFRptAttr:BP.En.EntityNoNameAttr
    {
        #region 基本属性
        /// <summary>
        /// 类型
        /// </summary>
        public const string FK_FlowSort = "FK_FlowSort";
        /// <summary>
        /// 流程
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// 执行
        /// </summary>
        public const string DoWhat = "DoWhat";
        #endregion
    }
    /// <summary>
    /// 这里存放每个外部程序设置的信息.	 
    /// </summary>
    public class WFRpt : EntityNoName
    {
        #region 基本属性
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.IsUpdate = true;
                uac.IsDelete = true;
                return uac;
            }
        }
        /// <summary>
        /// 外部程序设置的事务编号
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.FK_Flow);
            }
            set
            {
                SetValByKey(NodeAttr.FK_Flow, value);
            }
        }
        public string FK_FlowT
        {
            get
            {
                return this.GetValRefTextByKey(NodeAttr.FK_Flow);
            }
        }
        public string FK_FlowSort
        {
            get
            {
                return this.GetValStringByKey(WFRptAttr.FK_FlowSort);
            }
            set
            {
                SetValByKey(WFRptAttr.FK_FlowSort, value);
            }
        }
        public string FK_FlowSortT
        {
            get
            {
                return this.GetValRefTextByKey(WFRptAttr.FK_FlowSort);
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 外部程序设置
        /// </summary>
        public WFRpt() { }
        /// <summary>
        /// 外部程序设置
        /// </summary>
        /// <param name="_oid">外部程序设置ID</param>	
        public WFRpt(string _oid)
        {
            this.No = _oid;
            this.Retrieve();
        }
        /// <summary>
        /// 重写基类方法
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("WF_Rpt");

                map.EnDesc = this.ToE("FlowView", "流程视图"); // "流程视图(您设计的报表系统会自动体现在前台)";
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBStringPK(WFRptAttr.No, null, this.ToE("View","视图"), true, true, 2, 20, 10);
                map.AddTBString(WFRptAttr.Name, null, this.ToE("RptName" , "报表名称"), true, false, 0, 400, 10);

                map.AddDDLEntities(WFRptAttr.FK_Flow, null,null, new Flows(), false);
                map.AddDDLEntities(WFRptAttr.FK_FlowSort, null, null, new FlowSorts(), false);

                map.AttrsOfOneVSM.Add(new RptStations(), new Stations(), RptStationAttr.FK_Rpt, EmpStationAttr.FK_Station, DeptAttr.Name, DeptAttr.No, this.ToE("StaRight", "岗位访问权限"));
                map.AttrsOfOneVSM.Add(new RptEmps(), new Emps(), RptEmpAttr.FK_Rpt, RptEmpAttr.FK_Emp, DeptAttr.Name, DeptAttr.No, this.ToE("EmpRight", "人员访问权限"));

                //map.AddTBInt(WFRptAttr.NodeID, 0, "NodeID", false, false);
                //map.AddDDLSysEnum(WFRptAttr.ShowTime, 0, "发生时间", true, false, WFRptAttr.ShowTime, "@0=无(显示在表单底部)@1=当工作选择时@2=当保存时@3=当发送时");
                //map.AddTBString(WFRptAttr.FK_Flow, null, "流程编号", false, true, 0, 100, 10);
                //map.AddTBString(WFRptAttr.DoWhat, null, "执行什么？", false, true, 0, 100, 10);
                //map.AddTBInt(WFRptAttr.H, 0, "窗口高度", false, false);
                //map.AddTBInt(WFRptAttr.W, 0, "窗口宽度", false, false);

                RefMethod rm = new RefMethod();
                rm.Title = this.ToE("ViewDef", "视图定义");  //"视图定义";
                rm.ClassMethodName = this.ToString() + ".DoField()";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("RptRun", "报表运行");  //"报表运行";
                rm.ClassMethodName = this.ToString() + ".DoOpenRpt()";
                rm.Icon = "/Images/Btn/Table.gif";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("NewRpt", "新建报表");  //"新建报表";
                rm.ClassMethodName = this.ToString() + ".DoNew()";
                rm.Icon = "/Images/Btn/New.gif";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("OperVideo", "录像帮助"); // "录像帮助";
                rm.ClassMethodName = this.ToString() + ".DoHelp()";
                rm.Icon = "/Images/FileType/rm.gif";
                map.AddRefMethod(rm);

                this._enMap = map;
                return this._enMap;
            }
        }
        public string DoNew()
        {
            WFRpt rpt = new WFRpt();
            rpt.No = "V" + this.FK_Flow;
            int i = 0;
            while (rpt.IsExits)
            {
                rpt.No = "V" + this.FK_Flow + "_" + i;
            }
            rpt.FK_Flow = this.FK_Flow;
            rpt.FK_FlowSort = this.FK_FlowSort;
            rpt.Name = this.ToE("NewView", "新建流程视图");  //"新建流程视图";
            rpt.Insert();

            BP.PubClass.WinOpen("../Comm/UIEn.aspx?EnsName=BP.WF.WFRpts&MyPK=" + rpt.No, 500, 600);
            return null;
        }
        public string DoHelp()
        {
            BP.PubClass.WinOpen("www.ccflow.cn" , 500, 600);
            return null;
        }
       
        
        public string DoPanelEns()
        {
            BP.PubClass.WinOpen("../Comm/PanelEns.aspx?EnsName=" + this.No,500,600);
            return null;
        }
        public string DoGroup()
        {
            BP.PubClass.WinOpen("../Comm/GroupEnsMNum.aspx?EnsName=" + this.No, 500, 600);
            return null;
        }
        public string DoPower()
        {
            BP.PubClass.WinOpen("");
            return null;
        }
        public string DoField()
        {
            BP.PubClass.WinOpen("../WF/Admin/WFRptD.aspx?FK_Flow=" + this.FK_Flow + "&DoType=Edit&RefNo=" + this.No, 700 , 400);
            return null;
        }

        public string DoOpenRpt()
        {
            BP.PubClass.WinOpen("../WF/SelfWFRpt.aspx?FK_Flow=" + this.FK_Flow + "&DoType=Edit&RefNo=" + this.No, 700, 400);
            return null;
        }
        
        /// <summary>
        /// 初试化属性
        /// </summary>
        /// <returns></returns>
        public void DoInitAttrs()
        {
            MapData md = new MapData();
            md.No = this.No;
            md.RetrieveFromDBSources();
            if (md.SearchKeys == "")
                md.SearchKeys = "@FK_Dept@FK_NY@";

            md.No = this.Name;
            md.Save();

            RptAttrs attrs = new RptAttrs(this.No);
            RptAttr rptAttr = new RptAttr();

            MapAttr mattrN = new MapAttr(); //临时变量。

            Flow fl = new Flow(this.FK_Flow);
            MapAttrs mAttrsStartNode = new MapAttrs("ND" + int.Parse(this.FK_Flow) + "01"); // 
            string startNodeID = int.Parse(this.FK_Flow).ToString() + "01";
            if (attrs.Contains(RptAttrAttr.Field, "OID") == false)
            {
                /* 是否包含流程状态 */
                MapAttr mattr = mAttrsStartNode.GetEntityByKey(MapAttrAttr.KeyOfEn, "OID") as MapAttr;
                rptAttr.MyPK = this.No + "_OID";

                rptAttr.FK_Rpt = this.No;
                rptAttr.FK_Node = startNodeID;
                rptAttr.RefTable = "ND" + rptAttr.FK_Node;
                rptAttr.RefField = "OID";
                rptAttr.RefFieldName = "工作ID";

                rptAttr.Field = "OID";
                rptAttr.RefFieldName = "工作ID";
                rptAttr.IsCanDel = false;
                rptAttr.IsCanEdit = false;
                rptAttr.Insert();
            }
            mattrN = new MapAttr(this.No, "OID");
            if (mattrN.MyPK.Length == 0)
            {
                mattrN.FK_MapData = rptAttr.FK_Rpt;
               // mattrN.OID = 0;
                mattrN.Name = "工作ID";
                mattrN.KeyOfEn = "OID";
                mattrN.MyDataType = BP.DA.DataType.AppInt;
                mattrN.DefVal="0" ; 
                mattrN.UIVisible=true;
                mattrN.UIContralType= UIContralType.TB;
                mattrN.Insert();
            }

            if (attrs.Contains(RptAttrAttr.Field, "WFState") == false)
            {
                /* 是否包含流程状态 */
                MapAttr mattr = mAttrsStartNode.GetEntityByKey(MapAttrAttr.KeyOfEn, "WFState") as MapAttr;

                rptAttr.MyPK = this.No + "_WFState";
                rptAttr.FK_Rpt = this.No;
                rptAttr.FK_Node = startNodeID;

                rptAttr.RefTable = "ND" + rptAttr.FK_Node;
                rptAttr.RefField = "WFState";
                rptAttr.RefFieldName = this.ToE("FlowState", "流程状态"); //"流程状态";

                rptAttr.Field = "WFState";
                rptAttr.RefFieldName = this.ToE("FlowState", "流程状态");  //"流程状态";
                rptAttr.IsCanDel = false;
                rptAttr.IsCanEdit = false;
                rptAttr.Insert();
            }
            mattrN = new MapAttr(this.No, "WFState");
            if (mattrN.MyPK.Length == 0)
            {
                mattrN.FK_MapData = rptAttr.FK_Rpt;
                //mattrN.OID = 0;
                mattrN.Name = this.ToE("FlowState", "流程状态"); // "流程状态";
                mattrN.KeyOfEn = "WFState";
                mattrN.MyDataType = BP.DA.DataType.AppInt;
                mattrN.DefVal = "0";
                mattrN.UIVisible = true;
                mattrN.UIContralType = UIContralType.DDL;
                mattrN.UIBindKey = "WFState";
                mattrN.LGType = FieldTypeS.Enum;
                mattrN.Insert();
            }


            if (attrs.Contains(RptAttrAttr.Field, "RDT") == false)
            {
                /* 是否包含流程状态 */
                MapAttr mattr = mAttrsStartNode.GetEntityByKey(MapAttrAttr.KeyOfEn, "RDT") as MapAttr;

                rptAttr.MyPK = this.No + "_RDT";
                rptAttr.FK_Rpt = this.No;
                rptAttr.FK_Node = startNodeID;

                rptAttr.RefTable = "ND" + rptAttr.FK_Node;
                rptAttr.RefField = "RDT";
                rptAttr.RefFieldName = this.ToE("StartDate", "发起日期");  //"发起日期";

                rptAttr.Field = "RDT";
                rptAttr.RefFieldName = this.ToE("StartDate","发起日期");  //"发起日期";
                rptAttr.IsCanDel = false;
                rptAttr.IsCanEdit = false;
                rptAttr.Insert();
            }

            mattrN = new MapAttr(this.No, "RDT");
            if (mattrN.MyPK.Length == 0)
            {
                mattrN.FK_MapData = rptAttr.FK_Rpt;
                //mattrN.OID = 0;
                mattrN.Name = this.ToE("StartDate", "发起日期");  //"发起日期";
                mattrN.KeyOfEn = "RDT";
                mattrN.MyDataType = BP.DA.DataType.AppDate;
                mattrN.DefVal = "";
                mattrN.UIVisible = true;
                mattrN.UIContralType = UIContralType.TB;
                mattrN.Insert();
            }


            if (attrs.Contains(RptAttrAttr.Field, "FK_NY") == false)
            {
                /* 是否包含流程状态 */
                rptAttr.MyPK = this.No + "_FK_NY";
                rptAttr.FK_Rpt = this.No;
                rptAttr.FK_Node = startNodeID;

                rptAttr.RefTable = "ND" + rptAttr.FK_Node;
                rptAttr.RefField = "FK_NY";
                rptAttr.RefFieldName = this.ToE("BNY", "隶属年月"); //"隶属年月";

                rptAttr.Field = "FK_NY";
                rptAttr.RefFieldName = this.ToE("BNY", "隶属年月");  //"隶属年月";
                rptAttr.IsCanDel = false;
                rptAttr.IsCanEdit = false;
                rptAttr.Insert();
            }

            mattrN = new MapAttr(this.No, "FK_NY");
            if (mattrN.MyPK.Length == 0)
            {
                mattrN.FK_MapData = rptAttr.FK_Rpt;
              //  mattrN.OID = 0;
                mattrN.Name = "年月";
                mattrN.KeyOfEn = "FK_NY";
                mattrN.MyDataType = BP.DA.DataType.AppString;
                mattrN.DefVal = "";
                mattrN.UIVisible = true;
                mattrN.UIContralType = UIContralType.DDL;
                mattrN.LGType = FieldTypeS.FK;

                mattrN.UIBindKey = "BP.Pub.NYs";
                mattrN.Insert();
            }

            if (attrs.Contains(RptAttrAttr.Field, "FK_Dept") == false)
            {
                /* 是否包含流程状态 */
                rptAttr.MyPK = this.No + "FK_Dept";
                rptAttr.FK_Rpt = this.No;
                rptAttr.FK_Node = startNodeID;

                rptAttr.RefTable = "ND" + rptAttr.FK_Node;
                rptAttr.RefField = "FK_Dept";
                rptAttr.RefFieldName = this.ToE("Dept", "部门"); // "部门";

                rptAttr.Field = "FK_Dept";
                rptAttr.RefFieldName = this.ToE("Dept", "部门"); // "部门";
                rptAttr.IsCanDel = false;
                rptAttr.IsCanEdit = false;
                rptAttr.Insert();
            }

            mattrN = new MapAttr(this.No, "FK_Dept");
            if (mattrN.MyPK.Length == 0)
            {
                mattrN.FK_MapData = rptAttr.FK_Rpt;
               // mattrN.OID = 0;
                mattrN.Name = this.ToE("Dept", "部门"); // "部门";
                mattrN.KeyOfEn = "FK_Dept";
                mattrN.MyDataType = BP.DA.DataType.AppString;
                mattrN.DefVal = "";
                mattrN.UIVisible = true;
                mattrN.UIContralType = UIContralType.DDL;
                mattrN.LGType = FieldTypeS.FK;

                mattrN.UIBindKey = "BP.Port.Depts";
                mattrN.Insert();
            }

            if (attrs.Contains(RptAttrAttr.Field, "MyNum") == false)
            {
                /* 是否包含流程状态 */
                rptAttr.MyPK = this.No + "_MyNum";
                rptAttr.FK_Rpt = this.No;
                rptAttr.FK_Node = startNodeID;

                rptAttr.RefTable = "ND" + rptAttr.FK_Node;
                rptAttr.RefField = "MyNum";
                rptAttr.RefFieldName = this.ToE("Num","个数"); // "个数";

                rptAttr.Field = "MyNum";
                rptAttr.RefFieldName = this.ToE("Num", "个数");  //"个数";
                rptAttr.IsCanDel = false;
                rptAttr.IsCanEdit = false;
                rptAttr.Insert();
            }
            MapAttr attrMyNum = new MapAttr(this.No, "MyNum");
            if (attrMyNum.MyPK.Length == 0)
            {
                attrMyNum.MyDataType = BP.DA.DataType.AppInt;
                attrMyNum.KeyOfEn = "MyNum";
                attrMyNum.Name = this.ToE("Num", "个数"); // "个数";
                attrMyNum.HisEditType = BP.En.EditType.UnDel;
                attrMyNum.DefVal = "0";
                attrMyNum.UIContralType = UIContralType.TB;
                attrMyNum.UIVisible = true;
                attrMyNum.LGType = FieldTypeS.Normal;
                attrMyNum.IDX = -1000;
                attrMyNum.Insert();
            }
            this.DoGenerView();
        }
        public string DoGenerView()
        {
            RptAttrs attrs = new RptAttrs(this.No);
            Nodes nds = new Nodes(this.FK_Flow);

            #region 删除目标对象
            try
            {
                BP.DA.DBAccess.RunSQL("drop view " + this.No);
            }
            catch
            {
            }

            try
            {
                BP.DA.DBAccess.RunSQL("drop table " + this.No);
            }
            catch
            {
            }
            #endregion 删除目标对象

            string nodeid = int.Parse(this.FK_Flow) + "01";

            try
            {
                BP.DA.DBAccess.RunSQL("drop view " + this.No);
            }
            catch
            {
                try
                {
                    BP.DA.DBAccess.RunSQL("drop table " + this.No);
                }
                catch
                {
                }
            }

            string sql = "CREATE  VIEW " + this.No + " AS SELECT @";
            foreach (RptAttr attr in attrs)
            {
                Node nd = nds.GetEntityByKey(attr.FK_Node) as Node;
                if (nd == null)
                {
                    attr.Delete(); //删除已经不存在的节点。
                    continue;
                }


                sql += "," + attr.RefTable + "." + attr.RefField + " AS " + attr.Field;
            }
            attrs = new RptAttrs(this.No);

            sql += " FROM @";

            DataTable dt = DBAccess.RunSQLReturnTable(" SELECT DISTINCT RefTable, FK_Node FROM WF_RptAttr WHERE WF_RptAttr.FK_RPT='" + this.No + "'");
            foreach (DataRow dr in dt.Rows)
            {
                int mynodeid = int.Parse(dr[1].ToString());
                Node nd = nds.GetEntityByKey(mynodeid) as Node;

                
                    sql += "," + dr[0].ToString() + "";
            }
            sql += " WHERE @";

            foreach (DataRow dr in dt.Rows)
            {
                int mynodeid = int.Parse(dr[1].ToString());
                Node nd = nds.GetEntityByKey(mynodeid) as Node;

                
                    sql += "AND ND" + nodeid + ".OID=" + dr[0].ToString() + ".OID ";
            }
            sql = sql.Replace("@,", "");
            sql = sql.Replace("@AND", "");

            #region 检查数据表是否存在，不存在就CREATE.
            Flow fl = new Flow(this.FK_Flow);
            nds = fl.HisNodes;
            foreach (Node nd in nds)
            {
                
                //nd.HisWork.CheckPhysicsTable();
            }
            #endregion 检查数据表是否存在，不存在就CREATE.

            DBAccess.RunSQL(sql);
            BP.SystemConfig.DoClearCash();
            return null;
        }
        protected override void afterInsert()
        {
            this.DoInitAttrs();
            base.afterInsert();
        }
        protected override bool beforeUpdateInsertAction()
        {
            MapData md = new MapData();
            md.No = this.No;
            md.RetrieveFromDBSources();
            md.Name = this.Name;
            md.Save();
            return base.beforeUpdateInsertAction();
        }
        protected override void afterDelete()
        {
            RptAttrs attrs = new RptAttrs();
            attrs.Delete(RptAttrAttr.FK_Rpt, this.No);
            

            MapData md = new MapData();
            md.No = this.No;
            md.Delete();
            try
            {
                BP.DA.DBAccess.RunSQL("drop view " + this.No);
            }
            catch
            {
                try
                {
                    BP.DA.DBAccess.RunSQL("drop table " + this.No);
                }
                catch
                {
                }
            }
            base.afterDelete();
        }
        #endregion
    }
    /// <summary>
    /// 外部程序设置集合
    /// </summary>
    public class WFRpts : EntitiesNoName
    {
        #region 方法
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new WFRpt();
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 外部程序设置集合
        /// </summary>
        public WFRpts()
        {
        }
        /// <summary>
        /// 外部程序设置集合.
        /// </summary>
        /// <param name="FlowNo"></param>
        public WFRpts(string fk_flow)
        {
            this.Retrieve(NodeAttr.FK_Flow, fk_flow);
        }
        public WFRpts(int nodeid)
        {
            this.Retrieve(NodeAttr.NodeID, nodeid);
        }
        #endregion
    }
}
