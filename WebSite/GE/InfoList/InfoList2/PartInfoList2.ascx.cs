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


public partial class GE_Info_PartInfoList2 : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        InfoList2s ens = new InfoList2s();

        //行数、列数
        int rows = ens.GetEnsAppCfgByKeyInt("NumOfRow");
        int cols = ens.GetEnsAppCfgByKeyInt("NumOfCol");
        int pageSize = rows * cols;

        //GroupTitle的样式
        string groupTitle = ens.GetEnsAppCfgByKeyString("GroupTitleCSS");
        string title = ens.GetEnsAppCfgByKeyString("Title");

        #region 开始 [  InfoList2s ] 的矩阵输出

        ens.RetrieveRecom(pageSize);
        //TD的宽度
        double width = (1.0 / cols) * 100;
        string strWidth = width.ToString() + "%";

        this.AddTable();
        this.AddTR();
        int colspan = cols;
        if (ens.Count > 0)
        {
            //更多页面的打开方式
            int target = ens.GetEnsAppCfgByKeyInt("M_Target");
            string strTarget = "_self";
            if (target == 1)
            {
                strTarget = "_blank";
            }

            this.AddTD("colspan='" + colspan + "' class ='" + groupTitle + "'", "<a class='gengduo' style='float:right' target='" + strTarget + "' href='InfoList2.aspx?DoType=ShowMore' >更 多..</a>" + title);
        }
        else
        {
            this.AddTD("colspan='" + colspan + "' class ='" + groupTitle + "'", title);
        }
        this.AddTREnd();

        //定义显示列数 从0开始。
        this.AddTR();
        int idx = -1;
       // bool is1 = false;
        foreach (InfoList2 en in ens)
        {
            idx++;
            //if (idx == 0)
            //    is1 = this.AddTR(is1);

            this.AddTDBegin("style='width:" + strWidth + "'");
            this.Add("<li style='list-style:none'><a style='width: 98%;overflow: hidden;white-space: nowrap;-o-text-overflow: ellipsis;text-overflow: ellipsis;' href='InfoList2.aspx?RefNo=" + en.No + "' target=_blank>" + en.Name + "</a></li>");
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
                this.AddTD("style='width:" + strWidth + "'");
                this.AddTREnd();
            }
            else
            {
                this.AddTD("style='width:" + strWidth + "'");
            }
        }
        this.AddTableEnd();

        #endregion 结束  [  InfoList2 ]  矩阵输出
    }
}
