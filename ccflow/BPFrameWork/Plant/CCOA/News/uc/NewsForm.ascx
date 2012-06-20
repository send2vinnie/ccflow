<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsForm.ascx.cs" Inherits="CCOA_News_uc_NewsForm" %>
<div>
    <table style="width: 80%;">
        <tr>
            <th>
                栏目
            </th>
            <td>
                <asp:DropDownList ID="ddrChannel" runat="server" DataTextField="Name" DataValueField="No">
                </asp:DropDownList>
            </td>
            <td>
            </td>
        </tr>
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
                内容
            </th>
            <td>
                <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Height="240" />
            </td>
            <td>
            </td>
        </tr>
        <tr style="display: none;">
            <th>
                标题样式
            </th>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <th>
                文章类型
            </th>
            <td>
                <asp:DropDownList ID="ddrType" runat="server">
                    <asp:ListItem Selected="True" Value="0">原创</asp:ListItem>
                    <asp:ListItem Value="1">转发</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <th>
                副标题
            </th>
            <td>
                <asp:TextBox ID="txtSubTitle" runat="server" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <th>
                摘要
            </th>
            <td>
                <asp:TextBox ID="txtSummary" runat="server" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <th>
                关键字
            </th>
            <td>
                <asp:TextBox ID="txtKeyWords" runat="server" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <th>
                文章描述
            </th>
            <td>
                <asp:TextBox ID="txtDescribe" runat="server" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <th>
                排序
            </th>
            <td>
                <asp:TextBox ID="txtOrder" runat="server" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <th>
                选项
            </th>
            <td>
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
                <lizard:XButton ID="btnCommit" runat="server" Text="保存" OnClick="btnCommit_Click" />
                <lizard:XButton ID="btnCancle" runat="server" Text="取消" OnClick="btnCancle_Click" />
            </td>
            <td>
            </td>
        </tr>
    </table>
</div>
