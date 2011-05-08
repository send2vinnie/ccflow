<%@ control language="C#" autoeventwireup="true" inherits="WF_UC_Start, App_Web_h1ohljul" %>
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
</script>