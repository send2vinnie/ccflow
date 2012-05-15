<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MiniToolBar.ascx.cs" Inherits="CCOA_Controls_MiniToolBar" %>
<script src='<%=Page.ResolveUrl("~/Comm/Scripts/jquery-1.6.2.min.js") %>' type="text/javascript"></script>
<script src='<%=Page.ResolveUrl("~/Comm/Scripts/miniui/miniui.js") %>' type="text/javascript"></script>
<link href='<%=Page.ResolveUrl("~/Comm/Scripts/miniui/themes/icons.css") %>' rel="stylesheet"
    type="text/css" />
<link href="../../Comm/Scripts/miniui/themes/default/miniui.css" rel="stylesheet"
    type="text/css" />
<script type="text/javascript">
    function getsearchvalue() {
        var searchVaue = mini.get("#txtValue").getValue();
        window.location.href = "list.aspx?searchvalue=" + searchVaue;
    }
</script>
<div class="mini-toolbar">
    <table style="width: 100%;">
        <tr>
            <td style="width: 100%;">
                <a class="mini-button" iconcls="icon-reload" plain="true" href="<%=RetrunUrl %>">返回</a>
                <a class="mini-button" iconcls="icon-addfolder" plain="true" href="<%=AddUrl %>">增加</a>
                <a class="mini-button" iconcls="icon-remove" plain="true">删除</a> <span class="separator">
                </span><a class="mini-button" iconcls="icon-reload" plain="true" href="<%=RefreshUrl %>">
                    刷新</a> <a class="mini-button" iconcls="icon-download" plain="true">下载</a>
            </td>
            <td style="white-space: nowrap;">
                <input id="txtValue" class="mini-textbox" />
                <a class="mini-button" plain="true" onclick="getsearchvalue()">查询</a>
            </td>
        </tr>
    </table>
</div>
