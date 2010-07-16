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

public partial class WF_Admin_UC_CondBar :BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Request.QueryString["CondType"] != "0")
            return;


        this.AddTable();
        this.AddTR();
        this.AddTD("<a href='Cond.aspx?CondType=" + this.Request["CondType"] + "&FK_Flow=" + this.Request["FK_Flow"] + "&FK_MainNode=" + this.Request["FK_MainNode"] + "&FK_Node=" + this.Request["FK_Node"] + "&FK_Attr=" + this.Request["FK_Attr"] + "&DirType=" + this.Request["DirType"] + "&ToNodeID=" + this.Request["ToNodeID"] + "'>表单条件</a>");

            this.AddTD("<a href='CondStation.aspx?CondType=" + this.Request["CondType"] + "&FK_Flow=" + this.Request["FK_Flow"] + "&FK_MainNode=" + this.Request["FK_MainNode"] + "&FK_Node=" + this.Request["FK_Node"] + "&FK_Attr=" + this.Request["FK_Attr"] + "&DirType=" + this.Request["DirType"] + "&ToNodeID=" + this.Request["ToNodeID"] + "'>岗位条件</a>");
            this.AddTD("<a href='CondDept.aspx?CondType=" + this.Request["CondType"] + "&FK_Flow=" + this.Request["FK_Flow"] + "&FK_MainNode=" + this.Request["FK_MainNode"] + "&FK_Node=" + this.Request["FK_Node"] + "&FK_Attr=" + this.Request["FK_Attr"] + "&DirType=" + this.Request["DirType"] + "&ToNodeID=" + this.Request["ToNodeID"] + "'>部门条件</a>");

        this.AddTREnd();

        this.AddTableEnd();

    }
}
