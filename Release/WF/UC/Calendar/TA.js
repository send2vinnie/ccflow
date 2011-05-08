function DoAction(url, msg)
{
           if ( confirm( msg )==false )
		      return;
            window.open( url , 'TakeBack', 'height=300, width=400, toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no, left=400,top=300' ); 
	        window.location.reload();
	       // window.location.reload();
}
        
function TurnToCellOpen(  data )
{
   window.location.href='CellOpen.aspx?RefDate='+data;
}
 
 /*
function TrackTree( oid  )
{
   var  url='TrackTree.aspx?RefOID='+oid;
   newWindow=window.open( url , 'tree', 'width=800,top=50,left=100,height=500,scrollbars=yes,resizable=yes,toolbar=false,location=false');
   newWindow.focus();
}
*/

function Track( oid  )
{
   var  url='Track.aspx?RefOID='+oid;
   newWindow=window.open( url , 'tree', 'width=800,top=50,left=100,height=500,scrollbars=yes,resizable=yes,toolbar=false,location=false');
   newWindow.focus();
}

function OpenLog( oid)
{
   var  url='Log.aspx?RefOID='+oid ;
   WinOpen(url,'log');
}

function WinOpen( url, winName)
{
      newWindow=window.open( url , winName, 'width=700,top=100,left=200,height=400,scrollbars=yes,resizable=yes,toolbar=false,location=false');
      newWindow.focus();
}
function OpenWork( date)
{
   var  url='Work.aspx?RefDate='+date;
   WinOpen(url,'task');
}



