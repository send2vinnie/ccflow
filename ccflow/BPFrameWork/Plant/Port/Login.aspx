<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Port_Login" %>

<%@ Register Src="Controls/Log.ascx" TagName="Login" TagPrefix="lizard" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        * { font-family: "微软雅黑"}
        html, body, form { width:100%; height:100%; }
        body { background:url(Img/bg_blue_graded.jpg) repeat-x; }
        .bg { width:767px; height:416px; margin:auto; margin-top:100px;  background:url(Img/background_card.gif) no-repeat; overflow:hidden; }
        .Table { margin:auto; margin-top:140px; }
        .Table .Table { margin-top:0px; }
        .Table input[type=text], .Table input[type=password]{ width:140px; height:16px; line-height:16px;  }
        .Table td { height:30px; line-height:30px; }
        .Table a { font-size:12px; }
    </style>
   <object CLASSID="clsid:e6bd6993-164f-4277-ae97-5eb4bab56443"
	CODEBASE="Javascript/et99.cab#Version=1,00,0000"
	BORDER="0" VSPACE="0" HSPACE="0" ALIGN="TOP" HEIGHT="0" WIDTH="0"
</object>
</head>
<body>
    <form id="form1" runat="server">
    <div class="bg">
        <lizard:Login ID="uclogin" runat="server" />
    </div>
    </form>
</body>
</html>
