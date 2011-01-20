<%@ Register TagPrefix="uc1" TagName="UCEnd" Src="UCComm/UCEnd.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCTop" Src="UCComm/UCTop.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCMy" Src="UCComm/UCMy.ascx" %>

<%@ Page language="c#" Inherits="HiTax.RequestMyPass" CodeFile="RequestMyPass.aspx.cs" CodeFileBaseClass="BP.YG.YGPage" %>
<!DocType HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>
			<%=BP.YG.Global.BureauName%>
			- 用户注册</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		
		<LINK href="/Style/Glo.css" type="text/css" rel="stylesheet">
		<LINK href="/Style/Table.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="/Style/Comm.js"></script>
		
		<style type="text/css" id='sd'>
		.Note
		{
		  	border-top: inactivecaptiontext 1px solid;
	font: messagebox;
	color: olive;
		}
		</style>
		
	</HEAD>
	<body leftMargin="0" topMargin="0" class="body" >
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" border="0" class='Table' cellSpacing="1" align="center" cellPadding="1"
				width="70%">
				<TR>
					<TD colspan="2">
						<uc1:UCTop id="UCTop1" runat="server"></uc1:UCTop></TD>
				</TR>
				<TR>
					<TD colspan="2" class='TitleGreen'  >
						&nbsp; 请输入下面的信息，以帮助您快速找回密码。 <a href='/Home.aspx?B="+this.BureauNo+"'>返回主页</a> , <a href='Login.aspx' >登录系统</a>
					</TD>
				</TR>
				 
				<TR>
					<TD > &nbsp;&nbsp;&nbsp;&nbsp;用户名: </TD>					 
				</TR>
				<TR>
					<TD>&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox id="TB_No" runat="server"></asp:TextBox></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD   >&nbsp;&nbsp;&nbsp;&nbsp;验证码：</TD>
					<TD></TD>
				</TR>
				<TR>
					<TD>&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox id="TB_YZM" runat="server"></asp:TextBox></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD  ><FONT face="宋体">
							<P>
								&nbsp;&nbsp;<asp:Button id="Button1" runat="server" Text="下一步" onclick="Button1_Click"></asp:Button>&nbsp;&nbsp;&nbsp;
							</P>
							<P>
							<font class=Note >&nbsp;&nbsp;&nbsp;&nbsp;点下一步，系统会自动的把您的密码发送到您的邮箱，并自动转到登录界面。</FONT></font> </TD>
					<TD></TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label1" runat="server" ForeColor="Red"></asp:Label></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 100%"><FONT face="宋体"></FONT></TD>
					<TD style="HEIGHT: 100%"></TD>
				</TR>
				<TR>
					<TD colspan="2" align=center >
						<uc1:UCEnd id="UCEnd1" runat="server"></uc1:UCEnd></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
