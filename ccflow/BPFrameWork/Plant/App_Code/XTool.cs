﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
}