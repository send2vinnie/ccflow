// JScript 文件

function TROver(ctrl)
{
   ctrl.style.backgroundColor='LightSteelBlue';
}

function TROut(ctrl)
{
  ctrl.style.backgroundColor='white';
}

function WinOpen( url ,name )
{
  var newWindow=window.open( url  , name, 'width=900,top=60,left=60,height=500,scrollbars=yes,resizable=yes,toolbar=false,location=false' );
}
  
 

function VirtyDateTime(ctrl)
{
    if (event.keyCode == 58)
        return true;       
        
    if (event.keyCode == 45)
        return true;
        
    if (event.keyCode == 13)
        return true;
        
    if (event.keyCode < 48 || event.keyCode > 57)
        return false;
    else
        return true;
}
