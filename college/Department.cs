using college;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace college
{   
    //Add New DEpartment
    internal class Department
    {
        public string deptName, deptHead, deptLocation;
        public int? deptId;


        public Department()
        {
            this.deptId = null;
            deptName = "";
            deptHead = "";
            deptLocation = "";
        }

        public Department(int deptId)
        {
            string sql = "select * from College.Department where DeptId = " + deptId;
            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    this.deptId = reader.GetInt32("DeptId");
                    deptName = reader.GetString("DeptName");
                    deptHead = reader.GetString("DeptHead");
                    deptLocation = reader.GetString("DeptLocation");
                }
            }
            dbConn.CloseConnection(conn);
        }

        //Add New Dept
        public void AddNewDepartment()
        {
            Console.WriteLine("\n## ADDING NEW DEPARTMENT ##");

            MySqlConnection conn = dbConn.OpenConnection();
            Console.Write("\n    Department Name:");
            deptName = Console.ReadLine();

            Console.Write("\n    Department Head:");
            deptHead = Console.ReadLine();

            Console.Write("\n    Department Location:");
            deptLocation = Console.ReadLine();

            string sql = @"insert into college.department(deptName, deptHead, deptLocation)
                                       VALUES('" + deptName + "', '" + deptHead + "', '" + deptLocation + "')";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            dbConn.CloseConnection(conn);

            deptId = GetMaxDepId();
            Console.WriteLine("\n    Sucessfully Deparment Added with DeptId = "+ deptId + "\n\n");       
        }

        //Udate Dept
        public void UpdateDepartment()
        {           
            if (this.deptId > 0)
            {
                Console.WriteLine("\n## Updating Department ##");
                Console.Write("\n    New Department Name:");
                deptName = Console.ReadLine();

                Console.Write("\n    New Department Head:");
                deptHead = Console.ReadLine();

                Console.Write("\n    New Department Location:");
                deptLocation = Console.ReadLine();

                string sql = "UPDATE college.Department SET DeptName = '" + deptName + "', DeptHead= '" +
                    deptHead + "', DeptLocation = '" + deptLocation + "' where DeptId = " + deptId;

                MySqlConnection conn = dbConn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                dbConn.CloseConnection(conn);

                Console.WriteLine("\n    << Successfully updated >>\n");               
            }
            else Console.WriteLine("    You are Updating Non-Existing Department");
            
        }

        //View All Departments
        public void ViewDepartmentList()
        {          
            Console.WriteLine("\n## ALL AVAILABLE DEPARTMENTS ##\n");
            List<Department> Departments = new List<Department>();
            string sql = "select * from college.Department ";
            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    deptId = reader.GetInt32("DeptID");
                    deptName = reader.GetString("DeptName");
                    deptHead = reader.GetString("DeptHead");
                    deptLocation = reader.GetString("DeptLocation");

                    Department Dept = new Department()
                    {
                        deptId = deptId,  
                        deptName = deptName, 
                        deptLocation = deptLocation,
                        deptHead = deptHead 
                    };
                    Departments.Add(Dept);
                }
                dbConn.CloseConnection(conn);

                
                foreach(Department d in Departments)
                {
                    Console.WriteLine("    DepartmentID:" + d.deptId + "   " + "Department Name:" + d.deptName +
                        "   " + "Head:" + d.deptHead + "   " + "Location:" + d.deptLocation + "\n");
                }
                Console.WriteLine("        :Total Departments" + "(" + Departments.Count() + ")" + "\n");
                Console.WriteLine();
            }
        }

        //View Department
        public  void ViewDept()
        {   
            if (this.deptId > 0)
            {
                Console.WriteLine("\n    DepartmentID:" + this.deptId + "\n\n" + "    Department Name:" + deptName +
                    "\n\n" + "    Department Head:" + deptHead + "\n\n" + "    Department Location:" + deptLocation + "\n\n");
            }
            else Console.WriteLine("\n   Department Not Exists\n");           
        }

        //Return Max DepartmentID
        public int GetMaxDepId()
        {
            string sql = "select max(DeptId) from college.Department";
            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand( sql, conn);

            int maxDeptId = 0;
            using(MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    maxDeptId = reader.GetInt32(0);
                }
                else Console.WriteLine("Error no department Available");
            }
            dbConn.CloseConnection(conn);
            return maxDeptId;
        }

        //remove Dept
        public void RemoveDept()
        {
            if (this.deptId > 0) 
            {
                string sql = "delete from college.Department where DeptId = " + this.deptId;
                MySqlConnection conn = dbConn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                dbConn.CloseConnection(conn);

                Console.WriteLine("\n   << Successfully Removed >>");               
            }
            else Console.WriteLine("\n   Department Not Exists you Want to Remove"); 
        }
    }
}




/*
//View Department
public void ViewDept()
{
    Console.Write("Enter DeptId:");
    deptId = Convert.ToInt32(Console.ReadLine());

    string sql = "select * from college.department where DeptId = " + deptId;
    MySqlConnection conn = dbConn.OpenConnection();
    MySqlCommand cmd = new MySqlCommand(sql, conn);

    using (MySqlDataReader reader = cmd.ExecuteReader())
    {
        if (reader.Read())
        {
            deptId = reader.GetInt32("DeptId");
            deptName = reader.GetString("DeptName");
            deptHead = reader.GetString("DeptHead");
            deptLocation = reader.GetString("deptLocation");

            Console.WriteLine("\n    DepartmentID:" + deptId + "\n\n" + "    Department Name:" + deptName +
                 "\n\n" + "    Department Head:" + deptHead + "\n\n" + "    Department Location:" + deptLocation + "\n\n");
        }
        else Console.WriteLine("\n    Department Not Exist with DeptId = " + deptId);
    }
    dbConn.CloseConnection(conn);
}*/
