using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elearning.App_Code;

namespace Elearning
{
    public partial class StudentDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Calling in Database
                Course course = new Course();
                Users users = new Users();
                Modulescs mod = new Modulescs();
                // Calling in session objects
                course.CourseId = Int32.Parse(Session["CourseID"].ToString());
                users.CourseId = Int32.Parse(Session["CourseID"].ToString());

                // Grabbing the course name from database and setting as text 
                string coursename = course.getCourse();
                txtCourse.Text = coursename;

                // Calling datatable 
                DataTable dt = users.getTutors();

                if (dt != null)
                {
                    //set Listbox's data source to the DataTable
                    lstTutors.DataSource = dt;
                    //assign UserID database field to the value property
                    lstTutors.DataValueField = "UserID";
                    //assign RealName database field to the text property
                    lstTutors.DataTextField = "RealName";
                    // Bind data
                    lstTutors.DataBind();
                }

                DataTable dt2 = course.getModules();

                if(dt2 != null)
                {
                    // Seting repeater data source to Datatable
                    rptModules.DataSource = dt2;
                    // Bind Data
                    rptModules.DataBind();
                }
                

            }
        }

        protected void btnShowEmail_Click(object sender, EventArgs e)
        {
            // Call database 
            Users user = new Users();

            // Pausing system to display Ajax 
            System.Threading.Thread.Sleep(3000);

            // Validate ListBox selection
            if (lstTutors.SelectedIndex == -1)
            {
                lblError.Text = "You must select a tutor";

            }
            else
            {
                // Get UserID from selected tutor
                user.UserId = Int32.Parse(lstTutors.SelectedValue);

                // Getting email from DataBase 
                string email = user.getTutorEmail();
                lblEmail.Text = email;
            }
        }
    }
}