using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
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
                form.Visible = false;
                status.Visible = true;

                string username = Request.Form["loginname"];
                string oldpass = Request.Form["oldpass"];
                string newpass = Request.Form["newpass"];
                string confpass = Request.Form["confpass"];



                try
                {
                    if (confpass.Equals(newpass) && !oldpass.Equals(newpass) && !string.Empty.Equals(newpass))
                    {
                        ResetPassword(username, oldpass, newpass);
                        msg.Text = "Your password was successfully changed.";
                    }
                    else
                    {
                        msg.Text = "New Password invalid. It cannot be blank, or the same as your old password.";
                    }
                }
                catch (ArgumentException)
                {
                    msg.Text = "Unknown Username.";
                }
                catch (UnauthorizedAccessException)
                {
                    msg.Text = "Invalid Username or Password.";
                }
                catch (Exception)
                {
                    msg.Text = "Something went wrong. Please Try Again. If this persists, notify your Administrator.";
                }

            }
            else
            {
                form.Visible = true;
                status.Visible = false;
            }
        }

        protected void ResetPassword(string username, string password, string newpassword)
        {
            // Use the current computer's domain to construct the LDAP connection string.
            //string dnsdomain = IPGlobalProperties.GetIPGlobalProperties().DomainName;
            //string connectionString = "LDAP://" + dnsdomain + "/" + string.Join(",", dnsdomain.Split('.').Select(x => "dc=" + x));

            string connectionString = ConfigurationManager.ConnectionStrings["LDAPConnectionString"].ConnectionString;

            DirectoryEntry me = null;

            try
            {
                using (DirectoryEntry searchRoot = new DirectoryEntry(connectionString, username, password, AuthenticationTypes.Secure))
                {
                    using (DirectorySearcher ds = new DirectorySearcher(searchRoot, "(sAMAccountName=" + username + ")"))
                    {
                        SearchResult sr = ds.FindOne();
                        if (sr != null)
                            me = sr.GetDirectoryEntry();
                        else
                            throw new ArgumentException("Failed to find login name!");
                    }
                }
            }
            catch (Exception)
            {
                throw new UnauthorizedAccessException("Invalid username or password!");
            }

            me.Invoke("SetPassword", new object[] { newpassword });
            me.CommitChanges();
        }
    }
}