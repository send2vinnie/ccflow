using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.EIP.Interface;
using BP.EIP.Enum;
using BP.DA;

namespace BP.EIP.DAL
{
    public partial class RoleDAL:IRole
    {

        public System.Data.DataTable GetDTByDept(string departmentId)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetDTUserByRole(string roleId)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("SELECT PE.* FROM PORT_EMPSTATION PES ");
            sqlBuilder.Append("LEFT JOIN PORT_EMP PE ");
            sqlBuilder.Append("ON PES.FK_EMP = PE.NO ");
            sqlBuilder.Append("WHERE PES.FK_STATION='{0}'");
            string sql = sqlBuilder.ToString();
            sql = string.Format(sql, roleId);
            return DBAccess.RunSQLReturnTable(sql);
        }

        public int ClearRoleUser(string roleId)
        {
            string sql = "DELETE PORT_EMPSTATION WHERE FK_STATION='{0}'";
            sql = string.Format(sql, roleId);
            return DBAccess.RunSQLReturnValInt(sql);
        }

        public bool Exists(string No)
        {
            string sql = "SELECT NO FROM PORT_STATION WHERE NO='{0}'";
            sql = string.Format(sql, No);
            return XDBHelper.RunSQLReturnCOUNT(sql) > 0;
        }

        public void Add(BaseEntity entity, out StatusCode statusCode, out string statusMessage)
        {
            try
            {
                statusCode = StatusCode.Success;
                statusMessage = string.Empty;
                entity.Insert();
            }
            catch (Exception ex)
            {
                statusCode = StatusCode.Exception;
                statusMessage = ex.ToString();
            }
        }

        public BaseEntity GetEntity(string No)
        {
            return new BP.EIP.Port_Station(No);
        }

        public System.Data.DataTable GetDT()
        {
            BP.EIP.Port_Stations portStations = new Port_Stations();
            portStations.RetrieveAll();
            return portStations.ToDataSet().Tables[0];
        }

        public System.Data.DataTable GetDT(string[] Ids)
        {
            string queryIds = Tool.ArrayToString(Ids);
            string sql = "SELECT * FROM PORT_STATION WHERE NO IN ({0}) ";
            sql = string.Format(sql, queryIds);
            return DBAccess.RunSQLReturnTable(sql);
        }

        public int Delete(string Id)
        {
            Port_Station portStation = new Port_Station(Id);
            return portStation.Delete();
        }

        public int BatchDelete(string[] ids)
        {
            string deleteIds = Tool.ArrayToString(ids);
            string sql = "DELETE FROM PORT_STATION WHERE NO IN ({0})";
            sql = string.Format(sql, deleteIds);
            return DBAccess.RunSQLReturnValInt(sql);
        }

        public int SetDeleted(string[] ids)
        {
            string deleteIds = Tool.ArrayToString(ids);
            string sql = "UPDATE PORT_STATION SET STATUS='0' WHERE NO IN ('{0}')";
            sql = string.Format(sql, deleteIds);
            return DBAccess.RunSQLReturnValInt(sql);
        }

        public System.Data.DataTable Search(string searchValue)
        {
            string sql = "SELECT * FROM PORT_STATION WHERE NAME LIKE '%{0}%' OR DESCRIPTION LIKE '%{0}%'";
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
