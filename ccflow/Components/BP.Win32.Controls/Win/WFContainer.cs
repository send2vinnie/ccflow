using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using BP.Win.Controls;

namespace BP.Win32.Controls
{
	/// <summary>
	/// WFContainer 的摘要说明。
	/// </summary>
	public class WFContainer : System.Windows.Forms.Panel
	{

		#region 构造函数
		public WFContainer()
		{
			this.SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw,true);
			this.SetStyle(System.Windows.Forms.ControlStyles.UserPaint,true);
			this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint,true);
			this.SetStyle(System.Windows.Forms.ControlStyles.DoubleBuffer,true);
			this.SetStyle(System.Windows.Forms.ControlStyles.Selectable,true);
			
			this.BorderStyle = BorderStyle.Fixed3D;
			this.AllowDrop = true;
			this.BackColor = System.Drawing.Color.White;
			this.AutoScroll = true;

		}	
		#endregion 


		public bool lineContains(Point begin , Point end , Point loc ,int linewidth)
		{
			int xx = begin.X<end.X?begin.X:end.X;
			int yy = begin.Y<end.Y?begin.Y:end.Y;
			Rectangle rect = new Rectangle( xx,yy , 1,1);
			xx = Math.Abs( end.X-begin.X) + 2* linewidth;
			yy = Math.Abs( end.Y-begin.Y) + 2* linewidth;
			rect.Size = new Size( xx , yy );
			rect.Offset( -linewidth ,-linewidth );
			if(!rect.Contains( loc ))
			{
				return false;
			}

			double angle = Math.Atan2( loc.Y-begin.Y ,loc.X-begin.X )
				- Math.Atan2( end.Y-begin.Y , end.X-begin.X );

			xx = loc.X - begin.X;
			yy = loc.Y - begin.Y;
			double h = Math.Abs( Math.Sin( angle ) * Math.Sqrt( xx*xx + yy*yy ) ) ;
			
			if( h > linewidth )
				return false ;
			else
				return true;
		}



		#region 扩展成员变量
		protected Rectangle dragBoxFromMouseDown;
		protected Point locMouse_OffSet = new Point(0 ,0);//
		#endregion 扩展成员变量


		#region 重写方法

		public virtual void Save()
		{
		}
		protected virtual  void DrawLines(PaintEventArgs e)
		{
		}

		protected virtual void SetNodeLocation(string name , Point loc)
		{
		}


		protected override bool ProcessDialogKey(Keys keyData)
		{
			if ((Control.ModifierKeys & Keys.Control) == Keys.Control
				&& (keyData & Keys.S) == Keys.S) 
			{
				this.Save();
			}
			return base.ProcessDialogKey( keyData);
		}
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            this.DrawLines(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.dragBoxFromMouseDown = Rectangle.Empty;
        }
        protected override void OnDragOver(DragEventArgs e)
        {
            base.OnDragOver(e);
            //test
            //e.KeyState
            //1 鼠标左按钮。 
            //2 鼠标右按钮。 
            //4 SHIFT 键。 
            //8 CTRL 键。 
            //16 鼠标中键。 
            //32 ALT 键。 
            if (!e.Data.GetDataPresent(typeof(string)))
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            else if ((e.KeyState & 1) == 1 && //按下左键
                (e.AllowedEffect & DragDropEffects.Link) == DragDropEffects.Link)
            {
                e.Effect = DragDropEffects.Link;
            }
            else if ((e.KeyState & 1) == 1 &&
                (e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
            {
                Point loc = this.PointToClient(new Point(e.X, e.Y));
                loc.Offset(-this.locMouse_OffSet.X, -this.locMouse_OffSet.Y);

                this.SetNodeLocation(e.Data.GetData(typeof(string)).ToString(), loc);

                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
            //testend
        }
		#endregion 重写方法


		#region 生成工作流图片
        /// <summary>
        /// 保存流程图片
        /// </summary>
        /// <param name="flowImagePath"></param>
        /// <param name="fmt"></param>
        /// <param name="flowName"></param>
        public void SaveFlowImage(string flowImagePath, ImageFormat fmt, string flowName)
        {
            Bitmap img = new Bitmap(this.Width, this.Height);
            Graphics gp = Graphics.FromImage(img);
            gp.CompositingQuality = CompositingQuality.HighQuality;
            gp.SmoothingMode = SmoothingMode.HighQuality;

            PaintEventArgs e = new PaintEventArgs(gp, this.Bounds);
            this.OnParentBackColorChanged(e);
            this.OnPaintBackground(e);
            this.OnParentBackgroundImageChanged(e);
            this.OnPaint(e);
            foreach (Control con in this.Controls)
            {
                IPaint p = con as IPaint;
                if (p != null)
                {
                    p.Paint(e);
                }
            }

            //			try
            //			{
            //				Font f = this.Font; 
            //				Brush b = new SolidBrush( Color.Blue );
            //				gp.DrawString("009900900",f,b, 10 , 1000 );
            //				//gp.Flush();
            //			}
            //			catch
            //			{
            //
            //			}

            try
            {
                img.Save(flowImagePath, ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                BP.DA.Log.DebugWriteWarning(ex.Message);
                MessageBox.Show(ex.Message, "@保存工作流图片失败！但是不影响其它功能进行。" + flowImagePath);
            }

            gp.Dispose();
            img.Dispose();
        }
		#endregion 生成工作流图片

	}
}
