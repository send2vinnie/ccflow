<%@ Page Title="我的提问" Language="C#" MasterPageFile="~/Style/MasterPage.master" AutoEventWireup="true"
    CodeFile="Question.aspx.cs" Inherits="FAQ_Question" %>

<%@ Register Src="../UC/Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Style/resources .css" type="text/css" rel="Stylesheet" />
    <link href="../Style/reset.css" type="text/css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--左侧-->
    <div id="left_03">
        <h3 class="ttlm_ask">
            <span class="setLink">资源请求状态</span></h3>
        <ul class="kemuList">
            <uc1:Pub ID="Pub2" runat="server" />
            <!--pub-->
        </ul>
    </div>
    <!--/左侧-->
    <!--右侧-->
    <div id="right_03">
        <table width="100%">
            <tr>
                <td class="td14">
                    <img src="../Style/Img/round_01.gif" alt="" />
                </td>
                <td class="tdBg_blue">
                </td>
                <td class="td14">
                    <img src="../Style/Img/round_02.gif" alt="" />
                </td>
            </tr>
            <tr>
                <td class="tdBg_blue">
                </td>
                <td class="tbContent">
                    <uc1:Pub ID="Pub1" runat="server" />
                    <uc1:Pub ID="Pub3" runat="server" />
                    <!--pub-->
                </td>
                <td class="tdBg_blue">
                </td>
            </tr>
            <tr>
                <td class="td14">
                    <img src="../Style/Img/round_04.gif" alt="" />
                </td>
                <td class="tdBg_blue">
                </td>
                <td class="td14">
                    <img src="../Style/Img/round_03.gif" alt="" />
                </td>
            </tr>
        </table>
    </div>
    <!--/右侧-->

    <script language="javascript" type="text/javascript">
         function DoSelect(test) {

            ss = test.split(",");
            var url = 'Do.aspx?DoType=NumOfRead&OID=' + ss[1];
            window.showModalDialog(url);
            var url2 = ss[0] + '?RefOID=' + ss[1];
            window.open(url2);
            window.location.reload();
            return;
        }
    </script>

</asp:Content>
