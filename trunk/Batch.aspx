<%@ Page Language="C#" MasterPageFile="~/Comm/MasterPage.master" AutoEventWireup="true"
    CodeFile="Batch.aspx.cs" Inherits="Comm_Batch" Title="批处理" %>
<%@ Register Assembly="BP.Web.Controls" Namespace="BP.Web.Controls" TagPrefix="cc1" %>
<%@ Register Src="UC/UCSys.ascx" TagName="UCSys" TagPrefix="uc1" %>
<%@ Register src="UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  
    <style type="text/css">
        .Style1
        {
            width: 100%;
        }
    </style>
    
    <LINK href="Table<%=BP.Web.WebUser.Style%>.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="JScript.js"></script>
		<script language="JavaScript" src="ActiveX.js"></script>
		<script language="JavaScript" src="Menu.js"></script>
    <script language="javascript">
    
      function ShowEn(url, wName, h, w )
        {
           var s = "dialogWidth=" + parseInt(w) + "px;dialogHeight=" + parseInt(h) + "px;resizable:yes";
           var  val=window.showModalDialog( url,null,s);
           window.location.href=window.location.href;
        }
        
　　 function selectAll()
　　 {
　　   var arrObj=document.all;
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
　　 	function OpenAttrs(ensName)
		{
	       var url= './Sys/EnsAppCfg.aspx?EnsName='+ensName;
           var s =  'dialogWidth=680px;dialogHeight=480px;status:no;center:1;resizable:yes'.toString() ;
		   val=window.showModalDialog( url , null ,  s);
           window.location.href=window.location.href;
		}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table id="Table1" align="left" class="Table" cellspacing1 width='40%'>
        <tr>
         <td>
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td Class='ToolBar' >
                <uc2:ToolBar ID="ToolBar1" runat="server" />
                </td>
        </tr>
                
                <tr>
         <td>
                <uc1:UCSys ID="UCSys1" runat="server" />
            </td>
        </tr>
        <tr class='TRSum'>
            <td>
                <uc1:UCSys ID="UCSys3" runat="server" />
            </td>
        </tr>
        <tr>
            <td  Class='ToolBar' >
                    <uc1:UCSys ID="UCSys2" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
