<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="cc1" Namespace="BP.Web.Controls" Assembly="BP.Web.Controls" %>

<%@ Page Language="c#" Inherits="BP.Web.SignInCT" CodeFile="Signin.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>系统登录</title>
    <meta name="vs_showGrid" content="True">
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <script language="javascript">
		function winfix()
		{
	         if (document.layers) {  
		        width=screen.availWidth - 10;  
		         height=screen.availHeight - 20;  
	          } else {  
		        var width=screen.availWidth - 2;  
		        var height=screen.availHeight;  
	          }  
              self.resizeTo(width, height); 
              self.moveTo(0, 0);
         } 
         var mode;
         mode=1;
         function show(){
         // window.open('Note.htm')
           if (mode==0)
           {
              help.innerHTML="";
              mode=1;
           }
           else 
           { 
             help.innerHTML="<BR><b>1)</b>本系统与主体系统紧密结合,请采用系统的用户名登陆.";
             help.innerHTML+="<BR><b>2)</b>如果您是首次登陆，密码请用pub.登陆系统后请注意修改密码．为了保证你的数据安全，请注意保存好自己的密码．";
             help.innerHTML+="<BR><b>3)</b>使用过程中有问题，请发布到BBS或者反映给系统管理员．";             
             help.innerHTML+="<BR><BR>&nbsp;&nbsp;&nbsp;&nbsp;工作愉快．";
             mode=0;
            }
        }
		function SetHomePage()
		{
		  document.body.style.behavior="url(#default#homepage)";
		  document.body.setHomePage(window.location.href);
		//  document.body.all.homepageLink.click();
		}
		function setFocus(ctl)
		{  
		  if (document.forms[0][ctl] != null)  
		  { 
		    document.forms[0][ctl].focus();
		  }
		}		
		 
    </script>
    <link href="../Comm/Style.css" type="text/css" rel="stylesheet">
    <script language="JavaScript" src="../../Comm/JScript.js"></script>
    <style type="text/css">
        BODY
        {
            background-position: center 50%;
            background-attachment: fixed;
            background-image: url(SignInOfCT.jpg);
            background-repeat: no-repeat;
        }
    </style>
</head>
<body bgproperties="fixed" topmargin="0" leftmargin="0" rightmargin="0" bottommargin="0"
    onload="winfix();">
    <form id="SignIn" method="post" runat="server" defaultfocus="TB_No">
    <table id="Table2" height="100%" cellspacing="1" width="100%" cellpadding="1" border="0"
        align="center" style="background: #80c2f4">
        <tr height="40%" valign="bottom">
            <td  width="25%">
            </td>
            <td  align="center"><img src=./Img/Log3706.jpg  />
            </td>
            <td  width="25%">
            </td>
            <tr>
                <td align="center" colspan="3" valign="top">
                    <table align="center" id="Table3" border="0" style="width: 100%; height: 100%; background: url(img/loginbg2.jpg) repeat-x left top;
                        height: 137px;">
                        <tr>
                            <td height="25%">
                            </td>
                            <td align="left" width="36%" height='50%'>
                               
                                <table cellpadding="5" cellspacing="5" width="70%" align="center" style="border-right:#e2e3e5 1px solid;border-left:#e2e3e5 1px solid;"><tr><td align=right>
                                    用户名：
                                    </td><td>
                                <cc1:TB class="BigButton" ID="TB_No"  runat="server"
                                    ShowType="TB" Width="150px" Height="25px" BorderStyle="Solid" BorderWidth="1px"
  Bordercolor="#cccccc" BackColor="White"></cc1:TB></td></tr>
                                <tr><td align=right>
                                密码：
                                    </td><td>
                                <cc1:TB class="BigButton" ID="TB_Pass" runat="server" Width="150px" Height="25px" BorderStyle="Solid"
                                    Bordercolor="#cccccc" BorderWidth="1px" TextMode="Password" BackColor="White"  ></cc1:TB></td></tr><tr><td align=center  colspan="2">
                                <cc1:Btn ID="Btn1" runat="server"  
                                    Text="登录(O)"  OnClick="Btn1_Click" ></cc1:Btn></td></tr></table>
                            </td>
                            <td width="25%" style="background: url(img/loginright.jpg) no-repeat right top;">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="25%" colspan="3">
                </td>
            </tr>
    </table>
    </form>
</body>
</html>
