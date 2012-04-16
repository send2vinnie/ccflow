using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.Port;
using BP.CCOA;

public partial class CCOA_AddressBook_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int PageSize =Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
        //XPager1.OnPagerChanged += new CCOA_Control_XPager.RefreshEventHandler(XPager1_OnPagerChanged);
        if (!Page.IsPostBack)
        {
            //XPager1.InitControl(PageSize, ItemCount);
        }

    }

    //void XPager1_OnPagerChanged(object sender, CurrentPageEventArgs e)
    //{
        
    //}

    public Emps Emps
    {
        get
        {
            BP.Port.Emps emps = new BP.Port.Emps();
            emps.RetrieveAllFromDBSource();

            return emps;
        }
    }

    public int ItemCount
    {
        get
        {
            return Emps.Count;
        }
    }

    //public EmpInfo GetEmpInfo(Emp emp)
    //{
    //    BP.Port.EmpInfo info = new BP.Port.EmpInfo();
    //    info.RetrieveByAttr("FK_Emp", emp.No);

    //    return info;
    //}

}