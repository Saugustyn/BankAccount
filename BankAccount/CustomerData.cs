using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    internal class CustomerData
    {
        public string FirstName { get; }
        public string LastName { get; }
        public long Pesel { get; }
        public string Username { get; }
        public string Password { get; }

        public CustomerData(string firstName, string lastName, string pesel, string username, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Pesel = long.Parse(pesel);
            Username = username;
            Password = password;

        }
    }
}
