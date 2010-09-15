var strResult;
function selectAll(e)
{
    var str = "";
    var s = document.getElementsByTagName("INPUT");
    for(var i=0;i<s.length;i++)
    {
        if(s[i].type=="checkbox" && s[i].name=="chklinkman")
        {
            s[i].checked = e.checked;
            str += s[i].id;
        }
    }
    if(e.checked == true)
    {
	    document.getElementById("linkman").value = str;
    }
    else
    {
        document.getElementById("linkman").value = '';
    }
}

function Chk_Click(e)
{
    var str = document.getElementById("linkman").value;
    var strSub = e.id ;
    if(e.checked == true)
    {
        if(str.indexOf(strSub)==-1)
        {
            str+=strSub;
        }
    }
    else
    {
        str=str.replace(strSub,"");
    }
    document.getElementById("linkman").value = str;
}


 //AJAX 方法 查找用户编号
 var xmlhttp;
 function doAsyncPost(e)
 {
     if(CheckForm(document.forms[0]) != false)
     {
        //根据用户名查找用户详细信息
        if(e.id == "txtName")
        {
            createHTTP("EmpName=" + escape(e.value));
        }
        //根据用户编号查找用户详细信息
        else if(e.id == "txtNo")
        {
           createHTTP("EmpNo=" + escape(e.value));
        }
        //保存好友信息
        else if(e.id == "btnSave")
        {
            if (strResult.indexOf("查无此用户") != -1) 
            {
                alert("您输入的用户不存在!");
            }
            else
            {
                var str=ReplaceComma(document.getElementById("txtNo").value) + ",";    
                str+=ReplaceComma(document.getElementById("txtName").value) + ",";
                str+=ReplaceComma(document.getElementById("txtEmail").value) + ","; 
                str+=ReplaceComma(document.getElementById("txtMobile").value) + ",";
                str+=ReplaceComma(document.getElementById("txtPhone").value) + ",";
                str+=ReplaceComma(document.getElementById("txtBirthday").value) + ",";
                str+=ReplaceComma(document.getElementById("txtAddr").value) + ",";
                str+=ReplaceComma(document.getElementById("txtDept").value) + ",";
                str+=ReplaceComma(document.getElementById("txtNote").value) + ",";
                str+=ReplaceComma(document.getElementById("txtUser").name) + ",";
                createHTTP("strSave=" + escape(str));
            }
        }
        //更新联系人
        else if(e.id == "btnUpdate")
        {
            var str=ReplaceComma(document.getElementById("txtNo").value) + ",";    
            str+=ReplaceComma(document.getElementById("txtName").value) + ",";
            str+=ReplaceComma(document.getElementById("txtEmail").value) + ","; 
            str+=ReplaceComma(document.getElementById("txtMobile").value) + ",";
            str+=ReplaceComma(document.getElementById("txtPhone").value) + ",";
            str+=ReplaceComma(document.getElementById("txtBirthday").value) + ",";
            str+=ReplaceComma(document.getElementById("txtAddr").value) + ",";
            str+=ReplaceComma(document.getElementById("txtDept").value) + ",";
            str+=ReplaceComma(document.getElementById("txtNote").value) + ",";
            str+=ReplaceComma(document.getElementById("txtUser").name) + ",";
            str+=ReplaceComma(document.getElementById("txtUser").getAttribute("OID")) + ",";
            createHTTP("strUpdate=" + escape(str));
        }
        //发送邮件
        else if(e.id == "btnSend")
        {
            var Emps = escape(document.getElementById("txtReceiver").value);
            var Title = escape(document.getElementById("txtTitle").value);
            var CUser = escape(document.getElementById("CUser").value);
            var Doc =  escape(window.frames["eWebEditor1"]. getHTML());
            createHTTP("CUser="+CUser+"&Emps="+Emps+"&Title="+Title+"&Doc="+Doc);
        }
        else if(e.id == "btnReply")
        {
            var OID = escape(e.name);
            var Emps = escape(document.getElementById("txtReceiver").value);
            var Title = escape(document.getElementById("txtTitle").value);
            var CUser = escape(document.getElementById("CUser").value);
            var Doc = escape(window.frames["eWebEditor1"]. getHTML());
            createHTTP("OID="+OID+"&CUser="+CUser+"&Emps="+Emps+"&Title="+Title+"&Doc="+Doc);
        }
         //存草稿
        else if(e.id == "btnDraft")
        {
            var Emps = escape(document.getElementById("txtReceiver").value);
            var Title = escape(document.getElementById("txtTitle").value);
            var CUser = escape(document.getElementById("CUser").value);
            var Doc =  escape(window.frames["eWebEditor1"].getHTML());
            createHTTP("DraftCUser="+CUser+"&DraftEmps="+Emps+"&DraftTitle="+Title+"&DraftDoc="+Doc);
        }
        //删除联系人
        else if(e.id == "btnDelMsg")
        {
            var str = document.getElementById("linkman").value;
            createHTTP("DelOID=" + escape(str));
        }
     }
 }
 function createHTTP(para)
 {
    //alert(para);
    //根据不同的浏览器创建XMLHttpRequest
    if(window.ActiveXObject)
    {
       xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
	   //alert('创建浏览器对象成功');
    }
    else if(window.XMLHttpRequest)
    {
       xmlhttp=new XMLHttpRequest();
    }
    //状态变化与事件挂钩
    xmlhttp.onreadystatechange=StateDO;
    //获取XML文件的数据
    xmlhttp.open("POST","Handler.ashx",true); 
    xmlhttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
    xmlhttp.send(para);
 }
 function StateDO()
 {
    //判断是否是完成状态
    if(xmlhttp.readyState==4)
    {
      //alert('状态完成');
      //判断是否执行成功
      if(xmlhttp.status==200)
      {            
          //alert('执行成功');
          strResult=xmlhttp.responseText;
          var strs
          if(strResult.indexOf(",") != -1)
          {
            strs= strResult.split(",");
            document.getElementById("txtNo").value = strs[0];
            document.getElementById("txtName").value = strs[1];
            document.getElementById("txtAddr").value = strs[2];
            document.getElementById("txtPhone").value = strs[3];
            document.getElementById("txtMobile").value = strs[4];
            document.getElementById("txtEmail").value = strs[5];
            document.getElementById("txtDept").value = strs[6];  
            document.getElementById("txtNote").value=strs[7];          
          }
          else
          {
            if(strResult.length > 0)
            {
                alert(strResult);
            }
          }
      }
    }
 }
 //显示层
function openDiv(e)
{
    if (document.getElementById("divEmps").style.display == "none") 
    {
        var txtObject = e;
        var orgObject = document.getElementById("divEmps");
        var rect = getoffset(txtObject);
        orgObject.style.top = rect[0] + 23;
        orgObject.style.left = rect[1] + 3;
        orgObject.style.display = "block";
        orgObject.focus();
    }
    else
    {
        document.getElementById("divEmps").style.display = "none"
    }
}
function getoffset(e) 
{
    var t = e.offsetTop;
    var l = e.offsetLeft;
    while (e = e.offsetParent) 
    {
        t += e.offsetTop;
        l += e.offsetLeft;
    }
    var rec = new Array(1);
    rec[0] = t;
    rec[1] = l;
    return rec
}

function closeDiv()
{
    document.getElementById("divEmps").style.display = "none";
}

var myColor;
var myBgColor;
function myOver(e)
{
    myBgColor=e.style.backgroundColor;
    myColor = e.style.color;
    e.style.cursor = "pointer";
    e.style.backgroundColor = "#d6e3f3";
    e.style.color = "#0000FF";
}
function myOut(e)
{
    e.style.backgroundColor=myBgColor;
    e.style.color = myColor;
}
function myClick(e,strID,oid)
{
    var chk = document.getElementById(strID);
    chk.checked = !chk.checked;
    Chk_Click(chk);
    window.location.href="?op=m&oid="+oid;
}
function mydblClick(strID,op)
{
    window.location.href="?op="+op+"&OID="+strID;
}
function EditorSetValue(str)
{
   //window.frames["eWebEditor1"].setHTML(str);
   //document.getElementByName("eWebEditor1").setHTML(str);
}
function SelectEmp(e,strEmp)
{
    var str = document.getElementById("linkman").value;
    if(str.indexOf(strEmp) == -1)
    {
        str += strEmp;
        e.style.color = "#0000ff";
        e.onmouseout = null;
        e.onmouseover = null;
    }
    else
    {
        str = str.replace(strEmp,"");
        e.style.color = myColor;
        e.onmouseover = function(){myOver(e);};      
        e.onmouseout = function(){myOut(e);};  
    }
    document.getElementById("txtReceiver").value = str;
    document.getElementById("linkman").value = str;
}

//主函数
function CheckForm(oForm)
{
    var els = oForm.elements;
    //遍历所有表元素
    for(var i=0;i<els.length;i++)
    {
        //是否需要验证
        if(els[i].check)
        {
            //取得验证的正则字符串
            var sReg = els[i].check;
            //取得表单的值,用通用取值函数
            var sVal = GetValue(els[i]);
            //字符串->正则表达式,不区分大小写
            var reg = new RegExp(sReg,"i");
            if(!reg.test(sVal))
            {
                //验证不通过,弹出提示warning
                alert(els[i].warning);
                //该表单元素取得焦点,用通用返回函数
                GoBack(els[i])  
                return false;
            }
        }
    }
}

//通用取值函数分三类进行取值
//文本输入框,直接取值el.value
//单多选,遍历所有选项取得被选中的个数返回结果"00"表示选中两个
//单多下拉菜单,遍历所有选项取得被选中的个数返回结果"0"表示选中一个
function GetValue(el)
{
    //取得表单元素的类型
    var sType = el.type;
    switch(sType)
    {
        case "text":
        case "hidden":
        case "password":
        case "file":
        case "textarea": return el.value;
        case "checkbox":
        case "radio": return GetValueChoose(el);
        case "select-one":
        case "select-multiple": return GetValueSel(el);
    }
    //取得radio,checkbox的选中数,用"0"来表示选中的个数,我们写正则的时候就可以通过0{1,}来表示选中个数
    function GetValueChoose(el)
    {
        var sValue = "";
        //取得第一个元素的name,搜索这个元素组
        var tmpels = document.getElementsByName(el.name);
        for(var i=0;i<tmpels.length;i++)
        {
            if(tmpels[i].checked)
            {
                sValue += "0";
            }
        }
        return sValue;
    }
    //取得select的选中数,用"0"来表示选中的个数,我们写正则的时候就可以通过0{1,}来表示选中个数
    function GetValueSel(el)
    {
        var sValue = "";
        for(var i=0;i<el.options.length;i++)
        {
            //单选下拉框提示选项设置为value=""
            if(el.options[i].selected && el.options[i].value!="")
            {
                sValue += "0";
            }
        }
        return sValue;
    }
}

//通用返回函数,验证没通过返回的效果.分三类进行取值
//文本输入框,光标定位在文本输入框的末尾
//单多选,第一选项取得焦点
//单多下拉菜单,取得焦点
function GoBack(el)
{
    //取得表单元素的类型
    var sType = el.type;
    switch(sType)
    {
        case "text":
        case "hidden":
        case "password":
        case "file":
        case "textarea": el.focus();var rng = el.createTextRange(); rng.collapse(false); rng.select();
        case "checkbox":
        case "radio": var els = document.getElementsByName(el.name);els[0].focus();
        case "select-one":
        case "select-multiple":el.focus();
    }
}

function ReplaceComma(e) {
    while (e.indexOf(",") != -1) {
        e = e.replace(",", "");
    }
    return e;
}

function DelConfirm() {
    return window.confirm("您确定要删除吗?");
}