<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrdinaryUpload.aspx.cs" Inherits="OrdinaryUpload" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="../Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../Style/css/import.css" rel="stylesheet" type="text/css" />
    <link href="../Style/css/openwindow.css" rel="stylesheet" type="text/css" />
    <link href="../../Comm/Table2.css" rel="stylesheet" type="text/css" />
    <title>批量共享</title>

    <script type="text/jscript">
        function Esc() {
            if (event.keyCode == 27)
                window.close();
            return true;
        }

        function SelectZJ() {
            window.showModalDialog("RelGuide.aspx", window.self, "dialogWidth:740px;");
        }
        function SetValue(val, valname) {
            //document.getElementById("LabID").innerHTML="";
            document.getElementById("TxtZJ").value = valname;
            document.getElementById("HiddenID").value = val;
            //alert(document.getElementById("HiddenID").value);

        }

        function Click() {
            var txtzj = document.getElementById("TxtZJ").value;
            if (txtzj == "" || txtzj == null) {
                alert('您还没有选择章节！');
                return false;
            }
            else
                return true;
        }

        function ClearFile(i) {
           
            var tbId = "Pub1_TB_" + i;
//            document.getElementById(tbId).value = "";

            var id = "Pub1_F" + i;
            //如果传过来的参数是字符,则取id为该字符的元素,如果无此元素,则返回空
            var up = (typeof id == "string") ? document.getElementById(id) : id;
            if (typeof up != "object") return null;

            //创建一个span元素
            var tt = document.createElement("span");
            //添加id,以便后面使用
            tt.id = "__tt__";
            up.parentNode.insertBefore(tt, up);

            //创建一个form
            var tf = document.createElement("form");
            //将上传控件追加为form的子元素
            tf.appendChild(up);

            //将form加入到body
            document.getElementsByTagName("body")[0].appendChild(tf);

            //利用重置来清空上传控件内容
            tf.reset();

            //所上传控件放回原来的位置
            tt.parentNode.insertBefore(up, tt);

            //除上面创建的这个span
            tt.parentNode.removeChild(tt);
            tt = null;

            //移除上面临时创建的form
            tf.parentNode.removeChild(tf);
        }

        //类方法,清除多个上传控件的内容
        function ClearForm() {
            for (var idx = 1; idx < 11; idx++) {
                var tbId = "Pub1_TB_" + idx;
                document.getElementById(tbId).value = "";
            }

            var inputs, frm;

            inputs = document.getElementsByTagName("input");

            //遍历所有获取的元素,如果是上传控件类型,则加入到一个数组的末尾.
            var fs = [];
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "file") fs[fs.length] = inputs[i];
            }


            //创建一个form元素
            var tf = document.createElement("form");
            for (var i = 0; i < fs.length; i++) {

                //每个上传控件前创建一个span元素,用来标记它的位置,而span不会影响它的样式
                var tt = document.createElement("span");
                //为每个span加一个id,以便后面将上传控件放回原来位置
                tt.id = "__tt__" + i;

                //将这个span元素作为组中的每一个上传控件的兄弟元素插入到每一个上传控件之前
                fs[i].parentNode.insertBefore(tt, fs[i]);

                //将这个上传控件追加到新创建的form中
                tf.appendChild(fs[i]);
            }

            //将新创建的form追加到页面body中
            document.getElementsByTagName("body")[0].appendChild(tf);

            //重置form,以便清空各上传控件的值.(利用重置来清空内容)
            tf.reset();

            //将各个上传控件重新放回到原来的位置
            for (var i = 0; i < fs.length; i++) {
                var tt = document.getElementById("__tt__" + i);
                tt.parentNode.insertBefore(fs[i], tt);
                tt.parentNode.removeChild(tt);
            }
            tf.parentNode.removeChild(tf);
        }
    </script>

    <base target="_self" />
</head>
<body onkeypress="Esc()">
    <form id="form1" runat="server">
    <uc1:Pub ID="Pub1" runat="server" />
    </form>
</body>
</html>
