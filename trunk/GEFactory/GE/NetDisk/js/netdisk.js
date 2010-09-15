function getDeleteNodeID() {
    if (document.getElementById("NetDiskDir1_hidNodeID").value == "") {
        alert("请选择待删除的目录！");
        return false;
    }
    else {
        if (confirm('你确定要删除此目录吗?')) {
            return true;
        }
        else {
            return false;
        }
    }
};

function getMoveNodeID() {
    if (document.getElementById("NetDiskDir1_hidNodeID").value == "") {
        alert("请选择待移动的目录！");
        return false;
    }
    else {

        return true;
    }
};

function SetHidValue(nodeID, _Url, dirOID) {
    document.getElementById("NetDiskDir1_hidNodeID").value = nodeID;
    document.getElementById("NetDiskDir1_hidDirOID").value = dirOID;
    document.frames('fm').location = _Url;
};

function ShowDivName() {
    var mainDiv = document.getElementById("divLeft");
    bgDiv = document.createElement("div"); //创建一个div对象（背景层）    

    //定义div属性，即相当于    
    bgDiv.setAttribute("id", "bgDiv");
    //alert("***********")   
    bgDiv.style.position = "absolute";
    bgDiv.style.top = mainDiv.offsetTop;
    bgDiv.style.background = "#777";
    bgDiv.style.filter = "progid:DXImageTransform.Microsoft.Alpha(style=3,opacity=25,finishOpacity=0";
    bgDiv.style.opacity = "0.3";
    bgDiv.style.left = mainDiv.offsetLeft;

    bgDiv.style.width = document.body.clientWidth;

    bgDiv.style.height = document.body.clientHeight
    bgDiv.style.zIndex = "1";
    mainDiv.appendChild(bgDiv); //在body内添加该div对象

    var divDirName = document.getElementById("divDirName");

    var eve = getEvent(); 
//    var MouseLocation = CaptureMouseLocation(ev);
//    var MouseX = MouseLocation.x + "px";
    //    var MouseY = MouseLocation.y + "px";

//    var MouseX = document.body.scrollLeft + eve.clientX - document.body.clientLeft;
//    var MouseY = document.body.scrollTop + eve.clientY - document.body.clientTop;
//    
    //mouseX = eve.clientX; 
    //mouseY = eve.clientY;

//    divDirName.style.top = "200px";
//    divDirName.style.left = "300px";
//    divDirName.style.top = MouseX;
//    divDirName.style.left = MouseY;
    divDirName.style.display = "block"
    return false;
}

function getEvent() {
    if (document.all) return window.event;
    var func = getEvent.caller;
    while (func != null) {
        var arg0 = func.arguments[0];
        if (arg0) {
            if ((arg0.constructor == Event || arg0.constructor == MouseEvent) || (typeof (arg0) == "object" && arg0.preventDefault && arg0.stopPropagation)) {
                return arg0;
            }
        }
        func = func.caller;
    }
}

function hidDiv(obj) {
    obj.style.display = "none";
    return false;
};
function DoSaveName() {
    hidDiv(divDirName);
    hidDiv(bgDiv);
    var gradeNo = document.getElementById("NetDiskDir1_hidNodeID").value;
    var dirOid = document.getElementById("NetDiskDir1_hidDirOID").value;

    var url = 'SaveName.aspx?FK_GradeNo=' + gradeNo + "&DoType=SaveName"; ;
    var newWindow = window.showModalDialog(url, '编辑目录', 'scroll:1;status:1;help:1;resizable:1;dialogWidth:300px;dialogHeight:200px');
    window.location.href = 'NetDiskDir.aspx?FK_Dir=' + dirOid;

};

function DoSaveChildName() {
    hidDiv(divDirName);
    hidDiv(bgDiv);
    var gradeNo = document.getElementById("NetDiskDir1_hidNodeID").value;
    var dirOid = document.getElementById("NetDiskDir1_hidDirOID").value;

    if (gradeNo == "") {
        alert("请选择目录项！");
    }
    else {
        var url = 'SaveName.aspx?FK_GradeNo=' + gradeNo + "&DoType=SaveChildName";
        var newWindow = window.showModalDialog(url, '编辑目录', 'scroll:1;status:1;help:1;resizable:1;dialogWidth:300px;dialogHeight:200px');
        window.location.href = 'NetDiskDir.aspx?FK_Dir=' + dirOid;
    }
};

function DoEditName() {
    var gradeNo = document.getElementById("NetDiskDir1_hidNodeID").value;
    var dirOid = document.getElementById("NetDiskDir1_hidDirOID").value;

    if (gradeNo == "") {
        alert("请选择目录项！");
    }
    else {

        var randomVal = GetRandomNum();
        var url = 'SaveName.aspx?FK_GradeNo=' + gradeNo + "&DoType=EditName&randomVal=" + randomVal;
        var newWindow = window.showModalDialog(url, '编辑目录', 'scroll:1;status:1;help:1;resizable:1;dialogWidth:300px;dialogHeight:200px');
        window.location.href = 'NetDiskDir.aspx?FK_Dir=' + dirOid;
    }
    return false;
};

function GetRandomNum() {
    var Range =100;
    var Rand = Math.random();
    return (1 + Math.round(Rand * Range));
} 