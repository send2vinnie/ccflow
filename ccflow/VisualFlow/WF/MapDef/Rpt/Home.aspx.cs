using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.En;
using BP.Web;
using BP.DA;
using BP.Sys;

public partial class WF_MapDef_Rpt_Home : BP.Web.WebPage
{
    public string FK_Flow
    {
        get
        {
            string s= this.Request.QueryString["FK_Flow"];
            if (s == null)
                s ="007";
            return s;
        }
    }
    public int Idx
    {
        get
        {
            string s = this.Request.QueryString["Idx"];
            if (s == null)
                s = "0";
            return int.Parse(s);
        }
    }
    public string FK_MapData
    {
        get
        {
            string s = this.Request.QueryString["FK_MapData"];
            if (s == null)
                s = "ND7Rpt";
            return s;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "报表设计";
        Flow fl = new Flow(this.FK_Flow);
        this.BindLeft(fl);

        switch (this.Request.QueryString["ActionType"])
        {
            case null:
                break;
            case "Left":
                if (this.Idx == 0)
                    break;

                MapData md = new MapData(this.FK_MapData);
                AtPara ap = new AtPara(md.AttrsInTable);
                int i = 0;
                foreach (string s in ap.HisHT.Keys)
                {
                    i++;
                    //if (i==
                }
                break;
            case "Right":
                break;
            default:
                break;
        }

        switch (this.DoType)
        {
            case "ColumnsOrder":
                ColumnsOrder();
                break;
            case "SelectColumns":
                SelectColumns();
                break;
            case "SearchCond":
                this.SearchCond();
                break;
            default:
                this.BindHome();
                break;
        }
    }
    #region 显示顺序
    public void ColumnsOrder()
    {
        MapData md = new MapData(this.FK_MapData);
        AtPara ap = new AtPara(md.AttrsInTable);

        this.Pub2.AddTable();
        this.Pub2.AddCaptionLeft("移动箭头改变顺序");

        this.Pub2.AddTR();
        int idx = -1;
        foreach (string key in ap.HisHT.Keys)
        {
            idx++;
            this.Pub2.AddTDTitle("<a href=\"javascript:DoLeft('" + FK_Flow + "','" + FK_MapData + "','" + idx + "')\" ><img src='../../../Images/Arrowhead_Previous_S.gif' ></a>" + ap.GetValStrByKey(key) + "<a href=\"javascript:DoRight('" + FK_Flow + "','" + FK_MapData + "','" + idx + "')\" ><img src='../../../Images/Arrowhead_Next_S.gif' ></a>");
        }
        this.Pub2.AddTREnd();

        for (int i = 0; i < 12; i++)
        {
            this.Pub2.AddTR();
            foreach (string key in ap.HisHT.Keys)
                this.Pub2.AddTD();

            this.Pub2.AddTREnd();
        }
        this.Pub2.AddTableEnd();
    }
    void btn_ColumnsOrder_Click(object sender, EventArgs e)
    {
        MapData md = new MapData(this.FK_MapData);
        MapAttrs mattrs = new MapAttrs(md.No);
        string keys = "";
        foreach (MapAttr mattr in mattrs)
        {
            if (mattr.UIContralType != UIContralType.DDL)
                continue;

            CheckBox cb = this.Pub2.GetCBByID("CB_F_" + mattr.KeyOfEn);
            if (cb.Checked)
                keys += "@" + mattr.KeyOfEn;
        }
        md.SearchKeys = keys;
        md.Update();
        Cash.Map_Cash.Remove(this.FK_MapData);
        this.Alert("保存成功.");
    }
    #endregion

    #region 查询列表字段筛选
    public void SelectColumns()
    {
        this.Pub2.AddTable("width=90%");
        GroupFields gfs = new GroupFields(this.FK_Flow);
        MapAttrs mattrs = new MapAttrs(this.FK_MapData);
        MapData md = new MapData(this.FK_MapData);
        bool isBr = false;
        string attrInTable = md.AttrsInTable;
        foreach (GroupField gf in gfs)
        {
            this.Pub2.AddTR();
            this.Pub2.AddTDTitle(gf.Lab);
            this.Pub2.AddTREnd();

            this.Pub2.AddTR();
            this.Pub2.AddTDBigDocBegain();

            this.Pub2.AddTable("width=100% border=0");
              isBr = false;
            foreach (MapAttr attr in mattrs)
            {
                if (attr.GroupID != gf.OID)
                    continue;

                CheckBox cb = new CheckBox();
                cb.ID = "CB_" + attr.KeyOfEn;
                cb.Text = attr.Name;
                cb.Checked = attrInTable.Contains("@" + attr.KeyOfEn + "=");
                 

                if (isBr == false)
                    this.Pub2.AddTR();
                this.Pub2.AddTD(cb);
                if (isBr)
                    this.Pub2.AddTREnd();

                isBr = !isBr;
            }
            this.Pub2.AddTableEnd();


            this.Pub2.AddTDEnd();
            this.Pub2.AddTREnd();
        }
        this.Pub2.AddTR();
        this.Pub2.AddTDTitle("未分组");
        this.Pub2.AddTREnd();
        this.Pub2.AddTR();
        this.Pub2.AddTDBigDocBegain();

        this.Pub2.AddTable("width=100% border=0");
          isBr = false;
        foreach (MapAttr attr in mattrs)
        {
            if (gfs.Contains(attr.GroupID))
                continue;

            CheckBox cb = new CheckBox();
            cb.ID = "CB_" + attr.KeyOfEn;
            cb.Text = attr.Name;
            cb.Checked = attrInTable.Contains("@" + attr.KeyOfEn + "=");

            if (isBr == false)
                this.Pub2.AddTR();
            this.Pub2.AddTD(cb);
            if (isBr)
                this.Pub2.AddTREnd();

            isBr = !isBr;
        }
        this.Pub2.AddTableEnd();

        this.Pub2.AddTDEnd();
        this.Pub2.AddTREnd();


        this.Pub2.AddTRSum();
        this.Pub2.AddTDBegin();

        Button btn = new Button();
        btn.Text = " Save ";
        btn.Click += new EventHandler(btn_SelectColumns_Click);
        this.Pub2.Add(btn);

        this.Pub2.AddTDEnd();
        this.Pub2.AddTREnd();

        this.Pub2.AddTableEnd();
    }

    void btn_SelectColumns_Click(object sender, EventArgs e)
    {
        MapAttrs mattrs = new MapAttrs(this.FK_MapData);
        MapData md = new MapData(this.FK_MapData);
        string keys = "";
        foreach (MapAttr attr in mattrs)
        {
            CheckBox cb = this.Pub2.GetCBByID("CB_" + attr.KeyOfEn);
            if (cb == null)
                continue;
            if (cb.Checked == false)
                continue;
            keys += "@" + attr.KeyOfEn + "=" + attr.Name;
        }
        md.AttrsInTable = keys;
        md.DirectUpdate();
        this.Alert("保存成功.");
    }
    #endregion

    #region 查询条件定义
    public void SearchCond()
    {
        MapAttrs mattrs = new MapAttrs(this.FK_MapData);
        MapData md = new MapData(this.FK_MapData);

        #region 查询条件定义
        this.Pub2.AddFieldSet(this.ToE("WFRpt1r", "查询条件定义") + " - <a href=\"javascript:WinOpen('../Rpt/Search.aspx?FK_Flow=" + this.FK_Flow + "')\">" + this.ToE("WFRpt2r", "查询预览") + "</a>-<a href=\"javascript:WinOpen('../../../Comm/GroupEnsMNum.aspx?EnsName=" + this.MyPK + "')\">" + this.ToE("WFRpt3r", "分析预览") + "</a>");
        foreach (MapAttr mattr in mattrs)
        {
            if (mattr.UIContralType != UIContralType.DDL)
                continue;

            CheckBox cb = new CheckBox();
            cb.ID = "CB_F_" + mattr.KeyOfEn;
            if (md.SearchKeys.Contains("@" + mattr.KeyOfEn))
                cb.Checked = true;

            cb.Text = mattr.Name;
            this.Pub2.Add(cb);
            this.Pub2.AddBR();
        }

        this.Pub1.AddHR();
        Button btn = new Button();
        btn.Text = this.ToE("Save", "保存");
        btn.ID = "Btn_Save";
        btn.Click += new EventHandler(btn_SearchCond_Click);
        this.Pub2.Add(btn);
        this.Pub2.AddFieldSetEnd();
        #endregion
    }
    void btn_SearchCond_Click(object sender, EventArgs e)
    {
        MapData md = new MapData(this.FK_MapData);
        MapAttrs mattrs = new MapAttrs(md.No);
        string keys = "";
        foreach (MapAttr mattr in mattrs)
        {
            if (mattr.UIContralType != UIContralType.DDL)
                continue;

            CheckBox cb = this.Pub2.GetCBByID("CB_F_" + mattr.KeyOfEn);
            if (cb.Checked)
                keys += "@" + mattr.KeyOfEn;
        }
        md.SearchKeys = keys;
        md.Update();
        Cash.Map_Cash.Remove(this.FK_MapData);
        this.Alert("保存成功.");
    }
#endregion

    public void BindHome()
    {
        this.Pub2.AddH3("欢迎使用ccflow报表设计器.");
    }

    public void BindLeft(Flow fl)
    {
        this.Pub1.AddH2(fl.Name + " - 查询设计");
        this.Pub1.AddHR();

        this.Pub1.AddUL();

        this.Pub1.AddLi("<a href=\"Home.aspx?DoType=SelectColumns&FK_Flow=" + this.FK_Flow + "&FK_MapData=" + this.FK_MapData + "\"><b>查询列表字段筛选</b></a>");
        this.Pub1.Add("增加或移除查询结果集合中的列内容。<br><br>");

        this.Pub1.AddLi("<a href=\"Home.aspx?DoType=ColumnsOrder&FK_Flow=" + this.FK_Flow + "&FK_MapData=" + this.FK_MapData + "\"><b>列表字段显示顺序</b></a>");
        this.Pub1.Add("设置查询结果集合中的列的位置。<br><br>");

        this.Pub1.AddLi("<a href=\"Home.aspx?DoType=SearchCond&FK_Flow=" + this.FK_Flow + "&FK_MapData=" + this.FK_MapData + "\"><b>查询条件设计</b></a>");
        this.Pub1.Add("为查询功能设计查询条件。<br><br>");

        this.Pub1.AddLi("<a href=\"javascript:Card('" + this.FK_Flow + "','" + this.FK_MapData + "');\"><b>重设查询列表默认值</b></a>");
        this.Pub1.Add("按ccflow的默认值显示给用户。<br><br>");

        this.Pub1.AddLi("<a href=\"javascript:Card('" + this.FK_Flow + "','" + this.FK_MapData + "');\"><b>显示卡片</b></a>");
        this.Pub1.Add("对卡片显示出来的流程详细信息内容进行设计。<br><br>");

        this.Pub1.AddLi("<a href=\"javascript:View('" + this.FK_Flow + "','" + this.FK_MapData + "');\"><b>查询预览</b></a>");
        this.Pub1.Add("预览设计结果。<br><br>");

        this.Pub1.AddULEnd();
    }
}