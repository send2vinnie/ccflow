<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Forward.ascx.cs" Inherits="WF_UC_Forward" %>
<%@ Register src="../../Comm/UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc1" %>
<%@ Register src="FlowInfoSimple.ascx" tagname="FlowInfoSimple" tagprefix="uc2" %>
<table>
<TR>
<TD valign=top width=25%>
    <uc2:FlowInfoSimple ID="FlowInfoSimple1" runat="server" />
</TD>
<TD valign=top>
<table border=0  style="width: 100%; height: 100%">

            <tr>
                <td colspan=2  valign=top class=ToolBar >
                    <uc1:ToolBar ID="ToolBar1" runat="server" />
                </td>
            </tr>
            <tr>
            <td bgcolor=InfoBackground style="width: 20%"   valign=top>
            转发对象：<hr>
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                    </asp:CheckBoxList>
                </td>
            <td  valign=top>
                <%=BP.Sys.Language.GetValByUserLang("FNote", "转发原因")%> ：
                <asp:TextBox ID="TextBox1"  Text=''  runat="server" Height="270px" TextMode="MultiLine" Width="90%" Columns="18"></asp:TextBox></td>
            </tr>
</table>

</TD>
</TR>
</table>