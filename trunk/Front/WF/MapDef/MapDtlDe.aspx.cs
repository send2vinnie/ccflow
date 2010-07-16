﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BP.Sys;
using BP.En;
using BP.En;
using BP.Web.Controls;
using BP.DA;
using BP.Web;

public partial class Comm_MapDef_MapDtlDe : WebPage
{
    #region 属性
    public string MyPK
    {
        get
        {
            return this.Request.QueryString["FK_MapDtl"];
        }
    }
    public string DoType
    {
        get
        {
            return this.Request.QueryString["DoType"];
        }
    }
    public string FK_MapData
    {
        get
        {
            return this.Request.QueryString["FK_MapData"];
        }
    }
    public string FK_MapDtl
    {
        get
        {
            return this.Request.QueryString["FK_MapDtl"];
        }
    }
    #endregion 属性

    protected void Page_Load(object sender, EventArgs e)
    {
        MapData.IsEditDtlModel = true;

        MapData md = new MapData(this.FK_MapData);
        MapDtl dtl = new MapDtl(this.FK_MapDtl);

        this.Title = md.Name + " - " + this.ToE("DesignDtl", "设计明细");
        this.Pub1.AddTable();
        this.Pub1.AddCaptionLeftTX("<a href='MapDef.aspx?MyPK=" + md.No + "' ><img src='../../Images/Btn/Back.gif' border=0/>" + this.ToE("Back","返回") + ":" + md.Name + "</a> - <img src='../../Images/Btn/Table.gif' border=0/>" + dtl.Name + " - <a href=\"javascript:AddF('" + this.MyPK + "');\" ><img src='../../Images/Btn/New.gif' border=0/>" + this.ToE("NewField", "新建字段") + "</a> ");

        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("IDX");
        MapAttrs attrs = new MapAttrs(this.MyPK);
        foreach (MapAttr attr in attrs)
        {
            if (attr.UIVisible == false)
                continue;

            this.Pub1.Add("<TD class=Title nowarp=true>");
            this.Pub1.Add("<a href=\"javascript:Up('" + this.MyPK + "','" + attr.OID + "');\" ><img src='../../Images/Btn/Left.gif' alt='向左移动' border=0/></a>");
            if (attr.HisEditType == EditType.UnDel || attr.HisEditType== EditType.Edit)
            {
                switch (attr.LGType)
                {
                    case FieldTypeS.Normal:
                        this.Pub1.AddB("<a href=\"javascript:Edit('" + this.MyPK + "','" + attr.OID + "','" + attr.MyDataType + "');\"  alt='" + attr.KeyOfEn + "'>" + attr.Name + "</a>");
                        break;
                    case FieldTypeS.Enum:
                        this.Pub1.AddB("<a href=\"javascript:EditEnum('" + this.MyPK + "','" + attr.OID + "');\" alt='" + attr.KeyOfEn + "' >" + attr.Name + "</a>");
                        break;
                    case FieldTypeS.FK:
                        this.Pub1.AddB("<a href=\"javascript:EditTable('" + this.MyPK + "','" + attr.OID + "','" + attr.MyDataTypeS + "');\"  alt='" + attr.KeyOfEn + "'>" + attr.Name + "</a>");
                        break;
                    default:
                        break;
                }
            }
            else
            {
                this.Pub1.AddB(attr.Name);
            }
            //  this.Pub1.Add("[<a href=\"javascript:Insert('" + this.MyPK + "','" + attr.IDX + "');\" ><img src='../../Images/Btn/Insert.gif' border=0/>插入</a>]");
            this.Pub1.Add("<a href=\"javascript:Down('" + this.MyPK + "','" + attr.OID + "');\" ><img src='../../Images/Btn/Right.gif' alt='向右移动' border=0/></a>");
            this.Pub1.Add("</TD>");
        }
        this.Pub1.AddTREnd();

        for (int i = 0; i <= 10; i++)
        {
            this.Pub1.AddTR();
            this.Pub1.AddTDIdx(i);
            foreach (MapAttr attr in attrs)
            {

                if (attr.UIVisible == false)
                    continue;
                switch (attr.LGType)
                {
                    case FieldTypeS.Normal:
                        if (attr.MyDataType == BP.DA.DataType.AppBoolean)
                        {
                            CheckBox cb = new CheckBox();
                            cb.Checked = attr.DefValOfBool;
                            cb.Enabled = attr.UIIsEnable;
                            cb.Text = attr.Name;
                            this.Pub1.AddTD(cb);
                            break;
                        }

                        TB tb = new TB();
                        tb.ID = "TB_" + attr.KeyOfEn + "_" + i;
                        tb.Text = attr.DefVal;
                        tb.Enabled = attr.UIIsEnable;

                        this.Pub1.AddTD(tb);

                        tb.ShowType = attr.HisTBType;

                        if (tb.Enabled == false)
                            tb.Attributes["class"] = "TBReadonly";
                        switch (attr.MyDataType)
                        {
                            case BP.DA.DataType.AppString:
                                tb.Attributes["Width"] = attr.UIWidth + "px";
                                if (tb.Enabled)
                                    tb.Attributes["class"] = "TB";
                                else
                                    tb.Attributes["class"] = "TBReadonly";
                                break;
                            case BP.DA.DataType.AppDateTime:
                            case BP.DA.DataType.AppDate:
                                if (tb.Enabled)
                                    tb.Attributes["class"] = "TB";
                                else
                                    tb.Attributes["class"] = "TBReadonly";
                                break;
                            default:
                                tb.Attributes["Width"] = "20px";
                                if (tb.Enabled)
                                {
                                    // OnKeyPress="javascript:return VirtyNum(this);"
                                    tb.Attributes["OnKeyDown"] = "javascript:return VirtyNum(this);";
                                    tb.Attributes["onkeyup"] += "javascript:C" + i + "();C" + attr.KeyOfEn + "();";
                                    tb.Attributes["class"] = "TBNum";
                                }
                                else
                                {
                                    tb.Attributes["onpropertychange"] += "C" + attr.KeyOfEn + "();";
                                    tb.Attributes["class"] = "TBReadonlyNum";
                                }
                                break;
                        }
                        break;
                    case FieldTypeS.Enum:
                        DDL ddl = new DDL();
                        ddl.ID = "DDL_" + attr.KeyOfEn + "_" + i;
                        try
                        {
                            ddl.BindSysEnum(attr.KeyOfEn);
                            ddl.SetSelectItem(attr.DefVal);
                        }
                        catch (Exception ex)
                        {
                            BP.PubClass.Alert(ex.Message);
                        }
                        ddl.Enabled = attr.UIIsEnable;
                        this.Pub1.AddTDCenter(ddl);
                        break;
                    case FieldTypeS.FK:
                        DDL ddl1 = new DDL();
                        ddl1.ID = "s" + attr.KeyOfEn + "" + i;
                        try
                        {
                            EntitiesNoName ens = attr.HisEntitiesNoName;
                            ens.RetrieveAll();
                            ddl1.BindEntities(ens);
                            ddl1.SetSelectItem(attr.DefVal);
                        }
                        catch
                        {
                        }
                        ddl1.Enabled = attr.UIIsEnable;
                        this.Pub1.AddTDCenter(ddl1);
                        break;
                    default:
                        break;
                }
            }
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTRSum();
        this.Pub1.AddTD(this.ToE("Sum","合计"));
        foreach (MapAttr attr in attrs)
        {
            if (attr.UIVisible == false)
                continue;
            if (attr.IsNum && attr.LGType == FieldTypeS.Normal)
            {
                TB tb = new TB();
                tb.ID = "TB_" + attr.KeyOfEn ;
                tb.Text = attr.DefVal;
                tb.ShowType = attr.HisTBType;
                tb.ReadOnly = true;
                tb.Font.Bold = true;
                tb.BackColor = System.Drawing.Color.FromName("infobackground");
                this.Pub1.AddTD(tb);
            }
            else
            {
                this.Pub1.AddTD();
            }
        }
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();

        // 输出自动计算公式
        this.Response.Write("\n<script language='JavaScript'>");
        for (int i = 0; i <= 10; i++)
        {
            string top = "\n function C" + i + "() { \n ";
            string script = "";
            foreach (MapAttr attr in attrs)
            {
                if (attr.UIVisible == false)
                    continue;
                if (attr.IsNum == false)
                    continue;

                if (attr.LGType != FieldTypeS.Normal)
                    continue;

                if (attr.AutoFullDoc != "")
                {
                    script += this.GenerAutoFull(i.ToString(), attrs, attr);
                }
            }
            string end = " \n  } ";
            this.Response.Write(top + script + end);
        }
        this.Response.Write("\n</script>");


        // 输出合计算计公式
        foreach (MapAttr attr in attrs)
        {
            if (attr.UIVisible == false)
                continue;

            if (attr.LGType != FieldTypeS.Normal)
                continue;

            if (attr.IsNum == false)
                continue;

            string top = "\n<script language='JavaScript'> function C" + attr.KeyOfEn + "() { \n ";
            string end = "\n } </script>";
            this.Response.Write(top + this.GenerSum(attr) + " ; \t\n" + end);
        }
    }
    /// <summary>
    /// 生成列的计算
    /// </summary>
    /// <param name="pk"></param>
    /// <param name="attrs"></param>
    /// <param name="attr"></param>
    /// <returns></returns>
    public string GenerAutoFull(string pk, MapAttrs attrs, MapAttr attr)
    {
        string left = "\n  document.forms[0]." + this.Pub1.GetTBByID("TB_" + attr.KeyOfEn + "_" + pk).ClientID + ".value = ";
        string right = attr.AutoFullDoc;
        foreach (MapAttr mattr in attrs)
        {
            string tbID = "TB_" + mattr.KeyOfEn + "_" + pk;
            TB tb = this.Pub1.GetTBByID(tbID);
            if (tb == null)
                continue;
            right = right.Replace("@" + mattr.Name, " parseFloat( document.forms[0]." + this.Pub1.GetTBByID(tbID).ClientID + ".value.replace( ',' ,  '' ) ) ");
            right = right.Replace("@" + mattr.KeyOfEn, " parseFloat( document.forms[0]." + this.Pub1.GetTBByID(tbID).ClientID + ".value.replace( ',' ,  '' ) ) ");
        }
        string s = left + right;
        s += "\t\n  document.forms[0]." + this.Pub1.GetTBByID("TB_" + attr.KeyOfEn + "_" + pk).ClientID + ".value= VirtyMoney(document.forms[0]." + this.Pub1.GetTBByID("TB_" + attr.KeyOfEn + "_" + pk).ClientID + ".value ) ;";
        return s += " C" + attr.KeyOfEn + "();";
    }

    public string GenerSum(MapAttr mattr)
    {
        string left = "\n  document.forms[0]." + this.Pub1.GetTBByID("TB_" + mattr.KeyOfEn).ClientID + ".value = ";
        string right = "";
        for (int i = 0; i <= 10; i++)
        {
            string tbID = "TB_" + mattr.KeyOfEn + "_" + i;
            TB tb = this.Pub1.GetTBByID(tbID);
            if (i == 0)
                right += " parseFloat( document.forms[0]." + tb.ClientID + ".value.replace( ',' ,  '' ) )  ";
            else
                right += " +parseFloat( document.forms[0]." + tb.ClientID + ".value.replace( ',' ,  '' ) )  ";
        }

        string s = left + right + " ;";
        switch (mattr.MyDataType)
        {
            case BP.DA.DataType.AppMoney:
            case BP.DA.DataType.AppRate:
                return s += "\t\n  document.forms[0]." + this.Pub1.GetTBByID("TB_" + mattr.KeyOfEn).ClientID + ".value= VirtyMoney(document.forms[0]." + this.Pub1.GetTBByID("TB_" + mattr.KeyOfEn).ClientID + ".value ) ;";
            default:
                return s;
        }
    }
}
