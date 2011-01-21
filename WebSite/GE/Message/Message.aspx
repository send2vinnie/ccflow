<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Message.aspx.cs" Inherits="GE_Message_Message" ValidateRequest="false" %>

<%@ Register Src="../Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<html>
<head id="Head1" runat="server">
    <title>信息管理</title>
    <link href="../../Comm/Table2.css" rel="stylesheet" type="text/css" />
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="JScript.js"></script>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <div style="width: 950px; text-align: left;">
        <div class="div1">
            <span class="span"><a href="?op=Write">
                <img src="Img/nm.png" alt="写信" />写信</a> </span><span class="span"><a href="?op=UnRead">
                    <img src="Img/inbox.gif" />收信</a> </span><span class="span"><a href="?op=f">
                        <img src="Img/U01.gif" />联系人</a></span> <span class="span"><a href="?op=Receive">
                            <img src="Img/email_close.gif" />收件箱</a> </span><span class="span"><a href="?op=Draft">
                                <img src="Img/property.gif" />草稿箱</a></span> <span class="span"><a href="?op=Outbox">
                                    <img src="Img/sentbox.gif" />已发送</a></span> <span class="span"><a href="?op=Recycle">
                                        <img src="Img/email_delete.gif" />垃圾箱</a></span>
        </div>
        <div id="divList">
            <uc1:Pub ID="Pub1" runat="server" />
            <uc1:Pub ID="Pub2" runat="server" />
        </div>
    </div>
    <input id="txtContent" type="hidden" runat="server" />
    <input id="linkman" name="linkman" type="hidden" runat="server" />
    <input id="CUser" type="hidden" runat="server" />
    </form>
</body>
</html>
