<%@ Page Title="" Language="C#" MasterPageFile="~/CCOA/WinOpen.master" AutoEventWireup="true"
    CodeFile="Home.aspx.cs" Inherits="CCOA_Home" %>

<%@ Register Src="~/CCOA/Controls/Article_Newest.ascx" TagName="Article_Newest" TagPrefix="uc" %>
<%@ Register Src="~/CCOA/Controls/Email.ascx" TagName="Email" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="JS/Portal.js" type="text/javascript"></script>
    <link href="JS/portal.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        var portal = null;

        $.get("Ajax.aspx", { Action: "get", Name: "lulu" }, function (data, textStatus) {
            //返回的 data 可以是 xmlDoc, jsonObj, html, text, 等等.
            this;

            portal = new mini.ux.Portal();
            portal.set({
                style: "width: 100%;height:500px",
                columns: [250, "100%", 260]
            });
            portal.render(content);

            //panel
            var test = JSON.parse(data);
            for (var i = 0; i < test.length; i++) {
                test[i].column = parseInt(test[i].column);
            }
            //alert(JSON.stringify(test));
            portal.setPanels(test);

            var bodyEl = portal.getPanelBodyEl("p2");
            //bodyEl.appendChild(document.getElementById(""));

            //获取配置的panels信息
            var panels = portal.getPanels();

        });

        function getJsonData() {
            var panels = portal.getPanels();
            var values = "";
            for (var i = 0; i < panels.length; i++) {
                var panelid = panels[i].id;
                var column = $("#" + panelid).parent().attr('id');
                var seq = i;
                values = values + panelid + ":" + column + "#" + seq + ",";
            }
            $("#txtjson").val(values);
        }

        function saveLayout() {
            var panels = portal.getPanels();
            var values = "";
            for (var i = 0; i < panels.length; i++) {
                var panelid = panels[i].id;
                var column = $("#" + panelid).parent().attr('id');
                var seq = i;
                values = values + panelid + ":" + column + "|" + seq + ",";
            }

            values = values.substring(0, values.length - 1);

            $.ajax({
                type: "POST",
                datatype: "text",
                url: "SaveLayout.aspx?params=" + values,
                success: function (data) {
                    alert(data);
                }
            });
        }
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="tabs1" class="mini-tabs" activeindex="0" style="width: 100%; height: 515px;">
        <div title="工作台" iconcls="icon-add">
            <input type="button" onclick="saveLayout()" value="保存配置" />
            <div id="content">
            </div>
        </div>
    </div>
</asp:Content>
