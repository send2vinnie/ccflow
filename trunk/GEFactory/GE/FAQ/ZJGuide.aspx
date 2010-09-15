<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZJGuide.aspx.cs" Inherits="FAQ_ZJGuide" %>

<%@ Register Src="../UC/Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>无标题页</title>
    <link href="../Style/css/import.css" rel="stylesheet" type="text/css" />
    <link href="../Style/css/openwindow.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="JavaScript" src="../Comm/Table.js"></script>

    <script language="javascript" type="text/javascript">
        function DoShare(TEST) {
            var iHeight = 400;
            var iWidth = 600;
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2;        //获得窗口的中心位置;
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2;
            var url = 'Ask.aspx?zj=' + TEST;
            window.open(url, '', 'height=450,width=600,top=' + iTop + ', left=' + iLeft + ',,  toolbar=no, menubar=no, scrollbars=no,resizable=no,location=no, status=no');
            window.close();
            return;
        }
     var win = window.dialogArguments; //取出前一窗口的window对象    
     function Test(zj,name)
     {  
        win.SetValue(zj,name);  //所有的自定义函数，是放在window对象之下来存储的
        window.close();
     }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:Pub ID="Pub1" runat="server" />
    </div>
    </form>
</body>
</html>
