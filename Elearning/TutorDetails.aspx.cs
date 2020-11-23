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
    public partial class TutorDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                // Calling Databases 
                Course course = new Course();
                Users users = new Users();
                // Getting session objects 
                course.CourseId = Int32.Parse(Session["CourseID"].ToString());
                users.CourseId = Int32.Parse(Session["CourseID"].ToString());

                string coursename = course.getCourse();
                txtCourse.Text = coursename;

                // Calling DataTable to query for students 
                DataTable dt = users.getStudents();


                if(dt != null)
                {
                    //set Listbox's data source to the DataTable
                    lstStudents.DataSource = dt;
                    //assign UserID database field to the value property
                    lstStudents.DataValueField = "UserID";
                    //assign RealName database field to the text property
                    lstStudents.DataTextField = "RealName";
                    // Bind Data
                    lstStudents.DataBind();
                }
            }
        }

        protected void lstStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(3000);
            lblMatch.Text = "ListBox Updated";
        }

            protected void btnRemoveStudent_Click(object sender, EventArgs e)
        {
            // Calling in Database 
            Users user = new Users();

            // Validate selection 
            if (lstStudents.SelectedIndex == -1)
            {
                lblError.Text = "You must select a student";
               
            }
            else
            {
                // Getting userID of selected student
                user.UserId = Int32.Parse(lstStudents.SelectedValue);

                // Deleting student from databse 
                if (user.deleteStudent())
                {
                    // Removing student from listbox
                    lstStudents.Items.RemoveAt(lstStudents.SelectedIndex);
                    lblSuccess.Text = "Student Removed";
                }
                else
                {
                    lblError.Text = "Database connection error - student deletion failed";
                }
            }
        }

         
    }
}