<%@ WebHandler Language="C#" Class="GetCustomerSetting" %>

using System;
using System.Web;
using System.Web.SessionState;
using BP.DA;

public class GetCustomerSetting : IHttpHandler, IRequiresSessionState {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        BP.CCOA.SSOCustomerModule ssocm = null;
        try
        {
            ssocm = new BP.CCOA.SSOCustomerModule(BP.Web.WebUser.No);
        }
        catch (Exception ex)
        { 
        }

        if (ssocm != null)
        {
            context.Response.Write(ssocm.ModuleOrder);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}