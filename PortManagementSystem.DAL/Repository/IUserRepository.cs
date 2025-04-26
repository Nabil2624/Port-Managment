using PortManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.DAL.Repository
{
    public interface IUserRepository
    {
        public void AddUser(User user);
        public void UpdateUser(User user);
        public void DeleteUser(User user);
        public IQueryable<User> GetAll();
        public User GetById(int id);
        public Task<User> GetUserByEmailAsync(string email);
    }
}
