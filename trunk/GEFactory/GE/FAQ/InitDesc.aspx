<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InitDesc.aspx.cs" Inherits="FAQ_InitDesc" %>

<%@ Register Assembly="BP.GE" Namespace="BP.GE.Ctrl" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../UC/Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
    
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	   	<META   HTTP-EQUIV="pragma"   CONTENT="no-cache">   
         <META   HTTP-EQUIV="Cache-Control"   CONTENT="no-cache,   must-revalidate">   
         <META   HTTP-EQUIV="expires"   CONTENT="Wed,   26   Feb   1978   08:21:57   GMT">  
    <title></title>
        <link href="../Style/css/part.css"  rel="stylesheet" type="text/css" />
    <link href="/Edu/Style/css/import.css" rel="stylesheet" type="text/css" />
    <link href="/Edu/Style/css/layout.css" rel="stylesheet" type="text/css" />
    <link href="/Edu/Style/css/index.css" rel="stylesheet" type="text/css" />
   <link href="/Edu/Style/css/beike.css" rel="stylesheet" type="text/css" />
   <link href="/Edu/Style/css/sharefile.css" rel="stylesheet" type="text/css" />
    <link href="../Style/Table.css" rel="stylesheet" type="text/css" />
    <link href="../Style/port.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript" language="javascript">
     
        function DoCN(TEST) {
            if (window.confirm('您确定要采纳此回答吗？') == false)
                return;
            ss = TEST.split(",");
            window.showModalDialog('Do.aspx?RefOID=' + ss[0] + "&DoType=CaiNa&RefNo=" + ss[1] + "&rid=" + Math.random());
            window.location.href = 'OKDesc.aspx?RefOID=' + ss[0];
            return;
        }

        function DoEdit(oid) {
            window.showModalDialog('Edit.aspx?RefOID=' + oid + "&rid=" + Math.random(), '123', 'dialogHeight: 500px; dialogWidth: 700px;  center: yes; help: no');
            window.location.reload();
            return;
        }

        function DoDel(oid) {
            if (window.confirm('您确定要删除自己发布的请求吗？') == false)
                return;
            window.showModalDialog('Do.aspx?RefOID=' + oid + "&DoType=DelFAQ" + "&rid=" + Math.random());
            window.parent.location.reload();
            window.close();
            return;
        }

        /// 删除回复
        function DoDelReByAdmin(FUNCTION) {
            if (window.confirm('您确定要删除此回答吗？') == false)
                return;
            ss = FUNCTION.split(",");
            window.location.href = ('Delete.aspx?DelType=Dtl&RefOID=' + ss[1] + '&RefNo=' + ss[0]);
            return;
        }

        /// 删除问题
        function DoDelPostByAdmin(oid) {
            if (window.confirm('您确定要删除此提问吗？') == false)
                return;
            window.location.href=('Delete.aspx?DelType=Question&RefOID=' + oid);
            return;
        }
        /// 删除我的回答
        function DoDelMyRe(FUNCTION) {
            if (window.confirm('您确定要删除此次回答吗？') == false)
                return;
            ss = FUNCTION.split(",");
            window.showModalDialog('Do.aspx?RefNo=' + ss[0] + "&DoType=DoDelMyRe&RefOID=" + ss[1] + "&rid=" + Math.random());
            //alert("../faq/initdesc.aspx?RefOID=" + ss[1] + "&rid=" + Math.random());
            window.location.href = "../faq/initdesc.aspx?RefOID=" + ss[1] + "&rid=" + Math.random();
            
            return;
        }


                function Down(TEST) {
            ss = TEST.split(",");

            var url = '/edu/sharefile/sfdtl.aspx?RefOID=FAQ' + ss[0];
            window.open(url);
            return;
        }
        function DoLook(TEST) {
            var url = '../Port/Person.aspx?No=' + TEST;
            var iHeight = 260;
            var iWidth = 390;
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2;        //获得窗口的中心位置;
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2;
            window.open(url, '个人信息', 'top=' + iTop + ', left=' + iLeft + ',height=390,width=330,toolbar=no,hotkeys=yes, menubar=no, scrollbars=yes,resizable=no,location=no, status=no')
            return;
        }
        function DoUpDate(TEST) {
            ss = TEST.split(",");
            var url = 'UpDateDtl.aspx?RefNo=' + ss[0] + '&RefOID=' + ss[1] + "&rid=" + Math.random();
            window.showModalDialog(url, '123', 'dialogHeight: 500px; dialogWidth: 700px;  center: yes; help: no');
            window.location.reload();
            return;
        }
        //---退出
        function Esc()
        {
            if (event.keyCode == 27)     
            window.close();
            return true;
        }
        function Click() {
            var txtnr = document.getElementById("PubIng_TB_Doc").value;
            if (txtnr.length == null) {
                alert('描述不能为空');
                return false;
            }
            if (txtnr.length < 10 || txtnr.length > 500) {
                alert('描述应在10-499个字符内');
                return false;
            }
            else
                return true;


        }
    </script>
    


    <style type="text/css">
        html
        {
            font-size: 14px;
            color: #666;
        }
        img
        {
            border: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <p>
        <img src="../Style/Img/right_line_t.jpg" alt="" /></p>
    <div class="r_inner" >
               <p class="siteMap">
        所在位置： 个人中心 - <span class="font_b">我的请求</span></p>
        
<%--    <div align="right" style="height: 100%;">
        <div>
            <div style="float: left; width: 100%">--%>
                <uc1:Pub ID="PubQuestion" runat="server" />

          
          <!--收藏资源开始-->     
           <asp:Panel ID="PLSc" runat="server">
                  <span style="font-size: 12px; font-weight: normal; float: left; margin-right: 10px;">
                     <%--<uc2:UC_SC ID="UC_SC1" runat="server" />--%>
                     <cc1:GEFavorite ID="GEFavorite1"
                         runat="server">
                     </cc1:GEFavorite>
                </span>
          </asp:Panel>
            <!--收藏资源结束-->
                            
                  <uc1:Pub ID="PubQuestion2" runat="server" />
                
                <uc1:Pub ID="PubAnswer" runat="server" />
                <asp:Panel ID="PanelIng" runat="server">
                    <uc1:Pub ID="PubIng" runat="server" />
                </asp:Panel>
                <asp:Panel ID="PanelEdit" runat="server">
                    
                    <uc1:Pub ID="PubEdit" runat="server" />
                </asp:Panel>

<%--            </div>
        </div>
    </div>--%>
    </div>
    <p>
        <img src="/Edu/Style/Img/right_line_b.jpg" alt="" /></p>
    </form>
</body>
</html>
