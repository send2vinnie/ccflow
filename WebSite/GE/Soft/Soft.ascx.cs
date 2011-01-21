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

public partial class Comm_GE_Soft_Soft : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Softs softs = new Softs();
        //Tab标签的个数
        int numOfTab = softs.GetEnsAppCfgByKeyInt("NumOfTab");

        //行数、列数
        int rows = softs.GetEnsAppCfgByKeyInt("NumOfRow");
        int cols = softs.GetEnsAppCfgByKeyInt("NumOfCol");
        int pageSize = rows * cols;

        //GroupTitle的样式
        string groupTitle = softs.GetEnsAppCfgByKeyString("GroupTitleCSS");

        SoftSorts sorts = new SoftSorts();
        sorts.RetrieveAll();

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
        foreach (SoftSort sort in sorts)
        {
            if (i > numOfTab)
            {
                break;
            }
            i++;

            #region 开始 [  Softs ] 的矩阵输出

            Softs ens = new Softs();
            ens.RetrieveRecomByType(sort.No, -1);

            StringBuilder strTable = new StringBuilder();
            strTable.Append("<table style='width:100%;table-layout:fixed;' cellspacing='0'><tr>");

            //定义显示列数 从0开始。
            int idx = -1;
            isTDbg = false;
            int count = 1;
            foreach (Soft en in ens)
            {
                if(count > pageSize)
                {
                    break;
                }
                //获取软件附件
                SysFileManagers sfs = en.HisSysFileManagers;
                if (sfs == null)
                {
                    continue;
                }
                SysFileManager sf_soft = (SysFileManager)sfs.GetEntityByKey(SysFileManagerAttr.AttrFileNo, "Soft");
                if (sf_soft == null)
                {
                    continue;
                }

                count++;
                idx++;

                //设置信息交替背景颜色
                WuXiaoyun.AddTDBg(ref isTDbg, ref tdStyle, cols);

                strTable.Append("<td class='TD' style='width:" + strWidth + ";" + tdStyle + "'>");

                //详细信息
                strTable.Append("<table style='width:100%;table-layout:fixed;' cellspacing='0'><tr>");
                strTable.Append("<td style='width: 32%;'><a href='SoftDtl.aspx?RefNo=" + en.No + "' target=_blank>" + en.Name + "</a></td>");
                strTable.Append("<td style='width: 23%;'>更新日期：" + en.RDT + "</td>");
                strTable.Append("<td style='width: 17%;'>大小：" + WuXiaoyun.ConvertFileSize(sf_soft.MyFileSize) + "</td>");
                strTable.Append("<td style='width: 18%;'>下载次数：" + en.DownTimes + "</td>");
                strTable.Append("<td style='width: 10%;'><a href=\"" + this.Request.ApplicationPath + "/GE/Soft/Do.aspx?FileOID=" + sf_soft.OID
                            + "&DoType=DownLoad\" target='_blank'>下载</a></td>");
                strTable.Append("</tr>");
                strTable.Append("</table>");

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

            #endregion 结束  [  Softs ]  矩阵输出

            //Tab控件绑定数据
            GeTab1.Items.Add(sort.Name, strTable.ToString());
        }

        #region 开始 [  全部 Softs ] 的矩阵输出

        softs.RetrieveByRecom(-1);

        StringBuilder strTableAll = new StringBuilder();
        strTableAll.Append("<table style='width:100%;table-layout:fixed;' cellspacing='0'><tr>");

        //定义显示列数 从0开始。
        int all_idx = -1;
        isTDbg = false;
        int all_count = 1;
        foreach (Soft en in softs)
        {
            if (all_count > pageSize)
            {
                break;
            }

            //获取软件附件
            SysFileManagers sfs = en.HisSysFileManagers;
            if (sfs == null)
            {
                continue;
            }
            SysFileManager sf_soft = (SysFileManager)sfs.GetEntityByKey(SysFileManagerAttr.AttrFileNo, "Soft");
            if (sf_soft == null)
            {
                continue;
            }

            all_count++;
            all_idx++;

            //设置信息交替背景颜色
            WuXiaoyun.AddTDBg(ref isTDbg, ref tdStyle, cols);

            strTableAll.Append("<td class='TD' style='width:" + strWidth + ";" + tdStyle + "'>");

            //详细信息
            strTableAll.Append("<table style='width:100%;table-layout:fixed;' cellspacing='0'><tr>");
            strTableAll.Append("<td style='width: 32%;'><a href='SoftDtl.aspx?RefNo=" + en.No + "' target=_blank>" + en.Name + "</a></td>");
            strTableAll.Append("<td style='width: 23%;'>更新日期：" + en.RDT + "</td>");
            strTableAll.Append("<td style='width: 17%;'>大小：" + WuXiaoyun.ConvertFileSize(sf_soft.MyFileSize) + "</td>");
            strTableAll.Append("<td style='width: 18%;'>下载次数：" + en.DownTimes + "</td>");
            strTableAll.Append("<td style='width: 10%;'><a href=\"" + this.Request.ApplicationPath + "/GE/Soft/Do.aspx?FileOID=" + sf_soft.OID
                        + "&DoType=DownLoad\" target='_blank'>下载</a></td>");
            strTableAll.Append("</tr>");
            strTableAll.Append("</table>");

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

        #endregion 结束  [  全部 Softs ]  矩阵输出

        //Tab控件设置
        bool isShowTabTitle = softs.GetEnsAppCfgByKeyBoolen("IsShowTabTitle");
        int tabHeight = softs.GetEnsAppCfgByKeyInt("TabHeight");
        string title = "";
        if (isShowTabTitle)
        {
            title = softs.GetEnsAppCfgByKeyString("TabTitle");
        }
        GeTab1.Title = title;
        GeTab1.Height = tabHeight;
        //更多页面的打开方式
        int target = softs.GetEnsAppCfgByKeyInt("M_Target");
        string strTarget = "_self";
        if (target == 1)
        {
            strTarget = "_blank";
        }
        GeTab1.SelectedIndex = numOfTab;
        GeTab1.StrMore = "<a href='SoftMore.aspx' target='" + strTarget + "'>更 多..</a>";
    }
}
