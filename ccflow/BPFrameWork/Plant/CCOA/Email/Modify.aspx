﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Modify.aspx.cs" Inherits="CCOA_Email_Modify"
    Title="修改页" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script language="javascript" src="/js/CheckBox.js" type="text/javascript"></script>
    <script src="../../Comm/Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script src="../../Comm/Scripts/miniui/miniui.js" type="text/javascript"></script>
    <script src="../../Comm/Scripts/kindeditor/kindeditor.js" type="text/javascript"></script>
    <link href="../Style/control.css" rel="stylesheet" type="text/css" />
    <link href="../Style/demo.css" rel="stylesheet" type="text/css" />
    <link href="../../Comm/Scripts/kindeditor/themes/default/default.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript">

        $(function () {
            mini.parse();
            var editorId = "txtContent";

            //-------------------------------------------------------------
            var editor = KindEditor.create('#' + editorId, {
                resizeType: 1,
                uploadJson: 'kindeditor/upload_json.ashx',
                fileManagerJson: 'kindeditor/file_manager_json.ashx',
                allowPreviewEmoticons: false,
                allowImageUpload: true,
                items: [
		    'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
		    'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
		    'insertunorderedlist', '|', 'emoticons', 'image', 'link']
            });
        });
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="tdbg">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr style="display:none;">
                        <td height="25" width="30%" align="right">
                            主键Id ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblEmailId" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            主题 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtSubject" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            发件人 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtAddresser" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            收件人 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtAddressee" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            邮件内容 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtContent" runat="server" Width="600px" TextMode="MultiLine" Height="240px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            类型：0-普通1-重要2-紧急 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtPriorityLevel" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            创建时间 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtCreateTime" runat="server" Width="70px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            发送时间 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtSendTime" runat="server" Width="70px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <script src="/js/calendar1.js" type="text/javascript"></script>
            </td>
        </tr>
        <tr>
            <td class="tdbg" align="center" valign="bottom">
                <lizard:XButton ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" 
                    onmouseover="this.className='lizard-button-hover'" onmouseout="this.className='lizard-button'">
                </lizard:XButton>
                <lizard:XButton ID="btnCancle" runat="server" Text="取消" OnClick="btnCancle_Click" 
                    onmouseover="this.className='lizard-button-hover'" onmouseout="this.className='lizard-button'">
                </lizard:XButton>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
