<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MiniToolBar.ascx.cs" Inherits="CCOA_Controls_MiniToolBar" %>
<div class="mini-toolbar" style="padding: 2px;">
    <table style="width: 100%;">
        <tr>
            <td style="width: 100%;">
                <a class="mini-button" iconcls="icon-addfolder" plain="true" href="<%=AddUrl %>">增加</a>
                <a class="mini-button" iconcls="icon-remove" plain="true">删除</a> 
                <span class="separator"></span>
                <a class="mini-button" iconcls="icon-reload" plain="true">刷新</a> 
                <a class="mini-button" iconcls="icon-download" plain="true">下载</a>
            </td>
            <td style="white-space: nowrap;">
                <label style="font-family: Verdana;">
                    Filter by:
                </label>
                <input class="mini-textbox" />
            </td>
        </tr>
    </table>
</div>
