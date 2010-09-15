<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Welcome.aspx.cs" Inherits="Admin_Welcome" %>
<%@ Register src="../Comm/UC/UCSys.ascx" tagname="UCSys" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link href="../Comm/Table2.css" rel="stylesheet" type="text/css" />
    <style>
body {
	margin:3px;
	font-size:12px;
	background:url(style/img/main_bg.jpg) no-repeat right bottom;
	line-height:22px;
}
 
td {
	padding:0px 1px;
	height:25px;
	line-height:25px;
}
.td_title {
	text-align:center;
	background:#d8e9ed url(../img/th.gif) repeat-x bottom;
	font-weight:bolder;
	color:#004c70
}
</style>
</head>
<body>
    <form id="form1" runat="server">
    <table>
    <tr>
     <td>
    <uc2:UCSys ID="UCSys1" runat="server" />
   </td>
   
     <td>
   
    <uc2:UCSys ID="UCSys2" runat="server" />
   </td>
    
    </tr>
    </table>
    </form>
</body>
</html>
