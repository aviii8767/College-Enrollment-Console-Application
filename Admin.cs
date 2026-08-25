using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace college
{
    public class Admin : User
    {
        public int? adminId;


        public Admin()
        {
            this.adminId = null;
            firstName = "";
            lastName = "";
            gender = "";
            emailId = "";
            addressId = 0;
            phoneNo = "";
        }

        public Admin(int adminId)
        {

            string sql = "select * from college.Admin where AdminId = " + adminId;
            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    this.adminId = reader.GetInt32("AdminId");
                    firstName = reader.GetString("FirstName");
                    lastName = reader.GetString("LastName");
                    phoneNo = reader.GetString("PhoneNo");
                    emailId = reader.GetString("EmailId");
                    gender = reader.GetString("Gender");
                    addressId = reader.GetInt32("AddressId");
                                   
                }              
            }
            dbConn.CloseConnection(conn);
        }

        //1.Add New Admin
        public void RegisterAdmin()
        {           
            Console.WriteLine("\n## REGISTERING A NEW ADMIN ##");
            NewUser();

            string sql = @"INSERT INTO college.Admin(FirstName,LastName,PhoneNo,AddressId,EmailId,Gender)
                            VALUES('" + firstName + "','" + lastName + "','" + phoneNo + "'," + addressId + ",'"
                            + emailId + "','" + gender + "')";
            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            dbConn.CloseConnection(conn);

            adminId = GetMaxAdminId();
            Console.WriteLine("\n   Registered Successfully with AdminId = " + adminId + "\n");
        }

        //2.View Admin
        public void ViewAdmin()
        {
            if (this.adminId > 0)
            {
                Console.WriteLine("\n## ADMIN INFORMATION ##");
                Console.WriteLine("\n   AdminId: " + this.adminId + "\n\n" + "   Name: " + firstName + " " + lastName + "\n\n" +
                    "   PhoneNo:" + phoneNo + "\n\n" + "   Address:" + userAddress.GetFormatedAddressString() + "\n\n");
            }
            else Console.WriteLine("\n   Admin you want to view Not-Exists\n");      
        }

        //Update Admin
       public void UpdateAdmin()
        {
            if (this.adminId > 0) 
            {
                UpdateUser();

                string sql = "UPDATE college.Admin SET FirstName = '" + firstName + "'," + "LastName = '" + lastName + "'," +
                    "PhoneNo = '" + phoneNo + "'," + "AddressID = '" + addressId + "'," + "EmailId = '" + emailId + "'," +
                    "Gender = '" + gender + "' " + " where AdminId = " + this.adminId;

                MySqlConnection conn = dbConn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                dbConn.CloseConnection(conn);

                Console.WriteLine("\n   << SUCCESSFULLY UPDATED >>\n\n");        
            }
            else Console.WriteLine("\n   The Admin you Want to Update Not-Exists\n");       
        }       

        //Admin List
        public void ViewAdminsList()
        {
            string sql = "select * from college.Admin ";
            MySqlConnection conn = dbConn.OpenConnection();  
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            List<Admin> Admins = new List<Admin>();

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    adminId = reader.GetInt32("AdminId");
                    firstName = reader.GetString("FirstName");
                    lastName = reader.GetString("LastName");
                    phoneNo = reader.GetString("PhoneNo");
                    addressId = reader.GetInt32("AddressId");                                      

                    Admin ad = new Admin() 
                    {
                        adminId = adminId, 
                        firstName = firstName, 
                        lastName = lastName, 
                        phoneNo = phoneNo,
                        addressId = addressId 
                    };
                    Admins.Add(ad);
                }
                dbConn.CloseConnection(conn);
                if (Admins.Count() > 0)
                {
                    Console.WriteLine("\n## ADMIN LIST ##:\n");
                    Console.WriteLine("   Total Admins" + "(" + Admins.Count() + ")\n");
                    foreach (Admin A in Admins)
                    {
                        Address useraddress = new Address(A.addressId);
                        Console.WriteLine("   AdminId:" + A.adminId + "  " + "Admin Name:" + A.firstName + " " + A.lastName + "  " + "PhoneNo:" +
                            A.phoneNo + "  " + "Address:" + useraddress.GetFormatedAddressString() + "\n");
                    }
                    Console.WriteLine();
                }
                else Console.WriteLine("    Admin Not Exists\n\n");
                
            }
        }

        //Remove Admin
        public void RemoveAdmin()
        {
            if (this.adminId > 0)
            {
                string sql = "delete from college.Admin where AdminId = " + this.adminId;
                MySqlConnection conn = dbConn.OpenConnection();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                dbConn.CloseConnection(conn);

                Console.WriteLine("\n   << SUCCESSFULLY ADMIN REMOVED >>\n");
            }
            else Console.WriteLine("\n   Admin You want to remove Not-Exist\n"); 
        }

        //Get Max AdminId
        public static int GetMaxAdminId()
        {
            string sql = "SELECT max(AdminId) FROM college.Admin";
            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            int maxAdminId = 0;
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    maxAdminId = reader.GetInt32(0);
                }
                dbConn.CloseConnection(conn);
                return maxAdminId;
            }
        }
    }
}
