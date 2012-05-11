<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CCOA/WinOpen.master"
    CodeFile="Modify.aspx.cs" Inherits="Lizard.OA.Web.EIP_LayoutDetail.Modify" Title="修改页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="tdbg">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr style="display: none;">
                        <td height="25" width="30%" align="right">
                            DetailId ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblDetailId" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            所在列 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtColumnNo" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            Id ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtPanelId" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            标题 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtPanelTitle" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            是否有收缩按钮 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:CheckBox ID="chkShowCollapseButton" runat="server" Checked="False" />
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            是否显示 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:CheckBox ID="chkIsShow" runat="server" Checked="False" />
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            宽度 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtWidth" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            高度 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtHeight" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tdbg" align="center" valign="bottom">
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" class="inputbutton"
                    onmouseover="this.className='inputbutton_hover'" onmouseout="this.className='inputbutton'">
                </asp:Button>
                <asp:Button ID="btnCancle" runat="server" Text="取消" OnClick="btnCancle_Click" class="inputbutton"
                    onmouseover="this.className='inputbutton_hover'" onmouseout="this.className='inputbutton'">
                </asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
