<%@ Page Language="C#" MasterPageFile="~/DocFlow/Style/WinOpen.master" AutoEventWireup="true"
    CodeFile="DoClient.aspx.cs" Inherits="GovDoc_DoClient" Title="无标题页" %>

<%@ Register Src="../WF/Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script language="javascript">
function DoDelCaoGao(oid,fk_flow)
{
 if (window.confirm('您确定要删除草稿吗？')==false)
     return;
   var url='Do.aspx?DoType=DelCaogao&RefOID='+oid+'&FK_Flow='+fk_flow;
   window.showModalDialog(url);
   window.location.href=window.location.href;
}

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:Pub ID="Pub1" runat="server" />
</asp:Content>
