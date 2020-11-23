using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Elearning.App_Code
{
    public class Modulescs
    {
        public int ModuleID { get; set; }

        public string ModuleCode { get; set; }

        public string ModuleName { get; set; }

        private DatabaseConnection dataConn;

        public Modulescs()
        {
            dataConn = new DatabaseConnection();
        }

        //public DataTable getModules()
        //{
        //   dataConn.addParameter("CourseID", CourseId);

        //    string command = "Select ModuleCode, ModuleName FROM Modules " +
        //        "INNER JOIN CourseModules ON Modules.ModuleID = CourseModules.ModuleID WHERE CourseID=@CourseID";
        //    return dataConn.executeReader(command);
        //}
    }
}