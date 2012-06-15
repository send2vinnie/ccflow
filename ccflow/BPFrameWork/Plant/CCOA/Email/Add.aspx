<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="CCOA_Email_Add"
    Title="增加页" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script language="javascript" src="/js/CheckBox.js" type="text/javascript"></script>
    <script src="../../Comm/Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script src="../../Comm/Scripts/miniui/miniui.js" type="text/javascript"></script>
    <script src="../../Comm/Scripts/kindeditor/kindeditor.js" type="text/javascript"></script>
    <script src="../../Comm/Scripts/CommonLibs/TreeSelectWindow.js" type="text/javascript"></script>
    <link href="../Style/control.css" rel="stylesheet" type="text/css" />
    <link href="../Style/demo.css" rel="stylesheet" type="text/css" />
    <link href="../../Comm/Scripts/miniui/themes/default/miniui.css" rel="stylesheet"
        type="text/css" />
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

        function onButtonEdit() {
            //var buttonEdit = e.sender;
            var buttonEdit = $("#txtAddressee");

            mini.openTop({
                url: "../CCOA/Common/ListSelect.aspx?type=emp",
                showMaxButton: false,
                title: "选择人员",
                width: 600,
                height: 420,
                allowResize: false,
                onload: function () {
                    var iframe = this.getIFrameEl();
                    //iframe.contentWindow.SetData(null);
                },
                ondestory: function (action) {
                    if (action == "ok") {
                        var iframe = this.getIFrameEl();

                        var data = iframe.contentWindow.GetData();
                        buttonEdit.val(data);

                        var selectedIds = iframe.contentWindow.GetSelectedIds();
                        var selecedDeptIdText = $("#txtSelectedIds");
                        selecedDeptIdText.val(selectedIds);
                    }
                }
            });
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="tdbg">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr style="display: none">
                        <td height="25" width="10%" align="right">
                            主键Id ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtEmailId" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="10%" align="right">
                            主题 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtSubject" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="10%" align="right">
                            发件人 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtAddresser" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="10%" align="right">
                            <a href="#" onclick="onButtonEdit()">收件人</a> ：
                        </td>
                        <td height="25" width="*" align="left">
                            <lizard:xtextbox id="txtAddressee" readonly="true" runat="server" width="200px">
                            </lizard:xtextbox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="收件人不能为空！"
                                ControlToValidate="txtAddressee"></asp:RequiredFieldValidator>
                            <asp:HiddenField ID="txtSelectedIds" runat="server" />
                            <%--<asp:TextBox ID="txtAddressee" runat="server" class="mini-textbox" Width="200px"></asp:TextBox>--%>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="10%" align="right">
                            邮件内容 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtContent" runat="server" Width="100%" TextMode="MultiLine" Height="300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            附件 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="10%" align="right">
                            类型：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:CheckBoxList ID="chklstPriorityLevel" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="普通" Value="0" />
                                <asp:ListItem Text="重要" Value="1" />
                                <asp:ListItem Text="紧急" Value="2" />
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
                <script src="/js/calendar1.js" type="text/javascript"></script>
            </td>
        </tr>
        <tr>
            <td class="tdbg" align="center" valign="bottom">
                <asp:Button ID="btnSave" runat="server" Text="发送" OnClick="btnSave_Click" class="inputbutton"
                    onmouseover="this.className='inputbutton_hover'" onmouseout="this.className='inputbutton'">
                </asp:Button>
                <asp:Button ID="btnSaveDraft" runat="server" Text="存草稿" OnClick="btnSaveDraft_Click"
                    class="inputbutton" onmouseover="this.className='inputbutton_hover'" onmouseout="this.className='inputbutton'">
                </asp:Button>
                <asp:Button ID="btnCancle" runat="server" Text="取消" OnClick="btnCancle_Click" class="inputbutton"
                    onmouseover="this.className='inputbutton_hover'" onmouseout="this.className='inputbutton'">
                </asp:Button>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
