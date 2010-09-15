<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowMsg.aspx.cs" Inherits="FAQ_ShowMsg" %>

<%@ Register Src="../UC/Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script language="javascript" type="text/javascript">
        function DoOpen(test) {
            var url = 'Do.aspx?DoType=NumOfRead&OID=' + test;
            window.showModalDialog(url);
            var url2 ='InitDesc.aspx?RefOID=' + test;
            window.open(url2, 'd123' );
            window.close();
            return;
        }
        function DoSH() {
           window.parent.location.reload();
            window.close();
            return;
        }
        function DoAsk() {
            window.showModalDialog("Ask.aspx", '123', 'dialogHeight: 700px; dialogWidth:800px;  center: yes; help: no');
            window.close();
        }
    </script>

    <link href="../Style/Part.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-top: 20px; font-size: 12px;">
        <div class="messageBox clearfix" style="height: 200px">
            <div class="inner_01">
                <uc1:Pub ID="Pub1" runat="server" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
