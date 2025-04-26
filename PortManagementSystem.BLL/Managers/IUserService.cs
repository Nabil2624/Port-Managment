using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortManagementSystem.BLL.Dto_s;

namespace PortManagementSystem.BLL.Managers
{
    public interface IUserService
    {
        void AddUser(UserDTO dto);
        void EditUser(int id, UserDTO dto);
        void RemoveUser(int id);

    }
}
