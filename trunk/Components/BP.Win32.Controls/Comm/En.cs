using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using BP.En;

using BP.DA;
using BP.DTS;
using BP.Web;
using BP.Sys;
using BP.Web.Controls;
//using Cells = SourceGrid2.Cells.Real;
using BP.Win32.Controls;


namespace BP.Win32.Comm
{
	/// <summary>
	/// En 的摘要说明。
	/// </summary>
	public class En : System.Windows.Forms.Form  //BP.Win32.PageBase
	{
        public Entity HisEn = null;
		private System.Windows.Forms.ImageList imageList1;
		private BP.Win32.Controls.BPToolbar toolBar1;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.ToolBarButton Btn_New;
		private System.Windows.Forms.ToolBarButton Btn_Save;
		private System.Windows.Forms.ToolBarButton Btn_SaveAndNew;
		private System.Windows.Forms.ToolBarButton Btn_SaveAndClose;
		private System.Windows.Forms.ToolBarButton Btn_Close;
		private BP.Win32.Controls.GB gb1;
		private System.Windows.Forms.ToolBarButton Btn_Delete;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton Btn_Adjunct;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.ComponentModel.IContainer components;

		public En()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(En));
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.toolBar1 = new BP.Win32.Controls.BPToolbar();
			this.Btn_New = new System.Windows.Forms.ToolBarButton();
			this.Btn_Save = new System.Windows.Forms.ToolBarButton();
			this.Btn_Delete = new System.Windows.Forms.ToolBarButton();
			this.Btn_SaveAndNew = new System.Windows.Forms.ToolBarButton();
			this.Btn_Adjunct = new System.Windows.Forms.ToolBarButton();
			this.Btn_SaveAndClose = new System.Windows.Forms.ToolBarButton();
			this.Btn_Close = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.gb1 = new BP.Win32.Controls.GB();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.SuspendLayout();
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// toolBar1
			// 
			this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						this.Btn_New,
																						this.Btn_Save,
																						this.Btn_Delete,
																						this.Btn_SaveAndNew,
																						this.Btn_Adjunct,
																						this.Btn_SaveAndClose,
																						this.Btn_Close,
																						this.toolBarButton1});
			this.toolBar1.DropDownArrows = true;
			this.toolBar1.ImageList = this.imageList1;
			this.toolBar1.Location = new System.Drawing.Point(0, 0);
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.ShowToolTips = true;
			this.toolBar1.Size = new System.Drawing.Size(512, 28);
			this.toolBar1.TabIndex = 0;
			this.toolBar1.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
			this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
			// 
			// Btn_New
			// 
			this.Btn_New.ImageIndex = 0;
			this.Btn_New.Text = "新建";
			// 
			// Btn_Save
			// 
			this.Btn_Save.ImageIndex = 1;
			this.Btn_Save.Text = "保存";
			// 
			// Btn_Delete
			// 
			this.Btn_Delete.ImageIndex = 5;
			this.Btn_Delete.Text = "删除";
			// 
			// Btn_SaveAndNew
			// 
			this.Btn_SaveAndNew.ImageIndex = 2;
			this.Btn_SaveAndNew.Text = "保存并新建";
			this.Btn_SaveAndNew.Visible = false;
			// 
			// Btn_Adjunct
			// 
			this.Btn_Adjunct.ImageIndex = 7;
			this.Btn_Adjunct.Text = "附件";
			// 
			// Btn_SaveAndClose
			// 
			this.Btn_SaveAndClose.ImageIndex = 3;
			this.Btn_SaveAndClose.Text = "保存并关闭";
			this.Btn_SaveAndClose.Visible = false;
			// 
			// Btn_Close
			// 
			this.Btn_Close.ImageIndex = 4;
			this.Btn_Close.Text = "关闭";
			// 
			// toolBarButton1
			// 
			this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 359);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Size = new System.Drawing.Size(512, 22);
			this.statusBar1.TabIndex = 2;
			this.statusBar1.Text = "statusBar1";
			// 
			// gb1
			// 
			this.gb1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gb1.Location = new System.Drawing.Point(0, 28);
			this.gb1.Name = "gb1";
			this.gb1.Size = new System.Drawing.Size(512, 331);
			this.gb1.TabIndex = 3;
			this.gb1.TabStop = false;
			this.gb1.Text = "属性";
			// 
			// En
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(512, 381);
			this.Controls.Add(this.gb1);
			this.Controls.Add(this.statusBar1);
			this.Controls.Add(this.toolBar1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "En";
			this.Text = "En";
			this.Load += new System.EventHandler(this.En_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void En_Load(object sender, System.EventArgs e)
		{
		}
		public bool IsReadonly=false;

		
		/// <summary>
		/// 设置权限
		/// </summary>
		public void InitPower(UAC uac)
		{
            //return;

			//UAC uac =this.HiEn.HisUAC ; 
			if (this.IsReadonly || uac.IsUpdate==false)
			{
				this.Btn_New.Visible=false;
				this.Btn_Save.Visible=false;
				this.Btn_Delete.Visible=false;
				return;
			}

			this.Btn_New.Visible=false;
			this.Btn_Save.Visible=false;
			this.Btn_Delete.Visible=false;

			if (uac.IsInsert)
				this.Btn_New.Visible=true;
			
			if (uac.IsUpdate)
				this.Btn_Save.Visible=true;
			if (uac.IsDelete)
				this.Btn_Delete.Visible=true;

			if (this.HisEn.EnMap.Attrs.Contains("MyFileExt"))
			{
				this.Btn_Save.Visible=true;
			}
		}
		public void InitRefFunc()
		{
			Map map = this.HisEn.EnMap ;
            if (this.HisEn.IsEmpty)
			{
				/* 如果是一个没有保存在实体。*/
				foreach(RefMethod rm in map.HisRefMethods)
				{
					this.toolBar1.RemoveBtnByText(rm.Title);
				}
				return;
			}

			foreach(RefMethod rm in map.HisRefMethods)
			{
				BPToolbarBtn btn = new BPToolbarBtn("Btn_RM"+rm.Index,rm.Title);
				btn.ImageIndex=6;
				this.toolBar1.AddBtn( btn );
			}
		}

		public void Bind2_del(Entity en)
		{
            this.HisEn = en;
			this.InitPower(en.HisUAC); // 设置权限。
			this.InitRefFunc(); // 。

            this.Text = this.HisEn.EnDesc; 
			this.gb1.Controls.Clear();

			Map map = en.EnMap;
			int colIdx=0;
			int rowIdx=0;
			object val=null;
			int lab_x=10;
			int y=10;
			int lab_h=20;
			int lab_w=100;
			int docBox= 70;

			foreach(Attr attr in map.Attrs)
			{
				if (attr.UIVisible==false)
					continue;  /*如果不可见*/
				if (attr.MyFieldType==FieldType.RefText)
					continue; /*　如果是相关功能。　*/

				Label lab = new Label();
				lab.Text=attr.Desc+"：";
				lab.TextAlign = ContentAlignment.MiddleRight;
				lab.Location = new System.Drawing.Point( lab_x, y);
				lab.Size = new System.Drawing.Size(lab_w, lab_h);
				lab.AutoSize = false;
				lab.Name="lab_"+attr.Key;
				lab.BringToFront();
				this.gb1.Controls.Add(lab);
				val = en.GetValByKey(attr.Key);
				 
				switch(attr.UIContralType)
				{
					case UIContralType.TB:
						TB tb = new TB();
						tb.Name ="TB_"+ attr.Key ;
						tb.Size = new Size(this.Width -lab.Width -100 ,  lab_h );
						tb.Location = new System.Drawing.Point( lab_x+lab.Width, y) ;
						tb.ReadOnly = !this.IsReadonly && attr.UIIsReadonly;

					switch(attr.MyDataType)
					{
						case DataType.AppString:
                        case DataType.AppDateTime:
						case DataType.AppDate:
							break;
						case DataType.AppRate:
						case DataType.AppMoney:
						case DataType.AppInt:
						case DataType.AppFloat:
						case DataType.AppDouble:
							tb.TextAlign=HorizontalAlignment.Right;
							break;
						default:
							break;
					}
						tb.BringToFront();

						if(  attr.UIHeight>0 )
						{
							//locLab.Offset( 0 , OffsetH*2 );
							//locTB.Offset( 0 ,  OffsetH*2 );
							tb.Height = docBox;
							tb.Multiline = true;
							tb.ScrollBars = ScrollBars.Vertical ;
							y=y+tb.Height;
						}
						else
						{
							y=y+20;
						}
                        tb.Text = HisEn.GetValStringByKey(attr.Key);	
						this.gb1.Controls.Add(tb);
						break;
					case UIContralType.DDL:

						DDL ddl = new DDL();
						ddl.Name ="DDL_"+attr.Key;
						ddl.Location =  new System.Drawing.Point( lab_x+lab.Width, y)  ;
						ddl.Size = new Size( this.Width -lab.Width -100 , lab.Height );
						if ( attr.UIIsReadonly==false || this.IsReadonly )
							ddl.Enabled =false;
						else
							ddl.Enabled =true;

                        if (attr.UIIsReadonly == false || this.IsReadonly)
                        {
                            /* readonly 的情况。 */
                            ddl.BindItem(HisEn.GetValRefTextByKey(attr.Key), this.HisEn.GetValStringByKey(attr.Key), null);
                        }
                        else
                        {
                            /*  可以编辑的情况。*/
                            if (attr.MyFieldType == FieldType.Enum || attr.MyFieldType == FieldType.PKEnum)
                            {
                                // 如果是枚举类型
                                SysEnums enu = new SysEnums(attr.UIBindKey); //,"CN",en.GetValIntByKey(attr.Key));
                                ddl.BindEns(enu);
                            }
                            else
                            {
                                Entities ens = ClassFactory.GetEns(attr.UIBindKey);
                                ens.RetrieveAll();
                                ddl.BindEns(ens, attr.UIRefKeyText, attr.UIRefKeyValue);
                            }
                        }

                        //ddl.SetSelectedVal( en.GetValStrByKey(attr.Key) ) ;

                        ddl.SelectedValue = val;
                        ddl.SelectedText = val.ToString();

						y=y+20;
						ddl.BringToFront();
						this.gb1.Controls.Add(ddl);

#warning 如何设置选择项目。

						//ddl.SetSelectedText( en.GetValRefTextByKey(attr.Key) );
						//ddl.Show();
						//ddl.SelectedValue= new ListItem(val.ToString(),en.GetValRefTextByKey(attr.Key) );
						break;
					case UIContralType.CheckBok:
						CB cb = new CB();
						cb.Name="CB_"+attr.Key ; 
						cb.Location =  new System.Drawing.Point( lab_x+lab.Width, y)  ;
						cb.Text = attr.Desc ;
						cb.Size = new Size( this.Width -lab.Width -100 , lab.Height );
					
						if ( attr.UIIsReadonly==false || this.IsReadonly )
							cb.Enabled =false;
						else
							cb.Enabled =true;
					 
						this.gb1.Controls.Add( cb );
						y=y+20;
						cb.BringToFront();
						if (val.ToString()=="1")
							cb.Checked=true;
						else
							cb.Checked=false;
						break;
					default:
						throw new Exception("没有判断的类型。");
				}

				if (colIdx==4)
				{
					colIdx=0;
					rowIdx++;
				}
			}
			/* 处理附件 */
			if (en.EnMap.Attrs.Contains("MyFileExt")==false )
				return;

			Label lab1 = new Label();
			lab1.Text="附件：";
			lab1.TextAlign = ContentAlignment.MiddleRight;
			lab1.Location = new System.Drawing.Point( lab_x, y);
			lab1.Size = new System.Drawing.Size(lab_w, lab_h);
			lab1.AutoSize = false;
			lab1.Name="lab_addddd";

			this.gb1.Controls.Add(lab1);
			TB tb1 = new TB();
			tb1.Name ="TB_MyFile" ;
			tb1.Size = new Size(this.Width -lab1.Width -100 ,  lab_h );
			tb1.Location = new System.Drawing.Point( lab_x+lab1.Width, y) ;
			tb1.ReadOnly =false;
			tb1.BringToFront();
            tb1.Text = HisEn.GetValStringByKey("MyFileName");
			this.gb1.Controls.Add(tb1);

			y=y+20;
			Btn btn = new Btn();
			btn.Text="浏览...";
			btn.Location = new System.Drawing.Point( lab_x+lab1.Width, y) ;
			btn.Name="Btn_Brow";
			btn.Click+=new EventHandler(btn_Click);
			this.gb1.Controls.Add(btn);

			Btn btn1 = new Btn();
			btn1.Text="打开";
			btn1.Location = new System.Drawing.Point( lab_x+lab1.Width+btn.Width, y) ;
			btn1.Name="Btn_Open";
			btn1.Click+=new EventHandler(btn_Click);
			this.gb1.Controls.Add(btn1);

			Btn btn2 = new Btn();
			btn2.Text="删除";
			btn2.Location = new System.Drawing.Point( lab_x+lab1.Width+btn.Width+btn1.Width, y) ;
			btn2.Name="Btn_Del";
			btn2.Click+=new EventHandler(btn_Click);
			this.gb1.Controls.Add(btn2);

			this.HisEn = en;

		}
        public void Bind2(Entity en)
        {
            this.HisEn = en;
            this.InitPower(en.HisUAC); // 设置权限。
            this.InitRefFunc(); //

            this.Text = this.HisEn.EnDesc;
            this.gb1.Controls.Clear();

            Map map = en.EnMap;
            int colIdx = 0;
            int rowIdx = 0;
            object val = null;
            int lab_x = 10;
            int y = 10;
            int lab_h = 20;
            int lab_w = 100;
            int docBox = 70;

            foreach (Attr attr in map.Attrs)
            {
                if (attr.UIVisible == false)
                    continue;  /*如果不可见*/

                if (attr.MyFieldType == FieldType.RefText)
                    continue; /*　如果是相关功能。　*/

                Label lab = new Label();
                lab.Text = attr.Desc + "：";
                lab.TextAlign = ContentAlignment.MiddleRight;
                lab.Location = new System.Drawing.Point(lab_x, y);
                lab.Size = new System.Drawing.Size(lab_w, lab_h);
                lab.AutoSize = false;
                lab.Name = "lab_" + attr.Key;
                lab.BringToFront();
                this.gb1.Controls.Add(lab);
                val = en.GetValByKey(attr.Key);

                switch (attr.UIContralType)
                {
                    case UIContralType.TB:
                        TB tb = new TB();
                        tb.Name = "TB_" + attr.Key;
                        tb.Size = new Size(this.Width - lab.Width - 100, lab_h);
                        tb.Location = new System.Drawing.Point(lab_x + lab.Width, y);
                        tb.ReadOnly = !this.IsReadonly && attr.UIIsReadonly;

                        switch (attr.MyDataType)
                        {
                            case DataType.AppString:
                            case DataType.AppDateTime:
                            case DataType.AppDate:
                                break;
                            case DataType.AppRate:
                            case DataType.AppMoney:
                            case DataType.AppInt:
                            case DataType.AppFloat:
                            case DataType.AppDouble:
                                tb.TextAlign = HorizontalAlignment.Right;
                                break;
                            default:
                                break;
                        }
                        tb.BringToFront();

                        if (attr.UIHeight > 0)
                        {
                            //locLab.Offset( 0 , OffsetH*2 );
                            //locTB.Offset( 0 ,  OffsetH*2 );
                            tb.Height = docBox;
                            tb.Multiline = true;
                            tb.ScrollBars = ScrollBars.Vertical;
                            y = y + tb.Height;
                        }
                        else
                        {
                            y = y + 20;
                        }
                        tb.Text = HisEn.GetValStringByKey(attr.Key);
                        this.gb1.Controls.Add(tb);
                        break;
                    case UIContralType.DDL:

                        System.Windows.Forms.ComboBox ddl = new ComboBox();
                        ddl.DropDownStyle = ComboBoxStyle.DropDownList;
                        ddl.Name = "DDL_" + attr.Key;
                        ddl.Location = new System.Drawing.Point(lab_x + lab.Width, y);
                        ddl.Size = new Size(this.Width - lab.Width - 100, lab.Height);
                        if (attr.UIIsReadonly == false || this.IsReadonly)
                            ddl.Enabled = false;
                        else
                            ddl.Enabled = true;

                        if (attr.UIIsReadonly == false || this.IsReadonly)
                        {
                            /* readonly 的情况。 */

                            ddl.Items.Add(new ListItem(HisEn.GetValRefTextByKey(attr.Key),
                                this.HisEn.GetValStringByKey(attr.Key)));

                            // ddl.BindItem( HisEn.GetValRefTextByKey(attr.Key), this.HisEn.GetValStringByKey(attr.Key), null);
                        }
                        else
                        {
                            int idx = 0;
                            int i = 0;

                            /*  可以编辑的情况。*/
                            if (attr.MyFieldType == FieldType.Enum || attr.MyFieldType == FieldType.PKEnum)
                            {
                                // 如果是枚举类型
                                SysEnums ses = new SysEnums(attr.UIBindKey); //,"CN",en.GetValIntByKey(attr.Key));
                                foreach (SysEnum se in ses)
                                {
                                    ListItem li = new ListItem(se.Lab, val.ToString());
                                    ddl.Items.Add(li.Value + ":" + li.Text);
                                    // ddl.Items.Add(li);

                                    if (li.Value.ToString() == val.ToString())
                                        idx = i;

                                    i++;
                                }
                                ddl.SelectedIndex = idx;
                            }
                            else
                            {
                                Entities ens = ClassFactory.GetEns(attr.UIBindKey);
                                ens.RetrieveAll();
                                foreach (Entity myen in ens)
                                {
                                    ListItem li = new ListItem(myen.GetValStrByKey(attr.UIRefKeyValue),
                                        myen.GetValStrByKey(attr.UIRefKeyText));

                                    if (li.Value.ToString() == val.ToString() )
                                        idx = i;

                                    if (li.Text == li.Value.ToString())
                                        ddl.Items.Add(li.Text);
                                    else
                                        ddl.Items.Add(li.Value + ":" + li.Text);

                                    i++;
                                }
                                ddl.SelectedIndex = idx;
                            }
                        }

                        //ddl.SetSelectedVal( en.GetValStrByKey(attr.Key) ) ;

                        //ddl.SelectedText = val.ToString();

                        y = y + 20;
                        ddl.BringToFront();
                        this.gb1.Controls.Add(ddl);

#warning 如何设置选择项目。
                        //ddl.SetSelectedText( en.GetValRefTextByKey(attr.Key) );
                        //ddl.Show();
                        //ddl.SelectedValue= new ListItem(val.ToString(),en.GetValRefTextByKey(attr.Key) );
                        break;
                    case UIContralType.CheckBok:
                        CB cb = new CB();
                        cb.Name = "CB_" + attr.Key;
                        cb.Location = new System.Drawing.Point(lab_x + lab.Width, y);
                        cb.Text = attr.Desc;
                        cb.Size = new Size(this.Width - lab.Width - 100, lab.Height);

                        if (attr.UIIsReadonly == false || this.IsReadonly)
                            cb.Enabled = false;
                        else
                            cb.Enabled = true;

                        this.gb1.Controls.Add(cb);
                        y = y + 20;
                        cb.BringToFront();
                        if (val.ToString() == "1")
                            cb.Checked = true;
                        else
                            cb.Checked = false;
                        break;
                    default:
                        throw new Exception("没有判断的类型。");
                }

                if (colIdx == 4)
                {
                    colIdx = 0;
                    rowIdx++;
                }
            }
            /* 处理附件 */
            if (en.EnMap.Attrs.Contains("MyFileExt") == false)
                return;

            Label lab1 = new Label();
            lab1.Text = "附件：";
            lab1.TextAlign = ContentAlignment.MiddleRight;
            lab1.Location = new System.Drawing.Point(lab_x, y);
            lab1.Size = new System.Drawing.Size(lab_w, lab_h);
            lab1.AutoSize = false;
            lab1.Name = "lab_addddd";

            this.gb1.Controls.Add(lab1);
            TB tb1 = new TB();
            tb1.Name = "TB_MyFile";
            tb1.Size = new Size(this.Width - lab1.Width - 100, lab_h);
            tb1.Location = new System.Drawing.Point(lab_x + lab1.Width, y);
            tb1.ReadOnly = false;
            tb1.BringToFront();
            tb1.Text = HisEn.GetValStringByKey("MyFileName");
            this.gb1.Controls.Add(tb1);

            y = y + 20;
            Btn btn = new Btn();
            btn.Text = "浏览...";
            btn.Location = new System.Drawing.Point(lab_x + lab1.Width, y);
            btn.Name = "Btn_Brow";
            btn.Click += new EventHandler(btn_Click);
            this.gb1.Controls.Add(btn);

            Btn btn1 = new Btn();
            btn1.Text = "打开";
            btn1.Location = new System.Drawing.Point(lab_x + lab1.Width + btn.Width, y);
            btn1.Name = "Btn_Open";
            btn1.Click += new EventHandler(btn_Click);
            this.gb1.Controls.Add(btn1);

            Btn btn2 = new Btn();
            btn2.Text = "删除";
            btn2.Location = new System.Drawing.Point(lab_x + lab1.Width + btn.Width + btn1.Width, y);
            btn2.Name = "Btn_Del";
            btn2.Click += new EventHandler(btn_Click);
            this.gb1.Controls.Add(btn2);

            this.HisEn = en;

        }
        public void Information(string msg)
        {
            MessageBox.Show(msg);
        }

		private void btn_Click(object sender, EventArgs e)
		{
			Btn btn = sender as Btn;
			switch(btn.Name)
			{
				case "Btn_Brow":
					if ( this.openFileDialog1.ShowDialog()!=DialogResult.OK)
						return;
					string filenmae=this.openFileDialog1.FileName;
					System.IO.FileInfo f = new System.IO.FileInfo(filenmae);
					this.HisEn.SetValByKey("MyFileExt",f.Extension.Replace(".","")) ;
					this.HisEn.SetValByKey("MyFileName",f.Name) ;
					string objFile=this.HisEn.GetValStringByKey("MyFilePath")+this.HisEn.PKVal+f.Extension;
					System.IO.File.Copy(f.FullName,objFile,true);
					this.HisEn.Save();
					this.gb1.GetTBByKey("TB_MyFile").Text=f.Name;

					this.Information("文件上传成功。");
					break;
				case "Btn_Open":
					string objFile2=this.HisEn.GetValStringByKey("MyFilePath")+this.HisEn.PKVal+"."+this.HisEn.GetValStringByKey("MyFileExt");
					//this.RunExeFile(objFile2,"文件可能不存在。");
					break;
				case "Btn_Del":
					if (PubClass.Question("您确定要删除文件？"))
					{
						string objFile1=this.HisEn.GetValStringByKey("MyFilePath")+this.HisEn.PKVal+"."+this.HisEn.GetValStringByKey("MyFileExt");
						System.IO.File.Delete(objFile1);
						this.gb1.GetTBByKey("TB_MyFile").Text="";
						this.Information("文件删除成功。");
					}
					break;
				default:
					break;
			}
		}
        public void Bind(Entity en)
        {
            //			this.HiEn = en;
            //			this.InitPower(); // 设置权限。
            //
            //			Map map = en.EnMap;
            //
            //			this.grid1.RowsCount = 0;
            //			this.grid1.ColumnsCount = 0;
            //			
            //			grid1.Redim(map.HisPhysicsAttrs.Count,4);
            //			grid1.CCS_InitRows();
            //			int colIdx=0;
            //			int rowIdx=0;
            //			object val=null;
            //			foreach(Attr attr in map.Attrs)
            //			{
            //				val = en.GetValByKey(attr.Key);
            //				switch(attr.UIContralType)
            //				{
            //					case UIContralType.TB:
            //						if (attr.UIVisible==false)
            //							continue;  /*如果不可见*/
            //						if (attr.MyFieldType==FieldType.RefText)
            //							continue; /*　如果是相关功能。　*/
            //
            //						grid1[rowIdx,colIdx] = grid1.CreateCellLab(attr.Desc);
            //						colIdx++;
            //
            //						Cells.Cell cel = this.grid1.CreateCell(val,attr.MyDataType, !attr.UIIsReadonly );
            //						cel.Tag=attr.Key;
            //						grid1[rowIdx,colIdx] = cel;
            //						colIdx++;
            //						break;
            //					case UIContralType.DDL:
            //
            //
            //						break;
            //					case UIContralType.CheckBok:
            //						break;
            //					default:
            //						throw new Exception("没有判断的类型。");
            //				}
            //
            //				if (colIdx==4)
            //				{
            //					colIdx=0;
            //					rowIdx++;
            //				}
            //			}
            //
            //			//			Cells.Cell cel1 = this.grid1.CreateCellLab("".PadLeft(this.Width,'_' )) ;
            //			//			cel1.ColumnSpan = 4;
            //			//			grid1[rowIdx,0] = cel1;
            //
            //			grid1.AutoSizeAll();
        }
		 
		public void Do(string text)
		{
            switch (text)
            {
                case "保存":
                    this.DoSave();
                    this.InitRefFunc();
                    break;
                case "新建":
                    this.DoNew();
                    this.InitRefFunc();
                    break;
                case "附件":
                    if (this.HisEn.IsExits == false)
                    {
                        /*如果不存在*/
                        PubClass.Warning("需要您保存记录后才能上传文件。");
                        return;
                    }
                    if (this.HisEn.GetValStringByKey("MyFileExt") != "")
                    {
                        string filename = this.HisEn.GetValStringByKey("MyFileName");
                        string fileext = this.HisEn.GetValStringByKey("MyFileExt");

                        //string msg="现有文件["+filename+"."+fileext+"]：\n 1、如果打开请选择是。\n2、如果删除请";


                        //
                        //
                        //						
                        //						DialogResult  dr= MessageBox.Show(msg,"请选择",
                        //							MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question,MessageBoxDefaultButton.
                        //							Button2,MessageBoxOptions.DefaultDesktopOnly);
                        //
                        //
                        //
                        //
                        //						/*如果文件存在*/
                        //						if (this.Question("您确定删除文件！"))
                        //						{
                        //
                        //						}
                    }


                    break;
                case "删除":
                    if (PubClass.Question("您确定要删除当前的记录吗？"))
                    {
                        this.HisEn.Delete();
                        this.DoNew();
                    }

                    this.InitRefFunc();
                    break;
                case "关闭":
                    this.Close();
                    break;
                default:
                    this.DoRefMethod(text);
                    break;
                //throw new Exception( text) ;
            }
		}
		public void DoRefMethod(string text)
		{
            Map map = this.HisEn.EnMap; 
			foreach(RefMethod rm in map.HisRefMethods)
			{
				if (rm.Title!=text)
					continue ;

				if (rm.Warning!=null)
                    if (PubClass.Question(rm.Warning) == false)
						return ;

                rm.PKVal = this.HisEn.PKVal;
				object obj = rm.Do(null);
                if (obj == null)
                    return;


				if (obj.GetType()==typeof(string))
				{
                    MessageBox.Show(obj.ToString(), rm.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
				}
			}
		}
		/// <summary>
		/// 新建
		/// </summary>
        public void DoNew()
        {
            Entity en = this.HisEn.CreateInstance();
            en.ResetDefaultVal();

            if (en.EnMap.Attrs.Contains("No"))
            {
                Attr attr = en.EnMap.GetAttrByKey("No");
                if (attr.UIIsReadonly || en.EnMap.IsAutoGenerNo)
                {
                    if (en.GetValStringByKey("No") == "")
                    {
                        en.SetValByKey("No", en.GenerNewNoByKey("No"));
                        string val = SystemConfig.GetConfigXmlEns(ConfigKeyEns.IsInsertBeforeNew, en.ToString());
                        if (val == "1")
                        {
                            //CurrEn.SetValByKey("No",dr[attr.Key]);
                            en.Insert();
                        }
                    }
                }
            }


            //Attrs attrs = this.HisEn.EnMap.Attrs;
            //foreach (Attr attr in attrs)
            //{
            //    if (attr.MyFieldType == FieldType.Enum
            //        || attr.MyFieldType == FieldType.PKEnum
            //        || attr.MyFieldType == FieldType.FK
            //        || attr.MyFieldType == FieldType.PKFK)
            //    {
            //        en.SetValByKey(attr.Key, this.HisEn.GetValStringByKey(attr.Key));
            //    }
            //}
            this.HisEn = en;
            this.Bind2(en);
        }
		/// <summary>
		/// 保存
		/// </summary>
		public void DoSave()
		{
            try
            {
                Map map = this.HisEn.EnMap;
                foreach (Attr attr in map.Attrs)
                {
                    if (attr.UIVisible == false)
                        continue;  /*如果不可见*/
                    if (attr.MyFieldType == FieldType.RefText)
                        continue; /*　如果是相关功能。　*/

                    object val = null;
                    switch (attr.UIContralType)
                    {
                        case UIContralType.CheckBok:
                            if (this.gb1.GetCBByKey("CB_" + attr.Key).Checked)
                                val = 1;
                            else
                                val = 0;
                            break;
                        case UIContralType.DDL:
                            val = this.gb1.GetComboBoxByKey("DDL_" + attr.Key).SelectedItem.ToString() ;
                            if (val.ToString().Contains(":"))
                            {
                                val = val.ToString().Substring(0, val.ToString().IndexOf(':')   );
                            }
                            break;
                        case UIContralType.TB:
                            val = this.gb1.GetTBByKey("TB_" + attr.Key).Text;
                            break;
                        default:
                            break;
                    }
                    this.HisEn.SetValByKey(attr.Key, val);
                }

                this.HisEn.Save();
                this.statusBar1.Text = "更新成功@" + DateTime.Now.ToString("hh:mm");
            }
            catch (Exception ex)
            {
                throw ex;
            }
		}
		 

		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			this.Do(e.Button.Text);		
		}

		private void groupBox1_Enter(object sender, System.EventArgs e)
		{
		
		}

		
	}
}
