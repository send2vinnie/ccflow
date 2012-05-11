<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChannelForm.ascx.cs" Inherits="CCOA_Admin_ChannelForm" %>
<div style="border: 1px solid #e5e5e5;">
    <table width="100%">
        <tr>
            <th>
                标题
            </th>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <th>
                URL地址
            </th>
            <td>
                <asp:TextBox ID="txtUrl" runat="server" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <th>
                描述
            </th>
            <td>
                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Height="60px" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <th>
                类型
            </th>
            <td>
                <asp:DropDownList ID="ddlTpye" runat="server" Width="80">
                    <asp:ListItem Text="新闻" Value="0" />
                    <asp:ListItem Text="公告" Value="1" />
                </asp:DropDownList>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <th>
                状态
            </th>
            <td>
                <asp:DropDownList ID="ddlState" runat="server" Width="80">
                    <asp:ListItem Text="启用" Value="1" />
                    <asp:ListItem Text="禁用" Value="0" />
                </asp:DropDownList>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="btnCommit" runat="server" Text="保存" OnClick="btnCommit_Click" />
                <asp:Button ID="btnCancle" runat="server" Text="取消" OnClick="btnCancle_Click" />
            </td>
            <td>
            </td>
        </tr>
    </table>
</div>
