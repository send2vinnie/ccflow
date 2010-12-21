<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Msg.aspx.cs" 
Inherits="WF_Msg" Title="消息" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<%@ Register src="UC/Msg.ascx" tagname="Msg" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
	<script language="JavaScript" src="../Comm/JScript.js"></script>
		<script language="JavaScript" src="../Comm/Table.js"></script>
		<script language="JavaScript" src="../Comm/ActiveX.js"></script>
		<script language="javascript" for="document" event="onkeydown">
<!--
  if(event.keyCode==13)
     event.keyCode=9;
-->
		</script>
		<script language="JavaScript" src="Flow.js"></script>
		<script language=javascript>
		function Open( fk_flow )
		{
		   window.open('MyFlow.aspx?FK_Flow='+fk_flow+'&IsClose=1' , 'f' + fk_flow + '',  'width=700,top=100,left=200,height=400,scrollbars=yes,resizable=yes,toolbar=false,location=false' );
		}
		</script>
		
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <uc2:Msg ID="Msg1" runat="server" />

</asp:Content>

