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
    public int WorkID
    {
        get
        {
            try
            {
                return int.Parse(this.Request.QueryString["WorkID"]);
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

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Title = this.ToE("WorkBack", "工作退回");

        this.ToolBar1.Add("退回到:");
        this.ToolBar1.AddDDL("DDL1");
        this.ToolBar1.AddBtn("Btn_OK","确定");
        this.ToolBar1.GetBtnByID("Btn_OK").Attributes["onclick"] = " return confirm('您确定要执行吗？');";

        this.ToolBar1.AddBtn("Btn_Cancel", "取消");
        this.ToolBar1.GetBtnByID("Btn_OK").Click += new EventHandler(WF_UC_ReturnWork_Click);
        this.ToolBar1.GetBtnByID("Btn_Cancel").Click += new EventHandler(WF_UC_ReturnWork_Click);


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
                WorkNode wn = new WorkNode(this.WorkID, this.FK_Node);
                WorkNode pwn = wn.GetPreviousWorkNode();
                WorkNodes wns = new WorkNodes();
                if (wn.HisNode.HisFNType == FNType.River)
                {
                    wns.GenerByFID(wn.HisNode.HisFlow, this.WorkID);
                }
                else
                {
                    wns.GenerByWorkID(wn.HisNode.HisFlow, this.WorkID);
                }

                foreach (WorkNode mywn in wns)
                {
                    if (mywn.HisNode.NodeID == this.FK_Node)
                        continue;

                    this.DDL1.Items.Add(new ListItem(mywn.HisNode.Name, mywn.HisNode.NodeID.ToString()));
                }

                this.DDL1.SetSelectItem(pwn.HisNode.NodeID);
                this.DDL1.Enabled = false;

                Work wk = pwn.HisWork;
                this.TB1.Text = this.ToEP4("WBackInfo",
                    "{0}同志: \n您在{1}处理的“{2}”工作有错误，需要您重新办理．\n\n  此致!!!   \n {3}",
                    wk.Rec + wk.RecText, wk.CDT, pwn.HisNode.Name, WebUser.Name + BP.DA.DataType.CurrentDataTime);
            }
            catch (Exception ex)
            {
                this.Pub1.AddMsgOfWarning("提示:", "@:" + this.ToE("WorkBackErr", "下列原因造成不能退回:") + ex.Message);

               // this.Alert();  //下列原因造成不能退回
            }
        }




    }

    void WF_UC_ReturnWork_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        switch (btn.ID)
        {
            case "Btn_Cancel":
                this.Response.Redirect("MyFlow.aspx?FK_Flow=" + this.FK_Flow + "&WorkID=" + this.WorkID, true);
                return;
            default:
                break;
        }

        try
        {

            WorkNode wn = new WorkNode(this.WorkID, this.FK_Node);
            Work wk = wn.HisWork;
            WorkNode mywn = wn.DoReturnWork(this.DDL1.SelectedItemIntVal, this.TB1.Text);
            this.ToMsg(this.ToEP2("WReInfo", "@任务被你成功退回到【{0}】，退回给【{1}】。", mywn.HisNode.Name, mywn.HisWork.Rec), "info");
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
        this.Response.Redirect("MyFlowInfo.aspx?FK_Flow=" + this.FK_Flow + "&FK_Type=" + type + "&FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID, false);
    }
}
