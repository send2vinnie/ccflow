
var  Currdate ;  // 类名称．　实体名称．
var  Currtime;   // 外键，主键，枚举类型形成的 string.
var  WebPath;    // 虚拟当前的虚拟目录.

/* 在Datagirn  */
function OnDGMousedown(id, date, time )
{
   if ( event.button != 2)
      return true;
      
    Currdate = date;
    Currtime = time;   
    ShowMenu( id  );
    document.oncontextmenu= new Function("event.returnValue=false;");
}
function OnDGMousedown(id, date)
{
   if ( event.button != 2)
      return true;
      
    Currdate = date;   
    ShowMenu( id  );
    document.oncontextmenu= new Function("event.returnValue=false;");
}

/* 隐藏 Menum */
function HideMenu( id )
{
 // alert ( id );
  document.getElementById( id ).style.visibility='hidden';
 // alert ( id );
}

/* 显示 Menum */
function ShowMenu(id )
{
   var rightedge=document.body.clientWidth-event.clientX;
   var bottomedge=document.body.clientHeight-event.clientY;
      
   if (rightedge < document.getElementById( id ).offsetWidth )
      document.getElementById( id ).style.left=document.body.scrollLeft+event.clientX-document.getElementById( id ).offsetWidth;
   else
      document.getElementById( id ).style.left=document.body.scrollLeft+event.clientX;

   if (bottomedge< document.getElementById( id ).offsetHeight)
      document.getElementById( id ).style.top=document.body.scrollTop+event.clientY-document.getElementById( id ).offsetHeight;
   else
      document.getElementById( id ).style.top=document.body.scrollTop+event.clientY;

   document.getElementById( id ).style.visibility="visible";
}

function MTROn1(ctrl)
{
   ctrl.style.backgroundColor='royalblue';
}
function MTROut1(ctrl)
{
   ctrl.style.backgroundColor='Menu';
}
 
 /* 新建 */
function NewLog()
{
  var url = 'Log.aspx?RefDate='+Currdate+'&RefTime='+Currtime ;
  NewIt(url);
}

function NewMail()
{
   var url = './Mail/compose.aspx?RefDate='+Currdate+'&RefTime='+Currtime+"'";
   NewIt(url);
}

function NewWork()
{
   var url = "Work.aspx?RefDate="+Currdate+"&RefTime"+Currtime+"&Step=1";
   NewIt(url);
   window.location.reload();
   window.location.reload();
}

function Share()
{
   var url ='Share.htm';
   window.location.href=url;
}

function NewTask()
{
   var url ='Task.aspx?RefDate='+Currdate+'&RefTime='+Currtime+"'";
   NewIt(url);
}

function NewCycleEvent()
{
  var url = 'CycleEvent.aspx?RefDate='+Currdate+'&RefTime='+Currtime+"'";
  NewIt(url);
}

function NewNotepad()
{
  var url = 'Notepad.aspx?RefDate='+Currdate+'&RefTime='+Currtime+"'";
  NewIt(url);
}


 /* 打开 */
function OpenLog(oid)
{
  var url = 'Log.aspx?RefOID='+oid+'&RefDate='+Currdate+'&RefTime='+Currtime ;
  NewIt(url);
}

function OpenWork(oid)
{
   var url = 'Work.aspx?RefOID='+oid+'&RefDate='+Currdate+'&RefTime='+Currtime+"'";
   NewIt(url);
    window.location.reload();
}

function OpenTask(oid)
{
   var url ='Task.aspx?RefOID='+oid;
   NewIt(url);
}

function OpenCycleEvent(oid)
{
  var url = 'CycleEvent.aspx?RefOID='+oid+'&RefDate='+Currdate+'&RefTime='+Currtime+"'";
  NewIt(url);
}

function NewIt(url)
{
// var v= window.open(url,'lg', 'dialogHeight: 550px; dialogWidth: 850px; dialogTop: 100px; dialogLeft: 150px; center: yes; help: no');
  var v= window.showModalDialog(url,'lg', 'dialogHeight: 550px; dialogWidth: 850px; dialogTop: 100px; dialogLeft: 150px; center: yes; help: no');
  if (v==1)
    window.location.reload();
}





 


