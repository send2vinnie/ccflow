<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dtl.aspx.cs" Inherits="Comm_Dtl" %>
<%@ Register Assembly="BP.Web.Controls" Namespace="BP.Web.Controls" TagPrefix="cc1" %>
<%@ Register Src="UC/ucsys.ascx" TagName="ucsys" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
		<Meta http-equiv="Page-Enter" Content="revealTrans(duration=0.5, transition=8)" />
		<LINK href="./Style/Table.css" type="text/css" rel="stylesheet" />
		<script language="javascript" >
		    var isChange = false;
		    function SaveDtlData() {

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
        
        body
        {
        	font-size:smaller;
        }
    </style>
		<script language="JavaScript" src="./Style/JScript.js"></script>
    <script language="JavaScript" src="../../Comm/JS/Calendar.js" type="text/javascript"></script>    
</head>
<body topmargin="0" leftmargin="0" onkeypress="Esc()" > 
    <form id="form1" runat="server">
    <div id='P'  >
     <uc1:ucsys ID="Ucsys1" runat="server" />
        <uc1:ucsys ID="Ucsys2" runat="server" />
     <asp:Button ID="Button1" runat="server" Text=""  CssClass="HBtn" Visible=true
         onclick="Button1_Click" />
         </div>
    </form>
</body>
</html>