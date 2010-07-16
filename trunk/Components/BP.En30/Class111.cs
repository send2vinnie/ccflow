using System;
using System.Collections.Generic;
using System.Text;

namespace BP.En20
{

    public class DataProvider
    {
        public static IDataProvider Instance()
        {
            //use the cache because the reflection used later is expensive

            Cache cache = System.Web.HttpContext.Current.Cache;
            if (cache["IDataProvider"] == null)
            {
                //get the assembly path and class name from web.config
                String prefix = "";
                NameValueCollection context =

            (NameValueCollection)ConfigurationSettings.GetConfig("appSettings");
                if (context == null)
                {
                    //can not get settings
                    return null;
                }

                String assemblyPath = context[prefix +"DataProviderAssemblyPath"];
                String className = context[prefix + "DataProviderClassName"];

                // assemblyPath presented in virtual form, must convert to physical path   
                // assemblyPath = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath + "/bin/" + assemblyPath);     
                // Uuse reflection to store the constructor of the class that implements IWebForumDataProvider
                try
                {
                    cache.Insert("IDataProvider", Assembly.LoadFrom(assemblyPath).GetType(className).GetConstructor(new Type[0]), new CacheDependency(assemblyPath));
                }
                catch (Exception)
                {
                    // could not locate DLL file
                    HttpContext.Current.Response.Write("<b>ERROR:</b> Could not locate file: <code>" + assemblyPath + "</code> or could not locate class <code>" + className + "</code> in file.");
                    HttpContext.Current.Response.End();
                }
            }
            return (IDataProvider)(

         ((ConstructorInfo)cache["IDataProvider"]).Invoke(null));
        }
    }
}
