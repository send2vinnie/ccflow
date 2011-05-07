<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelfWFRpt.aspx.cs" Inherits="WF_SelfRpt" %>

<%@ Register Src="../Comm/UC/ucsys.ascx" TagName="ucsys" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>自定义报表</title>
    <LINK href="../Comm/Style.css" type="text/css" rel="stylesheet">
		<LINK href="../Comm/Table.css" type="text/css" rel="stylesheet">
		<LINK href="../Comm/Style.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../Comm/JScript.js"></script>
		<script language="JavaScript" src="../Comm/ActiveX.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td style="height: 1px">
                    <uc1:ucsys ID="Ucsys1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <uc1:ucsys ID="Ucsys2" runat="server" />
                </td>
            </tr>
        </table>
    
    </form>
</body>
</html>
