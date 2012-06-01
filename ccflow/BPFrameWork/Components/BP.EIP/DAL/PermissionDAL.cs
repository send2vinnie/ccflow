using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.EIP.Interface;
using BP.DA;
using BP.EIP.DALFactory;
using System.Data;

namespace BP.EIP.DAL
{
    public partial class PermissionDAL : IPermission
    {
        public System.Data.DataTable GetDTByFunction(string functionId)
        {
            string sql = "SELECT * FROM PORT_FUNCTIONOPERATE WHERE FK_FUNCTION='{0}'";
            sql = string.Format(sql, functionId);
            return DBAccess.RunSQLReturnTable(sql);
        }

        public System.Data.DataTable GetAllFunctions()
        {
            string sql = "SELECT * FROM PORT_FUNCTION";
            return DBAccess.RunSQLReturnTable(sql);
        }

        public System.Data.DataTable GetAllFunctions(string domainId)
        {
            string sql = "SELECT * FROM PORT_FUNCTION WHERE FK_DOMAIN='{0}'";
            sql = string.Format(sql, domainId);
            return DBAccess.RunSQLReturnTable(sql);
        }

        public System.Data.DataTable GetAllOperates()
        {
            string sql = "SELECT * FROM PORT_FUNCTIONOPERATE ";
            return DBAccess.RunSQLReturnTable(sql);
        }

        public System.Data.DataTable GetAllOperates(string domainId)
        {
            string sql = "SELECT PFO.* FROM PORT_FUNCTIONOPERATE PFO LEFT JOIN PORT_FUNCTION PF ON PFO.FK_FUNCTION=PF.NO WHERE PF.FK_DOMAIN='{0}'";
            sql = string.Format(sql, domainId);
            return DBAccess.RunSQLReturnTable(sql);
        }

        public System.Data.DataTable GetDTByUser(string userId)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("SELECT PFO.* FROM PORT_STATIONOPERATE PSO LEFT JOIN PORT_FUNCTIONOPERATE PFO ON PSO.FK_OPERATE=PFO.NO ");
            sqlBuilder.Append("WHERE PSO.FK_ROLE IN (SELECT PE.FK_STATION FROM PORT_EMPSTATION PE WHERE PE.FK_EMP='{0}')");
            string sql = sqlBuilder.ToString();
            sql = string.Format(sql, userId);
            return DBAccess.RunSQLReturnTable(sql);
        }

        public System.Data.DataTable GetDTByRole(string roleId)
        {
            string sql = "SELECT * FROM PORT_STATIONOPERATE PSO LEFT JOIN PORT_FUNCTIONOPERATE PFO ON PSO.FK_OPERATE=PFO.NO WHERE PSO.FK_ROLE='{0}'";
            sql = string.Format(sql, roleId);
            return DBAccess.RunSQLReturnTable(sql);
        }

        public BasePermission GetByCode(string code)
        {
            throw new NotImplementedException();
        }

        public bool HavePermission(string functionId, string controlName)
        {
            object cache = DataCache.GetCache(functionId);
            DataTable functionTable = null;
            if (cache == null)
            {
                //缓存不存在的情况下
                functionTable = GetDTByFunction(functionId);
                DataCache.SetCache(functionId, functionTable);
                cache = functionTable;
            }
            else
            {
                functionTable = cache as DataTable;
            }
            if (functionTable != null)
            {
                DataRow[] controlRows = functionTable.Select(string.Format("CONTROL_NAME='{0}'", controlName));
                return controlRows.Length > 0;
            }
            else
            {
                return false;
            }
        }

        public bool Exists(string No)
        {
            throw new NotImplementedException();
        }

        public void Add(BaseEntity entity, out Enum.StatusCode statusCode, out string statusMessage)
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

        public int Update(BaseEntity entity, out Enum.StatusCode statusCode, out string statusMessage)
        {
            throw new NotImplementedException();
        }

        public int BatchSave(List<BaseEntity> entityList)
        {
            throw new NotImplementedException();
        }


        public bool InsertPermission(string roleId, string[] operateIds)
        {
            IList<string> sqlList = new List<string>();
            foreach (string operateId in operateIds)
            {
                string sql = "INSERT INTO PORT_STATIONOPERATE(ID,FK_ROLE,FK_OPERATE)VALUES('{0}','{1}','{2}')";
                sql = string.Format(sql, Guid.NewGuid().ToString(), roleId, operateId);
                sqlList.Add(sql);
            }
            return XDBHelper.RunSql(sqlList) > 0;
        }
    }
}
