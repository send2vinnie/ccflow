﻿<%@ page title="" language="C#" masterpagefile="WinOpen.master" autoeventwireup="true" inherits="WF_RuningSmall, App_Web_5dpdp204" %>
<%@ Register src="UC/Runing.ascx" tagname="Runing" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="JavaScript" src="./../Comm/JScript.js"></script>
    <script language=javascript>
        function Do(warning, url) {
            if (window.confirm(warning) == false)
                return;

            window.location.href = url;
            // WinOpen(url);
        }
		</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Runing ID="Runing1" runat="server" />
</asp:Content>

