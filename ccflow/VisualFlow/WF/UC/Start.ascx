<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Start.ascx.cs" Inherits="WF_UC_Start" %>
<script type="text/javascript">
    function StartListUrl(url, fk_flow, pageid) {
        var v = window.showModalDialog(url, 'sd', 'dialogHeight: 550px; dialogWidth: 650px; dialogTop: 100px; dialogLeft: 150px; center: yes; help: no');
        //alert(v);
        if (v == null || v == "")
            return;
        // alert(v);
        //alert('');
        window.location.href = 'MyFlow' + pageid + '.aspx?FK_Flow=' + fk_flow + v;
    }
    function WinOpenIt(url) {
        var newWindow = window.open(url, 'newwindow', 'height=600,width=850,top=50,left=50,toolbar=no,menubar=no,scrollbars=no, resizable=yes,location=no, status=no');
        newWindow.focus();
        return;
    }
</script>