using System;
using System.Data;
using System.Collections;

namespace BP.Win.Controls
{
	/// <summary>
	/// ListItem 的摘要说明。
	/// </summary>
	public class ListItem
	{
		public ListItem()
		{
		}
		public ListItem(object Value ,string Text)
		{
			this._value = Value;
			this._text = Text;
		}
		public ListItem(object Value ,string Text ,object Tag)
		{
			this._value = Value;
			this._text = Text;
			this._tag = Tag;
		}


		private object _value = null;
		public object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}
		private string _text = null;
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = value;
			}
		}
		private object _tag = null;
		public object Tag
		{
			get
			{
				return this._tag;
			}
			set
			{
				this._tag = value;
			}
		}
	}
	public class ListItems :CollectionBase
	{
		public ListItems()
		{
		}
		public ListItems( DataTable tb )
		{
			if( tb.Columns.Count<2 )
			{
				throw new Exception( "所提供的数据不足！" );
			}
			else if( tb.Columns.Count==2 )
				foreach( DataRow row in tb.Rows )
				{
					this.Add( row[0] , row[1].ToString() , null );
				}
			else if( tb.Columns.Count==3 )
				foreach( DataRow row in tb.Rows )
				{
					this.Add( row[0] , row[1].ToString() ,row[3]);
				}
		}
		public ListItems( DataTable tb ,string val ,string text )
		{
			foreach( DataRow row in tb.Rows )
			{
				this.Add( row[val] , row[text].ToString() ,null);
			}
		}
		
		public ListItems( DataTable tb ,string val ,string text ,string tag )
		{
			foreach( DataRow row in tb.Rows )
			{
				this.Add( row[val] , row[text].ToString() ,row[tag]);
			}
		}
		public void FillTableSchema( DataTable tb )
		{
			this.Clear();
			foreach( DataColumn col in tb.Columns)
			{
				this.Add( col.ColumnName ,col.ColumnName , col );
			}
		}
		
		public DataTable ToDataTable()// DataTable tb ,string val ,string text ,string tag )
		{
			DataTable tb = new DataTable( "ListItems");
			tb.Columns.Add( "Value" ,typeof( string ));
			tb.Columns.Add( "Text" ,typeof( string ));
			tb.Columns.Add( "Tag" ,typeof( string ));
			foreach(ListItem it in this )
			{
				tb.Rows.Add( new object[]{it.Value,it.Text,it.Tag});
			}
			return tb;
		}

		public bool Contains( ListItem item )
		{
			string val = "";
			if(item.Value!=null)
				val = item.Value.ToString();
			foreach( ListItem it in this)
			{
				if( it.Value==null)
					return true;
				else if( it.Value.ToString()==val)
					return true;
			}
			return false;
		}
		public void Add( object val ,string text ,object tag )
		{
			if( val == null )
				return;
			if( GetListItem(val.ToString())==null)
			{
				ListItem item = new ListItem(val, text, tag );
				this.InnerList.Add( item );
			}
		}
		public void Add( ListItem item )
		{
			if( item.Value == null )
				return;
			if( !this.Contains( item))
				this.InnerList.Add( item );
		}
		public ListItem GetListItem( string valToStr )
		{
			foreach( ListItem it in this)
			{
				if( it.Value==null)
					return it;
				else if( it.Value.ToString()==valToStr)
					return it;
			}
			return null;
		}
	}
}
