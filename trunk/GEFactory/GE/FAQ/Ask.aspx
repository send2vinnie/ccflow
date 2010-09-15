<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ask.aspx.cs" Inherits="FAQ_Ask" %>

<%@ Register Src="../UC/Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>资源请求</title>
    <link href="../Style/css/import.css" rel="stylesheet" type="text/css" />
    <link href="../Style/css/openwindow.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">

    function Esc()
    {
        if (event.keyCode == 27)     
            window.close();
        return true;
    }
     function SelectZJ()
    {
            var iTop = (window.screen.availWidth) /1.6;        //获得窗口的中心位置;
            var iLeft = (window.screen.availHeight) /1.4;
        window.showModalDialog("KMGuide.aspx",window.self,"dialogWidth:"+iTop+"px;dialogHeight:"+iLeft+"px;");
        
    }
    function SetValue(val,valname)
    {

      document.getElementById("TB_ZJ").value=valname;
      document.getElementById("HiddenID").value=val;
            
    }

    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="open_wrapper">
        <h2 class="ttlm_share_01">
            <img src="../Style/Img/openwindow/ttls_request_01.gif" alt="资源请求" /></h2>
        <p>
            <span class="f_14_b">适用于：</span><asp:TextBox ID="TB_ZJ" Width="50%" runat="server"></asp:TextBox><asp:Button
                ID="Btn" runat="server" CssClass="btn_o_01" Text="选择章节" OnClientClick="SelectZJ()" />
        </p>
        <asp:HiddenField ID="HiddenID" runat="server" />
        <uc1:Pub ID="Pub1" runat="server" />
    </div>
    </form>
</body>
</html>
