using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
//using BP = BP.DA.DBAccess;


namespace BP.DA
{
    public class DBAccess
    {
        public static string GetWebConfigByKey(string key)
        {
            WFWord2007.ServiceReference1.DBAcceSoapClient db = new WFWord2007.ServiceReference1.DBAcceSoapClient();
            return db.GetSettingByKey(key);
        }
        public static int RunSQL(string sql)
        {
            WFWord2007.ServiceReference1.DBAcceSoapClient db = new WFWord2007.ServiceReference1.DBAcceSoapClient();

            return db.RunSQL(sql);
        }
        public static DataTable RunSQLReturnTable(string sql)
        {
            WFWord2007.ServiceReference1.DBAcceSoapClient db = new WFWord2007.ServiceReference1.DBAcceSoapClient();
            return db.RunSQLReturnTable(sql);
        }
        public static string  RunSQLReturnString(string sql)
        {
            WFWord2007.ServiceReference1.DBAcceSoapClient db = new WFWord2007.ServiceReference1.DBAcceSoapClient();
            return db.RunSQLReturnString(sql);
        }
        public static int GenerOID()
        {
            WFWord2007.ServiceReference1.DBAcceSoapClient db = new WFWord2007.ServiceReference1.DBAcceSoapClient();
            return db.GenerOID();
        }
    }
}
