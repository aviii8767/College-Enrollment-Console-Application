using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace college
{
    internal class Semester

    {
        public int? semId, semNo, semYear;
        public string semDescription;

        public Semester() 
        {
            semId = null;
            semNo = null;
            semYear = null;
            semDescription = "";
        }
        public Semester(int id)
        {
            string sql = "select * from college.Semester where semId = " + id;
            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    semId = reader.GetInt32("semId");
                    semNo = reader.GetInt32("semNo");
                    semYear = reader.GetInt32("semYear");
                    semDescription = reader.GetString("semDescription");
                }
            }
            dbConn.CloseConnection(conn);
        }
        //Add New Semester
        public void AddNewSemester()
        {
            Console.WriteLine("\n   ## ADDING NEW SEMESTER ##\n");
            Console.Write("   Enter semyear:");
            semYear = Convert.ToInt32(Console.ReadLine());

            Console.Write("\n   Enter semNo(1-8):");
            semNo = Convert.ToInt32(Console.ReadLine());

            Console.Write("\n   Enter semDescription:");
            semDescription = Console.ReadLine();

            string sql = @"insert into college.Semester(SemYear,SemNo, SemDescription) 
                        values('" + semYear + "','" + semNo + "','" + semDescription + "')";

            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            dbConn.CloseConnection(conn);

            Console.WriteLine("\n   << Semester Added Successfully >>\n");  
        }

        //Show All Semester
        public void ViewSemesters()
        {
            List<Semester> Semesters = new List<Semester>();
            string sql = "select * from college.semester";
            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            Console.WriteLine("\n## SEMESTER LIST ##\n");
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    semId = reader.GetInt32("SemId");
                    semYear = reader.GetInt32("SemYear");
                    semNo = reader.GetInt32("SemNo");
                    semDescription = reader.GetString("SemDescription");

                    Semester sem = new Semester() {
                        semId = semId,
                        semYear = semYear,
                        semNo = semNo,
                        semDescription = semDescription 
                    };
                    Semesters.Add(sem);
                }
                dbConn.CloseConnection(conn);

                Console.WriteLine("   Total Semesters" + "(" + Semesters.Count() + ")\n");
                foreach(Semester s in Semesters)
                {
                    Console.WriteLine("   SemId: " + s.semId + "  " + "SemYear: " + s.semYear + "  " + "SemNo: " + s.semNo + "  " +
                        "SemDescription: " + s.semDescription + "\n");
                }
                Console.WriteLine();
            }
        }


        //View SEM
        public void ViewSemester()
        {
            Console.Write("\n   Enter SemesterId:");
            semId = Convert.ToInt32(Console.ReadLine());

            string sql = "select * from college.Semester where semId = " + semId;
            MySqlConnection conn = dbConn.OpenConnection(); 
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            using(MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    Console.WriteLine("\n   SemId:" + reader.GetInt32("SemId") + "\n\n" + "   SemYear:" + reader.GetInt32("semYear") + "\n\n" +
                        "   SemNo:" + reader.GetInt32("semNo") + "\n\n" +"   SemDescription:" + reader.GetString("semDescription") + "\n\n");
                }
                else Console.WriteLine("\n    Semester Not Exists with SemId = "+ semId + "\n");
            }
            dbConn.CloseConnection(conn);
        }

        //update Sem
        public void UpdateSemester()
        {
            Console.WriteLine("\n\n   ## UPDATING SEMESTER ##\n");

            if (this.semId > 0)
            {
                Console.Write("   New SemYear:");
                semYear = Convert.ToInt32(Console.ReadLine());

                Console.Write("\n   New SemNo:");
                semNo = Convert.ToInt32(Console.ReadLine());

                Console.Write("\n   New SemDescription:");
                semDescription = Console.ReadLine();

                string sql = "UPDATE college.Semester SET SemYear = '" + semYear + "', SemNo= '" + semNo + "', semDescription = '" +
                    semDescription + "' where semId = " + semId;

                MySqlConnection conn = dbConn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                dbConn.CloseConnection(conn);

                Console.WriteLine("\n   << Successfully Updated >>\n");       
            }
            else Console.WriteLine("\n    Semester You want to Update Not-Exists\n");
        }

        //Remove Sem
        public void RemoveSemester()
        {
            if (this.semId > 0) 
            {
                string sql = "delete from college.Semester where SemId = " + this.semId;
                MySqlConnection conn = dbConn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine("\n    << Successfully Removed >>\n");
                dbConn.CloseConnection(conn);
            }
            else Console.WriteLine("\n    Semester You want to Remove Not-Exists\n");    
        }
    }
}
