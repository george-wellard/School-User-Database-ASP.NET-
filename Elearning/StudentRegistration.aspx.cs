using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Elearning.App_Code;
using System.Data;
using System.Security.Cryptography;
using System.Web.Security;

namespace Elearning
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if request is NOT a post back
            if (!Page.IsPostBack)
            {
                //create instane of middle layer business object
                Course course = new Course();
                Users users = new Users();

                //retrieve departments from middle layer into a DataTable
                DataTable dt = course.getAllCourses();

                //check if query was successful
                if (dt != null)
                {
                    //set DropDownList's data source to the DataTable
                    ddlCourses.DataSource = dt;
                    //assign CourseID database field to the value property
                    ddlCourses.DataValueField = "CourseID";
                    //assign CourseName database field to the text property
                    ddlCourses.DataTextField = "CourseName";
                    //bind data
                    ddlCourses.DataBind();
                }
                else
                {
                    lblError.Text = "Database connection error - cannot display courses.";
                }

            }

        }

    

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            //validate input
            if (txtUsername.Text.Length < 5)
            {
                lblError.Text = "Username must be at least 5 characters long.";
            }
            else if (txtPassword.Text.Length < 6)
            {
                lblError.Text = "Password must be at least 6 characters long.";
            }
            else if (!txtConfirmPassword.Text.Equals(txtPassword.Text))
            {
                lblError.Text = "Please confirm password.";
            }
            else if (txtRealName.Text.Equals(""))
            {
                lblError.Text = "Please enter your full name.";
            }
            else if (txtEmailAddress.Text.Equals(""))
            {
                lblError.Text = "Please enter your full email address";
            }
            else
            {
                //create instane of middle layer business object
                Users user = new Users();

                //set username property, so it  can be used as a parameter for the query
                user.UserName = txtUsername.Text;

                int saltSize = 5;
                string salt = setSalt(saltSize);
                string passwordHash = CreatePasswordHash(txtPassword.Text, salt);

                //check if the username exists
                if (user.userNameExists())
                {
                    //already exists so output error
                    lblError.Text = "Username already exists, please select another";
                }
                else
                {
                    //set properties, so it can be used as a parameter for the query
                    user.UserName = txtUsername.Text;
                    user.UserPassword = passwordHash; // Storing password as hash instead of input text
                    user.RealName = txtRealName.Text;
                    user.EmailAddress = txtEmailAddress.Text;
                    user.CourseId = Int32.Parse(ddlCourses.SelectedValue);
                    user.RoleId = Int32.Parse(ddlRoles.SelectedValue);
                    user.Salt = salt;

                    //attempt to add a worker and test if it is successful
                    if (user.addUser())
                    {

                        //redirect user to login page
                        Response.Redirect("~/UserLogin.aspx");
                    }
                    else
                    {
                        //exception thrown so display error
                        lblError.Text = "Database connection error - failed to insert record.";
                    }
                }
            }
        }

        private static string setSalt(int size)
        {
            // Generate a cryptographic random number using the cryptographic
            // service provider
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }

        private static string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);
            string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "SHA1");
            hashedPwd = String.Concat(hashedPwd, salt);
            return hashedPwd;
        }
    }
}