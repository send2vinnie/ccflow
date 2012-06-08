using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.EIP;
using System.Text;

public partial class CCOA_Notice_LoadRoleTree : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Port_Stations portStations = new Port_Stations();

        string roleName = Request.QueryString["RoleName"];
        if (string.IsNullOrEmpty(roleName))
        {
            portStations.RetrieveAll();
        }
        else
        {
            portStations.Retrieve(Port_StationAttr.Name, roleName);
        }
        StringBuilder sb = new StringBuilder();
        sb.Append("[");

        int loopNo = 0;
        
        foreach (Port_Station portStation in portStations)
        {
            loopNo += 1;
            sb.Append("{id:'");
            sb.Append(portStation.No);
            sb.Append("',text:'");
            sb.Append(portStation.Name);
            sb.Append("'}");
            if (loopNo != portStations.Count)
            {
                sb.Append(",");
            }
        }
        sb.Append("]");

        Response.Write(sb.ToString());
    }
}