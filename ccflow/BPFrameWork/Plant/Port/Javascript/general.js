String.prototype.trim=function(){
	return this.replace(/^\s*|\s*$/g,'');
}

var SubmitField = "";

function GetObj(s)
{
	return document.getElementById(s);
}
            
			
function markFocus(dis)
{
SubmitField = dis.id;
}

function pressGif(obj)
{
	obj.src = obj.src.replace("off","press");
}

function releaseGif(obj)
{
	obj.src = obj.src.replace("press","off");
}

function validateForm()
{
	if(SubmitField=="Url")
	{
		if(document.navigateForm.Url.value=="")
		{
	        alert("请输入一个有效网址");
			return false;
		}
	    else
	    {
			if(document.navigateForm.Path)
			{
	        	document.navigateForm.Path.value = "";
			}
			document.navigateForm.submit();
			return true;
	    }
	}
	else if(SubmitField=="Path")
	{
		if(document.navigateForm.Path.value=="")
		{
			alert("请输入一个有效路径");
			return false;
		}
        else if (!document.navigateForm.Path.value.match(/^\\\\[^\\]+(\\[^\\]+)+\\?$/))
		{
			var your_example="";
			if (document.navigateForm.Path.value.match(/^(\\\\[^\\]+\\[^\\]+\\?)/))
			{
				if (RegExp.$1)//ensure browser is compatible
				{
					your_example="\n\n在您的情况下，应为："+RegExp.$1;
				}
			}
			alert("路径应为 \'\\\\\\\\Server\\\\share\\\\\' 格式。"+your_example);
			document.navigateForm.Path.select();
			return false;
		}
		else
		{
			if(document.navigateForm.Url)
			{
	        	document.navigateForm.Url.value = "";
			}
			var targetVal = "/Portal/AccessFileShare?Goto=" + document.navigateForm.Path.value;
			document.navigateForm.Path.value = "";
			openFilePath(targetVal);
			return true;
		}
	}
	else
	{
		return false;
	}
}


var showingTools = false;
var showingMails = false;

function isRightClick(evt)
{
	evt = (evt)	? evt : (window.event) ? window.event : ""; //Either NS style or IE style
	try{if ((2==evt.button)||(6==evt.button)){return true;}}catch(e){};//IE right click
	try{if (3==evt.which){return true;}}catch(e){};//NS right click
	//Else - left click...
	return false;
}

function showDropHeaderMenu(evt, dis)
{
	if (isRightClick(evt)){return;}
	showDrop(dis);
}
function showDrop(dis)
{
	if(dis.childNodes && dis.childNodes[0].id=="Tools")
	{
		showingTools = true;
		if(GetObj("ToolsDiv").style.display == "inline")
		{
			hideDrop(dis.childNodes[0],"ToolsDiv")
		}
		else
		{
			matchDiv(dis.childNodes[0],"ToolsDiv");
			GetObj("ToolsDiv").style.display = "inline";
		}
	}
	else if(dis.childNodes && dis.childNodes[0].id=="Mails")
	{
		showingMails = true;
		if(GetObj("MailsDiv").style.display == "inline")
		{
			hideDrop(dis.childNodes[0],"MailsDiv")
		}
		else
		{
			matchDiv(dis.childNodes[0],"MailsDiv");
			GetObj("MailsDiv").style.display = "inline";
		}
	}
	else
	{
		if((showingTools==false)&&(GetObj("ToolsDiv").style.display == "inline"))
		{
			hideDrop(document.images.Tools,"ToolsDiv");
		}
		if((showingMails==false)&&(GetObj("MailsDiv").style.display == "inline"))
		{
			hideDrop(document.images.Mails,"MailsDiv");
		}
	}
}

function matchDiv(dis,divId)
	{
		var offset = 0;
		var offsetObject = eval(dis);
		while (offsetObject)
		{
			offset += offsetObject.offsetLeft;
			offsetObject = offsetObject.offsetParent;
		}
		GetObj(divId).style.left = parseInt(offset) - 110;
	}
var doResize = true;
function set_matchDiv()
{
var IE = (typeof(document.all) != "undefined");
if(IE)
	{
		doResize = !doResize;
	}
	if(doResize)
	{
		if(GetObj("ToolsDiv").style.display == "inline")
		{
			matchDiv(GetObj("Tools"),"ToolsDiv")
		}
		else if(GetObj("MailsDiv").style.display == "inline")
		{
			matchDiv(GetObj("Mails"),"MailsDiv")
		}
	}
}

function hideDrop(dis,divId)
	{
			GetObj(divId).style.display = "none";
			if(eval(dis).src.indexOf("_over")>-1)
			{
				onTabOut(dis.parentNode);
			}
	}
function onDropOut(dis,divId)
{
	showingTools = false;
	showingMails = false;
	if(GetObj(divId).style.display != "inline")
	{
	hideDrop(dis.childNodes[0],divId);
	}
}

function Tools_On(dis)
	{
		dis.className = dis.className.replace("_Off","_On");
	}

function Tools_Off(dis)
	{
		dis.className = dis.className.replace("_On","_Off");
	}

function switchOn(dis)
	{
		dis.className = "UrlTD_On_hand";
	}
function switchOff(dis)
	{
	dis.className = "UrlTD";
	}

var checkedCounter = 0;
function setRow(dis)
	{
	if(dis.checked)
		{
		dis.parentNode.parentNode.className = "UrlTD_On";
		checkedCounter++
		}
	else
		{
		dis.parentNode.parentNode.className = "UrlTD";
		checkedCounter--
		}
	
	if( checkedCounter > 0 )
		{
		GetObj("deleteBookmark").disabled = false;
		GetObj("deleteBookmark").style.backgroundColor = "#202082";
		}
	else
		{
		GetObj("deleteBookmark").disabled = true;
		GetObj("deleteBookmark").style.backgroundColor = "#ECE9D8";
		}
	}

function changeLocation(newLocation)
{
	top.document.location.href = newLocation;
}

function disable(stat, form)
{
	if(stat)
	{
		form.userName.style.backgroundColor = "#D4D0C8";
		form.password.style.backgroundColor = "#D4D0C8";
	}
	else
	{
		form.userName.style.backgroundColor = "white";
		form.password.style.backgroundColor = "white";
	}
	form.userName.disabled = stat;
	form.password.disabled = stat;
}

function showTitle(dis,titleStr)
{
dis.title = titleStr;
}

function onOver(dis)
{
	var objct = eval(GetObj(dis.id+"Icon"));
	if(dis.className == "UrlLinks")
		{
		dis.className = "UrlLinksOn";
		objct.src = objct.src.replace("_off","_over");
		}
	else
		{
		dis.className = "UrlLinks";
		objct.src = objct.src.replace("_over","_off");
		}
}

function onPress(dis)
{
if(dis.src.indexOf("_over")==-1)
	{
	dis.src = dis.src.replace(".gif","_over.gif");
	}
}
function onOut(dis)
{
dis.src = dis.src.replace("_over.gif",".gif");
showTitle(dis,'');
}

function onTabOver(dis)
{
	if(dis.childNodes[0].src.indexOf("_over")==-1)
	{
		dis.childNodes[0].src = dis.childNodes[0].src.replace(".gif","_over.gif");
		dis.className = "NavigateTab_on";
	}
}

function onTabOut(dis)
{
	dis.childNodes[0].src = dis.childNodes[0].src.replace("_over.gif",".gif");
	dis.className = "NavigateTab";
	showTitle(dis,'');
}

function onButtIn()
{
	if(GetObj("buttNormal").style.display == "inline")
	{
		GetObj("buttPressed").style.display = "inline";
		GetObj("buttNormal").style.display = "none";
	}
}

function onButtOut()
{
	if(GetObj("buttNormal").style.display == "none")
	{
		GetObj("buttNormal").style.display = "inline";
		GetObj("buttPressed").style.display = "none";
	}
}

function onRelease(dis,target, evt)
{
	try{if (isRightClick(evt)){return;}}catch(e){}
	top.location.href = target;
}
function HelpWinSNX()
{
	var help_win = window.open( "/Portal/UsersHelp_en_US/wwhelp.htm?context=portal_guide&topic=CS_WHAT_IS_THIS_1_1_2&single=true", "SNX_HelpWin", "toolbar=0,status=0,width=800,height=600,top=10,left=10" );
	help_win.focus();
}

function openFilePath(target)
{
		top.location.href = target;
}


/*
   name - name of the cookie
   value - value of the cookie
   [expires] - expiration date of the cookie
     (defaults to end of current session)
   [path] - path for which the cookie is valid
     (defaults to path of calling document)
   [domain] - domain for which the cookie is valid
     (defaults to domain of calling document)
   [secure] - Boolean value indicating if the cookie transmission requires
     a secure transmission
   * an argument defaults when it is assigned null as a placeholder
   * a null placeholder is not required for trailing omitted arguments
*/

function setCookie(name, value, expires, path, domain, secure) {
  var curCookie = name + "=" + escape(value) +
      ((expires) ? "; expires=" + expires.toGMTString() : "") +
      ((path) ? "; path=" + path : "") +
      ((domain) ? "; domain=" + domain : "") +
      ((secure) ? "; secure" : "");
  document.cookie = curCookie;
}


/*
  name - name of the desired cookie
  return string containing value of specified cookie or null
  if cookie does not exist
*/

function getCookie(name) {
  var dc = document.cookie;
  var prefix = name + "=";
  var begin = dc.indexOf("; " + prefix);
  if (begin == -1) {
    begin = dc.indexOf(prefix);
    if (begin != 0) return null;
  } else
    begin += 2;
  var end = document.cookie.indexOf(";", begin);
  if (end == -1)
    end = dc.length;
  return unescape(dc.substring(begin + prefix.length, end));
}


/*
   name - name of the cookie
   [path] - path of the cookie (must be same as path used to create cookie)
   [domain] - domain of the cookie (must be same as domain used to
     create cookie)
   path and domain default if assigned null or omitted if no explicit
     argument proceeds
*/

function deleteCookie(name, path, domain) {
  if (getCookie(name)) {
    document.cookie = name + "=" +
    ((path) ? "; path=" + path : "") +
    ((domain) ? "; domain=" + domain : "") +
    "; expires=Thu, 01-Jan-70 00:00:01 GMT";
  }
}

function deleteSelectCookie(cName)
{
	document.cookie = cName + "=" + escape("no") + "; expires=Fri, 31 Dec 1981 23:59:59 GMT;";
}

// date - any instance of the Date object
// * hand all instances of the Date object to this function for "repairs"

function fixDate(date) {
  var base = new Date(0);
  var skew = base.getTime();
  if (skew > 0)
    date.setTime(date.getTime() - skew);
}

function checkForm(fieldsArr)
{
	var obj;
	for( var i=0 ; i<arguments.length ; i++ )
	{
		obj = GetObj(arguments[i]);
//		if(obj && obj.value.trim()=="")
		if(obj && obj.value=="")
		{
			if (obj.displayError)
			{
				alert(obj.displayError);
			}
			else
			{
				alert("此字段不能为空。");
			}
			obj.focus();
			return false;
		}
	} 
	return true;
}

// Function from http://www.somacon.com/p143.php
// return the value of the radio button that is checked
// return an empty string if none are checked, or
// there are no radio buttons
// Usage: getCheckedValue(document.forms['formName'].elements['radioName'])
function getCheckedValue(radioObj) {
	if(!radioObj)
	{
		return "";
	}

	var radioLength = radioObj.length;
	if(radioLength == undefined)
	{
		if(radioObj.checked)
		{
			return radioObj.value;
		}
		else
		{
			return "";
		}
	}

	for(var i = 0; i < radioLength; i++) 
	{
		if(radioObj[i].checked) 
		{
			return radioObj[i].value;
		}
	}

	return "";
}

// Function from http://www.somacon.com/p143.php
// set the radio button with the given value as being checked
// do nothing if there are no radio buttons
// if the given value does not exist, all the radio buttons
// are reset to unchecked
function setCheckedValue(radioObj, newValue) {
	if(!radioObj)
	{
		return;
	}

	var radioLength = radioObj.length;
	if(radioLength == undefined) 
	{
		radioObj.checked = (radioObj.value == newValue.toString());
		return;
	}

	for(var i = 0; i < radioLength; i++) 
	{
		radioObj[i].checked = false;
		if(radioObj[i].value == newValue.toString()) 
		{
			radioObj[i].checked = true;
		}
	}
}
