using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.EIP.Interface;
using BP.EIP.Enum;
using BP.DA;

namespace BP.EIP.DAL
{
    public partial class StaffDAL : IStaff
    {

        public System.Data.DataTable GetAddressDT(string departmentId, string searchValue)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("SELECT PS.* FROM PORT_STAFFDEPT PSD LEFT JOIN PORT_STAFF PS ON PSD.FK_STAFF=PS.NO ");
            sqlBuilder.Append("WHERE PSD.FK_DEPT='{0}' ");
            sqlBuilder.Append(" AND ( IDCARD LIKE '%{1}%' ");
            sqlBuilder.Append(" OR PHONE LIKE '%{1}%' ");
            sqlBuilder.Append(" OR EMAIL LIKE '%{1}%' ");
            sqlBuilder.Append(" OR EMPNAME LIKE '%{1}%' ");
            sqlBuilder.Append(" OR ADDRESS LIKE '%{1}%' ");
            sqlBuilder.Append(")");
            string sql = sqlBuilder.ToString();
            sql = string.Format(sql, departmentId, searchValue);
            return DBAccess.RunSQLReturnTable(sql);
        }

        public System.Data.DataTable GetAddressDT(string departmentId, bool containChildren)
        {
            string sql = string.Empty;
            if (!containChildren)
            {
                sql = "SELECT PS.* FROM PORT_STAFFDEPT PSD LEFT JOIN PORT_STAFF PS ON PSD.FK_STAFF=PS.NO WHERE PSD.FK_DEPT='{0}'";
            }
            else
            {
                sql = "SELECT PS.* FROM PORT_STAFFDEPT PSD LEFT JOIN PORT_STAFF PS ON PSD.FK_STAFF=PS.NO WHERE PSD.FK_DEPT IN (SELECT NO FROM PORT_DEPT START WITH NO = '{0}' CONNECT BY PRIOR NO = PID)";
            }
            sql = string.Format(sql, departmentId);
            return DBAccess.RunSQLReturnTable(sql);
        }

        public int UpdateAddress(Port_Staff staffEntity, out StatusCode statusCode, out string statusMessage)
        {
            try
            {
                statusCode = StatusCode.Success;
                statusMessage = string.Empty;
                return staffEntity.Update();
            }
            catch (Exception ex)
            {
                statusCode = StatusCode.Exception;
                statusMessage = ex.ToString();
                return -1;
            }
        }

        public int BatchUpdateAddress(List<Port_Staff> staffEntites, out StatusCode statusCode, out string statusMessage)
        {
            int result = 0;
            try
            {
                foreach (Port_Staff portStaff in staffEntites)
                {
                    result += portStaff.Save();
                }
                statusCode = StatusCode.Success;
                statusMessage = string.Empty;
                return result;
            }
            catch (Exception ex)
            {
                statusCode = StatusCode.Exception;
                statusMessage = ex.ToString();
                return -1;
            }
        }

        public int SetStaffUser(string staffId, string userId)
        {
            string sql = "UPDATE PORT_EMP SET FK_EMP='{0}' WHERE NO='{1}'";
            sql = string.Format(sql, staffId, userId);
            return DBAccess.RunSQLReturnValInt(sql);
        }

        public int DeleteStaffUser(string staffId)
        {
            string sql = "DELETE FROM PORT_EMP WHERE FK_EMP='{0}'";
            sql = string.Format(sql, staffId);
            return DBAccess.RunSQLReturnValInt(sql);
        }

        public int MoveTo(string id, string departmentId)
        {
            string sql = "UPDATE PORT_STAFFDEPT SET FK_DEPT='{0}' WHERE FK_STAFF='{1}'";
            sql = string.Format(sql, departmentId, id);
            return DBAccess.RunSQLReturnValInt(sql);
        }

        public int BatchMoveTo(string[] ids, string departmentId)
        {
            string deleteIds = Tool.ArrayToString(ids);
            string sql = "UPDATE PORT_STAFFDEPT SET FK_DEPT='{0}' WHERE FK_STAFF IN ({1}) ";
            sql = string.Format(sql, departmentId, deleteIds);
            return DBAccess.RunSQLReturnValInt(sql);
        }

        public bool Exists(string No)
        {
            string sql = "SELECT NO FROM PORT_STAFF WHERE NO='{0}'";
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
            return new BP.EIP.Port_Staff(No);
        }

        public System.Data.DataTable GetDT()
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetDT(string[] Ids)
        {
            string queryIds = Tool.ArrayToString(Ids);
            string sql = "SELECT * FROM PORT_STAFF WHERE NO IN ({0})";
            sql = string.Format(sql, queryIds);
            return DBAccess.RunSQLReturnTable(sql);
        }

        public int Delete(string Id)
        {
            BP.EIP.Port_Staff portStaff = new Port_Staff(Id);
            return portStaff.Delete();
        }

        public int BatchDelete(string[] ids)
        {
            string deleteIds = Tool.ArrayToString(ids);
            string sql = "DELETE FROM PORT_STAFF WHERE NO IN ({0})";
            sql = string.Format(sql, deleteIds);
            return DBAccess.RunSQLReturnValInt(sql);
        }

        public int SetDeleted(string[] ids)
        {
            string deleteIds = Tool.ArrayToString(ids);
            string sql = "UPDATE PORT_STAFF SET STATUS='0' WHERE NO IN ({0})";
            sql = string.Format(sql, deleteIds);
            return DBAccess.RunSQLReturnValInt(sql);
        }

        public System.Data.DataTable Search(string searchValue)
        {
            string sql = "SELECT * FROM PORT_STAFF WHERE IDCARD LIKE '%{0}%' OR PHONE LIKE '%{0}%' OR EMAIL LIKE '%{0}%' OR EMPNAME LIKE '%{0}%' OR ADDRESS LIKE '%{0}%'";
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
