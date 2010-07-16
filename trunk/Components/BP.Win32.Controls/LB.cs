using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

using BP.Web.Controls;
using BP.DA;
using BP.En;

using BP.Web;
using BP.Sys;

namespace BP.Win32.Controls
{
	[ToolboxBitmap(typeof(System.Windows.Forms.ListBox))]
	public class LB : System.Windows.Forms.ListBox
	{
		#region 私有方法
		/// <summary>
		/// beforeBind
		/// </summary>
		private void beforeBind()
		{
			this.Items.Clear();
			if (AddAllLocation==AddAllLocation.Top || AddAllLocation==AddAllLocation.TopAndEnd )
			{
				Items.Add(new ListItem("all","-=全部=-"));
				 
				 
			}
		}
		/// <summary>
		/// afterBind
		/// </summary>
		private void afterBind()
		{
			BindDataListItems();
			if (AddAllLocation==AddAllLocation.End || AddAllLocation==AddAllLocation.TopAndEnd )
			{
				Items.Add(new ListItem("all","-=全部=-"));				
			}
		}

		#endregion

		#region 运行属性
		public string SelectedValueString
		{
			get
			{
				return	this.SelectedValue.ToString();
			}
			set
			{
				SetSelectedVal(value);
			}
		}
		public int SelectedValueInt
		{
			get
			{
				return int.Parse(this.SelectedValue.ToString()) ; 
			}
			set
			{
				SetSelectedVal(value.ToString());
			}		
		}
		#endregion 

		#region 公共属性
		/// <summary>
		/// all 放置的位置
		/// </summary>
		private AddAllLocation _AddAllLocation=AddAllLocation.None;
		/// <summary>
		/// all 放置的位置
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
		/// 存放数据源的列表
		/// </summary>
		private  ListItems  _HisListItems= new ListItems();
		/// <summary>
		/// 存放数据源的列表
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

		#region 构造
		public LB()
		{ 
		}
		#endregion


		#region bind entity
		

		/// <summary>
		/// 增加Item
		/// </summary>
		/// <param name="text">text</param>
		/// <param name="val">val</param>
		public void AddItem(string text, string val)
		{
			this.HisListItems.Add( new ListItem(val,text)) ; 
		}
		/// <summary>
		/// 增加Item
		/// </summary>
		/// <param name="text">text</param>
		/// <param name="val">val</param>
		/// <param name="tag">tag</param>
		public void AddItem(string text, string val, Object tag)
		{
			this.HisListItems.Add( new ListItem(val,text)) ; 
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
		/// BindEns
		/// </summary>
		/// <param name="ens">Entities</param>
		/// <param name="text">属性text</param>
		/// <param name="val">属性val</param>
		public void BindItem(  string text, string val)
		{
			this.beforeBind();			 
			this.AddItem( text,val ) ;			 
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
		public void SetSelectedVal(string selectedVal)
		{
			this.SelectedValue = selectedVal;			 
		}
		#endregion


		#region bind
		private Hashtable ItemTagTable = new Hashtable();

		private void BindDataListItems()
		{
			this.DataSource = null;
			DataTable tb = new DataTable( "ListItems");
			tb.Columns.Add( "Value" ,typeof( string ));
			tb.Columns.Add( "Text" ,typeof( string ));
			foreach( ListItem li in this.HisListItems )
			{
				tb.Rows.Add( new object[]{li.Value ,li.Text});
				ItemTagTable.Add( li.Value , li.Tag );
			}
			this.DisplayMember = "Text";
			this.ValueMember = "Value";
			tb.DefaultView.Sort = ValueMember;
			this.DataSource = tb.DefaultView;    // 在设置 ValueMember 之后
		}
		#endregion
	}
}
