<%@ Page Title="选择章节" Language="C#" MasterPageFile="~/Style/MasterPage.master" AutoEventWireup="true"
    CodeFile="ZJList.aspx.cs" Inherits="FAQ_ZJList" %>

<%@ Register Src="../UC/Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <base target="_self"></base>

    <script language="javascript" type="text/javascript">
        function DoShare(TEST) {
            var iHeight = 400;
            var iWidth = 600;
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2;        //获得窗口的中心位置;
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2;
            var url = 'Ask.aspx?zj=' + TEST;
            window.open(url, '', 'height=450px,width=600px,top=' + iTop + ', left=' + iLeft + ',,  toolbar=no, menubar=no, scrollbars=no,resizable=no,location=no, status=no');
            return;
        }
        function DoGuide() {
            var iHeight = 400;
            var iWidth = 600;
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2;        //获得窗口的中心位置;
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2;
            var url = 'KMGuide.aspx';
            window.open(url, '', 'height=' + window.screen.availHeight/2 +',width=' + window.screen.availWidth/2+ ',top=' + iTop + ', left=' + iLeft + ',  toolbar=yes, menubar=no, scrollbars=yes,resizable=yes,location=yes, status=yes');
            return;
        }
    </script>

    <table heigth="100%" width="100%">
        <tr valign="top">
            <td style="background-color: #F4F4F4" width="20%" align="center" height="100%">
                <table width="100%" height="100%">
                    <tr style="background-color: #3c90d1" height="10px">
                        <td height="20px">
                            使用细则
                        </td>
                    </tr>
                    <tr height="90%" valign="top">
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;当您在资源中心中搜索不到您想要的资源的时候，您可以通过资源请求来获取资源。</br>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;请求资源的步骤：首先选择您要获取资源的章节知识点，进入请求资源的页面，填写资源标题，详细的描述，悬赏的积分，确认提交后，您的请求就发出去了。</br>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;用户看到您的请求并回答您的请求，给您上传文件，答案可以是多个的，当您采纳了最佳的答案后，就会付给出最佳答案的用户积分。</br>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;您也可以回答用户的请求，问题被采纳后就会获得相应的悬赏积分。</br>
                            <a href="javascript:DoGuide()">使用向导</a>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="80%">
                <uc1:Pub ID="Pub1" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
