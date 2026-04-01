using BankAccountManagerWpf.LogicCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagerWpf
{
    //hardcoded users with full name and password
    //inherits from user to set the paswword
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users =
                [
                new User("Bob Dylan") {Pasword = "Bob123"},
                new User("Amelia Aster") {Pasword = "Amelia123"},
                new User("Vivian Scott") {Pasword = "Vivian123"},
                new User("Luiza Griffin") {Pasword = "Luiza123"},
                new User("Franceska DeMarti") {Pasword = "Francesca123"},
                new User("Lory Marti") {Pasword = "Lory123"},
                new User("Laila Martini") { Pasword = "Laila123"},
                new User("Violet Vi") {Pasword = "Violet123"}
                ];
        //public List<User> Users { get { return _users; } }

        //when in the main window the user puts username and password this method checks in
        //hard coded users to find the first match
        public User? ValidateUser(string userName, string password)
        {
            return _users.FirstOrDefault(u => u.UserName.Equals(userName, System.StringComparison.OrdinalIgnoreCase) &&
            u.Pasword == password);
        }
        
        //when in the transfer window the user puts the id of the account
        //to transfer to, this methods checks in to find the user with that id
        public User? FindUserById(string id)
        {
            return _users.FirstOrDefault(u =>
            u.UserId == id);
        }

        public List<User> GetUsers()
        {
            throw new NotImplementedException();
        }
    }
}
