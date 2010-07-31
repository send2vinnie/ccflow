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
using BP.DA;
using BP.En;
using BP.En;
using BP.Web;
using BP.Web.UC;

public partial class Comm_MapDef_Do : BP.Web.PageBase
{
    public string DoType
    {
        get
        {
            return this.Request.QueryString["DoType"];
        }
    }
    public string IDX
    {
        get
        {
            return this.Request.QueryString["IDX"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        switch (this.DoType)
        {
            case "AddEnum":
                SysEnumMain sem1 = new SysEnumMain(this.Request.QueryString["EnumKey"]);
                MapAttr attrAdd = new MapAttr();
                attrAdd.KeyOfEn = sem1.No;
                if (attrAdd.IsExit(MapAttrAttr.FK_MapData, this.MyPK, MapAttrAttr.KeyOfEn, sem1.No))
                {
                    BP.PubClass.Alert(ToE("FExits", "字段已经存在") + " [" + sem1.No + "]。");
                    BP.PubClass.WinClose();
                    return;
                }

                attrAdd.FK_MapData = this.MyPK;
                attrAdd.Name = sem1.Name;
                attrAdd.UIContralType = UIContralType.DDL;
                attrAdd.UIBindKey = sem1.No;
                attrAdd.MyDataType = BP.DA.DataType.AppInt;
                attrAdd.LGType = FieldTypeS.Enum;
                attrAdd.DefVal = "0";
                attrAdd.UIIsEnable = true;
                if (this.IDX == null || this.IDX == "")
                {
                    MapAttrs attrs1 = new MapAttrs(this.MyPK);
                    attrAdd.IDX = 0;
                }
                else
                {
                    attrAdd.IDX = int.Parse(this.IDX);
                }
                attrAdd.Insert();
                this.Response.Redirect("EditEnum.aspx?MyPK=" + this.MyPK + "&RefOID=" + attrAdd.OID, true);
                this.WinClose();
                return;
            case "DelEnum":
                string eKey = this.Request.QueryString["EnumKey"];
                SysEnumMain sem = new SysEnumMain();
                sem.No = eKey;
                sem.Delete();
                this.WinClose();
                return;
            case "AddSysEnum":
                this.AddFEnum();
                break;
            case "AddSFTable":
                this.AddSFTable();
                break;
            case "AddSFTableAttr":
                SFTable sf = new SFTable(this.Request.QueryString["RefNo"]);
                MapAttr attrAddFK = new MapAttr();
                attrAddFK.KeyOfEn = sf.FK_Val;
                if (attrAddFK.IsExit(MapAttrAttr.FK_MapData, this.MyPK, MapAttrAttr.KeyOfEn, sf.FK_Val))
                {
                    BP.PubClass.Alert(this.ToE("FExits", "字段已经存在") + " [" + sf.FK_Val + "]。");
                    BP.PubClass.WinClose();
                    return;
                }
                attrAddFK.FK_MapData = this.MyPK;
                attrAddFK.Name = sf.Name;
                attrAddFK.UIContralType = UIContralType.DDL;
                attrAddFK.UIBindKey = sf.No;
                attrAddFK.MyDataType = BP.DA.DataType.AppString;
                attrAddFK.LGType = FieldTypeS.FK;
                attrAddFK.DefVal = "";
                attrAddFK.UIIsEnable = true;
                if (this.IDX == null || this.IDX == "")
                {
                    MapAttrs attrs1 = new MapAttrs(this.MyPK);
                    attrAddFK.IDX = 0;
                }
                else
                {
                    attrAddFK.IDX = int.Parse(this.IDX);
                }
                attrAddFK.Insert();
                this.Response.Redirect("EditTable.aspx?MyPK=" + this.MyPK + "&RefOID=" + attrAddFK.OID, true);
                this.WinClose();
                return;
            case "AddF":
            case "ChoseFType":
                this.AddF();
                break;
            case "Up":
                MapAttr attrU = new MapAttr(this.RefOID);
                attrU.DoUp();
                this.WinClose();
                break;
            case "Down":
                MapAttr attrD = new MapAttr(this.RefOID);
                attrD.DoDown();
                this.WinClose();
                break;
            case "Jump":
                MapAttr attrFrom = new MapAttr( int.Parse( this.Request.QueryString["FromID"] ) );
                MapAttr attrTo = new MapAttr( int.Parse(this.Request.QueryString["ToID"]));
                attrFrom.DoJump(attrTo);
                this.WinClose();
                break;
            case "MoveTo":
                MapAttr attrM = new MapAttr();
                attrM.OID = int.Parse(this.Request.QueryString["FromID"]);
                attrM.Update(MapAttrAttr.GroupID, int.Parse(this.Request.QueryString["ToGFID"]));
                this.WinClose();
                break;
            case "Edit":
                Edit();
                break;
            case "Del":
                MapAttr attr = new MapAttr();
                attr.OID = this.RefOID;
                attr.Delete();
                this.WinClose();
                break;
            case "GFDoUp":
                GroupField gf = new GroupField(this.RefOID);
                gf.DoOrder();
                gf.Retrieve();
                if (gf.Idx == 0)
                {
                    this.WinClose();
                    return;
                }
                int oidIdx = gf.Idx;
                gf.Idx = gf.Idx -1;
                GroupField gfUp = new GroupField();
                if (gfUp.Retrieve(GroupFieldAttr.EnName, gf.EnName, GroupFieldAttr.Idx, gf.Idx) == 1)
                {
                    gfUp.Idx = oidIdx;
                    gfUp.Update();
                }
                gf.Update();
                this.WinClose();
                break;
            case "GFDoDown":
                GroupField mygf = new GroupField(this.RefOID);
                mygf.DoOrder();
                mygf.Retrieve();

                int oidIdx1 = mygf.Idx;
                mygf.Idx = mygf.Idx + 1;
                GroupField gfDown = new GroupField();
                if (gfDown.Retrieve(GroupFieldAttr.EnName, mygf.EnName, GroupFieldAttr.Idx, mygf.Idx) == 1)
                {
                    gfDown.Idx = oidIdx1;
                    gfDown.Update();
                }
                mygf.Update();
                this.WinClose();
                break;
            case "DtlDoUp":
                MapDtl dtl1 = new MapDtl(this.MyPK);
                if (dtl1.RowIdx > 0)
                {
                    dtl1.RowIdx = dtl1.RowIdx - 1;
                    dtl1.Update();
                }
                this.WinClose();
                break;
            case "DtlDoDown":
                MapDtl dtl2 = new MapDtl(this.MyPK);
                if (dtl2.RowIdx < 10)
                {
                    dtl2.RowIdx = dtl2.RowIdx + 1;
                    dtl2.Update();
                }
                this.WinClose();
                break;
            default:
                break;
        }
    }
    public void Edit()
    {
        MapAttr attr = new MapAttr(this.RefOID);
        switch (attr.MyDataType)
        {
            case BP.DA.DataType.AppString:
                //  this.Response.Redirect("EditF.aspx?RefOID="+this
                break;
            default:
                break;
        }
    }
    public string GroupField
    {
        get
        {
            return this.Request.QueryString["GroupField"];
        }
    }
    public void AddF()
    {
        this.Title = this.ToE("GuideNewField", "增加新字段向导");
     //   this.Pub1.AddH4(this.Title);


        this.Pub1.AddFieldSet(this.ToE("FType1", "新增普通字段"));
        this.Pub1.AddUL();
        this.Pub1.AddLi("<a href='EditF.aspx?DoType=Add&MyPK=" + this.MyPK + "&FType=" + BP.DA.DataType.AppString + "&IDX=" + this.IDX + "&GroupField="+this.GroupField+"'>" + this.ToE("TString", "字符型") + "</a> - <font color=Note>" + this.ToE("TStringD", "如:姓名、地址、邮编、电话") + "</font>");
        this.Pub1.AddLi("<a href='EditF.aspx?DoType=Add&MyPK=" + this.MyPK + "&FType=" + BP.DA.DataType.AppInt + "&IDX=" + this.IDX + "&GroupField=" + this.GroupField + "'>" + this.ToE("TInt", "整数型") + "</a> - <font color=Note>" + this.ToE("TIntD", "如:年龄、个数。") + "</font>");
        this.Pub1.AddLi("<a href='EditF.aspx?DoType=Add&MyPK=" + this.MyPK + "&FType=" + BP.DA.DataType.AppMoney + "&IDX=" + this.IDX + "&GroupField=" + this.GroupField + "'>" + this.ToE("TMoney", "金额型") + "</a> - <font color=Note>" + this.ToE("TMoneyD", "如:单价、薪水。") + "</font>");
        this.Pub1.AddLi("<a href='EditF.aspx?DoType=Add&MyPK=" + this.MyPK + "&FType=" + BP.DA.DataType.AppFloat + "&IDX=" + this.IDX + "&GroupField=" + this.GroupField + "'>" + this.ToE("TFloat", "浮点型") + "</a> - <font color=Note>" + this.ToE("TFloatD", "如：身高、体重、长度。") + "</font>");
        this.Pub1.AddLi("<a href='EditF.aspx?DoType=Add&MyPK=" + this.MyPK + "&FType=" + BP.DA.DataType.AppDate + "&IDX=" + this.IDX + "&GroupField=" + this.GroupField + "'>" + this.ToE("TDate", "日期型") + "</a> - <font color=Note>" + this.ToE("TDateD", "如：出生日期、发生日期。") + "</font>");
        this.Pub1.AddLi("<a href='EditF.aspx?DoType=Add&MyPK=" + this.MyPK + "&FType=" + BP.DA.DataType.AppDateTime + "&IDX=" + this.IDX + "&GroupField=" + this.GroupField + "'>" + this.ToE("TDateTime", "日期时间型") + "</a> - <font color=Note>" + this.ToE("TDateTimeD", "如：发生日期时间") + "</font>");
        this.Pub1.AddLi("<a href='EditF.aspx?DoType=Add&MyPK=" + this.MyPK + "&FType=" + BP.DA.DataType.AppBoolean + "&IDX=" + this.IDX + "&GroupField=" + this.GroupField + "'>" + this.ToE("TBool", "Boole型(是/否)") + "</a> - <font color=Note>" + this.ToE("TBoolD", "如：是否完成、是否达标") + "</font>");
        this.Pub1.AddULEnd();
        this.Pub1.AddFieldSetEnd();


        this.Pub1.AddFieldSet(this.ToE("FType2", "新增枚举字段(用来表示，状态、类型...的数据。)"));
        this.Pub1.AddUL();
        this.Pub1.AddLi("<a href='Do.aspx?DoType=AddSysEnum&MyPK=" + this.MyPK + "&IDX=" + this.IDX + "&GroupField=" + this.GroupField + "'>" + this.ToE("TEnum", "枚举型") + "</a> - " + this.ToE("TEnumD", "比如：性别:男/女。请假类型：事假/病假/婚假/产假/其它。"));
        this.Pub1.AddULEnd();
        this.Pub1.AddFieldSetEnd();


        this.Pub1.AddFieldSet(this.ToE("FType3", "新增外键字段(字典表，通常只有编号名称两个列)"));
        this.Pub1.AddUL();
        this.Pub1.AddLi("<a href='Do.aspx?DoType=AddSFTable&MyPK=" + this.MyPK + "&FType=Class&IDX=" + this.IDX + "&GroupField=" + this.GroupField + "'>" + this.ToE("TFK", "外键型") + "</a> - " + this.ToE("TFKD", "比如：岗位、税种、行业、经济性质。"));
        this.Pub1.AddULEnd();
        this.Pub1.AddFieldSetEnd();
    }
    public void AddFEnum()
    {
        this.Title = this.ToE("GuideNewField", "增加新字段向导");

        this.Pub1.AddFieldSet("<a href='Do.aspx?DoType=AddF&MyPK=" + this.MyPK + "&IDX=" + this.IDX + "'>" + this.ToE("GuideNewField", "增加新字段向导") + "</a> - <a href='SysEnum.aspx?DoType=New&MyPK=" + this.MyPK + "&IDX=" + this.IDX + "' >" + this.ToE("NewEnum", "新建枚举") + "</a>");

        this.Pub1.AddTable();
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("IDX");
        this.Pub1.AddTDTitle(this.ToE("No", "编号") + "(" + this.ToE("ClickToAdd", "点击增加到表单") + ")");
        this.Pub1.AddTDTitle(this.ToE("Name", "名称"));
        this.Pub1.AddTDTitle(this.ToE("Oper", "操作"));
        this.Pub1.AddTDTitle();
        this.Pub1.AddTREnd();

        BP.Sys.SysEnumMains sems = new SysEnumMains();
        sems.RetrieveAll();
        bool is1 = false;
        int idx = 0;
        foreach (BP.Sys.SysEnumMain sem in sems)
        {
            BP.Web.Controls.DDL ddl = null;
            try
            {
                ddl = new BP.Web.Controls.DDL();
                ddl.BindSysEnum(sem.No);
            }
            catch
            {
                continue;
            }

            idx++;
            is1 = this.Pub1.AddTR(is1);
            this.Pub1.AddTDIdx(idx);
            this.Pub1.AddTD("<a  href=\"javascript:AddEnum('" + this.MyPK + "','" + this.IDX + "','" + sem.No + "')\" >" + sem.No + "</a>");
            this.Pub1.AddTD(sem.Name);
            this.Pub1.AddTD("[<a href='SysEnum.aspx?DoType=Edit&MyPK=" + this.MyPK + "&IDX=" + this.IDX + "&RefNo=" + sem.No + "' >" + this.ToE("Edit", "编辑") + "</a>]");
            this.Pub1.AddTD(ddl);
            this.Pub1.AddTREnd();
        }

        this.Pub1.AddTableEnd();
        this.Pub1.AddFieldSetEnd();  
    }

    public void AddSFTable()
    {
        this.Title = this.ToE("GuideNewField", "增加新字段向导");

        this.Pub1.AddFieldSet("<a href='Do.aspx?DoType=AddF&MyPK=" + this.MyPK + "&IDX=" + this.IDX + "'>" + this.ToE("GuideNewField", "增加新字段向导") + "</a> - " + this.ToE("NewFK", "增加外键字段") + " - <a href='SFTable.aspx?DoType=New&MyPK=" + this.MyPK + "&IDX=" + this.IDX + "' >" + this.ToE("NewTable", "新建表") + "</a>");

        this.Pub1.AddTable();
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("IDX");
        this.Pub1.AddTDTitle(this.ToE("No", "编号") + "(" + this.ToE("ClickToAdd", "点击增加到表单") + ")");
        this.Pub1.AddTDTitle(this.ToE("Name","名称"));
        this.Pub1.AddTDTitle(this.ToE("Sort", "类别"));
        this.Pub1.AddTDTitle( this.ToE("Desc","描述") );
        this.Pub1.AddTREnd();

        BP.Sys.SFTables ens = new SFTables();
        ens.RetrieveAllFromDBSource();
        bool is1 = false;
        int idx = 0;
        foreach (BP.Sys.SFTable sem in ens)
        {
            idx++;
            //is1 = this.Pub1.AddTR(is1);
            is1=this.Pub1.AddTR(is1);
            this.Pub1.AddTDIdx(idx);
            this.Pub1.AddTD("<a  href=\"javascript:AddSFTable('" + this.MyPK + "','" + this.IDX + "','" + sem.No + "')\" >" + sem.No + "</a>");
            this.Pub1.AddTD(sem.Name);
          
            this.Pub1.AddTD(sem.SFTableTypeT);

            if (sem.IsClass)
                this.Pub1.AddTD("<a href=\"javascript:WinOpen('../../Comm/PanelEns.aspx?EnsName=" + sem.No + "','sg')\"  ><img src='../../Images/Btn/Edit.gif' border=0/>" + sem.TableDesc + "</a>");
            else
                this.Pub1.AddTD("<a href=\"javascript:WinOpen('SFTable.aspx?DoType=Edit&MyPK=" + this.MyPK + "&IDX=" + this.IDX + "&RefNo=" + sem.No + "','sg')\"  ><img src='../../Images/Btn/Edit.gif' border=0/>" + sem.TableDesc + "</a>");

            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEnd();


        this.Pub1.AddFieldSetEnd();  

    }
}
