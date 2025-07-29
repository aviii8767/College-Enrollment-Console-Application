using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace college
{
    public class Student : User
    {
        public int? studentId;
        private List<Result> _studentResults;
        public List<int> coursesEnrolled
        {
            get { return GetAllCoursesEnrolledForStudent(this.studentId); }
            private set { }

        }
        public List<Result> StudentResults
        {
            get { return GetStudentresut(); }
            private set { }

        }

        private List<Result> GetStudentresut()
        {
            if (_studentResults == null)
            {
                return Result.GetAllResultsOfStudent(Convert.ToInt32(studentId));
            }
            else
            {
                return _studentResults;
            }
        }

        private List<int> GetAllCoursesEnrolledForStudent(int? studentId)
        {
            List<int> coursesEnrolled = new List<int>();    
            string sql = "select ce.courseId " +
                "from courseEnrollment ce where StudentId = " + studentId;

            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            List<Course> Courses = new List<Course>();
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        coursesEnrolled.Add(reader.GetInt32("courseId"));
                    }
                    dbConn.CloseConnection(conn);

                   
                }
               
            }
            return coursesEnrolled;
        }

        public Student() {
            this.studentId = null;
            firstName = "";
            lastName = "";
            gender = "";
            emailId = "";
            addressId = 0;
            phoneNo = "";
        }

        public Student(int studentId)
        {
            string sql = "select * from college.student where studentId = " + studentId;
            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    this.studentId = reader.GetInt32("studentId");
                    firstName = reader.GetString("FirstName");
                    lastName = reader.GetString("LastName");
                    phoneNo = reader.GetString("PhoneNo");
                    emailId = reader.GetString("EmailId");
                    gender = reader.GetString("Gender");
                    addressId = reader.GetInt32("AddressId");
                    //userAddress = new Address(addressId);
                }
            }
            dbConn.CloseConnection(conn);
        }

        //1.Register New Student
        public void RegisterStudent()
        {           
            Console.WriteLine("\n## REGISTERING A NEW STUDENT ##");
            NewUser();

            string sql = @"INSERT INTO college.student
                           (FirstName,LastName,PhoneNo,AddressId,EmailId,Gender)
                            VALUES('" + firstName + "','" + lastName + "','" + phoneNo + "'," + addressId +
                            ",'" + emailId + "','" + gender + "')";
            
            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            dbConn.CloseConnection(conn);

            studentId = GetMaxStudentId();
            Console.WriteLine("\nRegistered Successfully with StuddentId = " + studentId + "\n");
        }

        //2.View Student
        public void ViewStudent()
        {
            
            if(this.studentId > 0)
            {
                Console.WriteLine("\n## STUDENT INFORMATION ##");
                Console.WriteLine("\n   StudentID: " + studentId + "\n\n" + "   Name: " + firstName + " " +
                    lastName + "\n\n" + "   PhoneNo:" + phoneNo + "   Address:" + userAddress.GetFormatedAddressString() + "\n\n");                                                  
            }
            else
            {
                Console.WriteLine("\n   May be you entered Non-Existing StudentId\n");
            }
        }

        //3.Show Courses Enrolled
        public void StudentCourseEnrolled()
        {
            CourseEnrollment cEnroll = new CourseEnrollment();
            cEnroll.ViewCoursesEnrolled(Convert.ToInt32(this.studentId));
        }


        //4.Udate student information
        public void UpdateStudent()
        {
            if (this.studentId > 0)
            {
                UpdateUser();

                string sql = "UPDATE college.Student SET FirstName = '" + firstName + "'," + "LastName = '" + lastName +
                    "'," + "PhoneNo = '" + phoneNo + "'," + "AddressID = '" + addressId + "'," + "EmailId = '" +
                    emailId + "'," + "Gender = '" + gender + "' " + " where studentId = " + this.studentId;

                MySqlConnection conn = dbConn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                dbConn.CloseConnection(conn);

                Console.WriteLine("\n   <<SUCCESSFULLY UPDATED>> \n\n");
            }
            else Console.WriteLine("   Student You Want to update Not-Exists\n");
                      
        }

        //5.Get Max studentId
        public static int GetMaxStudentId()
        {   
            int maxStudentId = 0;
            string sql = "select max(studentId) from college.student ";
            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    maxStudentId = reader.GetInt32(0);
                }
            }
            dbConn.CloseConnection(conn);
            return maxStudentId;            
        }

        //6.Remove Student
        public void RemoveStudent()
        {
            if(this.studentId > 0)
            {
                Result.RemoveResultofStudent(Convert.ToInt32(this.studentId));
                CourseEnrollment.RemoveEnrollment(Convert.ToInt32(this.studentId));

                string sql = "delete from college.student where studentId = " + this.studentId;
                MySqlConnection conn = dbConn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                Address.RemoveAddress(addressId);
                dbConn.CloseConnection(conn);

                Console.WriteLine("\n   << SUCCESSFULLY REMOVED >>\n");
            }
            else Console.WriteLine("\n   Student you want to Remove Not Exists\n");    
        }


        //7.List of students
        public void ViewStudentsList()
        {           
            Console.WriteLine("\n## STUDENTS LIST ##:\n");
            string sql = "select * from college.Student ";

            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            List<Student> students = new List<Student>();
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    studentId = reader.GetInt32("StudentId");
                    firstName = reader.GetString("FirstName");
                    lastName = reader.GetString("LastName");
                    phoneNo = reader.GetString("PhoneNo"); 
                    addressId = reader.GetInt32("addressId");
                    
                    Student student = new Student(Convert.ToInt32(studentId)) 
                    { 
                        studentId = studentId, 
                        firstName = firstName, 
                        lastName = lastName, 
                        phoneNo = phoneNo  
                    };
                    students.Add(student);
                }
                dbConn.CloseConnection(conn);

                Console.WriteLine("Total Students" + "(" + students.Count() + ")\n");
                foreach (Student student in students)
                {   
                    Address adr = new Address(student.addressId);
                    Console.WriteLine("   Id:" + student.studentId + "  " + "Name:" +student.firstName+ " " + student.lastName + "  " +
                        "PhoneNo:" + student.phoneNo + "  " + "Address:" + adr.GetFormatedAddressString() + "\n"  );
                }
            } 
        }


        //Geting RESULT

        public void ViewResult()
        {
            Console.WriteLine("## Your Result ##\n");
            foreach(Result rslt in StudentResults)
            {
                Console.WriteLine("CourseId:{0}  ExamType:{1}   Marks:{2}   MarksOutOf:{3}", rslt.courseId, rslt.examType,
                    rslt.marks, rslt.marksoutof);
            }
        }
    }
}
