<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NetDiskDtl.ascx.cs" Inherits="GE_NetDisk_NetDiskDtl" %>
<%@ Register Src="../Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<link href="js/disk.css" rel="Stylesheet" type="text/css" />
<script language="javascript">
    function WinOpen(url, winName) {
        var newWindow = window.showModelessDialog(url, winName, 'scroll:1;status:1;help:1;resizable:1;dialogWidth:680px;dialogHeight:420px');
        newWindow.focus();
        return;
        var newWindow = window.open(url, winName, 'scroll:1;status:1;help:1;resizable:1;width=200,height=200');
        newWindow.focus();
    }
    function DoDel(dirOid) {
        if (window.confirm('您确定要删除吗？') == false)
            return;

        var newWindow = window.open('Do.aspx?DoType=Delete&FK_Dir=' + dirOid, "删除文件", 'width=1px,height=1px,top=-1000px,left=300,scrollbars=yes,resizable=false,toolbar=false,location=false,center=yes,center: yes;');
        window.location.reload();
    }

    function DoDownload(dirOid) {
        var newWindow = window.open('Do.aspx?DoType=Download&FK_Dir=' + dirOid, "下载文件", 'width=1px,height=1px,top=-1000px,left=300,scrollbars=yes,resizable=false,toolbar=false,location=false,center=yes,center: yes;');
       
   
    }
    function Upload(dirID) {
        if (dirID == "") {
            alert("请选择上传文件的目录！");
            return;

        }
        else {

            var newWindow = window.showModalDialog('Upload.aspx?DirOID=' + dirID, 'up', 'scroll:0;status:1;help:1;resizable:1;dialogWidth:650px;dialogHeight:350px');
            //newWindow.focus();
            window.location.reload();
        }
    }
    function OrdinaryUpload(dirID) {
        if (dirID == "") {
            alert("请选择上传文件的目录！");
            return;

        }
        else {

            var newWindow = window.showModalDialog('OrdinaryUpload.aspx?DirOID=' + dirID, 'up', 'scroll:0;status:1;help:1;resizable:0;dialogWidth:600px;dialogHeight:420px');
            //newWindow.focus();
            window.location.reload();
        }
    }
</script>
 <uc1:Pub ID="Pub1" runat="server" />
 
