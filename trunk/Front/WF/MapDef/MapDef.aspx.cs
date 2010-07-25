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
                key = "ND1601";
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
    }
    public void InsertObjects()
    {
        foreach (GroupField gf in gfs)
        {
            if (rowIdx == gf.RowIdx && gf.IsUse == false)
            {
                gf.IsUse = true;
                currGF = gf;
                this.Pub1.AddTR();
                this.Pub1.AddTD("colspan=4 class=GroupField valign='top' style='height: 24px;' ", "<div style='text-align:left; float:left'><img src='./Style/Min.gif' alert='Min' id='Img" + gf.RowIdx + "' onclick=\"GroupBarClick('" + gf.RowIdx + "')\"  border=0 />&nbsp;<a href=\"javascript:GroupField('" + this.MyPK + "')\" >" + gf.Lab + "</a></div><div style='text-align:right; float:right'> <a href=\"javascript:GFDoUp('" + gf.OID + "')\" ><img src='../../Images/Btn/Up.gif' border=0/></a> <a href=\"javascript:GFDoDown('" + gf.OID + "')\" ><img src='../../Images/Btn/Down.gif' border=0/></a></div>");
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

                this.Pub1.AddTR(" ID='" + currGF.RowIdx + "_" + rowIdx + "1' ");
                this.Pub1.Add("<TD class=TD colspan=4 ><div style='text-align:left; float:left'>表格:<a href=\"javascript:EditDtl('" + this.MyPK + "','" + dtl.No + "')\" >" + dtl.Name + "</a></div><div style='text-align:right; float:right'><a href=\"javascript:document.getElementById('F" + dtl.No + "').contentWindow.AddF('" + dtl.No + "');\"><img src='../../Images/Btn/New.gif' border=0/>插入列</a><a href=\"javascript:document.getElementById('F" + dtl.No + "').contentWindow.CopyF('" + dtl.No + "');\"><img src='../../Images/Btn/Copy.gif' border=0/>复制列</a><a href=\"javascript:DtlDoUp('" + dtl.No + "')\" ><img src='../../Images/Btn/Up.gif' border=0/></a> <a href=\"javascript:DtlDoDown('" + dtl.No + "')\" ><img src='../../Images/Btn/Down.gif' border=0/></a></div></td>");
                this.Pub1.AddTREnd();

                this.Pub1.AddTR(" ID='" + currGF.RowIdx + "_" + rowIdx + "' ");
                this.Pub1.Add("<TD colspan=4 ID='TD" + dtl.No + "' height='50px' width='1000px'>");
                string src = "MapDtlDe.aspx?DoType=Edit&FK_MapData=" + this.MyPK + "&FK_MapDtl=" + dtl.No;
                this.Pub1.Add("<iframe ID='F" + dtl.No + "' style='padding:0px;border:0px;'  leftMargin='0'  topMargin='0' src='" + src + "' width='100%' height='10px'  scrolling=no  /></iframe>");
                this.Pub1.AddTDEnd();
                this.Pub1.AddTREnd();
            }
        }

    }
    #region varable.
    public GroupField currGF = new GroupField();
    private MapDtls _dtls;
    public MapDtls dtls
    {
        get
        {
            if (_dtls == null)
                _dtls = new MapDtls(this.MyPK);

            return _dtls;
        }
    }
    private GroupFields _gfs;
    public GroupFields gfs
    {
        get
        {
            if (_gfs == null)
                _gfs = new GroupFields(this.MyPK);

            return _gfs;
        }
    }
    public int rowIdx = 0;
    public bool isLeftNext = true;

    #endregion varable.

    /// <summary>
    /// 
    /// </summary>
    /// <param name="md"></param>
    public void BindAttrs(MapData md)
    {
        Attrs attrs = md.GenerHisMap().Attrs;
        MapAttrs mattrs = new MapAttrs(md.No);
        int count = mattrs.Count;
        //this.Pub1.AddB(this.ToE("DesignSheet", "设计表单") + " - <a href=\"javascript:AddF('" + this.MyPK + "');\" ><img src='../../Images/Btn/Add.gif' border=0/>" + this.ToE("NewField", "新建字段") + "</a> - <a href=\"javascript:AddTable('" + this.MyPK + "');\" ><img src='../../Images/Btn/Table.gif' border=0/>" + this.ToE("NewTable", "表格") + "</a> - <a href=\"CopyFieldFromNode.aspx?FK_Node=" + this.MyPK + "\" ><img src='../../Images/Btn/Add.gif' border=0/>" + this.ToE("NewField", "从节点复制字段") + "</a> - <a href=\"MapDtl.aspx?DoType=DtlList&FK_MapData=" + this.MyPK + "\" >" + this.ToE("DesignDtl", "设计明细") + "</a> - <a href=\"javascript:GroupField('" + md.No + "')\">字段分组</a><hr>");

        this.Pub1.AddB("&nbsp;&nbsp;" + this.ToE("DesignSheet", "设计表单") + " - <a href=\"javascript:AddF('" + this.MyPK + "');\" ><img src='../../Images/Btn/New.gif' border=0/>" + this.ToE("NewField", "新建字段") + "</a> - <a href=\"javascript:CopyFieldFromNode('" + this.MyPK + "');\" ><img src='../../Images/Btn/Copy.gif' border=0/>" + this.ToE("NewField", "复制字段") + "</a> - <a href=\"javascript:MapDtl('"+this.MyPK+"')\" >" + this.ToE("DesignDtl", "设计表格") + "</a> - <a href=\"javascript:GroupField('" + md.No + "')\">字段分组</a><hr>");
        
        int i = -1;
        int idx = -1;
        bool isHaveH = false;
        this.Pub1.Add("<Table class='Table' width='500px'  >");
        if (gfs.Count >= 1)
        {
            this.Pub1.AddTR();
            this.Pub1.AddTD("colspan=4 class=GroupField ", "<img src='./Style/Min.gif' alert='Min' id='Img101' onclick=\"GroupBarClick('101')\"  border=0 />&nbsp;<b>" + md.Name + "</b>");
            this.Pub1.AddTREnd();
            currGF.RowIdx = 101;
        }
        else
        {
            this.Pub1.Add("<TR>");
            this.Pub1.AddTD("colspan=4 class=GroupField ", "<b>" + md.Name + "</b>");
            this.Pub1.Add("</TR>");
        }

        isLeftNext = true;
        foreach (MapAttr attr in mattrs)
        {
            if (attr.HisAttr.IsRefAttr || attr.UIVisible == false )
                continue;

            if (isLeftNext == true)
                this.InsertObjects();

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
                rowIdx++;

                this.Pub1.AddTR(" ID='" + currGF.RowIdx + "_" + rowIdx + "' ");
                this.Pub1.Add("<TD class=FDesc colspan=4 width='100%' >");
                this.Pub1.Add(this.GenerLab(attr, idx, 0, count) + "<br>");
                TextBox mytbLine = new TextBox();
                mytbLine.TextMode = TextBoxMode.MultiLine;
                mytbLine.Rows = 8;
                mytbLine.Columns = 80;
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
                {
                    this.Pub1.AddTR(" ID='" + currGF.RowIdx + "_" + rowIdx + "' ");
                }

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
                this.InsertObjects();
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

                            if (attr.UIIsEnable )
                                tb.Attributes["onfocus"] = "calendar();";

                            this.Pub1.AddTD("colspan=" + colspanOfCtl, tb);
                            break;
                        case BP.DA.DataType.AppDateTime:
                            this.Pub1.AddTDDesc(this.GenerLab(attr, idx, i, count));
                            tb.ShowType = TBType.DateTime;
                            tb.Text = attr.DefVal;
                            if (attr.UIIsEnable == false)
                                tb.Attributes["onfocus"] = "calendar();";


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

        // 最后处理补充上它。
        if (isLeftNext == false)
        {
            this.Pub1.AddTD();
            this.Pub1.AddTD();
            this.Pub1.AddTREnd();
            rowIdx++;
        }

        #region 补充上放在最后的几个。
        foreach (GroupField gf in gfs)
        {
            if (rowIdx <= gf.RowIdx && gf.IsUse == false)
            {
                if (gf.RowIdx > rowIdx)
                {
                    gf.RowIdx = rowIdx;
                    gf.Update();
                }
                currGF = gf;
                this.Pub1.AddTR();
                this.Pub1.AddTD("colspan=4 class=GroupField ", "<div style='text-align:left; float:left'><img src='./Style/Max.gif' alert='Min' id='Img" + gf.RowIdx + "'  border=0 />&nbsp;<a href=\"javascript:GroupField('" + md.No + "')\">" + gf.Lab + "</a></div><div style='text-align:right; float:right'> <a href=\"javascript:GFDoUp('" + gf.OID + "')\" ><img src='../../Images/Btn/Up.gif' border=0/></a> </div>");
                this.Pub1.AddTREnd();
                rowIdx++;
            }
        }
        i = 0;
        foreach (MapDtl dtl in dtls)
        {
            i++;
            if (rowIdx <= dtl.RowIdx && dtl.IsUse == false)
            {
                if (dtl.RowIdx > rowIdx)
                {
                    dtl.RowIdx = rowIdx;
                    dtl.Update();
                }

                this.Pub1.AddTR(" ID='" + currGF.RowIdx + "_" + i + rowIdx + "' ");
                this.Pub1.Add("<TD class='TD' colspan=4   > <div style='text-align:left; float:left'>表格:<a href=\"javascript:EditDtl('" + this.MyPK + "','" + dtl.No + "')\" >" + dtl.Name + "</a></div><div style='text-align:right; float:right'><a href=\"javascript:document.getElementById('F" + dtl.No + "').contentWindow.AddF('" + dtl.No + "');\"><img src='../../Images/Btn/New.gif' border=0/>插入列</a><a href=\"javascript:document.getElementById('F" + dtl.No + "').contentWindow.CopyF('" + dtl.No + "');\"><img src='../../Images/Btn/Copy.gif' border=0/>复制列</a><a href=\"javascript:DtlDoUp('" + dtl.No + "')\" ><img src='../../Images/Btn/Up.gif' border=0/></a></div></td>");

                // this.Pub1.AddTD("colspan=4", "<div style='text-align:left; float:left'>表格:<a href=\"javascript:EditDtl('" + md.No + "','" + dtl.No + "')\" >" + dtl.Name + "</a></div><div style='text-align:right; float:right'><a href=\"javascript:DtlDoUp('" + dtl.No + "')\" ><img src='../../Images/Btn/Up.gif' border=0/></a> <a href=\"javascript:DtlDoDown('" + dtl.No + "')\" ><img src='../../Images/Btn/Down.gif' border=0/></a></div>");
                this.Pub1.AddTREnd();

                this.Pub1.AddTR(" ID='" + currGF.RowIdx + "_" + rowIdx + "' ");
                this.Pub1.Add("<TD colspan=4 ID='TD" + dtl.No + "' height='50px' >");
                string src = "MapDtlDe.aspx?DoType=Edit&FK_MapData=" + this.MyPK + "&FK_MapDtl=" + dtl.No;
                this.Pub1.Add("<iframe ID='F" + dtl.No + "' style='padding:0px;border:0px;'  leftMargin='0'  topMargin='0' src='" + src + "' scrolling=no  /></iframe>");
                this.Pub1.AddTDEnd();
                this.Pub1.AddTREnd();
            }
        }

        #endregion 补充上放在最后的几个。

        this.Pub1.AddTableEnd();


        #region 处理iFrom 的自适应的问题。
        string js = "\t\n<script type='text/javascript' >";
        foreach (MapDtl dtl in dtls)
        {
            js += "\t\n window.setInterval(\"ReinitIframe('" + dtl.No + "')\", 200);";
        }

        js += "\t\n</script>";
        this.Pub1.Add(js);
        #endregion 处理iFrom 的自适应的问题。



        #region 处理隐藏字段。
        //string SysFields = "OID,FID,FK_NY,Emps,FK_Dept,NodeState,WFState,BillNo,RDT,MyNum,WFLog,";
        string msg = ""; // +++++++ 编辑隐藏字段 +++++++++ <br>";
        foreach (MapAttr attr in mattrs)
        {
            if (attr.UIVisible)
                continue;
            switch (attr.KeyOfEn)
            {
                case "OID":
                case "FID":
                case "FK_NY":
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

        if (msg.Length > 10)
        {
            this.Pub1.AddFieldSet("编辑隐藏字段");
            this.Pub1.Add(msg);
            this.Pub1.Add("<br>说明：隐藏字段是不显示在表单里面，多用于属性的计算、方向条件的设置，报表的体现。");
            this.Pub1.AddFieldSetEnd();
        }
        #endregion 处理隐藏字段。

    }

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
}
