<%@ Page Language="C#" MasterPageFile="~/WF/MapDef/WinOpen.master" AutoEventWireup="true" CodeFile="MapDef.aspx.cs" Inherits="WF_MapDef_MapDef" Title="无标题页" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="JavaScript" src="JS.js"></script>
    <script language="JavaScript" src="../../Comm/JScript.js"></script>
    <script language="JavaScript" src="../../Comm/JS.js"></script>
    
    <script language="JavaScript" src="../../Comm/JS/Calendar.js"></script>
    <script language="javascript" >
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
    function GroupField(mypk)
    {
        var url='GroupField.aspx?RefNo='+mypk;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    function GroupFieldDel(mypk,refoid)
    {
        var url='GroupField.aspx?RefNo='+mypk+'&DoType=DelIt&RefOID='+refoid ;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    function GroupFieldNew(mypk)
    {
        var url='GroupField.aspx?RefNo='+mypk+'&DoType=New';
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
    }
    function GFDoUp(refoid)
    {
        var url='Do.aspx?DoType=GFDoUp&RefOID='+refoid ;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href ;
    }
    function GFDoDown(refoid)
    {
        var url='Do.aspx?DoType=GFDoDown&RefOID='+refoid ;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
    }
    function DtlDoUp(MyPK)
    {
        var url='Do.aspx?DoType=DtlDoUp&MyPK='+MyPK ;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href ;
    }
    function DtlDoDown(MyPK)
    {
        var url='Do.aspx?DoType=DtlDoDown&MyPK='+MyPK ;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
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
   
  function GroupBarClick( rowIdx )
  {
    var alt= document.getElementById('Img'+rowIdx).alert ; 
    var sta='block';  
    if (alt=='Max' ) 
     {
         sta='block';
         alt='Min' ;
         
      } else {
      
       sta='none';    
       alt='Max'; 
      
      }
      
      document.getElementById('Img'+rowIdx).src= './Img/'+ alt + '.gif';
      document.getElementById('Img'+rowIdx).alert= alt  ;    
      
       var i=0
       for (i=0;i<=40;i++)
       {
          if (document.getElementById( rowIdx +'_'+ i )==null)
                   continue;
          document.getElementById( rowIdx +'_' + i ).style.display= sta ;
      }
  }
   var isInser="";
  function ReinitIframe(dtlid) {

      try {
 
   
          var iframe = document.getElementById("F" + dtlid);
          var tdF = document.getElementById("TD" + dtlid);
          iframe.height = iframe.contentWindow.document.body.scrollHeight;
          iframe.width = iframe.contentWindow.document.body.scrollWidth ;
          
          if (tdF.width < iframe.width)
          {
             //alert(tdF.width +'  ' + iframe.width);
             tdF.width = iframe.width;
          }else
          {
           iframe.width =tdF.width ;
           
          }
          
          tdF.height = iframe.height;
          
         // alert("ss = " + tdF.width);
          // tdF.width = iframe.width;
          // alert(tdF.width);
          //  alert(iframe.width);
          //tdF.width = iframe.width;
          return;
      } catch (ex) {
      //alert(ex.message);
          return;
      }
      return;
      //      var bHeight = tdF.iframe.contentWindow.document.body.scrollHeight;
      //      var dHeight = iframe.contentWindow.document.documentElement.scrollHeight;
      //      var height = Math.max(bHeight, dHeight);
      //      iframe.height = height;
      //      tdF.height = height;
  }
  
  function CopyFieldFromNode( mypk )
  {
      var url='CopyFieldFromNode.aspx?FK_Node='+mypk ;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 600px;center: yes; help: no'); 
        window.location.href = window.location.href;
  }
  
    function EditDtl( mypk , dtlKey )
  {
      var url='MapDtl.aspx?DoType=Edit&FK_MapData=' + mypk +'&FK_MapDtl='+ dtlKey ;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 600px;center: yes; help: no'); 
        window.location.href = window.location.href;
  }
   function MapDtl( mypk  )
  {
      var url='MapDtl.aspx?DoType=DtlList&FK_MapData=' + mypk   ;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 600px;center: yes; help: no'); 
        window.location.href = window.location.href;
  }
  

</script>   
	<base target="_self" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <table border="0" cellpadding="0" cellspacing="0" >
            <tr>
                <td style="width: 1%; background-color:InfoBackground" class="BigDoc" valign=top>
              
<uc1:Pub ID="Left" runat="server" />
</td>
                <td style="width: 100%;"  valign=top>
                <uc1:Pub ID="Pub1" runat="server" />
                </td>
            </tr>
        </table>
</asp:Content>

