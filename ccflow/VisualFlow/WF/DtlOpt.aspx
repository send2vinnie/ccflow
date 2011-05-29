<%@ Page Title="" Language="C#" MasterPageFile="~/WF/WinOpen.master" AutoEventWireup="true" CodeFile="DtlOpt.aspx.cs" Inherits="WF_DtlOpt" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Comm/Style/Tabs.css" rel="stylesheet" type="text/css" />
    <script language=javascript>
        function selectAll() {
            var arrObj = document.all;
            if (document.forms[0].checkedAll.checked) {
                for (var i = 0; i < arrObj.length; i++) {
                    if (typeof arrObj[i].type != "undefined" && arrObj[i].type == 'checkbox') {
                        arrObj[i].checked = true;
                    }
                }
            } else {
                for (var i = 0; i < arrObj.length; i++) {
                    if (typeof arrObj[i].type != "undefined" && arrObj[i].type == 'checkbox')
                        arrObj[i].checked = false;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Pub ID="Top" runat="server" />
    <uc1:Pub ID="Pub1" runat="server" />
</asp:Content>

