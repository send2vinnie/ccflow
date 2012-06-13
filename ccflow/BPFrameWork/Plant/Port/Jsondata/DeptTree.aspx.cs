using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Port_Jsondata_DeptTree : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BP.EIP.Interface.IDepartment idal = BP.EIP.DALFactory.DataAccess.CreateDepartment();

        DataTable dt = idal.GetDT();

        DataTable dtJson = new DataTable();
        dtJson.Columns.Add("id");
        dtJson.Columns.Add("text");
        dtJson.Columns.Add("pid");

        if (dt!=null && dt.Rows.Count>0)
        {
            foreach (DataRow row in dt.Rows)
            {
                DataRow r = dtJson.NewRow();
                r["id"] = row["No"].ToString();
                r["text"] = row["Name"].ToString();
                r["pid"] = row["Pid"].ToString();
                
                dtJson.Rows.Add(r);
            }
        }

        string jsonData = Lizard.Tools.JsonHelper.DataTable2Json(dtJson);

        Response.Write(jsonData);
    }
}