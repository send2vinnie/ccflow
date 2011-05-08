<%@ page title="" language="C#" masterpagefile="WinOpen.master" autoeventwireup="true" inherits="WF_FlowSearchSmallSingle, App_Web_5dpdp204" %>
<%@ Register src="UC/FlowSearch.ascx" tagname="FlowSearch" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language=javascript>
        function Dtl(k) {
            WinOpen('DtlSearch.aspx?EnsName=ND' + parseInt(k) + 'Rpt', 'ss');
        }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:FlowSearch ID="FlowSearch1" runat="server" />
</asp:Content>

