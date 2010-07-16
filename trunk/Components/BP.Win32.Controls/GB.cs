using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BP.Win32.Controls
{
	[ToolboxBitmap(typeof(System.Windows.Forms.GroupBox))]
	public class GB : System.Windows.Forms.GroupBox
	{
		
		#region 构造
		public GB()
		{
			 
		}
		#endregion



		#region  find ctl
        public ComboBox GetComboBoxByKey(string key)
        {
            return (ComboBox)this.GetCtlByKey(key);

        }
		public DDL GetDDLByKey(string key)
		{ 
			return (DDL)this.GetCtlByKey(key);
			
		}

		public TB GetTBByKey(string key)
		{ 
			return (TB)this.GetCtlByKey(key);
		}
		public CB GetCBByKey(string key)
		{ 
			return (CB)this.GetCtlByKey(key);
		}
		public DateTimePicker GetDateByKey(string key)
		{ 
			return (DateTimePicker)this.GetCtlByKey(key);
		}

		public Control GetCtlByKey(string key)
		{
			foreach(Control ctl in this.Controls)
			{
				if (ctl.Name==key)
				{
					return  ctl;
				}
			}
			throw new Exception("没有找到name="+key+"的控件.");
		}
		/// <summary>
		/// 是否包含指定key 的 控件。
		/// </summary>
		/// <param name="key">指定的key</param>
		/// <returns>true/false</returns>
		public bool IsContains(string key)
		{
			foreach(Control ctl in this.Controls)
			{
				if (ctl.Name==key)
				{
					return  true;
				}
			}
			return false;
		}
		#endregion

	}
}
