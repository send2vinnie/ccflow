		    
    function ShowEn(url, wName)
    {
       val=window.showModalDialog( url , wName ,'dialogHeight: 550px; dialogWidth: 650px; dialogTop: 100px; dialogLeft: 150px; center: yes; help: no'); 
       window.location.href=window.location.href;
    }
    
    function OpenAttrs(ensName)
    {
	       var url= './Sys/EnsAppCfg.aspx?EnsName='+ensName;
           var s =  'dialogWidth=680px;dialogHeight=480px;status:no;center:1;resizable:yes'.toString() ;
		   val=window.showModalDialog( url , null ,  s);
           window.location.href=window.location.href;
    }
　　function selectAll()
　　{
　　   var arrObj=document.all;
　　   if(document.aspnetForm.checkedAll.checked)
　　   {
　　     for(var i=0;i<arrObj.length;i++)
　　     {
　　         if(typeof arrObj[i].type != "undefined" && arrObj[i].type=='checkbox') 
　　          {
　　           if (arrObj[i].name.indexOf('IDX_') > 0 )
　　              arrObj[i].checked =true;
　         　 }
　　      }
　　      document.aspnetForm.Toolbar$Btn_Delete.enable=true;
　　    }else{
　　      document.aspnetForm.Toolbar$Btn_Delete.enable=false;
　　     for(var i=0;i<arrObj.length;i++)
　　      {
　     　   if(typeof arrObj[i].type != "undefined" && arrObj[i].type=='checkbox') 
　     　   {
　　           if (arrObj[i].name.indexOf('IDX_') > 0 )
　     　             arrObj[i].checked =false;
　     　    }
　     　 }
　　    }
   }