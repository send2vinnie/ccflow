using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
///BasePage 的摘要说明
/// </summary>
public class BasePage : BP.Web.WebPage
{
	public BasePage()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    private string GetSelIDlist(GridView gridView)
    {
        string idlist = "";
        bool BxsChkd = false;
        for (int i = 0; i < gridView.Rows.Count; i++)
        {
            CheckBox ChkBxItem = (CheckBox)gridView.Rows[i].FindControl("DeleteThis");
            if (ChkBxItem != null && ChkBxItem.Checked)
            {
                BxsChkd = true;
                //#warning 代码生成警告：请检查确认Cells的列索引是否正确
                if (gridView.DataKeys[i].Value != null)
                {
                    idlist += gridView.DataKeys[i].Value.ToString() + ",";
                }
            }
        }
        if (BxsChkd)
        {
            idlist = idlist.Substring(0, idlist.LastIndexOf(","));
        }
        return idlist;
    }

    protected int m_PageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["PageSize"].ToString());

    public virtual void BindDropDownList(){}
}