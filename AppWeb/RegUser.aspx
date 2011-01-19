<%@ Register TagPrefix="uc1" TagName="UCPub" Src="UCComm/UCPub.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCLinkBar" Src="UCComm/UCLinkBar.ascx" %>
<%@ Page language="c#" Inherits="BP.Web.YG.HiTax.IRegUser" CodeFile="RegUser.aspx.cs" CodeFileBaseClass="BP.YG.YGPage" %>
<%@ Register TagPrefix="uc1" TagName="UCEnd" Src="UCComm/UCEnd.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCMy" Src="UCComm/UCMy.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCTop" Src="UCComm/UCTop.ascx" %>
<!DocType HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>新用户注册</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/Style/Glo.css" type="text/css" rel="stylesheet">
		<LINK href="/Style/Table.css" type="text/css" rel="stylesheet">
		<LINK href="/Style/Style0.css" type=text/css rel=stylesheet >
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
	<body leftMargin="0" topMargin="0"  >
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" class="Table" align="center" width='80%' border=0   >
				 
				<TR>
					<TD colspan="2" class=TD>
						 <uc1:UCTop id="UCTop1" runat="server"></uc1:UCTop></TD>
				</TR>
				 
				<TR   valign="top"  >
					<TD width='30%'  class='BigDoc' valign=top  bgcolor=InfoBackground  >
						<P><B>为什么要成为caishui800.cn 的用户？</B></P>
						<P>&nbsp; 注册caishui800.cn 用户您可以享受如下服务。</P>
						<P>1,&nbsp; 10000 多个财务、税务、办公文件，可供您浏览下载。</P>
						<P>2，您可以回答别人的问题，向别人提问,获取积分，兑换纪念品。</P>
						<P>3，您可以建立自己的blog，展示您的风采。</P>
						<P>4，您可以发表论文，表达自己的观点。</P>
						<P>5，您可以上传自己的文件分享给别人，同时你还可以分享别人上传的文件。</P>
						<P>6, 您可以与财税专家面对面交流，如果您是财税高手，可有幸成为我们财税顾问的成员。</P>
					</TD>
					<TD width='70%' class=BigDoc  >
						<uc1:UCPub id="UCPub1" runat="server"></uc1:UCPub></TD>
				</TR>
				<TR>
					<TD class="BigDoc" colspan="2" align=center >
						<uc1:UCEnd id="UCEnd1" runat="server"></uc1:UCEnd></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
