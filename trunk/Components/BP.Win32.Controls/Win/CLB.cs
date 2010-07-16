using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Collections;
using System.Reflection;
using System.ComponentModel;
using BP.DA;

namespace BP.Win.Controls
{
	[ToolboxBitmap(typeof(System.Windows.Forms.CheckedListBox))]
	public class CLB : System.Windows.Forms.CheckedListBox
	{
		#region 构造函数  重写方法
		public CLB()
		{
			//this.CheckOnClick=true;
		}

		protected override bool ProcessDialogKey(Keys keyData)
		{
			if(keyData==Keys.Return)
				return base.ProcessDialogKey(Keys.Tab);
			else
				return base.ProcessDialogKey(keyData);
		}
		#endregion 

		#region 添加方法
		public void SetAllChecked(bool check)
		{
			if(!check)
				for(int i=0;i<this.CheckedItems.Count;i++)
					this.SetItemChecked(i,check);
			else 
				for(int i=0;i<this.Items.Count;i++)
					this.SetItemChecked(i,check);
		}
		public void SetCheckByStr(string checks)
		{
			string[] strs=checks.Split(',');
			int pos=-1;
			foreach(string str in strs)
			{
				pos=this.Items.IndexOf(str);
				if(pos>-1)
					this.SetItemChecked(pos,true);
			}
		}
		public string GetCheckToStr()
		{
			string Checks="";
			for(int i=0;i<this.CheckedItems.Count;i++)
				Checks+=this.CheckedItems[i].ToString()+",";
			return Checks.TrimEnd(',');
		}
		public void Up()
		{
			int id=this.SelectedIndex;
			if(id>0)
			{
				bool b=this.GetItemChecked(id-1);
				object tmp=this.Items[id-1];

				this.Items[id-1]=this.Items[id];
				this.SetItemChecked(id-1,this.GetItemChecked(id));
				
				this.Items[id]=tmp;
				this.SetItemChecked(id,b);

				this.SelectedIndex=id-1;
			}
		}
		public void Down()
		{
			int id=this.SelectedIndex;
			if(id<this.Items.Count-1)
			{
				bool b=this.GetItemChecked(id+1);
				object tmp=this.Items[id+1];

				this.Items[id+1]=this.Items[id];
				this.SetItemChecked(id+1,this.GetItemChecked(id));
				
				this.Items[id]=tmp;
				this.SetItemChecked(id,b);

				this.SelectedIndex=id+1;
			}
		}
		public void Not()
		{
			for(int i=0;i<this.Items.Count;i++)
			{
				this.SetItemChecked(i,!this.GetItemChecked(i));
			}
		}
		
		public void ListColsOfTb(System.Data.DataTable tb)
		{
			this.Items.Clear();
			if(tb==null)
				return;
			for(int i=0;i<tb.Columns.Count;i++)
				this.Items.Add(tb.Columns[i].ColumnName);
		}
		#endregion 
		
		#region 数据绑定
		public void BindEntitiesNoName(string Name)
		{
            object ds = ClassFactory.GetEns(Name);
			Type tp = ds.GetType();
			MethodInfo method = tp.GetMethod("RetrieveAll",new Type[0]);
			method.Invoke(ds,null);

			this.BindData( ds );
		}
		public void BindData(object DataSource)
		{
			this.BindData(DataSource ,"No","Name");
		}
		public void BindData(object DataSource,string valName,string textName)
		{
			IListSource ls = DataSource as IListSource;
			if( ls!=null && !ls.ContainsListCollection )//DataSource 是表或视图
				this.BindDataDataView( ls.GetList() as DataView ,valName ,textName);
			else 
				this.BindDataIList( DataSource as IList ,valName ,textName);
		}
		public void BindDataIList( IList list,string valName,string textName)
		{
			this.DataSource = null;
			this.Items.Clear();
			if( list==null)
				return ;

			if(list.Count==0)
				return;
			PropertyInfo p1 = list[0].GetType().GetProperty(valName);
			PropertyInfo p2 = list[0].GetType().GetProperty(textName);

			MethodInfo mp = list.GetType().GetMethod("ToDataTable",new Type[0]);

			DataTable tb = null;
			if(p1!=null && p2!=null)
			{
				tb = new DataTable("IList");
				tb.Columns.Add(valName ,typeof(string));
				tb.Columns.Add(textName ,typeof(string));
				foreach(object en in list)
				{
					tb.Rows.Add(new object[]{p1.GetValue(en ,null) ,p2.GetValue(en ,null)});
				}
			}
			else if(mp!=null)
			{
				tb = mp.Invoke( list ,null) as DataTable;
				valName = tb.Columns[0].ColumnName;
				textName = tb.Columns[1].ColumnName;
			}
			else if(p1==null )
				throw new Exception("找不到属性“"+valName+"”！");
			else if(p2==null )
				throw new Exception("找不到属性“"+textName+"”！");

			this.BindDataDataTable( tb ,valName,textName);
		}

		public void BindDataDataTable(DataTable dt,string valName,string textName)
		{
			if( dt !=null)
				this.BindDataDataView( dt.DefaultView ,valName,textName);
			else
				this.BindDataDataView( null ,valName,textName);
		}
		public void BindDataDataView(DataView dv,string valName,string textName)
		{
			this.DataSource = null;
			this.Items.Clear();
			if(dv ==null || dv.Count==0)
				return;

			this.DisplayMember = textName;
			this.ValueMember = valName;
			if(dv.Table.Columns.IndexOf(valName)==-1)
				this.ValueMember = dv.Table.Columns[0].ColumnName;
			if(dv.Table.Columns.IndexOf(textName)==-1)
				this.DisplayMember = dv.Table.Columns[1].ColumnName;
			
			this.DataSource = dv;
		}



		public void CheckDatas(object Ens)
		{
//			MethodInfo mp = Ens.GetType().GetMethod("ToDataTable",new Type[0]);
//			DataTable tb = mp.Invoke( Ens ,null) as DataTable;
//			tb.DefaultView.Sort = this.ValueMember;
			
			for(int i = 0;i<this.Items.Count;i++)
			{
//				DataRowView row = this.Items[i] as DataRowView;
//				if(row !=null)
//				{
//					if(tb.DefaultView.FindRows( new object[]{ row[0]} ).Length != 0)
				this.SetItemCheckState( i ,CheckState.Checked);
//					else
//						this.SetItemChecked( i ,false);
//				}
			}

//			for(int i = 0;i<tb.DefaultView.Count;i++)
//			{
//				int tmp =this.Items.IndexOf( tb.DefaultView[i][this.ValueMember].ToString() );
//				if(tmp!=-1)
//				{
//					this.SetCheckByStr(tb.DefaultView[i][this.DisplayMember].ToString());
//				}
//			}
		}
		
		#endregion 
	}
}
