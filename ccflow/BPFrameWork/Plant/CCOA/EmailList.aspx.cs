using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CCOA;

public partial class EIP_EmailList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lbtAll_Click(null, null);
        }
    }

    private OA_Emails emailList;
    public OA_Emails EmailList
    {
        get
        {
            //BP.CCOA.OA_Emails list = new OA_Emails();
            //list.RetrieveAll();
            //emailList= list;
            return emailList;
        }
        set
        {
            emailList = value;
        }
    }

    protected void lbtAll_Click(object sender, EventArgs e)
    {
        OA_Emails list = new OA_Emails();
        list.RetrieveAll();
        EmailList = list;
    }
    protected void lbtUnRead_Click(object sender, EventArgs e)
    {
        OA_Emails list = new OA_Emails();
        list.RetrieveByAttr("Category", 2);
        EmailList = list;
    }
    protected void lbtReaded_Click(object sender, EventArgs e)
    {
        OA_Emails list = new OA_Emails();
        list.RetrieveByAttr("Category", 1);
        EmailList = list;
    }
}