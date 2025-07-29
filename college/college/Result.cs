using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace college
{
    public class Result
    {
        public string examType;
        public int? marks, courseId, studentId, marksoutof;


        public Result()
        {
            studentId = null;
            courseId = null;
            examType = "";
            marks = null;
            marksoutof = null;
        }

        public Result(int resultId)
        {
            string sql = "select * from result where resultId = " + resultId;
            MySqlConnection con = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    studentId = reader.GetInt32("studentId");
                    courseId = reader.GetInt32("CourseId");
                    examType = reader.GetString("examType");
                    marks = reader.GetInt32("marks");
                    marksoutof = reader.GetInt32("marksOutOf");   
                }
                dbConn.CloseConnection(con);
            }

        }

        /*public List<int> Studentresult
        {
            get { return GetAllResultsOfStudent(studentId); }
            set { }
        }*/

        public static List<Result> GetAllResultsOfStudent(int studentId)
        {
            List<Result> StudentResult = new List<Result>();

            string sql = "select * from result where studentId = " + studentId;
            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    StudentResult.Add(new Result(reader.GetInt32("resultId")));
                }
                dbConn.CloseConnection(conn);
            }
            return StudentResult;
        }



        //private List<int> GetAllResultsOfStudent(int? studentId)
        //{
        //    List<Result> StudentResult = new List<Result>();

        //    string sql = "select * from result where studentId = " + studentId;
        //    MySqlConnection conn = dbConn.OpenConnection();
        //    MySqlCommand cmd = new MySqlCommand(sql, conn);
        //    using (MySqlDataReader reader = cmd.ExecuteReader())
        //    {
        //        if (reader.Read())
        //        {
        //            Result result = new Result()
        //            {
        //                studentId = reader.GetInt32("StudentId"),
        //                courseId = reader.GetInt32("CourseId"),
        //                examType = reader.GetString("ExamType"),
        //                marks = reader.GetInt32("Marks"),
        //                marksoutof = reader.GetInt32("MarksOutOf"),

        //            };
        //            StudentResult.Add(result);
        //        }
        //        dbConn.CloseConnection(conn);
        //    }
        //    return StudentResult;
        //}


        /* public static int GetStudentresult(int? studentId)
         {
             List<Result> StudentResult =  new List<Result>();

             string sql = "select * from result where studentId = " + studentId;
             MySqlConnection conn = dbConn.OpenConnection();
             MySqlCommand cmd = new MySqlCommand(sql, conn);
             using (MySqlDataReader reader = cmd.ExecuteReader())
             {
                 if (reader.Read())
                 {
                     Result result = new Result()
                     {   
                         studentId = reader.GetInt32("StudentId"),
                         courseId = reader.GetInt32("CourseId"),
                         examType =reader.GetString("ExamType"),
                         marks = reader.GetInt32("Marks"),
                         marksoutof = reader.GetInt32("MarksOutOf"),

                     };
                     StudentResult.Add(result);
                 }
                 dbConn.CloseConnection(conn);
             }
             return StudentResult;

         }*/



        public void Addresult(int CourseId,string examType)
        {
            for (int i = 1; i <= CourseId; i++) 
            {
                Console.WriteLine("Enter Marks for StudentId = " + studentId + ":");
                marks = Convert.ToInt32(Console.ReadLine());
            }
        }
    


        public static void RemoveResultofStudent(int studentId)
        {
            string sql = "delete from college.result where studentId = " + studentId;
            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery(); 
            dbConn.CloseConnection(conn);
        }
    }
}



/*public class Result
    {
        public string examType;
        public int marks, courseId, studentId, marksoutof;


        public void Addresult(int courseId)
        {
            Console.WriteLine("\n## ADDING RESULT ##\n");
            //Console.WriteLine("   Enter CourseId:");
            //courseId = Convert.ToInt32(Console.ReadLine());
            Console.Write("\n   Exam Type:");
            examType = Console.ReadLine();
            Console.Write("\n   MarksOutof:");
            marksoutof = Convert.ToInt32(Console.ReadLine());

            bool var = true;
            while (var)
            {   
                Console.Write("\n   StudntId:");
                studentId = Convert.ToInt32(Console.ReadLine());
                Console.Write("\n   Marks Obtained:");
                marks = Convert.ToInt32(Console.ReadLine());
                //Think about Inner Joins
                string sql = "insert into college.Result(CourseId,StudentId,ExamType,Marks,Marksoutof) " +
                    "VALUES("+courseId+","+studentId+",'"+examType+"',"+marks+", "+marksoutof+")";

                MySqlConnection conn = dbConn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand(sql,conn);
                cmd.ExecuteNonQuery();
                dbConn.CloseConnection(conn);
            }
        }


        public static void RemoveResultofStudent(int studentId)
        {
            string sql = "delete from college.result where studentId = " + studentId;
            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery(); 
            dbConn.CloseConnection(conn);
        }
    }*/