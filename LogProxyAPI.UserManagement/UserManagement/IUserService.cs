using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogProxyAPI.Domain.Models;
using LogProxyAPI_HeidelbergCement.Models;

namespace LogProxyAPI.UserManagement.UserManagement
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
    }
}
