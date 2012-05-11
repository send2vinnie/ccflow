<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Show.aspx.cs" Inherits="Lizard.OA.Web.OA_News.Show"
    Title="显示页" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <style type="text/css">
        .NewsShow
        {
            font-family: 宋体, Arial, Helvetica, sans-serif;
            font-size: small;
            margin-right: auto;
            margin-left: auto;
            margin-top: 10px;
        }
        .NewsTitle
        {
            font-family: 宋体, Arial, Helvetica, sans-serif;
            font-size: 18px;
        }
        body
        {
            width: 960px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="tdbg" style="display: none">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td height="25" width="30%" align="right">
                            新闻类型 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblNewsType" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            点击量 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblClicks" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            是否阅读 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblIsRead" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table class="NewsShow" style="width: 100%;">
        <tr>
            <td bgcolor="#0099FF" colspan="3">
                <asp:Label ID="lblNewsTitle" runat="server" CssClass="NewsTitle"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td bgcolor="#CCCCFF" colspan="2" style="text-align: right">
                发布部门：发布人：<asp:Label ID="lblAuthor" runat="server"></asp:Label>
                发布于：<asp:Label ID="lblCreateTime" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: center">
                <asp:Label ID="lblNewsTitle0" runat="server" CssClass="NewsTitle"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: center">
                <asp:Label ID="lblNewsSubTitle" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblNewsContent" runat="server" Width="600px" Height="500px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td bgcolor="#CCCCFF" colspan="3">
                <asp:Label ID="lblUpUser" runat="server"></asp:Label>
                最后更新于<asp:Label ID="lblUpDT" runat="server"></asp:Label>
                &nbsp; &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    &nbsp;</form>
</body>
</html>
