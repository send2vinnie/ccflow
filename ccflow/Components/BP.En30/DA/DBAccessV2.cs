
/*
简介：负责存取数据的类
创建时间：2002-10
最后修改时间：2004-2-1 ccb

 说明：
  在次文件种处理了4种方式的连接。
  1， sql server .
  2， oracle.
  3， ole.
  4,  odbc.
  
*/
using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Data.OracleClient ; 
using System.EnterpriseServices;
using System.Data.OleDb;
using System.Web;
using System.Data.Odbc ; 
using System.IO;
using MySql.Data;
using MySql;
using MySql.Data.MySqlClient;
using MySql.Data.MySqlClient.Properties;
using IBM.Data;
using IBM.Data.Informix;
using IBM.Data.Utilities;

//using System.Web.Caching;

namespace BP.DA
{
	
	public class BPXml
	{
		/// <summary>
		/// 读取Xml数据到
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="table"></param>
		/// <returns></returns>
		public static DataTable ReadToDataTable(string fileName, string table)
		{
			DataSet ds = new DataSet();
			ds.ReadXml(fileName);
			return ds.Tables[table] ;
		}
	}
    ///// <summary>
    ///// 第三方系统 数据库访问配置。
    ///// </summary>
    //public class DBAccessOfThirdParty
    //{
    //    public static int RunSQLReturnIntVal(string sql )
    //    {
    //        switch( SystemConfig.ThirdPartySoftDBType)
    //        {
    //            case DBType.Sybase:
    //                return  DBAccessOfODBC.RunSQLReturnValInt(sql);
    //            case DBType.SQL2000:
    //            case DBType.MySQL:
    //                return (int)BP.DA.DBAccessOfMSSQL2000.RunSQLReturnVal(sql);
    //            case DBType.Oracle9i:
    //                return DBAccess.RunSQLReturnValInt(sql);
    //            default:
    //                throw new Exception("error dbtype..");
    //        }
    //    }
    //    public static float RunSQLReturnFloatVal(string sql )
    //    {
    //        switch (SystemConfig.ThirdPartySoftDBType)
    //        {
    //            case DBType.Sybase:
    //                return DBAccessOfODBC.RunSQLReturnFloatVal(sql);
    //            case DBType.SQL2000:
    //                return (float)DBAccessOfMSSQL2000.RunSQLReturnVal(sql);
    //            case DBType.Oracle9i:
    //                return DBAccess.RunSQLReturnValFloat(sql);
    //            default:
    //                throw new Exception("error dbtype..");
    //        }
    //    }

    //    public static int RunSQL(string sql)
    //    {
    //        switch( SystemConfig.ThirdPartySoftDBType)
    //        {
    //            case DBType.Sybase:
    //                return DBAccessOfODBC.RunSQL(sql);
				 
    //            case DBType.SQL2000:
    //                return DBAccessOfMSSQL2000.RunSQL(sql);
					 
    //            case DBType.Oracle9i:
    //                return DBAccess.RunSQL(sql);
 
    //            default:
    //                throw new Exception("error dbtype..");
    //        }
    //    }
    //    public static DataTable RunSQLReturnTable(string sql)
    //    {

    //        switch (SystemConfig.ThirdPartySoftDBType)
    //        {
    //            case DBType.Sybase:
    //                return DBAccessOfODBC.RunSQLReturnTable(sql);

    //            case DBType.SQL2000:
    //                return DBAccessOfMSSQL2000.RunSQLReturnTable(sql);

    //            case DBType.Oracle9i:
    //                return DBAccess.RunSQLReturnTable(sql);
    //            default:
    //                throw new Exception("error dbtype..");
    //        }
    //    }
    //}
	/// <summary>
	/// 数据库访问。
	/// 这个类负责处理了。实体信息
	/// </summary>
    public class DBAccess
    {
        #region 事务处理
        /// <summary>
        /// 执行增加一个事务
        /// </summary>
        public static void DoTransactionBegin()
        {
            return;
            DBAccess.RunSQL("begin transaction");
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public static void DoTransactionRollback()
        {
            return;
            try
            {
                DBAccess.RunSQL("rollback transaction");
            }
            catch
            {
            }
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public static void DoTransactionCommit()
        {
            return;
            try
            {
                DBAccess.RunSQL("commit transaction");
            }
            catch
            {
            }
        }
        #endregion 事务处理


        public static Paras DealParasBySQL(string sql, Paras ps)
        {
            Paras myps = new Paras();
            foreach (Para p in ps)
            {
                if (sql.Contains(":" + p.ParaName) == false)
                    continue;
                myps.Add(p);
            }
            return myps;
        }

        #region IO
        public static void copyDirectory(string Src, string Dst)
        {
            String[] Files;
            if (Dst[Dst.Length - 1] != Path.DirectorySeparatorChar)
                Dst += Path.DirectorySeparatorChar;
            if (!Directory.Exists(Dst)) Directory.CreateDirectory(Dst);
            Files = Directory.GetFileSystemEntries(Src);
            foreach (string Element in Files)
            {
                //   Sub   directories   
                if (Directory.Exists(Element))
                    copyDirectory(Element, Dst + Path.GetFileName(Element));
                //   Files   in   directory   
                else
                    File.Copy(Element, Dst + Path.GetFileName(Element), true);
            }
        }
        #endregion

        #region 读取Xml

        #endregion

        #region 关于运行存储过程

        #region 执行存储过程返回影响个数
        public static int RunSP(string spName, string paraKey, object paraVal)
        {
            Paras pas = new Paras();
            pas.Add(paraKey, paraVal);

            return DBAccess.RunSP(spName, pas);
        }
        /// <summary>
        /// 运行存储过程
        /// </summary>
        /// <param name="spName">名称</param>
        /// <returns>返回影响的行数</returns>
        public static int RunSP(string spName)
        {
            int i = 0;
            switch (BP.SystemConfig.AppCenterDBType)
            {
                case DBType.SQL2000:
                case DBType.Access:
                    ConnOfSQL connofsql = GetAppCenterDBConn as ConnOfSQL;
                    i = DBProcedure.RunSP(spName, connofsql.Conn);
                    HisConnOfSQLs.PutPool(connofsql);
                    return i;
                case DBType.Oracle9i:
                    ConnOfOra connofora = GetAppCenterDBConn as ConnOfOra;
                    try
                    {
                        i = DBProcedure.RunSP(spName, connofora.Conn);
                        HisConnOfOras.PutPool(connofora);
                    }
                    catch (Exception ex)
                    {
                        HisConnOfOras.PutPool(connofora);
                        throw ex;
                    }
                    return i;
                case DBType.Informix:
                    ConnOfInformix connMix = GetAppCenterDBConn as ConnOfInformix;
                    try
                    {
                        i = DBProcedure.RunSP(spName, connMix.Conn);
                        HisConnOfOras.PutPool(connMix);
                    }
                    catch (Exception ex)
                    {
                        HisConnOfOras.PutPool(connMix);
                        throw ex;
                    }
                    return i;
                default:
                    throw new Exception("Error: " + BP.SystemConfig.AppCenterDBType);
            }
        }
        public static int RunSPReturnInt(string spName)
        {
            switch (BP.SystemConfig.AppCenterDBType)
            {
                case DBType.SQL2000:
                    return DBProcedure.RunSP(spName, (SqlConnection)DBAccess.GetAppCenterDBConn);
                case DBType.MySQL:
                    return DBProcedure.RunSP(spName, (MySqlConnection)DBAccess.GetAppCenterDBConn);
                case DBType.Informix:
                    return DBProcedure.RunSP(spName, (IfxConnection)DBAccess.GetAppCenterDBConn);
                case DBType.Access:
                case DBType.Oracle9i:
                    // return DBProcedure.RunSP(spName, (OracleConnection)DBAccess.GetAppCenterDBConn);
                    ConnOfOra connofora = GetAppCenterDBConn as ConnOfOra;
                    int i = DBProcedure.RunSPReturnInt(spName, connofora.Conn);
                    HisConnOfOras.PutPool(connofora);
                    return i;
                default:
                    throw new Exception("Error: " + BP.SystemConfig.AppCenterDBType);
            }
        }
         
        /// <summary>
        /// 运行存储过程
        /// </summary>
        /// <param name="spName">名称</param>
        /// <param name="paras">参数</param>
        /// <returns>返回影响的行数</returns>
        public static int RunSP(string spName, Paras paras)
        {
            int i = 0;
            switch (BP.SystemConfig.AppCenterDBType)
            {
                case DBType.SQL2000:
                case DBType.MySQL:
                case DBType.Access:
                    ConnOfSQL conn = GetAppCenterDBConn as ConnOfSQL;
                    try
                    {
                        i = DBProcedure.RunSP(spName, paras, conn.Conn);
                        HisConnOfSQLs.PutPool(conn);
                    }
                    catch (Exception ex)
                    {
                        HisConnOfSQLs.PutPool(conn);
                        throw ex;
                    }
                    return i;
                case DBType.Oracle9i:
                    ConnOfOra connofora = GetAppCenterDBConn as ConnOfOra;
                    try
                    {
                        i = DBProcedure.RunSP(spName, paras, connofora.Conn);
                        HisConnOfOras.PutPool(connofora);
                    }
                    catch (Exception ex)
                    {
                        HisConnOfOras.PutPool(connofora);
                        throw ex;
                    }
                    return i;
                case DBType.Informix:
                    ConnOfInformix connMix = GetAppCenterDBConn as ConnOfInformix;
                    try
                    {
                        i = DBProcedure.RunSP(spName, paras, connMix.Conn);
                        HisConnOfOras.PutPool(connMix);
                    }
                    catch (Exception ex)
                    {
                        HisConnOfOras.PutPool(connMix);
                        throw ex;
                    }
                    return i;
                default:
                    throw new Exception("Error " + BP.SystemConfig.AppCenterDBType);
            }
        }
        #endregion

        #region 运行存储过程返回 DataTable
        /// <summary>
        /// 运行存储过程
        /// </summary>
        /// <param name="spName">名称</param>
        /// <returns>DataTable</returns>
        public static DataTable RunSPReTable(string spName)
        {
            switch (BP.SystemConfig.AppCenterDBType)
            {
                case DBType.SQL2000:
                case DBType.Access:
                    return DBProcedure.RunSPReturnDataTable(spName, (SqlConnection)DBAccess.GetAppCenterDBConn);
                case DBType.Oracle9i:
                    ConnOfOra connofora = GetAppCenterDBConn as ConnOfOra;
                    DataTable dt = null;
                    try
                    {
                        dt = DBProcedure.RunSPReturnDataTable(spName, connofora.Conn);
                        HisConnOfOras.PutPool(connofora);
                    }
                    catch (Exception ex)
                    {
                        HisConnOfOras.PutPool(connofora);
                        throw ex;
                    }
                    return dt;
                case DBType.Informix:
                    ConnOfInformix connmix = GetAppCenterDBConn as ConnOfInformix;
                    DataTable dt1 = null;
                    try
                    {
                        dt1 = DBProcedure.RunSPReturnDataTable(spName, connmix.Conn);
                        HisConnOfOras.PutPool(connmix);
                    }
                    catch (Exception ex)
                    {
                        HisConnOfOras.PutPool(connmix);
                        throw ex;
                    }
                    return dt1;
                default:
                    throw new Exception("Error " + BP.SystemConfig.AppCenterDBType);

            }
        }
        /// <summary>
        /// 运行存储过程
        /// </summary>
        /// <param name="spName">名称</param>
        /// <param name="paras">参数</param>
        /// <returns>DataTable</returns>
        public static DataTable RunSPReTable(string spName, Paras paras)
        {
            switch (BP.SystemConfig.AppCenterDBType)
            {
                case DBType.SQL2000:
                    return DBProcedure.RunSPReturnDataTable(spName, paras, (SqlConnection)DBAccess.GetAppCenterDBConn);
                case DBType.Oracle9i:
                    ConnOfOra connofora = GetAppCenterDBConn as ConnOfOra;
                    connofora.AddSQL(spName);
                    DataTable dt = DBProcedure.RunSPReturnDataTable(spName, paras, connofora.Conn);
                    HisConnOfOras.PutPool(connofora);
                    return dt;
                case DBType.Informix:
                    ConnOfInformix connIFX = GetAppCenterDBConn as ConnOfInformix;
                    connIFX.AddSQL(spName);
                    DataTable dt1 = DBProcedure.RunSPReturnDataTable(spName, paras, connIFX.Conn);
                    HisConnOfOras.PutPool(connIFX);
                    return dt1;
                case DBType.Access:
                default:
                    throw new Exception("Error " + BP.SystemConfig.AppCenterDBType);
            }
        }
        #endregion

        #endregion

        //构造函数
        static DBAccess()
        {
            CurrentSys_Serial = new Hashtable();
            KeyLockState = new Hashtable();
        }

        #region 运行中定义的变量
        public static readonly Hashtable CurrentSys_Serial;
        private static int readCount = -1;
        private static readonly Hashtable KeyLockState;
        public static BP.DA.ConnOfOras HisConnOfOras = null;
        public static BP.DA.ConnOfSQLs HisConnOfSQLs = null;
        public static BP.DA.ConnOfOLEs HisConnOfOLEs = null;
        public static BP.DA.ConnOfMySQLs HisConnOfMySQLs = null;
        public static BP.DA.ConnOfInformixs HisConnOfInformix = null;
        #endregion


        #region 产生序列号码方法
        /// <summary>
        /// 根据标识产生的序列号
        /// </summary>
        /// <param name="type">OID</param>
        /// <returns></returns>
        public static int GenerSequenceNumber(string type)
        {
            if (readCount == -1)//系统第一次运行时
            {
                DataTable tb = DBAccess.RunSQLReturnTable("SELECT CfgKey, IntVal FROM Sys_Serial ");
                foreach (DataRow row in tb.Rows)
                {
                    string str = row[0].ToString().Trim();
                    int id = Convert.ToInt32(row[1]);
                    try
                    {
                        CurrentSys_Serial.Add(str, id);
                        KeyLockState.Add(row[0].ToString().Trim(), false);

                    }
                    catch
                    {

                    }
                }
                readCount++;
            }

            if (CurrentSys_Serial.ContainsKey(type) == false)
            {
                DBAccess.RunSQL("insert into Sys_Serial values('" + type + "',1 )");
                return 1;
            }

            while (true)
            {
                while (!(bool)KeyLockState[type])
                {
                    KeyLockState[type] = true;
                    int cur = (int)CurrentSys_Serial[type];
                    if (readCount++ % 10 == 0)
                    {
                        readCount = 1;
                        int n = (int)CurrentSys_Serial[type] + 10;

                        Paras ps = new Paras();
                        ps.Add("intVal", n);
                        ps.Add("CfgKey", type);

                        string upd = "update Sys_Serial set intVal="+SystemConfig.AppCenterDBVarStr+"intVal WHERE CfgKey=" + SystemConfig.AppCenterDBVarStr + "CfgKey";
                        DBAccess.RunSQL(upd, ps);
                    }

                    cur++;
                    CurrentSys_Serial[type] = cur;
                    KeyLockState[type] = false;
                    return cur;
                }
            }
        }
        private static bool l_OID = false;
        public static int GenerOID()
        {
            string sql = "";
            if (DBAccess.RunSQL("UPDATE Sys_Serial SET IntVal=IntVal+1 WHERE CfgKey='OID'") == 0)
                DBAccess.RunSQL("INSERT INTO Sys_Serial (CfgKey,IntVal) VALUES ('OID',100)");
            sql = "SELECT  IntVal FROM Sys_Serial WHERE CfgKey='OID'";
            return DBAccess.RunSQLReturnValInt(sql);
        }
        public static int GenerOIDV2()
        {
            return DBAccess.RunSPReturnInt("GenerOID");
        }
        public static Int64 GenerOID(string cfgKey)
        {
            Paras ps = new Paras();
            ps.Add("CfgKey", cfgKey);
            string sql = "UPDATE Sys_Serial SET IntVal=IntVal+1 WHERE CfgKey=" + SystemConfig.AppCenterDBVarStr + "CfgKey";
            int num = DBAccess.RunSQL(sql, ps);
            if (num == 0)
            {
                sql = "INSERT INTO Sys_Serial (CFGKEY,INTVAL) VALUES ('" + cfgKey + "',100)";
                DBAccess.RunSQL(sql);
                return 100;
            }
            sql = "SELECT  IntVal FROM Sys_Serial WHERE CfgKey=" + SystemConfig.AppCenterDBVarStr + "CfgKey";
            return DBAccess.RunSQLReturnValInt(sql, ps);
        }
        /// <summary>
        /// 获取一个从OID, 更新到OID.
        /// 用例: 我已经明确知道要用到260个OID, 
        /// 但是为了避免多次取出造成效率浪费，就可以一次性取出 260个OID.
        /// </summary>
        /// <param name="cfgKey"></param>
        /// <param name="getOIDNum">要获取的OID数量.</param>
        /// <returns>从OID</returns>
        public static Int64 GenerOID(string cfgKey, int getOIDNum)
        {
            Paras ps = new Paras();
            ps.Add("CfgKey", cfgKey);
            string sql = "UPDATE Sys_Serial SET IntVal=IntVal+" + getOIDNum + " WHERE CfgKey=" + SystemConfig.AppCenterDBVarStr + "CfgKey";
            int num = DBAccess.RunSQL(sql, ps);
            if (num == 0)
            {
                getOIDNum = getOIDNum + 100;
                sql = "INSERT INTO Sys_Serial (CFGKEY,INTVAL) VALUES (" + SystemConfig.AppCenterDBVarStr + "CfgKey," + getOIDNum + ")";
                DBAccess.RunSQL(sql, ps);
                return 100;
            }
            sql = "SELECT  IntVal FROM Sys_Serial WHERE CfgKey=" + SystemConfig.AppCenterDBVarStr + "CfgKey";
            return DBAccess.RunSQLReturnValInt(sql, ps) - getOIDNum;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="intKey"></param>
        /// <returns></returns>
        public static Int64 GenerOIDByKey64(string intKey)
        {
            Paras ps = new Paras();
            ps.Add("CfgKey", intKey);
            string sql = "";
            sql = "UPDATE Sys_Serial SET IntVal=IntVal+1 WHERE CfgKey=" + SystemConfig.AppCenterDBVarStr + "CfgKey";
            int num = DBAccess.RunSQL(sql, ps);
            if (num == 0)
            {
                sql = "INSERT INTO Sys_Serial (CFGKEY,INTVAL) VALUES (" + SystemConfig.AppCenterDBVarStr + "CfgKey,'1')";
                DBAccess.RunSQL(sql,ps);
                return Int64.Parse(intKey + "1");
            }
            sql = "SELECT IntVal FROM Sys_Serial WHERE CfgKey=" + SystemConfig.AppCenterDBVarStr + "CfgKey";
            int val = DBAccess.RunSQLReturnValInt(sql,ps);
            return Int64.Parse(intKey + val.ToString());
        }
        public static Int32 GenerOIDByKey32(string intKey)
        {
            Paras ps = new Paras();
            ps.Add("CfgKey", intKey);

            string sql = "";
            sql = "UPDATE Sys_Serial SET IntVal=IntVal+1 WHERE CfgKey=" + SystemConfig.AppCenterDBVarStr + "CfgKey";
            int num = DBAccess.RunSQL(sql, ps);
            if (num == 0)
            {
                sql = "INSERT INTO Sys_Serial (CFGKEY,INTVAL) VALUES (" + SystemConfig.AppCenterDBVarStr + "CfgKey,'100')";
                DBAccess.RunSQL(sql, ps);
                return int.Parse(intKey + "100");
            }
            sql = "SELECT IntVal FROM Sys_Serial WHERE CfgKey=" + SystemConfig.AppCenterDBVarStr + "CfgKey";
            int val = DBAccess.RunSQLReturnValInt(sql, ps);
            return int.Parse(intKey + val.ToString());
        }
        public static Int64 GenerOID(string table, string intKey)
        {
            Paras ps = new Paras();
            ps.Add("CfgKey", intKey);

            string sql = "";
            sql = "UPDATE " + table + " SET IntVal=IntVal+1 WHERE CfgKey=" + SystemConfig.AppCenterDBVarStr + "CfgKey";
            int num = DBAccess.RunSQL(sql,ps);
            if (num == 0)
            {
                sql = "INSERT INTO " + table + " (CFGKEY,INTVAL) VALUES (" + SystemConfig.AppCenterDBVarStr + "CfgKey,100)";
                DBAccess.RunSQL(sql,ps);
                return int.Parse(intKey + "100");
            }
            sql = "SELECT  IntVal FROM " + table + " WHERE CfgKey=" + SystemConfig.AppCenterDBVarStr + "CfgKey";
            int val = DBAccess.RunSQLReturnValInt(sql,ps);

            return Int64.Parse(intKey + val.ToString());
        }
        #endregion

        #region 检查权限
        /// <summary>
        /// 检查session . 主要是判断是不是有用户登陆信息。
        /// </summary>
        public static void DoCheckSession()
        {
            if (HttpContext.Current != null && SystemConfig.IsDebug == false)
            {
                HttpContext.Current.Session["url"] = HttpContext.Current.Request.RawUrl;
                string str = "您的登陆时间太长，请重新登陆。";
                HttpContext.Current.Session["info"] = str;
                System.Web.HttpContext.Current.Response.Redirect(System.Web.HttpContext.Current.Request.ApplicationPath + SystemConfig.PageOfLostSession, true);
                //System.Web.HttpContext.Current.Response.Redirect(System.Web.HttpContext.Current.Request.ApplicationPath+"/Portal/ErrPage.aspx");
            }
        }
        #endregion


        #region 取得连接对象 ，CS、BS共用属性【关键属性】
        public static object GetAppCenterDBConn
        {
            get
            {
                string connstr = BP.SystemConfig.AppCenterDSN;
                switch (AppCenterDBType)
                {
                    case DBType.SQL2000:
                        if (HisConnOfSQLs == null)
                        {
                            HisConnOfSQLs = new ConnOfSQLs();
                            HisConnOfSQLs.Init();
                        }
                        return HisConnOfSQLs.GetOne();
                    case DBType.Oracle9i:
                        if (HisConnOfOras == null)
                        {
                            HisConnOfOras = new ConnOfOras();
                            HisConnOfOras.Init();
                        }
                        return HisConnOfOras.GetOneV2();
                    case DBType.MySQL:
                        if (HisConnOfOras == null)
                        {
                            HisConnOfMySQLs = new ConnOfMySQLs();
                            HisConnOfMySQLs.Init();
                        }
                        return HisConnOfMySQLs.GetOne();
                    case DBType.Informix:
                        if (HisConnOfOras == null)
                        {
                            HisConnOfInformix = new ConnOfInformixs();
                            HisConnOfInformix.Init();
                        }
                        return HisConnOfInformix.GetOne();
                    case DBType.Access:
                        if (HisConnOfOLEs == null)
                        {
                            HisConnOfOLEs = new ConnOfOLEs();
                            HisConnOfOLEs.Init();
                        }
                        return HisConnOfOLEs.GetOne();
                    default:
                        throw new Exception("发现未知的数据库连接类型！");
                }
            }
        }

        #endregion 取得连接对象 ，CS、BS共用属性
        /// <summary>
        /// AppCenterDBType
        /// </summary>
        public static DBType AppCenterDBType
        {
            get
            {
                return SystemConfig.AppCenterDBType;
            }
        }

        #region 运行 SQL

        #region 在指定的Connection上执行 SQL 语句，返回受影响的行数

        #region OleDbConnection
        public static int RunSQLDropTable(string table)
        {
            if (IsExitsObject(table))
            {
                switch (AppCenterDBType)
                {
                    case DBType.Oracle9i:
                    case DBType.SQL2000:
                    case DBType.Informix:
                    case DBType.Access:
                        return RunSQL("DROP TABLE " + table);
                    default:
                        throw new Exception(" Exception ");
                }
            }
            return 0;

            /* return RunSQL("TRUNCATE TABLE " + table);*/

        }

        public static int RunSQL(string sql, OleDbConnection conn, string dsn)
        {
            return RunSQL(sql, conn, CommandType.Text, dsn);
        }
        public static int RunSQL(string sql, OleDbConnection conn, CommandType sqlType, string dsn, params object[] pars)
        {
            try
            {
                if (conn == null)
                    conn = new OleDbConnection(dsn);

                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.ConnectionString = dsn;
                    conn.Open();
                }

                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.CommandType = sqlType;
                int i = cmd.ExecuteNonQuery();

                //cmd.ExecuteReader();

                cmd.Dispose();
                conn.Close();

                //lock_SQL_RunSQL = false;
                return i;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message + sql);
            }
        }
        #endregion

        #region SqlConnection
        /// <summary>
        /// 运行SQL
        /// </summary>
       // private static bool lock_SQL_RunSQL = false;
        /// <summary>
        /// 运行SQL, 返回影响的行数.
        /// </summary>
        /// <param name="sql">msSQL</param>
        /// <param name="conn">SqlConnection</param>
        /// <returns>返回运行结果。</returns>
        public static int RunSQL(string sql, SqlConnection conn, string dsn)
        {
            return RunSQL(sql, conn, CommandType.Text, dsn);
        }
        /// <summary>
        /// 运行SQL , 返回影响的行数.
        /// </summary>
        /// <param name="sql">msSQL</param>
        /// <param name="conn">SqlConnection</param>
        /// <param name="sqlType">CommandType</param>
        /// <param name="pars">params</param>
        /// <returns>返回运行结果</returns>
        public static int RunSQL(string sql, SqlConnection conn, CommandType sqlType, string dsn)
        {
            conn.Close();
#if DEBUG
            Debug.WriteLine(sql);
#endif
            //如果是锁定状态，就等待
            //while (lock_SQL_RunSQL)
            //    ;
            // 开始执行.
            //lock_SQL_RunSQL = true; //锁定
            string step = "1";
            try
            {

                if (conn == null)
                    conn = new SqlConnection(dsn);

                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.ConnectionString = dsn;
                    conn.Open();
                }

                step = "2";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = sqlType;
                step = "3";

                step = "4";
                int i = 0;
                try
                {
                    i = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    step = "5";
                    //lock_SQL_RunSQL = false;
                    cmd.Dispose();
                    step = "6";
                    throw new Exception("RunSQL step=" + step + ex.Message + " SQL=" + sql);
                }
                step = "7";
                cmd.Dispose();
               // lock_SQL_RunSQL = false;
                return i;
            }
            catch (System.Exception ex)
            {
                step = "8";
               // lock_SQL_RunSQL = false;
                throw new Exception("RunSQL2 step=" + step + ex.Message + " 设置连接时间=" + conn.ConnectionTimeout);
            }
            finally
            {
                step = "9";
                //lock_SQL_RunSQL = false;
                conn.Close();
            }
        }
        #endregion

        #region OracleConnection
        public static int RunSQL(string sql, OracleConnection conn, string dsn)
        {
            return RunSQL(sql, conn, CommandType.Text, dsn);
        }
        public static int RunSQL(string sql, OracleConnection conn, CommandType sqlType, string dsn)
        {
#if DEBUG
            Debug.WriteLine(sql);
#endif
            //如果是锁定状态，就等待
           // while (lock_SQL_RunSQL)
              //  ;
            // 开始执行.
           // lock_SQL_RunSQL = true; //锁定
            string step = "1";
            try
            {
                if (conn == null)
                    conn = new OracleConnection(dsn);

                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.ConnectionString = dsn;
                    conn.Open();
                }

                step = "2";
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = sqlType;
                step = "3";
                int i = 0;
                try
                {
                    i = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    step = "5";
                   // lock_SQL_RunSQL = false;
                    cmd.Dispose();
                    step = "6";
                    throw new Exception("RunSQL step=" + step + ex.Message + " SQL=" + sql);
                }
                step = "7";
                cmd.Dispose();

                
                //lock_SQL_RunSQL = false;
                return i;
            }
            catch (System.Exception ex)
            {
                step = "8";
               // lock_SQL_RunSQL = false;
                throw new Exception("RunSQL2 step=" + step + ex.Message);
            }
            finally
            {
                step = "9";
               // lock_SQL_RunSQL = false;
                conn.Close();
            }

            /*
            Debug.WriteLine( sql );
            try
            {
                OracleCommand cmd = new OracleCommand( sql ,conn);
                cmd.CommandType = sqlType;
                foreach(object par in pars)
                {
                    cmd.Parameters.Add( "par",par);
                }
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }
				
                int i= cmd.ExecuteNonQuery();				 
                cmd.Dispose();
                return i;				 
            }
            catch(System.Exception ex)
            {
                throw new Exception(ex.Message + sql );
            }
            finally
            {
                conn.Close();

            }
            */
        }
        #endregion

        #endregion

        #region 通过主应用程序在其他库上运行sql
        #region pk
        /// <summary>
        /// 建立主键
        /// </summary>
        /// <param name="tab">物理表</param>
        /// <param name="pk">主键</param>
        public static void CreatePK(string tab, string pk, DBType db)
        {
            string sql;
            switch (db)
            {
                case DBType.Informix:
                    sql = "ALTER TABLE " + tab.ToUpper() + " ADD CONSTRAINT  PRIMARY KEY(" + pk + ") CONSTRAINT " + tab + "pk ";
                    break;
                default:
                    sql = "ALTER TABLE " + tab.ToUpper() + " ADD CONSTRAINT " + tab + "pk PRIMARY KEY(" + pk.ToUpper() + ")";
                    break;
            }
            DBAccess.RunSQL(sql);
        }
        public static void CreatePK(string tab, string pk1, string pk2, DBType db)
        {
            string sql;
            switch (db)
            {
                case DBType.Informix:
                    sql = "ALTER TABLE " + tab.ToUpper() + " ADD CONSTRAINT  PRIMARY KEY(" + pk1.ToUpper() + "," + pk2.ToUpper() + ") CONSTRAINT " + tab + "pk ";
                    break;
                default:
                    sql = "ALTER TABLE " + tab.ToUpper() + " ADD CONSTRAINT " + tab + "pk  PRIMARY KEY(" + pk1.ToUpper() + "," + pk2.ToUpper() + ")";
                    break;
            }
            DBAccess.RunSQL(sql);
        }
        public static void CreatePK(string tab, string pk1, string pk2, string pk3, DBType db)
        {
            string sql;
            switch (db)
            {
                case DBType.Informix:
                    sql = "ALTER TABLE " + tab.ToUpper() + " ADD CONSTRAINT  PRIMARY KEY(" + pk1.ToUpper() + "," + pk2.ToUpper() + ","+pk3.ToUpper()+") CONSTRAINT " + tab + "pk ";
                    break;
                default:
                    sql = "ALTER TABLE " + tab.ToUpper() + " ADD CONSTRAINT " + tab + "pk PRIMARY KEY(" + pk1.ToUpper() + "," + pk2.ToUpper() + "," + pk3.ToUpper() + ")";
                    break;
            }
            DBAccess.RunSQL(sql);
        }
        #endregion


        #region index
        public static void CreatIndex(string table, string pk)
        {
            string sql = "";
            try
            {
                sql = "DROP INDEX " + table + "." + table + "ID";
                DBAccess.RunSQL(sql);
            }
            catch
            {
            }

            try
            {
                sql = "CREATE INDEX " + table + "ID ON " + table + " (" + pk + ")";
                DBAccess.RunSQL(sql);
            }
            catch
            {
            }
        }
        public static void CreatIndex(string table, string pk1, string pk2)
        {
            try
            {
                DBAccess.RunSQL("CREATE INDEX " + table + "ID ON " + table + " (" + pk1 + "," + pk2 + ")");
            }
            catch
            {
            }
        }
        public static void CreatIndex(string table, string pk1, string pk2, string pk3)
        {
            DBAccess.RunSQL("CREATE INDEX " + table + "ID ON " + table + " (" + pk1 + "," + pk2 + "," + pk3 + ")");
        }
        public static void CreatIndex(string table, string pk1, string pk2, string pk3, string pk4)
        {
            DBAccess.RunSQL("CREATE INDEX " + table + "ID ON " + table + " (" + pk1 + "," + pk2 + "," + pk3 + "," + pk3 + ")");
        }
        #endregion

        public static int CreatTableFromODBC(string selectSQL, string table, string pk)
        {
            DBAccess.RunSQLDropTable(table);
            string sql = "SELECT * INTO " + table + " FROM OPENROWSET('MSDASQL','" + SystemConfig.AppSettings["DBAccessOfODBC"] + "','" + selectSQL + "')";
            int i = DBAccess.RunSQL(sql);
            DBAccess.RunSQL("CREATE INDEX " + table + "ID ON " + table + " (" + pk + ")");
            return i;
        }
        public static int CreatTableFromODBC(string selectSQL, string table, string pk1, string pk2)
        {
            DBAccess.RunSQLDropTable(table);
            //DBAccess.RunSQL("DROP TABLE "+table);
            string sql = "SELECT * INTO " + table + " FROM OPENROWSET('MSDASQL','" + SystemConfig.AppSettings["DBAccessOfODBC"] + "','" + selectSQL + "')";
            int i = DBAccess.RunSQL(sql);
            DBAccess.RunSQL("CREATE INDEX " + table + "ID ON " + table + " (" + pk1 + "," + pk2 + ")");
            return i;
        }
        public static int CreatTableFromODBC(string selectSQL, string table, string pk1, string pk2, string pk3)
        {
            DBAccess.RunSQLDropTable(table);
            string sql = "SELECT * INTO " + table + " FROM OPENROWSET('MSDASQL','" + SystemConfig.AppSettings["DBAccessOfODBC"] + "','" + selectSQL + "')";
            int i = DBAccess.RunSQL(sql);
            DBAccess.RunSQL("CREATE INDEX " + table + "ID ON " + table + " (" + pk1 + "," + pk2 + "," + pk3 + ")");
            return i;
        }
        #endregion

        #region 在当前的Connection执行 SQL 语句，返回受影响的行数
        public static int RunSQL(string sql, CommandType sqlType, string dsn, params object[] pars)
        {
            object oconn = GetAppCenterDBConn;
            if (oconn is SqlConnection)
                return RunSQL(sql, (SqlConnection)oconn, sqlType, dsn);
            else if (oconn is OracleConnection)
                return RunSQL(sql, (OracleConnection)oconn, sqlType, dsn);
            else
                throw new Exception("获取数据库连接[GetAppCenterDBConn]失败！");
        }
        public static DataTable ReadProText(string proName)
        {
            string sql = "";
            switch (BP.SystemConfig.AppCenterDBType)
            {
                case DBType.Oracle9i:
                    sql = "SELECT text FROM user_source WHERE name=UPPER('" + proName + "') ORDER BY LINE ";
                    break;
                default:
                    sql = "SP_Help  " + proName;
                    break;
            }
            try
            {
                return BP.DA.DBAccess.RunSQLReturnTable(sql);
            }
            catch
            {
                sql = "select * from Port_Emp WHERE 1=2";
                return BP.DA.DBAccess.RunSQLReturnTable(sql);
            }
        }
        public static void RunSQLScript(string sqlOfScriptFilePath)
        {
            string str = BP.DA.DataType.ReadTextFile(sqlOfScriptFilePath);
            string[] strs = str.Split(';');
            foreach (string s in strs)
            {
                if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
                    continue;

                if (s.Contains("--"))
                    continue;

                if (s.Contains("/*"))
                    continue;

                BP.DA.DBAccess.RunSQL(s);
            }
        }

        public static void RunSQLs(string sql)
        {
            if (string.IsNullOrEmpty(sql))
                return;

            sql = sql.Replace("@GO","~");
            sql = sql.Replace("@", "~");
            string[] strs = sql.Split('~');
            foreach (string str in strs)
            {
                if (string.IsNullOrEmpty(str))
                    continue;

                if (str.Contains("--") || str.Contains("/*"))
                    continue;

                RunSQL(str);
            }
        }
        /// <summary>
        /// 运行带有参数的sql
        /// </summary>
        /// <param name="ps"></param>
        /// <returns></returns>
        public static int RunSQL(Paras ps)
        {
            return RunSQL(ps.SQL, ps);
        }
        /// <summary>
        /// 运行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int RunSQL(string sql)
        {
            if (sql == null || sql.Trim() == "")
                return 1;
            Paras ps = new Paras();
            ps.SQL = sql;
            return RunSQL(ps);
            //switch (AppCenterDBType)
            //{
            //    case DBType.SQL2000:
            //        return RunSQL_200705_SQL(sql);
            //    case DBType.Oracle9i:
            //        Paras ps = new Paras();
            //        return RunSQL_200705_Ora(sql,ps);
            //    case DBType.MySQL:
            //        return RunSQL_200705_MySQL(sql);
            //    case DBType.Informix:
            //        return RunSQL_201205_Informix(sql);
            //    case DBType.Access:
            //        return RunSQL_200705_OLE(sql);
            //    default:
            //        throw new Exception("发现未知的数据库连接类型！");
            //}
        }
        public static int RunSQL(string sql, string paraKey, object val)
        {
            Paras ens = new Paras();
            ens.Add(paraKey, val);
            return RunSQL(sql, ens);
        }
        public static int RunSQL(string sql, string paraKey1, object val1, string paraKey2, object val2)
        {
            Paras ens = new Paras();
            ens.Add(paraKey1, val1);
            ens.Add(paraKey2, val2);
            return RunSQL(sql, ens);
        }
        public static int RunSQL(string sql, string paraKey1, object val1, string paraKey2, object val2, string k3, object v3)
        {
            Paras ens = new Paras();
            ens.Add(paraKey1, val1);
            ens.Add(paraKey2, val2);
            ens.Add(k3, v3);
            return RunSQL(sql, ens);
        }
        /// <summary>
        /// 
        /// </summary>
        private static bool lockRunSQL = false;
        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static int RunSQL(string sql, Paras paras)
        {
            if (sql == null || sql.Trim() == "")
                return 1;
            while (lockRunSQL == true)
            {
            };
            lockRunSQL = true;
            int result = 0;
            try
            {
                switch (AppCenterDBType)
                {
                    case DBType.SQL2000:
                        result = RunSQL_200705_SQL(sql, paras);
                        break;
                    case DBType.Oracle9i:
                        result = RunSQL_200705_Ora(sql.Replace("]", "").Replace("[", ""), paras);
                        break;
                    case DBType.MySQL:
                        result = RunSQL_200705_MySQL(sql, paras);
                        break;
                    case DBType.Informix:
                        result = RunSQL_201205_Informix(sql, paras);
                        break;
                    case DBType.Access:
                        result = RunSQL_200705_OLE(sql, paras);
                        break;
                    default:
                        throw new Exception("发现未知的数据库连接类型！");
                }
                lockRunSQL = false;
                return result;
            }
            catch (Exception ex)
            {
                lockRunSQL = false;
                string msg = "";
                string mysql = sql.Clone() as string;
                foreach (Para p in paras)
                {
                    msg += "@" + p.ParaName + "=" + p.val + "," + p.DAType.ToString();
                    mysql = mysql.Replace(":" + p.ParaName + ",", "'" + p.val + "',");
                }
                throw new Exception(ex.Message + " Paras(" + paras.Count + ")=" + msg + "<hr>" + mysql);
            }
        }
        /// <summary>
        /// 运行sql
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns>执行结果</returns>
        private static int RunSQL_200705_SQL(string sql)
        {
            ConnOfSQL connofora = (ConnOfSQL)DBAccess.GetAppCenterDBConn;
            SqlConnection conn = connofora.Conn;
            try
            {
                if (conn == null)
                    conn = new SqlConnection(SystemConfig.AppCenterDSN);

                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                int i = cmd.ExecuteNonQuery();
                cmd.Dispose();
                HisConnOfSQLs.PutPool(connofora);
                return i;
            }
            catch (System.Exception ex)
            {
                HisConnOfSQLs.PutPool(connofora);
                if (BP.SystemConfig.IsDebug)
                {
                    string msg = "RunSQL2   SQL=" + sql + ex.Message;
                    Log.DebugWriteError(msg);
                    throw new Exception(msg);
                }
                else
                {
                    throw new Exception (ex.Message + " RUN SQL="+sql);
                }
            }
            finally
            {
                if (SystemConfig.IsBSsystem_Test == false)
                    conn.Close();

                HisConnOfSQLs.PutPool(connofora);
            }
        }
        private static int RunSQL_200705_SQL(string sql, Paras paras)
        {
            ConnOfSQL connofora = (ConnOfSQL)DBAccess.GetAppCenterDBConn;
            connofora.AddSQL(sql); //增加
            SqlConnection conn = connofora.Conn;
            try
            {
                if (conn == null)
                    conn = new SqlConnection(SystemConfig.AppCenterDSN);

                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.ConnectionString = SystemConfig.AppCenterDSN;
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;

                foreach (Para para in paras)
                {
                    SqlParameter oraP = new SqlParameter(para.ParaName, para.val);
                    cmd.Parameters.Add(oraP);
                }

                int i = cmd.ExecuteNonQuery();
                cmd.Dispose();
                HisConnOfSQLs.PutPool(connofora);
                return i;
            }
            catch (System.Exception ex)
            {
                HisConnOfSQLs.PutPool(connofora);
                if (BP.SystemConfig.IsDebug)
                {
                    string msg = "RunSQL2   SQL=" + sql + ex.Message;
                    Log.DebugWriteError(msg);
                    throw new Exception(msg);
                }
                else
                    throw ex;
            }
            finally
            {
                if (SystemConfig.IsBSsystem_Test == false)
                    conn.Close();
                HisConnOfSQLs.PutPool(connofora);
            }
        }
        /// <summary>
        /// 运行sql
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns>执行结果</returns>
        private static int RunSQL_200705_MySQL(string sql)
        {
            return RunSQL_200705_MySQL(sql, new Paras());
        }
        /// <summary>
        /// RunSQL_200705_MySQL
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        private static int RunSQL_200705_MySQL(string sql, Paras paras)
        {
            MySqlConnection conn = new MySqlConnection(SystemConfig.AppCenterDSN);
            int i = 0;
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.ConnectionString = SystemConfig.AppCenterDSN;
                    conn.Open();
                }

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                foreach (Para para in paras)
                {
                    MySqlParameter oraP = new MySqlParameter(para.ParaName, para.val);
                    cmd.Parameters.Add(oraP);
                }
                i = cmd.ExecuteNonQuery();
                cmd.Dispose();

                conn.Close();
                conn.Dispose();
                return i;
            }
            catch (System.Exception ex)
            {
                conn.Close();
                conn.Dispose();
                throw new Exception(ex.Message+"@SQL:"+sql);
            }
            //finally
            //{
            //    conn.Close();
            //    conn.Dispose();
            //    return i;
            //}
        }
        private static int RunSQL_200705_Ora(string sql,Paras paras)
        {
            ConnOfOra connofora = (ConnOfOra)DBAccess.GetAppCenterDBConn;
            connofora.AddSQL(sql); //增加

            OracleConnection conn = connofora.Conn;
            try
            {
                if (conn == null)
                    conn = new OracleConnection(SystemConfig.AppCenterDSN);

                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.ConnectionString = SystemConfig.AppCenterDSN;
                    conn.Open();
                }

                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;

                foreach (Para para in paras)
                {
                    OracleParameter oraP = new OracleParameter(para.ParaName, para.DATypeOfOra);
                    oraP.Size = para.Size;
                    oraP.Value = para.val;
                    cmd.Parameters.Add(oraP);
                }
                int i = cmd.ExecuteNonQuery();
                cmd.Dispose();
                HisConnOfOras.PutPool(connofora);
                return i;
            }
            catch (System.Exception ex)
            {
                HisConnOfOras.PutPool(connofora);
                if (BP.SystemConfig.IsDebug)
                {
                    string msg = "RunSQL2   SQL=" + sql + ex.Message;
                    Log.DebugWriteError(msg);
                    throw new Exception(msg);
                }
                else
                {
                    Log.DebugWriteError(ex.Message);
                    throw new Exception(ex.Message + sql);
                }
            }
            finally
            {
                if (SystemConfig.IsBSsystem_Test == false)
                    conn.Close();
                HisConnOfOras.PutPool(connofora);
            }
        }
        private static int RunSQL_200705_OLE(string sql, Paras para)
        {
            OleDbConnection conn = new OleDbConnection(SystemConfig.AppCenterDSN); // connofora.Conn;
            try
            {
                if (conn == null)
                    conn = new OleDbConnection(SystemConfig.AppCenterDSN);

                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.CommandType = CommandType.Text;

                foreach (Para mypara in para)
                {
                    OleDbParameter oraP = new OleDbParameter(mypara.ParaName, mypara.val);
                    cmd.Parameters.Add(oraP);
                }

                int i = cmd.ExecuteNonQuery();
                conn.Close();
                return i;
            }
            catch (System.Exception ex)
            {
                conn.Close();
                string msg = "RunSQL_200705_OLE   SQL=" + sql + ex.Message;
                Log.DebugWriteError(msg);
                throw new Exception(msg);
            }
            finally
            {
                conn.Close();
            }
        }
        private static int RunSQL_200705_OLE(string sql)
        {
            Paras ps = new Paras();
            return RunSQL_200705_OLE(sql, ps);
        }

        /// <summary>
        /// 运行sql
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns>执行结果</returns>
        private static int RunSQL_201205_Informix(string sql)
        {
            return RunSQL_201205_Informix(sql, new Paras());

            //ConnOfInformix connofora = (ConnOfInformix)DBAccess.GetAppCenterDBConn;
            //IfxConnection conn = connofora.Conn;
            //try
            //{
            //    if (conn == null)
            //        conn = new IfxConnection(SystemConfig.AppCenterDSN);

            //    if (conn.State != System.Data.ConnectionState.Open)
            //        conn.Open();

            //    IfxCommand cmd = new IfxCommand(sql, conn);
            //    cmd.CommandType = CommandType.Text;
            //    int i = cmd.ExecuteNonQuery();
            //    cmd.Dispose();
                
            //    conn.Close();
            //    conn.Dispose();

            //    HisConnOfInformix.PutPool(connofora);
            //    return i;
            //}
            //catch (System.Exception ex)
            //{
            //    conn.Close();
            //    conn.Dispose();

            //    HisConnOfInformix.PutPool(connofora);
            //    if (BP.SystemConfig.IsDebug)
            //    {
            //        string msg = "RunSQL2   SQL=" + sql + ex.Message;
            //        Log.DebugWriteError(msg);
            //        throw new Exception(msg);
            //    }
            //    else
            //    {
            //        throw new Exception(ex.Message + " RUN SQL=" + sql);
            //    }
            //}
            //finally
            //{
            //    if (SystemConfig.IsBSsystem_Test == false)
            //    {
            //        conn.Close();
            //        conn.Dispose();
            //    }

            //    conn.Close();
            //    HisConnOfInformix.PutPool(connofora);
            //}
        }
        private static int RunSQL_201205_Informix(string sql, Paras paras)
        {
            if (paras.Count != 0)
                sql = DealInformixSQL(sql);

            ConnOfInformix connofora = (ConnOfInformix)DBAccess.GetAppCenterDBConn;
            //  connofora.AddSQL(sql); //增加.

            IfxConnection conn = connofora.Conn;
            try
            {
                if (conn == null)
                    conn = new IfxConnection(SystemConfig.AppCenterDSN);

                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.ConnectionString = SystemConfig.AppCenterDSN;
                    conn.Open();
                }

                IfxCommand cmd = new IfxCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                foreach (Para para in paras)
                {
                    IfxParameter oraP = new IfxParameter(para.ParaName, para.val);
                    cmd.Parameters.Add(oraP);
                }

                int i = cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
                HisConnOfInformix.PutPool(connofora);
                return i;
            }
            catch (System.Exception ex)
            {
                conn.Close();
                HisConnOfInformix.PutPool(connofora);
                string msg = "RunSQL2   SQL=" + sql + "\r\n Message=: " + ex.Message;
                Log.DebugWriteError(msg);
                throw new Exception(msg);
            }
            finally
            {
                conn.Close();
                HisConnOfInformix.PutPool(connofora);
            }
        }
        #endregion

        #endregion

        #region 运行SQL 返回 DataTable
        #region 在指定的 Connection 上执行

        #region SqlConnection
        /// <summary>
        /// 锁
        /// </summary>
        private static bool lock_msSQL_ReturnTable = false;
        public static DataTable RunSQLReturnTable(string oraSQL, OracleConnection conn, CommandType sqlType, string dsn)
        {

#if DEBUG
            //Debug.WriteLine( oraSQL );
#endif

            
            try
            {
                if (conn == null)
                {
                    conn = new OracleConnection(dsn);
                    conn.Open();
                }

                if (conn.State != ConnectionState.Open)
                {
                    conn.ConnectionString = dsn;
                    conn.Open();
                }

                OracleDataAdapter oraAda = new OracleDataAdapter(oraSQL, conn);
                oraAda.SelectCommand.CommandType = sqlType;

               
                DataTable oratb = new DataTable("otb");
                oraAda.Fill(oratb);

                // peng add 07-19
                oraAda.Dispose();

                if (SystemConfig.IsBSsystem_Test == false)
                    conn.Close();

                return oratb;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message + " [RunSQLReturnTable on OracleConnection dsn=App ] sql=" + oraSQL + "<br>");
            }
            finally
            {
                // oraconn.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msSQL"></param>
        /// <param name="sqlconn"></param>
        /// <param name="sqlType"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public static DataTable RunSQLReturnTable(string msSQL, SqlConnection conn, string connStr, CommandType sqlType, params object[] pars)
        {
            string msg = "step1";

            if (conn.State == ConnectionState.Closed)
            {
                conn.ConnectionString = connStr;
                conn.Open();
            }

#if DEBUG
            Debug.WriteLine(msSQL);
#endif

            while (lock_msSQL_ReturnTable)
                ;


            SqlDataAdapter msAda = new SqlDataAdapter(msSQL, conn);

            msg = "error 2";

            msAda.SelectCommand.CommandType = sqlType;
            //CommandType.
            foreach (object par in pars)
            {
                msAda.SelectCommand.Parameters.AddWithValue("par", par);
            }

            DataTable mstb = new DataTable("mstb");
            //如果是锁定状态，就等待
            lock_msSQL_ReturnTable = true; //锁定
            try
            {
                msg = "error 3";
                try
                {
                    msg = "4";
                    msAda.Fill(mstb);
                }
                catch (Exception ex)
                {
                    msg = "5";
                    lock_msSQL_ReturnTable = false;
                    conn.Close();
                    throw new Exception(ex.Message + " msg=" + msg + " Run@DBAccess");
                }
                msg = "10";
                msAda.Dispose();
                msg = "11";
                //				if (SystemConfig.IsBSsystem==false )
                //				{
                //					msg="13";
                //					sqlconn.Close();
                //				}
                msg = "14";
                lock_msSQL_ReturnTable = false;// 返回前一定要开锁
                conn.Close();
            }
            catch (System.Exception ex)
            {
                lock_msSQL_ReturnTable = false;
                conn.Close();
                throw new Exception("[RunSQLReturnTable on SqlConnection 1] step = " + msg + "<BR>" + ex.Message + " sql=" + msSQL);
            }
            return mstb;
        }
        /// <summary>
        /// 运行sql 返回Table
        /// </summary>
        /// <param name="msSQL">要运行的sql</param>
        /// <param name="sqlconn">SqlConnection</param>
        /// <returns>DataTable</returns>
        public static DataTable RunSQLReturnTable(string msSQL, SqlConnection conn, string connStr)
        {
            return RunSQLReturnTable(msSQL, conn, connStr, CommandType.Text);
        }
        
        #endregion

        #region OleDbConnection
        /// <summary>
        /// 锁
        /// </summary>
        private static bool lock_oleSQL_ReturnTable = false;
        /// <summary>
        /// 运行sql 返回Table
        /// </summary>
        /// <param name="oleSQL">oleSQL</param>
        /// <param name="oleconn">连接</param>
        /// <param name="sqlType">类型</param>
        /// <param name="pars">参数</param>
        /// <returns>执行SQL返回的DataTable</returns>
        public static DataTable RunSQLReturnTable(string oleSQL, OleDbConnection oleconn, CommandType sqlType, params object[] pars)
        {
#if DEBUG
            Debug.WriteLine(oleSQL);
#endif


            while (lock_oleSQL_ReturnTable)
            {
                ;
            }  //如果是锁定状态，就等待
            lock_oleSQL_ReturnTable = true; //锁定
            string msg = "step1";
            try
            {
                OleDbDataAdapter msAda = new OleDbDataAdapter(oleSQL, oleconn);
                msg += "2";
                msAda.SelectCommand.CommandType = sqlType;
                foreach (object par in pars)
                {
                    msAda.SelectCommand.Parameters.AddWithValue("par", par);
                }
                DataTable mstb = new DataTable("mstb");
                msg += "3";
                msAda.Fill(mstb);
                msg += "4";
                // peng add 2004-07-19 .
                msAda.Dispose();
                msg += "5";
                if (SystemConfig.IsBSsystem_Test == false)
                {
                    msg += "6";
                    oleconn.Close();
                }
                msg += "7";
                lock_oleSQL_ReturnTable = false;//返回前一定要开锁
                return mstb;
            }
            catch (System.Exception ex)
            {
                lock_oleSQL_ReturnTable = false;//返回前一定要开锁
                throw new Exception("[RunSQLReturnTable on OleDbConnection] error  请把错误交给 peng . step = " + msg + "<BR>" + oleSQL + " ex=" + ex.Message);
            }
            finally
            {
                oleconn.Close();
            }
        }
        /// <summary>
        /// 运行sql 返回Table
        /// </summary>
        /// <param name="oleSQL">要运行的sql</param>
        /// <param name="sqlconn">OleDbConnection</param>
        /// <returns>DataTable</returns>
        public static DataTable RunSQLReturnTable(string oleSQL, OleDbConnection sqlconn)
        {
            return RunSQLReturnTable(oleSQL, sqlconn, CommandType.Text);
        }
        #endregion

        #region OracleConnection
        private static DataTable RunSQLReturnTable_200705_Ora(string selectSQL, Paras paras)
        {
            ConnOfOra connofObj = GetAppCenterDBConn as ConnOfOra;
            connofObj.AddSQL(selectSQL);
            OracleConnection conn = connofObj.Conn;
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                OracleDataAdapter ada = new OracleDataAdapter(selectSQL, conn);
                ada.SelectCommand.CommandType = CommandType.Text;

                // 加入参数
                foreach (Para para in paras)
                {
                    OracleParameter myParameter = new OracleParameter(para.ParaName, para.DATypeOfOra);
                    myParameter.Size = para.Size;
                    myParameter.Value = para.val;
                    ada.SelectCommand.Parameters.Add(myParameter);
                }

                DataTable oratb = new DataTable("otb");
                ada.Fill(oratb);
                ada.Dispose();
                HisConnOfOras.PutPool(connofObj);
                return oratb;
            }
            catch (System.Exception ex)
            {
                HisConnOfOras.PutPool(connofObj);
                string msg = "@运行查询在(RunSQLReturnTable_200705_Ora with paras)出错 sql=" + selectSQL + " @异常信息：" + ex.Message;

                msg += "@Para Num= " + paras.Count;
                foreach (Para pa in paras)
                {
                    msg += "@" + pa.ParaName + "=" + pa.val;
                }
                Log.DebugWriteError(msg);
                throw new Exception(msg);
            }
            finally
            {
                HisConnOfOras.PutPool(connofObj);
            }
        }
        /// <summary>
        /// RunSQLReturnTable_200705_Ora
        /// </summary>
        /// <param name="selectSQL">要执行的sql</param>
        /// <returns>返回table</returns>
        private static DataTable RunSQLReturnTable_200705_Ora_del(string selectSQL)
        {
            if (selectSQL.Contains(":"))
                throw new Exception("@sql 中有参数。"+selectSQL );

            ConnOfOra connofObj = GetAppCenterDBConn as ConnOfOra;
            OracleConnection conn = connofObj.Conn;
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                OracleDataAdapter ada = new OracleDataAdapter(selectSQL, conn);
                ada.SelectCommand.CommandType = CommandType.Text;
                DataTable oratb = new DataTable("otb");
                ada.Fill(oratb);
                ada.Dispose();
                HisConnOfOras.PutPool(connofObj);
                return oratb;
            }
            catch (System.Exception ex)
            {
                HisConnOfOras.PutPool(connofObj);
                string msg = "@运行查询在(RunSQLReturnTable_200705_Ora NoParas)出错 sql=" + selectSQL + " @异常信息：" + ex.Message;
                Log.DebugWriteError(msg);
                throw new Exception(msg);
            }
            finally
            {
                HisConnOfOras.PutPool(connofObj);
            }
        }
        /// <summary>
        /// RunSQLReturnTable_200705_SQL
        /// </summary>
        /// <param name="selectSQL">要执行的sql</param>
        /// <returns>返回table</returns>
        private static DataTable RunSQLReturnTable_200705_SQL_bak(string selectSQL, Paras paras)
        {
            ConnOfSQL connofObj = GetAppCenterDBConn as ConnOfSQL;
            connofObj.AddSQL(selectSQL);
            SqlConnection conn = connofObj.Conn;
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                SqlDataAdapter ada = new SqlDataAdapter(selectSQL, conn);
                ada.SelectCommand.CommandType = CommandType.Text;

                // 加入参数
                foreach (Para para in paras)
                {
                    SqlParameter myParameter = new SqlParameter(para.ParaName, para.val);
                    myParameter.Size = para.Size;
                    ada.SelectCommand.Parameters.Add(myParameter);
                }

                DataTable oratb = new DataTable("otb");
                ada.Fill(oratb);
                ada.Dispose();

                HisConnOfSQLs.PutPool(connofObj);
                return oratb;
            }
            catch (System.Exception ex)
            {
                HisConnOfSQLs.PutPool(connofObj);
                string msg = "@运行查询在(RunSQLReturnTable_200705_SQL with paras)出错 sql=" + selectSQL + " @异常信息：" + ex.Message;

                msg += "@Para Num= " + paras.Count;
                foreach (Para pa in paras)
                {
                    msg += "@" + pa.ParaName + "=" + pa.val;
                }
                Log.DebugWriteError(msg);
                throw new Exception(msg);
            }
            finally
            {
                HisConnOfSQLs.PutPool(connofObj);
            }
        }
        private static DataTable RunSQLReturnTable_200705_SQL(string selectSQL)
        {
            ConnOfSQL connofObj = GetAppCenterDBConn as ConnOfSQL;
            SqlConnection conn = connofObj.Conn;
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                SqlDataAdapter ada = new SqlDataAdapter(selectSQL, conn);
                ada.SelectCommand.CommandType = CommandType.Text;
                DataTable oratb = new DataTable("otb");
                ada.Fill(oratb);
                ada.Dispose();
                HisConnOfSQLs.PutPool(connofObj);
                return oratb;
            }
            catch (System.Exception ex)
            {
                HisConnOfSQLs.PutPool(connofObj);
                string msgErr = ex.Message;
                //if (msgErr.Contains("DataReader"))
                //{
                //    /*数据连接的问题*/
                //    conn.Close();
                //    return RunSQLReturnTable_200705_SQL(selectSQL);
                //}
                string msg = "@运行查询在(RunSQLReturnTable_200705_SQL)出错 sql=" + selectSQL + " @异常信息：" + msgErr;
                Log.DebugWriteError(msg);
                throw new Exception(msg);
            }
            //finally
            //{
            //    conn.Close();
            //    HisConnOfSQLs.PutPool(connofObj);
            //}
            //return RunSQLReturnTable_200705_SQL(selectSQL);
        }
        private static DataTable RunSQLReturnTable_200705_SQL(string sql, Paras paras)
        {
            SqlConnection conn = new SqlConnection(SystemConfig.AppCenterDSN);
            if (conn.State != ConnectionState.Open)
                conn.Open();

            SqlDataAdapter ada = new SqlDataAdapter(sql, conn);
            ada.SelectCommand.CommandType = CommandType.Text;

            // 加入参数
            foreach (Para para in paras)
            {
                SqlParameter myParameter = new SqlParameter(para.ParaName, para.val);
                myParameter.Size = para.Size;
                ada.SelectCommand.Parameters.Add(myParameter);
            }

            try
            {
                DataTable oratb = new DataTable("otb");
                ada.Fill(oratb);
                ada.Dispose();
                conn.Close();
                return oratb;
            }
            catch (Exception ex)
            {
                ada.Dispose();
                conn.Close();
                throw new Exception("SQL=" + sql + " Exception=" + ex.Message);
            }
        }
        private static string DealInformixSQL(string sql)
        {
            if (sql.Contains("?") == false)
                return sql;

            string mysql = "";
            if (sql.Contains("? ") == true || sql.Contains("?,") == true)
            {
                /*如果有空格,说明已经替换过了。*/
                return sql;
            }
            else
            {
                sql += " ";
                /*说明需要处理的变量.*/
                string[] strs = sql.Split('?');
                mysql = strs[0];
                for (int i = 1; i < strs.Length; i++)
                {
                    string str = strs[i];
                    switch (str.Substring(0, 1))
                    {
                        case " ":
                            mysql += "?" + str;
                            break;
                        case ")":
                            mysql += "?" + str;
                            break;
                        case ",":
                            mysql += "?" + str;
                            break;
                        default:
                            char[] chs = str.ToCharArray();
                            foreach (char c in chs)
                            {
                                if (c == ',')
                                {
                                    int idx1 = str.IndexOf(",");
                                    mysql += "?" + str.Substring(idx1);
                                    break;
                                }

                                if (c == ')')
                                {
                                    int idx1 = str.IndexOf(")");
                                    mysql += "?" + str.Substring(idx1);
                                    break;
                                }

                                if (c == ' ')
                                {
                                    int idx1 = str.IndexOf(" ");
                                    mysql += "?" + str.Substring(idx1);
                                    break;
                                }
                            }

                            //else
                            //{
                            //    mysql += "?" + str;
                            //}

                            //if (str.Contains(")") == true)
                            //    mysql += "?" + str.Substring(str.IndexOf(")"));
                            //else
                            //    mysql += "?" + str;
                            break;
                    }
                }
            }
            return mysql;
        }
        /// <summary>
        /// RunSQLReturnTable_200705_Informix
        /// </summary>
        /// <param name="selectSQL">要执行的sql</param>
        /// <returns>返回table</returns>
        private static DataTable RunSQLReturnTable_201205_Informix(string sql, Paras paras)
        {
            //if (paras.Count != 0 && sql.Contains("?") == false)
            //{
            //    sql = DealInformixSQL(sql);
            //}
            sql = DealInformixSQL(sql);

            IfxConnection conn = new IfxConnection(SystemConfig.AppCenterDSN);
            if (conn.State != ConnectionState.Open)
                conn.Open();

            IfxDataAdapter ada = new IfxDataAdapter(sql, conn);
            ada.SelectCommand.CommandType = CommandType.Text;

            // 加入参数
            foreach (Para para in paras)
            {
                IfxParameter myParameter = new IfxParameter(para.ParaName,para.val);
                myParameter.Size = para.Size;
                ada.SelectCommand.Parameters.Add(myParameter);
            }

            try
            {
                DataTable oratb = new DataTable("otb");
                ada.Fill(oratb);
                ada.Dispose();
                conn.Close();
                return oratb;
            }
            catch (Exception ex)
            {
                ada.Dispose();
                conn.Close();
                Log.DefaultLogWriteLineError(sql);
                Log.DefaultLogWriteLineError(ex.Message);

                throw new Exception("SQL=" + sql + " Exception=" + ex.Message);
            }
            finally
            {
                ada.Dispose();
                conn.Close();
            }
        }
        /// <summary>
        /// RunSQLReturnTable_200705_SQL
        /// </summary>
        /// <param name="selectSQL">要执行的sql</param>
        /// <returns>返回table</returns>
        private static DataTable RunSQLReturnTable_200705_MySQL(string selectSQL)
        {
            return RunSQLReturnTable_200705_MySQL(selectSQL, new Paras());
        }
        /// <summary>
        /// RunSQLReturnTable_200705_SQL
        /// </summary>
        /// <param name="selectSQL">要执行的sql</param>
        /// <returns>返回table</returns>
        private static DataTable RunSQLReturnTable_200705_MySQL(string sql, Paras paras)
        {
          //  string mcs = "Data Source=127.0.0.1;User ID=root;Password=root;DataBase=wk;Charset=gb2312;";
          //  MySqlConnection conn = new MySqlConnection(SystemConfig.AppCenterDSN);
          //  SqlDataAdapter ad = new SqlDataAdapter("select username,password from person", conn);
          //  DataTable dt = new DataTable();
          //  conn.Open();
          //  ad.Fill(dt);
          //  conn.Close();
          //  return dt;

            MySqlConnection conn = new MySqlConnection(SystemConfig.AppCenterDSN);
            if (conn.State != ConnectionState.Open)
                conn.Open();

            MySqlDataAdapter ada = new MySqlDataAdapter(sql, conn);
            ada.SelectCommand.CommandType = CommandType.Text;
           
            // 加入参数
            foreach (Para para in paras)
            {
                MySqlParameter myParameter = new MySqlParameter(para.ParaName, para.val);
                myParameter.Size = para.Size;
                ada.SelectCommand.Parameters.Add(myParameter);
            }

            try
            {
                DataTable oratb = new DataTable("otb");
                ada.Fill(oratb);
                ada.Dispose();

                conn.Close();
                conn.Dispose();
                return oratb;
            }
            catch (Exception ex)
            {
                ada.Dispose();
                conn.Close();
                throw new Exception("SQL=" + sql + " Exception=" + ex.Message);
            }
        }
        /// <summary>
        /// RunSQLReturnTable_200705_SQL
        /// </summary>
        /// <param name="selectSQL">要执行的sql</param>
        /// <returns>返回table</returns>
        private static DataTable RunSQLReturnTable_201205_Informix(string selectSQL)
        {
            return RunSQLReturnTable_201205_Informix(selectSQL, new Paras());
        }
        /// <summary>
        /// RunSQLReturnTable_200705_SQL
        /// </summary>
        /// <param name="selectSQL">要执行的sql</param>
        /// <returns>返回table</returns>
        private static DataTable RunSQLReturnTable_200705_OLE(string selectSQL, Paras paras)
        {
            OleDbConnection conn = new OleDbConnection(SystemConfig.AppCenterDSN);
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                OleDbDataAdapter ada = new OleDbDataAdapter(selectSQL, conn);
                ada.SelectCommand.CommandType = CommandType.Text;

                // 加入参数
                foreach (Para para in paras)
                {
                    OleDbParameter myParameter = new OleDbParameter(para.ParaName, para.DATypeOfOra);
                    myParameter.Size = para.Size;
                    myParameter.Value = para.val;
                    ada.SelectCommand.Parameters.Add(myParameter);
                }

                DataTable oratb = new DataTable("otb");
                ada.Fill(oratb);
                ada.Dispose();

                conn.Close();

                return oratb;
            }
            catch (System.Exception ex)
            {
                conn.Close();
                string msg = "@RunSQLReturnTable_200705_OLE with paras) Error sql=" + selectSQL + " @Messages：" + ex.Message;
                msg += "@Para Num= " + paras.Count;
                foreach (Para pa in paras)
                {
                    msg += "@" + pa.ParaName + "=" + pa.val + " type=" + pa.DAType.ToString();
                }
                Log.DebugWriteError(msg);
                throw new Exception(msg);
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion


        #endregion

        #region 在当前Connection上执行
        public static DataTable RunSQLReturnTable(Paras ps)
        {
            return RunSQLReturnTable(ps.SQL, ps);
        }
        public static int RunSQLReturnTableCount = 0;
        /// <summary>
        /// 传递一个select 语句返回一个查询结果集合。
        /// </summary>
        /// <param name="sql">select sql</param>
        /// <returns>查询结果集合DataTable</returns>
        public static DataTable RunSQLReturnTable(string sql)
        {
            Paras ps = new Paras();
            return RunSQLReturnTable(sql, ps);
        }
        public static DataTable RunSQLReturnTable(string sql, string key1, object v1,string key2,object v2)
        {
            Paras ens = new Paras();
            ens.Add(key1, v1);
            ens.Add(key2, v2);
            return RunSQLReturnTable(sql, ens);
        }
        public static DataTable RunSQLReturnTable(string sql, string key, object val)
        {
            Paras ens = new Paras();
            ens.Add(key, val);
            return RunSQLReturnTable(sql, ens);
        }
        private static bool lockRunSQLReTable = false;
        public static DataTable RunSQLReturnTable(string sql, Paras paras)
        {
            if (sql == null || sql.Length == 0)
                throw new Exception("要执行的 sql =null ");

            while (lockRunSQLReTable)
            {
            }

            try
            {
                lockRunSQLReTable = true;
                DataTable dt = null;
                switch (AppCenterDBType)
                {
                    case DBType.SQL2000:
                        dt = RunSQLReturnTable_200705_SQL(sql, paras);
                        break;
                    case DBType.Oracle9i:
                        dt = RunSQLReturnTable_200705_Ora(sql, paras);
                        break;
                    case DBType.Informix:
                        dt = RunSQLReturnTable_201205_Informix(sql, paras);
                        break;
                    case DBType.MySQL:
                        dt = RunSQLReturnTable_200705_MySQL(sql, paras);
                        break;
                    case DBType.Access:
                        dt = RunSQLReturnTable_200705_OLE(sql, paras);
                        break;
                    default:
                        throw new Exception("@发现未知的数据库连接类型！");
                }
                lockRunSQLReTable = false;
                return dt;
            }
            catch (Exception ex)
            {
                lockRunSQLReTable = false;
                throw ex;
            }
        }
        /// <summary>
        /// 传递一个select 语句返回一个查询DataSet集合
        /// </summary>
        /// <param name="sql">select sql</param>
        /// <returns>查询结果集合DataSet</returns>
        public static DataSet RunSQLReturnDataSet(string sql)
        {
            DataTable dt = RunSQLReturnTable(sql);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds;
        }
        #endregion 在当前Connection上执行

        #endregion

        #region 查询单个值的方法.

        #region OleDbConnection
        public static float RunSQLReturnValFloat(Paras ps)
        {
            return RunSQLReturnValFloat(ps.SQL, ps, 0);
        }
        public static float RunSQLReturnValFloat(string sql, Paras ps, float val)
        {
            ps.SQL=sql;
            object obj = DA.DBAccess.RunSQLReturnVal(ps);

            try
            {
                if (obj == null || obj.ToString() == "")
                    return val;
                else
                    return float.Parse(obj.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + sql + " @OBJ=" + obj);
            }
        }
        /// <summary>
        /// sdfsd
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static float RunSQLReturnValFloat(string sql, float val)
        {
            return RunSQLReturnValFloat(sql, new Paras(), val);
        }
        /// <summary>
        /// sdfsd
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static float RunSQLReturnValFloat(string sql)
        {
            try
            {
                return float.Parse(DA.DBAccess.RunSQLReturnVal(sql).ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + sql);
            }
        }
        public static int RunSQLReturnValInt(Paras ps, int IsNullReturnVal)
        {
            return RunSQLReturnValInt(ps.SQL, ps, IsNullReturnVal);
        }
        /// <summary>
        /// sdfsd
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="IsNullReturnVal"></param>
        /// <returns></returns>
        public static int RunSQLReturnValInt(string sql, int IsNullReturnVal)
        {
            object obj = "";
            obj = DA.DBAccess.RunSQLReturnVal(sql);
            if (obj == null || obj.ToString() == "" || obj == DBNull.Value)
                return IsNullReturnVal;
            else
                return Convert.ToInt32(obj);

            
        }
        public static int RunSQLReturnValInt(string sql, int IsNullReturnVal, Paras paras)
        {
            object obj = "";

            obj = DA.DBAccess.RunSQLReturnVal(sql, paras);
            if (obj == null || obj.ToString() == "")
                return IsNullReturnVal;
            else
                return Convert.ToInt32(obj);
        }
        public static decimal RunSQLReturnValDecimal(string sql, decimal IsNullReturnVal, int blws)
        {
            Paras ps = new Paras();
            ps.SQL=sql;
            return RunSQLReturnValDecimal(ps, IsNullReturnVal, blws);
        }
        public static decimal RunSQLReturnValDecimal(Paras ps, decimal IsNullReturnVal, int blws)
        {
            try
            {
                object obj = DA.DBAccess.RunSQLReturnVal(ps);
                if (obj == null || obj.ToString() == "")
                    return IsNullReturnVal;
                else
                {
                    decimal d = decimal.Parse(obj.ToString());
                    return decimal.Round(d, blws);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ps.SQL );
            }
        }
        public static int RunSQLReturnValInt(Paras ps)
        {
            string str = DBAccess.RunSQLReturnString(ps.SQL, ps);
            if (str.Contains("."))
                str = str.Substring(0, str.IndexOf("."));
            try
            {
                return Convert.ToInt32(str);
            }
            catch (Exception ex)
            {
                throw new Exception("@" + ps.SQL + "   Val=" + str + ex.Message);
            }
        }
        public static int RunSQLReturnValInt(string sql)
        {
            object obj = DBAccess.RunSQLReturnVal(sql);
            if (obj == null || obj == DBNull.Value )
                throw new Exception("@没有获取您要查询的数据,请检查SQL:" + sql + " @关于查询出来的详细信息已经记录日志文件，请处理。");
            string s = obj.ToString();
            if (s.Contains("."))
                s = s.Substring(0, s.IndexOf("."));
            return Convert.ToInt32(s);
        }
        public static int RunSQLReturnValInt(string sql, Paras paras)
        {
            return Convert.ToInt32(DA.DBAccess.RunSQLReturnVal(sql, paras));
        }
        public static int RunSQLReturnValInt(string sql, Paras paras, int isNullAsVal)
        {
            try
            {
                return Convert.ToInt32(DA.DBAccess.RunSQLReturnVal(sql, paras));
            }
            catch
            {
                return isNullAsVal;
            }
        }
        /// <summary>
        /// 查询单个值的方法
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="conn">OleDbConnection</param>
        /// <returns>object</returns>
        public static object RunSQLReturnVal(string sql, OleDbConnection conn, string dsn)
        {
            return RunSQLReturnVal(sql, conn, CommandType.Text, dsn);
        }

        public static string RunSQLReturnString(string sql, Paras ps)
        {
            if (ps == null)
                ps = new Paras();

            object obj = DBAccess.RunSQLReturnVal(sql, ps);
            if (obj == DBNull.Value || obj ==null )
                return null;
            else
                return obj.ToString();
        }
        /// <summary>
        /// 执行查询返回结果,如果为dbNull 返回 null.
        /// </summary>
        /// <param name="sql">will run sql.</param>
        /// <returns>,如果为dbNull 返回 null.</returns>
        public static string RunSQLReturnString(string sql)
        {
            try
            {
                return RunSQLReturnString(sql, new Paras());
            }
            catch (Exception ex)
            {
                throw new Exception("@运行 RunSQLReturnString出现错误：" + ex.Message + sql);
            }
        }
        public static string RunSQLReturnStringIsNull(Paras ps, string isNullAsVal)
        {
            string v = RunSQLReturnString(ps);
            if (v == null)
                return isNullAsVal;
            else
                return v;
        }
        public static string RunSQLReturnStringIsNull(string sql,string isNullAsVal)
        {
            try
            {
                string s= RunSQLReturnString(sql, new Paras());
                if (s == null)
                    return isNullAsVal;
                return s;
            }
            catch (Exception ex)
            {
                return isNullAsVal;
               // throw new Exception("@运行 RunSQLReturnString出现错误：" + ex.Message + sql);
            }
        }
        public static string RunSQLReturnString(Paras ps)
        {
            return RunSQLReturnString(ps.SQL,ps );
        }
        /// <summary>
        /// 查询单个值的方法
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="conn">OleDbConnection</param>
        /// <param name="sqlType">CommandType</param>
        /// <param name="pars">pars</param>
        /// <returns>object</returns>
        public static object RunSQLReturnVal(string sql, OleDbConnection conn, CommandType sqlType, params object[] pars)
        {

#if DEBUG
            Debug.WriteLine(sql);
#endif
            OleDbConnection tmpconn = new OleDbConnection(conn.ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sql, tmpconn);
            object val = null;
            try
            {
                tmpconn.Open();
                cmd.CommandType = sqlType;
                foreach (object par in pars)
                {
                    cmd.Parameters.AddWithValue("par", par);
                }
                val = cmd.ExecuteScalar();
            }
            catch (System.Exception ex)
            {
                cmd.Cancel();
                tmpconn.Close();
                cmd.Dispose();
                tmpconn.Dispose();
                throw new Exception(ex.Message + " [RunSQLReturnVal on OleDbConnection] " + sql);
            }
            tmpconn.Close();
            return val;
        }
        #endregion

        #region SqlConnection
        /// <summary>
        /// 查询单个值的方法
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="conn">SqlConnection</param>
        /// <returns>object</returns>
        public static object RunSQLReturnVal(string sql, SqlConnection conn, string dsn)
        {
            return RunSQLReturnVal(sql, conn, CommandType.Text, dsn);

        }
        /// <summary>
        /// 查询单个值的方法
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="conn">SqlConnection</param>
        /// <param name="sqlType">CommandType</param>
        /// <param name="pars">pars</param>
        /// <returns>object</returns>
        public static object RunSQLReturnVal(string sql, SqlConnection conn, CommandType sqlType, string dsn, params object[] pars)
        {
            //return DBAccess.RunSQLReturnTable(sql,conn,dsn,sqlType,null).Rows[0][0];

#if DEBUG
            Debug.WriteLine(sql);
#endif

            object val = null;
            SqlCommand cmd = null;

            try
            {
                if (conn == null)
                {
                    conn = new SqlConnection(dsn);
                    conn.Open();
                }

                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.ConnectionString = dsn;
                    conn.Open();
                }

                cmd = new SqlCommand(sql, conn);
                cmd.CommandType = sqlType;
                val = cmd.ExecuteScalar();
            }
            catch (System.Exception ex)
            {
                //return DBAccess.re

                cmd.Cancel();
                conn.Close();
                cmd.Dispose();
                conn.Dispose();
                throw new Exception(ex.Message + " [RunSQLReturnVal on SqlConnection] " + sql);
            }
            //conn.Close();
            return val;
        }
        #endregion



    
        #region 在当前的Connection执行 SQL 语句，返回首行首列
        public static int RunSQLReturnCOUNT(string sql)
        {
            return RunSQLReturnTable(sql).Rows.Count;
            //return RunSQLReturnVal( sql ,sql, sql );
        }
        public static object RunSQLReturnVal(string sql, string pkey, object val)
        {
            Paras ps = new Paras();
            ps.Add(pkey, val);

            return RunSQLReturnVal(sql, ps);
        }

        public static object RunSQLReturnVal(string sql,Paras paras)
        {
            RunSQLReturnTableCount++;
          //  Log.DebugWriteInfo("NUMOF " + RunSQLReturnTableCount + "===RunSQLReturnTable sql=" + sql);
            DataTable dt = null;
            switch (SystemConfig.AppCenterDBType)
            {
                case DBType.Oracle9i:
                    dt = DBAccess.RunSQLReturnTable_200705_Ora(sql, paras);
                    break;
                case DBType.SQL2000:
                    dt = DBAccess.RunSQLReturnTable_200705_SQL(sql, paras);
                    break;
                case DBType.MySQL:
                    dt = DBAccess.RunSQLReturnTable_200705_MySQL(sql, paras);
                    break;
                case DBType.Informix:
                    dt = DBAccess.RunSQLReturnTable_201205_Informix(sql, paras);
                    break;
                case DBType.Access:
                    dt = DBAccess.RunSQLReturnTable_200705_OLE(sql, paras);
                    break;
                default:
                    throw new Exception("@没有判断的数据库类型");
            }

            if (dt.Rows.Count == 0)
                return null;
            return dt.Rows[0][0];
        }
        public static object RunSQLReturnVal(Paras ps)
        {
            return RunSQLReturnVal(ps.SQL, ps);
        }

        public static object RunSQLReturnVal(string sql)
        {
            RunSQLReturnTableCount++;
            DataTable dt = null;
            switch (SystemConfig.AppCenterDBType)
            {
                case DBType.Oracle9i:
                    dt = DBAccess.RunSQLReturnTable_200705_Ora(sql, new Paras());
                    break;
                case DBType.SQL2000:
                    dt = DBAccess.RunSQLReturnTable_200705_SQL(sql, new Paras());
                    break;
                case DBType.Informix:
                    dt = DBAccess.RunSQLReturnTable_201205_Informix(sql, new Paras());
                    break;
                case DBType.MySQL:
                    dt = DBAccess.RunSQLReturnTable_200705_MySQL(sql, new Paras());
                    break;
                case DBType.Access:
                    dt = DBAccess.RunSQLReturnTable_200705_OLE(sql, new Paras());
                    break;
                default:
                    throw new Exception("@没有判断的数据库类型");
            }
            if (dt.Rows.Count == 0)
            {
             
#warning 不应该出现的异常 2011-12-03 
                string cols = "";
                foreach (DataColumn dc in dt.Columns)
                    cols += " , " + dc.ColumnName;

                BP.DA.Log.DebugWriteInfo("@SQL="+sql+" . 列="+cols);
                return null;
            }
            return dt.Rows[0][0];
        }
        #endregion

        #endregion

        #region 检查是不是存在
        /// <summary>
        /// 检查是不是存在
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns>检查是不是存在</returns>
        public static bool IsExits(string sql)
        {
            if (RunSQLReturnVal(sql) == null)
                return false;
            return true;
        }
        public static bool IsExits(string sql, Paras ps)
        {
            if (RunSQLReturnVal(sql, ps) == null)
                return false;
            return true;
        }
        /// <summary>
        /// 判断是否存在主键pk .
        /// </summary>
        /// <param name="tab">物理表</param>
        /// <returns>是否存在</returns>
        public static bool IsExitsTabPK(string tab)
        {
            BP.DA.Paras ps = new Paras();
            ps.Add("Tab", tab);
            string sql = "";
            switch (AppCenterDBType)
            {
                case DBType.Access:
                    return false;
                case DBType.SQL2000:
                    sql = "SELECT column_name, table_name,CONSTRAINT_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE table_name =@Tab ";
                    break;
                case DBType.Oracle9i:
                    sql = "SELECT constraint_name, constraint_type,search_condition, r_constraint_name  from user_constraints WHERE table_name = upper(:tab) AND constraint_type = 'P'";
                    break;
                case DBType.MySQL:
                    sql = "SELECT column_name, table_name, CONSTRAINT_NAME from INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE table_name =@Tab ";
                    break;
                case DBType.Informix:
                    sql = "SELECT * FROM sysconstraints c inner join systables t on c.tabid = t.tabid where t.tabname = lower(?) and constrtype = 'P'";
                    break;
                default:
                    throw new Exception("ssseerr ");
            }

            DataTable dt = DBAccess.RunSQLReturnTable(sql, ps);
            if (dt.Rows.Count >= 1)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 判断系统中是否存在对象.
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static bool IsExitsObject(string obj)
        {
            Paras ps = new Paras();
            ps.Add("obj", obj);

            switch (AppCenterDBType)
            {
                case DBType.Oracle9i:
                    if (obj.IndexOf(".") != -1)
                        obj = obj.Split('.')[1];
                    return IsExits("select tname from tab WHERE  tname = upper(:obj) ", ps);
                case DBType.SQL2000:
                    return IsExits("SELECT name  FROM sysobjects  WHERE  name = '" + obj + "'");
                case DBType.Informix:
                    return IsExits("select tabname from systables where tabname = '" + obj.ToLower() + "'");
                case DBType.MySQL:
                    if (obj.IndexOf(".") != -1)
                        obj = obj.Split('.')[1];
                    return IsExits("SELECT table_name, table_type FROM information_schema.tables  WHERE table_name = '" + obj + "'");
                case DBType.Access:
                    //return false ; //IsExits("SELECT * FROM MSysObjects WHERE (((MSysObjects.Name) =  '"+obj+"' ))");
                    return IsExits("SELECT * FROM MSysObjects WHERE Name =  '" + obj + "'");
                default:
                    throw new Exception("没有识别的数据库编号");
            }
        }

//        public static bool ss( string tbname, k)
//        {
//'功能：获取数据表键列字段名称
//'参数：tbname---表名；ktype---键类型（1为主键，2为外键，3为唯一键）
    
    


//Dim cnn As New ADODB.Connection
//Dim cat As New ADOX.Catalog
//Dim tbl As ADOX.Table
//Dim i As Long, j As Long
//Set cnn = CurrentProject.Connection
//Set cat.ActiveConnection = cnn
//Set tbl = cat.Tables(tbname)
//For i = 0 To tbl.Keys.Count - 1
//    If tbl.Keys(i).Type = ktype Then
//    For j = 0 To tbl.Keys(i).Columns.Count - 1
//    Keyname = Keyname & tbl.Keys(i).Columns(j).Name & ";"
//    Next
//    End If
//Next
//End Function  
//        }
        
        public static bool IsExitsTableCol(string table, string col)
        {
            Paras ps = new Paras();
            ps.Add("tab", table);
            ps.Add("col", col);

            int i = 0;
            switch (DBAccess.AppCenterDBType)
            {
                case DBType.Access:
                    return false;
                    break;
                case DBType.SQL2000:
                    i = DBAccess.RunSQLReturnValInt("SELECT  COUNT(*)  FROM information_schema.COLUMNS  WHERE TABLE_NAME='"+table+"' AND COLUMN_NAME='"+col+"'", 0);
                    break;
                case DBType.MySQL:
                    i = DBAccess.RunSQLReturnValInt("SELECT  COUNT(*)  FROM information_schema.COLUMNS  WHERE TABLE_NAME='" + table + "' AND COLUMN_NAME='" + col + "'", 0);
                    break;
                case DBType.Oracle9i:
                    if (table.IndexOf(".") != -1)
                        table = table.Split('.')[1];
                    i = DBAccess.RunSQLReturnValInt("SELECT COUNT(*) from user_tab_columns  WHERE table_name= upper(:tab) AND column_name= upper(:col) ", ps);
                    break;
                case DBType.Informix:
                    i = DBAccess.RunSQLReturnValInt("select count(*) from syscolumns c where tabid in (select tabid	from systables	where tabname = lower('" + table + "')) and c.colname = lower('" + col + "')", 0);
                    break;
                default:
                    throw new Exception("error");
            }

            if (i == 1)
                return true;
            else
                return false;
        }
        #endregion

        #region LoadConfig
        public static void LoadConfig(string cfgFile, string basePath)
        {
            if (!File.Exists(cfgFile))
                throw new Exception("找不到配置文件==>[" + cfgFile + "]1");

            StreamReader read = new StreamReader(cfgFile);
            string firstline = read.ReadLine();
            string cfg = read.ReadToEnd();
            read.Close();

            int start = cfg.ToLower().IndexOf("<appsettings>");
            int end = cfg.ToLower().IndexOf("</appsettings>");

            cfg = cfg.Substring(start, end - start + "</appsettings".Length + 1);

            cfgFile = basePath + "\\__$AppConfig.cfg";
            StreamWriter write = new StreamWriter(cfgFile);
            write.WriteLine(firstline);
            write.Write(cfg);
            write.Flush();
            write.Close();

            DataSet dscfg = new DataSet("cfg");
            try
            {
                dscfg.ReadXml(cfgFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }

          //  BP.SystemConfig.CS_AppSettings = new System.Collections.Specialized.NameValueCollection();
            BP.SystemConfig.CS_DBConnctionDic.Clear();
            foreach (DataRow row in dscfg.Tables["add"].Rows)
            {
                BP.SystemConfig.CS_AppSettings.Add(row["key"].ToString().Trim(), row["value"].ToString().Trim());
            }
            dscfg.Dispose();

            BP.SystemConfig.IsBSsystem = false;
        }


        #endregion
    }

	#region ODBC
    public class DBAccessOfODBC
    {
        /// <summary>
        /// 检查是不是存在
        /// </summary>
        public static bool IsExits(string selectSQL)
        {
            if (RunSQLReturnVal(selectSQL) == null)
                return false;
            return true;
        }

        #region 取得连接对象 ，CS、BS共用属性【关键属性】
        public static OdbcConnection GetSingleConn
        {
            get
            {
                if (SystemConfig.IsBSsystem_Test)
                {
                    OdbcConnection conn = HttpContext.Current.Session["DBAccessOfODBC"] as OdbcConnection;
                    if (conn == null)
                    {
                        conn = new OdbcConnection(SystemConfig.AppSettings["DBAccessOfODBC"]);
                        HttpContext.Current.Session["DBAccessOfODBC"] = conn;
                    }
                    return conn;
                }
                else
                {
                    OdbcConnection conn = SystemConfig.CS_DBConnctionDic["DBAccessOfODBC"] as OdbcConnection;
                    if (conn == null)
                    {
                        conn = new OdbcConnection(SystemConfig.AppSettings["DBAccessOfODBC"]);
                        SystemConfig.CS_DBConnctionDic["DBAccessOfODBC"] = conn;
                    }
                    return conn;
                }
            }
        }
        #endregion 取得连接对象 ，CS、BS共用属性


        #region 重载 RunSQLReturnTable

        #region 使用本地的连接
        public static DataTable RunSQLReturnTable(string sql)
        {
            return RunSQLReturnTable(sql, GetSingleConn, CommandType.Text);
        }
        public static DataTable RunSQLReturnTable(string sql, CommandType sqlType, params object[] pars)
        {
            return RunSQLReturnTable(sql, GetSingleConn, sqlType, pars);
        }

        #endregion

        #region 使用指定的连接
        public static DataTable RunSQLReturnTable(string sql, OdbcConnection conn)
        {
            return RunSQLReturnTable(sql, conn, CommandType.Text);
        }
        public static DataTable RunSQLReturnTable(string sql, OdbcConnection conn, CommandType sqlType, params object[] pars)
        {
            try
            {
                OdbcDataAdapter ada = new OdbcDataAdapter(sql, conn);
                ada.SelectCommand.CommandType = sqlType;
                foreach (object par in pars)
                {
                    ada.SelectCommand.Parameters.AddWithValue("par", par);
                }
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                DataTable dt = new DataTable("tb");
                ada.Fill(dt);
                // peng add 
                ada.Dispose();
                return dt;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message + sql);
            }
        }
        #endregion

        #endregion

        #region 重载 RunSQL

        #region 使用本地的连接
        public static int RunSQLReturnCOUNT(string sql)
        {
            return RunSQLReturnTable(sql).Rows.Count;
            //return RunSQLReturnVal( sql ,sql, sql );
        }
        public static int RunSQL(string sql)
        {
            return RunSQL(sql, GetSingleConn, CommandType.Text);
        }
        public static int RunSQL(string sql, CommandType sqlType, params object[] pars)
        {
            return RunSQL(sql, GetSingleConn, sqlType, pars);
        }
        #endregion 使用本地的连接

        #region 使用指定的连接
        public static int RunSQL(string sql, OdbcConnection conn)
        {
            return RunSQL(sql, conn, CommandType.Text);
        }
        public static int RunSQL(string sql, OdbcConnection conn, CommandType sqlType, params object[] pars)
        {
            Debug.WriteLine(sql);
            try
            {
                OdbcCommand cmd = new OdbcCommand(sql, conn);
                cmd.CommandType = sqlType;
                foreach (object par in pars)
                {
                    cmd.Parameters.AddWithValue("par", par);
                }
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }
                return cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message + sql);
            }
        }

        #endregion 使用指定的连接

        #endregion

        #region 执行SQL ，返回首行首列

        /// <summary>
        /// 运行select sql, 返回一个值。
        /// </summary>
        /// <param name="sql">select sql</param>
        /// <returns>返回的值object</returns>
        public static float RunSQLReturnFloatVal(string sql)
        {
            return (float)RunSQLReturnVal(sql, GetSingleConn, CommandType.Text);
        }
        public static int RunSQLReturnValInt(string sql)
        {
            return (int)RunSQLReturnVal(sql, GetSingleConn, CommandType.Text);
        }
        /// <summary>
        /// 运行select sql, 返回一个值。
        /// </summary>
        /// <param name="sql">select sql</param>
        /// <returns>返回的值object</returns>
        public static object RunSQLReturnVal(string sql)
        {
            return RunSQLReturnVal(sql, GetSingleConn, CommandType.Text);
        }
        /// <summary>
        /// 运行select sql, 返回一个值。
        /// </summary>
        /// <param name="sql">select sql</param>
        /// <param name="sqlType">CommandType</param>
        /// <param name="pars">params</param>
        /// <returns>返回的值object</returns>
        public static object RunSQLReturnVal(string sql, CommandType sqlType, params object[] pars)
        {
            return RunSQLReturnVal(sql, GetSingleConn, sqlType, pars);
        }
        public static object RunSQLReturnVal(string sql, OdbcConnection conn)
        {
            return RunSQLReturnVal(sql, conn, CommandType.Text);
        }
        public static object RunSQLReturnVal(string sql, OdbcConnection conn, CommandType sqlType, params object[] pars)
        {
            Debug.WriteLine(sql);
            OdbcConnection tmp = new OdbcConnection(conn.ConnectionString);
            OdbcCommand cmd = new OdbcCommand(sql, tmp);
            object val = null;
            try
            {
                cmd.CommandType = sqlType;
                foreach (object par in pars)
                {
                    cmd.Parameters.AddWithValue("par", par);
                }
                tmp.Open();
                val = cmd.ExecuteScalar();
            }
            catch (System.Exception ex)
            {
                tmp.Close();
                throw new Exception(ex.Message + sql);
            }
            tmp.Close();
            return val;
        }
        #endregion 执行SQL ，返回首行首列

    }
	#endregion

	/// <summary>
	/// 关于OLE 的连接
	/// </summary>
	public class DBAccessOfOLE
	{
		/// <summary>
		/// 检查是不是存在
		/// </summary>
		/// <param name="selectSQL"></param>
		/// <returns>检查是不是存在</returns>
		public static bool IsExits(string selectSQL)
		{
			if ( RunSQLReturnVal(selectSQL) == null)
				return false;
			return true;
		}


		#region 取得连接对象 ，CS、BS共用属性【关键属性】
		public static OleDbConnection GetSingleConn
		{
			get
			{
                if (SystemConfig.IsBSsystem_Test)
				{
					OleDbConnection conn = HttpContext.Current.Session["DBAccessOfOLE"] as OleDbConnection;
					if ( conn==null )
					{
						conn = new OleDbConnection( SystemConfig.DBAccessOfOLE );
						HttpContext.Current.Session[ "DBAccessOfOLE" ] = conn;
					}
					return conn;
				}
				else
				{
					OleDbConnection conn = SystemConfig.CS_DBConnctionDic["DBAccessOfOLE"] as OleDbConnection;
					if ( conn==null )
					{
						conn = new OleDbConnection( SystemConfig.DBAccessOfOLE  );
						SystemConfig.CS_DBConnctionDic[ "DBAccessOfOLE" ] = conn;
					}
					return conn;
				}
			}
		}
		#endregion 取得连接对象 ，CS、BS共用属性


		#region 重载 RunSQLReturnTable

		#region 使用本地的连接
		public static int RunSQLReturnCOUNT(string sql)
		{
			return RunSQLReturnTable(sql).Rows.Count;
			//return RunSQLReturnVal( sql ,sql, sql );
		}
		/// <summary>
		/// 运行查询语句返回Table
		/// </summary>
		/// <param name="sql">select sql</param>
		/// <returns>DataTable</returns>
		public static DataTable RunSQLReturnTable(string sql )
		{
			return RunSQLReturnTable( sql , GetSingleConn ,CommandType.Text );
		}
		/// <summary>
		/// 运行查询语句返回Table
		/// </summary>
		/// <param name="sql">select sql</param>
		/// <param name="sqlType">CommandType</param>
		/// <param name="pars">pareas</param>
		/// <returns>DataTable</returns>
		public static DataTable RunSQLReturnTable(string sql ,CommandType sqlType ,params object[] pars)
		{
			return RunSQLReturnTable( sql , GetSingleConn ,sqlType ,pars);
		}	
		#endregion 

		#region 使用指定的连接
		public static DataTable RunSQLReturnTable(string sql , OleDbConnection conn )
		{
			return RunSQLReturnTable( sql ,conn ,CommandType.Text );
		}
		public static DataTable RunSQLReturnTable(string sql , OleDbConnection conn ,CommandType sqlType ,params object[] pars)
		{
			try
			{
				OleDbDataAdapter ada = new OleDbDataAdapter( sql ,conn);
				ada.SelectCommand.CommandType = sqlType;
				foreach(object par in pars)
				{
                    ada.SelectCommand.Parameters.AddWithValue("par", par);
				}
				if (conn.State != ConnectionState.Open)
					conn.Open();
				DataTable dt = new DataTable("tb");
				ada.Fill( dt );
				ada.Dispose();
				return dt;
			}
			catch(System.Exception ex)
			{
				throw new Exception(ex.Message + sql);
			}
		}
		#endregion 

		#endregion

		#region 重载 RunSQL

		#region 使用本地的连接
		public static int RunSQL(string sql )
		{
			return RunSQL( sql , GetSingleConn ,CommandType.Text);
		}
		public static int RunSQL(string sql ,CommandType sqlType ,params object[] pars)
		{
			return RunSQL( sql , GetSingleConn ,sqlType , pars);
		}
		#endregion 使用本地的连接

		#region 使用指定的连接
		public static int RunSQL(string sql ,OleDbConnection conn )
		{
			return RunSQL( sql , conn ,CommandType.Text );
		}
		public static int RunSQL(string sql ,OleDbConnection conn ,CommandType sqlType ,params object[] pars)
		{
			Debug.WriteLine( sql );
			try
			{
				OleDbCommand cmd = new OleDbCommand( sql ,conn);
				cmd.CommandType = sqlType;
				foreach(object par in pars)
				{
                    cmd.Parameters.AddWithValue("par", par);
				}

				if (conn.State != System.Data.ConnectionState.Open)
					conn.Open();
				return cmd.ExecuteNonQuery();				 
			}
			catch(System.Exception ex)
			{
				throw new Exception(ex.Message + sql );
			}
		}

		#endregion 使用指定的连接

		#endregion 

		#region 执行SQL ，返回首行首列
		public static object RunSQLReturnVal(string sql )
		{
			return RunSQLReturnVal( sql ,GetSingleConn ,CommandType.Text );
		}
		public static object RunSQLReturnVal(string sql ,CommandType sqlType ,params object[] pars)
		{
			return RunSQLReturnVal( sql ,GetSingleConn , sqlType ,pars );
		}

		public static object RunSQLReturnVal(string sql ,OleDbConnection conn )
		{
			return RunSQLReturnVal( sql ,conn ,CommandType.Text );
		}
		public static object RunSQLReturnVal(string sql ,OleDbConnection conn ,CommandType sqlType ,params object[] pars)
		{
			Debug.WriteLine( sql );
			OleDbConnection tmpconn = new OleDbConnection(conn.ConnectionString);
			OleDbCommand cmd = new OleDbCommand( sql ,tmpconn);
			object val = null;
			try
			{
				cmd.CommandType = sqlType;
                foreach (object par in pars)
                {
                    cmd.Parameters.AddWithValue("par", par);
                }
				tmpconn.Open();
				val= cmd.ExecuteScalar();
			}
			catch(System.Exception ex)
			{
				cmd.Cancel();
				tmpconn.Close();
				throw new Exception(ex.Message + sql );
			}
			tmpconn.Close();
			return val;
		}
		#endregion 执行SQL ，返回首行首列
	}
	/// <summary>
	/// 关于DBAccessOfSQLServer2000 的连接
	/// </summary>
	public class DBAccessOfMSSQL2000
	{
		#region 关于运行存储过程


        public static object RunSPReObj(string spName, Paras paras)
        {

            SqlConnection conn = DBAccessOfMSSQL2000.GetSingleConn;

            SqlCommand cmd = new SqlCommand(spName, conn );
            cmd.CommandType = CommandType.StoredProcedure;
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();

            foreach (Para para in paras)
            {
                SqlParameter myParameter = new SqlParameter(para.ParaName, para.val );
                myParameter.Direction = ParameterDirection.Input;
                myParameter.Size = para.Size;
                myParameter.Value = para.val;
                cmd.Parameters.Add(myParameter);
            }

            return cmd.ExecuteScalar();
        }
        public static decimal RunSPReDecimal(string spName, Paras paras, decimal isNullReVal)
        {
            object obj = RunSPReObj(spName, paras);
            if (obj == null || obj == DBNull.Value)
                return isNullReVal;

            try
            {
                return decimal.Parse(obj.ToString() );
            }
            catch (Exception ex)
            {
                throw new Exception("RunSPReDecimal error="+ex.Message+" Obj="+obj );
            }
        }


		#region 执行存储过程返回影响个数
		/// <summary>
		/// 运行存储过程
		/// </summary>
		/// <param name="spName">名称</param>
		/// <returns>返回影响的行数</returns>
		public static int RunSP(string spName)
		{
			return DBProcedure.RunSP(spName,DBAccessOfMSSQL2000.GetSingleConn );
		}
		/// <summary>
		/// 运行存储过程
		/// </summary>
		/// <param name="spName">名称</param>
		/// <param name="paras">参数</param>
		/// <returns>返回影响的行数</returns>
		public static int RunSP(string spName, Paras paras)
		{
			return DBProcedure.RunSP(spName, paras, DBAccessOfMSSQL2000.GetSingleConn );
		}
		#endregion

		#region 运行存储过程返回 DataTable
		/// <summary>
		/// 运行存储过程
		/// </summary>
		/// <param name="spName">名称</param>
		/// <returns>DataTable</returns>
		public static DataTable RunSPReTable(string spName)
		{
			return DBProcedure.RunSPReturnDataTable(spName,DBAccessOfMSSQL2000.GetSingleConn);
		}
		/// <summary>
		/// 运行存储过程
		/// </summary>
		/// <param name="spName">名称</param>
		/// <param name="paras">参数</param>
		/// <returns>DataTable</returns>
		public static DataTable RunSPReTable(string spName, Paras paras)
		{
			return DBProcedure.RunSPReturnDataTable(spName,paras,DBAccessOfMSSQL2000.GetSingleConn);
		}
		#endregion

		#endregion

		private static bool lock_SQL=false;

		/// <summary>
		/// 检查是不是存在
		/// </summary>
		/// <param name="selectSQL"></param>
		/// <returns>检查是不是存在</returns>
		public static bool IsExits(string selectSQL)
		{
			if ( RunSQLReturnVal(selectSQL) == null)
				return false;
			return true;
		}


		#region 取得连接对象 ，CS、BS共用属性【关键属性】
		/// <summary>
		/// 取出当前的 连接。
		/// </summary>
		public static SqlConnection GetSingleConn
		{
			get
			{
                if (SystemConfig.IsBSsystem_Test)
				{
					SqlConnection conn = HttpContext.Current.Session["DBAccessOfMSSQL2000"] as SqlConnection;
					if (conn==null)
					{
						conn = new SqlConnection( SystemConfig.AppSettings[ "DBAccessOfMSSQL2000" ] );
						HttpContext.Current.Session[ "DBAccessOfMSSQL2000" ] = conn;
					}
					return conn;
				}
				else
				{
					SqlConnection conn = SystemConfig.CS_DBConnctionDic["DBAccessOfMSSQL2000"] as SqlConnection;
					if (conn==null)
					{
						conn = new SqlConnection( SystemConfig.AppSettings[ "DBAccessOfMSSQL2000" ] );
						SystemConfig.CS_DBConnctionDic[ "DBAccessOfMSSQL2000" ] = conn;
					}
					return conn;
				}
			}
		}
		#endregion 取得连接对象 ，CS、BS共用属性

		#region 重载 RunSQLReturnTable
		public static DataTable RunSQLReturnTable(string sql )
		{
			return DBAccess.RunSQLReturnTable( sql , GetSingleConn , SystemConfig.DBAccessOfMSSQL2000, CommandType.Text );
		}
		public static DataTable RunSQLReturnTable(string sql ,CommandType sqlType ,params object[] pars)
		{
			return DBAccess.RunSQLReturnTable( sql , GetSingleConn , SystemConfig.DBAccessOfMSSQL2000, sqlType ,pars);
		}
		public static int RunSQLReturnCOUNT(string sql)
		{
			return RunSQLReturnTable(sql).Rows.Count;
			//return RunSQLReturnVal( sql ,sql, sql );
		}
        public static bool IsExitsObject(string obj)
        {
            return IsExits("SELECT name  FROM sysobjects  WHERE  name = '" + obj + "'");
        }
		/// <summary>
		/// 运行SQL , 返回影响的行数.
		/// </summary>
		/// <param name="sql">msSQL</param>
		/// <param name="conn">SqlConnection</param>
		/// <param name="sqlType">CommandType</param>
		/// <param name="pars">params</param>
		/// <returns>返回运行结果</returns>
		public static int RunSQL(string sql )
		{
			SqlConnection conn=	DBAccessOfMSSQL2000.GetSingleConn;
			//string step="step=1" ;
			//如果是锁定状态，就等待.
			while(lock_SQL)  
			{
				lock_SQL=true; //锁定
			}
			try
			{		

				if (conn.State != System.Data.ConnectionState.Open)
					conn.Open();

				SqlCommand cmd = new SqlCommand( sql ,conn  );
				cmd.CommandType = CommandType.Text;

				int i = 0;
				try
				{
					i= cmd.ExecuteNonQuery();
				}
				catch(Exception ex)
				{
					throw ex;
				}
				cmd.Dispose();
				lock_SQL=false;
				return i;
			}
			catch(System.Exception ex)
			{
				lock_SQL=false;
				throw new Exception(ex.Message +"    SQL = "+sql );
			}
			finally
			{
				lock_SQL=false;
				conn.Close();
			}
		}
		#endregion

		#region 执行SQL ，返回首行首列
		public static object RunSQLReturnVal(string sql )
		{
			return DBAccess.RunSQLReturnVal( sql ,GetSingleConn , CommandType.Text ,  SystemConfig.DBAccessOfMSSQL2000 );
		}
		public static object RunSQLReturnVal(string sql ,CommandType sqlType ,params object[] pars)
		{
			return DBAccess.RunSQLReturnVal( sql ,GetSingleConn , sqlType ,SystemConfig.DBAccessOfMSSQL2000, pars );
		}
		#endregion 执行SQL ，返回首行首列

	}
	/// <summary>
	/// Oracle9i 的访问
	/// </summary>
	public class DBAccessOfOracle9i
	{

		#region 关于运行存储过程

		#region 执行存储过程返回影响个数
		/// <summary>
		/// 运行存储过程
		/// </summary>
		/// <param name="spName">名称</param>
		/// <returns>返回影响的行数</returns>
		public static int RunSP(string spName)
		{
			return DBProcedure.RunSP(spName,DBAccessOfOracle9i.GetSingleConn );
		}

        public static int RunSP(string spName, string paraKey, string paraVal)
        {
            Paras pas = new Paras();
            pas.Add(paraKey, paraVal);

            return DBProcedure.RunSP(spName, pas, DBAccessOfOracle9i.GetSingleConn );
        }
		/// <summary>
		/// 运行存储过程
		/// </summary>
		/// <param name="spName">名称</param>
		/// <param name="paras">参数</param>
		/// <returns>返回影响的行数</returns>
		public static int RunSP(string spName, Paras paras)
		{
			return DBProcedure.RunSP(spName, paras, DBAccessOfOracle9i.GetSingleConn );
		}

        
        public static object RunSPReObj(string spName, Paras paras)
        {


            OracleConnection conn = DBAccessOfOracle9i.GetSingleConn;
            
            OracleCommand cmd = new OracleCommand(spName, conn );
            cmd.CommandType = CommandType.StoredProcedure;
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();

            foreach (Para para in paras)
            {
                OracleParameter myParameter = new OracleParameter(para.ParaName, OracleType.VarChar);
                myParameter.Direction = ParameterDirection.Input;
                myParameter.Size = para.Size;
                myParameter.Value = para.val;
                cmd.Parameters.Add(myParameter);
            }

            return cmd.ExecuteScalar();
        }
        public static decimal RunSPReDecimal(string spName, Paras paras, decimal isNullReVal)
        {
            object obj = RunSPReObj(spName, paras);

            if (obj == null || obj==DBNull.Value )
                return isNullReVal;

            try
            {
                return decimal.Parse( obj.ToString() );
            }
            catch 
            {
                return isNullReVal;
            }
        }
		#endregion

		#region 运行存储过程返回 DataTable
		/// <summary>
		/// 运行存储过程
		/// </summary>
		/// <param name="spName">名称</param>
		/// <returns>DataTable</returns>
		public static DataTable RunSPReTable(string spName)
		{
			return DBProcedure.RunSPReturnDataTable(spName,DBAccessOfOracle9i.GetSingleConn);
		}
		/// <summary>
		/// 运行存储过程
		/// </summary>
		/// <param name="spName">名称</param>
		/// <param name="paras">参数</param>
		/// <returns>DataTable</returns>
		public static DataTable RunSPReTable(string spName, Paras paras)
		{
			return DBProcedure.RunSPReturnDataTable(spName,paras,DBAccessOfOracle9i.GetSingleConn);
		}
		#endregion

		#endregion


		/// <summary>
		/// 检查是不是存在
		/// </summary>
		/// <param name="selectSQL"></param>
		/// <returns>检查是不是存在</returns>
		public static bool IsExits(string selectSQL)
		{
			if ( RunSQLReturnVal(selectSQL) == null)
				return false;
			return true;
		}
	

		#region 取得连接对象 ，CS、BS共用属性【关键属性】
		public static OracleConnection GetSingleConn
		{
			get
			{
                if (SystemConfig.IsBSsystem_Test)
                {
                    OracleConnection conn = HttpContext.Current.Session["DBAccessOfOracle9i"] as OracleConnection;
                    if (conn == null)
                    {
                        conn = new OracleConnection(SystemConfig.DBAccessOfOracle9i);
                        conn.Open();
                        HttpContext.Current.Session["DBAccessOfOracle9i"] = conn;
                    }
                    return conn;
                }
                else
                {
                    OracleConnection conn = SystemConfig.CS_DBConnctionDic["DBAccessOfOracle9i"] as OracleConnection;
                    if (conn == null)
                    {
                        conn = new OracleConnection(SystemConfig.DBAccessOfOracle9i);
                        SystemConfig.CS_DBConnctionDic["DBAccessOfOracle9i"] = conn;
                        conn.Open();
                    }
                    else
                    {
                        if (conn.State != ConnectionState.Open)
                            conn.Open();
                    }
                    return conn;
                }
			}
		}
		#endregion 取得连接对象 ，CS、BS共用属性

		#region 重载 RunSQLReturnTable
		/// <summary>
		/// 运行sql返回table.
		/// </summary>
		/// <param name="sql">sql</param>
		/// <returns>返回的结果集合</returns>
        public static DataTable RunSQLReturnTable(string sql)
        {
            return DBAccess.RunSQLReturnTable(sql, GetSingleConn,
                CommandType.Text,
                SystemConfig.DBAccessOfOracle9i);
        }
		/// <summary>
		/// 运行sql返回table.
		/// </summary>
		/// <param name="sql">sql</param>
		/// <param name="sqlType">CommandType</param>
		/// <param name="pars">para</param>
		/// <returns>返回的结果集合</returns>
		public static DataTable RunSQLReturnTable(string sql ,CommandType sqlType ,params object[] pars)
		{
			return DBAccess.RunSQLReturnTable( sql , GetSingleConn ,sqlType , SystemConfig.DBAccessOfOracle9i  );
		}
		#endregion

		#region 重载 RunSQL
		public static int RunSQLTRUNCATETable(string table)
		{
			return DBAccess.RunSQL( "  TRUNCATE TABLE "+table  );
		}
		/// <summary>
		/// 运行SQL
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		public static int RunSQL(string sql )
		{
			return DBAccess.RunSQL( sql , GetSingleConn ,CommandType.Text, SystemConfig.DBAccessOfOracle9i  );
		}
		#endregion 
				
		#region 执行SQL ，返回首行首列
		/// <summary>
		/// 运行sql 返回一个object .
		/// </summary>
		/// <param name="sql"></param>
		/// <returns>object</returns>
		public static object RunSQLReturnVal(string sql )
		{
            return DBAccessOfOracle9i.RunSQLReturnTable(sql).Rows[0][0];
		}
		/// <summary>
		/// run sql return object values
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
        public static int RunSQLReturnIntVal(string sql)
        {
            try
            {
                return Convert.ToInt32(DBAccessOfOracle9i.RunSQLReturnVal(sql));
            }
            catch (Exception ex)
            {
                throw new Exception("RunSQLReturnIntVal 9i =" + ex.Message + " str = "+sql);
            }
        }
		/// <summary>
		/// run sql return float values.
		/// </summary>
		/// <param name="sql">will run sql</param>
		/// <returns>values</returns>
		public static float RunSQLReturnFloatVal(string sql )
		{

			object obj=DBAccessOfOracle9i.RunSQLReturnVal( sql );

			if (obj.ToString()=="System.DBNull")
				throw new Exception("@执行方法DBAccessOfOracle9i.RunSQLReturnFloatVal(sql)错误,运行的结果是null.请检查sql="+sql);

			try
			{
				return float.Parse(obj.ToString());
			}
			catch 
			{
				throw new Exception("@执行方法DBAccessOfOracle9i.RunSQLReturnFloatVal(sql)错误,运行的结果是["+obj.ToString()+"].不能向float 转换,请检查sql="+sql);
			}
		}
		/// <summary>
		/// 运行sql 返回float
		/// </summary>
		/// <param name="sql">要运行的sql</param>
		/// <param name="isNullAsVal">如果是Null, 返回的信息.</param>
		/// <returns></returns>
		public static float RunSQLReturnFloatVal(string sql, float isNullAsVal)
		{
			object obj=DBAccessOfOracle9i.RunSQLReturnVal( sql );
			try
			{
				System.DBNull dbnull=(System.DBNull)obj;
				return isNullAsVal;
			}
			catch
			{
			}		

			try
			{
				return float.Parse(obj.ToString());
			}
			catch 
			{
				throw new Exception("@执行方法DBAccessOfOracle9i.RunSQLReturnFloatVal(sql,isNullAsVal)错误,运行的结果是["+obj+"].不能向float 转换,请检查sql="+sql);
			}
		}
		/// <summary>
		/// run sql return decimal val
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		public static decimal RunSQLReturnDecimalVal(string sql )
		{			
			object obj=DBAccessOfOracle9i.RunSQLReturnVal( sql );
			if (obj==null)
				throw new Exception("@执行方法DBAccessOfOracle9i.RunSQLReturnDecimalVal()错误,运行的结果是null.请检查sql="+sql);
			try				
			{
				return decimal.Parse(obj.ToString());
			}
			catch 
			{
				throw new Exception("@执行方法DBAccessOfOracle9i.RunSQLReturnDecimalVal()错误,运行的结果是["+obj+"].不能向decimal 转换,请检查sql="+sql);
			}
		}
		/// <summary>
		/// run sql return decimal.
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="isNullAsVal"></param>
		/// <returns></returns>
		public static decimal RunSQLReturnDecimalVal(string sql, decimal isNullAsVal )
		{
			object obj=DBAccessOfOracle9i.RunSQLReturnVal( sql );
			try
			{
				System.DBNull dbnull=(System.DBNull)obj;
				return isNullAsVal;
			}
			catch
			{
			}		

			try
			{
				return decimal.Parse(obj.ToString());
			}
			catch 
			{
				throw new Exception("@执行方法DBAccessOfOracle9i.RunSQLReturnDecimalVal(sql,isNullAsVal)错误,运行的结果是["+obj+"].不能向float 转换,请检查sql="+sql);
			}
		}
		#endregion 执行SQL ，返回首行首列
	 
	}
	/// <summary>
	/// Oracle9i 的访问
	/// </summary>
    public class DBAccessOfOracle9i1
    {

        #region 关于运行存储过程

        #region 执行存储过程返回影响个数
        /// <summary>
        /// 运行存储过程
        /// </summary>
        /// <param name="spName">名称</param>
        /// <returns>返回影响的行数</returns>
        public static int RunSP(string spName)
        {
            return DBProcedure.RunSP(spName, DBAccessOfOracle9i1.GetSingleConn);
        }
        /// <summary>
        /// 运行存储过程
        /// </summary>
        /// <param name="spName">名称</param>
        /// <param name="paras">参数</param>
        /// <returns>返回影响的行数</returns>
        public static int RunSP(string spName, Paras paras)
        {
            return DBProcedure.RunSP(spName, paras, DBAccessOfOracle9i1.GetSingleConn);
        }
        public static int RunSP(string spName, string para, string paraVal)
        {
            Paras paras = new Paras();
            Para p = new Para(para, DbType.String, paraVal);
            paras.Add(p);
            return DBProcedure.RunSP(spName, paras, DBAccessOfOracle9i1.GetSingleConn);
        }
        #endregion

        #region 运行存储过程返回 DataTable
        /// <summary>
        /// 运行存储过程
        /// </summary>
        /// <param name="spName">名称</param>
        /// <returns>DataTable</returns>
        public static DataTable RunSPReTable(string spName)
        {
            return DBProcedure.RunSPReturnDataTable(spName, DBAccessOfOracle9i1.GetSingleConn);
        }
        /// <summary>
        /// 运行存储过程
        /// </summary>
        /// <param name="spName">名称</param>
        /// <param name="paras">参数</param>
        /// <returns>DataTable</returns>
        public static DataTable RunSPReTable(string spName, Paras paras)
        {
            return DBProcedure.RunSPReturnDataTable(spName, paras, DBAccessOfOracle9i1.GetSingleConn);
        }
        #endregion

        #endregion


        /// <summary>
        /// 检查是不是存在
        /// </summary>
        /// <param name="selectSQL"></param>
        /// <returns>检查是不是存在</returns>
        public static bool IsExits(string selectSQL)
        {
            if (RunSQLReturnVal(selectSQL) == null)
                return false;
            return true;
        }

        #region 取得连接对象 ，CS、BS共用属性【关键属性】
        public static OracleConnection GetSingleConn
        {
            get
            {
                if (SystemConfig.IsBSsystem_Test)
                {
                    OracleConnection conn = HttpContext.Current.Session["DBAccessOfOracle9i1"] as OracleConnection;
                    if (conn == null)
                    {
                        conn = new OracleConnection(SystemConfig.DBAccessOfOracle9i1);
                        conn.Open();
                        HttpContext.Current.Session["DBAccessOfOracle9i1"] = conn;
                    }
                    return conn;
                }
                else
                {
                    OracleConnection conn = SystemConfig.CS_DBConnctionDic["DBAccessOfOracle9i1"] as OracleConnection;
                    if (conn == null)
                    {
                        conn = new OracleConnection(SystemConfig.DBAccessOfOracle9i1);
                        SystemConfig.CS_DBConnctionDic["DBAccessOfOracle9i1"] = conn;
                        conn.Open();
                    }
                    else
                    {
                        if (conn.State != ConnectionState.Open)
                            conn.Open();
                    }

                    return conn;
                }
            }
        }
        #endregion 取得连接对象 ，CS、BS共用属性

        #region 重载 RunSQLReturnTable
        /// <summary>
        /// 运行sql返回table.
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns>返回的结果集合</returns>
        public static DataTable RunSQLReturnTable(string sql)
        {
            return DBAccess.RunSQLReturnTable(sql, GetSingleConn, CommandType.Text, SystemConfig.DBAccessOfOracle9i1);
        }
        /// <summary>
        /// 运行sql返回table.
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="sqlType">CommandType</param>
        /// <param name="pars">para</param>
        /// <returns>返回的结果集合</returns>
        public static DataTable RunSQLReturnTable(string sql, CommandType sqlType, params object[] pars)
        {
            return DBAccess.RunSQLReturnTable(sql, GetSingleConn, sqlType, SystemConfig.DBAccessOfOracle9i1);
        }
        #endregion

        #region 重载 RunSQL
        public static int RunSQLTRUNCATETable(string table)
        {
            return DBAccess.RunSQL("  TRUNCATE TABLE " + table);
        }
        /// <summary>
        /// 运行SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int RunSQL(string sql)
        {
            return DBAccess.RunSQL(sql, GetSingleConn, CommandType.Text, SystemConfig.DBAccessOfOracle9i1);
        }
        /// <summary>
        /// 运行 SQL
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="sqlType">CommandType</param>
        /// <param name="pars">参数</param>
        /// <returns>结果集</returns>
        public static int RunSQL(string sql, CommandType sqlType, params object[] pars)
        {
            return DBAccess.RunSQL(sql, GetSingleConn, sqlType, SystemConfig.DBAccessOfOracle9i1);
        }
        #endregion

        #region 执行SQL ，返回首行首列
        /// <summary>
        /// 运行sql 返回一个object .
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>object</returns>
        public static object RunSQLReturnVal(string sql)
        {
            return DBAccess.RunSQLReturnTable(sql).Rows[0][0];

            //return DBAccess.RunSQLReturnVal(sql,GetSingleConn, SystemConfig.DBAccessOfOracle9i1);
        }
        /// <summary>
        /// run sql return object values
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int RunSQLReturnIntVal(string sql)
        {
            //return (DBAccessOfOracle9i1.RunSQLReturnVal( sql );
            string str = DBAccessOfOracle9i1.RunSQLReturnVal(sql).ToString();
            try
            {
                return int.Parse(str);
            }
            catch (Exception ex)
            {
                throw new Exception("RunSQLReturnIntVal 9i =" + ex.Message + " str = " + str);
            }
        }
        /// <summary>
        /// run sql return float values.
        /// </summary>
        /// <param name="sql">will run sql</param>
        /// <returns>values</returns>
        public static float RunSQLReturnFloatVal(string sql)
        {

            object obj = DBAccessOfOracle9i1.RunSQLReturnVal(sql);

            if (obj.ToString() == "System.DBNull")
                throw new Exception("@执行方法DBAccessOfOracle9i1.RunSQLReturnFloatVal(sql)错误,运行的结果是null.请检查sql=" + sql);

            try
            {
                return float.Parse(obj.ToString());
            }
            catch
            {
                throw new Exception("@执行方法DBAccessOfOracle9i1.RunSQLReturnFloatVal(sql)错误,运行的结果是[" + obj.ToString() + "].不能向float 转换,请检查sql=" + sql);
            }
        }
        /// <summary>
        /// 运行sql 返回float
        /// </summary>
        /// <param name="sql">要运行的sql</param>
        /// <param name="isNullAsVal">如果是Null, 返回的信息.</param>
        /// <returns></returns>
        public static float RunSQLReturnFloatVal(string sql, float isNullAsVal)
        {
            object obj = DBAccessOfOracle9i1.RunSQLReturnVal(sql);
            try
            {
                System.DBNull dbnull = (System.DBNull)obj;
                return isNullAsVal;
            }
            catch
            {
            }

            try
            {
                return float.Parse(obj.ToString());
            }
            catch
            {
                throw new Exception("@执行方法DBAccessOfOracle9i1.RunSQLReturnFloatVal(sql,isNullAsVal)错误,运行的结果是[" + obj + "].不能向float 转换,请检查sql=" + sql);
            }
        }
        /// <summary>
        /// run sql return decimal val
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static decimal RunSQLReturnDecimalVal(string sql)
        {
            object obj = DBAccessOfOracle9i1.RunSQLReturnVal(sql);
            if (obj == null)
                throw new Exception("@执行方法DBAccessOfOracle9i1.RunSQLReturnDecimalVal()错误,运行的结果是null.请检查sql=" + sql);
            try
            {
                return decimal.Parse(obj.ToString());
            }
            catch
            {
                throw new Exception("@执行方法DBAccessOfOracle9i1.RunSQLReturnDecimalVal()错误,运行的结果是[" + obj + "].不能向decimal 转换,请检查sql=" + sql);
            }
        }
        /// <summary>
        /// run sql return decimal.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="isNullAsVal"></param>
        /// <returns></returns>
        public static decimal RunSQLReturnDecimalVal(string sql, decimal isNullAsVal)
        {
            object obj = DBAccessOfOracle9i1.RunSQLReturnVal(sql);
            try
            {
                System.DBNull dbnull = (System.DBNull)obj;
                return isNullAsVal;
            }
            catch
            {
            }

            try
            {
                return decimal.Parse(obj.ToString());
            }
            catch
            {
                throw new Exception("@执行方法DBAccessOfOracle9i1.RunSQLReturnDecimalVal(sql,isNullAsVal)错误,运行的结果是[" + obj + "].不能向float 转换,请检查sql=" + sql);
            }
        }
        #endregion 执行SQL ，返回首行首列
    }
}
 