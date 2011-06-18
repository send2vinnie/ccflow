<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ForwardUC.ascx.cs" Inherits="WF_UC_Forward_UC" %>
<%@ Register src="../../Comm/UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc1" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc2" %>
<table border=0  style="width: 500px; height: 100%" align=center>

            <tr>
                <td colspan=2  valign=top class=ToolBar align=left>
                    <uc1:ToolBar ID="ToolBar1" runat="server" />
                </td>
            </tr>
            <tr>
            <td bgcolor=InfoBackground style="width: 200px" align=right  valign=top>
            转发对象：<hr>
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                    </asp:CheckBoxList>
                </td>
            <td  valign=top style="width: 300px" align=left>
                <uc2:Pub ID="Pub1" runat="server" />
                </td>
            </tr>
</table>
