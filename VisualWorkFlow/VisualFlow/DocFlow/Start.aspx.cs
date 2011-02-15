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

public partial class GovDoc_Start : WebPage
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        this.Title = "工作发起";
        if (this.Session["FK_Flow"] != null)
        {
            this.WinOpen("MyFlow.aspx?FK_Flow=" + this.Session["FK_Flow"].ToString());
            this.Session["FK_Flow"] = null;
        }
        this.Bind();
    }
    /// <summary>
    /// Bind
    /// </summary>
    public void Bind()
    {
        int colspan = 5;
        this.Pub1.AddTable("width='100%' align=center");
        this.Pub1.AddTR();
        this.Pub1.Add("<TD class=TitleTop colspan=" + colspan + "></TD>");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.Add("<TD class=TitleMsg colspan=" + colspan + "><img src='./Img/Start.gif' > <b>" + this.ToE("Start", "公文拟定") + "</b></TD>");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTDTitle(this.ToE("IDX", "序"));
        this.Pub1.AddTDTitle(this.ToE("FlowSort", "流程类别"));
        this.Pub1.AddTDTitle(this.ToE("Name", "名称"));
        this.Pub1.AddTDTitle(this.ToE("FlowPict", "流程图"));
        this.Pub1.AddTDTitle(this.ToE("Desc", "描述"));
        this.Pub1.AddTREnd();

        string sql = "SELECT DISTINCT FK_Flow FROM WF_Node WHERE NODEID IN (  SELECT FK_NODE FROM WF_NODESTATION WHERE FK_STATION IN (SELECT FK_STATION FROM PORT_EMPSTATION WHERE FK_EMP='" + WebUser.No + "')  ) ";
        Flows fls = new Flows();
        BP.En.QueryObject qo = new BP.En.QueryObject(fls);
        qo.AddWhereInSQL("No", sql);
        qo.addAnd();
        qo.AddWhere(FlowAttr.IsOK, true);
        qo.addOrderBy("FK_FlowSort", "No");
        qo.DoQuery();
        int i = 0;
        bool is1 = false;
        DocType fk_sort = DocType.Etc;
        foreach (Flow fl in fls)
        {
            if (fl.HisFlowSheetType == FlowSheetType.SheetFlow)
                continue;

            i++;
            is1 = this.Pub1.AddTR(is1);
            this.Pub1.AddTDIdx(i);

            //   this.Pub1.AddTDB(fl.DocTypeT);
            if (fl.HisDocType == fk_sort)
                this.Pub1.AddTD();
            else
                this.Pub1.AddTDB(fl.DocTypeT);

            fk_sort = fl.HisDocType;

            this.Pub1.AddTD("<a href='DoClient.aspx?DoType=StartFlow&FK_Flow=" + fl.No + "' >" + fl.Name + "</a>");

            this.Pub1.AddTD("<a href=\"javascript:WinOpen('../Data/FlowDesc/" + fl.No + ".gif','sd');\"  >打开</a>");
            this.Pub1.AddTD(fl.Note);
            this.Pub1.AddTREnd();
        }

        this.Pub1.AddTRSum();
        this.Pub1.AddTD("colspan=" + colspan, "&nbsp;");
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();
    }

}
