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
using BP.Port;
using BP.Sys;
using BP.Port;
using BP.Web.Controls;
using BP.DA;
using BP.En;
using BP.Web;

public partial class WF_UC_ReturnWork : BP.Web.UC.UCBase3
{
    #region FK_Node
    public string FK_Flow
    {
        get
        {
            return this.Request.QueryString["FK_Flow"];
        }
    }
    public int FK_Node
    {
        get
        {
            try
            {
                return int.Parse(this.Request.QueryString["FK_Node"]);
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
    public Int64 WorkID
    {
        get
        {
            try
            {
                return Int64.Parse(this.Request.QueryString["WorkID"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    public DDL DDL1
    {
        get
        {
            return this.ToolBar1.GetDDLByID("DDL1");
        }
    }
    public TextBox  TB1
    {
        get
        {
            return this.Pub1.GetTextBoxByID("TB_Doc");
        }
    }
    #endregion FK_Node

    public void BindItWorkHL(BP.WF.Node nd)
    {
        this.ToolBar1.AddBtn("Btn_OK", this.ToE("OK", "确定"));
        this.ToolBar1.GetBtnByID("Btn_OK").Attributes["onclick"] = " return confirm('" + this.ToE("AYS", "您确定要执行吗?") + "');";
        this.ToolBar1.GetBtnByID("Btn_OK").Click += new EventHandler(WF_UC_ReturnWork_HL_Click);
        //if (nd.IsCanHidReturn)
        //{
        //    this.ToolBar1.AddBtn("Btn_ReturnHid", "隐形退回");
        //    this.ToolBar1.GetBtnByID("Btn_ReturnHid").Attributes["onclick"] = " return confirm('" + this.ToE("AYS", "您确定要执行吗?") + "');";
        //    this.ToolBar1.GetBtnByID("Btn_ReturnHid").Click += new EventHandler(WF_UC_ReturnWork_HL_Click);
        //}
        //this.ToolBar1.AddBtn("Btn_Cancel", this.ToE("Cancel", "取消"));
        //this.ToolBar1.GetBtnByID("Btn_Cancel").Click += new EventHandler(WF_UC_ReturnWork_Click);

        TextBox tb = new TextBox();
        tb.TextMode = TextBoxMode.MultiLine;
        tb.ID = "TB_Doc";
        tb.Rows = 20;
        tb.Columns = 50;
        this.Pub1.Add(tb);
        if (this.IsPostBack == false)
        {
            try
            {
                WorkNode wn = new WorkNode(this.FID, this.FK_Node);
                Work wk = wn.HisWork;
                this.TB1.Text = this.ToEP4("WBackInfo",
                    "{0}同志: \n   您在{1}处理的“{2}”工作有错误，需要您重新办理．\n\n  此致!!!   \n {3}",
                    wk.Rec + wk.RecText, wk.CDT, wn.HisNode.Name, WebUser.Name + BP.DA.DataType.CurrentDataTime);
            }
            catch (Exception ex)
            {
                this.Pub1.AddMsgOfWarning("提示:", "@:" + this.ToE("WorkBackErr", "下列原因造成不能退回:") + ex.Message);
            }
        }
    }

    public void BindItWorkFL(BP.WF.Node nd)
    {
        this.ToolBar1.AddLab("sd", "<b>退回到:</b>");
        this.ToolBar1.AddDDL("DDL1");

        this.ToolBar1.AddBtn("Btn_OK", this.ToE("OK", "确定"));
        this.ToolBar1.GetBtnByID("Btn_OK").Attributes["onclick"] = " return confirm('" + this.ToE("AYS", "您确定要执行吗?") + "');";
        this.ToolBar1.GetBtnByID("Btn_OK").Click += new EventHandler(WF_UC_ReturnWork_FL_Click);

        WorkNodes wns = new WorkNodes();
        if (wns.Count == 0)
            wns.GenerByFID(nd.HisFlow, this.FID);
        foreach (WorkNode mywn in wns)
        {
            if (mywn.HisNode.NodeID == this.FK_Node)
                continue;

            string sql = "SELECT IsPass FROM WF_GenerWorkerList WHERE WorkID=" + this.FID + " AND FK_Node=" + mywn.HisNode.NodeID;
            DataTable dt= DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count != 0)
            {
                string val = dt.Rows[0][0].ToString();
                if (val == "3")
                    continue;
            }
            this.DDL1.Items.Add(new ListItem(mywn.HisWork.RecText + "=>" + mywn.HisNode.Name, mywn.HisNode.NodeID.ToString()));
        }

        TB tb = new TB();
        tb.TextMode = TextBoxMode.MultiLine;
        tb.ID = "TB_Doc";
        tb.Rows = 15;
        tb.Columns = 50;
        this.Pub1.Add(tb);
        if (this.IsPostBack == false)
        {
            try
            {
                WorkNode wn = new WorkNode(this.WorkID, this.FK_Node);
                Work wk = wn.HisWork;
                this.TB1.Text = this.ToEP4("WBackInfo",
                    "{0}同志: \n   您在{1}处理的“{2}”工作有错误，需要您重新办理．\n\n  此致!!!   \n {3}",
                    wk.Rec + wk.RecText, wk.CDT, wn.HisNode.Name, WebUser.Name + BP.DA.DataType.CurrentDataTime);
            }
            catch (Exception ex)
            {
                this.Pub1.AddMsgOfWarning("提示:", "@:" + this.ToE("WorkBackErr", "下列原因造成不能退回:") + ex.Message);
            }
        }
    }
    void WF_UC_ReturnWork_FL_Click(object sender, EventArgs e)
    {
        try
        {
            WorkNode wn = new WorkNode(this.WorkID, this.FK_Node);
            Work wk = wn.HisWork;
            wk.OID = this.WorkID;
            wk.FID = this.FID;
            wk.Retrieve();
            WorkNode mywn = null;
            mywn = wn.DoReturnWork(this.ToolBar1.GetDDLByID("DDL1").SelectedItemIntVal, this.TB1.Text);

            // 退回事件。
            string msg = mywn.HisNode.HisNDEvents.DoEventNode(EventListOfNode.ReturnAfter, wk);

            this.ToMsg(this.ToEP2("WReInfo", "@任务被你成功退回到【{0}】，退回给【{1}】。", mywn.HisNode.Name, mywn.HisWork.Rec),
                "info");
            return;
        }
        catch (Exception ex)
        {
            this.ToMsg(ex.Message, "info");
        }
    }
    void WF_UC_ReturnWork_HL_Click(object sender, EventArgs e)
    {
        try
        {
            WorkNode wn = new WorkNode(this.FID, this.FK_Node);
            Work wk = wn.HisWork;
            WorkNode mywn = null;
            mywn = wn.DoReturnWorkHL(this.WorkID, this.TB1.Text);

            // 退回事件。
            string msg = mywn.HisNode.HisNDEvents.DoEventNode(EventListOfNode.ReturnAfter, wk);
            if (this.PageID == "ReturnWorkSmall")
                this.WinCloseWithMsg(this.ToEP2("WReInfo", "@任务被你成功退回到【{0}】，退回给【{1}】。", mywn.HisNode.Name, mywn.HisWork.Rec));
            else
                this.ToMsg(this.ToEP2("WReInfo", "@任务被你成功退回到【{0}】，退回给【{1}】。", mywn.HisNode.Name, mywn.HisWork.Rec), "info");
        }
        catch (Exception ex)
        {
            this.ToMsg(ex.Message, "info");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Title = this.ToE("WorkBack", "工作退回");
        BP.WF.Node nd = new BP.WF.Node(this.FK_Node);
        switch (nd.HisNodeWorkType)
        {
            case NodeWorkType.WorkHL:
                if (this.FID != 0)
                {
                    this.BindItWorkHL(nd);
                    return;
                }
              break;
            case NodeWorkType.WorkFHL:
                throw new Exception("系统没有判断的情况。");
            case NodeWorkType.WorkFL:
                // this.BindItWorkFL(nd);
                break;
            default:
                if (this.FID != 0)
                {
                    this.BindItWorkFL(nd);
                    return;
                }
                break;
        }

        this.ToolBar1.Add(this.ToE("ReturnTo", "<b>退回到:</b>"));
        this.ToolBar1.AddDDL("DDL1");
        this.ToolBar1.AddBtn("Btn_OK", this.ToE("OK", "确定"));
        this.ToolBar1.GetBtnByID("Btn_OK").Attributes["onclick"] = " return confirm('" + this.ToE("AYS", "您确定要执行吗?") + "');";
        this.ToolBar1.GetBtnByID("Btn_OK").Click += new EventHandler(WF_UC_ReturnWork_Click);

        //if (nd.IsCanHidReturn)
        //{
        //    this.ToolBar1.AddBtn("Btn_ReturnHid", "隐形退回");
        //    this.ToolBar1.GetBtnByID("Btn_ReturnHid").Attributes["onclick"] = " return confirm('" + this.ToE("AYS", "您确定要执行吗?") + "');";
        //    this.ToolBar1.GetBtnByID("Btn_ReturnHid").Click += new EventHandler(WF_UC_ReturnWork_Click);
        //}

        this.ToolBar1.AddBtn("Btn_Cancel", this.ToE("Cancel", "取消"));
        this.ToolBar1.GetBtnByID("Btn_Cancel").Click += new EventHandler(WF_UC_ReturnWork_Click);
        string appPath = this.Request.ApplicationPath;
        this.ToolBar1.Add("<input type=button value='" + this.ToE("Track", "轨迹") + "' enable=true onclick=\"WinOpen('" + appPath + "/WF/Chart.aspx?WorkID=" + this.WorkID + "&FK_Flow=" + this.FK_Flow + "','ds'); \" />");

        TextBox tb = new TextBox();
        tb.TextMode = TextBoxMode.MultiLine;
        tb.ID = "TB_Doc";
        tb.Rows = 15;
        tb.Columns = 50;
        this.Pub1.Add(tb);
        if (this.IsPostBack == false)
        {
            try
            {
                WorkNode wn = new WorkNode(this.WorkID, this.FK_Node);
                WorkNode pwn = wn.GetPreviousWorkNode();
                switch (pwn.HisNode.HisNodeWorkType)
                {
                    //case NodeWorkType.WorkFL:
                    //case NodeWorkType.StartWorkFL:
                    //case NodeWorkType.WorkFHL:
                    //    this.AddMsgOfInfo("提示", "您的上一步骤是分合流节点，您不能执行退回。");
                    //    return;
                    case NodeWorkType.WorkHL:
                    default:
                        break;
                }

                WorkNodes wns = new WorkNodes();
                //if (wn.HisNode.HisFNType == FNType.River)
                //    wns.GenerByFID(wn.HisNode.HisFlow, this.WorkID);

                if (wns.Count == 0)
                    wns.GenerByWorkID(wn.HisNode.HisFlow, this.WorkID);


                switch (nd.HisReturnRole)
                {
                    case ReturnRole.CanNotReturn:
                        return;
                    case ReturnRole.ReturnAnyNodes:
                        foreach (WorkNode mywn in wns)
                        {
                            if (mywn.HisNode.NodeID == this.FK_Node)
                                continue;

                            this.DDL1.Items.Add(new ListItem(mywn.HisWork.RecText + "=>" + mywn.HisNode.Name, mywn.HisNode.NodeID.ToString()));
                        }
                        break;
                    case ReturnRole.ReturnPreviousNode:
                        int nodeId = wn.GetPreviousWorkNode().HisNode.NodeID;
                        foreach (WorkNode mywn in wns)
                        {
                            if (mywn.HisNode.NodeID != this.FK_Node)
                                continue;

                            this.DDL1.Items.Add(new ListItem(mywn.HisWork.RecText + "=>" + mywn.HisNode.Name, mywn.HisNode.NodeID.ToString()));
                        }
                        break;
                    case ReturnRole.ReturnSpecifiedNodes: //退回指定的节点。
                        NodeReturns rnds = new NodeReturns();
                        rnds.Retrieve(NodeReturnAttr.FK_Node, this.FK_Node);
                        foreach (WorkNode mywn in wns)
                        {
                            if (mywn.HisNode.NodeID == this.FK_Node)
                                continue;

                            if (rnds.Contains(NodeReturnAttr.ReturnN, mywn.HisNode.NodeID) == false)
                                continue;

                            this.DDL1.Items.Add(new ListItem(mywn.HisWork.RecText + "=>" + mywn.HisNode.Name, mywn.HisNode.NodeID.ToString()));
                        }
                        break;
                    default:
                        throw new Exception("@没有判断的退回类型。");
                }

                this.DDL1.SetSelectItem(pwn.HisNode.NodeID);
                this.DDL1.Enabled = true;
                Work wk = pwn.HisWork;
                this.TB1.Text = this.ToEP4("WBackInfo",
                    "{0}同志: \n  您在{1}处理的“{2}”工作有错误，需要您重新办理．\n\n  此致!!!   \n {3}",
                    wk.Rec + wk.RecText, wk.CDT, pwn.HisNode.Name, WebUser.Name + BP.DA.DataType.CurrentDataTime);
            }
            catch (Exception ex)
            {
                this.Pub1.AddMsgOfWarning("提示:", "@:" + this.ToE("WorkBackErr", "下列原因造成不能退回:") + ex.Message);
            }
        }
    }
    void WF_UC_ReturnWork_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        switch (btn.ID)
        {
            case "Btn_Cancel":
                this.Response.Redirect("MyFlow"+Glo.FromPageType+".aspx?FK_Flow=" + this.FK_Flow + "&WorkID=" + this.WorkID+"&FK_Node="+this.FK_Node, true);
                return;
            default:
                break;
        }

        try
        {

            WorkNode wn = new WorkNode(this.WorkID, this.FK_Node);
            Work wk = wn.HisWork;
            WorkNode mywn = null;
            if (btn.ID == "Btn_ReturnHid")
            {
                mywn = wn.DoReturnWork(this.DDL1.SelectedItemIntVal, this.TB1.Text, true);
            }
            else
            {
                mywn = wn.DoReturnWork(this.DDL1.SelectedItemIntVal, this.TB1.Text);
            }

            // 退回事件。
            string msg = mywn.HisNode.HisNDEvents.DoEventNode(EventListOfNode.ReturnAfter, wk);

            this.ToMsg(this.ToEP2("WReInfo", "@任务被你成功退回到【{0}】，退回给【{1}】。", mywn.HisNode.Name, mywn.HisWork.Rec) + msg, "info");
            return;
        }
        catch (Exception ex)
        {
            this.ToMsg(ex.Message, "info");
        }
    }

    public void ToMsg(string msg, string type)
    {
        this.Session["info"] = msg;
        this.Response.Redirect("MyFlowInfo" + Glo.FromPageType + ".aspx?FK_Flow=" + this.FK_Flow + "&FK_Type=" + type + "&FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID, false);
    }
}
