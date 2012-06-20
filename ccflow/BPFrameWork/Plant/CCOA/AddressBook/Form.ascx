<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Form.ascx.cs" Inherits="CCOA_AddressBook_Form" %>

<table style="width: 80%;">
    <tr>
        <th>
            姓名
        </th>
        <td>
            <asp:TextBox ID="txtTitle" runat="server" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <th>
            分类
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
           电话
        </th>
        <td>
            <asp:TextBox ID="txtTel" runat="server" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <th>
            邮箱
        </th>
        <td>
            <asp:TextBox ID="txtEmail" runat="server" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <th>
            地址
        </th>
        <td>
            <asp:TextBox ID="txtAddress" runat="server" />
        </td>
        <td>
        </td>
    </tr>
    
    <tr>
        <th>
            职责
        </th>
        <td>
            <asp:TextBox ID="txtDuty" runat="server" />
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
