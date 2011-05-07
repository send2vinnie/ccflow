using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;

namespace BP.Win.Controls
{
	public enum  DGColumnType 
	{
		TB,
		CheckBox,
		DDL
	}
	public class DGColumnInfo
	{
		#region 属性
		private string _columnName;
		public  string ColumnName
		{
			get
			{
				return this._columnName;
			}
			set
			{
				this._columnName = value;
			}
		}
		private string _headText;
		public  string HeadText
		{
			get
			{
				return _headText;
			}
			set
			{
				_headText = value;
			}
		}
		private DGColumnType _columnType = DGColumnType.TB;
		public  DGColumnType ColumnType
		{
			get
			{
				return this._columnType;
			}
			set
			{
				this._columnType=value;
			}
		}
		private int _defaultWidth = 100;
		public  int DefaultWidth
		{
			get
			{
				return _defaultWidth;
			}
			set
			{
				_defaultWidth = value;
			}
		}

		private string _fktbName;
		public  string FKTbName
		{
			get
			{
				return _fktbName;
			}
			set
			{
				_fktbName=value;
			}
		}
		private bool _readOnly = false;
		public  bool ReadOnly 
		{
			get{ return this._readOnly;}
			set{ this._readOnly = value;}
		}


		#endregion

		#region 构造函数

		public DGColumnInfo(string colname)
		{
			this.ColumnName = colname;
			this.HeadText = colname;
		}
		public DGColumnInfo(string colname,string headtext)
		{
			this.ColumnName = colname;
			this.HeadText = headtext;
		}
		public DGColumnInfo(string colname,DGColumnType Type)
		{
			this.ColumnName=colname;
			this.HeadText=colname;
			this.ColumnType=Type;
		}

		public DGColumnInfo(string colname,DGColumnType Type,int width)
		{
			this.ColumnName=colname;
			this.HeadText=colname;
			this.ColumnType=Type;
			this.DefaultWidth=width;
		}
		public DGColumnInfo(string colname,DGColumnType Type,int width,string tbname)
		{
			this.ColumnName=colname;
			this.HeadText=colname;
			this.ColumnType=Type;
			this.DefaultWidth=width;
			this.FKTbName=tbname;
		}
		public DGColumnInfo(string colname,DGColumnType Type,string tbname)
		{
			this.ColumnName=colname;
			this.HeadText=colname;
			this.ColumnType=Type;
			this.FKTbName=tbname;
		}
		public DGColumnInfo(string colname,DGColumnType Type,int width,string tbname,string headtext)
		{
			this.ColumnName=colname;
			this.ColumnType=Type;
			this.DefaultWidth=width;
			this.FKTbName=tbname;
			this.HeadText=headtext;
		}
		#endregion 构造函数
	}
	
	// DGColumnInfo 的集合
	public class DGColumns : CollectionBase
	{
		public DGColumns()
		{
		}
		public DGColumnInfo this[int index]
		{
			get
			{
				return this.InnerList[index] as DGColumnInfo;
			}
		}
		public void Add( DGColumnInfo col)
		{
			this.InnerList.Add(col);
		}
		public void AddRange( DGColumns cols)
		{
			this.InnerList.AddRange( cols);
		}
		public void Remove( DGColumns col)
		{
			this.InnerList.Remove( col );
		}
	}
}