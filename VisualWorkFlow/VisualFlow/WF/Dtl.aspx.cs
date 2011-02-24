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
using BP.Web;
using BP.DA;
using BP.En;
using BP.Sys;
using BP.Web.Controls;

public partial class Comm_Dtl : WebPage
{

    #region 属性
    public string EnsName
    {
        get
        {
            string str = this.Request.QueryString["EnsName"];
            if (str == null)
                return "ND299Dtl";
            return str;
        }
    }
    public string RefPK
    {
        get
        {
            string str = this.Request.QueryString["RefPK"];
            return str;
        }
    }
    public string RefPKVal
    {
        get
        {
            string str = this.Request.QueryString["RefPKVal"];
            if (str == null)
                return "1";
            return str;
        }
    }
    #endregion 属性

    public int DtlCount
    {
        get
        {
            return int.Parse(ViewState["DtlCount"].ToString());
        }
        set
        {
            
            ViewState["DtlCount"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Bind();
    }
    public int IsWap
    {
        get
        {
            if (this.Request.QueryString["IsWap"] == "1")
                return 1;
            return 0;
        }
    }
    public bool IsEnable
    {
        get
        {
            string s = this.ViewState["R"] as string;
            if (s == null || s == "0")
                return false;
            return true;
        }
        set
        {
            if (value)
                this.ViewState["R"] = "1";
            else
                this.ViewState["R"] = "0";
        }
    }
    public void Bind()
    {
        MapDtl mdtl = new MapDtl(this.EnsName);

        #region 生成标题
        if (this.IsWap == 1)
        {
            this.Pub1.AddTable();
            this.Pub1.AddTR();
            this.Pub1.AddTD("<a href='./../WAP/MyFlow.aspx?WorkID=" + this.RefPKVal + "' />返回</a>");

            this.Pub1.AddTD("<input type=button onclick=\"javascript:SaveDtlData();\" value='保存' />");
            this.Pub1.AddTREnd();
            this.Pub1.AddTableEnd();
        }

        MapAttrs attrs = new MapAttrs(this.EnsName);
        this.Pub1.Add("<Table border=1 style='padding:0px' >");
        this.Pub1.AddTR();
        if (mdtl.IsShowIdx)
            this.Pub1.Add("<TD class='FDesc'></TD>");

        foreach (MapAttr attr in attrs)
        {
            if (attr.UIVisible == false)
                continue;

            if (attr.IsPK)
                continue;

            if (attr.UIIsEnable)
                this.IsEnable = true;
            this.Pub1.Add("<TD class='FDesc' nowarp=true width=" + attr.UIWidth + "px ><label>" + attr.Name + "</label></TD>");
        }
        if (mdtl.IsDelete)
        {
            this.Pub1.Add("<TD class='FDesc' nowarp=true ></TD>");
        }
        this.Pub1.AddTREnd();
        #endregion 生成标题

        GEDtls dtls = new GEDtls(this.EnsName);
        QueryObject qo = null;
        try
        {
            qo = new QueryObject(dtls);
            switch (mdtl.DtlOpenType)
            {
                case DtlOpenType.ForEmp:
                    qo.AddWhere(GEDtlAttr.RefPK, this.RefPKVal);
                    // #warning 需要判断。
                    // qo.addAnd();
                    //qo.AddWhere(GEDtlAttr.Rec, WebUser.No);
                    break;
                case DtlOpenType.ForWorkID:
                    qo.AddWhere(GEDtlAttr.RefPK, this.RefPKVal);
                    break;
                case DtlOpenType.ForFID:
                    qo.AddWhere(GEDtlAttr.FID, this.RefPKVal);
                    break;
            }
        }
        catch (Exception ex)
        {
            dtls.GetNewEntity.CheckPhysicsTable();

            #region 解决Access 不刷新的问题。
            string rowUrl = this.Request.RawUrl;
            if (rowUrl.IndexOf("rowUrl") > 1)
            {
                throw ex;
            }
            else
            {
                this.Response.Redirect(rowUrl + "&rowUrl=1&IsWap=" + this.IsWap, true);
                return;
            }
            #endregion
            //this.Response.Redirect("Dtl.aspx?EnsName=" + this.EnsName + "&RefPKVal=" + this.RefPKVal, true);
        }

        #region 生成翻页
        this.Pub2.Clear();
        try
        {
            int count = qo.GetCount();
            this.DtlCount = count;
            this.Pub2.Clear();
            this.Pub2.BindPageIdx(count, mdtl.RowsOfList, this.PageIdx, "Dtl.aspx?EnsName=" + this.EnsName + "&RefPKVal=" + this.RefPKVal + "&IsWap=" + this.IsWap);
            int num = qo.DoQuery("OID", mdtl.RowsOfList, this.PageIdx, false);

            if (mdtl.IsInsert)
            {
                int dtlCount = dtls.Count;
                for (int i = 0; i < mdtl.RowsOfList - dtlCount; i++)
                {
                    BP.Sys.GEDtl dt = new GEDtl(this.EnsName);
                    dt.ResetDefaultVal();
                    dt.OID = i;
                    dtls.AddEntity(dt);
                }

                if (num == mdtl.RowsOfList)
                {
                    BP.Sys.GEDtl dt1 = new GEDtl(this.EnsName);
                    dt1.ResetDefaultVal();
                    dt1.OID = mdtl.RowsOfList + 1;
                    dtls.AddEntity(dt1);
                }
            }
        }
        catch (Exception ex)
        {
            dtls.GetNewEntity.CheckPhysicsTable();

            #region 解决Access 不刷新的问题。
            string rowUrl = this.Request.RawUrl;
            if (rowUrl.IndexOf("rowUrl") > 1)
            {
                throw ex;
            }
            else
            {
                this.Response.Redirect(rowUrl + "&rowUrl=1&IsWap=" + this.IsWap, true);
                return;
            }
            #endregion
        }
        #endregion 生成翻页

        DDL ddl = new DDL();
        CheckBox cb = new CheckBox();

        #region 生成数据
        int idx = 1;
      //  bool is1 = false;
        foreach (BP.Sys.GEDtl dtl in dtls)
        {
            this.Pub1.AddTR();
            if (mdtl.IsShowIdx)
            {
                this.Pub1.AddTDIdx(idx++);
            }
            foreach (MapAttr attr in attrs)
            {
                if (attr.UIVisible == false)
                    continue;

                if (attr.KeyOfEn == "OID")
                    continue;

                string val = dtl.GetValByKey(attr.KeyOfEn).ToString();
                switch (attr.UIContralType)
                {
                    case UIContralType.TB:
                        TextBox tb = new TextBox();
                        //tb.LoadMapAttr(attr);
                        tb.ID = "TB_" + attr.KeyOfEn + "_" + dtl.OID;
                        tb.Enabled = attr.UIIsEnable;
                        tb.Attributes["onfocus"] = "isChange=true;";
                        switch (attr.MyDataType)
                        {
                            case DataType.AppString:
                                tb.Attributes["style"] = "width:" + attr.UIWidth + "px;border-width:0px;";
                                this.Pub1.AddTD("width='2px'", tb);
                                tb.Text = val;
                                break;
                            case DataType.AppDate:
                                tb.Attributes["style"] = "width:" + attr.UIWidth + "px;border-width:0px;";
                                tb.Text = val;
                                if (attr.UIIsEnable)
                                    tb.Attributes["onfocus"] = "calendar(this);isChange=true;";
                                this.Pub1.AddTD("width='2px'", tb);
                                break;
                            case DataType.AppDateTime:
                                tb.Attributes["style"] = "width:" + attr.UIWidth + "px;border-width:0px;";
                                tb.Text = val;
                                if (attr.UIIsEnable)
                                    tb.Attributes["onfocus"] = "CalendarHM();isChange=true;";
                                this.Pub1.AddTD("width='2px'", tb);
                                break;
                            case DataType.AppMoney:
                            case DataType.AppRate:
                                tb.Attributes["style"] = "width:" + attr.UIWidth + "px;border-width:0px;";
                                try
                                {
                                    tb.Text = decimal.Parse(val).ToString("0.00");
                                }
                                catch(Exception ex)
                                {
                                    this.Alert(ex.Message+" val ="+val);
                                    tb.Text = "0";
                                }
                                this.Pub1.AddTDNum(tb);
                                break;
                            default:
                                tb.Attributes["style"] = "width:" + attr.UIWidth + "px;text-align:right;border-width:0px;";
                                tb.Text = val;
                                this.Pub1.AddTDNum(tb);
                                break;
                        }

                        if (attr.IsNum && attr.LGType == FieldTypeS.Normal)
                        {
                            if (tb.Enabled)
                            {
                                // OnKeyPress="javascript:return VirtyNum(this);"
                                if (attr.MyDataType == DataType.AppInt)
                                    tb.Attributes["OnKeyDown"] = "javascript:return VirtyInt(this);";
                                else
                                    tb.Attributes["OnKeyDown"] = "javascript:return VirtyNum(this);";

                                tb.Attributes["onkeyup"] += "javascript:C" + dtl.OID + "();C" + attr.KeyOfEn + "();";
                                tb.Attributes["style"] = "width:" + attr.UIWidth + "px;text-align:right;border-width:0px;";
                            }
                            else
                            {
                                tb.Attributes["onpropertychange"] += "C" + attr.KeyOfEn + "();";
                                tb.Attributes["style"] = "width:" + attr.UIWidth + "px;text-align:right;border-width:0px;";
                            }
                        }
                        break;
                    case UIContralType.DDL:
                        ddl = new DDL();
                        if (attr.UIIsEnable)
                        {
                            ddl.LoadMapAttr(attr);
                            ddl.ID = "DDL_" + attr.KeyOfEn + "_" + dtl.OID;
                            ddl.Attributes["onchange"] = "isChange= true;";
                            this.Pub1.AddTD(ddl);
                            ddl.SetSelectItem(val);
                        }
                        else
                        {
                            ddl.Items.Add(new ListItem(dtl.GetValRefTextByKey(attr.KeyOfEn), val));
                            this.Pub1.AddTD(ddl);
                            ddl.Enabled = false;
                        }
                        break;
                    case UIContralType.CheckBok:
                        cb = new CheckBox();
                        cb.ID = "CB_" + attr.KeyOfEn + "_" + dtl.OID;
                        cb.Text = attr.Name;
                        if (val == "1")
                            cb.Checked = true;
                        else
                            cb.Checked = false;
                        cb.Attributes["onclick"] = "isChange= true;";
                        this.Pub1.AddTD(cb);
                        break;
                    default:
                        break;
                }
            }

            if (mdtl.IsDelete && dtl.OID > mdtl.RowsOfList + 1)
            {
                this.Pub1.Add("<TD border=0><img src='../Images/Btn/Delete.gif' onclick=\"javascript:Del('" + dtl.OID + "','" + this.EnsName + "')\" /></TD>");
            }
            else if (mdtl.IsDelete)
            {
                this.Pub1.Add("<TD class=TD border=0></TD>");
            }
            this.Pub1.AddTREnd();
        }

        #region 生成合计
        if (mdtl.IsShowSum && dtls.Count > 1)
        {
            this.Pub1.AddTRSum();
            if (mdtl.IsShowIdx)
                this.Pub1.AddTD();

            foreach (MapAttr attr in attrs)
            {
                if (attr.UIVisible == false)
                    continue;

                if (attr.IsNum && attr.LGType == FieldTypeS.Normal)
                {
                    TextBox tb = new TextBox();
                    tb.ID = "TB_" + attr.KeyOfEn;
                    tb.Text = attr.DefVal;
                   // tb.ShowType = attr.HisTBType;
                    tb.ReadOnly = true;
                    tb.Font.Bold = true;
                    tb.BackColor = System.Drawing.Color.FromName("infobackground");
                    switch (attr.MyDataType)
                    {
                        case DataType.AppRate:
                        case DataType.AppMoney:
                            tb.Text = dtls.GetSumDecimalByKey(attr.KeyOfEn).ToString("0.00");
                            tb.Attributes["style"] = "width:" + attr.UIWidth + "px;text-align:right;border:none";

                            break;
                        case DataType.AppInt:
                            tb.Text = dtls.GetSumIntByKey(attr.KeyOfEn).ToString();
                            tb.Attributes["style"] = "width:" + attr.UIWidth + "px;text-align:right;border:none";

                            break;
                        case DataType.AppFloat:
                            tb.Text = dtls.GetSumFloatByKey(attr.KeyOfEn).ToString();
                            tb.Attributes["style"] = "width:" + attr.UIWidth + "px;text-align:right;border:none";
                            break;
                        default:
                            break;
                    }
                    this.Pub1.AddTD("align=right",tb);
                }
                else
                {
                    this.Pub1.AddTD();
                }
            }
            this.Pub1.AddTREnd();
        }
        #endregion 生成合计


        #endregion 生成数据
        this.Pub1.AddTableEnd();

        //Button btn = new Button();
        //btn.Click += new EventHandler(Button1_Click);
        //btn.ID = "Btn_Save";
        //this.Pub2.Add(btn);

        #region 生成 自动计算行
        // 输出自动计算公式
        this.Response.Write("\n<script language='JavaScript' >");
        foreach (GEDtl dtl in dtls)
        {
            string top = "\n function C" + dtl.OID + "() { \n ";

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
                    script += this.GenerAutoFull(dtl.OID.ToString(), attrs, attr);
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
            string end = "\n  isChange =true ;  } </script>";
            this.Response.Write(top + this.GenerSum(attr, dtls) + " ; \t\n" + end);
        }
        #endregion
    }

    public void Delete()
    {

    }
    public void Save()
    {
        MapDtl mdtl = new MapDtl(this.EnsName);
        GEDtls dtls = new GEDtls(this.EnsName);
        QueryObject qo = null;
        qo = new QueryObject(dtls);
        switch (mdtl.DtlOpenType)
        {
            case DtlOpenType.ForEmp:
                qo.AddWhere(GEDtlAttr.RefPK, this.RefPKVal);
                //qo.addAnd();
                //qo.AddWhere(GEDtlAttr.Rec, WebUser.No);
                break;
            case DtlOpenType.ForWorkID:
                qo.AddWhere(GEDtlAttr.RefPK, this.RefPKVal);
                break;
            case DtlOpenType.ForFID:
                qo.AddWhere(GEDtlAttr.FID, this.RefPKVal);
                break;
        }

        int num = qo.DoQuery("OID", mdtl.RowsOfList, this.PageIdx, false);
        int dtlCount = dtls.Count;
        for (int i = 0; i < mdtl.RowsOfList - dtlCount; i++)
        {
            BP.Sys.GEDtl dt = new GEDtl(this.EnsName);
            dt.ResetDefaultVal();
            dt.OID = i;
            dtls.AddEntity(dt);
        }

        if (num == mdtl.RowsOfList)
        {
            BP.Sys.GEDtl dt1 = new GEDtl(this.EnsName);
            dt1.ResetDefaultVal();
            dt1.OID = mdtl.RowsOfList + 1;
            dtls.AddEntity(dt1);
        }

        Map map = dtls.GetNewEntity.EnMap;
        bool isTurnPage = false;
        string err = "";
        int idx=0;
        foreach (GEDtl dtl in dtls)
        {
            idx++;
            try
            {
                this.Pub1.Copy(dtl, dtl.OID.ToString(), map);
                if (dtl.OID < mdtl.RowsOfList + 2)
                {
                    int myOID = dtl.OID;
                    dtl.OID = 0;
                    if (dtl.IsBlank)
                        continue;

                    dtl.OID = myOID;
                    if (dtl.OID == mdtl.RowsOfList + 1)
                        isTurnPage = true;

                    dtl.RefPK = this.RefPKVal;
                    dtl.InsertAsNew();
                }
                else
                    dtl.Update();
            }
            catch(Exception ex)
            {
                err += "Row: " + idx + " Error \r\n" + ex.Message;
            }
        }

        if (err != "")
            this.Alert(err);

        if (isTurnPage)
        {
            int pageNum = 0;
            int count = this.DtlCount + 1;
            decimal pageCountD = decimal.Parse(count.ToString()) / decimal.Parse(mdtl.RowsOfList.ToString()); // 页面个数。
            string[] strs = pageCountD.ToString("0.0000").Split('.');
            if (int.Parse(strs[1]) > 0)
                pageNum = int.Parse(strs[0]) + 1;
            else
                pageNum = int.Parse(strs[0]);
            this.Response.Redirect("Dtl.aspx?EnsName=" + this.EnsName + "&RefPKVal=" + this.RefPKVal + "&PageIdx=" + pageNum, true);
        }
        else
            this.Response.Redirect("Dtl.aspx?EnsName=" + this.EnsName + "&RefPKVal=" + this.RefPKVal + "&PageIdx=" + this.PageIdx, true);
    }
    public void ExpExcel()
    {
        BP.Sys.MapDtl mdtl = new MapDtl(this.EnsName);

        this.Title = mdtl.Name;

        GEDtls dtls = new GEDtls(this.EnsName);
        QueryObject qo = null;
        qo = new QueryObject(dtls);
        switch (mdtl.DtlOpenType)
        {
            case DtlOpenType.ForEmp:
                qo.AddWhere(GEDtlAttr.RefPK, this.RefPKVal);
                //qo.addAnd();
                //qo.AddWhere(GEDtlAttr.Rec, WebUser.No);
                break;
            case DtlOpenType.ForWorkID:
                qo.AddWhere(GEDtlAttr.RefPK, this.RefPKVal);
                break;
            case DtlOpenType.ForFID:
                qo.AddWhere(GEDtlAttr.FID, this.RefPKVal);
                break;
        }
        qo.DoQuery();

        // this.ExportDGToExcelV2(dtls, this.Title + ".xls");
        //DataTable dt = dtls.ToDataTableDesc();
        // this.GenerExcel(dtls.ToDataTableDesc(), mdtl.Name + ".xls");

        this.GenerExcel_pri_Text(dtls.ToDataTableDesc(), mdtl.Name + "@" + WebUser.No + "@" + DataType.CurrentData + ".xls");

        //this.ExportDGToExcelV2(dtls, this.Title + ".xls");
        //dtls.GetNewEntity.CheckPhysicsTable();
        //this.Response.Redirect("Dtl.aspx?EnsName=" + this.EnsName + "&RefPKVal=" + this.RefPKVal, true);
    }
    void BPToolBar1_ButtonClick(object sender, EventArgs e)
    {
        ToolbarBtn btn = sender as ToolbarBtn;
        switch (btn.ID)
        {
            case NamesOfBtn.New:
            case NamesOfBtn.Save:
            case NamesOfBtn.SaveAndNew:
                this.Save();
                break;
            case NamesOfBtn.SaveAndClose:
                this.Save();
                this.WinClose();
                break;
            case NamesOfBtn.Delete:
                GEDtls dtls = new GEDtls(this.EnsName);
                QueryObject qo = new QueryObject(dtls);
                qo.DoQuery("OID", BP.SystemConfig.PageSize, this.PageIdx, false);
                foreach (GEDtl dtl in dtls)
                {
                    CheckBox cb = this.Pub1.GetCBByID("CB_" + dtl.PKVal);
                    if (cb == null)
                        continue;

                    if (cb.Checked)
                        dtl.Delete();
                }
                this.Pub1.Clear();
                this.Bind();
                break;
            case NamesOfBtn.Excel:
                this.ExpExcel();
                break;
            default:
                BP.PubClass.Alert("@当前版本不支持此功能。");
                break;
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
    public string GenerSum(MapAttr mattr, GEDtls dtls)
    {
        if (dtls.Count <= 1)
            return "";

        string ClientID = "";

        try
        {
            ClientID = this.Pub1.GetTextBoxByID("TB_" + mattr.KeyOfEn).ClientID;
        }
        catch
        {
            return "";
        }

        string left = "\n  document.forms[0]." + ClientID + ".value = ";
        string right = "";
        int i = 0;
        foreach (GEDtl dtl in dtls)
        {
            string tbID = "TB_" + mattr.KeyOfEn + "_" + dtl.OID;
            TextBox tb = this.Pub1.GetTextBoxByID(tbID);
            if (i == 0)
                right += " parseFloat( document.forms[0]." + tb.ClientID + ".value.replace( ',' ,  '' ) )  ";
            else
                right += " +parseFloat( document.forms[0]." + tb.ClientID + ".value.replace( ',' ,  '' ) )  ";

            i++;
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
   
    protected void Button1_Click(object sender, EventArgs e)
    {
        this.Save();
    }
}
