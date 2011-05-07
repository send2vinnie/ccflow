using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace BP.Win.Controls
{
	/// <summary>
	/// UC 的摘要说明。
	/// </summary>
	public class UCBase : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UCBase()
		{	InitializeComponent();

		}

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region 组件设计器生成的代码
		/// <summary> 
		/// 设计器支持所需的方法 - 不要使用代码编辑器 
		/// 修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion
	}
}
