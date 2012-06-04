<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MiniToolBar.ascx.cs" Inherits="CCOA_Controls_MiniToolBar" %>
<script src='<%=Page.ResolveUrl("~/Comm/Scripts/jquery-1.6.2.min.js") %>' type="text/javascript"></script>
<script src='<%=Page.ResolveUrl("~/Comm/Scripts/miniui/miniui.js") %>' type="text/javascript"></script>
<link href='<%=Page.ResolveUrl("~/Comm/Scripts/miniui/themes/icons.css") %>' rel="stylesheet"
    type="text/css" />
<link href="../../Comm/Scripts/miniui/themes/default/miniui.css" rel="stylesheet"
    type="text/css" />
<script type="text/javascript">
    //mini.parse();

    function getsearchvalue() {
        var searchVaue = mini.get("#txtValue").getValue();
        //window.location.href = "list.aspx?searchvalue=" + searchVaue;
        window.location.href = "<%=RefreshUrl %>?searchvalue=" + searchVaue;
    }

    var idList = "";
    function getSelectedIdList() {

        if (!confirm("确定要删除选中的数据吗？")) {
            return;
        }

        var chks = $("input[type='checkbox']:checkbox:checked");
        //var hiddens = $("input[type='hidden']");
        for (var i = 0; i < chks.length; i++) {
            var regS = new RegExp("DeleteThis", "g");
            var hiddenId = chks[i].id.replace(regS, "DeleteNo");
            var chkedNo = $("#" + hiddenId).val();
            idList = idList + "'" + chkedNo + "',";
        }
        if (idList.length > 0) {
            idList = idList.substring(0, idList.length - 1);
        }

        $.ajax({
            url: "Delete.aspx?idList=" + idList,
            success: function (data) {
                window.location.href = "<%=RefreshUrl %>";
                //alert(data);
            },
            error: function () {
                alert('删除失败！');
            }
        });
    }

    function add() {
        //alert("start");
        mini.openTop({
            url: "Port_Dept/Add.aspx",
            title: "新增", width: 600, height: 360,
            onload: function () {
                //alert("success!");
                //var iframe = this.getIFrameEl();
                //var data = { action: "new" };
                //iframe.contentWindow.SetData(data);
            },
            ondestory: function (action) {
                //grid.reload();
            }
        });
    }
</script>
<div class="mini-toolbar">
    <table style="width: 100%;">
        <tr>
            <td style="width: 100%;">
                <div id="ButtonContainers" runat="server" style="float: left;">
                </div>
                <span class="separator"></span><a class="mini-button" iconcls="icon-reload" plain="false"
                    href="<%=RefreshUrl %>">刷新</a> <a class="mini-button" iconcls="icon-download" plain="false">
                        下载</a>
            </td>
            <td style="white-space: nowrap;">
                <input id="txtValue" class="mini-textbox" />
                <a class="mini-button" plain="true" onclick="getsearchvalue()">查询</a>
            </td>
        </tr>
    </table>
</div>
