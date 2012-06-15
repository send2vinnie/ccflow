using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BP.CCOA;

/// <summary>
///XBindCategoryComboTool 的摘要说明
/// </summary>
public class XBindCategoryComboTool
{
    public static void BindCategory(XCategory category, System.Web.UI.WebControls.DropDownList ddl)
    {
        BP.CCOA.OA_Categorys list = new BP.CCOA.OA_Categorys();
        string strCategory = ((int)category).ToString();
        list.RetrieveByAttr(OA_CategoryAttr.Type, strCategory);
        ddl.DataSource = list;
        ddl.DataTextField = OA_CategoryAttr.CategoryName;
        ddl.DataValueField = OA_CategoryAttr.No;
        ddl.DataBind();
    }
}