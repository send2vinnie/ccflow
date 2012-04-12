using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CCOA;

public partial class CCOA_Admin_ChannelForm : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        Channel channel = new Channel();
        channel.No = Guid.NewGuid().ToString();
        channel.Name = txtTitle.Text;
        channel.ChannelName = txtTitle.Text;
        channel.FullUrl = txtUrl.Text;
        channel.Description = txtDescription.Text;
        channel.Type = "1";
        channel.Created = DateTime.Now;
        channel.State = 1;
        channel.ReferenceID = "";
        channel.ParentID = Guid.Empty.ToString();

        channel.Insert();
    }
    protected void btnCancle_Click(object sender, EventArgs e)
    {

    }
}