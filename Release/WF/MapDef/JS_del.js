﻿// JScript 文件

function TROver(ctrl)
{
   ctrl.style.backgroundColor='LightSteelBlue';
}

function TROut(ctrl)
{
  ctrl.style.backgroundColor='white';
}
function VirtyNum(ctrl) {
    if (event.keyCode == 229)
        return true;

    if (event.keyCode <= 105 && event.keyCode >= 96)
        return true;

    if (event.keyCode == 8 || event.keyCode == 190)
        return true;

    if (event.keyCode == 13)
        return true;

    if (event.keyCode == 46)
        return true;

    if (event.keyCode == 45)
        return true;

    if (event.keyCode < 48 || event.keyCode > 57)
        return false;
    else
        return true;
}

/* 输入的是否是字段类型 */
function IsDigit(s) {

    var patrn = new RegExp("^[a-zA-Z][a-zA-Z0-9_]*$");
    if (!patrn.exec(s.value)) {
        alert("请输入字母或数字，第一个字符必须是字母！")
        s.value = "";
        return false;
    }
    return true;
}
 

//    var patrn = '^[a-zA-Z][a-zA-Z0-9_]*$';

//   alert(s.value);

//   try
//   {
//    if (!patrn.exec(s.value)) {

//        alert('ssss');
//        s.value = '';
//        return false
//    }
//    alert('ssdddddd');
//    return true
//    }catch
//    {
//       
//    }
 

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

function RSize() {
    if (document.body.scrollWidth > (window.screen.availWidth - 100)) {
        window.dialogWidth = (window.screen.availWidth - 100).toString() + "px"
    } else {
        window.dialogWidth = (document.body.scrollWidth + 50).toString() + "px"
    }

    if (document.body.scrollHeight > (window.screen.availHeight - 70)) {
        window.dialogHeight = (window.screen.availHeight - 50).toString() + "px"
    } else {
        window.dialogHeight = (document.body.scrollHeight + 115).toString() + "px"
    }

    window.dialogLeft = ((window.screen.availWidth - document.body.clientWidth) / 2).toString() + "px"
    window.dialogTop = ((window.screen.availHeight - document.body.clientHeight) / 2).toString() + "px"
} 
function Esc()
    {
        if (event.keyCode == 27)     
        window.close();
       return true;
    }
