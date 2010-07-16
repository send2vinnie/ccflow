using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using CWAI.Win.Controls;
using System.Runtime.Remoting;

namespace CWAI.Win.Controls
{
	/// <summary>
	/// ClassTest 的摘要说明。
	/// </summary>
	public class ClassTest : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private CWAI.Win.Controls.Tree tree1;
		private CWAI.Win.Controls.TB tb1;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ClassTest()
		{
			InitializeComponent();
		}
		public void FrmShow()
		{
			this.Show();
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

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.tree1 = new CWAI.Win.Controls.Tree();
			this.tb1 = new CWAI.Win.Controls.TB();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(344, 15);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(72, 22);
			this.button1.TabIndex = 0;
			this.button1.Text = "确定";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// tree1
			// 
			this.tree1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tree1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.tree1.HideSelection = false;
			this.tree1.HotTracking = true;
			this.tree1.ImageIndex = -1;
			this.tree1.Location = new System.Drawing.Point(0, 40);
			this.tree1.Name = "tree1";
			this.tree1.SelectedImageIndex = -1;
			this.tree1.Size = new System.Drawing.Size(416, 280);
			this.tree1.TabIndex = 4;
			this.tree1.FirstExpand += new CWAI.Win.Controls.OnFirstExpandEventHandler(this.tree1_FirstExpand);
			// 
			// tb1
			// 
			this.tb1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tb1.Location = new System.Drawing.Point(0, 16);
			this.tb1.Name = "tb1";
			this.tb1.Size = new System.Drawing.Size(336, 21);
			this.tb1.TabIndex = 5;
			this.tb1.Text = "CWAI.En.Base.Entity";
			// 
			// ClassTest
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(416, 325);
			this.Controls.Add(this.tb1);
			this.Controls.Add(this.tree1);
			this.Controls.Add(this.button1);
			this.Name = "ClassTest";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "类库测试";
			this.ResumeLayout(false);

		}
		#endregion


		#region 事件
		private void button1_Click(object sender, System.EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			this.tree1.Nodes.Clear();
			TreeNode top=new TreeNode(this.tb1.Text ,0,2);
			top.Tag = this.tb1.Text;
			this.tree1.Nodes.Add(top);
			ArrayList arr = this.GetChildrenTypes( this.tb1.Text );
			if(arr==null )
				return;
			top.Text += "[ "+arr.Count+"]";
			foreach(object obj in arr)
			{
				Type ty = obj as Type;
				this.tree1.AddNode( top ,ty.FullName ,ty.FullName,0,2 ,true);
			}
			this.tree1.ExpandAll();
			this.Cursor = Cursors.Default;
		}

		private void tree1_FirstExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			if( e.Node.Tag!=null )
			{
				ArrayList arr = this.GetChildrenTypes( e.Node.Tag.ToString() );
				if(arr==null || arr.Count==0)
					return;
				e.Node.Text += "[ "+arr.Count+"]";
				foreach(object obj in arr)
				{
					Type ty = obj as Type;
					this.tree1.AddNode( e.Node ,ty.FullName ,ty.FullName,0,2 ,true);
				}
			}
			else
			{
				//MessageBox.Show(e.Node.Text);
			}
		}
		#endregion 事件

	
		
		private ArrayList GetChildrenTypes( string baseClassName)
		{
			ArrayList arr = new ArrayList();
			Type baseClass =null;
			foreach(Assembly ass in CWAI.Win.Controls.ClassFactory.CWAIAssemblies)
			{
				if(baseClass ==null)
					baseClass = ass.GetType( baseClassName);
				Type[] tps = ass.GetTypes();
				for(int i=0; i<tps.Length ;i++)
				{
					if( tps[i].BaseType==null
						|| !tps[i].IsClass
						//|| !tps[i].IsPublic
						)
						continue;
					if( tps[i].BaseType.FullName == baseClassName )
						arr.Add( tps[i] );
				}
			}
			if(baseClass ==null)
			{
				MessageBox.Show("找不到类型"+baseClassName+"！");
			}
			return arr ;
		}
	}
}
