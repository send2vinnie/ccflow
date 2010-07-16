using System;
using System.Drawing;
using System.Windows.Forms;

namespace BP.Win.Controls
{
	[ToolboxBitmap(typeof(System.Windows.Forms.StatusBar))]
	public class SBar : System.Windows.Forms.StatusBar
	{
		public SBar()
		{
			this.ShowPanels = true ;
		}
		
	}
}
