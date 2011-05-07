using System;
using System.Drawing;
using System.Windows.Forms;

namespace BP.Win.Controls
{
	[ToolboxBitmap(typeof(System.Windows.Forms.RadioButton))]
	public class RB : System.Windows.Forms.RadioButton
	{
		public RB()
		{
		}
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if(keyData==Keys.Return)
				return base.ProcessDialogKey(Keys.Tab);
			else
				return base.ProcessDialogKey(keyData);
		}
		
	}
}
