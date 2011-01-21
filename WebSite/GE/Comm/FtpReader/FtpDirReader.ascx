<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FtpDirReader.ascx.cs" Inherits="GE_Comm_FtpDirReader" %>
<%@ Register Src="../../Pub.ascx" TagName="Pub" TagPrefix="uc1" %>

<div class="dtree">
    <table class="Table" cellspacing="0" >
        <tr>
            <uc1:Pub ID="Pub1" runat="server" />
        </tr>
        <tr>
            <td valign="top" style="border-top:#abcbe6 1px solid;padding-left:5px;padding-top:5px;height:500px">
               <%-- <p>
                    <a href="javascript: d.openAll();">open all</a> | <a href="javascript: d.closeAll();">
                        close all</a></p>--%>
                <uc1:Pub ID="PubDir" runat="server" />
            </td>
        </tr>
    </table>
</div>
