﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="KeySearch.ascx.cs" Inherits="WF_UC_KeySearch" %>
<%@ Register src="../Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<br />
<script type="text/javascript">
    function OpenIt(fk_flow, fk_node, workid) {
        var url = './WFRpt.aspx?WorkID=' + workid + '&FK_Flow=' + fk_flow + '&FK_Node=' + fk_node;
        var newWindow = window.open(url, 'card', 'width=700,top=50,left=50,height=500,scrollbars=yes,resizable=yes,toolbar=false,location=false');
        newWindow.focus();
        return;
    }
</script>
<font size=Large><b>&nbsp;输入关键字:</b></font><asp:TextBox ID="TextBox1" runat="server" BorderStyle=Inset
 BorderColor=AliceBlue
 Font-Bold="True" 
        Font-Size="Large" Width="259px"></asp:TextBox> 
<asp:CheckBox ID="CheckBox1" runat="server" Font-Bold="True" 
    ForeColor="#0033CC" Text="仅查询我参与的流程" />
说明:为了提高查询效率请正确的选择查询方式.
        <br />
    &nbsp;<asp:Button ID="Btn_ByWorkID" runat="server" Text="按工作ID查" Font-Bold="True" 
        Font-Size="Larger" onclick="Button1_Click" />
        <asp:Button ID="Btn_ByTitle" runat="server" Text="按流程标题字段关键字查" Font-Bold="True" 
        Font-Size="Larger" onclick="Button1_Click" />
        <asp:Button ID="Btn_ByAll" runat="server" Text="全部字段关键字查" Font-Bold="True" 
        Font-Size=Larger onclick="Button1_Click" />
        <hr />
    <uc1:Pub ID="Pub1" runat="server" />
