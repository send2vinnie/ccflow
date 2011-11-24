using System;
using System.Collections.Generic;
using System.Web;
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
    public string EnsName
    {
        get
        {
            string s= this.Request.QueryString["EnsName"];
            if (s == null)
                s = "ND11Rpt";
            return s;
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
        #region 处理风格。
        this.Page.RegisterClientScriptBlock("s",
         "<link href='./../../Comm/Style/Table" + BP.Web.WebUser.Style + ".css' rel='stylesheet' type='text/css' />");
        if (this.Request.QueryString["PageIdx"] == null)
            this.PageIdx = 1;
        else
            this.PageIdx = int.Parse(this.Request.QueryString["PageIdx"]);
        #endregion 处理风格。

        #region 处理查询。
        Entity en = this.HisEns.GetNewEntity;
        Map map = en.EnMap;
        this.ToolBar1.InitByMapV2(map, 1);
        AttrSearchs searchs = map.SearchAttrs;
        foreach (AttrSearch attr in searchs)
        {
            string mykey = this.Request.QueryString[attr.Key];
            if (mykey == "" || mykey == null)
                continue;
            else
                this.ToolBar1.GetDDLByKey("DDL_" + attr.Key).SetSelectItem(mykey, attr.HisAttr);
        }

        if (this.Request.QueryString["Key"] != null)
        {
            this.ToolBar1.GetTBByID("TB_Key").Text = this.Request.QueryString["Key"];
        }
        #endregion 处理查询。


        #region 查询权限判断

        #endregion 查询权限判断

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
        qo = this.ToolBar1.GetnQueryObject(ens, en);

        this.Pub2.Clear();
        int maxPageNum = this.Pub2.BindPageIdx(qo.GetCount(), SystemConfig.PageSize, pageIdx, "Search.aspx?EnsName=" + this.EnsName);
        if (maxPageNum > 1)
            this.Pub2.Add("翻页键:← → PageUp PageDown");
        qo.DoQuery(en.PK, SystemConfig.PageSize, pageIdx);

        if (en.EnMap.IsShowSearchKey)
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

        this.DataPanelDtl(ens, null);

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
            this.UCSys1.Add("\t\n     location='Search.aspx?EnsName=" + this.EnsName + "&PageIdx=" + PPageIdx + "';");
        }

        if (this.PageIdx == maxPageNum)
        {
            this.UCSys1.Add("\t\n if (event.keyCode == 39 || event.keyCode == 40 || event.keyCode == 34) alert('已经是最后一页');");
        }
        else
        {
            this.UCSys1.Add("\t\n if (event.keyCode == 39 || event.keyCode == 40 || event.keyCode == 34) ");
            this.UCSys1.Add("\t\n     location='Search.aspx?EnsName=" + this.EnsName + "&PageIdx=" + ToPageIdx + "';");
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
    public void DataPanelDtl(Entities ens, string ctrlId)
    {
        MapData md = new MapData();
        md.CheckPhysicsTable();


        this.UCSys1.Controls.Clear();
        Entity myen = ens.GetNewEntity;
        string pk = myen.PK;
        string clName = myen.ToString();
        Attrs attrs = myen.EnMap.Attrs;

        // Attrs selectedAttrs = myen.EnMap.GetChoseAttrs(ens);

        #region 求出可显示的属性。
        Attrs selectedAttrs = new Attrs();
        string attrKeyShow = md.ShowAttrs;
        if (attrKeyShow == "")
            attrKeyShow = ",BillNo,CDT,Emps,FID,FK_Dept,FK_NY,FlowDaySpan,FlowEmps,FlowEnder,FlowEnderRDT,FlowStarter,FlowStartRDT,MyNum,OID,RDT,Rec,Title,WFState,";
        foreach (Attr attr in myen.EnMap.Attrs)
        {
            if (attr.Key == "Title")
                attr.UIVisible = true;

            if (attrKeyShow.Contains("," + attr.Key + ",") == false)
                continue;
            if (attr.Key == "MyNum")
                continue;
            if (attr.UIVisible == false)
                continue;

            selectedAttrs.Add(attr);
        }
        #endregion 求出可显示的属性.


        #region  生成标题
        this.UCSys1.Add("<Table border='1' width='20%' cellpadding='0' cellspacing='0' style='border-collapse: collapse' bordercolor='#C0C0C0'>");
        this.UCSys1.AddTR();
        this.UCSys1.AddTDTitle("序");
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
        string FocusField = "Title";
        int WinCardH = 500;
        int WinCardW = 400;
        #endregion 用户界面属性设置

        this.UCSys1.AddTDTitle("功能");
        this.UCSys1.AddTREnd();
        string urlExt = "";
        foreach (Entity en in ens)
        {
            #region 处理keys
            string style = WebUser.Style;
            string url = this.GenerEnUrl(en, attrs);
            #endregion

            urlExt = "\"javascript:ShowEn('UIEn.aspx?EnsName=" + ens.ToString() + "&PK=" + en.GetValByKey(pk) + url + "', 'cd','" + WinCardH + "','" + WinCardW + "');\"";
            is1 = this.UCSys1.AddTR(is1, "ondblclick=" + urlExt);

            #region 输出字段。
            idx++;
            this.UCSys1.AddTDIdx(idx);
            string val = "";
            foreach (Attr attr in selectedAttrs)
            {
                if (attr.UIVisible == false || attr.Key == "MyNum")
                    continue;
                if (attr.UIContralType == UIContralType.DDL)
                {
                    this.UCSys1.AddTD(en.GetValRefTextByKey(attr.Key));
                    return;
                }

                if (attr.UIHeight != 0)
                {
                    this.UCSys1.AddTDDoc("...", "...");
                    return;
                }

                string str = en.GetValStrByKey(attr.Key);
                if (FocusField == attr.Key)
                    str = "<a href=" + urlExt + ">" + str + "</a>";
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

        #region  求合计代码写在这里。
        bool IsHJ = false;
        foreach (Attr attr in selectedAttrs)
        {
            if (attr.MyFieldType == FieldType.RefText)
                continue;

            if (attr.UIContralType == UIContralType.DDL)
                continue;

            if (attr.Key == "OID" || attr.Key == "MID"
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
            this.UCSys1.Add("<TR class='TRSum' >");
            this.UCSys1.AddTD(this.ToE("Sum", "合计"));
            foreach (Attr attr in selectedAttrs)
            {
                if (attr.Key == "MyNum")
                    continue;

                if (attr.UIVisible ==false)
                    continue;

                if (attr.MyDataType == DataType.AppBoolean 
                    || attr.UIContralType == UIContralType.DDL
                    || attr.MyFieldType == FieldType.RefText)
                {
                    this.UCSys1.AddTD();
                    continue;
                }

                if (attr.Key == "OID" || attr.Key == "MID" || attr.Key.ToUpper() == "WORKID")
                {
                    this.UCSys1.AddTD();
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
            }
            this.UCSys1.AddTD();
            this.UCSys1.AddTREnd();
        }
        #endregion
        this.UCSys1.AddTableEnd();
    }
}