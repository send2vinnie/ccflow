
//校验密码  GUID模式
function Check(pid, pin, password) {
    try {
        var ePass = new ActiveXObject("ET99_FULL.ET99Full.1");

        //找锁             
        var ePassNum = ePass.FindToken(pid);
        if (ePassNum != 1) {
            alert('请插入正确的USB KEY!');
            return false;
        }
        //打开锁
        ePass.OpenToken(pid, 1);
        ePass.VerifyPIN(0, pin);        
        //验证
        var passKey = ePass.Read(0, 0, 32);           
        if (passKey == password) {
            document.getElementById('uclogin_btn1').click();
        } else {
            alert('USB KEY 验证失败!!请插入该帐号对应的USB KEY!');
        }
    } catch (error) {
        ePass.CloseToken();
        alert('请插入正确的USB KEY!');
        return false;
    }

}


//设置U-KEY密码
function SetPassKey(pid, pin, password) {
    try {
        var ePass = new ActiveXObject("ET99_FULL.ET99Full.1");

        //找锁             
        var ePassNum = ePass.FindToken(pid);
        if (ePassNum != 1) {
            alert('请插入正确的USB KEY!');
            return false;
        }
        //打开锁
        ePass.OpenToken(pid, 1);
        ePass.VerifyPIN(0, pin);
        //设置密码
        ePass.write(0, 0, 32, password);
        return true;
    } catch (error) {
        ePass.CloseToken();
        alert(error.number & 0xFFFF);
        return false;
    }
}
//设置PIN码
function SetPin(pid, oldpin, newpin) {
    try {
        var ePass = new ActiveXObject("ET99_FULL.ET99Full.1");

        //找锁             
        var ePassNum = ePass.FindToken(pid);
        if (ePassNum != 1) {
            alert('请插入正确的USB KEY!');
            return false;
        }
        //打开锁
        ePass.OpenToken(pid, 1);
        ePass.VerifyPIN(0, oldpin);
        //设置密码
        ePass.ChangeUserPIN(oldpin, newpin);
        return true;
    } catch (error) {
        ePass.CloseToken();
        alert(error.number & 0xFFFF);
        return false;
    }
}

//重置pin  传出超级用户的pin  默认ffffffffffffffff
function ResetPin(pid, sopin) {
    try {
        var ePass = new ActiveXObject("ET99_FULL.ET99Full.1");

        //找锁             
        var ePassNum = ePass.FindToken(pid);
        if (ePassNum != 1) {
            alert('请插入正确的USB KEY!');
            return false;
        }
        //打开锁
        ePass.OpenToken(pid, 1);
        ePass.VerifyPIN(1, sopin);
        ePass.ResetPIN(sopin);
        return true;
    } catch (error) {
        ePass.CloseToken();
        alert(error.number & 0xFFFF);
        return false;
    }
}
    