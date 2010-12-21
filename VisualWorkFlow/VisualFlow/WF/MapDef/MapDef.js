document.onmousedown = mouseDown;
document.onmouseup = mouseUp;
document.onmousemove = mouseMove;

function mouseDown()
{
   moveToFieldID = currFieldID;
    return true;
}
function mouseUp()
{

   if ( (gfID !=null && moveToFieldID !=null ) && (currFieldID != moveToFieldID) )
   {
        var url='Do.aspx?DoType=MoveTo&FromID='+moveToFieldID+'&ToGFID='+gfID+'&ToID='+moveToFieldID ;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
        moveToFieldID=null;
        gfID=null;
        currFieldID=null;
        return;
   }
   
//   if ( currFieldID!=null  && moveToFieldID !=null  && currFieldID != moveToFieldID  )
//   {
//      var url1='Do.aspx?DoType=Jump&FromID='+currFieldID+'&ToID='+moveToFieldID ;
//      var b1=window.showModalDialog( url1 , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no');
//      window.location.href = window.location.href;
//   }
    
  currFieldID=null;
  moveToFieldID=null;
  gfID=null;
  rowIdx=-1;
  return true;
}
    var currFieldID=null;
    var moveToFieldID=null;
    var isMove=false;
    var gfID=null;
    var DrgType="";
    function FieldOnMouseOver(id)
    {
//alert("FieldOnMouseOver = currFieldID="+id);
       currFieldID =id;
    }
    
    function FieldOnMouseOut(id)
    {
//alert("FieldOnMouseOut currFieldID=0" );
      currFieldID=null;
    }
    
    function GFOnMouseOver(id,rowIdx)
    {
//alert("GFOnMouseOver"+id+" - " +rowIdx );
       gfID =id;
       rowIdx =id;
    }
    
    function GFOnMouseOut()
    {
       gfID=null;
       rowIdx=-1;
    //   alert("GFOnMouseOver gfID="+gfID+" - rowIdx=" +rowIdx );
    }