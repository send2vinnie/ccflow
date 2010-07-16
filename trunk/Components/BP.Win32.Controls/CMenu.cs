using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BP.Win32.Controls
{
	/// <summary>
	/// 快捷菜单（右击弹出菜单）
	/// </summary>
	[ToolboxBitmap(typeof(System.Windows.Forms.ContextMenu))]
	public class CMenu : System.Windows.Forms.ContextMenu
	{
		public CMenu()
		{
		}
		/// <summary>
		/// 增加菜单项
		/// </summary>
		/// <param name="text"></param>
		public void AddItem(string text)
		{
			MenuItem mi = new MenuItem();
			mi.Text = text;
			this.MenuItems.Add(mi) ; 
		}
	}
	/// <summary>
	/// 菜单项目
	/// </summary>
	public class BPMenuItem:System.Windows.Forms.MenuItem
	{
		/// <summary>
		/// 菜单项目
		/// </summary>
		public BPMenuItem()
		{
		}
		/// <summary>
		/// 标记
		/// </summary>
		private string _Tag=null;
		/// <summary>
		/// 标记
		/// </summary>
		public new string Tag
		{
			get
			{
				return _Tag;
			}
			set
			{
				_Tag=value;
			}
		}
	}
}
