using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BP.DA;

namespace BP.EIP
{
    public partial class Port_Dept
    {
        public DataTable GetDepartmentByNameWithChild(string departmentName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM PORT_DEPT ");
            sb.Append("START WITH NAME LIKE '%{0}%' ");
            sb.Append("CONNECT BY PRIOR NO=PID ");
            string sql = sb.ToString();
            sql = string.Format(sql, departmentName);
            return DBAccess.RunSQLReturnTable(sql);
        }

        public Port_Depts GetPortDepartmentsByNameWithChild(string departmentName)
        {
            Port_Depts portDepts = new Port_Depts();
            Port_Dept portDept = new Port_Dept();

            DataTable departmentTable = GetDepartmentByNameWithChild(departmentName);
            foreach (DataRow departmentRow in departmentTable.Rows)
            {
                portDept = new Port_Dept();
                if (departmentRow[Port_DeptAttr.No] != null)
                {
                    portDept.No = departmentRow[Port_DeptAttr.No].ToString();
                }
                if (departmentRow[Port_DeptAttr.Name] != null)
                {
                    portDept.Name = departmentRow[Port_DeptAttr.Name].ToString();
                }
                if (departmentRow[Port_DeptAttr.FullName] != null)
                {
                    portDept.FullName = departmentRow[Port_DeptAttr.FullName].ToString();
                }
                if (departmentRow[Port_DeptAttr.Pid] != null)
                {
                    portDept.Pid = departmentRow[Port_DeptAttr.Pid].ToString();
                }
                if (departmentRow[Port_DeptAttr.Status] != null)
                {
                    string status = departmentRow[Port_DeptAttr.Status].ToString();
                    if (status != string.Empty)
                    {
                        portDept.Status = int.Parse(departmentRow[Port_DeptAttr.Status].ToString());
                    }
                    else
                    {
                        portDept.Status = 1;
                    }
                }
                if (departmentRow[Port_DeptAttr.Code] != null)
                {
                    portDept.Code = departmentRow[Port_DeptAttr.Code].ToString();
                }
                portDepts.AddEntity(portDept);
            }

            return portDepts;
        }
    }
}
