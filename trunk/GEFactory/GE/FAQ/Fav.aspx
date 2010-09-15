<%@ Page Title="收藏" Language="C#" MasterPageFile="~/Style/MasterPage.master" AutoEventWireup="true"
    CodeFile="Fav.aspx.cs" Inherits="FAQ_Fav" %>

<%@ Register Src="../UC/Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Style/reset.css" type="text/css" rel="Stylesheet" />
    <link href="../Style/resources .css" type="text/css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--左侧-->
    <div id="left_03">
        <div>
            <h3 class="ttlm_ask">
                <a class="setLink" href="javascript:DoChKM()">设置关注领域</a></h3>
        </div>
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
                    <img src='../Style/Img/spacer.gif' alt='' width='1' height='15' />
                    <uc1:Pub ID="Pub1" runat="server" />
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

    <script language="javascript">
         function DoSelect(test) {
            ss = test.split(",");
            var url = 'Do.aspx?DoType=NumOfRead&OID=' + ss[1];
            window.showModalDialog(url);
            var url2 = ss[0] + '?RefOID=' + ss[1] ;
            window.open(url2);
            window.location.reload();
            return;
        }
        function DoDelFav(oid) {
            if (window.confirm('您确定要删除此收藏吗？') == false)
                return;
            window.showModalDialog('Do.aspx?RefOID=' + oid + "&DoType=DelFav");
            window.location.reload();
            return;
        }
    </script>

</asp:Content>
