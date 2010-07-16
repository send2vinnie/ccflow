using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BP.Web.Controls ;

namespace BP.Win32.Controls
{
	/// <summary>
	///工具栏 
	/// </summary>
	[ToolboxBitmap(typeof(System.Windows.Forms.ToolBar))]
	public class BPToolbar : System.Windows.Forms.ToolBar
	{
		#region 自定义属性		 
//		/// <summary>
//		/// 从新设置HisBPImageList
//		/// 这个方法在AddBtn后调用它。
//		/// </summary>
//		protected void ReSetHisHisBPImageList()
//		{
//			this.ImageList.Images.Clear();
//			foreach(BPToolbarBtn btn in this.Buttons)
//			{
//				
//				this.ImageList.Images.Add( new Icon("D:\\WebApp\\WF\\images\\ToolBarIcon\\Btn\\Card.gif") );
//			}
//
//		}
		#endregion

		#region 构造
		/// <summary>
		/// 工具空间
		/// </summary>
		public BPToolbar()
		{
		}
		#endregion

		#region addbtn
		/// <summary>
		/// 
		/// </summary>
		/// <param name="text"></param>
		public void RemoveBtnByText(string text)
		{
			ToolBarButton btn1 = null;
			foreach(ToolBarButton btn in this.Buttons)
			{
				if (btn.Text== text)
				{
					btn1 = btn;
					break;
				}
			}
			 
			this.Buttons.Remove( btn1);
		}
		public bool Contains(string text)
		{
			foreach(ToolBarButton btn in this.Buttons)
			{
				if (btn.Text== text)
					return true;
			}
			return false;
		}
		/// <summary>
		///增加按钮 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="text"></param>
		public void AddBtn(string name, string text)
		{
			BPToolbarBtn btn = new BPToolbarBtn(name,text);
			this.AddBtn(btn);
		}
		public void AddBtn(BPToolbarBtn btn)
		{
			foreach(ToolBarButton btn1 in this.Buttons)
			{
				if (btn1.Text == btn.Text )
					return;
			}

			this.Buttons.Add(btn);
		}
		public void AddBtn(string name)
		{
			BPToolbarBtn btn = this._AddBtn(name);
			btn.ImageIndex =this.ImageList.Images.Count ;

			this.Buttons.Add(btn ); 

			AddImage(name) ; 

			//this.ImageList.Images.Add( new Icon("D:\\WebApp\\WF\\images\\ToolBarIcon\\Btn\\"+name+".gif" ))  ; 
		}
		/// <summary>
		/// 增加图标
		/// </summary>
		/// <param name="btnName"></param>
		public void AddImage(string btnName)
		{
			 
			
			// Be sure to use an appropriate escape sequence (such as the 
			// @) when specifying the location of the file.
			//System.Drawing.Image myImage = 	Image.FromFile(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal)+ @"\Image.gif");
			btnName=btnName.Substring(4);
			//if (this.ImageList==null)
			//	this.ImageList =this.FindForm().Controls.GetChildIndex(  
			System.Drawing.Image myImage = 	Image.FromFile("E:\\WebApp\\WF\\images\\ToolBarIcon\\Btn\\"+btnName+".gif"  );
			this.ImageList.Images.Add(myImage);
		}
		/// <summary>
		/// 增加间隔线
		/// </summary>
		/// <param name="id"></param>
		public void AddSpt(string id)
		{
			BPToolbarBtn btn = new BPToolbarBtn();
			btn.Style = ToolBarButtonStyle.Separator ; 			
			this.Buttons.Add(btn);
		}
		/// <summary>
		/// 增加单击时显示按钮
		/// </summary>
		/// <param name="name"></param>
		/// <param name="text"></param>
		public void AddDropDownBtnt(string name,string text)
		{
			BPToolbarBtn btn = new BPToolbarBtn(name,text) ; 
			btn.Style = ToolBarButtonStyle.DropDownButton ;
            this.Buttons.Add(btn);
		}

		/// <summary>
		/// 加入常用按钮
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		private  BPToolbarBtn  _AddBtn(string name)
		{
			string text="" ;
			switch(name)
			{				
				 
				case NamesOfBtn.Send:
					text="发送";
					break;
				case NamesOfBtn.Reply:
					text="回复";
					break;
				case NamesOfBtn.Forward:
					text="转发";
					break;
				case NamesOfBtn.Next:
					text="下一个";
					break;
				case NamesOfBtn.Previous:
					text="上一个";
					break;
				case NamesOfBtn.Selected:
					text="选择";
					break;
				case NamesOfBtn.Add:
					text="增加";
					break;
				case NamesOfBtn.Adjunct:
					text="附件";
					break;
				case NamesOfBtn.AllotTask:
					text="分批任务";
					break;
				case NamesOfBtn.Apply:
					text="申请";
					break;
				case NamesOfBtn.ApplyTask:
					text="申请任务";
					break;
				case NamesOfBtn.Back:
					text="后退";
					break;
				case NamesOfBtn.Card:
					text="卡片";
					break;
				case NamesOfBtn.Close:
					text="关闭";
					break;
				case NamesOfBtn.Confirm:
					text="确定";
					break;
				case NamesOfBtn.Delete:
					text="删除";
					break;
				case NamesOfBtn.Edit:
					text="编辑";
					break;
				case NamesOfBtn.EnList:
					text="列表";
					break;
				case NamesOfBtn.Cancel:
					text="取消";
					break;
				case NamesOfBtn.Export:
					text="导出";
					break;
				case NamesOfBtn.FileManager:
					text="文件管理";
					break;
				case NamesOfBtn.Help:
					text="帮助";
					break;
				case NamesOfBtn.Insert:
					text="插入";
					break;
				case NamesOfBtn.LogOut:
					text="注销";
					break;
				case NamesOfBtn.Messagers:
					text="消息";
					break;
				case NamesOfBtn.New:
					text="新建";
					break;
				case NamesOfBtn.Print:
					text="打印";
					break;
				case NamesOfBtn.Refurbish:
					text="刷新";
					break;
				case NamesOfBtn.Reomve:
					text="移除";
					break;
				case NamesOfBtn.Save:
					text="保存";
					break;
				case NamesOfBtn.SaveAndClose:
					text="保存并关闭";
					break;
				case NamesOfBtn.SaveAndNew:
					text="保存并新建";
					break;
				case NamesOfBtn.SaveAsDraft:
					text="保存草稿";
					break;
				case NamesOfBtn.Search:
					text="查找";
					break;
				case NamesOfBtn.SelectAll:
					text="选择全部";
					break;			 
				case NamesOfBtn.SelectNone:
					text="不选";
					break;
				case NamesOfBtn.View:
					text="查看";
					break;
				case NamesOfBtn.Update:
					text="更新";
					break;
				default:
					throw new Exception("@没有定义ToolBarBtn 标记 "+name);
			}	
			return  new BPToolbarBtn(name,text);
			 
		}
		#endregion
	}

	/// <summary>
	/// 工具栏按钮
	/// </summary>
	[ToolboxBitmap(typeof(System.Windows.Forms.ToolBarButton))]
	public class BPToolbarBtn : System.Windows.Forms.ToolBarButton
	{
		/// <summary>
		/// 工具空间
		/// </summary>
		public BPToolbarBtn()
		{
		}
		public BPToolbarBtn(string name,string text)
		{
			this.Text=text;
			//this.Name=name;
			
			//
		}
	}
	 
}
