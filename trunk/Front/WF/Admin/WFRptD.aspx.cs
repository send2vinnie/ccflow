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
using BP.WF;
using BP.En;
using BP.Port;
using BP.Web.Controls;
using BP.Web;
using BP.Sys;

public partial class WF_Admin_RptD : WebPage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        switch (this.DoType)
        {
            case "AddF":
                this.BindAddF();
                return;
            default:
                break;
        }

        WFRpt rpt = new WFRpt(this.RefNo);
        this.Title = rpt.Name + this.ToE("DesignRpt", "设计报表"); // "设计报表";
        this.Ucsys1.AddTable();
        this.Ucsys1.AddCaptionLeftTX(rpt.Name + " -<a href=\"javascript:AddF('" + this.RefNo + "','0');\"  ><img src='../../Images/Btn/New.gif' border=0/>" + this.ToE("NewField", "新建字段") + "</a>- " + BP.WF.Glo.GenerHelp("WFRpt"));
        this.Ucsys1.AddTR();
        this.Ucsys1.AddTDTitle("colspan=3",  this.ToE("Order","排序") );
        this.Ucsys1.AddTDTitle(this.ToE("Node","节点"));
      //  this.Ucsys1.AddTDTitle("字段英文名称");
        this.Ucsys1.AddTDTitle( this.ToE("FieldName","字段名称") );

        this.Ucsys1.AddTDTitle(this.ToE("FieldEName","字段英文名称"));
        this.Ucsys1.AddTDTitle(this.ToE("FieldName","字段名称") ); //"报表中文名称"
        this.Ucsys1.AddTDTitle(this.ToE("Oper", "操作"));
        this.Ucsys1.AddTREnd();

        RptAttrs attrs = new RptAttrs(this.RefNo);
        if (attrs.Count == 0)
        {
            rpt.DoInitAttrs();
            attrs = new RptAttrs(this.RefNo);
        }

        int i = 0;
        foreach (RptAttr attr in attrs)
        {
            i++;
            this.Ucsys1.AddTRTX();
            this.Ucsys1.AddTDIdx(i);
            this.Ucsys1.AddTD("<a href=\"javascript:Up('" + attr.MyPK + "','" + attr.MyPK + "');\" ><img src='../../Images/Btn/Up.gif' border=0/></a>");
            this.Ucsys1.AddTD("<a href=\"javascript:Down('" + attr.MyPK + "','" + attr.MyPK + "');\" ><img src='../../Images/Btn/Down.gif' border=0/></a>");
            this.Ucsys1.AddTD(attr.FK_NodeT);

          //  this.Ucsys1.AddTD(attr.RefField);
            this.Ucsys1.AddTD(attr.RefFieldName);

            this.Ucsys1.AddTD(attr.Field);
            TB tb = new TB();
            tb.ID = "TB_" + attr.MyPK;
            tb.Text = attr.FieldName;
            // tb.Enabled = attr.IsCanEdit;
            this.Ucsys1.AddTD(tb);

            this.Ucsys1.Add("<TD class=TD>");
            if (attr.IsCanDel)
                this.Ucsys1.Add("<a href=\"javascript:Del('" + attr.MyPK + "')\" ><img src='../../Images/Btn/Delete.gif' border=0/>" + this.ToE("Del", "删除") + "</a>");


            this.Ucsys1.Add("<a href=\"javascript:AddF('" + this.RefNo + "','" + attr.IDX + "')\" ><img src='../../Images/Btn/New.gif' border=0/>" + this.ToE("Insert","插入") + "</a>");
            this.Ucsys1.Add("</TD>");
            this.Ucsys1.AddTREnd();
        }
        this.Ucsys1.AddTRSum();
        this.Ucsys1.Add("<TD colspan=9>");

        //this.Ucsys1.Add("&nbsp;");
        //btn = new Button();
        //btn.Click += new EventHandler(btn_GenerRpt_Click);
        //btn.Text = "保存字段修改";
        //this.Ucsys1.Add(btn);

        this.Ucsys1.Add("  <a href=\"javascript:WinOpen('../../Comm/PanelEns.aspx?EnsName=" + this.RefNo + "','spt');\" target=_self ><img src='../../Images/Btn/Table.gif' border=0/>" + this.ToE("Search", "查询") + "</a>");
        this.Ucsys1.Add("  <a href=\"javascript:WinOpen('../../Comm/GroupEnsMNum.aspx?EnsName=" + this.RefNo + "','spt');\" target=_self ><img src='../../Images/Pub/Group.gif' border=0/>" + this.ToE("GroupFX", "分组分析") + "</a>");
        this.Ucsys1.Add("  <a href=\"javascript:WinOpen('../../Comm/Contrast.aspx?EnsName=" + this.RefNo + "','spt');\" target=_self ><img src='../../Images/Btn/Table.gif' border=0/>" + this.ToE("DBFX","对比分析") + "</a>");
        this.Ucsys1.Add("  <a href=\"javascript:WinOpen('../../Comm/PanelEns.aspx?EnsName=" + this.RefNo + "','spt');\" target=_self ><img src='../../Images/Btn/Table.gif' border=0/>" + this.ToE("MRpt", "多纬报表") + "</a>");

        Button btn = new Button();
        btn.Click += new EventHandler(btn_GenerRpt_Click);
        btn.Text =this.ToE("Save","保存")  ; //"保存字段定义";
        this.Ucsys1.Add("&nbsp;");
        this.Ucsys1.Add(btn);

        btn = new Button();
        btn.OnClientClick = "WinOpen('../../Comm/MapDef/SearchAttrIdx.aspx?RefNo=" + this.RefNo + "&DoType=SearchAttr','ds'); return false;";
        btn.Text = this.ToE("DefSearchFK", "定义查询条件"); // "定义查询条件";
        this.Ucsys1.Add("&nbsp;");
        this.Ucsys1.Add(btn);

        this.Ucsys1.Add("</TD>");
        this.Ucsys1.AddTREnd();
        this.Ucsys1.AddTableEnd();
    }
    void btn_GenerRpt_Click(object sender, EventArgs e)
    {
        RptAttrs attrs = new RptAttrs(this.RefNo);
        string msg = "";
        foreach (RptAttr attr in attrs)
        {
            string fName = this.Ucsys1.GetTBByID("TB_" + attr.MyPK).Text.Trim();
            if (fName == "" || fName.Length == 1)
            {
                msg += "\t\n 字段（" + attr.FieldName + "）不能为空。";
                continue;
            }

            if (attr.FieldName != fName)
            {
                attr.FieldName = fName;
                attr.Update();
                MapAttr sysattr = new MapAttr(attr.FK_Rpt, attr.Field);
                sysattr.Name = fName;
                sysattr.Update();
            }
        }

        if (msg != "")
        {
            this.Alert("Error: \t\n " + msg);
        }
        this.Alert("Error: \t\n " + msg);

        WFRpt rpt = new WFRpt(this.RefNo);
        rpt.DoGenerView();
    }
    
    public void BindAddF()
    {
        WFRpt rpt = new WFRpt(this.RefNo);
        Flow fl = new Flow(rpt.FK_Flow);
        Nodes nds = new Nodes(rpt.FK_Flow);
        RptAttrs rptAttrs = new RptAttrs(this.RefNo);

        this.Ucsys1.AddTable();
        this.Ucsys1.AddCaptionLeftTX(this.ToE("FieldsChose", "为构造视图选择字段"));
        this.Ucsys1.AddTR();
        this.Ucsys1.AddTDTitle("IDX");
        this.Ucsys1.AddTDTitle( this.ToE("Node","节点") );
        this.Ucsys1.AddTDTitle(this.ToE("FieldEName" ,"字段英文名称"));
        this.Ucsys1.AddTDTitle(this.ToE("FieldName","字段名称"));
        this.Ucsys1.AddTDTitle(this.ToE("Oper", "操作"));
        this.Ucsys1.AddTREnd();

        int i = 0;
        int nodeid = 0;
        GECheckStand sc = new GECheckStand();
       BP.En.Attrs checkAttrs= sc.EnMap.Attrs;
       foreach (BP.WF.Node nd in nds)
       {
           BP.En.Attrs attrs = new BP.En.Attrs();
           if (nd.IsCheckNode)
           {
               attrs = checkAttrs;
           }
           else
           {
               attrs = nd.HisWork.EnMap.Attrs;
           }

           foreach (BP.En.Attr attr in attrs)
           {
               if (attr.IsRefAttr || attr.UIVisible == false)
                   continue;

               switch (attr.Key)
               {
                   case GECheckStandAttr.OID:
                   case GECheckStandAttr.NodeID:
                   case GECheckStandAttr.RDT:
                   case GECheckStandAttr.CDT:
                   case GECheckStandAttr.CheckState:
                   case GECheckStandAttr.NodeState:
                  // case GECheckStandAttr.TaxpayerName:
                   case GECheckStandAttr.Sender:
                   case GECheckStandAttr.Emps:
                   case GECheckStandAttr.RefMsg:
                   case "FK_NY":
                   case "MyNum":
                   case "FK_Dept":
                       continue;
                       break;
                   default:
                       break;
               }

               i++;
               if (nodeid == nd.NodeID)
                   this.Ucsys1.AddTR();
               else
                   this.Ucsys1.AddTRSum();

               this.Ucsys1.AddTDIdx(i);
               if (nodeid == nd.NodeID)
                   this.Ucsys1.AddTD();
               else
                   this.Ucsys1.AddTDB(nd.Name);

               nodeid = nd.NodeID;

               this.Ucsys1.AddTD(attr.Key);
               this.Ucsys1.AddTD(attr.Desc);

               string mypk = this.RefNo + "_" + nd.NodeID + "_" + attr.Key;
               CheckBox cb = new CheckBox();
               cb.ID = "CB_" + mypk;
            //   cb.Text = "选择";
               cb.Checked = rptAttrs.IsExits("MyPK", mypk);

               this.Ucsys1.AddTD(cb);
               this.Ucsys1.AddTREnd();
           }
       }

        Button btn =new Button();
        btn.ID="Btn_Save";
        btn.Text= this.ToE("Save","保存");
        btn.Click += new EventHandler(btn_Save_Click);

        this.Ucsys1.AddTRSum();
        this.Ucsys1.Add("<TD colspan=5 align=center>");

        this.Ucsys1.Add(btn);

        btn = new Button();
        btn.OnClientClick = "window.close();";
        btn.Text = this.ToE("Close","关闭");
        this.Ucsys1.Add(btn);
        this.Ucsys1.Add("</TD>");

        this.Ucsys1.AddTREnd();
        this.Ucsys1.AddTableEnd();
    }

    void btn_Save_Click(object sender, EventArgs e)
    {
        RptAttrs rptAttrs = new RptAttrs(this.RefNo);
        WFRpt rpt = new WFRpt(this.RefNo);
        Flow fl = new Flow(rpt.FK_Flow);
        Nodes nds = new Nodes(rpt.FK_Flow);
        GECheckStand sc = new GECheckStand();
        BP.En.Attrs checkAttrs = sc.EnMap.Attrs;
        foreach (BP.WF.Node nd in nds)
        {
            BP.En.Attrs attrs = new BP.En.Attrs();
            if (nd.IsCheckNode)
            {
                attrs = checkAttrs;
            }
            else
            {
                attrs = nd.HisWork.EnMap.Attrs;
            }

            foreach (BP.En.Attr attr in attrs)
            {
                if (attr.IsRefAttr || attr.UIVisible == false)
                    continue;

                switch (attr.Key)
                {
                    case GECheckStandAttr.OID:
                    case GECheckStandAttr.NodeID:
                    case GECheckStandAttr.RDT:
                    case GECheckStandAttr.CDT:
                    case GECheckStandAttr.CheckState:
                    case GECheckStandAttr.NodeState:
//                    case GECheckStandAttr.TaxpayerName:
                    case GECheckStandAttr.Sender:
                    case GECheckStandAttr.Emps:
                    case GECheckStandAttr.RefMsg:
                    case "FK_NY":
                    case "MyNum":
                        continue;
                        break;
                    default:
                        break;
                }


                string mypk = this.RefNo + "_" + nd.NodeID + "_" + attr.Key;
                CheckBox cb = this.Ucsys1.GetCBByID("CB_" + mypk);

                // 数据源中是否存在。
                bool isExitDB = rptAttrs.IsExits("MyPK", mypk);

                if (cb.Checked && isExitDB)
                {
                    /*如果选择了这个属性，并且存在里面。*/
                    continue;
                }

                if (cb.Checked == false && isExitDB == false)
                {
                    /*如果没有选择这个属性，并且也没有存在里面。*/
                    continue;
                }

                RptAttr attrNOfRpt = new RptAttr();
                attrNOfRpt.MyPK = mypk;
                if (cb.Checked && isExitDB == false)
                {
                    /* 如果选择了但是数据源中没有存在它，就要增加这个属性。*/
                    attrNOfRpt.MyPK = mypk;
                    attrNOfRpt.FK_Rpt = this.RefNo;
                    attrNOfRpt.FK_Node = nd.NodeID.ToString();

                    attrNOfRpt.RefTable = nd.HisWork.EnMap.PhysicsTable;


                    if (nd.No == "01")
                    {
                        attrNOfRpt.RefField = attr.Field;
                        attrNOfRpt.RefFieldName = attr.Desc;

                        attrNOfRpt.Field = attr.Field;
                        attrNOfRpt.FieldName = attr.Desc;
                    }
                    else
                    {
                        attrNOfRpt.RefField = attr.Field;
                        attrNOfRpt.RefFieldName = attr.Desc;

                        attrNOfRpt.Field = attr.Field + nd.No;
                        attrNOfRpt.FieldName = nd.Name + "." + attr.Desc;
                    }

                    attrNOfRpt.IsCanDel = true;
                    attrNOfRpt.IsCanEdit = true;
                    attrNOfRpt.Insert();

                    // 加入他的MapAttr .
                    MapAttr mattrN = new MapAttr();
                    mattrN.FK_MapData = this.RefNo;
                    mattrN.Name = attrNOfRpt.FieldName;
                    mattrN.KeyOfEn = attrNOfRpt.Field;

                    // mattrN.Key = attrN.Key;
                    if (nd.IsCheckNode)
                    {
                        mattrN.FK_MapData = this.RefNo;
                        mattrN.Name = attrNOfRpt.FieldName;
                        mattrN.KeyOfEn = attrNOfRpt.Field;
                        switch (attr.Key)
                        {
                            case "Rec": //如果是记录人。
                                mattrN.LGType = FieldTypeS.FK;
                                mattrN.UIBindKey = "BP.Port.Emps";
                                mattrN.UIContralType = UIContralType.DDL;
                                mattrN.InsertAsNew();
                                break;
                            default:
                                mattrN.MyDataType = BP.DA.DataType.AppString;
                                mattrN.MinLen = 0;
                                mattrN.MinLen = 3000;
                                mattrN.InsertAsNew();
                                break;
                        }
                    }
                    else
                    {
                        MapAttr mattrOfNode = new MapAttr("ND" + nd.NodeID, attr.Key);
                        mattrN.Copy(mattrOfNode);
                        mattrN.FK_MapData = this.RefNo;
                        mattrN.Name = attrNOfRpt.FieldName;
                        mattrN.KeyOfEn = attrNOfRpt.Field;
                        mattrN.InsertAsNew();
                    }
                    continue;
                }

                if (cb.Checked == false && isExitDB)
                {
                    /*如果没选择这个属性，并且存在里面。*/
                    attrNOfRpt.Delete();

                    MapAttr attrDel = new MapAttr();
                    attrDel.Delete(MapAttrAttr.KeyOfEn, attrNOfRpt.Field,
                        MapAttrAttr.FK_MapData, this.RefNo);
                    continue;
                }
            }
            // 结束循环属性。
        }
        // 结束循环节电。

        //删除数据。
        rpt.DoGenerView();
        this.WinClose();
    }
  
}
