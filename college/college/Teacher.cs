using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace college
{
    public class Teacher : User
    {
        public int? teacherId;
        public int deptId,courseId;
        public List<int>  coursesTeaching
        {
            get { return GetCoursesTaughtByTeacher(); } 
        }

        private List<int> GetCoursesTaughtByTeacher()
        {
            List<int> coursesTeaching = new List<int>();
            string sql = "SELECT courseId from Course where teacherId=" + teacherId;
            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    coursesTeaching.Add(reader.GetInt32("courseId"));
                }

            }
            return coursesTeaching;
        }
        public Teacher()
        {
            this.teacherId = null;
            firstName = "";
            lastName = "";
            gender = "";
            emailId = "";
            addressId = 0;
            phoneNo = "";
            deptId = 0;
        }

        public Teacher(int teacherId)
        {
            string sql = "select * from college.Teacher where TeacherId = " + teacherId;

            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    this.teacherId = reader.GetInt32("TeacherId");
                    firstName = reader.GetString("FirstName");
                    lastName = reader.GetString("LastName");
                    phoneNo = reader.GetString("PhoneNo");
                    emailId = reader.GetString("EmailId");
                    gender = reader.GetString("Gender");
                    addressId = reader.GetInt32("AddressId");
                   // userAddress = new Address(addressId);
                    deptId = reader.GetInt32(deptId);
                }
            }
            dbConn.CloseConnection(conn);
        }
        

        //1.Register New Teacher
        public void RegisterTeacher()
        {          
            Console.WriteLine("## REGISTERING A NEW TEACHER ## ");
            NewUser();

            bool var1 = true;
            while (var1)
            {
                Department dept = new Department();
                dept.ViewDepartmentList();//To show Available Departments
                Console.Write("Enter Department Id from above list:");
                deptId = Convert.ToInt32(Console.ReadLine());
                dept = new Department(deptId);
                if (dept.deptId > 0)
                {
                    string sql = @"INSERT INTO college.teacher
                           (FirstName,LastName,PhoneNo,AddressId,EmailId,Gender,deptId)
                            VALUES('" + firstName + "', '" + lastName + "', '" + phoneNo + "', " + addressId +
                                ", '" + emailId + "', '" + gender + "', " + deptId + ")";

                    MySqlConnection conn = dbConn.OpenConnection();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    dbConn.CloseConnection(conn);

                    teacherId = GetMaxTeacherId();
                    Console.WriteLine("\n   Sucessfully registered New Teacher with TeacherId =" + teacherId + "\n\n");
                    var1 = false;
                }
                else Console.WriteLine("\n   You Entered Nonexisting DeptId Re-Enter it");
            }
                           
        }


        //2.View Teacher by taking TeacherId
        public void Viewteacher()
        {
            if (this.teacherId > 0)
            {
                Console.WriteLine("\n## TEACHER INFORMATION ##\n");
                Console.WriteLine("    TeacherID:" + teacherId + "\n\n" + "    Name:" + firstName +" " + lastName + "\n\n" +
                    "    DepartmentId:" + deptId + "\n\n" + "    Address:" + userAddress.GetFormatedAddressString() + "\n");
            }
             else Console.WriteLine("   Teacher Not Exists\n");     
        }

        //3.Udate Teacher information
        public void UpdateTeacher()
        {
            if (this.teacherId > 0)
            {
                UpdateUser();

                bool var = true;
                while (var)
                {
                    Department dept = new Department();
                    dept.ViewDepartmentList();//To show Available Departments
                    Console.Write("   Enter Department Id from above list:");
                    deptId = Convert.ToInt32(Console.ReadLine());
                    dept = new Department(deptId);
                    if (dept.deptId > 0)
                    {
                        string sql = "UPDATE college.Teacher SET FirstName = '" + firstName + "'," +
                            "LastName = '" + lastName + "'," + "PhoneNo = '" + phoneNo + "'," +
                            "AddressID = '" + addressId + "'," + "EmailId = '" + emailId + "'," + "Gender = '" + gender + "'," +
                            "DeptId =  " + deptId + " " + " where teacherId = " + this.teacherId;

                        MySqlConnection conn = dbConn.OpenConnection();
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                        dbConn.CloseConnection(conn);

                        Console.WriteLine("\n   << SUCCESSFULLY UPDATED >>\n ");
                        var = false;
                    }
                    else Console.WriteLine("\n   You Entered Nonexisting DeptId");
                }
            }
            else Console.WriteLine("\n   You Entered Nonexisting TeacherId");

        }

        //3.Show List of Teachers
        public void ViewTeachersList()
        {
            List<Teacher> teachers = new List<Teacher>();
            Console.WriteLine("\n## TEACHERS LIST ##");
            string sql = "select * from college.teacher ";

            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    teacherId = reader.GetInt32("TeacherId");
                    firstName = reader.GetString("FirstName");
                    lastName = reader.GetString("LastName");
                    deptId = reader.GetInt32("DeptId");

                    Teacher tchr = new Teacher
                    {
                        teacherId = teacherId,
                        firstName = firstName,
                        lastName = lastName,
                        deptId = deptId
                    };
                    teachers.Add(tchr);
                }
                dbConn.CloseConnection(conn);

                
                foreach (Teacher t in teachers)
                {
                    Console.WriteLine("   ID:" + t.teacherId + "  " + "Name:" + t.firstName + " " + t.lastName +
                        "  " + "DeptId:" + t.deptId + "\n");
                }
                Console.WriteLine("\n      :Total Teachers" + "(" + teachers.Count() + ")\n");
                Console.WriteLine();
            }
        }


        //4.Get Max TeacherId
        public static int GetMaxTeacherId()
        {
            int maxTeacherId = 0;
            string sql = "select max(TeacherId) from college.Teacher ";

            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    maxTeacherId = reader.GetInt32(0);
                }
                else
                {
                    Console.WriteLine("no one Teacher registered yet ");
                }
            }
            dbConn.CloseConnection(conn);
            return maxTeacherId;
            
        }

        //5.Course Allocated to Teacher
        public void CourseAllocated()
        {   
            Course crs = new Course();
            crs.CourseAllocatedtoTeacher(Convert.ToInt32(this.teacherId));
            courseId = Convert.ToInt32(crs.courseId);
        }

        //6.List Out student taken course of perticular teacher
        public void ListOfStudent()
        {            
            CourseEnrollment.studentTakenThisCourse(Convert.ToInt32(this.teacherId));
        }

        //7.Add Result 
        public void Result()
        {
            bool a = true;
            while (a) 
            {
                //Allocated Courses to Teacher
                string getCoursesql = "select * from college.course where teacherId = " + this.teacherId;
                MySqlConnection conn = dbConn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand(getCoursesql, conn);
                List<Course> CoursesAllocated = new List<Course>();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\n   Courses Allocated to You\n");
                    while (reader.Read())
                    {
                        int cId = reader.GetInt32("courseId");
                        string cName = reader.GetString("CourseName");
                        Course c = new Course()
                        {
                            courseId = cId,
                            courseName = cName
                        };

                        CoursesAllocated.Add(c);
                    }
                    dbConn.CloseConnection(conn);

                    foreach (Course cr in CoursesAllocated)
                    {
                        Console.WriteLine("   CourseId:" + cr.courseId + "  " + "CourseName:" + cr.courseName + "\n");
                    }
                }

                //select Course to add Result
                Console.Write("   Select course Id from Allocated Courses to Enter Result:");
                int selectedCourseId = Convert.ToInt32(Console.ReadLine());
                Console.Clear();

                if (CoursesAllocated.Any(c => c.courseId == selectedCourseId))
                {
                    //Exam Type
                    string examtype = "";
                    int? marksoutof = null;
                    bool var = true;
                    while (var)
                    {
                        Console.WriteLine("\n\nEXAM TYPE:\n");
                        Console.Write("   ~for practical Enter:1 \n\n   ~for assignments Enter:2 \n\n   ~for fieldwork Enter:3 \n\n" +
                            "   ~for final Enter:4 \n\n   Enter:");
                        int input = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();

                        switch (input) 
                        {
                            case 1:
                                examtype = "practical";
                                marksoutof = 20;
                                var = false;
                                break;
                            case 2:
                                examtype = "assignments";
                                marksoutof = 20;
                                var = false;
                                break;
                            case 3:
                                examtype = "fieldwork";
                                marksoutof = 20;
                                var = false;
                                break;
                            case 4:
                                examtype = "final";
                                marksoutof = 100;
                                var = false;
                                break;

                            default:
                                Console.WriteLine("\n  Enter valid Input for ExamType(1-4) Again\n");
                                break;
                        }
                    }


                    //give student list for that course
                    MySqlConnection studentconn = dbConn.OpenConnection();
                    string selectStudentsql = "select studentId from courseEnrollment where courseId = " + selectedCourseId;
                    MySqlCommand selctStudentcmd = new MySqlCommand(selectStudentsql, studentconn);
                    List<int> studentIds = new List<int>();
                    using (MySqlDataReader reader = selctStudentcmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            studentIds.Add(reader.GetInt32("studentId"));

                        }
                        dbConn.CloseConnection(studentconn);
                    }
                    foreach (int sid in studentIds)
                    {
                        bool var2 = true;
                        while (var2)
                        {
                            Console.Write("\n   Enter marks For StudentId = " + sid + " MarksOutOf:" + marksoutof + " ~");
                            int marks = Convert.ToInt32(Console.ReadLine());

                            if (marks >= 0 && marks <= marksoutof)
                            {
                                MySqlConnection insertConn = dbConn.OpenConnection();
                                string insertsql = "insert into result(courseId, studentid, ExamType, marks, MarksOutof) VALUES(" + selectedCourseId + ", " +
                                    sid + ",'" + examtype + "'," + marks + "," + marksoutof + ")";
                                MySqlCommand insertcmd = new MySqlCommand(insertsql, insertConn);
                                insertcmd.ExecuteNonQuery();
                                dbConn.CloseConnection(insertConn);
                                var2 = false;
                            }
                            else Console.WriteLine("   You are Entering Wrong Marks");
                        }
                    }
                    Console.WriteLine("\n<< SUCCESSFULLY ADDED RESULT FOR ALL STUDENTS >>");
                }
                else Console.WriteLine("Incorrect CourseId please select from Allocated Course only");
                //a = false;
            }
            
            
        } 

        //8.Remove Teacher
        public void RemoveTeacher()
        {
            if (this.teacherId > 0)
            {
                Course.RemoveTeacherfromCourse(Convert.ToInt32(this.teacherId));
                string sql = "delete from college.teacher where teacherId = " + this.teacherId;
                MySqlConnection conn = dbConn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                Address.RemoveAddress(addressId);
                dbConn.CloseConnection(conn);

                Console.WriteLine("\n   << SUCCESSFULLY TEACHER REMOVED >>\n\n");
            }
            else Console.WriteLine("   Teacher Not-Exists\n");
              
        }
    }
}
