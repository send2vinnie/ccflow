 
function ZBOfDB(fk_work,fk_taxpayer,fk_zb)
{
   var url= "ZBOfDB.aspx?FK_Work="+fk_work+"&FK_ZB="+fk_zb+"&FK_Taxpayer="+fk_taxpayer;
   //var a=window.showModalDialog( url , 'OneVs' ,'dialogHeight: 600px; dialogWidth: 800px; dialogTop: 100px; dialogLeft: 110px; center: yes; help: no'); 
   WinOpen(url, 'Hand'+fk_zb );
}

function ZBInfo(pk)
{
   var url= "../Comm/UIEn.aspx?ClassName=BP.PG.DTOfLHs&PK="+pk;
   var a=window.showModalDialog( url , 'OneVs' ,'dialogHeight: 600px; dialogWidth: 800px; dialogTop: 100px; dialogLeft: 110px; center: yes; help: no'); 
   //WinOpen(url, 'Hand');
}

function Comment( fk_work , fk_taxpayer, fk_zb )
{
   var url= "Comment.aspx?FK_Work="+fk_work+"&FK_Taxpayer="+fk_taxpayer+"&FK_ZB="+fk_zb;
   var a=window.showModalDialog( url , 'OneVs' ,'dialogHeight: 400px; dialogWidth: 650px;  center: yes; help: no'); 
   window.location.reload();
}


function OpenHandWork(fk_work,fk_datasource, qj )
{
   var url='HandWork.aspx?FK_Work='+fk_work+"&FK_DataSource="+fk_datasource+"&QJ="+qj;
  WinOpen(url, 'Hand');
  // var a=window.showModalDialog( url , 'OneVs' ,'dialogHeight: 600px; dialogWidth: 800px;  center: yes; help: no'); 
}

function OpenHandWorkHY(fk_work,fk_datasource, qj )
{
   var url='HandWork.aspx?PGType=HY&FK_Work='+fk_work+"&FK_DataSource="+fk_datasource+"&QJ="+qj;
   WinOpen(url, 'Hand');
  // var a=window.showModalDialog( url , 'OneVs' ,'dialogHeight: 600px; dialogWidth: 800px;  center: yes; help: no'); 
  
}

function ToCJ(fk_work,fk_datasource,qj, fk_taxpayer)
{
    // window.location.href='HandWork.aspx?PGType=HY&FK_Work='+fk_work+"&FK_DataSource="+fk_datasource+"&QJ="+qj +"&FK_Taxpayer="+fk_taxpayer; 
    var url ='HandWork.aspx?PGType=HY&FK_Work='+fk_work+"&FK_DataSource="+fk_datasource+"&QJ="+qj +"&FK_Taxpayer="+fk_taxpayer; 
     window.location.href=url;
   // window.href
      // var url='HandWork.aspx?DoType=ExcelOfCJ&FK_Work='+fk_work+'&FK_Taxpayer='+fk_taxpayer ;
    //var v= window.showModalDialog(url,'lg', 'dialogHeight: 550px; dialogWidth: 850px; dialogTop: 100px; dialogLeft: 150px; center: yes; help: no');
   // if (v==1)
        //window.location.reload();
	return;
}

function Submit(url)
{
   if (confim('您确定要提交评估信息吗？')==false)
        return;
   To(url);
}

function Interval(fk_work,fk_zb)
{
   var url='Interval.aspx?PGType=HY&FK_Work='+fk_work+"&FK_ZB="+fk_zb ;
   WinOpen(url,'sdsd');
}
