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
using BP.GE;

public partial class Comm_GE_Info_Info : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        InfoSorts sorts = new InfoSorts();
        sorts.RetrieveAll();

        Infos ens = new Infos();
        //整个页面的行数、列数
        int cols = ens.GetEnsAppCfgByKeyInt("NumOfCol");
        int rows = ens.GetEnsAppCfgByKeyInt("NumOfRow");
        //每个类别中的行数、列数
        int T_cols = ens.GetEnsAppCfgByKeyInt("ColsInType");
        int T_rows = ens.GetEnsAppCfgByKeyInt("RowsInType");

        //GroupTitle的样式
        string groupTitle = ens.GetEnsAppCfgByKeyString("GroupTitleCSS");

        //更多页面的打开方式
        int target = ens.GetEnsAppCfgByKeyInt("M_Target");
        string strTarget = "_self";
        if (target == 1)
        {
            strTarget = "_blank";
        }

        //每个类别TD的宽度
        double width = (1.0 / cols) * 100;
        string strWidth = width.ToString() + "%";
        //每个类别中，信息TD的宽度
        double T_width = (1.0 / T_cols) * 100;
        string T_strWidth = T_width.ToString() + "%";

        #region 开始 [  InfoSorts ] 的矩阵输出

        //控制字符超长样式
        this.Add("<style type='text/css'>");
        this.Add("tr td {overflow: hidden;white-space: nowrap;-o-text-overflow: ellipsis;text-overflow: ellipsis;}");
        this.Add("</style>");

        this.Add("<table style='width:100%;' cellspacing='0'>");
        this.AddTR();

        int sortCount = 1;
        int sortNum = cols * rows;
        int enNum = T_cols * T_rows;

        //定义显示列数 从0开始。
        int idx = -1;
        foreach (InfoSort sort in sorts)
        {
            //只显示单一类别
            if (this.RefNo != null && this.RefNo != "")
            {
                if (this.RefNo != sort.No)
                {
                    continue;
                }
            }
            else
            {
                if (sortCount > sortNum)
                {
                    break;
                }
                sortCount++;
            }

            idx++;
            this.Add("<td class='BigDoc' valign='Top' style='width:" + strWidth + "'>");

            //详细信息
            #region 开始 [  Infos ] 的矩阵输出

            ens.RetrieveByType(sort.No, enNum);

            this.AddTable("style='width:100%;' cellspacing='0'");
            this.AddTR();
            this.AddTD("colspan='" + T_cols + "' class ='" + groupTitle + "'", "<a class='gengduo' style='float:right' target='" + strTarget + "' href='InfoMore.aspx?RefNo=" + sort.No + "' >更 多..</a>" + sort.Name);
            this.AddTREnd();

            //TD交替背景色
            bool isTDbg = false;
            string tdStyle = "";

            //定义显示列数 从0开始。
            this.AddTR();
            int T_idx = -1;
            foreach (Info en in ens)
            {
                T_idx++;
                WuXiaoyun.AddTDBg(ref isTDbg, ref tdStyle, T_cols);
                this.AddTDBegin("style='" + tdStyle + "width:" + T_strWidth + "'");

                this.Add("<table style='width:100%;table-layout:fixed' cellspacing='0'>");
                this.AddTR();

                this.AddTD("style='width:48%;'", "<a href='InfoDtl.aspx?No=" + en.No + "' target=_blank>" + en.Name + "</a>");
                this.AddTD("style='width:30%;'", "阅读次数：" + en.NumRead);
                this.AddTD("style='width:22%;'", en.RDT);
                this.AddTREnd();
                this.AddTableEnd();

                this.AddTDEnd();

                if (T_idx == T_cols - 1)
                {
                    T_idx = -1;
                    this.AddTREnd();
                    isTDbg = false;
                }
            }

            while (T_idx != -1)
            {
                T_idx++;
                if (T_idx == T_cols - 1)
                {
                    T_idx = -1;
                    WuXiaoyun.AddTDBg(ref isTDbg, ref tdStyle, T_cols);
                    this.AddTD("class='TD' style='" + tdStyle + "width:" + T_strWidth + "'", "");
                    this.AddTREnd();
                    isTDbg = false;
                }
                else
                {
                    WuXiaoyun.AddTDBg(ref isTDbg, ref tdStyle, T_cols);
                    this.AddTD("class='TD' style='" + tdStyle + "width:" + T_strWidth + "'", "");
                }
            }
            this.AddTableEnd();

            #endregion 结束  [ Infos ]  矩阵输出

            this.AddTDEnd();

            if (idx == cols - 1)
            {
                idx = -1;
                this.AddTREnd();
            }
        }
        while (idx != -1)
        {
            idx++;
            if (idx == cols - 1)
            {
                idx = -1;
                this.AddTD("class='BigDoc' valign='Top' style='width:" + strWidth + "'", "");
                this.AddTREnd();
            }
            else
            {
                this.AddTD("class='BigDoc' valign='Top' style='width:" + strWidth + "'", "");
            }
        }
        this.AddTableEnd();

        #endregion 结束  [ InfoSorts ]  矩阵输出

    }
}
