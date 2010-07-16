using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CWAI.Win32.Controls
{
	[ToolboxBitmap(typeof(System.Windows.Forms.Button))]
	public class Btn : System.Windows.Forms.Button
	{
		public Btn()
		{
		}
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if(keyData==Keys.Down||keyData==Keys.Right)
				return base.ProcessDialogKey(Keys.Tab);
			else
				return base.ProcessDialogKey(keyData);
		}
		public string GetFileName(string filter)
		{
			OpenFileDialog openflie=new OpenFileDialog();
			openflie.RestoreDirectory = true;
			openflie.Filter=filter;//"Excel нд╪Ч(*.xls)|*.xls";
			if(openflie.ShowDialog()==DialogResult.OK)
			{
				return openflie.FileName;
			}
			else
				return "";
		}
		public string GetCheckCols( DataTable tb)
		{
			FCheckedList  fchk =new FCheckedList();
			fchk.BindData( tb );
			if( fchk.ShowDialog()==DialogResult.OK)
			{
				return fchk.Checks;
			}
			else
				return "";
		}
		
	}
}
