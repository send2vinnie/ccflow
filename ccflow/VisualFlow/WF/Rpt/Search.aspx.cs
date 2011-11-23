using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Web;
using BP.Port;
using BP;

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

        int ToPageIdx = this.PageIdx + 1;
        int PPageIdx = this.PageIdx - 1;

        this.Pub1.Add("<SCRIPT language=javascript>");
        this.Pub1.Add("\t\n document.onkeydown = chang_page;");
        this.Pub1.Add("\t\n function chang_page() { ");
        if (this.PageIdx == 1)
        {
            this.Pub1.Add("\t\n if (event.keyCode == 37 || event.keyCode == 33) alert('已经是第一页');");
        }
        else
        {
            this.Pub1.Add("\t\n if (event.keyCode == 37  || event.keyCode == 38 || event.keyCode == 33) ");
            this.Pub1.Add("\t\n     location='Search.aspx?EnsName=" + this.EnsName + "&PageIdx=" + PPageIdx + "';");
        }

        if (this.PageIdx == maxPageNum)
        {
            this.Pub1.Add("\t\n if (event.keyCode == 39 || event.keyCode == 40 || event.keyCode == 34) alert('已经是最后一页');");
        }
        else
        {
            this.Pub1.Add("\t\n if (event.keyCode == 39 || event.keyCode == 40 || event.keyCode == 34) ");
            this.Pub1.Add("\t\n     location='Search.aspx?EnsName=" + this.EnsName + "&PageIdx=" + ToPageIdx + "';");
        }

        this.Pub1.Add("\t\n } ");
        this.Pub1.Add("</SCRIPT>");
        return ens;
    }
}