<%@ Register TagPrefix="uc1" TagName="UCSys" Src="../UC/UCSys.ascx" %>
<%@ page language="c#" inherits="BP.Web.Comm.UI.SystemClass, App_Web_kzgztv25" %>
<%@ Register TagPrefix="cc1" Namespace="BP.Web.Controls" Assembly="BP.Web.Controls" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<!DocType HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SystemClass</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Table.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../JScript.js"></script>
		<script language=javascript>
<!--
//-->
</script>
	    <link href="../Style/Table.css" rel="stylesheet" type="text/css" />
	</HEAD>
	<body topmargin="0" leftmargin="0" >
		<form id="Form1" method="post" runat="server" >
			<FONT face="宋体">
				<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="1">
					<TR>
						<TD>
							<asp:Label id="Label1" runat="server">Label</asp:Label></TD>
					</TR>
					<TR>
						<TD>
							<cc1:BPToolBar id="BPToolBar1" Visible=false runat="server"></cc1:BPToolBar></TD>
					</TR>
					<TR>
						<TD valign="top" style="HEIGHT: 429px">
							<uc1:UCSys id="UCSys1" runat="server"></uc1:UCSys></TD>
					</TR>
					<TR>
						<TD>注册系统实体：就是规范化实体描述与物理数据库的对应是否符合，把不符合的修改为符合。</TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
