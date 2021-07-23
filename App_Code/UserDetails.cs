using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserDetails
/// </summary>
public class UserDetails:System.Web.Services.Protocols.SoapHeader
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Authtoken { get; set; }
    public UserDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public bool checkCreds(string UserName,string Password)
    {
        if (UserName == "admin" && Password == "admin")
            return true;
        else
            return false;
    }
    public bool checkCreds(UserDetails SoapHeader)
    {
        if (SoapHeader == null)
            return false;
        //check in cache
        if(!string.IsNullOrEmpty(SoapHeader.Authtoken))
            return (HttpRuntime.Cache[SoapHeader.Authtoken] !=null);
        return false;
    }


}