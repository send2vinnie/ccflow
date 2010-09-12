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
using BP.WF;
using BP.Port;
using BP.Sys;
using BP.Port;
using BP.DA;
using BP.En;
using BP.Web;

public partial class WF_UC_FlowSearch : BP.Web.UC.UCBase3
{
    public string FK_Flow
    {
        get
        {
            string s = this.Request.QueryString["FK_Flow"];
            return s;
        }
    }
    public string FK_Emp
    {
        get
        {
            string s = this.Request.QueryString["FK_Emp"];
            if (s == null)
                return WebUser.No;
            return s;
        }
    }
    public int FK_Node
    {
        get
        {
            try
            {
                return int.Parse(this.Request.QueryString["FK_Node"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    public string DT_F
    {
        get
        {
            string f = this.Session["DF"] as string;
            if (f == null)
            {
                this.Session["DF"] = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                return this.Session["DF"].ToString();
            }
            return f;
        }
    }
    public string DT_T
    {
        get
        {
            string f = this.Session["DT"] as string;
            if (f == null)
            {
                this.Session["DT"] = DataType.CurrentData;
                return this.Session["DT"].ToString();
            }
            return f;
        }
    }

    public void BindSearch()
    {
        Node nd = new Node(this.FK_Node);
        Works wks = nd.HisWorks;
        QueryObject qo = new QueryObject(wks);
        qo.AddWhere(WorkAttr.Rec, WebUser.No);
        qo.addAnd();
        if (BP.SystemConfig.AppCenterDBType == DBType.Access)
            qo.AddWhere("Mid(RDT,1,10) >='" + this.DT_F + "' AND Mid(RDT,1,10) <='" + this.DT_T + "' ");
        else
            qo.AddWhere("substring(RDT,1,10) >='" + this.DT_F + "' AND substring(RDT,1,10) <='" + this.DT_T + "' ");

        this.Pub2.BindPageIdx(qo.GetCOUNT(), 10, this.PageIdx, "FlowSearch.aspx?FK_Node=" + this.FK_Node);
        qo.DoQuery("OID", 10, this.PageIdx);

        // 生成页面数据。
        Attrs attrs = nd.HisWork.EnMap.Attrs;
        int colspan = 2;
        foreach (Attr attr in attrs)
        {
            if (attr.UIVisible == false)
                continue;
            colspan++;
        }
        this.Pub1.AddTable("width='100%' align=center");
        this.Pub1.AddTR();
        this.Pub1.Add("<TD class=TitleTop colspan=" + colspan + "></TD>");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.Add("<TD class=TitleMsg  align=left colspan=" + colspan + "><img src='./Img/EmpWorks.gif' > <b><a href=FlowSearch.aspx >流程查询</a>-<a href='FlowSearch.aspx?FK_Flow=" + nd.FK_Flow + "'>" + nd.FlowName + "</a>-" + nd.Name + "</b></TD>");
        this.Pub1.AddTREnd();


        this.Pub1.AddTR();
        this.Pub1.Add("<TD colspan=" + colspan + " class=TD>发生日期从:");

        TextBox tb = new TextBox();
        tb.ID = "TB_F";
        tb.Columns = 7;
        tb.Text = this.DT_F;
        this.Pub1.Add(tb);

        this.Pub1.Add("到:");
        tb = new TextBox();
        tb.ID = "TB_T";
        tb.Text = this.DT_T;
        tb.Columns = 7;
        this.Pub1.Add(tb);

        Button btn = new Button();
        btn.Text = " 查询 ";
        btn.Click += new EventHandler(btn_Click);
        this.Pub1.Add(btn);
        this.Pub1.Add("</TD>");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("序");
        foreach (Attr attr in attrs)
        {
            if (attr.UIVisible == false)
                continue;
          
            this.Pub1.AddTDTitle(attr.Desc);
        }
        this.Pub1.AddTDTitle("操作");
        this.Pub1.AddTREnd();

        int idx = 0;
        bool is1 = false;
        foreach (Entity en in wks)
        {
            idx++;
            is1 = this.Pub1.AddTR(is1);
            this.Pub1.AddTD(idx);
            foreach (Attr attr in attrs)
            {
                if (attr.UIVisible == false)
                    continue;

               


                switch (attr.MyDataType)
                {
                    case DataType.AppBoolean:
                        this.Pub1.AddTD(en.GetValBoolStrByKey(attr.Key));
                        break;
                    case DataType.AppFloat:
                    case DataType.AppInt:
                    case DataType.AppDouble:
                        this.Pub1.AddTD(en.GetValFloatByKey(attr.Key));
                        break;
                    case DataType.AppMoney:
                        this.Pub1.AddTDMoney(en.GetValDecimalByKey(attr.Key));
                        break;
                    default:
                        this.Pub1.AddTD(en.GetValStrByKey(attr.Key));
                        break;
                }
            }
            this.Pub1.AddTD("<a href=\"WFRpt.aspx?WorkID=" + en.GetValIntByKey("OID") + "&FID=" + en.GetValByKey("FID") + "&FK_Flow=" + nd.FK_Flow + "\" target=bk >报告</a>");
            this.Pub1.AddTREnd();
        }

        this.Pub1.AddTRSum();
        this.Pub1.AddTD("");
        foreach (Attr attr in attrs)
        {
            if (attr.UIVisible == false)
                continue;

             

            switch (attr.MyDataType)
            {
                case DataType.AppFloat:
                case DataType.AppInt:
                case DataType.AppDouble:
                    this.Pub1.AddTDB(wks.GetSumDecimalByKey(attr.Key).ToString());
                    break;
                case DataType.AppMoney:
                    this.Pub1.AddTDB(wks.GetSumDecimalByKey(attr.Key).ToString("0.00"));
                    break;
                default:
                    this.Pub1.AddTD();
                    break;
            }
        }
        this.Pub1.AddTD();
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();
    }

    void btn_Click(object sender, EventArgs e)
    {
        this.Session["DF"] = this.Pub1.GetTextBoxByID("TB_F").Text;
        this.Session["DT"] = this.Pub1.GetTextBoxByID("TB_T").Text;

        this.Response.Redirect("FlowSearch.aspx?FK_Node=" + this.FK_Node, true);
    }
    public void BindFlowWap()
    {
        Flow fl = new Flow(this.FK_Flow);
        int colspan = 4;
        this.Pub1.AddTable("width='600px' ");
        this.Pub1.AddTR();
        this.Pub1.Add("<TD class=TitleTop colspan=" + colspan + "></TD>");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
       
        this.Pub1.Add("<TD class=TitleMsg colspan=" + colspan + " align=left><img src='./Img/Start.gif' > <b><a href='FlowSearch.aspx' >返回</a> - " + fl.Name + "</b></TD>");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTDTitle(this.ToE("Name", "步骤"));
        this.Pub1.AddTDTitle(this.ToE("Name", "节点"));
        this.Pub1.AddTDTitle(this.ToE("FlowPict", "可执行否?"));
        this.Pub1.AddTREnd();

        Nodes nds = new Nodes(this.FK_Flow);
        Stations sts = WebUser.HisStations;
        foreach (Node nd in nds)
        {
            bool isCan = false;
            foreach (Station st in sts)
            {
                if (nd.HisStas.Contains("@" + st.No))
                {
                    isCan = true;
                    break;
                }
            }

            this.Pub1.AddTR();
            this.Pub1.AddTDIdx(nd.Step);
            if (isCan)
                this.Pub1.AddTD("<a href='FlowSearch.aspx?FK_Node=" + nd.NodeID + "'>" + nd.Name + "</a>");
            else
                this.Pub1.AddTD(nd.Name);

            if (isCan)
            {
                this.Pub1.AddTD("可执行");
            }
            else
            {
                this.Pub1.AddTD("不可执行");
            }
            this.Pub1.AddTREnd();
        }

        this.Pub1.AddTRSum();
        this.Pub1.AddTD("colspan=" + colspan, "&nbsp;");
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();
    }
    public void BindFlow()
    {
        Flow fl = new Flow(this.FK_Flow);
        int colspan = 5;
        this.Pub1.AddTable("width='600px' ");
        this.Pub1.AddTR();
        this.Pub1.Add("<TD class=TitleTop colspan=" + colspan + "></TD>");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();

        this.Pub1.Add("<TD class=TitleMsg colspan=" + colspan + " align=left><img src='./Img/Start.gif' > <b><a href='FlowSearch.aspx' >返回</a> - " + fl.Name + "</b></TD>");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTDTitle(this.ToE("Name", "节点步骤"));
        this.Pub1.AddTDTitle(this.ToE("Name", "节点"));
        this.Pub1.AddTDTitle(this.ToE("FlowPict", "可执行否?"));
        this.Pub1.AddTDTitle(this.ToE("FlowPict", "操作"));
        this.Pub1.AddTREnd();

        Nodes nds = new Nodes(this.FK_Flow);
        Stations sts = WebUser.HisStations;
        foreach (Node nd in nds)
        {
            this.Pub1.AddTR();
            this.Pub1.AddTD(nd.Step);
            this.Pub1.AddTD(nd.Name);

            bool isCan = false;
            foreach (Station st in sts)
            {
                if (nd.HisStas.Contains("@" + st.No))
                {
                    isCan = true;
                    break;
                }
            }

            if (isCan)
            {
                this.Pub1.AddTD("可执行");
                this.Pub1.AddTD("<a href='FlowSearch.aspx?FK_Node=" + nd.NodeID + "'>查询</a>");
            }
            else
            {
                this.Pub1.AddTD("不可执行");
                this.Pub1.AddTD();
            }
            this.Pub1.AddTREnd();
        }

        this.Pub1.AddTRSum();
        this.Pub1.AddTD("colspan=" + colspan, "&nbsp;");
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.FK_Flow != null)
        {
            if (WebUser.IsWap)
                this.BindFlowWap();
            else
                this.BindFlow();
            return;
        }

        if (this.FK_Node != 0)
        {
            this.BindSearch();
            return;
        }

        int colspan = 5;
        this.Pub1.AddTable("width='600px'");
        this.Pub1.AddTR();
        this.Pub1.Add("<TD class=TitleTop colspan=" + colspan + "></TD>");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        if (WebUser.IsWap)
            this.Pub1.Add("<TD align=left class=TitleMsg colspan=" + colspan + "><img src='./Img/Home.gif' ><a href='Home.aspx' >Home</a> - <img src='./Img/Search.gif' > - " + this.ToE("FlowSearch", "流程查询") + " </TD>");
        else
            this.Pub1.Add("<TD class=TitleMsg colspan=" + colspan + " align=left ><img src='./Img/Search.gif' > <b>流程查询</b></TD>");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTDTitle(this.ToE("IDX", "序"));
        this.Pub1.AddTDTitle(this.ToE("FlowSort", "流程类别"));
        this.Pub1.AddTDTitle(this.ToE("Name", "名称"));

        if (WebUser.IsWap == false)
        {
            this.Pub1.AddTDTitle(this.ToE("FlowPict", "流程图"));
            this.Pub1.AddTDTitle(this.ToE("Desc", "描述"));
        }
        this.Pub1.AddTREnd();

        string sql = "SELECT FK_Flow FROM WF_Node ";
        Flows fls = new Flows();
        fls.RetrieveAll();
        int i = 0;
        bool is1 = false;
        string fk_sort = null;
        foreach (Flow fl in fls)
        {
            if (fl.HisFlowSheetType == FlowSheetType.DocFlow)
                continue;
            i++;
            is1 = this.Pub1.AddTR(is1);
            this.Pub1.AddTDIdx(i);
            if (fl.FK_FlowSort == fk_sort)
                this.Pub1.AddTD();
            else
                this.Pub1.AddTDB(fl.FK_FlowSortText);

            fk_sort = fl.FK_FlowSort;


            this.Pub1.AddTD("<a href='FlowSearch.aspx?FK_Flow=" + fl.No + "'>" + fl.Name + "</a>");
            if (WebUser.IsWap == false)
            {
                this.Pub1.AddTD("<a href=\"javascript:WinOpen('../Data/FlowDesc/" + fl.No + ".gif','sd');\"  >打开</a>");
                this.Pub1.AddTD(fl.Note);
            }
            this.Pub1.AddTREnd();
        }

        this.Pub1.AddTRSum();
        this.Pub1.AddTD("colspan=" + colspan, "&nbsp;");
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();
    }
}
