using System;
using System.Drawing;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using BP.DA;

using BP.En;
using BP.Sys;
using BP.Win32.Controls;
using BP.Port ; 
using BP.Web; 
using BP.Pub;
using BP.Web.Controls;
using System.IO;
 

namespace BP.Win32.Comm
{
	/// <summary>
	/// UILoadData 的摘要说明。
	/// </summary>
	public class UILoadData : BP.Win32.PageBase
	{
		 

		private BP.Win32.Controls.BPToolbar bpToolbar1;
		private System.Windows.Forms.ToolBarButton Btn_Open;
		private System.Windows.Forms.ToolBarButton Btn_DBCheck;
		private System.Windows.Forms.ToolBarButton Btn_Close;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.ToolBarButton Btn_Template;
		private System.Windows.Forms.ToolBarButton Btn_Help;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ToolBarButton Btn_LoadAsOverride;
		private System.Windows.Forms.ToolBarButton Btn_LoadAsClear;
		private System.Windows.Forms.ToolBarButton Btn_GenerFile;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.ComponentModel.IContainer components;

		public UILoadData()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

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

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(UILoadData));
			this.bpToolbar1 = new BP.Win32.Controls.BPToolbar();
			this.Btn_Template = new System.Windows.Forms.ToolBarButton();
			this.Btn_GenerFile = new System.Windows.Forms.ToolBarButton();
			this.Btn_Open = new System.Windows.Forms.ToolBarButton();
			this.Btn_DBCheck = new System.Windows.Forms.ToolBarButton();
			this.Btn_LoadAsOverride = new System.Windows.Forms.ToolBarButton();
			this.Btn_LoadAsClear = new System.Windows.Forms.ToolBarButton();
			this.Btn_Close = new System.Windows.Forms.ToolBarButton();
			this.Btn_Help = new System.Windows.Forms.ToolBarButton();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// bpToolbar1
			// 
			this.bpToolbar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						  this.Btn_Template,
																						  this.Btn_GenerFile,
																						  this.Btn_Open,
																						  this.Btn_DBCheck,
																						  this.Btn_LoadAsOverride,
																						  this.Btn_LoadAsClear,
																						  this.Btn_Close,
																						  this.Btn_Help});
			this.bpToolbar1.DropDownArrows = true;
			this.bpToolbar1.ImageList = this.imageList1;
			this.bpToolbar1.Location = new System.Drawing.Point(0, 0);
			this.bpToolbar1.Name = "bpToolbar1";
			this.bpToolbar1.ShowToolTips = true;
			this.bpToolbar1.Size = new System.Drawing.Size(552, 41);
			this.bpToolbar1.TabIndex = 0;
			this.bpToolbar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.bpToolbar1_ButtonClick);
			// 
			// Btn_Template
			// 
			this.Btn_Template.ImageIndex = 6;
			this.Btn_Template.Text = "文件格式输出";
			this.Btn_Template.ToolTipText = "文件格式输出";
			// 
			// Btn_GenerFile
			// 
			this.Btn_GenerFile.ImageIndex = 6;
			this.Btn_GenerFile.Text = "获取外部数据";
			this.Btn_GenerFile.ToolTipText = "根据联接到其他的数据库上。";
			this.Btn_GenerFile.Visible = false;
			// 
			// Btn_Open
			// 
			this.Btn_Open.ImageIndex = 7;
			this.Btn_Open.Text = "选择文件";
			this.Btn_Open.ToolTipText = "选择文件";
			// 
			// Btn_DBCheck
			// 
			this.Btn_DBCheck.ImageIndex = 8;
			this.Btn_DBCheck.Text = "数据检查";
			// 
			// Btn_LoadAsOverride
			// 
			this.Btn_LoadAsOverride.ImageIndex = 9;
			this.Btn_LoadAsOverride.Text = "追加方式装载";
			// 
			// Btn_LoadAsClear
			// 
			this.Btn_LoadAsClear.ImageIndex = 10;
			this.Btn_LoadAsClear.Text = "清空方式装载";
			// 
			// Btn_Close
			// 
			this.Btn_Close.ImageIndex = 0;
			this.Btn_Close.Text = "关闭";
			this.Btn_Close.ToolTipText = "关闭";
			// 
			// Btn_Help
			// 
			this.Btn_Help.ImageIndex = 2;
			this.Btn_Help.Text = "帮助";
			this.Btn_Help.ToolTipText = "帮助";
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 251);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Size = new System.Drawing.Size(552, 22);
			this.statusBar1.TabIndex = 2;
			this.statusBar1.Text = "statusBar1";
			// 
			// dataGrid1
			// 
			this.dataGrid1.DataMember = "";
			this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(0, 41);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(552, 210);
			this.dataGrid1.TabIndex = 3;
			// 
			// UILoadData
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(552, 273);
			this.Controls.Add(this.dataGrid1);
			this.Controls.Add(this.statusBar1);
			this.Controls.Add(this.bpToolbar1);
			this.Name = "UILoadData";
			this.Text = "数据导入";
			this.Load += new System.EventHandler(this.UILoadData_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void bpToolbar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			//string str="";
			try
			{
				switch(e.Button.Text)
				{
					case "文件格式输出":
						this.saveFileDialog1.Title="ccflow软件--请选择文件存放的位置.";
						this.saveFileDialog1.FileName=this.HisEn.EnDesc;
						this.saveFileDialog1.DefaultExt=".xls";
						this.saveFileDialog1.ShowDialog();
						string file =this.saveFileDialog1.FileName;
						if (file.Trim().Length==0)
							return;
						this.ExportExcelExcelTemplate(this.HisEn,file);
						break;
					case "帮助":
						break;
					case "选择文件":
						this.BtnChoseFile();
						 
						if (this.Question("是否需要数据检查?"))
							this.BtnCheckDB();
						break;
					case "数据检查":
						this.BtnCheckDB();
						break;
					case "追加方式装载":
						this.BtnSaveToData(false);
						break;
					case "清空方式装载":
						if (this.Warning("清空方式装载 \t\n \t\n原来的数据将被删除不能恢复，把当前的数据加载里面，确认吗？"))
							this.BtnSaveToData(true);
						break;
					case "关闭":
						this.Close();
						break;					 
					default:
						throw new Exception(e.Button.Text+" error ");
				}
			}
			catch(Exception ex)
			{
				this.ResponseWriteRedMsg(ex);
			}
		}
		/// <summary>
		/// 检查数据的完整性
		/// </summary>
		public bool BtnCheckDB()
		{
			return true;			 
		}
		/// <summary>
		/// 保存到数据库
		/// </summary>
		public void BtnSaveToData(bool IsClear)
		{
			
			int okNum=0;
			int errorNum=0;
			string errMsg="";

			

			Entities ens =this.HisEns;
			if (IsClear)
			{
				ens.RetrieveAll();
				ens.Delete();
			}

			foreach(DataRow dr in this.Table.Rows)
			{
				try
				{
					Entity en =this.HisEns.GetNewEntity;
					foreach(DataColumn dc in this.Table.Columns)
					{
						en.SetValByDesc( dc.ColumnName, dr[dc.ColumnName].ToString().Trim() );
					}

					if (IsClear)
						en.Insert();
					else
						en.Save();
					ens.AddEntity(en);
					okNum++;
				}
				catch(Exception msg)
				{
					errorNum++;
					errMsg+=msg.Message;
				}
			}

			if (errorNum==0)
				this.Information("@文件导入信息:成功导入全部的信息,共"+okNum+"个。");
			else
				this.Alert("@文件导入信息:"+"\n成功导入数"+okNum+",导入错误数:"+errorNum+",错误信息如下:\n"+errMsg);
		}

		/// <summary>
		/// 导入
		/// </summary>
		public void BtnChoseFile()
		{
			try
			{
			 
				this.openFileDialog1.Filter="电子表格(*.xls)|*.xls|电子表格(文本文件(*.txt)|*.txt";
				this.openFileDialog1.ShowDialog();

				if(this.openFileDialog1.FileName.Trim()=="")
					throw new Exception("请按下浏览按钮找到要上传得文件，在执行此操作。");
			 
				#region 上传文件信息，验证。
				string fName = System.IO.Path.GetFileNameWithoutExtension(this.openFileDialog1.FileName );
				string fExt = System.IO.Path.GetExtension( this.openFileDialog1.FileName  ).ToLower();

				if( fExt!=".xls" && fExt!=".dbf")
					throw new Exception("请选择一个Excel文件(*.xls)或者dBASE数据表文件(*.dbf)！");

				//fPath += "U"+WebUser.No+"\\"+ fName+fExt ;
				//fPath = Server.MapPath( fPath);
				//	System.IO.Directory.CreateDirectory( Path.GetDirectoryName(fPath) );
				//	this.File1.PostedFile.SaveAs( fPath ); //如果已有，则覆盖！
				// 文件信息类。

				FileInfo fi = new FileInfo( this.openFileDialog1.FileName );
				this.statusBar1.Text ="文件名："+fName+fExt+"大小："+fi.Length.ToString("###,###,###")+"字节";

				#endregion 上传文件信息

				//string sql="SELECT ";				
				//				Attrs attrs =this.HisEn.EnMap.HisPhysicsAttrs;		
				//				foreach(Attr attr in attrs )
				//					sql+=attr.Desc+" ,";
				
				//sql=sql.Substring(0,sql.Length-1);

				string sql="";

				if(fExt==".xls")
					sql += "SELECT * FROM ["+  this.HisEn.EnDesc +"$]";
				else
					sql +="SELECT *  FROM  "+  this.HisEn.EnDesc ;

				try
				{
                    // #warning
					this.Table = DBLoad.GetTableByExt( this.openFileDialog1.FileName, sql );
				}
				catch(Exception ex)
				{
					//throw new Exception("执行查询出现错误，如果您选择的是excle 文件在sheet名称是否是["+this.HisEn.EnDesc+"]");

					throw new Exception("以下原因:\n 1)您选择的文件是否是excel 98格式以上. \n 2) excle 文件的 sheet的名称是否是["+this.HisEn.EnDesc+"]. \n系统的异常信息: "+ex.Message);

				}

				DataSet ds = new DataSet();
				ds.Tables.Add(this.Table);

				this.dataGrid1.DataSource=ds;
				this.dataGrid1.SetDataBinding(ds,this.Table.TableName);

				//System.IO.File.Delete( fPath ); // 删除这个文件。
				
			}
			catch(Exception ex)
			{
				throw new Exception("导入失败："+ ex.Message );
			}
			 
		}

		private void UILoadData_Load(object sender, System.EventArgs e)
		{
			this.Text="数据导入:"+this.HisEn.EnDesc;
			this.statusBar1.Text="请选择导入的文件";
		}
	 
	}
}
