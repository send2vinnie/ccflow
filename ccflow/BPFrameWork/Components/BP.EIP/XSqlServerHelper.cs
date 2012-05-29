using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using System.Data.SqlClient;

namespace BP.EIP
{
    public partial class XSqlServerHelper : XDBHelper
    {
        public static int RunSql_SqlServer(IList<string> sqlList)
        {
            ConnOfSQL connofora = (ConnOfSQL)DBAccess.GetAppCenterDBConn;
            SqlTransaction trans = null;
            SqlConnection conn = connofora.Conn;
            try
            {
                if (conn == null)
                    conn = new SqlConnection(SystemConfig.AppCenterDSN);
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                trans = conn.BeginTransaction();

                int result = 0;

                foreach (string sql in sqlList)
                {
                    SqlCommand cmd = new SqlCommand(sql, conn, trans);
                    result += cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }

                trans.Commit();

                return result;
            }
            catch (Exception ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                throw new Exception(ex.Message);
            }
            finally
            {
                if (SystemConfig.IsBSsystem_Test == false)
                    conn.Close();
                if (trans != null)
                {
                    trans.Dispose();
                }
                HisConnOfSQLs.PutPool(connofora);
            }
        }
    }
}
