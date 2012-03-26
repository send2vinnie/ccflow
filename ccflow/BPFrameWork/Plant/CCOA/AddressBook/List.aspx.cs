using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CCOA_AddressBook_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BP.Port.Emps emps = new BP.Port.Emps();
        emps.RetrieveAllFromDBSource();

        //this.Pub1.AddTable();
        //this.Pub1.AddTR();
        //this.Pub1.AddTH("IDX");
        //this.Pub1.AddTH("编号");
        //this.Pub1.AddTH("名称");
        //this.Pub1.AddTH("部门");
        //this.Pub1.AddTREnd();

        //int idx = 0;
        //foreach (Emp item in emps)
        //{
        //    idx++;
        //    this.Pub1.AddTR();
        //    this.Pub1.AddTDIdx(idx);
        //    this.Pub1.AddTD(item.No);
        //    this.Pub1.AddTD(item.Name);
        //    this.Pub1.AddTD(item.FK_DeptText);
        //    this.Pub1.AddTREnd();
        //}
        //this.Pub1.AddTableEnd();

    }
}