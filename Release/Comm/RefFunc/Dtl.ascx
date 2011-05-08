<%@ control language="C#" autoeventwireup="true" inherits="Comm_RefFunc_Dtl, App_Web_iayr1l4m" %>
<%@ Register Src="./../UC/ucsys.ascx" TagName="ucsys" TagPrefix="uc1" %>
<%@ Register src="./../UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc2" %>
<table    id="Table1" align="left"  width="100%" >
    <tr>
    <td class="ToolBar" >
        <uc2:ToolBar ID="ToolBar1" runat="server" />
    </td>
    </tr>
     <tr>
    <td>
        <uc1:ucsys ID="ucsys1" runat="server" />
        <uc1:ucsys ID="ucsys2" runat="server" />
    </td>
    </tr>
    </table>

