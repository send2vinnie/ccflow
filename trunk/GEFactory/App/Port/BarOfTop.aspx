<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BarOfTop.aspx.cs" Inherits="App_Port_BarOfTop" %>

<%@ Register src="../../Comm/UC/UCSys.ascx" tagname="UCSys" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="frameset_style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <uc1:UCSys ID="UCSys1" runat="server" />
    </form>
</body>
</html>
