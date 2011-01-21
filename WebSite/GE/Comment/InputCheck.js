function validate_email(field,alerttxt)
{
    with (field)
    {
        apos=value.indexOf("@")
        dotpos=value.lastIndexOf(".")
        if (apos<1||dotpos-apos<2) 
        { 
            alert(alerttxt);    
            return false
        }
        else
        {
            return true
        }
    }
}

function validate_IsEmpty(field,alerttxt)
{
    with(field)
    {
        strValue=value.replace(/(^\s*)|(\s*$)/g,"")
        {
            if(strValue=="")
            {
                alert(alerttxt);
            }
        }
    }
}

function validate_form()
{
    thisform=document.forms[0];
    with (thisform)
    {
      
        if (validate_IsEmpty(txtUserName,"Not Empty")==false)
        {
            email.focus();
            return false
        }
        
        if (validate_email(email,"Not a valid e-mail address!")==false)
        {
            email.focus();
            return false
        }
    }
}
