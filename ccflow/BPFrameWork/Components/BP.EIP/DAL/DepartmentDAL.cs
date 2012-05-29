using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.EIP.Interface;
using BP.DA;

namespace BP.EIP.DAL
{
    public partial class DepartmentDAL : IDepartment
    {

        public System.Data.DataTable GetParentDepartments(string departmentId)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("SELECT * FROM PORT_DEPT ");
            sqlBuilder.Append(" WHERE NO <> '{0}'");
            sqlBuilder.Append(" START WITH NO = '{0}'");
            sqlBuilder.Append(" CONNECT BY PRIOR PID = NO");
            string sql = string.Format(sqlBuilder.ToString(), departmentId);
            return DBAccess.RunSQLReturnTable(sql);
        }

        public System.Data.DataTable GetDTInner(string departmentId)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("SELECT * FROM PORT_DEPT ");
            sqlBuilder.Append(" WHERE NO <> '{0}'");
            sqlBuilder.Append(" START WITH NO = '{0}'");
            sqlBuilder.Append(" CONNECT BY PRIOR NO = PID");
            string sql = string.Format(sqlBuilder.ToString(), departmentId);
            return DBAccess.RunSQLReturnTable(sql);
        }

        public System.Data.DataTable GetStaffs(string departmentId)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("SELECT PS.* ");
            sqlBuilder.Append(" FROM PORT_STAFFDEPT PSD");
            sqlBuilder.Append(" LEFT JOIN PORT_DEPT PD");
            sqlBuilder.Append(" ON PSD.FK_DEPT = PD.NO");
            sqlBuilder.Append(" LEFT JOIN PORT_STAFF PS");
            sqlBuilder.Append(" ON PSD.FK_STAFF = PS.NO");
            sqlBuilder.Append(" WHERE PSD.FK_DEPT='{0}'");
            string sql = string.Format(sqlBuilder.ToString(), departmentId);
            return DBAccess.RunSQLReturnTable(sql);
        }

        public System.Data.DataTable GetParentStaffs(string departmentId)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("SELECT PS.* ");
            sqlBuilder.Append(" FROM PORT_STAFFDEPT PSD");
            sqlBuilder.Append(" LEFT JOIN PORT_DEPT PD");
            sqlBuilder.Append(" ON PSD.FK_DEPT = PD.NO");
            sqlBuilder.Append(" LEFT JOIN PORT_STAFF PS");
            sqlBuilder.Append(" ON PSD.FK_STAFF = PS.NO");
            sqlBuilder.Append("  WHERE PD.PID='{0}'");
            string sql = string.Format(sqlBuilder.ToString(), departmentId);
            return DBAccess.RunSQLReturnTable(sql);
        }

        public int MoveTo(string departmentId, string parentId)
        {
            string sql = "UPDATE PORT_DEPT SET PID='{0}' WHERE NO='{1}'";
            sql = string.Format(sql, parentId, departmentId);
            return DBAccess.RunSQL(sql);
        }

        public int BatchMoveTo(string[] departmentIds, string parentId)
        {
            string departmentId = string.Empty;

            foreach (string deptId in departmentIds)
            {
                departmentId += "'" + deptId + "',";
            }

            departmentId = departmentId.TrimEnd(',');

            string sql = "UPDATE PORT_DEPT SET PID='{0}' WHERE NO IN '({1})'";
            sql = string.Format(sql, parentId, departmentId);
            return DBAccess.RunSQL(sql);
        }

        public int BatchSetCode(IDictionary<string, string> idAndCodes)
        {
            throw new NotImplementedException();
           
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
