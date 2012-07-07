﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using BP.Web.Controls;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Web;
using BP.Sys;
using BP.Port;
using BP;
using BP.Web.Comm;

public partial class WF_Rpt_Search : WebPage
{
    #region 属性.
    public string DoType
    {
        get
        {
            string s = this.Request.QueryString["DoType"];
            if (s == null)
                s = "My";
            return s;
        }
    }
    public string FK_Flow
    {
        get
        {
            string s = this.Request.QueryString["FK_Flow"];
            if (s == null)
                s = "021";
            s = s.Replace("ND", "");
            s = s.Replace("Rpt", "");
            return s;
        }
    }
    public string EnsName
    {
        get
        {
            return "ND" + int.Parse(this.FK_Flow) + "Rpt";
        }
    }
    public Entities _HisEns = null;
    public new Entities HisEns
    {
        get
        {
            if (_HisEns == null)
            {
                if (this.EnsName != null)
                {
                    if (this._HisEns == null)
                        _HisEns = BP.DA.ClassFactory.GetEns(this.EnsName);
                }
            }
            return _HisEns;
        }
    }
    #endregion 属性.

    protected void Page_Load(object sender, EventArgs e)
    {
        #region 处理风格
        this.Page.RegisterClientScriptBlock("sds",
         "<link href='./../../Comm/Style/Table" + BP.Web.WebUser.Style + ".css' rel='stylesheet' type='text/css' />");

        if (this.Request.QueryString["PageIdx"] == null)
            this.PageIdx = 1;
        else
            this.PageIdx = int.Parse(this.Request.QueryString["PageIdx"]);
        #endregion 处理风格

        //if (WebUser.IsWap)
        //{
        //    this.Pub1.Add("<a href='./../WAP/Home.aspx' ><img src='./../WAP/Img/Home.gif' />Home</a>");
        //    this.Pub1.Add("-<a href='./../WAP/FlowSearch.aspx' >查询</a>");

        //    this.Pub1.Add(" - <a href='Search.aspx?FK_Flow=" + this.FK_Flow + "&EnsName=" + this.EnsName + "&DoType=My' ><img src='../../Images/Btn/Authorize.gif' />我参与的流程</a>");
        //    this.Pub1.Add(" - <a href='Search.aspx?FK_Flow=" + this.FK_Flow + "&EnsName=" + this.EnsName + "&DoType=Dept' ><img src='../../Images/Btn/CC.gif' />我部门的流程</a><br>");
        //}
        //else
        //{
        //    this.Pub1.Add("<a href='Search.aspx?FK_Flow=" + this.FK_Flow + "&EnsName=" + this.EnsName + "&DoType=My' ><img src='../../Images/Btn/Authorize.gif' />我参与的流程</a>");
        //    this.Pub1.Add(" - <a href='Search.aspx?FK_Flow=" + this.FK_Flow + "&EnsName=" + this.EnsName + "&DoType=Dept' ><img src='../../Images/Btn/CC.gif' />我部门的流程</a><br>");
        //}


        #region 处理查询设的默认.
        if (this.DoType == "My")
        {
            Entity en = this.HisEns.GetNewEntity;
            Map map = en.EnMap;
            AttrSearchs searchs = map.SearchAttrs;
        }
        else
        {
            #region 处理查询权限
            Entity en = this.HisEns.GetNewEntity;
            Map map = en.EnMap;

            this.ToolBar1.InitByMapV2(map, 1, this.EnsName);
            #region 生成查询条件

            #endregion


            this.ToolBar1.AddBtn(BP.Web.Controls.NamesOfBtn.Export);
            AttrSearchs searchs = map.SearchAttrs;
            string defVal = "";
            System.Data.DataTable dt = null;
            foreach (AttrSearch attr in searchs)
            {
                DDL mydll = this.ToolBar1.GetDDLByKey("DDL_" + attr.Key);
                if (mydll == null)
                    continue;
                defVal = mydll.SelectedItemStringVal;
                mydll.Attributes["onchange"] = "DDL_mvals_OnChange(this,'"+this.EnsName+"','" + attr.Key + "')";
                switch (attr.Key)
                {
                    case "FK_NY":
                        dt = DBAccess.RunSQLReturnTable("SELECT DISTINCT FK_NY FROM " + this.EnsName + " WHERE FK_NY!='' ORDER BY FK_NY");
                        mydll.Items.Clear();
                        mydll.Items.Add(new ListItem("=>月份", "all"));
                        foreach (DataRow dr in dt.Rows)
                        {
                            mydll.Items.Add(new ListItem(dr[0].ToString(), dr[0].ToString()));
                        }
                        mydll.SetSelectItem(defVal);
                        break;
                    case "FlowStarter":
                        dt = DBAccess.RunSQLReturnTable("SELECT No,Name FROM WF_Emp WHERE  FK_Dept IN (SELECT FK_Dept FROM  Port_DeptFlowScorp WHERE FK_Emp='" + WebUser.No + "') AND No IN (SELECT DISTINCT FlowStarter FROM " + this.EnsName + " WHERE FlowStarter!='')");
                        mydll.Items.Clear();
                        mydll.Items.Add(new ListItem("=>发起人", "all"));
                        foreach (DataRow dr in dt.Rows)
                        {
                            mydll.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                        }
                        mydll.SetSelectItem(defVal);
                        mydll.Attributes["onchange"] = "DDL_mvals_OnChange(this,'ND" + int.Parse(this.FK_Flow) + "Rpt','" + attr.Key + "')";
                        break;
                    case "FK_Dept":
                        if (WebUser.No != "admin")
                        {
                            dt = DBAccess.RunSQLReturnTable("SELECT No,Name FROM Port_Dept WHERE No IN (SELECT FK_Dept FROM  Port_DeptFlowScorp WHERE FK_Emp='" + WebUser.No + "')");
                            if (dt.Rows.Count == 0)
                            {
                                BP.WF.Port.DeptFlowScorp dfs = new BP.WF.Port.DeptFlowScorp();
                                dfs.FK_Dept = WebUser.FK_Dept;
                                dfs.FK_Emp = WebUser.No;
                                dfs.Insert();


                                dt = DBAccess.RunSQLReturnTable("SELECT No,Name FROM Port_Dept WHERE No IN (SELECT FK_Dept FROM  Port_DeptFlowScorp WHERE FK_Emp='" + WebUser.No + "')");
                            }
                            mydll.Items.Clear();
                            foreach (DataRow dr in dt.Rows)
                                mydll.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                        }

                        if (mydll.Items.Count >= 2)
                        {
                            ListItem liMvals = new ListItem("*多项组合..", "mvals");
                            liMvals.Attributes.CssStyle.Add("style", "color:green");
                            liMvals.Attributes.Add("color", "green");
                            liMvals.Attributes.Add("style", "color:green");
                        }
                        mydll.SetSelectItem(defVal);
                        break;
                    default:
                        break;
                }
            }
            #endregion 处理查询权限

            this.ToolBar1.GetBtnByID("Btn_Search").Click += new System.EventHandler(this.ToolBar1_ButtonClick);
            this.ToolBar1.GetBtnByID(BP.Web.Controls.NamesOfBtn.Export).Click += new System.EventHandler(this.ToolBar1_ButtonClick);
        }
        #endregion 处理查询设的默认。

        //处理按钮.
        this.SetDGData();
    }
    public Entities SetDGData()
    {
        return this.SetDGData(this.PageIdx);
    }
    public Entities SetDGData(int pageIdx)
    {
        Entities ens = this.HisEns;
        Entity en = ens.GetNewEntity;
        QueryObject qo = new QueryObject(ens);
        if (this.DoType == "My")
        {
            qo.AddWhere(GERptAttr.FlowEmps, " LIKE ", "'%@" + WebUser.No + "%'");
        }
        else
        {
            qo = this.ToolBar1.GetnQueryObject(ens, en);
        }

        this.Pub2.Clear();
        int maxPageNum = this.Pub2.BindPageIdx(qo.GetCount(), SystemConfig.PageSize, pageIdx, "Search.aspx?FK_Flow=" + this.FK_Flow + "&DoType=" + this.DoType);
        if (maxPageNum > 1)
            this.Pub2.Add("翻页键:← → PageUp PageDown");
        qo.DoQuery(en.PK, SystemConfig.PageSize, pageIdx);
        if (this.DoType == "Dept" && en.EnMap.IsShowSearchKey)
        {
            string keyVal = this.ToolBar1.GetTBByID("TB_Key").Text.Trim();
            if (keyVal.Length >= 1)
            {
                Attrs attrs = en.EnMap.Attrs;
                foreach (Entity myen in ens)
                {
                    foreach (Attr attr in attrs)
                    {
                        if (attr.IsFKorEnum)
                            continue;
                        if (attr.IsPK)
                            continue;
                        switch (attr.MyDataType)
                        {
                            case DataType.AppRate:
                            case DataType.AppMoney:
                            case DataType.AppInt:
                            case DataType.AppFloat:
                            case DataType.AppDouble:
                            case DataType.AppBoolean:
                                continue;
                            default:
                                break;
                        }
                        myen.SetValByKey(attr.Key, myen.GetValStrByKey(attr.Key).Replace(keyVal, "<font color=red>" + keyVal + "</font>"));
                    }
                }
            }
        }
        this.BindEns(ens, null);

        #region 生成js
        int ToPageIdx = this.PageIdx + 1;
        int PPageIdx = this.PageIdx - 1;
        this.UCSys1.Add("<SCRIPT language=javascript>");
        this.UCSys1.Add("\t\n document.onkeydown = chang_page;");
        this.UCSys1.Add("\t\n function chang_page() { ");
        if (this.PageIdx == 1)
        {
            this.UCSys1.Add("\t\n if (event.keyCode == 37 || event.keyCode == 33) alert('已经是第一页');");
        }
        else
        {
            this.UCSys1.Add("\t\n if (event.keyCode == 37  || event.keyCode == 38 || event.keyCode == 33) ");
            this.UCSys1.Add("\t\n     location='Search.aspx?DoType="+this.DoType+"&FK_Flow=" + this.FK_Flow + "&PageIdx=" + PPageIdx + "';");
        }

        if (this.PageIdx == maxPageNum)
        {
            this.UCSys1.Add("\t\n if (event.keyCode == 39 || event.keyCode == 40 || event.keyCode == 34) alert('已经是最后一页');");
        }
        else
        {
            this.UCSys1.Add("\t\n if (event.keyCode == 39 || event.keyCode == 40 || event.keyCode == 34) ");
            this.UCSys1.Add("\t\n     location='Search.aspx?DoType=" + this.DoType + "&FK_Flow=" + this.FK_Flow + "&PageIdx=" + ToPageIdx + "';");
        }

        this.UCSys1.Add("\t\n } ");
        this.UCSys1.Add("</SCRIPT>");
        #endregion 生成js

        return ens;
    }
    private string GenerEnUrl(Entity en, Attrs attrs)
    {
        string url = "";
        foreach (Attr attr in attrs)
        {
            switch (attr.UIContralType)
            {
                case UIContralType.TB:
                    if (attr.IsPK)
                        url += "&" + attr.Key + "=" + en.GetValStringByKey(attr.Key);
                    break;
                case UIContralType.DDL:
                    url += "&" + attr.Key + "=" + en.GetValStringByKey(attr.Key);
                    break;
            }
        }
        return url;
    }
    public void BindEns(Entities ens, string ctrlId)
    {
        MapData md = new MapData(this.EnsName);
        this.Title = md.Name + " - 流程通用查询";

        this.UCSys1.Controls.Clear();
        Entity myen = ens.GetNewEntity;
        string pk = myen.PK;
        string clName = myen.ToString();
        Attrs attrs = myen.EnMap.Attrs;

        #region 求出可显示的属性。
        Attrs selectedAttrs = new Attrs();
        string attrKeyShow = md.AttrsInTable;
        string[] strs = md.AttrsInTable.Split('@');
        foreach (string str in strs)
        {
            if (str == null || str == "")
                continue;

            string[] kv = str.Split('=');
            if (kv[0] == "MyNum")
                continue;
            selectedAttrs.Add(myen.EnMap.GetAttrByKey(kv[0]));
        }
        #endregion 求出可显示的属性.

        #region  生成标题
        //this.UCSys1.Add("<Table border='1' align=left width='20%' cellpadding='0' cellspacing='0' style='border-collapse: collapse' bordercolor='#C0C0C0'>");
        this.UCSys1.AddTable();
        this.UCSys1.AddTR();
        this.UCSys1.AddTDTitle("序");
        this.UCSys1.AddTDTitle("报告");
        foreach (Attr attr in selectedAttrs)
        {
            if (attr.IsRefAttr)
                continue;
            this.UCSys1.AddTDTitle(attr.Desc);
        }
        #endregion  生成标题

        bool isRefFunc = false;
        isRefFunc = true;
        int pageidx = this.PageIdx - 1;
        int idx = SystemConfig.PageSize * pageidx;
        bool is1 = false;

        #region 用户界面属性设置
        string focusField = "Title";
        int WinCardH = 500;
        int WinCardW = 400;
        #endregion 用户界面属性设置

        #region 数据输出.
        this.UCSys1.AddTREnd();
        string urlExt = "";
        foreach (Entity en in ens)
        {
            #region 处理keys
            string style = WebUser.Style;
            string url = this.GenerEnUrl(en, attrs);
            #endregion

            this.UCSys1.AddTR();


            #region 输出字段。
            idx++;
            this.UCSys1.AddTDIdx(idx);

            this.UCSys1.AddTD("<a href=\"javascript:WinOpen('./../WFRpt.aspx?FK_Flow=" + this.FK_Flow + "&WorkID=" + en.GetValStrByKey("OID") + "&FID=" + en.GetValStrByKey("FID") + "','tdr');\" ><img src='../Img/Track.png' border=0 /></a>");
        

            string val = "";
            foreach (Attr attr in selectedAttrs)
            {
                if (attr.IsRefAttr || attr.Key == "MyNum")
                    continue;

                if (attr.UIContralType == UIContralType.DDL)
                {
                    string s = en.GetValRefTextByKey(attr.Key);
                    if (s == null || s == "")
                    {
                        switch (attr.Key)
                        {
                            case "FK_NY":
                                s = en.GetValStringByKey(attr.Key);
                                break;
                            default:
                                break;
                        }
                    }
                    this.UCSys1.AddTD(s);
                    continue;
                }

                if (attr.UIHeight != 0)
                {
                    this.UCSys1.AddTDDoc("...", "...");
                    return;
                }

                string str = en.GetValStrByKey(attr.Key);
                if (focusField == attr.Key)
                {
                    //str = "<b><font color='blue' ><a href=" + urlExt + ">" + str + "</font></b></a>";
                    str = "<b><font color='blue' >" + str + "</font></a>";
                }
                switch (attr.MyDataType)
                {
                    case DataType.AppDate:
                    case DataType.AppDateTime:
                        if (str == "" || str == null)
                            str = "&nbsp;";
                        this.UCSys1.AddTD(str);
                        break;
                    case DataType.AppString:
                        if (str == "" || str == null)
                            str = "&nbsp;";

                        if (attr.UIHeight != 0)
                            this.UCSys1.AddTDDoc(str, str);
                        else
                            this.UCSys1.AddTD(str);
                        break;
                    case DataType.AppBoolean:
                        if (str == "1")
                            this.UCSys1.AddTD("是");
                        else
                            this.UCSys1.AddTD("否");
                        break;
                    case DataType.AppFloat:
                    case DataType.AppInt:
                    case DataType.AppRate:
                    case DataType.AppDouble:
                        this.UCSys1.AddTDNum(str);
                        break;
                    case DataType.AppMoney:
                        this.UCSys1.AddTDNum(decimal.Parse(str).ToString("0.00"));
                        break;
                    default:
                        throw new Exception("no this case ...");
                }
            }
            #endregion 输出字段。

       
            this.UCSys1.AddTREnd();
        }
        #endregion 数据输出.

        #region  求合计代码写在这里。
        bool IsHJ = false;
        foreach (Attr attr in selectedAttrs)
        {
            if (attr.MyFieldType == FieldType.RefText)
                continue;

            if (attr.UIContralType == UIContralType.DDL)
                continue;

            if (attr.Key == "OID" ||
                attr.Key == "MID"
                || attr.Key == "FID"
                || attr.Key.ToUpper() == "WORKID")
                continue;

            switch (attr.MyDataType)
            {
                case DataType.AppDouble:
                case DataType.AppFloat:
                case DataType.AppInt:
                case DataType.AppMoney:
                    IsHJ = true;
                    break;
                default:
                    break;
            }
        }

        if (IsHJ)
        {
            this.UCSys1.Add("<TR class='Sum' >");
            this.UCSys1.AddTD(this.ToE("Sum", "合计"));
            foreach (Attr attr in selectedAttrs)
            {
                if (attr.Key == "MyNum")
                    continue;

                if (attr.UIContralType == UIContralType.DDL)
                {
                    this.UCSys1.AddTD();
                    continue;
                }

                if (attr.UIVisible == false)
                    continue;

                if (attr.Key == "OID" || attr.Key == "MID" || attr.Key.ToUpper() == "WORKID")
                {
                    this.UCSys1.AddTD(attr.Key);
                    continue;
                }

                switch (attr.MyDataType)
                {
                    case DataType.AppDouble:
                        this.UCSys1.AddTDNum(ens.GetSumDecimalByKey(attr.Key));
                        break;
                    case DataType.AppFloat:
                        this.UCSys1.AddTDNum(ens.GetSumDecimalByKey(attr.Key));
                        break;
                    case DataType.AppInt:
                        this.UCSys1.AddTDNum(ens.GetSumDecimalByKey(attr.Key));
                        break;
                    case DataType.AppMoney:
                        this.UCSys1.AddTDJE(ens.GetSumDecimalByKey(attr.Key));
                        break;
                    default:
                        this.UCSys1.AddTD();
                        break;
                }
            } /*结束循环*/
            this.UCSys1.AddTD();
            this.UCSys1.AddTD();
            this.UCSys1.AddTREnd();
        }
        #endregion

        this.UCSys1.AddTableEnd();
    }
    private void ToolBar1_ButtonClick(object sender, System.EventArgs e)
    {
        try
        {
            Btn btn = (Btn)sender;
            switch (btn.ID)
            {
                case NamesOfBtn.Insert: //数据导出
                    this.Response.Redirect("UIEn.aspx?EnName=" + this.HisEn.ToString(), true);
                    return;
                case BP.Web.Controls.NamesOfBtn.Export:
                case NamesOfBtn.Excel: //数据导出
                    MapData md = new MapData(this.EnsName);
                    Entities ens = md.HisEns;
                    Entity en = ens.GetNewEntity;
                    QueryObject qo = new QueryObject(ens);
                    if (this.DoType == "My")
                        qo.AddWhere("FlowEmps", " LIKE ", "%" + WebUser.No + ",%");
                    else
                        qo = this.ToolBar1.GetnQueryObject(ens, en);

                    DataTable dt= qo.DoQueryToTable();
                    DataTable myDT = new DataTable();
                    Attrs attrs = md.AttrsInTableEns;
                    foreach (Attr attr in attrs)
                    {
                        myDT.Columns.Add(new DataColumn(attr.Desc, typeof(string)));
                    }

                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow myDR = myDT.NewRow();
                        foreach (Attr attr in attrs)
                        {
                            myDR[attr.Desc] = dr[attr.Key];
                        }
                        myDT.Rows.Add(myDR);
                    }

                    string file = "";
                    try
                    {
                        file = this.ExportDGToExcel(myDT, en.EnDesc);
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            file = this.ExportDGToExcel(ens.ToDataTableDescField(), en.EnDesc);
                        }
                        catch
                        {
                            throw new Exception("数据没有正确导出可能的原因之一是:系统管理员没正确的安装Excel组件，请通知他，参考安装说明书解决。@系统异常信息：" + ex.Message);
                        }
                    }
                    this.SetDGData();
                    return;
                case NamesOfBtn.Excel_S: //数据导出.
                    Entities ens1 = this.SetDGData();
                    try
                    {
                        this.ExportDGToExcel(ens1.ToDataTableDesc(), this.HisEn.EnDesc);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("数据没有正确导出可能的原因之一是:系统管理员没正确的安装Excel组件，请通知他，参考安装说明书解决。@系统异常信息：" + ex.Message);
                    }
                    this.SetDGData();
                    return;
                case NamesOfBtn.Xml: //数据导出
                    return;
                case "Btn_Print":  //数据导出.
                    return;
                default:
                    this.PageIdx = 1;
                    this.SetDGData(1);
                    this.ToolBar1.SaveSearchState(this.EnsName, null);
                    return;
            }
        }
        catch (Exception ex)
        {
            if (!(ex is System.Threading.ThreadAbortException))
            {
                this.ResponseWriteRedMsg(ex);
                //在这里显示错误
            }
        }
    }
}