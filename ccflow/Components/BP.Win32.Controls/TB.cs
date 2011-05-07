using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BP.Win32.Controls
{
	[ToolboxBitmap(typeof(System.Windows.Forms.TextBox))]
	public class TB : System.Windows.Forms.TextBox
	{
		public TB()
		{			
		}
		public TB(string name, string text)
		{
			this.Name=name;
			this.Text=text;
		}

	}
}
