<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Calendar.aspx.cs" Inherits="EIP_Calendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src='<%=Page.ResolveUrl("../Comm/Scripts/jquery-1.6.2.min.js") %>' type="text/javascript"></script>
    <script src='<%=Page.ResolveUrl("../Comm/Scripts/miniui/miniui.js") %>' type="text/javascript"></script>
    <script src='<%=Page.ResolveUrl("../Comm/Scripts/calendar.js") %>' type="text/javascript"></script>
    <script src='<%=Page.ResolveUrl("../Comm/Scripts/CheckBox.js") %>' type="text/javascript"></script>
    <link href="Style/master.css" rel="stylesheet" type="text/css" />
    <link href="Style/menu.css" rel="stylesheet" type="text/css" />
    <link href="Style/main.css" rel="stylesheet" type="text/css" />
    <link href="Style/control.css" rel="stylesheet" type="text/css" />
    <link href="Style/demo.css" rel="stylesheet" type="text/css" />
    <link href="../Comm/Scripts/miniui/themes/icons.css" rel="stylesheet" type="text/css" />
    <link href="../Comm/Scripts/miniui/themes/default/miniui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function onDateClick(e) {
            window.parent.location.href = "Memo/CalendarMemo.aspx";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="calendar1" class="mini-calendar" value="2011-12-11" showFooter="false" width="90" ondateclick="onDateClick">
        </div>
    </div>
    </form>
</body>
</html>
