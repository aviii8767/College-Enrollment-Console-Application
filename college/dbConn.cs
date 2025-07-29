using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace college
{
    public class dbConn
    {
        
        public static MySqlConnection OpenConnection()
        {
            string cs = "Server=localhost;Database=college;Uid=root;Pwd=adminPass@123; ";
            MySqlConnection conn = new MySqlConnection(cs);
            conn.Open();
            return conn;
        }

        public static void CloseConnection(MySqlConnection conn)
        {
            conn.Close();
        }
    }
}
