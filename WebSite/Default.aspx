<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="驰骋工作流程管理系统,.NET工作流程引擎,QQ:hiflow@qq.com,Tel:13589028385" %>

<%@ Register src="GE/Info/Info/InfoTabs.ascx" tagname="InfoTabs" tagprefix="uc1" %>

<%@ Register src="GE/Pict/ImgRun.ascx" tagname="ImgRun" tagprefix="uc2" %>
<%@ Register src="GE/Info/Info/InfoImgPlay.ascx" tagname="InfoImgPlay" tagprefix="uc3" %>


<%@ Register src="GE/Gener/ImgLink.ascx" tagname="ImgLink" tagprefix="uc4" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border=0 width='100%' >
<tr>
<td valign=top>
    <uc3:InfoImgPlay ID="InfoImgPlay1" runat="server" />
    <br />
</td>
<td valign=top align=left  nowarp=false width='200px'  class="TD">
<fieldset>
<legend>系统特点</legend>

   驰骋工作流程引擎是一款体积小巧、部署简单<br>、功能强大的流程设计执行管理工具。<br>
   它支持Access、SQLServer、Oracle数据库，<br>支持群集计算、支持多国语言。<br>
驰骋工作流引擎按<br>照国际标准开发、并且弱化了<br>专业难懂的概念，<br>面向业务人员推出的<br>符合中国国情了工作流程引擎。<br>
即使不懂程序开发，<br>只要了解单位业务，就可以<br>设计工作流程引擎。所有的设计都<br>是可视化的，所见既所得。<br>
驰骋工作流程引擎<br>广泛应用于政务、集团企业、<br>生产企业、软件企业、国土、信访、<br>广告行业。经过时<br>间的检验稳定、成熟。<br>
 
</fieldset>
    <%-- <ul>
     <li> 成功案例</li>
     </ul>--%>
</td>
</tr>

<!-- 信息发布部分 -->
<tr>
<td colspan=2>
    
<uc1:InfoTabs ID="InfoTabs1" runat="server" />

</td>
</tr>

<!-- 流程模板与下载部分 -->
<td colspan=2>
<table>
<tr>
<td width='70%' ><div>
<object id="ssss" width="480" height="370" ><param name="allowScriptAccess" value="always" />
<embed pluginspage="http://www.macromedia.com/go/getflashplayer" 
src="http://you.video.sina.com.cn/api/sinawebApi/outplayrefer.php/vid=40624234_1618288504/s.swf"
 type="application/x-shockwave-flash" name="ssss" allowFullScreen="true" allowScriptAccess="always" width="480" height="370">
 </embed></object>
 </div>
</td>
<td  valign=top>

<uc4:ImgLink ID="ImgLink1" runat="server" />
 
</td>
</tr>
</table>

<%--
    --%>
</td>
</tr>

</table>
</asp:Content>

