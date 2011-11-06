using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections;

namespace BP.Win.WF
{
	public class WinWFNodes : CollectionBase
	{
		public WinWFNodes()
		{
		}
        public WinWFNode this[int index]
        {
            get
            {
                return this.InnerList[index] as WinWFNode;
            }
        }
		public void AddNode(WinWFNode node)
		{
			this.InnerList.Add( node );
		}
		public void Remove(WinWFNode node)
		{
			this.InnerList.Remove(node);
		}
		public void Remove(int index)
		{
			this.InnerList.RemoveAt( index);
		}
	}

}
