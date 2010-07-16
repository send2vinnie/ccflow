using System;
using System.Windows.Forms;

namespace BP.Win.Controls
{
	/// <summary>
	/// DG_DDL 的摘要说明。
	/// </summary>
	public class DG_DDL :DDL
	{
		public DG_DDL()
		{
		}
		const int WM_KEYUP = 0x101;
		protected override void WndProc(ref Message m)
		{
			if(m.Msg == 0x101) // WM_KEYUP
			{//ignore keyup to avoid problem with tabbing & dropdownlist;
				return;
			}
			base.WndProc(ref m);
		}
		public void SetDataGrid( DataGrid parent)
		{
			this.Parent = parent;
		}
	}
	public delegate void EventHandlerDDLValueChanged( int changingRow, object newValue );
}
