using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.Port;
using BP.En;
using BP.Web;

namespace BP.WF
{
    /// <summary>
    /// 抄送方式
    /// </summary>
    public enum CCWay
    {
        /// <summary>
        /// 按照信息发送
        /// </summary>
        ByMsg,
        /// <summary>
        /// 按照e-mail
        /// </summary>
        ByEmail,
        /// <summary>
        /// 按照电话
        /// </summary>
        ByPhone,
        /// <summary>
        /// 按照数据库功能
        /// </summary>
        ByDBFunc
    }
    /// <summary>
    /// 抄送类型
    /// </summary>
    public enum CCType
    {
        /// <summary>
        /// 不抄送
        /// </summary>
        None,
        /// <summary>
        /// 按人员
        /// </summary>
        AsEmps,
        /// <summary>
        /// 按岗位
        /// </summary>
        AsStation,
        /// <summary>
        /// 按节点
        /// </summary>
        AsNode,
        /// <summary>
        /// 按部门
        /// </summary>
        AsDept,
        /// <summary>
        /// 按照部门与岗位
        /// </summary>
        AsDeptAndStation
    }
    /// <summary>
    /// 行文类型
    /// </summary>
    public enum XWType
    {
        /// <summary>
        /// 上行文
        /// </summary>
        Up,
        /// <summary>
        /// 平行文
        /// </summary>
        Line,
        /// <summary>
        /// 下行文
        /// </summary>
        Down
    }
    /// <summary>
    /// 公文类型
    /// </summary>
    public enum DocType
    {
        /// <summary>
        /// 正式的
        /// </summary>
        OfficialDoc,
        /// <summary>
        /// 便函
        /// </summary>
        UnOfficialDoc,
        /// <summary>
        /// 其它
        /// </summary>
        Etc
    }
    /// <summary>
    /// 流程表单类型
    /// </summary>
    public enum FlowSheetType
    {
        /// <summary>
        /// 表单流程
        /// </summary>
        SheetFlow,
        /// <summary>
        /// 公文流程
        /// </summary>
        DocFlow
    }
    /// <summary>
    /// 流程类型
    /// </summary>
    public enum FlowType
    {
        /// <summary>
        /// 平面流程
        /// </summary>
        Panel,
        /// <summary>
        /// 分合流
        /// </summary>
        FHL
    }
    /// <summary>
    /// 流程状态
    /// </summary>
    public enum WFState
    {
        /// <summary>
        /// 运行状态
        /// </summary>
        Runing,
        /// <summary>
        /// 完成状态
        /// </summary>
        Complete,
        /// <summary>
        /// 强制终止
        /// </summary>
        Stop,
        /// <summary>
        /// 删除
        /// </summary>
        Delete
    }
    /// <summary>
    /// 流程启动类型
    /// </summary>
    public enum FlowRunWay
    {
        /// <summary>
        /// 手工启动
        /// </summary>
        HandWork,
        /// <summary>
        /// 按照月份
        /// </summary>
        ByMonth,
        /// <summary>
        /// 按照周
        /// </summary>
        ByWeek,
        /// <summary>
        /// 按照天
        /// </summary>
        ByDay,
        /// <summary>
        /// 按小时
        /// </summary>
        ByHours
    }
    /// <summary>
    /// 流程属性
    /// </summary>
    public class FlowAttr : EntityNoNameAttr
    {
        #region base attrs
        /// <summary>
        /// CCType
        /// </summary>
        public const string CCType = "CCType";
        /// <summary>
        /// 抄送方式
        /// </summary>
        public const string CCWay = "CCWay";
        /// <summary>
        /// 流程类别
        /// </summary>
        public const string FK_FlowSort = "FK_FlowSort";
        /// <summary>
        /// 建立的日期。
        /// </summary>
        public const string CreateDate = "CreateDate";
        /// <summary>
        /// BookTable
        /// </summary>
        public const string BookTable = "BookTable";
        /// <summary>
        /// 开始工作节点类型
        /// </summary>
        public const string StartNodeType = "StartNodeType";
        /// <summary>
        /// StartNodeID
        /// </summary>
        public const string StartNodeID = "StartNodeID";
        /// <summary>
        /// 能不能流程Num考核。
        /// </summary>
        public const string IsCanNumCheck = "IsCanNumCheck";
        /// <summary>
        /// DateLit
        /// </summary>
        public const string DateLit = "DateLit";
        /// <summary>
        /// 是否显示附件
        /// </summary>
        public const string IsFJ = "IsFJ";
        #endregion base attrs

        /// <summary>
        /// 是否起用
        /// </summary>
        public const string IsOK = "IsOK";
        public const string CCStas = "CCStas";
        public const string IsCCAll = "IsCCAll";
        public const string Note = "Note";
        /// <summary>
        /// 流程类型
        /// </summary>
        public const string FlowType = "FlowType";
        /// <summary>
        /// 平均用天
        /// </summary>
        public const string AvgDay = "AvgDay";
        public const string FlowSheetType = "FlowSheetType";
        /// <summary>
        /// 文档类型
        /// </summary>
        public const string DocType = "DocType";
        /// <summary>
        /// 行文类型
        /// </summary>
        public const string XWType = "XWType";

        /// <summary>
        /// 流程运行类型
        /// </summary>
        public const string FlowRunWay = "FlowRunWay";
        /// <summary>
        /// 运行的设置
        /// </summary>
        public const string FlowRunObj = "FlowRunObj";
    }
    /// <summary>
    /// 流程
    /// 记录了流程的信息．
    /// 流程的编号，名称，建立时间．
    /// </summary>
    public class Flow : EntityNoName
    {
        /// <summary>
        /// 运行类型
        /// </summary>
        public FlowRunWay HisFlowRunWay
        {
            get
            {
                return (FlowRunWay)this.GetValIntByKey(FlowAttr.FlowRunWay);
            }
            set
            {
                this.SetValByKey(FlowAttr.FlowRunWay, (int)value);
            }
        }
        /// <summary>
        /// 运行对象
        /// </summary>
        public string FlowRunObj
        {
            get
            {
                return this.GetValStrByKey(FlowAttr.FlowRunObj);
            }
            set
            {
                this.SetValByKey(FlowAttr.FlowRunObj, value);
            }
        }
        /// <summary>
        /// 流程表单类型
        /// </summary>
        public FlowSheetType HisFlowSheetType
        {
            get
            {
                return (FlowSheetType)this.GetValIntByKey(FlowAttr.FlowSheetType);
            }
            set
            {
                this.SetValByKey(FlowAttr.FlowSheetType, (int)value);
            }
        }
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                if (Web.WebUser.No == "admin")
                    uac.IsUpdate = true;
                return uac;
            }
        }
        /// <summary>
        /// 文书物理表
        /// </summary>
        public string BookTable_del
        {
            get
            {
                string book = this.GetValStringByKey(FlowAttr.BookTable);
                if (book == null || book == "")
                {
                    book = "BK_" + this.HisStartNode.HisWork.EnMap.PhysicsTable;
                    this.SetValByKey(FlowAttr.BookTable, book);
                    this.Update();
                }
                return book;
            }
        }
       
        /// <summary>
        /// 执行复制
        /// </summary>
        /// <returns></returns>
        public string DoCopy()
        {
            return null;

            Flow fl = new Flow();
            fl.Copy(this);
            fl.No = this.GenerNewNoByKey(FlowAttr.No);
            fl.Name = "复制:" + this.Name;
            fl.Insert();

            Nodes nds = this.HisNodes;
            foreach (Node nd in nds)
            {
                Node nnd = new Node();
                nnd.Copy(nd);
            }
        }
        /// <summary>
        /// 校验流程
        /// </summary>
        /// <returns></returns>
        public string DoCheck()
        {
            string msg = "<font color=blue>" + this.ToE("About", "关于") + "《" + this.Name + " 》 " + this.ToE("FlowCheckInfo", "流程检查信息") + "</font><hr>";
            Nodes nds = new Nodes(this.No);
            Emps emps = new Emps();

            string rpt = "<html><title>" + this.Name + "-" + this.ToE("DesignRpt", "流程诊断报告") + "</title></html>";
            rpt += "\t\n<body>\t\n<table width='70%' align=center border=1>\t\n<tr>\t\n<td>";
            rpt += "<div aligen=center><h1><b>" + this.Name + " - " + this.ToE("FlowDRpt", "流程设计文档") + "</b></h1></div>";
            rpt += "<b>" + this.ToE("FlowDesc", "流程设计") + ":</b><br>";
            rpt += "&nbsp;&nbsp;" + this.Note;
            rpt += "<br><b>" + this.ToE("Info", "信息") + ":</b><br>";

            BookTemplates bks = new BookTemplates(this.No);

            rpt += "&nbsp;&nbsp;" + this.ToE("Step", "步骤") + ":" + nds.Count + "，" + this.ToE("Book", "文书") + ":" + bks.Count + "个。";

            #region 对节点进行检查
            foreach (Node nd in nds)
            {
                try
                {
                    if (nd.IsCheckNode == false)
                        nd.HisWork.CheckPhysicsTable();
                }
                catch (Exception ex)
                {
                    rpt += "@" + nd.Name + " , 节点类型NodeWorkTypeText=" + nd.NodeWorkTypeText + "出现错误.@err=" + ex.Message;
                }


                rpt += "<hr><b>" + this.ToEP1("NStep", "@第{0}步", nd.Step.ToString()) + "：" + nd.Name + "：</b><br>";
                rpt += this.ToE("Info", "信息") + "：";
                if (nd.IsCheckNode)
                {
                    rpt += this.ToE("CHTableDesc", "审核人、审核意见、审核时间。"); // "审核人、审核意见、审核时间。";
                }
                else
                {
                    BP.Sys.MapAttrs attrs = new BP.Sys.MapAttrs("ND" + nd.NodeID);
                    foreach (BP.Sys.MapAttr attr in attrs)
                    {
                        rpt += attr.KeyOfEn + " " + attr.Name + "、";
                    }
                }

                // 明细表检查。
                Sys.MapDtls dtls = new BP.Sys.MapDtls("ND" + nd.NodeID);
                foreach (Sys.MapDtl dtl in dtls)
                {
                    dtl.HisGEDtl.CheckPhysicsTable();

                    rpt += "<br>" + this.ToE("Dtl", "明细") + "：" + dtl.Name;
                    BP.Sys.MapAttrs attrs = new BP.Sys.MapAttrs(dtl.No);
                    foreach (BP.Sys.MapAttr attr in attrs)
                    {
                        rpt += attr.KeyOfEn + " " + attr.Name + "、";
                    }
                }

                rpt += "<br>" + this.ToE("Station", "岗位") + "：";

                // 岗位是否设置正确。
                msg += "<b>《" + nd.Name + "》：</b>" + this.ToE("NodeWorkType", "节点类型") + "：" + nd.HisNodeWorkType + "<hr>";
                if (nd.HisStations.Count == 0)
                {
                    msg += "<font color=red>" + this.ToE("Error", "错误") + "：" + nd.Name + " " + this.ToE("NoSetSta", "没有设置岗位") + "。</font>";
                }
                else
                {
                    msg += this.ToE("Station", "岗位") + "：";
                    foreach (BP.Port.Station st in nd.HisStations)
                    {
                        msg += st.Name + "、";
                        rpt += st.Name + "、";
                    }

                    emps.RetrieveInSQL("select fk_emp from port_empstation where fk_station in (select fk_station from wf_nodestation where fk_node='" + nd.NodeID + "' )");
                    if (emps.Count == 0)
                    {

                        msg += "<font color=red>" + this.ToE("F0", "岗位人员设置错误：虽然您设置了岗位，但是岗位上没有相关人员执行，流程到此步骤不能正常运行，请在用户维护里维护岗位。") + "</font>";
                    }
                    else
                    {
                        msg += this.ToE("F1", "本节点可执行的工作人员如下");
                        foreach (Emp emp in emps)
                        {
                            msg += emp.No + emp.Name + "、";
                        }
                    }
                }

                msg += "<br>";

                //对文书进行检查。
                BookTemplates books = nd.HisBookTemplates;
                if (books.Count == 0)
                {
                    msg += "";
                }
                else
                {
                    msg += this.ToE("Book", "文书");
                    rpt += "<br>" + this.ToE("Book", "文书") + "：";
                    foreach (BookTemplate book in books)
                    {
                        msg += book.Name + "、";
                        rpt += book.Name + "、";
                    }
                }
                msg += "<br>";
                //外部程序调用接口检查
                FAppSets sets = new FAppSets(nd.NodeID);
                if (sets.Count == 0)
                {
                    //msg += " 。";
                }
                else
                {
                    msg += ":";
                    foreach (FAppSet set in sets)
                    {
                        msg += set.Name + "," + this.ToE("DoWhat", "执行内容") + ":" + set.DoWhat + " 、";
                    }
                }
                msg += "<br>";

                // 节点完成条件的定义.
                Conds conds = new Conds(CondType.Node, nd.NodeID, 1 );
                if (conds.Count == 0)
                {
                    //msg += " 。";
                }
                else
                {
                    msg += " " + this.ToE("DirCond", "方向条件") + ":";
                    rpt += "<br>" + this.ToE("DirCond", "方向条件") + "：";

                    foreach (Cond cond in conds)
                    {
                        Node ndOfCond = new Node();
                        ndOfCond.NodeID = ndOfCond.NodeID;
                        if (ndOfCond.RetrieveFromDBSources() == 0)
                        {
                            msg += "<font color=red>设定的方向条件的节点已经被删除了，所以系统自动删除了这个条件。</font>";
                            cond.Delete();
                            continue;
                        }

                        try
                        {
                            if (ndOfCond.HisWork.EnMap.Attrs.Contains(cond.AttrKey) == false)
                                throw new Exception("属性:" + cond.AttrKey + " , " + cond.AttrName + " 不存在。");
                        }
                        catch (Exception ex)
                        {
                            msg += "<font color=red>" + ex.Message + "</font>";
                            ndOfCond.Delete();
                        }

                        msg += cond.AttrKey + cond.AttrName + cond.OperatorValue + "、";
                        rpt += cond.AttrKey + cond.AttrName + cond.OperatorValue + "、";
                    }
                }
                msg += "<br>";
            }
            #endregion

            msg += "<br><a href='../../Data/FlowDesc/" + this.Name + ".htm' target=_s>" + this.ToE("DesignRpt", "设计报告") + "</a>";
            msg += "<hr><b> </b> &nbsp; <br>" + DataType.CurrentDataTimeCN + "<br><br><br>";

            rpt += "\t\n</td></tr>\t\n</table>\t\n</body>\t\n</html>";

            try
            {
                BP.DA.DataType.SaveAsFile(BP.SystemConfig.PathOfData + "\\FlowDesc\\" + this.Name + ".htm", rpt);
            }
            catch
            {
            }

            msg += "<hr>开始检查基础数据是否完整<hr>";

            emps = new Emps();
            emps.RetrieveAllFromDBSource();
            foreach (Emp emp in emps)
            {
                Dept dept = new Dept();
                dept.No = emp.FK_Dept;
                if (dept.IsExits == false)
                    msg += "@人员:" + emp.Name + "，部门编号{" + dept.Name + "}不正确.";

                EmpStations ess = new EmpStations();
                ess.Retrieve(EmpStationAttr.FK_Emp, emp.No);
                if (ess.Count == 0)
                    msg += "@人员:" + emp.No + "," + emp.Name + ",没有工作岗位。";


                EmpDepts eds = new EmpDepts();
                eds.Retrieve(EmpStationAttr.FK_Emp, emp.No);
                if (eds.Count == 0)
                    msg += "@人员:" + emp.No + "," + emp.Name + ",没有工作部门。";
            }

            ////Depts depts =new Depts();
            ////depts.RetrieveAll();
            //string sql = "SELECT * FROM PORT_EMP WHERE FK_DEPT NOT IN (SELECT NO FROM PORT_DEPT)";


            return msg;
        }

        public string DoCheck_del()
        {
            string msg = "<font color=blue>" + this.ToE("About", "关于") + "《" + this.Name + " 》 " + this.ToE("FlowCheckInfo", "流程检查信息") + "</font><hr>";
            Nodes nds = new Nodes(this.No);
            Emps emps = new Emps();

            string rpt = "<html><title>" + this.Name + "-" + this.ToE("DesignRpt", "流程诊断报告") + "</title></html>";
            rpt += "\t\n<body>\t\n<table width='70%' align=center border=1>\t\n<tr>\t\n<td>";
            rpt += "<div aligen=center><h1><b>" + this.Name + " - " + this.ToE("FlowDRpt", "流程设计文档") + "</b></h1></div>";
            rpt += "<b>" + this.ToE("FlowDesc", "流程设计") + ":</b><br>";
            rpt += "&nbsp;&nbsp;" + this.Note;
            rpt += "<br><b>" + this.ToE("Info", "信息") + ":</b><br>";

            BookTemplates bks = new BookTemplates(this.No);
            rpt += "&nbsp;&nbsp;" + this.ToE("Step", "步骤") + ":" + nds.Count + "，" + this.ToE("Book", "文书") + ":" + bks.Count + "个。";

            #region 对节点进行检查
            foreach (Node nd in nds)
            {
                if (nd.IsCheckNode == false)
                    nd.HisWork.CheckPhysicsTable();


                rpt += "<hr><b>" + this.ToEP1("NStep", "@第{0}步", nd.Step.ToString()) + "：" + nd.Name + "：</b><br>";
                rpt += this.ToE("Info", "信息") + "：";
                if (nd.IsCheckNode)
                {
                    rpt += this.ToE("CHTableDesc", "审核人、审核意见、审核时间。"); // "审核人、审核意见、审核时间。";
                }
                else
                {
                    BP.Sys.MapAttrs attrs = new BP.Sys.MapAttrs("ND" + nd.NodeID);
                    foreach (BP.Sys.MapAttr attr in attrs)
                    {
                        rpt += attr.KeyOfEn + " " + attr.Name + "、";
                    }
                }
                // 明细表检查。
                Sys.MapDtls dtls = new BP.Sys.MapDtls("ND" + nd.No);
                foreach (Sys.MapDtl dtl in dtls)
                {
                    dtl.HisGEDtl.CheckPhysicsTable();

                    rpt += "<br>" + this.ToE("Dtl", "明细") + "：" + dtl.Name;
                    BP.Sys.MapAttrs attrs = new BP.Sys.MapAttrs(dtl.No);
                    foreach (BP.Sys.MapAttr attr in attrs)
                    {
                        rpt += attr.KeyOfEn + " " + attr.Name + "、";
                    }
                }
                rpt += "<br>" + this.ToE("Station", "岗位") + "：";

                // 岗位是否设置正确。
                msg += "<b>《" + nd.Name + "》：</b>" + this.ToE("NodeWorkType", "节点类型") + "：" + nd.HisNodeWorkType + "<hr>";
                if (nd.HisStations.Count == 0)
                {
                    msg += "<font color=red>" + this.ToE("Error", "错误") + "：" + nd.Name + " " + this.ToE("NoSetSta", "没有设置岗位") + "。</font>";
                }
                else
                {
                    msg += this.ToE("Station", "岗位") + "：";
                    foreach (BP.Port.Station st in nd.HisStations)
                    {
                        msg += st.Name + "、";
                        rpt += st.Name + "、";
                    }

                    emps.RetrieveInSQL("select fk_emp from port_empstation where fk_station in (select fk_station from wf_nodestation where fk_node='" + nd.NodeID + "' )");
                    if (emps.Count == 0)
                    {

                        msg += "<font color=red>" + this.ToE("F0", "岗位人员设置错误：虽然您设置了岗位，但是岗位上没有相关人员执行，流程到此步骤不能正常运行，请在用户维护里维护岗位。") + "</font>";
                    }
                    else
                    {
                        msg += this.ToE("F1", "本节点可执行的工作人员如下");
                        foreach (Emp emp in emps)
                        {
                            msg += emp.No + emp.Name + "、";
                        }
                    }
                }

                msg += "<br>";

                //对文书进行检查。
                BookTemplates books = nd.HisBookTemplates;
                if (books.Count == 0)
                {
                    msg += "";
                }
                else
                {
                    msg += this.ToE("Book", "文书");
                    rpt += "<br>" + this.ToE("Book", "文书") + "：";
                    foreach (BookTemplate book in books)
                    {
                        msg += book.Name + "、";
                        rpt += book.Name + "、";
                    }
                }
                msg += "<br>";
                //外部程序调用接口检查
                FAppSets sets = new FAppSets(nd.NodeID);
                if (sets.Count == 0)
                {
                    //msg += " 。";
                }
                else
                {
                    msg += ":";
                    foreach (FAppSet set in sets)
                    {
                        msg += set.Name + "," + this.ToE("DoWhat", "执行内容") + ":" + set.DoWhat + " 、";
                    }
                }
                msg += "<br>";


                // 节点完成条件的定义.
                Conds conds = new Conds(CondType.Node, nd.NodeID, 1);
                if (conds.Count == 0)
                {
                    //msg += " 。";
                }
                else
                {
                    msg += " " + this.ToE("DirCond", "方向条件") + ":";
                    rpt += "<br>" + this.ToE("DirCond", "方向条件") + "：";

                    foreach (Cond cond in conds)
                    {
                        msg += cond.AttrKey + cond.AttrName + cond.OperatorValue + "、";
                        rpt += cond.AttrKey + cond.AttrName + cond.OperatorValue + "、";
                    }
                }
                msg += "<br>";
            }
            #endregion

            msg += "<br><a href='../../Data/FlowDesc/" + this.Name + ".htm' target=_s>" + this.ToE("DesignRpt", "设计报告") + "</a>";
            msg += "<hr><b> </b> &nbsp; <br>" + DataType.CurrentDataTimeCN + "<br><br><br>";

            rpt += "\t\n</td></tr>\t\n</table>\t\n</body>\t\n</html>";
            BP.DA.DataType.SaveAsFile(BP.SystemConfig.PathOfData + "\\FlowDesc\\" + this.Name + ".htm", rpt);
            return msg;
        }

        #region 基本属性
        public bool IsCCAll
        {
            get
            {
                return this.GetValBooleanByKey(FlowAttr.IsCCAll);
            }
            set
            {
                this.SetValByKey(FlowAttr.IsCCAll, value);
            }
        }
        public bool IsFJ_del
        {
            get
            {
                return this.GetValBooleanByKey(FlowAttr.IsFJ);
            }
            set
            {
                this.SetValByKey(FlowAttr.IsFJ, value);
            }
        }
        /// <summary>
        /// 许诺完成期限
        /// </summary>
        public int DateLit
        {
            get
            {
                return this.GetValIntByKey(FlowAttr.DateLit);
            }
            set
            {
                this.SetValByKey(FlowAttr.DateLit, value);
            }
        }
        public decimal AvgDay
        {
            get
            {
                return this.GetValIntByKey(FlowAttr.AvgDay);
            }
            set
            {
                this.SetValByKey(FlowAttr.AvgDay, value);
            }
        }
        public int StartNodeID
        {
            get
            {
                return int.Parse(this.No + "01");
                //return this.GetValIntByKey(FlowAttr.StartNodeID);
            }
        }
        public string EnName
        {
            get
            {
                return "ND" + this.StartNodeID;
            }
        }

        /// <summary>
        /// 流程类别
        /// </summary>
        public string FK_FlowSort
        {
            get
            {
                return this.GetValStringByKey(FlowAttr.FK_FlowSort);
            }
            set
            {
                this.SetValByKey(FlowAttr.FK_FlowSort, value);
            }
        }
        public string FK_FlowSortText
        {
            get
            {
                return this.GetValRefTextByKey(FlowAttr.FK_FlowSort);
            }
        }
        /// <summary>
        /// 要抄送的岗位
        /// </summary>
        public string CCStas
        {
            get
            {
                return this.GetValStringByKey(FlowAttr.CCStas);
            }
            set
            {
                this.SetValByKey(FlowAttr.CCStas, value);
            }
        }
        #endregion

        #region 计算属性
        /// <summary>
        /// 流程类型(大的类型)
        /// </summary>
        public int FlowType_del
        {
            get
            {
                return this.GetValIntByKey(FlowAttr.FlowType);
            }
        }
        /// <summary>
        /// 是否自动流程
        /// </summary>
        public bool IsAutoWorkFlow_del
        {
            get
            {
                return false;
                /*
                if (this.FlowType==1)
                    return true;
                else
                    return false;
                    */
            }
        }
        public string Note
        {
            get
            {
                return this.GetValStringByKey("Note");
            }
        }
        /// <summary>
        /// 是否多线程自动流程
        /// </summary>
        public bool IsMutiLineWorkFlow_del
        {
            get
            {
                return false;
                /*
                if (this.FlowType==2 || this.FlowType==1 )
                    return true;
                else
                    return false;
                    */
            }
        }
        #endregion

        #region 扩展属性
        public DocType HisDocType
        {
            get
            {
                return (DocType)this.GetValIntByKey(FlowAttr.DocType);
            }
            set
            {
                this.SetValByKey(FlowAttr.DocType, (int)value);
            }
        }
        public string DocTypeT
        {
            get
            {
                return  this.GetValRefTextByKey(FlowAttr.DocType);
            }
        }
        /// <summary>
        /// 流程类型
        /// </summary>
        public FlowType HisFlowType
        {
            get
            {
                return (FlowType)this.GetValIntByKey(FlowAttr.FlowType);
            }
            set
            {
                this.SetValByKey(FlowAttr.FlowType, (int)value);
            }
        }
        /// <summary>
        /// 行文类型
        /// </summary>
        public XWType HisXWType
        {
            get
            {
                return (XWType)this.GetValIntByKey(FlowAttr.XWType);
            }
            set
            {
                this.SetValByKey(FlowAttr.XWType, (int)value);
            }
        }
        public string HisXWTypeT
        {
            get
            {
                return this.GetValRefTextByKey(FlowAttr.XWType);
            }
        }
        /// <summary>
        /// 节点
        /// </summary>
        public Nodes _HisNodes = null;
        /// <summary>
        /// 他的节点集合.
        /// </summary>
        public Nodes HisNodes
        {
            get
            {
                if (this._HisNodes == null)
                    _HisNodes = new Nodes(this.No);
                return _HisNodes;
            }
            set
            {
                _HisNodes = value;
            }
        }
        /// <summary>
        /// 他的 Start 节点
        /// </summary>
        public Node HisStartNode
        {
            get
            {

                foreach (Node nd in this.HisNodes)
                {
                    if (nd.IsStartNode)
                        return nd;
                }
                throw new Exception("@没有找到他的开始节点,工作流程[" + this.Name + "]定义错误.");
            }
        }
        /// <summary>
        /// 他的事务类别
        /// </summary>
        public FlowSort HisFlowSort
        {
            get
            {
                return new FlowSort(this.FK_FlowSort);
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 流程
        /// </summary>
        public Flow()
        {
        }
        /// <summary>
        /// 流程
        /// </summary>
        /// <param name="_No">编号</param>
        public Flow(string _No)
        {
            this.No = _No;
            if (SystemConfig.IsDebug)
            {
                int i = this.RetrieveFromDBSources();
                if (i == 0)
                    throw new Exception("流程编号不存在");
            }
            else
            {
                this.Retrieve();
            }
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

                Map map = new Map("WF_Flow");

                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = this.ToE("Flow", "流程");
                map.CodeStruct = "3";

                map.AddTBStringPK(FlowAttr.No, null, null, true, true, 1, 10, 3);
                map.AddTBString(FlowAttr.Name, null, null, true, false, 0, 50, 10);
                map.AddDDLEntities(FlowAttr.FK_FlowSort, "01", this.ToE("FlowSort", "流程类别"), new FlowSorts(), false);


                map.AddTBInt(FlowAttr.FlowType, (int)FlowType.Panel, "流程类型", false, false);

                // @0=业务流程@1=公文流程.
                map.AddTBInt(FlowAttr.FlowSheetType, (int)FlowSheetType.SheetFlow, "表单类型", false, false);


                map.AddDDLSysEnum(FlowAttr.DocType, (int)DocType.OfficialDoc, "公文类型(对公文有效)", false, false, FlowAttr.DocType,"@0=正式公文@1=便函");
                map.AddDDLSysEnum(FlowAttr.XWType, (int)XWType.Down, "行文类型(对公文有效)", false, false, FlowAttr.XWType, "@0=上行文@1=平行文@2=下行文");


                map.AddDDLSysEnum(FlowAttr.FlowRunWay, (int)FlowRunWay.HandWork, "运行方式", false, false, FlowAttr.FlowRunWay,
                    "@0=手工启动@1=按月启动@2=按天启动@3=按周启动@4=按天启动@5=按小时启动");
                map.AddTBString(FlowAttr.FlowRunObj, null, "运行内容", true, false, 0, 100, 10);

                map.AddTBString(FlowAttr.Note, null, null, true, false, 0, 100, 10);
                map.AddTBInt(FlowAttr.DateLit, 30, "许诺期限(0表示不下达)", false, false);


                map.AddBoolean(FlowAttr.IsOK, true, "是否起用", true, true);
                map.AddBoolean(FlowAttr.IsCCAll, false, "流程完成后抄送参与人员", true, true);
                map.AddTBString(FlowAttr.CCStas, null, "要抄送的岗位", false, false, 0, 2000, 10);

                map.AddTBDecimal(FlowAttr.AvgDay, 0, "平均运行用天", false, false);


                map.AddSearchAttr(FlowAttr.FK_FlowSort);

                RefMethod rm = new RefMethod();
                rm.Title = this.ToE("DesignCheckRpt", "设计检查报告"); // "设计检查报告";
                rm.ToolTip = "检查流程设计的问题。";
                rm.Icon = "/Images/Btn/Confirm.gif";
                rm.ClassMethodName = this.ToString() + ".DoCheck";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("ViewDef", "视图定义"); //"视图定义";
                rm.Icon = "/Images/Btn/View.gif";
                rm.ClassMethodName = this.ToString() + ".DoDRpt";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("RptRun", "报表运行"); // "报表运行";
                rm.ClassMethodName = this.ToString() + ".DoOpenRpt()";
                //rm.Icon = "/Images/Btn/Table.gif";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("FlowDataOut", "数据转出定义");  //"数据转出定义";
                //  rm.Icon = "/Images/Btn/Table.gif";
                rm.ToolTip = "在流程完成时间，流程数据转储存到其它系统中应用。";

                rm.ClassMethodName = this.ToString() + ".DoExp";
                map.AddRefMethod(rm);

                map.AttrsOfOneVSM.Add(new FlowStations(), new Stations(), FlowStationAttr.FK_Flow, FlowStationAttr.FK_Station, DeptAttr.Name, DeptAttr.No, "抄送岗位");

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion


        protected override bool beforeUpdateInsertAction()
        {
            if (this.FK_FlowSort == "00")
                this.HisFlowSheetType = FlowSheetType.DocFlow;
            else
                this.HisFlowSheetType = FlowSheetType.SheetFlow;

            return base.beforeUpdateInsertAction();
        }

        #region  公共方法
        /// <summary>
        /// 设计数据转出
        /// </summary>
        /// <returns></returns>
        public string DoExp()
        {
            this.DoCheck();
            PubClass.WinOpen("./../WF/Admin/Exp.aspx?CondType=0&FK_Flow=" + this.No, "文书", "cdsn", 800, 500, 210, 300);
            return null;
        }
        /// <summary>
        /// 定义报表
        /// </summary>
        /// <returns></returns>
        public string DoDRpt()
        {
            this.DoCheck();
            PubClass.WinOpen("./../WF/Admin/WFRpt.aspx?CondType=0&FK_Flow=" + this.No, "文书", "cdsn", 800, 500, 210, 300);
            return null;
        }
        /// <summary>
        /// 运行报表
        /// </summary>
        /// <returns></returns>
        public string DoOpenRpt()
        {
            BP.PubClass.WinOpen("../WF/SelfWFRpt.aspx?FK_Flow=" + this.No + "&DoType=Edit&RefNo=" + this.No, 700, 400);
            return null;
        }
        /// <summary>
        /// 执行新建
        /// </summary>
        public void DoNewFlow()
        {
            this.No = this.GenerNewNoByKey(FlowAttr.No);
            if (this.No.Substring(0, 1) == "1")
                this.No = "100";

            this.Name = BP.Sys.Language.GetValByUserLang("NewFlow", "新建流程") + this.No; //新建流程
            this.Save();


            Node nd = new Node();
            nd.NodeID = int.Parse(this.No + "01");
            nd.Name = BP.Sys.Language.GetValByUserLang("StartNode", "开始节点");//  "开始节点"; 
            nd.Step = 1;
            nd.FK_Flow = this.No;
            nd.FlowName = this.Name;
            nd.HisNodePosType = NodePosType.Start;
            nd.HisNodeWorkType = NodeWorkType.StartWork;
            nd.X = 100;
            nd.Y = 100;
            nd.Save();
            nd.CreateMap();


            nd = new Node();
            nd.NodeID = int.Parse(this.No + "99");
            nd.Name = BP.Sys.Language.GetValByUserLang("EndNode", "结束节点"); // "结束节点";
            nd.Step = 1;
            nd.FK_Flow = this.No;
            nd.FlowName = this.Name;
            nd.HisNodePosType = NodePosType.End;
            nd.HisNodeWorkType = NodeWorkType.WorkHL;
            nd.X = 100;
            nd.Y = 200;
            nd.Save();
            nd.CreateMap();
        }
        protected override bool beforeUpdate()
        {
            Node.CheckFlow(this);
            return base.beforeUpdate();
        }
        public void DoDelete()
        {
            string sql = "";
            sql = " DELETE wf_chofflow WHERE FK_FLOW='" + this.No + "'";
            DBAccess.RunSQL(sql);

            sql = " DELETE wf_generworkerlist WHERE FK_FLOW='" + this.No + "'";
            DBAccess.RunSQL(sql);

            sql = " DELETE wf_generworkflow WHERE FK_FLOW='" + this.No + "'";
            DBAccess.RunSQL(sql);

            // 删除岗位节点。
            sql = "  DELETE from wf_NodeStation  where   FK_Node in (select nodeid from wf_node where fk_flow='" + this.No + "')";
            DBAccess.RunSQL(sql);

            // 删除方向。
            sql = "  DELETE from wf_direction  where   node in (select nodeid from wf_node where fk_flow='" + this.No + "')";
            DBAccess.RunSQL(sql);
            sql = "  DELETE from wf_direction  where   tonode in (select nodeid from wf_node where fk_flow='" + this.No + "')";
            DBAccess.RunSQL(sql);

            //// 删除方向,条件.
            //sql = "  DELETE from wf_nodecompletecondition  where   nodeid in (select nodeid from wf_node where fk_flow='" + this.No + "')";
            //DBAccess.RunSQL(sql);
            //sql = "  DELETE from wf_globalcompletecondition  where   fk_flow='" + this.No + "'";
            //DBAccess.RunSQL(sql);
            //sql = "  DELETE from wf_directioncondition  where   nodeid in (select nodeid from wf_node where fk_flow='" + this.No + "')";
            //DBAccess.RunSQL(sql);

            // 删除报表
            WFRpts rpts = new WFRpts(this.No);
            rpts.Delete();

            // 外部程序设置
            sql = " DELETE WF_FAppSet WHERE  NodeID in (select NodeID from WF_Node where fk_flow='" + this.No + "')";
            DBAccess.RunSQL(sql);

            // 删除文书
            sql = " DELETE WF_BookTemplate WHERE  NodeID in (SELECT NodeID from WF_Node where fk_flow='" + this.No + "')";
            DBAccess.RunSQL(sql);

            Nodes nds = new Nodes(this.No);
            foreach (Node nd in nds)
            {
                sql = " DELETE Sys_MapData WHERE No='ND" + nd.NodeID + "'";
                DBAccess.RunSQL(sql);

                sql = " DELETE Sys_MapAttr WHERE FK_MapData='ND" + nd.NodeID + "'";
                DBAccess.RunSQL(sql);

                if (nd.IsCheckNode == false)
                {
                    try
                    {
                        sql = " DROP TABLE ND" + nd.NodeID;
                        DBAccess.RunSQL(sql);
                    }
                    catch
                    {
                    }
                }


                // 删除明细信息。
                Sys.MapDtls dtls = new BP.Sys.MapDtls("ND" + nd.NodeID);
                dtls.Delete();
            }

            sql = " DELETE WF_Node WHERE FK_FLOW='" + this.No + "'";
            DBAccess.RunSQL(sql);

            sql = " DELETE WF_LabNote WHERE FK_FLOW='" + this.No + "'";
            DBAccess.RunSQL(sql);

            this.Delete();
        }
        #endregion
    }
    /// <summary>
    /// 流程集合
    /// </summary>
    public class Flows : EntitiesNoName
    {
        #region 查询
        /// <summary>
        /// 查出来全部的自动流程
        /// </summary>
        public void RetrieveIsAutoWorkFlow()
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(FlowAttr.FlowType, 1);

            qo.addOrderBy(FlowAttr.No);

            qo.DoQuery();
        }
        /// <summary>
        /// 查询出来全部的在生存期间内的流程
        /// </summary>
        /// <param name="flowSort">流程类别</param>
        /// <param name="IsCountInLifeCycle">是不是计算在生存期间内 true 查询出来全部的 </param>
        public void Retrieve(string flowSort)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(FlowAttr.FK_FlowSort, flowSort);
            qo.addOrderBy(FlowAttr.No);
            qo.DoQuery();
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 工作流程
        /// </summary>
        public Flows() { }
        /// <summary>
        /// 工作流程
        /// </summary>
        /// <param name="fk_sort"></param>
        public Flows(string fk_sort)
        {
            this.Retrieve(FlowAttr.FK_FlowSort, fk_sort);
        }
        #endregion

        #region 得到实体
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Flow();
            }
        }
        #endregion
    }
}

