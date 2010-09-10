<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dtl.aspx.cs" Inherits="Comm_Dtl" %>
<%@ Register Assembly="BP.Web.Controls" Namespace="BP.Web.Controls" TagPrefix="cc1" %>
<%@ Register src="UC/Pub.ascx" tagname="Pub" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
		<Meta http-equiv="Page-Enter" Content="revealTrans(duration=0.5, transition=8)" />
		<LINK href="./Style/Table.css" type="text/css" rel="stylesheet" />
		<script language="javascript" >
		    var isChange = false;
		    function SaveDtlData() {

   //alert( isChange );
   
		        if (isChange == false)
		            return;
		        var btn = document.getElementById('Button1');
		        btn.click();
		        isChange = false;
		    }
    </script>
    <style type="text/css">
        .HBtn
        {
        	 width:1px;
        	 height:1px;
        	 display:none;
        }
    </style>

	<script language="JavaScript" src="./Style/JScript.js"></script>
    <script language="JavaScript" src="../../Comm/JS/Calendar.js" type="text/javascript"></script>    
</head>
<body topmargin="0" leftmargin="0" onkeypress="Esc()" style="font-size:smaller"> 
    <form id="form1" runat="server">
     <asp:Button ID="Button1" runat="server" Text=""  CssClass="HBtn" Visible=true
         onclick="Button1_Click" />
     <uc2:Pub ID="Pub1" runat="server" />
     <uc2:Pub ID="Pub2" runat="server" />
    </form>
</body>
</html>