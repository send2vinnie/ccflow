using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BP.Win32.Controls
{
	[ToolboxBitmap(typeof(System.Windows.Forms.RichTextBox))]
	public class RTB : System.Windows.Forms.RichTextBox
	{
		public RTB()
		{			
		}
		public RTB(string name, string text)
		{
			this.Name=name;
			this.Text=text;
		}
	}
}
