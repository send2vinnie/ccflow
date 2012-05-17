<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Show.aspx.cs" Inherits="Lizard.OA.Web.OA_News.Show"
    Title="显示页" %>

<%@ Register Src="../Controls/MiniToolBar.ascx" TagName="MiniToolBar" TagPrefix="uc1" %>
<%@ Register Src="../Controls/Attachment.ascx" TagName="Attachment" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <style type="text/css">
        body
        {
            text-align: center;
        }
        .NewsShow
        {
            font-family: 宋体, Arial, Helvetica, sans-serif;
            font-size: small;
            margin-top: 10px;
        }
        .NewsTitle
        {
            font-family: 宋体, Arial, Helvetica, sans-serif;
            font-size: 18px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table style="display: none" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="tdbg">
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
    <div style="width: 960px; margin-left: auto; margin-right: auto;">
        <table width="100%" class="NewsShow">
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
                    <asp:Label ID="lblNewsContent" runat="server" Width="600px" ></asp:Label>
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
                <td bgcolor="#CCCCFF" colspan="3">
                    <uc2:Attachment ID="Attachment1" runat="server" EnumType="News" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
