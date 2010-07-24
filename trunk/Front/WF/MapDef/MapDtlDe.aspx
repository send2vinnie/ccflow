<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MapDtlDe.aspx.cs" Inherits="Comm_MapDef_MapDtlDe" %>
<%@ Register Src="../UC/UCEn.ascx" TagName="UCEn" TagPrefix="uc2" %>
<%@ Register Assembly="BP.Web.Controls" Namespace="BP.Web.Controls" TagPrefix="cc1" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>表单设计</title>
	<script language="JavaScript" src="JS.js"></script>
	<script language="JavaScript" src="../../Comm/JScript.js" ></script>
	<base target="_self" />
	<script language="javascript">
	function Insert(mypk,IDX)
    {
        var url='Do.aspx?DoType=AddF&MyPK='+mypk+'&IDX=' +IDX ;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 400px; dialogWidth: 600px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
	function AddF(mypk)
    {
        var url='Do.aspx?DoType=AddF&MyPK='+mypk;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 400px; dialogWidth: 600px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    function CopyF(mypk)
    {
        var url='CopyDtlField.aspx?MyPK='+mypk;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 400px; dialogWidth: 600px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    function Edit(mypk,refoid, ftype)
    {
        var url='EditF.aspx?DoType=Edit&MyPK='+mypk+'&RefOID='+refoid +'&FType=' + ftype;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 400px; dialogWidth: 600px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    function EditEnum(mypk,refoid)
    {
        var url='EditEnum.aspx?DoType=Edit&MyPK='+mypk+'&RefOID='+refoid;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 400px; dialogWidth: 600px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
     function EditTable(mypk,refoid)
    {
        var url='EditTable.aspx?DoType=Edit&MyPK='+mypk+'&RefOID='+refoid;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 400px; dialogWidth: 600px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    
	function Up(mypk,refoid)
    {
        var url='Do.aspx?DoType=Up&MyPK='+mypk+'&RefOID='+refoid;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 400px; dialogWidth: 600px;center: yes; help: no'); 
        //window.location.href ='MapDef.aspx?PK='+mypk+'&IsOpen=1';
        window.location.href = window.location.href ;
    }
    function Down(mypk,refoid)
    {
        var url='Do.aspx?DoType=Down&MyPK='+mypk+'&RefOID='+refoid;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 400px; dialogWidth: 600px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    function Del(mypk,refoid)
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
  <script language="javascript" for="document" event="onkeydown">
    if(event.keyCode==13)
       event.keyCode=9;
    </script>
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/Table.css" rel="stylesheet" type="text/css" />
</head>
<body topmargin="0" leftmargin="0" onkeypress="Esc()" border=0  style="padding:0px">
    <form id="form1" runat="server">
     <uc1:Pub ID="Pub1" runat="server" />
     </form>
</body>
</html>
