using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using BP.Win.Controls;
using BP.WF;
using BP.Sys;
using BP.DA;
using BP.En;

namespace BP.Win.WF
{
	/// <summary>
	///  
	/// 用于处理对单个实体属性的编辑
	/// </summary>
	public class FrmAttr : FrmAttrBase // WFForm
	{
		private System.ComponentModel.IContainer components = null;

		#region 构造函数 基本属性
		public FrmAttr()
		{
			InitializeComponent();
		}
		
		#endregion 构造函数 基本属性

		#region 打开
		/// <summary>
		/// 
		/// </summary>
		/// <param name="title">标题</param>
		/// <param name="en">要编辑的Entity</param>
		/// <param name="allowEditImportantAttr">是否允许编辑重要属性[默认不可编辑的属性]</param>
		public DialogResult ShowAttr(string title , Entity en ,bool allowEditImportantAttr)
		{
			this.Text = title;
			this.AllowEditImportantAttr = allowEditImportantAttr;
			this.BindData(en);
			return this.ShowDialog();
		}
		#endregion 打开


		#region 数据绑定 ，保存
		protected Entity HisEntity =null;
		public override void BindData()
		{
			this.BindData( this.HisEntity );
		}
        public void BindData(Entity en)
        {
            this.ClearCtrl();

            this.HisEntity = en;
            if (en == null)
                return;

            Map map = en.EnMap;

            int OffsetH = 24;
            int tbWidth = 300;
            Point locLab = new Point(10, OffsetH - 12);
            Point locTB = new Point(187, OffsetH - 4 - 12);

            foreach (Attr att in map.Attrs)
            {
                if (att.MyFieldType == FieldType.RefText && (att.MyFieldType != FieldType.Enum || att.MyFieldType != FieldType.PKEnum))
                    continue;

                switch (att.UIContralType)
                {
                    case UIContralType.CheckBok:
                    case UIContralType.RadioBtn:
                    case UIContralType.TB:
                        if (att.UIVisible == false)
                            continue;
                        break;
                    default:
                        break;
                }

                Lab lab = new Lab();
                lab.AutoSize = true;
                lab.Name = "lab" + att.Desc;
                //lab.Text = att.Desc + "["+att.Field+"]";
                lab.Text = att.Desc;
               // lab.TextAlign = ContentAlignment.MiddleRight;


                if (lab.Width > locTB.X)
                    this.toolTip1.SetToolTip(lab, lab.Text);
                lab.Location = locLab;
                lab.TextAlign = ContentAlignment.MiddleRight;

                this.AddBoxControl(lab);

                if (att.UIBindKey != null && att.UIContralType == UIContralType.DDL && att.MyDataType != DataType.AppBoolean)
                {
                    if (this.AllowEditImportantAttr || !att.UIIsReadonly)
                    {
                        #region DDL
                        DDL ddl = new DDL();
                        ddl.DropDownStyle = ComboBoxStyle.DropDownList;
                        ddl.Name = att.Field;
                        ddl.Location = locTB;
                        ddl.Size = new Size(tbWidth, 22);
                        //ddl.Enabled = ;
                        this.AddBoxControl(ddl);
                        ddl.BringToFront();
                        try
                        {
                            if (att.UIBindKey == "BP.Port.Emps")
                            {
                                BP.Port.Emps ems = new BP.Port.Emps();
                                ems.RetrieveAll(10000);
                                ddl.BindData(ems, "OID", "Name");
                            }
                            else
                                ddl.BindEntitiesNoName(att.UIBindKey);
                        }
                        catch
                        {
                            SysEnums ens = new SysEnums(att.UIBindKey);
                            if (ens.Count > 0)
                                ddl.BindData(ens, SysEnumAttr.IntKey, SysEnumAttr.Lab);
                        }
                        object val = this.HisEntity.GetValByKey(att.Key);
                        ddl.SelectedIndex = ddl.GetIndexByValue(val);
                        #endregion DDL
                    }
                    else
                    {
                        TB tb = new TB();
                        tb.Name = att.Field;
                        tb.Size = new Size(tbWidth, 22);
                        tb.Location = locTB;
                        this.AddBoxControl(tb);
                        tb.BringToFront();
                        tb.ReadOnly = true;
                        tb.Text = this.HisEntity.GetValStringByKey(att.Key + "Text");
                        //	tb.Text +="["+ this.HisEntity.GetValStringByKey(att.Key) +"]";

                    }
                }
                else if (att.UIContralType == UIContralType.CheckBok || att.MyDataType == DataType.AppBoolean)
                {
                    #region boolean
                    DDL ddl = new DDL();
                    ddl.DropDownStyle = ComboBoxStyle.DropDownList;
                    ddl.Name = att.Field;
                    ddl.Location = locTB;
                    ddl.Size = new Size(tbWidth, 22);
                    ddl.Enabled = this.AllowEditImportantAttr || !att.UIIsReadonly;
                    this.AddBoxControl(ddl);
                    ddl.BringToFront();

                    ListItems its = new ListItems();
                    its.Add(new ListItem(0, "否"));
                    its.Add(new ListItem(1, "是"));
                    ddl.BindDataListItems(its);

                    object val = this.HisEntity.GetValByKey(att.Key);
                    ddl.SelectedIndex = ddl.GetIndexByValue(val);
                    #endregion boolean
                }
                else
                {
                    #region TB
                    TB tb = new TB();
                    tb.Name = att.Field;
                    tb.Size = new Size(tbWidth, 22);
                    tb.Location = locTB;
                    tb.ReadOnly = !this.AllowEditImportantAttr && att.UIIsReadonly;
                    this.AddBoxControl(tb);
                    tb.BringToFront();

                    if (att.UIContralType == UIContralType.TB && att.UIHeight > 0)
                    {
                        locLab.Offset(0, OffsetH * 2);
                        locTB.Offset(0, OffsetH * 2);
                        tb.Height = 66;
                        tb.Multiline = true;
                        tb.ScrollBars = ScrollBars.Vertical;
                    }
                    //tb.DataBindings.Add("Text" ,this.HisEntity ,att.Key);
                    tb.Text = this.HisEntity.GetValStringByKey(att.Key);
                    #endregion TB
                }

                locLab.Offset(0, OffsetH);
                locTB.Offset(0, OffsetH);
            }

            int h = locTB.Y;
            if (h > 400)
            {
                h = 400;
                locTB.Offset(20, 0);
            }
            //pan1.Size =  new Size( locTB.X + tbWidth+8 ,locTB.Y);
            this.BoxSize = new Size(locTB.X + tbWidth + 8, h);
        }
		protected override void EndEdit()
		{
            foreach (Control con in this.PanBox.Controls)
            {
                try
                {
                    DDL ddl = con as DDL;
                    if (ddl != null && ddl.Enabled)
                    {
                        this.HisEntity.SetValByKey(ddl.Name, ddl.SelectedValue);
                    }
                    else
                    {
                        TB tb = con as TB;
                        if (tb != null && tb.Enabled && !tb.ReadOnly)
                        {
                            this.HisEntity.SetValByKey(tb.Name, tb.Text);
                        }
                    }
                }
                catch
                {
                    continue;
                }
            }
		}
        public override bool Save()
        {
            if (this.HisEntity != null)
            {
                this.EndEdit();
                this.HisEntity.Save();
                this.HisEntity.RetrieveFromDBSources();
            }
            this.BindData(  this.HisEntity );
            return true;
        }
		#endregion 数据绑定 ，保存

		#region Windows 窗体设计器生成的代码
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
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// FrmAttr
			// 
			this.AcceptButton = null;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(322, 207);
			this.Name = "FrmAttr";
			this.Load += new System.EventHandler(this.FrmAttr_Load);

		}
		#endregion

		private void FrmAttr_Load(object sender, System.EventArgs e)
		{
		
		}

		
	}
}
