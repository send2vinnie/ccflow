using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.Port;
using BP.Sys;
using BP.Web.Controls;
using BP.DA;
using BP.En;
using BP.Web;

public partial class WF_BPR : WebPage
{
    #region 属性
    public string FK_Flow
    {
        get
        {
            return this.Request.QueryString["FK_Flow"];
        }
    }
    public string FK_Node
    {
        get
        {
            return this.Request.QueryString["FK_Node"];
        }
    }
    public string FK_NY
    {
        get
        {
            return this.Request.QueryString["FK_NY"];
        }
    }
    public string FK_Emp
    {
        get
        {
            return this.Request.QueryString["FK_Emp"];
        }
    }
    #endregion 属性

    public string GetDoType
    {
        get
        {
            if (this.FK_NY == null && this.FK_Emp == null && this.FK_Node == null)
                return "1.ShowFlowNodes";


            return "1.ShowFlowNodes";
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        Flow fl = new Flow(this.FK_Flow);
        this.Title = "流程成本分析-"+fl.Name;

        switch (this.GetDoType)
        {
            case "1.ShowFlowNodes":
            default:
                this.Bind1_ShowFlowNodes(fl);
                break;
        }
    }
    public void Bind1_ShowFlowNodes(Flow fl)
    {

        this.Pub1.AddTable();
        this.Pub1.AddCaptionLeft(fl.Name);
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("Idx");
        this.Pub1.AddTDTitle("步骤");
        this.Pub1.AddTDTitle("节点");
        this.Pub1.AddTDTitle("工作数");
        this.Pub1.AddTDTitle("平均用时(天)");
        this.Pub1.AddTDTitle("人员数");
        this.Pub1.AddTDTitle("岗位");
        this.Pub1.AddTREnd();
        Nodes nds = fl.HisNodes;

        int idx = 0;
        bool is1 = false;
        foreach (BP.WF.Node nd in nds)
        {
            idx++;
            is1=this.Pub1.AddTR(is1);
            this.Pub1.AddTDIdx(idx);
            this.Pub1.AddTD("第:"+nd.Step+"步");
            this.Pub1.AddTD(nd.Name);

            this.Pub1.AddTD(DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM ND"+nd.NodeID+" " ) );

            this.Pub1.AddTD(DBAccess.RunSQLReturnValInt("SELECT IsNULL( Avg(dbo.GetSpdays(RDT,CDT)),0) FROM ND" + nd.NodeID + " "));

            this.Pub1.AddTD(DBAccess.RunSQLReturnValInt("SELECT COUNT(DISTINCT Rec) FROM ND" + nd.NodeID + " "));


            this.Pub1.AddTDDoc(nd.HisStationsStr, nd.HisStationsStr);
            this.Pub1.AddTREnd();
        }

        this.Pub1.AddTableEnd();


    }
}