<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Lizard.OA.Web.OA_Notice.Add"
    Title="增加页" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <!--#include file="../inc/html_head.inc" -->
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
            //            var authType = mini.get("ddlAccessType").value;

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
                width: width,
                height: 420,
                allowDrag: false,
                allowResize: false,
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
                            <lizard:xtextbox id="txtNoticeTitle" runat="server" width="200px">
                            </lizard:xtextbox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            副标题 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <lizard:xtextbox id="txtNoticeSubTitle" runat="server" width="200px">
                            </lizard:xtextbox>
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
                            <lizard:xdropdownlist id="ddlAccessType" runat="server" width="200px">
                                <asp:ListItem>部门</asp:ListItem>
                                <asp:ListItem>角色</asp:ListItem>
                                <asp:ListItem>人员</asp:ListItem>
                            </lizard:xdropdownlist>
                            <a class="mini-button" href="#" onclick="SelectAccess()">选择</a>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            公告接收方 ：
                            <asp:HiddenField ID="txtSelectedIds" runat="server" />
                        </td>
                        <td>
                            <lizard:xtextbox id="txtSelected" textmode="MultiLine" readonly="true" runat="server"
                                height="53px" width="599px">
                            </lizard:xtextbox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            通告内容 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <lizard:xtextbox id="txtNoticeContent" runat="server" width="600px" textmode="MultiLine"
                                height="240px">
                            </lizard:xtextbox>
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
                </table>
                <script src="/js/calendar1.js" type="text/javascript"></script>
            </td>
        </tr>
        <tr>
            <td class="tdbg" align="center" valign="bottom">
                <lizard:xbutton id="btnSave" runat="server" text="保存" onclick="btnSave_Click" 
                    onmouseover="this.className='lizard-button-hover'" onmouseout="this.className='lizard-button'">
                </lizard:xbutton>
                <lizard:xbutton id="btnCancle" runat="server" text="取消" onclick="btnCancle_Click"
                     onmouseover="this.className='lizard-button-hover'" onmouseout="this.className='lizard-button'">
                </lizard:xbutton>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
