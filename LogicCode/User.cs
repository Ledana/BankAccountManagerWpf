using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagerWpf
{
    public class User
    {
        public string? FullName { get; set; }
        public string UserId = "123456789";
        private static int _count = 0;
        public string? UserName { get; set; }
        internal string? Pasword { get; set; }
        public BankAccount? BankAccount { get; set;}

        public User(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("The name can't be empty");

            //we put the name when creating a new user via ctor
            FullName = fullName;

            //userid keeps getting generated for every new user created
            int userId = Convert.ToInt32(UserId);
            _count++;
            UserId = (userId + _count).ToString();

            //username is created using the surname and 4 last digits of the ID
            int index = FullName.IndexOf(" ");
            UserName = FullName.Substring(index + 1) + UserId.Substring(UserId.Length - 4);

            //for every new user created a bank account is created for that user with
            //the id of the user
            BankAccount = new(this.UserId);
        }

        public User(string userId, string userName, string fullName, string password, BankAccount bankAccount)
        {
            UserId = userId;
            UserName = userName;
            FullName = fullName;
            Pasword = password;
            BankAccount = bankAccount;
        }
    }
}
