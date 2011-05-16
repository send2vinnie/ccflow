<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReturnWork.ascx.cs" Inherits="WF_UC_ReturnWork" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<%@ Register src="FlowInfoSimple.ascx" tagname="FlowInfoSimple" tagprefix="uc2" %>
<%@ Register src="../../Comm/UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc3" %>
<div align="center" width='50%'>

            <div  align=center class=Title  >
                    <uc3:ToolBar ID="ToolBar1" runat="server" />

            </div>

            <div style='height:4px;'>
            </div>

            <div >
                <uc1:Pub ID="Pub1" runat="server" />
            </div>

</div>
