using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CCOA;
using System.Data;

public partial class CCOA_Menu : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.lbl.Text = "";
        this.divPop.Visible = true;
    }

    public Menus MenuList
    {
        get
        {
            BP.CCOA.Menus menus = new Menus();
            menus.RetrieveAll();

            return menus;
        }
    }

}