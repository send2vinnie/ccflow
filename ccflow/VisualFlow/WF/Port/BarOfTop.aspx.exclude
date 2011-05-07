<%@ Reference Control="~/comm/uc/ucsys.ascx" %>
<%@ Page language="c#" Inherits="BP.Web.CT.GTS.Port.Bar" CodeFile="BarOfTop.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="UCSys" Src="../../Comm/UC/UCSys.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>Bar</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<meta http-equiv="refresh" content="10000">
		<base target=_blank >
		<style type="text/css"> 
	A:link { TEXT-DECORATION: none; color: #000000;}
	A:visited { TEXT-DECORATION: none; color: #000000;}
	A:hover { color: red}
		</style>

		<!--以下为最大化、最小化功能所用！-->
		<STYLE> 
		.navPoint { FONT-SIZE: 7pt; CURSOR: hand; FONT-FAMILY: Webdings } P { FONT-SIZE: 7pt } 		
		</STYLE>
		<script language=javascript>
		  function TDClick( webAppPath, file )
          {
              var url=webAppPath+"/Comm/port/LeftXml.aspx?xml="+file;
              var myParent, i ; 
            myParent = window.parent;
            if ( myParent == null ) 
               return;
            if ( myParent.frames == null )
               return;
            // alert(url);
            for ( i = 0; i< myParent.frames.length; i++ )
            {   
              
                  if (myParent.frames(i).name=='left')         
                  {
                     
                        window.parent.frames(i).location.href=url;
                  }
            }   
          }
          function WinOpen(url, winName)
          {
            newWindow=window.open( url , winName ,'width=600,top=200,left=150,height=400,scrollbars=yes,resizable=yes,toolbar=false,location=false');
            newWindow.focus();  
          }

//true:当前处于扩展状态，灰色；false:当前处于缩小状态，蓝色
//当窗体加载时，应为缩小状态
var  g_Extended=true;
//记录Frame的大小。
//var Top_Rows='75,*';
var Top_Rows='71,*';
//var Bottom_Cols='198,*';
var Bottom_Cols='192,*';

function shrinkForm()
{        
	window.parent.Top.rows=Top_Rows;
	window.parent.Bottom.cols=Bottom_Cols;
	switchUpPoint.innerText=5
	switchLeftPoint.innerText=3
}

function ExtendForm()
{
	
	//此段代码无法实现预定的功能。当窗口的帧的大小改变时，
	//帧的rows和cols属性不会马上改变。
	if(window.parent.Top.rows!='0,*') Top_Rows=window.parent.Top.rows;
	if(window.parent.Bottom.cols!='0,*') Bottom_Cols=window.parent.Bottom.cols;
	switchUpPoint.innerText=6
	switchLeftPoint.innerText=4

	window.parent.Top.rows='0,*';
	window.parent.Bottom.cols='0,*';

}

function switchUpBar(){
	if (switchUpPoint.innerText==5){
		switchUpPoint.innerText=6
		Top_Rows=window.parent.Top.rows;
		window.parent.Top.rows='0,*';
	}
	else{
		window.parent.Top.rows=Top_Rows;
		switchUpPoint.innerText=5
	}
	if(window.parent.Bottom.cols=='0,*' && window.parent.Top.rows=='0,*') 
		{
		document.all("Tip").innerText="标准化";
		document.all("imgsrc").src="max.gif";
		}
	else
		{
		document.all("Tip").innerText="最大化";
		document.all("imgsrc").src="mix.gif";
	    }
}

function switchLeftBar(){
	if (switchLeftPoint.innerText==3){
		switchLeftPoint.innerText=4
		Bottom_Cols=window.parent.Bottom.cols;

		window.parent.Bottom.cols='0,*';
	}
	else{
		window.parent.Bottom.cols=Bottom_Cols;
		switchLeftPoint.innerText=3
	}

	if(window.parent.Bottom.cols=='0,*' && window.parent.Top.rows=='0,*') 
		{
		document.all("Tip").innerText="标准化"
		document.all("imgsrc").src="max.gif";
		}
	else
		{
		document.all("Tip").innerText="最大化"
		document.all("imgsrc").src="mix.gif";
		}

}
function ShiftStatus()
{
//当窗体加载时，应为缩小状态
//当前状态为扩展状态，要切换到缩小状态。
  if (g_Extended==true)
  {
	document.all("Tip").innerText="最大化"
	document.all("imgsrc").src="mix.gif";
	shrinkForm()
   }
   else
   {
	document.all("Tip").innerText="标准化"
	document.all("imgsrc").src="max.gif";
	ExtendForm();
   }
   
   g_Extended=!g_Extended;
}

</script>
<!--以上为最大化、最小化功能所用！-->

</HEAD>
	<body  topmargin="0" leftmargin="0"  onload="ShiftStatus();" ondblclick="ShiftStatus();"  background="Title.gif">
	<form id="Form1" method="post" runat="server">
	
     <table border='0' width='100%' height='63'cellspacing='0' cellpadding='0'>
      <tr>
       <td width='400'></td>
       <td>
       
       <table border='0' width='100%' cellspacing='0' cellpadding='0'>
        <tr>
          <td width='100%'>&nbsp;&nbsp;&nbsp;</td>
        </tr>
        <tr>
          <td width="100%" align="left"><uc1:UCSys id=UCSys1 runat="server"></uc1:UCSys></td>
        </tr>

      </table>

       </td>
       <td></td>
       </tr>
     </table>

     
    </form>
	</body>
</HTML>