using System;
using BP.Port;
using System.Windows.Forms;
using BP.WF;
using BP.Comm;
namespace WFWord2007
{
    partial class Ribbon1 : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public void LoadXml()
        {
            BP.WF.Tabs tabs = new BP.WF.Tabs();
            tabs.RetrieveAll();

            BP.WF.GroupFuncs gs = new BP.WF.GroupFuncs();
            gs.RetrieveAll();

            BP.WF.Funcs fs = new BP.WF.Funcs();
            fs.RetrieveAll();

            this.SuspendLayout();
            this.Tabs.Clear();
            int i = 1;
            foreach (BP.WF.Tab tb in tabs)
            {
                i++;
                Microsoft.Office.Tools.Ribbon.RibbonTab mytab = Factory.CreateRibbonTab();
                mytab.Label = tb.Name;
                mytab.Name = "t" + tb.No + i;
                if (i == 2)
                    mytab.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Custom;
                else
                    mytab.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;

                mytab.SuspendLayout();

                foreach (BP.WF.GroupFunc g in gs)
                {
                    if (g.FK_Tab != tb.No)
                        continue;

                    Microsoft.Office.Tools.Ribbon.RibbonGroup group = Factory.CreateRibbonGroup();
                    group.Name = "s" + g.No;
                    group.Label = g.Name;
                    group.DialogLauncherClick += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(Btn_Click);
                    group.SuspendLayout();

                    foreach (BP.WF.Func f in fs)
                    {
                        if (f.FK_Group != g.No)
                            continue;

                        switch (f.CtlType)
                        {
                            case "Btn":
                                Microsoft.Office.Tools.Ribbon.RibbonButton btn = Factory.CreateRibbonButton();
                                btn.Name = "Btn_" + f.No;
                                btn.Label = f.Name;
                                btn.Tag = f;
                                try
                                {
                                    if (f.IsIcon)
                                    {
                                        btn.Image = System.Drawing.Image.FromFile(BP.WF.Glo.PathOfTInstall + "\\Img\\" + f.No + ".gif");
                                        btn.ShowImage = true;
                                    }
                                }
                                catch
                                {
                                }
                                btn.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(Btn_Click);
                                group.Items.Add(btn);
                                break;
                            default:
                                Microsoft.Office.Tools.Ribbon.RibbonLabel lab = Factory.CreateRibbonLabel();
                                lab.Name = "Lab_" + f.No;
                                lab.Label = f.Name;
                                lab.Tag = f;
                                group.Items.Add(lab);
                                break;
                        }
                    }
                    group.ResumeLayout(false);
                    group.PerformLayout();
                    mytab.Groups.Add(group);
                } // End add to Group.

                mytab.ResumeLayout(false);
                mytab.PerformLayout();
                this.Tabs.Add(mytab);
            } // End add to Tab.

            this.ResumeLayout(false);
            this.RibbonType = "Microsoft.Word.Document";
            //  this.RibbonType = "Microsoft.PowerPoint.Presentation";
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(Ribbon1_Load);
        }

        void Btn_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
        {
            try
            {
                Microsoft.Office.Tools.Ribbon.RibbonButton btn = (Microsoft.Office.Tools.Ribbon.RibbonButton)sender;
                BP.WF.Func func = (BP.WF.Func)btn.Tag;
                switch (func.DoType)
                {
                    case "RunExe":
                        System.Diagnostics.Process.Start(func.Tag);
                        return;
                    case "RunIE":
                        switch (func.No)
                        {

                            case "Send": //要执行签发.
                                if (WebUser.FK_Flow == null)
                                {
                                    MessageBox.Show("您没有执行公文拟稿的过程不能签发。");
                                    return;
                                }
                                if (WebUser.WorkID == 0)
                                {
                                    Work wk = new Work();
                                    wk.FK_Dept = WebUser.FK_Dept;
                                    wk.Title = "公文拟稿-" + DateTime.Now.ToString("MM月dd日hh时mm分");
                                    wk.NodeID = WebUser.FK_Node;
                                    wk.RDT = DateTime.Now.ToString("yyyy-MM-dd");
                                    wk.Rec = WebUser.No;
                                    wk.Insert();
                                    WebUser.WorkID = wk.OID;
                                }
                                else
                                {
                                    /*判断是否已经处理了。*/
                                    Work wk1 = new Work(WebUser.FK_Node, WebUser.WorkID);
                                    switch (wk1.HisNodeState)
                                    {
                                        case NodeState.Init:
                                        case NodeState.Back:
                                            break;
                                        default:
                                            MessageBox.Show("工作已经转入了下一个环节，您不能在处理了。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                                            return;
                                    }
                                }
                                break;
                            case "Return":
                                break;
                            case "UnSend":
                                if (MessageBox.Show("您确定要撤消发送吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                    return;
                                break;
                            case "Del":
                                if (MessageBox.Show("您确定要执行删除吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                    return;
                                break;
                            default:
                                break;
                        }

                        FrmIE ie = new FrmIE();
                        ie.Width = func.Width;
                        ie.Height = func.Height;

                        string tag = func.Tag;
                        tag = tag.Replace("@Serv", BP.WF.Glo.WFServ);
                        ie.ShowUrl(tag + "&UserNo=" + WebUser.No + "&FK_Flow=" + BP.Port.WebUser.FK_Flow + "&FK_Node=" + WebUser.FK_Node + "&WorkID=" + WebUser.WorkID);
                        ie.Text = "您好：" + WebUser.No + "，" + WebUser.Name + "。  -  " + func.Name;
                        ie.ShowInTaskbar = false;
                        ie.HisRibbon1 = this;
                        ie.ShowDialog();
                        this.ReSetState();
                        return;
                    default:
                        try
                        {
                            this.Do(func, btn);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("执行" + func.Name + "出现错误。" + ex.Message);
                        }
                        break;
                }

                this.ReSetState();
            }
            catch (Exception ex)
            {
                ReSetState();
                MessageBox.Show(ex.Message);
            }
        }

        #region 处理状态
        public void ReSetState()
        {
            this.Btn_Del.Enabled = true;
            this.Btn_Start.Enabled = true;
            this.Btn_Send.Enabled = true;
            this.Btn_Return.Enabled = true;
            this.Btn_FW.Enabled = true;
            this.Btn_UnSend.Enabled = true;
            this.Btn_Rpt.Enabled = true;
            this.Btn_Save.Enabled = true;
            this.Btn_History.Enabled = true;
            this.Btn_Start.Enabled = true;


            //保存选项
            this.Btn_SaveAs.Enabled = true;
            this.Btn_SaveAsPDF.Enabled = true;
            this.Btn_SaveToU.Enabled = true;
            this.Btn_SendToMail.Enabled = true;

            //流程功能
            this.Btn_EmpWorks.Enabled = true;
            this.Btn_Runing.Enabled = true;
            this.Btn_View.Enabled = true;



            switch (WebUser.DoWhat)
            {
                case "":
                    break;
                default:
                    break;
            }

            if (WebUser.No == null)
            {
                this.Btn_EmpWorks.Enabled = false;
                this.Btn_Runing.Enabled = false;
                this.Btn_View.Enabled = false;
                this.Btn_Start.Enabled = false;
            }

            if (WFWord2007.Globals.ThisAddIn.Application.Documents.Count == 0)
            {
                this.Btn_Start.Enabled = true;
                this.Btn_Send.Enabled = false;
                this.Btn_Return.Enabled = false;
                this.Btn_FW.Enabled = false;
                this.Btn_UnSend.Enabled = false;
                this.Btn_Rpt.Enabled = false;
                this.Btn_Save.Enabled = false;
                this.Btn_History.Enabled = false;
                this.Btn_SaveAs.Enabled = false;
                this.Btn_SaveAsPDF.Enabled = false;
                this.Btn_SaveToU.Enabled = false;
                this.Btn_SendToMail.Enabled = false;
                this.Btn_Del.Enabled = false;
                return;
            }



            if (WebUser.FK_Flow == null)
            {
                this.Btn_Save.Enabled = false;
                this.Btn_Send.Enabled = false;
                this.Btn_Del.Enabled = false;
            }


            if (WebUser.WorkID == 0)
            {
                this.Btn_FW.Enabled = false;
                this.Btn_UnSend.Enabled = false;
                this.Btn_Rpt.Enabled = false;
                this.Btn_Return.Enabled = false;
                this.Btn_History.Enabled = false;
                this.Btn_Del.Enabled = false;
            }
            else
            {
                if (WebUser.IsStartNode)
                {
                    this.Btn_UnSend.Enabled = false;
                    this.Btn_Rpt.Enabled = false;
                    this.Btn_Return.Enabled = false;
                    this.Btn_FW.Enabled = false;
                    this.Btn_History.Enabled = false;
                    this.Btn_Del.Enabled = true;
                }
                else
                {
                    this.Btn_Del.Enabled = false;
                    if (WebUser.HisWork == null)
                    {
                        this.Btn_UnSend.Enabled = false;
                        this.Btn_Rpt.Enabled = false;
                        this.Btn_Return.Enabled = false;
                        this.Btn_FW.Enabled = false;
                        this.Btn_History.Enabled = false;
                    }
                    else
                    {
                        if (WebUser.HisWork.Rec != WebUser.No)
                        {
                            this.Btn_UnSend.Enabled = false;
                            //  this.Btn_Rpt.Enabled = false;
                            this.Btn_Return.Enabled = false;
                            this.Btn_FW.Enabled = false;
                        }
                    }
                }
            }
        }
        public Microsoft.Office.Tools.Ribbon.RibbonButton Btn_Rpt
        {
            get
            {
                return this.GetBtn("Btn_Rpt");
            }
        }
        public Microsoft.Office.Tools.Ribbon.RibbonButton Btn_UnSend
        {
            get
            {
                return this.GetBtn("Btn_UnSend");
            }
        }

        public Microsoft.Office.Tools.Ribbon.RibbonButton Btn_Del
        {
            get
            {
                return this.GetBtn("Btn_Del");
            }
        }

        public Microsoft.Office.Tools.Ribbon.RibbonButton Btn_EmpWorks
        {
            get
            {
                return this.GetBtn("Btn_EmpWorks");
            }
        }
        public Microsoft.Office.Tools.Ribbon.RibbonButton Btn_Runing
        {
            get
            {
                return this.GetBtn("Btn_Runing");
            }
        }


        public Microsoft.Office.Tools.Ribbon.RibbonButton Btn_View
        {
            get
            {
                return this.GetBtn("Btn_View");
            }
        }


        public Microsoft.Office.Tools.Ribbon.RibbonButton Btn_Reg
        {
            get
            {
                return this.GetBtn("Btn_Reg");
            }
        }


        public Microsoft.Office.Tools.Ribbon.RibbonButton Btn_ReLogin
        {
            get
            {
                return this.GetBtn("Btn_ReLogin");
            }
        }


        public Microsoft.Office.Tools.Ribbon.RibbonButton Btn_LogOut
        {
            get
            {
                return this.GetBtn("Btn_LogOut");
            }
        }

        public Microsoft.Office.Tools.Ribbon.RibbonButton Btn_Return
        {
            get
            {
                return this.GetBtn("Btn_Return");
            }
        }
        public Microsoft.Office.Tools.Ribbon.RibbonButton Btn_FW
        {
            get
            {
                return this.GetBtn("Btn_FW");
            }
        }
        public Microsoft.Office.Tools.Ribbon.RibbonButton Btn_Send
        {
            get
            {
                return this.GetBtn("Btn_Send");
            }
        }
        public Microsoft.Office.Tools.Ribbon.RibbonButton Btn_Save
        {
            get
            {
                return this.GetBtn("Btn_Save");
            }
        }
        public Microsoft.Office.Tools.Ribbon.RibbonButton Btn_SendToMail
        {
            get
            {
                return this.GetBtn("Btn_SendToMail");
            }
        }
        public Microsoft.Office.Tools.Ribbon.RibbonButton Btn_SaveAs
        {
            get
            {
                return this.GetBtn("Btn_SaveAs");
            }
        }
        public Microsoft.Office.Tools.Ribbon.RibbonButton Btn_SaveAsPDF
        {
            get
            {
                return this.GetBtn("Btn_SaveAsPDF");
            }
        }
        public Microsoft.Office.Tools.Ribbon.RibbonButton Btn_Start
        {
            get
            {
                return this.GetBtn("Btn_Start");
            }
        }


        public Microsoft.Office.Tools.Ribbon.RibbonButton Btn_SaveToU
        {
            get
            {
                return this.GetBtn("Btn_SaveToU");
            }
        }

        public Microsoft.Office.Tools.Ribbon.RibbonButton Btn_History
        {
            get
            {
                return this.GetBtn("Btn_History");
            }
        }
        #endregion 处理状态

        public Microsoft.Office.Tools.Ribbon.RibbonButton GetBtn(string key)
        {
            foreach (Microsoft.Office.Tools.Ribbon.RibbonTab tab in this.Tabs)
            {
                foreach (Microsoft.Office.Tools.Ribbon.RibbonGroup g in tab.Groups)
                {
                    Microsoft.Office.Tools.Ribbon.RibbonButton btn;
                    for (int i = 0; i <= g.Items.Count; i++)
                    {
                        try
                        {
                            btn = g.Items[i] as Microsoft.Office.Tools.Ribbon.RibbonButton;
                            if (btn == null)
                                continue;

                            if (btn.Name == key)
                                return btn;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            MessageBox.Show("@没有找到ID=" + key + " 的 btn");
            return null;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="func"></param>
        /// <param name="btn"></param>
        public void Do(BP.WF.Func func, Microsoft.Office.Tools.Ribbon.RibbonButton btn)
        {
            switch (func.No)
            {
                case "LogOut":
                    if (MessageBox.Show("您确定要注销吗？", "执行确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                        return;

                    WebUser.No = null;
                    WebUser.Name = null;
                    WebUser.Pass = null;
                    WebUser.WorkID = 0;
                    WebUser.FK_Dept = null;
                    WebUser.FK_DeptName = null;
                    WebUser.FK_Flow = null;
                    WebUser.FK_Node = 0;
                    WebUser.HisDept = null;
                    WebUser.isLogin = false;
                    WebUser.IsSaveInfo = false;
                    WebUser.IsSavePass = false;
                    WebUser.SID = null;
                    try
                    {
                        System.IO.File.Delete("C:\\WF\\Profile.txt");
                    }
                    catch
                    {
                    }
                    this.ReSetState();
                    break;
                case "ReLogin":
                    FrmLogin fl = new FrmLogin();
                    fl.ShowDialog();
                    this.ReSetState();
                    break;
                case "Exit":
                    DialogResult dl = MessageBox.Show("您已经安全退出。", "您确定退出吗？", MessageBoxButtons.OKCancel);
                    if (dl != DialogResult.OK)
                        return;

                    System.IO.File.Delete("D:\\ShiDai\\Profile.txt");
                    btn.Enabled = false;
                    break;
                case "ChUser": // 切换用户.
                    FrmLogin fm = new FrmLogin();
                    fm.ShowDialog();
                    break;
                case "UploadKJ": // 课件.
                case "Upload":   // 课件.
                    //FrmUpload fm1 = new FrmUpload();
                    //fm1.ShowDialog();
                    break;
                case "WKInfo":
                    string msg = "\t\n No=" + WebUser.No;
                    msg += "\t\n FK_Flow=" + WebUser.FK_Flow;
                    msg += "\t\n FK_Node=" + WebUser.FK_Node;
                    msg += "\t\n WorkID=" + WebUser.WorkID;
                    MessageBox.Show(msg);
                    break;
                case "About":
                    AboutBox ab = new AboutBox();
                    ab.ShowDialog();
                    break;
                case "Save":
                    this.DoSave();
                    break;
                case "SaveTo":
                default:
                    MessageBox.Show("功能未实现：" + func.No + " " + func.Name);
                    break;
            }
        }
        /// <summary>
        /// 执行保存公文
        /// </summary>
        public void DoSave()
        {
            WFWord2007.Globals.ThisAddIn.DoSave();
        }

        public Ribbon1()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            if (BP.Port.WebUser.LoadProfile() == false)
            {
                FrmLogin lg = new FrmLogin();
                DialogResult dl = lg.ShowDialog();
                if (dl != DialogResult.OK)
                    return;
            }

            try
            {
                this.LoadXml();
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

            BP.Port.WebUser.HisRib = this;
            //  WFWord2007.Globals.ThisAddIn.HisRibbon1 = this;
            return;
        }

        #endregion

        //internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon1 Ribbon1
        {
            get { return this.GetRibbon<Ribbon1>(); }
        }
    }
}
