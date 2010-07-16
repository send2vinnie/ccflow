using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;


namespace BP.Win.Controls
{
	[ToolboxBitmap(typeof(System.Windows.Forms.DateTimePicker))]
	public class DTP : System.Windows.Forms.DateTimePicker
	{
		public DTP()
		{
			this.Format=System.Windows.Forms.DateTimePickerFormat.Custom;
			this.CustomFormat="yyyy-MM-dd";
		}
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if(keyData==Keys.Return)
				return base.ProcessDialogKey(Keys.Tab);
			else
				return base.ProcessDialogKey(keyData);
		}
		public string DateStr
		{
			get
			{
				return this.Value.ToString("yyyy-MM-dd");
			}
		}
		public string MonthDayStr
		{
			get
			{
				return this.Value.ToString("MM-dd");
			}
		}
		
		public void SetText_MMdd(string text)
		{
			DateTime dt = DateTime.Now;
			if(text.Length == 4)
				dt = DateTime.Parse(DateTime.Today.Year.ToString()+text);
			else if(text.Length == 5)
				dt = DateTime.Parse(DateTime.Today.Year.ToString() + text[2] + text);

			this.Value = dt;
		}
	}
}
