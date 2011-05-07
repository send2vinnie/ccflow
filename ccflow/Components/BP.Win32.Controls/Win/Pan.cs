using System;
using System.Drawing;
using System.Windows.Forms;

namespace BP.Win.Controls
{
	[ToolboxBitmap(typeof(System.Windows.Forms.Panel))]
	public class Pan : System.Windows.Forms.Panel
	{
		public Pan()
		{
		}

		public Control ActivateControl
		{
			get
			{
				foreach(Control con in this.Controls)
				{
					if(con.Visible)
						return con;
				}
				return this.Controls[0];
			}
		}
		private Control FindControl(string name)
		{
			foreach(Control con in this.Controls)
			{
				if(con.Name == name )
				{
					return con;
				}
			}
			return null;
		}
		private void RemoveControl(string name)
		{
			Control tmp = FindControl(name);
			if(tmp != null)
			{
				this.Controls.Remove(tmp);
				tmp.Dispose();
			}
		}

		public void AddUC( UCBase uc)
		{
			RemoveControl ( uc.Name );
			if(this.Controls.Count>0)
				this.ActivateControl.Hide();
			uc.Location = new Point(0,0);
			uc.Dock = DockStyle.Fill;
			this.Controls.Add( uc );
			uc.BringToFront();
		}
		
	}
}
