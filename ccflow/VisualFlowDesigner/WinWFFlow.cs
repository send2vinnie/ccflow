using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using BP.WF;
using BP.En;
using BP.Port;

namespace BP.Win.WF
{
	/// <summary>
	/// WinWFFlow 的摘要说明。
	/// </summary>
	public class WinWFFlow : BP.Win32.Controls.WFContainer // System.Windows.Forms.Panel
	{
        public string ToE(string no, string chVar)
        {
            return BP.Sys.Language.GetValByUserLang(no,chVar);
        }

		private ContextMenu contextMenu_Flow;
		private ContextMenu contextMenu_Node;
        private ContextMenu contextMenu_LabNote;
		private ContextMenu contextMenu_Line;


		#region 构造函数
		public WinWFFlow()
		{
			this.InitMenu();
			this.NodeCount=0;
			this._winLines = new WinLines();
			this._winLines.Parent = this;
		}	
		#endregion 

		#region 属性
        private Flow _HisFlow = null;     //工作流    数据实体
        public Flow HisFlow
        {
            get
            {
                if (_HisFlow == null)
                    _HisFlow = new Flow();

                _HisFlow.RetrieveFromDBSources();
                return _HisFlow;
            }
            set
            {
                _HisFlow = value;
            }
        }

		public Nodes HisNodes = null;   //节点集合  数据实体
		public Directions HisDirections = null;//方向集合  数据实体
        public LabNotes HisLabNotes = null;   //节点集合  数据实体

		public override void Save()
		{
            this.HisFlow.RetrieveFromDBSources();

			string err = "";
            try
            {
                err = this.ToE("WhenSavePict", "保存工作流图片描述时出错"); // "保存工作流图片描述时出错";
                this.SaveFlowImage(this.Name);

                err = this.ToE("WhenSaveNode", "保存工作流节点时出错");  //"保存工作流节点时出错";
                this.SaveHisNodes();

                err = this.ToE("WhenSaveLabel", "保存流程注释时出错");  //"保存流程注释时出错";
                this.SaveHisLabNotes();

                err = this.ToE("WhenSaveDir", "保存工作流流向时出错"); // "保存工作流流向时出错";
                this.SaveHisDirections();

                err = this.ToE("WhenSaveComplete", "检查流程完整时错误。"); //"检查流程完整时错误。";

                string msg = Node.CheckFlow(HisFlow);

                if (msg != null)
                {
                    MessageBox.Show(msg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(err + "：\n" + ex.Message, " error ！");
            }

			this.BindData( this.HisFlow );
		}
		public void SaveHisFlow()
		{
			this.HisFlow.Save();
		}
		public void SaveHisNodes()
		{
			foreach(Control con in this.Controls)
			{
				WinWFNode node = con as WinWFNode;
				if(node!=null && node.HisNode!=null && node.HisNode.NodeID>0 )
				{
					node.HisNode.X = node.Location.X - this.DisplayRectangle.X;
					node.HisNode.Y = node.Location.Y - this.DisplayRectangle.Y;
                    node.HisNode.DirectUpdate();
				}
			}
		}
        public void SaveHisLabNotes()
        {
            foreach (Control con in this.Controls)
            {
                WinWFLab node = con as WinWFLab;
                if (node != null && node.HisNode != null && node.HisNode.MyPK.Length > 0)
                {
                    node.HisNode.X = node.Location.X - this.DisplayRectangle.X;
                    node.HisNode.Y = node.Location.Y - this.DisplayRectangle.Y;
                    node.HisNode.DirectUpdate();
                }
            }
        }
        public void SaveHisDirections()
        {
            //if(this.CurrentLine.f)
            foreach (WinWFLine winl in this.WinLines)
            {
                if (winl.HisDirection != null
                    && winl.HisDirection.Node > 0
                    && winl.HisDirection.Node > 0)
                {
                    winl.HisDirection.Save();
                }
            }
        }
		#endregion 

		#region 绑定工作流 
		
        public void BindData(Flow flow)
        {
            this.HisFlow = flow;
            Nodes nds = new Nodes();
            nds.Retrieve(NodeAttr.FK_Flow, flow.No);
            this.HisNodes = nds; // new Nodes(flow.No);

            this.HisLabNotes = new LabNotes(flow.No);
            this.InitFlow();
        }

		/// <summary>
		/// 初始化流程
		/// </summary>
		protected void InitFlow()
		{
			this.Controls.Clear();
			this.WinLines.Clear();
            if (this.HisNodes != null)
            {
                this.HisDirections = new Directions();

                for (int i = 0; i < this.HisNodes.Count; i++)
                {
                    Node nd = this.HisNodes[i] as Node;
                    if (nd.X <= 0 || nd.X >= 20000)
                    {
                        nd.X = i * 50;  // 设置分布位置。如果在开始期间没有给Node的节点给值。
                    }
                    if (nd.Y <= 0)
                    {
                        nd.X = 8;
                        nd.Y = i * 50 + 8; // 设置分布位置。如果在开始期间没有给Node的节点给值。
                    }
                    this.AddNode(nd);
                    Directions ens = new Directions(nd.NodeID);
                    this.HisDirections.AddEntities(ens);
                }
                if (this.HisDirections.Count > 0)
                {
                    foreach (Direction l in this.HisDirections)
                    {
                        WinWFNode begin = this.GetWinNode(l.Node.ToString());
                        WinWFNode end = this.GetWinNode(l.ToNode.ToString());

                        if (begin == null || end == null)
                        {
                            string msg = "工作流节点方向预置有误！[" + l.Node + "→" + l.ToNode + "]";
                            if (begin == null)
                                msg += "\n在该工作流中未找到节点[" + l.Node + "]";
                            if (end == null)
                                msg += "\n在该工作流中未找到节点[" + l.ToNode + "]";
                            msg += "\n如果要删除方向，请从数据库手动删除！";
                            MessageBox.Show(msg, "加载方向失败！");
                        }
                        else
                        {
                            WinWFLine line = new WinWFLine(begin, end);
                            line.HisDirection = l;
                            this.WinLines.AddLine(line);
                        }
                    }

                    //this.AfterAddline();
                }
            }


            /*处理 流程注释。
             */
            for (int i = 0; i < this.HisLabNotes.Count; i++)
            {
                LabNote nd = this.HisLabNotes[i] as LabNote;
                if (nd.X <= 0 || nd.X >= 20000)
                {
                    nd.X = i * 50;  // 设置分布位置。如果在开始期间没有给Node的节点给值。
                }
                if (nd.Y <= 0)
                {
                    nd.X = 8;
                    nd.Y = i * 50 + 8; // 设置分布位置。如果在开始期间没有给Node的节点给值。
                }
                this.AddLabNote(nd);
            }

		}


		#endregion


		#region InitMenu
		private void InitMenu()
		{
			this.contextMenu_Flow = new ContextMenu();
			this.contextMenu_Node = new ContextMenu();
            this.contextMenu_LabNote = new ContextMenu();

			this.contextMenu_Node.Popup += new EventHandler( this.contextMenu_Node_Popup);
            this.contextMenu_LabNote.Popup += new EventHandler(this.contextMenu_LabNote_Popup);

			this.contextMenu_Line = new ContextMenu();
			MenuItem mi ;
			int index = 0;

			#region this.contextMenu_Flow
			index = index++;
			mi = new MenuItem("工作流属性(&R)" , new EventHandler(this.miFlowAttr_Click));
			mi.Index = index++;
			mi.DefaultItem = true;
			this.contextMenu_Flow.MenuItems.Add(mi);

			mi = new MenuItem("-");
			mi.Index = index++;
			this.contextMenu_Flow.MenuItems.Add(mi);

			mi = new MenuItem("刷新(&R)");
			mi.Index = index++;
			this.contextMenu_Flow.MenuItems.Add(mi);
			#endregion

			#region contextMenu_Node Add
			index = 0;

            mi = new MenuItem(this.ToE("NodeProperty", "节点属性"), new EventHandler(this.miNodeP_Click)); //节点属性
            mi.Name = "NodeProperty";
			mi.Index = index++;
			this.contextMenu_Node.MenuItems.Add(mi);

            //mi = new MenuItem(this.ToE("NodeCopy", "节点复制"), new EventHandler(this.miNodeCopy_Click)); //节点属性
            //mi.DefaultItem = false;
            //mi.Name = "NodeCopy";
            //mi.Index = index++;
            //mi.Enabled = false;
            //this.contextMenu_Node.MenuItems.Add(mi);


            mi = new MenuItem(this.ToE("DesignSheet", "设计表单"), new EventHandler(this.miWorkP_Click)); //设计表单
            mi.Index = index++;
            mi.DefaultItem = true;
            mi.Name = "DesignSheet";
            this.contextMenu_Node.MenuItems.Add(mi);

            ////
            //mi = new MenuItem(this.ToE("Dept", "工作部门"), new EventHandler(this.DoNodeDept_Click));//工作岗位
            //mi.Index = index++; //5
            //mi.Name = "Dept";
            //this.contextMenu_Node.MenuItems.Add(mi);


            ////
            mi = new MenuItem(this.ToE("Station", "工作岗位"), new EventHandler(this.DoNodeStation_Click));//工作岗位
            mi.Index = index++; //5
            mi.Name = "Station";
            this.contextMenu_Node.MenuItems.Add(mi);


#warning 去了节点事件。

            //mi = new MenuItem(this.ToE("DesignAction"), new EventHandler(this.DoEventAction_Click)); //"设计事件",
            //mi.Index = index++;
            //mi.Name = "DesignAction";
            //this.contextMenu_Node.MenuItems.Add(mi);


#warning 去了节点完成条件。


            //mi = new MenuItem("-");
            //mi.Index = index++;
            //this.contextMenu_Node.MenuItems.Add(mi);

            //mi = new MenuItem( this.ToE("NodeCond"), new EventHandler(this.miNodeCondition_Click)); //节点完成条件
            //mi.Index = index++;  //2  改变时要更改 contextMenu_Node_Popup 
            //mi.Name = "NodeCond";

            //this.contextMenu_Node.MenuItems.Add(mi);

            mi = new MenuItem(this.ToE("FlowCond", "流程完成条件"), new EventHandler(this.miFlowCondition_Click)); //"流程完成条件(&F)"
			mi.Index = index++;  //3  改变时要更改 contextMenu_Node_Popup 
            mi.Name = "FlowCond";
			this.contextMenu_Node.MenuItems.Add(mi);

            //mi = new MenuItem("-");
            //mi.Index = index++;//4
            //this.contextMenu_Node.MenuItems.Add(mi);

			//
            //mi = new MenuItem("-");
            //mi.Index = index++;
            //this.contextMenu_Node.MenuItems.Add(mi);

            //mi = new MenuItem("置于顶层(&T)" , new EventHandler(this.miTopLev_Click));
            //mi.Index = index++;
            //this.contextMenu_Node.MenuItems.Add(mi);
			
            //mi = new MenuItem("置于底层(&B)" , new EventHandler(this.miBottomLev_Click));
            //mi.Index = index++;
            //this.contextMenu_Node.MenuItems.Add(mi);

			mi = new MenuItem("-");
			mi.Index = index++;
			this.contextMenu_Node.MenuItems.Add(mi);

            mi = new MenuItem(this.ToE("DeleteNode", "删除节点"), new EventHandler(this.DoNodeDel_Click)); //删除节点
			//mi.Enabled = false;
			mi.Index = index++;
            mi.Name = "DeleteNode";
			this.contextMenu_Node.MenuItems.Add(mi);

			mi = new MenuItem("-");
			mi.Index = index++;
			this.contextMenu_Node.MenuItems.Add(mi);

            mi = new MenuItem(this.ToE("FlowProperty", "流程属性"), new EventHandler(this.miFlowP_Click)); //flow protece
            mi.Index = index++;
            mi.Name = "FlowProperty";
            this.contextMenu_Node.MenuItems.Add(mi);

            //mi = new MenuItem(this.ToE("FlowCheck", "流程校验"), new EventHandler(this.miFlowWorkCheck_Click)); // "流程校验(&C)"
            //mi.Index = index++;
            //mi.Name = "FlowCheck";
            //this.contextMenu_Node.MenuItems.Add(mi);

            //mi = new MenuItem("工作统计分析(&F)" , new EventHandler(this.DoNodeDel_Click));
            ////mi.Enabled = false;
            //mi.Index = index++;
            //this.contextMenu_Node.MenuItems.Add(mi);
			#endregion


			#region 增加注释属性
            index = 0;
            mi = new MenuItem(this.ToE("Edit", "修改"), new EventHandler(this.LabNote_Click));
            mi.Name = "Edit";
            mi.Index = index++;
            this.contextMenu_LabNote.MenuItems.Add(mi);

            mi = new MenuItem( this.ToE("Delete","删除"), new EventHandler(this.LabNote_Click));
            mi.Index = index++;
            mi.Name = "Delete";

            this.contextMenu_LabNote.MenuItems.Add(mi);
			#endregion

			#region contextMenu_Line Add
			index = 0;
            //mi = new MenuItem("属性(&R)" , new EventHandler(this.miLineP_Click));
            //mi.Index = index++;
            //this.contextMenu_Line.MenuItems.Add(mi);

            //mi = new MenuItem("-");
            //mi.Index = index++;
            //this.contextMenu_Line.MenuItems.Add(mi);

            mi = new MenuItem(this.ToE("DirCond", "方向条件"), new EventHandler(this.miLineDirCondition_Click)); // 方向条件
			mi.DefaultItem = true;
			mi.Index = index++;
            mi.Name = "DirCond";
			this.contextMenu_Line.MenuItems.Add(mi);

			mi = new MenuItem("-");
			mi.Index = index++;
			this.contextMenu_Line.MenuItems.Add(mi);

			mi = new MenuItem(this.ToE("Delete","删除"), new EventHandler(this.miLineDel_Click));
			mi.Index = index++;
            mi.Name = "Delete";
			this.contextMenu_Line.MenuItems.Add(mi);
			#endregion
		}
		#endregion
		

		#region Line

		private WinWFLine _currentLine = null;
		public WinWFLine CurrentLine
		{
			get
			{
				return this._currentLine;
			}
			set
			{
				this._currentLine = value;
			}
		}
		private WinLines _winLines;
		public WinLines WinLines
		{
			get
			{
				return this._winLines;
			}
		}
		/// <summary>
		/// 点loc是否在某条线上
		/// </summary>
		protected void FindLine( Point loc)
		{
			if( this._currentLine!=null)
			{
				this._currentLine.LostFocus();
				this._currentLine = null;
			}

			for(int i =0 ; i<this.WinLines.Count ;i++)
			{
				WinWFNode n1 = this.WinLines[i].NodeBegin;
				WinWFNode n2 = this.WinLines[i].NodeEnd;
				if(n1==null || n2==null)
					continue;
					
				Point p1 = new Point(n1.Location.X+n1.Width/2,n1.Location.Y+n1.Height/2);
				Point p2 = new Point(n2.Location.X+n2.Width/2,n2.Location.Y+n2.Height/2);

				if( this.lineContains(p1 , p2 ,loc , 5))
				{
					this._currentLine = this.WinLines[i];
					this._currentLine.Focus();
					return ;
				}
			}
		}
		/// <summary>
		/// 绘线
		/// </summary>
		protected override void DrawLines(PaintEventArgs e)
		{
			foreach(WinWFLine line in this.WinLines)
			{
                BP.Win.Controls.IPaint p = line as BP.Win.Controls.IPaint;
				p.Paint(e) ;
			}
		}
		/// <summary>
		/// 删除连接node的所有线
		/// </summary>
		public void DeleteLines(WinWFNode node)
		{
			this.WinLines.DeleteLinesByWinNode( node );
		}
		public void AddLine(WinWFLine line)
		{
			this.WinLines.AddLine( line);
		}
		public void AfterAddline()
		{
			this.ResetTools();
			if( this._currentLine!=null)
			{
				this._currentLine.LostFocus();
				this._currentLine = null;
			}
			this.Refresh();
		}
		#endregion Line

		#region 添加方法
		/// <summary>
		/// 重置工具箱
		/// </summary>
		public void ResetTools()
		{
            Global.ToolIndex = 0;
			this.Cursor = this.FindForm().Cursor;
		}
		#endregion


		#region 重写方法
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown( e );
			locMouse_OffSet.X = e.X;
			locMouse_OffSet.Y = e.Y;
			
			this.FindLine( locMouse_OffSet );
            if (e.Button == MouseButtons.Left)
            {
                switch (Global.ToolIndex)
                {
                    case 1: //增加普通节点。
                        this.Cursor = Cursors.WaitCursor;
                        this.AddNode(locMouse_OffSet);
                        this.Cursor = Cursors.Default;
                        break;
                    //case 2: //增加审核节点。
                    //    if (this._currentLine != null)
                    //        this._currentLine.Focus(); //line 处于选择状态
                    //    //this.AddNode(locMouse_OffSet);
                    //    break;
                    case 3: //增加备注。
                        this.Cursor = Cursors.WaitCursor;
                        this.AddLabNote(locMouse_OffSet);
                        this.Cursor = Cursors.Default;
                        break;
                    default:
                        if (this._currentLine != null)
                            this._currentLine.Focus(); //line 处于选择状态
                        break;
                }
                this.ResetTools();
            }
            else if (e.Button == MouseButtons.Right)
            {	//线段右键菜单
                if (this._currentLine != null)
                {
                    this.contextMenu_Line.Show(this, this.locMouse_OffSet);
                }
                else
                {
                    int a = 70;
                    if ((this.locMouse_OffSet.X < a && this.locMouse_OffSet.Y < a)
                        || (this.Width - this.locMouse_OffSet.X < a && this.locMouse_OffSet.Y < a)
                        || (this.locMouse_OffSet.X < a && this.Height - this.locMouse_OffSet.Y < a)
                        || (this.Width - this.locMouse_OffSet.X < a && this.Height - this.locMouse_OffSet.Y < a))
                    {
                        this.contextMenu_Flow.Show(this, this.locMouse_OffSet);
                    }
                }
            }
		}
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
            this.Cursor = Global.CurrentToolCursor;
		}
		protected override void OnDoubleClick(EventArgs e)
		{
			base.OnDoubleClick (e);
			this.FindLine( this.locMouse_OffSet );
			if( this._currentLine != null)
			{
				this.miLineDirCondition_Click(this._currentLine , null);
			}
			else
			{
				int a = 70;
				if((this.locMouse_OffSet.X<a && this.locMouse_OffSet.Y<a)
					||(this.Width-this.locMouse_OffSet.X<a && this.locMouse_OffSet.Y<a)
					||(this.locMouse_OffSet.X<a && this.Height-this.locMouse_OffSet.Y<a)
					||(this.Width-this.locMouse_OffSet.X<a && this.Height-this.locMouse_OffSet.Y<a) )
				{
					this.miFlowAttr_Click(this ,null);
				}
			}
		}

		

		
		protected override void Dispose( bool disposing )
		{
			this.contextMenu_Flow.Dispose();
			contextMenu_Node.Dispose();
			contextMenu_Line.Dispose();

			base.Dispose( disposing );
		}
		
		#endregion 重写方法

		#region 节点管理
        public WinWFLab GetWinLab(string name)
        {
            foreach (Control con in this.Controls)
            {
                WinWFLab node = con as WinWFLab;
                if (node != null && node.Name == name)
                {
                    return node;
                }
            }
            return null;
        }

		public WinWFNode GetWinNode(string name)
		{
			foreach(Control con in this.Controls)
			{
				WinWFNode node = con as WinWFNode;
				if(node!=null && node.Name == name)
				{
					return node;
				}
			}
			return null;
		}
		public WinWFNode GetWinNode()
		 {
			 foreach(Control con in this.Controls)
			 {
				 WinWFNode node = con as WinWFNode;
				 if(node!=null && node.Focused)
				 {
					 return node;
				 }
			 }
			 return null;
		 }
		public BP.WF.Node GetWFNode()
		{
			foreach(Control con in this.Controls)
			{
				WinWFNode node = con as WinWFNode;
				if(node!=null && node.Focused )
				{
					return node.HisNode;
				}
			}
			return null;
		}

		public bool FindNode(string name)
		{
			foreach(Control con in this.Controls)
			{
				WinWFNode node = con as WinWFNode;
				if(node!=null && node.Name == name)
				{
					node.Focus();
					return true;
				}
			}
			return false;
		}
		protected override void SetNodeLocation(string name , Point loc)
		{
            if (this._currentLine != null)
            {
                this._currentLine.LostFocus();
                this._currentLine = null;
            }


			WinWFNode node = this.GetWinNode(name) ;
			if(node != null)
			{
				node.Location = loc;
				this.Refresh();
			}

            WinWFLab lab = this.GetWinLab(name);
            if (lab != null)
            {
                lab.Location = loc;
                this.Refresh();
            }

			this.dragBoxFromMouseDown = Rectangle.Empty;
		}
		public WinWFNode ActivateNode
		{
			get
			{
				foreach(Control con in this.Controls)
				{
					WinWFNode node = con as WinWFNode;
					if(node!=null && node.Focused)
					{
						return node;
					}
				}
				return null;
			}
		}
        private void AddLabNote(Point loc)
        {
            BP.Win.WF.WinWFLab node = new BP.Win.WF.WinWFLab();
            node.Name = "labnote" + this.LabNoteCount;
            node.Text = "说明" + this.LabNoteCount;
            node.Location = loc;

            LabNote nd = new LabNote();
            nd.FK_Flow = this.HisFlow.No;
            nd.Name = this.ToE("Label", "标签"); //  "标签";
            nd.X = loc.X;
            nd.Y = loc.Y;
            nd.Save();

            node.HisNode = nd;

            this.Controls.Add(node);

            this.LabNoteCount++;
            this.AfterAddLabNote(node);
            node.BindWFNode();
        }
        private void AddNode(Point loc)
        {
            BP.Win.WF.WinWFNode node = new BP.Win.WF.WinWFNode();
            node.Name = "node" + this.NodeCount;
            node.Text = this.ToE("Node", "节点") + this.NodeCount;
            node.Location = loc;
            Node nd = this.HisFlow.DoNewNode(loc.X, loc.Y);

            node.HisNode = nd;
            this.Controls.Add(node);
            this.NodeCount++;
            this.AfterAddNode(node);
            node.BindWFNode();
        }
		/// <summary>
		/// 增加一个节点
		/// </summary>
		/// <param name="wfn"></param>
        private void AddNode(BP.WF.Node wfn)
		{
			if(!this.FindNode(wfn.NodeID.ToString()))
			{
				BP.Win.WF.WinWFNode node = new BP.Win.WF.WinWFNode( wfn );
		
				this.Controls.Add( node );
				this.NodeCount++;

				this.AfterAddNode(node);
			}
		}
        private void AddLabNote(LabNote wfn)
        {
            if (!this.FindNode(wfn.MyPK.ToString()))
            {
                BP.Win.WF.WinWFLab node = new BP.Win.WF.WinWFLab(wfn);

                this.Controls.Add(node);
                this.LabNoteCount++;
                this.AfterAddLabNote(node);
            }
        }
		/// <summary>
		/// 在增加节点后,处理的工作.把事件与属性.
		/// 都给这个节点.
		/// </summary>
		/// <param name="sender"></param>
        private void AfterAddNode(WinWFNode sender)
        {
            WinWFNode node = sender as WinWFNode;
            node.ContextMenu = this.contextMenu_Node;

            node.MouseMove += new MouseEventHandler(this.WFNode_MouseMove);
            node.MouseDown += new MouseEventHandler(this.WFNode_MouseDown);
            node.MouseUp += new MouseEventHandler(this.WFNode_MouseUp);

            // 拖动事件.
            node.GiveFeedback += new GiveFeedbackEventHandler(this.WFNode_GiveFeedback);

            // 双击事件.
            node.DoubleClick += new EventHandler(this.miNodeP_Click);
        }
        private void AfterAddLabNote(WinWFLab sender)
        {
            WinWFLab node = sender as WinWFLab;
            node.ContextMenu = this.contextMenu_LabNote;

            node.MouseMove += new MouseEventHandler(this.LabNote_MouseMove);
            node.MouseDown += new MouseEventHandler(this.LabNote_MouseDown);
            node.MouseUp += new MouseEventHandler(this.LabNote_MouseUp);

            // 拖动事件.
            node.GiveFeedback += new GiveFeedbackEventHandler(this.LabNote_GiveFeedback);

            // 双击事件.
            node.DoubleClick += new EventHandler(this.LabNote_Click);
        }
        private void LabNote_Click(object sender, System.EventArgs e)
        {
            //WinWFLab lab = sender as WinWFLab;
            //if (lab != null)
            //{
            //    VisualFlowDesigner.FrmNodeLabel frm = new VisualFlowDesigner.FrmNodeLabel();
            //    frm.Tag = lab.HisNode;
            //    frm.ShowDialog();
            //    lab.HisNode = frm.Tag as LabNote;
            //    return;
            //}

            MenuItem mi = sender as MenuItem;
            ContextMenu menu = mi.Parent as ContextMenu;
            WinWFLab nd = menu.SourceControl as WinWFLab;

        //    MessageBox.Show( nd.Name );

            switch (mi.Name)
            {
                case "删除":
                case "Delete":
                case "Del":
                case "删除标签":
                    nd.HisNode.Delete();
                    nd.Visible = false;
                    break;
                default:
                    VisualFlowDesigner.FrmNodeLabel frm = new VisualFlowDesigner.FrmNodeLabel();
                    frm.LabName = nd.HisNode.Name;
                    frm.ShowDialog();
                    nd.HisNode.Name = frm.LabName;
                    nd.HisNode.Update("Name", frm.LabName);
                    this.SaveHisLabNotes();
                    //  nd.HisNode.RetrieveFromDBSources();
                    nd.BindWFNode();
                    break;
            }
        }
	 
		private int NodeCount=0;
        private int LabNoteCount = 0;

		public void ShowWFNodeAttr()
		{
			WinWFNode node = this.GetWinNode();
		}
		private void ShowWFNodeAttr( WinWFNode node )
		{
			if( node != null )
			{
				FrmAttr fattr = new FrmAttr();
				if(node.HisNode==null)
				{
					node.HisNode = new Node();
					node.HisNode.NodeID = 0;
                    node.HisNode.Name = this.ToE("NewNode", "新建节点");// "新建节点";
					node.HisNode.FK_Flow = this.HisFlow.No;
					node.BindWFNode();
				}
				else if(node.HisNode.NodeID > 0)
					node.HisNode.Retrieve();//040310 //刷新

				node.HisNode.X = node.Location.X - this.DisplayRectangle.X;
				node.HisNode.Y = node.Location.Y - this.DisplayRectangle.Y;
				bool edit = node.HisNode.NodeID>0?false:true;
                if (fattr.ShowAttr(this.ToE("NodeProperty", "节点属性") + " [" + node.HisNode.Name + "]", node.HisNode, edit) == DialogResult.OK)
					node.BindWFNode();
			}
		}
		#endregion 节点管理


        #region LabNote 拖放事件
        private void LabNote_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            locMouse_OffSet.X = e.X;
            locMouse_OffSet.Y = e.Y;
            Size dragSize = SystemInformation.DragSize;
            dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);

        }
        private void LabNote_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.dragBoxFromMouseDown = Rectangle.Empty;
        }
        private void LabNote_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            WinWFLab con = sender as WinWFLab;
            if (!con.Focused)//未获得焦点
            {
            }
            else//已获得焦点
            {
                if (dragBoxFromMouseDown != Rectangle.Empty && !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    DragDropEffects dropEffect = DragDropEffects.None;
                    if (Global.ToolIndex == 0)
                        dropEffect = con.DoDragDrop(con.Name, DragDropEffects.Move);//
                    else if (Global.ToolIndex == 2)
                        dropEffect = con.DoDragDrop(con.Name, DragDropEffects.Link);//
                }
            }
        }
        /// <summary>
        /// 拖放事件.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabNote_GiveFeedback(object sender, System.Windows.Forms.GiveFeedbackEventArgs e)
        {
            WinWFLab con = sender as WinWFLab;
            e.UseDefaultCursors = false;
            con.Cursor = Cursors.SizeAll; // 设置光标的类型.
        }
        #endregion 节点备注 拖放事件



		#region 节点事件

		#region 拖放事件
		private void WFNode_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			locMouse_OffSet.X = e.X;
			locMouse_OffSet.Y = e.Y;
			Size dragSize = SystemInformation.DragSize;
			dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width /2),e.Y - (dragSize.Height /2)), dragSize);
		}
		private void WFNode_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			this.dragBoxFromMouseDown = Rectangle.Empty;
		}
		private void WFNode_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			WinWFNode con = sender as WinWFNode;
			if(!con.Focused)//未获得焦点
			{
			}
			else//已获得焦点
			{
				if (dragBoxFromMouseDown != Rectangle.Empty && !dragBoxFromMouseDown.Contains(e.X, e.Y)) 
				{
					DragDropEffects dropEffect = DragDropEffects.None ;
                    if (Global.ToolIndex == 0)
						dropEffect = con.DoDragDrop(con.Name, DragDropEffects.Move );//
                    else if (Global.ToolIndex == 2)
						dropEffect = con.DoDragDrop(con.Name, DragDropEffects.Link );//

					if (dropEffect == DragDropEffects.Move) 
					{
					}
					else if (dropEffect == DragDropEffects.Link) 
					{
					}
				}
			}
		}
		/// <summary>
		/// 拖放事件.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WFNode_GiveFeedback(object sender, System.Windows.Forms.GiveFeedbackEventArgs e) 
		{
			WinWFNode con = sender as WinWFNode;
			e.UseDefaultCursors = false;
			con.Cursor = Cursors.SizeAll; // 设置光标的类型.
		}
		#endregion 拖放事件
	
		#region 右键菜单事件
        private void contextMenu_Node_Popup(object sender, System.EventArgs e)
        {
            ContextMenu menu = sender as ContextMenu;
            WinWFNode node = menu.SourceControl as WinWFNode;
            if (node != null && node.HisNode != null)
            {
                bool flag = false; // node.HisNode.IsCheckNode;
                this.contextMenu_Node.MenuItems[1].Enabled = !flag;


                //	this.contextMenu_Node.MenuItems[2].Enabled = !flag;
                //this.contextMenu_Node.MenuItems[5].Enabled = true;
                //this.contextMenu_Node.MenuItems[6].Enabled = true;
                //this.contextMenu_Node.MenuItems[7].Enabled = true;
            }
        }
        private void contextMenu_LabNote_Popup(object sender, System.EventArgs e)
        {
            ContextMenu menu = sender as ContextMenu;
            WinWFLab node = menu.SourceControl as WinWFLab;

            //if (node != null && node.HisNode != null)
            //{
            //    bool flag = node.HisNode.IsCheckNode;
            //    this.contextMenu_Node.MenuItems[1].Enabled = !flag;
            //    //	this.contextMenu_Node.MenuItems[2].Enabled = !flag;
            //    this.contextMenu_Node.MenuItems[5].Enabled = true;
            //    this.contextMenu_Node.MenuItems[6].Enabled = true;
            //    this.contextMenu_Node.MenuItems[7].Enabled = true;
            //}
        }

        private void miTopLev_Click(object sender, System.EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            ContextMenu menu = mi.Parent as ContextMenu;
            menu.SourceControl.BringToFront();
        }

		private void miBottomLev_Click(object sender, System.EventArgs e)
		{
			MenuItem mi = sender as MenuItem ;
			ContextMenu menu = mi.Parent as ContextMenu;
			menu.SourceControl.SendToBack();
		}
        private void DoNodeDel_Click(object sender, System.EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            ContextMenu menu = mi.Parent as ContextMenu;
            WinWFNode node = menu.SourceControl as WinWFNode;
            if (node == null)
                return;

            if (node.HisNode.IsStartNode == true)
            {
                MessageBox.Show(this.ToE("NoDelStartNode", "您不能删除开始节点"),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (BP.Win.Controls.MSG.ShowQuestion(this.ToE("AYS", "您确定吗？"), "提示") == DialogResult.No)
                return;
            try
            {
                node.HisNode.Delete();
            }
            catch (Exception ex)
            {
                if (BP.Win.Controls.MSG.ShowQuestion("节点" + node.HisNode.Name + "[" + node.HisNode.NodeID + "]删除失败！：" + ex.Message + "\n是否强行从数据库中删除该节点？", "删除失败！") == DialogResult.Yes)
                {
                    string sql = "delete from wf_node where NodeId =" + node.HisNode.NodeID;
                    DA.DBAccess.RunSQL(sql);
                    this.DeleteLines(node);
                    node.Dispose();
                    this.Refresh();
                }
                else
                {
                    return;
                }
            }
            this.DeleteLines(node);
            node.Dispose();
            this.Refresh();
        }
        private void miFlowWorkCheck_Click(object sender, System.EventArgs e)
        {
            if (this.HisFlow == null)
                return;



            string msg = this.HisFlow.DoCheck();
            MessageBox.Show(msg);
        }
        private void miFlowP_Click(object sender, System.EventArgs e)
        {
            if (this.HisFlow == null)
                return;

            WinWFNode node = sender as WinWFNode;
            if (node == null)
            {
                MenuItem mi = sender as MenuItem;
                ContextMenu menu = mi.Parent as ContextMenu;
                node = menu.SourceControl as WinWFNode;
            }
            if (node != null)
            {
                FrmAttr fattr = new FrmAttr();
                if (node.HisNode == null)
                {
                    node.HisNode = new Node();
                    node.HisNode.NodeID = 0;
                    node.HisNode.Name = "新建节点";
                    node.HisNode.FK_Flow = this.HisFlow.No;
                    node.BindWFNode();
                }
                else if (node.HisNode.NodeID > 0)
                    node.HisNode.Retrieve();//040310 //刷新

                node.HisNode.X = node.Location.X - this.DisplayRectangle.X;
                node.HisNode.Y = node.Location.Y - this.DisplayRectangle.Y;
                bool edit = false; // node.HisNode.OID>0?false:true;
                if (fattr.ShowAttr(this.ToE("FlowProperty", "流程属性") + " [" + node.HisNode.HisFlow.Name + "]", node.HisNode.HisFlow, edit) == DialogResult.OK)
                    node.BindWFNode();
            }
        }
        private void DoEventAction_Click(object sender, System.EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            ContextMenu menu = mi.Parent as ContextMenu;
            WinWFNode nd = menu.SourceControl as WinWFNode;
            BP.WF.Frm.FrmAction fa = new BP.WF.Frm.FrmAction();
            fa.ShowNode(nd.HisNode);
        }
        private void DoNodeStation_Click(object sender, System.EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            ContextMenu menu = mi.Parent as ContextMenu;
            WinWFNode node = menu.SourceControl as WinWFNode;

            //if (node != null && node.HisNode != null && node.HisNode.NodeID > 0)
            //    return;

            node.HisNode.Retrieve();
            BP.WF.Global.DoUrl("/DoPort.aspx?DoType=StaDef&PK=" + node.HisNode.NodeID);
            return;
        }

		/// <summary>
        /// 设计表单
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void miWorkP_Click(object sender, System.EventArgs e)
        {
            if (this.HisFlow == null)
                return;

            WinWFNode node = sender as WinWFNode;
            if (node == null)
            {
                MenuItem mi = sender as MenuItem;
                ContextMenu menu = mi.Parent as ContextMenu;
                node = menu.SourceControl as WinWFNode;
            }

            FrmAttr fattr = new FrmAttr();
            node.HisNode.Retrieve();

            node.HisNode.X = node.Location.X - this.DisplayRectangle.X;
            node.HisNode.Y = node.Location.Y - this.DisplayRectangle.Y;

            BP.WF.Global.DoUrl("/DoPort.aspx?DoType=MapDef&PK=ND" + node.HisNode.NodeID);
            return;
        }
        private void miNodeCopy_Click(object sender, System.EventArgs e)
        {
            if (this.HisFlow == null)
                return;

            WinWFNode node = sender as WinWFNode;
            if (node == null)
            {
                MenuItem mi = sender as MenuItem;
                ContextMenu menu = mi.Parent as ContextMenu;
                node = menu.SourceControl as WinWFNode;
            }

            if (node == null)
            {
                MessageBox.Show("没有确定要复制的节点。");
                return;
            }

            Node nd = node.HisNode;
            Global.ToolIndex = 1;
            Global._copyNode = nd;

        }
        private void miNodeP_Click(object sender, System.EventArgs e)
        {
            if (this.HisFlow == null)
                return;

            WinWFNode node = sender as WinWFNode;
            if (node == null)
            {
                MenuItem mi = sender as MenuItem;
                ContextMenu menu = mi.Parent as ContextMenu;
                node = menu.SourceControl as WinWFNode;
            }


            if (node != null)
            {
                node.HisNode.RetrieveFromDBSources();

                FrmAttr fattr = new FrmAttr();
                if (node.HisNode == null)
                {
                    node.HisNode = new Node();
                    node.HisNode.NodeID = 0;
                    node.HisNode.Name = "新建节点";
                    node.HisNode.FK_Flow = this.HisFlow.No;
                    node.BindWFNode();
                }
                else if (node.HisNode.NodeID > 0)
                {
                    node.HisNode.Retrieve();//040310 //刷新
                }

                node.HisNode.X = node.Location.X - this.DisplayRectangle.X;
                node.HisNode.Y = node.Location.Y - this.DisplayRectangle.Y;

                bool edit = node.HisNode.NodeID > 0 ? false : true;

                node.HisNode.FlowName = this.HisFlow.Name;
                node.HisNode.FK_Flow = this.HisFlow.No;
                node.HisNode.Update();

                BP.WF.Global.DoEdit(node.HisNode);
                node.HisNode.RetrieveFromDBSources();
                node.BindWFNode();
                // if (fattr.ShowAttr("节点属性 [" + node.HisNode.Name + "]", node.HisNode, edit) == DialogResult.OK)
                //   node.BindWFNode();
            }
        }
		private void miNodeCondition_Click(object sender, System.EventArgs e)
		{
			WinWFNode node = sender as WinWFNode; 
			if( node == null )
			{
				MenuItem mi = sender as MenuItem ;
				ContextMenu menu = mi.Parent as ContextMenu;
				node = menu.SourceControl as WinWFNode; 
			}
            if (node != null && node.HisNode != null && node.HisNode.NodeID > 0)
            {
                node.HisNode.Retrieve();  //040310 //刷新
                Node nd = node.HisNode;
                BP.WF.Global.DoUrl("./DoPort.aspx?DoType=Dir&CondType=" + (int)CondType.Node + "&FK_Flow=" + node.HisNode.FK_Flow + "&FK_MainNode=" + nd.NodeID + "&FK_Node=" + nd.NodeID + "&FK_Attr=" );
                return;

                //FrmCondition fcon = new FrmCondition();
                //fcon.ShowFrm(node.HisNode.NodeID, "节点完成条件 －[" + node.HisNode.Name + "]", nd);
            }
		}
        private void miFlowCondition_Click(object sender, System.EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            ContextMenu menu = mi.Parent as ContextMenu;
            WinWFNode node = menu.SourceControl as WinWFNode;
            if (this.HisFlow != null && node != null && node.HisNode != null && node.HisNode.NodeID > 0)
            {
                node.HisNode.Retrieve();
                Node wfnd = node.HisNode;
                BP.WF.Global.DoUrl("./DoPort.aspx?DoType=Dir&FK_Flow=" + wfnd.FK_Flow + "&FK_MainNode=" + wfnd.NodeID + "&FK_Node="+node.HisNode.NodeID+"&FK_Attr=&CondType=" + (int)CondType.Flow);
                return;
                //fcon.ShowFrm(node.HisNode.NodeID, "流程完成条件 －[" + this.HisFlow.Name + "]", conn);
            }
        }
         
        private void DoNodeEmp_Click(object sender, System.EventArgs e)
        {
           
            //MenuItem mi = sender as MenuItem;
            //ContextMenu menu = mi.Parent as ContextMenu;
            //WinWFNode node = menu.SourceControl as WinWFNode;

            ////if (node != null && node.HisNode != null && node.HisNode.NodeID > 0)
            ////    return;

            //node.HisNode.Retrieve();
            //FrmOneToMore fotm = new FrmOneToMore();
            //fotm.lab1.Text = this.ToE("Node", "节点");
            //fotm.lab2.Text = this.ToE("Emp", "操作员");

            //fotm.SetBindKey(this.ToE("NodeEmp", "节点人员")
            //    , NodeAttr.NodeID, false
            //    , "No", false
            //    , NodeStationAttr.FK_Node, NodeEmpAttr.FK_Emp);

            //Nodes ns = new Nodes(this.HisFlow.No);

            //Emps ens2 = new Emps();
            //ens2.RetrieveAll();
            //fotm.FillEnsAndEns(ns, NodeAttr.Name, false, node.HisNode
            //    , ens2, "Name");

            //NodeEmps nds = new NodeEmps(node.HisNode.NodeID);
            //fotm.BindCheckEns(nds);

            ////fotm.tree2.DoubleClick += new EventHandler(this.tree2_DoubleClick);
            ////fotm.btnNext.Text = this.ToE("EmpStation");  // "岗位工作人员";
            ////fotm.btnNext.Show();
            ////fotm.btnNext.Click += new EventHandler(this.tree2_DoubleClick);
            //fotm.DeteleErrorData();
            //fotm.ShowDialog();
        }
		private  Nodes GetNodesIsNotStart()
		{
			Nodes ns = new Nodes( this.HisFlow.No );
			int p = 0;
			while(p<ns.Count)
			{
				Node n = ns[p] as Node;
				if( n.IsStartNode )
					ns.RemoveEn( n );
				else
					p++;
			}
			return ns;
		}
		#endregion 右键菜单事件

		#endregion 节点事件

		#region line 右键事件
		private void miLineDel_Click(object sender, System.EventArgs e)
		{
			if(this._currentLine != null)
			{
				this.WinLines.RemoveLine( this._currentLine);
				this._currentLine = null;
				this.Refresh();
			}
		}
		private void miLineP_Click(object sender, System.EventArgs e)
		{
			WinWFLine line = this._currentLine;//sender as WinWFLine;
            if (line != null)
            {
                if (line.HisDirection == null)
                {
                    line.HisDirection = new Direction();
                    line.HisDirection.Node = line.NodeBegin.HisNode.NodeID;
                    line.HisDirection.ToNode = line.NodeEnd.HisNode.NodeID;
                }

                FrmAttr fotm = new FrmAttr();
                if (fotm.ShowAttr("方向属性 [" + line.NodeBegin.Text + "→" + line.NodeEnd.Text + "]", line.HisDirection, false) == DialogResult.OK)
                    this.Refresh();
            }
		}
        private void miLineDirCondition_Click(object sender, System.EventArgs e)
        {
            WinWFLine line = this._currentLine;
            if (line != null && line.HisDirection != null)
            {
                BP.WF.Global.DoUrl("./DoPort.aspx?DoType=Dir&CondType=" + (int)CondType.Dir + "&FK_Flow=" + this.HisFlow.No + "&FK_MainNode=" + line.HisDirection.Node + "&FK_Node=" + line.HisDirection.Node + "&FK_Attr=&ToNodeID=" + line.HisDirection.ToNode);
                return;
            }
        }
		#endregion 

		#region this.ContextMenu 事件处理

		private void miFlowAttr_Click(object sender, System.EventArgs e)
		{
			if( this._currentLine != null)
			{
				this.miLineP_Click(this._currentLine , null);
			}
			else
				this.ShowFlowAttr();
		}
        public void ShowFlowAttr()
        {
            if (this.HisFlow != null)
            {
                if (this.HisFlow.No != "0000")
                    this.HisFlow.Retrieve();//040310 //刷新

                FrmAttr fattr = new FrmAttr();
                if (fattr.ShowAttr(this.ToE("FlowProperty", "流程属性") + " [" + this.HisFlow.Name + "]", this.HisFlow, false) == DialogResult.OK)
                    this.Refresh();
            }
        }
		#endregion

		#region 生成工作流图片
		/// <summary>
		/// 生成工作流图片
		/// </summary>
		public void SaveFlowImage(string flowName )
		{
            this.SaveFlowImage(Global.FlowImagePath + this.HisFlow.No + ".gif", ImageFormat.Gif, flowName);
		}
		#endregion 生成工作流图片

	}
}
