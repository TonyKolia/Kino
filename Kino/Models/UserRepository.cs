using Kino.Models.KinoDBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Kino.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly KinoDBContext db;

        public UserRepository(KinoDBContext db)
        {
            this.db = db;
        }

        public async Task<User> AddUser(User user)
        {
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
            return user;
        }

        public async Task DeteleUser(int id)
        {
            var userToDelete = db.Users.Where(u => u.Id == id).FirstOrDefault();
            db.Users.Remove(userToDelete);
            await db.SaveChangesAsync();
        }

        public async Task<User> GetUser(int? id, string username)
        {
            User user = null;
            if (id.HasValue)
            {
                user = await db.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            }

            if(!string.IsNullOrEmpty(username))
            {
                user = await db.Users.Where(u => u.Username == username).FirstOrDefaultAsync();
            }

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await db.Users.ToListAsync();
        }

        public async Task<User> UpdateUser(User user)
        {
            var userToUpdate = db.Users.Where(u => u.Id == user.Id).FirstOrDefault();
            if(userToUpdate != null)
            {
                userToUpdate.Birthdate = user.Birthdate;
                userToUpdate.Category = user.Category;
                userToUpdate.Email = user.Email;
                userToUpdate.Password = user.Password;
                userToUpdate.Username = user.Username;
                db.Entry(userToUpdate).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }

            return userToUpdate;
        }
    }
}
