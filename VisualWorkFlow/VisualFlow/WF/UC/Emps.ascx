<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Emps.ascx.cs" Inherits="WF_UC_Emps" %>
<script type="text/javascript">
function DoUp(no)
{
    var url="Do.aspx?DoType=EmpDoUp&RefNo="+no;
    val=window.showModalDialog( url , 'f4' ,'dialogHeight: 50px; dialogWidth: 6px; dialogTop: 10px; dialogLeft: 10px; center: yes; help: no'); 
    window.location.href=window.location.href;
    return;
}
function DoDown(no)
{
    var url="Do.aspx?DoType=EmpDoDown&RefNo="+no;
    val=window.showModalDialog( url , 'f4' ,'dialogHeight: 50px; dialogWidth: 6px; dialogTop: 10px; dialogLeft: 10px; center: yes; help: no'); 
    window.location.href=window.location.href;
    return;
}
</script>
