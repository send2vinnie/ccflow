using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.Web;
using BP.WF;
using BP.En;
using BP.Sys;


public partial class WF_Chart : WebPage
{
    #region attrs
    public Int64 WorkID
    {
        get
        {
            return Int64.Parse(this.Request.QueryString["WorkID"]);
        }
    }
    public Int64 FID
    {
        get
        {
            return Int64.Parse(this.Request.QueryString["FID"]);
        }
    }
    public int FK_Node
    {
        get
        {
            return int.Parse(this.Request.QueryString["FK_Node"]);
        }
    }
    public string FK_Flow
    {
        get
        {
            return this.Request.QueryString["FK_Flow"];
        }
    }
    #endregion attrs

    protected void Page_Load(object sender, EventArgs e)
    {
        switch (this.DoType)
        {
            case "FlowChart": // 流程图形。
                break;
            case "FlowDT": // 流程运行数据。。
                break;
            default:
                break;
        }
    }
    public void FlowChart()
    {
    }
    public void FlowDT()
    {
    }
}