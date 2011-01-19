<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleSmallTypeEdit.aspx.cs" Inherits="Tax666.AppWeb.Manager.AticleManage.ArticleSmallTypeEdit" %>
<%@ Import Namespace="Tax666.AppWeb" %>
<%@ Register assembly="Tax666.WebControls" namespace="Tax666.WebControls" tagprefix="Tax666WebControls" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
      <%=Global.GetHeadInfo()%><script language="javascript">
    function inputcheckvalid(){
        var charBag = "[^`~!@#$%^&/\',|.*]";
        var typename = document.getElementById("txtsmalltypename");
        var typedesc = document.getElementById("txttypedesc");
        
        if(trim(typename.value) == "" || typename.value.length > 20){
            alert("文章小类名称不能为空或长度不能超过20个字符！");
           typename.focus();
           return false;
        }
        if (trim(typename.value) != ""){  
            for (var i = 0; i < typename.value.length; i++) {
                var c = typename.value.charAt(i);
                if (charBag.indexOf(c) > -1) {
                   alert("文章小类名称中含有非法字符(" + c +")！");
                   typename.focus();
                   return false;
                }
            }
        }
        
        if(typedesc.value.length > 200){
            alert("文章小类描述长度不能超过200个字符！");
           typedesc.focus();
           return false;
        }
        if (trim(typedesc.value) != ""){  
            for (var i = 0; i < typedesc.value.length; i++) {
                var c = typedesc.value.charAt(i);
                if (charBag.indexOf(c) > -1) {
                   alert("文章小类描述中含有非法字符(" + c +")！");
                   typedesc.focus();
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
            <Tax666WebControls:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadOPTxt="添加/修改文章小类" HeadTitleTxt="添加/修改文章小类">
                <Tax666WebControls:HeadMenuButtonItem  ButtonUrlType="Href" ButtonVisible="false" />
            </Tax666WebControls:HeadMenuWebControls>
            <Tax666WebControls:TabOptionWebControls ID="TabOptionWebControls1" runat="server">
                <Tax666WebControls:TabOptionItem id="TabOptionItem1" runat="server" Tab_Name="添加/修改文章小类">
                    <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
                        <tr>
		                    <td class="table_tdtxt" colspan="2" style="text-align:left;">&nbsp;<b>操作说明及操作结果提示：</b><br />
		                        <div style="padding-left:32px;color:#606060;">
                                    ㈠、以下“*”项目均为必填项目；<br />
                                    ㈡、如果使某类文章类别无效，则该类别下所有的相关记录均为无效，但此时并不删除记录；可以使用设置有效来恢复。
                                </div>
		                    </td>
		                </tr>
                        <tr>
                            <td class="table_tdtxt"><font color="red">*</font> 文章小类名称(长度2-20个字符)</td>
                            <td class="table_tdinput"><input type="text" id="txtsmalltypename" class="txtinput" style="width:210px;" onfocus="this.select();" runat="server" maxlength="20" />
                                <span style="padding:0 10px 0 20px;">所属文章大类</span><asp:DropDownList ID="dplBigType" runat="server" Width="108px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="table_tdtxt">小类描述<br />长度限制在200字符以内</td>
                            <td class="table_tdinput"><textarea id="txttypedesc" class="txtinput" style="width:420px;height:64px;" onfocus="this.select();" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="table_tdtxt">该小类是否有效</td>
                            <td class="table_tdinput">
                                <asp:radiobutton id="radValid" runat="server" Text="有效类别" GroupName="ValidTab" Checked="True"></asp:radiobutton>
		                        <span style="padding-left:36px;"><asp:radiobutton id="radInvalid" runat="server" Text="无效类别" GroupName="ValidTab"></asp:radiobutton></span></td>
                        </tr>
                        <tr id="SubmitTr" runat="server"><td colspan="2" align="right" class="table_tdbtn">
                            <asp:button id="btnOK" CausesValidation="False" runat="server" CssClass="btnbig" Text="确  定" OnClick="btnOK_Click"></asp:button>
                            <asp:button id="btnNew" CausesValidation="False" runat="server" CssClass="btnbig" Text="添加新小类" OnClick="btnNew_Click"></asp:button>
                            <asp:Button id="btnCancel" runat="server" CssClass="btnbig" Text="文章小类管理" CausesValidation="False" OnClick="btnCancel_Click"></asp:Button></td>
                        </tr>                    
                    </table>
                </Tax666WebControls:TabOptionItem>
            </Tax666WebControls:TabOptionWebControls>
        </div>
    </form>
</body>
</html>