
/* JS相册效果代码 */
var browse = window.navigator.appName.toLowerCase(); 
var MyMar; 
var speed = 1; //速度，越大越慢 
var spec = 1; //每次滚动的间距, 越大滚动越快 
var minOpa = 50; //滤镜最小值 
var maxOpa = 100; //滤镜最大值 
var spa = 2; //缩略图区域补充数值 
var w = 0; 
spec = (browse.indexOf("microsoft") > -1) ? spec : ((browse.indexOf("opera") > -1) ? spec*10 : spec*20); 
function $(e) {return document.getElementById(e);} 
function goleft() {$('photos').scrollLeft -= spec;} 
function goright() {$('photos').scrollLeft += spec;} 
function setOpacity(e, n) { 
if (browse.indexOf("microsoft") > -1) e.style.filter = 'alpha(opacity=' + n + ')'; 
else e.style.opacity = n/100; 
} 
$('goleft').style.cursor = 'pointer'; 
$('goright').style.cursor = 'pointer'; 
$('mainphoto').onmouseover = function() {setOpacity(this, maxOpa);openDiv2();} 
$('mainphoto').onmouseout = function() {setOpacity(this, minOpa);closeDiv();} 
$('mainphoto').onclick = function() {location = this.getAttribute('name');} 
$('goleft').onmouseover = function() {MyMar=setInterval(goleft, speed);} 
$('goleft').onmouseout = function() {clearInterval(MyMar);} 
$('goright').onmouseover = function() {MyMar=setInterval(goright,speed);} 
$('goright').onmouseout = function() {clearInterval(MyMar);} 
window.onload = function() { 
setOpacity($('mainphoto'), minOpa); 
var rHtml = ''; 
var p = $('showArea').getElementsByTagName('img'); 
for (var i=0; i<p.length; i++) { 
w += parseInt(p[i].getAttribute('width')) + spa; 
setOpacity(p[i], minOpa); 
p[i].onclick = function() {location = this.getAttribute('name');} 
p[i].onmouseover = function() { 
//设置渐隐效果
$('mainphoto').style.filter="blendTrans(duration=3)";
$('mainphoto').filters.blendTrans.Apply();
$('mainphoto').src=this.getAttribute('rel'); 
$('mainphoto').filters.blendTrans.Play();
//设置透明度
setOpacity(this, maxOpa); 
//$('mainphoto').src = this.getAttribute('rel'); 
$('mainphoto').setAttribute('name', this.getAttribute('name')); 
//设置注释层的内容
if(this.getAttribute('showNote')=='true')
{
 openDiv(this);
}
//setOpacity($('mainphoto'), maxOpa); 
} 
p[i].onmouseout = function() { 
setOpacity(this, minOpa); 
setOpacity($('mainphoto'), minOpa); 
closeDiv();
} 
rHtml += '<img src="' + p[i].getAttribute('rel') + '" width="0" height="0" alt="" />'; 
} 
$('showArea').style.width = parseInt(w) + 'px'; 
var rLoad = document.createElement("div"); 
$('photos').appendChild(rLoad); 
rLoad.style.width = "1px"; 
rLoad.style.height = "1px"; 
rLoad.style.overflow = "hidden"; 
rLoad.innerHTML = rHtml; 
} 


/*获取或设置弹出层的位置*/

$('Note').onmouseover = openDiv2;
$('Note').onmouseout = closeDiv;

function openDiv(e)
{
     $('Note').innerHTML=e.getAttribute('Note');
     if(e.getAttribute('NotePosition')=='Right')
     {
        openDivOnRight();
     }
     else
     {
        openDivOnLeft();
     }
}

function openDiv2()
{
     document.getElementById("Note").style.display="block";
}
function openDivOnRight()
{
    if(document.getElementById("Note").style.display=="none")
    {
        var txtObject = document.getElementById("mainphoto");
        var orgObject= document.getElementById("Note")  
        var rect = getoffset(txtObject);
        orgObject.style.top = rect[0] + 150;
        orgObject.style.left = rect[1] + 600;
        orgObject.style.display = "block";
        orgObject.focus();
    }
 }
 
 function openDivOnLeft()
{
    if(document.getElementById("Note").style.display=="none")
    {
        var txtObject = document.getElementById("mainphoto");
        var orgObject= document.getElementById("Note")  
        var rect = getoffset(txtObject);
        orgObject.style.top = rect[0] + 150;
        orgObject.style.left = rect[1] - 315;
        orgObject.style.display = "block";
        orgObject.focus();
    }
 }
 
function getoffset(e)
{  
     var t=e.offsetTop;  
     var l=e.offsetLeft;  
     while(e=e.offsetParent) 
     {  
          t+=e.offsetTop;  
          l+=e.offsetLeft;  
     }  
     var rec = new Array(1); 
     rec[0]  = t; 
     rec[1] = l; 
     return rec 
}

function closeDiv()
{  
    document.getElementById("Note").style.display="none";
}