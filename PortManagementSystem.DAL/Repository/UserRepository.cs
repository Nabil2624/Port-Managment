using PortManagementSystem.DAL.Database;
using PortManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PortManagementSystem.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private ProgramContext _context;

        public UserRepository(ProgramContext context) {
            _context = context;
        }
        
        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public IQueryable<User> GetAll()
        {
            var found = _context.Users;
            return found;
        }

        public User GetById(int id)
        {
            var found = _context.Users.Find(id);
            return found;
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            var found = await _context.Users.FirstOrDefaultAsync(u => u.email == email);
            return found;
        }
    }
}
