<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopWindow.aspx.cs" Inherits="CCOA_News_PopWindow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!--#include file="../inc/html_head.inc" -->
    <script type="text/javascript">

        function popwin(e) {
            //var txtSelect = this;
            var txtSelect = $("#txtSelect");

            mini.openTop({
                url: "test.aspx",
                showMaxButton: true,
                title: "弹出多选",
                width: 650,
                height: 380,
                onload: function () {
                    var iframe = this.getIFrameEl();
                    //iframe.contentWindow.SetData(null);
                },
                ondestory: function (action) {
                    if (action == "ok") {
                        var iframe = this.getIFrameEl();
                        var data = iframe.contentWindow.GetData();
                        data = mini.clone(data);
                        txtSelect.val(data);
                    }
                }
            });
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input id="txtSelect" type="text" />
        <a href="#" onclick="popwin()">选择</a>
    </div>
    </form>
</body>
</html>
