<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Attachment.ascx.cs" Inherits="CCOA_Controls_Attachment" %>
<link href="../Style/control.css" rel="stylesheet" type="text/css" />
<div class="attachment" id="divAttachment" runat="server">
    <h3>
        普通附件</h3>
    <table class="attachment-file" width="100%" border="0">
        <% foreach (var item in AttachList)
           { %>
        <tr>
            <th class="fileicon" rowspan="3">
                图标
            </th>
        </tr>
        <tr>
            <td>
                <ul>
                    <li><a href='<%=item.FilePath %>'>
                        <%= item.FileNeme %></a> </li>
                </ul>
            </td>
        </tr>
        <tr>
            <td>
                <a href='<%=item.FilePath %>'>下载</a> | <a href="#">打开</a>| <a href="#">在线预览</a>
            </td>
        </tr>
        <%} %>
    </table>
</div>
