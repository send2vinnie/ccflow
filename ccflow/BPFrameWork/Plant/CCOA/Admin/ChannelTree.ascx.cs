using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CCOA;

public partial class CCOA_Admin_ChannelTree : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public Channels NewsChannels
    {
        get
        {
            Channels channels = new Channels();
            channels.RetrieveByAttr("Type", "0");
            return channels;
        }
    }

    public Channels NoticeChannels
    {
        get
        {
            Channels channels = new Channels();
            channels.RetrieveByAttr("Type", "1");
            return channels;
        }
    }
}