using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BP.Web.Controls;
using BP.Sys;
using BP.DTS;
//using BP.DataIO.En;

namespace BP.Web.GS.Comm.Sys
{
	/// <summary>
	/// DTS 的摘要说明。
	/// </summary>
	public partial class DTS : PageBase
	{
		protected BP.Web.Controls.ToolbarDDL DDL_UserType
		{
			get
			{
				return this.BPToolBar1.GetDDLByKey("DDL_UserType");
			}
		}

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
		this.Label1.Text = this.GenerLabelStr(  "数据调度");
			if (this.IsPostBack==false)
			{
				BP.DTS.SysDTSs.InitDataIOEns();

				this.BPToolBar1.AddLab("lab1","选择用户类型");
				this.BPToolBar1.AddDDL("DDL_UserType",true);
				this.DDL_UserType.BindSysEnum("UserType",false,BP.Web.Controls.AddAllLocation.None);

				//this.DDL_RunType.AutoPostBack=true;
				//this.BPToolBar1.AddBtn(NamesOfBtn.View,"查看数据源");
				//this.BPToolBar1.AddBtn(NamesOfBtn.Confirm,"查看目地数据");
				//this.BPToolBar1.AddBtn(NamesOfBtn.Delete,"删除目地数据");

				this.BPToolBar1.AddBtn(NamesOfBtn.DataIO,"执行");
				//this.BPToolBar1.AddBtn(NamesOfBtn.Confirm,"执行默认设置");
				//this.BPToolBar1.AddBtn(NamesOfBtn.DTS,"清空方式导入");

				this.BPToolBar1.AddBtn(NamesOfBtn.SelectAll);
				this.BPToolBar1.AddBtn(NamesOfBtn.SelectNone);
				//this.BPToolBar1.AddBtn(NamesOfBtn.Help);

				if (WebUser.HisUserType==UserType.User)
				{
					this.DDL_UserType.Items.RemoveAt(0);
					this.DDL_UserType.Items.RemoveAt(0);
				}
				else if (WebUser.HisUserType==UserType.AppAdmin)
				{
					this.DDL_UserType.Items.RemoveAt(0);
				}

				this.Bind();
			}

			this.BPToolBar1.ButtonClick+=new EventHandler(BPToolBar1_ButtonClick);
			this.DDL_UserType.SelectedIndexChanged+=new EventHandler(DDL_UserType_SelectedIndexChanged);

		}
		/// <summary>
		/// 邦定
		/// </summary>
		public void Bind()
		{
			this.CBL1.Items.Clear();

			#region  bind it .
			ArrayList al =BP.DA.ClassFactory.GetObjects("BP.DTS.DataIOEn");
			int i=1;
			int selectval=this.DDL_UserType.SelectedItemIntVal ; 
			// 初始化系统实体。
			foreach(DataIOEn en in al)
			{
				int ii=(int)en.HisRunTimeType;

				if ( (int)en.HisUserType!=selectval)
					continue;

				if (en.Enable==false)
					continue;

				this.CBL1.Items.Add(new ListItem(i.ToString()+","+en.Title+"@说明:"+en.Note,en.ToString() ));
				i++;
			}
			#endregion
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		private void BPToolBar1_ButtonClick(object sender, EventArgs e)
		{
			BP.Web.Controls.ToolbarBtn btn =( ToolbarBtn)sender;

			DateTime dtfrom=DateTime.Now;
			this.Label1.Text="开始执行时间为:"+dtfrom.ToString("hh:mm:ss");
			switch(btn.ID)
			{
				case NamesOfBtn.DataIO:
					foreach(ListItem li in this.CBL1.Items)
					{
						if (li.Selected==false)
							continue;
						DataIOEn en =(DataIOEn)BP.DA.ClassFactory.GetDataIOEn(li.Value);
						en.Do();
					}
					break;
				case NamesOfBtn.SelectNone:
					this.CBL1.SelectNone();
					break;
				case NamesOfBtn.SelectAll:
					this.CBL1.SelectAll();
					break;
				case NamesOfBtn.Help:
					break;
				case NamesOfBtn.Delete:
					foreach(ListItem li in this.CBL1.Items)
					{
						if (li.Selected==false)
							continue;
                        DataIOEn en = (DataIOEn)BP.DA.ClassFactory.GetDataIOEn(li.Value);
						en.DeleteObjData();
						this.ResponseWriteBlueMsg("删除完毕:"+en.Title);
						//this.ShowDataTable(dt);
						break;
					}

					break;

				case NamesOfBtn.Confirm:
					foreach(ListItem li in this.CBL1.Items)
					{
						if (li.Selected==false)
							continue;
                        DataIOEn en = (DataIOEn)BP.DA.ClassFactory.GetDataIOEn(li.Value);
						DataTable dt= en.GetToDataTable();
						this.ShowDataTable(dt);
						break;
					}
					break;

				case NamesOfBtn.View:
					foreach(ListItem li in this.CBL1.Items)
					{
						if (li.Selected==false)
							continue;
                        DataIOEn en = (DataIOEn)BP.DA.ClassFactory.GetDataIOEn(li.Value);
						DataTable dt= en.GetFromDataTable();
						this.ShowDataTable(dt);
						break;
					}
					break;

				case "Btn_Obj":
					break;
				default:
					break;
			}

			DateTime dtto=DateTime.Now;
			//TimeSpan ts=DateTim.
            this.Label1.Text += "结束时间为:" + dtto.ToString("hh:mm:ss");
		}

		private void DDL_UserType_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.Bind();
		}
	}
}
