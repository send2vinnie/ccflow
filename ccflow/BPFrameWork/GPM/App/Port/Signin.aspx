<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="cc1" Namespace="BP.Web.Controls" Assembly="BP.Web.Controls" %>

<%@ Page Language="c#" Inherits="BP.Web.SignInPG" CodeFile="Signin.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>
        <%=BP.SystemConfig.SysName%>
    </title>
    <meta name="vs_showGrid" content="True">
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <script language="javascript">
        function winfix() {
            if (document.layers) {
                width = screen.availWidth - 10;
                height = screen.availHeight - 20;
            } else {
                var width = screen.availWidth - 2;
                var height = screen.availHeight;
            }
            self.resizeTo(width, height);
            self.moveTo(0, 0);
        }
        var mode;
        mode = 1;
        function show() {
            // window.open('Note.htm')
            if (mode == 0) {
                help.innerHTML = "";
                mode = 1;
            }
            else {
                help.innerHTML = "<BR><b>1)</b>��ϵͳ������ϵͳ���ܽ��,�����ϵͳ���û�����½.";
                help.innerHTML += "<BR><b>2)</b>��������״ε�½����������pub.��½ϵͳ����ע���޸����룮Ϊ�˱�֤������ݰ�ȫ����ע�Ᵽ����Լ������룮";
                help.innerHTML += "<BR><b>3)</b>ʹ�ù����������⣬�뷢����BBS���߷�ӳ��ϵͳ����Ա��";
                help.innerHTML += "<BR><BR>&nbsp;&nbsp;&nbsp;&nbsp;������죮";
                mode = 0;
            }
        }
        function SetHomePage() {
            document.body.style.behavior = "url(#default#homepage)";
            document.body.setHomePage(window.location.href);
            //  document.body.all.homepageLink.click();
        }
        function setFocus(ctl) {
            if (document.forms[0][ctl] != null) {
                document.forms[0][ctl].focus();
            }
        }		
		 
    </script>
    <link href="../../Comm/Style.css" type="text/css" rel="stylesheet">
    <script language="JavaScript" src="../../Comm/JScript.js"></script>
    <style type="text/css">
        BODY
        {
            background-position: center 50%;
            background-attachment: fixed;
            background-repeat: no-repeat;
            background-color: #F2f5fa;
        }
    </style>
</head>
<body bgproperties="fixed" topmargin="0" leftmargin="0" rightmargin="0" bottommargin="0"
    class="Login<%=BP.SystemConfig.SysNo%>" onload="winfix();">
    <form id="SignIn" method="post" runat="server" defaultfocus="TB_No">
    <div style="background: url(Img/bg_top.png); height: 75px">
    </div>
    <table id="Table2" height="450px" cellspacing="1" width="100%" cellpadding="1" border="0"
        align="center">
        <tr>
            <td align="center" width="60%" height="100%">
                <table align="center" id="Table3" border="0" style="width: 70%; height: 100%">
                    <tr>
                        <td style="width: 524px;">
                            <img src="Img/tb.png" />
                        </td>
                        <td align="left" height="60%">
                            <div style="border: 1px solid #e5e5e5; height: 334px; padding-top: 150px;">
                                <cc1:Lab ID="Lab1" runat="server">
										<font size="3">&nbsp;&nbsp;<b>�û���</b></font></cc1:Lab>
                                <cc1:TB ID="TB_No" Style="background-image: url(Img/text.png);" runat="server" ShowType="TB"
                                    Width="165px" Height="28px" BorderStyle="none" Font-Size="14px"></cc1:TB><br>
                                <cc1:Lab ID="Lab2" runat="server">
										<font size="3">&nbsp;&nbsp;<b>��&nbsp;&nbsp;&nbsp;&nbsp;��</b></font></cc1:Lab>
                                <cc1:TB Style="background-image: url(Img/text.png);" ID="TB_Pass" runat="server"
                                    Width="164px" Height="28px" BorderStyle="none" TextMode="Password" BackColor="White"></cc1:TB>
                                <br />
                                <br />
                                <div style="margin-left:120px;">
                                 <cc1:Btn ID="Btn1" runat="server"
                                    Style="background: url(Img/btlogin.png); width: 108px; height: 33px; border: none;"
                                    OnClick="Btn1_Click"></cc1:Btn>
                                </div>
                               
                            </div>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div style="background: url(Img/bg_bottom.png); height: 70px; text-align: center;
        line-height: 70px;">
        <span style="color: #fff; font-size: 12px;">��Ȩ���У��������������������ι�˾ ���������о�Ժ-�����¼ϵͳ</span>
    </div>
    </form>
</body>
</html>
