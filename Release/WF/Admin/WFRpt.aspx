﻿<%@ page language="C#" autoeventwireup="true" inherits="WF_Admin_View, App_Web_0lc3lnp4" %>
<%@ Register Src="../../Comm/UC/ucsys.ascx" TagName="ucsys" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link href="../Style/Table.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../../Comm/JScript.js"></script>
</head>
<body class="Body<%=BP.Web.WebUser.Style%>"   leftMargin=0  topMargin=0>
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;
            height: 100%">
            <tr>
                <td valign=top>
                   <uc2:ucsys ID="Ucsys1" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
