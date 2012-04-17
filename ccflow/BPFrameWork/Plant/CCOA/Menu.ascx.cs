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

    public BP.GPM.Menus MenuList
    {
        get
        {

            BP.GPM.Menus menus = new BP.GPM.Menus();
            menus.Retrieve(BP.GPM.MenuAttr.FK_STem, "CCOA", BP.GPM.MenuAttr.TreeNo);
            return menus;
        }
    }
}