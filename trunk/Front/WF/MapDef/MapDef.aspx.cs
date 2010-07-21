using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BP.Sys;
using BP.En;
using BP.Web.Controls;
using BP.DA;
using BP.Web;

public partial class WF_MapDef_MapDef : WebPage
{
    public string MyPK
    {
        get
        {
            string key = this.Request.QueryString["MyPK"];
            if (key == null)
                key = this.Request.QueryString["PK"];
            if (key == null)
                key = "ND201";
            return key;
        }
    }
    /// <summary>
    /// IsEditMapData
    /// </summary>
    public bool IsEditMapData
    {
        get
        {
            string s = this.Request.QueryString["IsEditMapData"];
            if (s == null || s == "1")
                return true;
            return false;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
       
        MapData m2d = new MapData();
        m2d.CheckPhysicsTable();

        MapData md = new MapData(this.MyPK);
        BindAttrs(md);
        //MapDtls dtls = new MapDtls(this.MyPK);
        //if (dtls.Count >= 1)
        //    this.Pub1.AddHR();

        //foreach (MapDtl dtl in dtls)
        //{
        //    this.Pub1.AddB("[<a href='MapDtlDe.aspx?DoType=Edit&FK_MapData=" + this.MyPK + "&FK_MapDtl=" + dtl.No + "' >" + dtl.Name + "</a>]");
        //}
    }

    public void InsertDtl(MapDtls dtls, int idx)
    {
        MapDtl mydtl = null;
        foreach (MapDtl dtl in dtls)
        {
            //if (dtl.InsertIdx != idx)
            //    continue;

            mydtl = dtl;
            break;
        }

        if (mydtl == null)
            return;


        this.Pub1.AddTR();
        this.Pub1.Add("<TD　colspan=4 class=TD height=100px >");

        this.Pub1.Add("<iframe frameborder=1 leftMargin='0' topMargin='0' src='MapDtlDe.aspx?DoType=Edit&FK_MapData="+mydtl.FK_MapData+"&FK_MapDtl="+mydtl.No+"' width='100%' height='100%' class=iframe name=fm style='border-style:none;' id=fm />");

        this.Pub1.Add("</TD>");
        this.Pub1.AddTREnd();
    }
     
    /// <summary>
    /// 
    /// </summary>
    /// <param name="md"></param>
    public void BindAttrs(MapData md)
    {
        GroupFields gfs = new GroupFields(md.No);
        MapDtls dtls = new MapDtls(this.MyPK);

        Attrs attrs = md.GenerHisMap().Attrs;
        MapAttrs mattrs = new MapAttrs(md.No);
        int count = mattrs.Count;
        bool isReadonly = false;
        //this.Pub1.AddB(this.ToE("DesignSheet", "设计表单") + " - <a href=\"javascript:AddF('" + this.MyPK + "');\" ><img src='../../Images/Btn/Add.gif' border=0/>" + this.ToE("NewField", "新建字段") + "</a> - <a href=\"javascript:AddTable('" + this.MyPK + "');\" ><img src='../../Images/Btn/Table.gif' border=0/>" + this.ToE("NewTable", "表格") + "</a> - <a href=\"CopyFieldFromNode.aspx?FK_Node=" + this.MyPK + "\" ><img src='../../Images/Btn/Add.gif' border=0/>" + this.ToE("NewField", "从节点复制字段") + "</a> - <a href=\"MapDtl.aspx?DoType=DtlList&FK_MapData=" + this.MyPK + "\" >" + this.ToE("DesignDtl", "设计明细") + "</a> - <a href=\"javascript:GroupField('" + md.No + "')\">字段分组</a><hr>");

        this.Pub1.AddB("&nbsp;&nbsp;" + this.ToE("DesignSheet", "设计表单") + " - <a href=\"javascript:AddF('" + this.MyPK + "');\" ><img src='../../Images/Btn/New.gif' border=0/>" + this.ToE("NewField", "新建字段") + "</a> - <a href=\"CopyFieldFromNode.aspx?FK_Node=" + this.MyPK + "\" ><img src='../../Images/Btn/Copy.gif' border=0/>" + this.ToE("NewField", "复制字段") + "</a> - <a href=\"MapDtl.aspx?DoType=DtlList&FK_MapData=" + this.MyPK + "\" >" + this.ToE("DesignDtl", "设计明细") + "</a> - <a href=\"javascript:GroupField('" + md.No + "')\">字段分组</a><hr>");

        int i = -1;
        int idx = -1;
        bool isHaveH = false;

        this.Pub1.Add("<Table class='Table' >");
        if (gfs.Count >= 1)
        {
            this.Pub1.AddTR("onclick=\"GroupBarClick('0');\" class=GroupField ");
            this.Pub1.AddTD("colspan=4 class=GroupField width='600px' ", "&nbsp;&nbsp;<img src='./Style/Max.gif' alert='Min' id='Img0'  border=0 />&nbsp;<b>" + md.Name + "</b>");
            this.Pub1.AddTREnd();
        }
        else
        {
            this.Pub1.Add("<TR>");
            this.Pub1.AddTD("colspan=4  width='600px' class=GroupField ", "&nbsp;&nbsp;<b>" + md.Name + "</b>");
            this.Pub1.Add("</TR>");
        }

        int rowIdx = 0;
        bool isLeftNext = true;
        GroupField currGF = new GroupField();
        foreach (MapAttr attr in mattrs)
        {
            #region 处理不显示的
            if (attr.HisAttr.IsRefAttr)
                continue;

            if (attr.UIVisible == false)
            {
                isHaveH = true;
                continue;
            }
            #endregion

            #region deal GroupField
            if (isLeftNext == true)
            {
                foreach (GroupField gf in gfs)
                {
                    if (rowIdx == gf.RowIdx && gf.IsUse == false )
                    {
                        gf.IsUse = true;
                        currGF = gf;
                        this.Pub1.AddTR();
                        this.Pub1.AddTD("colspan=4 class=GroupField ", "&nbsp;&nbsp;<img onclick=\"GroupBarClick('" + gf.RowIdx + "');\" src='./Style/Max.gif' alert='Min' id='Img" + gf.RowIdx + "'  border=0 />&nbsp;<b>" + gf.Lab + "</b><div > <a href=\"javascript:GroupField('" + md.No + "')\" >编辑</a> <a href=\"javascript:GFDoUp('" + gf.OID + "')\" >上移</a>  <a href=\"javascript:GFDoDown('" + gf.OID + "')\" >下移</a> </div>");
                        this.Pub1.AddTREnd();
                        isLeftNext = true;
                        break;
                    }
                }


                foreach (MapDtl dtl in dtls)
                {
                    if (rowIdx == dtl.RowIdx && dtl.IsUse == false)
                    {
                        dtl.IsUse = true;
                        this.Pub1.AddTR();
                        this.Pub1.AddTDBegin("colspan=4  width='100%' height='300px'  "); //, "&nbsp;&nbsp;<img onclick=\"GroupBarClick('" + gf.RowIdx + "');\" src='./Style/Max.gif' alert='Min' id='Img" + gf.RowIdx + "'  border=0 />&nbsp;<b>" + gf.Lab + "</b><div > <a href=\"javascript:GroupField('" + md.No + "')\" >编辑</a> <a href=\"javascript:GFDoUp('" + gf.OID + "')\" >上移</a>  <a href=\"javascript:GFDoDown('" + gf.OID + "')\" >下移</a> </div>");

                        string src = "MapDtlDe.aspx?DoType=Edit&FK_MapData="+md.No+"&FK_MapDtl="+dtl.No;

                        this.Pub1.Add("<iframe frameborder=1 leftMargin='0' topMargin='0' src='" + src + "' width='100%' height='100%' class=iframe name=fm style='border-style:none;' id=fm > </iframe>");

                        this.Pub1.AddTDEnd();
                        this.Pub1.AddTREnd();
                    }
                }

            }
            #endregion deal groupField


            // 显示的顺序号.
            idx++;
            if (attr.IsBigDoc && attr.UIIsLine)
            {
                if (isLeftNext == false)
                {
                    this.Pub1.AddTD();
                    this.Pub1.AddTD();
                    this.Pub1.AddTREnd();
                }

                this.Pub1.AddTR(" ID='" + currGF.RowIdx + "_" + rowIdx + "' ");
                this.Pub1.Add("<TD class=FDesc colspan=4>");
                this.Pub1.Add(this.GenerLab(attr, idx, 0, count) + "<br>");
                TextBox mytbLine = new TextBox();
                mytbLine.TextMode = TextBoxMode.MultiLine;
                mytbLine.Rows = 8;
                mytbLine.Columns = 60;
                mytbLine.Attributes["width"] = "100%";
                this.Pub1.Add(mytbLine);
                this.Pub1.AddTDEnd();
                this.Pub1.AddTREnd();
                rowIdx++;
                isLeftNext = true;
                continue;
            }

            if (attr.IsBigDoc)
            {
                if (isLeftNext)
                    this.Pub1.AddTR(" ID='" + currGF.RowIdx + "_" + rowIdx + "' ");

                this.Pub1.Add("<TD class=FDesc colspan=2>");
                this.Pub1.Add(this.GenerLab(attr, idx, 0, count) + "<br>");
                TextBox mytbLine = new TextBox();
                mytbLine.TextMode = TextBoxMode.MultiLine;
                mytbLine.Rows = 8;
                mytbLine.Columns = 30;
                mytbLine.Attributes["width"] = "100%";
                this.Pub1.Add(mytbLine);
                this.Pub1.AddTDEnd();

                if (isLeftNext == false)
                {
                    this.Pub1.AddTREnd();
                    rowIdx++;
                }

                isLeftNext = !isLeftNext;
                continue;
            }

            //计算 colspanOfCtl .
            int colspanOfCtl = 1;
            if (attr.UIIsLine)
                colspanOfCtl = 3;

            if (attr.UIIsLine)
            {
                if (isLeftNext == false)
                {
                    this.Pub1.AddTD();
                    this.Pub1.AddTD();
                    this.Pub1.AddTREnd();
                    rowIdx++;
                }
                isLeftNext = true;

                foreach (GroupField gf in gfs)
                {
                    if (rowIdx == gf.RowIdx && gf.IsUse==false)
                    {
                        gf.IsUse = true;
                        currGF = gf;
                        this.Pub1.AddTR();
                        this.Pub1.AddTD("colspan=4 class=GroupField ", "&nbsp;&nbsp;<img onclick=\"GroupBarClick('" + gf.RowIdx + "');\" src='./Style/Max.gif' alert='Min' id='Img" + gf.RowIdx + "'  border=0 />&nbsp;<b>" + gf.Lab + "</b><div > <a href=\"javascript:GroupField('" + md.No + "')\" >编辑</a> <a href=\"javascript:GFDoUp('" + gf.OID + "')\" >上移</a>  <a href=\"javascript:GFDoDown('" + gf.OID + "')\" >下移</a> </div>");
                        this.Pub1.AddTREnd();
                        isLeftNext = true;
                        i = -1;
                        break;
                    }
                }
            }


            if (isLeftNext)
            {
                this.Pub1.AddTR(" ID='" + currGF.RowIdx + "_" + rowIdx + "' ");
            }

            TB tb = new TB();
            tb.Attributes["width"] = "100%";
            tb.Columns = 60;

            #region add contrals.
            switch (attr.LGType)
            {
                case FieldTypeS.Normal:

                    tb.Enabled = attr.UIIsEnable;
                    switch (attr.MyDataType)
                    {
                        case BP.DA.DataType.AppString:
                            this.Pub1.AddTDDesc(this.GenerLab(attr, idx, i, count));
                            tb.ShowType = TBType.TB;
                            tb.Text = attr.DefVal;
                            this.Pub1.AddTD("colspan=" + colspanOfCtl, tb);
                            break;
                        case BP.DA.DataType.AppDate:
                            this.Pub1.AddTDDesc(this.GenerLab(attr, idx, i, count));
                            tb.ShowType = TBType.Date;
                            tb.Text = attr.DefVal;
                            this.Pub1.AddTD("colspan=" + colspanOfCtl, tb);
                            break;
                        case BP.DA.DataType.AppDateTime:
                            this.Pub1.AddTDDesc(this.GenerLab(attr, idx, i, count));
                            tb.ShowType = TBType.DateTime;
                            tb.Text = attr.DefVal;
                            this.Pub1.AddTD("colspan=" + colspanOfCtl, tb);
                            break;
                        case BP.DA.DataType.AppBoolean:
                            if (attr.UIIsLine)
                                this.Pub1.AddTDDesc(this.GenerLab(attr, idx, i, count));
                            else
                                this.Pub1.AddTDDesc(this.GenerLab(attr, idx, i, count));

                            CheckBox cb = new CheckBox();
                            cb.Text = attr.Name;
                            cb.Checked = attr.DefValOfBool;
                            cb.Enabled = attr.UIIsEnable;
                            this.Pub1.AddTD("colspan=" + colspanOfCtl, cb);
                            break;
                        case BP.DA.DataType.AppDouble:
                        case BP.DA.DataType.AppFloat:
                        case BP.DA.DataType.AppInt:
                            this.Pub1.AddTDDesc(this.GenerLab(attr, idx, i, count));
                            tb.ShowType = TBType.Num;
                            tb.Text = attr.DefVal;
                            this.Pub1.AddTD("colspan=" + colspanOfCtl, tb);
                            break;
                        case BP.DA.DataType.AppMoney:
                        case BP.DA.DataType.AppRate:
                            this.Pub1.AddTDDesc(this.GenerLab(attr, idx, i, count));
                            tb.ShowType = TBType.Moneny;
                            tb.Text = attr.DefVal;
                            this.Pub1.AddTD("colspan=" + colspanOfCtl, tb);
                            break;
                        default:
                            break;
                    }
                    tb.Attributes["width"] = "100%";
                    switch (attr.MyDataType)
                    {
                        case BP.DA.DataType.AppString:
                        case BP.DA.DataType.AppDateTime:
                        case BP.DA.DataType.AppDate:
                            if (tb.Enabled)
                                tb.Attributes["class"] = "TB";
                            else
                                tb.Attributes["class"] = "TBReadonly";
                            break;
                        default:
                            if (tb.Enabled)
                                tb.Attributes["class"] = "TBNum";
                            else
                                tb.Attributes["class"] = "TBReadonlyNum";
                            break;
                    }
                    break;
                case FieldTypeS.Enum:
                    this.Pub1.AddTDDesc(this.GenerLab(attr, idx, i, count));
                    DDL ddle = new DDL();
                    ddle.BindSysEnum(attr.KeyOfEn);
                    ddle.SetSelectItem(attr.DefVal);
                    ddle.Enabled = attr.UIIsEnable;
                    this.Pub1.AddTD("colspan=" + colspanOfCtl, ddle);
                    break;
                case FieldTypeS.FK:
                    this.Pub1.AddTDDesc(this.GenerLab(attr, idx, i, count));
                    DDL ddl1 = new DDL();
                    ddl1.ID = "DDL_" + attr.KeyOfEn;
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
                    this.Pub1.AddTD("colspan=" + colspanOfCtl, ddl1);
                    break;
                default:
                    break;
            }
            #endregion add contrals.

            if (colspanOfCtl == 3)
            {
                isLeftNext = true;
                this.Pub1.AddTREnd();
                rowIdx++;
                continue;
            }

            if (isLeftNext == false)
            {
                isLeftNext = true;
                this.Pub1.AddTREnd();
                rowIdx++;
                continue;
            }
            isLeftNext = false;
        }

        if (isLeftNext == false)
        {
            this.Pub1.AddTD();
            this.Pub1.AddTD();
            this.Pub1.AddTREnd();
        }

        foreach (GroupField gf in gfs)
        {
            if (rowIdx <= gf.RowIdx && gf.IsUse == false)
            {
                if (gf.RowIdx > rowIdx)
                {
                    gf.RowIdx = rowIdx;
                    gf.Update();
                }

                this.Pub1.AddTR();
                this.Pub1.AddTD("colspan=4 class=GroupField ", "&nbsp;&nbsp;<img src='./Style/Max.gif' alert='Min' id='Img" + gf.RowIdx + "'  border=0 />&nbsp;<b>" + gf.Lab + "</b><div ><a href=\"javascript:GroupField('" + md.No + "')\" >编辑</a> <a href=\"javascript:GFDoUp('" + gf.OID + "')\" >上移</a> <a href=\"javascript:GroupFieldDel('"+md.No+"'," + gf.OID + "')\" >上移</a></div>");
                this.Pub1.AddTREnd();
                 
            }
        }
        this.Pub1.AddTableEnd();

        // 输出隐藏的字段让用户编辑
        if (isHaveH == false)
            return;

        this.Pub1.AddFieldSet("编辑隐藏字段");

        string msg = ""; // +++++++ 编辑隐藏字段 +++++++++ <br>";
        foreach (MapAttr attr in mattrs)
        {
            if (attr.UIVisible)
                continue;
            switch (attr.KeyOfEn)
            {
                case "OID":
                case "Emps":
                case "FK_Dept":
                case "NodeState":
                case "WFState":
                case "BillNo":
                case "RDT":
                case "MyNum":
                case "WFLog":
                    continue;
                    break;
                default:
                    break;
            }
            msg += "<a href=\"javascript:Edit('" + this.MyPK + "','" + attr.OID + "','" + attr.MyDataType + "');\">" + attr.Name + "</a> ";
        }
        this.Pub1.Add(msg);
        this.Pub1.Add("<br>说明：隐藏字段是不显示在表单里面，多用于属性的计算、方向条件的设置，报表的体现。");

        this.Pub1.AddFieldSetEnd();



        //if (attrs.Count > 16 )
        //{
        //    this.Pub1.AddFieldSet("字段分组");

        //    this.Pub1.Add("比如:@Field2=分组1@Field2=分组2<br>");
        //    TextBox tbt = new TextBox();
        //    tbt.Text = "";
        //    tbt.ID = "TB_GrouTitle";
        //    tbt.Columns = 50;
        //    tbt.Text = cfg.GroupTitle;
        //    this.Pub1.Add(tbt);

        //    Button btn = new Button();
        //    btn.Text = " 应用 ";
        //    btn.ID = "Btn_Save";
        //    btn.Click += new EventHandler(btn_Click);

        //    this.Pub1.Add(btn); //, "<a href='javascript:HelpGroup()' ><img src='../../Images/Btn/Help.gif' border=0/>什么是字段分组？</a>");

        //    this.Pub1.Add("<br>说明：利用字段分组就是把一个表单的字段分组显示它，用于表单数据字段较多的情况。");
        //    this.Pub1.AddFieldSetEnd();
        //}
    }

    //void btn_Click(object sender, EventArgs e)
    //{
    //    BP.Sys.EnCfg cfg = new EnCfg(this.MyPK);
    //    cfg.No = this.MyPK;
    //    cfg.GroupTitle = this.Pub1.GetTextBoxByID("TB_GrouTitle").Text;
    //    cfg.Save();
    //    this.Response.Redirect(this.PageID + ".aspx?MyPK=" + this.MyPK, true);
    //}


    public string GenerLab(MapAttr attr, int idx, int i, int count)
    {

        string lab = attr.Name;
        if (attr.MyDataType == DataType.AppBoolean && attr.UIIsLine)
            lab = "编辑";

        bool isLeft = true;
        if (i == 1)
        {
            isLeft = false;
        }

        if (attr.HisEditType == EditType.Edit || attr.HisEditType == EditType.UnDel)
        {
            switch (attr.LGType)
            {
                case FieldTypeS.Normal:
                    lab = "<a  href=\"javascript:Edit('" + this.MyPK + "','" + attr.OID + "','" + attr.MyDataType + "');\">" + lab + "</a>";
                    break;
                case FieldTypeS.FK:
                    lab = "<a  href=\"javascript:EditTable('" + this.MyPK + "','" + attr.OID + "','" + attr.MyDataType + "');\">" + lab + "</a>";
                    break;
                case FieldTypeS.Enum:
                    lab = "<a  href=\"javascript:EditEnum('" + this.MyPK + "','" + attr.OID + "','" + attr.MyDataType + "');\">" + lab + "</a>";
                    break;
                default:
                    break;
            }
        }
        else
        {
            lab = attr.Name;
        }

        if (idx == 0)
        {
            /*第一个。*/
            return lab + "<a href=\"javascript:Down('" + this.MyPK + "','" + attr.OID + "','1');\" ><img src='../../Images/Btn/Right.gif' alt='向右动顺序' border=0/></a>";
        }

        if (idx == count - 1)
        {
            /*到数第一个。*/
            return "<a href=\"javascript:Up('" + this.MyPK + "','" + attr.OID + "','1');\" ><img src='../../Images/Btn/Left.gif' alt='向左移动顺序' border=0/></a>" + lab;
        }
        return "<a href=\"javascript:Up('" + this.MyPK + "','" + attr.OID + "','1');\" ><img src='../../Images/Btn/Left.gif' alt='向下移动顺序' border=0/></a>" + lab + "<a href=\"javascript:Down('" + this.MyPK + "','" + attr.OID + "','1');\" ><img src='../../Images/Btn/Right.gif' alt='向右移动顺序' border=0/></a>";
    }
    public void InsertCellsData(MapData md, string groupID, int groupIdx)
    {
        this.Pub1.AddTR();
        this.Pub1.Add("<TD colspan=4  ID=" + groupID + groupIdx + " >");
        this.Pub1.Add("<table border=0  width=100%  >");
        MapAttrs attrs1 = md.GenerHisTableCells;
        this.Pub1.AddTR();
        foreach (MapAttr attr in attrs1)
        {
            if (attr.UIVisible == false)
                continue;


            this.Pub1.AddTDTitle("width='" + attr.UIWidth + "px'", "<a href=\"javascript:Edit('" + this.MyPK + "T','" + attr.OID + "','" + attr.MyDataType + "');\" >" + attr.Name + "</a>");
        }
        this.Pub1.AddTREnd();

        for (int y = 0; y < md.CellsY; y++)
        {
            this.Pub1.AddTR();
            int idx = 0;
            foreach (MapAttr attr in attrs1)
            {
                if (attr.UIVisible == false)
                    continue;



                switch (attr.LGType)
                {
                    case FieldTypeS.Normal:
                        TB tb = new TB();
                        tb.Attributes["width"] = "100%";
                        tb.Columns = 60;
                        tb.Enabled = attr.UIIsEnable;
                        switch (attr.MyDataType)
                        {
                            case BP.DA.DataType.AppString:

                                tb.ShowType = TBType.TB;
                                tb.Text = attr.DefVal;
                                this.Pub1.AddTD(tb);
                                break;
                            case BP.DA.DataType.AppDate:

                                tb.ShowType = TBType.Date;
                                tb.Text = attr.DefVal;
                                this.Pub1.AddTD(tb);
                                break;
                            case BP.DA.DataType.AppDateTime:

                                tb.ShowType = TBType.DateTime;
                                tb.Text = attr.DefVal;
                                this.Pub1.AddTD(tb);
                                break;
                            case BP.DA.DataType.AppBoolean:
                                CheckBox cb = new CheckBox();
                                cb.Text = attr.Name;
                                cb.Checked = attr.DefValOfBool;
                                cb.Enabled = attr.UIIsEnable;
                                this.Pub1.AddTD(cb);
                                break;
                            case BP.DA.DataType.AppDouble:
                            case BP.DA.DataType.AppFloat:
                            case BP.DA.DataType.AppInt:
                                tb.ShowType = TBType.Num;
                                tb.Text = attr.DefVal;
                                this.Pub1.AddTD("align=right",tb);
                                break;
                            case BP.DA.DataType.AppMoney:
                            case BP.DA.DataType.AppRate:
                                tb.ShowType = TBType.Moneny;
                                tb.Text = attr.DefVal;
                                this.Pub1.AddTD("align=right", tb);
                                break;
                            default:
                                break;
                        }

                        tb.Attributes["width"] = "100%";
                        switch (attr.MyDataType)
                        {
                            case BP.DA.DataType.AppString:
                                tb.Attributes["class"] = "TB";
                                break;
                            case BP.DA.DataType.AppDateTime:
                            case BP.DA.DataType.AppDate:
                                if (tb.Enabled)
                                    tb.Attributes["class"] = "TB";
                                else
                                    tb.Attributes["class"] = "TBReadonly";
                                break;
                            default:
                                if (tb.Enabled)
                                    tb.Attributes["class"] = "TBNum";
                                else
                                    tb.Attributes["class"] = "TBReadonlyNum";
                                break;
                        }
                        break;
                    case FieldTypeS.Enum:
                        DDL ddle = new DDL();
                        ddle.BindSysEnum(attr.KeyOfEn);
                        ddle.SetSelectItem(attr.DefVal);
                        ddle.Enabled = attr.UIIsEnable;
                        this.Pub1.AddTD(ddle);
                        break;
                    case FieldTypeS.FK:
                        DDL ddl1 = new DDL();
                        ddl1.ID = "s" + attr.KeyOfEn;
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
                        this.Pub1.AddTD(ddl1);
                        break;
                    default:
                        break;
                }
            }
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEnd();
        this.Pub1.AddTDEnd();
        this.Pub1.AddTREnd();
    }
}
