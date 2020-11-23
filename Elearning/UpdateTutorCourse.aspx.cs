using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elearning.App_Code;
using System.Data;

namespace Elearning
{
    public partial class UpdateTutorCourse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
 
                // Calling databases
                Course course = new Course();
                Users users = new Users();
                // Calling course sesssion of current course id
                course.CourseId = Int32.Parse(Session["CourseID"].ToString());

                // Searching Database for course name
                string coursename = course.getCourse();

                // Setting selected course name as textbox text
                txtCourse.Text = coursename;

                DataTable dt = course.getAllCourses();

                if (dt != null)
                {
                    //set Listbox's data source to the DataTable
                    lstCourses.DataSource = dt;
                    //assign CourseID database field to the value property
                    lstCourses.DataValueField = "CourseID";
                    //assign CourseName database field to the text property
                    lstCourses.DataTextField = "CourseName";
                    //bind data
                    lstCourses.DataBind();
                }
                else
                {
                    lblError.Text = "Database connection error - cannot display courses.";
                }
            }
        }

        protected void btnUpdateCourse_Click(object sender, EventArgs e)
        {
            // Call datavases
            Users user = new Users();
            Course course = new Course();;
            // Set the users course id to the selected course
            user.CourseId = Int32.Parse(lstCourses.SelectedValue);
            // Call in userid session object 
            user.UserId = Int32.Parse(Session["UserID"].ToString());

            // Changing the courseid session value to the selected courses value
            Session["CourseID"] = lstCourses.SelectedValue;

            // Calling database method to change the course within the data base 
            if (user.updateCourse())
            {
                // Take user to the account page 
                Response.Redirect("~/UserAccount.aspx?UpdateSuccess=Course");
            }
            else
            {
                lblError.Text = "Database connection error - course update failed";
            }
        }

        protected void btnSearchCourse_Click(object sender, EventArgs e)
        {
          //  System.Threading.Thread.Sleep(3000);

            Course course = new Course();

            // Validating user input 
            if(TxtSearch.Text.Equals(""))
            {
                lblError.Text = "Please enter a courseID";
            }
            else
            {
                // Setting the course name to the searched item. So the database knows what to search 
                course.CourseName = TxtSearch.Text;

                // Calling method from DataBase to search for same Coursename 
                if(course.SearchCourseByName())
                {
                    lblError.Text = "Selected Course: " + TxtSearch.Text;

                    // Setting the selected value to the text item within the search 
                    lstCourses.SelectedValue = lstCourses.Items.FindByText(TxtSearch.Text).Value;
                }
                else
                {
                    lblError.Text = "Could not find course";
                }
            }
        }

    }
}