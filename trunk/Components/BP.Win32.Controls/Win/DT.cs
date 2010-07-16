using System;
using System.Data;
using System.Data.SqlClient;

namespace BP.Win.Controls
{
	/// <summary>
	/// DT 的摘要说明。
	/// </summary>
	public class DT : DataTable
	{
		public DT():base()
		{
		}
		public DT(string name):base(name)
		{
		}
		private SqlConnection conn;
		private SqlDataAdapter ada;
		public void BindData( SqlConnection con , string sql)
		{
			this.conn = new SqlConnection(con.ConnectionString);
			ada = new SqlDataAdapter( sql , this.conn);
			SqlCommandBuilder build = new SqlCommandBuilder( ada );
			try
			{
				ada.DeleteCommand = build.GetDeleteCommand();
				ada.InsertCommand = build.GetInsertCommand();
				ada.UpdateCommand = build.GetUpdateCommand();
			}
			catch(Exception ex)
			{
				System.Windows.Forms.MessageBox.Show( ex.Message );
			}

			ada.FillSchema( this, SchemaType.Mapped);
			ada.Fill( this );
		}
		public bool Save()
		{
			DataTable tmp = this.GetChanges();
			if( tmp !=null)
			{
				this.ada.Update( tmp );
				this.AcceptChanges();
			}
			return true;
		}
	}
}
