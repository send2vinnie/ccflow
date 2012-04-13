﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsForm.ascx.cs" Inherits="CCOA_News_uc_NewsForm" %>
<div>
    <table style="width: 80%;">
        <tr>
            <th>
                栏目
            </th>
            <td>
                <base:XDropDownList ID="ddrChannel" runat="server" DataTextField="Name" DataValueField="No">
                </base:XDropDownList>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <th>
                标题
            </th>
            <td>
                <base:XTextBox ID="txtTitle" runat="server" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <th>
                内容
            </th>
            <td>
                <base:XTextBox ID="txtContent" runat="server" TextMode="MultiLine" Height="240" />
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
                <base:XDropDownList ID="ddrType" runat="server">
                    <asp:ListItem Selected="True" Value="0">原创</asp:ListItem>
                    <asp:ListItem Value="1">转发</asp:ListItem>
                </base:XDropDownList>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <th>
                副标题
            </th>
            <td>
                <base:XTextBox ID="txtSubTitle" runat="server" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <th>
                摘要
            </th>
            <td>
                <base:XTextBox ID="txtSummary" runat="server" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <th>
                关键字
            </th>
            <td>
                <base:XTextBox ID="txtKeyWords" runat="server" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <th>
                文章描述
            </th>
            <td>
                <base:XTextBox ID="txtDescribe" runat="server" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <th>
                排序
            </th>
            <td>
                <base:XTextBox ID="txtOrder" runat="server" />
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
                <base:XDropDownList ID="ddlState" runat="server" Width="80">
                    <asp:ListItem Text="启用" Value="1" />
                    <asp:ListItem Text="禁用" Value="0" />
                </base:XDropDownList>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <base:XButton ID="btnCommit" runat="server" Text="保存" OnClick="btnCommit_Click" />
                <base:XButton ID="btnCancle" runat="server" Text="取消" OnClick="btnCancle_Click" />
            </td>
            <td>
            </td>
        </tr>
    </table>
</div>
