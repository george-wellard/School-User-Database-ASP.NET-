using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elearning.App_Code;

namespace Elearning
{
    public partial class UpdatePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Checking session exists to know if user is logged in
            if (Session.Count == 0)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
        }

        protected void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            // Validating input 
            if (txtCurrentPassword.Text.Length < 6)
            {
                lblError.Text = "Current password must be at least 6 characters long.";
            }
            else if (txtNewPassword.Text.Length < 6)
            {
                lblError.Text = "New password must be at least 6 characters long.";
            }
            else if (!txtConfirmPassword.Text.Equals(txtNewPassword.Text))
            {
                lblError.Text = "Please confirm new password.";
            }
            else
            {
                // Calling database 
                Users user = new Users();
                // Calling ID session object 
                user.UserId = Int32.Parse(Session["UserID"].ToString());

                // Getting password from database based on ID
                string password = user.getPasswordWithID();

                // Ensuring it matches with input 
                if (password.Equals(txtCurrentPassword.Text))
                {
                    // Setting new password as 
                    user.UserPassword = txtNewPassword.Text;

                    // Updating database with new password
                    if (user.updatePasswordByUserId())
                    {
                        Response.Redirect("~/UserAccount.aspx?UpdateSuccess=Password");
                    }
                    else
                    {
                        lblError.Text = "Database connection error - could not update password";
                    }
                }
                else
                {
                    lblError.Text = "Current password is incorrect";
                }

            }
        }
    }
   
}