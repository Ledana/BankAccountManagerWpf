using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagerWpf.LogicCode
{
    public interface IUserRepository
    {
        //public List<User> Users { get; }
        public List<User> GetUsers();
        public User? ValidateUser(string userName, string password);
        public User? FindUserById(string id);
    }
}
