using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace college
{
    public class Address
    {
        public int _addressId;
        public string add1, add2, city, state, country;
        public int? zip;


        public Address()
        {
            add1 = "" ;
            add2 = "" ;
            city = "" ;
            state = "" ;
            country = "" ;
            zip = null ;
        }

        /// <summary>
        /// Creates an object of existing Address
        /// </summary>
        /// <param name="addressId"> Address id of User. </param>
        public Address(int  addressId)
        {
            //Populate Address here

            string sql = "select * from college.Address where AddressId =" + addressId;
            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    _addressId = reader.GetInt32("AddressId");
                    add1 = reader.GetString("Addr1");
                    add2 = reader.GetString("Addr2");
                    city = reader.GetString("city");
                    state = reader.GetString("state");
                    country = reader.GetString("country");
                    zip = reader.GetInt32("zip");
                }
                else Console.WriteLine("Wrong AddressId");
            }
            dbConn.CloseConnection(conn);  
        }

        //Adding New Address
        public  int AddNewAddress()
        {
            MySqlConnection conn = dbConn.OpenConnection();
            Console.WriteLine("\n## ENTER ADDRESS DETAILS ##\n");

            Console.Write("   Enter Add1: ");
            add1 = Console.ReadLine();

            Console.Write("\n   Enter Add2: ");
            add2 = Console.ReadLine();

            Console.Write("\n   Enter City: ");
            city = Console.ReadLine();

            Console.Write("\n   Enter State: ");
            state = Console.ReadLine();

            Console.Write("\n   Enter zip: ");
            zip = Convert.ToInt32(Console.ReadLine());

            Console.Write("\n   Enter Country: ");
            country = Console.ReadLine();

            string sql = @"insert into college.Address(Addr1,Addr2,City,State,Zip,Country)
                        VALUES('" + add1 + "','" + add2 + "','" + city + "','" + state + "','" + zip + "','" + country + "')";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            dbConn.CloseConnection(conn);

            _addressId = GetMaxAddress();
            return _addressId;
        }

        //Getting Max AddressID
        public  int GetMaxAddress()
        {
            int maxAddress = 0;
            string sql = "select max(addressid) from college.address ";

            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    maxAddress = reader.GetInt32(0);
                }
            }
            dbConn.CloseConnection(conn);
            _addressId = maxAddress;
            return maxAddress;         
        }

        public string GetFormatedAddressString()
        {
            return  add1 + "," +add2+ "," +city + "("+state+")"+ "-"+ zip;
        }

        public static void RemoveAddress(int AddId)
        {
            string sql = "delete from college.Address where AddressID = " + AddId;

            MySqlConnection conn = dbConn.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            dbConn.CloseConnection(conn);   
        }
    }
}
