<%@ WebHandler Language="C#" Class="SetCustomerSetting" %>

using System;
using System.Web;
using System.Web.SessionState;
using BP.DA;

public class SetCustomerSetting : IHttpHandler, IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        BP.CCOA.SSOCustomerModule ssocm = null;
        try
        {
            ssocm = new BP.CCOA.SSOCustomerModule(BP.Web.WebUser.No);
            ssocm.ModuleOrder = context.Request["moduleOrder"];
            ssocm.Update();
        }
        catch (Exception ex)
        {
            ssocm = new BP.CCOA.SSOCustomerModule();
            ssocm.No = BP.Web.WebUser.No;
            ssocm.UserNo = BP.Web.WebUser.No;
            ssocm.ModuleOrder = context.Request["moduleOrder"];
            ssocm.Insert();
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}