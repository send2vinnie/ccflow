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
using BP.GE;
using BP.En;
using BP.DA;
using BP.Sys;
using BP.Port;
using BP.Web.Controls;

public partial class Peng_Default :WebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /*
         * 编写规范：
         * 1，分页控件由两个子控件组合而成。一个显示内容Pub1，一个显示分页的信息Pub2。
         * 2, 系统要做两次查询，首先查询出来显示的个数，其次显示要分页的数据。
         */

        this.Pub1.AddH4("分页输出-范例");
        this.Pub1.AddHR();

        #region 开始 [  信息发布 ] 的分页输出.
        int pageSize = BP.SystemConfig.PageSize; //定义页面的数据行数。
        Infos ens = new Infos();
        BP.En.QueryObject qo = new QueryObject(ens);

        int maxPageNum = this.Pub2.BindPageIdx(qo.GetCount(), pageSize,this.PageIdx,"?1=1"); //生成分页的样式。PageIdx只有页面继承了WebPage 才有这个属性。
        qo.DoQuery("No", pageSize, this.PageIdx);  // 查询出来指定页面的数据。

        this.Pub1.AddTable();
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("编号");
        this.Pub1.AddTDTitle("名称");
        this.Pub1.AddTDTitle("日期");
        this.Pub1.AddTDTitle("类型");
        this.Pub1.AddTREnd();
        bool is1 = false;
        foreach (Info en in ens)
        {
           is1= this.Pub1.AddTR(is1);
            this.Pub1.AddTD(en.No);
            this.Pub1.AddTD(en.Name);
            this.Pub1.AddTD(en.RDT);
            this.Pub1.AddTD(en.FK_SortT);
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEnd();
        #endregion 结束 [  信息发布 ] 的分页输出



        this.Pub3.AddH4("输出-源代码");
        string msg = BP.DA.DataType.ReadTextFile(BP.SystemConfig.PathOfWebApp + "\\Peng\\PageIndex.aspx.cs");
        msg = DataType.ParseText2Html(msg);
        this.Pub3.Add(msg);
    }
}
