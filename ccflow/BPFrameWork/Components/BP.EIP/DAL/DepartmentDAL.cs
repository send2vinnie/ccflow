using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.EIP.Interface;
using BP.DA;
using BP.EIP.Enum;

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
            string departmentId = Tool.ArrayToString(departmentIds);

            string sql = "UPDATE PORT_DEPT SET PID='{0}' WHERE NO IN '({1})'";
            sql = string.Format(sql, parentId, departmentId);
            return DBAccess.RunSQL(sql);
        }

        public int BatchSetCode(IDictionary<string, string> idAndCodes)
        {
            IList<string> sqlList = new List<string>();
            foreach (KeyValuePair<string, string> keyAndValue in idAndCodes)
            {
                string sql = "UPDATE PORT_DEPT SET CODE='{0}' WHERE NO='{1}'";
                sql = string.Format(sql, keyAndValue.Key, keyAndValue.Value);
                sqlList.Add(sql);
            }
            return XDBHelper.RunSql(sqlList);
        }

        public bool Exists(string No)
        {
            string sql = "SELECT NO FROM PORT_DEPT WHERE NO='{0}'";
            sql = string.Format(sql, No);
            return XDBHelper.RunSQLReturnCOUNT(sql) > 0;
        }

        public void Add(BaseEntity entity, out StatusCode statusCode, out string statusMessage)
        {
            Port_Dept portDept = entity as Port_Dept;
            try
            {
                portDept.Insert();
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
            return new Port_Dept(No);
        }

        public System.Data.DataTable GetDT()
        {
            var list = new BP.EIP.Port_Depts();
            list.RetrieveAll();
            return list.ToDataSet().Tables[0];
        }

        public System.Data.DataTable GetDT(string[] Ids)
        {
            string selectIds = Tool.ArrayToString(Ids);
            string sql = "SELECT * FROM PORT_DEPT WHERE NO IN ({0})";
            sql = string.Format(sql, selectIds);
            return DBAccess.RunSQLReturnTable(sql);
        }

        public int Delete(string Id)
        {
            BP.EIP.Port_Dept portDept = new Port_Dept(Id);
            return portDept.Delete();
        }

        public int BatchDelete(string[] ids)
        {
            string deleteIds = Tool.ArrayToString(ids);
            string sql = "DELETE FROM PORT_DEPT WHERE NO IN ({0})";
            sql = string.Format(sql, deleteIds);
            return DBAccess.RunSQL(sql);
        }

        public int SetDeleted(string[] ids)
        {
            string deleteIds = Tool.ArrayToString(ids);
            string sql = "UPDATE PORT_DEPT SET STATUS='0' WHERE ID IN {0}";
            sql = string.Format(sql, deleteIds);
            return DBAccess.RunSQL(sql);
        }

        public System.Data.DataTable Search(string searchValue)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("SELECT * FROM PORT_DEPT WHERE NAME LIKE '%{0}%' ");
            sqlBuilder.Append(" OR FULL_NAME LIKE '%{0}%'");
            string sql = sqlBuilder.ToString();
            sql = string.Format(sql, searchValue);
            return DBAccess.RunSQLReturnTable(sql);
        }

        public int Update(BaseEntity entity, out StatusCode statusCode, out string statusMessage)
        {
            try
            {
                BP.EIP.Port_Dept portDept = entity as BP.EIP.Port_Dept;
                statusCode = StatusCode.Success;
                statusMessage = "";
                return portDept.Update();
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
