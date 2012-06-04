using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.EIP.Interface;
using BP.DA;
using System.Data;
using BP.EIP.Enum;
using BP.EIP.Enum;

namespace BP.EIP.DAL
{
    internal class RuleDAL : IRule
    {
        public bool IsUserInRole(string userId, string roleId)
        {
            string sql = "SELECT NO FROM PORT_EMPSTATION WHERE FK_EMP = '{0}' AND FK_STATION = '{1}'";
            sql = string.Format(sql, userId, roleId);
            return DBAccess.RunSQLReturnTable(sql).Rows.Count > 0;
        }

        public bool IsAuthorized(string permissionCode)
        {
            throw new NotImplementedException();
        }

        public DataTable GetMenus(string appName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT id,pid,text,iconCls,url");
            strSql.Append(" FROM V_MENU");
            strSql.Append(" WHERE APPNAME='" + appName + "'");
            strSql.Append(" ORDER BY id");
            return DBAccess.RunSQLReturnTable(strSql.ToString());
        }

        public bool Exists(string No)
        {
            string sql = "SELECT NO FROM PORT_RULE WHERE NO='{0}'";
            sql = string.Format(sql, No);
            return DBAccess.RunSQLReturnCOUNT(sql) > 0;
        }

        public void Add(BaseEntity entity, out StatusCode statusCode, out string statusMessage)
        {
            try
            {
                statusCode = Enum.StatusCode.Success;
                statusMessage = string.Empty;
                entity.Insert();
            }
            catch (Exception ex)
            {
                statusCode = Enum.StatusCode.Exception;
                statusMessage = ex.ToString();
            }
        }

        public BaseEntity GetEntity(string No)
        {
            return new BP.EIP.Port_Rule(No);
        }

        public System.Data.DataTable GetDT()
        {
            BP.EIP.Port_Rules rules = new Port_Rules();
            rules.RetrieveAll();
            return rules.ToDataSet().Tables[0];
        }

        public System.Data.DataTable GetDT(string[] Ids)
        {
            string queryIds = Tool.ArrayToString(Ids);
            string sql = "SELECT * FROM PORT_RULE WHERE NO IN ({0})";
            sql = string.Format(sql, queryIds);
            return DBAccess.RunSQLReturnTable(sql);
        }

        public int Delete(string Id)
        {
            BP.EIP.Port_Rule portRule = new Port_Rule(Id);
            return portRule.Delete();
        }

        public int BatchDelete(string[] ids)
        {
            string deleteIds = Tool.ArrayToString(ids);
            string sql = "DELETE FROM PORT_RULE WHERE NO IN ({0})";
            sql = string.Format(sql, deleteIds);
            return DBAccess.RunSQLReturnValInt(sql);
        }

        public int SetDeleted(string[] ids)
        {
            string deleteIds = Tool.ArrayToString(ids);
            string sql = "UPDATE PORT_RULE SET STATUS='0' WHERE NO IN ({0})";
            sql = string.Format(sql, deleteIds);
            return DBAccess.RunSQLReturnValInt(sql);
        }

        public System.Data.DataTable Search(string searchValue)
        {
            string sql = "SELECT * FOM PORT_RULE WHERE PERMISSION LIKE '%{0}%' OR DESCRIPTION LIKE '%{0}%'";
            sql = string.Format(sql, searchValue);
            return DBAccess.RunSQLReturnTable(sql);
        }

        public int Update(BaseEntity entity, out StatusCode statusCode, out string statusMessage)
        {
            try
            {
                statusCode = Enum.StatusCode.Success;
                statusMessage = string.Empty;
                return entity.Update();
            }
            catch (Exception ex)
            {
                statusCode = Enum.StatusCode.Exception;
                statusMessage = ex.ToString();
                return -1;
            }
        }

        public int BatchSave(List<BaseEntity> entityList)
        {
            int result = 0;
            foreach (BaseEntity entity in entityList)
            {
                result += entity.Save();
            }
            return result;
        }
    }
}
