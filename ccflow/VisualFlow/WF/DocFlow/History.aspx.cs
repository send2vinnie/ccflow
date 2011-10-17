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
using BP.Web;
using BP.En;
using BP.DA;
using BP.WF;
using BP.Sys;
using BP.Port;
using BP;

public partial class GovDoc_History : WebPage
{
    public string FK_Flow
    {
        get
        {
            return this.Request.QueryString["FK_Flow"];
        }
    }
    public int WorkID
    {
        get
        {
            return int.Parse(this.Request.QueryString["WorkID"]);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Flow fl = new Flow(this.FK_Flow);
        this.Pub1.AddTable();
        this.Pub1.AddCaptionLeft(fl.Name + " - 历史信息");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("节点");
        this.Pub1.AddTDTitle("发起人");
        this.Pub1.AddTDTitle("接受时间");
        this.Pub1.AddTDTitle("应完成时间");
        this.Pub1.AddTDTitle("操作");
        this.Pub1.AddTREnd();

        WorkerLists wls = new WorkerLists();
        wls.Retrieve(WorkerListAttr.WorkID, this.WorkID, WorkerListAttr.RDT);

        foreach (WorkerList wl in wls)
        {
            if (wl.IsEnable == false)
                continue;

            this.Pub1.AddTR();
            this.Pub1.AddTD(wl.FK_Node);
            this.Pub1.AddTD(wl.FK_EmpText);
            this.Pub1.AddTD(wl.RDT);
            this.Pub1.AddTD(wl.SDT);
            this.Pub1.AddTD("<a href='DoClient.aspx?DoType=OpenDoc&FK_Flow=" + this.FK_Flow + "&FK_Node=" + wl.FK_Node + "&WorkID=" + wl.WorkID + "'>打开</a>");
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEnd();

        //BP.WF.LabNoteAttr
    }
}
