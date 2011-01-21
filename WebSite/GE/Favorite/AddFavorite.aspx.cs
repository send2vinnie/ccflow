using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GE_Favorite_AddFavorite : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(BP.Web.WebUser.No))
        {
            BP.GE.GeFun.ShowMessage(this.Page, "strJS6", "对不起请登录!");
        }
        else
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["url"] != null&&Request.QueryString["title"] != null)
                {
                    this.txtFileName.Text = Server.UrlDecode(Request.QueryString["title"].ToString());
                }
                FillddlFavName();
            }
        }
    }

    private void FillddlFavName()
    {
        ddlFavName.Items.Clear();
        BP.GE.GEFavNames ens = new BP.GE.GEFavNames();
        ens.Retrieve(BP.GE.GEFavNameAttr.FK_Emp, BP.Web.WebUser.No);
        ListItem li = new ListItem("所有收藏", "-1");
        ddlFavName.Items.Add(li);
        foreach (BP.GE.GEFavName en1 in ens)
        {
            li = new ListItem(en1.Name, en1.OID.ToString());
            ddlFavName.Items.Add(li);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        BP.GE.GEFavList en = new BP.GE.GEFavList();
        en.FK_Emp = BP.Web.WebUser.No;
        en.RDT = DateTime.Now.ToString();
        en.Title = txtFileName.Text;
        en.FK_FavNameID = Convert.ToInt32(ddlFavName.SelectedValue);
        en.Url = Server.UrlDecode(Request.QueryString["url"].ToString());
        en.Insert();
        BP.GE.GeFun.ShowMessage(this.Page, "strJS3", "收藏成功!");
    }
}