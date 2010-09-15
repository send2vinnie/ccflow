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
using BP.GE;
using BP.En;
using BP.DA;
using BP.Web;

public partial class Peng_JZOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        this.Pub1.AddH4("矩阵输出-范例");
        this.Pub1.AddHR();


        #region 开始 [  信息发布 ] 的矩阵输出
        Infos ens = new Infos();
        ens.RetrieveAll();
        int cols = 3; //定义显示列数 从0开始。

        this.Pub1.AddTable();
        int idx = -1;
        bool is1 = false;
        foreach (Info en in ens)
        {
            idx++;
            if (idx == 0)
                is1=this.Pub1.AddTR(is1);


            this.Pub1.AddTD(en.Name);

            if (idx == cols-1)
            {
                idx = -1;
                this.Pub1.AddTREnd();
            }
        }

        while (idx != -1)
        {
            idx++;
            if (idx == cols-1)
            {
                idx = -1;
                this.Pub1.AddTD();
                this.Pub1.AddTREnd();
            }
            else
            {
                this.Pub1.AddTD();
            }
        }
        this.Pub1.AddTableEnd();

        #endregion 结束  [  信息发布 ]  矩阵输出

        string msg = BP.DA.DataType.ReadTextFile(BP.SystemConfig.PathOfWebApp + "\\Peng\\JZOut.aspx.cs");
        msg = DataType.ParseText2Html(msg);
        this.Pub1.AddH4("矩阵输出-源代码");
        this.Pub1.Add(msg);
    }
}
