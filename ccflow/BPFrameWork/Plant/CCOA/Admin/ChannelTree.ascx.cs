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

    public Channels Channels
    {
        get
        {
            Channels channels = new Channels();
            //articles.Filter("ArticleType", "0");
            channels.RetrieveAll();
            return channels;
        }
    }
}