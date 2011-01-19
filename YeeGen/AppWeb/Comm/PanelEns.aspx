<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Page language="c#" Inherits="BP.Web.Comm.PanelEns" CodeFile="PanelEns.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="UCSys" Src="UC/UCSys.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="BP.Web.Controls" Assembly="BP.Web.Controls" %>
<%@ Register src="UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc2" %>
<!DocType HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>感谢您选择:<%=BP.SystemConfig.DeveloperShortName%>
			-> 信息列表</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<Meta http-equiv="Page-Enter" Content="revealTrans(duration=0.5, transition=8)">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="./Style/Table.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="JScript.js"></script>
		<script language="JavaScript" src="ActiveX.js"></script>
		<script language="JavaScript" src="Menu.js"></script>
		<script language="JavaScript" src="ShortKey.js"></script>
		<script language="javascript">
		function ShowEn(url, wName, h, w )
        {
           var s = "dialogWidth=" + parseInt(w) + "px;dialogHeight=" + parseInt(h) + "px;resizable:yes";
           var  val=window.showModalDialog( url,null,s);
           window.location.href=window.location.href;
        }
        
		function ImgClick()
		{
		}
		
		function OpenAttrs(ensName)
		{
	       var url= './Sys/EnsAppCfg.aspx?EnsName='+ensName;
           var s =  'dialogWidth=680px;dialogHeight=480px;status:no;center:1;resizable:yes'.toString() ;
		   val=window.showModalDialog( url , null ,  s);
           window.location.href=window.location.href;
		}
		</script>
	</HEAD>
	<body   onkeypress="Esc()"   onkeydown='DoKeyDown();' topmargin="0" leftmargin="0" >
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table" align="left" cellSpacing="1" cellPadding="1" border="0" width="100%">
				<TR>
					<TD height="0" >
						<asp:Label id="Label1" runat="server">Label</asp:Label>
					</TD>
				</TR>
				<TR>
					<TD class="toolbar" >
                        <uc2:ToolBar ID="ToolBar1" runat="server" />
                    </TD>
				</TR>
				<TR align="justify" height="0px">
					<TD>
						<uc1:UCSys id="UCSys1" runat="server"></uc1:UCSys>
					</TD>
				</TR>
				<TR>
					<TD Class="toolbar" ><FONT face="宋体" size=2>
							<uc1:UCSys id="UCSys2" runat="server"></uc1:UCSys></FONT>
					</TD>
				</TR>
			</TABLE>
			 
		</form>
	</body>
</HTML>
