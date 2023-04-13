using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace MatchingCoffees
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
           Session["First_Name"]    =   string.Empty;
           Session["Last_Name"]     =   string.Empty;
           Session["Username"]      =   string.Empty;
           Session["UID"]           = string.Empty;
           Session["DOB"]           = string.Empty;
           Session["ProfilePicture"] = string.Empty;
           Session["Pref_Coffee"]    = string.Empty;
           Session["Pref_WakeHour"]  = string.Empty;
           Session["Pref_Gender"]    = string.Empty;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}