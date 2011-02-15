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

public partial class GovDoc_MyFlowDoc : WebPage
{
    public Int64 WorkID
    {
        get
        {
            if (ViewState["WorkID"] == null)
            {
                if (this.Request.QueryString["WorkID"] == null)
                    return 0;
                else
                    return Int64.Parse(this.Request.QueryString["WorkID"]);
            }
            else
                return Int64.Parse(ViewState["WorkID"].ToString());
        }
        set
        {
            ViewState["WorkID"] = value;
        }
    }
    /// <summary>
    /// 当前的 NodeID ,在开始时间,nodeID,是地一个,流程的开始节点ID.
    /// </summary>
    public int FK_Node
    {
        get
        {
            string FK_Node = this.Request.QueryString["FK_Node"];
            return int.Parse(FK_Node);
        }
    }
    /// <summary>
    /// 当前的流程编号
    /// </summary>
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


    private void Btn_Click(object sender, System.EventArgs e)
    {
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        BP.WF.XML.FlowBars bars = new BP.WF.XML.FlowBars();
        bars.RetrieveAll();
        foreach (BP.WF.XML.FlowBar bar in bars)
        {
            this.PubBar.Add("<a href='" + bar.No + "&FK_Flow=" + this.FK_Flow + "&WorkID=" + this.WorkID + "&FK_Node=" + this.FK_Node + "' >" + bar.Name + "</a>&nbsp;&nbsp;");
        }
        this.PubBar.AddHR();

        BP.Web.Controls.Btn btn = new BP.Web.Controls.Btn();
        btn.ID = "Btn_Send";
        btn.Text = "发送";
        btn.Click += new EventHandler(Btn_Click);
        this.PubBar.Add(btn);

        btn = new BP.Web.Controls.Btn();
        btn.ID = "Btn_Save";
        btn.Text = "保存";
        btn.Click += new EventHandler(Btn_Click);
        this.PubBar.Add(btn);

        btn = new BP.Web.Controls.Btn();
        btn.ID = "Btn_Return";
        btn.Text = "退回";
        btn.Attributes["onclick"] = "windows.href='ReturnWork.aspx?FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID + "'";
        btn.Click += new EventHandler(Btn_Click);
        this.PubBar.Add(btn);

        btn = new BP.Web.Controls.Btn();
        btn.ID = "Btn_FW";
        btn.Text = "转发";
        btn.Attributes["onclick"] = "windows.href='Forward.aspx?FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID + "'";
        btn.Click += new EventHandler(Btn_Click);
        this.PubBar.Add(btn);

        btn = new BP.Web.Controls.Btn();
        btn.ID = "Btn_UnDo";
        btn.Text = "撤消";
        btn.Click += new EventHandler(Btn_Click);
        this.PubBar.Add(btn);

    }
}
