<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AutoFill.aspx.cs" Inherits="Comm_MapDef_AutoFill" %>
<%@ Register src="../UC/Pub.ascx" tagname="Pub" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>自动完成</title>
    <link href="../../Comm/Style/Table.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
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
<body onkeypress="Esc()"  onload="RSize()"  >
    <form id="form1" runat="server">
    <div align=center >
        <uc1:Pub ID="Pub1" runat="server" />
    </div>
    </form>
</body>
</html>
