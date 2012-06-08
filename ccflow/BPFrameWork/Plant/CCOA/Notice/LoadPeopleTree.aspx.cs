using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.EIP;
using System.Text;

public partial class CCOA_Notice_LoadPeopleTree : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Port_Emps portEmps = new Port_Emps();

        string peopleName = Request.QueryString["PeopleName"];

        if (string.IsNullOrEmpty(peopleName))
        {
            portEmps.RetrieveAll();
        }
        else
        {
            portEmps.Retrieve(Port_EmpAttr.Name, peopleName);
        }

        StringBuilder sb = new StringBuilder();
        sb.Append("[");

        int loopNo = 0;

        foreach (Port_Emp portEmp in portEmps)
        {
            loopNo += 1;
            sb.Append("{id:'");
            sb.Append(portEmp.No);
            sb.Append("',text:'");
            sb.Append(portEmp.Name);
            sb.Append("'}");
            if (loopNo != portEmps.Count)
            {
                sb.Append(",");
            }
        }
        sb.Append("]");

        Response.Write(sb.ToString());
    }
}