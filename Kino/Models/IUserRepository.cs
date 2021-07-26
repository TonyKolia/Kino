using Kino.Models.KinoDBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kino.Models
{
    public interface IUserRepository
    {
        public Task<User> GetUser(int? id, string username);
        public Task<User> AddUser(User user);
        public Task<IEnumerable<User>> GetUsers();
        public Task<User> UpdateUser(User user);
        public Task DeteleUser(int id);
        public Task<bool> UserExists(string username, string email);
        public Task<bool> ValidateUserCredentials(string username, string password);
    }
}
