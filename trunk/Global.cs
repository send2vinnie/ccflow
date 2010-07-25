using System;
using System.Collections;
using System.IO;
using System.Data;
using System.Windows.Forms;
using BP.WF.Design;
using BP.WF;
using BP.Win.WF;
using BP.Win.Controls;

namespace BP.WF
{
	/// <summary>
	/// Global 的摘要说明。
	/// </summary>
    public class Global
    {

        #region 发布设置
        public static string Host
        {
            get
            {
                return "127.0.0.1/Front/";

                string s = BP.SystemConfig.AppSettings["BPMHost"];
                if (s == null)
                    s = "127.0.0.1/Front/";
                return s;
            }
        }
        static Global()
        {
            SystemConfig.IsBSsystem = false;
            System.DateTime dt = File.GetLastWriteTime(Application.ExecutablePath);
            VersionDate = dt.ToString("yyyy-MM-dd HH:mm");
        }
        #endregion 发布设置

        #region Ver
        public static readonly string VersionDate = "2004-03-16 15:26"; //  
        public static readonly string UpdateItems = "";//"\r\n此更新不包括目录“Image”，如果登陆失败，请手动更新目录“Image”中的文件！";//"\n最近更新项目：BP.Tax.WF ->CUI， ->YUE"; //
        private static string _updatePath = "";
        public static string UpdatePath
        {
            get
            {
                return _updatePath;
            }
        }
        public static readonly string AppConfigPath = Application.StartupPath + "\\App.config";
        private static string _RefConfigPath = "";
        public static string RefConfigPath
        {
            get
            {
                return _RefConfigPath;
            }
        }
        #endregion Ver

        #region 登陆信息
        public static string User = "8888";
        public static string Right = "管理员";
        public static string RightID = "00";
        public static MainForm MainForm;
        public static BP.Port.Emp LoadEmp
        {
            get
            {
                return BP.Win.WF.Global.LoadEmp;
            }
            set
            {
                BP.Win.WF.Global.LoadEmp = value;
            }
        }
        #endregion 登陆信息

        #region 登陆
        public static void DoEdit(BP.En.Entities ens)
        {
            BP.Win32.FrmIE ie = new BP.Win32.FrmIE();
            ie.ShowIE("http://" + Global.Host + "/DoPort.aspx?EnsName=" + ens.ToString() + "&DoType=Ens" + "&Lang=" + BP.Web.WebUser.SysLang);
        }
        public static void DoEdit(BP.En.Entity en)
        {
            BP.Win32.FrmIE ie = new BP.Win32.FrmIE();
            ie.ShowIE("http://" + Global.Host + "/DoPort.aspx?EnName=" + en.ToString() + "&DoType=En&PK=" + en.PKVal + "&Lang=" + BP.Web.WebUser.SysLang);
        }
        /// <summary>
        /// 执行URL
        /// </summary>
        /// <param name="url"></param>
        public static void DoUrl(string url)
        {
            BP.Win32.FrmIE ie = new BP.Win32.FrmIE();
            ie.ShowIE("http://" + Global.Host + "/" + url + "&Lang=" + BP.Web.WebUser.SysLang);
            // ie.ShowIE("http://localhost/Front/DoPort.aspx?En=" + en.ToString() + "&DoType=En&PK=" + en.PKVal);
        }
        public static void DoUrlByType(string type, string refNo)
        {
            if (type.Contains("RunFlow"))
            {
                DoIE("http://" + Global.Host + "/DoPort.aspx?DoType=" + type+"&Lang="+BP.Web.WebUser.SysLang);
                return;
            }

            BP.Win32.FrmIE ie = new BP.Win32.FrmIE();
            switch (type)
            {
                case "RunFlow":
                    DoIE("http://" + Global.Host + "/DoPort.aspx?DoType=" + type + "&Lang=" + BP.Web.WebUser.SysLang);
                    break;
                default:
                    ie.ShowIE("http://" + Global.Host + "/DoPort.aspx?DoType=" + type + "&RefNo=" + refNo + "&Lang=" + BP.Web.WebUser.SysLang);
                    break;
            }
        }
        public static void DoIE(string url)
        {
            System.Diagnostics.Process.Start("IExplore.exe", url);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string JieMi(string str)
        {
            if ((str.Length % 4) != 0)
            {
                throw new ArgumentException("不是正确的BASE64编码，请检查。", "str");
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(str, "^[A-Z0-9/+=]*$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                throw new ArgumentException("包含不正确的BASE64编码，请检查。", "str");
            }
            string Base64Code = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789+/=";
            int page = str.Length / 4;
            System.Collections.ArrayList outMessage = new System.Collections.ArrayList(page * 3);
            char[] message = str.ToCharArray();
            for (int i = 0; i < page; i++)
            {
                byte[] instr = new byte[4];
                instr[0] = (byte)Base64Code.IndexOf(message[i * 4]);
                instr[1] = (byte)Base64Code.IndexOf(message[i * 4 + 1]);
                instr[2] = (byte)Base64Code.IndexOf(message[i * 4 + 2]);
                instr[3] = (byte)Base64Code.IndexOf(message[i * 4 + 3]);
                byte[] outstr = new byte[3];
                outstr[0] = (byte)((instr[0] << 2) ^ ((instr[1] & 0x30) >> 4));
                if (instr[2] != 64)
                {
                    outstr[1] = (byte)((instr[1] << 4) ^ ((instr[2] & 0x3c) >> 2));
                }
                else
                {
                    outstr[2] = 0;
                }
                if (instr[3] != 64)
                {
                    outstr[2] = (byte)((instr[2] << 6) ^ instr[3]);
                }
                else
                {
                    outstr[2] = 0;
                }
                outMessage.Add(outstr[0]);
                if (outstr[1] != 0)
                    outMessage.Add(outstr[1]);
                if (outstr[2] != 0)
                    outMessage.Add(outstr[2]);
            }
            byte[] outbyte = (byte[])outMessage.ToArray(Type.GetType("System.Byte"));
            return System.Text.Encoding.Default.GetString(outbyte);
        }
        private static int times = 1;
        /// <summary>
        /// 装载配置通过文件
        /// </summary>
        /// <returns></returns>
        public static bool LoadConfigByFile()
        {
            string path = "D:\\WorkFlow\\Front\\web.config"; //如果有这个文件就装载它。
            if (System.IO.File.Exists(path))
            {
                BP.DA.ClassFactory.LoadConfig(path);
                try
                {
                    BP.Port.Emp em = new BP.Port.Emp("admin");
                }
                catch
                {
                    BP.Port.Emp em = new BP.Port.Emp("admin");
                }


                BP.SystemConfig.IsBSsystem_Test = false;
                BP.SystemConfig.IsBSsystem = false;
                SystemConfig.IsBSsystem = false;
                BP.Win.WF.Global.FlowImagePath = "D:\\WorkFlow\\Front\\Data\\FlowDesc\\";

                BP.Web.WebUser.SysLang = BP.WF.Glo.Language;
                BP.SystemConfig.IsBSsystem_Test = false;
                BP.SystemConfig.IsBSsystem = false;
                SystemConfig.IsBSsystem = false;
                return true;
            }
            else
            {
                return false;
            }

        }
        public static bool LoadConfig()
        {
            BP.Web.WebUser.SysLang = BP.WF.Glo.Language;
            if (LoadConfigByFile() == true)
                return true;

            string filePath = null;
            try
            {
                try
                {
                    BP.SystemConfig.CS_AppSettings.Clear();
                }
                catch
                {
                }


                string s = System.Environment.CurrentDirectory + "\\FlowDesign\\Data\\";
                BP.WF.Glo.IntallPath = System.Environment.CurrentDirectory + "\\..\\..\\..\\";

                BP.Web.WebUser.SysLang = Glo.Language;
                BP.SystemConfig.CS_AppSettings = new System.Collections.Specialized.NameValueCollection();
                BP.SystemConfig.CS_DBConnctionDic.Clear();
                filePath = BP.WF.Glo.IntallPath + "\\FlowDesign\\Data\\ccs_" + BP.Web.WebUser.SysLang + "_" + Glo.OEM_Flag + ".key";

                s = BP.DA.DataType.ReadTextFile(filePath);
                s = JieMi(s);
                string[] strs = s.Split('@');
                BP.SystemConfig.AppSettings.Clear();
                foreach (string key in strs)
                {
                    if (key == null || key == "")
                        continue;

                    if (key.Contains("=") == false)
                        continue;

                    string[] mystrs = key.Split('=');
                    BP.SystemConfig.AppSettings[mystrs[0]] = key.Replace(mystrs[0] + "=", "");
                }

                BP.SystemConfig.IsBSsystem_Test = false;
                BP.SystemConfig.IsBSsystem = false;
                SystemConfig.IsBSsystem = false;
                BP.Win.WF.Global.FlowImagePath = System.Environment.CurrentDirectory;

                BP.Web.WebUser.SysLang = BP.WF.Glo.Language;
                BP.SystemConfig.IsBSsystem_Test = false;
                BP.SystemConfig.IsBSsystem = false;
                SystemConfig.IsBSsystem = false;

                BP.DA.Log.DebugWriteInfo("sdsds");

                // MessageBox.Show(SystemConfig.PathOfWebApp);

                //if (System.IO.Directory.Exists(SystemConfig.PathOfLog) ==false)
                //    throw new Exception("error path");
                //   throw new Exception("sd");

                BP.DA.Log.DefaultLogWriteLine(BP.DA.LogType.Error, "ssss");
                BP.Port.Emp em = new BP.Port.Emp("admin");
                return true;
            }
            catch (Exception ex)
            {
                times++;
                if (times >= 3)
                {
                    //   MessageBox.Show(SystemConfig.IsDebug.ToString());
                    //    MessageBox.Show(.ToString());

                    string msg = "以下原因导致您的测试无法进行。";
                    msg += "\t\n \t\n 1、您本地机器上的杀毒软件，防火墙的干扰，不能连接远程服务器。";
                    msg += "\t\n \t\n 2、远程服务器已经关闭，或者死机故障。";
                    msg += "\t\n \t\n 3、系统已升级，本地系统已经不兼容。";
                    msg += "\t\n \t\n 4、其它意外的错误。";
                    msg += "\t\n\t\n\t\n 请联系我们或者重新下载新版本的程序解决此问题。http://ccflow.cn ";

                    msg += "\t\n  website：http://ccflow.cn ";
                    msg += "\t\n  MSN： ccflow.cn@hotmail.com ";
                    msg += "\t\n  QQ： 793719823 ";
                    msg += "\t\n  Skpe： chichengsoft ";

                    MessageBox.Show(msg + " \t\n Exception" + ex.Message, "对不起演示无法进行...", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return false;
                }
                else
                {
                    return LoadConfig();
                }
            }
        }
        private static void LoadIt(string file, string imgPath)
        {
            BP.SystemConfig.IsBSsystem_Test = false;
            BP.SystemConfig.IsBSsystem = false;
            SystemConfig.IsBSsystem = false;

            BP.DA.ClassFactory.LoadConfig( file );

            BP.Win.WF.Global.FlowImagePath = imgPath;
            BP.SystemConfig.IsBSsystem_Test = false;
            BP.SystemConfig.IsBSsystem = false;
            SystemConfig.IsBSsystem = false;
        }
        #endregion

        #region 应用开发

        static bool Application_Start()
        {
            BP.Win32.FrmLanguage lan = new BP.Win32.FrmLanguage();
            lan.ShowDialog();

            if (LoadConfig()==false)
                return false;

            MainForm = new MainForm();
            Win.WF.Global.ToolCursors = new Cursor[7];//=[MainForm.wfToolBar1.ButtonsCount];
            Win.WF.Global.ToolCursors[0] = Cursors.Default;
            Win.WF.Global.ToolCursors[1] = Cursors.Cross;
            Win.WF.Global.ToolCursors[2] = Cursors.Cross;
            Win.WF.Global.ToolCursors[3] = Cursors.Cross;
            Win.WF.Global.ToolCursors[4] = Cursors.Cross;
            Win.WF.Global.ToolCursors[5] = Cursors.Cross;
            Win.WF.Global.ToolCursors[6] = Cursors.Cross;
            Win.WF.Global.WFTools = MainForm.wfToolBar1;
            return true;
        }
        public static void Update()
        {
            MainForm.Dispose();
            System.Diagnostics.Process.Start(Application.StartupPath + "\\OnlineUpdate.exe");
            Application.Exit();
        }

        [STAThread]
        static void Main()
        {
            DeleteConfigFile();

            if (!Application_Start())
                return;


            Application.Run(MainForm);
            if (MainForm != null)
                MainForm.Dispose();

            Application.Exit();
        }
        public static void DeleteConfigFile()
        {
            try
            {
                System.IO.File.Delete("WF2008.exe.config");
            }
            catch
            {
            }
        }
        #endregion


        #region 流程常用的方法
        /// <summary>
        ///  load flow Template as new flow.
        /// </summary>
        /// <param name="path"></param>
        public static Flow DoLoadFlowTemplate(string fk_flowSort, string path)
        {
            string flowFile = path + "\\Flow.xml";
            DataSet ds = new DataSet();
            ds.ReadXml(flowFile);
            DataTable dt = ds.Tables[0];
            Flow fl = new Flow();
            string oldFlowNo = dt.Rows[0]["No"].ToString();
            int oldFlowID = int.Parse(oldFlowNo);
            try
            {
                fl.DoNewFlow();

                int flowID = int.Parse(fl.No);

                #region 处理流程表数据
                foreach (DataColumn dc in dt.Columns)
                {
                    string val = dt.Rows[0][dc.ColumnName] as string;
                    switch (dc.ColumnName)
                    {
                        case "No":
                        case "FK_FlowSort":
                            continue;
                        case "Name":
                            val = "Copy of " + val;
                            break;
                        default:
                            break;
                    }
                    fl.SetValByKey(dc.ColumnName, val);
                }
                fl.FK_FlowSort = fk_flowSort;
                fl.Update();
                #endregion 处理流程表数据


                string[] fls = System.IO.Directory.GetFiles(path);
                foreach (string f in fls)
                {
                    ds.Tables.Clear();
                    FileInfo finfo = new FileInfo(f);
                    switch (finfo.Name)
                    {
                        case "Flow.xml": //模版文件。
                            continue;
                        case "BookTemplates.xml": //模版文件。
                            ds.ReadXml(f);
                            if (ds.Tables.Count == 0)
                                continue;
                            dt = ds.Tables[0];
                            try
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    BookTemplate bt = new BookTemplate();
                                    //   bt.NodeID = flowID;
                                    foreach (DataColumn dc in dt.Columns)
                                    {
                                        string val = dr[dc.ColumnName] as string;
                                        switch (dc.ColumnName)
                                        {
                                            case "NodeID":
                                                if (val.Length == 3)
                                                    val = flowID + val.Substring(1);
                                                else
                                                    val = flowID + val.Substring(2);
                                                break;
                                            default:
                                                break;
                                        }
                                        bt.SetValByKey(dc.ColumnName, val);
                                    }
                                    int i = 0;
                                    string no = bt.No;
                                    while (bt.IsExits == false)
                                    {
                                        bt.No = no + i.ToString();
                                    }
                                    bt.Insert();
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("@read xml file error [" + f + "] , Msg = " + ex.Message);
                            }
                            break;
                        case "Conds.xml": //Conds.xml。
                            ds.ReadXml(f);
                            if (ds.Tables.Count == 0)
                                continue;
                            dt = ds.Tables[0];
                            try
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    Cond cd = new Cond();
                                    cd.FK_Flow = fl.No;
                                    foreach (DataColumn dc in dt.Columns)
                                    {
                                        string val = dr[dc.ColumnName] as string;
                                        switch (dc.ColumnName)
                                        {
                                            case "ToNodeID":
                                            case "FK_Node":
                                            case "NodeID":
                                                if (val.Length == 3)
                                                    val = flowID + val.Substring(1);
                                                else if (val.Length == 4)
                                                    val = flowID + val.Substring(2);
                                                break;
                                            default:
                                                break;
                                        }
                                        cd.SetValByKey(dc.ColumnName, val);
                                    }
                                    cd.MyPK = DA.DBAccess.GenerOID().ToString();
                                    cd.Insert();
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("@read xml file error [" + f + "] , Msg = " + ex.Message);
                            }

                            break;
                        case "Directions.xml": //FAppSets.xml。
                            ds.ReadXml(f);
                            if (ds.Tables.Count == 0)
                                continue;
                            dt = ds.Tables[0];
                            try
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    Direction dir = new Direction();
                                    foreach (DataColumn dc in dt.Columns)
                                    {
                                        string val = dr[dc.ColumnName] as string;
                                        switch (dc.ColumnName)
                                        {
                                            case "Node":
                                            case "ToNode":
                                                if (val.Length == 3)
                                                    val = flowID + val.Substring(1);
                                                else if (val.Length == 4)
                                                    val = flowID + val.Substring(2);
                                                break;
                                            default:
                                                break;
                                        }
                                        dir.SetValByKey(dc.ColumnName, val);
                                    }
                                    dir.Insert();
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("@read xml file error [" + f + "] , Msg = " + ex.Message);
                            }
                            break;
                        case "FAppSets.xml": //FAppSets.xml。
                            ds.ReadXml(f);
                            if (ds.Tables.Count == 0)
                                continue;
                            dt = ds.Tables[0];

                            try
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    FAppSet fs = new FAppSet();
                                    fs.FK_Flow = fl.No;
                                    foreach (DataColumn dc in dt.Columns)
                                    {
                                        string val = dr[dc.ColumnName] as string;
                                        switch (dc.ColumnName)
                                        {
                                            case "FK_Node":
                                                if (val.Length == 3)
                                                    val = flowID + val.Substring(1);
                                                else if (val.Length == 4)
                                                    val = flowID + val.Substring(2);
                                                break;
                                            default:
                                                break;
                                        }
                                        fs.SetValByKey(dc.ColumnName, val);
                                    }
                                    fs.OID = DA.DBAccess.GenerOID();
                                    fs.Insert();
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("@read xml file error [" + f + "] , Msg = " + ex.Message);
                            }
                            break;
                        case "FlowStations.xml": //FlowStations.xml。
                            ds.ReadXml(f);
                            if (ds.Tables.Count == 0)
                                continue;
                            dt = ds.Tables[0];

                            try
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    FlowStation fs = new FlowStation();
                                    fs.FK_Flow = fl.No;
                                    foreach (DataColumn dc in dt.Columns)
                                    {
                                        string val = dr[dc.ColumnName] as string;
                                        switch (dc.ColumnName)
                                        {
                                            case FlowStationAttr.FK_Flow:
                                                continue;
                                            default:
                                                break;
                                        }
                                        fs.SetValByKey(dc.ColumnName, val);
                                    }
                                    fs.Insert();
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("@read xml file error [" + f + "] , Msg = " + ex.Message);
                            }
                            break;
                        case "LabNotes.xml": //LabNotes.xml。
                            ds.ReadXml(f);
                            if (ds.Tables.Count == 0)
                                continue;
                            dt = ds.Tables[0];

                            try
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    LabNote ln = new LabNote();
                                    ln.FK_Flow = fl.No;
                                    foreach (DataColumn dc in dt.Columns)
                                    {
                                        string val = dr[dc.ColumnName] as string;
                                        switch (dc.ColumnName)
                                        {
                                            case LabNoteAttr.FK_Flow:
                                                continue;
                                            default:
                                                break;
                                        }
                                        ln.SetValByKey(dc.ColumnName, val);
                                    }
                                    ln.OID = DA.DBAccess.GenerOID();
                                    ln.InsertAsOID(ln.OID);
                                }
                                break;
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("@read xml file error [" + f + "] , Msg = " + ex.Message);
                            }
                        case "NodeDept.xml": //FAppSets.xml。
                            ds.ReadXml(f);
                            if (ds.Tables.Count == 0)
                                continue;
                            dt = ds.Tables[0];
                            try
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    NodeDept dir = new NodeDept();
                                    foreach (DataColumn dc in dt.Columns)
                                    {
                                        string val = dr[dc.ColumnName] as string;
                                        switch (dc.ColumnName)
                                        {
                                            case "FK_Node":
                                                if (val.Length == 3)
                                                    val = flowID + val.Substring(1);
                                                else if (val.Length == 4)
                                                    val = flowID + val.Substring(2);
                                                break;
                                            default:
                                                break;
                                        }
                                        dir.SetValByKey(dc.ColumnName, val);
                                    }
                                    dir.Insert();
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("@read xml file error [" + f + "] , Msg = " + ex.Message);
                            }
                            break;
                        case "Nodes.xml": //LabNotes.xml。
                            ds.ReadXml(f);
                            if (ds.Tables.Count == 0)
                                continue;
                            dt = ds.Tables[0];
                            try
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    Node nd = new Node();
                                    nd.FK_Flow = fl.No;
                                    foreach (DataColumn dc in dt.Columns)
                                    {
                                        string val = dr[dc.ColumnName] as string;
                                        switch (dc.ColumnName)
                                        {
                                            case NodeAttr.NodeID:
                                                if (val.Length == 3)
                                                    val = flowID + val.Substring(1);
                                                else if (val.Length == 4)
                                                    val = flowID + val.Substring(2);
                                                break;
                                            case NodeAttr.FK_Flow:
                                            case "FK_FlowSort":
                                                continue;
                                            case NodeAttr.ShowSheets:
                                            case NodeAttr.HisToNDs:
                                            case NodeAttr.GroupStaNDs:
                                                string key = "@" + flowID;
                                                val = val.Replace(key, "");
                                                break;
                                            default:
                                                break;
                                        }
                                        nd.SetValByKey(dc.ColumnName, val);
                                    }
                                    try
                                    {
                                        nd.DirectInsert();
                                    }
                                    catch
                                    {
                                        nd.DirectUpdate();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("@read xml file error [" + f + "] , Msg = " + ex.Message);
                            }
                            break;
                        case "NodeStations.xml": //FAppSets.xml。
                            ds.ReadXml(f);
                            if (ds.Tables.Count == 0)
                                continue;
                            dt = ds.Tables[0];
                            try
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    NodeStation ns = new NodeStation();
                                    foreach (DataColumn dc in dt.Columns)
                                    {
                                        string val = dr[dc.ColumnName] as string;
                                        switch (dc.ColumnName)
                                        {
                                            case "FK_Node":
                                                if (val.Length == 3)
                                                    val = flowID + val.Substring(1);
                                                else if (val.Length == 4)
                                                    val = flowID + val.Substring(2);
                                                break;
                                            default:
                                                break;
                                        }
                                        ns.SetValByKey(dc.ColumnName, val);
                                    }
                                    ns.Insert();
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("@read xml file error [" + f + "] , Msg = " + ex.Message);
                            }
                            break;
                        case "RptAttrs.xml": //LabNotes.xml。
                            ds.ReadXml(f);
                            if (ds.Tables.Count == 0)
                                continue;
                            dt = ds.Tables[0];

                            try
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    RptAttr attr = new RptAttr();
                                    attr.FK_Node = fl.No;
                                    foreach (DataColumn dc in dt.Columns)
                                    {
                                        string val = dr[dc.ColumnName] as string;
                                        switch (dc.ColumnName)
                                        {
                                            case RptAttrAttr.FK_Node:
                                                if (val.Length == 3)
                                                    val = flowID + val.Substring(1);
                                                else if (val.Length == 4)
                                                    val = flowID + val.Substring(2);
                                                break;
                                            default:
                                                break;
                                        }
                                        attr.SetValByKey(dc.ColumnName, val);
                                    }
                                    attr.MyPK = attr.FK_Node + "_" + attr.FK_Rpt + "_" + attr.RefAttrOID + "_" + attr.RefField;
                                    attr.Save();
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("@read xml file error [" + f + "] , Msg = " + ex.Message);
                            }
                            break;
                        case "RptStations.xml": //RptEmps.xml。
                        case "RptEmps.xml": //RptEmps.xml。
                            break;
                        case "Sys_Enum.xml": //RptEmps.xml。
                            ds.ReadXml(f);
                            if (ds.Tables.Count == 0)
                                continue;
                            try
                            {
                                dt = ds.Tables[0];
                                foreach (DataRow dr in dt.Rows)
                                {
                                    Sys.SysEnum se = new Sys.SysEnum();
                                    foreach (DataColumn dc in dt.Columns)
                                    {
                                        string val = dr[dc.ColumnName] as string;
                                        switch (dc.ColumnName)
                                        {
                                            case RptAttrAttr.FK_Node:
                                                break;
                                            default:
                                                break;
                                        }
                                        se.SetValByKey(dc.ColumnName, val);
                                    }
                                    if (se.IsExits)
                                        continue;
                                    se.Insert();
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("@read xml file error [" + f + "] , Msg = " + ex.Message);
                            }
                            break;
                        case "Sys_EnumMain.xml": //RptEmps.xml。
                            ds.ReadXml(f);
                            if (ds.Tables.Count == 0)
                                continue;
                            dt = ds.Tables[0];

                            try
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    Sys.SysEnumMain sem = new Sys.SysEnumMain();
                                    foreach (DataColumn dc in dt.Columns)
                                    {
                                        string val = dr[dc.ColumnName] as string;
                                        //switch (dc.ColumnName)
                                        //{
                                        //    case RptAttrAttr.FK_Node:
                                        //        break;
                                        //    default:
                                        //        break;
                                        //}
                                        sem.SetValByKey(dc.ColumnName, val);
                                    }
                                    if (sem.IsExits)
                                        continue;
                                    sem.Insert();
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("@read xml file error [" + f + "] , Msg = " + ex.Message);
                            }
                            break;
                        case "Sys_MapAttr.xml": //RptEmps.xml。
                            ds.ReadXml(f);
                            if (ds.Tables.Count == 0)
                                continue;
                            dt = ds.Tables[0];

                            try
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    Sys.MapAttr ma = new Sys.MapAttr();
                                    foreach (DataColumn dc in dt.Columns)
                                    {
                                        string val = dr[dc.ColumnName] as string;
                                        switch (dc.ColumnName)
                                        {
                                            case Sys.MapAttrAttr.FK_MapData:
                                            case Sys.MapAttrAttr.KeyOfEn:
                                                val = val.Replace("ND" + oldFlowID, "ND" + flowID);
                                                break;
                                            default:
                                                break;
                                        }
                                        ma.SetValByKey(dc.ColumnName, val);
                                    }
                                    bool b = ma.IsExit(Sys.MapAttrAttr.FK_MapData, ma.FK_MapData, Sys.MapAttrAttr.KeyOfEn, ma.KeyOfEn);
                                    if (b == true)
                                        ma.Update();
                                    else
                                        ma.InsertAsNew();
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("@read xml file error [" + f + "] , Msg = " + ex.Message);
                            }
                            break;
                        case "Sys_MapData.xml": //RptEmps.xml。
                            ds.ReadXml(f);
                            if (ds.Tables.Count == 0)
                                continue;
                            dt = ds.Tables[0];

                            try
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    Sys.MapData md = new Sys.MapData();
                                    foreach (DataColumn dc in dt.Columns)
                                    {
                                        string val = dr[dc.ColumnName] as string;
                                        switch (dc.ColumnName)
                                        {
                                            case Sys.MapDataAttr.No:
                                                val = val.Replace("ND" + oldFlowNo, "ND" + fl.No);
                                                break;
                                            default:
                                                break;
                                        }
                                        md.SetValByKey(dc.ColumnName, val);
                                    }
                                    md.Save();
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("@read xml file error [" + f + "] , Msg = " + ex.Message);
                            }
                            break;
                        case "Sys_MapDtl.xml": //RptEmps.xml。
                            ds.ReadXml(f);
                            if (ds.Tables.Count == 0)
                                continue;
                            dt = ds.Tables[0];
                            try
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    Sys.MapDtl md = new Sys.MapDtl();
                                    foreach (DataColumn dc in dt.Columns)
                                    {
                                        string val = dr[dc.ColumnName] as string;
                                        switch (dc.ColumnName)
                                        {
                                            case Sys.MapDtlAttr.No:
                                            case Sys.MapDtlAttr.FK_MapData:
                                            case Sys.MapDtlAttr.PTable:
                                                val = val.Replace("ND" + oldFlowID, "ND" + flowID);
                                                break;
                                            default:
                                                break;
                                        }
                                        md.SetValByKey(dc.ColumnName, val);
                                    }
                                    md.Save();
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("@read xml file error [" + f + "] , Msg = " + ex.Message);
                            }
                            break;

                        case "WF_NodeEmps.xml": //FAppSets.xml。
                            ds.ReadXml(f);
                            if (ds.Tables.Count == 0)
                                continue;
                            dt = ds.Tables[0];
                            try
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    NodeEmp ne = new NodeEmp();
                                    foreach (DataColumn dc in dt.Columns)
                                    {
                                        string val = dr[dc.ColumnName] as string;
                                        switch (dc.ColumnName)
                                        {
                                            case "FK_Node":
                                                if (val.Length == 3)
                                                    val = flowID + val.Substring(1);
                                                else if (val.Length == 4)
                                                    val = flowID + val.Substring(2);
                                                break;
                                            default:
                                                break;
                                        }
                                        ne.SetValByKey(dc.ColumnName, val);
                                    }
                                    ne.Insert();
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("@read xml file error [" + f + "] , Msg = " + ex.Message);
                            }
                            break;
                        case "WFRpts.xml": //RptEmps.xml。
                            ds.ReadXml(f);
                            if (ds.Tables.Count == 0)
                                continue;
                            dt = ds.Tables[0];

                            try
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    WFRpt rpt = new WFRpt();
                                    foreach (DataColumn dc in dt.Columns)
                                    {
                                        string val = dr[dc.ColumnName] as string;
                                        switch (dc.ColumnName)
                                        {
                                            case WFRptAttr.FK_FlowSort:
                                                val = fl.FK_FlowSort;
                                                break;
                                            case WFRptAttr.FK_Flow:
                                                val = fl.No;
                                                break;
                                            default:
                                                break;
                                        }
                                        rpt.SetValByKey(dc.ColumnName, val);
                                    }

                                    try
                                    {
                                        rpt.Save();
                                    }
                                    catch
                                    {

                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("@read xml file error [" + f + "] , Msg = " + ex.Message);
                            }
                            break;
                        default:
                            throw new Exception("@unhandle xml file named " + f);
                    }
                }


                #region 处理数据完整性。
                BP.DA.DBAccess.RunSQL("DELETE WF_Cond WHERE ToNodeID NOT IN (SELECT NodeID FROM WF_Node)");
                BP.DA.DBAccess.RunSQL("DELETE WF_Cond WHERE FK_Node NOT IN (SELECT NodeID FROM WF_Node)");
                BP.DA.DBAccess.RunSQL("DELETE WF_Cond WHERE NodeID NOT IN (SELECT NodeID FROM WF_Node)");
                #endregion

                MessageBox.Show("the flow templete succeedfuly install your system, you must be redesign it for fit your work.", "ok", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return fl;
            }
            catch (Exception ex)
            {
                fl.DoDelete();
                throw ex;
            }
        }
        /// <summary>
        /// gener flow Template.
        /// </summary>
        /// <param name="fl"></param>
        public static void DoGenerFlowTemplate(Flow fl)
        {
            string path = "C:\\FlowTemplate\\" + fl.Name + "\\";
            if (System.IO.Directory.Exists(path) == false)
                System.IO.Directory.CreateDirectory(path);

            // 把流程信息生成一个文件。
            Flows fls = new Flows();
            fls.AddEntity(fl);
            fls.SaveToXml(path + "Flow.xml");

            // 节点信息
            Nodes nds = fl.HisNodes;
            nds.SaveToXml(path + "Nodes.xml");

            // 文书信息
            BookTemplates tmps = new BookTemplates(fl.No);
            tmps.SaveToXml(path + "BookTemplates.xml");
            foreach (BookTemplate tmp in tmps)
            {
                File.Copy(@"D:\WorkFlow\Front\Data\CyclostyleFile\" + tmp.No + ".rtf", path + "\\" + tmp.No + ".rtf", true);
            }

            // 条件信息
            Conds cds = new Conds(fl.No);
            cds.SaveToXml(path + "Conds.xml");

            // 方向
            string sqlin = "SELECT NodeID from wf_node where fk_flow='" + fl.No + "'";
            string sql = "select * from WF_Direction where Node IN (" + sqlin + ") OR ToNode In (" + sqlin + ")";
            DataSet ds = BP.DA.DBAccess.RunSQLReturnDataSet(sql);
            ds.WriteXml(path+"Directions.xml");


            // 应用设置 FAppSet
            FAppSets sets = new FAppSets(fl.No);
            sets.SaveToXml(path + "FAppSets.xml");



            // 流程发送完后抄送到岗位 
            FlowStations fstas = new FlowStations(fl.No);
            fstas.SaveToXml(path + "FlowStations.xml");

            // 流程标签.
            LabNotes labs = new LabNotes(fl.No);
            labs.SaveToXml(path + "LabNotes.xml");


            // 节点与部门。
            sql = "select * from WF_NodeDept where FK_Node IN (" + sqlin + ")";
            ds = BP.DA.DBAccess.RunSQLReturnDataSet(sql);
            ds.WriteXml(path + "NodeDept.xml");

            // 节点与岗位权限。
            sql = "select * from WF_NodeStation where FK_Node IN (" + sqlin + ")";
            ds = BP.DA.DBAccess.RunSQLReturnDataSet(sql);
            ds.WriteXml(path + "NodeStations.xml");

            // 节点与人员。
            sql = "select * from WF_NodeEmp where FK_Node IN (" + sqlin + ")";
            ds = BP.DA.DBAccess.RunSQLReturnDataSet(sql);
            ds.WriteXml(path + "WF_NodeEmps.xml");

            // 流程报表。
            WFRpts rpts = new WFRpts(fl.No);
            rpts.SaveToXml(path + "WFRpts.xml");

            // 流程报表属性
            RptAttrs rptAttrs = new RptAttrs();
            rptAttrs.RetrieveAll();
            rptAttrs.SaveToXml(path + "RptAttrs.xml");


            // 流程报表访问权限。
            RptStations rptStations = new RptStations(fl.No);
            rptStations.RetrieveAll();
            rptStations.SaveToXml(path + "RptStations.xml");


            // 流程报表人员访问权限。
            RptEmps rptEmps = new RptEmps(fl.No);
            rptEmps.RetrieveAll();
            rptEmps.SaveToXml(path + "RptEmps.xml");

            int flowID = int.Parse(fl.No);

            // Sys_MapData
            sql = "SELECT * FROM Sys_MapData WHERE No LIKE 'ND" + flowID + "%'";
            ds = BP.DA.DBAccess.RunSQLReturnDataSet(sql);
            ds.WriteXml(path + "Sys_MapData.xml");


            // Sys_MapAttr
            sql = "SELECT * FROM Sys_MapAttr WHERE  FK_MapData LIKE 'ND" + flowID + "%'";
            ds = BP.DA.DBAccess.RunSQLReturnDataSet(sql);
            ds.WriteXml(path + "Sys_MapAttr.xml");

            // Sys_EnumMain
            sql = "SELECT * FROM Sys_EnumMain WHERE No IN (SELECT KeyOfEn from Sys_MapAttr WHERE FK_MapData LIKE 'ND" + flowID + "%' )";
            ds = BP.DA.DBAccess.RunSQLReturnDataSet(sql);
            ds.WriteXml(path + "Sys_EnumMain.xml");

            // Sys_MapDtl
            sql = "SELECT * FROM Sys_MapDtl WHERE  FK_MapData LIKE 'ND" + flowID + "%'";
            ds = BP.DA.DBAccess.RunSQLReturnDataSet(sql);
            ds.WriteXml(path + "Sys_MapDtl.xml");
            

            // Sys_Enum
            sql = "SELECT * from Sys_Enum WHERE EnumKey IN ( SELECT No FROM Sys_EnumMain where NO IN (SELECT KeyOfEn from Sys_MapAttr WHERE FK_MapData LIKE 'ND" + flowID + "%' ) )";
            ds = BP.DA.DBAccess.RunSQLReturnDataSet(sql);
            ds.WriteXml(path + "Sys_Enum.xml");

#warning share flow Template.
            string msg = "The flow Template is gener secceed,  would you like to share it ?";
            if (System.Windows.Forms.MessageBox.Show(msg, "Thanks for you those ccflow.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
            }
        }
        #endregion
    }

    public enum StartModel
    {
        /// <summary>
        /// 启动流程设计器
        /// </summary>
        WorkFlow,
        /// <summary>
        /// 调度
        /// </summary>
        DTS,
    }
}
