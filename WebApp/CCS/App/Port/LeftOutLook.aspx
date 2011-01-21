<%@ Page language="c#" Inherits="BP.Web.WF.Port.LeftTree" CodeFile="LeftOutLook.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="UCSys" Src="../../Comm/UC/UCSys.ascx" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>LeftTree</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="LeftOutlook.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="LeftOutlook.js"></script>
		<STYLE type=text/css>
A:link {
	COLOR: #636363
}
A.ff {
	FONT-SIZE: 12px; TEXT-DECORATION: none}
A:visited {
	COLOR: #999999
}
A:hover {
	COLOR: #cc0033
}

.Title
{
 
 
}
</STYLE>		
	</HEAD>
	<body topmargin="0" leftmargin="0"  class="Body"    >
		<form id="Form1" method="post" runat="server">
		 <uc1:UCSys id="UCSys1"  runat="server"></uc1:UCSys>
							<iewc:TreeView id="TreeView1"  runat="server"></iewc:TreeView>
		</form>
	</body>
</HTML>
