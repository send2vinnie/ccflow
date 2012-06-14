using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MenuItem rootItem = new MenuItem("select mode");
            foreach (WebPartDisplayMode mode in WebPartManager1.DisplayModes)
            {
                if (mode.IsEnabled(WebPartManager1))
                {
                    rootItem.ChildItems.Add(new MenuItem(mode.Name));

                }
            }
            ModesMenu.Items.Add(rootItem);



        }
    }
    protected void ModesMenu_MenuItemClick(object sender, MenuEventArgs e)
    {
        foreach (WebPartDisplayMode mode in WebPartManager1.DisplayModes)
        {
            if (mode.Name == e.Item.Text)
            {
                WebPartManager1.DisplayMode = mode;
                break;
            }
        }
    }
}

