using System;
using System.Drawing;
using System.Windows.Forms;

namespace BP.Win.Controls
{
	[ToolboxBitmap(typeof(System.Windows.Forms.CheckBox))]
	public class CB : System.Windows.Forms.CheckBox
	{
		public CB()
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
