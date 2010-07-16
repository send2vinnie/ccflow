using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace BP.Win.Controls
{
	[ToolboxBitmap(typeof(System.Windows.Forms.ToolBar))]
	public class TBar : System.Windows.Forms.ToolBar
	{
		public TBar()
		{
			//this.Appearance = ToolBarAppearance.Flat;
			//this.ButtonSize = new Size(22,22);
			this.ShowToolTips = true;

			this.BackColor = SystemColors.Control;
		}

	}
}
