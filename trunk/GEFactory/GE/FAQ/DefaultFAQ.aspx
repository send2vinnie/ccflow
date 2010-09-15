<%@ Page Language="C#" MasterPageFile="~/Style/V2.master" AutoEventWireup="true"
    CodeFile="DefaultFAQ.aspx.cs" Inherits="FAQ_DefaultFAQ" Title="资源请求详细页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
    function reinitIframe() {
        var iframe = document.getElementById("frmBody");
        try {
            var bHeight = iframe.contentWindow.document.body.scrollHeight;
            var dHeight = iframe.contentWindow.document.documentElement.scrollHeight;
            var height = Math.max(bHeight, dHeight); iframe.height = height;
        } catch (ex) { } 
    }
         window.onload = function() {
         var param = window.location.href.split('?')[1];

         document.getElementById('frmBody').src="/Edu/FAQ/InitDesc.aspx?" + param;
     }     
window.setInterval("reinitIframe()", 200);</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <iframe name='frmBody' id="frmBody" frameborder="0" marginheight="0" marginwidth="0"
        border="0" id="alimamaifrm" name="frm" scrolling="no" width="100%" onload="this.height=100"
        src=""></iframe>
</asp:Content>
