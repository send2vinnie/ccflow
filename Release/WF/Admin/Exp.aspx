<%@ page language="C#" autoeventwireup="true" inherits="WF_Admin_Exp, App_Web_0lc3lnp4" %>
<%@ Register Src="../../Comm/UC/ucsys.ascx" TagName="ucsys" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>数据转出接口</title>
    <link href="../../Comm/Style.css" rel="stylesheet" type="text/css" />
    <link href="../../Comm/Table.css" rel="stylesheet" type="text/css" />
    <script  language=javascript>
    function Del(mypk, fk_flow, refoid)
    {
        if (window.confirm('您确定要删除吗？') ==false)
            return ;
    
        var url='Do.aspx?DoType=Del&MyPK='+mypk+'&RefOID='+refoid;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 400px; dialogWidth: 600px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    function Esc()
    {
        if (event.keyCode == 27)     
        window.close();
       return true;
    }
    </script>
</head>
<body topmargin="0" leftmargin="0" onkeypress="Esc()" >
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td style="height:1px">
                    <uc1:ucsys ID="Ucsys1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
