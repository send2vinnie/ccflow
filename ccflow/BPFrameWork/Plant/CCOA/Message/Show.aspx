<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Show.aspx.cs" Inherits="Lizard.OA.Web.OA_Message.Show"
    Title="显示页" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../Style/control.css" rel="stylesheet" type="text/css" />
    <link href="../Style/demo.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            text-align: center;
            background: #F8F8F8;
        }
        .NoticeShow
        {
            font-family: 宋体, Arial, Helvetica, sans-serif;
            font-size: small;
            margin-top: 10px;
            border: 1px solid #83ACCF;
            background: #fff;
        }
        .NoticeShow .contentTitle
        {
            background: url('../Images/3-1.jpg') repeat-x;
            font-size: large;
            text-align: center;
            height: 60px;
            line-height: 80px;
        }
        .NoticeShow .contentSubTitle
        {
            font-size: medium;
            text-align: center;
            padding-left: 10px;
            padding-right: 10px;
        }
        .NoticeShow .content
        {
            margin-left: auto;
            margin-right: auto;
            padding-bottom: 50px;
        }
        .NoticeTitle
        {
            font-family: 宋体, Arial, Helvetica, sans-serif;
            font-size: 18px;
            background: url('../Images/title_bg.jpg');
            height: 23px;
            border-bottom: 1px solid #83ACCF;
        }
        .RelatedInfo
        {
            font-family: 宋体, Arial, Helvetica, sans-serif;
            font-size: 12px;
            background: url('../Images/info_bg.jpg');
            height: 23px;
            border-bottom: 1px solid #83ACCF;
        }
        .NoticeBottom
        {
            background: url('../Images/bottom_bg.jpg');
            min-height: 30px;
            line-height: 30px;
            font-size: 12px;
        }
    </style>
</head>
<body>
    <form id="Form1" runat="server">
    <table style="width: 100%; display: none" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="tdbg">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <%--  <tr>
                        <td height="25" width="30%" align="right">
                            主键Id ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblMessageId" runat="server"></asp:Label>
                        </td>
                    </tr>--%>
                    <tr>
                        <td height="25" width="30%" align="right">
                            消息标题 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblMessageName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            消息类型 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblMeaageType" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            消息内容 ：
                        </td>
                        <td>
                            <asp:TextBox ID="txtMessageContent" runat="server" Width="400px" TextMode="MultiLine"
                                Height="84px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            发布人 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblAuthor" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            发布时间 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblCreateTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <%--  <tr>
                        <td height="25" width="30%" align="right">
                            最后更新时间 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblUpDT" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            状态：1-有效0-无效 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </td>
                    </tr>--%>
                </table>
            </td>
        </tr>
    </table>
    <div style="width: 960px; margin-left: auto; margin-right: auto;">
        <table width="100%" class="NoticeShow" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="NoticeTitle" colspan="3">
                    <asp:Label ID="lblMessageTitle" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="RelatedInfo" colspan="3" style="text-align: right">
                    发布人：<asp:Label ID="Label1" runat="server"></asp:Label>
                    发布于：<asp:Label ID="Label2" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3" class="content" align="left">
                    <asp:Label ID="lblMessageContent" runat="server" Width="100%"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center;" class="NoticeBottom" colspan="3">
                    <%--<input id="Button1"  type="button" value="转发" />&nbsp;--%><input id="Button2"
                        type="button" value="关闭" onclick="javascript:self.close()" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
