﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Lizard.OA.Web.OA_News.Add"
    Title="增加页" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../../Comm/Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script src="../../Comm/Scripts/miniui/miniui.js" type="text/javascript"></script>
    <script src="../../Comm/Scripts/kindeditor/kindeditor.js" type="text/javascript"></script>
    <script src="../Upload/swfupload/swfupload.js" type="text/javascript"></script>
    <link href="../Style/control.css" rel="stylesheet" type="text/css" />
    <link href="../Style/demo.css" rel="stylesheet" type="text/css" />
    <link href="../../Comm/Scripts/miniui/themes/default/miniui.css" rel="stylesheet"
        type="text/css" />
    <link href="../../Comm/Scripts/kindeditor/themes/default/default.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript">

        $(function () {
            mini.parse();
            var editorId = "txtNewsContent";

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

        function onUploadSuccess(e) {

            alert("上传成功：" + e.serverData);
            this.setText("");
        }

        function startUpload() {
            var fileupload = mini.get("fileupload1");
            fileupload.startUpload();
        }

        function onSelect(arg) {
            var txtSelect = $("#xtxtReader");
            var hfSelects = $("#hfSelects");
            var dataUrl = "";
            if (arg == "dept") {
                dataUrl = "../CCOA/Common/TreeList.aspx?type=" + arg;
            }
            else {
                dataUrl = "../CCOA/Common/ListSelect.aspx?type=" + arg;
            }
            var hfAccessType = $("#hfSelects");
            hfAccessType.val(arg);

            mini.openTop({
                url: dataUrl,
                showMaxButton: true,
                title: "选择发布对象",
                width: 650,
                height: 380,
                allowDrag: false,
                allowResize: false,
                onload: function () {
                    var iframe = this.getIFrameEl();
                    //iframe.contentWindow.SetData(null);
                },
                ondestory: function (action) {
                    if (action == "ok") {
                        var iframe = this.getIFrameEl();
                        var data = iframe.contentWindow.GetData();
                        var ids = iframe.contentWindow.GetSelectedIds();
                        data = mini.clone(data);
                        txtSelect.val(data);
                        hfSelects.val(ids);
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
                            新闻标题 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtNewsTitle" runat="server" Width="600px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            副标题 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtNewsSubTitle" runat="server" Width="600px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            新闻类型 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <lizard:XDropDownList ID="ddlNewsType" runat="server" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            选择发布对象 ：
                        </td>
                        <td height="25" width="*" align="left">
                            按 <a href="#" onclick="onSelect('dept')">部门</a> <a href="#" onclick="onSelect('role')">
                                角色</a> <a href="#" onclick="onSelect('emp')">人员</a><br />
                            <lizard:XTextBox ID="xtxtReader" runat="server" Width="400px" Height="40px" />
                            <asp:HiddenField ID="hfSelects" runat="server" />
                            <asp:HiddenField ID="hfAccessType" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            新闻内容 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtNewsContent" runat="server" Width="600px" TextMode="MultiLine"
                                Height="240px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            发布人 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtAuthor" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            发布时间 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtCreateTime" runat="server" Width="100px" CssClass="mini-datepicker"></asp:TextBox>
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
                <lizard:XButton ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" 
                    onmouseover="this.className='lizard-button-hover'" onmouseout="this.className='lizard-button'">
                </lizard:XButton>
                <lizard:XButton ID="btnCancle" runat="server" Text="取消" OnClick="btnCancle_Click" 
                    onmouseover="this.className='lizard-button-hover'" onmouseout="this.className='lizard-button'">
                </lizard:XButton>
            </td>
        </tr>
    </table>
    <br />
    </form>
</body>
</html>
