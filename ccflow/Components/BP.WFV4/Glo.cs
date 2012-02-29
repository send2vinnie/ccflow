using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading;
using System.Diagnostics;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Win32;   
using BP.Sys;
using BP.DA;
using BP.En;
using BP;

namespace BP.WF
{
    public class Glo
    {
        /// <summary>
        /// 装载流程数据 
        /// </summary>
        /// <param name="xlsFile"></param>
        public static string LoadFlowDataWithToSpecNode(string xlsFile)
        {
            DataTable dt = BP.DBLoad.GetTableByExt(xlsFile);
            string err = "";
            string info = "";

            foreach (DataRow dr in dt.Rows)
            {
                string flowPK = dr["FlowPK"].ToString();
                string starter = dr["Starter"].ToString();
                string executer = dr["Executer"].ToString();
                int toNode = int.Parse(dr["ToNodeID"].ToString().Replace("ND", ""));
                Node nd = new Node();
                nd.NodeID = toNode;
                if (nd.RetrieveFromDBSources() == 0)
                {
                    err += "节点ID错误:" + toNode;
                    continue;
                }
                string sql = "SELECT count(*) as Num FROM ND" + int.Parse(nd.FK_Flow) + "01 WHERE FlowPK='" + flowPK + "'";
                int i = DBAccess.RunSQLReturnValInt(sql);
                if (i == 1)
                    continue; // 此数据已经调度了。

                #region 检查数据是否完整。
              

                BP.Port.Emp emp = new BP.Port.Emp();
                emp.No = executer;
                if (emp.RetrieveFromDBSources() == 0)
                {
                    err += "@账号:" + starter + ",不存在。";
                    continue;
                }
                if (string.IsNullOrEmpty(emp.FK_Dept))
                {
                    err += "@账号:" + starter + ",没有部门。";
                    continue;
                }

                emp.No = starter;
                if (emp.RetrieveFromDBSources() == 0)
                {
                    err += "@账号:" + executer + ",不存在。";
                    continue;
                }
                if (string.IsNullOrEmpty(emp.FK_Dept))
                {
                    err += "@账号:" + executer + ",没有部门。";
                    continue;
                }
                #endregion 检查数据是否完整。

                BP.Web.WebUser.SignInOfGener(emp);
                Flow fl = nd.HisFlow;
                Work wk = fl.NewWork();
                foreach (DataColumn dc in dt.Columns)
                    wk.SetValByKey(dc.ColumnName.Trim(), dr[dc.ColumnName].ToString().Trim());

                wk.SetValByKey(WorkAttr.Rec, Web.WebUser.No);
                wk.SetValByKey(StartWorkAttr.FK_Dept, Web.WebUser.FK_Dept);
                wk.SetValByKey("FK_NY", DataType.CurrentYearMonth);
                wk.SetValByKey(WorkAttr.MyNum, 1);
                wk.Update();

                Node ndStart = nd.HisFlow.HisStartNode;
                WorkNode wn = new WorkNode(wk, ndStart);
                wn.JumpToNode = nd;
                wn.JumpToEmp = executer;
                try
                {
                    info += "<hr>" + wn.AfterNodeSave();
                }
                catch (Exception ex)
                {
                    err += "<hr>" + ex.Message;
                    WorkFlow wf = new WorkFlow(fl, wk.OID);
                    wf.DoDeleteWorkFlowByReal();
                }
            }
            return info + err;
        }
        public static string LoadFlowDataWithToSpecEndNode(string xlsFile)
        {
            DataTable dt = BP.DBLoad.GetTableByExt(xlsFile);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.WriteXml("C:\\已完成.xml");

            string err = "";
            string info = "";
            int idx = 0;
            foreach (DataRow dr in dt.Rows)
            {
                string flowPK = dr["FlowPK"].ToString().Trim();
                if (string.IsNullOrEmpty(flowPK))
                    continue;

                string starter = dr["Starter"].ToString();
                string executer = dr["Executer"].ToString();
                int toNode = int.Parse(dr["ToNodeID"].ToString().Replace("ND", ""));
                Node ndOfEnd = new Node();
                ndOfEnd.NodeID = toNode;
                if (ndOfEnd.RetrieveFromDBSources() == 0)
                {
                    err += "节点ID错误:" + toNode;
                    continue;
                }

                if (ndOfEnd.IsEndNode == false)
                {
                    err += "节点ID错误:" + toNode + ", 非结束节点。";
                    continue;
                }

                string sql = "SELECT count(*) as Num FROM ND" + int.Parse(ndOfEnd.FK_Flow) + "01 WHERE FlowPK='" + flowPK + "'";
                int i = DBAccess.RunSQLReturnValInt(sql);
                if (i == 1)
                    continue; // 此数据已经调度了。

                #region 检查数据是否完整。
                //发起人发起。
                BP.Port.Emp emp = new BP.Port.Emp();
                emp.No = executer;
                if (emp.RetrieveFromDBSources() == 0)
                {
                    err += "@账号:" + starter + ",不存在。";
                    continue;
                }

                if (string.IsNullOrEmpty(emp.FK_Dept))
                {
                    err += "@账号:" + starter + ",没有设置部门。";
                    continue;
                }

                emp = new BP.Port.Emp();
                emp.No = starter;
                if (emp.RetrieveFromDBSources()==0)
                {
                    err += "@账号:" + starter + ",不存在。";
                    continue;
                }
                else
                {
                    emp.RetrieveFromDBSources();
                    if (string.IsNullOrEmpty(emp.FK_Dept))
                    {
                        err += "@账号:" + starter + ",没有设置部门。";
                        continue;
                    }
                }
                #endregion 检查数据是否完整。


                BP.Web.WebUser.SignInOfGener(emp);
                Flow fl = ndOfEnd.HisFlow;
                Work wk = fl.NewWork();
                foreach (DataColumn dc in dt.Columns)
                    wk.SetValByKey(dc.ColumnName.Trim(), dr[dc.ColumnName].ToString().Trim());

                wk.SetValByKey(WorkAttr.Rec, Web.WebUser.No);
                wk.SetValByKey(StartWorkAttr.FK_Dept, Web.WebUser.FK_Dept);
                wk.SetValByKey("FK_NY", DataType.CurrentYearMonth);
                wk.SetValByKey(WorkAttr.MyNum, 1);
                wk.Update();

                Node ndStart =fl.HisStartNode;
                WorkNode wn = new WorkNode(wk, ndStart);
                wn.JumpToNode = ndOfEnd;
                wn.JumpToEmp = executer;
                try
                {
                    info += "<hr>" + wn.AfterNodeSave();
                }
                catch (Exception ex)
                {
                    err += "<hr>启动错误:" + ex.Message;
                    DBAccess.RunSQL("DELETE FROM ND" + int.Parse(ndOfEnd.FK_Flow) + "01 WHERE FlowPK='" + flowPK + "'");
                    WorkFlow wf = new WorkFlow(fl, wk.OID);
                    wf.DoDeleteWorkFlowByReal();
                    continue;
                }

                //结束点结束。
                emp = new BP.Port.Emp(executer);
                BP.Web.WebUser.SignInOfGener(emp);

                Work wkEnd = ndOfEnd.GetWork(wk.OID);
                foreach (DataColumn dc in dt.Columns)
                    wkEnd.SetValByKey(dc.ColumnName.Trim(), dr[dc.ColumnName].ToString().Trim());

                wkEnd.SetValByKey(WorkAttr.Rec, Web.WebUser.No);
                wkEnd.SetValByKey(StartWorkAttr.FK_Dept, Web.WebUser.FK_Dept);
                wkEnd.SetValByKey("FK_NY", DataType.CurrentYearMonth);
                wkEnd.SetValByKey(WorkAttr.MyNum, 1);

                wkEnd.Update();
                try
                {
                    WorkNode wnEnd = new WorkNode(wkEnd, ndOfEnd);
                  //  wnEnd.AfterNodeSave();
                    info += "<hr>" + wnEnd.AfterNodeSave();
                }
                catch (Exception ex)
                {
                    err += "<hr>结束错误(系统直接删除它):" + ex.Message;
                    WorkFlow wf = new WorkFlow(fl, wk.OID);
                    wf.DoDeleteWorkFlowByReal();
                    continue;
                }
            }
            return info + err;
        }
        public static void ResetFlowView()
        {
            string sql = "DROP VIEW V_WF_Data ";
            try
            {
                BP.DA.DBAccess.RunSQL(sql);
            }
            catch
            {
            }

            Flows fls = new Flows();
            fls.RetrieveAll();
            sql = "CREATE VIEW V_WF_Data AS ";
            foreach (Flow fl in fls)
            {
                fl.CheckRpt();
                sql += "\t\n SELECT '" + fl.No + "' as FK_Flow, '" + fl.Name + "' AS FlowName, '" + fl.FK_FlowSort + "' as FK_FlowSort,CDT,Emps,FID,FK_Dept,FK_NY,";
                sql += "MyNum,OID,RDT,Rec,Title,WFState,FlowEmps,";
                sql += "FlowStarter,FlowStartRDT,FlowEnder,FlowEnderRDT,FlowDaySpan FROM ND" + int.Parse(fl.No) + "Rpt";
                sql += "\t\n  UNION";
            }
            sql = sql.Substring(0, sql.Length - 6);
            sql += "\t\n GO";
            BP.DA.DBAccess.RunSQL(sql);
        }
        public static void Rtf2PDF(object pathOfRtf, object pathOfPDF)
        {
            Object Nothing = System.Reflection.Missing.Value;
            //创建一个名为WordApp的组件对象    
            Microsoft.Office.Interop.Word.Application wordApp =
    new Microsoft.Office.Interop.Word.ApplicationClass();
            //创建一个名为WordDoc的文档对象并打开    
            Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Open(ref pathOfRtf, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
     ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
    ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);

            //设置保存的格式    
            object filefarmat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;

            //保存为PDF    
            doc.SaveAs(ref pathOfPDF, ref filefarmat, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
    ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
     ref Nothing, ref Nothing, ref Nothing);
            //关闭文档对象    
            doc.Close(ref Nothing, ref Nothing, ref Nothing);
            //推出组建    
            wordApp.Quit(ref Nothing, ref Nothing, ref Nothing);
            GC.Collect();
        }

        #region 属性
        public static string SessionMsg
        {
            get
            {
                Paras p = new Paras();
                p.SQL = "SELECT Msg FROM WF_Emp where No=@FK_Emp";
                p.AddFK_Emp();
                return DBAccess.RunSQLReturnString(p);
            }
            set
            {
                Paras p = new Paras();
                p.SQL = "UPDATE WF_Emp SET Msg=" + SystemConfig.AppCenterDBVarStr + "v WHERE No=" + SystemConfig.AppCenterDBVarStr + "FK_Emp";
                p.AddFK_Emp();
                p.Add("v", value);
                DBAccess.RunSQL(p);
            }
        }
        #endregion 属性

        #region 属性
        private static string _FromPageType = null;
        public static string FromPageType
        {
            get
            {
                _FromPageType = null;
                if (_FromPageType == null)
                {
                    try
                    {
                        string url = System.Web.HttpContext.Current.Request.RawUrl;
                        int i = url.LastIndexOf("/") + 1;
                        int i2 = url.IndexOf(".aspx") - 6;

                        url = url.Substring(i);
                        url = url.Substring(0, url.IndexOf(".aspx"));
                        _FromPageType = url;
                        if (_FromPageType.Contains("SmallSingle"))
                            _FromPageType = "SmallSingle";
                        else if (_FromPageType.Contains("Small"))
                            _FromPageType = "Small";
                        else
                            _FromPageType = "";
                    }
                    catch (Exception ex)
                    {
                        _FromPageType = "";
                        //  throw new Exception(ex.Message + url + " i=" + i + " i2=" + i2);
                    }
                }
                return _FromPageType;
            }
        }

        private static Attrs _AttrsOfRpt = null;
        public static Attrs AttrsOfRpt
        {
            get
            {
                if (_AttrsOfRpt == null)
                {
                    _AttrsOfRpt = new Attrs();
                    _AttrsOfRpt.AddTBInt(GERptAttr.OID, 0, "WorkID", true, true);
                    _AttrsOfRpt.AddTBInt(GERptAttr.FID, 0, "FlowID", false, false);

                    _AttrsOfRpt.AddTBString(GERptAttr.Title, null, "标题", true, false, 0, 10, 10);
                    _AttrsOfRpt.AddTBString(GERptAttr.FlowStarter, null, "发起人", true, false, 0, 10, 10);
                    _AttrsOfRpt.AddTBString(GERptAttr.FlowStartRDT, null, "发起时间", true, false, 0, 10, 10);

                    _AttrsOfRpt.AddTBString(GERptAttr.WFState, null, "状态", true, false, 0, 10, 10);

                    //Attr attr = new Attr();
                    //attr.Desc = "流程状态";
                    //attr.Key = "WFState";
                    //attr.MyFieldType = FieldType.Enum;
                    //attr.UIBindKey = "WFState";
                    //attr.UITag = "@0=进行中@1=已经完成";

                    _AttrsOfRpt.AddDDLSysEnum(GERptAttr.WFState, 0, "流程状态", true, true, GERptAttr.WFState);

                    _AttrsOfRpt.AddTBString(GERptAttr.FlowEmps, null, "参与人", true, false, 0, 10, 10);
                    _AttrsOfRpt.AddTBString(GERptAttr.FlowEnder, null, "结束人", true, false, 0, 10, 10);
                    _AttrsOfRpt.AddTBString(GERptAttr.FlowEnderRDT, null, "结束时间", true, false, 0, 10, 10);
                    _AttrsOfRpt.AddTBDecimal(GERptAttr.FlowDaySpan, 0, "跨度(天)", true, false);
                    //_AttrsOfRpt.AddTBString(GERptAttr.FK_NY, null, "隶属月份", true, false, 0, 10, 10);
                }
                return _AttrsOfRpt;
            }
        }
        #endregion 属性

        /// <summary>
        /// 安装包
        /// </summary>
        public static void DoInstallDataBase(string lang,string yunXingHuanjing)
        {
            ArrayList al = null;
            string info = "BP.En.Entity";
            al = BP.DA.ClassFactory.GetObjects(info);

            #region 1, 修复表
            foreach (Object obj in al)
            {
                Entity en = null;
                en = obj as Entity;
                if (en == null)
                    continue;

                string table = null;
                try
                {
                    table = en.EnMap.PhysicsTable;
                    if (table == null)
                        continue;
                }
                catch
                {
                    continue;
                }

                switch (table)
                {
                    case "WF_EmpWorks":
                    case "WF_GenerEmpWorkDtls":
                    case "WF_GenerEmpWorks":
                    case "WF_NodeExt":
                        continue;
                    case "Sys_Enum":
                        en.CheckPhysicsTable();
                        break;
                    default:
                        en.CheckPhysicsTable();
                        break;
                }

                en.PKVal = "123";
                try
                {
                    en.RetrieveFromDBSources();
                }
                catch (Exception ex)
                {
                    Log.DebugWriteWarning(ex.Message);
                    en.CheckPhysicsTable();
                }
            }
            #endregion 修复

            #region 2, 注册枚举类型 sql
            // 2, 注册枚举类型。
            BP.Sys.Xml.EnumInfoXmls xmls = new BP.Sys.Xml.EnumInfoXmls();
            xmls.RetrieveAll();
            foreach (BP.Sys.Xml.EnumInfoXml xml in xmls)
            {
                BP.Sys.SysEnums ses = new BP.Sys.SysEnums();
                ses.RegIt(xml.Key, xml.Vals);
            }
            #endregion 注册枚举类型

            #region 3, 执行基本的 sql
            string sqlscript =  SystemConfig.PathOfData + "\\Install\\SQLScript\\Port_" + yunXingHuanjing + "_" + lang + ".sql";
            BP.DA.DBAccess.RunSQLScript(sqlscript);
            #endregion 修复

            #region 4, 创建视图与系统函数
            sqlscript =  SystemConfig.PathOfData + "\\Install\\SQLScript\\CreateViewSQL.sql";
            BP.DA.DBAccess.RunSQLScript(sqlscript);
            #endregion 创建视图与系统函数

            #region 5, 初始化数据。
            sqlscript =  SystemConfig.PathOfData + "\\Install\\SQLScript\\InitPublicData.sql" ;
            BP.DA.DBAccess.RunSQLScript(sqlscript);
            #endregion 初始化数据

            #region 6, 生成临时的wf数据。
            BP.Port.Emps emps = new BP.Port.Emps();
            emps.RetrieveAllFromDBSource();
            int i = 0;
            foreach (BP.Port.Emp emp in emps)
            {
                i++;
                BP.WF.Port.WFEmp wfEmp = new BP.WF.Port.WFEmp();
                wfEmp.Copy(emp);
                if (wfEmp.Email.Length == 0)
                    wfEmp.Email = emp.No + "@ccflow.org";

                if (wfEmp.Tel.Length == 0)
                    wfEmp.Tel = "88553730-6" + i.ToString().PadLeft(2, '0');

                if (wfEmp.IsExits)
                    wfEmp.Update();
                else
                    wfEmp.Insert();
            }

            // 生成简历数据.
            int oid = 1000;
            foreach (BP.Port.Emp emp in emps)
            {
                for (int myIdx = 0; myIdx < 6; myIdx++)
                {
                    BP.WF.Demo.Resume re = new Demo.Resume();
                    re.NianYue = "200" + myIdx + "年01月";
                    re.FK_Emp = emp.No;
                    re.GongZuoDanWei = "工作部门-" + myIdx;
                    re.ZhengMingRen = "张" + myIdx;
                    re.BeiZhu = emp.Name + "同志工作认真.";
                    oid++;
                    re.InsertAsOID(oid);
                }
            }
            #endregion 初始化数据
        }
        public static void KillProcess(string processName) //杀掉进程的方法
        {
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process pro in processes)
            {
                string name = pro.ProcessName + ".exe";
                if (name.ToLower() == processName.ToLower())
                    pro.Kill();
            }
        }
        public static string GenerFlowNo(string rptKey)
        {
            rptKey = rptKey.Replace("ND", "");
            rptKey = rptKey.Replace("Rpt", "");
            switch (rptKey.Length)
            {
                case 0:
                    return "001";
                case 1:
                    return "00" + rptKey;
                case 2:
                    return "0" + rptKey;
                case 3:
                    return rptKey;
                default:
                    return "001";
            }
            return rptKey;
        }
        /// <summary>
        /// 
        /// </summary>
        public static bool IsShowFlowNum
        {
            get
            {
                switch (SystemConfig.AppSettings["IsShowFlowNum"])
                {
                    case "1":
                        return true;
                    default:
                        return false;
                }
            }
        }
        /// <summary>
        /// 仅仅显示用户编号？
        /// 考虑到中文的操作系统，与英文的操作系统不同。
        /// 
        /// </summary>
        public static bool IsShowUserNoOnly
        {
            get
            {
                switch (SystemConfig.AppSettings["IsShowUserNoOnly"])
                {
                    case "1":
                        return true;
                    default:
                        return false;
                }
            }
        }
        /// <summary>
        /// 产生word文档.
        /// </summary>
        /// <param name="wk"></param>
        public static void GenerWord(object filename, Work wk)
        {
            BP.WF.Glo.KillProcess("WINWORD.EXE");
            string enName = wk.EnMap.PhysicsTable;
            try
            {
                RegistryKey delKey = Registry.LocalMachine.OpenSubKey(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Shared Tools\Text Converters\Import\",
                    true);
                delKey.DeleteValue("MSWord6.wpc");
                delKey.Close();
            }
            catch
            {
            }

            GroupField currGF = new GroupField();
            MapAttrs mattrs = new MapAttrs(enName);
            GroupFields gfs = new GroupFields(enName);
            MapDtls dtls = new MapDtls(enName);
            foreach (MapDtl dtl in dtls)
                dtl.IsUse = false;

            // 计算出来单元格的行数。
            int rowNum = 0;
            foreach (GroupField gf in gfs)
            {
                rowNum++;
                bool isLeft = true;
                foreach (MapAttr attr in mattrs)
                {
                    if (attr.UIVisible == false)
                        continue;

                    if (attr.GroupID != gf.OID)
                        continue;

                    if (attr.UIIsLine)
                    {
                        rowNum++;
                        isLeft = true;
                        continue;
                    }

                    if (isLeft == false)
                        rowNum++;
                    isLeft = !isLeft;
                }
            }

            rowNum = rowNum + 2 + dtls.Count;

            // 创建Word文档
            string CheckedInfo = "";
            string message = "";
            Object Nothing = System.Reflection.Missing.Value;

            //  object filename = fileName;

            Word.Application WordApp = new Word.ApplicationClass();
            Word.Document WordDoc = WordApp.Documents.Add(ref  Nothing, ref  Nothing, ref  Nothing, ref  Nothing);
            try
            {
                WordApp.ActiveWindow.View.Type = Word.WdViewType.wdOutlineView;
                WordApp.ActiveWindow.View.SeekView = Word.WdSeekView.wdSeekPrimaryHeader;

                #region 增加页眉
                // 添加页眉 插入图片
                string pict = SystemConfig.PathOfDataUser + "log.jpg"; // 图片所在路径
                if (System.IO.File.Exists(pict))
                {
                    System.Drawing.Image img = System.Drawing.Image.FromFile(pict);
                    object LinkToFile = false;
                    object SaveWithDocument = true;
                    object Anchor = WordDoc.Application.Selection.Range;
                    WordDoc.Application.ActiveDocument.InlineShapes.AddPicture(pict, ref  LinkToFile,
                        ref  SaveWithDocument, ref  Anchor);
                    //    WordDoc.Application.ActiveDocument.InlineShapes[1].Width = img.Width; // 图片宽度
                    //    WordDoc.Application.ActiveDocument.InlineShapes[1].Height = img.Height; // 图片高度
                }
                WordApp.ActiveWindow.ActivePane.Selection.InsertAfter("[驰骋业务流程管理系统 http://ccFlow.org]");
                WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft; // 设置右对齐
                WordApp.ActiveWindow.View.SeekView = Word.WdSeekView.wdSeekMainDocument; // 跳出页眉设置
                WordApp.Selection.ParagraphFormat.LineSpacing = 15f; // 设置文档的行间距
                #endregion

                // 移动焦点并换行
                object count = 14;
                object WdLine = Word.WdUnits.wdLine; // 换一行;
                WordApp.Selection.MoveDown(ref  WdLine, ref  count, ref  Nothing); // 移动焦点
                WordApp.Selection.TypeParagraph(); // 插入段落

                // 文档中创建表格
                Word.Table newTable = WordDoc.Tables.Add(WordApp.Selection.Range, rowNum, 4, ref  Nothing, ref  Nothing);

                // 设置表格样式
                newTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleThickThinLargeGap;
                newTable.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;

                newTable.Columns[1].Width = 100f;
                newTable.Columns[2].Width = 100f;
                newTable.Columns[3].Width = 100f;
                newTable.Columns[4].Width = 100f;

                // 填充表格内容
                newTable.Cell(1, 1).Range.Text = wk.EnDesc;
                newTable.Cell(1, 1).Range.Bold = 2; // 设置单元格中字体为粗体

                // 合并单元格
                newTable.Cell(1, 1).Merge(newTable.Cell(1, 4));
                WordApp.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter; // 垂直居中
                WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter; // 水平居中

                int groupIdx = 1;
                foreach (GroupField gf in gfs)
                {
                    groupIdx++;
                    // 填充表格内容
                    newTable.Cell(groupIdx, 1).Range.Text = gf.Lab;
                    newTable.Cell(groupIdx, 1).Range.Font.Color = Word.WdColor.wdColorDarkBlue; // 设置单元格内字体颜色
                    newTable.Cell(groupIdx, 1).Shading.BackgroundPatternColor = Word.WdColor.wdColorGray25;
                    // 合并单元格
                    newTable.Cell(groupIdx, 1).Merge(newTable.Cell(groupIdx, 4));
                    WordApp.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    groupIdx++;

                    bool isLeft = true;
                    bool isColumns2 = false;
                    int currColumnIndex = 0;
                    foreach (MapAttr attr in mattrs)
                    {
                        if (attr.UIVisible == false)
                            continue;

                        if (attr.GroupID != gf.OID)
                            continue;

                        if (newTable.Rows.Count < groupIdx)
                            continue;

                        #region 增加明细表
                        foreach (MapDtl dtl in dtls)
                        {
                            if (dtl.IsUse)
                                continue;

                            if (dtl.RowIdx != groupIdx - 3)
                                continue;

                            if (gf.OID != dtl.GroupID)
                                continue;

                            GEDtls dtlsDB = new GEDtls(dtl.No);
                            QueryObject qo = new QueryObject(dtlsDB);
                            switch (dtl.DtlOpenType)
                            {
                                case DtlOpenType.ForEmp:
                                    qo.AddWhere(GEDtlAttr.RefPK, wk.OID);
                                    break;
                                case DtlOpenType.ForWorkID:
                                    qo.AddWhere(GEDtlAttr.RefPK, wk.OID);
                                    break;
                                case DtlOpenType.ForFID:
                                    qo.AddWhere(GEDtlAttr.FID, wk.OID);
                                    break;
                            }
                            qo.DoQuery();

                            newTable.Rows[groupIdx].SetHeight(100f, Word.WdRowHeightRule.wdRowHeightAtLeast);
                            newTable.Cell(groupIdx, 1).Merge(newTable.Cell(groupIdx, 4));

                            Attrs dtlAttrs = dtl.GenerMap().Attrs;
                            int colNum = 0;
                            foreach (Attr attrDtl in dtlAttrs)
                            {
                                if (attrDtl.UIVisible == false)
                                    continue;
                                colNum++;
                            }

                            newTable.Cell(groupIdx, 1).Select();
                            WordApp.Selection.Delete(ref Nothing, ref Nothing);
                            Word.Table newTableDtl = WordDoc.Tables.Add(WordApp.Selection.Range, dtlsDB.Count + 1, colNum,
                                ref Nothing, ref Nothing);

                            newTableDtl.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                            newTableDtl.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;

                            int colIdx = 1;
                            foreach (Attr attrDtl in dtlAttrs)
                            {
                                if (attrDtl.UIVisible == false)
                                    continue;
                                newTableDtl.Cell(1, colIdx).Range.Text = attrDtl.Desc;
                                colIdx++;
                            }

                            int idxRow = 1;
                            foreach (GEDtl item in dtlsDB)
                            {
                                idxRow++;
                                int columIdx = 0;
                                foreach (Attr attrDtl in dtlAttrs)
                                {
                                    if (attrDtl.UIVisible == false)
                                        continue;
                                    columIdx++;

                                    if (attrDtl.IsFKorEnum)
                                        newTableDtl.Cell(idxRow, columIdx).Range.Text = item.GetValRefTextByKey(attrDtl.Key);
                                    else
                                    {
                                        if (attrDtl.MyDataType == DataType.AppMoney)
                                            newTableDtl.Cell(idxRow, columIdx).Range.Text = item.GetValMoneyByKey(attrDtl.Key).ToString("0.00");
                                        else
                                            newTableDtl.Cell(idxRow, columIdx).Range.Text = item.GetValStrByKey(attrDtl.Key);

                                        if (attrDtl.IsNum)
                                            newTableDtl.Cell(idxRow, columIdx).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                                    }
                                }
                            }

                            groupIdx++;
                            isLeft = true;
                        }
                        #endregion 增加明细表

                        if (attr.UIIsLine)
                        {
                            currColumnIndex = 0;
                            isLeft = true;
                            if (attr.IsBigDoc)
                            {
                                newTable.Rows[groupIdx].SetHeight(100f, Word.WdRowHeightRule.wdRowHeightAtLeast);
                                newTable.Cell(groupIdx, 1).Merge(newTable.Cell(groupIdx, 4));
                                newTable.Cell(groupIdx, 1).Range.Text = attr.Name + ":\r\n" + wk.GetValStrByKey(attr.KeyOfEn);
                            }
                            else
                            {
                                newTable.Cell(groupIdx, 2).Merge(newTable.Cell(groupIdx, 4));
                                newTable.Cell(groupIdx, 1).Range.Text = attr.Name;
                                newTable.Cell(groupIdx, 2).Range.Text = wk.GetValStrByKey(attr.KeyOfEn);
                            }
                            groupIdx++;
                            continue;
                        }
                        else
                        {
                            if (attr.IsBigDoc)
                            {
                                if (currColumnIndex == 2)
                                {
                                    currColumnIndex = 0;
                                }

                                newTable.Rows[groupIdx].SetHeight(100f, Word.WdRowHeightRule.wdRowHeightAtLeast);
                                if (currColumnIndex == 0)
                                {
                                    newTable.Cell(groupIdx, 1).Merge(newTable.Cell(groupIdx, 2));
                                    newTable.Cell(groupIdx, 1).Range.Text = attr.Name + ":\r\n" + wk.GetValStrByKey(attr.KeyOfEn);
                                    currColumnIndex = 3;
                                    continue;
                                }
                                else if (currColumnIndex == 3)
                                {
                                    newTable.Cell(groupIdx, 2).Merge(newTable.Cell(groupIdx, 3));
                                    newTable.Cell(groupIdx, 2).Range.Text = attr.Name + ":\r\n" + wk.GetValStrByKey(attr.KeyOfEn);
                                    currColumnIndex = 0;
                                    groupIdx++;
                                    continue;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                string s = "";
                                if (attr.LGType == FieldTypeS.Normal)
                                {
                                    if (attr.MyDataType == DataType.AppMoney)
                                        s = wk.GetValDecimalByKey(attr.KeyOfEn).ToString("0.00");
                                    else
                                        s = wk.GetValStrByKey(attr.KeyOfEn);
                                }
                                else
                                {
                                    s = wk.GetValRefTextByKey(attr.KeyOfEn);
                                }

                                switch (currColumnIndex)
                                {
                                    case 0:
                                        newTable.Cell(groupIdx, 1).Range.Text = attr.Name;
                                        if (attr.IsSigan)
                                        {
                                            string path = BP.SystemConfig.PathOfDataUser + "\\Siganture\\" + s + ".jpg";
                                            if (System.IO.File.Exists(path))
                                            {
                                                System.Drawing.Image img = System.Drawing.Image.FromFile(path);
                                                object LinkToFile = false;
                                                object SaveWithDocument = true;
                                                //object Anchor = WordDoc.Application.Selection.Range;
                                                object Anchor = newTable.Cell(groupIdx, 2).Range;

                                                WordDoc.Application.ActiveDocument.InlineShapes.AddPicture(path, ref  LinkToFile,
                                                    ref  SaveWithDocument, ref  Anchor);
                                                //    WordDoc.Application.ActiveDocument.InlineShapes[1].Width = img.Width; // 图片宽度
                                                //    WordDoc.Application.ActiveDocument.InlineShapes[1].Height = img.Height; // 图片高度
                                            }
                                            else
                                            {
                                                newTable.Cell(groupIdx, 2).Range.Text = s;
                                            }
                                        }
                                        else
                                        {
                                            if (attr.IsNum)
                                            {
                                                newTable.Cell(groupIdx, 2).Range.Text = s;
                                                newTable.Cell(groupIdx, 2).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                                            }
                                            else
                                            {
                                                newTable.Cell(groupIdx, 2).Range.Text = s;
                                            }
                                        }
                                        currColumnIndex = 1;
                                        continue;
                                        break;
                                    case 1:
                                        newTable.Cell(groupIdx, 3).Range.Text = attr.Name;
                                        if (attr.IsSigan)
                                        {
                                            string path = BP.SystemConfig.PathOfDataUser + "\\Siganture\\" + s + ".jpg";
                                            if (System.IO.File.Exists(path))
                                            {
                                                System.Drawing.Image img = System.Drawing.Image.FromFile(path);
                                                object LinkToFile = false;
                                                object SaveWithDocument = true;
                                                object Anchor = newTable.Cell(groupIdx, 4).Range;
                                                WordDoc.Application.ActiveDocument.InlineShapes.AddPicture(path, ref  LinkToFile,
                                                    ref  SaveWithDocument, ref  Anchor);
                                            }
                                            else
                                            {
                                                newTable.Cell(groupIdx, 4).Range.Text = s;
                                            }
                                        }
                                        else
                                        {
                                            if (attr.IsNum)
                                            {
                                                newTable.Cell(groupIdx, 4).Range.Text = s;
                                                newTable.Cell(groupIdx, 4).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                                            }
                                            else
                                            {
                                                newTable.Cell(groupIdx, 4).Range.Text = s;
                                            }
                                        }
                                        currColumnIndex = 0;
                                        groupIdx++;
                                        continue;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }  //结束循环

                #region 添加页脚
                WordApp.ActiveWindow.View.SeekView = Word.WdSeekView.wdSeekPrimaryFooter;
                WordApp.ActiveWindow.ActivePane.Selection.InsertAfter("模板由ccflow自动生成，严谨转载。此流程的详细内容请访问 http://doc.ccFlow.org。 建造流程管理系统请致电: 0531-88553730 QQ:ccflow@ccflow.org");
                WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                #endregion

                // 文件保存
                WordDoc.SaveAs(ref  filename, ref  Nothing, ref  Nothing, ref  Nothing,
                    ref  Nothing, ref  Nothing, ref  Nothing, ref  Nothing,
                    ref  Nothing, ref  Nothing, ref  Nothing, ref  Nothing, ref  Nothing,
                    ref  Nothing, ref  Nothing, ref  Nothing);

                WordDoc.Close(ref  Nothing, ref  Nothing, ref  Nothing);
                WordApp.Quit(ref  Nothing, ref  Nothing, ref  Nothing);
                try
                {
                    string docFile = filename.ToString();
                    string pdfFile = docFile.Replace(".doc", ".pdf");
                    Glo.Rtf2PDF(docFile, pdfFile);
                }
                catch (Exception ex)
                {
                    BP.DA.Log.DebugWriteInfo("@生成pdf失败." + ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
                // WordApp.Quit(ref  Nothing, ref  Nothing, ref  Nothing);
                WordDoc.Close(ref  Nothing, ref  Nothing, ref  Nothing);
                WordApp.Quit(ref  Nothing, ref  Nothing, ref  Nothing);
            }
        }
        public static string GenerHelp(string helpId)
        {
            switch (helpId)
            {
                case "Bill":
                    return "<a href=\"http://ccFlow.org\"  target=_blank><img src='../../Images/FileType/rm.gif' border=0/>" + BP.Sys.Language.GetValByUserLang("OperVideo", "操作录像") + "</a>";
                case "FAppSet":
                    return "<a href=\"http://ccFlow.org\"  target=_blank><img src='../../Images/FileType/rm.gif' border=0/>" + BP.Sys.Language.GetValByUserLang("OperVideo", "操作录像") + "</a>";
                default:
                    return "<a href=\"http://ccFlow.org\"  target=_blank><img src='../../Images/FileType/rm.gif' border=0/>" + BP.Sys.Language.GetValByUserLang("OperVideo","操作录像") + "</a>";
                    break;
            }
        }
        public static string NodeImagePath
        {
            get {
                return Glo.IntallPath + "\\Data\\Node\\";
            }
        }
        public static void ClearDBData()
        {
            string sql = "DELETE FROM WF_GenerWorkFlow WHERE fk_flow not in (select no from wf_flow )";
            BP.DA.DBAccess.RunSQL(sql);

            sql = "DELETE FROM WF_GenerWorkerlist WHERE fk_flow not in (select no from wf_flow )";
            BP.DA.DBAccess.RunSQL(sql);
        }
        public static string OEM_Flag = "CCS";
        public static string FlowFileBill
        {
            get { return Glo.IntallPath + "\\DataUser\\Bill\\"; }
        }
        private static string _IntallPath = null;
        public static string IntallPath
        {
            get
            {
                if (_IntallPath == null)
                {
                    _IntallPath = SystemConfig.PathOfWebApp;
                    //   throw new Exception("@您没有 _IntallPath 赋值.");
                }
                return _IntallPath;
            }
            set
            {
                _IntallPath = value;
            }
        }
        /// <summary>
        /// 语言
        /// </summary>
        public static string Language = "CH";
        public static bool IsQL
        {
            get
            {
                string s = BP.SystemConfig.AppSettings["IsQL"];
                if (s == null || s == "0")
                    return false;
                return true;
            }
        }
        /// <summary>
        /// 是否显示标题图片？
        /// </summary>
        public static bool IsShowTitle
        {
            get
            {
                return BP.SystemConfig.GetValByKeyBoolen("IsShowTitle", true);
            }
        }

        public static string CurrPageID
        {
            get
            {
                try
                {
                    string url = System.Web.HttpContext.Current.Request.RawUrl;

                    int i = url.LastIndexOf("/") + 1;
                    int i2 = url.IndexOf(".aspx") - 6;
                    try
                    {
                        url = url.Substring(i);
                        return url.Substring(0, url.IndexOf(".aspx"));

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message + url + " i=" + i + " i2=" + i2);
                    }
                }
                catch(Exception ex)
                {
                    throw new Exception("获取当前PageID错误:"+ex.Message);
                }
            }
        }
    }
}
