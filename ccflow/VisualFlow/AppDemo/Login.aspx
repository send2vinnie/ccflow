<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="AppDemo_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<html xmlns="http://www.w3.org/1999/xhtml">
<%@ Register src="UC/Login.ascx" tagname="Login" tagprefix="uc1" %>

<head id="Head1" runat="server">
    <title><%=BP.SystemConfig.SysName %></title>
    <style type="text/css">
        * { margin:0; padding:0;}
        html, body, form { width:100%; height:100%; font-family:"微软雅黑"; }
        body { background:#efefef;}
        .bg { width:1280px; height:100%; position:relative; background:url(Img/LoginBJ.jpg) no-repeat 0px 0px; margin:auto; border-left:1px solid #333; border-right:1px solid #333;}
        .login { position:absolute; left:590px; top:250px; height:200px; width:350px; overflow:hidden;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="bg">
        <div class="login">
            <uc1:Login ID="Login1" runat="server"/>
        </div>
    </div>
    </form>
</body>


<ml>