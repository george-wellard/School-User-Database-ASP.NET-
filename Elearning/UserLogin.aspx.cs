using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elearning.App_Code;
using System.Security.Cryptography;
using System.Web.Security;

namespace Elearning
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //validate input before connecting to database
            if (txtUsername.Text.Length < 5)
            {
                lblError.Text = "Username is too short";
            }
            else if (txtPassword.Text.Length < 6)
            {
                lblError.Text = "Password is too short";
            }
            else
            {
                // Calling database 
                Users user = new Users();

                // storing inputted values as part of database 
                user.UserName = txtUsername.Text;
                user.UserPassword = txtPassword.Text;

                if (user.authenticateUser())
                {
                    // Send to Account page
                    Response.Redirect("~/UserAccount.aspx");
                }
                else
                {
                    lblError.Text = "Incorrect username and/or password";
                }
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            // Send to registration page 
            Response.Redirect("~/StudentRegistration.aspx");
        }
    }
}