using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
    public string DoType
    {
        get
        {
            return this.Request.QueryString["DoType"];
        }
    }
    #endregion attrs

    protected void Page_Load(object sender, EventArgs e)
    {
        switch (this.DoType)
        {
            case "Chart": //流程图形.
                FlowChart(this.FK_Flow);
                this.Title = "流程图";
                break;
            case "DT": // 流程运行数据。。
                FlowDT(this.FK_Flow, this.WorkID);
                break;
            case "ALS": // 流程数据分析。
                FlowALS(this.FK_Flow);
                break;
            default:
            //    throw new Exception("参数不完整.");
                break;
        }
    }
    public void FlowChart(string fk_flow)
    {
    }
    public void FlowDT(string fk_flow, Int64 workid)
    {
    }
    public void FlowALS(string fk_flow)
    {
    }
}