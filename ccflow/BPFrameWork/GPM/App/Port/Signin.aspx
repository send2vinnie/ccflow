<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="cc1" Namespace="BP.Web.Controls" Assembly="BP.Web.Controls" %>
<%@ Page language="c#" Inherits="BP.Web.SignInPG" CodeFile="Signin.aspx.cs" %>
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
             help.innerHTML="<BR><b>1)</b>��ϵͳ������ϵͳ���ܽ��,�����ϵͳ���û�����½.";
             help.innerHTML+="<BR><b>2)</b>��������״ε�½����������pub.��½ϵͳ����ע���޸����룮Ϊ�˱�֤������ݰ�ȫ����ע�Ᵽ����Լ������룮";
             help.innerHTML+="<BR><b>3)</b>ʹ�ù����������⣬�뷢����BBS���߷�ӳ��ϵͳ����Ա��";             
             help.innerHTML+="<BR><BR>&nbsp;&nbsp;&nbsp;&nbsp;������죮";
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
     BODY { BACKGROUND-POSITION: center 50%; BACKGROUND-ATTACHMENT: fixed; BACKGROUND-IMAGE: url(CCFlowWelcome.jpg); BACKGROUND-REPEAT: no-repeat }
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
								<td width="30%"><FONT face="����"></FONT></td>
								<TD align="left" width="36%" height='60%'><FONT face="����"></FONT><br>
									<br>
									<cc1:Lab id="Lab1" runat="server">
										<font size="2">&nbsp;&nbsp;<b>�û���</b></font></cc1:Lab>
									<cc1:tb class="BigButton" id="TB_No" style="BACKGROUND-IMAGE: url(beer.gif)" runat="server"
										ShowType="TB" Width="88px" BorderStyle="Solid" BorderWidth="1px"></cc1:tb><br>
									<cc1:Lab id="Lab2" runat="server">
										<font size="2">&nbsp;&nbsp;<b>��&nbsp;&nbsp;&nbsp;&nbsp;��</b></font></cc1:Lab>
									<cc1:tb class="BigButton" id="TB_Pass" runat="server" Width="88px" BorderStyle="Solid" BorderWidth="1px"
										TextMode="Password" BackColor="White"></cc1:tb>
									<cc1:btn id="Btn1" runat="server" BorderStyle="Solid" BorderWidth="1px" BackColor="Azure"
										Text="��¼(O)" BorderColor="Black" onclick="Btn1_Click"></cc1:btn>
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
