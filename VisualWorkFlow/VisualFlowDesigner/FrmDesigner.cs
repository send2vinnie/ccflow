using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using BP.WF;

namespace BP.Win.WF
{
	/// <summary>
	/// WFDesigner 的摘要说明。
	/// </summary>
	public class FrmDesigner : WFForm
	{
        private BP.Win.WF.WinWFFlow winFlow1;
        private ContextMenuStrip contextMenuStrip1;
        private ImageList imageList1;
        private ToolStripMenuItem Btn_Rpt;
        private ToolStripMenuItem Btn_Check;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem Btn_New;
        private ToolStripMenuItem Btn_Del;
        private ToolStripMenuItem Btn_Copy;
        private ToolStripMenuItem Btn_GenerFlowTemplate;
        private ToolStripMenuItem Btn_Run;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem Btn_DataTo;
        private ToolStripMenuItem FlowFlowToolStripMenuItem;
        private ToolStripMenuItem FlowToolStripMenuItem;
        private ToolStripMenuItem AddNodeToolStripMenuItem;
        private ToolStripMenuItem CheckNodeToolStripMenuItem;
        private ToolStripMenuItem OrdinNodeToolStripMenuItem;
        private ToolStripMenuItem LabToolStripMenuItem;
        private ToolStripMenuItem NodeLineToolStripMenuItem;
        private IContainer components;
    
		public FrmDesigner()
		{
			InitializeComponent();
			this.winFlow1 = new WinWFFlow();
			this.winFlow1.Dock =DockStyle.Fill;
			this.Controls.Add(this.winFlow1);  
		}
		public WinWFFlow HisWinFlow
		{
			get
			{
				return this.winFlow1;
			}
		}
        public bool IsEdit = false;
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (this.IsEdit)
            {
                if (MessageBox.Show(this.ToE("NoSaveClose","没有保存，您确定要保存吗？"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    base.OnFormClosing(e);
                    return;
                }
                else
                {
                    this.Activate();
                }
            }
            base.OnFormClosing(e);
        }
         
		public void BindData(Flow flow)
		{
			this.winFlow1.BindData( flow );
		}

        public void Save()
        {
            this.winFlow1.Save();
        }

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDesigner));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CheckNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OrdinNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NodeLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FlowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_Run = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_Check = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Btn_Rpt = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_DataTo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Btn_New = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_Del = new System.Windows.Forms.ToolStripMenuItem();
         //   this.Btn_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_GenerFlowTemplate = new System.Windows.Forms.ToolStripMenuItem();


            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.FlowFlowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddNodeToolStripMenuItem,
            this.FlowToolStripMenuItem,
            this.Btn_Run,
            this.Btn_Check,
            this.toolStripSeparator2,
            this.Btn_Rpt,
            this.Btn_DataTo,
            this.toolStripSeparator1,
            this.Btn_New,
            this.Btn_Del,
            this.Btn_GenerFlowTemplate
            });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(184, 214);
            this.contextMenuStrip1.Text = "流程菜单";
            this.contextMenuStrip1.DoubleClick += new System.EventHandler(this.contextMenuStrip1_DoubleClick);
            // 
            // AddNodeToolStripMenuItem
            // 
            this.AddNodeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CheckNodeToolStripMenuItem,
            this.OrdinNodeToolStripMenuItem,
            this.LabToolStripMenuItem,
            this.NodeLineToolStripMenuItem});

          //  this.AddNodeToolStripMenuItem.Image = global::FlowDesign.Properties.Resources.WorkFlowOp;

            this.AddNodeToolStripMenuItem.Name = "AddNodeToolStripMenuItem";
            this.AddNodeToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.AddNodeToolStripMenuItem.Text = this.ToE("NewNode", "增加节点"); //增加节点
          //  this.AddNodeToolStripMenuItem.Text = "增加节点"; 

            // 
            // CheckNodeToolStripMenuItem
            // 
            //this.CheckNodeToolStripMenuItem.Image = global::FlowDesign.Properties.Resources.StandardChecks;
            //this.CheckNodeToolStripMenuItem.Name = "CheckNode";
            //this.CheckNodeToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            //this.CheckNodeToolStripMenuItem.Text = this.ToE("CheckNode", "审核节点");
            //this.CheckNodeToolStripMenuItem.DoubleClick += new System.EventHandler(this.Menu_Click);
            //this.CheckNodeToolStripMenuItem.Click += new System.EventHandler(this.Menu_Click);
            // 
            // OrdinNodeToolStripMenuItem
            // 
   //         this.OrdinNodeToolStripMenuItem.Image = global::FlowDesign.Properties.Resources.Work;
            this.OrdinNodeToolStripMenuItem.Name = "OrdinaryNode";
            this.OrdinNodeToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.OrdinNodeToolStripMenuItem.Text = this.ToE("OrdinaryNode", "普通节点"); 
            this.OrdinNodeToolStripMenuItem.Click += new System.EventHandler(this.Menu_Click);

            // 
            // LabToolStripMenuItem
            // 
            this.LabToolStripMenuItem.Name = "Label";
            this.LabToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.LabToolStripMenuItem.Text = this.ToE("Label", "标签");
            this.LabToolStripMenuItem.DoubleClick += new System.EventHandler(this.Menu_Click);
            this.LabToolStripMenuItem.Click += new System.EventHandler(this.Menu_Click);
            // 
            // NodeLineToolStripMenuItem
            // 
            this.NodeLineToolStripMenuItem.Name = "NodeLine";
            this.NodeLineToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.NodeLineToolStripMenuItem.Text = this.ToE("NodeLine", "节点连线"); ;
            this.NodeLineToolStripMenuItem.DoubleClick += new System.EventHandler(this.Menu_Click);
            this.NodeLineToolStripMenuItem.Click += new System.EventHandler(this.Menu_Click);

            // 
            // FlowToolStripMenuItem
            // 
            this.FlowToolStripMenuItem.Checked = true;
            this.FlowToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
          //  this.FlowToolStripMenuItem.Image = global::FlowDesign.Properties.Resources.Authorize;
            this.FlowToolStripMenuItem.Name = "FlowProperty";
            this.FlowToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.FlowToolStripMenuItem.Text = this.ToE("FlowProperty","流程属性") ;
            this.FlowToolStripMenuItem.DoubleClick += new System.EventHandler(this.Menu_Click);
            this.FlowToolStripMenuItem.Click += new System.EventHandler(this.Menu_Click);

            // 
            // Btn_Run
            // 
   //         this.Btn_Run.Image = global::FlowDesign.Properties.Resources.Start;
            this.Btn_Run.Name = "Run";
            this.Btn_Run.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.Btn_Run.Size = new System.Drawing.Size(183, 22);
            this.Btn_Run.Text = this.ToE("RunFlow", "运行流程");
            this.Btn_Run.DoubleClick += new System.EventHandler(this.Menu_Click);
            this.Btn_Run.Click += new System.EventHandler(this.Menu_Click);
            // 
            // Btn_Check
            // 
          //  this.Btn_Check.Image = global::FlowDesign.Properties.Resources.Confirm;
            this.Btn_Check.Name = "FlowRpt";
            this.Btn_Check.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.Btn_Check.Size = new System.Drawing.Size(183, 22);
            this.Btn_Check.Text = this.ToE("FlowRpt", "流程诊断报告");
            this.Btn_Check.DoubleClick += new System.EventHandler(this.Menu_Click);
            this.Btn_Check.Click += new System.EventHandler(this.Menu_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(180, 6);
            // 
            // Btn_Rpt
            // 
            this.Btn_Rpt.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Rpt.Image")));
            this.Btn_Rpt.Name = "RptDefinition";
            this.Btn_Rpt.Size = new System.Drawing.Size(183, 22);
            this.Btn_Rpt.Text = this.ToE("RptDefinition", "报表定义"); // "报表定义";
            this.Btn_Rpt.DoubleClick += new System.EventHandler(this.Menu_Click);
            this.Btn_Rpt.Click += new System.EventHandler(this.Menu_Click);
            // 
            // Btn_DataTo
            // 
   //         this.Btn_DataTo.Image = global::FlowDesign.Properties.Resources.Table;
            this.Btn_DataTo.Name = "RptDefinitionTurn";
            this.Btn_DataTo.Size = new System.Drawing.Size(183, 22);
            this.Btn_DataTo.Text = this.ToE("RptDefinitionTurn", "数据转出定义");  //"数据转出定义";
            this.Btn_DataTo.ToolTipText = "在流程运行完时数据按照一定的规则转出到指定的系统表中。";
            this.Btn_DataTo.DoubleClick += new System.EventHandler(this.Menu_Click);
            this.Btn_DataTo.Click += new System.EventHandler(this.Menu_Click);

            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(180, 6);
            // 
            // Btn_New
            // 
         //   this.Btn_New.Image = global::FlowDesign.Properties.Resources.New;
            this.Btn_New.Name = "NewFlow";
            this.Btn_New.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.Btn_New.Size = new System.Drawing.Size(183, 22);
            this.Btn_New.Text = this.ToE("NewFlow", "新建流程");// "新建流程";
            this.Btn_New.DoubleClick += new System.EventHandler(this.Menu_Click);
            this.Btn_New.Click += new System.EventHandler(this.Menu_Click);
            // 
            // Btn_Del
            // 
   //         this.Btn_Del.Image = global::FlowDesign.Properties.Resources.Delete;
            this.Btn_Del.Name = "Delete";
            this.Btn_Del.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.Btn_Del.Size = new System.Drawing.Size(183, 22);
            this.Btn_Del.Text = this.ToE("Delete", "删除");  //"删除";
            this.Btn_Del.DoubleClick += new System.EventHandler(this.Menu_Click);
            this.Btn_Del.Click += new System.EventHandler(this.Menu_Click);


            //// 
            //// Btn_Copy
            //// 
            ////this.Btn_Copy.Image = global::FlowDesign.Properties.Resources.Confirm;
            //this.Btn_Copy.Name = "Copy";
            ////this.Btn_Copy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            //this.Btn_Copy.Size = new System.Drawing.Size(183, 22);
            //this.Btn_Copy.Text = this.ToE("Copy", "复制流程");  //"复制";
            //this.Btn_Copy.DoubleClick += new System.EventHandler(this.Menu_Click);
            //this.Btn_Copy.Click += new System.EventHandler(this.Menu_Click);


            // 
            // Btn_GenerFlowTemplate
            // 
            //this.Btn_Copy.Image = global::FlowDesign.Properties.Resources.Confirm;
            this.Btn_GenerFlowTemplate.Name = "GenerFlowTemplate";
            this.Btn_GenerFlowTemplate.Size = new System.Drawing.Size(183, 22);

            this.Btn_GenerFlowTemplate.Text = this.ToE("GenerFlowTemplate", "生成流程模版");  //"复制";

            this.Btn_GenerFlowTemplate.DoubleClick += new System.EventHandler(this.Menu_Click);
            this.Btn_GenerFlowTemplate.Click += new System.EventHandler(this.Menu_Click);

            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "New.gif");
            this.imageList1.Images.SetKeyName(1, "Save.gif");
            this.imageList1.Images.SetKeyName(2, "DataIO.gif");
            this.imageList1.Images.SetKeyName(3, "Delete.gif");
            //this.imageList1.Images.SetKeyName(4, "Copy.gif");

            // 
            // FlowFlowToolStripMenuItem
            // 
            this.FlowFlowToolStripMenuItem.Name = "FlowProperty";
            this.FlowFlowToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.FlowFlowToolStripMenuItem.Text = this.ToE("FlowProperty", "流程属性");  //"流程属性";
            this.FlowFlowToolStripMenuItem.ToolTipText = "执行流程的相关设计。";
            this.FlowFlowToolStripMenuItem.DoubleClick += new System.EventHandler(this.Menu_Click);
            this.FlowFlowToolStripMenuItem.Click += new System.EventHandler(this.Menu_Click);
            // 
            // FrmDesigner
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
       //     this.BackgroundImage = global::FlowDesign.Properties.Resources.BJ1;
            this.ClientSize = new System.Drawing.Size(447, 379);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmDesigner";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = this.ToE("FlowDesign", "工作流设计");  //"工作流设计";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.DoubleClick += new System.EventHandler(this.contextMenuStrip1_DoubleClick);
            this.Load += new System.EventHandler(this.FrmDesigner_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
		}
		#endregion

		private void FrmDesigner_Load(object sender, System.EventArgs e)
		{
			this.BackColor=Color.DarkBlue ;
		}
        private void Menu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem btn = sender as ToolStripMenuItem;
            if (btn == null)
            {
                PubClass.Alert(e.ToString());
                return;
            }

            DoText( btn.Name.Replace("Btn_Run","") );
        }

        public void DoText(string text)
        {
            switch (text)
            {

                case "Run":
                case "运行流程":
                case "启动流程":
                    BP.WF.Global.DoUrlByType("RunFlow&FK_Flow=" + this.HisWinFlow.HisFlow.No, null);
                    break;
                case "保存":
                case "Save":
                    this.HisWinFlow.Save();
                    break;
                case "FlowRpt":
                case "流程设计检查报告":
                case "设计检查报告":
                case "流程诊断报告":
                case "流程校验":
                    this.Save();
                    BP.WF.Global.DoUrlByType("FlowCheck", this.HisWinFlow.HisFlow.No);
                    break;
                case "CheckFlow":
                case "RptDefinition":
                case "RptDefinitionTurn":
                case "检查流程正确性":
                case "报表定义":
                case "数据转出定义":
                    BP.WF.Global.DoUrl("/DoPort.aspx?DoType=WFRpt&PK=ND" + int.Parse(this.HisWinFlow.HisFlow.No) + "Rpt");
                    //报表定义
                    //BP.WF.Global.DoEdit(this.HisWinFlow.HisFlow);
                    break;
                case "新建流程":
                case "新建":
                case "NewFlow":
                    MessageBox.Show(this.ToE("WhenNewFlow", "请点，工具栏上的新建按钮，或者在树菜单上点右键。"), " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "删除":
                case "Delete":
                case "Del":
                case "删除流程":
                    if (BP.Win32.PubClass.Question(this.ToE("AYS", "您确定吗？")) == false)
                        return;

                    if (BP.Win32.PubClass.Question(this.ToE("AYS", "您确定吗？")) == false)
                        return;
                    this.HisWinFlow.HisFlow.DoDelete();
                    this.Close();
                    break;
                case "FlowProperty":
                case "属性":
                case "流程属性":
                    Flow fl = this.HisWinFlow.HisFlow;
                    BP.WF.Global.DoEdit(fl);
                    return;

                case "OrdinaryNode":
                case "普通节点":
                    BP.Win.WF.Global.ToolIndex = 1;
                    break;
                case "审核节点":
                case "CheckNode":
                    BP.Win.WF.Global.ToolIndex = 1;
                    break;
                case "节点连线":
                case "节点连接线":
                case "NodeLine":
                    MessageBox.Show(this.ToE("WhenAddLine",
                        "当鼠标呈十字形时，请先点连接的第一个节点，然后点要连接的第二个节点。"),
                        " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BP.Win.WF.Global.ToolIndex = 2;
                    break;
                case "标签":
                case "Label":
                    BP.Win.WF.Global.ToolIndex = 3;
                    break;
                //  case "复制节点":
                //case "NodeCopy":
                //    BP.Win.WF.Global.ToolIndex = 1;
                //    if (BP.Win32.PubClass.Question(this.ToE("AYS", "您确定吗？")) == false)
                //        return;
                //    Flow fl1 = this.HisWinFlow.HisFlow;
                //    fl1.DoCopy();
                //    break;
                case "复制":
                case "Copy":
                    BP.Win.WF.Global.ToolIndex = 0;
                    if (BP.Win32.PubClass.Question(this.ToE("AYS", "您确定吗？")) == false)
                        return;
                    Flow fl11 = this.HisWinFlow.HisFlow;
                    fl11.DoCopy();
                    break;

                case "导出流程模板":
                case "GenerFlowTemplate":
                    string msg = this.ToE("CheckAlertInfo", 
                        "系统将会花一些时间处理如下事情。\r\n\r\n1，为每一个流程执行体检。\r\n2，生成html流程报表\r\n3，生成系统可识别的流程模板。\r\n==================\r\n请耐心等待，直到鼠标形状变化为止。");

                    if (MessageBox.Show(msg,
                         "Note", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                    {
                        return;
                    }
                    this.Cursor = Cursors.WaitCursor;
                    Flows.GenerHtmlRpts();
                    this.Cursor = Cursors.Default;
                    //BP.WF.Global.DoGenerFlowTemplate(this.HisWinFlow.HisFlow);
                    break;
                default:
                    MessageBox.Show("未处理的命令。" + text);
                    break;
            }
        }
        private void contextMenuStrip1_DoubleClick(object sender, EventArgs e)
        {
            this.DoText("流程属性");
        }
	}
}
