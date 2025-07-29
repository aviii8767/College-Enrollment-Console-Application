using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace college
{   

    public class User
    {
        public string firstName, lastName, phoneNo, emailId, gender;
        public int addressId;
        private Address _address;
        public Address userAddress
        {
            get {
                    if (_address == null)
                    {
                      _address = new Address(this.addressId);
                    }
                return _address;
                }
            private set { }
        }


        public void NewUser()
        {
            Console.Write("\n   Enter FirstName:");
            firstName = Console.ReadLine();

            Console.Write("\n   Enter LastName:");
            lastName = Console.ReadLine();

            Console.Write("\n   Enter PhoneNo:");
            phoneNo = Console.ReadLine();

            Console.Write("\n   Enter EmailId:");
            emailId = Console.ReadLine();

            Console.Write("\n   Enter Gender:");
            gender = Console.ReadLine();

            addressId = userAddress.AddNewAddress();
        }

        public void UpdateUser() 
        {
            Console.WriteLine("\n## UPDATING INFORMATION ##");
            Console.Write("\n   Enter New FirstName:");
            firstName = Console.ReadLine();

            Console.Write("\n   Enter New LastName:");
            lastName = Console.ReadLine();

            Console.Write("\n   Enter New PhoneNo:");
            phoneNo = Console.ReadLine();

            Console.Write("\n   Enter New EmailId:");
            emailId = Console.ReadLine();

            Console.Write("\n   Enter Gender:");
            gender = Console.ReadLine();
            Console.WriteLine();
            
            addressId = userAddress.AddNewAddress();
        }
    }
}
