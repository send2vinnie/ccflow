<%@ page language="C#" masterpagefile="~/WF/MapDef/WinOpen.master" autoeventwireup="true" inherits="WF_AllotTask, App_Web_cgmehdjt" title="无标题页" %>

<%@ Register src="UC/AllotTask.ascx" tagname="AllotTask" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="javascript" type="text/javascript" >
function RSize() {
    if (document.body.scrollWidth > (window.screen.availWidth - 100)) {
        window.dialogWidth = (window.screen.availWidth - 100).toString() + "px"
    } else {
        window.dialogWidth = (document.body.scrollWidth + 50).toString() + "px"
    }

    if (document.body.scrollHeight > (window.screen.availHeight - 70)) {
        window.dialogHeight = (window.screen.availHeight - 50).toString() + "px"
    } else {
        window.dialogHeight = (document.body.scrollHeight + 115).toString() + "px"
    }

    window.dialogLeft = ((window.screen.availWidth - document.body.clientWidth) / 2).toString() + "px"
    window.dialogTop = ((window.screen.availHeight - document.body.clientHeight) / 2).toString() + "px"
} 
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:AllotTask ID="AllotTask1" runat="server" />
</asp:Content>

