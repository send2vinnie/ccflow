using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CCOA_Control_XPager : System.Web.UI.UserControl
{
    public delegate void RefreshEventHandler(object sender, CurrentPageEventArgs e);

    public event RefreshEventHandler OnPagerChanged;
    
    //当前页
    public int CurrentPage
    {
        get { return ViewState["cPage"] == null ? 1 : Convert.ToInt32(ViewState["cPage"]); }
        set { ViewState["cPage"] = value; }
    }
    //全部记录条数
    public int RecordCount
    {
        get { return ViewState["rCount"] == null ? 0 : Convert.ToInt32(ViewState["rCount"]); }
        set { ViewState["rCount"] = value; }
    }
    //每页显示记录数量
    public int PageSize 
    {
        get { return ViewState["pgSize"] == null ? 0 : Convert.ToInt32(ViewState["pgSize"]); }
        set { ViewState["pgSize"] = value; }
    }
    //共有多少页
    public int PageCount
    {
        get { return ViewState["pgCount"] == null ? 0 : Convert.ToInt32(ViewState["pgCount"]); }
        set { ViewState["pgCount"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void InitControl()
    {
        this.InitControl(this.PageSize, this.RecordCount);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pageSize">每页显示记录数量</param>
    /// <param name="recordCount">全部记录条数</param>
    public void InitControl(int pageSize, int recordCount)
    {
        this.PageSize = pageSize == 0 ? 10 : pageSize;
        this.RecordCount = recordCount;

        PageCount = RecordCount / PageSize;
        if (RecordCount % PageSize > 0)  //判断余数是否大于0
            PageCount = PageCount + 1;

        if (PageCount == 0 && recordCount > 0)
            PageCount++;

        this.InitButton(); // 初始化所有按钮
    }

    private void InitButton()
    {
        lbnFirstPage.Enabled = true;
        lbnPrevPage.Enabled = true;
        lbnNextPage.Enabled = true;
        lbnLastPage.Enabled = true;
        goPage.Enabled = true;

        if (PageCount <= 1)                 //只有一页或者小于一页
        {
            lbnFirstPage.Enabled = false;
            lbnPrevPage.Enabled = false;
            lbnNextPage.Enabled = false;
            lbnLastPage.Enabled = false;
            goPage.Enabled = false;
        }
        else
        {
            if (CurrentPage == PageCount)       //当前页数等于最大页数
            {
                lbnNextPage.Enabled = false;
                lbnLastPage.Enabled = false;
            }

            if (CurrentPage == 1)                   //当前页数等于1的时候
            {
                lbnPrevPage.Enabled = false;
                lbnFirstPage.Enabled = false;
            }
        }
        if (PageCount == 0)                     //如果总页数为0则没有一条记录，当前页数也应为0
            CurrentPage = 0;
        lblCurrentPage.Text = CurrentPage.ToString();
        lblPageCount.Text = PageCount.ToString();
        lblRecordCount.Text = RecordCount.ToString();
    }

    public void Bind()
    {
        PagerChange(new CurrentPageEventArgs(CurrentPage, PageSize));
    }

    protected virtual void PagerChange(CurrentPageEventArgs e)
    {
        InitButton();
        if (OnPagerChanged != null)
        {
            OnPagerChanged(this, e);
        }
    }
    protected void lbnFirstPage_Click(object sender, EventArgs e)
    {
        CurrentPage = 1;
        PagerChange(new CurrentPageEventArgs(CurrentPage, PageSize));   
    }
    protected void lbnPrevPage_Click(object sender, EventArgs e)
    {
        CurrentPage = CurrentPage > 1 ? CurrentPage - 1 : CurrentPage;
        PagerChange(new CurrentPageEventArgs(CurrentPage, PageSize));  

    }
    protected void lbnNextPage_Click(object sender, EventArgs e)
    {
        CurrentPage = CurrentPage < PageCount ? CurrentPage + 1 : CurrentPage;
        PagerChange(new CurrentPageEventArgs(CurrentPage, PageSize));  
    }
    protected void lbnLastPage_Click(object sender, EventArgs e)
    {
        CurrentPage = PageCount;
        PagerChange(new CurrentPageEventArgs(CurrentPage, PageSize));  
    }
    protected void goPage_Click(object sender, EventArgs e)
    {
        int search = 1;
        if (int.TryParse(SearchIndex.Text.Trim(),out search))
        {
            search = Convert.ToInt32(Math.Abs(Convert.ToInt32(SearchIndex.Text.Trim())));
            if (search >= 1 && search <= PageCount)
            { 
                CurrentPage = search;
            }
            if (search <= 1)
            {
                CurrentPage = 1;
                SearchIndex.Text = "1";
            }
            if (search >= PageCount)
            {
                CurrentPage = PageCount;
                SearchIndex.Text = PageCount.ToString();
            }
            PagerChange(new CurrentPageEventArgs(CurrentPage, PageSize));
        }
        else
        { return; }
    }
}


public class CurrentPageEventArgs : EventArgs
{
    public readonly int currentPage;
    public readonly int pageSize;
    public CurrentPageEventArgs(int currentPage, int pageSize)
    {
        this.currentPage = currentPage;
        this.pageSize = pageSize;
    }
}