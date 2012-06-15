<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Show.aspx.cs" Inherits="Lizard.OA.Web.OA_Notice.Show"
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
                            <asp:Label ID="lblNoticeType" runat="server"></asp:Label>
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
        <table width="100%" class="NoticeShow" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="NoticeTitle" colspan="3">
                    <asp:Label ID="lblNoticeTitle" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="RelatedInfo" colspan="3" style="text-align: right">
                    发布部门：发布人：<asp:Label ID="lblAuthor" runat="server"></asp:Label>
                    发布于：<asp:Label ID="lblCreateTime" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3" class="contentTitle">
                    <asp:Label ID="lblNoticeTitle0" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3" class="contentSubTitle">
                    <asp:Label ID="lblNoticeSubTitle" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3" class="content">
                    <asp:Label ID="lblNoticeContent" runat="server" Width="938px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="NoticeBottom" colspan="3">
                    <asp:Label ID="lblUpUser" runat="server"></asp:Label>
                    最后更新于<asp:Label ID="lblUpDT" runat="server"></asp:Label>
                    &nbsp; &nbsp;
                </td>
            </tr>
            <tr>
                <td class="NoticeBottom" colspan="3">
                    <uc2:Attachment ID="Attachment1" runat="server" EnumType="notice" />
                </td>
            </tr>
            <tr>
                <td style="text-align:center;" class="NoticeBottom" colspan="3">
                    <input id="Button1" type="button" value="转发" />&nbsp;<input id="Button2" type="button"
                        value="关闭" onclick="javascript:self.close()" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
