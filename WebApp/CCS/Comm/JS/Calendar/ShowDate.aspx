<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowDate.aspx.cs" Inherits="YCJY_My97DatePicker_ShowDate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

<script type="text/javascript" src="WdatePicker.js"></script>


    <title>日历控件</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtDate" runat="server" CssClass="Wdate" onFocus="new WdatePicker(this,'%Y-%M-%D %h:%m:%s',true,'default')"></asp:TextBox>
    </div>
    </form>
</body>
</html>
