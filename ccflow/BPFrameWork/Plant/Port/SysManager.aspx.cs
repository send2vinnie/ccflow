using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.DA;

public partial class Port_SysManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.BindDate();
        }
    }

    public void BindDate()
    {
        string sql = "select * from Port_SysInfo order by SysGroup, SysOrder";
        System.Data.DataTable table = new System.Data.DataTable();
        table = DBAccess.RunSQLReturnTable(sql);
        this.grid.DataSource = table;
        this.grid.DataKeyNames = new string[] { "NO" };
        this.grid.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string sysName = this.txtSysName.Text.Trim();
        string sysUrl = this.txtSysUrl.Text.Trim();
        string sysDescription = this.txtSysDescription.Text.Trim();
        string sysOrder = this.txtSysOrder.Text.Trim();
        string sysGroup = this.ddlGroup.SelectedValue;
        string sysGroupName = this.ddlGroup.SelectedItem.Text;

        BP.CCOA.SysInfo sysInfo = new BP.CCOA.SysInfo();
        sysInfo.No = Guid.NewGuid().ToString();
        sysInfo.SysName = sysName;
        sysInfo.SysUrl = sysUrl;
        sysInfo.SysDescription = sysDescription;
        sysInfo.SysOrder = sysOrder;
        sysInfo.SysGroup = sysGroup;
        sysInfo.SysGroupName = sysGroupName;

        sysInfo.Insert();

        this.txtSysName.Text = string.Empty;
        this.txtSysUrl.Text = string.Empty;
        this.txtSysDescription.Text = string.Empty;
        this.txtSysOrder.Text = string.Empty;
        this.BindDate();
    }
    protected void grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = e.Keys[0].ToString();
        string sql = "delete from Port_SysInfo where NO = '" + id + "' ";
        DBAccess.RunSQL(sql);
        this.BindDate();
    }
    protected void grid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.grid.EditIndex = e.NewEditIndex;
        this.BindDate();
    }
    protected void grid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        this.grid.EditIndex = -1;
        this.BindDate();
    }
    protected void grid_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string id = e.Keys[0].ToString();
        BP.CCOA.SysInfo sysInfo = new BP.CCOA.SysInfo();

        string sysName = ((TextBox)this.grid.Rows[e.RowIndex].FindControl("txtSysName")).Text;
        string sysUrl = ((TextBox)this.grid.Rows[e.RowIndex].FindControl("txtSysUrl")).Text;
        string sysDescription = ((TextBox)this.grid.Rows[e.RowIndex].FindControl("txtSysDescription")).Text;
        string sysOrder = ((TextBox)this.grid.Rows[e.RowIndex].FindControl("txtSysOrder")).Text;
        string sysGroup = ((TextBox)this.grid.Rows[e.RowIndex].FindControl("txtSysGroup")).Text;

        sysInfo.No = id;
        sysInfo.SysName = sysName;
        sysInfo.SysUrl = sysUrl;
        sysInfo.SysDescription = sysDescription;
        sysInfo.SysOrder = sysOrder;
        sysInfo.SysGroup = sysGroup;

        sysInfo.Update();
        this.grid.EditIndex = -1;
        this.BindDate();
    }
}