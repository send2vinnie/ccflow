using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Collections;
using System.Reflection;
using System.ComponentModel;
using BP.Web.Controls;
using BP.DA;
using BP.En;

using BP.Web;
using BP.Sys;
namespace BP.Win32.Controls
{
	/// <summary>
	/// 下拉列表(DropDownList)
	/// </summary>
	[ToolboxBitmap(typeof(System.Windows.Forms.ComboBox))]
	public class DDL : System.Windows.Forms.ComboBox//组合框
	{
		#region 运行属性
		public string selectVal=null;
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint (e);
			if (selectVal==null)
			{
			}
			else
			{
				this.SetSelectedVal(selectVal);
			}
		}

		#endregion 

		#region 公共属性
		/// <summary>
		/// all 放置的位置
		/// </summary>
		private AddAllLocation _AddAllLocation=AddAllLocation.None;
		/// <summary>
		/// -=全部=-项的放置的位置
		/// </summary>
		public AddAllLocation AddAllLocation
		{
			get
			{
				return _AddAllLocation;
			}
			set
			{
				_AddAllLocation=value;
			}
		}
		/// <summary>
		/// 列表集合
		/// </summary>
		private  ListItems  _HisListItems= new ListItems();
		/// <summary>
		/// 列表集合
		/// </summary>
		public ListItems HisListItems
		{
			get
			{
				return _HisListItems;
			}
			set
			{
				_HisListItems=value;
			}
		}
		#endregion 

		#region 私有方法
		// <summary>
		/// 加载=全部=-项
		/// </summary>
		private void beforeBind()
		{
			//this.Items.Clear();
			if (AddAllLocation==AddAllLocation.Top || AddAllLocation==AddAllLocation.TopAndEnd )
			{
				this.AddItem("=全部=", "all");
			}
		}
		/// <summary>
		/// 加载=全部=-项
		/// </summary>
		private void afterBind()
		{
			if (AddAllLocation==AddAllLocation.End || AddAllLocation==AddAllLocation.TopAndEnd )
			{
				this.AddItem("-=全部=-","all");
			}
			BindDataListItems();
		}
		#endregion

		#region  构造方法
		public DDL()
		{
			this.DropDownWidth=200;
			this.MaxDropDownItems=20;
			this.DropDownStyle = ComboBoxStyle.DropDownList;
		} 
		#endregion

		#region 私有方法

		#endregion 私有方法

		#region 公共方法
		/// <summary>
		/// 绑定数据列表元素
		/// </summary>
		/// <param name="its"></param>
		public void BindDataListItems1( ListItems its)
		{
		//	ItemTagTable.Clear();
			this.DataSource = null ;
			this.Items.Clear();

			DataTable tb = new DataTable( "ListItems");
			tb.Columns.Add( "Value" ,typeof( string ));
			tb.Columns.Add( "Text" ,typeof( string ));
			foreach( ListItem li in its)
			{
				tb.Rows.Add( new object[]{li.Value ,li.Text});
				//ItemTagTable.Add( li.Value , li.Tag );
			}
			this.DisplayMember = "Text";
			this.ValueMember = "Value";
			tb.DefaultView.Sort = ValueMember;

			this.DataSource = tb.DefaultView;    // 在设置 ValueMember 之后
		}
		public void BindDataListItems( ListItems its)
		{
			this.Items.Clear();
			foreach(ListItem li in its)
			{
				this.Items.Add( li);
			}
			//			this.DisplayMember = "Text";
			//			this.ValueMember = "Value";
		}
		private bool _stopEvent = false ;
		/// <summary>
		/// 取得数据源
		/// </summary>
		public  DataView DVSource
		{
			get { return this.DataSource as DataView; }
		}
		/// <summary>
		/// 根据给定值获取其索引
		/// </summary>
		/// <param name="val"></param>
		/// <returns></returns>		
		public int GetIndexByValue(object val)
		{
			if(this.DVSource!=null)
			{
				if(DVSource.Sort!=this.ValueMember)
					DVSource.Sort = this.ValueMember;
				int ri = DVSource.Find(val);
				return ri;
			}
			else if(this.SelectedItem!=null)
			{
				_stopEvent = true ;
				if(val==null)
					return -1;
				int pos = -1;
				for(int i=0;i<this.Items.Count;i++)
				{
					this.SelectedIndex = i;
					if(this.SelectedValue.ToString() == val.ToString())
					{
						pos = i ;
						break;
					}
				}
				if(pos==-1)
					this.SelectedIndex = -1 ;

				_stopEvent = false ;
				this.OnSelectedValueChanged (null);
				return pos ;
			}
			return -1;
		
		}

		#endregion 公共方法

		/// <summary>
		///加载数据列表元素集合
		/// </summary>
		private void BindDataListItems()
		{
			this.DataSource = null ;

			DataTable tb = new DataTable( "ListItems");
			tb.Columns.Add( "Value" ,typeof( string ));
			tb.Columns.Add( "Text" ,typeof( string ));

			foreach( ListItem li in this.HisListItems )
			{
				tb.Rows.Add( new object[]{li.Value ,li.Text});
		//		ItemTagTable.Add( li.Value , li.Tag );
			}
			this.DisplayMember = "Text";
			this.ValueMember = "Value";
			tb.DefaultView.Sort = ValueMember;
			this.DataSource = tb.DefaultView;    // 在设置 ValueMember 之后
		}

		#region bind entity
		/// <summary>
		/// 增加Item
		/// </summary>
		/// <param name="text">文本</param>
		/// <param name="val">值</param>
		public void AddItem(string text, string val)
		{
			this.HisListItems.Add( new ListItem(val,text)) ; 
		}
		/// <summary>
		/// 当前选择的Item
		/// </summary>
		public ListItem SelectedListItem
		{
			get
			{
				return  this.HisListItems[this.SelectedIndex] ; 
			}
		}
		/// <summary>
		/// 增加Item
		/// </summary>
		/// <param name="text">文本</param>
		/// <param name="val">值</param>
		/// <param name="tag">tag</param>
		private void AddItem(string text, string val, Object tag)
		{
			this.HisListItems.Add( new ListItem(val,text)) ;
		}		
	/// <summary>
	/// 增加Item
	/// </summary>
	/// <param name="li"></param>
		private void AddItem(ListItem li)
		{
			this.HisListItems.Add( li ) ; 
		}	
		/// <summary>
		/// BindEns
		/// </summary>
		/// <param name="ens">Entities</param>
		/// <param name="text">属性text</param>
		/// <param name="val">属性val</param>
		public void BindEns(Entities ens , string text, string val)
		{
			this.beforeBind();
			foreach(Entity en in ens)
			{
				this.AddItem(en.GetValStringByKey(text),en.GetValStringByKey(val),en) ; 
			}
			this.afterBind();
		}
		/// <summary>
		/// 增加Listitems
		/// </summary>
		/// <param name="lis"></param>
		public void BindItem(ListItems lis)
		{
		
			foreach(ListItem li in lis)
			{
				this.AddItem( li ) ; 
			}		 	 
		 
		}
		/// <summary>
		/// BindEns
		/// </summary>
		/// <param name="ens">Entities</param>
		/// <param name="text">属性text</param>
		/// <param name="val">属性val</param>
		public void BindItem(  string text, string val, object obj )
		{
			this.beforeBind();			 
			this.AddItem( text,val, obj ) ;			 
			this.afterBind();
		}
		/// <summary>
		/// BindEns
		/// </summary>
		/// <param name="ens">Entities</param>
		/// <param name="text">属性text</param>
		/// <param name="val">属性val</param>
		public void BindEns(Entity en , string text, string val)
		{
			this.beforeBind();
			 
			this.AddItem(en.GetValStringByKey(text),en.GetValStringByKey(val),en) ; 
			 
			this.afterBind();
		}
		/// <summary>
		/// BindEns
		/// </summary>
		/// <param name="ens">Entities</param>
		/// <param name="text">属性text</param>
		/// <param name="val">属性val</param>
		public void BindEns(SysEnum en )
		{
			this.beforeBind();
			this.AddItem(en.Lab,en.IntKey.ToString(),en) ; 
			this.afterBind();
		}
		/// <summary>
		/// 枚举
		/// </summary>
		/// <param name="ens"></param>
		public void BindEns(Sys.SysEnums ens)
		{
			BindEns(ens,SysEnumAttr.Lab,SysEnumAttr.IntKey);
		}
		/// <summary>
		/// EntitiesNoName
		/// </summary>
		/// <param name="ens">EntitiesNoName</param>
		public void BindEns(EntitiesNoName ens)
		{ 
			BindEns(ens,"Name","No");
		}
		/// <summary>
		/// 设置选择的item
		/// </summary>
		/// <param name="selectedVal">selectedVal</param>
		public void SetSelectedVal(object selectedVal)
		{
			//			foreach(object obj in this.Items)
			//			{
			//				if (obj ==selectedVal )
			//					 
			//			}

            

			int idx =  this.HisListItems.GetIndexByItemValue(selectedVal) ;
			ListItem li = this.HisListItems[idx];
			this.SelectedItem =li ; 
		}
//		public void SetSelectedText(object text)
//		{
//			this.SelectedText =text.ToString();
//		}
//		public void SetSelectedValue(object valu)
//		{
//			this.SelectedValue =valu;
//		}

		/// <summary>
		/// 设置选择的selectedText
		/// </summary>
		/// <param name="selectedText">selectedText</param>
		public void SetSelectedText(string selectedText)
		{
			if (selectedText.Trim().Length==0)
				this.SelectedIndex=0;

			try
			{
				this.SelectedText = selectedText;
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
				 
			}

			//	SetSelectedText(selectedText);
		}
		#endregion

		#region bind 固定的枚举  data
		/// <summary>
		/// 加载年
		/// </summary>
		public void BindYear()
		{
			
			//			this.beforeBind();
			for(int i = 2000; i<  2010; i++)
			{
				Items.Add(new ListItem(i.ToString() ,i+"年")) ;
			}
			//			this.afterBind();
		}
		/// <summary>
		/// 加载月
		/// </summary>
		///<param name="Year4">年号，必须为yyyy格式</param>
		public void BindMonth(string Year4)
		{
			for(int i=1; i<=12;i++)
			{
				//				string no = Year4 +"-"+ i.ToString().PadLeft(2,'0');
				Items.Add(new ListItem(i.ToString().PadLeft(2,'0'),i+"月份")) ;
			}
		}
		/// <summary>
		/// 邦定枚举类型
		/// </summary>
		/// <param name="enumKey"></param>
        public void BindEnum(string enumKey)
        {
            this.beforeBind();
             
            SysEnums ens = new SysEnums(enumKey);
            foreach (SysEnum en in ens)
            {
               this.AddItem( en.Lab, en.IntKey.ToString() );
            }
            this.afterBind();
        }
		/// <summary>
		///加载日 
		/// </summary>
		public void BindDay(int year,int month)
		{
			
			int days = System.DateTime.DaysInMonth( year ,month);
			//			string yyyyMM = year.ToString() +"-"+ month.ToString().PadLeft(2,'0');
			for(int i=1;i<=days;i++)
			{
				//				string no = yyyyMM +"-"+ i.ToString().PadLeft(2,'0');
				//				string name = i.ToString()+"日";
				Items.Add(new ListItem(i.ToString().PadLeft(2,'0'),i+"日"));
			} 
		}

		public void BindWeek()
		{
			 
		}
		
		public void Bindhh()
		{
			 
		}
		public void Bindmm()
		{
			 
		}
		#endregion
	

		#region 重写方法
		//		protected override bool ProcessDialogKey(Keys keyData)
		//		{
		//			//base.onpro
		//			 
		//			if(keyData==Keys.Return)
		//				return base.ProcessDialogKey(Keys.Tab);
		//			else
		//				return base.ProcessDialogKey(keyData);
		//		}
		#endregion 重写方法

		#region 添加属性及方法
	
		private DDLShowType _DDLShowType = DDLShowType.None;
		public DDLShowType DDLShowType 
		{
			get { return this._DDLShowType ;} 
			set { this._DDLShowType = value ;} 
		}
		#endregion
	}
}
		
	 
	
	
 
