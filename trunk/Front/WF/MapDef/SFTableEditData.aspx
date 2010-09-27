<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SFTableEditData.aspx.cs" Inherits="Comm_MapDef_SFTableEditData" %>
<%@ Register src="../UC/Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href="../Style/Style.css" rel="stylesheet" type="text/css" />
<link href="../Style/Table.css" rel="stylesheet" type="text/css" />
    <title runat=server />
  <script language="JavaScript" src="JS.js"></script>
    <script language=javascript>
    /* ESC Key Down  */
    function Esc()
    {
        if (event.keyCode == 27)
            window.close();
       return true;
    }
    function Del( refno, pageidx, enpk )
    {
        if (window.confirm('您确定要删除字段['+enpk+']吗？') ==false)
            return ;
        var url='SFTableEditData.aspx?RefNo=' + refno + '&PageIdx='+ pageidx  + '&EnPK=' + enpk ;
        window.location.href=url;
    }
    </script>
    <base target=_self /> 
</head>
<body topmargin="0" leftmargin="0" onkeypress="Esc()" onload="RSize()" >

<form id="form1" runat="server">
<uc1:Pub ID="Pub1" runat="server" />
<uc1:Pub ID="Pub2" runat="server" />
<uc1:Pub ID="Pub3" runat="server" />
</form>  
</body>
</html>



