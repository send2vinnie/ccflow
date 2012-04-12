using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class control_XSearch : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.divDateRange.Visible = ShowDateRange;
    }

    public bool ShowDateRange { get; set; }

    public string Caption
    {
        set { this.lblCaption.Text = value; }
    }

    public string StartDateText
    {
        set { this.lblStartDate.Text = value; }
    }

    public string EndDateText
    {
        set { this.lblEndDate.Text = value; }
    }

    public string FilterType
    {
        get
        {
            return this.ddlFilter.SelectedValue;
        }
    }
    public string FilterValue
    {
        get
        {
            return this.txtFilter.Text.Trim();
        }
    }
    public string StartDate
    {
        get
        {
            return this.txtStartDate.Text;
        }
        set { this.txtStartDate.Text = value; }
    }

    public string EndDate
    {
        get
        {
            return this.txtEndDate.Text;
        }
        set { this.txtEndDate.Text = value; }
    }

    public void AddItem(ListItem item)
    {
        this.ddlFilter.Items.Add(item);
    }
    public void InsertItem(int index,ListItem item)
    {
        this.ddlFilter.Items.Insert(index,item);
    }
    public void BindData(object dataSource,string textField,string valueField)
    {
        this.ddlFilter.DataSource = dataSource;
        this.ddlFilter.DataTextField = textField;
        this.ddlFilter.DataValueField = valueField;
        this.DataBind();
    }
    public void ClearItems()
    {
        this.ddlFilter.Items.Clear();
    }
}