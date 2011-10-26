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
using BP.Sys;
using BP.En;
using BP.Web.Controls;
using BP.DA;
using BP.Web;

public partial class Comm_MapDef_MapDtlDe : WebPage
{
    #region 属性
    public new string MyPK
    {
        get
        {
            return this.Request.QueryString["FK_MapDtl"];
        }
    }
    public new string DoType
    {
        get
        {
            return this.Request.QueryString["DoType"];
        }
    }
    public string FK_MapExt
    {
        get
        {
            return this.Request.QueryString["FK_MapExt"];
        }
    }
    public new string Key
    {
        get
        {
            return this.Request.QueryString["Key"];
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
        this.Title = "从表设计";

        MapData.IsEditDtlModel = true;

        MapData md = new MapData(this.FK_MapData);
        MapDtl dtl = new MapDtl(this.FK_MapDtl);

        if (dtl.IsView == false)
            return;

        MapAttrs attrs = new MapAttrs(this.MyPK);

        MapExts mes = new MapExts(this.MyPK);
        if (mes.Count != 0)
        {
            this.Page.RegisterClientScriptBlock("s8",
          "<script language='JavaScript' src='./../Scripts/jquery-1.4.1.min.js' ></script>");

            this.Page.RegisterClientScriptBlock("b8",
         "<script language='JavaScript' src='./../Scripts/MapExt.js' ></script>");

            this.Page.RegisterClientScriptBlock("dCd",
   "<script language='JavaScript' src='./../../DataUser/JSLibData/" + this.FK_MapDtl + ".js' ></script>");

            this.Pub1.Add("<div id='divinfo' style='width: 155px; position: absolute; color: Lime; display: none;cursor: pointer;align:left'></div>");
        }

        if (attrs.Count == 0)
            dtl.IntMapAttrs();

        this.Title = md.Name + " - " + this.ToE("DesignDtl", "设计明细");
        this.Pub1.Add("<Table border=0 ID='Tab' style='padding:0px;align:left' >");
   //     this.Pub1.AddCaptionLeftTX("<a href='MapDef.aspx?MyPK=" + md.No + "' ><img src='../../Images/Btn/Back.gif' border=0/>" + this.ToE("Back","返回") + ":" + md.Name + "</a> - <img src='../../Images/Btn/Table.gif' border=0/>" + dtl.Name + " - <a href=\"javascript:AddF('" + this.MyPK + "');\" ><img src='../../Images/Btn/New.gif' border=0/>" + this.ToE("NewField", "新建字段") + "</a> ");

        this.Pub1.AddTR();
        if (dtl.IsShowIdx)
            this.Pub1.AddTDTitle(); 

        foreach (MapAttr attr in attrs)
        {
            if (attr.UIVisible == false)
                continue;

            this.Pub1.Add("<TH>");
            this.Pub1.Add("<a href=\"javascript:Up('" + this.MyPK + "','" + attr.MyPK + "');\" ><img src='../../Images/Btn/Left.gif' class=Arrow alt='向左移动' border=0/></a>");
            if (attr.HisEditType == EditType.UnDel || attr.HisEditType == EditType.Edit)
            {
                switch (attr.LGType)
                {
                    case FieldTypeS.Normal:
                        this.Pub1.Add("<a href=\"javascript:Edit('" + this.MyPK + "','" + attr.MyPK + "','" + attr.MyDataType + "');\"  alt='" + attr.KeyOfEn + "'>" + attr.Name + "</a>");
                        break;
                    case FieldTypeS.Enum:
                        this.Pub1.Add("<a href=\"javascript:EditEnum('" + this.MyPK + "','" + attr.MyPK + "');\" alt='" + attr.KeyOfEn + "' >" + attr.Name + "</a>");
                        break;
                    case FieldTypeS.FK:
                        this.Pub1.Add("<a href=\"javascript:EditTable('" + this.MyPK + "','" + attr.MyPK + "','" + attr.MyDataTypeS + "');\"  alt='" + attr.KeyOfEn + "'>" + attr.Name + "</a>");
                        break;
                    default:
                        break;
                }
            }
            else
            {
                this.Pub1.Add(attr.Name);
            }
            //  this.Pub1.Add("[<a href=\"javascript:Insert('" + this.MyPK + "','" + attr.IDX + "');\" ><img src='../../Images/Btn/Insert.gif' border=0/>插入</a>]");
            this.Pub1.Add("<a href=\"javascript:Down('" + this.MyPK + "','" + attr.MyPK + "');\" ><img src='../../Images/Btn/Right.gif' class=Arrow alt='向右移动' border=0/></a>");
            this.Pub1.Add("</TH>");  
        }
        this.Pub1.AddTREnd();

        for (int i =1 ; i <= dtl.RowsOfList; i++)
        {
            this.Pub1.AddTR();
             if (dtl.IsShowIdx)
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

                        TextBox tb = new TextBox();
                        tb.ID = "TB_" + attr.KeyOfEn + "_" + i;
                        tb.Text = attr.DefVal;
                        tb.ReadOnly = !attr.UIIsEnable;
                        this.Pub1.AddTD(tb);
                        switch (attr.MyDataType)
                        {
                            case BP.DA.DataType.AppString:
                                tb.Attributes["style"] = "width:" + attr.UIWidth + "px;border: none;";
                                if (attr.UIHeight > 25)
                                {
                                    tb.TextMode = TextBoxMode.MultiLine;
                                    tb.Attributes["Height"] = attr.UIHeight + "px";
                                    tb.Rows = attr.UIHeight / 25 ;
                                }
                                //if (tb.Enabled)
                                //    tb.Attributes["class"] = "TB";
                                //else
                                //    tb.Attributes["class"] = "TBReadonly";
                                break;
                            case BP.DA.DataType.AppDateTime:
                                tb.Attributes["style"] = "width:"+attr.UIWidth+"px;border: none;";
                                if (attr.UIIsEnable)
                                {
                                    tb.Attributes["onfocus"] = "WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'});";
                                    //tb.Attributes["class"] = "TBcalendar";
                                }
                                break;
                            case BP.DA.DataType.AppDate:
                                tb.Attributes["style"] = "width:" + attr.UIWidth + "px;border: none;";
                                if (attr.UIIsEnable)
                                {
                                    tb.Attributes["onfocus"] = "WdatePicker();";
                                  //  tb.Attributes["class"] = "TBcalendar";
                                }

                                break;
                            default:
                                tb.Attributes["style"] = "width:" + attr.UIWidth + "px;border: none;";
                                if (tb.ReadOnly == false)
                                {
                                    // OnKeyPress="javascript:return VirtyNum(this);"
                                    //tb.Attributes["OnKeyDown"] = "javascript:return VirtyNum(this);";

                                    if (attr.MyDataType == DataType.AppInt)
                                        tb.Attributes["OnKeyDown"] = "javascript:return VirtyInt(this);";
                                    else
                                        tb.Attributes["OnKeyDown"] = "javascript:return VirtyNum(this);";

                                    tb.Attributes["onkeyup"] += "javascript:C" + i + "();C" + attr.KeyOfEn + "();";
                                    tb.Attributes["class"] = "TBNum";
                                }
                                else
                                {
                                    // tb.Attributes["onpropertychange"] += "C" + attr.KeyOfEn + "();";
                                    tb.Attributes["class"] = "TBNumReadonly";
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
                        ddl1.ID = "DDL_" + attr.KeyOfEn + "_" + i;
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
        if (dtl.IsShowSum)
        {
            this.Pub1.AddTRSum();
            if (dtl.IsShowIdx)
                this.Pub1.AddTD(this.ToE("Sum", "合计"));

            foreach (MapAttr attr in attrs)
            {
                if (attr.UIVisible == false)
                    continue;
                if (attr.IsNum && attr.LGType == FieldTypeS.Normal)
                {
                    TB tb = new TB();
                    tb.ID = "TB_" + attr.KeyOfEn;
                    tb.Text = attr.DefVal;
                    tb.ShowType = attr.HisTBType;
                    tb.ReadOnly = true;
                    tb.Font.Bold = true;
                    tb.BackColor = System.Drawing.Color.FromName("infobackground");
                  //  tb.Attributes["class"] = "TBNum";
                    tb.Attributes["class"] = "TBNumReadonly";
                    this.Pub1.AddTD(tb);
                }
                else
                {
                    this.Pub1.AddTD();
                }
            }
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEnd();

        #region 处理设计时自动填充明细表.
        if (this.Key != null)
        {
            MapExt me = new MapExt(this.FK_MapExt);
            string[] strs = me.Tag1.Split('$');
            foreach (string str in strs)
            {
                if (str.Contains(this.FK_MapDtl) == false)
                    continue;

                string[] ss = str.Split(':');

                string sql = ss[1];
                sql = sql.Replace("@Key", this.Key);
                sql = sql.Replace("@key", this.Key);
                sql = sql.Replace("@val", this.Key);
                sql = sql.Replace("@Val", this.Key);

                DataTable dt = DBAccess.RunSQLReturnTable(sql);
                int idx = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    idx++;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        string val = dr[dc.ColumnName].ToString();
                        try
                        {
                            this.Pub1.GetTextBoxByID("TB_" + dc.ColumnName + "_" + idx).Text = val;
                        }
                        catch
                        {
                        }

                        try
                        {
                            this.Pub1.GetDDLByID("DDL_" + dc.ColumnName + "_" + idx).SetSelectItem(val);
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }
        #endregion 处理设计时自动填充明细表.

        #region 处理拓展属性.
        for (int i = 1; i <= dtl.RowsOfList; i++)
        {
            foreach (MapExt me in mes)
            {
                switch (me.ExtType)
                {
                    case MapExtXmlList.ActiveDDL:
                        DDL ddlPerant = this.Pub1.GetDDLByID("DDL_" + me.AttrOfOper + "_" + i);
                        if (ddlPerant == null)
                        {
                            me.Delete();
                            continue;
                        }

                        DDL ddlChild = this.Pub1.GetDDLByID("DDL_" + me.AttrsOfActive + "_" + i);
                        ddlPerant.Attributes["onchange"] = "DDLAnsc(this.value,\'" + ddlChild.ClientID + "\', \'" + me.MyPK + "\')";


                        string val = ddlPerant.SelectedItemStringVal;
                        DataTable dt = DBAccess.RunSQLReturnTable(me.Doc.Replace("@Key", val));
                        ddlChild.Items.Clear();
                        foreach (DataRow dr in dt.Rows)
                        {
                            ddlChild.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                        }
                        break;
                    case MapExtXmlList.FullCtrl: // 自动填充.
                        TextBox tbAuto = this.Pub1.GetTextBoxByID("TB_" + me.AttrOfOper   + "_" + i);
                        if (tbAuto == null)
                        {
                            me.Delete();
                            continue;
                        }
                        tbAuto.Attributes["onkeyup"] = "DoAnscToFillDiv(this,this.value,\'" + tbAuto.ClientID + "\', \'" + me.MyPK + "\');";
                        tbAuto.Attributes["AUTOCOMPLETE"] = "OFF";
                        // tbAuto.Attributes["onkeyup"] = "DoAnscToFillDiv(this,this.value);";
                        //    tbAuto.Attributes["onkeyup"] = "DoAnscToFillDiv(this,this.value,\'" + tbAuto.ClientID + "\', \'" + me.MyPK + "\');";
                        break;
                    case MapExtXmlList.InputCheck:
                         TextBox tbCheck = this.Pub1.GetTextBoxByID("TB_" + me.AttrOfOper   + "_" + i);
                         if (tbCheck != null)
                         {
                             tbCheck.Attributes[me.Tag2] += me.Tag1 + "(this);";
                         }
                         else
                         {
                             me.Delete();
                         }
                        break;
                    case MapExtXmlList.PopVal: //弹出窗.
                        TB tb = this.Pub1.GetTBByID("TB_" + me.AttrOfOper + me.AttrOfOper + "_" + i);
                        if (tb == null)
                        {
                            me.Delete();
                            continue;
                        }
                        tb.Attributes["ondblclick"] = "ReturnVal(this,'" + me.Doc + "','sd');";
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion 处理拓展属性.


        // 输出自动计算公式
        this.Response.Write("\n<script language='JavaScript'>");
        for (int i = 1; i <= dtl.RowsOfList;  i++ )
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

            if (attr.MyDataType ==DataType.AppBoolean )
                continue;

            string top = "\n<script language='JavaScript'> function C" + attr.KeyOfEn + "() { \n ";
            string end = "\n } </script>";
            this.Response.Write(top + this.GenerSum(attr,dtl) + " ; \t\n" + end);
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
        try
        {
            string left = "\n  document.forms[0]." + this.Pub1.GetTextBoxByID("TB_" + attr.KeyOfEn + "_" + pk).ClientID + ".value = ";
            string right = attr.AutoFullDoc;
            foreach (MapAttr mattr in attrs)
            {
                string tbID = "TB_" + mattr.KeyOfEn + "_" + pk;
                TextBox tb = this.Pub1.GetTextBoxByID(tbID);
                if (tb == null)
                    continue;
                right = right.Replace("@" + mattr.Name, " parseFloat( document.forms[0]." + this.Pub1.GetTextBoxByID(tbID).ClientID + ".value.replace( ',' ,  '' ) ) ");
                right = right.Replace("@" + mattr.KeyOfEn, " parseFloat( document.forms[0]." + this.Pub1.GetTextBoxByID(tbID).ClientID + ".value.replace( ',' ,  '' ) ) ");
            }
            string s = left + right;
            s += "\t\n  document.forms[0]." + this.Pub1.GetTextBoxByID("TB_" + attr.KeyOfEn + "_" + pk).ClientID + ".value= VirtyMoney(document.forms[0]." + this.Pub1.GetTextBoxByID("TB_" + attr.KeyOfEn + "_" + pk).ClientID + ".value ) ;";
            return s += " C" + attr.KeyOfEn + "();";
        }
        catch (Exception ex)
        {
            this.Alert(ex.Message);

            return "";
        }
        
    }

    public string GenerSum(MapAttr mattr,MapDtl dtl)
    {
        if (dtl.IsShowSum == false)
            return "";

        if (mattr.MyDataType == DataType.AppBoolean)
            return "";

        string left = "\n  document.forms[0]." + this.Pub1.GetTextBoxByID("TB_" + mattr.KeyOfEn).ClientID + ".value = ";
        string right = "";
        for (int i = 1; i <= dtl.RowsOfList; i++)
        {
            string tbID = "TB_" + mattr.KeyOfEn + "_" + i;
            TextBox tb = this.Pub1.GetTextBoxByID(tbID);
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
                return s += "\t\n  document.forms[0]." + this.Pub1.GetTextBoxByID("TB_" + mattr.KeyOfEn).ClientID + ".value= VirtyMoney(document.forms[0]." + this.Pub1.GetTextBoxByID("TB_" + mattr.KeyOfEn).ClientID + ".value ) ;";
            default:
                return s;
        }
    }
}
