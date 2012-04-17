<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu.ascx.cs" Inherits="CCOA_Menu" %>
<div class="Menu">
    <h3>
        菜单选项
    </h3>
    <ul>
        <% foreach (BP.GPM.Menu item in MenuList)
           {
               if (item.HisMenuType== BP.GPM.MenuType.FuncDot)
                   continue; /*如果是功能点*/
               if (BP.GPM.Glo.IsCanDoIt(item.OID,item.HisCtrlWay)==false)
                   continue; /*如果没有权限*/
               if (item.TreeNo.Length == 2)
               {
      %>
           <li>
            <img src="<%= item.Img %>" alt="" /><%=item.Name%>
          </li>
        <%}
                else
          {%>
        <li>&nbsp;&nbsp;<img src="<%= item.Img %>" alt="" /><a href="<%= item.Url %>" target=_blank ><%=item.Name%></a>
        </li>
        <%}%>
        <%}%>
    </ul>
    <div id="divPop" runat="server" visible="false" style="width: 100px; height: 50px;">
        <asp:Label ID="lbl" runat="server" />
    </div>
</div>
