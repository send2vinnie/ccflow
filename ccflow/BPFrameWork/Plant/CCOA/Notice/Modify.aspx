<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Modify.aspx.cs" Inherits="Lizard.OA.Web.OA_Notice.Modify"
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
    <link href="../../Comm/Scripts/miniui/themes/default/miniui.css" rel="stylesheet"
        type="text/css" />
    <link href="../../Comm/Scripts/kindeditor/themes/default/default.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript">

        $(function () {
            mini.parse();
            var editorId = "txtNoticeContent";

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

        function SelectAccess() {

            var authType = document.getElementById("ddlAccessType").value;

            var formTitle = "选择" + authType;
            var formUrl = "";
            var width = 320;

            switch (authType) {
                case "部门":
                    formUrl = "SelectAccess.aspx";
                    break;
                case "角色":
                    formUrl = "SelectRole.aspx";
                    width = 600;
                    break;
                case "人员":
                    formUrl = "SelectPeople.aspx";
                    width = 600;
                    break;
            }

            mini.openTop({
                url: "../../CCOA/Notice/" + formUrl,
                showMaxButton: false,
                title: formTitle,
                allowDrag: false,
                allowResize: false,
                width: width,
                height: 420,
                onload: function () {

                },
                ondestory: function (action) {
                    if (action == "ok") {
                        var iframe = this.getIFrameEl();
                        var data = iframe.contentWindow.GetData();
                        var selectedDept = $("#txtSelected");
                        selectedDept.val(data);

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
                    <tr>
                        <td height="25" width="30%" align="right">
                            通告标题 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtNoticeTitle" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            副标题 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtNoticeSubTitle" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            通告类型 ：
                        </td>
                         <td height="25" width="*" align="left">
                            <lizard:xdropdownlist id="ddlNoticeType" runat="server" width="200px" />
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            发布类别 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:DropDownList ID="ddlAccessType" runat="server" Width="200px">
                                <asp:ListItem>部门</asp:ListItem>
                                <asp:ListItem>角色</asp:ListItem>
                                <asp:ListItem>人员</asp:ListItem>
                            </asp:DropDownList>
                            <a class="mini-button" href="#" onclick="SelectAccess()">选择</a>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            公告接收方 ：
                            <asp:HiddenField ID="txtSelectedIds" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtSelected" ReadOnly="true" TextMode="MultiLine" runat="server"
                                Height="53px" Width="599px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            通告内容 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtNoticeContent" runat="server" Width="600px" TextMode="MultiLine"
                                Height="240px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
               
            </td>
        </tr>
        <tr>
            <td class="tdbg" align="center" valign="bottom">
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" class="inputbutton"
                    onmouseover="this.className='inputbutton_hover'" onmouseout="this.className='inputbutton'">
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
