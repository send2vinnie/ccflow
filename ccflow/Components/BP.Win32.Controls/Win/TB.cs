using System;
using System.Drawing;
using System.Windows.Forms;

namespace BP.Win.Controls
{
	[ToolboxBitmap(typeof(System.Windows.Forms.TextBox))]
	public class TB : System.Windows.Forms.TextBox
	{
		public TB()
		{
		}
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if(keyData==Keys.Return && !this.Multiline)
				return base.ProcessDialogKey(Keys.Tab);
			else
				return base.ProcessDialogKey(keyData);
		}
		
	}
}
