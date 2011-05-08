<%@ Register TagPrefix="cc1" Namespace="BP.Web.Controls" Assembly="BP.Web.Controls" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ page language="c#" inherits="BP.Web.Comm.UI.UIEnDtl, App_Web_55maidti" %>
<%@ Register Assembly="BP.Web.Controls" Namespace="BP.Web.Controls" TagPrefix="cc1" %>
<%@ Register Src="UC/ucsys.ascx" TagName="ucsys" TagPrefix="uc1" %>
<%@ Register src="UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
		<Meta http-equiv="Page-Enter" Content="revealTrans(duration=0.5, transition=8)" />
		<LINK href="Menu.css" type="text/css" rel="stylesheet" />
		<LINK href="./Style/Table.css" type="text/css" rel="stylesheet" />
		<script language="JavaScript" type="text/javascript" src="JScript.js"></script>
		<script language="JavaScript" type="text/javascript" src="./JS/Calendar/WdatePicker.js"></script>
		<script language="JavaScript" type="text/javascript" src="ShortKey.js"></script>
		<base target=_self />
    <style type="text/css">
        .Style1
        {
            width: 100%;
        }
    </style>
</head>
<body topmargin="0" leftmargin="0" onkeypress="Esc()" onload="RSize();"> 
    <form id="aspnetForm" runat="server" name="aspnetForm" >
    <table    id="Table1" align="left"  width="100%" >
    <tr>
    <td class="toolbar" >
        <uc2:ToolBar ID="ToolBar1" runat="server" />
    </td>
    </tr>
     <tr>
    <td>
        <uc1:ucsys ID="ucsys1" runat="server" />
        <uc1:ucsys ID="ucsys2" runat="server" />
    </td>
    </tr>
    </table>
    </form>
</body>
</html>