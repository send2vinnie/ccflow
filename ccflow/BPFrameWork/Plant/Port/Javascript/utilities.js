function pressGif(dis)
{
	dis.src = dis.src.replace("off","press");
}

function releaseGif(dis)
{
	dis.src = dis.src.replace("press","off");
}

			
function GetObj(s)
{
	return document.getElementById(s);
}
            
var objCapsPosRef;
var sCapsLockHeadline = "大写锁定已打开";
var sCapsLockMessage = "打开大写锁定可能会造成密码输入错误。";
            
function setCapsLockDiv(sRef, sHeadline, sMessage) 
{
	if (!document.createElement) { return; }
	objCapsPosRef = GetObj(sRef);  
	if (!objCapsPosRef) {return;}
	try{
		var elemDiv = document.createElement('div');
		if (typeof(elemDiv.innerHTML) != 'string') { return; }
		elemDiv.id = 'CapsLockDiv';
		elemDiv.style.position = 'absolute';
		elemDiv.style.display = 'none';
		elemDiv.style.border = '#000000 1px solid';
		elemDiv.style.background = '#C4CED0';
		elemDiv.innerHTML = '<table cellpadding=1 cellspacing=1 width=210 style="font-size: 11px;" align=left><tr><td width=10><img src="/unauth/images/exclamation.jpg"></td><td align=left valign=center><b>' + sHeadline + '</b></td></tr><tr><td colspan=2>' + sMessage + '</td></tr></table>';
		// Set absolute position
		document.body.appendChild(elemDiv);
		PositionCapsMessage();
		window.onresize = PositionCapsMessage;
	}
	catch(e){}
}
             
function PositionCapsMessage()
{
	var nRefWidth;
	var elemDiv = GetObj("CapsLockDiv");
	var top = objCapsPosRef.offsetTop;
	var left = objCapsPosRef.offsetLeft;
	var parent = objCapsPosRef.offsetParent;
	while (parent != document.body) {
		top += parent.offsetTop;
		left += parent.offsetLeft;
		parent = parent.offsetParent;
	}
	elemDiv.style.top = top;
	nRefWidth = parseInt(objCapsPosRef.style.width); // Assumes the reference object has a width attribute set
	if (isNaN(nRefWidth)) nRefWidth = 100; // A safe default in case its width isn't set or is irrelevant
	elemDiv.style.left = left + nRefWidth + 13;
}
            
function capsDetect( e ) 
{
	//if the browser did not pass event information to the handler, check in window.event
	if( !e ) { e = window.event; } if( !e ) { return; }
	//what (case sensitive in good browsers) key was pressed
	//this uses all three techniques for checking, just in case
	var theKey = 0;
	if( e.which ) { theKey = e.which; } //Netscape 4+, etc.
	else if( e.keyCode ) { theKey = e.keyCode; } //Internet Explorer, etc.
	else if( e.charCode ) { theKey = e.charCode } //Gecko - probably not needed
	//was the shift key was pressed
	var theShift = false;
	if( e.shiftKey ) { theShift = e.shiftKey; } //Internet Explorer, etc.
	else if( e.modifiers ) { //Netscape 4
	//check the third bit of the modifiers value (says if SHIFT is pressed)
		if( e.modifiers & 4 ) { //bitwise AND
			theShift = true;
		}
	}
 
	//if upper case, check if shift is not pressed
	if( theKey > 64 && theKey < 91 && !theShift ) {
		HideCapsDiv(false);
	}
	//if lower case, check if shift is pressed
	else if( theKey > 96 && theKey < 123 && theShift ) {
		HideCapsDiv(false);
	}
	else{
		//Hide caps note ;
		HideCapsDiv(true);
	}
}
            
function HideCapsDiv(bHide)
{
	var CapsDiv = GetObj("CapsLockDiv");
	if (CapsDiv)
	{
		CapsDiv.style.display = bHide ? 'none' : 'block';
	}
}

function setCookie(name, value, expires, path, domain, secure) {
  	var curCookie = name + "=" + escape(value) +
		((expires) ? "; expires=" + expires.toGMTString() : "") +
		((path) ? "; path=" + path : "") +
		((domain) ? "; domain=" + domain : "") +
		((secure) ? "; secure" : "");
	document.cookie = curCookie;
}
