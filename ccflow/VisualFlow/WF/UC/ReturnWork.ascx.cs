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
        //if (nd.IsCanHidReturn)
        //{
        //    this.ToolBar1.AddBtn("Btn_ReturnHid", "隐形退回");
        //    this.ToolBar1.GetBtnByID("Btn_ReturnHid").Attributes["onclick"] = " return confirm('" + this.ToE("AYS", "您确定要执行吗?") + "');";
        //    this.ToolBar1.GetBtnByID("Btn_ReturnHid").Click += new EventHandler(WF_UC_ReturnWork_HL_Click);
        //}
        //this.ToolBar1.AddBtn("Btn_Cancel", this.ToE("Cancel", "取消"));
        //this.ToolBar1.GetBtnByID("Btn_Cancel").Click += new EventHandler(WF_UC_ReturnWork_Click);

        this.ToolBar1.AddBtn("Btn_OK", this.ToE("OK", "确定退回"));
        this.ToolBar1.GetBtnByID("Btn_OK").Attributes["onclick"] = " return confirm('" + this.ToE("AYS", "您确定要执行吗?") + "');";
        this.ToolBar1.GetBtnByID("Btn_OK").Click += new EventHandler(WF_UC_ReturnWork_HL_Click);

     //FHLFlow.aspx?WorkID=318&FID=120&FK_Flow=006&FK_Node=604

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
                //int fk_node = DBAccess.RunSQLReturnValInt("SELECT FK_Node FROM WF_GenerWorkFlow WHERE  WorkID=" + this.WorkID);
                //BP.WF.Node mynd = new BP.WF.Node(fk_node);
                //string fk_empText = DBAccess.RunSQLReturnString("SELECT FK_EmpText FROM WF_Generworkerlist WHERE FK_Node="+this.FK_Node+" AND WorkID="+this.WorkID);
                this.TB1.Text = "您好: \n   您处理的工作有错误，需要您重新办理．\n\n  此致!!!   \n " + WebUser.Name + BP.DA.DataType.CurrentDataTime;
                //this.TB1.Text = this.ToEP4("WBackInfo",
                //    "{0}同志: \n   您{1}处理的“{2}”工作有错误，需要您重新办理．\n\n  此致!!!   \n {3}",
                //   fk_empText, "", nd.Name, WebUser.Name + BP.DA.DataType.CurrentDataTime);
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



      //DataTable dt = BP.WF.Dev2Interface.DB_GenerWillReturnNodes(this.FK_Node, this.WorkID, this.FID);


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
        if (nd.IsBackTracking)
        {
            /*如果允许原路退回*/
            CheckBox cb = new CheckBox();
            cb.ID = "CB_IsBackTracking";
            cb.Text = "退回后是否要原路返回?";
            this.ToolBar1.Add(cb);
        }
        else
        {
            this.ToolBar1.Add("<input type=button value='" + this.ToE("Track", "轨迹") + "' enable=true onclick=\"WinOpen('" + appPath + "/WF/Chart.aspx?WorkID=" + this.WorkID + "&FK_Flow=" + this.FK_Flow + "','ds'); \" />");
        }
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
                DataTable dt = BP.WF.Dev2Interface.DB_GenerWillReturnNodes(this.FK_Node, this.WorkID,this.FID);
                foreach (DataRow dr in dt.Rows)
                {
                    this.DDL1.Items.Add(new ListItem(dr["Name"].ToString(), dr["No"].ToString()));
                }

                WorkNode pwn = wn.GetPreviousWorkNode();
                this.DDL1.SetSelectItem(pwn.HisNode.NodeID);
                this.DDL1.Enabled = true;
                Work wk = pwn.HisWork;
                if (wn.HisNode.FocusField != "")
                {
                    this.TB1.Text = wn.HisWork.GetValStrByKey(wn.HisNode.FocusField);
                }
                else
                {
                    this.TB1.Text = this.ToEP4("WBackInfo",
                  "{0}同志: \n  您在{1}处理的“{2}”工作有错误，需要您重新办理．\n\n此致!!!   \n\n  {3}",
                  "", wk.CDT, pwn.HisNode.Name, WebUser.Name + "\n  " + BP.DA.DataType.CurrentDataTime);
                }
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
            if (wn.HisNode.IsBackTracking)
            {
                bool IsBackTracking= this.Pub1.GetCBByID("CB_IsBackTracking").Checked;
                mywn = wn.DoReturnWork(this.DDL1.SelectedItemIntVal, this.TB1.Text, IsBackTracking);
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
