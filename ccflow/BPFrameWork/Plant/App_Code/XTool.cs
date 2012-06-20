using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

/// <summary>
///XTool 的摘要说明
/// </summary>
public static class XTool
{
    public static string ConvertBooleanText(object objValue)
    {
        if (objValue == null) return "";
        else
        {
            if (objValue.ToString().Length == 0) return "";
            if (objValue.ToString() == "1")
                return "是";
            else
                return "否";
        }
    }

    public static string DateTimeConvert(DateTime datetime)
    {
        return datetime.ToString("yyyy-MM-dd HH:mm:ss");
    }

    public static string Now()
    {
        return DateTimeConvert(DateTime.Now);
    }

    public static string GetCatelogyByCode(object code)
    {
        try
        {
            BP.CCOA.OA_Category category = new BP.CCOA.OA_Category(code.ToString());
            return category.CategoryName;
        }
        catch
        {
            return string.Empty;
        }
    }

    public static int GetListCount(string listType)
    {
        XmlDocument xml = new XmlDocument();
        string path = HttpContext.Current.Request.ApplicationPath + "/CCOA/Config/desktop.xml";
        xml.Load(path);

        string nodeValue = xml.SelectSingleNode("/" + listType + "/count").Value.ToString();
        if (!string.IsNullOrEmpty(nodeValue))
        {
            return int.Parse(nodeValue);
        }

        return 5;
    }
}