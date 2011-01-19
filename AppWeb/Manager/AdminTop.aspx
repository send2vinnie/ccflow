<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminTop.aspx.cs" Inherits="Tax666.AppWeb.Manager.AdminLogin" %>
<%@ Import Namespace="Tax666.AppWeb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <%=Global.GetHeadInfo()%>  
</head>
<body>
     <form id="form2" runat="server">
        <table class="layertop" cellpadding="0">
            <thead><tr><th><div id="logo" title="521联合供货网后台管理">521联合供货网后台管理</div></th>
            <th><div id="toplink">
                <a href="Javascript:void(0);">管理首页</a><img src="<%=Global.WebPath%>/Manager/Images/spliter.gif" alt="" />
                <a href="/" target="_blank">网站首页</a><img src="<%=Global.WebPath%>/Manager/Images/spliter.gif" alt="" />
                <a href="Javascript:void(0);">系统维护</a><img src="<%=Global.WebPath%>/Manager/Images/spliter.gif" alt="" />
                <a href="Javascript:void(0);">帮助</a></div></th>
            </tr></thead>
        </table>
    </form>
</body>
</html>
