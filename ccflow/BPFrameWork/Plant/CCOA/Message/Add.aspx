<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Lizard.OA.Web.OA_Message.Add"
    Title="增加页" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../Style/control.css" rel="stylesheet" type="text/css" />
    <link href="../Style/demo.css" rel="stylesheet" type="text/css" />
    <script src="../../Comm/Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script src="../../Comm/Scripts/miniui/miniui.js" type="text/javascript"></script>
    <link href="../Style/control.css" rel="stylesheet" type="text/css" />
    <link href="../Style/demo.css" rel="stylesheet" type="text/css" />
    <link href="../../Comm/Scripts/miniui/themes/default/miniui.css" rel="stylesheet"
        type="text/css" />
    <link href="../../Comm/Scripts/kindeditor/themes/default/default.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript">

        function SelectReceiver() {
            var authType = document.getElementById("ddlAccessType").value;
            var arg;
            switch (authType) {
                case "部门":
                    arg = "dept";
                    break;
                case "角色":
                    arg = "role";
                    break;
                case "人员":
                    arg = "emp";
                    break;
            }
            onSelect(arg);
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
    <form id="Form1" runat="server">
    <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="tdbg">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td height="25" width="30%" align="right">
                            消息标题 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtMessageName" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            消息类型 ：
                        </td>
                        <%--<td height="25" width="*" align="left">
                            <asp:TextBox ID="txtMeaageType" runat="server" Width="200px"></asp:TextBox>
                        </td>--%>
                        <td height="25" width="*" align="left">
                            <lizard:xdropdownlist id="ddlMessageType" runat="server" width="200px" />
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
                            <a class="mini-button" href="#" onclick="SelectReceiver()">选择</a>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" height="25" width="30%" align="right">
                            接收对象：
                        </td>
                        <td>
                            <%-- 按<a href="#" onclick="onSelect('dept')">部门</a><a href="#" onclick="onSelect('role')">角色</a><a
                                href="#" onclick="onSelect('emp')">人员</a><br />--%>
                            <lizard:xtextbox id="xtxtReader" runat="server" width="400px" height="40px" readonly="true" />
                            <asp:HiddenField ID="hfSelects" runat="server" />
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
                    <%--  <tr>
                        <td height="25" width="30%" align="right">
                            发布人 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtAuthor" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>--%>
                    <tr>
                        <td height="25" width="30%" align="right">
                            发布时间 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtCreateTime" runat="server" Width="200px" CssClass="mini-datepicker"></asp:TextBox>
                        </td>
                    </tr>
                    <%--   <tr>
                        <td height="25" width="30%" align="right">
                            最后更新时间 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtUpDT" runat="server" Width="70px"></asp:TextBox>
                        </td>
                    </tr>--%>
                    <%--  <tr>
                        <td height="25" width="30%" align="right">
                            状态：1-有效0-无效 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:CheckBox ID="chkStatus" Text="状态：1-有效0-无效" runat="server" Checked="False" />
                        </td>
                    </tr>--%>
                </table>
                <script src="/js/calendar1.js" type="text/javascript"></script>
            </td>
        </tr>
        <tr>
            <td class="tdbg" align="center" valign="bottom">
                <asp:Button ID="btnSave" runat="server" Text="发送" OnClick="btnSave_Click" class="inputbutton"
                    onmouseover="this.className='inputbutton_hover'" onmouseout="this.className='inputbutton'">
                </asp:Button>
                <asp:Button ID="btnCancle" runat="server" Text="取消" OnClick="btnCancle_Click" class="inputbutton"
                    onmouseover="this.className='inputbutton_hover'" onmouseout="this.className='inputbutton'">
                </asp:Button>
            </td>
        </tr>
    </table>
    <br />
    </form>
</body>
</html>
