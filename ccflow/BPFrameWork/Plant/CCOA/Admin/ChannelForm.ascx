<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChannelForm.ascx.cs" Inherits="CCOA_Admin_ChannelForm" %>
<div>
    <table style="width: 80%;">
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
                URL地址
            </th>
            <td>
                <base:XTextBox ID="txtUrl" runat="server" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <th>
                描述
            </th>
            <td>
                <base:XTextBox ID="txtDescription" runat="server" TextMode="MultiLine" />
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
        <td ></td>
            <td >
                <base:XButton ID="btnCommit" runat="server" Text="保存" 
                    onclick="btnCommit_Click" />
                <base:XButton ID="btnCancle" runat="server" Text="取消" 
                    onclick="btnCancle_Click" />
            </td> <td ></td>
        </tr>
    </table>
</div>