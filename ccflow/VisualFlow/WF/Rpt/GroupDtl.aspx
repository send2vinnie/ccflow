<%@ Register TagPrefix="uc1" TagName="UCSys" Src="../../Comm/UC/UCSys.ascx" %>
<%@ Page language="c#" Inherits="BP.Web.WF.Comm.UIContrastDtl" CodeFile="GroupDtl.aspx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="BP.Web.Controls" Assembly="BP.Web.Controls" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<!DocType HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>��ϸ</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JavaScript" src="../../Comm/Menu.js"></script>
		<script language="JavaScript" src="../../Comm/JScript.js"></script>
		<LINK href="../../Comm/Style/Table0.css" type="text/css" rel="stylesheet">
		 <script language="javascript">
    function ShowEn(url, wName)
    {
       val=window.showModalDialog( url , wName ,'dialogHeight: 550px; dialogWidth: 650px; dialogTop: 100px; dialogLeft: 150px; center: yes; help: no'); 
     // window.location.href=window.location.href;
    }
    </script>
    <base target=_self />
	</HEAD>
	<body  onkeypress=Esc() leftMargin=0    topMargin=0 >
		<form id="Form1" method="post" runat="server">
				<TABLE id="Table1"  width="98%" border="0">
					<TR>
                    <TD class=Toolbar><asp:Label id="Label1" runat="server">Label</asp:Label></TD>
					</TR>
					<TR>
						<TD>
							<uc1:UCSys id="UCSys1" runat="server"></uc1:UCSys></TD>
					</TR>
				</TABLE>
		</form>
	</body>
</HTML>
