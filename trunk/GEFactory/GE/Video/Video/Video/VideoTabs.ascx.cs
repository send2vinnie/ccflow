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

public partial class Comm_GE_Video_VideoTabs : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Videos videos = new Videos();
        //Tab标签的个数
        int numOfTab = videos.GetEnsAppCfgByKeyInt("NumOfTab");

        //行数、列数
        int rows = videos.GetEnsAppCfgByKeyInt("NumOfRow");
        int cols = videos.GetEnsAppCfgByKeyInt("NumOfCol");
        int pageSize = rows * cols;

        //GroupTitle的样式
        string groupTitle = videos.GetEnsAppCfgByKeyString("GroupTitleCSS");

        //更多页面的打开方式
        int target = videos.GetEnsAppCfgByKeyInt("M_Target");
        Alert(target.ToString());
        string strTarget = "_self";
        if (target == 1)
        {
            strTarget = "_blank";
        }

        VideoSorts sorts = new VideoSorts();
        sorts.RetrieveAll();
        numOfTab = numOfTab > sorts.Count ? sorts.Count : numOfTab;

        //TD的宽度
        double width = (1.0 / cols) * 100;
        string strWidth = width.ToString() + "%";

        //控制字符超长样式
        this.PubCSS.Add("<style type='text/css'>");
        this.PubCSS.Add("tr td span,tr td a{overflow: hidden;white-space: nowrap;-o-text-overflow: ellipsis;text-overflow: ellipsis;}");
        this.PubCSS.Add("</style>");

        bool isTDbg = false;
        string tdStyle = "";
        int i = 1;
        foreach (VideoSort sort in sorts)
        {
            if (i > numOfTab)
            {
                break;
            }
            i++;
            #region 开始 [  Videos ] 的矩阵输出

            Videos ens = new Videos();
            ens.RetrieveRecomByType(sort.No, pageSize);

            StringBuilder strTable = new StringBuilder();
            strTable.Append("<table style='width:100%;table-layout:fixed;' cellspacing='0' cellpadding='2'><tr>");

            //定义显示列数 从0开始。
            int idx = -1;
            isTDbg = false;
            foreach (Video en in ens)
            {
                if (en.FK_Sort != sort.No)
                {
                    continue;
                }
                idx++;

                //设置信息交替背景颜色
                WuXiaoyun.AddTDBg(ref isTDbg, ref tdStyle, cols);

                strTable.Append("<td class='TD' style='width:" + strWidth + ";" + tdStyle + "'>");
                //详细信息
                //string enName = en.Name;
                //if (enName.Length > 10)
                //{
                //    enName = BP.GE.Glo.DealStrLength(enName, 10);
                //    enName += "...";
                //}
                strTable.Append("<span class='author' style='float:right;margin-right:10px;width:30%'>" + en.Author + "</span><nobr><a style='width:60%' href='VideoTabsDtl.aspx?No=" + en.No + "' target=_blank>" + en.Name + "</a></nobr>");
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

            #endregion 结束  [  Videos ]  矩阵输出

            //Tab控件绑定数据
            GeTab1.Items.Add(sort.Name, strTable.ToString());
        }

        #region 开始 [  全部 Videos ] 的矩阵输出

        videos.RetrieveByRecom(pageSize);
        StringBuilder strTableAll = new StringBuilder();
        strTableAll.Append("<table style='width:100%;table-layout:fixed;' cellspacing='0' cellpadding='2'><tr>");

        //定义显示列数 从0开始。
        int all_idx = -1;
        isTDbg = false;
        foreach (Video en in videos)
        {
            all_idx++;

            //设置信息交替背景颜色
            WuXiaoyun.AddTDBg(ref isTDbg, ref tdStyle, cols);

            strTableAll.Append("<td class='TD' style='width:" + strWidth + ";" + tdStyle + "'>");
            //详细信息
            //string enName = en.Name;
            //if (enName.Length > 10)
            //{
            //    enName = BP.GE.Glo.DealStrLength(enName, 10);
            //    enName += "...";
            //}
            strTableAll.Append("<span class='author' style='float:right;margin-right:10px;width:30%'>" + en.Author + "</span><nobr><a style='width:60%' href='VideoTabsDtl.aspx?No=" + en.No + "' target=_blank>" + en.Name + "</a></nobr>");
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

        #endregion 结束  [  全部 Videos ]  矩阵输出

        //Tab控件设置
        bool isShowTabTitle = videos.GetEnsAppCfgByKeyBoolen("IsShowTabTitle");
        int tabHeight = videos.GetEnsAppCfgByKeyInt("TabHeight");
        string title = "";
        if (isShowTabTitle)
        {
            title = videos.GetEnsAppCfgByKeyString("TabTitle");
        }
        GeTab1.Title = title;
        GeTab1.Height = tabHeight;
        GeTab1.SelectedIndex = numOfTab;
        GeTab1.StrMore = "<a href='VideoTabsMore.aspx' target='" + strTarget + "'>更 多..</a>";
    }
}