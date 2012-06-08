using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using BP.EIP;

public partial class CCOA_Notice_LoadDeptTree : System.Web.UI.Page
{
    BP.EIP.Port_Depts depts = null;

    private IList<string> m_DeptNos = new List<string>();

    protected void Page_Load(object sender, EventArgs e)
    {

        depts = new BP.EIP.Port_Depts();

        Port_Depts queryDepts = null;

        if (string.IsNullOrEmpty(Request.QueryString["DeptName"]))
        {
            depts.RetrieveAll();
            queryDepts = this.GetParentDepts();
        }
        else
        {
            string deptName = Request.QueryString["DeptName"];
            BP.EIP.Port_Dept portDept = new BP.EIP.Port_Dept();
            depts = portDept.GetPortDepartmentsByNameWithChild(deptName);
            queryDepts = depts;
        }

        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("[");

        foreach (BP.EIP.Port_Dept dept in queryDepts)
        {
            if (!this.m_DeptNos.Contains(dept.No))
            {
                jsonBuilder.Append("{id:'" + dept.No + "',text:'" + dept.Name + "',pid:'" + dept.Pid + "'");

                IList<BP.EIP.Port_Dept> childDepts = this.GetChildDepts(dept.No, depts);
                if (childDepts.Count > 0)
                {
                    this.GetDeptJson(jsonBuilder, childDepts);
                }

                jsonBuilder.Append("},");
            }
        }
        string jsonString = jsonBuilder.ToString().TrimEnd(',');
        jsonString += "]";
        Response.Write(jsonString);
    }

    private Port_Depts GetParentDepts()
    {
        Port_Depts portDepts = new Port_Depts();
        foreach (BP.EIP.Port_Dept dept in depts)
        {
            if (dept.Pid.Trim() == string.Empty)
            {
                portDepts.AddEntity(dept);
            }
        }
        return portDepts;
    }

    private IList<BP.EIP.Port_Dept> GetChildDepts(string parentId, BP.EIP.Port_Depts depts)
    {
        IList<BP.EIP.Port_Dept> childDepts = new List<BP.EIP.Port_Dept>();

        foreach (BP.EIP.Port_Dept dept in depts)
        {
            if (dept.Pid == parentId)
            {
                childDepts.Add(dept);
            }
        }
        return childDepts;
    }

    private void GetDeptJson(StringBuilder sb, IList<BP.EIP.Port_Dept> childDepts)
    {
        sb.Append(",children:[");
        int loopNo = 0;
        foreach (BP.EIP.Port_Dept dept in childDepts)
        {
            loopNo += 1;
            sb.Append("{id:'" + dept.No + "',text:'" + dept.Name + "',pid:'" + dept.Pid + "'");
            IList<BP.EIP.Port_Dept> cldDepts = this.GetChildDepts(dept.No, depts);
            if (cldDepts.Count > 0)
            {
                this.GetDeptJson(sb, cldDepts);
            }
            sb.Append("}");
            if (loopNo != childDepts.Count)
            {
                sb.Append(",");
            }
            m_DeptNos.Add(dept.No);
        }
        sb.Append("]");
    }

}