using college;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace college
{
    public class SuperAdmin : User
    {
        public int? superAdminId;


        public SuperAdmin()
        {
            this.superAdminId = null;
            firstName = "";
            lastName = "";
            gender = "";
            emailId = "";
            addressId = 0;
            phoneNo = "";
        }

        public SuperAdmin(int superAdminId)
        {
            string sql = "select * from college.SuperAdmin where SuperAdminId = " + superAdminId;
            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    this.superAdminId = reader.GetInt32("superAdminId");
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

        public void ViewSuperAdmin()
        {
            Console.WriteLine("\n## SUPER ADMIN INFORMATION ##");
            Console.WriteLine("\n   AdminId: " + superAdminId + "\n\n" + "   Name: " + firstName + " " + lastName + "\n\n" + 
                "   PhoneNo:" + phoneNo + "\n\n" + "   Address:" + userAddress.GetFormatedAddressString() + "\n\n");
        }

        public void UpdateSuperAdmin()
        {           
            UpdateUser();

            string sql = "UPDATE college.SuperAdmin SET FirstName = '" + firstName + "'," +
                "LastName = '" + lastName + "'," + "PhoneNo = '" + phoneNo + "'," + "AddressID = '" +
                addressId + "'," + "EmailId = '" + emailId + "'," +
                "Gender = '" + gender + "' " + " where superAdminId = " + this.superAdminId;

            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            dbConn.CloseConnection(conn);

            Console.WriteLine("\n<< SUCCESSFULLY UPDATED >>\n");
        }
    }
}









/*bool var4 = true;
bool var5 = true;
while (var4)
{
    Console.Write("   Are you old Admin:1\n\n   Are you New Admin Enter:2\n\n   Enter:");
    int input = Convert.ToInt32(Console.ReadLine());
    Console.Clear();
    if (input == 1)
    {
        //old Admin
        Console.Write("   Enter Your AdminId:");
        adminId = Convert.ToInt32(Console.ReadLine());

        admin = new Admin(adminId);

        if (admin.adminId > 0)
        {
            Console.WriteLine("   Admin exists\n");
            var4 = false;
        }
        else
        {
            Console.WriteLine("   Admin not exist with AdminId = " + adminId + "\n\n");
            Console.Write("Try again? (yes/no)\nEnter:");
            if (Console.ReadLine().ToLower() != "yes")
            {
                Console.WriteLine("   << THANK YOU >>");
                var4 = false;
                var5 = false;
            }
            else
            {
                continue;
            }
        }
    }
    else if (input == 2)
    {
        //to register new admin
        admin = new Admin();
        admin.RegisterAdmin();
        adminId = Convert.ToInt32(admin.adminId);
        var4 = false;
    }
    else
    {
        Console.WriteLine("   Enter valid Input(1,2)");
    }
}*/