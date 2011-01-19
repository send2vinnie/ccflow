<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleEdit.aspx.cs" Inherits="Tax666.AppWeb.Manager.AticleManage.ArticleEdit" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<%@ Import Namespace="Tax666.AppWeb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register assembly="Tax666.WebControls" namespace="Tax666.WebControls" tagprefix="Tax666WebControls" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <%=Global.GetHeadInfo()%>
    <link rel="stylesheet" type="text/css" href="<%=Global.WebPath%>/Manager/JScript/FineMessBox/css/subModal.css" />
    <script type="text/javascript" src="<%=Global.WebPath%>/Manager/JScript/FineMessBox/js/common.js"></script>
    <script type="text/javascript" src="<%=Global.WebPath%>/Manager/JScript/FineMessBox/js/subModal.js"></script>
    <script language="javascript" type="text/javascript">
        var Source = "gif,jpg,png,bmp";

	    function checkStr(str){
		    if(Source == "" || Source == null)
			    return true;
		    else{
			    var ss = Source.split(",");
			    var allow = false;
			    for(var i = 0; i < ss.length; i++){
				    if(str == ss[i]){allow = true;}
			    }
			    return allow;
		    }
	    }
    	
	    //验证是否符合指定的上传格式
	    function checkData()
	    {
		    if (document.forms[0].filepath != null){
			    var str = document.forms[0].filepath.value;

			    var extName = str.split(".")
			    var ext = extName[extName.length-1];
			    ext = ext.toLowerCase();
			    if(!checkStr(ext))
			    {
				    //alert("提示： "+ext+" 格式的文件不能上传！\t\t\t\n\n只允许以下格式的文件：\n\n\t"+Source);
				    document.forms[0].filepath.outerHTML = document.forms[0].filepath.outerHTML.replace(/value=\w/g,'');
				    return false;
			    }
		    }
	    }

        //设置文章的基本属性信息；
	    function setAuthor(str){
		    var obj = document.getElementById("Author");
		    obj.value = str;
	    }
    	
	    function SetDefaultValue(){
		    var chkobj = document.getElementById("chkDefault");
		    var emailobj = document.getElementById("txtEmail");
		    var sourceobj = document.getElementById("txtSourse");
		    var homeobj = document.getElementById("txtHomepage");
		    var authorobj = document.getElementById("Author");
    		
		    if (chkobj.checked){
			    emailobj.value = "qifl23702570@163.com";
			    sourceobj.value = "521联合供货网";
			    homeobj.value = "http://www.521189.com";
			    authorobj.value = "521联合供货网";
		    }else{
			    emailobj.value = "";
			    sourceobj.value = "";
			    homeobj.value = "";
			    authorobj.value = "";
		    }
	    }
    	
	    function inputcheck(){
	        var charBag = "[^`~@#$%^&/\'|*]";
            var patternmail = /^\w+((-\w+)|(\.\w+))*\@\w+((\.|-)\w+)*\.\w+$/;
            
	        var titleobj = document.getElementById("txtTitle");
		    var editobj = document.getElementById("Editor1");
		    var emailobj = document.getElementById("txtEmail");
		    var urlobj = document.getElementById("txtHomepage");
    	    
	        if (trim(titleobj.value) == "" || titleobj.value.length > 80 || titleobj.value.length < 2){
	            alert("文章标题不能为空，其长度为2～80字符！");
		        //titleobj.focus();
		        return false;
	        }else{  
                for (var i = 0; i < titleobj.value.length; i++) {
                    var c = titleobj.value.charAt(i);
                    if (charBag.indexOf(c) > -1) {
                       alert("文章标题中含有非法字符(" + c +")！");
                       //titleobj.focus();
                       return false;
                    }
                }
            }
            
		    if (editobj.value.length > 20000){
			    alert("文章正文内容长度为："+ editobj.value.length + "字符，已经超过" + (editobj.value.length - 20000) + "个字符！\n\n请更改内容后再重试。");
			    return false;
		    }
		    if (trim(editobj.value).length < 10){
			    alert("文章正文内容长度不能少于10个字符！");
			    return false;
		    }
    	    
    	    if (trim(emailobj.value).length > 0){
                if (!patternmail.test(emailobj.value)) {
                    alert("请输入合法的电子邮件地址！");
                    //emailobj.focus();
                    //emailobj.select();
                    return false;
                }
            }
           
	    }
	    
	    function ShowNewsImages(path)
        {
            showPopWin('浏览主题图片','ShowNewsPic.aspx?imgpath='+path, 240, 200, "",true,false)
        }
        
        function AlertMessageBox(Messages) {
            DispClose = false; 
            window.location.href = location.href;
            alert(Messages);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <div class="main">
           <Tax666WebControls:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadOPTxt="添加资讯文章或对文章的属性进行修改操作" HeadTitleTxt="添加/修改文章记录" HeadHelpTxt="①可上传图片格式：gif,jpg,png,bmp②大小限制在2MB内">
                <Tax666WebControls:HeadMenuButtonItem ButtonUrlType="Href" ButtonVisible="false" />
            </Tax666WebControls:HeadMenuWebControls>
            <Tax666WebControls:TabOptionWebControls ID="TabOptionWebControls1" runat="server">
                <Tax666WebControls:TabOptionItem id="TabOptionItem1" runat="server" Tab_Name="添加/修改文章内容">
                    <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
		                <tr>
                            <td class="table_tdtxt"><font color="red">*</font>文章标题(长度80字内)</td>
                            <td class="table_tdinput"><input type="text" id="txtTitle" maxlength="80" class="txtinput" style="width:304px;" onfocus="this.select();" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="table_tdtxt">文章所属栏目</td>
                            <td class="table_tdinput"><asp:dropdownlist id="dplModuleType" runat="server" Width="304px"></asp:dropdownlist></td>
                        </tr>
                        <tr>
                            <td class="table_tdinput" align="center" colspan="2">
                                <div style="width:98%;padding:5px 0 0 0;text-align:center;color:#666;"><b><font color="red">*</font>文 章 正 文 内 容</b>(长度限制在10000个字符以内)</div>
                                <div style="width:98%;padding:0 0 5px 0;text-align:left;">
                                    <CE:Editor ID="Editor1" runat="server" ThemeType="Office2003_BlueTheme" Width="100%" EditorWysiwygModeCss="~/CuteSoft_Client/example.css" FilesPath="~/CuteSoft_Client/CuteEditor/" Height="280px" AutoConfigure="Compact" SecurityPolicyFile="Admin.config">
                                        <FrameStyle BackColor="White" BorderColor="#DDDDDD" BorderStyle="Solid" BorderWidth="1px" CssClass="CuteEditorFrame" Height="100%" Width="100%" />
                                    </CE:Editor>
                                </div>
                            </td>
                        </tr>
                    </table>
                </Tax666WebControls:TabOptionItem>
                <Tax666WebControls:TabOptionItem id="TabOptionItem2" runat="server" Tab_Name="资讯文章属性编辑">
                    <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
                        <tr>
                            <td class="table_tdtxt"><font color="red">*</font>文章SEO关键字</td>
                            <td class="table_tdinput"><input type="text" id="txtSEOKey" maxlength="120" class="txtinput" style="width:304px;" onfocus="this.select();" runat="server" onblur="javascript:this.value=this.value.replace(/，/ig,',');" /><span style="padding-left:8px;color:#258DC9;font-family:宋体;">(多个关键字之间用半角逗号“,”隔开)</span></td>
                        </tr>
                        <tr id="trUpFile" runat="server">
                            <td class="table_tdtxt">文章主题图片 - 上传图片</td>
                            <td class="table_tdinput">
                                <input class="flatTextBox" id="filepath" contentEditable="false" style="width: 462px;height:26px;" onpropertychange="checkData();"	type="file" name="filepath" />
                                <span style="padding-left:4px;"><input class="flatbtn" onclick="filepath.outerHTML=filepath.outerHTML.replace(/value=\w/g,'')" type="button" value="清除" /></span></td>
                        </tr>
                        <tr id="trResPath" runat="server">
                            <td class="table_tdtxt">文章主题图片 - 图片路径</td>
                            <td class="table_tdinput"><a href="javascript:void(0);" onclick="javascript:ShowNewsImages('<%=m_ImagePath %>');" style="cursor:pointer;">浏览文章主题图片</a>
                                <span style="padding-left:30px;"><asp:checkbox id="chkChanageRes" runat="server" CssClass="tdStyle1" Text="更换资讯图片" AutoPostBack="True" OnCheckedChanged="chkChanageRes_CheckedChanged"></asp:checkbox></span></td>
                        </tr>
                        <tr>
                            <td class="table_tdtxt">文章作者<span style="padding-left:10px;"><input id="chkDefault" onclick="javascript:SetDefaultValue();" type="checkbox" name="chkDefault"><label for="chkDefault">使用默认值</label></span></td>
                            <td class="table_tdinput">
                                <input class="txtinput" id="Author" style="width: 120px" onfocus="javascript:this.select();" type="text" maxLength="40" name="Author" runat="server" />&nbsp;
                                <select id="AuthorList" style="width:108px" onchange="javascript:setAuthor(this.value);">
				                    <option value="" selected>← (清除)</option>
				                    <option value="521联合供货网">521联合供货网</option>
				                    <option value="频道管理员">频道管理员</option>
				                    <option value="521供货网代理商">521供货网代理商</option>
				                    <option value="相关媒体转载">相关媒体转载</option>
				                    <option value="佚名">佚名</option>
			                    </select><span style="padding:0 4px 0 10px;">作者Email</span><asp:textbox id="txtEmail" runat="server" MaxLength="60" CssClass="txtinput" Width="216px" onfocus="this.select();"></asp:textbox></td>
                        </tr>
                        <tr>
                            <td class="table_tdtxt">资讯文章来源或出处</td>
                            <td class="table_tdinput">
                                <asp:textbox id="txtSourse" runat="server" MaxLength="100" CssClass="txtinput" Width="236px" onfocus="this.select();"></asp:textbox>
                                <span style="padding:0 4px 0 33px;">网址</span><asp:textbox id="txtHomepage" runat="server" MaxLength="60" CssClass="txtinput" Width="216px" onfocus="this.select();"></asp:textbox></td>
                        </tr>
                        <tr>
                            <td class="table_tdtxt">文章属性设置</td>
                            <td class="table_tdinput">
                                <asp:checkbox id="chkValid" runat="server" Text="有效文章" Checked="True"></asp:checkbox>
                                <span style="padding:0 40px;"><asp:checkbox id="chkTop" runat="server" Text="文章列表固顶" Font-Names="Tahoma" Font-Size="9pt" AutoPostBack="False"></asp:checkbox></span>
                                <asp:checkbox id="chkIsCommend" runat="server" Text="推荐该文章" Checked="false"></asp:checkbox></td>
                        </tr>
                    </table>
                  </Tax666WebControls:TabOptionItem>
            </Tax666WebControls:TabOptionWebControls>
            <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
                <tr id="SubmitTr" runat="server"><td align="right" class="table_tdbtn">
                    <asp:button id="btnOK" CausesValidation="False" runat="server" CssClass="btnbig" Text="确  定" OnClick="btnOK_Click"></asp:button>
                    <asp:button id="btnNew" CausesValidation="False" runat="server" CssClass="btnbig" Text="添加新文章" OnClick="btnNew_Click"></asp:button>
                    <asp:Button id="btnCancel" runat="server" CssClass="btnbig" Text="文章列表管理" CausesValidation="False" OnClick="btnCancel_Click"></asp:Button></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

