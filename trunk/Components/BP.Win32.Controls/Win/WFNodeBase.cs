using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
//using BP.WF;
using BP.Win.Controls;

namespace BP.Win.WF
{
	/// <summary>
	/// WFNodeBase 的摘要说明。
	/// </summary>
	public class WFNodeBase : System.Windows.Forms.UserControl ,IPaint
	{
        public int XX = 0;
        public int YY = 0;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.XX = e.X;
            this.YY = e.Y;
            base.OnMouseDown(e);
        }
		public override Font Font
		{
            get
            {
                //Font f = new Font("方正姚体",10);
                //Font f = new Font("方正姚体",10);
                //f.Bold=true;
                return base.Font;
            }
			set
			{
				base.Font = value;
			}
		}

        public WFNodeBase()
        {
            this.SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.UserPaint, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.DoubleBuffer, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.Selectable, true);
            this.BackColor = Color.White;
            this.Height = 22;
        }

		#region 属性
		//public Image GetImage(BP.WF
		private Image  _image = null;
		public  Image  Image
		{
			get
			{
				if (_image==null)
				{
					//					string url = BP.SystemConfig.NodeImagePath + Work.ToString();
					//					_image = Image.FromFile( url );
					throw new Exception("使用图片前没有给它赋值。");
				}
				return _image;
			}
			set
			{
				_image=value;
			}
		}
        public void SetShowTip(string text)
        {
            ToolTip _tip = new ToolTip();
            _tip.SetToolTip(this, text);
        }
        protected virtual void UpdateSize(string imgUrl)
        {
         //   throw new Exception(imgUrl);
            Graphics g = this.CreateGraphics();
            int w = g.MeasureString(this.Text, this.Font).ToSize().Width;
            this.Width = w + 3;
            this.Height = this.Font.Height;
            this.Image = Image.FromFile(imgUrl);
            this.Width += this.Image.Width;
            this.Height = this.Image.Height;
        }
		/// <summary>
		/// 中心Point
		/// </summary>
		public Point CenterPoint
		{
			get
			{
				return _centerPoint;
			}
			set
			{
				_centerPoint = value;
			}
		}
		private Point _centerPoint =new Point(0,0);
		private   string _mouseLeaveImageUrl ="";
		protected string MouseLeaveImageUrl 
		{
			get
			{
				return _mouseLeaveImageUrl ;
			}
			set
			{
				_mouseLeaveImageUrl = value;
			}
		}
		private string _mouseOnImageUrl ="";
		protected  string MouseOnImageUrl 
		{
			get
			{
				return _mouseOnImageUrl ;
			}
			set
			{
				_mouseOnImageUrl = value;
			}
		}
		private bool _userBackgroundImage = false;
		protected  bool UserBackgroundImage
		{
			get
			{
				return _userBackgroundImage ;
			}
			set
			{
				_userBackgroundImage = value;
			}
		}
		#endregion 属性

		
		#region 鼠标事件
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter (e);
			if(this.MouseOnImageUrl!="")
			{
				this.BackgroundImage.Dispose();
				this.BackgroundImage = Image.FromFile(this.MouseOnImageUrl);
			}
		}
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave (e);
			if(this.MouseOnImageUrl!="")
			{
				this.BackgroundImage.Dispose();
				this.BackgroundImage = Image.FromFile(this.MouseLeaveImageUrl);
			}
		}
		#endregion


		#region 行为
		protected override void OnLocationChanged(EventArgs e)
		{
			base.OnLocationChanged (e);
			this._centerPoint.X =this.Location.X + this.Width/2 ;
			this._centerPoint.Y =this.Location.Y + this.Height/2 ;
		}
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged (e);
			this._centerPoint.X =this.Location.X + this.Width/2 ;
			this._centerPoint.Y =this.Location.Y + this.Height/2 ;
		}

		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			this.Refresh();
		}
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			this.Refresh();
		}
		#endregion

		#region 绘画接口
		protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)
		{
			if( this._userBackgroundImage )
			{
				base.OnPaintBackground(e );
			}
			else
			{
				int d0=0;
				LinearGradientBrush lbrush ;
                Rectangle rect0 = new Rectangle(d0, d0, this.Width - d0 * 2, this.Height - d0 * 2);
				//lbrush = new LinearGradientBrush(rect0,this.BackColor,Color.White,LinearGradientMode.ForwardDiagonal);
				//lbrush = new LinearGradientBrush(rect0,Color.Wheat,this.BackColor,LinearGradientMode.Horizontal);

                if (this.ToString() == "BP.Win.WF.WinWFLab")
                {
                    rect0 = new Rectangle(d0, d0, this.Width - d0 * 2, this.Height - d0 * 2);
                    lbrush = new LinearGradientBrush(rect0, Color.White, Color.White, LinearGradientMode.Horizontal);
                }
                else
                {
                    lbrush = new LinearGradientBrush(rect0, Color.Wheat, this.BackColor, LinearGradientMode.Horizontal);
                }


			//	e.Graphics.FillEllipse(lbrush,rect0);
               e.Graphics.FillRectangle(lbrush, rect0);
				lbrush.Dispose(); 
			}
		}
		public bool ThumbnailCallback()
		{
			return false;
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			int  d=3;
			Rectangle  rect = new Rectangle(d,d,this.Width - d*2 ,this.Height-d*2); //rect.Offset( this.Location.X ,this.Location.Y);//与IPaint 区别
			if( !this._userBackgroundImage)
			{
				if( this.Image != null )
				{
					Rectangle imgrect = new Rectangle( rect.Location ,new Size(this.Image.Height,this.Image.Height-2));
					Bitmap myBitmap = new Bitmap( this.Image );
					Image.GetThumbnailImageAbort myCallback =
						new Image.GetThumbnailImageAbort(ThumbnailCallback);
					Image myThumbnail = myBitmap.GetThumbnailImage(
						imgrect.Width,imgrect.Height, myCallback, IntPtr.Zero);
					e.Graphics.DrawImage( myThumbnail , imgrect );
					//
					rect.Width -= this.Image.Width;
					rect.Offset(  this.Image.Width ,0 );
				}
				SolidBrush brush = new SolidBrush(this.ForeColor);
				//e.Graphics.DrawString( this.Text ,this.Font,brush,rect.Location);// ,this.TextFormat);

				int i= this.Image.Height-this.Font.Height;

				try
				{
					e.Graphics.DrawString( this.Text ,this.Font,brush,rect.Location.X, i/2  ); 
				}
				catch(Exception ex)
				{
					throw new Exception("可能是图片位置错误。"+ex.Message);
				}

				//e.Graphics.DrawString( this.Text ,this.Font,brush,rect.Location.X, (300-this.Font.Height)/2 );// ,this.TextFormat);


				brush.Dispose();

				rect.Offset(-3,-3);
				rect.Width +=6;
				rect.Height +=6;
				if( this.Focused )
					ControlPaint.DrawBorder(e.Graphics,rect,ControlPaint.Dark(Color.White),2,ButtonBorderStyle.Outset 
						,ControlPaint.Dark(Color.Black),2,ButtonBorderStyle.Outset
						,ControlPaint.Dark(Color.Black),2,ButtonBorderStyle.Outset
						,ControlPaint.Dark(Color.Black),2,ButtonBorderStyle.Outset);
				else
					ControlPaint.DrawBorder(e.Graphics,rect,ControlPaint.Dark(Color.White),2,ButtonBorderStyle.Solid 
						,ControlPaint.Dark(Color.White),1,ButtonBorderStyle.Solid
						,ControlPaint.Dark(Color.White),1,ButtonBorderStyle.Solid
						,ControlPaint.Dark(Color.White),1,ButtonBorderStyle.Solid);
			}
		}

		void IPaint.Paint(System.Windows.Forms.PaintEventArgs e)
		{
			if( this._userBackgroundImage )
			{
				base.OnPaintBackground(e );
			}
			else
			{
				int d0=0;
				LinearGradientBrush lbrush ;
				Rectangle rect0 = new Rectangle(d0,d0,this.Width-d0*2 ,this.Height-d0*2);
				rect0.Offset( this.Location.X ,this.Location.Y);
			
				lbrush = new LinearGradientBrush(rect0,this.BackColor,Color.White,LinearGradientMode.ForwardDiagonal);
				e.Graphics.FillRectangle(lbrush,rect0);

				lbrush.Dispose();
			}

			int  d=3;
			Rectangle  rect = new Rectangle(d,d,this.Width-d*2 ,this.Height-d*2);
			rect.Offset( this.Location.X ,this.Location.Y);
			if( !this._userBackgroundImage)
			{
				if( this.Image != null )
				{
					Bitmap myBitmap = new Bitmap( this.Image );
					Rectangle imgrect = new Rectangle( rect.Location ,new Size(this.Image.Height,this.Image.Height-2));
					Image.GetThumbnailImageAbort myCallback =
						new Image.GetThumbnailImageAbort(ThumbnailCallback);
					Image myThumbnail = myBitmap.GetThumbnailImage(
						imgrect.Width,imgrect.Height, myCallback, IntPtr.Zero);
					e.Graphics.DrawImage( myThumbnail , imgrect );
					//
					rect.Width -= this.Image.Width;
					rect.Offset(  this.Image.Width ,0 );
				}

				SolidBrush brush = new SolidBrush(this.ForeColor);
				e.Graphics.DrawString( this.Text ,this.Font,brush,rect.Location);// ,this.TextFormat);
				brush.Dispose();
			}

		}
		#endregion 绘画接口


	}
}
