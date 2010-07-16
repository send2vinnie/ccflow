using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using BP.DA;

using BP.En;
using BP.Sys;
using BP.Win32.Controls;
using BP.Port ; 
using BP.Web; 

namespace BP.Win32.Controls
{
	/// <summary>
	/// UIEnsRelation 的摘要说明。
	/// </summary>
	public class UIEnsRelation : BP.Win32.PageBase
	{
		private System.ComponentModel.IContainer components;

		public UIEnsRelation()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(UIEnsRelation));
			this.toolBarButton_Save = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton_Cancel = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton_Close = new System.Windows.Forms.ToolBarButton();
			this.bpToolbar1 = new BP.Win32.Controls.BPToolbar();
			this.tc1 = new BP.Win32.Controls.TC();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// toolBarButton_Save
			// 
			this.toolBarButton_Save.ImageIndex = 0;
			this.toolBarButton_Save.Tag = "Save";
			this.toolBarButton_Save.Text = "保存";
			// 
			// toolBarButton_Cancel
			// 
			this.toolBarButton_Cancel.ImageIndex = 1;
			this.toolBarButton_Cancel.Tag = "Cancel";
			this.toolBarButton_Cancel.Text = "取消";
			// 
			// toolBarButton_Close
			// 
			this.toolBarButton_Close.ImageIndex = 2;
			this.toolBarButton_Close.Tag = "Close";
			this.toolBarButton_Close.Text = "关闭";
			// 
			// bpToolbar1
			// 
			this.bpToolbar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						  this.toolBarButton_Save,
																						  this.toolBarButton_Cancel,
																						  this.toolBarButton_Close});
			this.bpToolbar1.ButtonSize = new System.Drawing.Size(31, 35);
			this.bpToolbar1.DropDownArrows = true;
			this.bpToolbar1.ImageList = this.imageList1;
			this.bpToolbar1.Location = new System.Drawing.Point(0, 0);
			this.bpToolbar1.Name = "bpToolbar1";
			this.bpToolbar1.ShowToolTips = true;
			this.bpToolbar1.Size = new System.Drawing.Size(512, 41);
			this.bpToolbar1.TabIndex = 1;
			this.bpToolbar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.bpToolbar1_ButtonClick);
			// 
			// tc1
			// 
			this.tc1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tc1.Location = new System.Drawing.Point(0, 41);
			this.tc1.Name = "tc1";
			this.tc1.SelectedIndex = 0;
			this.tc1.Size = new System.Drawing.Size(512, 324);
			this.tc1.TabIndex = 2;
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// UIEnsRelation
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(512, 365);
			this.Controls.Add(this.tc1);
			this.Controls.Add(this.bpToolbar1);
			this.MaximizeBox = false;
			this.Name = "UIEnsRelation";
			this.Text = "UIEnsRelation";
			this.Load += new System.EventHandler(this.UIEnsRelation_Load);
			this.ResumeLayout(false);

		}
		#endregion

		public void BindEn(Entity myen, bool isReadonly)
		{
			this.HisEn = myen;
			this.IsReadonly =isReadonly ;		
		}		
		private void UIEnsRelation_Load(object sender, System.EventArgs e)
		{
			string ql=this.HisEn.PKVal.ToString();
		//	Emp emp=new Emp((int(ql));//

//			QueryObject qo =new QueryObject(this);
//			qo.AddWhere("OID",this.No) ; 
//			qo.DoQuery();
			this.Text= this.HisEn.EnDesc+" -- 关系维护"+ "["+this.HisEn.PKVal.ToString()+"]" ; // ["+ tree1.SelectedNode.Text +"]";
			this.tc1.Width = this.Width ;	
			this.tc1.Height  = this.Height ;	

			#region 加入他的明细
			this.tc1.TabPages.Clear();			
			EnDtls enDtls= this.HisEn.EnMap.Dtls;
			if ( enDtls.Count > 0 )
			{								
				foreach(EnDtl enDtl in enDtls)
				{	 
					TabPage tp =new TabPage();
					tp.Width=this.Width ;
					tp.Height=this.Height ;

					tp.Text = enDtl.Desc ;
					tp.Tag=enDtl;

					DG dg =new DG();
					dg.Name=enDtl.EnsName;
					dg.Dock=DockStyle.Fill;

					dg.Width = tp.Width ;
					dg.Height  = tp.Height ;

                    Entities dtls = ClassFactory.GetEns(enDtl.EnsName);
					QueryObject qo = new QueryObject(dtls);
					qo.AddWhere(enDtl.RefKey, 1);
					qo.DoQuery();
					dg.BindEnsThisOnly(dtls,false,true);

					tp.Controls.Add( dg );
					this.tc1.TabPages.Add(tp);					
				}
			}
			#endregion

			#region 加入一对多的实体编辑
			AttrsOfOneVSM oneVsM= this.HisEn.EnMap.AttrsOfOneVSM;
			if ( oneVsM.Count > 0 )
			{
				foreach(AttrOfOneVSM vsM in oneVsM)
				{
					TabPage tp =new TabPage();
					tp.Width=this.Width -10 ;
					tp.Height=this.Height-85 ;
					tp.Text = vsM.Desc ;
					tp.Tag=vsM;
					
					Tree tree = new Tree();
					tree.Name = vsM.Desc;
				
					tree.Width = tp.Width ;
					tree.Height  = tp.Height ;
					

					tp.Controls.Add( tree );
					this.tc1.TabPages.Add(tp);
                  
					#region 树	tree
					//attr.AttrOfMValue
					Entities ensOfM = vsM.EnsOfM;
					ensOfM.RetrieveAll();
					tree.BindEns(ensOfM,vsM.AttrOfMText,vsM.AttrOfMValue,true) ;		 
 
					Entities ensOfMM = vsM.EnsOfMM;
					QueryObject qo = new QueryObject(ensOfMM);
					qo.AddWhere(vsM.AttrOfOneInMM,this.HisEn.PKVal);
					qo.DoQuery();

					tree.SetChecked(ensOfMM,vsM.AttrOfMInMM) ; 

					tree.Dock=DockStyle.Fill;

					
					 
					#endregion					 

				}
			}
			#endregion
		}		

		 #region 变量
		/// <summary>
		/// HisEn
		/// </summary>
		private Entity _HisEn=null;
		private System.Windows.Forms.ToolBarButton toolBarButton_Save;
		private System.Windows.Forms.ToolBarButton toolBarButton_Cancel;
		private System.Windows.Forms.ToolBarButton toolBarButton_Close;
		private BP.Win32.Controls.BPToolbar bpToolbar1;
		private BP.Win32.Controls.TC tc1;
		private System.Windows.Forms.ImageList imageList1;	 
		/// <summary>
		/// HisEn
		/// </summary>
		public new Entity  HisEn
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
		private bool IsReadonly=false;	 


		#endregion 

		 #region 
		private void bpToolbar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
		
			string msg="";			
			try
			{
				switch(e.Button.Tag.ToString())
				{
					
					case "Save":
						this.Btn_Save();
						break;
					case "Cancel":
						this.Btn_Cancel();
						break;				
					case "Close":
						this.Close();
						break;					 
					default:
						throw new Exception("error tag = "+e.Button.Tag.ToString() ); 						
				}
			}
			catch(Exception ex)
			{
				this.Alert(msg+ex.Message);
				 
			}
		}


		/// <summary>
		/// 保存
		/// </summary>
		private void Btn_Save()
		{
			
			foreach(System.Windows.Forms.TabPage tp in this.tc1.TabPages)
			{
				string str=tp.Tag.ToString();
				if (str=="BP.En.AttrOfOneVSM")
				{
					/* 如果 */					
					
					AttrOfOneVSM attr = (BP.En.AttrOfOneVSM)tp.Tag;
					Entities ensOfMM = attr.EnsOfMM ;
					QueryObject qo = new QueryObject(ensOfMM);
					qo.AddWhere(attr.AttrOfOneInMM,this.HisEn.PKVal);
					qo.DoQuery();
					ensOfMM.Delete();

					Tree tree=(Tree)tp.Controls[0];
					Entities selectedEns = tree.GetCurrentSelectedEns();

					foreach(Entity en in  selectedEns )
					{
						Entity en1 =ensOfMM.GetNewEntity;
						en1.SetValByKey(attr.AttrOfOneInMM,this.HisEn.PKVal);
						en1.SetValByKey(attr.AttrOfMInMM,en.PKVal );
						en1.Insert();
					}
				}
			}
			//			BP.PubClass.Alert("保存成功");
			System.Windows.Forms.MessageBox.Show("全部保存成功！","保存成功");		
		}
		/// <summary>
		/// 取消
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Btn_Cancel()
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
		#endregion 

	}

}
