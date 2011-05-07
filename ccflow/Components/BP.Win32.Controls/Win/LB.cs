using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace BP.Win.Controls
{
	[ToolboxBitmap(typeof(System.Windows.Forms.ListBox))]
	public class LB : System.Windows.Forms.ListBox
	{
		public LB()
		{
			this.HorizontalScrollbar=true;
		}
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if(keyData==Keys.Return)
				return base.ProcessDialogKey(Keys.Tab);
			else
				return base.ProcessDialogKey(keyData);
		}
		
		public object[] GetSelectedItemsArray()
		{
			object[] items = new object[this.SelectedItems.Count];
			for(int i =0;i<this.SelectedItems.Count;i++)
			{
				items[i] =  this.SelectedItems[i];
			}
			return items;
		}

		public void BindEntities(CollectionBase list)
		{
			this.DataSource = list; 
		}
	}
}
