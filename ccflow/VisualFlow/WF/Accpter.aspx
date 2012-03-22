<%@ Page Language="C#" MasterPageFile="WinOpen.master" AutoEventWireup="true" CodeFile="Accpter.aspx.cs" Inherits="WF_Accpter" Title="无标题页" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<%@ Register assembly="BP.Web.Controls" namespace="BP.Web.Controls" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="JavaScript" src="../../Comm/JScript.js"　type="text/javascript"></script>
        <script type="text/javascript">
            //调用发送按钮

            function send() {

                window.opener.document.getElementById('ContentPlaceHolder1_MyFlowUC1_MyFlow1_ToolBar1_Btn_Send').click();
                window.close();
            }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table border=0 width='100%'>
<tr>
<td valign=top><uc1:Pub ID="Left" runat="server" /></td>
</tr>
<tr>
<td><uc1:Pub ID="Pub1" runat="server" /></td>
</tr>
    </table>
</asp:Content>
