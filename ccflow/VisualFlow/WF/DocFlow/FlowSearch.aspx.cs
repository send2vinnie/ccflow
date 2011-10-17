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
using BP.Web;
using BP.En;
using BP.DA;
using BP.WF;
using BP.Sys;
using BP.Port;

public partial class GovDoc_FlowSearch : WebPage
{
    public string FK_Flow
    {
        get
        {
            string s = this.Request.QueryString["FK_Flow"];
            if (s == "")
                return null;
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
        qo.AddWhere("substring(RDT,1,10) >='" + this.DT_F + "' AND substring(RDT,1,10) <='" + this.DT_T + "' ");
        //if (nd.IsCheckNode)
        //{
        //    qo.addAnd();
        //    qo.AddWhere(CheckWorkAttr.NodeID, FK_Node);
        //}

        this.Pub1.BindPageIdx(qo.GetCount(), 10, this.PageIdx, "FlowSearch.aspx?FK_Node=" + this.FK_Node);
        qo.DoQuery("OID", 10, this.PageIdx);

        // 生成页面数据。
        Attrs attrs = nd.HisWork.EnMap.Attrs;
        int colspan = 3;
        foreach (Attr attr in attrs)
        {
            if (attr.UIVisible == false)
                continue;
            colspan++;
        }
        this.Pub2.AddTable("width='100%'");
        this.Pub2.AddTR();
        this.Pub2.Add("<TD class=TitleTop colspan=" + colspan + "></TD>");
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.Add("<TD class=TitleMsg colspan=" + colspan + "><img src='./Img/EmpWorks.gif' > <b>您的位置：<a href=FlowSearch.aspx >流程查询</a> =><a href='FlowSearch.aspx?FK_Flow=" + nd.FK_Flow + "'>" + nd.FlowName + "</a> => " + nd.Name + "</b></TD>");
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.Add("<TD colspan=" + colspan + " class=TD>发生日期从:");

        TextBox tb = new TextBox();
        tb.ID = "TB_F";
        tb.Columns = 7;
        tb.Text = this.DT_F;
        this.Pub2.Add(tb);

        this.Pub2.Add("到:");
        tb = new TextBox();
        tb.ID = "TB_T";
        tb.Text = this.DT_T;
        tb.Columns = 7;
        this.Pub2.Add(tb);

        Button btn = new Button();
        btn.Text = " 查询 ";
        btn.Click += new EventHandler(btn_Click);
        this.Pub2.Add(btn);
        this.Pub2.Add("</TD>");
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.AddTDTitle("序");
        foreach (Attr attr in attrs)
        {
            if (attr.UIVisible == false)
                continue;
            //if (attr.Key == CheckWorkAttr.RefMsg || attr.Key == CheckWorkAttr.CheckState)
            //    continue;

            this.Pub2.AddTDTitle(attr.Desc);
        }
        this.Pub2.AddTDTitle("colspan=2","操作");
        this.Pub2.AddTREnd();

        int idx = 0;
        bool is1 = false;
        foreach (Entity en in wks)
        {
            idx++;
            is1 = this.Pub2.AddTR(is1);
            this.Pub2.AddTD(idx);
            this.Pub2.AddTDA("DoClient.aspx?DoType=OpenFlow&FK_Flow=" + nd.FK_Flow + "&FK_Node=" + this.FK_Node + "&WorkID=" + en.GetValIntByKey("OID"), en.GetValStringByKey("Title"));
            foreach (Attr attr in attrs)
            {
                if (attr.Key == "Title")
                    continue;
                if (attr.UIVisible == false)
                    continue;
                //if (attr.Key == CheckWorkAttr.RefMsg || attr.Key == CheckWorkAttr.CheckState)
                //    continue;

                switch (attr.MyDataType)
                {
                    case DataType.AppBoolean:
                        this.Pub2.AddTD(en.GetValBoolStrByKey(attr.Key));
                        break;
                    case DataType.AppFloat:
                    case DataType.AppInt:
                    case DataType.AppDouble:
                        this.Pub2.AddTD(en.GetValFloatByKey(attr.Key));
                        break;
                    case DataType.AppMoney:
                        this.Pub2.AddTD(en.GetValDecimalByKey(attr.Key).ToString("0.00"));
                        break;
                    default:
                        this.Pub2.AddTD(en.GetValStrByKey(attr.Key));
                        break;
                }
            }
            this.Pub2.AddTD("<a href=\"../WF/WFRpt.aspx?WorkID=" + en.GetValIntByKey("OID") + "&FID=" + en.GetValByKey("FID") + "&FK_Flow=" + nd.FK_Flow + "\" target=bk >报告</a>-<a href=\"../WF/Chart.aspx?WorkID=" + en.GetValIntByKey("OID") + "&FID=" + en.GetValByKey("FID") + "&FK_Flow=" + nd.FK_Flow + "\" target=bk >轨迹</a>");
            this.Pub2.AddTREnd();
        }

        this.Pub2.AddTRSum();
        this.Pub2.AddTD("");
        foreach (Attr attr in attrs)
        {
            if (attr.UIVisible == false)
                continue;

            //if (attr.Key == CheckWorkAttr.RefMsg || attr.Key == CheckWorkAttr.CheckState)
            //    continue;

            switch (attr.MyDataType)
            {
                case DataType.AppFloat:
                case DataType.AppInt:
                case DataType.AppDouble:
                    this.Pub2.AddTDB(wks.GetSumDecimalByKey(attr.Key).ToString());
                    break;
                case DataType.AppMoney:
                    this.Pub2.AddTDB(wks.GetSumDecimalByKey(attr.Key).ToString("0.00"));
                    break;
                default:
                    this.Pub2.AddTD();
                    break;
            }
        }
        this.Pub2.AddTD();
        this.Pub2.AddTREnd();
        this.Pub2.AddTableEnd();
    }

    void btn_Click(object sender, EventArgs e)
    {
        this.Session["DF"] = this.Pub2.GetTextBoxByID("TB_F").Text;
        this.Session["DT"] = this.Pub2.GetTextBoxByID("TB_T").Text;

        this.Response.Redirect("FlowSearch.aspx?FK_Node=" + this.FK_Node, true);
    }
    public void BindFlow()
    {
        Flow fl = new Flow(this.FK_Flow);
        int colspan = 5;
        this.Pub2.AddTable("width='100%'");
        this.Pub2.AddTR();
        this.Pub2.Add("<TD class=TitleTop colspan=" + colspan + "></TD>");
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.Add("<TD class=TitleMsg colspan=" + colspan + "><img src='./Img/Start.gif' > <b><a href='FlowSearch.aspx' >返回流程列表</a>：流程查询=>" + fl.Name + "</b></TD>");
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.AddTDTitle(this.ToE("Name", "节点步骤"));
        this.Pub2.AddTDTitle(this.ToE("Name", "节点"));
        this.Pub2.AddTDTitle(this.ToE("FlowPict", "可执行否?"));
        this.Pub2.AddTDTitle(this.ToE("FlowPict", "操作"));
        this.Pub2.AddTREnd();

        Nodes nds = new Nodes(this.FK_Flow);
        Stations sts = WebUser.HisStations;
        foreach (Node nd in nds)
        {
            this.Pub2.AddTR();
            this.Pub2.AddTD(nd.Step);
            this.Pub2.AddTD(nd.Name);

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
                this.Pub2.AddTD("可执行");
                this.Pub2.AddTD("<a href='FlowSearch.aspx?FK_Node=" + nd.NodeID + "'>查询</a>");
            }
            else
            {
                this.Pub2.AddTD("不可执行");
                this.Pub2.AddTD();
            }
            this.Pub2.AddTREnd();
        }

        this.Pub2.AddTRSum();
        this.Pub2.AddTD("colspan=" + colspan, "&nbsp;");
        this.Pub2.AddTREnd();
        this.Pub2.AddTableEnd();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.FK_Flow != null)
        {
            this.BindFlow();
            return;
        }

        if (this.FK_Node != 0)
        {
            this.BindSearch();
            return;
        }

        int colspan = 5;
        this.Pub2.AddTable("width='100%'");
        this.Pub2.AddTR();
        this.Pub2.Add("<TD class=TitleTop colspan=" + colspan + "></TD>");
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.Add("<TD class=TitleMsg colspan=" + colspan + "><img src='./Img/Start.gif' > <b>点流程名称执行查询</b></TD>");
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.AddTDTitle(this.ToE("IDX", "序"));
        this.Pub2.AddTDTitle(this.ToE("FlowSort", "流程类别"));
        this.Pub2.AddTDTitle(this.ToE("Name", "名称"));
        this.Pub2.AddTDTitle(this.ToE("FlowPict", "流程图"));
        this.Pub2.AddTDTitle(this.ToE("Desc", "描述"));
        this.Pub2.AddTREnd();

        string sql = "SELECT FK_Flow FROM WF_Node ";
        Flows fls = new Flows();
        fls.RetrieveAll();
        int i = 0;
        bool is1 = false;
        string fk_sort = null;
        foreach (Flow fl in fls)
        {
            if (fl.HisFlowSheetType != FlowSheetType.DocFlow)
                continue;
            i++;
            is1 = this.Pub2.AddTR(is1);
            this.Pub2.AddTDIdx(i);
            if (fl.FK_FlowSort == fk_sort)
                this.Pub2.AddTD();
            else
                this.Pub2.AddTDB(fl.FK_FlowSortText);

            fk_sort = fl.FK_FlowSort;

            this.Pub2.AddTD("<a href='FlowSearch.aspx?FK_Flow=" + fl.No + "'>" + fl.Name + "</a>");

            this.Pub2.AddTD("<a href=\"javascript:WinOpen('../Data/FlowDesc/" + fl.No + ".gif','sd');\"  >打开</a>");
            this.Pub2.AddTD(fl.Note);
            this.Pub2.AddTREnd();
        }

        this.Pub2.AddTRSum();
        this.Pub2.AddTD("colspan=" + colspan, "&nbsp;");
        this.Pub2.AddTREnd();
        this.Pub2.AddTableEnd();

    }
}
