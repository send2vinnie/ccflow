<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NetDiskDir.ascx.cs" Inherits="GE_NetDisk_NetDiskDir" %>
<%@ Register Src="../Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<link href="js/dtree.css" rel="Stylesheet" type="text/css" />

<script src="js/dtree.js" type="text/javascript"></script>

<script src="js/netdisk.js" type="text/javascript"></script>

<table width='100%'>
    <tr>
        <td valign="top" height='500px' style="width: 25%">
            <div class="dtree" id="divLeft">
                <table>
                    <tr>
                        <td>
                            <%--<input type="button" id="btnAddNew" onclick="return ShowDivName()" value="新建" style="width: 50px;" />--%>
                            <asp:ImageButton ID ="btnAddNew" OnClientClick="return ShowDivName();"  ImageUrl="~/GE/NetDisk/img/ibnew.jpg"  runat ="server" />
                        </td>
                        <td>
                            <%--<asp:Button ID="btnDelete" Text="删除" OnClientClick="return getDeleteNodeID()" OnClick="btnDelete_Click"
                                runat="server" Style="width: 50px;" />--%>
                            <asp:ImageButton ID ="btnDelete" OnClientClick="return getDeleteNodeID()" OnClick ="btnDelete_Click"  ImageUrl="~/GE/NetDisk/img/ibdelete.jpg" runat ="server" />
                                
                        </td>
                        <td>
                           <%-- <asp:Button ID="btnUp" Text="上移" OnClientClick="return getMoveNodeID()" OnClick="btnUp_Click"
                                runat="server" Style="width: 50px;" />--%>
                            <asp:ImageButton ID ="btnUp" OnClientClick="return getMoveNodeID()" OnClick="btnUp_Click" ImageUrl="~/GE/NetDisk/img/ibup.jpg"  runat ="server"/>
                                
                        </td>
                        <td>
                        <%--    <asp:Button ID="btnDown" Text="下移" OnClientClick="return getMoveNodeID()" OnClick="btnDown_Click"
                                runat="server" Style="width: 50px;" />--%>
                            <asp:ImageButton ID ="btnDown" OnClientClick="return getMoveNodeID()"  OnClick="btnDown_Click" ImageUrl="~/GE/NetDisk/img/ibdown.jpg"  runat ="server"/>
                                
                        </td>
                        <td>
                            <%--<input type="button" id="btnEditName" onclick="DoEditName()" value="重命名" style="width: 55px;" />--%>
                            <asp:ImageButton ID ="btnEditName" OnClientClick="return DoEditName();"  ImageUrl="~/GE/NetDisk/img/ibeditname.jpg"  runat ="server"/>
                            
                        </td>
                    </tr>
                </table>
                <uc1:Pub ID="Pub1" runat="server" />
            </div>
        </td>
        <td valign="top" height='800px' class='iframe' style='border: 1px #ccc solid; padding: 8px 10px'>
            <uc1:Pub ID="Pub2" runat="server" />
        </td>
    </tr>
</table>
<div id="divDirName" style="background: #fff; border: 1px #666 solid; position: relative;
    z-index: 2; left: 30px; top: -795px; width: 80px; display: none;">
    <div style="font-weight: bolder; font-size: 12px; height: 25px; line-height: 25px;
        background-color: #b2cffb; padding-left: 10px;">
        新建目录</div>
    <table class="table_input">
        <tr>
            <td align="center">
               <%-- <input type="button" id="btnAddNewSil" class="btn_01" value="新建同级目录" style="width: 90px;
                    height: auto" onclick="DoSaveName()" />--%>
                            <asp:ImageButton ID ="btnAddNewSil"   OnClientClick="DoSaveName()" ImageUrl="~/GE/NetDisk/img/ibxjtjml.jpg" runat ="server"/>
                    
            </td>
        </tr>
        <tr>
            <td align="center">
                <%--<input type="button" id="btnAddNewChild" class="btn_01" value="新建子目录" style="width: 90px;
                    height: auto" onclick="DoSaveChildName()" />--%>
                            <asp:ImageButton ID ="btnAddNewChild" OnClientClick ="DoSaveChildName()"  ImageUrl="~/GE/NetDisk/img/ibxjzml.jpg" runat ="server"/>
                    
            </td>
        </tr>
        <tr>
            <td align="center">
<%--                <input type="button" id="btnCancel" class="btn_01" value="取消" style="width: 90px;
                    height: auto" onclick="javascript:hidDiv(divDirName);hidDiv(bgDiv);" />--%>
                            <asp:ImageButton ID ="btnCancel" OnClientClick="javascript:hidDiv(divDirName);hidDiv(bgDiv);return false;"  ImageUrl="~/GE/NetDisk/img/ibcancle.jpg" runat ="server"/>
                    
            </td>
        </tr>
    </table>
</div>
<input type="hidden" id="hidNodeID" runat="server" />
<input type="hidden" id="hidDirOID" runat="server" />
