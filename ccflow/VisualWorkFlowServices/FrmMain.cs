using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using BP.WF;
using BP.Port;
using BP.En;
using BP.Sys;
using BP.DA;
using BP;
using BP.Web;

namespace SMSServices
{
    public enum ScanSta
    {
        Working,
        Pause,
        Stop
    }
    public partial class FrmMain : Form
    {
        delegate void SetTextCallback(string text);
        public FrmMain()
        {
            InitializeComponent();
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Show();
            this.notifyIcon1.Visible = true;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // this.toolStripStatusLabel1.Text = "服务停止状态...";
            this.toolStripStatusLabel1.Text = "服务暂停";

            this.textBox1.Text = "服务停止...";
            this.Btn_StartStop.Text = "启动";
        }

        Thread thread = null;
        private void Btn_StartStop_Click(object sender, EventArgs e)
        {
            if (this.Btn_StartStop.Text == "启动")
            {
                if (this.thread == null)
                {
                 //   Glo.ScanDT = null;
                    //  Glo.CurrPoll = this.HisPoll;
                    ThreadStart ts = new ThreadStart(ReadFiles);
                    thread = new Thread(ts);
                    thread.Start();

                    this.Btn_StartStop.Text = "暂停";
                }

                this.HisScanSta = ScanSta.Working;
                this.SetText("服务启动***********" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                this.Btn_StartStop.Text = "暂停";
                this.toolStripStatusLabel1.Text = "服务启动";
            }
            else
            {
                this.SetText("服务暂停***********" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                this.HisScanSta = ScanSta.Pause;
                this.Btn_StartStop.Text = "启动";
                this.toolStripStatusLabel1.Text = "服务暂停";
            }
        }
        public ScanSta HisScanSta = ScanSta.Pause;

        public void ReadFiles()
        {
            BP.WF.Flows fls = new BP.WF.Flows();
            fls.RetrieveAll();

            string sql = "";
            HisScanSta = ScanSta.Working;
            int idx = 0;
            while (true)
            {
                System.Threading.Thread.Sleep(500);

                while (this.HisScanSta == ScanSta.Pause)
                {
                    System.Threading.Thread.Sleep(3000);
                    if (this.checkBox1.Checked)
                        Console.Beep();
                    //this.SetText("暂停中...");
                }

                this.SetText("开始检查自动发起流程....");


                #region 自动启动流程
                foreach (BP.WF.Flow fl in fls)
                {
                    if (fl.IsOK == false
                        || fl.HisFlowRunWay == BP.WF.FlowRunWay.HandWork)
                        continue;

                    if (DateTime.Now.ToString("HH:mm") == fl.Tag)
                        continue;

                    if (fl.RunObj == null || fl.RunObj == "")
                    {
                        string msg = "您设置自动运行流程错误，没有设置流程内容，流程编号：" + fl.No;
                        this.SetText(msg);
                        continue;
                    }

                    #region 判断当前时间是否可以运行它。
                    string nowStr = DateTime.Now.ToString("yyyy-MM-dd,HH:mm");
                    string[] strs = fl.RunObj.Split('@'); //破开时间串。
                    bool IsCanRun = false;
                    foreach (string str in strs)
                    {
                        if (string.IsNullOrEmpty(str))
                            continue;
                        if (nowStr.Contains(str))
                            IsCanRun = true;
                    }
                    if (IsCanRun == false)
                        continue;

                    // 设置时间.
                    fl.Tag = DateTime.Now.ToString("HH:mm");
                    #endregion 判断当前时间是否可以运行它。

                    // 以此用户进入.
                    switch (fl.HisFlowRunWay)
                    {
                        case BP.WF.FlowRunWay.SpecEmp: //指定人员按时运行。
                            string RunObj = fl.RunObj;
                            string fk_emp = RunObj.Substring(0, RunObj.IndexOf('@'));

                            BP.Port.Emp emp = new BP.Port.Emp();
                            emp.No = fk_emp;
                            if (emp.RetrieveFromDBSources() == 0)
                            {
                                this.SetText("启动自动启动流程错误：发起人(" + fk_emp + ")不存在。");
                                continue;
                            }
                            BP.Web.WebUser.SignInOfGener(emp);
                            BP.WF.Dev2Interface.Node_StartWork(fl.No, null);
                            continue;
                        case BP.WF.FlowRunWay.DataModel: //按数据集合驱动的模式执行。
                            this.SetText("@开始执行数据驱动流程调度:"+fl.Name);
                            this.DTS_Flow(fl);
                            //BP.WF.Dev2Interface.DTS_AutoStarterFlow(fl);
                            continue;
                        default:
                            break;
                    }
                }

                if (BP.Web.WebUser.No != "admin")
                {
                    BP.Port.Emp empadmin = new BP.Port.Emp("admin");
                    BP.Web.WebUser.SignInOfGener(empadmin);
                }
                #endregion 发送消息

                #region 发送消息
                BP.TA.SMSs sms = new BP.TA.SMSs();
                BP.En.QueryObject qo = new BP.En.QueryObject(sms);
                //    qo.AddWhere(BP.TA.SMSAttr.SMSSta, (int)BP.TA.SMSSta.UnRun);
                sms.Retrieve(BP.TA.SMSAttr.SMSSta, (int)BP.TA.SMSSta.UnRun);
                foreach (BP.TA.SMS sm in sms)
                {
                    if (this.HisScanSta == ScanSta.Stop)
                        return;

                    while (this.HisScanSta == ScanSta.Pause)
                    {
                        if (this.HisScanSta == ScanSta.Stop)
                            return;

                        System.Threading.Thread.Sleep(3000);

                        if (this.checkBox1.Checked)
                            Console.Beep();
                    }

                    if (sm.Email.Length == 0)
                    {
                        sm.HisSMSSta = BP.TA.SMSSta.RunOK;
                        sm.Update();
                        continue;
                    }

                    try
                    {
                        this.SetText("@执行：" + sm.Tel + " email: " + sm.Email);
                        this.SendMail(sm);

                        idx++;
                        this.SetText("已完成 , 第:" + idx + " 个.");
                        this.SetText("--------------------------------");

                        if (this.checkBox1.Checked)
                            Console.Beep();
                    }
                    catch (Exception ex)
                    {
                        this.SetText("@错误：" + ex.Message);
                    }
                }
                #endregion 发送消息

                //this.SetText("已执行:" + idx + " 数据.....");
                System.Threading.Thread.Sleep(1000);
                switch (this.toolStripStatusLabel1.Text)
                {
                    case "服务启动":
                        this.toolStripStatusLabel1.Text = "服务启动..";
                        break;
                    case "服务启动..":
                        this.toolStripStatusLabel1.Text = "服务启动....";
                        break;
                    case "服务启动....":
                        this.toolStripStatusLabel1.Text = "服务启动......";
                        break;
                    default:
                        this.toolStripStatusLabel1.Text = "服务启动";
                        break;
                }
            }
        }
        public void DTS_Flow(BP.WF.Flow fl)
        { 
            #region 读取数据.
            BP.Sys.MapExt me = new MapExt();
            me.MyPK = "ND" + int.Parse(fl.No) + "01" + "_" + MapExtXmlList.PageLoadFull;
            int i = me.RetrieveFromDBSources();
            if (i == 0)
            {
                BP.DA.Log.DefaultLogWriteLineError("没有为流程(" + fl.Name + ")的开始节点设置发起数据,请参考说明书解决.");
                return;
            }

            // 获取从表数据.
            DataSet ds = new DataSet();
            string[] dtlSQLs = me.Tag1.Split('*');
            foreach (string sql in dtlSQLs)
            {
                if (string.IsNullOrEmpty(sql))
                    continue;

                string[] tempStrs = sql.Split('=');
                string dtlName = tempStrs[0];
                DataTable dtlTable = BP.DA.DBAccess.RunSQLReturnTable(sql.Replace(dtlName + "=", ""));
                dtlTable.TableName = dtlName;
                ds.Tables.Add(dtlTable);
            }
            #endregion 读取数据.

            #region 检查数据源是否正确.
            string errMsg = "";
            // 获取主表数据.
            DataTable dtMain = BP.DA.DBAccess.RunSQLReturnTable(me.Tag);
            if (dtMain.Rows.Count == 0)
            {
                BP.DA.Log.DefaultLogWriteLineError("流程(" + fl.Name + ")此时无任务.");
                this.SetText("流程(" + fl.Name + ")此时无任务.");
                return;
            }

            this.SetText("@查询到("+dtMain.Rows.Count+")条任务.");

            if (dtMain.Columns.Contains("Starter") == false)
                errMsg += "@配值的主表中没有Starter列.";

            if (dtMain.Columns.Contains("MainPK") == false)
                errMsg += "@配值的主表中没有MainPK列.";
           
            if (errMsg.Length > 2)
            {
                this.SetText(errMsg);
                BP.DA.Log.DefaultLogWriteLineError("流程(" + fl.Name + ")的开始节点设置发起数据,不完整." + errMsg);
                return;
            }
            #endregion 检查数据源是否正确.

            #region 处理流程发起.
            string nodeTable = "ND" + int.Parse(fl.No) + "01";
            int idx = 0;
            foreach (DataRow dr in dtMain.Rows)
            {
                idx++;

                string mainPK = dr["MainPK"].ToString();
                string sql = "SELECT OID FROM " + nodeTable + " WHERE MainPK='" + mainPK + "'";
                if (DBAccess.RunSQLReturnTable(sql).Rows.Count != 0)
                {
                    this.SetText("@" + fl.Name + ",第" + idx + "条,此任务在之前已经完成。");
                    continue; /*说明已经调度过了*/
                }

                string starter = dr["Starter"].ToString();
                if (WebUser.No != starter)
                {
                    BP.Web.WebUser.Exit();
                    BP.Port.Emp emp = new BP.Port.Emp();
                    emp.No = starter;
                    if (emp.RetrieveFromDBSources() == 0)
                    {
                        this.SetText("@" + fl.Name + ",第" + idx + "条,设置的发起人员:" + emp.No + "不存在.");
                        BP.DA.Log.DefaultLogWriteLineInfo("@数据驱动方式发起流程(" + fl.Name + ")设置的发起人员:" + emp.No + "不存在。");
                        continue;
                    }
                    WebUser.SignInOfGener(emp);
                }

                #region  给值.
                Work wk = fl.NewWork();
                foreach (DataColumn dc in dtMain.Columns)
                    wk.SetValByKey(dc.ColumnName, dr[dc.ColumnName].ToString());

                if (ds.Tables.Count != 0)
                {
                    // MapData md = new MapData(nodeTable);
                    MapDtls dtls = new MapDtls(nodeTable);
                    foreach (MapDtl dtl in dtls)
                    {
                        foreach (DataTable dt in ds.Tables)
                        {
                            if (dt.TableName != dtl.No)
                                continue;

                            //删除原来的数据。
                            GEDtl dtlEn = dtl.HisGEDtl;
                            dtlEn.Delete(GEDtlAttr.RefPK, wk.OID.ToString());

                            // 执行数据插入。
                            foreach (DataRow drDtl in dt.Rows)
                            {
                                if (drDtl["RefMainPK"].ToString() != mainPK)
                                    continue;

                                dtlEn = dtl.HisGEDtl;
                                foreach (DataColumn dc in dt.Columns)
                                    dtlEn.SetValByKey(dc.ColumnName, drDtl[dc.ColumnName].ToString());

                                dtlEn.RefPK = wk.OID.ToString();
                                dtlEn.OID = 0;
                                dtlEn.Insert();
                            }
                        }
                    }
                }
                #endregion  给值.

                // 处理发送信息.
                Node nd = fl.HisStartNode;
                try
                {
                    WorkNode wn = new WorkNode(wk, nd);
                    string msg = wn.AfterNodeSave();
                    BP.DA.Log.DefaultLogWriteLineInfo(msg);
                    this.SetText("@" + fl.Name + ",第" + idx + "条,发起人员:" + WebUser.No + "-" + WebUser.Name + "已完成.\r\n" + msg);
                    //this.SetText("@第（" + idx + "）条任务，" + WebUser.No + " - " + WebUser.Name + "已经完成。\r\n" + msg);
                }
                catch (Exception ex)
                {
                    this.SetText("@" + fl.Name + ",第" + idx + "条,发起人员:" + WebUser.No + "-" + WebUser.Name + "发起时出现错误.\r\n" + ex.Message);
                    BP.DA.Log.DefaultLogWriteLineWarning(ex.Message);
                }
            }
            #endregion 处理流程发起.
        }
        /// <summary>
        /// 发送邮件。
        /// </summary>
        /// <param name="sms"></param>
        public void SendMail(BP.TA.SMS sms)
        {
            System.Net.Mail.MailMessage myEmail = new System.Net.Mail.MailMessage();
            myEmail.From = new MailAddress("ccflow.cn@gmail.com", "ccflow", System.Text.Encoding.UTF8);

            myEmail.To.Add( sms.Email);
            myEmail.Subject = sms.EmailTitle;
            myEmail.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码


            myEmail.Body = sms.EmailDoc;
            myEmail.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码
            myEmail.IsBodyHtml = true;//是否是HTML邮件

            myEmail.Priority = MailPriority.High;//邮件优先级

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("ccflow.cn@gmail.com", "ccflow123");

            //上述写你的GMail邮箱和密码
            client.Port = 587;//Gmail使用的端口
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;//经过ssl加密
            object userState = myEmail;
            try
            {
                //简单一点儿可以client.Send(msg);
                // MessageBox.Show("发送成功");

                client.SendAsync(myEmail, userState);

                sms.HisSMSSta = BP.TA.SMSSta.RunOK;
                sms.Update();
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                throw ex;
            }
        }
        private void SetText(string text)
        {
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);

                try
                {
                    this.Invoke(d, new object[] { text });
                }
                catch
                {
                }
            }
            else
            {
                this.textBox1.Text += "\r\n" + text;
                this.textBox1.SelectionStart = this.textBox1.TextLength;
                this.textBox1.ScrollToCaret();
            }
        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            this.HisScanSta = ScanSta.Stop;
            this.Close();
        }

        private void FrmMain_SizeChanged(object sender, EventArgs e)
        {
            this.Hide();
            this.notifyIcon1.Visible = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            notifyIcon1_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BP.WF.Flow fl = new BP.WF.Flow("040");
            BP.WF.Dev2Interface.DTS_AutoStarterFlow(fl);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //  把流程运行到最后的节点上去，并且结束流程。
            //string file = @"C:\aa\流程已完成.xls";
            //string info = BP.WF.Glo.LoadFlowDataWithToSpecEndNode(file);
            //BP.DA.Log.DefaultLogWriteLineInfo(info);


            string file1 = @"C:\aa\流程未完成.xls";
            string info1 = BP.WF.Glo.LoadFlowDataWithToSpecNode(file1);
            BP.DA.Log.DefaultLogWriteLineInfo(info1);

            MessageBox.Show("执行成功。");
        }
    }
}
