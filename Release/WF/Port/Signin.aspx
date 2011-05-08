<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="cc1" Namespace="BP.Web.Controls" Assembly="BP.Web.Controls" %>
<%@ page language="c#" inherits="BP.Web.SignInMain, App_Web_sd4z43pd" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>
			<%=BP.SystemConfig.SysName%>
		</title>
		<meta name="vs_showGrid" content="True">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
		function winfix()
		{
	         if (document.layers) {  
		        width=screen.availWidth - 10;  
		         height=screen.availHeight - 20;  
	          } else {  
		        var width=screen.availWidth - 2;  
		        var height=screen.availHeight;  
	          }  
              self.resizeTo(width, height); 
              self.moveTo(0, 0);
         } 
         var mode;
         mode=1;
         function show(){
         // window.open('Note.htm')
           if (mode==0)
           {
              help.innerHTML="";
              mode=1;
           }
           else 
           { 
             help.innerHTML="<BR><b>1)</b>本系统与主体系统紧密结合,请采用系统的用户名登陆.";
             help.innerHTML+="<BR><b>2)</b>如果您是首次登陆，密码请用pub.登陆系统后请注意修改密码．为了保证你的数据安全，请注意保存好自己的密码．";
             help.innerHTML+="<BR><b>3)</b>使用过程中有问题，请发布到BBS或者反映给系统管理员．";             
             help.innerHTML+="<BR><BR>&nbsp;&nbsp;&nbsp;&nbsp;工作愉快．";
             mode=0;
            }
        }
		function SetHomePage()
		{
		  document.body.style.behavior="url(#default#homepage)";
		  document.body.setHomePage(window.location.href);
		//  document.body.all.homepageLink.click();
		}
		function setFocus(ctl)
		{  
		  if (document.forms[0][ctl] != null)  
		  { 
		    document.forms[0][ctl].focus();
		  }
		}		
		</script>
		<LINK href="../../Comm/Style.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../../Comm/JScript.js"></script>
		<STYLE TYPE="text/css">
     BODY { BACKGROUND-POSITION: center 50%; BACKGROUND-ATTACHMENT: fixed; BACKGROUND-IMAGE: url(Singin.jpg); BACKGROUND-REPEAT: no-repeat }
		</STYLE>
	</HEAD>
	<body bgProperties="fixed" topmargin="0" leftmargin="0" rightmargin="0"
		bottommargin="0" class="Login<%=BP.SystemConfig.SysNo%>"  onload="winfix();"   >
		<form id="SignIn" method="post" runat="server" defaultfocus="TB_No">
			<TABLE id="Table2" height="100%" cellSpacing="1" width="100%" cellPadding="1" border="0"
				align="center">
				<TR>
					<TD align="center" width="70%" height="100%">
						<TABLE align="center" id="Table3" border="0" style="WIDTH: 100%; HEIGHT: 100%">
							<TR>
								<td width="30%"><FONT face="宋体"></FONT></td>
								<TD align="left" width="36%" height='60%'><FONT face="宋体"></FONT><br>
									<br>
										<font size="2">用户名</font>
									<cc1:tb class="BigButton" id="TB_No" style="BACKGROUND-IMAGE: url(beer.gif)" runat="server"
										ShowType="TB" Width="88px" BorderStyle="Solid" BorderWidth="1px"></cc1:tb><br>
										<font size="2">密&nbsp;&nbsp;&nbsp;&nbsp;码</font>
									<cc1:tb class="BigButton" id="TB_Pass" runat="server" Width="88px" BorderStyle="Solid" BorderWidth="1px"
										TextMode="Password" BackColor="White"></cc1:tb>
                                    <asp:DropDownList ID="DDL_Lang" runat="server">
                                        <asp:ListItem Selected="True" Value="CH">简体中文</asp:ListItem>
                                        <asp:ListItem Value="B5">繁体中文</asp:ListItem>
                                        <asp:ListItem Value="EN">英文-English</asp:ListItem>
                                        <asp:ListItem Value="RU">俄语-Русский</asp:ListItem>
                                        <asp:ListItem Value="JP">日本語</asp:ListItem>
                                        
                                    </asp:DropDownList>
									<cc1:btn id="Btn1" runat="server" BorderStyle="Solid" BorderWidth="1px" BackColor="Azure"
										Text="登录(O)" BorderColor="Black" onclick="Btn1_Click"></cc1:btn>
								</TD>
								<td width="33%"></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
