using System;
using System.IO;
using System.Data;
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Data.SqlClient;
using BP.DA;
namespace BP
{
	/// <summary>
	/// DBLoad 的摘要说明。
	/// </summary>
	public class DBLoad
	{
		static  DBLoad()
		{
		}
		public static DataTable GetTableFromExcel_del(string filePath , string tbName ,string selectCols,int topCount)
		{
			DataTable Table = null;
			string strConn="Provider=Microsoft.Jet.OLEDB.4.0;Data Source ="+ filePath +";Extended Properties=Excel 8.0";
			OleDbConnection conn = new OleDbConnection(strConn );
			string sql = " select ";
			if(topCount == -1)
				sql += selectCols + " from ["+tbName+"]";
			else
				sql +="top " + topCount +" "+ selectCols + " from ["+tbName+"]";
			OleDbDataAdapter ada = new OleDbDataAdapter( sql,conn);
			ada.SelectCommand.CommandType = CommandType.Text;
			try
			{
				conn.Open();
				Table = new DataTable( tbName);
				ada.Fill( Table );
			}
			catch(Exception ex)
			{
				conn.Close();
				throw new Exception(ex.Message);
			}
			conn.Close();
			return Table;
		}

	
		public static int ImportTableInto(DataTable impTb ,string intoTb, string select ,int clear)
		{
			int count = 0;
			DataTable target = null ;

			//导入前是否先清空
			if( clear==1)
				DBAccess.RunSQL( "delete from " + intoTb );

			try
			{
				target = DBAccess.RunSQLReturnTable( select );
			}
			catch(Exception ex) //select查询出错，可能是缺少列
			{
				throw new Exception("源表格式有误，请核对！"+ex.Message +" :"+select);
			}

			object conn = DBAccess.GetAppCenterDBConn;

			SqlDataAdapter sqlada = null;
			OracleDataAdapter oraada = null;
			DBType dbt = DBAccess.AppCenterDBType;
			if( dbt == DBType.SQL2000 )
			{
				sqlada = new SqlDataAdapter( select ,(SqlConnection)DBAccess.GetAppCenterDBConn );
				SqlCommandBuilder bl = new SqlCommandBuilder( sqlada );
				sqlada.InsertCommand = bl.GetInsertCommand();

				count = ImportTable( impTb,target,sqlada );
			}
			else if( dbt == DBType.Oracle9i )
			{
				oraada = new OracleDataAdapter( select ,(OracleConnection)DBAccess.GetAppCenterDBConn );
				OracleCommandBuilder bl = new OracleCommandBuilder( oraada );
				oraada.InsertCommand = bl.GetInsertCommand();

				count = ImportTable( impTb,target,oraada );
			}
			else
				throw new Exception( "未获取数据库连接！ " );

			target.Dispose();
			return count;
		}


		private static int ImportTable( DataTable source ,DataTable target , SqlDataAdapter sqlada )
		{
			int count = 0;
			try
			{
				if( sqlada.InsertCommand.Connection.State!= ConnectionState.Open )
					sqlada.InsertCommand.Connection.Open();
				sqlada.InsertCommand.Transaction = sqlada.InsertCommand.Connection.BeginTransaction();
				source.Columns.Add("错误提示",typeof( string));
				source.Columns["错误提示"].MaxLength = 1000;

				int i =0;
				while( i < source.Rows.Count ) 	//for( int i=0;i<;i++)
				{
					for( int c=0;c< target.Columns.Count ;c++)
					{
						sqlada.InsertCommand.Parameters[c].Value = source.Rows[i][c];
					}
					try//个别记录失败，跳过
					{
						sqlada.InsertCommand.ExecuteNonQuery();
					}
					catch( Exception ex )
					{
						source.Rows[i]["错误提示"] =ex.Message;
						i++;
						continue;
					}
					count++; //已导入的记录数
					source.Rows.RemoveAt( i );
				}
				sqlada.InsertCommand.Transaction.Commit();
			}
			catch( Exception ex)
			{
				if( sqlada.InsertCommand.Transaction!=null)
					sqlada.InsertCommand.Transaction.Rollback();
				sqlada.InsertCommand.Connection.Close();
				throw new Exception( "导入数据失败！"+ex.Message );
			}
			return count;
		}
		private static int ImportTable( DataTable source ,DataTable target , OracleDataAdapter oraada )
		{
			int count = 0;
			try
			{
				if( oraada.InsertCommand.Connection.State!= ConnectionState.Open )
					oraada.InsertCommand.Connection.Open();
				oraada.InsertCommand.Transaction = oraada.InsertCommand.Connection.BeginTransaction();
				int i =0;
				while( i < source.Rows.Count ) 	//for( int i=0;i<;i++)
				{
					for( int c=0;c< target.Columns.Count ;c++)
					{
						oraada.InsertCommand.Parameters[c].Value = source.Rows[i][c];
					}
//					if( i>6 )
//						throw new Exception( "Test！" );
					try//个别记录失败，跳过
					{
						oraada.InsertCommand.ExecuteNonQuery();
					}
					catch
					{
						i++;
						continue;
					}
					count++; //已导入的记录数
					source.Rows.RemoveAt( i );
				}
				oraada.InsertCommand.Transaction.Commit();
			}
			catch( Exception ex)
			{
				if( oraada.InsertCommand.Transaction!=null)
					oraada.InsertCommand.Transaction.Rollback();
				oraada.InsertCommand.Connection.Close();
				throw new Exception( "导入数据失败！"+ex.Message );
			}
			return count;
		}
        public static string GenerFirstTableName(string fileName)
        {
          //  string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;   Data   Source='" + fileName + "';   extended   Properties=Excel   8.0;";
            string strConn = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
            try
            {
                if (fileName.ToLower().Contains(".xlsx"))
                {
                   // strConn = "Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";

                    strConn = "Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
                }
                OleDbConnection con = new OleDbConnection(strConn);
                con.Open();
                //=============================================================================计算出有多少个工作表sheet   
                DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                con.Close();
                con.Dispose();
                return dt.Rows[0][2].ToString().Trim();
            }
            catch(Exception ex)
            {
                throw new Exception("@获取table出错误："+ex.Message+strConn);
            }

            //Label8.Text = "工作表个数：" + dt.Rows.Count;
            //Label8.Font.Size = 10;
            ////此处的名称按照单词的首个字母来排序   
            //Label9.Text = "第一个工作表名称是：" + dt.Rows[0][2].ToString().Trim() + "，第二个工作表的名称是：" + dt.Rows[1][2].ToString().Trim() + "，第三个工作表的名称是：" + dt.Rows[2][2].ToString().Trim();
            //Label9.Font.Size = 10;
        }
		/// <summary>
		/// 通过文件，sql ,取出Table.
		/// </summary>
		/// <param name="filePath"></param>
		/// <param name="sql"></param>
		/// <returns></returns>
        public static DataTable GetTableByExt(string filePath,string sql)
        {
            DataTable Tb = new DataTable("Tb");
            Tb.Rows.Clear();

            string typ = System.IO.Path.GetExtension(filePath).ToLower();
            string strConn;
            switch (typ.ToLower() )
            {
                case ".xls":
                    sql = "SELECT * FROM [" + GenerFirstTableName(filePath) + "]";
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source =" + filePath + ";Extended Properties=Excel 8.0";
                    System.Data.OleDb.OleDbConnection conn = new OleDbConnection(strConn);
                    OleDbDataAdapter ada = new OleDbDataAdapter(sql, conn);
                    try
                    {
                        conn.Open();
                        ada.Fill(Tb);
                        Tb.TableName = Path.GetFileNameWithoutExtension(filePath);
                    }
                    catch (System.Exception ex)
                    {
                        conn.Close();
                        throw ex;//(ex.Message);
                    }
                    conn.Close();
                    break;
                case ".xlsx":
                    sql = "SELECT * FROM [" + GenerFirstTableName(filePath)+"]";
                    try
                    {
                        strConn = "Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
                        System.Data.OleDb.OleDbConnection conn121 = new OleDbConnection(strConn);
                        OleDbDataAdapter ada91 = new OleDbDataAdapter(sql, conn121);
                        conn121.Open();
                        ada91.Fill(Tb);
                        Tb.TableName = Path.GetFileNameWithoutExtension(filePath);
                        conn121.Close();
                        ada91.Dispose();
                    }
                    catch (System.Exception ex1)
                    {
                        try
                        {
                            strConn = "Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\"";
                            System.Data.OleDb.OleDbConnection conn1215 = new OleDbConnection(strConn);
                            OleDbDataAdapter ada919 = new OleDbDataAdapter(sql, conn1215);
                            ada919.Fill(Tb);
                            Tb.TableName = Path.GetFileNameWithoutExtension(filePath);
                            ada919.Dispose();
                            conn1215.Close();
                        }
                        catch
                        {

                        }
                        throw ex1;//(ex.Message);
                    }
                    break;
                case ".dbf":
                    strConn = "Driver={Microsoft dBASE Driver (*.DBF)};DBQ=" + System.IO.Path.GetDirectoryName(filePath) + "\\"; //+FilePath;//
                    OdbcConnection conn1 = new OdbcConnection(strConn);
                    OdbcDataAdapter ada1 = new OdbcDataAdapter(sql, conn1);
                    conn1.Open();
                    try
                    {
                        ada1.Fill(Tb);
                    }
                    catch//(System.Exception ex)
                    {
                        try
                        {
                            int sel = ada1.SelectCommand.CommandText.ToLower().IndexOf("select") + 6;
                            int from = ada1.SelectCommand.CommandText.ToLower().IndexOf("from");
                            ada1.SelectCommand.CommandText = ada1.SelectCommand.CommandText.Remove(sel, from - sel);
                            ada1.SelectCommand.CommandText = ada1.SelectCommand.CommandText.Insert(sel, " top 10 * ");
                            ada1.Fill(Tb);
                            Tb.TableName = "error";
                        }
                        catch (System.Exception ex)
                        {
                            conn1.Close();
                            throw new Exception("读取DBF数据失败！" + ex.Message + " SQL:" + sql);
                        }
                    }
                    conn1.Close();
                    break;
                default:
                    break;
            }
            return Tb;
        }
	}
}
