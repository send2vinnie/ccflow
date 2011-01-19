<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomeLinkEdit.aspx.cs" Inherits="Tax666.AppWeb.Manager.AticleManage.HomeLinkEdit" %>
<%@ Import Namespace="Tax666.AppWeb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register assembly="Tax666.WebControls" namespace="Tax666.WebControls" tagprefix="Tax666WebControls" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <%=Global.GetHeadInfo()%>
    <script language="javascript">
    var Source = "gif,jpg,png";
    function checkStr(str)
    {
	    if(Source == "" || Source == null)
		    return true;
	    else
	    {
		    var ss = Source.split(",");
		    var allow = false;
		    for(var i = 0; i < ss.length; i++)
		    {
			    if(str == ss[i]){allow = true;}
		    }
		    return allow;
	    }
    }

    //验证是否符合指定的上传格式
    function checkData(){
	    if (document.forms[0].logoPath.value != ""){
		    var str = document.forms[0].logoPath.value;
		    var logoobj = document.getElementById("logoPath");
		    if(str != null || str != ""){
			    var extName = str.split(".")
			    var ext = extName[extName.length-1];
			    ext = ext.toLowerCase();
			    if(!checkStr(ext)){
				    alert("提示： "+ext+" 格式的文件不能上传！\t\t\t\n\n只允许以下格式的文件：\n\n\t"+Source);
				    logoobj.outerHTML=logoobj.outerHTML.replace(/value=\w/g,'');
				    return false;
			    }
		    }
	    }else return true;
    }
    </script>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <div class="main">
            <Tax666WebControls:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadOPTxt="添加网站友情链接或对其属性进行修改" HeadTitleTxt="添加/修改网站友情链接记录" HeadHelpTxt="Logo大小控制在180×60">
                <Tax666WebControls:HeadMenuButtonItem ButtonUrlType="Href" ButtonVisible="false" />
            </Tax666WebControls:HeadMenuWebControls>
            <Tax666WebControls:TabOptionWebControls ID="TabOptionWebControls1" runat="server">
                <Tax666WebControls:TabOptionItem id="TabOptionItem1" runat="server" Tab_Name="添加/修改网站友情链接">
                
                    <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
                        <tr>
		                    <td class="table_tdtxt" colspan="2" style="text-align:left;">&nbsp;<b>操作说明及操作结果提示：</b><br />
		                        <div style="padding-left:32px;color:#606060;">
                                    ㈠、请正确的填写网站的URL链接地址的格式，如：http://www.521189.com；<br />
                                    ㈡、如果您的网站有Logo，请上传到本站，大小控制在 88×31，否则系统将对图片进行缩放，导致图片的质量下降。
                                </div>
		                    </td>
		                </tr>
                        <tr>
                            <td class="table_tdtxt"><font color="red">*</font> 友情网站名称(长度20字内)</td>
                            <td class="table_tdinput"><asp:textbox id="txtName" MaxLength="20" Width="200px" CssClass="txtinput" onfocus="this.select();" runat="server"></asp:textbox>
                                <span style="padding:0 10px 0 20px;">网站类型</span><asp:dropdownlist id="dplHomeType" Width="120px" runat="server"></asp:dropdownlist>&nbsp;
                                <asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtName" Font-Size="9pt" Font-Names="Tahoma">请输入友情网站名！</asp:requiredfieldvalidator></td>
                        </tr>
                        <tr>
                            <td class="table_tdtxt"><font color="red">*</font> 网站的URL链接地址</td>
                            <td class="table_tdinput"><asp:textbox id="txtUrl" MaxLength="100" Width="400px" CssClass="txtinput" runat="server"></asp:textbox>&nbsp;
                                <asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txtUrl" Font-Size="9pt" Font-Names="Tahoma">请输入URL链接地址！</asp:requiredfieldvalidator>
                                <asp:regularexpressionvalidator id="RegularExpressionValidator7" runat="server" ValidationExpression="(([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?)|(http://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?)" Display="Dynamic" ControlToValidate="txtUrl" Font-Size="9pt" Font-Names="Tahoma">URL地址输入错误！</asp:regularexpressionvalidator></td>
                        </tr>
                        <tr id="trUpFile" runat="server">
                            <td class="table_tdtxt">网站图标LOGO<br />LOGO图片Ｗ×Ｈ在 180×60 内</td>
                            <td class="table_tdinput">
                                <input class="flatTextBox" id="logoPath" style="width:346px;height:24px;" type="file" name="logoPath" onpropertychange="return checkData();" />&nbsp;<input class="flatbtn" onclick="logoPath.outerHTML=logoPath.outerHTML.replace(/value=\w/g,'')" type="button" value="清除" /><br />
			                    <div style="color:#808080;">(支持上传文件类型：*.gif、*.jpg、*.jpeg、*.bmp，文件大小控制在2MB以内)</div></td>
                        </tr>
                        <tr id="trResPath" runat="server">
                            <td class="table_tdtxt">网站Logo文件名称</td>
                            <td class="table_tdinput"><asp:label id="lblResPath" runat="server" CssClass="tdStyle2"></asp:label>&nbsp;&nbsp;
			                <asp:CheckBox id="chkChanageRes" runat="server" AutoPostBack="True" OnCheckedChanged="chkChanageRes_CheckedChanged"></asp:CheckBox></td>
                        </tr>
                        <tr>
                            <td class="table_tdtxt">友情链接的类型</td>
                            <td class="table_tdinput"><asp:RadioButton id="radLink1" runat="server" Text="显示文字链接" GroupName="links" Checked="True"></asp:RadioButton>&nbsp;&nbsp;
			                    <asp:RadioButton id="radLink2" runat="server" CssClass="tdStyle2" Text="显示LOGO图标链接" GroupName="links"></asp:RadioButton></td>
                        </tr>
                        <tr>
                            <td class="table_tdtxt">您的网站简介<br />请控制在500字符以内。</td>
                            <td class="table_tdinput"><div style="padding:6px 0;"><asp:textbox id="txtDesc" MaxLength="500" Width="400px" CssClass="txtinput" runat="server" TextMode="MultiLine" Height="64px"></asp:textbox>&nbsp;
                                <asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="txtName" Font-Size="9pt" Font-Names="Tahoma">请输入简介！</asp:requiredfieldvalidator></div></td>
                        </tr>
                        <tr id="SubmitTr" runat="server"><td colspan="2" align="right" class="table_tdbtn">
                            <asp:button id="btnOK" runat="server" CssClass="btnbig" Text="确  定" OnClick="btnOK_Click"></asp:button>
                            <asp:Button id="btnCancel" runat="server" CssClass="btnbig" Text="列表管理" CausesValidation="False" OnClick="btnCancel_Click"></asp:Button></td>
                        </tr>
                    </table>
                </Tax666WebControls:TabOptionItem>
            </Tax666WebControls:TabOptionWebControls>
        </div>
    </form>
</body>
</html>