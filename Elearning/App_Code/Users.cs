using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Security.Cryptography;
using System.Web.Security;

namespace Elearning.App_Code
{
    public class Users
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string HashPassword { get; set; }
        public string Salt { get; set; }
        public string RealName { get; set; }
        public string EmailAddress { get; set; }
        public int RoleId { get; set; }
        public int CourseId { get; set; }

        private DatabaseConnection dataConn;

        public Users()
        {
            dataConn = new DatabaseConnection();
        }

        //  Creating hash
        private static string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);
            string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "SHA1");
            hashedPwd = String.Concat(hashedPwd, salt);
            return hashedPwd;
        }

        public bool authenticateUser()
        {
            dataConn.addParameter("@UserName", UserName);
            dataConn.addParameter("@UserPassword", UserPassword);

            string command = "Select UserID, UserPassword, RealName, EmailAddress, CourseID, RoleID, Salt FROM Users " +
                            "WHERE UserName=@UserName";

            DataTable table = dataConn.executeReader(command);
               

            if (table.Rows.Count > 0)               
            {
                // Creating hash from inputed password using the salt session 
                string InputPassword = CreatePasswordHash(UserPassword, table.Rows[0]["Salt"].ToString());

                // Grabbing the password as it exists in the storage 
                string ExistingPassword = table.Rows[0]["UserPassword"].ToString();

                // Making sure that both password types match 
                if (InputPassword.Equals(ExistingPassword))
                {
                    // Creating session objects 
                    HttpContext.Current.Session["UserID"] = table.Rows[0]["UserID"].ToString();
                    HttpContext.Current.Session["RealName"] = table.Rows[0]["RealName"].ToString();
                    HttpContext.Current.Session["EmailAddress"] = table.Rows[0]["EmailAddress"].ToString();
                    HttpContext.Current.Session["CourseID"] = table.Rows[0]["CourseID"].ToString();
                    HttpContext.Current.Session["RoleID"] = table.Rows[0]["RoleID"].ToString();

                    return true;
                }
                else
                {
                    return false;
                }
                
            }               
            else               
            {               
                return false;
           
            }

        }

        public bool userNameExists()
        {
            dataConn.addParameter("@UserName", UserName);


            string command = "Select COUNT(UserName) FROM Users WHERE UserName=@UserName";

            int result = dataConn.executeScalar(command); //result of count

            return result > 0 || result == -1; //if record found or exception caught
        }

        public bool addUser()
        {
            dataConn.addParameter("@UserName", UserName);
            dataConn.addParameter("@UserPassword", UserPassword);
            dataConn.addParameter("@RealName", RealName);
            dataConn.addParameter("@EmailAddress", EmailAddress);
            dataConn.addParameter("@CourseID", CourseId);
            dataConn.addParameter("@RoleID", RoleId);
            dataConn.addParameter("@Salt", Salt);

            // Inserting new properties into data table
            string command = "INSERT INTO Users (UserName, UserPassword, RealName, EmailAddress, CourseID, RoleID, Salt) " +
                            "VALUES (@UserName, @UserPassword, @RealName, @EmailAddress, @CourseID, @RoleID, @Salt)";

            return dataConn.executeNonQuery(command) > 0; //i.e. 1 or more rows affected
        }

        public string getPasswordWithID()
        {
            dataConn.addParameter("@UserID", UserId);

            string command = "Select UserPassword FROM Users WHERE UserID=@UserID";

            DataTable table = dataConn.executeReader(command);

            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["UserPassword"].ToString(); // Getting password string stored in table 
            }
            else
            {
                return "";
            }
        }

        public bool updatePasswordByUserId()
        {
            dataConn.addParameter("@UserPassword", UserPassword);
            dataConn.addParameter("@UserID", UserId);

            string command = "Update Users Set UserPassword=@UserPassword WHERE UserID=@UserID";

            return dataConn.executeNonQuery(command) > 0; //i.e. 1 or more rows affected
        }

        public bool updateCourse()
        {
            dataConn.addParameter("@CourseID", CourseId);
            dataConn.addParameter("@UserID", UserId);

            string command = "Update Users Set CourseID=@CourseID WHERE UserID=@UserID";

            return dataConn.executeNonQuery(command) > 0; //i.e. 1 or more rows affected
        }

        public bool deleteStudent()
        {
            dataConn.addParameter("@UserID", UserId);

            string command = "DELETE FROM Users WHERE UserID=@UserID";
            return dataConn.executeNonQuery(command) > 0;
        }

        public string getTutorEmail()
        {
            dataConn.addParameter("@UserID", UserId);

            string command = "Select EmailAddress FROM Users WHERE UserID=@UserID";

            DataTable table = dataConn.executeReader(command);

            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["EmailAddress"].ToString(); // Getting email address string stored in table
            }
            else
            {
                return "";
            }
        }

        public DataTable getStudents()
        {
            dataConn.addParameter("@CourseID", CourseId);

            string command = "Select * FROM Users WHERE CourseID=@CourseID AND RoleID=1";
            return dataConn.executeReader(command);
        }

        public DataTable getTutors()
        {
            dataConn.addParameter("@CourseID", CourseId);

            string command = "Select * FROM Users WHERE CourseID=@CourseID AND RoleID=2";
            return dataConn.executeReader(command);
        }

    }
}