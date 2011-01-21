var speed = 10; //数字越大速度越慢
var tab = document.getElementById("demo");
var tab1 = document.getElementById("demo1");
var tab2 = document.getElementById("demo2");
tab2.innerHTML = tab1.innerHTML; //克隆demo1为demo2
tab.scrollTop = tab.scrollHeight
function Marquee() {
    if (tab1.offsetTop - tab.scrollTop >= 0)//当滚动至demo1与demo2交界时
        tab.scrollTop += tab2.offsetHeight //demo跳到最顶端
    else {
        tab.scrollTop--
    }
}
var MyMar = setInterval(Marquee, speed);
tab.onmouseover = function () { clearInterval(MyMar) }; //鼠标移上时清除定时器达到滚动停止的目的
tab.onmouseout = function () { MyMar = setInterval(Marquee, speed) }; //鼠标移开时重设定时器