using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BP.Web;
using BP.CCOA;

public partial class CCOA_TodoList : System.Web.UI.Page
{
    public List<OA_TodoWork> WorkList
    {
        get
        {
            return (List<OA_TodoWork>)ViewState["WorkList"];
        }
        set
        {
            ViewState["WorkList"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            WorkList = new List<OA_TodoWork>();
            DataTable dt = BP.WF.Dev2Interface.DB_GenerEmpWorksOfDataTable(WebUser.No);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    OA_TodoWork work = new OA_TodoWork();
                    work.FlowName = row["FlowName"].ToString();
                    work.NodeName = row["NodeName"].ToString();
                    work.RDT = row["RDT"].ToString();
                    work.Starter = row["Starter"].ToString();
                    work.Title = row["Title"].ToString();
                    string url = string.Format(
                        "../../../ccFlow/WF/MyFlowSmall.aspx?FK_Node={0}&FK_Flow={1}&FID={2}&WorkID={3}",
                        row["FK_NODE"], 
                        row["FK_Flow"].ToString(),
                        row["FID"].ToString(),
                        row["WorkID"].ToString());
                    work.Url = url;
                    WorkList.Add(work);
                }
            }
        }
    }
}