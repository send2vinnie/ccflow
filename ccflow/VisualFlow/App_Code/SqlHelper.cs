using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
///SqlHelper 的摘要说明
/// </summary>
public class SqlHelper
{
    public SqlHelper()
    {
    }
    public SqlConnection Connection
    {
        get
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["TestDBConnectionString"].ConnectionString;
            return con;
        }
    }

    public DataTable ExecuteSqlReturnTable(string strSql)
    {
        SqlDataAdapter adapter = new SqlDataAdapter(strSql, this.Connection);
        DataTable dt = new DataTable();
        adapter.Fill(dt);
        return dt;        
    }
}