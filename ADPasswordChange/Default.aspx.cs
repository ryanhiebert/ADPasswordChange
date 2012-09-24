using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADPasswordChange
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                status.Visible = true;

                string username = Request.Form["loginname"];
                string oldpass = Request.Form["oldpass"];
                string newpass = Request.Form["newpass"];
                string confpass = Request.Form["confpass"];

                if (confpass == newpass && oldpass != newpass && string.Empty != newpass)
                {
                    string dnsdomain = ConfigurationManager.AppSettings["DomainName"];
                    if (dnsdomain == null) { dnsdomain = IPGlobalProperties.GetIPGlobalProperties().DomainName; }

                    string searchbase = ConfigurationManager.AppSettings["SearchBase"];
                    if (searchbase == null) { searchbase = string.Join(",", dnsdomain.Split('.').Select(x => "dc=" + x)); }

                    PrincipalContext context = new PrincipalContext(ContextType.Domain, dnsdomain, searchbase, username, oldpass);
                    UserPrincipal user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, username);
                    if (user == null) { user = UserPrincipal.FindByIdentity(context, IdentityType.UserPrincipalName, username); }
                    if (user != null)
                    {
                        user.ChangePassword(oldpass, newpass);
                        msg.Text = "Password Changed Successfully!";
                    }
                    else
                    {
                        msg.Text = "Problem finding username.";
                    }
                }
                else
                {
                    msg.Text = "New Password invalid. It cannot be blank, or the same as your old password.";
                }
            }
            else
            {
                status.Visible = false;
            }
        }
    }
}