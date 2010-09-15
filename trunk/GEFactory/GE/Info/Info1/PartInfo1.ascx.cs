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


public partial class Comm_GE_Info_PartInfo1 : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Info1s ens = new Info1s();

        //行数、列数
        int rows = ens.GetEnsAppCfgByKeyInt("NumOfRow");
        int cols = ens.GetEnsAppCfgByKeyInt("NumOfCol");
        int pageSize = rows * cols;

        //GroupTitle的样式
        string groupTitle = ens.GetEnsAppCfgByKeyString("GroupTitleCSS");
        string title = ens.GetEnsAppCfgByKeyString("Title");

        //更多页面的打开方式
        int target = ens.GetEnsAppCfgByKeyInt("M_Target");
        string strTarget = "_self";
        if (target == 1)
        {
            strTarget = "_blank";
        }

        #region 开始 [  Info1s ] 的矩阵输出

        ens.RetrieveRecom(pageSize);
        this.AddTable();
        this.AddTR();
        int colspan = cols;
        if (ens.Count > 0)
        {
            this.AddTD("colspan='" + colspan + "' class ='" + groupTitle + "'", "<a class='gengduo' style='float:right' target='" + strTarget + "' href='InfoMore1.aspx' >更 多..</a>" + title);
        }
        else
        {
            this.AddTD("colspan='" + colspan + "' class ='" + groupTitle + "'", title);
        }
        this.AddTREnd();

        //定义显示列数 从0开始。
        this.AddTREnd();
        int idx = -1;
        // bool is1 = false;
        foreach (Info1 en in ens)
        {
            idx++;
            //if (idx == 0)
            //    is1 = this.AddTR(is1);

            this.AddTDBegin();
            this.Add("<li style='list-style:none'><a style='width: 50px;overflow: hidden;white-space: nowrap;-o-text-overflow: ellipsis;text-overflow: ellipsis;' href='InfoDtl2.aspx?No=" + en.No + "' target=_blank>" + en.Name + "</a>&nbsp;&nbsp;&nbsp;&nbsp;" + en.RDT
                        + "<span> &nbsp;&nbsp;&nbsp;&nbsp;阅读次数：" + en.NumRead + "</span></li>");
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
                this.AddTD();
                this.AddTREnd();
            }
            else
            {
                this.AddTD();
            }
        }
        this.AddTableEnd();

        #endregion 结束  [  Info1s ]  矩阵输出
    }
}
