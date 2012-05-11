using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CCOA;
using Lizard.Controls;

public partial class CCOA_AddressBook_Form : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnCommit_Click(object sender, EventArgs e)
    {
        AddrBook addrbook = new AddrBook();
        addrbook.No = Guid.NewGuid().ToString();
        addrbook.Tel = txtTel.Text;
        addrbook.Email = txtEmail.Text;
        addrbook.Duty = txtDuty.Text;
        addrbook.Addr = txtAddress.Text;
        addrbook.Name = txtTitle.Text;

        addrbook.Insert();
    }
    protected void btnCancle_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../CCOA/AddressBook/List.aspx");
    }
}