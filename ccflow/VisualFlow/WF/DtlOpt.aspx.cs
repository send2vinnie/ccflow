﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.En;
using BP.WF.XML;
using BP.Web;
using BP.Sys;

public partial class WF_DtlOpt : WebPage
{
    public Int64 WorkID
    {
        get
        {
            return Int64.Parse(this.Request.QueryString["WorkID"]);
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
        if (this.Request.QueryString["Flag"] == "ExpTemplete")
        {
            string file = this.Request.PhysicalApplicationPath + @"\DataUser\DtlTemplete\" + this.FK_MapDtl + ".xls";
            if (System.IO.File.Exists(file) == false)
            {
                this.WinCloseWithMsg("设计错误：流程设计人员没有把该导入的明细表模版放入"+file);
                return;
            }
            BP.PubClass.OpenExcel(file, dtl.Name + ".xls");
            this.WinClose();
        }

        if (this.Request.QueryString["Flag"] == "ExpTemplete")
        {
            string file = this.Request.PhysicalApplicationPath + @"\DataUser\DtlTemplete\" + this.FK_MapDtl + ".xls";
            if (System.IO.File.Exists(file) == false)
            {
                this.WinCloseWithMsg("设计错误：流程设计人员没有把该导入的明细表模版放入" + file);
                return;
            }
            BP.PubClass.OpenExcel(file, dtl.Name + ".xls");
            this.WinClose();
            return;
        }

        if (this.Request.QueryString["Flag"] == "ExpData")
        {
            GEDtls dtls = new GEDtls(this.FK_MapDtl);
            dtls.Retrieve(GEDtlAttr.RefPK, this.WorkID);
            this.ExportDGToExcelV2(dtls, dtl.Name + ".xls");
            this.WinClose();
            return;
        }

        if (dtl.IsExp)
        {
            this.Pub1.AddFieldSet("数据导出");
            this.Pub1.AddP("点下面的连接进行本明细表的导出，您可以根据列的需要增减列。");
            string urlExp = "DtlOpt.aspx?DoType=" + this.DoType + "&WorkID=" + this.WorkID + "&FK_MapDtl=" + this.FK_MapDtl + "&Flag=ExpData";
            this.Pub1.Add("<p align=center><a href='" + urlExp + "' target=_blank ><img src='../Images/FileType/xls.gif' border=0 /><b>导出数据</b></a></p>");
            this.Pub1.AddFieldSetEnd();
        }

        if (dtl.IsImp)
        {
            this.Pub1.AddFieldSet("导入:" + dtl.Name);
            this.Pub1.AddP("下载数据模版:利用数据模板导出一个数据模板，您可以在此基础上进行数据编辑，把编辑好的信息<br>在通过下面的功能导入进来，以提高工作效率。");
            string url = "DtlOpt.aspx?DoType=" + this.DoType + "&WorkID=" + this.WorkID + "&FK_MapDtl=" + this.FK_MapDtl + "&Flag=ExpTemplete";
            this.Pub1.Add("<p align=center><a href='" + url + "' target=_blank ><img src='../Images/FileType/xls.gif' border=0 />数据模版</a></p>");
            this.Pub1.Add("<br>");

            this.Pub1.Add("格式数据文件:");
            System.Web.UI.WebControls.FileUpload fu = new System.Web.UI.WebControls.FileUpload();
            fu.ID = "fup";
            this.Pub1.Add(fu);

            this.Pub1.Add("<br>");

            BP.Web.Controls.DDL ddl = new BP.Web.Controls.DDL();
            ddl.Items.Add(new ListItem("选择导入方式", "all"));
            ddl.Items.Add(new ListItem("清空方式", "0"));
            ddl.Items.Add(new ListItem("追加方式", "1"));
            ddl.ID = "DDL_ImpWay";
            this.Pub1.Add(ddl);

            Button btn = new Button();
            btn.Text = "导入";
            btn.ID = "Btn_" + dtl.No;
            btn.Click += new EventHandler(btn_Click);
            this.Pub1.Add(btn);
            this.Pub1.AddFieldSetEnd();
        }
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
        try
        {
            BP.Web.Controls.DDL DDL_ImpWay = (BP.Web.Controls.DDL)this.Pub1.FindControl("DDL_ImpWay");
            System.Web.UI.WebControls.FileUpload fuit = (System.Web.UI.WebControls.FileUpload)this.Pub1.FindControl("fup");
            if (DDL_ImpWay.SelectedIndex == 0)
            {
                this.Alert("请选择导入方式.");
                return;
            }

            MapDtl dtl = new MapDtl(this.FK_MapDtl);
            string file = this.Request.PhysicalApplicationPath + "\\Temp\\" + WebUser.No + ".xls";
            fuit.SaveAs(file);

            GEDtls dtls = new GEDtls(this.FK_MapDtl);
            System.Data.DataTable dt = BP.DBLoad.GetTableByExt(file);

            file = this.Request.PhysicalApplicationPath + "\\DataUser\\DtlTemplete\\" + this.FK_MapDtl + ".xls";
            System.Data.DataTable dtTemplete = BP.DBLoad.GetTableByExt(file);

            #region 检查两个文件是否一致。
            foreach (DataColumn dc in dtTemplete.Columns)
            {
                bool isHave = false;
                foreach (DataColumn mydc in dt.Columns)
                {
                    if (dc.ColumnName == mydc.ColumnName)
                    {
                        isHave = true;
                        break;
                    }
                }
                if (isHave == false)
                    throw new Exception("@您导入的excel文件不符合系统要求的格式。");
            }
            #endregion 检查两个文件是否一致。


            #region 生成要导入的属性.
             
            BP.En.Attrs attrs = dtls.GetNewEntity.EnMap.Attrs;
            BP.En.Attrs attrsExp = new BP.En.Attrs();
            foreach (DataColumn dc in dtTemplete.Columns)
            {
                foreach (Attr attr in attrs)
                {
                    if (attr.UIVisible == false)
                        continue;

                    if (attr.IsRefAttr)
                        continue;

                    if (attr.Desc == dc.ColumnName.Trim())
                    {
                        attrsExp.Add(attr);
                        break;
                    }
                }
            }
            #endregion 生成要导入的属性.


            #region 执行导入数据.
            if (DDL_ImpWay.SelectedIndex == 1)
                BP.DA.DBAccess.RunSQL("DELETE "+dtl.PTable+" WHERE RefPK='"+this.WorkID+"'");

            int i = 0;
            Int64 oid =  BP.DA.DBAccess.GenerOID(this.FK_MapDtl, dt.Rows.Count);
            string rdt = BP.DA.DataType.CurrentData;
            foreach (DataRow dr in dt.Rows)
            {
                GEDtl dtlEn = dtls.GetNewEntity as GEDtl;
                dtlEn.ResetDefaultVal();

                foreach (BP.En.Attr attr in attrsExp)
                {
                    if (attr.UIVisible == false || dr[attr.Desc]==DBNull.Value)
                        continue;

                    string val = dr[attr.Desc].ToString() ;
                    if (val == null)
                        continue;
                    val = val.Trim();
                    switch (attr.MyFieldType)
                    {
                        case FieldType.Enum:
                        case FieldType.PKEnum:
                            SysEnums ses = new SysEnums(attr.UIBindKey);
                            foreach (SysEnum se in ses)
                            {
                                if (val == se.Lab)
                                {
                                    val = se.IntKey.ToString();
                                    break;
                                }
                            }
                            break;
                        case FieldType.FK:
                        case FieldType.PKFK:
                            break;
                        default:
                            break;
                    }

                    dtlEn.SetValByKey(attr.Key, val);
                }
                dtlEn.RefPKInt = (int)this.WorkID;
                dtlEn.SetValByKey("RDT", rdt);
                dtlEn.SetValByKey("Rec", WebUser.No);
                i++;
                dtlEn.InsertAsOID(oid);
                oid++;
            }
            #endregion 执行导入数据.

            this.Alert("共有(" + i + ")条数据导入成功。");
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "‘");
            this.Alert(msg);
        }
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

        string spField = ",Checker,Check_RDT,Check_Note,";

        foreach (MapAttr attr in attrs)
        {
            if (attr.UIVisible == false
                && spField.Contains("," +attr.KeyOfEn+",")==false)
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
                if (attr.UIVisible == false
                    && spField.Contains("," + attr.KeyOfEn + ",") == false)
                    continue;

                if (attr.MyDataType == BP.DA.DataType.AppBoolean)
                {
                    this.Pub1.AddTD(item.GetValBoolStrByKey(attr.KeyOfEn));
                    continue;
                }

                switch (attr.UIContralType)
                {
                    case UIContralType.DDL:
                        this.Pub1.AddTD(item.GetValRefTextByKey(attr.KeyOfEn));
                        continue;
                    default:
                        this.Pub1.AddTD(item.GetValStrByKey(attr.KeyOfEn));
                        continue;
                }
            }
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEndWithHR();

        if (geDtls.Count == 0)
            return;

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
        btn.Text = "导入并重新编辑(追加方式)";
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
            /*如果是清空方式导入。*/
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