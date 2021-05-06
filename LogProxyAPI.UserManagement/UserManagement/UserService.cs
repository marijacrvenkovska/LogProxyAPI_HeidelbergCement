using LogProxyAPI.Core.InMemoryStore;
using LogProxyAPI.Domain.Models;
using System.Linq;
using System.Threading.Tasks;

namespace LogProxyAPI.UserManagement.UserManagement
{
    public class UserService : IUserService
    {
        public Task<User> Authenticate(string username, string password)
        {
            return Task.FromResult(UserStore.GetAll().SingleOrDefault(x => x.Password == password && x.Username == username));            
        }
    }
}
