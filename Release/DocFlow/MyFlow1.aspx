


<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" 
Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="uc1" TagName="UCSys" Src="../Comm/UC/UCSys.ascx" %>
<%@ page language="c#" inherits="BP.Web.WF.GovDoc_MyFlow, App_Web_dcnuosxx" %>
<%@ Register TagPrefix="uc1" TagName="UCEn" Src="../Comm/UC/UCEn.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="BP.Web.Controls" Assembly="BP.Web.Controls" %>
<%@ Register Src="../WF/Pub.ascx" TagName="Pub" TagPrefix="uc2" %>
<html>
<head>
    <title>我的工作 --
        <%=BP.Web.WebUser.No %>
        <%=BP.Web.WebUser.Name %>，<%=BP.Web.WebUser.FK_Dept %><%=BP.Web.WebUser.FK_DeptName %>
    </title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="./Style/Style.css" type="text/css" rel="stylesheet">
    <link href="./Style/Table.css" type="text/css" rel="stylesheet">
    <script language="JavaScript" src="./Style/JScript.js"></script>
</head>
<body topmargin="0" leftmargin="0" bgcolor=white >
    <form id="Form1" method="post" runat="server">
    <table id="Table1"  width="97%" height="97%" border="1" class="Table"  bgcolor=white >
        <tr height="1%">
            <td height="1%" background="Images/Title_MyFlow.gif" align="center">
            <font color=white ><b>
                    <uc1:UCSys ID="UCSys1" runat="server"></uc1:UCSys></b></font>
            </td>
        </tr>
        <tr height="1%">
            <td style="height: 1%">
                <cc1:BPToolBar ID="BPToolBar2" runat="server" AutoPostBack="True" Width="117%">
                </cc1:BPToolBar>
            </td>
        </tr>
        <tr valign="top" height="95%" width="100%">
            <td valign="top" height="100%" width="100%">
                <uc1:UCEn ID="UCEn1" runat="server"></uc1:UCEn>
                <div align="center">
                    <uc2:Pub ID="Pub1" runat="server" />
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
