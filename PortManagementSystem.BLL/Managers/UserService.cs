using PortManagementSystem.BLL.Dto_s;
using PortManagementSystem.BLL.Managers;
using PortManagementSystem.DAL.Models;
using PortManagementSystem.DAL.Repository;
using System.Security.Cryptography;
using System.Text;

namespace PortManagementSystem.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void AddUser(UserDTO dto)
        {
            CreatePasswordHash(dto.Password, out byte[] hash, out byte[] salt);

            var user = new User
            {
                username = dto.UserName,
                email = dto.Email,
                PasswordHash = hash,
                PasswordSalt = salt,
                role = "User"
            };

            _userRepository.AddUser(user);
        }

        public void AddAdmin(AdminDTO dto)
        {
            CreatePasswordHash(dto.Password, out byte[] hash, out byte[] salt);
            var admin = new User
            {
                username = dto.UserName,
                email = dto.Email,
                PasswordHash = hash,
                PasswordSalt = salt,
                role = "Admin"
            };
            _userRepository.AddUser(admin);
        }

        public void EditUser(int id, UserEditDTO dto)
        {
            var existingUser = _userRepository.GetById(id);
            if (existingUser == null) return;

            existingUser.username = dto.UserName;
            existingUser.email = dto.Email;
            existingUser.role = dto.Role;

            if (!string.IsNullOrEmpty(dto.Password))
            {
                CreatePasswordHash(dto.Password, out byte[] hash, out byte[] salt);
                existingUser.PasswordHash = hash;
                existingUser.PasswordSalt = salt;
            }

            _userRepository.UpdateUser(existingUser);
        }

        public void RemoveUser(int id)
        {
            var user = _userRepository.GetById(id);
            if (user != null)
            {
                _userRepository.DeleteUser(user);
            }
        }

        public void UserEditHisSelf(UserEditHisSelfDTO dto, int userId)
    {

        var existingUser = _userRepository.GetById(userId);
        if (existingUser == null) return;

        existingUser.username = dto.UserName;
        existingUser.email = dto.Email;


        if (!string.IsNullOrEmpty(dto.Password))
        {
            CreatePasswordHash(dto.Password, out byte[] hash, out byte[] salt);
            existingUser.PasswordHash = hash;
            existingUser.PasswordSalt = salt;
        }

        _userRepository.UpdateUser(existingUser);
    }

    public void UserRemoveHisSelf(int userId)
    {

        var user = _userRepository.GetById(userId);
        if (user != null)
        {
            _userRepository.DeleteUser(user);
        }
    }

        private void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using var hmac = new HMACSHA512();
            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

    }
}
