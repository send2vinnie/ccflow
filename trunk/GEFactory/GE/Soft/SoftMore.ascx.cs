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

public partial class Comm_GE_Soft_SoftMore : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Softs ens = new Softs();
        //行数、列数
        int cols = ens.GetEnsAppCfgByKeyInt("M_NumOfCol");
        int rows = ens.GetEnsAppCfgByKeyInt("M_NumOfRow");
        int pageSize = cols * rows;

        //GroupTitle的样式
        string groupTitle = ens.GetEnsAppCfgByKeyString("GroupTitleCSS");

        //左侧分类列表
        this.PubSort.AddTable();
        this.PubSort.AddTR();
        this.PubSort.AddTD("class ='" + groupTitle + "'", "分类");
        this.PubSort.AddTREnd();

        this.PubSort.AddTR();
        this.PubSort.AddTDBegin();

        SoftSorts sorts = new SoftSorts();
        sorts.RetrieveAll();

        string type = "";
        this.PubSort.MenuSelfVerticalBegin();
        //添加全部选项
        if (this.RefNo == null || this.RefNo == "")
        {
            this.PubSort.MenuSelfVerticalItemS("SoftMore.aspx", "全部", null);
            type = "全部";
        }
        else
        {
            this.PubSort.MenuSelfVerticalItem("SoftMore.aspx", "全部", null);
        }

        foreach (SoftSort sort in sorts)
        {
            if (this.RefNo == sort.No)
            {
                this.PubSort.MenuSelfVerticalItemS("SoftMore.aspx?RefNo=" + sort.No, sort.Name, null);
                type = sort.Name;
            }
            else
            {
                this.PubSort.MenuSelfVerticalItem("SoftMore.aspx?RefNo=" + sort.No, sort.Name, null);
            }
        }
        this.PubSort.MenuSelfVerticalEnd();

        this.PubSort.AddTDEnd();
        this.PubSort.AddTREnd();
        this.PubSort.AddTableEnd();

        //翻页控件
        QueryObject qo = new QueryObject(ens);
        if (this.RefNo != null && this.RefNo != "")
        {
            qo.AddWhere(SoftAttr.FK_Sort, this.RefNo);
        }
        qo.addOrderByDesc(SoftAttr.RecomIdx);
        this.PubPage.Add("<div class='divPage' style='text-align:right;'>");
        this.PubPage.BindPageIdx(qo.GetCount(), pageSize, this.PageIdx, "SoftMore.aspx?RefNo=" + this.RefNo);
        qo.DoQuery("No", pageSize, this.PageIdx);
        this.PubPage.Add("</div>");

        //页面标题
        try
        {
            this.Page.Title = ens.GetEnsAppCfgByKeyString("AppName");
        }
        catch
        {
        }

        this.PubContent.AddTable();
        this.PubContent.AddTR();
        this.PubContent.AddTD("colspan='" + cols + "' class ='" + groupTitle + "'", type);
        this.PubContent.AddTREnd();

        this.PubContent.AddTR();

        #region 开始 [  Softs ] 的矩阵输出

        //控制字符超长样式
        this.Add("<style type='text/css'>");
        this.Add("tr td {overflow: hidden;white-space: nowrap;-o-text-overflow: ellipsis;text-overflow: ellipsis;}");
        this.Add("</style>");

        //TD的宽度
        double width = (1.0 / cols) * 100;
        string strWidth = width.ToString() + "%";

        //TD交替背景色
        bool isTDbg = false;
        string tdStyle = "";

        //定义显示列数 从0开始。
        int idx = -1;
        foreach (Soft en in ens)
        {
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

            idx++;

            //设置信息交替背景颜色
            WuXiaoyun.AddTDBg(ref isTDbg, ref tdStyle, cols);

            this.PubContent.AddTDBegin("class='TD' style='width:" + strWidth + ";" + tdStyle + "'");

            //详细信息
            this.PubContent.Add("<table style='width:100%;table-layout:fixed' cellspacing='0'>");
            this.PubContent.AddTR();
            this.PubContent.AddTD("style='width: 32%;padding-left:10px;'","<a href='SoftDtl.aspx?RefNo=" + en.No + "' target=_blank>" + en.Name + "</a>");
            this.PubContent.AddTD("style='width: 23%;'", "更新日期：" + en.RDT );
            this.PubContent.AddTD("style='width: 17%;'", "大小：" + WuXiaoyun.ConvertFileSize(sf_soft.MyFileSize));
            this.PubContent.AddTD("style='width: 18%;'", "下载次数：" + en.DownTimes);
            this.PubContent.AddTD("style='width: 10%;'", "<a href=\"" + this.Request.ApplicationPath + "/GE/Soft/Do.aspx?FileOID=" + sf_soft.OID
                        + "&DoType=DownLoad\" target='_blank'>下载</a>");
            this.PubContent.AddTREnd();
            this.PubContent.AddTableEnd();

            this.PubContent.AddTDEnd();

            if (idx == cols - 1)
            {
                idx = -1;
                this.PubContent.AddTREnd();
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
                this.PubContent.AddTD("class='TD' style='" + tdStyle + "width:" + strWidth + "'", "");
                this.PubContent.AddTREnd();
                isTDbg = false;
            }
            else
            {
                WuXiaoyun.AddTDBg(ref isTDbg, ref tdStyle, cols);
                this.PubContent.AddTD("class='TD' style='" + tdStyle + "width:" + strWidth + "'", "");
            }
        }

        #endregion 结束  [  Soft ]  矩阵输出

        if (ens.Count <= 0)
        {
            this.PubContent.AddTD("class='TD' colspan='" + cols + "'", "<span class='none'>该类别下没有相关信息。</span>");
        }

        this.PubContent.AddTREnd();
        this.PubContent.AddTableEnd();
    }
}
