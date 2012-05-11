using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lizard.Common;
using BP.CCOA;
public partial class Controls_XMenu : BaseUC
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //var bll = new Lizard.GPM.BLL.EIP_Menu();
            //var domains = DomainHelper.GetDomainListByUserId("1");
            //var list = MenuHelper.GetMenusByDomains(domains);

            //foreach (var item in list)
            //{
            //    if (item.MenuNo.Length == 2)
            //    {
            //        NavigationMenu.Items.Add(new MenuItem(item.Title, item.MenuNo, item.Img, item.Url));
            //    }
            //}
            //foreach (MenuItem item in NavigationMenu.Items)
            //{
            //    var childList = GetChildList(item.Value, list);

            //    foreach (var c in childList)
            //    {
            //        item.ChildItems.Add(new MenuItem(c.Title, c.MenuNo, c.Img, c.Url));
            //    }
            //}
        }
    }

    public List<EIP_Menu> GetChildList(string pid, List<EIP_Menu> LstMenu)
    {
        var result = from l in LstMenu
                     where l.Pid == pid
                     select l;

        return result.ToList<EIP_Menu>();
    }
}