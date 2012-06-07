<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="CCOA_News_test" %>

<%@ Register Src="../Controls/TreeList.ascx" TagName="TreeList" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:TreeList ID="TreeList1" runat="server" />
    </div>
    </form>
</body>
</html>
