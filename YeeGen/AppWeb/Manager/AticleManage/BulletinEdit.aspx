<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BulletinEdit.aspx.cs" Inherits="Tax666.AppWeb.Manager.AticleManage.BulletinEdit" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<%@ Import Namespace="Tax666.AppWeb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register assembly="Tax666.WebControls" namespace="Tax666.WebControls" tagprefix="Tax666WebControls" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <%=Global.GetHeadInfo()%>
      <script language="javascript" type="text/javascript">
     function checkInput(){
	    var editobj = document.getElementById("Editor1");
	    var titleobj = document.getElementById("txtTitle");
	    var dateobj1 = document.getElementById("txtStartTime");
        var dateobj2 = document.getElementById("txtEndTime");
        if (titleobj.value.length == 0){
            alert("公告信息的标题不能为空！");
            titleobj.focus();
		    return false;
        }
	    if (editobj.value.length > 5000){
		    alert("您输入的公告信息内容长度为："+ editobj.value.length + "字符，已经超过" + (editobj.value.length - 5000) + "个字符！\n\n请更改公告内容的长度后再重试。");
		    return false;
	    }
	    if (editobj.value.length == 0){
		    alert("公告信息内容不能为空！");
		    return false;
	    }
    	
	    if(dateobj1.value == ""){
            alert("请输入公告信息的开始时间。");
            return false;
        }
        if(dateobj2.value == ""){
            alert("请输入公告信息的结束时间。");
            return false;
        }
    }
    
    //屏蔽窗口错误输出；不显示脚本错误信息
        window.onerror = function(){
            //return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main">
           <Tax666WebControls:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadOPTxt="对系统公告信息进行添加、属性修改操作" HeadTitleTxt="添加/修改系统公告信息" HeadHelpTxt="公告发布期限：系统默认有效期为60天">
                <Tax666WebControls:HeadMenuButtonItem ButtonUrlType="Href" ButtonVisible="false" />
            </Tax666WebControls:HeadMenuWebControls>
            <Tax666WebControls:TabOptionWebControls ID="TabOptionWebControls1" runat="server">
                <Tax666WebControls:TabOptionItem id="TabOptionItem1" runat="server" Tab_Name="添加/修改系统公告信息">
                    <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
		                <tr>
                            <td class="table_tdtxt"><font color="red">*</font> 公告标题(长度100字符内)</td>
                            <td class="table_tdinput"><input type="text" id="txtTitle" maxlength="100" class="txtinput" style="width:392px;" onfocus="this.select();" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="table_tdinput" colspan="2" align="center">
                                <div style="width:98%;padding:5px 0 0 0;text-align:center;color:#666;"><b>公 告 内 容</b>(字数控制在5000字符以内)</div>
                                <div style="width:98%;padding:0 0 5px 0;text-align:left;">
                                    <CE:Editor ID="Editor1" runat="server" AutoConfigure="Minimal" ThemeType="Office2003_BlueTheme" Height="220px" Width="100%" EditorWysiwygModeCss="~/CuteSoft_Client/example.css" FilesPath="~/CuteSoft_Client/CuteEditor/" SecurityPolicyFile="Admin.config">
                                        <FrameStyle BackColor="White" BorderColor="#DDDDDD" BorderStyle="Solid" BorderWidth="1px" CssClass="CuteEditorFrame" Height="100%" Width="100%" />
                                    </CE:Editor>
                                </div>
                            </td>
                        </tr>                        
                        <tr>
                            <td class="table_tdtxt">发布起止日期(默认为60天)</td>
                            <td class="table_tdinput">
                                开始日期：<input type="text" id="txtStartTime" name="txtStartTime" class="txtinput1" style="width:120px; font-family:Tahoma;" onfocus="javascript:HS_setDate(this);" contentEditable="false" title="请选择开始日期" runat="server" />
		                        <span style="padding-left:20px;">结束日期：<input type="text" id="txtEndTime" name="txtEndTime" class="txtinput1" style="width:120px; font-family:Tahoma;" onfocus="javascript:HS_setDate(this);" contentEditable="false" runat="server" title="请选择结束日期" /></span><span style="color:#666;padding-left:10px;">(默认有效期为60天)</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_tdtxt">发布范围及本系统公告审核</td>
                            <td class="table_tdinput">
                                <asp:RadioButton ID="radScoreMe" runat="server" Checked="True" GroupName="PublishScore" Text="本系统有效" />&nbsp;&nbsp;
                                <asp:RadioButton ID="radScoreAll" runat="server" GroupName="PublishScore" Text="所有系统有效（需审核）" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                公告审核：<asp:RadioButton ID="radPublish" runat="server" Checked="True" GroupName="publish" Text="立即发布" />&nbsp;&nbsp;
                                <asp:RadioButton ID="radNoPublish" runat="server" GroupName="publish" Text="暂不发布" />
                            </td>
                        </tr>
                        <tr id="SubmitTr" runat="server"><td colspan="2" align="right" class="table_tdbtn">
                            <asp:button id="btnOK" CausesValidation="False" runat="server" CssClass="btnbig" Text="确  定" OnClick="btnOK_Click"></asp:button>
                            <asp:Button id="btnCancel" runat="server" CssClass="btnbig" Text="公告管理" CausesValidation="False" OnClick="btnCancel_Click"></asp:Button></td>
                        </tr>
		            </table>
                </Tax666WebControls:TabOptionItem>
            </Tax666WebControls:TabOptionWebControls> 
        </div>
    </form>
</body>
</html>
