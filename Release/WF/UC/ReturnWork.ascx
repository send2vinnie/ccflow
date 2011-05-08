<%@ control language="C#" autoeventwireup="true" inherits="WF_UC_ReturnWork, App_Web_dwfygdvu" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<%@ Register src="FlowInfoSimple.ascx" tagname="FlowInfoSimple" tagprefix="uc2" %>
<%@ Register src="../../Comm/UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc3" %>
<link href="../Style/Table.css" rel="stylesheet" type="text/css" />
<table id="Table1" height="99%" align=left border=0 >
  <tr>
<td rowspan=3 valign=top nowarp=true width=25% >
</td>
</tr>
        <tr height="1%">
            <td style="height: 1%" class=ToolBar align=left>
                    <uc3:ToolBar ID="ToolBar1" runat="server" />
            </td>
        </tr>
        <tr valign="top" width="100%">
            <td valign="top" height="100%" width="100%" align=left>
                <uc1:Pub ID="Pub1" runat="server" />
            </td>
        </tr>

    </table>
