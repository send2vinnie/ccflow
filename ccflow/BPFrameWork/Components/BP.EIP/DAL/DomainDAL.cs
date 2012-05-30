using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.EIP.Interface;
using BP.EIP.Enum;
using BP.DA;

namespace BP.EIP.DAL
{
    public partial class DomainDAL : IDomain
    {

        public System.Data.DataTable GetUsersInDomain(string domainId)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("SELECT PE.* FROM PORT_EMPDOMAIN PED ");
            sqlBuilder.Append("LEFT JOIN PORT_EMP PE ");
            sqlBuilder.Append("ON PED.FK_EMP = PE.NO ");
            sqlBuilder.Append("WHERE PED.FK_DOMAIN = '{0}'");
            string sql = sqlBuilder.ToString();
            sql = string.Format(sql, domainId);
            return DBAccess.RunSQLReturnTable(sql);
        }

        public System.Data.DataTable GetRolesInDomain(string domainId)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("SELECT PS.* FROM PORT_STATIONDOMAIN PSD ");
            sqlBuilder.Append("LEFT JOIN PORT_STATION PS ");
            sqlBuilder.Append("ON PSD.FK_STATION = PS.NO ");
            sqlBuilder.Append("WHERE PSD.FK_DOMAIN='{0}'");
            string sql = sqlBuilder.ToString();
            sql = string.Format(sql, domainId);
            return DBAccess.RunSQLReturnTable(sql);
        }

        public System.Data.DataTable GetSubDomain(string domainId)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("SELECT * FROM PORT_DOMAIN ");
            sqlBuilder.Append("WHERE NO <> '{0}' ");
            sqlBuilder.Append("START WITH NO = '{0}' ");
            sqlBuilder.Append("CONNECT BY PRIOR NO = PARENTID ");
            string sql = sqlBuilder.ToString();
            sql = string.Format(sql, domainId);
            return DBAccess.RunSQLReturnTable(sql);
        }

        public System.Data.DataTable GetParentDomain(string domainId)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("SELECT * FROM PORT_DOMAIN ");
            sqlBuilder.Append("WHERE NO <> '{0}' ");
            sqlBuilder.Append("START WITH NO = '{0}' ");
            sqlBuilder.Append("CONNECT BY PRIOR PARENTID = NO ");
            string sql = sqlBuilder.ToString();
            sql = string.Format(sql, domainId);
            return DBAccess.RunSQLReturnTable(sql);
        }

        public System.Data.DataTable GetRulesInDomain(string domainId)
        {
            string sql = "SELECT * FROM PORT_RULE WHERE FK_DOMAIN = '{0}'";
            sql = string.Format(sql, domainId);
            return DBAccess.RunSQLReturnTable(sql);
        }

        public int MoveTo(string id, string parentId)
        {
            string sql = "UPDATE PORT_DOMAIN SET PARENTID='{0}' WHERE NO='{1}'";
            sql = string.Format(sql, parentId, id);
            return DBAccess.RunSQLReturnValInt(sql);
        }

        public int BatchMoveTo(string[] ids, string parentId)
        {
            string moveIds = Tool.ArrayToString(ids);
            string sql = "UPDATE PORT_DOMAIN SET PARENTID='{0}' WHERE NO IN ({1})'";
            sql = string.Format(sql, parentId, moveIds);
            return DBAccess.RunSQLReturnValInt(sql);
        }

        public bool Exists(string No)
        {
            string sql = "SELECT NO FROM PORT_DOMAIN WHERE NO='{0}'";
            sql = string.Format(sql, No);
            return DBAccess.RunSQLReturnCOUNT(sql) > 0;
        }

        public void Add(BaseEntity entity, out StatusCode statusCode, out string statusMessage)
        {
            try
            {
                entity.Insert();
                statusCode = StatusCode.Success;
                statusMessage = string.Empty;
            }
            catch (Exception ex)
            {
                statusCode = StatusCode.Exception;
                statusMessage = ex.ToString();
            }
        }

        public BaseEntity GetEntity(string No)
        {
            return new BP.EIP.Port_Domain(No);
        }

        public System.Data.DataTable GetDT()
        {
            BP.EIP.Port_Domains domains = new Port_Domains();
            domains.RetrieveAll();
            return domains.ToDataSet().Tables[0];
        }

        public System.Data.DataTable GetDT(string[] Ids)
        {
            string queryIds = Tool.ArrayToString(Ids);
            string sql = "SELECT * FROM PORT_DOMAIN WHERE ID IN ({0})";
            sql = string.Format(sql, queryIds);
            return DBAccess.RunSQLReturnTable(sql);
        }

        public int Delete(string Id)
        {
            BP.EIP.Port_Domain domain = new Port_Domain(Id);
            return domain.Delete();
        }

        public int BatchDelete(string[] ids)
        {
            string deleteIds = Tool.ArrayToString(ids);
            string sql = "DELETE FORM PORT_DOMAIN WHERE ID IN ({0})";
            sql = string.Format(sql, deleteIds);
            return DBAccess.RunSQLReturnValInt(sql);
        }

        public int SetDeleted(string[] ids)
        {
            string deleteIds = Tool.ArrayToString(ids);
            string sql = "UPDATE PORT_DOMAIN SET STATUS='0' WHERE ID IN ({0})";
            sql = string.Format(sql, deleteIds);
            return DBAccess.RunSQLReturnValInt(sql);
        }

        public System.Data.DataTable Search(string searchValue)
        {
            string sql = "SELECT * FROM PORT_DOMAIN WHERE DOMAINNAME LIKE '%{0}%'";
            sql = string.Format(sql, searchValue);
            return DBAccess.RunSQLReturnTable(sql);
        }

        public int Update(BaseEntity entity, out StatusCode statusCode, out string statusMessage)
        {
            try
            {
                statusCode = StatusCode.Success;
                statusMessage = string.Empty;
                return entity.Update();
            }
            catch (Exception ex)
            {
                statusCode = StatusCode.Exception;
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
