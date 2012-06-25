using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BP.EIP;

/// <summary>
///XAuthTool 的摘要说明
/// </summary>
public class XAuthTool
{
    public static string GetAuthNameByAuthId(string authId, string accessType)
    {
        switch (accessType)
        {
            case "部门":
                Port_Dept portDept = new Port_Dept(authId);
                return portDept.Name;
            case "角色":
                Port_Station portStation = new Port_Station(authId);
                return portStation.Name;
            case "人员":
                Port_Emp portEmp = new Port_Emp(authId);
                return portEmp.Name;
            default:
                return string.Empty;
        }
    }
}