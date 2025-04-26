using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortManagementSystem.BLL.Dto_s;
using PortManagementSystem.DAL.Models;

namespace PortManagementSystem.BLL.Managers
{
    public interface IAuthService
    {
        Task<TokenDTO> LoginAsync(LoginDTO loginDTO);
        string GenerateToken(User user);
    }
}
