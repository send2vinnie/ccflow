<%@ page language="c#" inherits="BP.Web.SignInOfRe, App_Web_sd4z43pd" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SignInOfRe</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<!--<LINK href="../../Comm/Style.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../../Comm/JScript.js"></script>--><LINK href="Style/re_style.css" type="text/css" rel="stylesheet">
		<script language="javascript">
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
	<body onunload="javascript:ReLoad();">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体"></FONT><FONT face="宋体"></FONT><FONT face="宋体"></FONT><FONT face="宋体">
			</FONT><FONT face="宋体"></FONT><FONT face="宋体"></FONT><FONT face="宋体"></FONT><FONT face="宋体">
			</FONT><FONT face="宋体"></FONT><FONT face="宋体"></FONT><FONT face="宋体"></FONT><FONT face="宋体">
			</FONT><FONT face="宋体"></FONT><FONT face="宋体"></FONT><FONT face="宋体"></FONT><FONT face="宋体">
			</FONT><FONT face="宋体"></FONT><FONT face="宋体"></FONT><FONT face="宋体"></FONT><FONT face="宋体">
			</FONT><FONT face="宋体"></FONT><FONT face="宋体"></FONT><FONT face="宋体"></FONT><FONT face="宋体">
			</FONT><FONT face="宋体"></FONT><FONT face="宋体"></FONT>
			<div id="LoginBox-title"></div>
			<table id="Table2" style="WIDTH: 500px; HEIGHT: 260px" height="152" cellSpacing="1" cellPadding="1"
				align="center" border="0" background="Images/re_LogOn.gif">
				<tr>
					<td>
						<table id="Table3" width="480" border="0" style="WIDTH: 480px; HEIGHT: 40px">
							<tr>
								<td height="40"><FONT face="宋体"></FONT></td>
							</tr>
						</table>
						<TABLE id="Table1" style="WIDTH: 198px; HEIGHT: 151px" cellSpacing="1" cellPadding="1"
							width="198" align="center" border="0">
							<TR> <!---->
								<TD align="right" width="54" height="33">用户名:</TD>
								<TD><LABEL><asp:textbox id="TB_No" onMouseOver="this.style.borderColor='#9ecc00'" onMouseOut="this.style.borderColor='#84a1bd'"
											runat="server" Width="128px" Height="23px"></asp:textbox></LABEL></TD>
							</TR>
							<TR>
								<TD align="right" height="33">密&nbsp;&nbsp;&nbsp;&nbsp;码:</TD>
								<TD><asp:textbox id="TB_Pass" onMouseOver="this.style.borderColor='#9ecc00'" onMouseOut="this.style.borderColor='#84a1bd'"
										runat="server" Width="128px" TextMode="Password" Height="23px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD height="36">&nbsp;</TD>
								<TD align="left">
									<TABLE id="Btn1-border" onmouseover="this.style.borderColor='#00FF00'" onmouseout="this.style.borderColor='#D2E1EE'"
										cellSpacing="0" cellPadding="0" border="0">
										<TR>
											<TD><asp:button id="Btn1" runat="server" Text="&nbsp;&nbsp;确&nbsp;&nbsp;&nbsp;认&nbsp;" onclick="Btn1_Click"></asp:button>
                                                 </TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD id="LoginBox-title2" align=center colSpan="2"><FONT face="宋体"></FONT></TD>
								
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr>
				</tr>
			</table>
<!--
<b>测试人员</b>

<HR>
省级人员:
sjz 省局长审批<BR>

zgcsp 征管处审批<BR>
zgcsl 征管处受理<BR>

szcsp 税政处审批<BR>
szcsl 税政处受理<BR>

fgcsp 法规处审批<BR>
fgcsl 法规处受理<BR>

<hr>
区县市人员

02101 邢益富 (征收人员)<BR>
xjz  区县市局长审批<BR>
zgsp 征管税审批<BR>
zgsl 征管受理<BR>

szsp 税政审批<BR>
szsl 税政受理<BR>

dtzr 大厅主任<BR>
<hr>
文昌地方税务局清澜税务所
02141	周乃均<BR>
02147	符小燕<BR>
02142	黄山 (所长)<BR>
02145	潘少波<BR>
02146	庄燕<BR>
02143	梁彩萍<BR>
02144	林方庄<BR>
02148	吕烈辉<BR>

<hr>
028888 信息管理员<BR>
admin  超级用户<BR>
-->
		</form>
	</body>
</HTML>
