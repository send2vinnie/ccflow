<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KMGuide.aspx.cs" Inherits="FAQ_KMGuide" %>
<%@ Register src="../UC/Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head  runat="server">
<base target = "_self" \>
    <title>无标题页</title>
    <link href="../Style/css/import.css" rel="stylesheet" type="text/css" />
<link href="../Style/css/openwindow.css" rel="stylesheet" type="text/css" />
    </head>
<body>
    <form id="form1" runat="server">
    <div>
           <uc1:Pub ID="Pub1" runat="server" />
    </div>
    </form>
</body>
</html>
