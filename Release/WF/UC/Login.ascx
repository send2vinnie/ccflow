<%@ control language="C#" autoeventwireup="true" inherits="WF_UC_Login, App_Web_h1ohljul" %>
<script  language=javascript>
function ExitAuth( fk_emp )
{
   if (window.confirm('您确定要退出授权登陆模式吗？')==false)
       return;
       
    var url='Do.aspx?DoType=ExitAuth&FK_Emp='+fk_emp;
    WinShowModalDialog(url,'');
    window.location.href='Tools.aspx';
}
</script>

