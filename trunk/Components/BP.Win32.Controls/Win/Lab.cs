using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

namespace BP.Win.Controls
{
	[System.Drawing.ToolboxBitmap(typeof(System.Windows.Forms.Label))]
	public class Lab : System.Windows.Forms.Label
	{
		public Lab()
		{
			this.AutoSize=true;
			//this.ForeColor=Color.Blue;
		}
	}
}
