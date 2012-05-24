using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.EIP.Interface;
using BP.DA;
using System.Data;

namespace BP.EIP.DAL
{
    internal class RuleDAL : IRule
    {
        public bool IsUserInRole(string userId, string roleId)
        {
            throw new NotImplementedException();
        }

        public bool IsAuthorized(string permissionItemCode)
        {
            throw new NotImplementedException();
        }

        public DataTable GetMenus(string appName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT TOP 1000 id,pid,text,iconCls,url");
            strSql.Append(" FROM V_MENU");
            strSql.Append(" WHERE APPNAME='" + appName + "'");
            strSql.Append(" ORDER BY id");
            return DBAccess.RunSQLReturnTable(strSql.ToString());
        }

        public bool Exists(string No)
        {
            throw new NotImplementedException();
        }

        public void Add(BaseEntity entity, out string statusCode, out string statusMessage)
        {
            throw new NotImplementedException();
        }

        public BaseEntity GetEntity(string No)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetDT()
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetDT(string[] Ids)
        {
            throw new NotImplementedException();
        }

        public int Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public int BatchDelete(string[] ids)
        {
            throw new NotImplementedException();
        }

        public int SetDeleted(string[] ids)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable Search(string searchValue)
        {
            throw new NotImplementedException();
        }

        public int Update(BaseEntity entity, out string statusCode, out string statusMessage)
        {
            throw new NotImplementedException();
        }

        public int BatchSave(List<BaseEntity> entityList)
        {
            throw new NotImplementedException();
        }
    }
}
