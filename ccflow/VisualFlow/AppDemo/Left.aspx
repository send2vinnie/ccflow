<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Left.aspx.cs" Inherits="AppDemo_Left" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>导航</title>
    <link href="css/basic.css" rel="stylesheet" type="text/css" />
    <link href="css/base.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="JS_zhut">
        <div class="JS_zhut_n">
            <div class="left fl">
	            <div class="title"><span class="fl ml20">系统导航</span></div>
                <ul id="Ul1" class="menu" runat="server">
                    <li runat="server" id="Li2s" style=" background:url(Img/Menu/mun_10.jpg) -1px 0px"><a  href="../WF/StartSmall.aspx" target="main">发起</a></li>
                    <li runat="server" id="get" style=" background:url(Img/Menu/mun_2.jpg) -1px 0px"><a  href="../WF/EmpWorksSmall.aspx" target="main">待办 (<%= EmpWorks%>)</a></li>
                    <li runat="server" id="Li1" style=" background:url('Img/Menu/mun_3.jpg') -1px 0px"><a  href="../WF/RuningSmall.aspx" target="main">在途</a></li>
                    <li runat="server" id="over" style=" background:url(Img/Menu/mun_4.jpg) -1px 0px"><a  href="../WF/FlowSearchSmall.aspx" target="main">查询 </a></li>
                    <li runat="server" id="cld" style=" background:url(Img/Menu/mun_6.jpg) -1px 0px"><a  href="../WF/CalendarSmall.aspx" target="main">日历</a></li>
                    <li runat="server" id="end" style=" background:url(Img/Menu/mun_5.jpg) -1px 0px"><a  href="../WF/ToolsSmall.aspx" target="main">设置 </a></li>
                </ul>
            </div>
        </div>
    </div>
    </form>
</body>
</html>

