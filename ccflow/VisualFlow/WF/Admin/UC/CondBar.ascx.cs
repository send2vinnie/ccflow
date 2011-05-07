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
using BP.Web.Controls;
using BP.DA;
using BP.En;
using BP.Web;

public partial class WF_Admin_UC_CondBar :BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Request.QueryString["CondType"] != "2")
            return;


        switch (this.PageID)
        {
            case "Cond":
                this.AddB("<a href='Cond.aspx?CondType=" + this.Request["CondType"] + "&FK_Flow=" + this.Request["FK_Flow"] + "&FK_MainNode=" + this.Request["FK_MainNode"] + "&FK_Node=" + this.Request["FK_Node"] + "&FK_Attr=" + this.Request["FK_Attr"] + "&DirType=" + this.Request["DirType"] + "&ToNodeID=" + this.Request["ToNodeID"] + "'>" + this.ToE("FormCond", "表单条件") + "</a>");
                this.Add(" - <a href='CondStation.aspx?CondType=" + this.Request["CondType"] + "&FK_Flow=" + this.Request["FK_Flow"] + "&FK_MainNode=" + this.Request["FK_MainNode"] + "&FK_Node=0&FK_Attr=" + this.Request["FK_Attr"] + "&DirType=" + this.Request["DirType"] + "&ToNodeID=" + this.Request["ToNodeID"] + "'>" + this.ToE("StaCond", "岗位条件") + "</a>");
                this.Add(" - <a href='CondDept.aspx?CondType=" + this.Request["CondType"] + "&FK_Flow=" + this.Request["FK_Flow"] + "&FK_MainNode=" + this.Request["FK_MainNode"] + "&FK_Node=0&FK_Attr=" + this.Request["FK_Attr"] + "&DirType=" + this.Request["DirType"] + "&ToNodeID=" + this.Request["ToNodeID"] + "'>" + this.ToE("DeptCond", "部门条件") + "</a>");
                break;
            case "CondStation":
                this.Add("<a href='Cond.aspx?CondType=" + this.Request["CondType"] + "&FK_Flow=" + this.Request["FK_Flow"] + "&FK_MainNode=" + this.Request["FK_MainNode"] + "&FK_Node=" + this.Request["FK_Node"] + "&FK_Attr=" + this.Request["FK_Attr"] + "&DirType=" + this.Request["DirType"] + "&ToNodeID=" + this.Request["ToNodeID"] + "'>" + this.ToE("FormCond", "表单条件") + "</a>");
                this.AddB(" - <a href='CondStation.aspx?CondType=" + this.Request["CondType"] + "&FK_Flow=" + this.Request["FK_Flow"] + "&FK_MainNode=" + this.Request["FK_MainNode"] + "&FK_Node=0&FK_Attr=" + this.Request["FK_Attr"] + "&DirType=" + this.Request["DirType"] + "&ToNodeID=" + this.Request["ToNodeID"] + "'>" + this.ToE("StaCond", "岗位条件") + "</a>");
                this.Add(" - <a href='CondDept.aspx?CondType=" + this.Request["CondType"] + "&FK_Flow=" + this.Request["FK_Flow"] + "&FK_MainNode=" + this.Request["FK_MainNode"] + "&FK_Node=0&FK_Attr=" + this.Request["FK_Attr"] + "&DirType=" + this.Request["DirType"] + "&ToNodeID=" + this.Request["ToNodeID"] + "'>" + this.ToE("DeptCond", "部门条件") + "</a>");
                break;
            case "CondDept":
                this.Add("<a href='Cond.aspx?CondType=" + this.Request["CondType"] + "&FK_Flow=" + this.Request["FK_Flow"] + "&FK_MainNode=" + this.Request["FK_MainNode"] + "&FK_Node=" + this.Request["FK_Node"] + "&FK_Attr=" + this.Request["FK_Attr"] + "&DirType=" + this.Request["DirType"] + "&ToNodeID=" + this.Request["ToNodeID"] + "'>" + this.ToE("FormCond", "表单条件") + "</a>");
                this.Add(" - <a href='CondStation.aspx?CondType=" + this.Request["CondType"] + "&FK_Flow=" + this.Request["FK_Flow"] + "&FK_MainNode=" + this.Request["FK_MainNode"] + "&FK_Node=0&FK_Attr=" + this.Request["FK_Attr"] + "&DirType=" + this.Request["DirType"] + "&ToNodeID=" + this.Request["ToNodeID"] + "'>" + this.ToE("StaCond", "岗位条件") + "</a>");
                this.AddB(" - <a href='CondDept.aspx?CondType=" + this.Request["CondType"] + "&FK_Flow=" + this.Request["FK_Flow"] + "&FK_MainNode=" + this.Request["FK_MainNode"] + "&FK_Node=0&FK_Attr=" + this.Request["FK_Attr"] + "&DirType=" + this.Request["DirType"] + "&ToNodeID=" + this.Request["ToNodeID"] + "'>" + this.ToE("DeptCond", "部门条件") + "</a>");
                break;
            default:
                break;
        }

        this.AddHR();

       

    }
}
