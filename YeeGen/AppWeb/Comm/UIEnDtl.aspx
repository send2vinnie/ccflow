<%@ Register TagPrefix="cc1" Namespace="BP.Web.Controls" Assembly="BP.Web.Controls" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Page language="c#" Inherits="BP.Web.Comm.UI.UIEnDtl" CodeFile="UIEnDtl.aspx.cs" %>

<%@ Register Assembly="BP.Web.Controls" Namespace="BP.Web.Controls" TagPrefix="cc1" %>
<%@ Register Src="UC/ucsys.ascx" TagName="ucsys" TagPrefix="uc1" %>

<%@ Register src="UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
		<Meta http-equiv="Page-Enter" Content="revealTrans(duration=0.5, transition=8)" />
		<LINK href="Menu.css" type="text/css" rel="stylesheet" />
		<LINK href="./Style/Table.css" type="text/css" rel="stylesheet" />
		<script language="JavaScript" src="JScript.js"></script>
		<script language="JavaScript" src="ShortKey.js"></script>
		
		<base target=_self />
		    <script language="javascript">
　　 function selectAll()
　　 {
　　 
　　 alert('ssssssss');
　　 
//　　  alert('sdsds');
//　　   var arrObj=document.all;
//　　  alert( arrObj );
//　　  alert( document.aspnetForm.checkedAll.checked );
　　  
　　   if(document.aspnetForm.checkedAll.checked)
　　   {
　　     for(var i=0;i<arrObj.length;i++)
　　     {
　　         if(typeof arrObj[i].type != "undefined" && arrObj[i].type=='checkbox') 
　　          {
　　          arrObj[i].checked =true;
　         　 }
　　      }
　　    }else{
　　     for(var i=0;i<arrObj.length;i++)
　　      {
　     　   if(typeof arrObj[i].type != "undefined" && arrObj[i].type=='checkbox') 
　     　    arrObj[i].checked =false;
　     　 }
　　    }
　　 }
    </script>
    <style type="text/css">
        .Style1
        {
            width: 100%;
        }
    </style>
</head>
<body topmargin="0" leftmargin="0" onkeypress="Esc()"> 
    <form id="aspnetForm" runat="server" name=aspnetForm >
    <table  style="width: 100%" id="Table1" align="left" cellSpacing="1" cellPadding="1" border="1" width="100%" >
    <tr>
    <td class="toolbar"  class="toolbar" >
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