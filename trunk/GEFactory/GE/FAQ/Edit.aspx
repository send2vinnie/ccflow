<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="FAQ_Edit" %>

<%@ Register Src="../UC/Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑内容</title>
    <base target="_self" />
    <link href="../Style/Part.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding-right: 20px">
        <p style="font-weight: bolder; font-size: 13px; margin-bottom: 10px; padding: 20px 0 0 20px;
            color: orange">
            请编辑您的内容:</p>
        <uc1:Pub ID="Pub1" runat="server" />
    </div>
    </form>
</body>
</html>
