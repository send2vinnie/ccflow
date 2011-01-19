<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeftMenu.aspx.cs" Inherits="Tax666.AppWeb.Manager.LeftMenu" %>
<%@ Import Namespace="Tax666.AppWeb" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <%=Global.GetHeadInfo()%>
    <script language="javascript" type="text/javascript">
        var menunum = <%=menuid %>;
        
        function menuInit(){            
            for (var i = 1; i < menunum; i++) {
		        document.getElementById("menu_" + i).style.display = "none";
		        document.getElementById("td_" + i).style.background="url(Images/admin_title_bg_hide.gif)";
	        }
	        
	        if(menunum > 1){
	            document.getElementById("menu_1").style.display = "";
	            document.getElementById("td_1").style.background="url(Images/admin_title_bg_show.gif)";
	        }
	        
	        documentbody = document.documentElement.clientHeight > document.body.clientHeight ? document.documentElement : document.body;
			var leftbar = document.getElementById('leftbar');
			leftbar.style.height = documentbody.clientHeight +'px';
			
			var leftbarleft = document.body.clientWidth-12;
			leftbar.style.left = leftbarleft + 'px';
			leftbar.style.top = documentbody.scrollTop + 'px';
			
			document.onscroll = function(){ 
											leftbar.style.height=documentbody.clientHeight +'px';
											leftbar.style.top=documentbody.scrollTop + 'px'; 
										}
			document.onresize = function(){ 
											leftbar.style.height=documentbody.clientHeight +'px';											
											leftbar.style.top=documentbody.scrollTop + 'px'; 
										}
        }
        
        function menuChange(id){
	        for (var i = 1; i < menunum; i++) {
		        if (i == id) {
			        document.getElementById("menu_" + i).style.display = "";
			        document.getElementById("td_" + i).style.background="url(Images/admin_title_bg_hide.gif)";
		        } else {
			        document.getElementById("menu_" + i).style.display = "none";
			        document.getElementById("td_" + i).style.background="url(Images/admin_title_bg_show.gif)";
		        }
	        }        	
        }
        
        var NowClickName="";
    
        function NowShow(TopMenuName,Url) {
            document.getElementById(TopMenuName).className  = "table_body";
            if (NowClickName!="" &&NowClickName!=TopMenuName)
                document.getElementById(NowClickName).className  = "table_none"; 
            NowClickName = TopMenuName;
            
            window.parent.frames["mainframe"].location=Url;
        }
        
        function TDOverOROut(iname){
            if (NowClickName!=iname){
                document.getElementById(iname).className = "table_none";
            }
        }
        function TDOverORIn(iname){
            if (NowClickName != iname){
                document.getElementById(iname).className = "table_body";
            }
        }
        
        function resizediv_onClick(){
			var leftbar = document.getElementById('leftbar');
			
			if (document.getElementById('menu').style.display != 'none'){
				top.document.getElementsByTagName('FRAMESET')[1].cols = "12,*";
				document.getElementById('menu').style.display = 'none';
				
				leftbar.className = "expand";				
				leftbar.style.left = 0;
			}
			else{
				top.document.getElementsByTagName('FRAMESET')[1].cols = "204,*";
				document.getElementById('menu').style.display = '';
				
				leftbar.className = "collapse";
				var leftbarleft = document.body.clientWidth-12;
			    leftbar.style.left = leftbarleft + 'px';
			}
		}
    </script>
</head>
<body style="background-color:#4eb1df;" onload="menuInit()">
    <form id="form1" runat="server">
        <div id="menu" class="menumain">
			<table cellSpacing="0" cellPadding="0" width="180" align="center">
				<tr><td vAlign="bottom" height="40"><a href="<%=Global.WebPath%>/Manager/ServerCheck.aspx" target="right"><img height="38" src="<%=Global.WebPath%>/Manager/Images/admin_title.gif" width="180" border="0"></a></td>
				</tr><tr>
					<td style="background: url(<%=Global.WebPath%>/Manager/Images/admin_title_bg_quit.gif)" height="25">&nbsp; 
						&nbsp;<a href="<%=Global.WebPath%>/Manager/SignOut.aspx" target="_parent" class="a1">安全退出</a> &nbsp; 
						&nbsp;|&nbsp;&nbsp;<a href="javascript:void(0)" class="a1">资料维护</a>
					</td>
				</tr>
			</table>
			<div class="tdspace"></div>
            <asp:Literal ID="litMenu" runat="server"></asp:Literal>
            <table cellSpacing="0" cellPadding="0" width="180" align="center">
				<tr style="cursor: default">
					<td class="menu_title" style="background: url(<%=Global.WebPath%>/Manager/Images/admin_about.gif); cursor: default" height="25" style="font:11px normal Tahoma;">&nbsp;&nbsp;<uc1:Pub 
                            ID="Pub1" runat="server" />
                        <b>
                        <br />
                        <br />
                        <br />
                        CopyRight &copy;2009	<div id="leftbar" class="collapse" onclick="return resizediv_onClick()" title="打开/关闭导航"></div>
    </form>
</body>
</html>
