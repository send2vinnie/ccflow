<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserInfo.aspx.cs" Inherits="EIP_UserInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 98px;
            font-size: 12px;
            line-height: 18px;
        }
        .style2
        {
            width: 165px;
            font-size: 12px;
            line-height: 18px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td class="style1">
                    当前用户：
                </td>
                <td class="style2">
                    <%=BP.Web.WebUser.Name%>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    姓名：
                </td>
                <td class="style2">
                    <%=BP.Web.WebUser.Name %>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    职位：
                </td>
                <td class="style2">
                    <%--<%=BP.Web.WebUser.HisStation.Name %>--%>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
