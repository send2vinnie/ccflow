<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Demo.aspx.cs" Inherits="_Demo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        //异步填充省数据
        function DoAnscPro() {
            var json_data = { "pro": "All" };
            $.ajax({
                type: "get",
                data: json_data,
                url: "Handler.ashx",
                beforeSend: function (XMLHttpRequest) {
                    //ShowLoading();
                },
                success: function (data, textStatus) {
                    $("#ddlPro").empty();
                    var dataObj = eval("(" + data + ")"); //转换为json对象 
                    $.each(dataObj.Head, function (idx, item) {
                        $("#ddlPro").append("<option value='" + item.proID + "'>" + item.proName + "</option");
                    });
                    DoAnsc($("#ddlPro").val());
                },
                complete: function (XMLHttpRequest, textStatus) {
                    //HideLoading();
                },
                error: function () {
                    //请求出错处理
                }
            });
        }

        //根据选择的省异步加载城市.
        function DoAnsc(e) {
            var json_data = { "city": e };
            $.ajax({
                type: "get",
                url: "Handler.ashx",
                data: json_data,
                beforeSend: function (XMLHttpRequest) {
                    //ShowLoading();
                },
                success: function (data, textStatus) {
                    $("#ddlCity").empty();
                    var dataObj = eval("(" + data + ")"); //转换为json对象 
                    $.each(dataObj.Head, function (idx, item) {
                        //if (idx == 0) {
                        //    return true; //同countinue，返回false同break 
                        //}
                        $("#ddlCity").append("<option onclick='alert(this.value)' value='" + item.cityID + "'>" + item.cityName + "</option");
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

        //城市列表发生改变时执行
        function ddlCity_onchange(e) {
            var strVal = $("#ddlPro").val() + $("#ddlPro option:selected").text();
            strVal += $("#ddlCity").val() + $("#ddlCity option:selected").text();
            alert(strVal);
        }
    </script>
    <script type="text/javascript">
        function openDiv(e) {
            if (document.getElementById("divinfo").style.display == "none") {
                var txtObject = document.getElementById("Text1");
                var orgObject = document.getElementById("divinfo")
                var rect = getoffset(txtObject);
                orgObject.style.top = rect[0] + 22;
                orgObject.style.left = rect[1];
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
        var oldValue = "";
        var highlightindex = -1;
        //根据关键字动态查询.
        function DoAnscToFillDiv(sender, e) {

            openDiv(sender);

            var myEvent = event || window.event;
            var myKeyCode = myEvent.keyCode;
            //获得ID为divinfo里面的DIV对象   
            var autoNodes = $("#divinfo").children("div");

            if (myKeyCode == 38) {
                if (highlightindex != -1) {
                    autoNodes.eq(highlightindex).css("background-color", "white");
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
            }

            else if (myKeyCode == 40) {
                if (highlightindex != -1) {
                    autoNodes.eq(highlightindex).css("background-color", "white");
                    highlightindex++;
                }
                else {
                    highlightindex++;
                }

                if (highlightindex == autoNodes.length) {
                    autoNodes.eq(autoNodes.length).css("background-color", "white");
                    highlightindex = 0;
                }
                autoNodes.eq(highlightindex).css("background-color", "blue");
            }
            else if (myKeyCode == 13) {
                if (highlightindex != -1) {
                    //获得选中的那个的文本值   
                    var textInputText = autoNodes.eq(highlightindex).text();
                    autoNodes.eq(highlightindex).css("background-color", "white");
                    $("#Text1").val(textInputText);
                    $("#divinfo").hide();
                    oldValue = textInputText;
                    highlightindex = -1;
                }
            }
            else {
                if (e != oldValue) {
                    $("#divinfo").empty();
                    var json_data = { "key": e };
                    $.ajax({
                        type: "get",
                        url: "Handler.ashx",
                        data: json_data,
                        beforeSend: function (XMLHttpRequest) {
                            //ShowLoading();
                        },
                        success: function (data, textStatus) {
                            if (data != "") {
                                highlightindex = -1;
                                var dataObj = eval("(" + data + ")"); //转换为json对象 
                                $.each(dataObj.Head, function (idx, item) {
                                    $("#divinfo").append("<div name='" + idx + "' onmouseover='MyOver(this)' onmouseout='MyOut(this)' onclick='div_onclick(\"" + item.cityName + "\")' value='" + item.cityID + "'>" + item.cityName + "</div>");
                                });
                            }
                        },
                        complete: function (XMLHttpRequest, textStatus) {
                            //HideLoading();
                        },
                        error: function () {
                            //请求出错处理
                        }
                    });
                    oldValue = e;
                }
            }
        }

        function div_onclick(e) {
            $("#Text1").val(e);
            $("#divinfo").empty();
            $("#divinfo").css("display", "none");
            highlightindex = -1;
            oldValue = e;
        }

        function MyOver(sender) {
            if (highlightindex != -1) {
                $("#divinfo").children("div").eq(highlightindex).css("background-color", "white");
            }
            highlightindex = $(sender).attr("name");
            $(sender).css("background-color", "blue");
        }

        function MyOut(sender) {
            $(sender).css("background-color", "White");
        }

        function hiddiv() {
            $("#divinfo").css("display", "none");
        }
    </script>
</head>
<body bgcolor=silver >
    <form id="form1" runat="server">
    <div>
        <input type="button" onclick="DoAnscPro()" value="Test" />
    </div>
    </form>
    <select id="ddlPro" onchange="DoAnsc(this.value)">
    </select>
    <select id="ddlCity" onchange="ddlCity_onchange(this.value)">
    </select>
    <br />
    <br />
    <br />
    <br />
    <br /> 
    <br />
    <br />
    <div id="divinfo" style="width: 155px; position: absolute; color: Lime; display: none;
        cursor: pointer">
    </div>
    &nbsp;<input id="Text1" type="text" onkeyup="DoAnscToFillDiv(this,this.value)" style="position: absolute;
        top: 314px; left: 739px;" />
</body>
</html>
