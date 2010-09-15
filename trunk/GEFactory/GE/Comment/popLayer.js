//弹出层
function HiddenLayer()
{
    document.getElementById("doing").style.display="none"; 
    document.getElementById("divLogin").style.display="none";
}
function ShowNo()                        //隐藏两个层 
{ 
    document.getElementById("doing").style.display="none";
    document.getElementById("divLogin").style.display="none";
} 
function $(id)        
{ 
    return (document.getElementById) ? document.getElementById(id) : document.all[id] ; 
} 
function showFloat()                    //根据屏幕的大小显示两个层 
{ 
    var range = getRange(); 
    $('doing').style.width = range.width + "px"; 
    $('doing').style.height = range.height + "px"; 
    $('doing').style.display="block";
    document.getElementById("divLogin").style.display="block";
    document.getElementById("eWebEditor1").style.width = "621px"; 
    document.getElementById("eWebEditor1").style.height = "458px"; 
} 

function getRange()                      //得到屏幕的大小 
{
    var top = Math.max(document.body.scrollTop, document.documentElement.scrollTop);
    var left = Math.max(document.body.scrollLeft, document.documentElement.scrollLeft);
    var height = Math.max(document.body.scrollHeight, document.documentElement.scrollHeight);
    var width = Math.max(document.body.clientWidth, document.documentElement.clientWidth);
    return { top: top, left: left, height: height, width: width };
} 

//弹出模式对话框
function showWindow(url)
{
    var arg = window.showModalDialog(url,window,"dialogWidth=630px;dialogHeight=400px;status=no;scroll=no;"); 
    if(arg == true)
    {
        window.location.href = window.location.href;
    }
}
function MyAlert() 
{
    alert("对不起请先登陆!");
}