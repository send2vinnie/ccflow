﻿<%@ control language="C#" autoeventwireup="true" inherits="WF_UC_Emps, App_Web_h1ohljul" %>
<script type="text/javascript">
    function DoUp(no, keys) {
        var url = "Do.aspx?DoType=EmpDoUp&RefNo=" + no + '&dt=' + keys;
    val = window.showModalDialog(url, 'f4', 'dialogHeight: 5px; dialogWidth: 6px; dialogTop: 100px; dialogLeft: 100px; center: yes; help: no'); 
    window.location.href=window.location.href;
    return;
}
function DoDown(no,keys)
{
    var url = "Do.aspx?DoType=EmpDoDown&RefNo=" + no + '&sd=' + keys;
    val=window.showModalDialog( url , 'f4' ,'dialogHeight: 5px; dialogWidth: 6px; dialogTop: 100px; dialogLeft: 100px; center: yes; help: no'); 
    window.location.href=window.location.href;
    return;
}
</script>
