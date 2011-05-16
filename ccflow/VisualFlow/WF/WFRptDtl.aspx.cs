using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BP.WF;
using BP.En;
using BP.Web;
using BP.DA;


public partial class WF_WFDtl : WebPage
{
    public new string EnName
    {
        get
        {
            return this.Request.QueryString["EnName"];
        }
    }
    public new string RefPK
    {
        get
        {
            return this.Request.QueryString["RefPK"];
        }
    }
    public string OID
    {
        get
        {
            return this.Request.QueryString["OID"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.RegisterClientScriptBlock("s",
          "<link href='" + this.Request.ApplicationPath + "/Comm/Style/Table" + BP.Web.WebUser.Style + ".css' rel='stylesheet' type='text/css' />");


        BP.Sys.GEDtls dtls = new BP.Sys.GEDtls(this.EnName);
        QueryObject qo = new QueryObject(dtls);
        qo.AddWhere(BP.Sys.GEDtlAttr.RefPK, this.RefPK);
        qo.DoQuery();

        //  throw new Exception(qo.SQL);

        Map map = dtls.GetNewEntity.EnMap;

        this.Ucsys1.AddTable();
        this.Ucsys1.AddCaptionLeftTX(map.EnDesc + " - <a href='WFRptDtl.aspx?RefPK="+this.RefPK+"&EnName="+this.EnName+"&DoType=Exp' ><img src='../Images/Btn/Excel.gif' border=0>输出到Excel</a>");
        this.Ucsys1.AddTR();
        this.Ucsys1.AddTDTitle("序");
        foreach (Attr attr in map.Attrs)
        {
            if (attr.Key == "RefPK" || attr.Key == "OID")
                continue;

            this.Ucsys1.AddTDTitle(attr.Desc);
        }
        this.Ucsys1.AddTREnd();

        int i = 1;
        bool is1 = false;
        foreach (BP.Sys.GEDtl dtl in dtls)
        {
            is1 = this.Ucsys1.AddTR(is1);

            this.Ucsys1.AddTDIdx(i++);
            foreach (Attr attr in map.Attrs)
            {
                if (attr.Key == "RefPK" || attr.Key == "OID")
                    continue;

                this.Ucsys1.AddTD(dtl.GetValStrByKey(attr.Key));
            }
            this.Ucsys1.AddTREnd();
        }


        this.Ucsys1.AddTRSum();
        this.Ucsys1.AddTD();


        foreach (Attr attr in map.Attrs)
        {
            if (attr.Key == "RefPK" || attr.Key == "OID")
                continue;

            if (attr.IsNum == false || attr.Key=="FID")
                this.Ucsys1.AddTD();
            else
            {
                this.Ucsys1.AddTD(dtls.GetSumFloatByKey(attr.Key));
            }
        }
        this.Ucsys1.AddTREnd();

        this.Ucsys1.AddTableEnd();

        if (this.Request.QueryString["DoType"] != null)
        {
          //  this.ExportDGToExcel(dtls.ToDataTableDesc(), map.EnDesc);
        }
    }
}
