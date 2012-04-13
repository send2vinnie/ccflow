<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddrBook.ascx.cs" Inherits="CCOA_AddressBook_AddrBook" %>
<table width="100%">
        <tr >
            <td style="background:url(Img/bgx.png) repeat-x; background-position-y:-252px;">
                姓名
            </td>
            <td  style="background:url(Img/bgx.png) repeat-x; background-position-y:-252px;">
                邮件地址
            </td>
            <td  style="background:url(Img/bgx.png) repeat-x; background-position-y:-252px;">
                手机号码
            </td>
        </tr>
        <%foreach (BP.CCOA.AddrBook item in AddrBooks)
          {%>
        <tr>
            <td>
                <%=item.Name %>
            </td>
            <td>
                <%=item.Email%>
            </td>
            <td>
                <%=item.Tel%>
            </td>
        </tr>
        <%  } %>
    </table>