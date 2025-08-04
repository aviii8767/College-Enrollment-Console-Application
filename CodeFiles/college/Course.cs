using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace college
{
    public class Course
    {   

        public string courseName, courseDescription;
        public DateTime? startDt, endDate;
        public int? courseId, deptId, semId,teacherId;
        public List<int> enrolledStudents
        {
            get { return GetAllEnrolledStudentInCourse(courseId); }
            private set { }
        }

        private List<int> GetAllEnrolledStudentInCourse(int? courseId)
        {
            List<int> studentEnrolled = new List<int>();
            string sql = "select studentId from College.CourseEnrollment where CourseId = " + courseId;
            MySqlConnection con = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    studentEnrolled.Add(reader.GetInt32("studentId"));
                }
                dbConn.CloseConnection(con);
            }
            return studentEnrolled;
        }

        public Course()
        {
            courseId = null;
            courseName = "";
            startDt = null;
            endDate = null;
            courseDescription = "";
            deptId = null;
            semId = null;
            teacherId = null;

        }
        public Course(int crsId)
        {
            string sql = "select * from College.Course where CourseId = " + crsId;
            MySqlConnection con = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            using(MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    courseId = reader.GetInt32("CourseId");
                    courseName =  reader.GetString("CourseName");
                    startDt = reader.GetDateTime("startDt");
                    endDate = reader.GetDateTime("endDate");
                    courseDescription = reader.GetString("courseDescription");
                    deptId = reader.GetInt32("deptId");
                    semId = reader.GetInt32("semId");
                    teacherId = reader.GetInt32("teacherId");
                }
                dbConn.CloseConnection(con);
            }
            
        }

        //1.Add New Course
        public void AddNewCourse()
        {
            Console.WriteLine("\n## ADDING NEW COURSE ##\n");
            
            Console.Write("   Enter courseName: ");
            courseName = Console.ReadLine();

            Console.Write("   Enter startDt (yyyy/MM/dd format): ");
            string dateString = Console.ReadLine();
            startDt = DateTime.ParseExact(dateString, "yyyy/MM/dd", null);

            Console.Write("   Enter endDate (yyyy/MM/dd format): ");
            endDate = DateTime.ParseExact(Console.ReadLine(), "yyyy/MM/dd", null);

            Console.Write("   Enter courseDescription: ");
            courseDescription = Console.ReadLine();

            //choose Semester
            bool var = true;
            while (var)
            {   
                Semester sem = new Semester();
                sem.ViewSemesters();

                Console.Write("   Choose SemesterId from above:");
                int selectedSemId = Convert.ToInt32(Console.ReadLine());
                sem = new Semester(selectedSemId);
                if (sem.semId > 0)
                {
                    semId = selectedSemId;
                    var = false;
                }
                else Console.WriteLine("\n    You Enter Non-Existing SemesterId Enter Again\n");
            }

            //choose Department
            bool var1 = true;
            while (var1)
            {
                Department dept = new Department();
                dept.ViewDepartmentList();

                Console.Write("   choose DepartmentId from above options:");
                int selectedDeptId = Convert.ToInt32(Console.ReadLine());
                dept = new Department(selectedDeptId);

                if ( dept.deptId > 0)
                {
                    deptId = selectedDeptId;
                    var1 = false;
                }
                else Console.WriteLine("\n    You Enter Non-Existing DepartmentId Enter Again\n");
            }
            //Allocate teacher
            bool var2 = true;
            while (var2)
            {
                Teacher tchr = new Teacher();
                tchr.ViewTeachersList();

                Console.Write("   Select TeacherId from above List:");
                int selectedTeacherId = Convert.ToInt32(Console.ReadLine());
                tchr = new Teacher(selectedTeacherId);
                if (tchr.teacherId > 0)
                {   
                    teacherId = selectedTeacherId;
                    var2 = false;
                }
                else Console.WriteLine("\n    You Enter Non-Existing TeacherId Enter Again");
            }   
            string sql = "insert into college.course(courseName, StartDt, EndDate, courseDescription,deptId, semId, teacherId)" +
                            "VALUES('" + courseName + "','" + startDt?.ToString("yyyy/MM/dd") + "','" + endDate?.ToString("yyyy/MM/dd") +
                            "','" + courseDescription + "'," + deptId + "," + semId + "," + teacherId + ")";

            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            dbConn.CloseConnection(conn);

            Console.WriteLine("\n   << Course Added Sucessfully >>\n\n");           
        }

        //Update Course 
        public void UpdateCourse()
        {
            Console.WriteLine("\n## UPDATING COURSE ##\n");

            Console.Write("\n   Enter courseId you want to Update: ");
            courseId = Convert.ToInt32(Console.ReadLine());

            Console.Write("\n   Enter New courseName:");
            courseName = Console.ReadLine();

            Console.Write("\n   Enter New startDt (yyyy/MM/dd format):");
            string dateString = Console.ReadLine();
            startDt = DateTime.ParseExact(dateString, "yyyy/MM/dd", null);

            Console.Write("\n   Enter New endDate (yyyy/MM/dd format):");
            endDate = DateTime.ParseExact(Console.ReadLine(), "yyyy/MM/dd", null);

            Console.Write("\n   Enter New courseDescription:");
            courseDescription = Console.ReadLine();

            //choose Semester
            bool var = true;
            while (var)
            {
                Semester sem = new Semester();
                sem.ViewSemesters();
                Console.Write("\n   Choose New SemesterId from above:");
                int selectedSemId = Convert.ToInt32(Console.ReadLine());
                sem = new Semester(selectedSemId);
                if (sem.semId > 0)
                {
                    semId = selectedSemId;
                    var = false;
                }
                else Console.WriteLine("\n   You Enter Non-Existing SemesterId Enter Again");
            }

            //choose Department
            bool var1 = true;
            while (var1)
            {
                Department dept = new Department();
                dept.ViewDepartmentList();

                Console.Write("\n   choose New DepartmentId from above options: ");
                int selectedDeptId = Convert.ToInt32(Console.ReadLine());
                dept = new Department(selectedDeptId);

                if (dept.deptId > 0)
                {
                    deptId = selectedDeptId;
                    var1 = false;
                }
                else Console.WriteLine("\n   You Enter Non-Existing DepartmentId Enter Again");
            }
            //Allocate teacher
            bool var2 = true;
            while (var2)
            {
                Teacher tchr = new Teacher();
                tchr.ViewTeachersList();
                Console.Write("   Select New TeacherId from above List:");
                int selectedTeacherId = Convert.ToInt32(Console.ReadLine());
                tchr = new Teacher(selectedTeacherId);
                if (tchr.teacherId > 0)
                {
                    teacherId = selectedTeacherId;
                    var2 = false;
                }
                else Console.WriteLine("\n   You Enter Non-Existing TeacherId Enter Again");
            }

            string sql = "UPDATE college.Course SET CourseName = '" + courseName +
                "', StartDt = '" + startDt?.ToString("yyyy/MM/dd") + "', EndDate = '" + endDate?.ToString("yyyy/MM/dd") +
                "', DeptId = " + deptId + ", courseDescription = '" + courseDescription + "', SemId = " + semId +
                ", TeacherId = " + teacherId + " where CourseId = " + courseId;

            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            dbConn.CloseConnection(conn);

            Console.WriteLine("\n   << Successfully Updated Course >>\n");
        }

        //2.Show List of Courses Available
        public void ViewCoursesList()
        {               
            Console.WriteLine("\n## COURSES AVAILABLE ##:\n");
            string sql = "select * from college.course ";
            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            List<Course> Courses = new List<Course>();

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    courseId = reader.GetInt32("courseID");
                    courseName = reader.GetString("courseName");
                    deptId = reader.GetInt32("deptId");
                    semId = reader.GetInt32("semId");
                    teacherId = reader.GetInt32("teacherId");//Cant Cnvert null to int (When teacher Id is null in course)-------------------------------
                    startDt = reader.GetDateTime("startDt");
                    
                    Course crs = new Course() 
                    {
                        courseId = courseId, 
                        courseName = courseName, 
                        semId = semId,
                        teacherId = teacherId,
                        deptId = deptId,
                        startDt = startDt                  
                    };
                    Courses.Add(crs);

                }
                dbConn.CloseConnection(conn);
                Console.WriteLine("Total Courses" + "(" + Courses.Count() + ")\n");
                foreach (Course c in Courses) 
                {
                    Console.WriteLine("   courseId:" + c.courseId + "  Course Name:" + c.courseName +
                        "  DepartmentId:" + c.deptId + "  SemesterId:" + c.semId + "  TeacherId:" + c.teacherId +
                        "  StartDate:" + c.startDt?.ToString("yyyy/MM/dd")+ "\n");
                }
                Console.WriteLine();
            }
        }

        //Course Allocated 
        public void CourseAllocatedtoTeacher(int teacherId) 
        {
            string sql = "select * from college.course where teacherId ="+teacherId;
            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            List<Course> courses = new List<Course>();
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    Console.WriteLine("## COURSE ALLOCATED TO YOU ##\n");

                    while (reader.Read())
                    {   
                        Course course = new Course() 
                        {
                            courseId = reader.GetInt32("CourseId"),
                            courseName = reader.GetString("CourseName"),
                            startDt = reader.GetDateTime("StartDt"),
                            endDate = reader.GetDateTime("endDate") 
                        };
                        courses.Add(course);
                    }
                    Console.WriteLine("\nTotal Courses Aloocated" + "(" + courses.Count() + ")\n");
                    foreach(Course crs in courses)
                    {
                        Console.WriteLine("   courseId:" + crs.courseId + "   CourseName:"+crs.courseName+"   StartDt:"+crs.startDt+
                            "   EndDt:"+crs.endDate+"\n");
                    }
                    Console.WriteLine();
                                     
                }
                else Console.WriteLine("    Course Not Allocated to You\n");
                dbConn.CloseConnection(conn);
            }
        }

        //5.Remove Teacher From Course
        public static void RemoveTeacherfromCourse(int teacherId)
        {
            string sql = "update college.course set TeacherId = null where teacherId =" + teacherId;
            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql,conn);
            cmd.ExecuteNonQuery();
            dbConn.CloseConnection(conn);
        }

        //View Course
        public void ViewCourse()
        {
            if (this.courseId > 0) 
            {
                string sql = "select * from college.Course where CourseId = " + this.courseId;
                MySqlConnection conn = dbConn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        courseId = reader.GetInt32("courseID");
                        courseName = reader.GetString("courseName");
                        deptId = reader.GetInt32("deptId");
                        semId = reader.GetInt32("semId");
                        teacherId = reader.GetInt32("teacherId");
                        Console.WriteLine("\n   courseId: " + courseId + "   " + "Course Name: " + courseName + "   " +
                        "DepartmentId:" + deptId + "   " + "SemesterId:" + semId + "   " + "TeacherId:" + teacherId + "\n");
                    }
                    dbConn.CloseConnection(conn);
                } 
            }
            else Console.WriteLine("\n    Course you want to View Not-Exists \n");
        }       

        //Remove Course
        public void RemoveCourse()
        {
            if (this.courseId > 0) 
            {
                string sql = "delete from college.Course where CourseId = " + this.courseId;
                MySqlConnection conn = dbConn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                dbConn.CloseConnection(conn);

                Console.WriteLine("\n   << Course Removed SucessFully >>\n"); 
            }
            else Console.WriteLine("The Course You want to Remove Not-Exists\n");    
        }

        public static int getCourseIdfromTeacherId(int teacherId)
        {
            string sql = "select * from college.course where teacherId =" + teacherId;
            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    int courseId = reader.GetInt32(0);
                    dbConn.CloseConnection(conn);
                    return Convert.ToInt32(courseId);
                }
                else
                {
                    dbConn.CloseConnection(conn);
                    return 0;
                }

                }
            }
    }
}



/*public void ViewCourse()
        {
            Console.Write("\n    Enter CourseId:");
            int CrsId = Convert.ToInt32(Console.ReadLine());

            string sql = "select * from college.Course where CourseId = " + CrsId;
            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    courseId = reader.GetInt32("courseID");
                    courseName = reader.GetString("courseName");
                    deptId = reader.GetInt32("deptId");
                    semId = reader.GetInt32("semId");
                    teacherId = reader.GetInt32("teacherId");

                    Console.WriteLine("\n   courseId: " + courseId + "   " + "Course Name: " + courseName + "   " +
                        "DepartmentId:" + deptId + "   " + "SemesterId:" + semId + "   " + "TeacherId:" + teacherId + "\n");
                }
                else Console.WriteLine("\n    Course Not Exists With CourseId = " + courseId + "\n");
                dbConn.CloseConnection(conn);
            }            
        }       */