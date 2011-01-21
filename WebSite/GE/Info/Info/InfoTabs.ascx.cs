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
using BP.Sys;
using System.Text;

public partial class Comm_GE_Info_InfoTabs : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Infos infos = new Infos();
        //Tab标签的个数
        int numOfTab = infos.GetEnsAppCfgByKeyInt("NumOfTab");

        //行数、列数
        int rows = infos.GetEnsAppCfgByKeyInt("NumOfRow");
        int cols = infos.GetEnsAppCfgByKeyInt("NumOfCol");
        int pageSize = rows * cols;

        //GroupTitle的样式
        string groupTitle = infos.GetEnsAppCfgByKeyString("GroupTitleCSS");

        //更多页面的打开方式
        int target = infos.GetEnsAppCfgByKeyInt("M_Target");
        string strTarget = "_self";
        if (target == 1)
        {
            strTarget = "_blank";
        }

        InfoSorts sorts = new InfoSorts();
        sorts.RetrieveAll();
        numOfTab = numOfTab > sorts.Count ? sorts.Count : numOfTab;

        //TD的宽度
        double width = (1.0 / cols) * 100;
        string strWidth = width.ToString() + "%";

        //控制字符超长样式
        this.PubCSS.Add("<style type='text/css'>");
        this.PubCSS.Add("tr td {overflow: hidden;white-space: nowrap;-o-text-overflow: ellipsis;text-overflow: ellipsis;}");
        this.PubCSS.Add("</style>");

        bool isTDbg = false;
        string tdStyle = "";
        int i = 1;
        foreach (InfoSort sort in sorts)
        {
            if (i > numOfTab)
            {
                break;
            }
            i++;
            #region 开始 [  Infos ] 的矩阵输出

            Infos ens = new Infos();
            ens.RetrieveByType(sort.No, pageSize);

            StringBuilder strTable = new StringBuilder();
            strTable.Append("<table style='width:100%;table-layout:fixed;' cellspacing='0' border='0' cellpadding='0'><tr>");

            //定义显示列数 从0开始。
            int idx = -1;
            isTDbg = false;
            foreach (Info en in ens)
            {
                idx++;

                //设置信息交替背景颜色
                WuXiaoyun.AddTDBg(ref isTDbg, ref tdStyle, cols);

                strTable.Append("<td class='TD' style='width:" + strWidth + ";" + tdStyle + "'>");
                //详细信息
                string enName = en.Name;
                if (en.Name.Length > 14)
                {
                    enName = BP.GE.Glo.DealStrLength(en.Name, 14);
                    enName += "...";
                }

                strTable.Append("<a href='InfoDtl.aspx?No=" + en.No + "' target=_blank>" + enName + "</a>");
                //strTable.Append("<table style='width:100%;table-layout:fixed;' cellspacing='0'><tr>");
                //strTable.Append("<td style='width: 100%;'><a href='InfoDtl.aspx?No=" + en.No + "' target=_blank>" + en.Name + "</a></td>");
                ////strTable.Append("<td style='width: 24%;'>作者：" + en.Author + "</td>");
                //strTable.Append("</tr>");
                //strTable.Append("</table>");

                strTable.Append("</td>");

                if (idx == cols - 1)
                {
                    idx = -1;
                    strTable.Append("</tr>");
                    isTDbg = false;
                }
            }

            while (idx != -1)
            {
                idx++;
                if (idx == cols - 1)
                {
                    idx = -1;

                    WuXiaoyun.AddTDBg(ref isTDbg, ref tdStyle, cols);
                    strTable.Append("<td class='TD' style='width:" + strWidth + ";" + tdStyle + "'></td>");
                    strTable.Append("</tr>");
                    isTDbg = false;
                }
                else
                {
                    WuXiaoyun.AddTDBg(ref isTDbg, ref tdStyle, cols);
                    strTable.Append("<td class='TD' style='width:" + strWidth + ";" + tdStyle + "'></td>");
                }
            }
            strTable.Append("</table>");

            #endregion 结束  [  Infos ]  矩阵输出

            //Tab控件绑定数据
            GeTab1.Items.Add(sort.Name, strTable.ToString());
        }

        #region 开始 [  全部 Infos ] 的矩阵输出

        infos.RetrieveRecom(pageSize);

        StringBuilder strTableAll = new StringBuilder();
        strTableAll.Append("<table style='width:100%;table-layout:fixed;' cellspacing='0' border='0' cellpadding='0'><tr>");

        //定义显示列数 从0开始。
        int all_idx = -1;
        isTDbg = false;
        foreach (Info en in infos)
        {
            all_idx++;

            //设置信息交替背景颜色
            WuXiaoyun.AddTDBg(ref isTDbg, ref tdStyle, cols);

            strTableAll.Append("<td class='TD' style='width:" + strWidth + ";" + tdStyle + "'>");
            //详细信息
            string enName = en.Name;
            if (en.Name.Length > 14)
            {
                enName = BP.GE.Glo.DealStrLength(en.Name, 14);
                enName += "...";
            }

            strTableAll.Append("<a href='InfoDtl.aspx?No=" + en.No + "' target=_blank>" + enName + "</a>");
            //strTableAll.Append("<table style='width:100%;table-layout:fixed;' cellspacing='0'><tr>");
            //strTableAll.Append("<td style='width: 100%;'><a href='InfoDtl.aspx?No=" + en.No + "' target=_blank>" + en.Name + "</a></td>");
            ////strTable.Append("<td style='width: 24%;'>作者：" + en.Author + "</td>");
            //strTableAll.Append("</tr>");
            //strTableAll.Append("</table>");

            strTableAll.Append("</td>");

            if (all_idx == cols - 1)
            {
                all_idx = -1;
                strTableAll.Append("</tr>");
                isTDbg = false;
            }
        }

        while (all_idx != -1)
        {
            all_idx++;
            if (all_idx == cols - 1)
            {
                all_idx = -1;

                WuXiaoyun.AddTDBg(ref isTDbg, ref tdStyle, cols);
                strTableAll.Append("<td class='TD' style='width:" + strWidth + ";" + tdStyle + "'></td>");
                strTableAll.Append("</tr>");
                isTDbg = false;
            }
            else
            {
                WuXiaoyun.AddTDBg(ref isTDbg, ref tdStyle, cols);
                strTableAll.Append("<td class='TD' style='width:" + strWidth + ";" + tdStyle + "'></td>");
            }
        }
        strTableAll.Append("</table>");

        //Tab控件绑定数据
        string strAll = "";
        if (numOfTab > 0)
        {
            strAll = "全部";
        }
        GeTab1.Items.Add(strAll, strTableAll.ToString());

        #endregion 结束  [  全部 Infos ]  矩阵输出

        //Tab控件设置
        bool isShowTabTitle = infos.GetEnsAppCfgByKeyBoolen("IsShowTabTitle");
        int tabHeight = infos.GetEnsAppCfgByKeyInt("TabHeight");
        string title = "";
        if (isShowTabTitle)
        {
            title = infos.GetEnsAppCfgByKeyString("TabTitle");
        }
        GeTab1.Title = title;
        GeTab1.Height = tabHeight;
        GeTab1.SelectedIndex = numOfTab;
        GeTab1.StrMore = "<a href='InfoMore.aspx' target='" + strTarget + "'>更 多..</a>";
    }
}
