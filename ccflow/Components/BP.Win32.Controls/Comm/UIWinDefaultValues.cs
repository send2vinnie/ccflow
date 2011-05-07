using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using BP.DA;

using BP.En;
using BP.Sys;
using BP.Win32.Controls;
using BP.Port ; 
using BP.Web; 
using BP.Web.Controls;

namespace BP.Win32.Controls
{
	/// <summary>
	/// UIDefaultValues 的摘要说明。
	/// </summary>
	public class UIWinDefaultValues : System.Windows.Forms.Form 
	{
		#region 控件
		private BP.Win32.Controls.BPToolbar bpToolbar1;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		private BP.Win32.Controls.TB tb1;
		private BP.Win32.Controls.Btn btn1;
		private BP.Win32.Controls.Btn btn2;
		private BP.Win32.Controls.Btn btn3;
		private BP.Win32.Controls.Btn btn4;
		private BP.Win32.Controls.CBL cbl1;

		#region 设置变量
		#region Ens
		private Entity _HisEn=null;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton toolBarButton2;
		private System.Windows.Forms.ToolBarButton toolBarButton3;
		private System.Windows.Forms.ToolBarButton toolBarButton4;
		private System.Windows.Forms.ToolBarButton toolBarButton5;
		private System.Windows.Forms.ToolBarButton toolBarButton6;
	
		public Entity HisEn
		{
			get
			{
				return _HisEn;
			}
			set
			{
				_HisEn=value;
			}
		}
		#endregion 

	    private string _attrKey=null;
		public string AttrKey
		{
			get
			{
				return _attrKey;
			}
			set
			{
				_attrKey=value;

			}
		}
		#endregion

		#region 私有
		 
		#endregion

		#region 构造函数

		

		public UIWinDefaultValues(Entity en, string attrKey)
		{
		 
			this.HisEn =en;
			this.AttrKey =attrKey;
		 
			this.Text="获取或设置实体\""+en.EnDesc+"\"的\""+attrKey+"\"默认值";
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			this.BindCBL(3);
			this.ShowDialog();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
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
		#endregion

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.bpToolbar1 = new BP.Win32.Controls.BPToolbar();
			this.tb1 = new BP.Win32.Controls.TB();
			this.btn1 = new BP.Win32.Controls.Btn();
			this.btn2 = new BP.Win32.Controls.Btn();
			this.btn3 = new BP.Win32.Controls.Btn();
			this.btn4 = new BP.Win32.Controls.Btn();
			this.cbl1 = new BP.Win32.Controls.CBL();
			this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton6 = new System.Windows.Forms.ToolBarButton();
			this.SuspendLayout();
			// 
			// bpToolbar1
			// 
			this.bpToolbar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						  this.toolBarButton1,
																						  this.toolBarButton2,
																						  this.toolBarButton3,
																						  this.toolBarButton4,
																						  this.toolBarButton5,
																						  this.toolBarButton6});
			this.bpToolbar1.DropDownArrows = true;
			this.bpToolbar1.Location = new System.Drawing.Point(0, 0);
			this.bpToolbar1.Name = "bpToolbar1";
			this.bpToolbar1.ShowToolTips = true;
			this.bpToolbar1.Size = new System.Drawing.Size(688, 41);
			this.bpToolbar1.TabIndex = 0;
			this.bpToolbar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.bpToolbar1_ButtonClick);
			// 
			// tb1
			// 
			this.tb1.Location = new System.Drawing.Point(16, 56);
			this.tb1.Name = "tb1";
			this.tb1.Size = new System.Drawing.Size(104, 21);
			this.tb1.TabIndex = 2;
			this.tb1.Text = "tb1";
			// 
			// btn1
			// 
			this.btn1.BtnType = BP.Web.Controls.BtnType.Confirm;
			this.btn1.Location = new System.Drawing.Point(128, 56);
			this.btn1.Name = "btn1";
			this.btn1.Size = new System.Drawing.Size(120, 23);
			this.btn1.TabIndex = 3;
			this.btn1.Text = "保存为我的默认值";
			// 
			// btn2
			// 
			this.btn2.BtnType = BP.Web.Controls.BtnType.Confirm;
			this.btn2.Location = new System.Drawing.Point(264, 56);
			this.btn2.Name = "btn2";
			this.btn2.Size = new System.Drawing.Size(120, 23);
			this.btn2.TabIndex = 4;
			this.btn2.Text = "保存为全局默认值";
			// 
			// btn3
			// 
			this.btn3.BtnType = BP.Web.Controls.BtnType.Confirm;
			this.btn3.Location = new System.Drawing.Point(472, 56);
			this.btn3.Name = "btn3";
			this.btn3.TabIndex = 5;
			this.btn3.Text = "确定";
			this.btn3.Click += new System.EventHandler(this.btn3_Click);
			// 
			// btn4
			// 
			this.btn4.BtnType = BP.Web.Controls.BtnType.Confirm;
			this.btn4.Location = new System.Drawing.Point(392, 56);
			this.btn4.Name = "btn4";
			this.btn4.TabIndex = 6;
			this.btn4.Text = "取消";
			this.btn4.Click += new System.EventHandler(this.btn4_Click);
			// 
			// cbl1
			// 
			this.cbl1.Location = new System.Drawing.Point(0, 88);
			this.cbl1.Name = "cbl1";
			this.cbl1.Size = new System.Drawing.Size(680, 212);
			this.cbl1.TabIndex = 7;
			// 
			// toolBarButton1
			// 
			this.toolBarButton1.Text = "我的默认值";
			// 
			// toolBarButton2
			// 
			this.toolBarButton2.Text = "全局默认值";
			// 
			// toolBarButton3
			// 
			this.toolBarButton3.Text = "历史记录";
			// 
			// toolBarButton4
			// 
			this.toolBarButton4.Text = "删除";
			// 
			// toolBarButton5
			// 
			this.toolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// toolBarButton6
			// 
			this.toolBarButton6.Text = "帮助";
			// 
			// UIWinDefaultValues
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(688, 325);
			this.Controls.Add(this.cbl1);
			this.Controls.Add(this.btn4);
			this.Controls.Add(this.btn3);
			this.Controls.Add(this.btn2);
			this.Controls.Add(this.btn1);
			this.Controls.Add(this.tb1);
			this.Controls.Add(this.bpToolbar1);
			this.Name = "UIWinDefaultValues";
			this.Text = "UIDefaultValues";
			this.ResumeLayout(false);

		}
		#endregion

		private void BindCBL(int i)
		{
			try
			{
				string sql=""; 
				switch(i)
				{
					case 1:
						sql="SELECT DefaultVal, DefaultVal as Text FROM Sys_UIDefaultValue WHERE EmpId="+WebUser.No+" and ClassName='"+this.HisEn.ToString()+"' and AttrKey='"+this.AttrKey+"'" ;
						break;
					case 2:
						sql="SELECT DefaultVal, DefaultVal as Text  FROM Sys_UIDefaultValue WHERE ClassName='"+this.HisEn.ToString()+"' and AttrKey='"+this.AttrKey+"' and EmpId=0";

						break;
					case 3:
						string field=this.HisEn.EnMap.GetFieldByKey(this.AttrKey) ; 
						sql="SELECT DISTINCT TOP 40  "+field+" as DefaultVal,  "+field+" as Text  FROM  "+this.HisEn.EnMap.PhysicsTable+" WHERE  len(rtrim(ltrim("+field+" )) ) > 0 " ;
						break;
					default:
						throw new Exception("errory ");
				}

				DataTable dt = DBAccess.RunSQLReturnTable(sql); 
				this.cbl1.BindTable(dt,"DefaultVal","DefaultVal") ; 

				//this.cbl1.bin
				 
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message) ; 
				//this.Alert(ex);
			}

		}

		private void bpToolbar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			string msg="";
			
			try 
			{
				switch(e.Button.Text)
				{
					case "保存":
						msg="执行保存出现错误："; 
						//this.Btn_Save();
						break;
					case "关闭":
						this.Close();
						break;
					case "新建":
						msg="执行新建出现错误：";
						//this.Btn_New();
						break;
					default:
						//this.dg1.TableStyles[0].clo
						// this.dg1.TableStyles[0].GridColumnStyles[0].HeaderText
						break;
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(msg+ex.Message ) ; 

				//this.Alert(msg+ex.Message);
				 
			}
		}

		private void btn3_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btn4_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
