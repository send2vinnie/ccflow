<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleManage.aspx.cs" Inherits="Tax666.AppWeb.Manager.AticleManage.ArticleManage" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="Tax666.AppWeb" %>
<%@ Register assembly="Tax666.WebControls" namespace="Tax666.WebControls" tagprefix="Tax666WebControls" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <%=Global.GetHeadInfo()%>
    <script type="text/javascript" language="javascript">
    var charBag = "[^`~!@#$%^&/\',|.*]";
    function checktextinput(){
        var txtkey = document.getElementById("txtKey");
        
        if (trim(txtkey.value) == "" || txtkey.value.length > 20 || txtkey.value.length < 2){
	        alert("搜索字符串应该为2～20字符的长度！");
		    txtkey.focus();
		    return false;
	    }else{  
            for (var i = 0; i < txtkey.value.length; i++) {
                var c = txtkey.value.charAt(i);
                if (charBag.indexOf(c) > -1) {
                   alert("搜索字符串中含有非法字符(" + c +")！");
                   txtkey.focus();
                   return false;
                }
            }
        }
    }
     </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main">
            <Tax666WebControls:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadOPTxt="对资讯文章进行添加、修改、浏览、审核和删除等操作" HeadTitleTxt="资讯文章内容管理" HeadHelpTxt="点击文章标题可对该记录属性进行编辑修改">
                <Tax666WebControls:HeadMenuButtonItem ButtonUrlType="Href" ButtonVisible="false" />
            </Tax666WebControls:HeadMenuWebControls>
            <Tax666WebControls:TabOptionWebControls ID="TabOptionWebControls1" runat="server">
                <Tax666WebControls:TabOptionItem id="TabOptionItem1" runat="server" Tab_Name="资讯文章信息列表">
                    <table class="tableBorder1" cellSpacing="1" cellPadding="0" align="center">
	                    <tr>
		                    <td class="TableBody2" noWrap align="left" style="height:36px;padding:0 15px;">
			                    <table cellSpacing="0" cellPadding="1" width="100%" border="0">
				                    <tr>
					                    <td align="left">栏目
						                    <asp:dropdownlist id="dplType" runat="server" AutoPostBack="True" CssClass="select1" Width="228px" OnSelectedIndexChanged="dplType_SelectedIndexChanged"></asp:dropdownlist>&nbsp;&nbsp;有效
						                    <asp:dropdownlist id="dplvalid" runat="server" Width="80px" CssClass="select1" AutoPostBack="True" OnSelectedIndexChanged="dplType_SelectedIndexChanged">
						                        <asp:ListItem Value="2">不限</asp:ListItem>
							                    <asp:ListItem Value="1">有效文章</asp:ListItem>
							                    <asp:ListItem Value="0">无效文章</asp:ListItem>
						                    </asp:dropdownlist>&nbsp;&nbsp;审核
						                    <asp:dropdownlist id="dplAudit" runat="server" Width="108px" CssClass="select1" AutoPostBack="True" OnSelectedIndexChanged="dplType_SelectedIndexChanged">
						                        <asp:ListItem Value="2">全部文章</asp:ListItem>
							                    <asp:ListItem Value="0">审核未通过文章</asp:ListItem>
							                    <asp:ListItem Value="1">审核通过文章</asp:ListItem>
						                    </asp:dropdownlist>
					                    </td>
				                    </tr>
			                    </table>
		                    </td>
	                    </tr>
	                    <tr>
		                    <td class="TableBody2" noWrap align="left" style="height:40px;padding:0 15px;">
			                    <table cellSpacing="0" cellPadding="1" width="100%" border="0">
				                    <tr>
					                    <td align="left" width="160" style="padding-top:4px;"><asp:button id="btnAdd" runat="server" Text="添加新文章" CssClass="btnbig" OnClick="btnAdd_Click"></asp:button></td>
					                    <td align="right">文章标题或内容关键字：<input type="text" id="txtKey" class="txtinput" style="width:176px;" onfocus="this.select();" runat="server" maxlength="30" /></td>
					                    <td align="right" width="72" style="padding-top:4px;">&nbsp;<asp:Button ID="btnSearch" runat="server" Text="搜 索" CssClass="btnnormal" CausesValidation="False" OnClick="dplType_SelectedIndexChanged" /></td>
					                </tr>
					            </table>
					        </td>
					    </tr>
                    </table>                
                    <table class="tableBorder1" cellSpacing="1" cellPadding="0" align="center">
	                    <tr>
		                    <th width="36">选择</th>
		                    <th width="*">文章标题</th>
		                    <th width="72">所属大类</th>
		                    <th width="88">所属小类</th>
		                    <th width="64">添加日期</th>
		                    <th width="48">图片?</th>
		                    <th width="48">有效</th>
                            <th width="48">审核</th>
		                    <th width="48">置顶</th>
                            <th width="48">推荐</th>
	                    </tr>
                    </table>               
                    <table class="tableBorder4" id="tabNoRec" cellSpacing="1" cellPadding="0" align="center" runat="server" style="height:32px;">
	                    <tr>
		                    <td class="TableBody1">&nbsp;&nbsp;对不起，该类别下没有满足条件的记录列表。</td>
	                    </tr>
                    </table>
                     <asp:datagrid id="listRecord" runat="server" CssClass="tableBorder1" Width="100%" GridLines="None" AutoGenerateColumns="False" CellPadding="0" HorizontalAlign="Center" ShowHeader="False" CellSpacing="1" ItemStyle-Height="32px">
	                    <Columns>
	                        <asp:TemplateColumn HeaderText="操作">
			                    <ItemStyle HorizontalAlign="Center" Width="36px" CssClass="TableBody1"></ItemStyle>
			                    <ItemTemplate>
				                    <asp:CheckBox id="chk" runat="server"></asp:CheckBox>				
			                    </ItemTemplate>
		                    </asp:TemplateColumn>
		                    <asp:TemplateColumn HeaderText="文章标题">
			                    <ItemStyle CssClass="TableBody1" HorizontalAlign="Left"></ItemStyle>
			                    <ItemTemplate>
		                            <a href='<%=Global.WebPath%>/Manager/AticleManage/ArticleEdit.aspx?articleid=<%#DataBinder.Eval(Container.DataItem,"ArticleID")%>' class="a1" title='<%# DataBinder.Eval(Container.DataItem, "Title")%>'>
			                            <%# Tax666.Common.StringUtil.ControlStrLength(DataBinder.Eval(Container.DataItem, "Title").ToString(), 42)%>
		                            </a>
	                            </ItemTemplate>
		                    </asp:TemplateColumn>
		                    <asp:BoundColumn DataField="BigTypeName" HeaderText="所属栏目">
			                    <ItemStyle Wrap="False" Width="72px" CssClass="TableBody1" HorizontalAlign="Center" Font-Size="9pt"></ItemStyle>
		                    </asp:BoundColumn>
		                    <asp:BoundColumn DataField="SmallTypeName" HeaderText="所属栏目">
			                    <ItemStyle Wrap="False" Width="88px" CssClass="TableBody1" HorizontalAlign="Center" Font-Size="9pt"></ItemStyle>
		                    </asp:BoundColumn>
		                    <asp:BoundColumn DataField="CreateTime" HeaderText="添加日期" DataFormatString="{0:yyyy-MM-dd}">
			                    <ItemStyle HorizontalAlign="Right" Width="64px" CssClass="TableBody1" Font-Size="8pt" Font-Names="Tahoma"></ItemStyle>
		                    </asp:BoundColumn>
		                    <asp:TemplateColumn HeaderText="图片?">
			                    <ItemStyle HorizontalAlign="Center" Width="48px" CssClass="TableBody2"></ItemStyle>
			                    <ItemTemplate>
				                    <%#(DataBinder.Eval(Container.DataItem, "ArticlePicPath").ToString().Trim() == "") ? "<b><font color=red>×</font></b>" : "<b>√</b>"%>
			                    </ItemTemplate>
		                    </asp:TemplateColumn>
		                    <asp:TemplateColumn HeaderText="有效">
			                    <ItemStyle HorizontalAlign="Center" Width="48px" CssClass="TableBody1"></ItemStyle>
			                    <ItemTemplate>
				                    <%#((bool)DataBinder.Eval(Container.DataItem, "IsAvailable")) ? "<b>√</b>" : "<b><font color=red>×</font></b>"%>
			                    </ItemTemplate>
		                    </asp:TemplateColumn>
		                    <asp:TemplateColumn HeaderText="审核">
			                    <ItemStyle HorizontalAlign="Center" Width="48px" CssClass="TableBody1"></ItemStyle>
			                    <ItemTemplate>
				                    <%#((bool)DataBinder.Eval(Container.DataItem, "IsAudit")) ? "<b>√</b>" : "<b><font color=red>×</font></b>"%>
			                    </ItemTemplate>
		                    </asp:TemplateColumn>
		                    <asp:TemplateColumn HeaderText="置顶">
			                    <ItemStyle HorizontalAlign="Center" Width="48px" CssClass="TableBody2"></ItemStyle>
			                    <ItemTemplate>
				                    <%#((bool)DataBinder.Eval(Container.DataItem, "IsTop")) ? "<b>√</b>" : "<b><font color=red>×</font></b>"%>
			                    </ItemTemplate>
		                    </asp:TemplateColumn>
		                    <asp:TemplateColumn HeaderText="推荐">
			                    <ItemStyle HorizontalAlign="Center" Width="48px" CssClass="TableBody2"></ItemStyle>
			                    <ItemTemplate>
				                    <%#((bool)DataBinder.Eval(Container.DataItem, "IsCommend")) ? "<b>√</b>" : "<b><font color=red>×</font></b>"%>
			                    </ItemTemplate>
		                    </asp:TemplateColumn>
	                    </Columns>
                    </asp:datagrid>
                    <table class="tableBorder4" cellSpacing="0" cellPadding="0" align="center">
	                    <tr>
		                    <td class="TableBody1" vAlign="middle" align="left" style="height:40px;">&nbsp;<input id="CheckAll" name="CheckAll" type="checkbox" onclick="ChooseAll()"/><label for="CheckAll">选择当前页面所有记录</label>&nbsp;&nbsp;
		                        <asp:button id="btnDel" runat="server" Text="设置有效/无效" CssClass="btnbig" OnClick="btnDel_Click"></asp:button>&nbsp;
			                    <asp:button id="btnAudit" runat="server" Text="审核通过/取消" CssClass="btnbig" OnClick="btnAudit_Click"></asp:button>&nbsp;
			                    <asp:button id="btnTop" runat="server" Text="置顶/取消置顶" CssClass="btnbig" OnClick="btnTop_Click"></asp:button>&nbsp;
			                    <asp:button id="btnCommend" runat="server" Text="推荐/取消推荐" CssClass="btnbig" OnClick="btnCommend_Click"></asp:button>&nbsp;
			                    <asp:button id="btnDelete" runat="server" Text="删除文章" CssClass="btnbig" OnClick="btnDelete_Click"></asp:button></td>
		                </tr>
		            </table>
		            <table class="tableBorder4" cellSpacing="0" cellPadding="0" align="center" style="height:36px;">
	                    <tr>
		                    <td class="TableBody1" noWrap align="right" width="65%" height="32">将选中的文章转移到栏目：
			                    <asp:dropdownlist id="dplType1" runat="server" Width="228px"></asp:dropdownlist>
		                    </td>
		                    <td class="TableBody1" noWrap width="35%" align="left" style="padding-left:10px;"><asp:button id="btnMove" runat="server" Text="转移" CssClass="btnnormal" OnClick="btnMove_Click"></asp:button></td>
	                    </tr>
                    </table>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="font-size:12px;font-family:Tahoma;color:#666;" height="32">
                        <tr>
                            <td width="30%" align="left">总记录：<%=m_RecordCount%>&nbsp;&nbsp;每页：10&nbsp;&nbsp;当前页：<%=m_CurrentPageIndex%>/<%=m_PageCount%></td>
                            <td width="70%" align="right"><webdiyer:AspNetPager CssClass="pages" CurrentPageButtonClass="cpb" ID="pagesTrade" runat="server" AlwaysShow="True" FirstPageText="首页" Font-Size="12px" LastPageText="尾页" NextPageText="下一页" PageSize="10" PrevPageText="上一页" ShowBoxThreshold="1" PageIndexBoxClass="inputbox" ShowPageIndexBox="Always" SubmitButtonClass="btngo" TextAfterPageIndexBox="页" TextBeforePageIndexBox="第" NumericButtonCount="5" OnPageChanged="pagesTrade_PageChanged"></webdiyer:AspNetPager></td>
                        </tr>
                    </table>
                </Tax666WebControls:TabOptionItem>
            </Tax666WebControls:TabOptionWebControls>
        </div>
    </form>
</body>
</html>
