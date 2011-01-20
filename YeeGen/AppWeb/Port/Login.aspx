<%@ Register TagPrefix="uc1" TagName="UCTop" Src="UCComm/UCTop.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCEnd" Src="UCComm/UCEnd.ascx" %>
<%@ Page language="c#" Inherits="BP.YG.Login" CodeFile="Login.aspx.cs" CodeFileBaseClass="BP.YG.YGPage" %>
<%@ Register TagPrefix="uc1" TagName="UCPub" Src="UCComm/UCPub.ascx" %>
<!DocType HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>	<%=BP.YG.Global.BureauName%> - 登录</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/Style/Glo.css" type="text/css" rel="stylesheet">
		<LINK href="/Style/Table.css" type="text/css" rel="stylesheet">
		<LINK href="/Style/Style<%=BP.YG.Global.Style%>.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="/Style/Comm.js"></script>
		
		
	</HEAD>
	<body leftMargin="0" topMargin="0"  >
				<FORM id="Form1" method="post" runat="server">
						<TABLE id="Table1"  cellSpacing="1" class=Table  cellPadding="1"   border="0"
							align="center">
							<TR>
								<TD colspan="2" class="TD" >
									<uc1:UCTop id="UCTop1" runat="server"></uc1:UCTop></TD>
							</TR>
							<TR  >
								<TD height='100%' valign="top" align=right  style="PADDING-RIGHT: 20px; PADDING-LEFT: 20px; MARGIN-LEFT: 20px; MARGIN-RIGHT: 20px">
										<TABLE   height='99%' class=TableGreen   cellSpacing="1" cellPadding="1"  border="0" align="center"  >
											<TR>
												<TD class="TitleGreen" >服务介绍</TD>
											</TR>
											<TR>
												<TD class='BigDoc' valign=top >
													<P>&nbsp;&nbsp;&nbsp; 本网站属于会员积分制，积分是根据您对网站贡献和对网友的帮助自动累计。</P>
													<P>&nbsp;&nbsp;&nbsp;&nbsp;用积分，您可以购买本网站的纪念品，关于积分规则请参考<A href="AboutCent.htm" target=_blank >积分规则</A>。&nbsp;</P>
													<P>&nbsp;&nbsp;&nbsp;&nbsp;<STRONG>加入系统您可以获取如下服务。</STRONG></p>
													<ul class=UL>
													<li>&nbsp;1，您可以建立您自己的blog，展示您的风采。 </li>
													<li>&nbsp;2，您可以发表文章表达自己的观点、见解，获取积分。 </li>
													<li>&nbsp;3，在您遇到困难时，您可以提出向网管、财税顾问、其它网友提出问题。想心大家会给您一个很好的答案。您可以回答别人的问题获取积分。</li>
													<li>&nbsp;4，您可以发表贴子，展示自己的观点。 </li>
													<li>&nbsp;5，您可以下载税务、稽查、财务、办公、企业管理等等方面的资料。 </li>
													<li>&nbsp;6，如果您是税务机关的科（处）负责人，您可以创建您自己的主页。可以轻松拥有自己的单位的网站，二级域名。</li>
													</ul>
													<P>&nbsp;&nbsp;&nbsp;<b> 祝您有一个很好的收获。</b></P>
													 
												</TD>
											</TR>
											<TR>
												<TD></TD>
											</TR>
										</TABLE>
									 
									<P><STRONG> &nbsp;&nbsp;&nbsp;&nbsp;</STRONG></P>
								</TD>
								<TD width='50%'   valign="top"  align=left class="BigDoc">
								
								<table border=1 cellpadding='0' bgcolor=InfoBackground width='90%'  >
								<TR>
								<TD class=BigDoc >
									<BR>
									<P>用户名：
										<asp:TextBox id="TB_No" runat="server" Width="104px"></asp:TextBox></P>
									<P>密&nbsp;码：
										<asp:TextBox id="TB_Pass" runat="server" Width="104px" TextMode="Password"></asp:TextBox></P>
									<P>
										<asp:CheckBox id="CheckBox1" runat="server" Text="记住我的用户与密码." Checked="True"></asp:CheckBox></P>
									<P>
										<asp:Button id="Button1"   runat="server" Text=" 登 录 " onclick="Button1_Click"></asp:Button><BR>
										<hr>
										<asp:Label id="Label1" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
										<ul class=UL>
										<li>
										<A href="RegUser.aspx?B=<%=BP.YG.Global.BureauNo %>&WhereGo=<%=BP.YG.Global.GoWhere%>" ><font color=green><b>注册新用户(20秒快速注册)</b></font></A></li>
										<li><A href="RequestMyPass.aspx">找回密码了</A></li>
										<li><A href="Home.aspx?B=<%=BP.YG.Global.BureauNo%>" >返回</A></li>
										</ul>
									 </TD>
									 </TR>
									 
									 <TR>
									  <TD class=TitleGreen bgcolor=Window >
									  注册立刻享有
									  </TD>
									 </TR>
									 
									 <TR>
									  <TD class=BigDoc bgcolor=Honeydew >
									    <ul class=UL>
									    <li>建博客http://yourname.space.caishui800.cn</li>
									    <li>最新的财税咨讯</li>
									    <li>在线资料库</li>
									    <li>共享文件</li>
									    <li>论坛</li>
									    <li>与专家交流面对面</li>
									    </ul>
									  </TD>
									 </TR>
									 </Table>
								</TD>
							</TR>
							
							<TR>
							<TD  class='TDEnd'  colspan=2>
							<uc1:UCEnd id="UCEnd1"  runat="server"></uc1:UCEnd>
							</TD>
							</TR>
							
						</TABLE>
				</FORM>
	</body>
</HTML>
