using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for MyWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class MyWebService : System.Web.Services.WebService
{
    public UserDetails SoapHeader;
    public MyWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [WebMethod]
    [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
    public string AuthenticationUser()
    {
        if (SoapHeader == null)
            return "please provide username and pass";
        if (string.IsNullOrEmpty(SoapHeader.UserName) || string.IsNullOrEmpty(SoapHeader.Password))
            return "please provide UserName and pass";
        if (!SoapHeader.checkCreds(SoapHeader.UserName, SoapHeader.Password))
            return "invalid username and pass";
        //create and store token in cache.
        string token = Guid.NewGuid().ToString();
        HttpRuntime.Cache.Add(token, SoapHeader.UserName, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
            TimeSpan.FromMinutes(30),System.Web.Caching.CacheItemPriority.NotRemovable,null);
        return token;
    }


    [WebMethod]
    [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
    public string HelloWorld()
    {
        if (SoapHeader == null)
            return "Please call AuthenticationMethod() first";
        if(!SoapHeader.checkCreds(SoapHeader))
            return "please call AuthenticationMethod() first";
        return "Hello" + HttpRuntime.Cache[SoapHeader.Authtoken];
    }

}
