<%@ Page Title="" Language="C#" MasterPageFile="~/Comm/RefFunc/MasterPage.master" AutoEventWireup="true" CodeFile="UIEn.aspx.cs" Inherits="Comm_RefFunc_UIEn" %>
<%@ Register src="Left.ascx" tagname="Left" tagprefix="uc1" %>
<%@ Register src="../UC/UIEn.ascx" tagname="UIEn" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
		<base target=_self />      

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Left ID="Left1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <uc2:UIEn ID="UIEn1" runat="server" />

    <script type="text/javascript">
            function btnClientClick() {
                
               var designerWindow = window.opener.document;
                var slPlugin = designerWindow.getElementById('silverlightControlHost').childNodes[0];

                if("BP.WF.Ext.NodeO" == "<%= Request["EnName"] %>")
                {
                    var paras = "<%= Request["PK"] %>," + window.document.getElementById("ContentPlaceHolder2_UIEn1_UCEn1_TB_Name").value;
                    slPlugin.content.Designer.OnAspNetPageClosed("FlowNodeProperty", paras);
                }

                if("BP.WF.Ext.FlowSheet" == "<%= Request["EnName"] %>")
                {
                    var paras = "<%= Request["PK"] %>," + window.document.getElementById("ContentPlaceHolder2_UIEn1_UCEn1_TB_Name").value;
                    slPlugin.content.Designer.OnAspNetPageClosed("FlowProperty", paras);

                }        

            };

            var btn = document.getElementById("ContentPlaceHolder2_UIEn1_ToolBar1_Btn_Save");
            if(window.addEventListener){
                btn.addEventListener('click', btnClientClick, false);
           } 
           else {
                btn.attachEvent('onclick',  btnClientClick);
            }           
           
        </script>
</asp:Content>


