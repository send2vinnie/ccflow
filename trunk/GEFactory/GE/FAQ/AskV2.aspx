<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AskV2.aspx.cs" Inherits="FAQ_AskV2"
    Title="资源请求"" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../UC/Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>

    <script language="javascript" type="text/javascript">
        function DoSelect(test) {
            ss = test.split(",");
            var url = 'Do.aspx?DoType=NumOfRead&OID=' + ss[1];
            window.showModalDialog(url);
            var url2 = ss[0] + '?RefOID=' + ss[1];

//            var iHeight = 0;
//            var iWidth = 900;
//            var iTop = (window.screen.availHeight - 30 - iHeight) / 2;        //获得窗口的垂直位置;
//            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2;

            window.open(url2, 'frmBody', ss[1]);


            //            window.location.reload();

            return;
        }

        function DoChKM() {
            var url = '../Port/SelectKM.aspx';
            var winName = 'sdf';
            val = window.showModalDialog(url, winName, 'dialogHeight: 550px; dialogWidth: 650px; dialogTop: 100px; dialogLeft: 150px; center: yes; help: no');
            window.location.reload();

            return;
        }
        function DoAsk() {
            var iTop = (window.screen.availHeight - 30) / 1.4;        //获得窗口的垂直位置;
            var iLeft = (window.screen.availWidth - 10) / 1.7;
            window.showModalDialog("Ask.aspx", '123', 'dialogHeight: ' + iTop + 'px; dialogWidth:' + iLeft + 'px;  center: yes; help: no');
            window.location.reload();
            return;
        }
    </script>

    <link href="../Style/css/part.css" rel="stylesheet" type="text/css" />
    <link href="/Edu/Style/css/import.css" rel="stylesheet" type="text/css" />
    <link href="/Edu/Style/css/layout.css" rel="stylesheet" type="text/css" />
    <link href="/Edu/Style/css/index.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <p>
        <img src="../Style/Img/right_line_t.jpg" alt="" /></p>
    <div class="r_inner">
        <p class="siteMap">
            所在位置： 个人中心 - <span class="font_b">请求资源</span></p>
        <table class="tab_setwork">
            <tr>
                <td width="113">
                    <p>
                        <img src="../Style/Img/ttls_set.gif" alt="请选择关注领域" /></p>
                </td>
                <td width="405" valign="bottom">
                    <a class="link_num" href="javascript:DoChKM()">(修改)</a>
                </td>
                <td width="166" align="right">
                    <a class="link_qa" href="javascript:DoAsk()">我要请求</a>
                </td>
            </tr>
        </table>
        <uc1:Pub ID="Pub3" runat="server" />
        <uc1:Pub ID="Pub1" runat="server" />
        <div class="pageBox">
            <uc1:Pub ID="Pub2" runat="server" />
        </div>
    </div>
    <p>
        <img src="/Edu/Style/Img/right_line_b.jpg" alt="" /></p>
</body>
</html>
