using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class Port_Port_Port_Json : System.Web.UI.Page
{
    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    BP.EIP.Interface.IDepartment idal = BP.EIP.DALFactory.DataAccess.CreateDepartment();

    //    DataTable dtAll = idal.GetDT();
    //    string jsonData = Lizard.Tools.JsonHelper.DataTable2Json(dtAll);

    //    Response.Write(jsonData);
    //}

    BP.EIP.Port_Depts depts = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        depts = new BP.EIP.Port_Depts();
        depts.RetrieveAll();

        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("[");
        foreach (BP.EIP.Port_Dept dept in this.GetParentDepts())
        {
            jsonBuilder.Append("{id:'" + dept.No + "',text:'" + dept.Name + "',pid:'" + dept.Pid + "'");

            IList<BP.EIP.Port_Dept> childDepts = this.GetChildDepts(dept.No, depts);
            if (childDepts.Count > 0)
            {
                this.GetDeptJson(jsonBuilder, childDepts);
            }

            jsonBuilder.Append("},");
        }
        string jsonString = jsonBuilder.ToString().TrimEnd(',');
        jsonString += "]";
        Response.Write(jsonString);
    }

    private IList<BP.EIP.Port_Dept> GetParentDepts()
    {
        IList<BP.EIP.Port_Dept> parentDepts = new List<BP.EIP.Port_Dept>();

        foreach (BP.EIP.Port_Dept dept in depts)
        {
            if (dept.Pid.Trim() == string.Empty)
            {
                parentDepts.Add(dept);
            }
        }

        return parentDepts;
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
        }
        sb.Append("]");
    }
}