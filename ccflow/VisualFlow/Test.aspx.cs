using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TestFrm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string file = @"C:\aa\开票流程1.xls";
    string info=    BP.WF.Glo.LoadFlowDataWithToSpecNode(file);
    this.Response.Write(info);
        return;
        //  WF/Frm.aspx?FK_MapData=ND18201&IsTest=1&WorkID=0&FK_Node=401
        this.Response.Redirect("./WF/Frm.aspx?FK_MapData=ND18201&IsTest=1&WorkID=0&FK_Node=401", true);
        BP.WF.Flow fl = new BP.WF.Flow();
        fl.No = "001";
        fl.Retrieve();
        fl.Name = "ssssssssss";
        fl.Update();

        this.Response.Write(fl.Name);

        BP.WF.Flow flm = new BP.WF.Flow();
        flm.Name = "sdsds";
        flm.FK_FlowSort = "01";
        flm.No = "009";
        flm.Insert();
    }
}