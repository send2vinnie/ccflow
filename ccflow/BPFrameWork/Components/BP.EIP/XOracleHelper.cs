using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using System.Data.OracleClient;

namespace BP.EIP
{
    public partial class XOracleHelper : XDBHelper
    {
        public override int RunSqlByTrans(IList<string> sqlList)
        {
            ConnOfOra connofora = (ConnOfOra)DBAccess.GetAppCenterDBConn;
            OracleTransaction trans = null;
            OracleConnection conn = connofora.Conn;
            try
            {
                if (conn == null)
                    conn = new OracleConnection(SystemConfig.AppCenterDSN);
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                trans = conn.BeginTransaction();

                int result = 0;

                foreach (string sql in sqlList)
                {
                    OracleCommand cmd = new OracleCommand(sql, conn, trans);
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
                //HisConnOfSQLs.PutPool(connofora);
                HisConnOfOras.PutPool(connofora);
            }
        }
    }
}
