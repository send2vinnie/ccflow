<%@ page title="" language="C#" masterpagefile="~/WF/Admin/WinOpen.master" autoeventwireup="true" inherits="WF_Admin_FeatureSetUI, App_Web_h5o3zino" %>

<%@ Register src="UC/FeatureSet.ascx" tagname="FeatureSet" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:FeatureSet ID="FeatureSet1" runat="server" />
</asp:Content>

