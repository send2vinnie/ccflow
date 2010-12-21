<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="FlowSearch.aspx.cs"
 Inherits="Face_FlowSearch" Title="查询与分析" %>
<%@ Register src="UC/FlowSearch.ascx" tagname="FlowSearch" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language=javascript>
function Dtl(k)
{
  WinOpen('DtlSearch.aspx?EnsName=ND'+parseInt( k) +'Rpt' , 'ss');
}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:FlowSearch ID="FlowSearch1" runat="server" />
</asp:Content>

