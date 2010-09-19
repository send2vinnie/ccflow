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
using BP.WF;
using BP.Port;
using BP.Sys;
using BP.Port;
using BP.DA;
using BP.En;
using BP.Web;

public partial class WF_UC_Bill : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Books books = new Books();
        books.RetrieveAll();

        this.AddTable();
        this.AddTR();
        this.AddTDTitle("ID");
        this.AddTDTitle("流程");
        this.AddTDTitle("节点");
        this.AddTDTitle("部门");
        this.AddTDTitle("单据名称");
        this.AddTDTitle("打印日期");
        this.AddTDTitle("打印人");
        this.AddTREnd();

        int i = 0;
        bool is1 = false;
        foreach (Book book in books)
        {
            this.AddTR(is1);
            i++;
            this.AddTDIdx(i);
            this.AddTD(book.FK_FlowT);
            this.AddTD(book.FK_NodeT);
            this.AddTD(book.FK_DeptT);

            this.AddTDA("javascript:WinOpen('"+book.Url+"')","<img src='../../Images/Btn/Word.gif' border=0 />"+book.FK_BookText);
            this.AddTD(book.RDT);
            this.AddTD(book.FK_EmpT);
            this.AddTREnd();
        }
        this.AddTableEnd();
    }
}
