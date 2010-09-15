<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AboutUs.ascx.cs" Inherits="GE_Comm_AboutUs" %>

<table width='80%' align=center>
<tr align="center">
<td colspan="2"><h3 style="font-size:20px; font-weight:bold;"><%= BP.SystemConfig.CustomerName %></h3><hr height=1px  color=#ededed /></td>
</tr>
<tr>
<TD  width= "50%"  align="center" rowspan="2" valign="top">
<br>
<%
string url =this.Request.ApplicationPath+"/Data/CustData/"+ BP.SystemConfig.CityNo+"/map.jpg";
 %>
<img src="<%=url %>"  height='400px' width='500px' />

<br>

</TD>
<TD align="left" width= "60%"  valign="top" style="padding-left:20px;">
<img src="images/contacttitle.jpg" />
</TD>

</tr>

<tr>
<TD align="left" width= "40%"  valign="top" style="padding-left:20px; line-height:200%;"><h2 style="font-size:16px; font-weight:bold;">客户服务热线：<%=BP.SystemConfig.AppSettings["ServiceTel"].ToString()%></h2>
    <p><b>电话：</b><%=BP.SystemConfig.AppSettings["Tel"].ToString()%></p>
	 <p><b>传真：</b><% =BP.SystemConfig.AppSettings["Fax"].ToString()%></p>
	 <p><b>网址：</b><%=BP.SystemConfig.AppSettings["WebSite"].ToString()%></p>
     <p><b>电子邮箱：</b><%=BP.SystemConfig.AppSettings["Email"].ToString()%></p>
    <p><b>地址：</b><%=BP.SystemConfig.AppSettings["Address"].ToString()%></p>
    <p><b>联系人：</b><%=BP.SystemConfig.AppSettings["Contacter"].ToString()%></p>
    &nbsp;</TD>

</tr>

</table>