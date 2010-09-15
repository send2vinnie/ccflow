<%@ Page language="c#" Inherits="BP.Web.SignInOfRe" CodeFile="SignInOfRe.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>系统登录</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Comm/Style.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../../Comm/JScript.js"></script>
		<script language=javascript >
		 function ReLoad()
         {
             var myParent, i ; 
            myParent = window.parent;
            if ( myParent == null ) 
               return;
            if ( myParent.frames == null )
               return;
               
            for ( i = 0; i< myParent.frames.length; i++ )
            {
               if ( window.parent.frames(i).name=='mainfrm')
                   continue;
               window.parent.frames(i).location.reload();
            }
          }
        </script>

	</HEAD>
	<body  onunload="javascript:ReLoad();"   >
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<br>
				<br>
				<TABLE id="Table2" style="WIDTH: 544px; HEIGHT: 160px" cellSpacing="1" cellPadding="1"
					width="544" border="1">
					<TR>
						<TD></TD>
					</TR>
					<TR>
						<TD>
							<TABLE id="Table1" align="center" cellSpacing="1" cellPadding="1" width="306" border="1"
								style="WIDTH: 306px; HEIGHT: 94px">
								<TR>
									<TD style="WIDTH: 51px" rowSpan="4"><IMG src="ChangeUser_5.ico"></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 70px; HEIGHT: 23px">用户名:</TD>
									<TD style="HEIGHT: 23px">
										<asp:TextBox id="TB_No" runat="server"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 70px; HEIGHT: 14px">密&nbsp; 码:</TD>
									<TD style="HEIGHT: 14px">
										<asp:TextBox id="TB_Pass" runat="server" TextMode="Password"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 70px"></TD>
									<TD>
										<asp:Button id="Btn1" runat="server" Text=" 登 录 " onclick="Btn1_Click"></asp:Button></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD></TD>
					</TR>
				</TABLE>
				<br>
				<br>
			</FONT>
		</form>
	</body>
</HTML>
