using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using BP.WF;
using BP.Win.Controls;

namespace BP.Win.WF
{
	/// <summary>
	/// WinWFNode 的摘要说明。
	/// </summary>
	public class WinWFNode :  WFNodeBase  //System.Windows.Forms.UserControl ,IPaint
	{
		public WinWFNode() 
		{

		}

        public WinWFNode(Node wfn)
        {
            this.HisNode = wfn;
            this.BackColor = Color.White;
            this.BindWFNode();
        }

		#region 属性
		public  WinWFFlow ParentWinFlow
		{
			get
			{
				return this.Parent as WinWFFlow;
			}
		}
		private NodePosType _positionType = NodePosType.Start;
		public  NodePosType PositionType
		{
			get
			{
				return _positionType;
			}
		}
        private NodeWorkType _workType = NodeWorkType.WorkHL;
		public  NodeWorkType  WorkType
		{
			get
			{
				return _workType;
			}
		}
		private Node _HisNode= null;
        public Node HisNode
        {
            get
            {
                return this._HisNode;
            }
            set
            {
                this._HisNode = value;
            }
        }
        public new string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                if (this.HisNode != null)
                    this.HisNode.Name = value;
                //throw new Exception(Glo.NodeImagePath + this.WorkType.ToString());
                UpdateSize(Glo.NodeImagePath + this.WorkType.ToString() + ".bmp");
            }
        }
		#endregion 属性

		
		#region 鼠标事件
        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            if (Global.ToolIndex == 2)
            {
                WinWFFlow fcon = this.Parent as WinWFFlow;
                if (fcon.CurrentLine == null)
                {
                    return;
                    fcon.CurrentLine = new WinWFLine();
                    fcon.CurrentLine.NodeBegin = this;
                    fcon.CurrentLine.Name = this.Name;
                }
                else
                {
                    this.OnMouseDown(null);
                }
            }
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
        }
		protected override void OnMouseDown(MouseEventArgs e )
		{
            if (e!=null)
			   base.OnMouseDown( e );

           //if ( e == null && e.Button != MouseButtons.XButton1)
           //    return;

            if (Global.ToolIndex == 2)
			{
				WinWFFlow fcon = this.Parent as WinWFFlow;
				if( fcon.CurrentLine == null)
				{
					fcon.CurrentLine = new WinWFLine();
					fcon.CurrentLine.NodeBegin = this;
					fcon.CurrentLine.Name = this.Name;
                  // this.Cursor.
				}
				else if(fcon.CurrentLine.NodeEnd ==null)
				{
					if(fcon.CurrentLine.Name == this.Name)
					{
						//fcon.CurrentLine = null;
						return;
					}

                    /* 判断是否有这条线，没有在画它。*/
                    foreach (WinWFLine ct in fcon.WinLines)
                    {
                        if (ct.HisDirection.ToNode == this.HisNode.NodeID
                            && ct.HisDirection.Node ==fcon.CurrentLine.NodeBegin.HisNode.NodeID  )
                        {
                            // MessageBox.Show("sd");
                            return;
                        }
                    }


					fcon.CurrentLine.NodeEnd = this;
                    if (fcon.CurrentLine.NodeEnd.HisNode.IsStartNode)
                    {
                        MessageBox.Show("您不能向开始工作节点方向连接线。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        fcon.CurrentLine.NodeEnd = null;
                        fcon.CurrentLine = null;
                        return;
                    }

					fcon.CurrentLine.Name +="_"+this.Name;
					fcon.CurrentLine.HisDirection =new Direction();
					fcon.CurrentLine.Text ="";
					if(fcon.CurrentLine.NodeBegin.HisNode!=null)
					{
						fcon.CurrentLine.HisDirection.Node = fcon.CurrentLine.NodeBegin.HisNode.NodeID;
						fcon.CurrentLine.Text = fcon.CurrentLine.NodeBegin.Text ;
					}
					fcon.CurrentLine.Text += "→"; 
					if(fcon.CurrentLine.NodeEnd.HisNode!=null)
					{
						fcon.CurrentLine.HisDirection.ToNode = fcon.CurrentLine.NodeEnd.HisNode.NodeID;
						fcon.CurrentLine.Text += fcon.CurrentLine.NodeEnd.Text;
					}


					fcon.AddLine( fcon.CurrentLine );
					fcon.AfterAddline();
				}
				else
					fcon.CurrentLine = null;
			}
		}
		#endregion


		#region 绑定 方法
		public void BindWFNode()
		{
			if(_HisNode!=null && _HisNode.NodeID >0)
			{
				this.Name= _HisNode.NodeID.ToString();

				if(!File.Exists(this.MouseLeaveImageUrl) )
				{
					this.MouseLeaveImageUrl = "";
				}

				if(this.MouseLeaveImageUrl !="")  
				{
					this.BackgroundImage = Image.FromFile( this.MouseLeaveImageUrl);
					this.Size = this.BackgroundImage.Size;
					this.UserBackgroundImage = true;
				}
				else
					this.UserBackgroundImage = false;

				this.Text = _HisNode.Name;
				this.Location = new Point(_HisNode.X , _HisNode.Y);

				string err = "";
				try
				{
					err = "获取 HisNodePosType 出错！";
					this._positionType = _HisNode.HisNodePosType ; 
					err = "获取 HisNodeWorkType 出错！";
					this._workType = _HisNode.HisNodeWorkType;

					err = " 设置节点样式 Position 出错！";
					this.SetStyleByPosition();
					err = " 设置节点样式 WorkType 出错！";
                    this.DoReSetNodeImg();
				}
				catch( Exception ex)
				{
					this.SetShowTip( err +"["+_HisNode.Name  +"] :\r\n"+ex.Message);
					this.BackColor = Color.Gray; //表示 获取HisNodePosType时出错
				}
			}
		}
		private void SetStyleByPosition()
		{
			//this.BackColor = Color.White;
			//return ;
			switch ( this.PositionType )
			{
				case NodePosType.Start :
					this.BackColor = Color.Green;
					break;
				case NodePosType.Mid:
					//this.BackColor = Color.Wheat;
					this.BackColor = Color.White;
					break;
				case NodePosType.End :
					this.BackColor = Color.Red;
					break;
				default :
					this.BackColor = Color.Gray;
					break;
			}
		}
		private void DoReSetNodeImg()
		{
            string url = Glo.NodeImagePath + this.WorkType.ToString() + ".bmp";
			this.Image = Image.FromFile( url );
			this.UpdateSize( url );
		}
		#endregion 方法
		
	}
}
