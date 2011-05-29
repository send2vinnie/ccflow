using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.WF.XML;
using BP.Web;
using BP.Sys;

public partial class WF_DtlOpt : WebPage
{
    public int WorkID
    {
        get
        {
            return int.Parse(this.Request.QueryString["WorkID"]);
        }
    }
    public string FK_MapDtl
    {
        get
        {
            return this.Request.QueryString["FK_MapDtl"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Title = "明细选项";

        WorkOptDtlXmls xmls = new WorkOptDtlXmls();
        xmls.RetrieveAll();
        MapDtl dtl = new MapDtl(this.FK_MapDtl);

        this.Pub1.Add("\t\n<div id='tabsJ'  align='center'>");
        this.Pub1.Add("\t\n<ul>");
        foreach (WorkOptDtlXml item in xmls)
        {
            switch (item.No)
            {
                case "UnPass":
                    if (dtl.IsEnablePass == false)
                        continue;
                    break;
                default:
                    break;
            }

            string url = item.URL + "?DoType=" + item.No + "&WorkID=" + this.WorkID + "&FK_MapDtl=" + this.FK_MapDtl;
            this.Pub1.AddLi("<a href=\"" + url + "\" ><span>" + item.Name + "</span></a>");
        }
        this.Pub1.Add("\t\n</ul>");
        this.Pub1.Add("\t\n</div>");

        switch (this.DoType)
        {
            case "UnPass":
                this.BindUnPass();
                break;
            case "ExpImp":
            default:
                this.BindExpImp();
                break;
        }
    }
    private void BindExpImp()
    {
        

        MapDtl dtl = new MapDtl(this.FK_MapDtl);
        if (this.Request.QueryString["Flag"] != null)
        {
            GEDtls dtls = new GEDtls(this.FK_MapDtl);
            this.ExportDGToExcelV2(dtls, dtl.Name + ".xls");
            this.WinClose();
        }

        this.Pub1.AddFieldSet("数据模板导出");

        this.Pub1.AddP("利用数据模板导出一个数据模板，您可以在此基础上进行数据编辑，把编辑好的信息<br>在通过下面的功能导入进来，以提高工作效率。");

        string url = "DtlOpt.aspx?DoType=" + this.DoType + "&WorkID=" + this.WorkID + "&FK_MapDtl=" + this.FK_MapDtl + "&Flag=1";

        this.Pub1.Add("<p align=center><a href='" + url + "' target=_blank >导出数据模板到Excel</a></p>");

        this.Pub1.AddFieldSetEnd();

        this.Pub1.AddFieldSet("导入:" + dtl.Name);

        this.Pub1.Add("<br>");

        this.Pub1.Add("格式数据文件:");

        FileUpload fu = new FileUpload();
        fu.ID = "F" + dtl.No;
        this.Pub1.Add(fu);
        this.Pub1.Add("<br>");

        Button btn = new Button();
        btn.Text = "导入";
        btn.ID = "Btn_" + dtl.No;
        btn.Click += new EventHandler(btn_Click);
        this.Pub1.Add(btn);
        btn = new Button();
        btn.Text = "下载数据模板";
        btn.ID = "Btn_Exp" + dtl.No;
        btn.Click += new EventHandler(btn_Exp_Click);
        this.Pub1.Add(btn);
        this.Pub1.Add("<br>");
        this.Pub1.Add("<br>");
        this.Pub1.AddFieldSetEnd();
    }
    void btn_Exp_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        string id = btn.ID.Replace("Btn_Exp", "");

        MapDtl dtl = new MapDtl(id);
        GEDtls dtls = new GEDtls(id);
        this.ExportDGToExcelV2(dtls, dtl.Name + ".xls");
        return;
    }
    void btn_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        string id = btn.ID.Replace("Btn_", "");
    }

    private void BindUnPass()
    {
        MapDtl dtl = new MapDtl(this.FK_MapDtl);
        Node nd = new Node(dtl.FK_MapData);

        string starter = "SELECT Rec FROM ND" + int.Parse(nd.FK_Flow + "01") + " WHERE OID=" + this.WorkID;
        starter = BP.DA.DBAccess.RunSQLReturnString(starter);

        GEDtls geDtls = new GEDtls(dtl.No);
        geDtls.Retrieve(GEDtlAttr.Rec, starter,"IsPass","0");

        MapAttrs attrs = new MapAttrs(dtl.No);
        this.Pub1.AddTable();
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("IDX");

        if (geDtls.Count > 0)
        {
            string str1 = "<INPUT id='checkedAll' onclick='selectAll()' type='checkbox' name='checkedAll'>";
            this.Pub1.AddTDTitle(str1);
        }
        else
        {
            this.Pub1.AddTDTitle();
        }

        foreach (MapAttr attr in attrs)
        {
            if (attr.UIVisible == false)
                continue;

            this.Pub1.AddTDTitle(attr.Name);
        }
        this.Pub1.AddTREnd();
        int idx = 0;
        foreach (GEDtl item in geDtls)
        {
            idx++;
            this.Pub1.AddTR();
            this.Pub1.AddTDIdx(idx);
            CheckBox cb = new CheckBox();
            cb.ID = "CB_" + item.OID;
            this.Pub1.AddTD(cb);
            foreach (MapAttr attr in attrs)
            {
                if (attr.UIVisible == false)
                    continue;
                if (attr.MyDataType == BP.DA.DataType.AppBoolean)
                    this.Pub1.AddTD(item.GetValBoolStrByKey(attr.KeyOfEn));
                else
                    this.Pub1.AddTD(item.GetValStrByKey(attr.KeyOfEn));
            }
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEndWithHR();

        if (geDtls.Count == 0)
        {
            return;
        }

        if (nd.IsStartNode == false)
            return;


        Button btn = new Button();
        btn.ID = "Btn_Delete";
        btn.Text = "批量删除";
        btn.Attributes["onclick"] = " return confirm('" + this.ToE("AYS", "您确定要执行吗？") + "');";
        btn.Click += new EventHandler(btn_DelUnPass_Click);
        this.Pub1.Add(btn);

        btn = new Button();
        btn.ID = "Btn_Imp";
        btn.Text = "导入并重新编辑(覆盖方式)";
        btn.Click += new EventHandler(btn_Imp_Click);
        this.Pub1.Add(btn);

        btn = new Button();
        btn.ID = "Btn_ImpClear";
        btn.Text = "导入并重新编辑(清空方式)";
        btn.Click += new EventHandler(btn_Imp_Click);
        this.Pub1.Add(btn);
    }

    void btn_DelUnPass_Click(object sender, EventArgs e)
    {
        MapDtl dtl = new MapDtl(this.FK_MapDtl);
        Node nd = new Node(dtl.FK_MapData);
        string starter = "SELECT Rec FROM ND" + int.Parse(nd.FK_Flow + "01") + " WHERE OID=" + this.WorkID;
        starter = BP.DA.DBAccess.RunSQLReturnString(starter);
        GEDtls geDtls = new GEDtls(this.FK_MapDtl);
        geDtls.Retrieve(GEDtlAttr.Rec, starter, "IsPass", "0");
        foreach (GEDtl item in geDtls)
        {
            if (this.Pub1.GetCBByID("CB_" + item.OID).Checked==false)
                continue;
            item.Delete();
        }
        this.Response.Redirect(this.Request.RawUrl, true);
    }
    void btn_Imp_Click(object sender, EventArgs e)
    {
        MapDtl dtl = new MapDtl(this.FK_MapDtl);
        Button btn = sender as Button;
        if (btn.ID.Contains("ImpClear"))
        {
            /*如果是晴空方式导入。*/
            BP.DA.DBAccess.RunSQL("DELETE " + dtl.PTable + " WHERE RefPK='" + this.WorkID + "'");
        }

        Node nd = new Node(dtl.FK_MapData);
        string starter = "SELECT Rec FROM ND" + int.Parse(nd.FK_Flow + "01") + " WHERE OID=" + this.WorkID;
        starter = BP.DA.DBAccess.RunSQLReturnString(starter);
        GEDtls geDtls = new GEDtls(this.FK_MapDtl);
        geDtls.Retrieve(GEDtlAttr.Rec, starter, "IsPass", "0");

        string strs = "";
        foreach (GEDtl item in geDtls)
        {
            if (this.Pub1.GetCBByID("CB_" + item.OID).Checked == false)
                continue;
            strs += ",'" + item.OID + "'";
        }
        if (strs == "")
        {
            this.Alert("请选择要执行的数据。");
            return;
        }
        strs=strs.Substring(1);
        BP.DA.DBAccess.RunSQL("UPDATE  " + dtl.PTable + " SET RefPK='" + this.WorkID + "',BatchID=0,Check_Note='',Check_RDT='"+BP.DA.DataType.CurrentDataTime+"', Checker='',IsPass=1  WHERE OID IN (" + strs + ")");
        this.WinClose();
    }
}