<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OKDesc.aspx.cs" Inherits="FAQ_OKDesc" %>

<%@ Register Assembly="BP.GE" Namespace="BP.GE.Ctrl" TagPrefix="cc1" %>
<%--<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../UC/Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<%--<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

	   	<meta   http-equiv ="pragma"   CONTENT="no-cache">   
         <meta   http-equiv ="Cache-Control"   CONTENT="no-cache,   must-revalidate">   
         <meta   http-equiv ="expires"   CONTENT="Wed,   26   Feb   1978   08:21:57   GMT">  
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
    
    
        //admin删除admin的帖子
        function DoDel(oid) {
            if (window.confirm('您确定要删除自己发布的请求吗？') == false)
                return;
            window.showModalDialog('Do.aspx?RefOID=' + oid + "&DoType=DelFAQ");
            window.parent.location.reload();
            window.close();
            return;
        }
        /// 删除问题
        function DoDelPostByAdmin(oid) {
            if (window.confirm('您确定要删除此请求吗？') == false)
                return;
            window.location.href = ('Delete.aspx?DelType=Question&RefOID=' + oid);
            return;
        }
        function Down(TEST) {
            ss = TEST.split(",");
//            window.showModalDialog('Do.aspx?RefNo=' + ss[0] + "&DoType=UpScanTimes");

//            var url = 'FileDownLoad.aspx?RefNo=' + ss[0] + '&RefOID=' + ss[1];

//            var iHeight = 500;
//            var iWidth = 800;
//            var iTop = (window.screen.availHeight - 30 - iHeight) / 2;        //获得窗口的中心位置;
//            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2;
            var url = ss[1] + '/edu/sharefile/sfDtl.aspx?RefOID=FAQ' + ss[0];
            window.open(url);
            return;
        }
        function DoLook(TEST) {
            var url = '../Port/Person.aspx?No=' + TEST;
            var iHeight = 260;
            var iWidth = 390;
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2;        //获得窗口的中心位置;
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2;
            window.open(url, TEST, 'top=' + iTop + ', left=' + iLeft + ',height=390,width=330,toolbar=no,hotkeys=yes, menubar=no, scrollbars=yes,resizable=no,location=no, status=no')
            return;
        }
        //---退出
        function Esc() {
            if (event.keyCode == 27)
                window.close();
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
           <div class="r_inner">
               <p class="siteMap">
        所在位置： 个人中心 - <span class="font_b">我的请求</span></p>
 
                    <uc1:Pub ID="PubQuestion" runat="server" />
                              <!--收藏资源开始-->
         <%-- <uc2:UC_SC ID="UC_SC1" runat="server" />--%>
         <cc1:GEFavorite ID="GEFavorite1"
             runat="server">
         </cc1:GEFavorite>
                <!--收藏资源结束-->
                  <uc1:Pub ID="PubQuestion2" runat="server" />
                    <uc1:Pub ID="PubIsOK" runat="server" />
                    <uc1:Pub ID="PubAnswer" runat="server" />

    </div>
     <p>
        <img src="/Edu/Style/Img/right_line_b.jpg" alt="" /></p>
    </form>
</body>
</html>
