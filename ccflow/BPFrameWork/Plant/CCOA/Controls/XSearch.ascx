<%@ Control Language="C#" AutoEventWireup="true" CodeFile="XSearch.ascx.cs" Inherits="control_XSearch" %>
<link href="../Style/cupertino/jquery-ui-1.8.14.custom.css" rel="stylesheet" type="text/css" />
<table cellspacing="0" style="vertical-align: middle;">
    <tr>
        <td>
            <asp:Label ID="lblCaption" runat="server" Text="选择要搜索的项：">
            </asp:Label>&nbsp;
        </td>
        <td>
            <asp:DropDownList ID="ddlFilter" runat="server" Width="100px">
                <asp:ListItem Text="<-全部->" Value="ALL" Selected="True" />
            </asp:DropDownList>
            <asp:TextBox ID="txtFilter" runat="server"></asp:TextBox>&nbsp;&nbsp;
        </td>
        <td>
            <div id="divDateRange" runat="server">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:Label ID="lblStartDate" runat="server" Text="开始日期："></asp:Label>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtStartDate" runat="server" class="date" Width="100px"></asp:TextBox>&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblEndDate" runat="server" Text="结束日期："></asp:Label>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtEndDate" runat="server" class="date" Width="100px"></asp:TextBox>&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
