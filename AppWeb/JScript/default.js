//加入收藏夹脚本
function addfavorite(sURL, sTitle) {
    switch (getOs()) {
        case 0: alert("未知的浏览器，加入收藏失败，请使用Ctrl+D进行添加"); break;
        case 1: window.external.addFavorite(sURL, sTitle); break;
        case 2:
            {
                try {
                    window.sidebar.addPanel(sTitle, sURL, "");
                } catch (e) {
                    alert("加入收藏失败，请使用Ctrl+D进行添加");
                }
            }
    }
}

//获取浏览器的版本
function getOs() {
    if (navigator.userAgent.indexOf("MSIE") > 0) return 1;
    if (isFirefox = navigator.userAgent.indexOf("Firefox") > 0) return 2;
    if (isSafari = navigator.userAgent.indexOf("Safari") > 0) return 3;
    if (isCamino = navigator.userAgent.indexOf("Camino") > 0) return 4;
    if (isMozilla = navigator.userAgent.indexOf("Gecko/") > 0) return 5;
    return 0;
}

//设为首页（兼容通用浏览器）
function setHomePage(obj, url) {
    try {//ie
        obj.style.behavior = "url(#default#homepage)";
        obj.setHomePage(url);
    } catch (e) {//ff
        if (window.netscape) {
            try {
                netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
            } catch (e) {
                alert("此操作被Firefox浏览器拒绝！请手动设置。方法：请在浏览器地址栏输入“about:config”并回车然后将[signed.applets.codebase_principal_support]设置为'true'");
            }
            var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components.interfaces.nsIPrefBranch);
            prefs.setCharPref('browser.startup.homepage', url);
        }
    }
}

/*输入实时验证*/
// 只允许输入数字和回车键 调用：onkeypress="return isDigit(event.keyCode|event.which);"
function IsDigit(code) {
    return ((code >= 48 && code <= 57) || code == 13 || code == 8) ? true : false;
}

//身份证号码输入验证：只允许数字、x和X(del和backspace的值分别为8和46)
function IsCardNo(code) {
    return ((code >= 48 && code <= 57) || (code == 13) || (code == 88) || (code == 120)) ? true : false;
}

//电话号码输入验证：只允许数字、()和-
function IsTel(code) {
    return ((code >= 48 && code <= 57) || (code == 13) || (code == 40) || (code == 41) || (code == 45) || code == 8 || code == 46) ? true : false;
}

// 只允许输入数字和“.”  浮点数输入；
function IsFloat(code) {
    return ((code >= 48 && code <= 57) || code == 13 || code == 8 || (code == 46)) ? true : false;
}

// 只允许输入数字和“,”；
function IsCode(code) {
    return ((code >= 48 && code <= 57) || code == 13 || code == 8 || (code == 44)) ? true : false;
}

function MenuOnMouseOver(obj) {
    obj.className = 'menubar_button';
}

function MenuOnMouseOut(obj) {
    obj.className = 'menubar_button_on';
}
