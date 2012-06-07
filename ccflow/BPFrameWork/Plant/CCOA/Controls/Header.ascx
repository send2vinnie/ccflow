<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="CCOA_Header" %>
<div class="header">
    <div style="height: 60px; font-size: 40px; font-family: Arial Unicode MS; font-weight: bold;">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    Logo
                </td>
                <td>
                    <span class="style1">OA自动办公系统</span>
                </td>
                <td style="width: 260px;">
                    <span class="style2"><a href="../Home.aspx">CCOA</a></span> <span class="style2"><a
                        href="../Home.aspx">CCCRM</a></span> <span class="style2"><a href="../Home.aspx">CCIM</a></span>
                    <select id="selectSkin" onchange="onSkinChange(this.value)" style="width: 100px;">
                        <option value="default">Default</option>
                        <option value="blue">Blue</option>
                        <option value="gray">Gray</option>
                        <option value="olive2003">Olive2003</option>
                        <option value="blue2003">Blue2003</option>
                        <option value="blue2010">Blue2010</option>
                    </select>
                </td>
            </tr>
        </table>
    </div>
</div>
