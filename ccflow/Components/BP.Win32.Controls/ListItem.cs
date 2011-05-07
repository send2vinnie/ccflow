using System;
using System.Data;
using System.Collections;

namespace BP.Win32.Controls
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
		public ListItem(string Text ,object Value ,object Tag)
		{
			this._value = Value;
			this._text = Text;
			this._tag = Tag;
		}

		private int _index = 0;
		public int Index
		{
			get
			{
				return this._index;
			}
			set
			{
				this._index = value;
			}
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
		public ListItem this[int index]
		{			
			get
			{	
				return (ListItem)this.InnerList[index];
			}
		}
		public ListItem this[string val]
		{			
			get
			{	
				foreach(ListItem li in this.InnerList)
				{
					if (li.Value.ToString()==val)
						return li;
				}
				throw new Exception("没有找到val='"+val+"' 的Listitem.");
			}
		}
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
					this.Add(  row[1].ToString() ,row[0] , null );
				}
			else if( tb.Columns.Count==3 )
				foreach( DataRow row in tb.Rows )
				{
					this.Add(  row[1].ToString() ,row[0] ,row[3]);
				}
		}
		public ListItems( DataTable tb ,string val ,string text )
		{
			foreach( DataRow row in tb.Rows )
			{
				this.Add( row[text].ToString() , row[val] ,null);
			}
		}
		
		public ListItems( DataTable tb ,string val ,string text ,string tag )
		{
			foreach( DataRow row in tb.Rows )
			{
				this.Add( row[text].ToString() , row[val] ,row[tag]);
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
		public void Add( string text ,object val , object tag )
		{
			if( val == null )
				return;
			if( GetListItem(val.ToString())==null)
			{
				ListItem item = new ListItem(text,val,  tag );
				item.Index=this.Count;
				this.InnerList.Add( item );
			}
		}
		public void Add( ListItem item )
		{
			if( item.Value == null )
				return;

			item.Index=this.Count ;

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
		/// <summary>
		/// 取出索引值根据 val 没有找到就抛出异常.
		/// </summary>
		/// <param name="val">val</param>
		/// <returns>索引值</returns>
		public int GetIndexByItemValue( object val )
		{
			for(int i = 0; i < this.Count ; i++)
			{
				if ( this[i].Value.ToString().Trim()==val.ToString().Trim()  )
				{
					return i;
				}
			}
			throw new Exception("erro 错误：GetIndexByItemValue ="+val);
		}
		/// <summary>
		/// 取出索引值根据 文本.没有找到就抛出异常
		/// </summary>
		/// <param name="text">文本</param>
		/// <returns>索引值</returns>
		public int GetIndexByItemText( string text )
		{
			for(int i =0 ; i < this.Count ; i++)
			{
				string mytext=this[i].Text.Trim();
				if (mytext ==text.Trim())
				{
					return i;
				}
			}
			return -1;
			//return 0;
			//throw new Exception("erro text ="+text);
		}
	}
}
