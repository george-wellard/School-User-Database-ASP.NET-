using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Elearning.App_Code
{
    public class CourseModules
    {
        public int ModuleID { get; set; }

        public int CourseID { get; set; }

        private DatabaseConnection dataConn; 

        public CourseModules()
        {
            dataConn = new DatabaseConnection();
        }

    }
}