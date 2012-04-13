<%@ Page Title="" Language="C#" MasterPageFile="~/Port/WinOpen.master" AutoEventWireup="true"
    CodeFile="NewHome.aspx.cs" Inherits="Port_NewHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .module
        {
            float: left;
            width: 800px;
            margin-left: 10px;
            margin-top: 10px;
            display: inline;
        }
        
        .column
        {
            width: 50%;
            float: left;
        }
        .portlet
        {
            margin: 0 1em 1em 0;
        }
        .portlet-header
        {
            margin: 0.3em;
            padding: 4px 4px;
        }
        .portlet-header .ui-icon
        {
            float: right;
        }
        .portlet-content
        {
            padding: 0.4em;
            height: 90px;
            overflow: hidden;
        }
        .ui-sortable-placeholder
        {
            border: 1px dotted black;
            visibility: visible !important;
            height: 50px !important;
        }
        .ui-sortable-placeholder *
        {
            visibility: hidden;
        }
        .portlet-content li
        {
            list-style-type: circle;
        }
    </style>
    <script type="text/javascript">
        $(function () {

            var listleft = $("div[id = 'column_left']");
            var listright = $("div[id = 'column_right']");

            $(".column").sortable({
                connectWith: ".column",
                revert: true, //缓冲效果 
                cursor: 'move'
            });

            $(".column").bind("sortcreate", function (event, ui) {
                //alert("sortcreate");
            });
            $(".column").bind("sortstart", function (event, ui) {
                //alert("sortstart");
            });
            $(".column").bind("sort", function (event, ui) {
                //alert("sort");
            });
            $(".column").bind("sortchange", function (event, ui) {
                //alert("sortchange");
            });
            $(".column").bind("sortbeforestop", function (event, ui) {
                //alert("sortbeforestop");
            });
            $(".column").bind("sortstop", function (event, ui) {

                var new_order_left = []; //左栏布局
                var new_order_right = []; //右栏布局

                listleft.children(".portlet").each(function () {
                    new_order_left.push(this.title);
                });
                listright.children(".portlet").each(function () {
                    new_order_right.push(this.title);
                });

                var newleftid = new_order_left.join(',');
                var newrightid = new_order_right.join(',');

                var moduleOrder = newleftid + ":" + newrightid;

                $.ajax({
                    type: "POST",
                    url: "ashx/SetCustomerSetting.ashx",
                    data: "moduleOrder=" + moduleOrder
                });

            });


            $(".column").bind("sortupdate", function (event, ui) {
                //alert("sortupdate");
            });

            $(".portlet").addClass("ui-widget ui-widget-content ui-helper-clearfix ui-corner-all")
			.find(".portlet-header")
				.addClass("ui-widget-header ui-corner-all")
				.prepend("<span class='ui-icon ui-icon-minusthick'></span>")
				.end()
			.find(".portlet-content");

            $(".portlet-header .ui-icon").click(function () {
                $(this).toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
                $(this).parents(".portlet:first").find(".portlet-content").toggle();
            });

            $(".column").disableSelection();

            $.ajax({
                type: "POST",
                url: "ashx/GetCustomerSetting.ashx",
                success: function (list) {

                    if (list == "") {
                        list = "1,2,3,4:5,6,7,8";
                    }
                    var listnode = $(".column").find(".portlet");
                    listleft.children(".portlet").detach();
                    listright.children(".portlet").detach();
                    var leftNo = list.split(':')[0].split(',');
                    var rightNo = list.split(':')[1].split(',');
                    //alert(leftNo + rightNo);

                    $.each(leftNo, function (li, lobj) {
                        $.each(listnode, function (i, obj) {
                            if (lobj == obj.title)
                                listleft.append(obj);
                        });
                    });

                    $.each(rightNo, function (ri, robj) {
                        $.each(listnode, function (i, obj) {
                            if (robj == obj.title)
                                listright.append(obj);
                        });
                    });
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="module">
        <div class="column" id="column_left">
            <div class="portlet" title="1">
                <div class="portlet-header">
                    自定义模块一</div>
                <div class="portlet-content">
                    <ul>
                        <asp:Repeater ID="rpt1" runat="server">
                            <ItemTemplate>
                                <li><a href="#">
                                    <%# Eval("Title") %></a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
            <div class="portlet" title="2">
                <div class="portlet-header">
                    自定义模块二</div>
                <div class="portlet-content">
                </div>
            </div>
            <div class="portlet" title="3">
                <div class="portlet-header">
                    自定义模块三</div>
                <div class="portlet-content">
                </div>
            </div>
            <div class="portlet" title="4">
                <div class="portlet-header">
                    自定义模块四</div>
                <div class="portlet-content">
                </div>
            </div>
        </div>
        <div class="column" id="column_right">
            <div class="portlet" title="5">
                <div class="portlet-header">
                    自定义模块五</div>
                <div class="portlet-content">
                </div>
            </div>
            <div class="portlet" title="6">
                <div class="portlet-header">
                    自定义模块六</div>
                <div class="portlet-content">
                </div>
            </div>
            <div class="portlet" title="7">
                <div class="portlet-header">
                    自定义模块七</div>
                <div class="portlet-content">
                </div>
            </div>
            <div class="portlet" title="8">
                <div class="portlet-header">
                    自定义模块八</div>
                <div class="portlet-content">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
