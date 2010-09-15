<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PartFDBDir.ascx.cs" Inherits="GE_FDB_PartFDBDir" %>
<%@ Register Src="../Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<div class="dtree">
    <table class="Table" cellspacing="0">
        <tr>
            <uc1:Pub ID="Pub1" runat="server" />
        </tr>
        <tr>
            <td class="TD" valign="top"  height='500px'>
                <p>
                    <a href="javascript: d.openAll();">open all</a> | <a href="javascript: d.closeAll();">
                        close all</a></p>
                <uc1:Pub ID="PubDir" runat="server" />
            </td>
        </tr>
    </table>
</div>
