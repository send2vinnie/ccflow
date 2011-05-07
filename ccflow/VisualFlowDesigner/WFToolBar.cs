using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;

namespace BP.Win.WF
{
	[ToolboxBitmap(typeof(System.Windows.Forms.ToolBar))]
	public class WFToolBar : System.Windows.Forms.ToolBar
	{
		public WFToolBar()
		{
			FillToolBarButton(4);
			this.Appearance = ToolBarAppearance.Flat;
			this.ButtonSize = new Size(22,22);
			this.BorderStyle = BorderStyle.Fixed3D;

		}
		
		[Category("行为"),Description("设置value个工具按钮")]
		public int  ButtonsCount 
		{
			get
			{
				return this.Buttons.Count;
			}
			set
			{
				this.FillToolBarButton(value);
			}
		}

        public void FillToolBarButton(int count)
        {
            this.Buttons.Clear();
            ToolBarButton tbtn = new ToolBarButton();
            tbtn.ImageIndex = 0;
            tbtn.Style = ToolBarButtonStyle.ToggleButton;
            tbtn.Pushed = true;
            this.Buttons.Add(tbtn);

            for (int i = 1; i < count; i++)
            {
                tbtn = new ToolBarButton();
                tbtn.ImageIndex = i;
                tbtn.Style = ToolBarButtonStyle.ToggleButton;
                this.Buttons.Add(tbtn);
            }

            this.Buttons[0].Text = this.ToE("Mouse", "鼠标");
            this.Buttons[1].Text = this.ToE("OrdinaryNode", "节点");
            this.Buttons[2].Text = this.ToE("NodeLine", "节点连线");
            this.Buttons[3].Text = this.ToE("Label", "标签");
        }
        public string ToE(string no,string chval)
        {
           // return no;
            return BP.Sys.Language.GetValByUserLang(no,chval);
        }
		protected override void OnButtonClick(ToolBarButtonClickEventArgs e)
		{
			base.OnButtonClick(e);
			Global.ToolIndex = e.Button.ImageIndex;
		}

		public void SetPushButton(int index )
		{
			for(int i=0;i<this.Buttons.Count ; i++)
			{
				if(i == index)
				{
					this.Buttons[i].Pushed = true;
				}
				else
					this.Buttons[i].Pushed = false;
			}
		
		}
	}
}
