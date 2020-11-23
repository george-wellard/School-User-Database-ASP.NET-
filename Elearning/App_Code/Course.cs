using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Elearning.App_Code
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }

        private DatabaseConnection dataConn;

        public Course()
        {
            dataConn = new DatabaseConnection();
        }

        public DataTable getAllCourses()
        {
            string command = "Select * FROM Courses";
            return dataConn.executeReader(command);
        }

        public string getCourse()
        {
            dataConn.addParameter("@CourseID", CourseId);

            string command = "Select CourseName FROM Courses WHERE CourseID=@CourseID";

            DataTable table = dataConn.executeReader(command);

            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["CourseName"].ToString();
            }
            else
            {
                return "";
            }
        }

        public DataTable getModules()
        {
            dataConn.addParameter("CourseID", CourseId);

            // Joining the Module and CourseModules tables based on courseid. Making sure to only select modules based on courseid 
            string command = "Select ModuleCode, ModuleName FROM Modules " +
                "INNER JOIN CourseModules ON Modules.ModuleID = CourseModules.ModuleID WHERE CourseID=@CourseID";
            return dataConn.executeReader(command);
        }

        public bool SearchCourseByName()
        {
            dataConn.addParameter("CourseName", CourseName);

            string command = "Select COUNT(CourseName) FROM Courses WHERE CourseName=@CourseName";

            int result = dataConn.executeScalar(command); //result of count

            return result > 0 || result == -1; //if record found or exception caught
        }
    }
}