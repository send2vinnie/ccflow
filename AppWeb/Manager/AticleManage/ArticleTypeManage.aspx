<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleTypeManage.aspx.cs" Inherits="Tax666.AppWeb.Manager.AticleManage.ArticleTypeManage" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/UserControls/ArticleBigType.ascx" TagName="ArticleBigType" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/ArticleSmallType.ascx" TagName="ArticleSmallType" TagPrefix="uc2" %>
<%@ Import Namespace="Tax666.AppWeb" %>
<%@ Register assembly="Tax666.WebControls" namespace="Tax666.WebControls" tagprefix="Tax666WebControls" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <%=Global.GetHeadInfo()%>
</head>
<body>
     <form id="form1" runat="server">
       <div class="main">     
             <Tax666WebControls:HeadMenuWebControls ID="HeadMenuWebControls2" runat="server" HeadOPTxt="对文章资讯类别进行增删改查等操作" HeadTitleTxt="文章资讯类别管理" HeadHelpTxt="点击列表链接可对该记录属性进行编辑修改">
               <Tax666WebControls:HeadMenuButtonItem ButtonUrlType="Href" ButtonVisible="false" />
            </Tax666WebControls:HeadMenuWebControls>
             <Tax666WebControls:TabOptionWebControls ID="TabOptionWebControls1" runat="server">
                <Tax666WebControls:TabOptionItem id="TabOptionItem1" runat="server" Tab_Name="文章大类管理">
                    <uc1:ArticleBigType ID="ArticleBigType1" runat="server" />
                </Tax666WebControls:TabOptionItem>
                <Tax666WebControls:TabOptionItem id="TabOptionItem2" runat="server" Tab_Name="文章小类管理">
                    <uc2:ArticleSmallType ID="ArticleSmallType1" runat="server" />
                </Tax666WebControls:TabOptionItem>
            </Tax666WebControls:TabOptionWebControls>
       </div>
    </form>
</body>
</html>
