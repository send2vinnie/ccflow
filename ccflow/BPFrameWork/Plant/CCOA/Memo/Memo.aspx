<%@ Page Title="" Language="C#" MasterPageFile="~/CCOA/WinOpen.master" AutoEventWireup="true"
    CodeFile="Memo.aspx.cs" Inherits="CCOA_Memo_Memo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../Comm/JS/jquery-1.6.min.js" type="text/javascript"></script>
    <script src="../../Comm/JS/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../../Comm/JS/plugins/jquery.window.js" type="text/javascript"></script>
    <link href="../../Comm/JS/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../Comm/JS/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../Comm/JS/themes/default/window.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        //        $("td").onmouseover(
        //            function () {
        //                alert('xxx');
        //            }
        //        );

        function popupDiv(sender) {
            //alert(sender);

            open();
        }



        function open() {
            $('#w').window('open');
        }
        function closeWindow() {
            $('#w').window('close');
        }
     
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <xuc:XToolBar ID="XToolBar" runat="server" title="个人备忘列表" />
    <table class="Memo" width="100%">
        <tr class="title">
            <th>
                星期日
            </th>
            <th>
                星期一
            </th>
            <th>
                星期二
            </th>
            <th>
                星期三
            </th>
            <th>
                星期四
            </th>
            <th>
                星期五
            </th>
            <th>
                星期六
            </th>
        </tr>
        <tr>
            <% for (int i = 1; i <= 7; i++)
               {%>
            <td style="height: 60px; vertical-align: top;" onclick="popupDiv(this)">
                <span style="background: #DCF0FB; border: solid 1px #AACCEE; width: 50px; height: 10px;">
                    <%=i %>日</span><br />
                <div style="margin-top: 5px;">
                    <ul>
                        <li>工作事项1 </li>
                        <li>工作事项2</li>
                    </ul>
                </div>
            </td>
            <% }%></tr>
        <tr>
            <% for (int i = 8; i <= 14; i++)
               {%>
            <td style="height: 60px; vertical-align: top;">
                <span style="background: #DCF0FB; border: solid 1px #AACCEE; width: 50px; height: 10px;">
                    <%=i %>日</span>
            </td>
            <% }%></tr>
        <tr>
            <% for (int i = 15; i <= 21; i++)
               {%>
            <td style="height: 60px; vertical-align: top;">
                <span style="background: #DCF0FB; border: solid 1px #AACCEE; width: 50px; height: 10px;">
                    <%=i %>日</span>
            </td>
            <% }%></tr>
        <tr>
            <% for (int i = 22; i <= 28; i++)
               {%>
            <td style="height: 60px; vertical-align: top;">
                <span style="background: #DCF0FB; border: solid 1px #AACCEE; width: 50px; height: 10px;">
                    <%=i %>日</span>
            </td>
            <% }%></tr>
        <tr>
            <% for (int i = 29; i <= 31; i++)
               {%>
            <td style="height: 60px; vertical-align: top;">
                <span style="background: #DCF0FB; border: solid 1px #AACCEE; width: 50px; height: 10px;">
                    <%=i %>日</span>
            </td>
            <% }%></tr>
    </table>
    <div id="w" class="easyui-window" title="新备忘" closed="true" modal="true" style="width: 500px;
        height: 250px; padding: 5px;">
        <div class="easyui-layout" fit="true">
            <div region="center" border="false" style="padding: 10px; background: #fff; border: 1px solid #ccc;">
                <table width="100%">
                    <tr>
                        <th>
                            主题:
                        </th>
                        <td colspan="3">
                            <input type="text" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            描述：
                        </th>
                        <td colspan="3">
                            <textarea id="TextArea1" cols="20" rows="4" style="width:100%;"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            提醒类型:
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlType" runat="server" Width="100px">
                                <asp:ListItem Text="无" Value="" />
                            </asp:DropDownList>
                        </td>
                        <th>
                            提醒时间：
                        </th>
                        <td>
                        <input type="text" />
                        </td>
                    </tr>
                </table>
            </div>
            <div region="south" border="false" style="text-align: right; height: 30px; line-height: 30px;">
                <a class="easyui-linkbutton" iconcls="icon-ok" href="javascript:void(0)" onclick="closeWindow()">
                    Ok</a> <a class="easyui-linkbutton" iconcls="icon-cancel" href="javascript:void(0)"
                        onclick="closeWindow()">Cancel</a>
            </div>
        </div>
    </div>
</asp:Content>
