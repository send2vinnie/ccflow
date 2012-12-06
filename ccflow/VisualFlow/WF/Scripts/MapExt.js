﻿// ********************** 根据关键字动态查询. ******************************** //
var oldValue = "";
var oid ;
var highlightindex = -1;
function DoAnscToFillDiv(sender, e, tbid, fk_mapExt) {

    openDiv(sender, tbid);
    var myEvent = event || window.event;
    var myKeyCode = myEvent.keyCode;
    // 获得ID为divinfo里面的DIV对象 .  
    var autoNodes = $("#divinfo").children("div");
    if (myKeyCode == 38) {
        if (highlightindex != -1) {
            autoNodes.eq(highlightindex).css("background-color", "Silver");
            autoNodes.eq(highlightindex).css("color", "Black");
            if (highlightindex == 0) {
                highlightindex = autoNodes.length - 1;
            }
            else {
                highlightindex--;
            }
        }
        else {
            highlightindex = autoNodes.length - 1;
        }
        autoNodes.eq(highlightindex).css("background-color", "blue");
        autoNodes.eq(highlightindex).css("color", "white");
    }
    else if (myKeyCode == 40) {
        if (highlightindex != -1) {
            autoNodes.eq(highlightindex).css("background-color", "Silver");
            autoNodes.eq(highlightindex).css("color", "black");
            highlightindex++;
        }
        else {
            highlightindex++;
        }

        if (highlightindex == autoNodes.length) {
            autoNodes.eq(autoNodes.length).css("background-color", "Silver");
            autoNodes.eq(autoNodes.length).css("color", "black");
            highlightindex = 0;
        }
        autoNodes.eq(highlightindex).css("background-color", "blue");
        autoNodes.eq(highlightindex).css("color", "white");
    }
    else if (myKeyCode == 13) {
        if (highlightindex != -1) {

            //获得选中的那个的文本值
            var textInputText = autoNodes.eq(highlightindex).text();
            var strs = textInputText.split('|');
            autoNodes.eq(highlightindex).css("background-color", "Silver");
            $("#" + tbid).val(strs[0]);
            $("#divinfo").hide();
            oldValue = strs[0];

            // 填充.
            FullIt(oldValue, tbid, fk_mapExt);
            highlightindex = -1;
        }
    }
    else {
        if (e != oldValue) {
            $("#divinfo").empty();
            var json_data = { "Key": e, "FK_MapExt": fk_mapExt, "KVs": kvs };
            $.ajax({
                type: "get",
                url: "HanderMapExt.ashx",
                data: json_data,
                beforeSend: function (XMLHttpRequest, fk_mapExt) {
                    //ShowLoading();
                },
                success: function (data, textStatus) {
                    /* 如何解决与文本框的宽度与下拉框的一样宽。*/
                    //alert($("#" + tbid));
                    if (data != "") {
                        highlightindex = -1;
                        dataObj = eval("(" + data + ")"); // 转换为json对象 
                        $.each(dataObj.Head, function (idx, item) {
                            $("#divinfo").append("<div style='" + itemStyle + "' name='" + idx + "' onmouseover='MyOver(this)' onmouseout='MyOut(this)' onclick=\"ItemClick('" + sender.id + "','" + item.No + "','" + tbid + "','" + fk_mapExt + "');\" value='" + item.No + "'>" + item.No + '|' + item.Name + "</div>");
                        });
                    }
                },
                complete: function (XMLHttpRequest, textStatus) {
                    //    alert('HideLoading');
                    //HideLoading();
                },
                error: function () {
                    alert('error when load data.');
                    //请求出错处理
                }
            });
            oldValue = e;
        }
    }
}

function FullIt(oldValue, tbid, fk_mapExt) {

    if (oid == null)
        oid = GetQueryString('OID');

    if (oid == null)
        oid = GetQueryString('WorkID');

    if (oid == null)
        oid = 0;

    //执行填充主表的控件.
    FullCtrl(oldValue, tbid, fk_mapExt);

    //执行个性化填充下拉框.
    FullCtrlDDL(oldValue, tbid, fk_mapExt);

    //执行填充从表.
    FullDtl(oldValue, fk_mapExt);

    //执行m2m 关系填充.
    FullM2M(oldValue, fk_mapExt);
}
//打开div.
function openDiv_bak(e, tbID) {

    //alert(document.getElementById("divinfo").style.display);
    if (document.getElementById("divinfo").style.display == "none") {
        var txtObject = document.getElementById(tbID);
        var orgObject = document.getElementById("divinfo");

        var rect = getoffset(txtObject);
        orgObject.style.top = rect[0] + 22;
        orgObject.style.left = rect[1];

        //        orgObject.style.top =  $("#" + tbID).attr("top") + 22;
        //        orgObject.style.left = $("#" + tbID).attr("left");

        orgObject.style.display = "block";
        txtObject.focus();
    }
}
function openDiv(e, tbID) {

    //alert(document.getElementById("divinfo").style.display);
    if (document.getElementById("divinfo").style.display == "none") {

        var txtObject = document.getElementById(tbID);
        var orgObject = document.getElementById("divinfo");
        var rect = getoffset(txtObject);
        var t = rect[0] + 22;
        var l = rect[1];

        orgObject.style.top = t + 'px';
        orgObject.style.left = l + 'px';
        orgObject.style.display = "block";
        txtObject.focus();
    }
}
function getoffset(e) {
    var t = e.offsetTop;
    var l = e.offsetLeft;
    while (e = e.offsetParent) {
        t += e.offsetTop;
        l += e.offsetLeft;
    }
    var rec = new Array(1);
    rec[0] = t;
    rec[1] = l;
    return rec
}

/* 内置的Pop自动返回值. */
function ReturnValCCFormPopVal(ctrl, fk_mapExt, refEnPK) {
    url = 'FrmPopVal.aspx?FK_MapExt=' + fk_mapExt + '&RefPK=' + refEnPK + '&CtrlVal=' + ctrl.value;
    var v = window.showModalDialog(url, 'opp', 'dialogHeight: 550px; dialogWidth: 650px; dialogTop: 100px; dialogLeft: 150px; center: yes; help: no');
    if (v == null || v == '' || v == 'NaN') {
        return;
    }
    ctrl.value = v;
    return;
}


/*  ReturnValTBFullCtrl */
function ReturnValTBFullCtrl(ctrl, fk_mapExt) {
    var url = 'FrmReturnValTBFullCtrl.aspx?CtrlVal=' + ctrl.value + '&FK_MapExt=' + fk_mapExt;
    var v = window.showModalDialog(url, 'wd', 'dialogHeight: 550px; dialogWidth: 650px; dialogTop: 100px; dialogLeft: 150px; center: yes; help: no');
    if (v == null || v == '' || v == 'NaN') {
        return;
    }
    ctrl.value = v;
    // 填充.
    FullIt(oldValue, ctrl.id, fk_mapExt);
    return;
}

var kvs = null;
function GenerPageKVs() {
    var ddls = null;
    ddls = parent.document.getElementsByTagName("select");
    kvs = "";
    for (var i = 0; i < ddls.length; i++) {
        var id = ddls[i].name;

        if (id.indexOf('DDL_') == -1) {
            continue;
        }
        var myid = id.substring(id.indexOf('DDL_') + 4);
        kvs += '~' + myid + '=' + ddls[i].value;
    }

    ddls = document.getElementsByTagName("select");
    for (var i = 0; i < ddls.length; i++) {
        var id = ddls[i].name;

        if (id.indexOf('DDL_') == -1) {
            continue;
        }
        var myid = id.substring(id.indexOf('DDL_') + 4);
        kvs += '~' + myid + '=' + ddls[i].value;
    }
    return kvs;
}


//var kvs = null;
//function GenerPageKVs() {
//    var ddls = document.getElementsByTagName("select");
//    kvs = "";
//    for (var i = 0; i < ddls.length; i++) {
//        var id = ddls[i].name;
//        var myid = id.substring(id.indexOf('DDL_') + 4);
//        kvs += '~' + myid + '=' + ddls[i].value;
//        //  if (ddls[i].type == "text" || ddls[i].type == "checkbox") {
//        //}
//    }
//}
/* 自动填充 */
function DDLFullCtrl(e, ddlChild, fk_mapExt) {
    GenerPageKVs();

    var json_data = { "Key": e, "FK_MapExt": fk_mapExt, "KVs": kvs };
    $.ajax({
        type: "get",
        url: "HanderMapExt.ashx?KVs=" + kvs,
        data: json_data,
        beforeSend: function (XMLHttpRequest) {
            //ShowLoading();
        },
        success: function (data, textStatus) {
            var dataObj = eval("(" + data + ")"); //转换为json对象 
            for (var i in dataObj.Head) {
                if (typeof (i) == "function")
                    continue;
                FullIt(e, ddlChild, fk_mapExt);
                return;
            }
        },
        complete: function (XMLHttpRequest, textStatus) {
            //HideLoading();
        },
        error: function () {
            //请求出错处理
        }
    });
}
/* 级联下拉框*/
function DDLAnsc(e, ddlChild, fk_mapExt) {
    GenerPageKVs();
    var json_data = { "Key": e, "FK_MapExt": fk_mapExt, "KVs": kvs };
    $.ajax({
        type: "get",
        url: "HanderMapExt.ashx",
        data: json_data,
        beforeSend: function (XMLHttpRequest) {
            //ShowLoading();
        },
        success: function (data, textStatus) {

            // 这里要设置一下获取的外部数据.
            // var seleValOfOld = $("#" + ddlChild).selectedindex;
            // alert(seleValOfOld);
            // 获取原来选择值.

            var oldVal = null;
            var ddl = document.getElementById(ddlChild);
            var mylen = ddl.options.length - 1;
            while (mylen >= 0) {
                if (ddl.options[mylen].selected) {
                    oldVal = ddl.options[mylen].value;
                }
                mylen--;
            }

            //alert(oldVal);

            $("#" + ddlChild).empty();
            var dataObj = eval("(" + data + ")"); //转换为json对象.

            $.each(dataObj.Head, function (idx, item) {
                $("#" + ddlChild).append("<option value='" + item.No + "'>" + item.Name + "</option");
            });

            var isInIt = false;
            mylen = ddl.options.length - 1;
            while (mylen >= 0) {
                if (ddl.options[mylen].value == oldVal) {
                    ddl.options[mylen].selected = true;
                    isInIt = true;
                    break;
                }
                mylen--;
            }
            if (isInIt == false) {

                $("#" + ddlChild).append("<option value='" + oldVal + "' selected='selected' >*请选择</option");
            }
            //  alert(oldVal);
        },
        complete: function (XMLHttpRequest, textStatus) {
            //HideLoading();
        },
        error: function () {
            //请求出错处理
        }
    });
}


function FullM2M(key, fk_mapExt) {
    //alert(key);
    GenerPageKVs();
    var json_data = { "Key": key, "FK_MapExt": fk_mapExt, "DoType": "ReqM2MFullList", "OID": oid, "KVs": kvs };
    $.ajax({
        type: "get",
        url: "HanderMapExt.ashx",
        data: json_data,
        beforeSend: function (XMLHttpRequest) {
            //ShowLoading();
        },
        success: function (data, textStatus) {
            if (data == "")
                return;

            var dataObj = eval("(" + data + ")"); //转换为json对象.
            for (var i in dataObj.Head) {
                if (typeof (i) == "function")
                    continue;

                for (var k in dataObj.Head[i]) {
                    var fullM2M = dataObj.Head[i][k];
                    var frm = document.getElementById('F' + fullM2M);
                    if (frm == null)
                        continue;

                    var src = frm.src;
                    var idx = src.indexOf("&Key");
                    if (idx == -1)
                        src = src + '&Key=' + key + '&FK_MapExt=' + fk_mapExt;
                    else
                        src = src.substring(0, idx) + '&Key=' + key + '&FK_MapExt=' + fk_mapExt;
                    frm.src = src;
                }
            }
        },
        complete: function (XMLHttpRequest, textStatus) {
            //HideLoading();
        },
        error: function () {
            //请求出错处理
        }
    });
}

//填充明细.
function FullDtl(key, fk_mapExt) {

    GenerPageKVs();
    //FullM2M(key, fk_mapExt); //填充M2M.
    var json_data = { "Key": key, "FK_MapExt": fk_mapExt, "DoType": "ReqDtlFullList", "OID": oid, "KVs": kvs };
    $.ajax({
        type: "get",
        url: "HanderMapExt.ashx",
        data: json_data,
        beforeSend: function (XMLHttpRequest) {
            //ShowLoading();
        },
        success: function (data, textStatus) {
            if (data == "")
                return;

            var dataObj = eval("(" + data + ")"); //转换为json对象.
            for (var i in dataObj.Head) {
                if (typeof (i) == "function")
                    continue;

                for (var k in dataObj.Head[i]) {
                    var fullDtl = dataObj.Head[i][k];
                    //  alert('您确定要填充从表吗?，里面的数据将要被删除。' + key + ' ID= ' + fullDtl);
                    var frm = document.getElementById('F' + fullDtl);
                    var src = frm.src;
                    var idx = src.indexOf("&Key");
                    if (idx == -1)
                        src = src + '&Key=' + key + '&FK_MapExt=' + fk_mapExt;
                    else
                        src = src.substring(0, idx) + '&Key=' + key + '&FK_MapExt=' + fk_mapExt;
                    frm.src = src;
                }
            }
        },
        complete: function (XMLHttpRequest, textStatus) {
            //HideLoading();
        },
        error: function () {
            //请求出错处理
        }
    });
}

function FullCtrlDDL(key, ctrlIdBefore, fk_mapExt) {
    GenerPageKVs();
    var json_data = { "Key": key, "FK_MapExt": fk_mapExt, "DoType": "ReqDDLFullList", "KVs": kvs };
    $.ajax({
        type: "get",
        url: "HanderMapExt.ashx",
        data: json_data,
        beforeSend: function (XMLHttpRequest) {
            //ShowLoading();
        },
        success: function (data, textStatus) {
            if (data == "")
                return;

            var dataObj = eval("(" + data + ")"); //转换为json对象 
            var beforeID = ctrlIdBefore.substring(0, ctrlIdBefore.indexOf('DDL_'));
            var endId = ctrlIdBefore.substring(ctrlIdBefore.lastIndexOf('_'));

            for (var i in dataObj.Head) {
                if (typeof (i) == "function")
                    continue;

                for (var k in dataObj.Head[i]) {
                    var fullDDLID = dataObj.Head[i][k];

                    //alert(fullDDLID);
                    FullCtrlDDLDB(key, fullDDLID, beforeID, endId, fk_mapExt);
                }
            }
        },
        complete: function (XMLHttpRequest, textStatus) {
            //HideLoading();
        },
        error: function () {
            //请求出错处理
        }
    });
}
function FullCtrlDDLDB(e, ddlID, ctrlIdBefore, endID, fk_mapExt) {
    GenerPageKVs();
   // alert('FullCtrlDDLDBs:' + ddlID + ' ctrlIdBefore: ' + ctrlIdBefore);

    var json_data = { "Key": e, "FK_MapExt": fk_mapExt, "DoType": "ReqDDLFullListDB", "MyDDL": ddlID, "KVs": kvs };
    $.ajax({
        type: "get",
        url: "HanderMapExt.ashx",
        data: json_data,
        beforeSend: function (XMLHttpRequest) {
            //ShowLoading();
        },
        success: function (data, textStatus) {

       //     alert(textStatus);

            endID = endID.replace('_', '');
            if (endID != parseInt(endID)) {
                endID = "";
            } else {
                endID = "_" + endID;
            }
            var id = ctrlIdBefore + "DDL_" + ddlID + "" + endID;
           // alert('FullCtrlDDLDB:' + id);

            $("#" + id).empty();
            var dataObj = eval("(" + data + ")"); //转换为json对象 
            //alert(data);
            //alert( $("#" + id) );
//            $.each(dataObj.Head, function (idx, item) {
//                $("#" + ddlChild).append("<option value='" + item.No + "'>" + item.Name + "</option");
//            });

            $.each(dataObj.Head, function (idx, item) {
//                alert(idx);
//                alert(item.No);
//                alert(item.Name);
                $("#" + id).append("<option value='" + item.No + "'>" + item.Name + "</option");
                //$("#" + id).append("<option value='" + item.No + "'>" + item.Name + "</option");
            });
        },
        complete: function (XMLHttpRequest, textStatus) {
            //HideLoading();
        },
        error: function () {
            //请求出错处理
        }
    });
}

function FullCtrl(e, ctrlIdBefore, fk_mapExt) {
    e = escape(e);
    GenerPageKVs();
    var json_data = { "Key": e, "FK_MapExt": fk_mapExt, "DoType": "ReqCtrl", "KVs": kvs };
    $.ajax({
        type: "get",
        url: "HanderMapExt.ashx",
        data: json_data,
        beforeSend: function (XMLHttpRequest) {
            //ShowLoading();
        },
        success: function (data, textStatus) {
            var dataObj = eval("(" + data + ")"); //转换为json对象 
            var beforeID = null;
            var endId = null;

            // 根据ddl 与 tb 不同。 
            if (ctrlIdBefore.indexOf('DDL_') > 1) {
                beforeID = ctrlIdBefore.substring(0, ctrlIdBefore.indexOf('DDL_'));
                endId = ctrlIdBefore.substring(ctrlIdBefore.lastIndexOf('_'));
            } else {
                beforeID = ctrlIdBefore.substring(0, ctrlIdBefore.indexOf('TB_'));
                endId = ctrlIdBefore.substring(ctrlIdBefore.lastIndexOf('_'));
            }

            for (var i in dataObj.Head) {
                if (typeof (i) == "function")
                    continue;

                for (var k in dataObj.Head[i]) {
                    if (k == 'No' || k == 'Name')
                        continue;

                    //  alert(k + ' val= ' + dataObj.Head[i][k]);

                    $("#" + beforeID + 'TB_' + k).val(dataObj.Head[i][k]);
                    $("#" + beforeID + 'TB_' + k + endId).val(dataObj.Head[i][k]);

                    $("#" + beforeID + 'DDL_' + k).val(dataObj.Head[i][k]);
                    $("#" + beforeID + 'DDL_' + k + endId).val(dataObj.Head[i][k]);

                    if (dataObj.Head[i][k] == '1') {
                        $("#" + beforeID + 'CB_' + k).attr("checked", true);
                        $("#" + beforeID + 'CB_' + k + endId).attr("checked", true);
                    } else {
                        $("#" + beforeID + 'CB_' + k).attr("checked", false);
                        $("#" + beforeID + 'CB_' + k + endId).attr("checked", false);
                    }
                }
            }
        },
        complete: function (XMLHttpRequest, textStatus) {
            //HideLoading();
        },
        error: function () {
            //请求出错处理
            alert('error where funnCtrl');
        }
    });
}

var itemStyle = 'padding:2px;color: #000000;background-color:Silver;width:100%;border-style: none double double none;border-width: 1px;';
var itemStyleOfS = 'padding:2px;color: #000000;background-color:green;width:100%';
function ItemClick(sender, val, tbid, fk_mapExt) {

    $("#divinfo").empty();
    $("#divinfo").css("display", "none");
    highlightindex = -1;
    oldValue = val;

    $("#" + tbid).val(oldValue);

    // 填充.
    FullIt(oldValue, tbid, fk_mapExt);
}

function MyOver(sender) {
    if (highlightindex != -1) {
        $("#divinfo").children("div").eq(highlightindex).css("background-color", "Silver");
    }

    highlightindex = $(sender).attr("name");
    $(sender).css("background-color", "blue");
    $(sender).css("color", "white");
}

function MyOut(sender) {
    $(sender).css("background-color", "Silver");
    $(sender).css("color", "black");
}

function hiddiv() {
    $("#divinfo").css("display", "none");
}

// 获取参数.
function GetQueryString(name) {
   var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)","i");
   var r = window.location.search.substr(1).match(reg);
   if (r!=null) 
   return unescape(r[2]); 
   return null;
}
