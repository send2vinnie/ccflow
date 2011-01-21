<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Result.aspx.cs" Inherits="GE_PJ_Result" %>

<%@ Register Src="../Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<html>
<head id="Head1" runat="server">
    <title>投票结果</title>
    <link href="../../Comm/Table2.css" rel="stylesheet" type="text/css" />
    <% Response.Expires = -1; %>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%; text-align: center">
    <uc1:Pub ID="Pub1" runat="server" />
       <%-- <input id="Button1" type="button" value=" 关 闭 " onclick="window.close()" />--%>
    </div>
    </form>
</body>
</html>
