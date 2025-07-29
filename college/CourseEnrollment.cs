using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace college
{
    public class CourseEnrollment
    {
        int studentId, courseId;
        bool var = true;

        //Enroll For New Course
        public void EnrollNewCourse(int id)
        {
            studentId = id;
            while (var)
            {
                Course crs = new Course();
                crs.ViewCoursesList();
                Console.Write("\n   Select CourseId to Enroll:");
                courseId = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                crs = new Course(courseId);
                if (crs.courseId > 0)
                {
                    int enrolled = CheckEnrollment();//Check if already enrolled

                    if (enrolled == 0)
                    {
                        //Enroll 
                        string sql = "insert into college.courseEnrollment(StudentId,CourseId) " +
                            "VALUES(" + studentId + "," + courseId + ")";
                        MySqlConnection conn = dbConn.OpenConnection();
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                        dbConn.CloseConnection(conn);

                        Console.WriteLine("\n   *** SUCESSFULLY ENROLLED *** \n");
                        var = false;
                    }
                    else
                    {
                        Console.WriteLine("\n   Already Enrolled CourseId:" + courseId + "\n\n");
                        var = false;
                    }
                }
                else Console.WriteLine("\n   You Enter Non-existing CourseId");
            }
        }

        public int CheckEnrollment()
        {
            string sql = "SELECT * FROM college.courseenrollment where studentId = " + studentId + " AND CourseID = " + courseId + " ";
            MySqlConnection conn = dbConn.OpenConnection();
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.ExecuteNonQuery();
                dbConn.CloseConnection(conn);
                return count;
            }
        }

        //Show Courses Enrolled by perticular Student
        public void ViewCoursesEnrolled(int id)
        {
            Student student = new Student(id);
            Console.WriteLine("## ENROLLED COURSES: ##\n");
                if(student.coursesEnrolled.Count > 0)
                { 
                    foreach (int cNum in student.coursesEnrolled)
                    {
                        Course c = new Course(cNum);
                        Teacher tchr = new Teacher(Convert.ToInt32(c.teacherId)); 
                        string TeacherName = tchr.firstName + " " + tchr.lastName;

                        Semester semm = new Semester(Convert.ToInt32(c.semId));
                        int semYr = Convert.ToInt32(semm.semYear);
                        int semNo = Convert.ToInt32(semm.semNo);
                        string semDesc = semm.semDescription;

                        Console.WriteLine("   courseId:" + c.courseId + "   CourseName:" + c.courseName + "   Teacher Name:" +
                            TeacherName + "   SemYear:" + semYr + "   SemNo:" + semNo + "   semDescription:" + semDesc + " \n");
                    }
                    Console.WriteLine();
                }
                else Console.WriteLine("\n   Not Enrolled for any Course\n");
            
        }

        //remove enrollment

        public static void RemoveEnrollment(int studentId)
        {
            string sql = "delete from college.CourseEnrollment where studentId = " + studentId;
            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            dbConn.CloseConnection(conn);
        }


        //Student List As per Course
        public static void studentTakenThisCourse(int teacherId)
        {
            //List<Student> StudentList = new List<Student>();

            //string sql = "SELECT std.StudentId as sId,std.FirstName as sFirstName,std.LastName as sLastName," +
            //    "std.Phoneno as sPhoneNo,std.EmailId as sEmailId,std.Gender as sGender,std.AddressId as saddress " +
            //    "FROM college.Teacher AS t " +
            //    "inner join Course AS co ON  t.TeacherId = co.TeacherId " +
            //    "inner join CourseEnrollment AS ce ON co.courseId = ce.CourseId " +
            //    "inner join student AS std ON ce.StudentId = std.StudentId WHERE t.TeacherId = " + teacherId;

            //MySqlConnection conn = dbConn.OpenConnection();
            //MySqlCommand cmd = new MySqlCommand(sql, conn);

            //Console.WriteLine("## STUDENT ENROLLED FOR YOUR COURSE ##\n");
            //using (MySqlDataReader reader = cmd.ExecuteReader())
            //{
            //    while (reader.Read())
            //    {
            //        Student st = new Student()
            //        {
            //            studentId = reader.GetInt32("sId"),
            //            firstName = reader.GetString("sFirstName"),
            //            lastName = reader.GetString("sLastName"),
            //            phoneNo = reader.GetString("sPhoneNo"),
            //            emailId = reader.GetString("sEmailId"),
            //            gender = reader.GetString("sGender"),
            //            addressId = reader.GetInt32("saddress")

            //        };
            //        StudentList.Add(st);
            //    }
            //    dbConn.CloseConnection(conn);
            //    Console.WriteLine("Total Student Enrolled For Your Course:" + StudentList.Count() + "\n");
            //    foreach (Student student in StudentList)
            //    {
            //        Address adr = new Address(student.addressId);

            //        Console.WriteLine("    ID:" + student.studentId + "    Name:" + student.firstName + " " +
            //            student.lastName + "    Phone:" + student.phoneNo + "    Address:" + adr.GetFormatedAddressString() + "\n");
            //    }
            //}
            Console.WriteLine("## STUDENT ENROLLED FOR YOUR COURSE ##\n");
            Teacher tch = new Teacher(teacherId);

            foreach(int courseId in tch.coursesTeaching)
            {
                Course courseTeaching = new Course(courseId);
                
                foreach(int studentIds in courseTeaching.enrolledStudents)
                {
                    Student student = new Student(studentIds);
                    Console.WriteLine("    studentID:" + student.studentId + " Name:" + student.firstName + " " +
                        student.lastName + " Phone:" + student.phoneNo + " Address:" + 
                        student.userAddress.GetFormatedAddressString() + "\n");
                }
                Console.WriteLine("\n        :Total {0} Student Enrolled for your CourseId: {1}\n",
                    courseTeaching.enrolledStudents.Count(), courseId);
            }


        }

    }
}