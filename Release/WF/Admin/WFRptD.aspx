<%@ page language="C#" autoeventwireup="true" inherits="WF_Admin_RptD, App_Web_0lc3lnp4" %>
<%@ Register Src="../../Comm/UC/ucsys.ascx" TagName="ucsys" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/Table.css" rel="stylesheet" type="text/css" />
    	<script language="JavaScript" src="../../Comm/JScript.js"></script>
		<script language="JavaScript" src="../../Comm/Menu.js"></script>
		<script language="JavaScript" src="../../Comm/Table.js"></script>
		
    <base target=_self /> 
    <script  language=javascript>
	function Insert(mypk,IDX)
    {
        var url='Do.aspx?DoType=AddF&MyPK='+mypk+'&IDX=' +IDX ;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 400px; dialogWidth: 600px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    function Edit(mypk,refoid, ftype)
    {
        var url='WFRptDo.aspx?DoType=Edit&MyPK='+mypk+'&RefOID='+refoid +'&FType=' + ftype;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 400px; dialogWidth: 600px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    
	function Up(mypk,refoid)
    {
        var url='WFRptDo.aspx?DoType=Up&MyPK='+mypk+'&RefOID='+refoid;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 400px; dialogWidth: 600px;center: yes; help: no'); 
        //window.location.href ='MapDef.aspx?PK='+mypk+'&IsOpen=1';
        window.location.href = window.location.href ;
    }
    function Down(mypk,refoid)
    {
        var url='WFRptDo.aspx?DoType=Down&MyPK='+mypk+'&RefOID='+refoid;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 400px; dialogWidth: 600px;center: yes; help: no'); 
        window.location.href = window.location.href;
       //   window.location.href ='MapDef.aspx?PK='+mypk+'&IsOpen=1';
      //  window.location.href ='MapDef.aspx?PK='+mypk+'&IsOpen=1';
    }
    function Del(mypk,refoid)
    {
        if (window.confirm('您确定要删除吗？\t\n\t\n提示：删除后您需要重新生成报表才成生效。') ==false)
            return ;
        var url='WFRptDo.aspx?DoType=Del&MyPK='+mypk+'&RefOID='+refoid;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 400px; dialogWidth: 600px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    function AddF(mypk, idx)
    {
        var url='WFRptD.aspx?DoType=AddF&RefNo='+mypk+'&IDX=' +idx;
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
<body topmargin="0" leftmargin="0" onkeypress="Esc()"  >
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;
            height: 100%">
            <tr>
                <td valign=top>
                   <uc2:ucsys ID="Ucsys1" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
