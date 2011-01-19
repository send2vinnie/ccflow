<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="Tax666.AppWeb.Manager.AdminLogin1" %>
<%@ Import Namespace="Tax666.AppWeb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <%=Global.GetHeadInfo() %>
</head>
<body>
    <form id="form1" runat="server">
        <div id="alertbox" style="margin-top:80px; margin-bottom:60px;">
            <div class="alerttop"></div>
            <div class="alertmain" style="height:236px;">
	            <div class="alerthead">
                    <div class="alerttitle"><img src="<%=Global.WebPath%>/Manager/Images/icoUser.gif" border="0" /> 管理员用户登录</div>
                    <div class="alerttitleright">税务咨询高端服务后台管理</div>
                </div>
	            <div class="linespace"></div>
                <div class="alerttext">
    	            <div class="alertinfo"></div>
                    <div class="alerttextright">
        	            <ul>
            	            <li><span class="loginfrm_span">管理用户名：</span><input type="text" id="agentname" class="txtinput" style=" width:172px;"  onfocus="this.select();" maxlength="20" runat="server" /></li>
                            <li><span class="loginfrm_span">登录密码：</span><input type="password" id="agentpwd" class="pwdinput" style=" width:172px;"  onfocus="this.select();" maxlength="20" runat="server" />&nbsp;
	                            <img src="<%=Global.WebPath%>/Manager/Images/rankey.gif" border="0" onkeydown="Calc.password.value=document.getElementById('agentpwd').value" style="cursor: hand" onclick="password1=agentpwd;showkeyboard();Calc.password.value=''" alt="为了您登录安全，请使用密码软键盘输入密码！" onpropertychange="Calc.password.value=document.getElementById('agentpwd').value" /></li>
                            <li style="height:48px;"><span class="loginfrm_span"  style="height:48px;line-height:48px;">验 证 码：</span><input class="txtinput" id="validcode" style=" width:80px;" onkeypress="return isDigit(event.keyCode|event.which);" type="text" name="validcode" autocomplete="off" onfocus="this.select();" maxlength="6" runat="server" />
                                &nbsp;&nbsp;<img id="validimg" style="cursor:pointer;" onclick="this.src='<%=Global.WebPath%>/Utils/VerifyImagePage.aspx?time=' + Math.random()" title="点击刷新验证码" align="absMiddle" src="" /></li>
                            <li style="text-align:left; padding-top:20px;padding-left:80px;"><asp:Button ID="btnAgentSign" runat="server" Text="登  录" CssClass="btnbig" CausesValidation="False" /></li>
                        </ul>
                    </div>
                </div>
            </div>    
            <div class="alertbottom"></div>
        </div>
        <script type="text/javascript">
            document.getElementById('validimg').src='<%=Global.WebPath%>/Utils/VerifyImagePage.aspx?time=' + Math.random();
            document.getElementById('validcode').value = "";
        </script>
    </form>
</body>
</html>
