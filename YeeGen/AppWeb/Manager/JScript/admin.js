//字符处理;
//去左右空格; 
function trim(s){
 	return rtrim(ltrim(s)); 
}
//去左空格; 
function ltrim(s){
 	return s.replace( /^\s*/, ""); 
} 
//去右空格; 
function rtrim(s){ 
 	return s.replace( /\s*$/, "");
}

/************************************
//验证输入
************************************/
/*输入实时验证*/
// 只允许输入数字和回车键 调用：onkeypress="return isDigit(event.keyCode|event.which);"
function IsDigit(code){
    return ((code >= 48 && code <= 57) || code == 13 || code == 8) ? true : false;
}

//只允许输入数字（支持IE、ff）调用：onkeypress="return IsDigit(event);" 
//function IsDigit(e){
//    var key = window.event ? e.keyCode : e.which;
//    var keychar = String.fromCharCode(key);
//    var reg = /\d/;
//    
//    var result = reg.test(keychar);
//    if(!result)
//        return false;
//    else
//        return true;
//}

//身份证号码输入验证：只允许数字、x和X(del和backspace的值分别为8和46)
function IsCardNo(code){
	return ((code >= 48 && code <= 57) || (code == 13) || (code == 88) || (code == 120)) ? true : false;
}

//电话号码输入验证：只允许数字、()和-
function IsTel(code){
    return ((code >= 48 && code <= 57) || (code == 13) || (code == 40) || (code == 41) || (code == 45) || code == 8 || code == 46) ? true : false;
}

// 只允许输入数字和“.”  浮点数输入；
function IsFloat(code){
    return ((code >= 48 && code <= 57) || code == 13 || code == 8 || (code == 46)) ? true : false;
}

// 只允许输入数字和“,”；
function IsCode(code){
    return ((code >= 48 && code <= 57) || code == 13 || code == 8 || (code == 44)) ? true : false;
}

//***********金额输入时判断“.”输入个数是否合法
function judgedotnum(str){
    var num = 0;
    if (str.charAt(0) == '.'){
        num = 1;
    }else{
        for(var i=0; i < str.length; i++){
            var c = str.charAt(i);
            if (c == '.'){
                num++;
            }
        }
    }
    return num;
}

function MenuOnMouseOver(obj) {
    obj.className = 'menubar_button';
}

function MenuOnMouseOut(obj) {
    obj.className = 'menubar_button_on';
}

//显示/隐藏层函数
function showDiv(div)
{
	var el = document.getElementById(div);
	el.style.display = (el.style.display == 'none') ? 'block' : 'none';
}