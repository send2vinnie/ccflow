<%@ page language="C#" masterpagefile="WinOpen.master" autoeventwireup="true" inherits="WF_Accpter, App_Web_5dpdp204" title="无标题页" %>

<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>

<%@ Register assembly="BP.Web.Controls" namespace="BP.Web.Controls" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="JavaScript" src="../../Comm/JScript.js"　type="text/javascript"></script>
     <link href="../Comm/Style/Table.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table>
<tr>
 <td>
     <cc1:Tree ID="Tree1" runat="server">
     </cc1:Tree>
    </td>
    
    <td>
    <uc1:Pub ID="Pub1" runat="server" />
    </td>
   
    </tr>
    </table>
</asp:Content>

