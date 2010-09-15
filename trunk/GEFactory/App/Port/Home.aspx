<%@ Page language="c#" CodeFile="Home.aspx.cs" AutoEventWireup="false" Inherits="BP.Web.WF.Port.Home" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TITLE>感谢您选择时代智囊通用考试系统V1.0</TITLE>
		<LINK href="/BaseStyleSheet.css" type="text/css" rel="stylesheet">
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Comm/Style.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		function logout()
		{
		   window.open ("Exit.aspx", "logout", "height=100, width=400, toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no, left=300,top=200")
		}
		function SetState()
		{
  		   window.status="Thanks for you chose BP.WF";
		}
		</script>
	</HEAD>
	<frameset onunload="logout()" onload="SetState()" id="frame1" rows="8,10,82%" border="0" frameSpacing="0"
		frameBorder="0" bordercolor=#ff6666>
		<frame name="header" id="frame3" src="Head.aspx" noResize scrolling="no" style="BACKGROUND-COLOR: #ff6666">
		<frame name="calltopmenu" src="calltopmenu.htm" frameborder="0" noresize scrolling="no">
		<frameset name="framename" id="frame2" cols="83,10,85%" bordercolor="#009966" style="BACKGROUND-COLOR: #ff6666"
			frameborder="no" onload="SetState()" onunload="logout()">
			<frame name="left" src="../../Comm/Port/Left2.aspx" DESIGNTIMEDRAGDROP="11">
			<FRAME name="callleftmenu" src="callleftmenu.htm" frameBorder="0" noResize scrolling="no" bordercolor=#ff6666 >
			<frame name="mainfrm" src="MyWork.aspx" noResize scrolling="yes" bordercolor=#ff6666>
		</frameset>
	</frameset>
</HTML>
