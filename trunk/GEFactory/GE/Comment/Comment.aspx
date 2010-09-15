<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Comment.aspx.cs" Inherits="GE_Comment_GEPJ"
    ValidateRequest="false" %>

<html>
<head id="Head1" runat="server">
    <script type="text/javascript" src="../ckeditor/ckeditor.js"></script>
    <script type="text/javascript" src="../ckeditor/ckeditor_source.js"></script>
    <script type="text/javascript" src="../ckeditor/ckeditor.js"></script>
    <script src="../ckeditor/sample.js" type="text/javascript"></script>
    <link type="text/css" href="../ckeditor/sample.css" rel="stylesheet" />
    <script type="text/javascript">
        document.oncontextmenu = function () { return false };
    </script>
    <title>发表评论</title>
    <base target="_self" />
    <script type='text/javascript' src='CheckForm.js'></script>
    <link rel="Stylesheet" type="text/css" href="Stylesheet.css" />
    <script type="text/javascript">
        window.onbeforeunload = function () {

        }
        function mySucess() {
            window.returnValue = true;
            alert("评论成功!");
            window.close();
        }
    </script>
    <% Response.Expires = -1; %>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="tbl_Content">
            <tr>
                <td>
                    <span style="font-weight: bold; font-size: 15px; letter-spacing: 2px;">请输入评论内容:</span>
                </td>
            </tr>
            <tr>
                <td>
                    <%--<input type="hidden" name="txtContent" id="txtContent" runat="server" />--%>
                    <%-- <iframe id="eWebEditor1" name="eWebEditor1" frameborder="0" scrolling="no" src="Edit/editor.htm?id=txtContent&amp;style=coolblue"
                        style="display: block"></iframe>--%>
                    <textarea cols="80" id="txtContent" name="txtContent" rows="10" runat="server" style="word-break:break-all"></textarea>
                </td>
            </tr>
            <tr>
                <td class="tbl_Content_Td">
                    用户名：<input name="txtUserName" type="text" value="" id="txtUserName" check="^\S+$"
                        warning="用户名不能为空,且不能含有空格!" style="width: 100px;" runat="server" readonly="readonly" />
                    <%--<asp:Label id="lblUserName" style="width: 100px;" runat="server"></asp:Label>--%>
                    验证码：<input name="txtCheckCode" type="text" id="txtCheckCode" check="^\S+$" warning="请输入验证码!"
                        style="width: 100px;" runat="server" /><img src="CheckCode.aspx" alt="验证码" />
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Button Text="发表评论" OnClientClick="return CheckForm(document.forms[0]);" ID="btnSubmit"
                        runat="server" OnClick="btnSubmit_Click" />
                    <input type="button" value=" 关 闭 " onclick="window.close()" />
                </td>
            </tr>
            <%--<tr>
                <td>
                    <span class="Span_A"><a href class="A_Login">登陆</a><a href class="A_Login">注册</a></span>
                </td>
            </tr>--%>
        </table>
    </div>
    <script type="text/javascript">
        CKEDITOR.replace('txtContent',
				{
				    skin: 'kama',
				    extraPlugins: 'uicolor',
				    toolbar:
					[
                        ['Bold', 'Italic', 'Underline', 'Strike', '-',
                         'Subscript', 'Superscript', '-',
                         'NumberedList', 'BulletedList', '-',
                         'TextColor', 'BGColor', '-',
                         'Smiley']
					]
				});
    </script>
    </form>
</body>
</html>
