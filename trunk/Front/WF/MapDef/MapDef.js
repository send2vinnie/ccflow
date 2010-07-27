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
   if ( gfID > -1 && moveToFieldID > 0 && currFieldID != moveToFieldID )
   {
         var url='Do.aspx?DoType=MoveTo&FromID='+moveToFieldID+'&ToGFID='+gfID ;
        var b=window.showModalDialog( url , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
        window.location.href = window.location.href;
        moveToFieldID=0;
        gfID=-1;
        currFieldID=0;
        return;
   }

   if ( currFieldID>0  && moveToFieldID >  0 && currFieldID != moveToFieldID  )
   {
      var url1='Do.aspx?DoType=Jump&FromID='+currFieldID+'&ToID='+moveToFieldID ;
      var b1=window.showModalDialog( url1 , 'ass' ,'dialogHeight: 500px; dialogWidth: 700px;center: yes; help: no'); 
      window.location.href = window.location.href;
   } 
  currFieldID=0;
  moveToFieldID=0;
  gfID=-1;
  rowIdx=-1;
  return true;
}
    var currFieldID=0;
    var moveToFieldID=0;
    var isMove=false;
    var gfID=-1;
    var DrgType="";
    function FieldOnMouseOver(id)
    {
       currFieldID =id;
    }
    
    function FieldOnMouseOut(id)
    {
      currFieldID=0;
    }
    
    function GFOnMouseOver(id,rowIdx)
    {
       gfID =id;
       rowIdx =id;
    }
    
    function GFOnMouseOut()
    {
       gfID=-1;
       rowIdx=-1;
    }