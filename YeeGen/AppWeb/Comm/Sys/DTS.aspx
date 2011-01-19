<%@ Page language="c#" Inherits="BP.Web.GS.Comm.Sys.DTS" CodeFile="DTS.aspx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="BP.Web.Controls" Assembly="BP.Web.Controls" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<!DocType HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>数据转换</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Table.css" type="text/css" rel="stylesheet">
		<LINK href="../CSS/Link.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../JScript.js"></script>
	</HEAD>
	<body topmargin="0" leftmargin="0">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<TABLE id="Table1" height="100%" cellSpacing="1" cellPadding="1" width="100%" border="1">
					<TR>
						<TD height="1%">
							<asp:Label id="Label1" runat="server">Label</asp:Label></TD>
					</TR>
					<TR>
						<TD height="1%"><cc1:bptoolbar id="BPToolBar1" runat="server"></cc1:bptoolbar></TD>
					</TR>
					<TR>
						<TD valign="top" height="100%"><cc1:cbl id="CBL1" runat="server" Width="100%"></cc1:cbl></TD>
					</TR>
					<TR>
						<TD height="1%">
                            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                        </TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
