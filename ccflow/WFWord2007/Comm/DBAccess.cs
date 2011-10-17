using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CCFlowWord2007;
using CCFlowWord2007.ServiceReference1;


namespace BP.DA
{
    public class DBAccess
    {
        public static string GetWebConfigByKey(string key)
        {
            DocFlowSoapClient db = new DocFlowSoapClient();
            return db.GetSettingByKey(key);
        }
        public static int RunSQL(string sql)
        {
            DocFlowSoapClient db = new DocFlowSoapClient();
            return db.RunSQL(sql);
        }
        public static DataTable RunSQLReturnTable(string sql)
        {
            DocFlowSoapClient db = new DocFlowSoapClient();
            return db.RunSQLReturnTable(sql);
        }
        public static string  RunSQLReturnString(string sql)
        {
            DocFlowSoapClient db = new DocFlowSoapClient();
            return db.RunSQLReturnString(sql);
        }
        public static int GenerOID()
        {
            DocFlowSoapClient db = new DocFlowSoapClient();
            return db.GenerOID();
        }
    }
}
