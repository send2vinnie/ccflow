using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BP.WF;
using BP.En;
using BP.DA;

public partial class WF_UC_WFRpt : BP.Web.UC.UCBase3
{


    #region 属性
    public string DoType
    {
        get
        {
            return this.Request.QueryString["DoType"];
        }
    }
    public int StartNodeID
    {
        get
        {
            return int.Parse(this.FK_Flow + "01");
        }
    }
    public string FK_Flow
    {
        get
        {
            string flow = this.Request.QueryString["FK_Flow"];
            if (flow == null)
            {
                throw new Exception("@没有获取它的流程编号。");
                //BP.WF.CHOfFlow fl = new CHOfFlow(this.WorkID);
                //return fl.FK_Flow;
            }
            else
            {
                return flow;
            }
        }
    }
    public int WorkID
    {
        get
        {
            return int.Parse(this.Request.QueryString["WorkID"]);
        }
    }
    public int NodeID
    {
        get
        {
            try
            {
                return int.Parse(this.Request.QueryString["NodeID"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    public int FID
    {
        get
        {
            try
            {
                return int.Parse(this.Request.QueryString["FID"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Pub1.Clear();
        Flow fl = new Flow(this.FK_Flow);
        switch (fl.HisFlowType)
        {
            case FlowType.Panel:
                this.BindPanel(fl);
                break;
            default:
                if (this.WorkID == this.FID)
                    this.BindRavie(fl);
                else
                    this.BindBrach(fl);
                break;
        }
    }
    public void BindPanel(Flow fl)
    {
        try
        {
            WorkFlow wf = new WorkFlow(new Flow(this.FK_Flow), this.WorkID, this.FID);
            this.Pub1.BindWFRptV2(wf);
        }
        catch (Exception ex)
        {
            this.Pub1.AddHR();
            this.Pub1.AddMsgOfInfo("生成工作报告期间出现错误,造成此原因如下:", "<BR><BR>1, 此流程为虚拟流程。<BR><BR>2，流程数据已经删除。3，参与此流程上的人员编号错误。<BR><BR>技术信息:" + ex.Message);
        }
    }

    #region 分流
    /// <summary>
    /// 分流干流
    /// </summary>
    /// <param name="fl"></param>
    public void BindRavie(Flow fl)
    {
        //  WorkFlow wf = new WorkFlow(fl, this.WorkID, this.FID);

        this.Pub1.AddH4("关于（" + fl.Name + "）工作报告");
        this.Pub1.AddHR();

        Node ndStart = fl.HisStartNode;
        StartWork sw = ndStart.HisWork as StartWork;
        sw.OID = this.WorkID;
        sw.Retrieve();

        this.Pub1.Add("流程发起人：" + sw.RecText + "，发起日期：" + sw.RDT + " ；流程状态：" + sw.WFStateT);

        Nodes nds = fl.HisNodes;
        foreach (Node nd in nds)
        {
            if (nd.HisFNType != FNType.Branch)
                continue;

            string sql = "SELECT OID,Rec,RDT FROM ND" + nd.NodeID + " WHERE FID=" + this.FID;
            DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
                continue;

            this.Pub1.AddBR("分支流程如下：");
            this.Pub1.AddTable();
            this.Pub1.AddTR();
            this.Pub1.AddTDTitle("接受人");
            this.Pub1.AddTDTitle("接受日期");
            this.Pub1.AddTDTitle("工作报告");
            this.Pub1.AddTREnd();

            foreach (DataRow dr in dt.Rows)
            {
                this.Pub1.AddTR();
                this.Pub1.AddTD(dr["Rec"].ToString());
                this.Pub1.AddTD(dr["RDT"].ToString());
                this.Pub1.AddTD("<a href='WFRpt.aspx?FK_Flow=" + this.FK_Flow + "&FID=" + this.FID + "&WorkID=" + dr["OID"] + "' target=_blank >工作报告</a>");
                this.Pub1.AddTREnd();
            }
            this.Pub1.AddTableEnd();
            break;
        }

        ReturnWorks rws = new ReturnWorks();
        rws.Retrieve(ReturnWorkAttr.WorkID, this.WorkID);

        ForwardWorks fws = new ForwardWorks();
        fws.Retrieve(ReturnWorkAttr.WorkID, this.WorkID);

        WorkNodes wns = new WorkNodes();
        wns.GenerByFID(fl, this.FID);

        this.Pub1.BindWorkNodes(wns, rws, fws);

        this.Pub1.AddHR("流程报告结束");
    }
    /// <summary>
    /// 分流支流
    /// </summary>
    /// <param name="fl"></param>
    public void BindBrach(Flow fl)
    {
        //  WorkFlow wf = new WorkFlow(fl, this.WorkID, this.FID);
        WorkNodes wns = new WorkNodes();
        wns.GenerByFID(fl, this.FID);

        this.Pub1.AddH4("关于（" + fl.Name + "）工作报告");
        this.Pub1.AddHR();

        Node nd = fl.HisStartNode;
        StartWork sw = nd.HisWork as StartWork;
        sw.FID = this.FID;
        sw.OID = this.WorkID;
        sw.RetrieveFID();

        this.Pub1.Add("流程发起人：" + sw.RecText + "，发起日期：" + sw.RDT + " ；流程状态：" + sw.WFStateT);

        ReturnWorks rws = new ReturnWorks();
        rws.Retrieve(ReturnWorkAttr.WorkID, this.WorkID);

        ForwardWorks fws = new ForwardWorks();
        fws.Retrieve(ReturnWorkAttr.WorkID, this.WorkID);

        this.Pub1.BindWorkNodes(wns, rws, fws);

        this.Pub1.AddHR("流程报告结束");
    }
    #endregion 分流


    #region 合流
    /// <summary>
    /// 合流干流
    /// </summary>
    /// <param name="fl"></param>
    public void BindHeLiuRavie(Flow fl)
    {

    }
    /// <summary>
    /// 合流支流
    /// </summary>
    /// <param name="fl"></param>
    public void BindHeLiuBrach(Flow fl)
    {

    }
    #endregion 合流

    public void BindFHLWork(GenerFH hf)
    {
        this.Pub1.AddH4(hf.Title);
        this.Pub1.AddHR();

        this.Pub1.AddFieldSet("当前节点基本信息");
        this.Pub1.AddBR("接受时间：" + hf.RDT);
        this.Pub1.AddBR("接受人：" + hf.ToEmpsMsg);
        this.Pub1.AddFieldSetEndBR();

        GenerWorkFlows gwfs = new GenerWorkFlows();
        gwfs.Retrieve(GenerWorkFlowAttr.FID, this.FID);

        this.Pub1.AddFieldSet("分流人员信息");

        this.Pub1.AddTable();
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("标题");
        this.Pub1.AddTDTitle("发起人");
        this.Pub1.AddTDTitle("发起日期");
        this.Pub1.AddTDTitle("");
        this.Pub1.AddTREnd();

        foreach (GenerWorkFlow gwf in gwfs)
        {
            if (gwf.WorkID == this.FID)
                continue;

            this.Pub1.AddTR();
            this.Pub1.AddTD(gwf.Title);
            this.Pub1.AddTD(gwf.Rec);
            this.Pub1.AddTD(gwf.RDT);
            this.Pub1.AddTD("<a href='WFRpt.aspx?WorkID=" + gwf.WorkID + "&FK_Flow=" + gwf.FK_Flow + "&FID=" + gwf.FID + "' target=_b" + gwf.WorkID + ">工作报告</a>");
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEndWithBR();
        this.Pub1.AddFieldSetEnd();
    }


}
