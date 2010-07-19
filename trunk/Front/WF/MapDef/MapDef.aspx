<%@ Page Language="C#" MasterPageFile="~/WF/MapDef/WinOpen.master" AutoEventWireup="true" CodeFile="MapDef.aspx.cs" Inherits="WF_MapDef_MapDef" Title="无标题页" %>

<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="JavaScript" src="JS.js"></script>
    <script language="JavaScript" src="../JScript.js"></script>
	<base target="_self" />
	<script language=javascript>
	function HelpGroup()
	{
	   var msg='字段分组：就是把类似的字段放在一起，让用户操作更友好。\t\n比如：我们纳税人设计一个基础信息采集节点。';
	   msg+='在登记纳税人基础信息时，我们可以把基础信息、车船信息、房产信息、投资人信息分组。\t\n \t\n分组的格式为:@从字段名称1=分组名称1@从字段名称2=分组名称2 ,\t\n比如：节点信息设置，@NodeID=基本信息@LitData=考核信息。';
       alert( msg);
	}
	function DoGroupF( enName)
	{
	    var b=window.showModalDialog( 'GroupTitle.aspx?EnName='+enName , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
	}
	function Insert(mypk,IDX)
    {
        var url='Do.aspx?DoType=AddF&MyPK='+mypk+'&IDX=' +IDX ;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
	function AddF(mypk)
    {
        var url='Do.aspx?DoType=AddF&MyPK='+mypk;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    function AddTable(mypk)
    {
        var url='EditCells.aspx?MyPK='+mypk;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    function CopyFieldFromNode(mypk)
    {
        var url='CopyFieldFromNode.aspx?DoType=AddF&FK_Node='+mypk;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
     
    function Edit(mypk,refoid, ftype)
    {
        var url='EditF.aspx?DoType=Edit&MyPK='+mypk+'&RefOID='+refoid +'&FType=' + ftype;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    function EditEnum(mypk,refoid)
    {
        var url='EditEnum.aspx?DoType=Edit&MyPK='+mypk+'&RefOID='+refoid;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
     function EditTable(mypk,refoid)
    {
        var url='EditTable.aspx?DoType=Edit&MyPK='+mypk+'&RefOID='+refoid;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    
	function Up(mypk,refoid,idx)
    {
        var url='Do.aspx?DoType=Up&MyPK='+mypk+'&RefOID='+refoid+'&ToIdx='+idx;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        //window.location.href ='MapDef.aspx?PK='+mypk+'&IsOpen=1';
        window.location.href = window.location.href ;
    }
    function Down(mypk,refoid,idx)
    {
        var url='Do.aspx?DoType=Down&MyPK='+mypk+'&RefOID='+refoid +'&ToIdx='+idx;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
       //   window.location.href ='MapDef.aspx?PK='+mypk+'&IsOpen=1';
      //  window.location.href ='MapDef.aspx?PK='+mypk+'&IsOpen=1';
    }
    function Del(mypk,refoid)
    {
        if (window.confirm('您确定要删除吗？') ==false)
            return ;
    
        var url='Do.aspx?DoType=Del&MyPK='+mypk+'&RefOID='+refoid;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 600px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
	function Esc()
    {
        if (event.keyCode == 27)     
        window.close();
       return true;
    }
    function GroupBarClick( Field )
{

  var alt= document.getElementById('Img'+Field).alert ; 
    var sta='block';  
    if (alt=='Max' ) 
     {
         sta='block';
         alt='Min' ;
     } else {
       sta='none';    
       alt='Max'; 
      }
      
      document.getElementById('Img'+Field).src= './Img/'+ alt + '.gif';
      document.getElementById('Img'+Field).alert= alt  ;    
      
        
       var i=0
       for (i=0;i<=10;i++)
       {
          if (document.getElementById( Field + i )==null)
                   continue;
          document.getElementById( Field + i ).style.display= sta ;
      }
}
</script>
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%; background-color:White">
            <tr>
                <td style="height: 1px">
                <uc1:Pub ID="Pub1" runat="server" />
                </td>
            </tr>
        </table>
</asp:Content>

