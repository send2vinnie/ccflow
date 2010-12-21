<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SFTable.aspx.cs" Inherits="Comm_MapDef_SFTable" %>
<%@ Register Src="../UC/ucsys.ascx" TagName="ucsys" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title runat=server />
    <link href="../../../Comm/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../../../Comm/Style/Table.css" rel="stylesheet" type="text/css" />
    
		<script language="JavaScript" src="JS.js"></script>
    
    <script language=javascript>
    /* ESC Key Down  */
    function Esc()
    {
        if (event.keyCode == 27)
            window.close();
       return true;
    }
    </script>
    <base target=_self /> 
</head>
<body topmargin="0" leftmargin="0" onkeypress="Esc()" onload="RSize()"  >
    <form id="form1" runat="server">
    <div align=center>
        <uc1:ucsys ID="Ucsys1" runat="server" />
        </div>
    </form>
</body>
</html>
